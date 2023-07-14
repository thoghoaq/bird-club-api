using BirdClubAPI.Domain.DTOs.Request.Newsfeed.Blog;
using BirdClubAPI.Domain.DTOs.Request.Newsfeed.Comment;
using BirdClubAPI.Domain.DTOs.Response.Comment;
using BirdClubAPI.Domain.DTOs.View.Blog;
using BirdClubAPI.Domain.DTOs.View.Common;
using BirdClubAPI.Domain.DTOs.View.Newsfeed;

namespace BirdClubAPI.BusinessLayer.Services.Newsfeed
{
    public interface INewsfeedService
    {
        KeyValuePair<MessageViewModel, BlogViewModel?> CreateBlog(CreateBlogRequestModel requestModel);
        bool DeleteBlog(int id);
        KeyValuePair<MessageViewModel, BlogViewModel?> GetBlog(int id);
        List<BlogViewModel> GetBlogs();
        NewsfeedViewModel GetNewsFeed(int memberid);
        NewsfeedViewModel GetNewsfeeds(int page, int size, int? memberId = null);
        Task<CommentRm?> PostComment(NewsfeedCommentRequest request);
         Task<MessageViewModel> PostLiked(int memberId, int newsfeedId);
      
        KeyValuePair<MessageViewModel, BlogViewModel?> UpdateBlog(int id, UpdateBlogRm request);
    }
}
