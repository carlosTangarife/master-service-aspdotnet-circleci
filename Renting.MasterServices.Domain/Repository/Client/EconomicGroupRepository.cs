using Autofac.Features.Indexed;
using Renting.MasterServices.Domain.Entities.Client;
using Renting.MasterServices.Domain.IRepository.Client;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Renting.MasterServices.Infraestructure.Enums;

namespace Renting.MasterServices.Domain.Repository.Client
{
    public class EconomicGroupRepository : ERepository<EconomicGroup>, IEconomicGroupRepository
    {
        private readonly IIndex<DataBaseConnection, IQueryableUnitOfWork> index;

        public EconomicGroupRepository(IIndex<DataBaseConnection, IQueryableUnitOfWork> index)
            : base(index[DataBaseConnection.Surenting]) { this.index = index; }

        public async Task<IList<EconomicGroup>> GetEconomicsGroupAsync(string userId = null)
        {
            string query = userId != null ? $"spWCLSGruposEconomicos @userId = '{userId}'" : "spWCLSGruposEconomicos";
            return await index[DataBaseConnection.Surenting].ExecWithStoreProcedureAsync<EconomicGroup>(query).ConfigureAwait(false);
        }
    }
}
