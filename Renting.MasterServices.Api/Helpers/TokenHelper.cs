using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Renting.MasterServices.Api.Helpers
{
    /// <summary>
    /// TokenHelper
    /// </summary>
    public class TokenHelper: ITokenHelper
    {
        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        /// <param name="claims">The claims.</param>
        /// <returns></returns>
        public string GetUserId(IEnumerable<Claim> claims)
        {
            return claims.FirstOrDefault(x => x.Type.Contains("sub", StringComparison.OrdinalIgnoreCase)).Value;
        }

        /// <summary>
        /// Gets the user email.
        /// </summary>
        /// <param name="claims">The claims.</param>
        /// <returns></returns>
        public string GetUserEmail(IEnumerable<Claim> claims)
        {
            return claims.FirstOrDefault(x => x.Type.Contains("email", StringComparison.OrdinalIgnoreCase)).Value;
        }

        /// <summary>
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        public bool IsAdmin(IEnumerable<Claim> claims)
        {
            var roles = claims.FirstOrDefault(x => x.Type.Contains("rc_role", StringComparison.OrdinalIgnoreCase)).Value?.Split(',');
            return roles.Any() && roles.Contains("WEBPRODUCTIVIDAD.Admin");
        }

    }
}
