using Autofac.Features.Indexed;
using Renting.MasterServices.Domain.Entities.Provider;
using Renting.MasterServices.Domain.IRepository.Provider;
using static Renting.MasterServices.Infraestructure.Enums;

namespace Renting.MasterServices.Domain.Repository.Provider
{
    public class AnnouncementRepository : ERepository<Announcement>, IAnnouncementRepository
    {

        public AnnouncementRepository(IIndex<DataBaseConnection, IQueryableUnitOfWork> index)
            : base(index[DataBaseConnection.GestionFlotaTrans]) {
        }
    }
}
