using System;
using System.Threading.Tasks;
using MassTransit;

namespace Amped.Core.NewBookmark
{
    public class CreateBookmarkCommandHandler : IConsumer<CreateBookmarkCommand>
    {
        private readonly IBookmarkRepository _bookmarkRepository;
        private readonly IEventStream _stream;

        public CreateBookmarkCommandHandler(IBookmarkRepository bookmarkRepository, IEventStream stream)
        {
            _bookmarkRepository = bookmarkRepository ?? throw new ArgumentNullException(nameof(bookmarkRepository));
            _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        }

        public async Task Consume(ConsumeContext<CreateBookmarkCommand> context)
        {
            var bookmark = Bookmark.CreateUnreadBookmark(context.Message.Uri, "Fred");
            
            await _bookmarkRepository.Add(bookmark);
            
            await _stream.Broadcast(new BookmarkCreatedEvent
            {
                Uri = bookmark.Uri,
                Owner = bookmark.Owner
            });
        }
    }
}