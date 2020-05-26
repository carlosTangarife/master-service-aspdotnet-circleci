using Microsoft.AspNetCore.Http;
using Renting.MasterServices.Core.Dtos.Provider;
using Renting.MasterServices.Domain.Entities.Provider;
using System.Threading.Tasks;

namespace Renting.MasterServices.Core.Interfaces.Provider
{
    public interface IAnnouncementService : IService<Announcement, AnnouncementDto>
    {
        Task CreateAnnouncementAsync(AnnouncementDto announcement, IFormFile file);
        Task UpdateAnnouncementAsync(AnnouncementDto announcement, IFormFile file);
        Task UpdateAnnouncementStateAsync(int? id, bool state);
        Task DeleteAnnouncementAsync(int? id);
    }
}
