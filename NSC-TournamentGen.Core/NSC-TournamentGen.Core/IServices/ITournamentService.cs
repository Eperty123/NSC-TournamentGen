using System.Collections.Generic;
using NSC_TournamentGen.Core.Models;

namespace NSC_TournamentGen.Core.IServices
{
    public interface ITournamentService
    {
        List<Tournament> GetAllTournaments();
    }
}