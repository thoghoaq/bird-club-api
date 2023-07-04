using BirdClubAPI.BusinessLayer.Services.Bird;
using Microsoft.AspNetCore.Mvc;

namespace BirdClubAPI.PresentationLayer.Controllers
{
    [Route("api/v1/birds")]
    [ApiController]
    public class BirdsController : ControllerBase
    {
        private readonly IBirdService _birdService;

        public BirdsController(IBirdService birdService)
        {
            _birdService = birdService;
        }

        [HttpGet]
        public IActionResult GetBirds()
        {
            return Ok(_birdService.GetBirds());
        }
    }
}
