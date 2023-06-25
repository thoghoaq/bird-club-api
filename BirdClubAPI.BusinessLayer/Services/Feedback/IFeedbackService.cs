using BirdClubAPI.Domain.DTOs.View.Feedback;

namespace BirdClubAPI.BusinessLayer.Services.Feedback
{
    public interface IFeedbackService
    {
        //KeyValuePair<MessageViewModel, CreateFeedbackViewModel> CreateFeedback(CreateFeedbackRequestModel requestModel);
        List<FeedbackViewModel> GetFeedbacks(int activityId);

    }
}
