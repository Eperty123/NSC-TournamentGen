using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NSC_TournamentGen.Converters;
using NSC_TournamentGen.Core.IServices;
using NSC_TournamentGen.Core.Models;
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

            var tournament = _tournamentService.GetTournament(id);
            if (tournament != null) return Ok(tournament.ToDto());
            return BadRequest("The tournament does not exist.");
        }

    }
}