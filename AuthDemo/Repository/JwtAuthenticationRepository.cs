using AuthDemo.Interface;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthDemo.Repository
{
    public class JwtAuthenticationRepository : IJwtAuthenticaionRepository
    {
        private readonly IDictionary<string, string> userCredentital = new Dictionary<string, string>()
        {
            { "test1", "password1"},
            { "test2", "password2"},
        };
        private readonly string key;

        public JwtAuthenticationRepository(string key)
        {
            this.key = key;
        }
        public string Authenticate(string userName, string password)
        {
            if (!userCredentital.Any(item => item.Key == userName && item.Value == password))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {

                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(tokenKey), 
                            SecurityAlgorithms.HmacSha256Signature
                        )
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            
            return tokenHandler.WriteToken(token);
        }
    }
}
