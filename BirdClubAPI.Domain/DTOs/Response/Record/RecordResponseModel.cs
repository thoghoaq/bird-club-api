using BirdClubAPI.Domain.DTOs.Response.Comment;

namespace BirdClubAPI.Domain.DTOs.Response.Record
{
    public class RecordResponseModel
    {
        public int BirdId { get; set; }
        public string BirdName { get; set; } = null!;
        public string Species { get; set; } = null!;
        public int Quantity { get; set; }
        public string? Photo { get; set; }
        public List<CommentRm> Comments { get; set; } = new List<CommentRm>();
        public int? LikeCount { get; set; }
        public bool? IsLiked { get; set; }
    }
}
