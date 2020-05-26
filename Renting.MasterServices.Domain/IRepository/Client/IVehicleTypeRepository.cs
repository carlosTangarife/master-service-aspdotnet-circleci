using Renting.MasterServices.Domain.Entities.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Renting.MasterServices.Domain.IRepository.Client
{
    public interface IVehicleTypeRepository : IERepository<VehicleType>
    {
        Task<IList<VehicleType>> GetVehicleTypes();
    }
}
