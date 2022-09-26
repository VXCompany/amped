using System.Threading.Tasks;
using Amped.Bookmarks.FunctionApp.CQRS.RatedBookmarks;
using Microsoft.Azure.WebJobs;
using NSubstitute;
using Xunit;

namespace Amped.Bookmarks.FunctionApp.Tests.CQRS.RatedBookmarks
{
    public class RatedBookmarksTests
    {
        [Fact]
        public async Task ItShouldSelectAndMapAllItemsFromInput() // Todo: Cut this method into two, because this violates the SRP.
        {
            const string expectedUri = "http://eureka.com";
            const string expectedOwner = "johnny";
            
            var input = new []
            {
                new Poco{ uri = expectedUri, owner = expectedOwner }
            };

            var actual = Substitute.For<IAsyncCollector<RatedBookmark>>();
            
            await Function.Run(input, actual);

            await actual
                .Received(1)
                .AddAsync(Arg.Is<RatedBookmark>(x => x.Owner == expectedOwner && x.Uri == expectedUri));
        }
    }
}