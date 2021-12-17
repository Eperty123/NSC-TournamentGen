using NSC_TournamentGen.Core.Models;
using NSC_TournamentGen.DataAccess.Entities;
using System.Linq;

namespace NSC_TournamentGen.DataAccess
{
    public static class TournamentEntityConverter
    {
        public static Tournament ToModel(this TournamentEntity tournamentEntity)
        {
            var tournament = new Tournament();
            tournament.Id = tournamentEntity.Id;
            tournament.Name = tournamentEntity.Name;
            tournament.Rounds = tournamentEntity.Rounds.Select(r => new Round()
            {
                Id = r.Id,
                Name = r.Name,
                Brackets = r.Brackets
                .Select(b => new Bracket()
                {
                    Id = b.Id,
                    IsExecuted = b.IsExecuted,
                    Participant1 = new Participant()
                    {
                        Id = b.Participant1 != null ? b.Participant1.Id : 0,
                        Name = b.Participant1 != null ? b.Participant1.Name : null,
                    },
                    Participant2 = new Participant()
                    {
                        Id = b.Participant2 != null ? b.Participant2.Id : 0,
                        Name = b.Participant2 != null ? b.Participant2.Name : null,
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
                    Id = tournamentEntity.Id,
                    Name = tournamentEntity.Name,
                }
            }).ToList();
            tournament.CurrentRoundId = tournamentEntity.CurrentRoundId;
            tournament.UserId = tournamentEntity.UserId;
            return tournament;
        }
        public static TournamentEntity ToEntity(this Tournament tournament)
        {
            var tournamentEntity = new TournamentEntity();
            tournamentEntity.Id = tournament.Id;
            tournamentEntity.Name = tournament.Name;
            tournamentEntity.Rounds = tournament.Rounds.Select(r => new RoundEntity()
            {
                Id = r.Id,
                Name = r.Name,
                Brackets = r.Brackets
                .Select(b => new BracketEntity()
                {
                    Id = b.Id,
                    IsExecuted = b.IsExecuted,
                    Participant1 = new ParticipantEntity()
                    {
                        Id = b.Participant1 != null ? b.Participant1.Id : 0,
                        Name = b.Participant1 != null ? b.Participant1.Name : null,
                    },
                    Participant2 = new ParticipantEntity()
                    {
                        Id = b.Participant2 != null ? b.Participant2.Id : 0,
                        Name = b.Participant2 != null ? b.Participant2.Name : null,
                    },
                    Participant1Id = b.Participant1Id,
                    Participant2Id = b.Participant2Id,
                    Round = new RoundEntity()
                    {
                        Id = r.Id,
                        Name = r.Name,
                    },
                    WinnerId = b.WinnerId,

                }).ToList(),
                Tournament = new TournamentEntity()
                {
                    Id = tournament.Id,
                    Name = tournament.Name,
                }
            }).ToList();
            tournamentEntity.CurrentRoundId = tournament.CurrentRoundId;
            tournamentEntity.UserId = tournament.UserId;
            return tournamentEntity;
        }
    }
}
