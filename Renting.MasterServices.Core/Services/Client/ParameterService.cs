using AutoMapper;
using log4net;
using Renting.MasterServices.Core.Dtos.Client;
using Renting.MasterServices.Core.Interfaces.Client;
using Renting.MasterServices.Domain.Entities.Client;
using Renting.MasterServices.Domain.IRepository.Client;
using Renting.MasterServices.Infraestructure;
using System.Threading.Tasks;

namespace Renting.MasterServices.Core.Services.Client
{
    /// <summary>
    /// ParameterService
    /// </summary>
    /// <seealso cref="Services.Service{Parameter, ParameterDto}" />
    /// <seealso cref="IParameterService" />
    public class ParameterService : Service<Parameter, ParameterDto>, IParameterService
    {
        private readonly IParameterRepository parameterRepository;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterService"/> class.
        /// </summary>
        /// <param name="parameterRepository">The parameter repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="log">The log.</param>
        public ParameterService(IParameterRepository parameterRepository,
            IMapper mapper, ILog log) : base(parameterRepository, log, mapper)
        {
            this.parameterRepository = parameterRepository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Gets the parameter by name asynchronous.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <returns></returns>
        public async Task<ParameterDto> GetParameterByNameAsync(string parameterName)
        {
            var parameters = await parameterRepository.GetParameterByName(parameterName, Constant.PARAMETER_TYPE).ConfigureAwait(false);
            return mapper.Map<ParameterDto>(parameters);
        }
    }
}
