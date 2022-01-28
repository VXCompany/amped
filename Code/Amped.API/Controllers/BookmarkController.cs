using Microsoft.AspNetCore.Mvc;
using Amped.Core;

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

        [Route("all")]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new []
            {
                (object) new { id = 1, uri = "https://test1.com/amped-is-the-bomb" },
                new { id = 2, uri = "https://test2.com/why-amped-rulez", read = true }
            });
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
