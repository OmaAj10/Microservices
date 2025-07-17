using Web.Models;
using Web.Models.Dto;
using Web.Utility;

namespace Web.Service.IService;

public class AuthService : IAuthService
{
    private readonly IBaseService _baseService;
    
    public AuthService(IBaseService baseService)
    {
        _baseService = baseService;
    }
    public async Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = StaticDetails.ApiType.POST,
            Data = loginRequestDto,
            Url = StaticDetails.AuthAPIBase + $"/api/auth/login",
        });
    }

    public async Task<ResponseDto?> RegisterAsync(RegistrationRequestDto registrationRequestDto)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = StaticDetails.ApiType.POST,
            Data = registrationRequestDto,
            Url = StaticDetails.AuthAPIBase + $"/api/auth/register",
        });
    }

    public async Task<ResponseDto?> AssignRoleAsync(RegistrationRequestDto registrationRequestDto)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = StaticDetails.ApiType.POST,
            Data = registrationRequestDto,
            Url = StaticDetails.AuthAPIBase + $"/api/auth/AssignRole",
        });
    }
}