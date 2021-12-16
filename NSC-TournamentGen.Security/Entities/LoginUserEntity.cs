namespace NSC_TournamentGen.Security.Entities
{
    public class LoginUserEntity
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string HashedPassword { get; set; }
        public string Role { get; set; }
        public string Salt { get; set; }
    }
}
