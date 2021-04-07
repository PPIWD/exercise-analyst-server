using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login()
        {
            return Ok("Gites");
        }
        
        [HttpPost("register")]
        public async Task<IActionResult> Register()
        {
            return Ok("Gites register");
        }
    }
}