using NSC_TournamentGen.Security.Entities;
using NSC_TournamentGen.Security.IServices;
using NSC_TournamentGen.Security.Models;
using System.Text;

namespace NSC_TournamentGen.Security
{
    public class AuthDbSeeder : IAuthDbSeeder
    {
        private readonly AuthDbContext _authCtx;
        private readonly ISecurityService _securityService;

        public AuthDbSeeder(AuthDbContext authCtx, ISecurityService securityService)
        {
            _authCtx = authCtx;
            _securityService = securityService;
        }

        public void SeedDevelopment()
        {
            #region Auth Db Context
            _authCtx.Database.EnsureDeleted();
            _authCtx.Database.EnsureCreated();

            _authCtx.LoginUsers.AddRange(
                new LoginUserEntity { Username = "Carlo", Role = "Administrator", HashedPassword = _securityService.HashPassword("test", Encoding.ASCII.GetBytes("Onii-chan")), Salt = "Onii-chan" },
                new LoginUserEntity { Username = "Niko", Role = "Administrator", HashedPassword = _securityService.HashPassword("test", Encoding.ASCII.GetBytes("Obel")), Salt = "Obel" },
                new LoginUserEntity { Username = "Svend", Role = "Administrator", HashedPassword = _securityService.HashPassword("test", Encoding.ASCII.GetBytes("Hallum")), Salt = "Hallum" }
            );

            _authCtx.SaveChanges();

            #endregion
        }

        public void SeedProduction()
        {
            _authCtx.Database.EnsureCreated();
        }
    }
}
