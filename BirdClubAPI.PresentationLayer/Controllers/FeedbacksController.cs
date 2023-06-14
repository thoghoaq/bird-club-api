using BirdClubAPI.BusinessLayer.Services.Feedback;
using BirdClubAPI.Domain.DTOs.View.Feedback;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BirdClubAPI.PresentationLayer.Controllers
{
    [Route("api/v1/feedbacks")]
    [ApiController]
    public class FeedbacksController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbacksController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        /// <summary>
        /// API lấy list feedbacks của 1 activity
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns></returns>

        [HttpGet("{activityId}")]
        public IActionResult GetFeedbacks(int activityId)
        {
            var feedbacks = _feedbackService.GetFeedbacks(activityId);
           return Ok(feedbacks);
        }
    }
}
