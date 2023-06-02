using BirdClubAPI.Domain.DTOs.Response.Activity;
using BirdClubAPI.Domain.Entities;

namespace BirdClubAPI.DataAccessLayer.Repositories.Activity
{
    public interface IActivityRepository
    {
        ActivityResponseModel? AttendanceActivity(Domain.Entities.Attendance requestModel);
        ActivityResponseModel? CreateActivity(Domain.Entities.Activity requestModel);
        List<ActivityResponseModel> GetActivities();
        Domain.Entities.Activity? GetActivities(int id);
        bool UpdateActivity(Domain.Entities.Activity activity);
    }
}
