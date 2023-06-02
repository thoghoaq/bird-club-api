﻿using BirdClubAPI.BusinessLayer.Services.Activity;
using BirdClubAPI.Domain.DTOs.Request.Activity;
using BirdClubAPI.Domain.DTOs.View.Acitivity;
using BirdClubAPI.Domain.DTOs.View.Common;
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
        /// API lấy tất cả activities có status true
        /// </summary>
        [HttpGet]
        public ActionResult<List<AcitivityViewModel>> GetActivities()
        {
            List<AcitivityViewModel> acitivities = _activityService.GetActivities();
            return Ok(acitivities);
        }

        /// <summary>
        /// API lấy tất cả activities cho calender
        /// </summary>
        [HttpGet("calender")]
        public ActionResult<List<ActivityCalenderViewModel>> GetCalenderActivities()
        {
            List<ActivityCalenderViewModel> acitivities = _activityService.GetCalenderActivities();
            return Ok(acitivities);
        }

        /// <summary>
        /// API lấy thông tin của 1 activity
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult GetActivities(int id)
        {
            var response = _activityService.GetActivities(id);
            if (response.Key.StatusCode.Equals(HttpStatusCode.NotFound))
            {
                return NotFound(response.Key);
            }
            return Ok(response.Value);
        }

        /// <summary>
        /// API update thông tin của activity
        /// </summary>
        [HttpPut("{id}")]
        public IActionResult UpdateActivity(int id, UpdateActivityRequestModel requestModel)
        {
            MessageViewModel result = _activityService.UpdateActivity(id, requestModel);
            if (result.StatusCode.Equals(HttpStatusCode.NotFound)) {
                return NotFound(result);
            }
            if (result.StatusCode.Equals(HttpStatusCode.Conflict))
            {
                return Conflict(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// API update status của activity, active or deactive
        /// </summary>
        [HttpPut("{id}/status")]
        public IActionResult UpdateActivityStatus(int id, UpdateActivityStatusRequestModel requestModel)
        {
            MessageViewModel result = _activityService.UpdateActivityStatus(id, requestModel);
            if (result.StatusCode.Equals(HttpStatusCode.NotFound))
            {
                return NotFound(result);
            }
            if (result.StatusCode.Equals(HttpStatusCode.Conflict))
            {
                return Conflict(result);
            }
            return Ok(result);
        }
        /// <summary>
        /// API lấy attendance của 1 activity
        /// </summary>
        [HttpGet("{id}/listattendance")]
        public ActionResult<List<AttendanceViewModel>> GetAttendance(int id)
        {
            var response = _activityService.GetAttendance(id);
            if (response.Key.StatusCode.Equals(HttpStatusCode.NotFound))
            {
                return NotFound(response.Key);
            }
            return Ok(response.Value);
        }
    }
}
