using BirdClubAPI.Domain.DTOs.Response.Record;

namespace BirdClubAPI.DataAccessLayer.Repositories.Record
{
    public interface IRecordRepository
    {
        Domain.Entities.Record? GetRecords(int id);
        List<RecordResponseModel> GetRecord();
        Domain.Entities.Newsfeed? CreateRecord(int ownerId, Domain.Entities.Record requestModel);
        List<RecordResponseModel> GetRecordByMember(int memberId);
        Domain.Entities.Record? GetNewfeed(int recordId);
        bool EditRecord(Domain.Entities.Record record);
    }
}
