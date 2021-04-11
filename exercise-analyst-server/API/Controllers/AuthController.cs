using System.Net;
using System.Threading.Tasks;
using API.Services.Auth;
using API.Services.Auth.Dtos;
using API.Services.Common;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        
        [HttpPost("login")]
        [Produces(typeof(Response<LoginResponse>))]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var response = await _authService.LoginAsync(request);

            if (response.HttpStatusCode == HttpStatusCode.Unauthorized)
                return Unauthorized(response);

            return Ok(response);
        }
        
        [HttpPost("register")]
        [Produces(typeof(Response<RegisterResponse>))]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var response = await _authService.RegisterAsync(request);

            if (response.HttpStatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);

            return Ok(response);
        }
    }
}