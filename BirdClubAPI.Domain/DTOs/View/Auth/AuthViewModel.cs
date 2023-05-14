namespace BirdClubAPI.Domain.DTOs.View.Auth
{
    public class AuthViewModel
    {
        public string JwtToken { get; set; } = null!;
        public string UserType { get; set; } = null!;
        public int UserId { get; set; }
        public string DisplayName { get; set; } = null!;
    }
}
