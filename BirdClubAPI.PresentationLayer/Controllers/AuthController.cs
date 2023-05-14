using BirdClubAPI.BusinessLayer.Services.Auth;
using BirdClubAPI.Domain.DTOs.Request.Auth;
using Microsoft.AspNetCore.Http;
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
    }
}
