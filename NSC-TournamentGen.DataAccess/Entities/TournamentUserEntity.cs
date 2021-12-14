namespace NSC_TournamentGen.DataAccess.Entities
{
    public class TournamentUserEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TournamentId { get; set; }

        public TournamentEntity Tournament { get; set; }
        public UserEntity User { get; set; }
        
        
    }
}