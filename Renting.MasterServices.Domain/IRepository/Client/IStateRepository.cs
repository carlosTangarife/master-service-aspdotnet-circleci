using Renting.MasterServices.Domain.Entities.Client;
using System.Collections.Generic;

namespace Renting.MasterServices.Domain.IRepository.Client
{
    public interface IStateRepository : IERepository<State>
    {
        IList<State> GetStates(string[] parametersStates);
    }
}
