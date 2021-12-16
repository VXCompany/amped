using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
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
    } 
    
    public class IntegrationTests
    {
        public IntegrationTests(TestFixture fixture)
        {
            
        }

        [Fact]
        public void Can_create_a_bookmark()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.PostAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();
        }

    }
}