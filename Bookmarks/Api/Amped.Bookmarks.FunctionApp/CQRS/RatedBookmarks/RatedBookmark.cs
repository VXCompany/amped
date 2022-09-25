namespace Amped.Bookmarks.FunctionApp.CQRS.RatedBookmarks;

public record RatedBookmark(string Uri, string Owner, int Rating);