using AutoMapper;
using BirdClubAPI.DataAccessLayer.Context;
using BirdClubAPI.Domain.DTOs.Response.Record;
using Microsoft.EntityFrameworkCore;

namespace BirdClubAPI.DataAccessLayer.Repositories.Record
{
    public class RecordRepository : IRecordRepository
    {
        private readonly BirdClubContext _context;
        private readonly IMapper _mapper;

        public RecordRepository(BirdClubContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Domain.Entities.Newsfeed? CreateRecord(int ownerId,Domain.Entities.Record record)
        {
            try
            {
                var result = _context.Newsfeeds.Add(new Domain.Entities.Newsfeed
                {
                    PublicationTime = DateTime.UtcNow.AddHours(7),
                    Record = record,
                    OwnerId = ownerId
                });
                _context.SaveChanges();
                return _mapper.Map<Domain.Entities.Newsfeed>(result.Entity);
            }
            catch
            {
                return null;
            }
        }

        public Domain.Entities.Record? DeleteRecord(int id)
        {
            var record = _context.Records.Where(e => e.NewsfeedId== id).FirstOrDefault();
            if (record != null)
            {
                _context.Records.Remove(record);
                _context.SaveChanges();
                var newfeed = _context.Newsfeeds.Where(e => e.Id== id).FirstOrDefault();
                _context.Newsfeeds.Remove(newfeed);
                _context.SaveChanges();
            }return null;
        }

        public bool EditRecord(Domain.Entities.Record record)
        {
            try
            {

                _context.Records.Update(record);
                _context.SaveChanges();
                return true;

            }
            catch
            {
                return false;
            }
        }

        public Domain.Entities.Record? GetNewfeed(int recordId)
        {
            return _context.Records.Where(e => e.NewsfeedId== recordId).FirstOrDefault();
        }

        public List<RecordResponseModel> GetRecord()
        {
            return _context.Records
                .Include(e => e.Bird)
                .Select(b => new RecordResponseModel
            {
                NewsfeedId = b.NewsfeedId,
                BirdName = b.Bird.Name,
                BirdId = b.Bird.Id,
                Species = b.Bird.Species ?? string.Empty,
                Photo = b.Photo,
                Quantity = b.Quantity
            }).ToList();
        }

        public List<RecordResponseModel> GetRecordByMember(int memberId)
        {
            return _context.Records
                .Include(e => e.Bird)
                .Include(e => e.Newsfeed)
                .Where(e => e.Newsfeed.OwnerId == memberId)
                .Select(b => new RecordResponseModel
                {
                    NewsfeedId = b.NewsfeedId,
                    BirdName = b.Bird.Name,
                    BirdId = b.Bird.Id,
                    Species = b.Bird.Species ?? string.Empty,
                    Quantity = b.Quantity,
                    Photo = b.Photo
                }).ToList();
        }

        public Domain.Entities.Record? GetRecords(int id)
        {
            return _context.Records.Where(e => e.NewsfeedId ==  id).FirstOrDefault();
        }
    }
}
