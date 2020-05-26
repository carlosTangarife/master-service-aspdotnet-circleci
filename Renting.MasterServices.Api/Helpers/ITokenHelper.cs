using System.Collections.Generic;
using System.Security.Claims;

namespace Renting.MasterServices.Api.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITokenHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool IsAdmin(IEnumerable<Claim> claims);
        string GetUserId(IEnumerable<Claim> claims);
        string GetUserEmail(IEnumerable<Claim> claims);
    }
}
