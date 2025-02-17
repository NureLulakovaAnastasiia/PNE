using Microsoft.AspNetCore.Mvc;
using StringChangeLibrary;
namespace ServerSide.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StringController : ControllerBase
    {
        private readonly StringClass _libraryObject;

        public StringController(StringClass libraryObject)
        {
            _libraryObject = libraryObject;
        }
       
       
        [HttpGet(Name = "ChangeBrackets")]
        public IActionResult ChangeBrackets(string value)
        {
            var res = _libraryObject.ChangeBrackets(value);
            if (String.IsNullOrEmpty(res))
            {
                return StatusCode(500);
            }
            return Ok(res);
        }
    }
}
