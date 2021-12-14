using NSC_TournamentGen.Core.Models;
using NSC_TournamentGen.DataAccess.Entities;

namespace NSC_TournamentGen.DataAccess
{
    public class DbSeeder : IDbSeeder
    {
        private readonly MainDbContext _ctx;

        public DbSeeder(MainDbContext ctx)
        {
            _ctx = ctx;
        }

        public void SeedDevelopment()
        {
            #region Main Db Context
            _ctx.Database.EnsureDeleted();
            _ctx.Database.EnsureCreated();
            
           
           
            

            _ctx.User.Add(new UserEntity { Id = 1, Username = "Svend", Password = "ost" });
            _ctx.User.Add(new UserEntity { Id = 2, Username = "Niko", Password = "ost" });
            _ctx.User.Add(new UserEntity { Id = 3, Username = "Carlo", Password = "ost" });
            
            
            _ctx.SaveChanges();
            _ctx.Participant.Add(new ParticipantEntity() {Id = 1, Name = "OstOle"});
            _ctx.Participant.Add(new ParticipantEntity() {Id = 2, Name = "OstJohn"});
            _ctx.SaveChanges();
            _ctx.Tournament.Add(new TournamentEntity() {Id = 1, Name = "OstTournament", UserId = 1});
            _ctx.SaveChanges();
            _ctx.Round.Add(new RoundEntity() {Id = 1, Name = "finale", TournamentId = 1});
            _ctx.SaveChanges();
            _ctx.Bracket.Add(new BracketEntity() {Id = 1, RoundId = 1, Participant1Id = 1, Participant2Id = 2});
            _ctx.SaveChanges();
            #endregion
        }

        public void SeedProduction()
        {
            _ctx.Database.EnsureCreated();
        }
    }
}