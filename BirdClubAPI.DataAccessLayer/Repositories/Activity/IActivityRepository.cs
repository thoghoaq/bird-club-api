using BirdClubAPI.Domain.DTOs.Response.Activity;
using BirdClubAPI.Domain.Entities;

namespace BirdClubAPI.DataAccessLayer.Repositories.Activity
{
    public interface IActivityRepository
    {
        ActivityResponseModel? CreateActivity(Domain.Entities.Activity requestModel);
        bool DeleteAttendanceRequest(AttendanceRequest request);
        List<ActivityResponseModel> GetActivities();
        Domain.Entities.Activity? GetActivities(int id);
        AttendanceRequest? GetAttendanceRequest(int memberId, int activityId);
        Attendance? PostAttendance(int memberId, int activityId);
        AttendanceRequest? PostAttendanceRequest(int memberId, int activityId);
        bool RemoveAttendanceRequest(AttendanceRequest request);
        bool UpdateActivity(Domain.Entities.Activity activity);
    }
}
