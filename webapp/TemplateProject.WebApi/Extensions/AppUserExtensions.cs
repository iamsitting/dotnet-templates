using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TemplateProject.Entities.Identity;

namespace TemplateProject.WebApi.Extensions;

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