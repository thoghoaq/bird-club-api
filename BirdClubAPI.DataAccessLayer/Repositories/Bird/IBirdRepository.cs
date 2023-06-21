using BirdClubAPI.Domain.DTOs.Response.Bird;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BirdClubAPI.DataAccessLayer.Repositories.Bird
{
    public interface IBirdRepository
    {
        Domain.Entities.Bird? GetBirds(int id);
        List<BirdResponseModel> GetBird();
        BirdResponseModel? CreateBird(Domain.Entities.Bird requestModel);
    }
}
