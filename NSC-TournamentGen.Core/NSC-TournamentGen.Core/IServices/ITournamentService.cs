using System.Collections.Generic;
using NSC_TournamentGen.Core.Models;

namespace NSC_TournamentGen.Core.IServices
{
    public interface ITournamentService
    {
        Tournament GetTournament(int id);
        List<Tournament> GetAllTournaments();
    }
}