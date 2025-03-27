using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthApiController : Controller
{
    [HttpPost("register")]
    public async Task<IActionResult> Register()
    {
        return Ok();
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login()
    {
        return Ok();
    }
}