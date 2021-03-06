using System.Threading.Tasks;
using Amped.Bookmarks.Core.NewBookmark;
using Amped.Core;
using Microsoft.AspNetCore.Mvc;

namespace Amped.Bookmarks.API.Controllers;

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
    public async Task<IActionResult> Create([FromServices] ICommandQueue queue, CreateBookmarkCommand command)
    {
        await queue.Send(command);
        return Accepted();
    }
        
    [HttpPost("markAsRead")]
    public IActionResult MarkAsRead(MarkAsReadRequest markAsReadRequest)
    {
        return Ok();
    }
}