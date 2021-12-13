namespace NSC_TournamentGen.DataAccess.Entities
{
    public class ParticipantEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public BracketEntity Bracket { get; set; }
        
    }
}