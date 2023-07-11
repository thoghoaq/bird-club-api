namespace BirdClubAPI.Domain.DTOs.View.Record
{
    public class RecordViewModel
    {
        public int NewsfeedId { get; set; }
        public int BirdId { get; set; }
        public string BirdName { get; set; } = null!;
        public string Species { get; set; } = null!;
        public int Quantity { get; set; }
        public string? Photo { get; set; }
    }
}
