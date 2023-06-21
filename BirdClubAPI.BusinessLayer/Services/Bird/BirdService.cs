using AutoMapper;

using BirdClubAPI.DataAccessLayer.Repositories.Bird;
using BirdClubAPI.Domain.DTOs.Request.Bird;
using BirdClubAPI.Domain.DTOs.View.Bird;
using BirdClubAPI.Domain.DTOs.View.Common;


namespace BirdClubAPI.BusinessLayer.Services.Bird
{
    public class BirdService : IBirdService
    {
        private readonly IBirdRepository _birdRespository;
        private readonly IMapper _mapper;

        public BirdService(IBirdRepository birdRespository, IMapper mapper)
        {
            _birdRespository = birdRespository;
            _mapper = mapper;
        }

        public KeyValuePair<MessageViewModel, AddBirdViewModel?> AddBird(AddBirdRequestModel requestModel)
        {
            var bird = _mapper.Map<Domain.Entities.Bird>(requestModel);
            var result = _birdRespository.CreateBird(bird);
            if (result == null)
            {
                return new KeyValuePair<MessageViewModel, AddBirdViewModel?>(
                    new MessageViewModel
                    {
                        StatusCode = System.Net.HttpStatusCode.InternalServerError,
                        Message = "Error occurs when insert this bird"
                    }, null
                    );
            }

            return new KeyValuePair<MessageViewModel, AddBirdViewModel?>(
                new MessageViewModel
                {
                    StatusCode = System.Net.HttpStatusCode.Created,
                    Message = string.Empty
                },
                _mapper.Map<AddBirdViewModel>(result)
                );
        }

        public KeyValuePair<MessageViewModel, List<BirdViewModel>> GetBird()
        {
            var birds = _birdRespository.GetBird();
            if(birds == null)
            {
                return new KeyValuePair<MessageViewModel, List<BirdViewModel>>
                  (new MessageViewModel
                  {
                      StatusCode = System.Net.HttpStatusCode.NotFound,
                      Message = "There are no any bird" ,
                  },
                   new List<BirdViewModel>()
                  );
            }
     
            return new KeyValuePair<MessageViewModel, List<BirdViewModel>>
                (new MessageViewModel
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Message = string.Empty
                },
                  _mapper.Map<List<BirdViewModel>>(birds)
                 );
         
        }
    }
}
