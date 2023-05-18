using AutoMapper;
using BirdClubAPI.Domain.Commons.Enums;
using BirdClubAPI.DataAccessLayer.Repositories.Newsfeed;
using BirdClubAPI.Domain.DTOs.View.Newsfeed;
using BirdClubAPI.Domain.DTOs.View.Common;
using BirdClubAPI.Domain.DTOs.View.Blog;
using BirdClubAPI.Domain.DTOs.Request.Newsfeed.Blog;
using BirdClubAPI.Domain.Entities;

namespace BirdClubAPI.BusinessLayer.Services.Newsfeed
{
    public class NewsfeedService : INewsfeedService
    {
        private readonly INewsfeedRepository _newsfeedRepository;
        private readonly IMapper _mapper;

        public NewsfeedService(INewsfeedRepository newsfeedRepository, IMapper mapper)
        {
            _newsfeedRepository = newsfeedRepository;
            _mapper = mapper;
        }

        public KeyValuePair<MessageViewModel, BlogViewModel?> CreateBlog(CreateBlogRequestModel requestModel)
        {
            Domain.Entities.Newsfeed blog = new Domain.Entities.Newsfeed
            {
                OwnerId = requestModel.OwnerId,
                PublicationTime = DateTime.UtcNow.AddHours(7),
                Blog = new Blog
                {
                    Content = requestModel.Content,
                    Title = requestModel.Title,
                },
            };
            var result = _newsfeedRepository.Create(blog);
            if (result == null)
            {
                return new KeyValuePair<MessageViewModel, BlogViewModel?>(
                    new MessageViewModel
                    {
                        Message = "Internal error, can not post this blog",
                        StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    }, null
                    );
            }
            return new KeyValuePair<MessageViewModel, BlogViewModel?>(
                new MessageViewModel
                {
                    Message = string.Empty,
                    StatusCode = System.Net.HttpStatusCode.OK,
                },
                _mapper.Map<BlogViewModel>(_newsfeedRepository.GetBlogs(result.Id))
                );
        }

        public NewsfeedViewModel GetNewsfeeds(int limit, int page, int size)
        {
            var newsfeeds = _newsfeedRepository.GetNewsfeeds(limit, page, size);
            var response = new NewsfeedViewModel
            {
                Total = newsfeeds.Count,
                Newsfeeds = newsfeeds
            };
            return response;
        }
    }
}
