using BirdClubAPI.DataAccessLayer.Context;

namespace BirdClubAPI.DataAccessLayer.Repositories.Bird
{
    public class BirdRepository : IBirdRepository
    {
        private readonly BirdClubContext _context;

        public BirdRepository(BirdClubContext context)
        {
            _context = context;
        }

        public List<Domain.Entities.Bird> GetBirds()
        {
            return _context.Birds.ToList();
        }
    }
}
