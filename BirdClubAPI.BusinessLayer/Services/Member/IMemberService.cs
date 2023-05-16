using BirdClubAPI.Domain.DTOs.Request.Member;
using BirdClubAPI.Domain.DTOs.View.Common;

namespace BirdClubAPI.BusinessLayer.Services.Member
{
    public interface IMemberService
    {
        public MessageViewModel UpdateMemberProfile(int memberId, UpdateMemberRequestModel requestModel);
    }
}
