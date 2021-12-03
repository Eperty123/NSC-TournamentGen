using Microsoft.EntityFrameworkCore;
using NSC_TournamentGen.Core.Models;
using NSC_TournamentGen.DataAccess.Entities;

namespace NSC_TournamentGen.DataAccess
{
    public class MainDbContext : DbContext
    {
        
        public MainDbContext(DbContextOptions<MainDbContext> options): base(options) {}
        public DbSet<UserEntity> User { get; set; }
        public DbSet<TournamentEntity> Tournament { get; set; }
    }
}