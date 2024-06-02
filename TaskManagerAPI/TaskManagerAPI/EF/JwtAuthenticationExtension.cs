namespace TaskManagerAPI.EF;

using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;

using System.Text;

public static class JwtAuthenticationExtension
{
    public static AuthenticationBuilder AddJwtAuthentication(this AuthenticationBuilder authenticationBuilder)
    {
        authenticationBuilder
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;

                var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("a6e0cbea095e2e672c8bbdb266d891c4958237f2fdd3586f6ddd557ac9d45db2"));

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = signingKey,
                    ValidateIssuer = true,
                    ValidIssuer = "TaskManagerIssuer",
                    ValidateAudience = true,
                    ValidAudience = "TaskManagerIssuer",
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

        return authenticationBuilder;
    }
}
