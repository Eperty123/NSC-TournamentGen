using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NSC_TournamentGen.Core.Models;
using NSC_TournamentGen.DataAccess.Entities;
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
            var tournamentEntity = _ctx.Tournament
                .Include(t => t.User)
                .Include(t => t.Rounds)
                .ThenInclude(r => r.Brackets)
                .ThenInclude(b => b.Participant1)
                .Include(t => t.Rounds)
                .ThenInclude(r => r.Brackets)
                .ThenInclude(b => b.Participant2)
                .FirstOrDefault(t => t.Id == id);

            if (tournamentEntity == null)
            {
                return null;
            }

            var participants = new List<Participant>();
            foreach (var round in tournamentEntity.Rounds)
            {
                foreach (var bracket in round.Brackets)
                {
                    if (!participants.Exists(p => p.Id == bracket.Participant1Id))
                    {
                        participants.Add(
                            new Participant {Id = bracket.Participant1Id, Name = bracket.Participant1.Name});
                    }

                    if (!participants.Exists(p => p.Id == bracket.Participant2Id))
                    {
                        participants.Add(
                            new Participant {Id = bracket.Participant2Id, Name = bracket.Participant2.Name});
                    }
                }
            }

            var tournament = new Tournament
            {
                Id = tournamentEntity.Id,
                Name = tournamentEntity.Name,
                Rounds = tournamentEntity.Rounds.Select(round => new Round
                {
                    Name = round.Name,
                    Id = round.Id,
                    Brackets = round.Brackets.Select(bracket => new Bracket
                    {
                        Id = bracket.Id,
                        Participant1 = new Participant
                        {
                            Id = bracket.Participant1Id,
                            Name = bracket.Participant1.Name
                        },
                        Participant2 = new Participant
                        {
                            Id = bracket.Participant2Id,
                            Name = bracket.Participant2.Name
                        }

                    }).ToList()
                }).ToList(),
                User = new User
                {
                    Id = tournamentEntity.UserId,
                    Username = tournamentEntity.User.Username
                }
            };

            return tournament;
        }

        public List<Tournament> ReadAllTournaments()
        {
            var tournamentEntityList = _ctx.Tournament
                .Include(t => t.User)
                .Include(t => t.Rounds)
                .ThenInclude(r => r.Brackets)
                .ThenInclude(b => b.Participant1)
                .Include(t => t.Rounds)
                .ThenInclude(r => r.Brackets)
                .ThenInclude(b => b.Participant2)
                .ToList();

            var tournamentList = new List<Tournament>();
            foreach (var tournamentEntity in tournamentEntityList)
            {
                var participants = new List<Participant>();
                foreach (var round in tournamentEntity.Rounds)
                {
                    foreach (var bracket in round.Brackets)
                    {
                        if (!participants.Exists(p => p.Id == bracket.Participant1Id))
                        {
                            participants.Add(new Participant
                                {Id = bracket.Participant1Id, Name = bracket.Participant1.Name});
                        }

                        if (!participants.Exists(p => p.Id == bracket.Participant2Id))
                        {
                            participants.Add(new Participant
                                {Id = bracket.Participant2Id, Name = bracket.Participant2.Name});
                        }
                    }
                }

                var tournament = new Tournament
                {
                    Id = tournamentEntity.Id,
                    Name = tournamentEntity.Name,
                    Rounds = tournamentEntity.Rounds.Select(round => new Round
                    {
                        Name = round.Name,
                        Id = round.Id,
                        Brackets = round.Brackets.Select(bracket => new Bracket
                        {
                            Id = bracket.Id,
                            Participant1 = new Participant
                            {
                                Id = bracket.Participant1Id,
                                Name = bracket.Participant1.Name
                            },
                            Participant2 = new Participant
                            {
                                Id = bracket.Participant2Id,
                                Name = bracket.Participant2.Name
                            }

                        }).ToList()
                    }).ToList(),
                    User = new User
                    {
                        Id = tournamentEntity.UserId,
                        Username = tournamentEntity.User.Username
                    }
                };
                tournamentList.Add(tournament);
            }

            return tournamentList;
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
                return new Tournament {Id = foundTournament.Id, Name = foundTournament.Name};
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
                // foundTournament.Participants = tournament.Participants;
                // foundTournament.Type = tournament.Type;

                // Update the found tournament in the database.
                _ctx.Tournament.Update(foundTournament);

                // Save changes to the database.
                _ctx.SaveChanges();

                // Return a *new* Tournament instance from the updated tournament.
                return new Tournament {Id = foundTournament.Id, Name = foundTournament.Name};
            }

            return null;
        }

        public Tournament CreateTournament(Tournament tournament)
        {
            var tournamentEntity = _ctx.Tournament;
            
            _ctx.Add(new TournamentEntity
            {
                Id = tournament.Id,
                Name = tournament.Name,
                Rounds = tournament.Rounds.Select(round => new RoundEntity
                {
                    Name = round.Name,
                    Id = round.Id,
                    Brackets = round.Brackets.Select(bracket => new BracketEntity
                    {
                        Id = bracket.Id,
                        Participant1 = new ParticipantEntity
                        {
                            Id = bracket.Participant1Id,
                            Name = bracket.Participant1.Name
                        },
                        Participant2 = new ParticipantEntity
                        {
                            Id = bracket.Participant2Id,
                            Name = bracket.Participant2.Name
                        }

                    }).ToList()
                }).ToList()
            });
            _ctx.SaveChanges();
            //Not sure what to return, lars hjælp
            return tournament;
        }
    }
}