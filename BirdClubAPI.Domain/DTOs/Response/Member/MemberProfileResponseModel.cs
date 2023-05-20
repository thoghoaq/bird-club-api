using BirdClubAPI.Domain.DTOs.Response.User;

namespace BirdClubAPI.Domain.DTOs.Response.Member
{
    public class MemberProfileResponseModel
    {
        public int UserId { get; set; }
        public string? Phone { get; set; }
        public string? Avatar { get; set; }
        public string? Address { get; set; }
        public bool MembershipStatus { get; set; }
        public string? Birthday { get; set; }
        public string? About { get; set; }

        public UserResponseModel User { get; set; } = null!;
    }
}
