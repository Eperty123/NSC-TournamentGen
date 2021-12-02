using System.Linq;
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
            return _ctx.Tournament.Select(t => new Tournament
            {
                Id = t.Id,
                Name = t.Name,
                Participants = t.Participants,
                Type = t.Type
            }).FirstOrDefault(t => id == t.Id);
        }
    }
}