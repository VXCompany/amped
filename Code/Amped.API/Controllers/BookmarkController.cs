using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

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
            var bookmark = new Bookmark(createBookmarkRequest.Uri, "Fred");

            _bookmarkRepository.Add(bookmark);

            return Ok();
        }
    }

    public class CreateBookmarkRequest
    {
        public Uri Uri { get; set; }
    }
}
