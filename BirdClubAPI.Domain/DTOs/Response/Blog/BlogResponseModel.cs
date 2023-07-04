using BirdClubAPI.Domain.DTOs.Response.Comment;

namespace BirdClubAPI.Domain.DTOs.Response.Blog
{
    public class BlogResponseModel
    {
        public string Title { get; set; } = null!;
        public string? Content { get; set; }
        public List<CommentRm> Comments { get; set; } = new List<CommentRm>();
    }
}
