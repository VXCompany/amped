using System.Threading.Tasks;
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
        public async Task<IActionResult> Get([FromServices] Queries.IBookmarkRepository repository)
        {
            var bookmarks = await repository.GetAll();
            
            return Ok(bookmarks);
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
