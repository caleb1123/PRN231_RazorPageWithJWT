using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class JWT
    {
        private readonly string secretKey;
        private readonly string issuer;
        private readonly string audience;

        public JWT(string secretKey, string issuer, string audience)
        {
            this.secretKey = secretKey;
            this.issuer = issuer;
            this.audience = audience;
        }

        public string GenerateToken(string email, int? role)
        {
            string rolename = role switch
            {
                1 => "ADMIN",
                2 => "STAFF",
                3 => "MANAGER",
                4 => "CUSTOMER",
                _ => "GUEST" 
            };

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.UtcNow.AddMinutes(30)).ToUnixTimeSeconds().ToString()),
                new Claim("role", rolename)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
