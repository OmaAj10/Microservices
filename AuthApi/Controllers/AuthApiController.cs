using AuthApi.Models.Dto;
using AuthApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthApiController : Controller
{
    private readonly IAuthService _authService;
    protected readonly ResponseDto _responseDto;

    public AuthApiController(IAuthService authService)
    {
        _authService = authService;
        _responseDto = new();
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegistrationRequestDto registrationRequestDto)
    {
        var errorMessage = await _authService.Register(registrationRequestDto);

        if (!string.IsNullOrEmpty(errorMessage))
        {
            _responseDto.IsSuccess = false;
            _responseDto.Message = errorMessage;
            return BadRequest(_responseDto);
        }
        return Ok(_responseDto);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody]LoginRequestDto loginRequestDto)
    {
        var loginResponse = await _authService.Login(loginRequestDto);

        if (loginResponse.UserDto == null)
        {
            _responseDto.IsSuccess = false;
            _responseDto.Message = "Username or password is incorrect.";
            return BadRequest(_responseDto);
        }

        _responseDto.Result = loginResponse;
        return Ok(_responseDto);
    }
}