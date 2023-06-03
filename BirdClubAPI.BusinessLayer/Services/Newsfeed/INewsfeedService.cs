using BirdClubAPI.Domain.DTOs.Request.Newsfeed.Blog;
using BirdClubAPI.Domain.DTOs.Response.Newsfeed;
using BirdClubAPI.Domain.DTOs.View.Blog;
using BirdClubAPI.Domain.DTOs.View.Common;
using BirdClubAPI.Domain.DTOs.View.Newsfeed;

namespace BirdClubAPI.BusinessLayer.Services.Newsfeed
{
    public interface INewsfeedService
    {
        KeyValuePair<MessageViewModel, BlogViewModel?> CreateBlog(CreateBlogRequestModel requestModel);
        KeyValuePair<MessageViewModel, BlogViewModel?> GetBlog(int id);
        NewsfeedViewModel GetNewsfeeds(int limit, int page, int size);
    }
}
