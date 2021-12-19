using Microsoft.EntityFrameworkCore;
using NSC_TournamentGen.Security.Entities;

namespace NSC_TournamentGen.Security
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        public DbSet<LoginUserEntity> LoginUsers { get; set; }
    }
}
