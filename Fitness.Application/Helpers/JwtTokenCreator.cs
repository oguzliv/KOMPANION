using Fitness.Domain.Entites;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Fitness.Application.Abstractions.Response;
using Fitness.Application.Models.UserModels.UserResponses;

namespace Fitness.Application.Helpers
{
    public class JwtTokenCreator
    {
        public Response TokenCreator(User user, string secret)
        {
            List<Claim> claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Username),
                new(JwtRegisteredClaimNames.Sid, user.Id.ToString()),
                new(ClaimTypes.Email, user.Email),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secret));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(30), signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            var response = new LoginResponse
            {
                Token = jwt,
                Data = user,
                IsSuccess = true
            };

            return response;
        }
    }
}