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
        public DbSet<RoundEntity> Round { get; set; }
        public DbSet<ParticipantEntity> Participant { get; set; }
        public DbSet<BracketEntity> Bracket { get; set; }
        public DbSet<TournamentUserEntity> TournamentUser { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<BracketEntity>()
                .HasOne(c => c.Participant1)
                .WithMany(ct => ct.BracketsParticipants1)
                .OnDelete(DeleteBehavior.SetNull);
            
            modelBuilder.Entity<BracketEntity>()
                .HasOne(c => c.Participant2)
                .WithMany(ct => ct.BracketsParticipants2)
                .OnDelete(DeleteBehavior.SetNull);
            
            modelBuilder.Entity<BracketEntity>()
                .HasOne(c => c.Round)
                .WithMany(ct => ct.Brackets)
                .OnDelete(DeleteBehavior.SetNull);
            
            modelBuilder.Entity<RoundEntity>()
                .HasOne(c => c.Tournament)
                .WithMany(ct => ct.Rounds)
                .OnDelete(DeleteBehavior.SetNull);
            
            modelBuilder.Entity<TournamentEntity>()
                .HasOne(c => c.User)
                .WithMany(ct => ct.Tournaments)
                .OnDelete(DeleteBehavior.SetNull);
            
            modelBuilder.Entity<TournamentUserEntity>()
                .HasKey(ol => new { ol.TournamentId, ol.UserId });
            
            modelBuilder.Entity<TournamentUserEntity>()
                .HasOne(ol => ol.Tournament)
                .WithMany(o => o.TournamentUsers)
                .HasForeignKey(ol => ol.TournamentId);
            
            modelBuilder.Entity<TournamentUserEntity>()
                .HasOne(ol => ol.User)
                .WithMany(o => o.TournamentUsers)
                .HasForeignKey(ol => ol.UserId);
            
            
            
        }
    }
}