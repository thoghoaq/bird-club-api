using BirdClubAPI.Domain.DTOs.Request.Activity;
using BirdClubAPI.Domain.DTOs.Response.Activity;
using BirdClubAPI.Domain.DTOs.View.Acitivity;
using BirdClubAPI.Domain.DTOs.View.Common;

namespace BirdClubAPI.BusinessLayer.Services.Activity
{
    public interface IActivityService
    {
        KeyValuePair<MessageViewModel, AttendanceActivityViewModel?> AttendanceActivity(AttendanceActivityRequestModel requestModel);
        KeyValuePair<MessageViewModel, AcitivityCreateViewModel?> CreateActivity(CreateActivityRequestModel requestModel);
        Task<MessageViewModel> DeclineAttendance(int memberId, int activityId);
        List<AcitivityViewModel> GetActivities(bool? isAll);
        KeyValuePair<MessageViewModel, AcitivityViewModel?> GetActivities(int id);
        KeyValuePair<MessageViewModel, List<AttendanceViewModel?>> GetAttendance(int id);
        List<AcitivityViewModel> GetActivitiesByOwner(int ownerId);
        List<ActivityCalenderViewModel> GetCalenderActivities();
        List<ActivityCalenderViewModel> GetCalenderActivitiesByMember(int memberId);
        Task<MessageViewModel> PostAttendance(int memberId, int activityId);
        Task<MessageViewModel> RequestAttendance(int memberId, int activityId);
        AttendanceStatusRm GetUserAttendanceStatus(int id, int memberId);
        MessageViewModel UpdateActivity(int id, UpdateActivityRequestModel requestModel);
        MessageViewModel UpdateActivityStatus(int id, UpdateActivityStatusRequestModel requestModel);
        KeyValuePair<MessageViewModel, List<AttendanceRequestRm>> GetAttendanceRequests(int id);
        Task<bool> PostComment(int id, ActivityCommentRequest request);
    }
}
