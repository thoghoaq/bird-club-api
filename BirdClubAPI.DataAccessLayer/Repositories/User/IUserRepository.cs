using BirdClubAPI.Domain.DTOs.Request.Auth;

namespace BirdClubAPI.DataAccessLayer.Repositories.User
{
    public interface IUserRepository
    {
        Domain.Entities.User? Get(string email, string password);
        Domain.Entities.User? Create(RegisterRequestModel requestModel);
    }
}
