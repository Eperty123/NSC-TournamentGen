using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSC_TournamentGen.Core.IServices;
using NSC_TournamentGen.Dtos;

namespace NSC_TournamentGen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]

        public ActionResult<UsersDto> ReadAll()
        {
            try
            {
                var users = _userService.GetAllUsers()
                    .Select(user => new UserDto
                    {
                        Id = user.Id,
                        Username = user.Username,
                        Password = user.Password
                    }).ToList();

                return Ok(new UsersDto
                {
                    UserList = users
                });


            }
            catch (Exception)
            {
                return StatusCode(500, "sikke noget lort");
            }
        }

    }
}