using BirdClubAPI.Domain.DTOs.Response.Member;

namespace BirdClubAPI.Domain.DTOs.View.Blog
{
    public class BlogViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Content { get; set; }
        public DateTime PublicationTime { get; set; }
        public MemberResponseModel Owner { get; set; } = null!;
    }
}
