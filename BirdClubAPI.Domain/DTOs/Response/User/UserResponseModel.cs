namespace BirdClubAPI.Domain.DTOs.Response.User
{
    public class UserResponseModel
    {
        public string DisplayName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string UserType { get; set; } = null!;
        public int Id { get; set; }
    }
}
