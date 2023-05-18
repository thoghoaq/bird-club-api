using BirdClubAPI.Domain.DTOs.Response.Newsfeed;

namespace BirdClubAPI.Domain.DTOs.View.Newsfeed
{
    public class NewsfeedViewModel
    {
        public int Total { get; set; }
        public List<NewsfeedResponseModel> Newsfeeds { get; set; } = new List<NewsfeedResponseModel>();
    }
}
