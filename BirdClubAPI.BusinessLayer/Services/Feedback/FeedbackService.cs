using AutoMapper;
using BirdClubAPI.BusinessLayer.Helpers;
using BirdClubAPI.DataAccessLayer.Repositories.Activity;
using BirdClubAPI.DataAccessLayer.Repositories.Feedback;
using BirdClubAPI.Domain.DTOs.Request.Feedback;
using BirdClubAPI.Domain.DTOs.Response.Feedback;
using BirdClubAPI.Domain.DTOs.View.Common;
using BirdClubAPI.Domain.DTOs.View.Feedback;

namespace BirdClubAPI.BusinessLayer.Services.Feedback
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IMapper _mapper;
        private readonly IActivityRepository _activityRepository;


        public FeedbackService (IFeedbackRepository feedbackRepository, IMapper mapper, IActivityRepository activityRepository)
        {
            _feedbackRepository = feedbackRepository;
            _mapper = mapper;
            _activityRepository = activityRepository;
        }

        public async Task<KeyValuePair<MessageViewModel, FeedbackResponseModel?>> CreateFeedback(CreateFeedbackRequestModel requestModel)
        {
            var feedback = new Domain.Entities.Feedback
            {
                ActivityId = requestModel.ActivityId,
                OwnerId = requestModel.OwnerId,
                Rating = requestModel.Rating,
                Time = DateTime.UtcNow.AddHours(7),
                Content = requestModel.Content
            };
            var result = _feedbackRepository.CreateFeedback(feedback);
            if (result == null)
            {
                return new KeyValuePair<MessageViewModel, FeedbackResponseModel?>(
                    new MessageViewModel
                    {
                        StatusCode = System.Net.HttpStatusCode.InternalServerError,
                        Message = "Error occur when create this feedback"
                    }, null
                    );
            }

            var owner = _activityRepository.GetActivity(requestModel.ActivityId)!;
            var notification = new Notification
            {
                Title = "Activity",
                Message = $"There are new member (id: {requestModel.OwnerId}) feedback your activity: {requestModel.ActivityId}"
            };
            await FirebaseHelper.Write(owner.OwnerId, notification);

            return new KeyValuePair<MessageViewModel, FeedbackResponseModel?>(
                new MessageViewModel
                {
                    StatusCode = System.Net.HttpStatusCode.Created,
                    Message = string.Empty
                },
                _mapper.Map<FeedbackResponseModel>(result)
                );
        }

        public List<FeedbackViewModel> GetFeedbacks(int activityId)
        {
            List<FeedbackResponseModel> feedbacks = _feedbackRepository.GetFeedbacks(activityId); 
            return _mapper.Map<List<FeedbackViewModel>>(feedbacks);
        }
    }
}
