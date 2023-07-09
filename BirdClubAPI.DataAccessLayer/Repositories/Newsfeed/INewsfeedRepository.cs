using BirdClubAPI.Domain.DTOs.Response.Blog;
using BirdClubAPI.Domain.DTOs.Response.Newsfeed;
using BirdClubAPI.Domain.DTOs.View.Blog;
using BirdClubAPI.Domain.Entities;

namespace BirdClubAPI.DataAccessLayer.Repositories.Newsfeed
{
    public interface INewsfeedRepository
    {
        List<NewsfeedResponseModel> GetNewsfeeds(int page, int size);
        Domain.Entities.Newsfeed? Create(Domain.Entities.Newsfeed newsfeed);
        BlogDetailResponseModel? GetBlogs(int id);
        bool UpdateBlog(Domain.Entities.Blog blog);
        List<NewsfeedResponseModel> GetNewsFeed(int memberid);
        int GetNewsFeedCount();
        Domain.Entities.Like? GetLike(int memberId, int newsfeedId);
        bool Unliked(Domain.Entities.Like alreadyLiked);
        Domain.Entities.Like? PostLiked(int memberId, int newsfeedId);
        Domain.Entities.Newsfeed? GetNewsFeedById(int newsfeedId);
        List<BlogViewModel> GetBlogs();
    }
}
