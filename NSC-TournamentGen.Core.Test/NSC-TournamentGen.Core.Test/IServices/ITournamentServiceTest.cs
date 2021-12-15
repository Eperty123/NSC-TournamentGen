using System.Collections.Generic;
using Moq;
using NSC_TournamentGen.Core.IServices;
using NSC_TournamentGen.Core.Models;
using Xunit;

namespace NSC_TournamentGen.Core.Test.IServices
{
    public class ITournamentServiceTest
    {
        
        [Fact]

        public void ITournamentService_Exists()
        {
            var serviceMock = new Mock<ITournamentService>();
            Assert.NotNull(serviceMock.Object);

        }
        
        [Fact]
        public void ITournamentService_GetAllTournaments()
        {
            var serviceMock = new Mock<ITournamentService>();
            serviceMock
                .Setup(sevice => sevice.GetAllTournaments())
                .Returns(new List<Tournament>());
            Assert.NotNull(serviceMock.Object.GetAllTournaments());

        }
        [Fact]
        public void ITournamentService_GetTournament_NotNull()
        {
            var serviceMock = new Mock<ITournamentService>();
            serviceMock.Setup(service => service.GetTournament(1)).Returns(new Tournament());
            var foundTournament = serviceMock.Object.GetTournament(1);

            Assert.NotNull(foundTournament);
        }
        [Fact]
        public void ITournamentService_DeleteTournament_NotNull()
        {
            var serviceMock = new Mock<ITournamentService>();
            serviceMock.Setup(service => service.DeleteTournament(1)).Returns(new Tournament());
            var deletedTournament = serviceMock.Object.DeleteTournament(1);
            Assert.NotNull(deletedTournament);
        }
        [Fact]
        public void ITournamentService_UpdateTournament_NotNull()
        {
            var serviceMock = new Mock<ITournamentService>();
            var replacement = new Tournament() { Name  = "test"};
            serviceMock.Setup(service => service.UpdateTournament(1, replacement)).Returns(new Tournament());
            var updatedTournament = serviceMock.Object.UpdateTournament(1, replacement);
            Assert.NotNull(updatedTournament);
        }
        
        
    }
}