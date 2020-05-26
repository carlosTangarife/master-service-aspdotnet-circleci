using Autofac.Features.Indexed;
using Renting.MasterServices.Domain.IRepository.Provider;
using static Renting.MasterServices.Infraestructure.Enums;

namespace Renting.MasterServices.Domain.Repository.Provider
{
    public class ProviderRepository : ERepository<Entities.Provider.Provider>, IProviderRepository
    {
        private readonly IIndex<DataBaseConnection, IQueryableUnitOfWork> index;

        public ProviderRepository(IIndex<DataBaseConnection, IQueryableUnitOfWork> index)
            : base(index[DataBaseConnection.Surenting]) { this.index = index; }
    }
}
