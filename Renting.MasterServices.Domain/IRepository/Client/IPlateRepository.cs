using Renting.MasterServices.Domain.Entities.Client;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Renting.MasterServices.Domain.IRepository.Client
{
    public interface IPlateRepository: IERepository<Plate>
    {
        Task<IList<Plate>> GetPlatesByClient(int clientId);
        Task UpdatePlateKm(PlateKmRequest plateKmRequest, string userEmail);
    }
}
