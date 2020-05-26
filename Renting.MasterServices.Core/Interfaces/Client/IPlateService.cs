using Renting.MasterServices.Core.Dtos.Client;
using Renting.MasterServices.Domain.Entities.Client;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Renting.MasterServices.Core.Interfaces.Client
{
    /// <summary>
    /// IPlateService
    /// </summary>
    /// <seealso cref="Renting.MasterServices.Core.Interfaces.IService{Renting.MasterServices.Domain.Entities.Client.Plate, Renting.MasterServices.Core.Dtos.Client.PlateDto}" />
    public interface IPlateService: IService<Plate, PlateDto>
    {
        /// <summary>
        /// Gets the plates by client asynchronous.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <returns></returns>
        Task<IList<PlateDto>> GetPlatesByClientAsync(int clientId);

        /// <summary>
        /// Updates the plate km asynchronous.
        /// </summary>
        /// <param name="plateKmRequest">The plate km request.</param>
        /// <param name="userEmail">The user email.</param>
        /// <returns></returns>
        Task UpdatePlateKmAsync(PlateKmRequestDto plateKmRequest, string userEmail);
    }
}
