namespace BirdClubAPI.Domain.DTOs.Request.Member
{
    public class UpdateMemberRequestModel
    {
        public string? DisplayName { get; set; }
        public string? Phone { get; set; }
        public string? Avatar { get; set; }
        public string? Address { get; set; }
    }
}
