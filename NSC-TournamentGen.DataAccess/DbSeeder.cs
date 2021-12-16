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
            
            _ctx.Users.Add(new UserEntity { Id = 1, Username = "Svend", Password = "ost" });
            _ctx.Users.Add(new UserEntity { Id = 2, Username = "Niko", Password = "ost" });
            _ctx.Users.Add(new UserEntity { Id = 3, Username = "Carlo", Password = "ost" });
            
            //_ctx.SaveChanges();
            //_ctx.Participant.Add(new ParticipantEntity() {Id = 1, Name = "OstOle1"});
            //_ctx.Participant.Add(new ParticipantEntity() {Id = 2, Name = "OstJohn2"});
            //_ctx.Participant.Add(new ParticipantEntity() {Id = 3, Name = "OstOle3"});
            //_ctx.Participant.Add(new ParticipantEntity() {Id = 4, Name = "OstJohn4"});
            //_ctx.Participant.Add(new ParticipantEntity() {Id = 5, Name = "OstOle5"});
            //_ctx.Participant.Add(new ParticipantEntity() {Id = 6, Name = "OstJohn6"});
            //_ctx.Participant.Add(new ParticipantEntity() {Id = 7, Name = "OstOle7"});
            //_ctx.Participant.Add(new ParticipantEntity() {Id = 8, Name = "OstJohn8"});
            //_ctx.Participant.Add(new ParticipantEntity() {Id = 9, Name = "OstOle9"});
            //_ctx.Participant.Add(new ParticipantEntity() {Id = 10, Name = "OstJohn10"});
            //_ctx.SaveChanges();
            //_ctx.Tournament.Add(new TournamentEntity() {Id = 1, Name = "OstTournament", UserId = 1});
            //_ctx.SaveChanges();
            //_ctx.Round.Add(new RoundEntity() {Id = 1, Name = "finale", TournamentId = 1});
            //_ctx.Round.Add(new RoundEntity() {Id = 2, Name = "semifinale", TournamentId = 1});
            //_ctx.Round.Add(new RoundEntity() {Id = 3, Name = "kvartfinale", TournamentId = 1});
            //_ctx.Round.Add(new RoundEntity() {Id = 4, Name = "otteneDel finale", TournamentId = 1});
            //_ctx.SaveChanges();
            //_ctx.Bracket.Add(new BracketEntity() {Id = 1, RoundId = 1, Participant1Id = 1, Participant2Id = 2});
            //_ctx.Bracket.Add(new BracketEntity() {Id = 2, RoundId = 2, Participant1Id = 1, Participant2Id = 2});
            //_ctx.Bracket.Add(new BracketEntity() {Id = 3, RoundId = 2, Participant1Id = 1, Participant2Id = 2});
            //_ctx.Bracket.Add(new BracketEntity() {Id = 4, RoundId = 3, Participant1Id = 1, Participant2Id = 2});
            //_ctx.Bracket.Add(new BracketEntity() {Id = 5, RoundId = 3, Participant1Id = 1, Participant2Id = 2});
            //_ctx.Bracket.Add(new BracketEntity() {Id = 6, RoundId = 3, Participant1Id = 1, Participant2Id = 2});
            //_ctx.Bracket.Add(new BracketEntity() {Id = 7, RoundId = 3, Participant1Id = 1, Participant2Id = 2});
            //_ctx.Bracket.Add(new BracketEntity() {Id = 8, RoundId = 4, Participant1Id = 9, Participant2Id = 10});
            //_ctx.Bracket.Add(new BracketEntity() {Id = 9, RoundId = 4, Participant1Id = 7, Participant2Id = 8});
            //_ctx.Bracket.Add(new BracketEntity() {Id = 10, RoundId = 4, Participant1Id = 5, Participant2Id = 6});
            //_ctx.Bracket.Add(new BracketEntity() {Id = 11, RoundId = 4, Participant1Id = 3, Participant2Id = 4});
            //_ctx.Bracket.Add(new BracketEntity() {Id = 12, RoundId = 4, Participant1Id = 1, Participant2Id = 2});
            _ctx.SaveChanges();
            #endregion
        }

        public void SeedProduction()
        {
            _ctx.Database.EnsureCreated();
        }
    }
}