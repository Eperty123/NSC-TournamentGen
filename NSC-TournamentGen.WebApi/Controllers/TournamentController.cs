using System;
using System.Collections.Generic;
using System.Linq;
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
                        Type = tournament.Type,
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

            // Id ok, proceed!
            var foundTournament = _tournamentService.GetTournament(id);
            if (foundTournament != null) return Ok(new TournamentDto()
            {
                Id = foundTournament.Id,
                Name = foundTournament.Name,
                Rounds = foundTournament.
                
                
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
                if (foundTournament != null) return Ok(new TournamentDto()
                {
                    Id = foundTournament.Id,
                    Name = foundTournament.Name,
                    Participants = foundTournament.Participants,
                    Type = foundTournament.Type
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
                    foundTournament.Participants = tournament.Participants;
                    foundTournament.Type = tournament.Type;

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