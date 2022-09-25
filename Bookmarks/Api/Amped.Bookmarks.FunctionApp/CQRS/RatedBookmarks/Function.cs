using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;

namespace Amped.Bookmarks.FunctionApp.CQRS.RatedBookmarks;

public static class Function
{
    [FunctionName("ReadModel")]
    public static async Task Run(
            [CosmosDBTrigger("Bookmarks", "Bookmarks", ConnectionStringSetting = "connectionString", CreateLeaseCollectionIfNotExists = true)]
            IEnumerable<dynamic> input, 
            
            [CosmosDB("Bookmarks", "mv_RatedBookmarks", ConnectionStringSetting = "connectionString")]
            IAsyncCollector<RatedBookmark> toDoItemsOut
        )
    {
        var ratedBookmarks = input.Select(x => new RatedBookmark(x.uri, x.owner, 0));
        foreach (var ratedBookmark in ratedBookmarks)
        {
            await toDoItemsOut.AddAsync(ratedBookmark);
        }
    }
}