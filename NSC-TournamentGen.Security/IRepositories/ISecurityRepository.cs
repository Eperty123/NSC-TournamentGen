using NSC_TournamentGen.Security.Models;

namespace NSC_TournamentGen.Security.IRepositories
{
    public interface ISecurityRepository
    {
        AuthUser ReadUser(string username);
    }
}
