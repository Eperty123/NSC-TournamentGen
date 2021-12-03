using NSC_TournamentGen.Core.Models;

namespace NSC_TournamentGen.Domain.IRepositories
{
    public interface ITournamentRepository
    {
        Tournament ReadTournament(int id);
    }
}