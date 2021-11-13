using Application._Common.Options;
using Application._Common.Services;
using Domain.Entities.Accounts.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class JwtTokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly AuthOptions _authOptions;

        public JwtTokenService(IConfiguration configuration, IOptions<AuthOptions> options)
        {
            _configuration = configuration;
            _authOptions = options.Value;
        }

        public async Task<string> GetTokenForAccount(Account account)
        {
            var now = DateTime.UtcNow;
            var claims = GetIdentity(account);

            var jwt = new JwtSecurityToken(
                    issuer: _authOptions.Issuer,
                    audience: _authOptions.Audience,
                    notBefore: now,
                    claims: claims.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(_authOptions.Lifetime)),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authOptions.IssuerKey)), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        private ClaimsIdentity GetIdentity(Account account)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, account.UserName),
                new Claim("Id", account.Id.ToString())
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims);
            
            return claimsIdentity;
        }
    }
}
