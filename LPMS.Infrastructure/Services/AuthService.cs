using FluentResults;
using LPMS.Domain.Models.ConfigModels;
using LPMS.Domain.Models.RnRModels.AuthModels;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LPMS.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly ISystemUserRepository _identityUserRepository;
        private readonly JWTConfig _jwtConfig;
        public AuthService(ISystemUserRepository identityUserRepository, IOptions<JWTConfig> jwtConfig)
        {
            _identityUserRepository = identityUserRepository;
            _jwtConfig = jwtConfig.Value;
        }
        public async Task<Result<AuthTokenResponse>> GenerateAuthTokenAsync(Guid userId, CultureInfo culture)
        {
            try
            {
                var sysUser = await _identityUserRepository.GetUserByIdAsync(userId);

                if (sysUser == null)
                    return Result.Fail(culture.GetResource(nameof(Resources.User_not_found)));

                var tokenHandler = new JwtSecurityTokenHandler();
                var signingKey = Encoding.ASCII.GetBytes(_jwtConfig.IssuerSigningKey);

                var claims = await GetUserClaimsAsync(sysUser);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims.ToArray()),
                    Expires = DateTime.Now.AddMinutes(_jwtConfig.ExpirationTimeInMin),
                    Issuer = _jwtConfig.ValidIssuer,
                    IssuedAt = DateTime.Now,
                    Audience = _jwtConfig.ValidAudience,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(signingKey), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));

                return Result.Ok(new AuthTokenResponse()
                {
                    Token = token,
                    Expires = tokenDescriptor.Expires,
                });
            }
            catch (Exception e)
            {
                Log.Error(exception: e, messageTemplate: $"Exception occured: {e.Message}");

                return Result.Fail(culture.GetResource(nameof(Resources.Unexpected_Error)));
            }
        }

        private async Task<List<Claim>> GetUserClaimsAsync(SystemUser user)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Email) };
            claims = await AddRoleClaimsAsync(user, claims);
            claims = await AddDepartmentClaimsAsync(user, claims);

            return claims;
        }

        private async Task<List<Claim>> AddRoleClaimsAsync(SystemUser user, List<Claim> claims)
        {
            var userRoles = await _identityUserRepository.GetUserRolesAsync(user);

            if(userRoles == null || !userRoles.Any())
                return claims;

            foreach (var r in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, r.Name));
            }

            return claims;
        }

        private async Task<List<Claim>> AddDepartmentClaimsAsync(SystemUser user, List<Claim> claims)
        {
            var userDepartments = new List<string>() { Departments.Finance, Departments.IT };

            foreach (var d in userDepartments)
            {
                claims.Add(new Claim("Department", d));
            }

            return claims;
        }
    }
}
