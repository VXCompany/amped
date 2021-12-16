using Amped.API.Controllers;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Amped.API.Tests.IntegrationTests
{
    public class BookmarkTests
    {
        private TestFixture _testFixture = new();

        [Fact]
        public async Task Can_create_a_bookmark()
        {
            var request = new CreateBookmarkRequest
            {
                Uri = new Uri("https://app.totallyamped.tech/getting-started")
            };

            var serialized = JsonConvert.SerializeObject(request);

            var content = new StringContent(serialized, Encoding.UTF8, "application/json");
            var result = await _testFixture.Client.PostAsync(@"/api/bookmark/create", content);

            result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task Can_mark_bookmark_as_read()
        {
            var request = new MarkAsReadRequest
            {
                Id = 42
            };

            var serialized = JsonConvert.SerializeObject(request);
            var content = new StringContent(serialized, Encoding.UTF8, "application/json");

            var result = await _testFixture.Client.PostAsync(@"/api/bookmark/markAsRead", content);

            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}