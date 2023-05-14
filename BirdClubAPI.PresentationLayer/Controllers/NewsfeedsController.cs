using BirdClubAPI.BusinessLayer.Services.Newsfeed;
using Microsoft.AspNetCore.Mvc;

namespace BirdClubAPI.PresentationLayer.Controllers
{
    [Route("api/v1/newsfeed")]
    [ApiController]
    public class NewsfeedsController : ControllerBase
    {
        private readonly INewsfeedService _newsfeedService;

        public NewsfeedsController(INewsfeedService newsfeedService)
        {
            _newsfeedService = newsfeedService;
        }

        /// <summary>
        /// Get newsfeeds, newsfeedType = 0 (BLOG), newsfeedType = 1 (RECORD)
        /// </summary>
        [HttpGet]
        public ActionResult GetNewsfeeds() {
            return Ok(_newsfeedService.GetNewsfeeds());
        }
    }
}
