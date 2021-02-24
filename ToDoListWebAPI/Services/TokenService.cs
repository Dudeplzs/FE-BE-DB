using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ToDoListWebAPI.Models;
using Microsoft.IdentityModel.Tokens;

namespace ToDoListWebAPI.Services
{
    public class TokenService
    {
        public static string GenerateToken(AppUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email.ToString()),
                    new Claim(ClaimTypes.Name, user.Username.ToString()),
                    new Claim(ClaimTypes.Role, user.Roles.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = 
                new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
