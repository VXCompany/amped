using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Amped.Core.NewBookmark;

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
        public async Task<IActionResult> Create([FromServices] IBus bus, CreateBookmarkCommand command)
        {
            await bus.Publish(command);
            return Accepted();
        }
        
        [HttpPost("markAsRead")]
        public IActionResult MarkAsRead(MarkAsReadRequest markAsReadRequest)
        {
            return Ok();
        }
    }
}
