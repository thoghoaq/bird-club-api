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
        public IActionResult Login(LoginFormRequestModel loginForm)
        {
            var result = _authService.Login(loginForm);
            if (result.Key.StatusCode == HttpStatusCode.OK)
            {
                return Ok(result.Value);
            }
            else
            {
                return Unauthorized(result.Key);
            }
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterRequestModel requestModel)
        {
            var result = _authService.Register(requestModel);
            if (result == true)
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
        public ActionResult ApproveMember(int id)
        {
            var reponse = _authService.ApproveMember(id);
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
        public ActionResult RejectUser(int id)
        {
            var reponse = _authService.RejectUser(id);
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
    }
}
