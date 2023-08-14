using Core.Entities.Concrete;
using System.Security.Claims;

namespace Core.Utilities.Security.Jwt
{
    public interface ITokenHelper
    {
        Tokens CreateToken(User user, List<OperationClaim> operationClaims);
        bool ValidateRefreshToken(string refreshToken);
        ClaimsPrincipal? GetClaimsFromExpiredToken(string expiredAccessToken);
    }
}
