using System.Net;

namespace BirdClubAPI.Domain.DTOs.View.Common
{
    public class MessageViewModel
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
