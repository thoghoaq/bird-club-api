namespace BirdClubAPI.Domain.DTOs.Request.Newsfeed.Comment
{
    public class NewsfeedCommentRequest
    {
        public int NewsfeedId { get; set; }
        public int OwnerId { get; set; }
        public string Content { get; set; } = null!;
    }
}
