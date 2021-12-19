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
            return tournamentEntity.ToModel();
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
                tournamentList.Add(tournamentEntity.ToModel());
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
                var desiredRound = foundTournament.Rounds.FirstOrDefault(x => x.Id == roundId);
                var desiredBracket = desiredRound.Brackets.FirstOrDefault(x => x.Id == bracketId);
                var desiredWinner = desiredBracket.WinnerId = participantId;

                _ctx.SaveChanges();

                // Return a *new* Tournament instance from the updated tournament.
                return foundTournament.ToModel();
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
                            //Id = (int)bracket.Participant1?.Id,
                            Name = bracket.Participant1?.Name,
                        },
                        Participant2 = new ParticipantEntity
                        {
                            //Id = (int)bracket.Participant2?.Id,
                            Name = bracket.Participant2?.Name,
                        },
                        Participant1Id = (int)bracket.Participant1?.Id,
                        Participant2Id = (int)bracket.Participant2?.Id,
                        WinnerId = bracket.WinnerId,

                    }).ToList(),
                }).ToList();

                foundTournament.CurrentRoundId = tournament.CurrentRoundId;

                // Update the found tournament in the database.
                //_ctx.Tournaments.Update(foundTournament);

                // Save changes to the database.
                _ctx.SaveChanges();

                // Return a *new* Tournament instance from the updated tournament.
                return foundTournament.ToModel();
            }

            return null;
        }

        public Tournament CreateTournament(Tournament tournament)
        {
            var tournamentEntities = _ctx.Tournaments;
            var roundEntities = _ctx.Rounds;
            var bracketEntities = _ctx.Brackets;
            var userEntities = _ctx.Users;
            var participantEntities = _ctx.Participants;
            var newTournamentId = (tournamentEntities.Count() > 0 ? tournamentEntities.ToList().Max(x => x.Id) : tournamentEntities.Count()) + 1;
            var newRoundId = (roundEntities.Count() > 0 ? roundEntities.ToList().Max(x => x.Id) : roundEntities.Count()) + 1;
            var newBracketId = (bracketEntities.Count() > 0 ? bracketEntities.ToList().Max(x => x.Id) : bracketEntities.Count()) + 1;
            var newUserId = (userEntities.Count() > 0 ? userEntities.ToList().Max(x => x.Id) : userEntities.Count()) + 1;
            var newParticiapntId = (participantEntities.Count() > 0 ? participantEntities.ToList().Max(x => x.Id) : participantEntities.Count()) + 1;
            var rounds = new List<RoundEntity>();
            var brackets = new List<BracketEntity>();

            var tournamentEntity = new TournamentEntity()
            {
                Id = newTournamentId,
                Name = tournament.Name,
                UserId = 1,
                //CurrentRoundId = 1,
            };
            int roundIndex = 0;

            foreach (var round in tournament.Rounds)
            {
                int bracketIndex = 0;
                roundIndex++;
                foreach (var bracket in round.Brackets)
                {
                    bracketIndex++;
                    var bracketEntity = new BracketEntity()
                    {
                        Id = newBracketId++,
                        IsExecuted = bracket.IsExecuted,
                    };
                    ParticipantEntity participant1 = null;
                    ParticipantEntity participant2 = null;

                    participant1 = new ParticipantEntity()
                    {
                        Id = newParticiapntId++,
                        Name = bracket.Participant1 != null ? bracket.Participant1.Name : null,
                    };

                    participant2 = new ParticipantEntity()
                    {
                        Id = newParticiapntId++,
                        Name = bracket.Participant2 != null ? bracket.Participant2.Name : null,
                    };

                    if (bracket.IsExecuted)
                        bracketEntity.WinnerId = newParticiapntId - 1;
                    bracketEntity.Participant1 = participant1;
                    bracketEntity.Participant2 = participant2;
                    bracketEntity.Participant1Id = participant1.Id;
                    bracketEntity.Participant2Id = participant2.Id;

                    //if (roundIndex == tournament.Rounds.Count && bracketIndex == round.Brackets.Count)
                    //    bracketEntity.IsExecuted = true;

                    brackets.Add(bracketEntity);
                }

                var roundEntity = new RoundEntity()
                {
                    Name = round.Name,
                    Id = newRoundId++,
                    Brackets = new List<BracketEntity>(brackets),
                    TournamentId = newTournamentId,
                };

                rounds.Add(roundEntity);
                brackets.Clear();
            }
            tournamentEntity.Rounds = rounds;

            tournamentEntities.Add(tournamentEntity);
            _ctx.SaveChanges();

            // Modify the id to match the created tournament's.
            tournament.Id = newTournamentId;
            return tournament;
        }

        public Tournament AssignWinnersForNextRound(int tournamentId, int roundId)
        {
            var tournament = _ctx.Tournaments.FirstOrDefault(x => x.Id == tournamentId);
            if (tournament != null)
            {
                tournament.CurrentRoundId = roundId;
                var allRounds = tournament.Rounds;
                var currentRound = allRounds.FirstOrDefault(x => x.Id == roundId);
                var currentRoundIndex = allRounds.IndexOf(currentRound) + 1;

                if (currentRound != null)
                {
                    // Assign winners from each bracket to the next round.
                    for (int i = 0; i < currentRound.Brackets.Count; i++)
                    {
                        var bracket = currentRound.Brackets[i];
                        if (bracket.WinnerId > 0)
                        {
                            var winner = bracket.Participant1 != null && bracket.Participant1.Id == bracket.WinnerId ? bracket.Participant1 :
                                bracket.Participant2 != null && bracket.Participant2.Id == bracket.WinnerId ? bracket.Participant2 : null;

                            // Found winner, assign it to the next round's corresponding bracket.
                            if (winner != null)
                            {
                                var validRoundIndex = currentRoundIndex % allRounds.Count;
                                var nextRound = allRounds[validRoundIndex];
                                var validBracketIndex = (i > 0 ? i - 1 : i) % nextRound.Brackets.Count;
                                var dividedIndex = nextRound.Brackets.Count / 2;
                                var evenBrackets = currentRound.Brackets.Count == nextRound.Brackets.Count;

                                // Even number work but odd does not.
                                //var nextBracket = currentRoundIndex > (dividedIndex % 2 == 0 ? dividedIndex : dividedIndex - 1) ? GetAvailableBracket(nextRound) : nextRound.Brackets[!evenBrackets ? validBracketIndex : i];
                                
                                // Odd number work but even does not.
                                var nextBracket = currentRoundIndex > (dividedIndex % 2 == 0 ? dividedIndex : dividedIndex % 2 == 1 ? dividedIndex : dividedIndex - i) ? GetAvailableBracket(nextRound) : nextRound.Brackets[!evenBrackets ? validBracketIndex : i];
                                var alreadyRegistered = IsAlreadyRegisteredOnBracket(nextBracket, winner) || IsParticipantAssignedABracket(nextRound, winner.Id);
                                var availableSlot = GetAvailableSlot(nextBracket);

                                if (nextRound != null && nextBracket != null && !alreadyRegistered)
                                {
                                    switch (availableSlot)
                                    {
                                        // Participant 1
                                        case 0:
                                            nextBracket.Participant1 = winner;
                                            nextBracket.Participant1Id = winner.Id;
                                            break;

                                        // Participant 2
                                        case 1:
                                            nextBracket.Participant2 = winner;
                                            nextBracket.Participant2Id = winner.Id;
                                            break;

                                            // No spots left, too bad.
                                    }

                                    // Finish this bracket.
                                    bracket.IsExecuted = true;
                                }
                            }
                        }
                    }
                }

                _ctx.SaveChanges();
                return tournament.ToModel();
            }
            return null;
        }

        public bool IsRoundComplete(RoundEntity round)
        {
            return round != null && round.Brackets.All(x => x.WinnerId > 0);
        }

        public bool IsAlreadyRegisteredOnBracket(BracketEntity bracket, ParticipantEntity participant)
        {
            return bracket != null && participant != null && ((bracket.Participant1 != null && bracket.Participant1.Id == participant.Id ||
                bracket.Participant2 != null && bracket.Participant2.Id == participant.Id));
        }


        public bool IsParticipantAssignedABracket(RoundEntity round, int participantId)
        {
            return round.Brackets.Any(x => x.Participant1.Id == participantId || x.Participant2.Id == participantId);
        }

        public int GetAvailableSlot(BracketEntity bracket)
        {
            return bracket != null && (bracket.Participant1 == null || bracket.Participant1 != null && string.IsNullOrEmpty(bracket.Participant1.Name)) ? 0 :
                bracket != null && (bracket.Participant2 == null || bracket.Participant2 != null && string.IsNullOrEmpty(bracket.Participant2.Name)) ? 1
                : -1;
        }

        public BracketEntity GetAvailableBracket(RoundEntity round)
        {
            return round.Brackets.FirstOrDefault(x => string.IsNullOrEmpty(x.Participant1?.Name) || string.IsNullOrEmpty(x.Participant2?.Name));
        }
    }
}