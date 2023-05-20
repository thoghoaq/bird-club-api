using AutoMapper;
using BirdClubAPI.DataAccessLayer.Context;
using BirdClubAPI.Domain.DTOs.Response.Activity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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

        public List<ActivityResponseModel> GetActivities()
        {
            var activities = _context.Activities
                .Include(e => e.Owner)
                    .ThenInclude(e => e.User)
                .ToList();
            if (activities.IsNullOrEmpty())
            {
                return new List<ActivityResponseModel>();
            }
            return _mapper.Map<List<ActivityResponseModel>>(activities);
        }
    }
}
