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
        void TournamentManager_CanGenerateSingleElimination()
        {
            var tInput = new TournamentInput()
            {
                Participants = "Svend\nNiko\nCarlo\nRasmus"
            };
            var participantList = tInput.Participants.Split('\n').ToList();
            tInput.AmountOfParticipants = participantList.Count;
            var manager = new TournamentManager();
            manager.GenerateBrackets(tInput.AmountOfParticipants, participantList);
            Debug.WriteLine($"Brackets: {manager._bracketsDictionary.Count}");
            Assert.NotEmpty(manager._bracketsDictionary);
        }
    }
}
