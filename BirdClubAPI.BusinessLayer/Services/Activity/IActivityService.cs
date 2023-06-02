using BirdClubAPI.Domain.DTOs.Request.Activity;
using BirdClubAPI.Domain.DTOs.View.Acitivity;
using BirdClubAPI.Domain.DTOs.View.Common;

namespace BirdClubAPI.BusinessLayer.Services.Activity
{
    public interface IActivityService
    {
        KeyValuePair<MessageViewModel, AttendanceActivityViewModel?> AttendanceActivity(AttendanceActivityRequestModel requestModel);
        KeyValuePair<MessageViewModel, AcitivityCreateViewModel?> CreateActivity(CreateActivityRequestModel requestModel);
        MessageViewModel DeclineAttendance(int memberId, int activityId);
        List<AcitivityViewModel> GetActivities();
        KeyValuePair<MessageViewModel, AcitivityViewModel?> GetActivities(int id);
        List<AcitivityViewModel> GetActivitiesByOwner(int ownerId);
        List<ActivityCalenderViewModel> GetCalenderActivities();
        MessageViewModel PostAttendance(int memberId, int activityId);
        MessageViewModel RequestAttendance(int memberId, int activityId);
        MessageViewModel UpdateActivity(int id, UpdateActivityRequestModel requestModel);
        MessageViewModel UpdateActivityStatus(int id, UpdateActivityStatusRequestModel requestModel);
    }
}
