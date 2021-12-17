using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSC_TournamentGen.Converters;
using NSC_TournamentGen.Core.IServices;
using NSC_TournamentGen.Core.Models;
using NSC_TournamentGen.DataAccess.Repositories;
using NSC_TournamentGen.Domain.IRepositories;
using NSC_TournamentGen.Domain.Services;
using NSC_TournamentGen.Dtos;

namespace NSC_TournamentGen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentController : ControllerBase
    {
        private readonly ITournamentService _tournamentService;

        public TournamentController(ITournamentService tournamentService)
        {
            _tournamentService = tournamentService;
        }


        [HttpPost]
        public ActionResult<TournamentDto> Create([FromBody] TournamentInput tournamentInput)
        {
            var generatedTournament = _tournamentService.CreateTournament(tournamentInput);
            if (generatedTournament != null) return Ok(generatedTournament.ToDto());
            return BadRequest("Failed to create tournament! Please try again.");
        }

        [HttpPut("winner/{id}")]
        public ActionResult<TournamentDto> MakeWinner([FromBody] WinnerDto winnerDto)
        {
            var foundTournament = _tournamentService.GetTournament(winnerDto.TournamentId);
            if (foundTournament != null)
            {
                return Ok(_tournamentService.MakeWinner(winnerDto.TournamentId, winnerDto.RoundId, winnerDto.BracketId, winnerDto.ParticipantId).ToDto());
            }
            return BadRequest("Failed to update tournament! Please try again.");
        }

        [HttpGet]
        public ActionResult<TournamentsDto> ReadAll()
        {
            try
            {
                var tournaments = _tournamentService.GetAllTournaments()
                    .Select(tournament => new TournamentDto
                    {
                        Id = tournament.Id,
                        Name = tournament.Name,
                    }).ToList();

                return Ok(new TournamentsDto
                {
                    TournamentList = tournaments,
                });
            }
            catch (Exception)
            {
                return StatusCode(500, "sikke noget lort");
            }
        }

        // GET api/Tournament/5 -- READ By Id
        [HttpGet("{id}")]
        public ActionResult<TournamentDto> Get(int id)
        {
            if (id < 1) return BadRequest("Id must be greater then 0!");

            //var manager = new TournamentManager(_tournamentService);
            //var input = new TournamentInput()
            //{
            //    Name = "Test",
            //    Participants = "Lugh\nTarte\nRuti\nRed\nHaruka\nSora\nKunai\nLuna\nAku\nQueen",
            //};
            //var generatedTournament = manager.MakeTournament(input.Participants.Split('\n').ToList());

            //return Ok(generatedTournament.ToDto());

            var tournament = _tournamentService.GetTournament(id);
            if (tournament != null)
                return Ok(tournament.ToDto());
            return BadRequest("The tournament does not exist.");

            // Id ok, proceed!
            //return Ok(new TournamentDto
            //{
            //    Id = 1,
            //    Name = "Hentai Tournament",
            //    Rounds = new List<RoundDto>()
            //      {
            //          new RoundDto
            //          {
            //               Id = 1,
            //                Name = "Pre-round 0",
            //                 Brackets = new List<BracketDto>()
            //                 {
            //                     new BracketDto
            //                     {
            //                          Id = 1,
            //                           Participant1 = new ParticipantDto
            //                           {
            //                               Id = 1,
            //                                Name = "Red",

            //                           },
            //                           Participant2 = new ParticipantDto
            //                           {
            //                               Id = 2,
            //                               Name = "Ruti"
            //                           }
            //                     },
            //                          new BracketDto
            //                     {
            //                          Id = 1,
            //                           Participant1 = new ParticipantDto
            //                           {
            //                               Id = 1,
            //                                Name = "Lough",

            //                           },
            //                           Participant2 = new ParticipantDto
            //                           {
            //                               Id = 2,
            //                               Name = "Tarte"
            //                           }
            //                     }
            //                 },
            //          }
            //          ,
            //          new RoundDto
            //          {
            //               Id = 1,
            //                Name = "Round 1",
            //                 Brackets = new List<BracketDto>()
            //                 {
            //                     new BracketDto
            //                     {
            //                          Id = 1,
            //                           Participant1 = new ParticipantDto
            //                           {
            //                               Id = 1,
            //                                Name = "Haruka",
            //                           },
            //                           Participant2 = new ParticipantDto
            //                           {
            //                               Id = 2,
            //                               Name = "Sora",
            //                           }
            //                     },
            //                          new BracketDto
            //                     {
            //                          Id = 1,
            //                           Participant1 = new ParticipantDto
            //                           {
            //                               Id = 1,
            //                                Name = "Lough",

            //                           },
            //                           Participant2 = new ParticipantDto
            //                           {
            //                               Id = 2,
            //                               Name = "Tarte"
            //                           }
            //                     },
            //                          new BracketDto
            //                     {
            //                          Id = 1,
            //                           Participant1 = new ParticipantDto
            //                           {
            //                               Id = 1,
            //                                Name = "Placeholder 1",

            //                           },
            //                           Participant2 = new ParticipantDto
            //                           {
            //                               Id = 2,
            //                               Name = "Placeholder 2"
            //                           }
            //                     },
            //                          new BracketDto
            //                     {
            //                          Id = 1,
            //                           Participant1 = new ParticipantDto
            //                           {
            //                               Id = 1,
            //                                Name = "Placeholder 3",

            //                           },
            //                           Participant2 = new ParticipantDto
            //                           {
            //                               Id = 2,
            //                               Name = "Placeholder 4"
            //                           }
            //                     }
            //                 },
            //          },
            //          new RoundDto
            //          {
            //               Id = 1,
            //                Name = "Round 2 (Semi final)",
            //                 Brackets = new List<BracketDto>()
            //                 {
            //                     new BracketDto
            //                     {
            //                          Id = 1,
            //                           Participant1 = new ParticipantDto
            //                           {
            //                               Id = 1,
            //                                Name = "Haruka",
            //                           },
            //                           Participant2 = new ParticipantDto
            //                           {
            //                               Id = 2,
            //                               Name = "Sora",
            //                           }
            //                     },
            //                          new BracketDto
            //                     {
            //                          Id = 1,
            //                           Participant1 = new ParticipantDto
            //                           {
            //                               Id = 1,
            //                                Name = "Lough",

            //                           },
            //                           Participant2 = new ParticipantDto
            //                           {
            //                               Id = 2,
            //                               Name = "Tarte"
            //                           }
            //                     },
            //                 },
            //          },
            //          new RoundDto
            //          {
            //               Id = 1,
            //                Name = "Round 3 (Final)",
            //                 Brackets = new List<BracketDto>()
            //                 {
            //                     new BracketDto
            //                     {
            //                          Id = 1,
            //                               Participant1 = new ParticipantDto
            //                           {
            //                               Id = 1,
            //                                Name = "Haruka",
            //                           }
            //                     },
            //                 },
            //          }
            //      },
            //    User = new UserDto
            //    {
            //        Id = 1,
            //        Username = "Erika",
            //        Password = "213",
            //    },
            //});
            //var foundTournament = _tournamentService.GetTournament(id);
            //if (foundTournament != null) return Ok(new TournamentDto()
            //{
            //    Id = foundTournament.Id,
            //    Name = foundTournament.Name,
            //    Rounds = 


            //});
            //else return StatusCode(500, "User not found.");
        }

    }
}