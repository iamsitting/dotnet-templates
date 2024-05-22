namespace TemplateProject.Infrastructure.Tokens;

public interface ITokenService
{
    string GenerateOptOutToken(OptOutParameters parameters);
    OptOutParameters GetOptOutParameters(string token);
    bool VerifyToken(string token);
}