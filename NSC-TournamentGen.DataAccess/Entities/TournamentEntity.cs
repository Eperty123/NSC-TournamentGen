using NSC_TournamentGen.Core.Models;

namespace NSC_TournamentGen.DataAccess.Entities
{
    public class TournamentEntity
    {
        public int Id { get; set; }
        public string Name  { get; set; }
        public TournamentType Type { get; set; }
        public string Participants { get; set; }
    }
}