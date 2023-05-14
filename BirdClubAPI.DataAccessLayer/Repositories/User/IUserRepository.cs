namespace BirdClubAPI.DataAccessLayer.Repositories.User
{
    public interface IUserRepository
    {
        Domain.Entities.User? Get(string email, string password);
    }
}
