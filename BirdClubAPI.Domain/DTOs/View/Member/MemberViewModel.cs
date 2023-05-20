namespace BirdClubAPI.Domain.DTOs.View.Member
{
    public class MemberViewModel
    {
        public int Id { get; set; }
        public string DisplayName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string UserType { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Avatar { get; set; }
        public string? Address { get; set; }
        public bool MembershipStatus { get; set; }
        public string? Birthday { get; set; }
        public string? About { get; set; }
    }
}
