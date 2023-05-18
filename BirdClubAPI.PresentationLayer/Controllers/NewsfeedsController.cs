using BirdClubAPI.BusinessLayer.Services.Newsfeed;
using BirdClubAPI.Domain.DTOs.View.Newsfeed;
using Microsoft.AspNetCore.Mvc;

namespace BirdClubAPI.PresentationLayer.Controllers
{
    [Route("api/v1/newsfeeds")]
    [ApiController]
    public class NewsfeedsController : ControllerBase
    {
        private readonly INewsfeedService _newsfeedService;

        public NewsfeedsController(INewsfeedService newsfeedService)
        {
            _newsfeedService = newsfeedService;
        }

        /// <summary>
        /// Get newsfeeds, newsfeedType = 0 (BLOG), newsfeedType = 1 (RECORD), 
        /// {limit} is max number of record, 
        /// {page} is page number, 
        /// {size} is number records of a page
        /// </summary>
        [HttpGet]
        public ActionResult<NewsfeedViewModel> GetNewsfeeds(int limit, int page, int size) {
            return Ok(_newsfeedService.GetNewsfeeds(limit, page, size));
        }
    }
}
