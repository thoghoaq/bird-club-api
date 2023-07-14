using BirdClubAPI.BusinessLayer.Services.Newsfeed;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id) {
            try
            {
                var result = _newsfeedService.DeleteBlog(id);
                return result ? NoContent() : BadRequest();
            } catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
