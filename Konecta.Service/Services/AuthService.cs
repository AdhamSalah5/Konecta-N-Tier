using Konecta.Core.Models;
using Konecta.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Konecta.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;

        // Fake users (hardcoded)
        private readonly List<User> _users = new()
        {
            new User { Username = "admin", Password = "admin123", Role = "Admin" },
            new User { Username = "user", Password = "user123", Role = "User" }
        };

        public AuthService(IConfiguration config)
        {
            _config = config;
        }

        public Task<string?> AuthenticateAsync(string username, string password)
        {
            var user = _users.Find(u => u.Username == username && u.Password == password);
            if (user == null) return Task.FromResult<string?>(null);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return Task.FromResult(jwt);
        }
    }
}
