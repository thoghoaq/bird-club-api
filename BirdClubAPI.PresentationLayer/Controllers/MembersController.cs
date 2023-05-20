using BirdClubAPI.BusinessLayer.Services.Member;
using BirdClubAPI.Domain.DTOs.Request.Member;
using BirdClubAPI.Domain.DTOs.View.Member;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BirdClubAPI.PresentationLayer.Controllers
{
    [Route("api/v1/members")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IMemberService _memberService;

        public MembersController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        /// <summary>
        /// API chỉnh sửa thông tin của 1 member
        /// </summary>
        [HttpPut("{id}")]
        public IActionResult EditMemberProfile(int id, UpdateMemberRequestModel requestModel)
        {
            var response = _memberService.UpdateMemberProfile(id, requestModel);
            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return Ok(response);
            } else if (response.StatusCode == HttpStatusCode.NotFound) {
                return NotFound(response);
            } else
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// API thay đổi membership status của 1 member
        /// </summary>
        [HttpPut("{id}/membership-status")]
        public IActionResult EditMemberProfile(int id, UpdateMembershipStatusModel requestModel)
        {
            var response = _memberService.UpdateMembershipStatus(id, requestModel);
            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return Ok(response);
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound(response);
            }
            else
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Lấy thông tin của member
        /// </summary>
        [HttpGet("{id}")]
        public ActionResult<MemberViewModel> GetProfile(int id)
        {
            var response = _memberService.GetProfile(id);
            if (response.Key.StatusCode.Equals(HttpStatusCode.NotFound))
            {
                return NotFound(response.Key);
            }
            return Ok(response.Value);
        }

        /// <summary>
        /// Lấy toàn bộ member trong CLB
        /// </summary>
        [HttpGet]
        public ActionResult<List<MemberViewModel>> GetMembers()
        {
            var response = _memberService.GetMembers();
            if (response.Key.StatusCode.Equals(HttpStatusCode.NotFound))
            {
                return NotFound(response.Key);
            }
            return Ok(response.Value);
        }
    }
}
