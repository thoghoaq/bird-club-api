using BirdClubAPI.Domain.DTOs.Request.Auth;
using BirdClubAPI.Domain.DTOs.View.Auth;
using BirdClubAPI.Domain.DTOs.View.Common;

namespace BirdClubAPI.BusinessLayer.Services.Auth
{
    public interface IAuthService
    {
        KeyValuePair<MessageViewModel, AuthViewModel?> Login (LoginFormRequestModel loginFormRequest);
        bool Register(RegisterRequestModel requestModel);
    }
}
