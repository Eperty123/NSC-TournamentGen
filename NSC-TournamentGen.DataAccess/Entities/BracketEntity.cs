using System.Collections.Generic;

namespace NSC_TournamentGen.DataAccess.Entities
{
    public class BracketEntity
    {
        public int Id { get; set; }
        public List<ParticipantEntity> Participants { get; set; }
        public RoundEntity Round { get; set; }
    }
}