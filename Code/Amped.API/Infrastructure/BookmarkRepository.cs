using Amped.API.Core;
using System.Collections.Generic;

namespace Amped.API.Infrastructure
{
    public class BookmarkRepository : IBookmarkRepository
    {
        private static readonly List<Bookmark> _bookmarks = new();

        public void Add(Bookmark bookmark)
        {
            _bookmarks.Add(bookmark);
        }
    }
}