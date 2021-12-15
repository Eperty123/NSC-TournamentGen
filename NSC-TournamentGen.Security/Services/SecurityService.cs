using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NSC_TournamentGen.Security.IRepositories;
using NSC_TournamentGen.Security.IServices;
using NSC_TournamentGen.Security.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NSC_TournamentGen.Security.Services
{
    public class SecurityService : ISecurityService
    {
        readonly ISecurityRepository _securityRepository;
        readonly IConfiguration _configuration;

        public SecurityService(IConfiguration configuration, ISecurityRepository securityRepository)
        {
            _configuration = configuration;
            _securityRepository = securityRepository;
        }

        public JwtToken GenerateJwtToken(string username, string password)
        {
            var user = GetUser(username);
            if (user != null && Authenticate(user, password))
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtConfig:Secret"]));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                #region Role-based authentication
                var identityOptions = new IdentityOptions();

                // Role based claim. We need the IdentityOptions with claims of
                // a RoleClaimType to add the roles.
                // Needed to make role-based authentication to work.
                // Remember to add AddAuthorization policy in start up file, bro!
                var claims = new List<Claim>
                {
                    new Claim(identityOptions.ClaimsIdentity.RoleClaimType, user.Role),
                };
                #endregion

                var token = new JwtSecurityToken(
                    _configuration["JwtConfig:Issuer"],
                    audience: _configuration["JwtConfig:Audience"],
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: credentials,
                    claims: claims
                );

                var generatedJwt = new JwtSecurityTokenHandler().WriteToken(token);
                return new JwtToken
                {
                    Jwt = generatedJwt,
                    Message = "Token generated.",
                };
            }
            else
            {
                return new JwtToken
                {
                    Message = "Incorrect username or password."
                };
            }
        }

        public AuthUser GetUser(string username)
        {
            return _securityRepository.ReadUser(username);
        }

        bool Authenticate(AuthUser authUser, string plainPassword)
        {
            if (authUser != null)
            {
                var hashedPassword = HashPassword(plainPassword, authUser.Salt);
                return authUser.HashedPassword.Equals(hashedPassword);
            }
            return false;
        }

        public string HashPassword(string plainPassword, byte[] salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: plainPassword,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8
            ));
        }
    }
}
