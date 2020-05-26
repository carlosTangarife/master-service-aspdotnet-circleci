using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace Renting.MasterServices.Api.Auth
{
    /// <summary>
    /// AuthMiddlewareSetupExtension
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class AuthMiddlewareSetupExtension
    {
        /// <summary>
        /// Adds the b2 c authentication.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="JsonConfig">The json configuration.</param>
        public static void AddB2CAuth(this IServiceCollection services, IConfiguration JsonConfig)
        {
            var ValidationKey = new AzureB2CKeyValidation(JsonConfig.GetSection("B2CAuthentication").GetValue<Uri>("KeysUrl"));
            var iskr = ValidationKey.GetKeysAsync().GetAwaiter().GetResult();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true, 
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        RequireSignedTokens = true,
                        RequireExpirationTime = true,
                        RoleClaimType = "rc_role",
                        ValidIssuer = JsonConfig.GetSection("B2CAuthentication").GetValue<string>("TokenIssuer"),
                        ValidAudiences = JsonConfig.GetSection("B2CAuthentication:Audiences").Get<IEnumerable<string>>(),
                        IssuerSigningKeyResolver = (tokenText, securityToken, keyId, parameters) =>
                        {
                           return iskr;
                        }
                    };
                });
        }
    }
}
