using BirdClubAPI.BusinessLayer.Services.Member;
using BirdClubAPI.Domain.DTOs.Request.Member;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BirdClubAPI.PresentationLayer.Controllers
{
    [Route("api/v1/member")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IMemberService _memberService;

        public MembersController(IMemberService memberService)
        {
            _memberService = memberService;
        }

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
    }
}
