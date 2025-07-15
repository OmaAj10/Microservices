using AuthApi.Models;

namespace AuthApi.Services;

public interface IJwtTokeGenerator
{
    string GenerateToken(ApplicationUser applicationUser);
}