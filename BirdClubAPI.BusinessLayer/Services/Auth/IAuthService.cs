using BirdClubAPI.Domain.DTOs.Request.Auth;
using BirdClubAPI.Domain.DTOs.View.Auth;
using BirdClubAPI.Domain.DTOs.View.Common;
using BirdClubAPI.Domain.DTOs.View.Member;

namespace BirdClubAPI.BusinessLayer.Services.Auth
{
    public interface IAuthService
    {
        Task<MessageViewModel> ApproveMember(int id);
        KeyValuePair<MessageViewModel, List<GuestViewModel?>> GetListGuest();
        Task<KeyValuePair<MessageViewModel, AuthViewModel?>> Login (LoginFormRequestModel loginFormRequest);
        Task<bool> Register(RegisterRequestModel requestModel);
        Task<MessageViewModel> RejectUser(int id);
        Task<bool> ResendEmail(string email);
        UserViewModel ShowUser();
    }
}
