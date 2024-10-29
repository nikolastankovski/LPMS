using LPMS.Domain.Models.ConfigModels;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using LPMS.Domain.Models.RnRModels.AuthModels;

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

        public async Task<Result<AuthTokenResponse>> GetAuthTokenAsync(LoginRequest request, CultureInfo culture)
        {
            try
            {
                SystemUser? sysUser = await _systemUserRepository.GetUserByEmailAsync(request.Email);

                if (sysUser == null)
                    return Result.Fail(culture.GetResource(nameof(Resources.User_Doesnt_Exist)));

                if (!sysUser.EmailConfirmed)
                    return Result.Fail(culture.GetResource(nameof(Resources.Email_Not_Verified)));

                if (sysUser.LockoutEnabled && (sysUser.LockoutEnd.HasValue && sysUser.LockoutEnd.Value.UtcDateTime > DateTime.UtcNow))
                    return Result.Fail(culture.GetResource(nameof(Resources.Account_Is_Blocked)));

                bool areCredentialsCorrect = await _systemUserRepository.IsCorrectPasswordAsync(sysUser, request.Password);

                if (!areCredentialsCorrect)
                    return Result.Fail(culture.GetResource(nameof(Resources.Incorrect_credentials)));

                TokenModel? accessToken = await GenerateAccessToken(sysUser);
                TokenModel? refreshToken = await GenerateRefreshToken(sysUser);

                return Result.Ok(new AuthTokenResponse()
                {
                    AccessToken = accessToken.Token,
                    Expires = accessToken.ExpiresUTC,
                    RefreshToken = refreshToken.Token
                });
            }
            catch (Exception e)
            {
                Log.Error(exception: e, messageTemplate: e.ToMessageTemplate());

                return Result.Fail(culture.GetResource(nameof(Resources.Unexpected_Error)));
            }
        }

        public async Task<Result<AuthTokenResponse>> RefreshAuthTokenAsync(RefreshTokenRequest request, CultureInfo culture)
        {
            try
            {
                ClaimsPrincipal? expiredTokenPrincipal = GetPrincipalFromExpiredToken(request.AuthToken);

                if(expiredTokenPrincipal?.Identity?.Name is null)
                    return Result.Fail(culture.GetResource(nameof(Resources.Token_Not_Valid)));

                SystemUser? sysUser = await _systemUserRepository.GetUserByEmailAsync(expiredTokenPrincipal.Identity.Name);

                if(sysUser is null)
                    return Result.Fail(culture.GetResource(nameof(Resources.User_Doesnt_Exist)));

                if(DateTime.UtcNow > sysUser.RefreshTokenExpiresUTC || sysUser.RefreshToken != request.RefreshToken)
                    return Result.Fail(culture.GetResource(nameof(Resources.Token_Not_Valid)));

                if (!sysUser.EmailConfirmed)
                    return Result.Fail(culture.GetResource(nameof(Resources.Email_Not_Verified)));

                if (sysUser.LockoutEnabled && (sysUser.LockoutEnd.HasValue && DateTime.UtcNow > sysUser.LockoutEnd.Value.UtcDateTime))
                    return Result.Fail(culture.GetResource(nameof(Resources.Account_Is_Blocked)));

                TokenModel? accessToken = await GenerateAccessToken(sysUser);
                TokenModel? refreshToken = await GenerateRefreshToken(sysUser);

                return Result.Ok(new AuthTokenResponse()
                {
                    AccessToken = accessToken.Token,
                    Expires = accessToken.ExpiresUTC,
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

            var expiresUTC = DateTime.UtcNow.AddMinutes(_jwtConfig.ExpirationTimeInMin);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims.ToArray()),
                Expires = expiresUTC,
                Issuer = _jwtConfig.ValidIssuer,
                IssuedAt = DateTime.Now,
                Audience = _jwtConfig.ValidAudience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtConfig.IssuerSigningKey)), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));

            return new TokenModel() { Token = token, ExpiresUTC = expiresUTC };
        }

        private async Task<TokenModel> GenerateRefreshToken(SystemUser sysUser)
        {
            var randomNumber = new byte[64];
            using var generator = RandomNumberGenerator.Create();
            generator.GetBytes(randomNumber);

            var refreshToken = new TokenModel()
            {
                Token = Convert.ToBase64String(randomNumber),
                ExpiresUTC = DateTime.UtcNow.AddHours(_jwtConfig.RefreshTokenExpirationTimeInHours),
            };

            sysUser.RefreshToken = refreshToken.Token;
            sysUser.RefreshTokenExpiresUTC = refreshToken.ExpiresUTC;

            await _systemUserRepository.UpdateAsync(sysUser);

            return refreshToken;
        }

        private async Task<List<Claim>> GetUserClaimsAsync(SystemUser user)
        {
            var claims = new List<Claim>
            { 
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Email, user.Email)
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

        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string expiredToken)
        {
            var tokenParameters = new TokenValidationParameters
            {
                ValidIssuer = _jwtConfig.ValidIssuer,
                ValidAudience = _jwtConfig.ValidAudience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtConfig.IssuerSigningKey)),
                ValidateLifetime = false
            };

            var principal = new JwtSecurityTokenHandler().ValidateToken(token: expiredToken, validationParameters: tokenParameters, out SecurityToken securityToken);

            return principal;
        }
    }
}
