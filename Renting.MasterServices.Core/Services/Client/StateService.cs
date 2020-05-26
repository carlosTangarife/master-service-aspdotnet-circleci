using AutoMapper;
using log4net;
using Renting.MasterServices.Core.Dtos.Client;
using Renting.MasterServices.Core.Interfaces.Client;
using Renting.MasterServices.Domain.Entities.Client;
using Renting.MasterServices.Domain.IRepository.Client;
using System.Collections.Generic;

namespace Renting.MasterServices.Core.Services.Client
{
    /// <summary>
    /// StateService
    /// </summary>
    /// <seealso cref="Services.Service{State, StateDto}" />
    /// <seealso cref="IStateService" />
    public class StateService : Service<State, StateDto>, IStateService
    {
        private readonly IStateRepository stateRepository;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="StateService"/> class.
        /// </summary>
        /// <param name="stateRepository">The state repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="log">The log.</param>
        public StateService(IStateRepository stateRepository,
            IMapper mapper, ILog log) : base(stateRepository, log, mapper)
        {
            this.stateRepository = stateRepository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Gets the states.
        /// </summary>
        /// <param name="parametersStates">The parameters states.</param>
        /// <returns></returns>
        public IList<StateDto> GetStates(string[] parametersStates)
        {
            var states = stateRepository.GetStates(parametersStates);
            return mapper.Map<IList<StateDto>>(states);
        }
    }
}
