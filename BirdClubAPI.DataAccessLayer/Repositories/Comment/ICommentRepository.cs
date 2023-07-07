namespace BirdClubAPI.DataAccessLayer.Repositories.Comment
{
    public interface ICommentRepository
    {
        Domain.Entities.Comment? Create(Domain.Entities.Comment comment);
    }
}
