using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Claims;

namespace Renting.MasterServices.Api.Auth
{

    /// <summary>
    /// AuthPropsExtension
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class AuthPropsExtension
    {
        /// <summary>
        /// In case you need to deserialize a claim to a non simple object
        /// let's say a dictionary or Tuple
        /// </summary>
        /// <typeparam name="T">the generic expected</typeparam>
        /// <param name="self">the claim collection</param>
        /// <param name="claimType">the claim we're looking for</param>
        /// <returns></returns>
        public static T ClaimsToObject<T>(this IEnumerable<Claim> self, string claimType)
        {
            var propClaim = self.FirstOrDefault(c => c.Type.Equals(claimType, StringComparison.OrdinalIgnoreCase));
            if (propClaim != null)
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(propClaim.Value);
            }
            return default(T);
        }

    }
}
