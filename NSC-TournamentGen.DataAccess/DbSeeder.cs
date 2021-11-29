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
            _ctx.Users.Add(new UserEntity() {Username = "Niko", Password = "dev"});
            _ctx.Users.Add(new UserEntity() {Username = "Svend", Password = "dev"});
            _ctx.Users.Add(new UserEntity() {Username=  "Carlo", Password = "dev"});
         
            _ctx.SaveChanges();
        }

        public void SeedProduction()
        {
            _ctx.Database.EnsureCreated();
        }
    }
}