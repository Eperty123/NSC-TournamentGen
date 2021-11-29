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
            _ctx.User.Add(new UserEntity {Id = 1,UserName = "Svend",Password = "ost"});
            _ctx.User.Add(new UserEntity {Id = 2,UserName = "Niko",Password = "ost"});
            _ctx.User.Add(new UserEntity {Id = 3,UserName = "Carlo",Password = "ost"});
         
            _ctx.SaveChanges();
        }

        public void SeedProduction()
        {
            _ctx.Database.EnsureCreated();
        }
    }
}