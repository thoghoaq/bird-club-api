using BirdClubAPI.DataAccessLayer.Context;

namespace BirdClubAPI.DataAccessLayer.Repositories.User
{
    public class UserRepository : IUserRepository
    {
        private readonly BirdClubContext _context;

        public UserRepository(BirdClubContext context)
        {
            _context = context;
        }

        public Domain.Entities.User? Get(string email, string password)
        {
            var user = _context.Users.SingleOrDefault(e => e.Email.Equals(email) && e.Password.Equals(password));
            return user;
        }
    }
}
