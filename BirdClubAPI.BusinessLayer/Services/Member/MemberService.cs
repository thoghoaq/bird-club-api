using AutoMapper;
using BirdClubAPI.DataAccessLayer.Repositories.Member;
using BirdClubAPI.Domain.DTOs.Request.Member;
using BirdClubAPI.Domain.DTOs.View.Common;

namespace BirdClubAPI.BusinessLayer.Services.Member
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;

        public MemberService(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public MessageViewModel UpdateMemberProfile(int memberId, UpdateMemberRequestModel requestModel)
        {
            var member = _memberRepository.GetMember(memberId);
            if (member == null)
            {
                return new MessageViewModel
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "Not found this member"
                };
            }
            else
            {
                // Map field not null in requestModel to member.
                if (requestModel.DisplayName != null)
                {
                    member.User.DisplayName = requestModel.DisplayName;
                }
                var requestProperties = typeof(UpdateMemberRequestModel).GetProperties();
                foreach (var property in requestProperties)
                {
                    var requestValue = property.GetValue(requestModel);
                    if (requestValue != null)
                    {
                        var memberProperty = member.GetType().GetProperty(property.Name);
                        memberProperty?.SetValue(member, requestValue);
                    }
                }

                // Update member.
                var result = _memberRepository.UpdateMember(member);
                if (result == true)
                {
                    return new MessageViewModel
                    {
                        StatusCode = System.Net.HttpStatusCode.NoContent,
                        Message = "Update member successful"
                    };
                }
                else
                {
                    return new MessageViewModel
                    {
                        StatusCode = System.Net.HttpStatusCode.Conflict,
                        Message = "Can not update member"
                    };
                }
            }
        }
    }
}
