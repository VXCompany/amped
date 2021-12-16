using Amped.API.Core;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("create")]
        public IActionResult Create(CreateBookmarkRequest createBookmarkRequest)
        {
            var bookmark = new Bookmark(createBookmarkRequest.Uri, "Fred");

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
