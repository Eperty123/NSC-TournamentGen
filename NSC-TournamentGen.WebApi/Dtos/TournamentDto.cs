using System.Collections.Generic;
using NSC_TournamentGen.Core.Models;

namespace NSC_TournamentGen.Dtos
{
    public class TournamentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<TournamentUserDto> TournamentUsers { get; set; }
        public int CurrentRoundId { get; set; }
        public List<RoundDto> Rounds { get; set; }
        public int UserId { get; set; }
        public UserDto User { get; set; }
    }


}