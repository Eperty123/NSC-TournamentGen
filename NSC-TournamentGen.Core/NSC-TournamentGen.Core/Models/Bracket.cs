using System.Collections.Generic;

namespace NSC_TournamentGen.Core.Models
{
    public class Bracket
    {
        public int Id { get; set; }
        // public List<ParticipantEntity> Participants { get; set; }
        public int  Participant1Id { get; set; }
        public Participant  Participant1 { get; set; }
        public int  Participant2Id { get; set; }
        public Participant  Participant2 { get; set; }
        public int RoundId { get; set; }
        public Round Round { get; set; }
        public int WinnerId { get; set; }
        public bool IsExecuted { get; set; }
    }
    
    
}