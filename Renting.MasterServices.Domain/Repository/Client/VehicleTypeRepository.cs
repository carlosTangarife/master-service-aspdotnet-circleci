using Autofac.Features.Indexed;
using Renting.MasterServices.Domain.Entities.Client;
using Renting.MasterServices.Domain.IRepository.Client;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Renting.MasterServices.Infraestructure.Enums;

namespace Renting.MasterServices.Domain.Repository.Client
{
    public class VehicleTypeRepository : ERepository<VehicleType>, IVehicleTypeRepository
    {
        private readonly IIndex<DataBaseConnection, IQueryableUnitOfWork> index;

        public VehicleTypeRepository(IIndex<DataBaseConnection, IQueryableUnitOfWork> index)
            : base(index[DataBaseConnection.GestionFlota]) { this.index = index; }

        public async Task<IList<VehicleType>> GetVehicleTypes()
        {
            return await index[DataBaseConnection.GestionFlota].ExecWithStoreProcedureAsync<VehicleType>($"spWCLSTipoVehiculoXCliente").ConfigureAwait(false);
        }
    }
}
