using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSC_TournamentGen.Domain.Services;
using NSC_TournamentGen.Dtos;

namespace NSC_TournamentGen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentController : ControllerBase
    {
        private readonly TournamentService _tournamentService;

        public TournamentController(TournamentService tournamentService)
        {
            _tournamentService = tournamentService;
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
                Participants = foundTournament.Participants
            });
            else return StatusCode(500, "User not found.");
        }
    }
}