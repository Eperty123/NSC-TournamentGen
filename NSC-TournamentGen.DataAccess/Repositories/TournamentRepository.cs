using NSC_TournamentGen.Core.Models;
using NSC_TournamentGen.Domain.IRepositories;

namespace NSC_TournamentGen.DataAccess.Repositories
{
    public class TournamentRepository : ITournamentRepository
    {
        private readonly MainDbContext _ctx;

        public TournamentRepository(MainDbContext ctx)
        {
            _ctx = ctx;
        }
        
        public Tournament ReadTournament(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}