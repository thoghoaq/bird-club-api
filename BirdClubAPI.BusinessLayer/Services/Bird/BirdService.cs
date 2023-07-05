using AutoMapper;
using BirdClubAPI.DataAccessLayer.Repositories.Bird;
using BirdClubAPI.Domain.DTOs.Response.Bird;

namespace BirdClubAPI.BusinessLayer.Services.Bird
{
    public class BirdService : IBirdService
    {
        private readonly IBirdRepository _birdRepository;
        private readonly IMapper _mapper;

        public BirdService(IBirdRepository birdRepository, IMapper mapper)
        {
            _birdRepository = birdRepository;
            _mapper = mapper;
        }

        public List<BirdResponseModel> GetBirds()
        {
            return _mapper.Map<List<BirdResponseModel>>(_birdRepository.GetBirds());
        }
    }
}
