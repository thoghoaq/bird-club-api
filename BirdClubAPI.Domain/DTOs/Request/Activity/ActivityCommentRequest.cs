namespace BirdClubAPI.Domain.DTOs.Request.Activity
{
    public class ActivityCommentRequest
    {
        public int OwnerId { get; set; }
        public string Content { get; set; } = null!;
    }
}
