using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace NSC_TournamentGen.Core.Models
{
    public class Tournament
    {
        public int Id { get; set; }
        public string Name  { get; set; }
        public TournamentType Type { get; set; }
        public string Participants { get; set; }
    }
}