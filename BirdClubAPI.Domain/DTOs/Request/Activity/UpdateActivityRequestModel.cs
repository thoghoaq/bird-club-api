namespace BirdClubAPI.Domain.DTOs.Request.Activity
{
    public class UpdateActivityRequestModel
    {
        public string? Name { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }
        public string? ActivityType { get; set; }
        public string? Background { get; set; }
    }
}
