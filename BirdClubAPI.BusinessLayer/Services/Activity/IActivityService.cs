using BirdClubAPI.Domain.DTOs.Request.Activity;
using BirdClubAPI.Domain.DTOs.View.Acitivity;
using BirdClubAPI.Domain.DTOs.View.Common;

namespace BirdClubAPI.BusinessLayer.Services.Activity
{
    public interface IActivityService
    {
        KeyValuePair<MessageViewModel, AcitivityCreateViewModel?> CreateActivity(CreateActivityRequestModel requestModel);
        List<AcitivityViewModel> GetActivities();
        KeyValuePair<MessageViewModel, AcitivityViewModel?> GetActivities(int id);
        List<ActivityCalenderViewModel> GetCalenderActivities();
        MessageViewModel UpdateActivity(int id, UpdateActivityRequestModel requestModel);
        MessageViewModel UpdateActivityStatus(int id, UpdateActivityStatusRequestModel requestModel);
    }
}
