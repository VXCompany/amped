using System.Threading.Tasks;
using Amped.Bookmarks.Core;

namespace Amped.Core;

public interface IBookmarkRepository
{
    Task Add(Bookmark bookmark);
}