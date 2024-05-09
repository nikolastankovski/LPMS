using LPMS.Domain.Models.ConfigModels;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LPMS.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly ISystemUserRepository _identityUserRepository;
        private readonly JWTConfig _jwtConfig;
        public TokenService(ISystemUserRepository identityUserRepository, IOptions<JWTConfig> jwtConfig)
        {
            _identityUserRepository = identityUserRepository;
            _jwtConfig = jwtConfig.Value;
        }
        public async Task<string> GenerateTokenAsync(Guid userId)
        {
            var user = await _identityUserRepository.GetUserByIdAsync(userId);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.IssuerSigningKey);

            if (user == null) return string.Empty;

            var roles = await _identityUserRepository.GetUserRolesAsync(user);

            var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Email) };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims.ToArray()),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public Task<string> GenerateTokenAsync(string email)
        {
            throw new NotImplementedException();
        }
    }
}
