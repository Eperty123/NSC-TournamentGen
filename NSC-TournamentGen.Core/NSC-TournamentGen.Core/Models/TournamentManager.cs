using NSC_TournamentGen.Core.IServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NSC_TournamentGen.Core.Models
{
    public class TournamentManager
    {
        public int TournamentNumber;
        public int AmountOfBrackets;

        public int AmountOfRounds;
        public List<string> Participants;
        public Dictionary<int, List<Bracket>> BracketsDictionary;

        ITournamentService _tournamentService;



        public TournamentManager(ITournamentService tournamentService)
        {
            Initialize();
            _tournamentService = tournamentService;
        }

        private void Initialize()
        {
            BracketsDictionary = new Dictionary<int, List<Bracket>>();
            Participants = new List<string>();
        }

        public Tournament MakeTournament(TournamentInput tournamentInput)
        {
            Participants = new List<string>(tournamentInput.Participants.Split('\n'));
            CalculateTournamentNumber(Participants.Count); // need info about Amount of Participant -> tournament input
            CalculateAmountOfRounds(Participants.Count); // need info about Amount of Participant -> tournament input + TournamentNumber
            CalculateAmountOfBracket(Participants.Count); // need info about Amount of Participant -> tournament input + TournamentNumber
            //return MakeFirstRound(participants);
            var rounds = GenerateAllRounds(Participants);
            return new Tournament()
            {
                Id = 1,
                Name = tournamentInput.Name,
                Rounds = rounds,
            };
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
            var rounds = new List<Round>();
            var randomList = MakeRandomList(participants);
            var amountOfParticipants = randomList.Count;
            var bracketsList = new List<Bracket>();
            var amountLeft = amountOfParticipants;
            var counter = 0;
            var idCounter = 1;
            var amountOfBrackets = Math.Ceiling((double)amountOfParticipants / 2);
            var amountOfBracketsToExecute = amountOfParticipants - TournamentNumber;

            for (int i = 0; i < AmountOfRounds; i++)
            {
                for (int b = 0; b < amountOfBrackets; b++)
                {
                    var bracket = new Bracket
                    {
                        //Id = b + 1,
                    };

                    // First round only...
                    if (i == 0)
                    {
                        if (amountOfBracketsToExecute > b)
                        {
                            bracket.IsExecuted = true;
                            var part1Name = randomList[b + counter];
                            bracket.Participant1 = new Participant
                            {
                                Id = idCounter,
                                Name = part1Name
                            };
                            idCounter++;
                            counter++;
                            var part2Name = randomList[b + counter];
                            bracket.Participant2 = new Participant
                            {
                                Id = idCounter,
                                Name = part2Name
                            };
                            idCounter++;
                            amountLeft = amountLeft - 2;
                            //bracketsList.Add(bracket);
                        }
                        else
                        {
                            if (amountLeft % 2 == 0 && amountLeft > 0)
                            {
                                var part1Name = randomList[b + counter];

                                bracket.Participant1 = new Participant
                                {
                                    Id = idCounter,
                                    Name = part1Name
                                };
                                counter++;
                                idCounter++;
                                var part2Name = randomList[b + counter];
                                bracket.Participant2 = new Participant
                                {
                                    Id = idCounter,
                                    Name = part2Name
                                };
                                bracket.WinnerId = bracket.Participant1.Id;
                                amountLeft = amountLeft - 2;
                                //bracketsList.Add(bracket);
                            }
                            else if (amountLeft % 2 == 1 && amountLeft > 0)
                            {
                                var part1Name = randomList[b + counter];

                                bracket.Participant1 = new Participant
                                {
                                    Id = idCounter,
                                    Name = part1Name
                                };
                                idCounter++;
                                amountLeft--;
                                bracket.WinnerId = bracket.Participant1.Id;
                                //bracketsList.Add(bracket);
                            }
                        }
                    }
                    else
                    {
                        // Make placeholder participants.
                        bracket.Participant1 = new Participant
                        {
                            Name = ""
                        };
                        bracket.Participant2 = new Participant
                        {
                            Name = ""
                        };
                    }

                    bracketsList.Add(bracket);
                }


                rounds.Add(new Round()
                {
                    Id = i,
                    Brackets = new List<Bracket>(bracketsList),
                    Name = $"Round {i + 1}"
                });

                // Half the amount of bracket to get going please.
                amountOfBrackets /= 2;

                // Clear it to be re-used for next round.
                bracketsList.Clear();
            }
            return rounds;
        }

        public Tournament MakeFirstRound(List<string> participants)
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
            var round = new Round
            {
                Name = "Round 1",
                Brackets = bracketsList
            };
            return new Tournament
            {
                Name = "Tha Tournament",
                Rounds = new List<Round> { round },
                UserId = 1
            };
        }

        public List<string> MakeRandomList(List<string> participants)
        {
            var random = new Random();
            var testList = participants.OrderBy(item => random.Next());


            return testList.ToList();
        }


        public bool NoPreRounds(int amountOfParticipants)
        {
            var t = TournamentNumber;
            if (t - amountOfParticipants != 0)
            {
                return false;
            }

            if (t - amountOfParticipants == 0)
            {
                return true;
            }

            throw new Exception("Noget gik galt med noPreRounds");
        }

        public int CalculateAmountOfBracket(int amountOfParticipants)
        {
            return AmountOfBrackets = (int)Math.Ceiling((double)amountOfParticipants / 2);
        }

        public int CalculateAmountOfRounds(int amountOfParticipants)
        {
            var t = TournamentNumber;
            while (t > 0)
            {
                t = t / 2;
                AmountOfRounds++;
            }

            if (!NoPreRounds(amountOfParticipants))
            {
                return AmountOfRounds + 1;
            }


            return AmountOfRounds;
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