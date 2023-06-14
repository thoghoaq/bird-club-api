
using BirdClubAPI.BusinessLayer.Services.Bird;
using BirdClubAPI.Domain.DTOs.View.Bird;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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


        /// <summary>
        /// APi lấy list Birds 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<BirdViewModel>> GetBirds()
        {
            var response = _birdService.GetBirds();
            if (response.Key.StatusCode.Equals(HttpStatusCode.NotFound))
            {
                return NotFound(response.Key);
            }
            return Ok(response.Value);
        }
    }
}
