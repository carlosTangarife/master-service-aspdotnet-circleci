using Autofac.Features.Indexed;
using Renting.MasterServices.Domain.Entities.Client;
using Renting.MasterServices.Domain.IRepository.Client;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Renting.MasterServices.Infraestructure.Enums;

namespace Renting.MasterServices.Domain.Repository.Client
{
    public class PlateRepository : ERepository<Plate>, IPlateRepository
    {
        private readonly IIndex<DataBaseConnection, IQueryableUnitOfWork> index;

        public PlateRepository(IIndex<DataBaseConnection, IQueryableUnitOfWork> index)
            : base(index[DataBaseConnection.GestionFlota]) { this.index = index; }

        public async Task<IList<Plate>> GetPlatesByClient(int clientId)
        {
            return await index[DataBaseConnection.GestionFlota].ExecWithStoreProcedureAsync<Plate>($"spWPGetPlacas @piIDCliente = {clientId}").ConfigureAwait(false);
        }

        public async Task UpdatePlateKm(PlateKmRequest plateKmRequest, string userEmail)
        {
            await index[DataBaseConnection.SurentingTrans].ExecWithStoreProcedureAsync<Plate>(
            $"spEstadosKMFiltros @pstrPlaca = '{plateKmRequest.PlateCode}' , @pintUltContador = {plateKmRequest.LastCounter}, @pRecorridoCentury = {plateKmRequest.RouteCentury}, " +
            $"@PidTipoContador = {plateKmRequest.CounterType}, @pStrUsuarioIngreso = '{userEmail}', @pdtmFechaContador = '{plateKmRequest.CounterDate.ToString("MM/dd/yyyy")}', " +
            $"@pidAdministradorFlota = '', @pbitKmValido = 1, @strFuente = 'spWPRegistrarContadores'").ConfigureAwait(false);
        }
    }
}
