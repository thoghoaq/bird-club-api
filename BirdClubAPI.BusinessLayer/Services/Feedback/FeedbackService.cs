using AutoMapper;
using BirdClubAPI.DataAccessLayer.Repositories.Bird;
using BirdClubAPI.DataAccessLayer.Repositories.Feedback;
using BirdClubAPI.Domain.DTOs.Request.Feedback;
using BirdClubAPI.Domain.DTOs.Response.Activity;
using BirdClubAPI.Domain.DTOs.Response.Feedback;
using BirdClubAPI.Domain.DTOs.View.Acitivity;
using BirdClubAPI.Domain.DTOs.View.Bird;
using BirdClubAPI.Domain.DTOs.View.Common;
using BirdClubAPI.Domain.DTOs.View.Feedback;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace BirdClubAPI.BusinessLayer.Services.Feedback
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IMapper _mapper;


        public FeedbackService (IFeedbackRepository feedbackRepository, IMapper mapper)
        {
            _feedbackRepository = feedbackRepository;
            _mapper = mapper;
        }

        public KeyValuePair<MessageViewModel, CreateFeedbackViewModel> CreateFeedback(CreateFeedbackRequestModel requestModel)
        {
            var feedback = _mapper.Map<Domain.Entities.Feedback>(requestModel);
            var result = _feedbackRepository.CreateFeedback(feedback);
            if (result == null)
            {
                return new KeyValuePair<MessageViewModel, CreateFeedbackViewModel?>(
                    new MessageViewModel
                    {
                        StatusCode = System.Net.HttpStatusCode.InternalServerError,
                        Message = "Error occur when create this feedback"
                    }, null
                    );
            }

            return new KeyValuePair<MessageViewModel, CreateFeedbackViewModel?>(
                new MessageViewModel
                {
                    StatusCode = System.Net.HttpStatusCode.Created,
                    Message = string.Empty
                }, 
                _mapper.Map<CreateFeedbackViewModel>(result)
                );
         }

        public List<FeedbackViewModel> GetFeedbacks(int activityId)
        {
            List<FeedbackResponseModel> feedbacks = _feedbackRepository.GetFeedbacks(activityId); 
            return _mapper.Map<List<FeedbackViewModel>>(feedbacks);
        }
    }
}
