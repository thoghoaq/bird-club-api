using BirdClubAPI.Domain.DTOs.Response.Activity;
using BirdClubAPI.Domain.DTOs.Response.Member;

namespace BirdClubAPI.DataAccessLayer.Repositories.Activity
{
    public interface IActivityRepository
    {
        ActivityResponseModel? CreateActivity(Domain.Entities.Activity requestModel);
        List<ActivityResponseModel> GetActivities();
        Domain.Entities.Activity? GetActivities(int id);
        Domain.Entities.Activity? GetActivity(int activityId);
        List<MemberResponseModel> GetAttendance();
        bool UpdateActivity(Domain.Entities.Activity activity);
    }
}
