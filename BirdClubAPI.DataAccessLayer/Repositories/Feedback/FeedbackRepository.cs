using AutoMapper;
using BirdClubAPI.DataAccessLayer.Context;
using BirdClubAPI.Domain.DTOs.Response.Bird;
using BirdClubAPI.Domain.DTOs.Response.Feedback;
using BirdClubAPI.Domain.DTOs.View.Feedback;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdClubAPI.DataAccessLayer.Repositories.Feedback
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly BirdClubContext _context;
        private readonly IMapper _mapper;

        public FeedbackRepository(BirdClubContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<FeedbackResponseModel> GetFeedbacks(int activityId)
        {
            var feedbacks = _context.Feedbacks
               .Where(e  => e.ActivityId == activityId)
               .ToList();
               if(feedbacks.IsNullOrEmpty())
            {
                return new List<FeedbackResponseModel>();
            }
            return _mapper.Map<List<FeedbackResponseModel>>(feedbacks);
        }
    }
}
