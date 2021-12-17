using Microsoft.Extensions.DependencyInjection;
using Moq;
using NSC_TournamentGen.Core.IServices;
using NSC_TournamentGen.Core.Models;
using NSC_TournamentGen.Domain;
using NSC_TournamentGen.Domain.IRepositories;
using NSC_TournamentGen.Domain.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace NSC_TournamentGen.Core.Test
{

    public class TournamantManagerTest
    {
        Mock<ITournamentService> _service = new Mock<ITournamentService>();
        Mock<ITournamentRepository> _repository = new Mock<ITournamentRepository>();

        [Fact]
        void TournamentManager_CanGenerateSingleEliminationWithFourParticipants()
        {
            var tInput = new TournamentInput()
            {
                Participants = "Svend\nNiko\nCarlo\nRasmus"
            };


            var participantList = tInput.Participants.Split('\n').ToList();
            var manager = new TournamentManager(_service.Object, _repository.Object);
            manager.MakeTournament(tInput);
            Assert.Equal(expected: 3, manager.AmountOfBrackets);

        }

        [Fact]
        void TournamentManager_CanGenerateSingleEliminationWithTenParticipants()
        {
            var tInput = new TournamentInput()
            {
                Participants = "Carlo\nRasmus\nNiko\nCarlo\nRasmus\nNiko\nCarlo\nRasmus\nCarlo\nRasmus"
            };
            var participantList = tInput.Participants.Split('\n').ToList();
            var manager = new TournamentManager(_service.Object, _repository.Object);
            manager.MakeTournament(tInput);
            Assert.Equal(expected: 12, manager.AmountOfBrackets);
        }

        [Fact]
        void TournamentManager_GenerateRounds()
        {
            var tInput = new TournamentInput()
            {
                Participants = "Carlo\nRasmus\nNiko\nCarlo\nRasmus\nNiko\nCarlo\nRasmus"
            };
            var participantList = tInput.Participants.Split('\n').ToList();
            var manager = new TournamentManager(_service.Object, _repository.Object);
            manager.MakeTournament(tInput);
            var rounds = manager.GenerateAllRounds(participantList);

            Console.WriteLine(string.Join(",", rounds));
            Assert.Equal(3, rounds.Count);
        }



    }
}
