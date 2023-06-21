using AutoMapper;
using BirdClubAPI.DataAccessLayer.Repositories.Record;
using BirdClubAPI.Domain.DTOs.Request.Record;
using BirdClubAPI.Domain.DTOs.View.Common;
using BirdClubAPI.Domain.DTOs.View.Record;

namespace BirdClubAPI.BusinessLayer.Services.Record
{
    public class RecordService : IRecordService
    {
        private readonly IRecordRepository _recordRespository;
        private readonly IMapper _mapper;

        public RecordService(IRecordRepository recordRespository, IMapper mapper)
        {
            _recordRespository = recordRespository;
            _mapper = mapper;
        }

        public KeyValuePair<MessageViewModel, RecordViewModel?> AddRecord(AddRecordRequestModel requestModel)
        {
            var record = _mapper.Map<Domain.Entities.Record>(requestModel);
            var result = _recordRespository.CreateRecord(requestModel.OwnerId ,record);
            if (result == null)
            {
                return new KeyValuePair<MessageViewModel, RecordViewModel?>(
                    new MessageViewModel
                    {
                        StatusCode = System.Net.HttpStatusCode.InternalServerError,
                        Message = "Error occurs when insert this record"
                    }, null
                    );
            }
            return new KeyValuePair<MessageViewModel, RecordViewModel?>(
                new MessageViewModel
                {
                    StatusCode = System.Net.HttpStatusCode.Created,
                    Message = "Add record success"
                },
                null
                );
        }

        public KeyValuePair<MessageViewModel, List<RecordViewModel>> GetRecord()
        {
            var birds = _recordRespository.GetRecord();
            if(birds == null)
            {
                return new KeyValuePair<MessageViewModel, List<RecordViewModel>>
                  (new MessageViewModel
                  {
                      StatusCode = System.Net.HttpStatusCode.NotFound,
                      Message = "There are no any record" ,
                  },
                   new List<RecordViewModel>()
                  );
            }
     
            return new KeyValuePair<MessageViewModel, List<RecordViewModel>>
                (new MessageViewModel
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Message = string.Empty
                },
                  _mapper.Map<List<RecordViewModel>>(birds)
                 );
         
        }

        public KeyValuePair<MessageViewModel, List<RecordViewModel>> GetRecordsOfMember(int memberId)
        {
            var birds = _recordRespository.GetRecordByMember(memberId);
            if (birds == null)
            {
                return new KeyValuePair<MessageViewModel, List<RecordViewModel>>
                  (new MessageViewModel
                  {
                      StatusCode = System.Net.HttpStatusCode.NotFound,
                      Message = "There are no any bird",
                  },
                   new List<RecordViewModel>()
                  );
            }

            return new KeyValuePair<MessageViewModel, List<RecordViewModel>>
                (new MessageViewModel
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Message = string.Empty
                },
                  _mapper.Map<List<RecordViewModel>>(birds)
                 );
        }
    }
}
