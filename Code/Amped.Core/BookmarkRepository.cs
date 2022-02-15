using System.Collections.Generic;
using System.Threading.Tasks;

namespace Amped.Core
{
    public class BookmarkRepository : IBookmarkRepository
    {
        private static readonly List<Bookmark> _bookmarks = new();

        public Task Add(Bookmark bookmark)
        {
            _bookmarks.Add(bookmark);
            return Task.CompletedTask;
        }
    }
}