using Microsoft.EntityFrameworkCore;
using NSC_TournamentGen.DataAccess.Entities;

namespace NSC_TournamentGen.DataAccess
{
    public class MainDbContext : DbContext
    {

        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options) { }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<TournamentEntity> Tournaments { get; set; }
        public DbSet<RoundEntity> Rounds { get; set; }
        public DbSet<ParticipantEntity> Participants { get; set; }
        public DbSet<BracketEntity> Brackets { get; set; }
        public DbSet<TournamentUserEntity> TournamentUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BracketEntity>()
                .HasOne(c => c.Participant1)
            .WithMany(ct => ct.BracketsParticipants1)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BracketEntity>()
                .HasOne(c => c.Participant2)
            .WithMany(ct => ct.BracketsParticipants2)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BracketEntity>()
                .HasOne(c => c.Round)
                .WithMany(ct => ct.Brackets)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RoundEntity>()
                .HasOne(c => c.Tournament)
                .WithMany(ct => ct.Rounds)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TournamentEntity>()
                .HasOne(c => c.User)
                .WithMany(ct => ct.Tournaments)
                .OnDelete(DeleteBehavior.Cascade);

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