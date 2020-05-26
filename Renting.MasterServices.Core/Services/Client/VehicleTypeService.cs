using AutoMapper;
using log4net;
using Renting.MasterServices.Core.Dtos.Client;
using Renting.MasterServices.Core.Interfaces.Client;
using Renting.MasterServices.Domain.Entities.Client;
using Renting.MasterServices.Domain.IRepository.Client;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Renting.MasterServices.Core.Services.Client
{
    /// <summary>
    /// VehicleTypeService
    /// </summary>
    /// <seealso cref="Services.Service{VehicleType, VehicleTypeDto}" />
    /// <seealso cref="IVehicleTypeService" />
    public class VehicleTypeService : Service<VehicleType, VehicleTypeDto>, IVehicleTypeService
    {
        private readonly IVehicleTypeRepository vehicleTypeRepository;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleTypeService"/> class.
        /// </summary>
        /// <param name="vehicleTypeRepository">The vehicle type repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="log">The log.</param>
        public VehicleTypeService(IVehicleTypeRepository vehicleTypeRepository,
            IMapper mapper, ILog log) : base(vehicleTypeRepository, log, mapper)
        {
            this.vehicleTypeRepository = vehicleTypeRepository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Gets the vehicle types asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IList<VehicleTypeDto>> GetVehicleTypesAsync()
        {
            var vehicleTypes = await vehicleTypeRepository.GetVehicleTypes().ConfigureAwait(false);
            return mapper.Map<IList<VehicleTypeDto>>(vehicleTypes);
        }
    }
}
