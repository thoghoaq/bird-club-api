namespace BirdClubAPI.Domain.DTOs.Request.Auth
{
    public class RegisterRequestModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
        public string Birthday { get; set; }
    }
}
