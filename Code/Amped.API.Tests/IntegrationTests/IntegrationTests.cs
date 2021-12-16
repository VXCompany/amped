using System;
using System.Net.Http;
using System.Threading.Tasks;
using Amped.API.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

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
    
    public class IntegrationTests
    {
        private readonly TestFixture _fixture;

        public IntegrationTests()
        {
            _fixture = new TestFixture();
        }

        [Fact]
        public async Task  Can_create_a_bookmark()
        {
            var testFixture = new TestFixture();

            var request = new CreateBookmarkRequest
            {
                Uri = new Uri("https://app.totallyamped.tech/getting-started")

            };

            var serialized = JsonConvert.SerializeObject(request);

            var result = await testFixture.Client.PostAsync(@"/api/bookmark/create", new StringContent(serialized));

            result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
    }
}