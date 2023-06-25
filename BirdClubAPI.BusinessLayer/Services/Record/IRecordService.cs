using BirdClubAPI.Domain.DTOs.Request.Record;
using BirdClubAPI.Domain.DTOs.View.Common;
using BirdClubAPI.Domain.DTOs.View.Record;

namespace BirdClubAPI.BusinessLayer.Services.Record
{
    public interface IRecordService
    {
        KeyValuePair<MessageViewModel, List<RecordViewModel>> GetRecord();
        KeyValuePair<MessageViewModel, RecordViewModel?> AddRecord(AddRecordRequestModel requestModel);
        KeyValuePair<MessageViewModel, List<RecordViewModel>> GetRecordsOfMember(int memberId);
    }
}