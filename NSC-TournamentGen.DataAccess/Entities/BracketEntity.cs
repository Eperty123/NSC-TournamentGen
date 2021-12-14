using System.Collections.Generic;

namespace NSC_TournamentGen.DataAccess.Entities
{
    public class BracketEntity
    {
        public int Id { get; set; }
       // public List<ParticipantEntity> Participants { get; set; }
        public int  Participant1Id { get; set; }
        public ParticipantEntity  Participant1 { get; set; }
        public int  Participant2Id { get; set; }
        public ParticipantEntity  Participant2 { get; set; }
        public int RoundId { get; set; }
        public RoundEntity Round { get; set; }
        public int WinnerId { get; set; }
    }
}