using System.Collections.Generic;

namespace NSC_TournamentGen.Dtos
{
    public class RoundDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<BracketDto> Brackets { get; set; }
        public int TournamentId { get; set; }
        public TournamentDto Tournament { get; set; }
    }
}