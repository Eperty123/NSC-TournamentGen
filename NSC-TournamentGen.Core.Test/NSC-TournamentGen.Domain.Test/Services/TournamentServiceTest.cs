using Moq;
using NSC_TournamentGen.Core.Models;
using NSC_TournamentGen.Domain.IRepositories;
using NSC_TournamentGen.Domain.Services;
using System.Collections.Generic;
using Xunit;

namespace NSC_TournamentGen.Domain.Test.Services
{
    public class TournamentServiceTest
    {
        [Fact]


        public void TournamentService_GetTournament_NotNull()
        {
            var mockrepo = new Mock<ITournamentRepository>();
            var service = new TournamentService(mockrepo.Object);

            mockrepo.Setup(x => x.ReadTournament(1)).
                Returns(new Tournament());

            var foundUTournament = service.GetTournament(1);
            mockrepo.Verify(r => r.ReadTournament(1), Times.Once);
            Assert.NotNull(foundUTournament);
        }


        [Fact]
        public void TournamentService_GetAll_NotNull()
        {
            var mockrepo = new Mock<ITournamentRepository>();
            var service = new TournamentService(mockrepo.Object);

            mockrepo.Setup(x => x.ReadAllTournaments()).
                Returns(new List<Tournament>());

            var allTournaments = service.GetAllTournaments();
            mockrepo.Verify(r => r.ReadAllTournaments(), Times.AtLeastOnce);
            Assert.NotNull(allTournaments);
        }
    }
}