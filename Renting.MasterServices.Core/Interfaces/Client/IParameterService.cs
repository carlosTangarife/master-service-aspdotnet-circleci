using Renting.MasterServices.Core.Dtos.Client;
using Renting.MasterServices.Domain.Entities.Client;
using System.Threading.Tasks;

namespace Renting.MasterServices.Core.Interfaces.Client
{
    /// <summary>
    /// IParameterService
    /// </summary>
    /// <seealso cref="Renting.MasterServices.Core.Interfaces.IService{Renting.MasterServices.Domain.Entities.Client.Parameter, Renting.MasterServices.Core.Dtos.Client.ParameterDto}" />
    public interface IParameterService : IService<Parameter, ParameterDto>
    {
        /// <summary>
        /// Gets the parameter by name asynchronous.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <returns></returns>
        Task<ParameterDto> GetParameterByNameAsync(string parameterName);
    }
}
