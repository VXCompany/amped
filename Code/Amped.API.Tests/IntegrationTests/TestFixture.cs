using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http;

namespace Amped.API.Tests.IntegrationTests
{
    public class TestFixture : IDisposable
    {
        private readonly HttpClient _client;

        public TestFixture()
        {
            var factory = new WebApplicationFactory<Startup>();
            _client = factory.CreateClient();
        }

        public void Dispose()
        {
            _client.Dispose();
        }

        public HttpClient Client => _client;
    }
}