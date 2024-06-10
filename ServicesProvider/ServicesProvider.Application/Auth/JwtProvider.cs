﻿using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ServicesProvider.Domain.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ServicesProvider.Application.Auth
{
    public class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _options;

        public JwtProvider(IOptions<JwtOptions> options)
        {
            _options = options.Value;
        }

        public string GenerateToken(User user)
        {
            {
                Claim[] claims =
                {
                    new(CustomClaims.UserId, user.Id.ToString())
                };

                var signingCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
                    SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    claims: claims,
                    signingCredentials: signingCredentials,
                    expires: DateTime.UtcNow.AddHours(_options.ExpitesHours));

                var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

                return tokenValue;
            }
        }
    }
}
