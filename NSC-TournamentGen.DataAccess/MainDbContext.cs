using Microsoft.EntityFrameworkCore;
using NSC_TournamentGen.DataAccess.Entities;

namespace NSC_TournamentGen.DataAccess
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options) {}
        public DbSet<UserEntity> Users { get; set; }
    }
}