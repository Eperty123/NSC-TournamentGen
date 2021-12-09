using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NSC_TournamentGen.Security.IServices;
using NSC_TournamentGen.Security.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace NSC_TournamentGen.Security.Services
{
    public class SecurityService : ISecurityService
    {
        IConfiguration _configuration { get; set; }
        readonly AuthDbContext _ctx;

        public SecurityService(IConfiguration configuration, AuthDbContext authDbContext)
        {
            _configuration = configuration;
            _ctx = authDbContext;
        }

        public JwtToken GenerateJwtToken(string username, string password)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtConfig:Secret"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["JwtConfig:Issuer"],
                audience: _configuration["JwtConfig:Audience"],
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
            );

            var generatedJwt = new JwtSecurityTokenHandler().WriteToken(token);
            return new JwtToken
            {
                Jwt = generatedJwt,
                Message = "Token generated.",
            };
        }
    }
}
