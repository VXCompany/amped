using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;

namespace Amped.Bookmarks.FunctionApp.CQRS.RatedBookmarks;

public static class Function
{
    [FunctionName("ReadModel")]
    public static async Task Run(
            [CosmosDBTrigger("bookmarks", "bookmarks", ConnectionStringSetting = "connectionString", CreateLeaseCollectionIfNotExists = true)]
            IEnumerable<dynamic> input, 
            
            [CosmosDB("bookmarks", "mv_ratedbookmarks", ConnectionStringSetting = "connectionString")]
            IAsyncCollector<RatedBookmark> ratedBookmarks
        )
    {
        var mappedRatedBookmarks = input.Select(x => new RatedBookmark(x.uri, x.owner, 0));
        foreach (var ratedBookmark in mappedRatedBookmarks)
        {
            await ratedBookmarks.AddAsync(ratedBookmark);
        }
    }
}