using System;
using System.Threading.Tasks;
using MassTransit;

namespace Amped.Core.NewBookmark
{
    public class CreateBookmarkCommandHandler : IConsumer<CreateBookmarkCommand>
    {
        private readonly IBookmarkRepository _bookmarkRepository;

        public CreateBookmarkCommandHandler(IBookmarkRepository bookmarkRepository)
        {
            _bookmarkRepository = bookmarkRepository ?? throw new ArgumentNullException(nameof(bookmarkRepository));
        }

        public async Task Consume(ConsumeContext<CreateBookmarkCommand> context)
        {
            var bookmark = Bookmark.CreateUnreadBookmark(context.Message.Uri, "Fred");

            await _bookmarkRepository.Add(bookmark);
        }
    }
}