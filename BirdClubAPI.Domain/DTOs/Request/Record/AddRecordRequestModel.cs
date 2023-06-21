namespace BirdClubAPI.Domain.DTOs.Request.Record
{
    public class AddRecordRequestModel
    {
        public int OwnerId { get; set; }
        public int BirdId { get; set; }
        public int Quantity { get; set; } 
        public string? Photo { get; set; }
    }
}
