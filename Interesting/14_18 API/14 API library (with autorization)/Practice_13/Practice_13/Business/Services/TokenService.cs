using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Practice_13.Business.DTO;
using Practice_13.Business.Services.Interfaces;
using Practice_13.Utils;

namespace Practice_13.Business.Services
{
    public class TokenService : ITokenService
    {
        public string CreateToken(AccountDTO account)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, account.Username),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, account.Role),
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            var jwtRules = new JwtSecurityToken(
                issuer: JwtAuthOptions.ISSUER,
                audience: JwtAuthOptions.AUDIENCE,
                notBefore: DateTime.UtcNow,
                claims: claimsIdentity.Claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromSeconds(JwtAuthOptions.LIFETIME_SEC)),
                signingCredentials: new SigningCredentials(
                    JwtAuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256
                    )
                );

            return new JwtSecurityTokenHandler().WriteToken(jwtRules);
        }
    }
}
