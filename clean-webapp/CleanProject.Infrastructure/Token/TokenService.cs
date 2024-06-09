using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CleanProject.CoreApplication.Infrastructure.Token;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CleanProject.Infrastructure.Token;

internal class TokenService : ITokenService
{
    private readonly SymmetricSecurityKey _signingKey;
    private readonly string _issuer;
    private readonly string _audience;
    public TokenService(IOptions<TokenOptions> options)
    {
        _issuer = options.Value.Issuer;
        _audience = options.Value.Audience;
        _signingKey =  new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.SecretKey));
    }
    public string GenerateOptOutToken(OptOutParameters parameters)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var claims = new ClaimsIdentity(new []
        {
            new Claim("expirationDate", parameters.ExpirationDate.Ticks.ToString()),
            new Claim("userId", parameters.UserId.ToString()),
        });
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = claims,
            Expires = parameters.ExpirationDate,
            Issuer = _issuer,
            Audience = _audience,
            SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public OptOutParameters GetOptOutParameters(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = _signingKey,
            ValidateIssuerSigningKey = true,
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidAudience = _audience,
            ValidIssuer = _issuer
        };
        
        var claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidationParameters, out _);

        var value = claimsPrincipal.FindFirst("expirationDate")?.Value;
        var s = claimsPrincipal.FindFirst("userId")?.Value;
        
        if (value == null || s == null) throw new Exception("Token is invalid");
        
        var tokenParameters = new OptOutParameters(new DateTime(long.Parse(value)), int.Parse(s));

        return tokenParameters;
    }

    public bool VerifyToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            var validationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = _signingKey,
                ValidateIssuerSigningKey = true,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidAudience = _audience,
                ValidIssuer = _issuer
            };

            tokenHandler.ValidateToken(token, validationParameters, out _);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex}");
            return false;
        }
    }
    
    public string GenerateJwtTokenForClaims(IEnumerable<Claim> claims)
    {
        var credentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(24),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}