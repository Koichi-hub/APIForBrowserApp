using APIForBrowserApp.Database;
using APIForBrowserApp.Helpers;
using APIForBrowserApp.Models;
using APIForBrowserApp.Services.Interfaces;
using AutoMapper;

namespace APIForBrowserApp.Services
{
    public class AuthService : IAuthService
    {
        private readonly DatabaseContext databaseContext;
        private readonly IMapper mapper;

        public AuthService(
            DatabaseContext databaseContext,
            IMapper mapper
        )
        {
            this.databaseContext = databaseContext;
            this.mapper = mapper;
        }

        public AppResult<LoginResponse> Login(LoginRequest loginRequest)
        {
            var result = AppResultFactory.Create<LoginResponse>();

            var user = databaseContext.Users.FirstOrDefault(x => x.Login == loginRequest.Login);
            if (user is null)
            {
                result.Status = StatusCodes.Status400BadRequest;
                result.Message = "wrong login or password";
                return result;
            }

            if (user.Password != UserPasswordHelper.HashPassword(loginRequest.Password))
            {
                result.Status = StatusCodes.Status400BadRequest;
                result.Message = "wrong login or password";
                return result;
            }
            result.Data = mapper.Map<LoginResponse>(user);

            return result;
        }
    }
}
