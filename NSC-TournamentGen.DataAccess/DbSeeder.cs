using NSC_TournamentGen.DataAccess.Entities;

namespace NSC_TournamentGen.DataAccess
{
    public class DbSeeder
    {
        private readonly MainDbContext _ctx;

        public DbSeeder(MainDbContext ctx)
        {
            _ctx = ctx;
        }

        public void SeedDevelopment()
        {
            _ctx.Database.EnsureDeleted();
            _ctx.Database.EnsureCreated();
            _ctx.User.Add(new UserEntity {Id = 1,Username = "Svend",Password = "ost"});
            _ctx.User.Add(new UserEntity {Id = 2,Username = "Niko",Password = "ost"});
            _ctx.User.Add(new UserEntity {Id = 3,Username = "Carlo",Password = "ost"});
            _ctx.Tournament.Add(new TournamentEntity() {Id = 1,Name = "Svend",Participants = "lol"});
            _ctx.Tournament.Add(new TournamentEntity() {Id = 2,Name = "Sick Tournament",Participants = "lol"});
            _ctx.Tournament.Add(new TournamentEntity() {Id = 3,Name = "HentaiTournamentNSFW",Participants = "carlo"});
            
            
         
            _ctx.SaveChanges();
        }

        public void SeedProduction()
        {
            _ctx.Database.EnsureCreated();
        }
    }
}