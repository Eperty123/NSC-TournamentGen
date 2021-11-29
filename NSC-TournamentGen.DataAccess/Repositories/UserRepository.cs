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
            return _ctx.Users
                .Select(ue => new User
                {
                    Id = ue.Id,
                    Username = ue.Username,
                    Password = ue.Password
                })
                .ToList();
        }
}
}