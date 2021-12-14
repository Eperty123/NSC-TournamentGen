using System.Collections.Generic;

namespace NSC_TournamentGen.Dtos
{
    public class ParticipantDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<BracketDto> BracketsParticipants1 { get; set; }
        public List<BracketDto> BracketsParticipants2 { get; set; }
    }
}