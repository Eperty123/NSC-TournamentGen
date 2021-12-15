using NSC_TournamentGen.Core.Models;
using System.Collections.Generic;

namespace NSC_TournamentGen.Domain.IRepositories
{
    public interface ITournamentRepository
    {
        Tournament ReadTournament(int id);
        List<Tournament> ReadAllTournaments();
        Tournament DeleteTournament(int id);
        Tournament UpdateTournament(int id, Tournament tournament);
        Tournament CreateTournament(Tournament tournament);
    }
}