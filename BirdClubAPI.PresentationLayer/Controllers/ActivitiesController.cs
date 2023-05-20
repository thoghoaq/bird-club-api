using BirdClubAPI.BusinessLayer.Services.Activity;
using BirdClubAPI.Domain.DTOs.Request.Activity;
using BirdClubAPI.Domain.DTOs.View.Acitivity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;

namespace BirdClubAPI.PresentationLayer.Controllers
{
    [ApiController]
    [Route("api/v1/activities")]
    public class ActivitiesController : ControllerBase
    {
        private readonly IActivityService _activityService;

        public ActivitiesController(IActivityService activityService)
        {
            _activityService = activityService;
        }

        /// <summary>
        /// API tạo 1 activity (event), activiyType thuộc ["EVENT","TOURNAMENT","OFFLINE"]
        /// </summary>
        [HttpPost]
        public IActionResult CreateActivity(CreateActivityRequestModel requestModel)
        {
            var result = _activityService.CreateActivity(requestModel);
            if (result.Key.StatusCode.Equals(HttpStatusCode.InternalServerError))
            {
                return BadRequest(result.Key);
            }
            return CreatedAtAction("CreateActivity", result.Value);
        }

        /// <summary>
        /// API lấy tất cả activities
        /// </summary>
        [HttpGet]
        public ActionResult<List<AcitivityViewModel>> GetActivities()
        {
            List<AcitivityViewModel> acitivities = _activityService.GetActivities();
            return Ok(acitivities);
        }
    }
}
