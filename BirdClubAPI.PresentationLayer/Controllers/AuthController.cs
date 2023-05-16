using BirdClubAPI.BusinessLayer.Services.Auth;
using BirdClubAPI.Domain.DTOs.Request.Auth;
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
    }
}
