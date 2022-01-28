using System;
using System.Threading.Tasks;

namespace Amped.Core
{
    public class NewBookmarkUseCase : INewBookmarkUseCase
    {
        private readonly IBookmarkRepository _bookmarkRepository;

        public NewBookmarkUseCase(IBookmarkRepository bookmarkRepository)
        {
            _bookmarkRepository = bookmarkRepository ?? throw new ArgumentNullException(nameof(bookmarkRepository));
        }

        public async Task CreateBookmark(Bookmark bookmark)
        {
            await _bookmarkRepository.Add(bookmark);
        }
    }
}