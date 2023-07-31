using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json.Serialization;

namespace Amped.Functions;

public static class GetUrlInfo
{
    [FunctionName("GetUrlInfo")]
    public static async Task<IActionResult> RunAsync(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]
        HttpRequest req, ILogger log)
    {
        log.LogInformation("GetUrlInfo HTTP trigger function processed a request.");

        try
        {
            // Authentication in JWT / Bearer scenarios
            var accessToken = GetAccessToken(req);
            if (accessToken.IsNullOrEmpty())
            {
                return new UnauthorizedResult();
            }

            var identity = await ValidateAccessTokenAsync(accessToken);

            // Authentication in App Service Easy Auth scenarios
            // var identity = ClaimsPrincipalParser.Parse(req);

            var isAuthenticated = identity is {Identity.IsAuthenticated: true};
            if (isAuthenticated)
            {
                req.HttpContext.User.AddIdentities(identity.Identities);
            }
            else
            {
                return new UnauthorizedResult();
            }

            // At this point the caller is authenticated
            // and we can gather the claims information
            var claimsList = req.HttpContext.User.Claims.ToList();

            // We now know who or what we are dealing with
            // so we have identity information that can be used for further authorization
            // and we can also use the claims information to make decisions

            // Your actual function code goes here
            string url = req.Query["url"];
            if (string.IsNullOrWhiteSpace(url))
            {
                return new BadRequestObjectResult("Please pass a url on the query string");
            }

            var uri = new Uri(url);

            var webInfo = await WebScraper.GetWebInfoAsync(uri);

            return new OkObjectResult(webInfo);
        }
        catch (HttpRequestException e)
        {
            return new NoContentResult();
        }
        catch (Exception e)
        {
            log.LogError(e, "Error validating request");
          
            return new UnauthorizedResult();
        }
    }

    private static async Task<ClaimsPrincipal> ValidateAccessTokenAsync(string accessToken)
    {
        var audience = Environment.GetEnvironmentVariable("Audience");
        var issuer = Environment.GetEnvironmentVariable("Issuer");

        var configManager =
            new ConfigurationManager<OpenIdConnectConfiguration>(
                $"{issuer}.well-known/openid-configuration",
                new OpenIdConnectConfigurationRetriever());

        var config = await configManager.GetConfigurationAsync();
        var tokenValidator = new JwtSecurityTokenHandler();

        var validationParameters = new TokenValidationParameters
        {
            ValidAudiences = audience?.Split(","),
            ValidIssuers = issuer?.Split(","),
            ValidateIssuer = true,
            IssuerSigningKeys = config.SigningKeys
        };

        var claimsPrincipal = tokenValidator.ValidateToken(accessToken, validationParameters, out _);
        return claimsPrincipal;
    }

    private static string GetAccessToken(HttpRequest req)
    {
        var authorizationHeader = req.Headers?["Authorization"];
        var parts = authorizationHeader?.ToString().Split(null) ?? Array.Empty<string>();
        if (parts.Length == 2 && parts[0].Equals("Bearer"))
            return parts[1];
        return null;
    }
}

public static class WebScraper
{
    private static readonly HttpClient HttpClient = new();

    public static async Task<WebInfoResource> GetWebInfoAsync(Uri uri)
    {
        var htmlDocument = new HtmlDocument();

        var httpResponse = await HttpClient.GetAsync(uri);
        await EnsureSuccessStatusCodeAsync(httpResponse);

        var pageHtml = await httpResponse.Content.ReadAsStringAsync();
        htmlDocument.LoadHtml(pageHtml);

        var titleNode = htmlDocument.DocumentNode.SelectSingleNode("//title");

        var response = new WebInfoResource
        {
            Title = titleNode?.InnerText,
            Url = uri.ToString()
        };

        return response;
    }

    private static async Task EnsureSuccessStatusCodeAsync(HttpResponseMessage httpResponse)
    {
        if (httpResponse.IsSuccessStatusCode)
            return;

        var statusCode = httpResponse.StatusCode;
        var httpContent = await httpResponse.Content.ReadAsStringAsync();
        var reasonPhrase = httpResponse.ReasonPhrase;

        throw new HttpRequestException(
            $"The HTTP status code of the response was not expected ({statusCode}): {reasonPhrase} - {httpContent}");
    }
}

public sealed class WebInfoResource
{
    public string Url { get; set; }
    public string Title { get; set; }
}

public static class ClaimsPrincipalParser
{
    private class ClientPrincipalClaim
    {
        [JsonPropertyName("typ")] public string Type { get; set; }
        [JsonPropertyName("val")] public string Value { get; set; }
    }

    private class ClientPrincipal
    {
        [JsonPropertyName("auth_typ")] public string IdentityProvider { get; set; }
        [JsonPropertyName("name_typ")] public string NameClaimType { get; set; }
        [JsonPropertyName("role_typ")] public string RoleClaimType { get; set; }
        [JsonPropertyName("claims")] public IEnumerable<ClientPrincipalClaim> Claims { get; set; }
    }

    public static ClaimsPrincipal Parse(HttpRequest req)
    {
        var principal = new ClientPrincipal();

        if (req.Headers.TryGetValue("x-ms-client-principal", out var header))
        {
            var data = header[0];
            var decoded = Convert.FromBase64String(data);
            var json = Encoding.UTF8.GetString(decoded);
            principal = JsonSerializer.Deserialize<ClientPrincipal>(json,
                new JsonSerializerOptions {PropertyNameCaseInsensitive = true});
        }

        var identity = new ClaimsIdentity(principal.IdentityProvider,
            principal.NameClaimType, principal.RoleClaimType);
        identity.AddClaims(principal.Claims.Select(c => new Claim(c.Type, c.Value)));

        return new ClaimsPrincipal(identity);
    }
}