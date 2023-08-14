using Core.Entities.Concrete;
using Core.Utilities.Security.Encryption;
using Core.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Jwt
{
    public class JwtHelper : ITokenHelper
    {
        public IConfiguration Configuration { get; }
        private TokenOptions _tokenOptions;

        public JwtHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>()!;
        }
        public Tokens CreateToken(User user, List<OperationClaim> operationClaims)
        {
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            DateTime tokenTime = DateTime.UtcNow;
            JwtSecurityToken jwtSecurityToken = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, operationClaims, tokenTime.AddMinutes(_tokenOptions.AccessTokenExpiration), tokenTime);
            JwtSecurityToken jwtRefreshToken = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, operationClaims, tokenTime.AddDays(_tokenOptions.RefreshTokenExpirationDays), tokenTime);
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            string accessToken = handler.WriteToken(jwtSecurityToken);
            string refreshToken = handler.WriteToken(jwtRefreshToken);
            return new Tokens
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public bool ValidateRefreshToken(string refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken)) return false;
            TokenValidationParameters validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _tokenOptions.Issuer,
                ValidAudience = _tokenOptions.Audience,
                IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey),
                ClockSkew = TimeSpan.Zero,
                LifetimeValidator = (DateTime? notBefore, DateTime? expires,
                    SecurityToken securityToken,
                    TokenValidationParameters validationParameters) => expires != null ? expires > DateTime.Now : false,
            };
            var handler = new JwtSecurityTokenHandler();

            try
            {
                SecurityToken validatedToken;
                ClaimsPrincipal claimsPrincipal = handler.ValidateToken(refreshToken, validationParameters, out validatedToken);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user,
            SigningCredentials signingCredentials, List<OperationClaim> operationClaims,
            DateTime expires, DateTime notBefore)
        {
            var jwtSecurityToken = new JwtSecurityToken(
                    issuer: _tokenOptions.Issuer,
                    audience: _tokenOptions.Audience,
                    expires: expires,
                    notBefore: notBefore,
                    claims: SetClaims(user, operationClaims),
                    signingCredentials: signingCredentials
                );
            return jwtSecurityToken;
        }

        private IEnumerable<Claim> SetClaims(User user, List<OperationClaim> operationClaims)
        {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddName($"{user.FirstName} {user.LastName}");
            claims.AddEmail(user.Email);
            claims.AddRoles(operationClaims.Select(c => c.Name).ToArray());
            return claims;
        }
    }
}
