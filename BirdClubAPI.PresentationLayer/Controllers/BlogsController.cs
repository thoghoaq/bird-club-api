using BirdClubAPI.BusinessLayer.Services.Newsfeed;
using Microsoft.AspNetCore.Mvc;

namespace BirdClubAPI.PresentationLayer.Controllers
{
    [ApiController]
    [Route("api/v1/blogs")]
    public class BlogsController : ControllerBase
    {
        private readonly INewsfeedService _newsfeedService;

        public BlogsController(INewsfeedService newsfeedService)
        {
            _newsfeedService = newsfeedService;
        }

        [HttpGet]
        public IActionResult GetBlogs()
        {
            return Ok(_newsfeedService.GetBlogs());
        }
    }
}
