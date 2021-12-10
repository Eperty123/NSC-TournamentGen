﻿using NSC_TournamentGen.Core.Models;
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
            _ctx.Tournament.Add(new TournamentEntity() { Id = 1, Name = "Svend", Participants = "lol", Type = TournamentType.SingleElimination });
            _ctx.Tournament.Add(new TournamentEntity() { Id = 2, Name = "Sick Tournament", Participants = "lol", Type = TournamentType.SingleElimination });
            _ctx.Tournament.Add(new TournamentEntity() { Id = 3, Name = "HentaiTournamentNSFW", Participants = "carlo", Type = TournamentType.SingleElimination });

            _ctx.SaveChanges();
            #endregion
        }

        public void SeedProduction()
        {
            _ctx.Database.EnsureCreated();
        }
    }
}