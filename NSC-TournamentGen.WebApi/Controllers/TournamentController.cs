using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSC_TournamentGen.Core.IServices;
using NSC_TournamentGen.Core.Models;
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
        public ActionResult<TournamentInputDto> CreateTournament([FromBody]TournamentInputDto tournamentInputDto)
        {
            try
            {
                var _tournamentInput = new TournamentInput
                {
                    AmountOfParticipants = tournamentInputDto.AmountOfParticipants,
                    Participants = tournamentInputDto.Participants,
                    Name = tournamentInputDto.Name
                };
                var createdTournament = _tournamentService.CreateTournament(_tournamentInput);
                /*if (createdTournament != null) return Ok (new TournamentInputDto
                {
                    Participants = createdTournament.Participants,
                    AmountOfParticipants = createdTournament.AmountOfParticipants
                });*/
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Failed to create tournament.");
            }

            return BadRequest("Failed to create tournament.");
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
                        Rounds = tournament.Rounds.Select(round => new RoundDto()
                        {
                            Name = round.Name,
                            Id = round.Id,
                            Brackets = round.Brackets.Select(bracket => new BracketDto()
                            {
                                Id = bracket.Id,
                                Participant1 = new ParticipantDto()
                                {
                                    Id = bracket.Participant1Id,
                                    Name = bracket.Participant1.Name
                                },
                                Participant2 = new ParticipantDto()
                                {
                                    Id = bracket.Participant2Id,
                                    Name = bracket.Participant2.Name
                                }
                            }).ToList()
                        }).ToList(),
                        User = new UserDto()
                        {
                            Id = tournament.UserId,
                            Username = tournament.User.Username
                        }
                    });

                return Ok(new TournamentsDto
                {
                    TournamentList = tournaments.ToList()
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

            // Id ok, proceed!
            var foundTournament = _tournamentService.GetTournament(id);
            if (foundTournament != null)
                return Ok(new TournamentDto()
                {
                    Id = foundTournament.Id,
                    Name = foundTournament.Name,
                    Rounds = foundTournament.Rounds.Select(round => new RoundDto()
                    {
                        Name = round.Name,
                        Id = round.Id,
                        Brackets = round.Brackets.Select(bracket => new BracketDto()
                        {
                            Id = bracket.Id,
                            Participant1 = new ParticipantDto()
                            {
                                Id = bracket.Participant1Id,
                                Name = bracket.Participant1.Name
                            },
                            Participant2 = new ParticipantDto()
                            {
                                Id = bracket.Participant2Id,
                                Name = bracket.Participant2.Name
                            }
                        }).ToList()
                    }).ToList(),
                    User = new UserDto()
                    {
                        Id = foundTournament.UserId,
                        Username = foundTournament.User.Username
                    }
                });
            else return StatusCode(500, "User not found.");
        }

        // DELETE api/Tournament/5
        [HttpDelete("{id}")]
        public ActionResult<TournamentDto> Delete(int id)
        {
            try
            {
                if (id < 1) return BadRequest("Id must be greater then 0!");

                // Id ok, proceed!
                var foundTournament = _tournamentService.DeleteTournament(id);
                if (foundTournament != null)
                    return Ok(new TournamentDto()
                    {
                        Id = foundTournament.Id,
                        Name = foundTournament.Name,
                    });
                else return BadRequest("Tournament not found");
            }
            catch (Exception)
            {
                return StatusCode(500, "Failed to delete Tournament");
            }
        }

        // PUT api/Tournament/5 -- Update
        [HttpPut("{id}")]
        public ActionResult<TournamentDto> Put(int id, [FromBody] TournamentDto tournament)
        {
            try
            {
                if (id < 1) return BadRequest("Id must be greater then 0!");

                // Id ok, proceed!

                var foundTournament = _tournamentService.GetTournament(id);
                if (foundTournament != null)
                {
                    // Update the values of the found tournament with the replacement.
                    foundTournament.Name = tournament.Name;
                    
                    // Update the replacement's id with the found one.
                    tournament.Id = foundTournament.Id;

                    // Now update the tournament.
                    var updatedTournament = _tournamentService.UpdateTournament(id, foundTournament);
                    if (updatedTournament != null) return Ok(tournament);
                }
                else return BadRequest("Tournament not found.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Failed to update tournament.");
            }

            return BadRequest("Failed to update tournament.");
        }
    }
}