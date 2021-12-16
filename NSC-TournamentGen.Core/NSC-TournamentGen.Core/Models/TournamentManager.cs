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
            CalculateAmountOfBracket(Participants);
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
            var t = TournamentNumber;
            var players = participants.Count;
            var bracketsList = new List<Bracket>();
            var roundList = new List<Round>();
            int id = 0;

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
            var amountOfBracket = Math.Round((decimal) amountOfParticipants / 2);
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

        public List<Round> MakeAllBracketAfterFirstRound(List<string> participants)
        {
            var t = TournamentNumber;
            var players = participants.Count;
            var bracketsList = new List<Bracket>();
            var roundList = new List<Round>();

            if (players == 4 || players == 8 || players == 16 || players == 32)
            {
                t = t / 2;
            }
            var rounds = t;
            
            t = t / 2;

            while (rounds > 1)
            {
                for (int i = 0; i < t; i++)
                {
                    var bracket = new Bracket();
                    bracketsList.Add(bracket);
                }
                var round = new Round
                {
                    Name = "skal laves",
                    Brackets = new List<Bracket>(bracketsList)
                };
                bracketsList.Clear();
                rounds /= 2;
                t /=2 ;
                roundList.Add(round);
            }

            return roundList;
        }

        public int CalculateAmountOfBracket(List<string> participants)
        {
            var amountOfParticipant = participants.Count;
            var t = TournamentNumber;
            var amountOfBracketInPreRounds = Math.Ceiling((double) amountOfParticipant)/2;
            var totalbracket = (int)amountOfBracketInPreRounds + t - 1;

            if (amountOfParticipant == 32)
            {
                return AmountOfBrackets = t-1;
            }
            if (amountOfParticipant == 16)
            {
                return AmountOfBrackets = t-1;
            }
            if (amountOfParticipant == 8)
            {
                return AmountOfBrackets = t-1;
            }
            if (amountOfParticipant == 4)
            {
                return AmountOfBrackets = t-1;
            }
            return AmountOfBrackets = totalbracket;


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