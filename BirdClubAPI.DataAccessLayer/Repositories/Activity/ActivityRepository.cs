using AutoMapper;
using BirdClubAPI.DataAccessLayer.Context;
using BirdClubAPI.Domain.DTOs.Response.Activity;
using BirdClubAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;

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

        public ActivityResponseModel? AttendanceActivity(Domain.Entities.Attendance attendance)
        {
           try
            {
                var result = _context.Add(attendance);
                _context.SaveChanges();
                return _mapper.Map<ActivityResponseModel>(result.Entity);
            } 
            catch
            {
                return null;
            }
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

        public bool DeleteAttendanceRequest(AttendanceRequest request)
        {
            try
            {
                _context.AttendanceRequests.Remove(request);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<ActivityResponseModel> GetActivities()
        {
            var activities = _context.Activities
                .Where(e => e.Status == true)
                .Include(e => e.Owner)
                    .ThenInclude(e => e.User)
                .ToList();
            if (activities.IsNullOrEmpty())
            {
                return new List<ActivityResponseModel>();
            }
            return _mapper.Map<List<ActivityResponseModel>>(activities);
        }

        public Domain.Entities.Activity? GetActivities(int id)
        {
            var activity = _context.Activities
                .Include(e => e.Owner)
                    .ThenInclude(e => e.User)
                .SingleOrDefault(e => e.Id == id);
            if (activity == null || activity.Status == false)
            {
                return null;
            }
            return activity;
        }

        public AttendanceRequest? GetAttendanceRequest(int memberId, int activityId)
        {
            var request = _context.AttendanceRequests.Find(memberId, activityId);
            return request;
        }

        public Attendance? PostAttendance(int memberId, int activityId)
        {
            try
            {
                var result = _context.Attendances.Add(new Attendance
                {
                    MemberId = memberId,
                    ActivityId = activityId,
                    AttendanceTime = DateTime.UtcNow.AddHours(7)
                });
                _context.SaveChanges();
                return result.Entity;
            }
            catch
            {
                return null;
            }
        }

        public AttendanceRequest? PostAttendanceRequest(int memberId, int activityId)
        {
            try
            {
                var result = _context.AttendanceRequests.Add(new AttendanceRequest
                {
                    MemberId = memberId,
                    ActivityId = activityId,
                    RequestTime = DateTime.UtcNow.AddHours(7)
                });
                _context.SaveChanges();
                return result.Entity;
            }
            catch
            {
                return null;
            }
        }

        public bool RemoveAttendanceRequest(AttendanceRequest request)
        {
            try
            {
                var result = _context.AttendanceRequests.Remove(request);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateActivity(Domain.Entities.Activity activity)
        {
            try
            {
                _context.Activities.Attach(activity);
                _context.Entry(activity).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
