using Autofac.Features.Indexed;
using Renting.MasterServices.Domain.Entities.Client;
using Renting.MasterServices.Domain.IRepository.Client;
using System.Collections.Generic;
using System.Linq;
using static Renting.MasterServices.Infraestructure.Enums;

namespace Renting.MasterServices.Domain.Repository.Client
{
    /// <summary>
    /// StateRepository
    /// </summary>
    /// <seealso cref="Renting.MasterServices.Domain.Repository.ERepository{Renting.MasterServices.Domain.Entities.Client.State}" />
    /// <seealso cref="Renting.MasterServices.Domain.IRepository.Client.IStateRepository" />
    public class StateRepository : ERepository<State>, IStateRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StateRepository"/> class.
        /// </summary>
        /// <param name="index">The index.</param>
        public StateRepository(IIndex<DataBaseConnection, IQueryableUnitOfWork> index)
            : base(index[DataBaseConnection.Surenting]) {
        }

        /// <summary>
        /// Gets the states.
        /// </summary>
        /// <param name="parametersStates">The parameters states.</param>
        /// <returns></returns>
        public IList<State> GetStates(string[] parametersStates)
        {
            var states = GetAll(filter: t => parametersStates.Contains(t.Id.ToString()))
                .GroupBy(t => new {t.StateName, t.GroupBy })
                .Select(s => s.FirstOrDefault())
                .ToList();
            return states;
        }
    }
}
