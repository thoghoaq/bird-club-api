using BirdClubAPI.Domain.DTOs.Response.User;

namespace BirdClubAPI.Domain.DTOs.Response.Member
{
    public class MemberResponseModel
    {
        public int MemberId { get; set; }
        public string? Avatar { get; set; }
        public string DisplayName { get; set; } = null!;
    }
}
