using NSC_TournamentGen.Core.IServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NSC_TournamentGen.Core.Models
{
    public class TournamentManager
    {
        public int TournamentNumber;
        public List<string> Participants { get; set; }
        ITournamentService _tournamentService;

        public TournamentManager(ITournamentService tournamentService)
        {
            Initialize();
            _tournamentService = tournamentService;
        }

        private void Initialize()
        {
            Participants = new List<string>();
        }

        public Tournament MakeTournament(TournamentInput tournamentInput)
        {
            Participants = new List<string>(tournamentInput.Participants.Split('\n'));
            CalculateTournamentNumber(Participants.Count); // need info about Amount of Participant -> tournament input      
            var rounds = GenerateAllRounds(Participants);
            return new Tournament()
            {
                Id = 1,
                Name = tournamentInput.Name,
                Rounds = rounds,
            };
        }

        public Tournament AssignWinnersForNextRound(int tournamentId)
        {
            var tournament = _tournamentService.GetTournament(tournamentId);
            if (tournament != null)
            {
                var allRounds = tournament.Rounds;
                var unfinishedRounds = allRounds.Where(x => !IsRoundComplete(x)).ToList();
                var finishedRounds = allRounds.Where(x => IsRoundComplete(x)).ToList();
                var currentRound = unfinishedRounds.FirstOrDefault();

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
                                var validRoundIndex = tournament.CurrentRoundId % allRounds.Count;
                                var nextRound = allRounds[validRoundIndex + 1];
                                var nextBracket = nextRound.Brackets[i];
                                if (nextRound != null && nextBracket != null)
                                {
                                    switch (GetAvailableSlot(nextBracket))
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
                                    }
                                }

                                nextRound.Brackets[i] = nextBracket;
                            }
                        }
                    }
                }
                return tournament;
            }
            return null;
        }

        public int GetAvailableSlot(Bracket bracket)
        {
            return bracket.Participant1Id == 0 ? 0 : bracket.Participant2Id == 0 ? 1 : -1;
        }

        public Bracket UpdateBracketWinner(int tournamentId, int roundId, int participantId)
        {
            var tournament = _tournamentService.GetTournament(tournamentId);
            var desiredRound = tournament.Rounds.FirstOrDefault(x => x.Id == roundId);
            var desiredBracket = desiredRound.Brackets.FirstOrDefault(x => x.Participant1Id == participantId || x.Participant2Id == participantId);
            if (desiredBracket != null)
            {
                desiredBracket.WinnerId = participantId;
                return desiredBracket;
            }
            return null;
        }

        public bool IsRoundComplete(Round round)
        {
            return round != null && round.Brackets.All(x => x.WinnerId > 0);
        }

        public List<Round> GenerateAllRounds(List<string> participants)
        {
            var t = TournamentNumber;
            var players = participants.Count;
            var bracketsList = new List<Bracket>();
            var roundList = new List<Round>();
            int id = 1;

            if (players == 4 || players == 8 || players == 16 || players == 32)
            {
                t = t / 2;
            }
            var rounds = t;

            t = t / 2;

            roundList.Add(MakeFirstRound(participants));

            while (rounds > 1)
            {
                for (int i = 0; i < t; i++)
                {
                    var bracket = new Bracket();
                    bracketsList.Add(bracket);
                }
                var round = new Round
                {
                    Name = $"Round {id + 1 }",
                    Brackets = new List<Bracket>(bracketsList)
                };
                bracketsList.Clear();
                rounds /= 2;
                t /= 2;
                roundList.Add(round);
                id++;
            }


            return roundList;
        }

        public Round MakeFirstRound(List<string> participants)
        {
            var randomList = MakeRandomList(participants);
            var amountOfParticipants = randomList.Count;
            var bracketsList = new List<Bracket>();
            var amountLeft = amountOfParticipants;
            var counter = 0;
            var idCounter = 1;
            var amountOfBracket = Math.Round((decimal)amountOfParticipants / 2);
            var amountOfBracketToExecute = amountOfParticipants - TournamentNumber;

            for (int i = 0; i < amountOfBracket; i++)
            {
                if (amountOfBracketToExecute > i)
                {
                    var bracket = new Bracket
                    {
                        Id = i + 1,
                        IsExecuted = true
                    };
                    var part1Name = randomList[i + counter];
                    bracket.Participant1 = new Participant
                    {
                        Id = idCounter,
                        Name = part1Name
                    };
                    idCounter++;
                    counter++;
                    var part2Name = randomList[i + counter];
                    bracket.Participant2 = new Participant
                    {
                        Id = idCounter,
                        Name = part2Name
                    };
                    idCounter++;
                    amountLeft = amountLeft - 2;
                    bracketsList.Add(bracket);
                }
                else
                {
                    var bracket1 = new Bracket
                    {
                        Id = i + 1,
                        IsExecuted = true,

                    };
                    if (amountLeft % 2 == 0 && amountLeft > 0)
                    {
                        var part1Name = randomList[i + counter];
                        bracket1.Participant1 = new Participant
                        {
                            Id = idCounter,
                            Name = part1Name
                        };
                        counter++;
                        idCounter++;
                        var part2Name = randomList[i + counter];
                        bracket1.Participant2 = new Participant
                        {
                            Id = idCounter,
                            Name = part2Name
                        };
                        bracket1.WinnerId = bracket1.Participant1.Id;
                        amountLeft = amountLeft - 2;
                        bracketsList.Add(bracket1);
                    }

                    if (amountLeft % 2 == 1 && amountLeft > 0)
                    {
                        var part1Name = randomList[i + counter];
                        bracket1.Participant1 = new Participant
                        {
                            Id = idCounter,
                            Name = part1Name
                        };
                        idCounter++;
                        amountLeft--;
                        bracket1.WinnerId = bracket1.Participant1.Id;
                        bracketsList.Add(bracket1);
                    }
                }
            }
            return new Round
            {
                Name = "Round 1",
                Brackets = bracketsList
            };
        }

        public List<string> MakeRandomList(List<string> participants)
        {
            var random = new Random();
            var testList = participants.OrderBy(item => random.Next());


            return testList.ToList();
        }

        public int CalculateTournamentNumber(int amountOfParticipants)
        {
            if (amountOfParticipants > 32 || amountOfParticipants < 4)
            {
                throw new Exception("Participant is over 32 or lower then 4.");
            }

            if (amountOfParticipants == 32)
            {
                return TournamentNumber = 32;
            }

            if (amountOfParticipants > 15)
            {
                return TournamentNumber = 16;
            }

            if (amountOfParticipants > 7)
            {
                return TournamentNumber = 8;
            }

            if (amountOfParticipants == 4)
            {
                return TournamentNumber = 4;
            }
            else
            {
                throw new Exception("noget gik galt ved CalculatTournamentNUmbers");
            }
        }
    }
}