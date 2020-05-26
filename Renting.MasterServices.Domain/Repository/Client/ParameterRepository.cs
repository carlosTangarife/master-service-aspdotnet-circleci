using Autofac.Features.Indexed;
using Renting.MasterServices.Domain.Entities.Client;
using Renting.MasterServices.Domain.IRepository.Client;
using System.Linq;
using System.Threading.Tasks;
using static Renting.MasterServices.Infraestructure.Enums;

namespace Renting.MasterServices.Domain.Repository.Client
{
    public class ParameterRepository : ERepository<Parameter>, IParameterRepository
    {

        public ParameterRepository(IIndex<DataBaseConnection, IQueryableUnitOfWork> index)
            : base(index[DataBaseConnection.GestionFlota]) {
        }

        public async Task<Parameter> GetParameterByName(string parameterName, string parameterType)
        {
            var parameter = await GetAllAsync(p => p.Name == parameterName && p.Type == parameterType).ConfigureAwait(false);
            return parameter.FirstOrDefault();
        }
    }
}
