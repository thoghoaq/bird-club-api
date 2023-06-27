using BirdClubAPI.DataAccessLayer.Context;
using BirdClubAPI.Domain.Commons.Constants;
using BirdClubAPI.Domain.DTOs.Request.Auth;
using BirdClubAPI.Domain.DTOs.View.Auth;
using Microsoft.EntityFrameworkCore;

namespace BirdClubAPI.DataAccessLayer.Repositories.User
{
    public class UserRepository : IUserRepository
    {
        private readonly BirdClubContext _context;

        public UserRepository(BirdClubContext context)
        {
            _context = context;
        }

        public Domain.Entities.User? ApproveMember(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null) return null;
            if (user.UserType == UserTypeConstants.GUEST)
            {
     
                user.UserType = UserTypeConstants.MEMBER;
                _context.SaveChanges();


                var member = new Domain.Entities.Member
                {
                    UserId = user.Id,
                    MembershipStatus = true,
                };
                _context.Members.Add(member);
                _context.SaveChanges();
                

                return user;
            }
            return null;
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
                    UserType = UserTypeConstants.GUEST,
                    Birthday = DateOnly.Parse(requestModel.Birthday),
                };
                var result = _context.Add(user);
                _context.SaveChanges();
                if (result == null) return null;
                return result.Entity;
            } catch
            {
                return null;
            }
        }

        public Domain.Entities.User? Get(string email, string password)
        {
            var user = _context.Users
                .Select(e => new Domain.Entities.User
                {
                    Id = e.Id,
                    Email = e.Email,
                    DisplayName= e.DisplayName,
                    Password = e.Password,
                    UserType = e.UserType,
                    Member = e.Member
                }).SingleOrDefault(e => e.Email.Equals(email) && e.Password.Equals(password));
            return user;
        }
        public List<GuestViewModel>? GetListGuest()
        {
            var guests = _context.Users
                .Where(e => e.UserType == UserTypeConstants.GUEST)
                .Select(e => new GuestViewModel
                {
                    Id = e.Id,
                    Email = e.Email,
                    DisplayName = e.DisplayName,
                    Password = e.Password,
                    UserType = e.UserType,
                    Birthday = e.Birthday.ToString(),
                }).ToList();

            if (guests.Any())
            {
                return guests;
            }
            else
            {
                return null;
            }
        }
    }
}
