using System.Collections.Generic;

namespace NSC_TournamentGen.Core.Models
{
    public class Round
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Bracket> Brackets { get; set; }
        public int TournamentId { get; set; }
        public Tournament Tournament { get; set; }
    }
}