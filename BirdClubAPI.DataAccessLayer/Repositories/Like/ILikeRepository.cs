namespace BirdClubAPI.DataAccessLayer.Repositories.Like
{
    public interface ILikeRepository
    {
        List<Domain.Entities.Like> GetLikes(int newsfeedId);
    }
}
