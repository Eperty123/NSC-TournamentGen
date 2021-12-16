using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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
            var tournamentEntity = _ctx.Tournaments
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
                            new Participant { Id = bracket.Participant1Id, Name = bracket.Participant1.Name });
                    }

                    if (!participants.Exists(p => p.Id == bracket.Participant2Id))
                    {
                        participants.Add(
                            new Participant { Id = bracket.Participant2Id, Name = bracket.Participant2.Name });
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
                            Id = (int)bracket.Participant1?.Id,
                            Name = bracket.Participant1?.Name,
                        },
                        Participant2 = new Participant
                        {
                            Id = (int)bracket.Participant2?.Id,
                            Name = bracket.Participant2?.Name,
                        },
                        Participant1Id = (int)bracket.Participant1?.Id,
                        Participant2Id = (int)bracket.Participant2?.Id,
                        WinnerId = bracket.WinnerId,

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
            var tournamentEntityList = _ctx.Tournaments
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
                            { Id = bracket.Participant1Id, Name = bracket.Participant1.Name });
                        }

                        if (!participants.Exists(p => p.Id == bracket.Participant2Id))
                        {
                            participants.Add(new Participant
                            { Id = bracket.Participant2Id, Name = bracket.Participant2.Name });
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
                                Id = (int)bracket.Participant1?.Id,
                                Name = bracket.Participant1?.Name,
                            },
                            Participant2 = new Participant
                            {
                                Id = (int)bracket.Participant2?.Id,
                                Name = bracket.Participant2?.Name,
                            },
                            Participant1Id = (int)bracket.Participant1?.Id,
                            Participant2Id = (int)bracket.Participant2?.Id,
                            WinnerId = bracket.WinnerId,

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
            var foundTournament = _ctx.Tournaments.FirstOrDefault(x => x.Id == id);

            if (foundTournament != null)
            {
                // Remove from the database.
                _ctx.Tournaments.Remove(foundTournament);

                // Save changes to the database.
                _ctx.SaveChanges();

                // Return a *new* Tournament instance from the found tournament.
                return new Tournament { Id = foundTournament.Id, Name = foundTournament.Name };
            }

            // None found, return null.
            return null;
        }

        public Tournament MakeWinner(int tournamentId, int roundId, int bracketId, int participantId)
        {
            var foundTournament = _ctx.Tournaments.FirstOrDefault(x => x.Id == tournamentId);

            if (foundTournament != null)
            {
                // TODO: Fix wrong bracket id being updated.
                foreach (var round in foundTournament.Rounds)
                {
                    if (round.Id == roundId)
                    {
                        foreach (var bracket in round.Brackets)
                        {
                            if (bracket.Id == bracketId)
                                bracket.WinnerId = participantId;
                        }
                    }
                }


                _ctx.SaveChanges();

                // Return a *new* Tournament instance from the updated tournament.
                return new Tournament
                {
                    Id = foundTournament.Id,
                    Name = foundTournament.Name,
                    Rounds = foundTournament.Rounds.Select(round => new Round
                    {
                        Id = round.Id,
                        Name = round.Name,
                        Brackets = round.Brackets.Select(bracket => new Bracket
                        {
                            Id = bracket.Id,
                            Participant1 = new Participant
                            {
                                Id = (int)bracket.Participant1?.Id,
                                Name = bracket.Participant1?.Name,
                            },
                            Participant2 = new Participant
                            {
                                Id = (int)bracket.Participant2?.Id,
                                Name = bracket.Participant2?.Name,
                            },
                            Participant1Id = (int)bracket.Participant1Id,
                            Participant2Id = (int)bracket.Participant2Id,
                            WinnerId = bracket.WinnerId,

                        }).ToList()
                    }).ToList()
                };
            }

            return null;
        }

        public Tournament UpdateTournament(int id, Tournament tournament)
        {
            var foundTournament = _ctx.Tournaments.FirstOrDefault(x => x.Id == id);

            if (foundTournament != null)
            {
                // Make changes to the found tournament.
                foundTournament.Name = tournament.Name;
                foundTournament.Rounds = tournament.Rounds.Select(round => new RoundEntity
                {
                    Name = round.Name,
                    Brackets = round.Brackets.Select(bracket => new BracketEntity
                    {
                        Participant1 = new ParticipantEntity
                        {
                            Id = (int)bracket.Participant1?.Id,
                            Name = bracket.Participant1?.Name,
                        },
                        Participant2 = new ParticipantEntity
                        {
                            Id = (int)bracket.Participant2?.Id,
                            Name = bracket.Participant2?.Name,
                        },
                        Participant1Id = (int)bracket.Participant1?.Id,
                        Participant2Id = (int)bracket.Participant2?.Id,
                        WinnerId = bracket.WinnerId,

                    }).ToList()
                }).ToList();

                // Update the found tournament in the database.
                //_ctx.Tournaments.Update(foundTournament);

                // Save changes to the database.
                _ctx.SaveChanges();

                // Return a *new* Tournament instance from the updated tournament.
                return new Tournament
                {
                    Id = foundTournament.Id,
                    Name = foundTournament.Name,
                    Rounds = foundTournament.Rounds.Select(round => new Round
                    {
                        Id = round.Id,
                        Name = round.Name,
                        Brackets = round.Brackets.Select(bracket => new Bracket
                        {
                            Id = bracket.Id,
                            Participant1 = new Participant
                            {
                                Id = (int)bracket.Participant1?.Id,
                                Name = bracket.Participant1?.Name,
                            },
                            Participant2 = new Participant
                            {
                                Id = (int)bracket.Participant2?.Id,
                                Name = bracket.Participant2?.Name,
                            },
                            Participant1Id = (int)bracket.Participant1?.Id,
                            Participant2Id = (int)bracket.Participant2?.Id,
                            WinnerId = bracket.WinnerId,

                        }).ToList()
                    }).ToList()
                };
            }

            return null;
        }

        public Tournament CreateTournament(Tournament tournament)
        {
            var tournamentEntities = _ctx.Tournaments;
            var roundEntities = _ctx.Rounds;
            var bracketEntities = _ctx.Brackets;
            var userEntities = _ctx.Users;
            var newTournamentId = tournamentEntities.Count() + 1;
            var newRoundId = roundEntities.Count() + 1;
            var newBracketId = bracketEntities.Count() + 1;
            var newUserId = userEntities.Count() + 1;


            var tournamentEntity = new TournamentEntity()
            {
                Id = newTournamentId,
                Name = tournament?.Name,
            };
            tournamentEntities.Add(new TournamentEntity
            {
                Id = newTournamentId,
                Name = tournament.Name,
                Rounds = tournament.Rounds.Select(round => new RoundEntity
                {
                    Name = round.Name,
                    Id = newRoundId++ + 1,
                    Brackets = round.Brackets.Select(bracket => new BracketEntity
                    {
                        Id = newBracketId++ + 1,
                        Participant1 = new ParticipantEntity
                        {
                            Id = bracket.Participant1Id,
                            Name = bracket.Participant1?.Name,
                        },
                        Participant2 = new ParticipantEntity
                        {
                            Id = bracket.Participant2Id,
                            Name = bracket.Participant2?.Name,
                        }

                    }).ToList()
                }).ToList(),
                UserId = 1,
            });
            _ctx.SaveChanges();

            // Modify the id to match the created tournament's.
            tournament.Id = newTournamentId;
            return tournament;
        }
    }
}