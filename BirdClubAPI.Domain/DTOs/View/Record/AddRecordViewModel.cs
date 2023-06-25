namespace BirdClubAPI.Domain.DTOs.View.Record
{
    public class AddRecordViewModel
    {
        public string Name { get; set; } = null!;
        public string? Species { get; set; }
        public int Id { get; set; }
    }
}
