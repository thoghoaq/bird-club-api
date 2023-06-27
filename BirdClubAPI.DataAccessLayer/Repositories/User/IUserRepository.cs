using BirdClubAPI.Domain.DTOs.Request.Auth;
using BirdClubAPI.Domain.DTOs.View.Auth;

namespace BirdClubAPI.DataAccessLayer.Repositories.User
{
    public interface IUserRepository
    {
        Domain.Entities.User? Get(string email, string password);
        Domain.Entities.User? Create(RegisterRequestModel requestModel);
        List<GuestViewModel>? GetListGuest();
        Domain.Entities.User? ApproveMember(int userId);
    }
}
