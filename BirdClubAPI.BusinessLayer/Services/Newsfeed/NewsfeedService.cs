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

        public List<NewsfeedViewModel> GetNewsfeeds(int limit, int page, int size)
        {
            var result = _mapper.Map<List<NewsfeedViewModel>>(_newsfeedRepository.GetNewsfeeds(limit, page, size)); 
            foreach (var item in result)
            {
                if (item.Blog != null) item.NewsfeedType = NewsfeedTypeEnum.BLOG;
                else if (item.Record != null) item.NewsfeedType = NewsfeedTypeEnum.RECORD;
            };
            return result;
        }
    }
}
