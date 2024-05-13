using APIForBrowserApp.Models;

namespace APIForBrowserApp.Services.Interfaces
{
    public interface IAuthService
    {
        AppResult<LoginResponse> Login(LoginRequest loginRequest);
    }
}
