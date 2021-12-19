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

            #endregion
        }

        public void SeedProduction()
        {
            _ctx.Database.EnsureCreated();
        }
    }
}