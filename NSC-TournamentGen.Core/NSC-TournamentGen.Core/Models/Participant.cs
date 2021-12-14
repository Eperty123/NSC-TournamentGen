using System.Collections.Generic;

namespace NSC_TournamentGen.Core.Models
{
    public class Participant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Bracket> BracketsParticipants1 { get; set; }
        public List<Bracket> BracketsParticipants2 { get; set; }
    }
}