using BirdClubAPI.BusinessLayer.Helpers;
using BirdClubAPI.BusinessLayer.Services.Auth;
using BirdClubAPI.Domain.DTOs.Request.Auth;
using BirdClubAPI.Domain.DTOs.View.Auth;
using BirdClubAPI.Domain.DTOs.View.Common;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BirdClubAPI.PresentationLayer.Controllers
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginFormRequestModel loginForm)
        {
            try
            {
                var result = await _authService.Login(loginForm);
                if (result.Key.StatusCode == HttpStatusCode.OK)
                {
                    return Ok(result.Value);
                }
                else
                {
                    return Unauthorized(result.Key);
                }
            } catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new
                {
                    ex.Message
                });
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestModel requestModel)
        {
            var result = await _authService.Register(requestModel);
            if (result)
            {
                return Ok();
            } else
            {
                return BadRequest(new MessageViewModel
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Invalid user"
                });
            }
        }
        [HttpGet("listguest")]
        public ActionResult<List<GuestViewModel>> GetListGuest()
        {
            var response = _authService.GetListGuest();
            if (response.Key.StatusCode.Equals(HttpStatusCode.NotFound))
            {
                return NotFound(response.Key);
            }
            return Ok(response.Value);
        }
        [HttpPost("{id}/approve")]
        public async Task<ActionResult> ApproveMember(int id)
        {
            var reponse = await _authService.ApproveMember(id);
            if (reponse.StatusCode == HttpStatusCode.OK)
            {
                return Ok(reponse);
            }
            else
            {
                return NotFound(reponse);
            }
        }

        [HttpDelete("{id}/reject")]
        public async Task<ActionResult> RejectUser(int id)
        {
            var reponse = await _authService.RejectUser(id);
            if (reponse.StatusCode == HttpStatusCode.OK)
            {
                return Ok(reponse);
            }
            else
            {
                return NoContent();
            }
        }


        [HttpGet]
        public ActionResult ShowUser()
        {
            var response = _authService.ShowUser();           
            return Ok(response);
        }

        [HttpPost("resend-email")]
        public async Task<IActionResult> ResendEmail(string email)
        {
            var result = await _authService.ResendEmail(email);
            if (!result)
            {
                return BadRequest(new { message = "Email has verified" });
            }
            return NoContent();
        }
    }
}
