using NSC_TournamentGen.Core.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NSC_TournamentGen.Core.Test
{

    public class TournamantManagerTest
    {

        [Fact]
        void TournamentManager_CanGenerateSingleEliminationWithFourParticipants()
        {
            var tInput = new TournamentInput()
            {
                Participants = "Svend\nNiko\nCarlo\nRasmus"
            };
            var participantList = tInput.Participants.Split('\n').ToList();
            tInput.AmountOfParticipants = participantList.Count;
            var manager = new TournamentManager();
            manager.MakeTournament(tInput,participantList);
            Assert.Equal(expected:2,manager.TournamentNumber/2);
            manager.TestPrintD();
        }
        
        [Fact]
        void TournamentManager_CanGenerateSingleEliminationWithTenParticipants()
        {
            var tInput = new TournamentInput()
            {
                Participants = "Svend\nNiko\nCarlo\nRasmus\nNiko\nCarlo\nRasmus\nNiko\nCarlo\nRasmus"
            };
            var participantList = tInput.Participants.Split('\n').ToList();
            tInput.AmountOfParticipants = participantList.Count;
            var manager = new TournamentManager();
            manager.MakeTournament(tInput, participantList);
            Debug.WriteLine($"Brackets: {manager.BracketsDictionary.Count}");
            Assert.Equal(expected:9,manager.AmountOfBracket);
            
        }
    }
}
