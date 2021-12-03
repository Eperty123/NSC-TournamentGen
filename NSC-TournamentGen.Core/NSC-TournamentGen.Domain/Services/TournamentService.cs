using System.Collections.Generic;
using NSC_TournamentGen.Core.IServices;
using NSC_TournamentGen.Core.Models;
using NSC_TournamentGen.Domain.IRepositories;

namespace NSC_TournamentGen.Domain.Services
{
    public class TournamentService : ITournamentService
    {
        private readonly ITournamentRepository _tournamentRepository;

        public TournamentService(ITournamentRepository tournamentRepository)
        {
            _tournamentRepository = tournamentRepository;
        }
        
        public List<Tournament> GetAllTournaments()
        {
            return _tournamentRepository.ReadAllTournaments();
        }

        public Tournament GetTournament(int id)
        {
            return _tournamentRepository.ReadTournament(id);
        }
    }
}