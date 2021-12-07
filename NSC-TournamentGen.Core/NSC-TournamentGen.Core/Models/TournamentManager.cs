using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace NSC_TournamentGen.Core.Models
{
    public class TournamentManager
    {
        public int _tournamentNumber;
        public int _amountOfBracket;
        public int _amountOfRounds;
        public Dictionary<int, List<Bracket>> _bracketsDictionary;
        
        
        


        public void MakeBracketsForTournament(TournamentInput tournamentInput, List<string> participants)
        {
            CalculateTournamentNumber(tournamentInput.AmountOfParticipants); // need info about Amount of Participant -> tournament input
            CalculateAmountOfRounds(tournamentInput.AmountOfParticipants);  // need info about Amount of Participant -> tournament input + TournamentNumber
            CalculateAmountOfBracket(tournamentInput.AmountOfParticipants); // need info about Amount of Participant -> tournament input + TournamentNumber
            GenerateBrackets(tournamentInput.AmountOfParticipants, participants); // // need info about Amount of Participant -> tournament input + TournamentNumber + amountOfBracket + amountOfRounds;
        }
        
        public void GenerateBrackets(int amountOfParticipants, List<string> participants)
        {
            var rnglist = MakeRandomList(participants);
            var aor = _amountOfRounds;
            var t = _tournamentNumber;
            var counter = 0;
            _bracketsDictionary = new Dictionary<int, List<Bracket>>();
            
            /*
            // tilføjer preBacket til Dictionary.
            if (!NoPreRounds(amountOfParticipants))
            {
                var bracketslist = new List<Bracket>();
                for (int i = 0; i < amountOfParticipants - t; i++)
                {
                    var bracket = new Bracket
                    {
                        Id = i + 1,
                        Participants = new List<string>()
                    };
                    bracket.Participants.Add(rnglist[i+counter]);
                    counter++;
                    bracket.Participants.Add(rnglist[i+counter]);
                    bracketslist.Add(bracket);
                }
                _bracketsDictionary.Add(0, bracketslist);
            }
            // tilføjer alle MainBracket til Dictionary
            */
            
            while (aor > 0)
            {
                var bracketslist = new List<Bracket>();
                if (NoPreRounds(amountOfParticipants))
                {
                    for (int i = 0; i < aor; i++)
                    {
                    
                        var bracket = new Bracket
                        {
                            Id = i + 1,
                            Participants = new List<string>()
                        };
                        bracket.Participants.Add(rnglist[i+counter]);
                        counter++;
                        bracket.Participants.Add(rnglist[i+counter]);
                        bracketslist.Add(bracket);
                    }
                    _bracketsDictionary.Add(t, bracketslist);
                    t = t / 2;
                    aor--;
                }
                
                // hvis der er preBracket så  tilføjer den andeldes.
                else
                {
                    for (int i = 0; i < counter; i++)
                    {
                    
                        var bracket = new Bracket
                        {
                            Id = i + 1,
                            Participants = new List<string>()
                        };
                        bracket.Participants.Add(rnglist[i+counter]);
                        bracketslist.Add(bracket);
                    }

                    var xcounter = counter;
                    for (int i = 0; i < _tournamentNumber/2 -counter; i++)
                    {
                    
                        var bracket = new Bracket
                        {
                            Id = i + 1,
                            Participants = new List<string>()
                        };
                        bracket.Participants.Add(rnglist[i+xcounter]);
                        xcounter++;
                        bracket.Participants.Add(rnglist[i+xcounter]);
                        bracketslist.Add(bracket);
                    }
                    _bracketsDictionary.Add(t, bracketslist);
                    t = t / 2;
                    aor--;
                }

            }

            if (_bracketsDictionary.Count != _amountOfRounds)
            {
                throw new Exception(
                    "Antallet af bracket i dictionaory stemmer ikke over ens med calculateAmountOfBracket ");
            }
        }
        
        
        public List<string> MakeRandomList(List<string> participants)
        {
            
            var random = new Random();
            var testList = participants.OrderBy(item => random.Next());
            
            
            return testList.ToList();
        }
        
        

        public bool NoPreRounds(int amountOfParticipants)
        {
            var t = _tournamentNumber;
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
            var t = _tournamentNumber;
            return _amountOfBracket = (t - 1) + amountOfParticipants - t;
        }

        public int CalculateAmountOfRounds(int amountOfParticipants)
        {
            var t = _tournamentNumber;
            while (t > 1)
            {
                t = t / 2;
                _amountOfRounds++;
            }

            if (!NoPreRounds(amountOfParticipants))
            {
                return _amountOfRounds + 1;
            }


            return _amountOfRounds;
        }


        public int CalculateTournamentNumber(int amountOfParticipants)
        {
            if (amountOfParticipants > 32 || amountOfParticipants < 4)
            {
                throw new Exception("Participant is over 32 or lower then 4.");
            }

            if (amountOfParticipants == 32)
            {
                return _tournamentNumber = 32;
            }

            if (amountOfParticipants > 15)
            {
                return _tournamentNumber = 16;
            }

            if (amountOfParticipants > 7)
            {
                return _tournamentNumber = 8;
            }

            if (amountOfParticipants == 4)
            {
                return _tournamentNumber = 4;
            }
            else
            {
                throw new Exception("noget gik galt ved CalculatTournamentNUmbers");
            }
        }
    }
}