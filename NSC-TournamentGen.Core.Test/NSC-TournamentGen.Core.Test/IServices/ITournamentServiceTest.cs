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
        
    }
}