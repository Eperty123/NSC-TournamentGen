using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace NSC_TournamentGen.Core.Models
{
    public class Tournament
    {
        public int Id { get; set; }
        public string Name  { get; set; }

        //public List<TournamentUser> TournamentUsers { get; set; }
        public int CurrentRoundId { get; set; }
        public List<Round> Rounds { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}