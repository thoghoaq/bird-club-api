using BirdClubAPI.Domain.DTOs.Response.Member;

namespace BirdClubAPI.Domain.DTOs.Response.Comment
{
    public class CommentRm
    {
        public string Content { get; set; } = null!;
        public DateTime PublicationTime { get; set; }
        public int OwnerId { get; set; }
        public string Type { get; set; } = null!;
        public int ReferenceId { get; set; }
        public int Id { get; set; }

        public virtual MemberResponseModel Owner { get; set; } = null!;
    }
}
