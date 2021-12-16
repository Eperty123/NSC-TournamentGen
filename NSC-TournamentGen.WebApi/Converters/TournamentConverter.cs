using NSC_TournamentGen.Core.Models;
using NSC_TournamentGen.DataAccess.Entities;
using NSC_TournamentGen.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace NSC_TournamentGen.Converters
{
    public static class TournamentConverter
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
            return tournamentDto;
        }

        public static Tournament ToModel(this TournamentDto tournamentDto)
        {
            var tournament = new Tournament();
            tournament.Id = tournamentDto.Id;
            tournament.Name = tournamentDto.Name;
            tournament.Rounds = tournamentDto.Rounds.Select(r => new Round()
            {
                Id = r.Id,
                Name = r.Name,
                Brackets = r.Brackets
                .Select(b => new Bracket()
                {
                    Id = b.Id,

                    Participant1 = new Participant()
                    {
                        Id = b.Participant1 != null ? b.Participant1.Id : 0,
                        Name = b.Participant1 != null ? b.Participant1.Name : "",
                    },
                    Participant2 = new Participant()
                    {
                        Id = b.Participant2 != null ? b.Participant2.Id : 0,
                        Name = b.Participant2 != null ? b.Participant2.Name : "",
                    },
                    Participant1Id = b.Participant1Id,
                    Participant2Id = b.Participant2Id,
                    Round = new Round()
                    {
                        Id = r.Id,
                        Name = r.Name,
                    },
                    WinnerId = b.WinnerId,

                }).ToList(),
                Tournament = new Tournament()
                {
                    Id = tournamentDto.Id,
                    Name = tournamentDto.Name,
                }
            }).ToList();
            return tournament;
        }

        public static Tournament ToModel(this TournamentEntity tournamentDto)
        {
            var tournament = new Tournament();
            tournament.Id = tournamentDto.Id;
            tournament.Name = tournamentDto.Name;
            tournament.Rounds = tournamentDto.Rounds.Select(r => new Round()
            {
                Id = r.Id,
                Name = r.Name,
                Brackets = r.Brackets
                .Select(b => new Bracket()
                {
                    Id = b.Id,

                    Participant1 = new Participant()
                    {
                        Id = b.Participant1 != null ? b.Participant1.Id : 0,
                        Name = b.Participant1 != null ? b.Participant1.Name : "",
                    },
                    Participant2 = new Participant()
                    {
                        Id = b.Participant2 != null ? b.Participant2.Id : 0,
                        Name = b.Participant2 != null ? b.Participant2.Name : "",
                    },
                    Participant1Id = b.Participant1Id,
                    Participant2Id = b.Participant2Id,
                    Round = new Round()
                    {
                        Id = r.Id,
                        Name = r.Name,
                    },
                    WinnerId = b.WinnerId,

                }).ToList(),
                Tournament = new Tournament()
                {
                    Id = tournamentDto.Id,
                    Name = tournamentDto.Name,
                }
            }).ToList();
            return tournament;
        }
    }
}
