using Amped.API.Controllers;
using FluentAssertions;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Amped.API.Tests.IntegrationTests
{
    public class BookmarkTests
    {
        private readonly HttpClient _api = new ApiTestClientBuilder().Build();

        ~BookmarkTests() => _api.Dispose();

        [Fact]
        public async Task Can_list_bookmarks()
        {
            var result = await _api.GetAsync(@"/api/bookmark/all");

            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        
        [Fact]
        public async Task Can_create_a_bookmark()
        {
            var request = new CreateBookmarkRequest
            {
                Uri = new Uri("https://app.totallyampednow.com/getting-started")
            };

            var result = await _api.PostAsync(@"/api/bookmark/create", request.ToStringContent());

            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Can_mark_bookmark_as_read()
        {
            var request = new MarkAsReadRequest
            {
                Id = 42
            };

            var result = await _api.PostAsync(@"/api/bookmark/markAsRead", request.ToStringContent());

            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}