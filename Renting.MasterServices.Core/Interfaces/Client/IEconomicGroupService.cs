using Renting.MasterServices.Core.Dtos.Client;
using Renting.MasterServices.Domain.Entities.Client;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Renting.MasterServices.Core.Interfaces.Client
{
    /// <summary>
    /// IEconomicGroupService
    /// </summary>
    /// <seealso cref="Renting.MasterServices.Core.Interfaces.IService{Renting.MasterServices.Domain.Entities.Client.EconomicGroup, Renting.MasterServices.Core.Dtos.Client.EconomicGroupDto}" />
    public interface IEconomicGroupService : IService<EconomicGroup, EconomicGroupDto>
    {
        /// <summary>
        /// Gets the economics group asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="isAdmin">if set to <c>true</c> [is admin].</param>
        /// <returns></returns>
        Task<IList<EconomicGroupDto>> GetEconomicsGroupAsync(string userId, bool isAdmin);
    }
}
