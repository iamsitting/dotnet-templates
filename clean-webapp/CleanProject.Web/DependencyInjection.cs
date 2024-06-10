using System.Text;
using CleanProject.Persistence.EF;
using CleanProject.Persistence.EF.Entities.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace CleanProject.Web;

public static class DependencyInjection
{
    public static void AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentity<AppUser, AppRole>()
            .AddEntityFrameworkStores<CleanProjectContext>()
            .AddDefaultTokenProviders();
        var settings = configuration.GetRequiredSection(Infrastructure.Token.TokenOptions.Key).Get<Infrastructure.Token.TokenOptions>();

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
            });
        
        services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/login-path";
            options.LogoutPath = "/logout-path";
        });
    }
}