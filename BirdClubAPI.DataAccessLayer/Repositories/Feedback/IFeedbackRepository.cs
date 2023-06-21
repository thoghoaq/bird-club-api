using BirdClubAPI.Domain.DTOs.Response.Bird;
using BirdClubAPI.Domain.DTOs.Response.Feedback;
using BirdClubAPI.Domain.DTOs.View.Feedback;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdClubAPI.DataAccessLayer.Repositories.Feedback
{
    public interface IFeedbackRepository
    {
        FeedbackResponseModel? CreateFeedback(Domain.Entities.Feedback requestModel);
       
        List<FeedbackResponseModel> GetFeedbacks(int activityId);
    }
}
