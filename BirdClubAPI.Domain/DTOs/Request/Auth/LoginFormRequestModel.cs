using System.ComponentModel.DataAnnotations;

namespace BirdClubAPI.Domain.DTOs.Request.Auth
{
    public class LoginFormRequestModel
    {
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}
