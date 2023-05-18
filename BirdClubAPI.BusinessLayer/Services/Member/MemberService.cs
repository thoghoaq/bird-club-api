using AutoMapper;
using BirdClubAPI.DataAccessLayer.Repositories.Member;
using BirdClubAPI.Domain.DTOs.Request.Member;
using BirdClubAPI.Domain.DTOs.Response.Member;
using BirdClubAPI.Domain.DTOs.View.Common;
using BirdClubAPI.Domain.DTOs.View.Member;

namespace BirdClubAPI.BusinessLayer.Services.Member
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;

        public MemberService(IMemberRepository memberRepository, IMapper mapper)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
        }

        public KeyValuePair<MessageViewModel, List<MemberViewModel>> GetMembers()
        {
            var members = _memberRepository.GetMembers();
            if (members == null)
            {
                return new KeyValuePair<MessageViewModel, List<MemberViewModel>>(
                    new MessageViewModel
                    {
                        StatusCode = System.Net.HttpStatusCode.NotFound,
                        Message = "There are no any member"
                    },
                    new List<MemberViewModel>()
                    );
            }
            return new KeyValuePair<MessageViewModel, List<MemberViewModel>>(

                new MessageViewModel
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Message = string.Empty
                },
                _mapper.Map<List<MemberViewModel>>(members)
                );
        }

        public KeyValuePair<MessageViewModel, MemberViewModel?> GetProfile(int id)
        {
            var member = _mapper.Map<MemberProfileResponseModel>(_memberRepository.GetMember(id));
            if (member == null)
            {
                return new KeyValuePair<MessageViewModel, MemberViewModel?>
                (
                    new MessageViewModel
                    {
                        StatusCode = System.Net.HttpStatusCode.NotFound,
                        Message = "Not found this member"
                    },
                    null
                );
            }
            var response = _mapper.Map<MemberViewModel>(member);
            return new KeyValuePair<MessageViewModel, MemberViewModel?>(
                new MessageViewModel
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Message = string.Empty
                },
                response
                );
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

        public MessageViewModel UpdateMembershipStatus(int id, UpdateMembershipStatusModel requestModel)
        {
            var member = _memberRepository.GetMember(id);
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
                var requestProperties = typeof(UpdateMembershipStatusModel).GetProperties();
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
