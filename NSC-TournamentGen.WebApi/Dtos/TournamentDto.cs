using NSC_TournamentGen.Core.Models;

namespace NSC_TournamentGen.Dtos
{
    public class TournamentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public TournamentType Type { get; set; }
        public string Participants { get; set; }
    }
}