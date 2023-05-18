using System.ComponentModel.DataAnnotations;

namespace BirdClubAPI.Domain.DTOs.Request.Newsfeed.Blog
{
    public class CreateBlogRequestModel
    {
        [Required]
        public int OwnerId { get; set; }
        [Required]
        public string Title { get; set; } = null!;
        public string? Content { get; set; }
    }
}
