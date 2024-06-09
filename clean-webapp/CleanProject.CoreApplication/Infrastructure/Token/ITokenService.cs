using System.Security.Claims;

namespace CleanProject.CoreApplication.Infrastructure.Token;

public interface ITokenService
{
    string GenerateOptOutToken(OptOutParameters parameters);
    OptOutParameters GetOptOutParameters(string token);
    bool VerifyToken(string token);
    string GenerateJwtTokenForClaims(IEnumerable<Claim> claims);
}