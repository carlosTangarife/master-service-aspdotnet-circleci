using Renting.MasterServices.Core.Dtos.Client;
using Renting.MasterServices.Domain.Entities.Client;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Renting.MasterServices.Core.Interfaces.Client
{
    /// <summary>
    /// IVehicleTypeService 
    /// </summary>
    /// <seealso cref="Renting.MasterServices.Core.Interfaces.IService{Renting.MasterServices.Domain.Entities.Client.VehicleType, Renting.MasterServices.Core.Dtos.Client.VehicleTypeDto}" />
    public interface IVehicleTypeService : IService<VehicleType, VehicleTypeDto>
    {
        /// <summary>
        /// Gets the vehicle types asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IList<VehicleTypeDto>> GetVehicleTypesAsync();
    }
}
