using Autofac.Features.Indexed;
using Renting.MasterServices.Domain.Entities.Provider;
using Renting.MasterServices.Domain.IRepository.Provider;
using static Renting.MasterServices.Infraestructure.Enums;

namespace Renting.MasterServices.Domain.Repository.Provider
{
    public class UserProviderRepository : ERepository<UserProvider>, IUserProviderRepository
    {
        private readonly IIndex<DataBaseConnection, IQueryableUnitOfWork> index;

        public UserProviderRepository(IIndex<DataBaseConnection, IQueryableUnitOfWork> index)
            : base(index[DataBaseConnection.WebProd]) { this.index = index; }
    }
}
