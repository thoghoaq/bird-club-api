using BirdClubAPI.Domain.DTOs.Request.Feedback;
using BirdClubAPI.Domain.DTOs.Response.Feedback;
using BirdClubAPI.Domain.DTOs.View.Common;
using BirdClubAPI.Domain.DTOs.View.Feedback;

namespace BirdClubAPI.BusinessLayer.Services.Feedback
{
    public interface IFeedbackService
    {
        KeyValuePair<MessageViewModel, FeedbackResponseModel?> CreateFeedback(CreateFeedbackRequestModel requestModel);
        List<FeedbackViewModel> GetFeedbacks(int activityId);

    }
}
