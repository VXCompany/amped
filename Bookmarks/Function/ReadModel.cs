using Microsoft.Azure.WebJobs;

namespace Function;

public static class ReadModel
{
    [FunctionName("ReadModel")]
    public static async Task Run([Microsoft.Azure.Functions.Worker.CosmosDBTrigger(
            databaseName: "Amped",
            collectionName: "Bookmarks",
            ConnectionStringSetting = "connectionString",
            CreateLeaseCollectionIfNotExists = true)]
        IReadOnlyList<Bookmark> input, 
        [CosmosDB(
            databaseName: "Amped",
            collectionName: "mv_myBookmarks",
            ConnectionStringSetting = "CosmosDBConnection")]
        IAsyncCollector<RatedBookmark> toDoItemsOut)
    {
        // var logger = context.GetLogger("ReadModel");
        // if (input != null && input.Count > 0)
        // {
        //     logger.LogInformation("Documents modified: " + input.Count);
        //     logger.LogInformation("First document Id: " + input[0].Uri);
        // }

        var stuff = input.Select(x => new RatedBookmark(x.Uri, x.Owner, 0));
        foreach (var thing in stuff)
        {
            await toDoItemsOut.AddAsync(thing);
        }
    }
}

public class Bookmark
{
    public string Uri { get; set; }

    public string Owner { get; set; }
}

public record RatedBookmark(string Uri, string Owner, int Rating);
