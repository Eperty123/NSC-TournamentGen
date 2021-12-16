using System.Collections.Generic;

namespace NSC_TournamentGen.DataAccess.Entities
{
    public class TournamentEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<TournamentUserEntity> TournamentUsers { get; set; }

        public List<RoundEntity> Rounds { get; set; }
        public int UserId { get; set; }
        public UserEntity User { get; set; }

    }
}