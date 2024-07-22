using FluentResults;
using LPMS.Domain.Models.ConfigModels;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace LPMS.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly ISystemUserRepository _systemUserRepository;
        private readonly JWTConfig _jwtConfig;

        public AuthService(ISystemUserRepository systemUserRepository, IOptions<JWTConfig> jwtConfig)
        {
            _systemUserRepository = systemUserRepository;
            _jwtConfig = jwtConfig.Value;
        }

        public async Task<Result<AuthenticationTokenResponse>> GetAuthenticationToken(AuthenticationTokenRequest request, CultureInfo culture)
        {
            try
            {
                var sysUser = await _systemUserRepository.GetUserByEmailAsync(request.Email);

                if (sysUser == null)
                    return Result.Fail(culture.GetResource(nameof(Resources.User_not_found)));

                var areCredentialsCorrect = await _systemUserRepository.IsCorrectPasswordAsync(sysUser, request.Password);

                if (!areCredentialsCorrect)
                    return Result.Fail(culture.GetResource(nameof(Resources.Incorrect_credentials)));

                var accessToken = await GenerateAccessToken(sysUser);
                var refreshToken = await GenerateRefreshToken(sysUser);

                return Result.Ok(new AuthenticationTokenResponse()
                {
                    AccessToken = accessToken.Token,
                    Expires = accessToken.Expires,
                    RefreshToken = refreshToken.Token

                });
            }
            catch (Exception e)
            {
                Log.Error(exception: e, messageTemplate: e.ToMessageTemplate());

                return Result.Fail(culture.GetResource(nameof(Resources.Unexpected_Error)));
            }
        }

        private async Task<TokenModel> GenerateAccessToken(SystemUser sysUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var claims = await GetUserClaimsAsync(sysUser);

            var expires = DateTime.Now.AddMinutes(_jwtConfig.ExpirationTimeInMin);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims.ToArray()),
                Expires = expires,
                Issuer = _jwtConfig.ValidIssuer,
                IssuedAt = DateTime.Now,
                Audience = _jwtConfig.ValidAudience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtConfig.IssuerSigningKey)), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));

            return new TokenModel() { Token = token, Expires = expires };
        }

        private async Task<TokenModel> GenerateRefreshToken(SystemUser sysUser)
        {
            var randomNumber = new byte[64];
            using var generator = RandomNumberGenerator.Create();
            generator.GetBytes(randomNumber);

            var refreshToken = new TokenModel()
            {
                Token = Convert.ToBase64String(randomNumber),
                Expires = DateTime.Now.AddHours(_jwtConfig.RefreshTokenExpirationTimeInHours),
            };

            sysUser.RefreshToken = refreshToken.Token;
            sysUser.RefreshTokenExpires = refreshToken.Expires;

            await _systemUserRepository.UpdateAsync(sysUser);

            return refreshToken;
        }

        private async Task<List<Claim>> GetUserClaimsAsync(SystemUser user)
        {
            var claims = new List<Claim>
            { 
                new Claim(ClaimTypes.Name, user.Email),
            };
            claims = await AddRoleClaimsAsync(user, claims);
            //claims = await AddDepartmentAndDivisionClaimsAsync(user, claims);

            return claims;
        }

        private async Task<List<Claim>> AddRoleClaimsAsync(SystemUser user, List<Claim> claims)
        {
            var userRoles = await _systemUserRepository.GetUserRolesAsync(user);

            if (userRoles == null || !userRoles.Any())
                return claims;

            foreach (var role in userRoles)
            {
                if (!string.IsNullOrEmpty(role.Name))
                    claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            return claims;
        }

        /*private async Task<List<Claim>> AddDepartmentAndDivisionClaimsAsync(SystemUser user, List<Claim> claims)
        {
            var userDepartmentAndDivisions = await _accxDeptxDivRepository.GetBySystemUserIdAsync(user.Id);

            List<string> departmentCodes = userDepartmentAndDivisions
                                                .Where(x => x.Department != null)
                                                .Select(x => x.Department.Code)
                                                .Distinct()
                                                .ToList();

            foreach (var code in departmentCodes)
                claims.Add(new Claim(CustomClaims.Department, code));

            List<string> divisionCodes = userDepartmentAndDivisions
                                                .Where(x => x.Division != null)
                                                .Select(x => x.Division.Code)
                                                .Distinct()
                                                .ToList();

            foreach (var code in divisionCodes)
                claims.Add(new Claim(CustomClaims.Division, code));

            return claims;
        }*/

        public async Task<bool> ValidateExpiredToken(string expiredToken)
        {
            var tokenParameters = new TokenValidationParameters
            {
                ValidIssuer = _jwtConfig.ValidIssuer,
                ValidAudience = _jwtConfig.ValidAudience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtConfig.IssuerSigningKey)),
                ValidateLifetime = false
            };

            var validate = await new JwtSecurityTokenHandler().ValidateTokenAsync(token: expiredToken, validationParameters: tokenParameters);

            return validate.IsValid;
        }
    }
}
