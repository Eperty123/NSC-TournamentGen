using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSC_TournamentGen.Security.IServices;

namespace NSC_TournamentGen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly ISecurityService _securityService;

        public AuthController(ISecurityService securityService)
        {
            _securityService = securityService;
        }

        [AllowAnonymous]
        [HttpPost(nameof(Login))]
        public ActionResult<TokenDto> Login(LoginDto loginDto)
        {
            var token = _securityService.GenerateJwtToken(loginDto.Username, loginDto.Password);

            return new TokenDto
            {
                Jwt = token.Jwt,
                Message = token.Message,
            };
        }
    }
}
