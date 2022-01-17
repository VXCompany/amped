using Microsoft.AspNetCore.Mvc;
using Amped.API.Core;

 namespace Amped.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookmarkController : ControllerBase
    {
        private readonly IBookmarkRepository _bookmarkRepository;

        public BookmarkController(IBookmarkRepository bookmarkRepository)
        {
            _bookmarkRepository = bookmarkRepository;
        }

        [Route("create")]
        [HttpPost]
        public IActionResult Create(CreateBookmarkRequest createBookmarkRequest)
        {
            var bookmark = Bookmark.CreateUnreadBookmark(createBookmarkRequest.Uri, "Fred");

            _bookmarkRepository.Add(bookmark);

            return Ok();
        }
        
        [HttpPost("markAsRead")]
        public IActionResult MarkAsRead(MarkAsReadRequest markAsReadRequest)
        {
            return Ok();
        }
    }
}
