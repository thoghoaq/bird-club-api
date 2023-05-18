using BirdClubAPI.Domain.DTOs.Response.Member;

namespace BirdClubAPI.DataAccessLayer.Repositories.Member
{
    public interface IMemberRepository
    {
        Domain.Entities.Member? GetMember(int memberId);
        List<MemberProfileResponseModel> GetMembers();
        bool UpdateMember(Domain.Entities.Member member);
    }
}
