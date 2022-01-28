using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;

namespace Amped.API.Tests.IntegrationTests
{
    public class ApiTestClientBuilder
    {
        public HttpClient Build()
        {
            var factory = new WebApplicationFactory<Startup>();
            return factory.CreateClient();
        }
    }
}