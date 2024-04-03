using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LyncasSales.Token
{
    public class TokenService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly string _secret;

        public TokenService(UserManager<IdentityUser> userManager, TokenValidationParameters tokenValidationParameters, string secret)
        {
            _userManager = userManager;
            _tokenValidationParameters = tokenValidationParameters;
            _secret = secret;
        }

        public async Task<object> GenerateTokensAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            var token = GenerateJwtToken(user);
            var refreshToken = GenerateRefreshToken();

            return new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                refreshToken = refreshToken
            };
        }

        private JwtSecurityToken GenerateJwtToken(IdentityUser user)
        {
            var key = Encoding.ASCII.GetBytes(_secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Email, user.Email)
                }),
                Expires = DateTime.UtcNow.AddHours(24),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            return (JwtSecurityToken)tokenHandler.CreateToken(tokenDescriptor);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

    }
}
