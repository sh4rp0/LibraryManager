using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        [HttpPost("Add")]
        public IActionResult AddBook(string bookName)
        {
            return Ok($"New book added: { bookName }");
        }
    }
}
