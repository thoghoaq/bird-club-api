using BirdClubAPI.Domain.DTOs.Request.Auth;
using BirdClubAPI.Domain.DTOs.Response.User;
using BirdClubAPI.Domain.DTOs.View.Auth;
using BirdClubAPI.Domain.DTOs.View.Member;

namespace BirdClubAPI.DataAccessLayer.Repositories.User
{
    public interface IUserRepository
    {
        Domain.Entities.User? Get(string email, string password);
        Domain.Entities.User? Create(RegisterRequestModel requestModel);
        Domain.Entities.User? ApproveMember(int userId);
        List<GuestViewModel>? GetListGuest();
        List<UserResponseModel> ShowUser();
    }
}
