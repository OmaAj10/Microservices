namespace Web.Service.IService;

public interface ITokeProvider
{
    void SetToken(string token);
    string? GetToken();
    void ClearToken();
}