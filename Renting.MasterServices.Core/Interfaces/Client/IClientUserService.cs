using Renting.MasterServices.Core.Dtos.Client;
using Renting.MasterServices.Domain.Entities.Client;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Renting.MasterServices.Core.Interfaces.Client
{
    /// <summary>
    /// IClientUserService
    /// </summary>
    /// <seealso cref="Renting.MasterServices.Core.Interfaces.IService{Renting.MasterServices.Domain.Entities.Client.ClientUser, Renting.MasterServices.Core.Dtos.Client.ClientUserDto}" />
    public interface IClientUserService : IService<ClientUser, ClientUserDto>
    {
        /// <summary>
        /// Gets the clients by user identifier asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="isAdmin">if set to <c>true</c> [is admin].</param>
        /// <param name="economicGroupId">The economic group identifier.</param>
        /// <returns></returns>
        Task<IList<ClientUserDto>> GetClientsByUserIdAsync(string userId, bool isAdmin, int economicGroupId);
    }
}
