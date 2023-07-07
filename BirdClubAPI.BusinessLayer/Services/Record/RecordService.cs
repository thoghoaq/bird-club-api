using AutoMapper;
using AutoMapper.Execution;
using BirdClubAPI.DataAccessLayer.Repositories.Record;
using BirdClubAPI.Domain.DTOs.Request.Member;
using BirdClubAPI.Domain.DTOs.Request.Record;
using BirdClubAPI.Domain.DTOs.View.Common;
using BirdClubAPI.Domain.DTOs.View.Record;
using BirdClubAPI.Domain.Entities;
using System.Diagnostics.Metrics;

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

        public MessageViewModel DeleteRecord(int id)
        {
            var record = _recordRespository.DeleteRecord(id);
            if (record != null)
            {
                return new MessageViewModel
                {
                    StatusCode = System.Net.HttpStatusCode.NoContent,
                    Message = string.Empty
                };
            }
            else
            {
                return new MessageViewModel
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Message = string.Empty
                };
            }
        }

        public MessageViewModel EditRecord(int recordId, EditRecordRequestModel requestModel)
        {
            var record = _recordRespository.GetNewfeed(recordId);
            if (record == null)
            {
                return new MessageViewModel
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "Not found this record"
                };
            }
            else
            {
                var requestProperties = typeof (EditRecordRequestModel).GetProperties();
                foreach ( var property in requestProperties )
                {
                    var value = property.GetValue(requestModel);
                    if(value != null)
                    {
                        var recordProperty = record.GetType().GetProperty(property.Name);
                        if (recordProperty != null)
                        {
                            recordProperty?.SetValue(record, value);
                        }
                    }
                }
                if (requestModel.Photo!= null)
                {
                    record.Photo= requestModel.Photo;
                }
                var result = _recordRespository.EditRecord(record);
                if (result == true) 
                {
                    return new MessageViewModel
                    {
                        StatusCode = System.Net.HttpStatusCode.NoContent,
                        Message = "Update record successful"
                    };
                }
                else
                {
                    return new MessageViewModel
                    {
                        StatusCode = System.Net.HttpStatusCode.Conflict,
                        Message = "Can not update record"
                    };
                }
            }

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
