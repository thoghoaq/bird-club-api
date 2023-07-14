using BirdClubAPI.Domain.Commons.Enums;

namespace BirdClubAPI.Domain.DTOs.Response.Activity
{
    public class AttendanceStatusRm
    {
        public AttendanceStatusEnum Status { get; set; }
        public string Message { get; set; } = null!;
        public bool? IsFeedback { get; set; }
    }
}
