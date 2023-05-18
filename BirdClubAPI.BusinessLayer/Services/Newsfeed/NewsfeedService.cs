using AutoMapper;
using BirdClubAPI.Domain.Commons.Enums;
using BirdClubAPI.DataAccessLayer.Repositories.Newsfeed;
using BirdClubAPI.Domain.DTOs.View.Newsfeed;

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
