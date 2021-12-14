using System.Collections.Generic;

namespace NSC_TournamentGen.DataAccess.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public List<TournamentEntity> Tournaments { get; set; }
        public List<TournamentUserEntity> TournamentUsers { get; set; }
    }
}