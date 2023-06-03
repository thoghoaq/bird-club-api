

namespace BirdClubAPI.Domain.DTOs.View.Acitivity
{
    public class AttendanceViewModel
    {
       
        public int MemberId { get; set; }
        public string? Avatar { get; set; }
        public string DisplayName { get; set; } = null!;
    }
}
