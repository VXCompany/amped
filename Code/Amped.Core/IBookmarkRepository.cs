using System.Threading.Tasks;

namespace Amped.Core
{
    public interface IBookmarkRepository
    {
        Task Add(Bookmark bookmark);
    }
}