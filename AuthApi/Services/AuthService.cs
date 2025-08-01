using AuthApi.Data;
using AuthApi.Models;
using AuthApi.Models.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthApi.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IJwtTokeGenerator _jwtTokeGenerator;

    public AuthService(AppDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IJwtTokeGenerator jwtTokeGenerator)
    {
        _db = db;
        _userManager = userManager;
        _roleManager = roleManager;
        _jwtTokeGenerator = jwtTokeGenerator;
    }
    
    public async Task<string> Register(RegistrationRequestDto registrationRequestDto)
    {
        ApplicationUser user = new()
        {
            UserName = registrationRequestDto.Email,
            Email = registrationRequestDto.Email,
            NormalizedEmail = registrationRequestDto.Email.ToUpper(),
            Name = registrationRequestDto.Name,
            PhoneNumber = registrationRequestDto.PhoneNumber
        };

        try
        {
            var result = await _userManager.CreateAsync(user, registrationRequestDto.Password);

            if (result.Succeeded)
            {
                var userToReturn = _db.ApplicationUsers.First(u => u.UserName == registrationRequestDto.Email);
                UserDto userDto = new()
                {
                    Email = userToReturn.Email,
                    Id = userToReturn.Id,
                    Name = userToReturn.Name,
                    PhoneNumber = userToReturn.PhoneNumber
                };
                
                return "";
            }
            else
            {
                return result.Errors.FirstOrDefault().Description;
            }
        }
        catch (Exception e)
        {
        }

        return "Error Encountered";
    }

    public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
    {
        var user = await _db.ApplicationUsers.FirstOrDefaultAsync(u =>
            u.UserName.ToLower() == loginRequestDto.Username.ToLower());

        bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

        if (user == null || isValid == false)
        {
            return new LoginResponseDto() {UserDto = null, Token = ""};
        }
        
        var token = _jwtTokeGenerator.GenerateToken(user);

        UserDto userDto = new()
        {
            Id = user.Id,
            Email = user.Email,
            Name = user.Name,
            PhoneNumber = user.PhoneNumber
        };

        LoginResponseDto loginResponseDto = new()
        {
            UserDto = userDto,
            Token = token
        };
        return loginResponseDto;
    }

    public async Task<bool> AssignRole(string email, string roleName)
    {
        var user = await _db.ApplicationUsers.FirstOrDefaultAsync(u =>
            u.Email.ToLower() == email.ToLower());

        if (user != null)
        {
            if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
            {
                //create role if does not exist
                _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
            }
            await _userManager.AddToRoleAsync(user, roleName);
            return true;
        }

        return false;
    }
}