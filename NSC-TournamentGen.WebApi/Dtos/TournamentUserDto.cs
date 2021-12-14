namespace NSC_TournamentGen.Dtos
{
    public class TournamentUserDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TournamentId { get; set; }

        public TournamentDto Tournament { get; set; }
        public UserDto User { get; set; }

    }
}