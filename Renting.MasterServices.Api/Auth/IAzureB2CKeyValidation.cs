using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Renting.MasterServices.Api.Auth
{
    /// <summary>
    /// IAzureB2CKeyValidation
    /// </summary>
    public interface IAzureB2CKeyValidation
    {
        /// <summary>
        /// Gets the keys asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<SecurityKey>> GetKeysAsync();

        /// <summary>
        /// Invalidates the keys.
        /// </summary>
        void InvalidateKeys();
    }
}
