using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace NSC_TournamentGen.Core.Models
{
    public class TournamentManager
    {
        static int tournamentNumber;
        static int amountOfBracket;
        private static int amountOfRounds;
        public Dictionary<int, List<Bracket>> BracketsDictionary;
        
        

        public void GenerateBrackets(TournamentInput tournamentInput)
        {
            
            // tilføjer preBacket til Dictionary.
            if (tournamentNumber - tournamentInput.AmountOfParticipants != 0)
            {
                var bracketslist = new List<Bracket>();
                    var t = tournamentNumber;
                    for (int i = 0; i < tournamentInput.AmountOfParticipants-tournamentNumber; i++)
                    {
                        var bracket = new Bracket
                        {
                            Id = i + 1,
                            Participants = new List<string>()
                        };
                        bracketslist.Add(bracket);
                    }
                    BracketsDictionary.Add(0,bracketslist);
                    
            }
            // tilføjer alle MainBracket til Dictionary
            while (tournamentNumber > 0)
            {
                var bracketslist = new List<Bracket>();
                var t = tournamentNumber;
                for (int i = 0; i < tournamentNumber; i++)
                {
                    var bracket = new Bracket
                    {
                        Id = i + 1,
                        Participants = new List<string>()
                    };
                    bracketslist.Add(bracket);
                }
                BracketsDictionary.Add(t,bracketslist);
                tournamentNumber = tournamentNumber / 2;
            }

            if (BracketsDictionary.Count == amountOfBracket)
            {
                throw new Exception(
                    "Antallet af bracket i dictionaory stemmer ikke over ens med calculateAmountOfBracket ");
            }
            
        }

        public int CalculateAmountOfBracket(TournamentInput tournamentInput)
        {
           return amountOfBracket = (tournamentNumber * 2 - 1) + tournamentInput.AmountOfParticipants - tournamentNumber;
           
        }

        public int CalculateAmountOfRounds(TournamentInput tournamentInput)
        {
            while (tournamentNumber > 0)
            {
                    tournamentNumber = tournamentNumber / 2;
                    amountOfRounds++;
            }

            if (tournamentNumber - tournamentInput.AmountOfParticipants != 0)
            {
                return amountOfRounds +1;
            }
            
            
            return amountOfRounds;
            
        }
        
        

        public int CalculateTournamentNumber(TournamentInput tournamentInput){

            if (tournamentInput.AmountOfParticipants > 32 || tournamentInput.AmountOfParticipants < 4)
            {
                throw new Exception("Participant is over 32 or lower then 4.");
            }
            
            if (tournamentInput.AmountOfParticipants == 32)
            {
                return tournamentNumber = 16;
            }

            if (tournamentInput.AmountOfParticipants < 32 && tournamentInput.AmountOfParticipants > 15)
            {
                return tournamentNumber = 8; }
            if (tournamentInput.AmountOfParticipants < 16 && tournamentInput.AmountOfParticipants > 7)
            {
                return tournamentNumber = 4; }
            if (tournamentInput.AmountOfParticipants < 8 && tournamentInput.AmountOfParticipants > 3)
            {
                return tournamentNumber = 2; }
            else
            {
                throw new Exception("noget gik galt ved CalculatTournamentNUmbers");
            }
            
        }
        
    }
    
}