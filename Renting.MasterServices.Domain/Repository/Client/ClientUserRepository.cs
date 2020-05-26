using Autofac.Features.Indexed;
using Renting.MasterServices.Domain.Entities.Client;
using Renting.MasterServices.Domain.IRepository.Client;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Renting.MasterServices.Infraestructure.Enums;

namespace Renting.MasterServices.Domain.Repository.Client
{
    public class ClientUserRepository : ERepository<ClientUser>, IClientUserRepository
    {
        private readonly IIndex<DataBaseConnection, IQueryableUnitOfWork> index;

        public ClientUserRepository(IIndex<DataBaseConnection, IQueryableUnitOfWork> index)
            : base(index[DataBaseConnection.Surenting]) { this.index = index; }

        public async Task<IList<ClientUser>> GetClientsByUserIdAsync(string userId = null, int? economicGroupId = null)
        {
            string query = userId != null ? $"spWCLSConsultarClientes @userId = '{userId}', @idGrupoEconomico = {economicGroupId.Value}" :
                $"spWCLSConsultarClientes @idGrupoEconomico = {economicGroupId.Value}";
            return await index[DataBaseConnection.Surenting].ExecWithStoreProcedureAsync<ClientUser>(query).ConfigureAwait(false);
        }
    }
}
