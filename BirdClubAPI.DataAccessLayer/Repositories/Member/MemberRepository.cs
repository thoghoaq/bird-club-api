using AutoMapper;
using BirdClubAPI.DataAccessLayer.Context;
using BirdClubAPI.Domain.DTOs.Response.Member;
using BirdClubAPI.Domain.DTOs.Response.User;
using Microsoft.EntityFrameworkCore;

namespace BirdClubAPI.DataAccessLayer.Repositories.Member
{
    public class MemberRepository : IMemberRepository
    {
        private readonly BirdClubContext _context;
        private readonly IMapper _mapper;

        public MemberRepository(BirdClubContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Domain.Entities.Member? GetMember(int memberId)
        {
            return _context.Members.Where(e => e.UserId == memberId).Include(e => e.User).FirstOrDefault();
        }

        public List<MemberProfileResponseModel> GetMembers()
        {
            return _context.Members.Select(e => new MemberProfileResponseModel
            {
                Address = e.Address,
                Avatar = e.Avatar,
                MembershipStatus = e.MembershipStatus,
                Phone = e.Phone,
                UserId = e.UserId,
                User = _mapper.Map<UserResponseModel>(e.User)
            }).ToList();
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
