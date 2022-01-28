using Microsoft.AspNetCore.Mvc;
using Amped.Core;

namespace Amped.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookmarkController : ControllerBase
    {
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
        public IActionResult Create([FromServices] INewBookmarkUseCase useCase, CreateBookmarkRequest createBookmarkRequest)
        {
            var bookmark = Bookmark.CreateUnreadBookmark(createBookmarkRequest.Uri, "Fred");

            useCase.CreateBookmark(bookmark);

            return Ok();
        }
        
        [HttpPost("markAsRead")]
        public IActionResult MarkAsRead(MarkAsReadRequest markAsReadRequest)
        {
            return Ok();
        }
    }
}
