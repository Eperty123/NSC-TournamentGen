using NSC_TournamentGen.Core.Models;
using NSC_TournamentGen.DataAccess.Entities;
using NSC_TournamentGen.Security.Models;

namespace NSC_TournamentGen.DataAccess
{
    public class DbSeeder
    {
        private readonly MainDbContext _ctx;
        private readonly AuthDbContext _authCtx;

        public DbSeeder(MainDbContext ctx, AuthDbContext authCtx)
        {
            _ctx = ctx;
            _authCtx = authCtx;
        }

        public void SeedDevelopment()
        {
            #region Main Db Context
            _ctx.Database.EnsureDeleted();
            _ctx.Database.EnsureCreated();

            _ctx.User.Add(new UserEntity { Id = 1, Username = "Svend", Password = "ost" });
            _ctx.User.Add(new UserEntity { Id = 2, Username = "Niko", Password = "ost" });
            _ctx.User.Add(new UserEntity { Id = 3, Username = "Carlo", Password = "ost" });
            _ctx.Tournament.Add(new TournamentEntity() { Id = 1, Name = "Svend", Participants = "lol", Type = TournamentType.SingleElimination });
            _ctx.Tournament.Add(new TournamentEntity() { Id = 2, Name = "Sick Tournament", Participants = "lol", Type = TournamentType.SingleElimination });
            _ctx.Tournament.Add(new TournamentEntity() { Id = 3, Name = "HentaiTournamentNSFW", Participants = "carlo", Type = TournamentType.SingleElimination });

            _ctx.SaveChanges();
            #endregion

            #region Auth Db Context
            _authCtx.Database.EnsureDeleted();
            _authCtx.Database.EnsureCreated();

            _authCtx.LoginUsers.AddRange(
                new LoginUserEntity { Username = "Carlo", Password = "test" },
                new LoginUserEntity { Username = "Niko", Password = "test" },
                new LoginUserEntity { Username = "Svend", Password = "test" }
            );

            _authCtx.SaveChanges();

            #endregion
        }

        public void SeedProduction()
        {
            _ctx.Database.EnsureCreated();
            _authCtx.Database.EnsureCreated();
        }
    }
}