using System.Security.Claims;

namespace TemplateProject.Infrastructure.Tokens;

public interface ITokenService
{
    string GenerateOptOutToken(OptOutParameters parameters);
    OptOutParameters GetOptOutParameters(string token);
    bool VerifyToken(string token);
    string GenerateJwtTokenForClaims(IEnumerable<Claim> claims);
}