using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace NSC_TournamentGen.Core.Models
{
    public class TournamentManager
    {
        public int TournamentNumber;
        public int AmountOfBracket;

        public int AmountOfRounds;
        //public Dictionary<int, List<Bracket>> BracketsDictionary;


/*
        public TournamentManager()
        {
            Initialize();
        }

        private void Initialize()
        {
            BracketsDictionary = new Dictionary<int, List<Bracket>>();
        }

        public void TestPrintD()
        {
            foreach (KeyValuePair<int, List<Bracket>> list in BracketsDictionary)
            {
                Console.WriteLine("Key: {0}, Value: {1}",
                    list.Key, list.Value);
            }
        }
*/

        public void MakeTournament(TournamentInput tournamentInput, List<string> participants)
        {
            CalculateTournamentNumber(tournamentInput.AmountOfParticipants); // need info about Amount of Participant -> tournament input
            CalculateAmountOfRounds(tournamentInput
                .AmountOfParticipants); // need info about Amount of Participant -> tournament input + TournamentNumber
            CalculateAmountOfBracket(tournamentInput
                .AmountOfParticipants); // need info about Amount of Participant -> tournament input + TournamentNumber
            MakeFirstRound(participants);

            // MakeBracketForTournament(tournamentInput.AmountOfParticipants);
        }
/*
        public void MakeBracketForTournament(int amountOfParticipants)
        {
            if (NoPreRounds(amountOfParticipants))
            {
                MakeFirstRoundWithNoPreRounds();
                MakeAllBracketAfterFirstRound();
            }
            else
            {   MakePreRounds(amountOfParticipants);
                MakeFirstRoundWithPreRounds(amountOfParticipants);
                MakeAllBracketAfterFirstRound();
            }
        }

*/
        public Tournament MakeFirstRound(List<string> participants)
        {
            var randomList = MakeRandomList(participants);
            var amountOfParticipants = randomList.Count;
            var t = TournamentNumber;
            var bracketsList = new List<Bracket>();
            var amountLeft = amountOfParticipants;
            var counter = 0;
            var idCounter = 1;
            var amountOfBracket = Math.Ceiling((double) amountOfParticipants / 2);
            var amountOfBracketExe = amountOfParticipants - TournamentNumber;

            for (int i = 0; i < amountOfBracket; i++)
            {
                if (amountOfBracketExe > i)
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
                        bracket1.WinnerId1 = bracket1.Participant1.Id;
                        bracket1.WinnerId2 = bracket1.Participant2.Id;
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
                        bracket1.WinnerId1 = bracket1.Participant1.Id;
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
                Rounds = new List<Round> {round},
                UserId = 1
            };
        }


        /*  
        

        public Tournament MakeFirstRoundWithPreRounds(List<string> participants)
        {
            var randomList = MakeRandomList(participants);
            var amountOfParticipants = randomList.Count;
            var aor = AmountOfRounds;
            var t = TournamentNumber;
            var bracketsList = new List<Bracket>();
            var amountLeft = amountOfParticipants - ((amountOfParticipants - t) * 2);
            var counter = (amountOfParticipants - t) * 2;

            for (int i = 0; i < t / 2; i++)
            {
                var bracket = new Bracket
                {
                    Id = i + 1
                };
                
                if (amountLeft == 0)
                {
                    bracketsList.Add(bracket);
                }
                if (amountLeft % 2 == 0 && amountLeft > 0)
                {
                    var part1Name = randomList[i + counter];
                    bracket.Participant1 = new Participant
                    {
                        Name = part1Name
                    };
                    counter++;
                    var part2Name = randomList[i + counter];
                    bracket.Participant2 = new Participant
                    {
                        Name = part2Name
                    };;
                    amountLeft = amountLeft - 2;
                    bracketsList.Add(bracket);
                }
                if (amountLeft % 2 == 1 && amountLeft > 0)
                {
                    var part1Name = randomList[i + counter];
                    bracket.Participant1 = new Participant
                    {
                        Name = part1Name
                    };
                    amountLeft--;
                    bracketsList.Add(bracket);
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
                Rounds = new List<Round> {round},
                UserId = 1
            };
            // BracketsDictionary.Add(t, bracketsList);
        }

        public void MakeFirstRoundWithNoPreRounds()
        {
            var t = TournamentNumber;
            var counter = 0;
            var bracketsList = new List<Bracket>();

            for (int i = 0; i < t / 2; i++)
            {
                var bracket = new Bracket
                {
                    Id = i + 1,
                    Participants = new List<string>()
                };
                
                bracket.Participants.Add(RandomParticipantsList[i + counter]);
                counter++;
                bracket.Participants.Add(RandomParticipantsList[i + counter]);
                
                bracketsList.Add(bracket);
            }

            BracketsDictionary.Add(t, bracketsList);
        }

        public void MakeAllBracketAfterFirstRound()
        {
            var t = TournamentNumber /2;
            var bracketsList = new List<Bracket>();

            for (int j = 0; j < t; j++)
            {
                for (int i = 0; i <= t / 2; i++)
                {
                    var bracket = new Bracket
                    {
                        Id = i + 1,
                        Participants = new List<string>()
                    };
                    bracketsList.Add(bracket);
                }
                BracketsDictionary.Add(t, bracketsList);
                t = t / 2;
            }
        }

        public void MakePreRounds(int amountOfParticipants)
        {
            var aor = AmountOfRounds;
            var t = TournamentNumber;
            var counter = 0;
            var bracketsList = new List<Bracket>();
            for (int i = 0; i < amountOfParticipants - t; i++)
            {
                var bracket = new Bracket
                {
                    Id = i + 1,
                    Participants = new List<string>()
                };

                bracket.Participants.Add(RandomParticipantsList[i + counter]);
                counter++;
                bracket.Participants.Add(RandomParticipantsList[i + counter]);
                bracketsList.Add(bracket);
            }

            BracketsDictionary.Add(0, bracketsList);
        }

*/
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
            var t = TournamentNumber;
            var testDouble = Math.Ceiling((double)amountOfParticipants/2);
            return AmountOfBracket = (int) testDouble;
        }

        public int CalculateAmountOfRounds(int amountOfParticipants)
        {
            var t = TournamentNumber;
            while (t > 1)
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