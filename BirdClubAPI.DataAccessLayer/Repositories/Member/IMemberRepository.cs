namespace BirdClubAPI.DataAccessLayer.Repositories.Member
{
    public interface IMemberRepository
    {
        Domain.Entities.Member? GetMember(int memberId);
        bool UpdateMember(Domain.Entities.Member member);
    }
}
