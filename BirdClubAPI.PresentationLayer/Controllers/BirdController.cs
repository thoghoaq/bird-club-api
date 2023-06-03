
using BirdClubAPI.BusinessLayer.Services.Bird;
using BirdClubAPI.Domain.DTOs.View.Bird;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BirdClubAPI.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BirdController : ControllerBase
    {
        private readonly IBirdService _birdService;

        public BirdController(IBirdService birdService)
        {
            _birdService = birdService;
        }

        [HttpGet]
        public ActionResult<List<BirdViewModel>> GetBird()
        {
            var response = _birdService.GetBird();
            if (response.Key.StatusCode.Equals(HttpStatusCode.NotFound))
            {
                return NotFound(response.Key);
            }
            return Ok(response.Value);
        }
    }
}
