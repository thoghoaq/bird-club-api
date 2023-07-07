using BirdClubAPI.BusinessLayer.Services.Newsfeed;
using BirdClubAPI.Domain.DTOs.Request.Attendance;
using BirdClubAPI.Domain.DTOs.Request.Newsfeed.Blog;
using BirdClubAPI.Domain.DTOs.Request.Newsfeed.Comment;
using BirdClubAPI.Domain.DTOs.View.Blog;
using BirdClubAPI.Domain.DTOs.View.Newsfeed;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
        public ActionResult<NewsfeedViewModel> GetNewsfeeds(int page, int size, int? memberId) {
            return Ok(_newsfeedService.GetNewsfeeds(page, size, memberId));
        }

        /// <summary>
        /// Post new blog
        /// </summary>
        [HttpPost("blogs")]
        public IActionResult PostBlog(CreateBlogRequestModel requestModel)
        {
            var result = _newsfeedService.CreateBlog(requestModel);
            if (result.Key.StatusCode.Equals(HttpStatusCode.InternalServerError))
            {
                return BadRequest(result.Key);
            }
            return CreatedAtAction("PostBlog", result.Value);
        }
        /// <summary>
        /// Get blog
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("blogs/{id}")]
        public ActionResult<BlogViewModel> GetBlog(int id) {
            var response = _newsfeedService.GetBlog(id);
            if (response.Key.StatusCode.Equals(HttpStatusCode.NotFound))
            {
                return NotFound(response.Key);
            }
            return Ok(response.Value);
        }

        /// <summary>
        /// Update blog
        /// </summary>
        [HttpPut("blogs/{id}")]
        public ActionResult<BlogViewModel> UpdateBlog(int id, UpdateBlogRm request)
        {
            var response = _newsfeedService.UpdateBlog(id, request);
            if (response.Key.StatusCode.Equals(HttpStatusCode.NotFound))
            {
                return NotFound(response.Key);
            } else if (response.Key.StatusCode.Equals(HttpStatusCode.InternalServerError))
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, response.Key);
            }
            return NoContent();
        }

        [HttpGet("by-member/{memberid}")]
        public ActionResult <NewsfeedViewModel> GetNewsFeed(int memberid)
        {
            var response = _newsfeedService.GetNewsFeed(memberid);          
            return Ok(response);
        }
        /// <summary>
        /// API Like in newsfeed
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("like")]
        public IActionResult PostLiked(LikeRequestModel request)
        {
            var result = _newsfeedService.PostLiked(request.MemberId, request.NewsFeedId);
            if (result.StatusCode.Equals(HttpStatusCode.OK))
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("{id}/comment")]
        public IActionResult PostComment(NewsfeedCommentRequest request)
        {
            try
            {
                var result = _newsfeedService.PostComment(request);
                return CreatedAtAction("PostComment", result);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
