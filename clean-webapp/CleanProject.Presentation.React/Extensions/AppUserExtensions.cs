using System.Security.Claims;
using CleanProject.Persistence.EF.Entities.Identity;
using Microsoft.IdentityModel.JsonWebTokens;

namespace CleanProject.Presentation.React.Extensions;

public static class AppUserExtensions
{
    public static Claim[] GetJwtClaims(this AppUser user)
    {
        Claim[] claims = [];
        claims = [..claims, new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())];
        claims = [..claims, new Claim(JwtRegisteredClaimNames.Email, user.Email!)];
        return claims;
    }
}