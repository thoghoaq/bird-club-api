using BirdClubAPI.Domain.DTOs.Request.Member;
using BirdClubAPI.Domain.DTOs.View.Common;
using BirdClubAPI.Domain.DTOs.View.Member;

namespace BirdClubAPI.BusinessLayer.Services.Member
{
    public interface IMemberService
    {
        KeyValuePair<MessageViewModel, List<MemberViewModel>> GetMembers();
        KeyValuePair<MessageViewModel, MemberViewModel?> GetProfile(int id);
        MessageViewModel UpdateMemberProfile(int memberId, UpdateMemberRequestModel requestModel);
        MessageViewModel UpdateMembershipStatus(int id, UpdateMembershipStatusModel requestModel);
    }
}
