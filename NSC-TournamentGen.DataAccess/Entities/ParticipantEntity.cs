using System.Collections.Generic;

namespace NSC_TournamentGen.DataAccess.Entities
{
    public class ParticipantEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<BracketEntity> BracketsParticipants1 { get; set; }
        public List<BracketEntity> BracketsParticipants2 { get; set; }
        
        
    }
}