namespace BirdClubAPI.Domain.DTOs.Response.Activity
{
    public class AttendanceRequestRm
    {
        public int MemberId { get; set; }
        public string? Avatar { get; set; }
        public string DisplayName { get; set; } = null!;
        public string UserType { get; set; } = null!;
        public DateTime RequestTime { get; set; }
    }
}
