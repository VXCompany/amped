using System.Collections;
using System.Collections.Generic;

namespace Amped.API.Controllers
{
    public class BookmarkRepository : IBookmarkRepository
    {
        private static readonly List<Bookmark> _bookmarks = new ();
        
        public void Add(Bookmark bookmark)
        {
            _bookmarks.Add(bookmark);
        }
    }
}