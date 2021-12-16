using Amped.API.Controllers;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Amped.API.Tests.IntegrationTests
{
    public class BookmarkTests
    {
        [Fact]
        public async Task Can_create_a_bookmark()
        {
            var testFixture = new TestFixture();

            var request = new CreateBookmarkRequest
            {
                Uri = new Uri("https://app.totallyamped.tech/getting-started")

            };

            var serialized = JsonConvert.SerializeObject(request);

            var result = await testFixture.Client.PostAsync(@"/api/bookmark/create", new StringContent(serialized, Encoding.UTF8, "application/json"));

            result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
    }
}