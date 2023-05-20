using AutoMapper;
using BirdClubAPI.DataAccessLayer.Context;
using BirdClubAPI.Domain.DTOs.Response.Activity;

namespace BirdClubAPI.DataAccessLayer.Repositories.Activity
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly BirdClubContext _context;
        private readonly IMapper _mapper;

        public ActivityRepository(BirdClubContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ActivityResponseModel? CreateActivity(Domain.Entities.Activity activity)
        {
            try
            {
                var result = _context.Add(activity);
                _context.SaveChanges();
                return  _mapper.Map<ActivityResponseModel>(result.Entity);
            }
            catch
            {
                return null;
            }
        }
    }
}
