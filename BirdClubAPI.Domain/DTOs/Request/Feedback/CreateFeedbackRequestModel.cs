namespace BirdClubAPI.Domain.DTOs.Request.Feedback
{
    public  class CreateFeedbackRequestModel
    {
        public int OwnerId { get; set; }
        public int ActivityId { get; set; }
        public string Content { get; set; } = null!;
        public int Rating { get; set; }
    }
}
