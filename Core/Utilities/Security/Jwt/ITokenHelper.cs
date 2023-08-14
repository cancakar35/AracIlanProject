using Core.Entities.Concrete;

namespace Core.Utilities.Security.Jwt
{
    public interface ITokenHelper
    {
        Tokens CreateToken(User user, List<OperationClaim> operationClaims);
        bool ValidateRefreshToken(string refreshToken);
    }
}
