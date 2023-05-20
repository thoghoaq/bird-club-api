using BirdClubAPI.Domain.DTOs.Response.Activity;

namespace BirdClubAPI.DataAccessLayer.Repositories.Activity
{
    public interface IActivityRepository
    {
        ActivityResponseModel? CreateActivity(Domain.Entities.Activity requestModel);
    }
}
