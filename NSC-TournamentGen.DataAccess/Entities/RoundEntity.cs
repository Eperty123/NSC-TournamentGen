using System.Collections.Generic;

namespace NSC_TournamentGen.DataAccess.Entities
{
    public class RoundEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<BracketEntity> Brackets { get; set; }

        public TournamentEntity Tournament { get; set; }
    }
}