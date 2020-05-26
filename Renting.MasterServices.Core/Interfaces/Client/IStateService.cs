using Renting.MasterServices.Core.Dtos.Client;
using Renting.MasterServices.Domain.Entities.Client;
using System.Collections.Generic;

namespace Renting.MasterServices.Core.Interfaces.Client
{
    /// <summary>
    /// IStateService
    /// </summary>
    /// <seealso cref="Renting.MasterServices.Core.Interfaces.IService{Renting.MasterServices.Domain.Entities.Client.State, Renting.MasterServices.Core.Dtos.Client.StateDto}" />
    public interface IStateService : IService<State, StateDto>
    {
        /// <summary>
        /// Gets the states.
        /// </summary>
        /// <param name="parametersStates">The parameters states.</param>
        /// <returns></returns>
        IList<StateDto> GetStates(string[] parametersStates);
    }
}
