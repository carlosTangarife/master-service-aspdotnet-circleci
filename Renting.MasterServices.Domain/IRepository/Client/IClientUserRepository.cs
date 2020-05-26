using Renting.MasterServices.Domain.Entities.Client;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Renting.MasterServices.Domain.IRepository.Client
{
    public interface IClientUserRepository : IERepository<ClientUser>
    {
        Task<IList<ClientUser>> GetClientsByUserIdAsync(string userId = null, int? economicGroupId = null);
    }
}
