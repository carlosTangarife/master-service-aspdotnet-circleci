using Renting.MasterServices.Domain.Entities.Client;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Renting.MasterServices.Domain.IRepository.Client
{
    public interface IEconomicGroupRepository : IERepository<EconomicGroup>
    {
        Task<IList<EconomicGroup>> GetEconomicsGroupAsync(string userId = null);
    }
}
