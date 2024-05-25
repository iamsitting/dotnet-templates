using System.Configuration;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using TemplateProject.Database;
using TemplateProject.Entities.Identity;
using TokenOptions = TemplateProject.Infrastructure.Tokens.TokenOptions;

namespace TemplateProject.WebApi;

public static class DependencyInjection
{
    public static void AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentity<AppUser, AppRole>()
            .AddEntityFrameworkStores<TemplateProjectContext>()
            .AddDefaultTokenProviders();
        var settings = configuration.GetRequiredSection(TokenOptions.Key).Get<TokenOptions>();

        if (settings == null) throw new NullReferenceException("TokenOptions not defined");

        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.SecretKey));

        // Configure multiple authentication schemes
        services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = settings.Issuer,
                    ValidAudience = settings.Audience,
                    IssuerSigningKey = signingKey
                };
            })
            .AddCookie(IdentityConstants.ApplicationScheme, options =>
            {
                // Cookie authentication configuration
                options.LoginPath = "/login-path";
                options.LogoutPath = "/logout-path";
            });
    }
}