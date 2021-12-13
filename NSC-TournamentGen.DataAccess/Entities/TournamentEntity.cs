using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NSC_TournamentGen.Core.Models;

namespace NSC_TournamentGen.DataAccess.Entities
{
    public class TournamentEntity
    {
        public int Id { get; set; }
        public string Name  { get; set; }
        public TournamentType Type { get; set; }
        public ParticipantEntity Participants { get; set; }
        public List<TournamentUserEntity> TournamentUsers { get; set; }
        
        public List<RoundEntity> Rounds { get; set; }
        public UserEntity User { get; set; }
    }
}