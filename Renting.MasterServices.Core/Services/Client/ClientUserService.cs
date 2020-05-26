using AutoMapper;
using log4net;
using Renting.MasterServices.Core.Dtos.Client;
using Renting.MasterServices.Core.Interfaces;
using Renting.MasterServices.Core.Interfaces.Client;
using Renting.MasterServices.Domain.Entities.Client;
using Renting.MasterServices.Domain.IRepository.Client;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Renting.MasterServices.Infraestructure;

namespace Renting.MasterServices.Core.Services.Client
{
    /// <summary>
    /// ClientUserService
    /// </summary>
    /// <seealso cref="Services.Service{ClientUser, ClientUserDto}" />
    /// <seealso cref="IClientUserService" />
    public class ClientUserService : Service<ClientUser, ClientUserDto>, IClientUserService
    {
        private readonly IClientUserRepository clientUserRepository;
        private readonly IMapper mapper;
        private readonly ILog log;
        private readonly ICacheService cache;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientUserService"/> class.
        /// </summary>
        /// <param name="clientUserRepository">The client user repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="log">The log.</param>
        /// <param name="cache">The cache.</param>
        public ClientUserService(IClientUserRepository clientUserRepository,
            IMapper mapper, ILog log, ICacheService cache) : base(clientUserRepository, log, mapper)
        {
            this.clientUserRepository = clientUserRepository;
            this.mapper = mapper;
            this.log = log;
            this.cache = cache;
        }

        /// <summary>
        /// Gets the clients by user identifier asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="isAdmin">if set to <c>true</c> [is admin].</param>
        /// <param name="economicGroupId">The economic group identifier.</param>
        /// <returns></returns>
        public async Task<IList<ClientUserDto>> GetClientsByUserIdAsync(string userId, bool isAdmin, int economicGroupId)
        {
            log.Debug("INICIO BUSCAR DATOS EN BD - CLIENTES");
            IList<ClientUser> clientsUsers = isAdmin ?
                await clientUserRepository.GetClientsByUserIdAsync(economicGroupId: economicGroupId).ConfigureAwait(false) :
                await clientUserRepository.GetClientsByUserIdAsync(userId, economicGroupId).ConfigureAwait(false);
            log.Debug("FIN BUSCAR DATOS EN BD - CLIENTES");
            log.Debug("INICIO BUSCAR DATOS EN CACHE - CLIENTES");
            if (clientsUsers.Any())
            {
                var firstClientId = clientsUsers.OrderBy(x => x.ClientName).FirstOrDefault().Id;
                log.Debug("INICIO BUSCAR DATOS EN CACHE - CLIENTES");
                var clientId = cache.Find($"{Constant.CLIENT_ID_CACHE_NAME}{userId}", () => firstClientId);
                log.Debug("FIN BUSCAR DATOS EN CACHE - CLIENTES");
                var existClient = clientsUsers.Any(t => t.Id == clientId);
                if (!existClient)
                {
                    clientId = firstClientId;
                    cache.Set($"{Constant.CLIENT_ID_CACHE_NAME}{userId}", clientId);
                }

                foreach (var clientsUser in clientsUsers.Where(clientsUser => clientsUser.Id == clientId))
                {
                    clientsUser.Selected = true;
                }

                log.Debug("INICIO SET DE DATOS EN CACHE - CLIENTES");
                cache.Set($"{Constant.ECONMIC_GROUP_ID_CACHE_NAME}{userId}", economicGroupId);
                log.Debug("FIN SET DE DATOS EN CACHE - CLIENTES");
            }

            return mapper.Map<IList<ClientUserDto>>(clientsUsers.OrderBy(x => x.ClientName));
        }
    }
}
