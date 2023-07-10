using BirdClubAPI.Domain.DTOs.Request.Auth;
using BirdClubAPI.Domain.DTOs.View.Auth;
using BirdClubAPI.Domain.DTOs.View.Common;
using BirdClubAPI.Domain.DTOs.View.Member;

namespace BirdClubAPI.BusinessLayer.Services.Auth
{
    public interface IAuthService
    {
        MessageViewModel ApproveMember(int id);
        KeyValuePair<MessageViewModel, List<GuestViewModel?>> GetListGuest();
        KeyValuePair<MessageViewModel, AuthViewModel?> Login (LoginFormRequestModel loginFormRequest);
        bool Register(RegisterRequestModel requestModel);
        MessageViewModel RejectUser(int id);
        UserViewModel ShowUser();
    }
}
