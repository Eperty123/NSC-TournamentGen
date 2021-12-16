using System.Collections.Generic;
using NSC_TournamentGen.Core.Models;

namespace NSC_TournamentGen.Core.IServices
{
    public interface ITournamentService
    {
        Tournament GetTournament(int id);
        List<Tournament> GetAllTournaments();
        Tournament DeleteTournament(int id);
        Tournament UpdateTournament(int id, Tournament tournament);
        Tournament CreateTournament(TournamentInput tournamentInput);
        public Tournament MakeWinner(int tournamentId, int roundId, int bracketId, int participantId);
    }
}