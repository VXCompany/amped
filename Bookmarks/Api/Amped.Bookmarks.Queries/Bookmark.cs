using Newtonsoft.Json;

namespace Amped.Bookmarks.Queries;

public class Bookmark
{
    public Guid Id { get; set; }
        
    public Uri Uri { get; set; }
        
    public bool Read { get; set; }
        
    [JsonIgnore] // muhahaha!
    public Guid Owner { get; set; }
}