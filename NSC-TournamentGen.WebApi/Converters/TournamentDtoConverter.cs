using NSC_TournamentGen.Core.Models;
using NSC_TournamentGen.DataAccess.Entities;
using NSC_TournamentGen.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace NSC_TournamentGen.Converters
{
    public static class TournamentDtoConverter
    {
        public static TournamentDto ToDto(this Tournament tournament)
        {
            var tournamentDto = new TournamentDto();
            tournamentDto.Id = tournament.Id;
            tournamentDto.Name = tournament.Name;
            tournamentDto.Rounds = tournament.Rounds.Select(r => new RoundDto()
            {
                Id = r.Id,
                Name = r.Name,
                Brackets = r.Brackets
                .Select(b => new BracketDto()
                {
                    Id = b.Id,
                    IsExecuted = b.IsExecuted,
                    Participant1 = new ParticipantDto()
                    {
                        Id = b.Participant1 != null ? b.Participant1.Id : 0,
                        Name = b.Participant1 != null ? b.Participant1.Name : "",
                    },
                    Participant2 = new ParticipantDto()
                    {
                        Id = b.Participant2 != null ? b.Participant2.Id : 0,
                        Name = b.Participant2 != null ? b.Participant2.Name : "",
                    },
                    Participant1Id = b.Participant1Id,
                    Participant2Id = b.Participant2Id,
                    Round = new RoundDto()
                    {
                        Id = r.Id,
                        Name = r.Name,
                    },
                    WinnerId = b.WinnerId,

                }).ToList(),
                Tournament = new TournamentDto()
                {
                    Id = tournamentDto.Id,
                    Name = tournamentDto.Name,
                    Rounds = tournamentDto.Rounds,
                }
            }).ToList();
            tournamentDto.CurrentRoundId = tournament.CurrentRoundId;
            tournamentDto.UserId = tournament.UserId;
            return tournamentDto;
        }
    }
}
