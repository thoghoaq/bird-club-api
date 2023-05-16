using BirdClubAPI.DataAccessLayer.Context;
using BirdClubAPI.Domain.Commons.Constants;
using BirdClubAPI.Domain.DTOs.Request.Auth;

namespace BirdClubAPI.DataAccessLayer.Repositories.User
{
    public class UserRepository : IUserRepository
    {
        private readonly BirdClubContext _context;

        public UserRepository(BirdClubContext context)
        {
            _context = context;
        }

        public Domain.Entities.User? Create(RegisterRequestModel requestModel)
        {
            try
            {
                var user = new Domain.Entities.User
                {
                    Email = requestModel.Email,
                    Password = requestModel.Password,
                    DisplayName = requestModel.DisplayName,
                    UserType = UserTypeConstants.MEMBER,
                };
                var result = _context.Add(user);
                _context.SaveChanges();
                if (result == null) return null;
                return result.Entity;
            } catch (Exception ex)
            {
                return null;
            }
        }

        public Domain.Entities.User? Get(string email, string password)
        {
            var user = _context.Users.SingleOrDefault(e => e.Email.Equals(email) && e.Password.Equals(password));
            return user;
        }
    }
}
