using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;

namespace Function;

public static class ReadModel
{
    [FunctionName("ReadModel")]
    public static async Task Run([CosmosDBTrigger(
            databaseName: "Amped",
            collectionName: "Bookmarks",
            ConnectionStringSetting = "connectionString",
            CreateLeaseCollectionIfNotExists = true)]
        IEnumerable<dynamic> input, 
        [CosmosDB(
            databaseName: "Amped",
            collectionName: "mv_RatedBookmarks",
            ConnectionStringSetting = "connectionString")]
        IAsyncCollector<RatedBookmark> toDoItemsOut)
    {
        // var logger = context.GetLogger("ReadModel");
        // if (input != null && input.Count > 0)
        // {
        //     logger.LogInformation("Documents modified: " + input.Count);
        //     logger.LogInformation("First document Id: " + input[0].Uri);
        // }

        var stuff = input.Select(x => new RatedBookmark(x.uri, x.owner, 0));
        foreach (var thing in stuff)
        {
            await toDoItemsOut.AddAsync(thing);
        }
    }
}

public class Bookmark
{
    public string id { get; set; }
    
    public string uri { get; set; }

    public string owner { get; set; }
}

public record RatedBookmark(string Uri, string Owner, int Rating);