using BirdClubAPI.DataAccessLayer.Context;
using System.Data;

namespace BirdClubAPI.DataAccessLayer.Repositories.Comment
{
    public class CommentRepository : ICommentRepository
    {
        private readonly BirdClubContext _context;

        public CommentRepository(BirdClubContext context)
        {
            _context = context;
        }

        public Domain.Entities.Comment? Create(Domain.Entities.Comment comment)
        {
            try
            {
                var result = _context.Add(comment);
                _context.SaveChanges();
                return result.Entity;
            } catch (Exception ex)
            {
                throw new DBConcurrencyException(ex.Message);
            }
        }
    }
}
