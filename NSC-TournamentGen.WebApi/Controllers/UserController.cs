using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
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

        // GET api/User/5 -- READ By Id
        [HttpGet("{id}")]
        public ActionResult<UserDto> Get(int id)
        {
            if (id < 1) return BadRequest("Id must be greater then 0!");

            // Id ok, proceed!
            var foundUser = _userService.GetUser(id);
            if (foundUser != null) return Ok(new UserDto
            {
                Id = foundUser.Id,
                Username = foundUser.Username,
                Password = foundUser.Password
            });
            else return StatusCode(500, "User not found.");
        }

        // POST api/User -- CREATE
        [HttpPost]
        public ActionResult<UserDto> Post([FromBody] UserDto user)
        {
            try
            {
                var createdUser = _userService.CreateUser(user.Username, user.Password);
                if (createdUser != null) return Ok(new UserDto
                {
                    Id = createdUser.Id,
                    Username = createdUser.Username,
                    Password = createdUser.Password
                });
            }
            catch (Exception)
            {
                return StatusCode(500, "Failed to create user.");
            }

            return BadRequest("Failed to create user.");
        }

        // PUT api/User/5 -- Update
        [HttpPut("{id}")]
        public ActionResult<UserDto> Put(int id, [FromBody] UserDto user)
        {
            try
            {
                if (id < 1) return BadRequest("Id must be greater then 0!");

                // Id ok, proceed!

                var foundUser = _userService.GetUser(id);
                if (foundUser != null)
                {
                    // Update the values of the found user with the replacement.
                    foundUser.Username = user.Username;
                    foundUser.Password = user.Password;

                    // Update the replacement's id with the found one.
                    user.Id = foundUser.Id;

                    // Now update the user.
                    var updatedUser = _userService.UpdateUser(id, foundUser);
                    if (updatedUser != null) return Ok(user);
                }
                else return BadRequest("User not found.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Failed to update user.");
            }

            return BadRequest("Failed to update user.");
        }

        // DELETE api/User/5
        [HttpDelete("{id}")]
        public ActionResult<UserDto> Delete(int id)
        {
            try
            {
                if (id < 1) return BadRequest("Id must be greater then 0!");

                // Id ok, proceed!
                var foundUser = _userService.DeleteUser(id);
                if (foundUser != null) return Ok(new UserDto
                {
                    Id = foundUser.Id,
                    Username = foundUser.Username,
                    Password = foundUser.Password
                });
                else return BadRequest("User not found.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Failed to delete user.");
            }
        }

    }
}