namespace BirdClubAPI.Domain.DTOs.Request.Activity
{
    public class CreateActivityRequestModel
    {
        public string Name { get; set; } = null!;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }
        public string ActivityType { get; set; } = null!;
        public int OwnerId { get; set; }
    }
}
