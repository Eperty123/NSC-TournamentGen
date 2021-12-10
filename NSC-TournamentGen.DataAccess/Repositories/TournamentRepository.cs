using System.Collections.Generic;
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
            }).FirstOrDefault(t => t.Id == id);
        }

        public List<Tournament> ReadAllTournaments()
        {
            return _ctx.Tournament.Select(t => new Tournament
            {
                Id = t.Id,
                Name = t.Name,
                Participants = t.Participants,
                Type = t.Type,
            }).ToList();
        }

        public Tournament DeleteTournament(int id)
        {
            var foundTournament = _ctx.Tournament.FirstOrDefault(x => x.Id == id);

            if (foundTournament != null)
            {
                // Remove from the database.
                _ctx.Tournament.Remove(foundTournament);

                // Save changes to the database.
                _ctx.SaveChanges();

                // Return a *new* Tournament instance from the found tournament.
                return new Tournament { Id = foundTournament.Id, Name = foundTournament.Name, Participants = foundTournament.Participants, Type = foundTournament.Type};
            }

            // None found, return null.
            return null;
        }

        public Tournament UpdateTournament(int id, Tournament tournament)
        {
            var foundTournament = _ctx.Tournament.FirstOrDefault(x => x.Id == id);

            if (foundTournament != null)
            {
                // Make changes to the found tournament.
                foundTournament.Name = tournament.Name;
                foundTournament.Participants = tournament.Participants;
                foundTournament.Type = tournament.Type;

                // Update the found tournament in the database.
                _ctx.Tournament.Update(foundTournament);

                // Save changes to the database.
                _ctx.SaveChanges();

                // Return a *new* Tournament instance from the updated tournament.
                return new Tournament { Id = foundTournament.Id, Name = foundTournament.Name, Participants = foundTournament.Participants, Type = foundTournament.Type};
            }

            return null;
        }

        public TournamentInput CreateTournament(TournamentInput tournamentInput)
        {
            throw new System.NotImplementedException();
        }
    }
}