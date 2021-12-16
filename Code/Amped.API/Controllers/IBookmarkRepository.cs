namespace Amped.API.Controllers
{
    public interface IBookmarkRepository
    {
        void Add(Bookmark bookmark);
    }

    public class BookmarkRepository : IBookmarkRepository
    {
        public void Add(Bookmark bookmark)
        {
            throw new System.NotImplementedException();
        }
    }
}