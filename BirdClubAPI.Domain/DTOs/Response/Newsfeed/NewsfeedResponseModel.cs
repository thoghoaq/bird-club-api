using BirdClubAPI.Domain.Commons.Enums;
using BirdClubAPI.Domain.DTOs.Response.Blog;
using BirdClubAPI.Domain.DTOs.Response.Comment;
using BirdClubAPI.Domain.DTOs.Response.Member;
using BirdClubAPI.Domain.DTOs.Response.Record;

namespace BirdClubAPI.Domain.DTOs.Response.Newsfeed
{
    public class NewsfeedResponseModel
    {
        public int Id { get; set; }
        public DateTime PublicationTime { get; set; }
        public NewsfeedTypeEnum NewsfeedType { get; set; }
        public MemberResponseModel Owner { get; set; } = null!;
        public BlogResponseModel? Blog { get; set; }
        public RecordResponseModel? Record { get; set; }
    }
}
