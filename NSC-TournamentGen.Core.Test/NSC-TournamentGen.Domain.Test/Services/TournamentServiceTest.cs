using Moq;
using NSC_TournamentGen.Core.Models;
using NSC_TournamentGen.Domain.IRepositories;
using NSC_TournamentGen.Domain.Services;
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
    }
}