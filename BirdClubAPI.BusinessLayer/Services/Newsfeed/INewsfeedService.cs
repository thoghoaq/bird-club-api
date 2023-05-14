using BirdClubAPI.Domain.DTOs.Response.Newsfeed;
using BirdClubAPI.Domain.DTOs.View.Newsfeed;

namespace BirdClubAPI.BusinessLayer.Services.Newsfeed
{
    public interface INewsfeedService
    {
        List<NewsfeedViewModel> GetNewsfeeds();
    }
}
