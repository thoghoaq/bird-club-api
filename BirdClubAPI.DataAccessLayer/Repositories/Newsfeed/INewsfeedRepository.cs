using BirdClubAPI.Domain.DTOs.Response.Newsfeed;

namespace BirdClubAPI.DataAccessLayer.Repositories.Newsfeed
{
    public interface INewsfeedRepository
    {
        List<NewsfeedResponseModel> GetNewsfeeds();
    }
}
