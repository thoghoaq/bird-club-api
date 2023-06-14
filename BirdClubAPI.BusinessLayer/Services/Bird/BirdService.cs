using AutoMapper;

using BirdClubAPI.DataAccessLayer.Repositories.Bird;

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

         public KeyValuePair<MessageViewModel, List<BirdViewModel>> GetBirds()
        {
            var birds = _birdRespository.GetBirds();
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
