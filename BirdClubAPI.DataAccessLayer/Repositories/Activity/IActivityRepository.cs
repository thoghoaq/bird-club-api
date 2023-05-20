using BirdClubAPI.Domain.DTOs.Response.Activity;

namespace BirdClubAPI.DataAccessLayer.Repositories.Activity
{
    public interface IActivityRepository
    {
        ActivityResponseModel? CreateActivity(Domain.Entities.Activity requestModel);
        List<ActivityResponseModel> GetActivities();
        Domain.Entities.Activity? GetActivities(int id);
        bool UpdateActivity(Domain.Entities.Activity activity);
    }
}
