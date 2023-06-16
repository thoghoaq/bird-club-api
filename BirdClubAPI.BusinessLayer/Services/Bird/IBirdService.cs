using BirdClubAPI.Domain.DTOs.View.Bird;
﻿using BirdClubAPI.Domain.DTOs.Request.Bird;
using BirdClubAPI.Domain.DTOs.View.Bird;
using BirdClubAPI.Domain.DTOs.View.Common;

namespace BirdClubAPI.BusinessLayer.Services.Bird
{
    public interface IBirdService
    {
        KeyValuePair<MessageViewModel, List<BirdViewModel>> GetBird();
        KeyValuePair<MessageViewModel, AddBirdViewModel?> AddBird(AddBirdRequestModel requestModel);
    }
}