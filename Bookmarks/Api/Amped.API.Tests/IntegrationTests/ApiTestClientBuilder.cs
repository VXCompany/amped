using System.Net.Http;

namespace Amped.API.Tests.IntegrationTests;

public class ApiTestClientBuilder
{
    public HttpClient Build()
    {   
        var factory = new TestWebApplicationFactory();
        return factory.CreateClient();
    }
}