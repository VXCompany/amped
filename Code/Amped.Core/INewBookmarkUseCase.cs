using System.Threading.Tasks;

namespace Amped.Core
{
    public interface INewBookmarkUseCase
    {
        Task CreateBookmark(Bookmark bookmark);
    }
}