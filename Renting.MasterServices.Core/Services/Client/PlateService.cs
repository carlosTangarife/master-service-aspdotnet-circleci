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
    /// PlateService
    /// </summary>
    /// <seealso cref="Services.Service{Plate, PlateDto}" />
    /// <seealso cref="IPlateService" />
    public class PlateService : Service<Plate, PlateDto>, IPlateService
    {
        private readonly IPlateRepository plateByClientRepository;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlateService"/> class.
        /// </summary>
        /// <param name="plateByClientRepository">The plate by client repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="log">The log.</param>
        public PlateService(IPlateRepository plateByClientRepository,
            IMapper mapper, ILog log) : base(plateByClientRepository, log, mapper)
        {
            this.plateByClientRepository = plateByClientRepository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Gets the plates by client asynchronous.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <returns></returns>
        public async Task<IList<PlateDto>> GetPlatesByClientAsync(int clientId)
        {
            var plates = await plateByClientRepository.GetPlatesByClient(clientId).ConfigureAwait(false);
            return mapper.Map<IList<PlateDto>>(plates);
        }

        /// <summary>
        /// Updates the plate km asynchronous.
        /// </summary>
        /// <param name="plateKmRequest">The plate km request.</param>
        /// <param name="userEmail">The user email.</param>
        public async Task UpdatePlateKmAsync(PlateKmRequestDto plateKmRequest, string userEmail)
        {
            var _plateKmRequest = mapper.Map<PlateKmRequest>(plateKmRequest);
            await plateByClientRepository.UpdatePlateKm(_plateKmRequest, userEmail).ConfigureAwait(false);
        }
    }
}
