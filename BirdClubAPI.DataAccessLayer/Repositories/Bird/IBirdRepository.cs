namespace BirdClubAPI.DataAccessLayer.Repositories.Bird
{
    public interface IBirdRepository
    {
        List<Domain.Entities.Bird> GetBirds();
    }
}
