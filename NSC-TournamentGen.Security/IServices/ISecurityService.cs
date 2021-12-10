using NSC_TournamentGen.Security.Entities;
using NSC_TournamentGen.Security.Models;

namespace NSC_TournamentGen.Security.IServices
{
    public interface ISecurityService
    {
        JwtToken GenerateJwtToken(string username, string password);
        AuthUser GetUser(string username);
        string HashPassword(string plainPassword, byte[] salt);
    }
}
