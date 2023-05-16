using BirdClubAPI.DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;

namespace BirdClubAPI.DataAccessLayer.Repositories.Member
{
    public class MemberRepository : IMemberRepository
    {
        private readonly BirdClubContext _context;

        public MemberRepository(BirdClubContext context)
        {
            _context = context;
        }

        public Domain.Entities.Member? GetMember(int memberId)
        {
            return _context.Members.Where(e => e.UserId == memberId).Include(e => e.User).FirstOrDefault();
        }

        public bool UpdateMember(Domain.Entities.Member member)
        {
            try
            {
                _context.Members.Attach(member);
                _context.Entry(member).State = EntityState.Modified;
                _context.SaveChanges();
                return true;

            } catch
            {
                return false;
            }
        }
    }
}
