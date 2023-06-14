﻿using BirdClubAPI.Domain.DTOs.View.Acitivity;
using BirdClubAPI.Domain.DTOs.View.Bird;
using BirdClubAPI.Domain.DTOs.View.Common;
using BirdClubAPI.Domain.DTOs.View.Feedback;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdClubAPI.BusinessLayer.Services.Feedback
{
    public interface IFeedbackService
    {
        
        List<FeedbackViewModel> GetFeedbacks(int activityId);

    }
}
