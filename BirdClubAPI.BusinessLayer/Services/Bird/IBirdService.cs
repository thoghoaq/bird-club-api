using BirdClubAPI.Domain.DTOs.Response.Bird;

namespace BirdClubAPI.BusinessLayer.Services.Bird
{
    public interface IBirdService
    {
        List<BirdResponseModel> GetBirds();
    }
}
