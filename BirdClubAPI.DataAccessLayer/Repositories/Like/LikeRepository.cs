using BirdClubAPI.DataAccessLayer.Context;

namespace BirdClubAPI.DataAccessLayer.Repositories.Like
{
    public class LikeRepository : ILikeRepository
    {
        private readonly BirdClubContext _context;

        public LikeRepository(BirdClubContext context)
        {
            _context = context;
        }

        public List<Domain.Entities.Like> GetLikes(int newsfeedId)
        {
            return _context.Likes.Where(e => (e.Type == "RECORD" || e.Type == "BLOG") && e.ReferenceId == newsfeedId).ToList();
        }
    }
}
