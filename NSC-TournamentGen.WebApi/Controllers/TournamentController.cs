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
                        Participants = tournament.Participants,
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
                Participants = foundTournament.Participants,
                Type = foundTournament.Type
            });
            else return StatusCode(500, "User not found.");
        }
    }
}