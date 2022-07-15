using System;

namespace Amped.Core.NewBookmark;

public class BookmarkCreatedEvent
{
    // todo: Implement correlation id
        
    public Uri Uri { get; set; }
    public object Owner { get; set; }
}