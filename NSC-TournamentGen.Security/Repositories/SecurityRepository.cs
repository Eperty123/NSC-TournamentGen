using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NSC_TournamentGen.Security.Entities;
using NSC_TournamentGen.Security.IRepositories;
using NSC_TournamentGen.Security.Models;
using System.Linq;
using System.Text;

namespace NSC_TournamentGen.Security.Repositories
{
    public class SecurityRepository : ISecurityRepository
    {
        readonly AuthDbContext _ctx;

        public SecurityRepository(AuthDbContext authDbContext)
        {
            _ctx = authDbContext;
        }

        public AuthUser ReadUser(string username)
        {
            var foundUser = _ctx.LoginUsers.FirstOrDefault(x => x.Username == username);
            if (foundUser != null)
            {
                return new AuthUser()
                {
                    Id = foundUser.Id,
                    UserName = foundUser.Username,
                    HashedPassword = foundUser.HashedPassword,
                    Role = foundUser.Role,
                    Salt = Encoding.ASCII.GetBytes(foundUser.Salt),
                };
            }
            return null;
        }
    }
}
