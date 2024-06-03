using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace LibraryManager.Api.Controllers
{
    [Route("[controller]")]
    public class BooksController : ApiController
    {
        public BooksController(ProblemDetailsFactory problemDetailsFactory) : base( problemDetailsFactory )
        {
            
        }

        [HttpGet()]
        public IActionResult ListBooks()
        {
            return Ok(Array.Empty<string>());
        }

        [HttpPost("Add")]
        public IActionResult AddBook(string bookName)
        {
            return Ok($"New book added: { bookName }");
        }
    }
}
