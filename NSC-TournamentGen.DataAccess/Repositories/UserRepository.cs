using System.Collections.Generic;
using System.Linq;
using NSC_TournamentGen.Core.Models;
using NSC_TournamentGen.Domain.IRepositories;

namespace NSC_TournamentGen.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MainDbContext _ctx;

        public UserRepository(MainDbContext ctx)
        {
            _ctx = ctx;
        }
        
        public List<User> ReadAll()
        {
            return _ctx.User.Select(u => new User
            {
                Id = u.Id,
                Username = u.UserName,
                Password = u.Password
            }).ToList();
        }
    }
}