namespace BirdClubAPI.Domain.DTOs.View.Acitivity
{
    public class ActivityCalenderViewModel
    {
        public string Name { get; set; } = null!;
        public DateTime CreateTime { get; set; }
        public DateTime Date { get; set; }
        public string StartTime { get; set; } = null!;
        public string EndTime { get; set; } = null!;
        public string? Location { get; set; }
        public string ActivityType { get; set; } = null!;
        public int Id { get; set; }
    }
}
