using BirdClubAPI.BusinessLayer.Services.Activity;
using BirdClubAPI.Domain.Commons.Enums;
using BirdClubAPI.Domain.DTOs.Request.Activity;
using BirdClubAPI.Domain.DTOs.Request.Attendance;
using BirdClubAPI.Domain.DTOs.Request.Newsfeed.Comment;
using BirdClubAPI.Domain.DTOs.Response.Activity;
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
        public ActionResult<List<AcitivityViewModel>> GetActivities(bool? isAll)
        {
            List<AcitivityViewModel> acitivities = _activityService.GetActivities(isAll);
            return Ok(acitivities);
        }

        /// <summary>
        /// API lấy tất cả activities thuộc sở hữu của mình
        /// </summary>
        [HttpGet("by-owner")]
        public ActionResult<List<AcitivityViewModel>> GetActivitiesByOwner(int ownerId)
        {
            List<AcitivityViewModel> acitivities = _activityService.GetActivitiesByOwner(ownerId);
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
        /// API lấy tất cả activities cho calender bởi 1 member
        /// </summary>
        [HttpGet("calender-of-member")]
        public ActionResult<List<ActivityCalenderViewModel>> GetCalenderActivities(int memberId)
        {
            List<ActivityCalenderViewModel> acitivities = _activityService.GetCalenderActivitiesByMember(memberId);
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

        /// <summary>
        /// API request attendance 1 activity 
        /// </summary>
        [HttpPost("attendance")]
        public IActionResult AttendanceActivity(AttendanceActivityRequestModel requestModel)
        {
            var result = _activityService.AttendanceActivity(requestModel);
            if (result.Key.StatusCode.Equals(HttpStatusCode.InternalServerError))
            {
                return BadRequest(result.Key);
            }
            return CreatedAtAction("AttendanceActivity", result.Value);
        }

        /// <summary>
        /// API gửi request attend 1 activity
        /// </summary>
        [HttpPost("attendance-requests")]
        public IActionResult RequestAttendance(AttendanceRequestModel request)
        {
            var result = _activityService.RequestAttendance(request.MemberId, request.ActivityId);
            if (result.StatusCode.Equals(HttpStatusCode.BadRequest))
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// API accept request
        /// </summary>
        [HttpPost("attendances")]
        public IActionResult PostAttendance(AttendanceRequestModel request)
        {
            var result = _activityService.PostAttendance(request.MemberId, request.ActivityId);
            if (result.StatusCode.Equals(HttpStatusCode.OK))
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// API decline an attendance request
        /// </summary>
        [HttpDelete("attendance-requests")]
        public IActionResult DeclineAttendance(AttendanceRequestModel request)
        {
            var result = _activityService.DeclineAttendance(request.MemberId, request.ActivityId);
            if (result.StatusCode.Equals(HttpStatusCode.OK))
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// API check status of user's attendance on activity, NOT_ATTEND = 0, PENDING = 1, REJECTED = 2,ACCEPTED = 3, CLOSED = 4, NOT_FOUND = 5,
        /// </summary>
        [HttpGet("{id}/user-attendance-status")]
        public IActionResult GetUserAttendanceStatus(int id, int memberId)
        {
            AttendanceStatusRm result = _activityService.GetUserAttendanceStatus(id, memberId);
            if (result.Status.Equals(AttendanceStatusEnum.NOT_FOUND))
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// API lấy attendance request của 1 activity
        /// </summary>
        [HttpGet("{id}/attendance-requests")]
        public ActionResult<List<AttendanceViewModel>> GetAttendanceRequests(int id)
        {
            var response = _activityService.GetAttendanceRequests(id);
            if (response.Key.StatusCode.Equals(HttpStatusCode.NotFound))
            {
                return NotFound(response.Key);
            }
            return Ok(response.Value);
        }

        [HttpPost("{id}/comment")]
        public IActionResult PostComment(int id, ActivityCommentRequest request)
        {
            try
            {
                var response = _activityService.PostComment(id, request);
                return Ok(response);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
