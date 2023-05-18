using BirdClubAPI.Domain.Commons.Enums;
using BirdClubAPI.Domain.DTOs.Response.Blog;
using BirdClubAPI.Domain.DTOs.Response.Member;
using BirdClubAPI.Domain.DTOs.Response.Record;

namespace BirdClubAPI.Domain.DTOs.View.Newsfeed
{
    public class NewsfeedViewModel
    {
        public int Id { get; set; }
        public NewsfeedTypeEnum NewsfeedType { get; set; }
        public DateTime PublicationTime { get; set; }
        public MemberResponseModel Owner { get; set; } = null!;
        public BlogResponseModel? Blog { get; set; }
        public RecordResponseModel? Record { get; set; }
    }
}
