using AutoMapper;
using log4net;
using Renting.MasterServices.Core.Dtos.Provider;
using Renting.MasterServices.Core.Interfaces;
using Renting.MasterServices.Core.Interfaces.Provider;
using Renting.MasterServices.Domain.IRepository.Provider;
using Renting.MasterServices.Infraestructure;
using System.Collections.Generic;
using System.Linq;

namespace Renting.MasterServices.Core.Services.Provider
{
    /// <summary>
    /// 
    /// </summary>
    public class ProviderService : Service<Domain.Entities.Provider.Provider, ProviderDto>, IProviderService
    {
        private readonly IProviderRepository providerRepository;
        private readonly IMapper mapper;
        private readonly ICacheService cache;
        private readonly IUserProviderService userProviderService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserProviderService"/> class.
        /// </summary>
        /// <param name="providerRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="log"></param>
        /// <param name="cache"></param>
        public ProviderService(IProviderRepository providerRepository, IUserProviderService userProviderService,
        IMapper mapper, ILog log, ICacheService cache) : base(providerRepository, log, mapper)
        {
            this.providerRepository = providerRepository;
            this.mapper = mapper;
            this.cache = cache;
            this.userProviderService = userProviderService;
        }

        /// <summary>
        /// Obtiene una lista de proveedores por el email
        /// </summary>
        /// <param name="emailUser">email del usuario</param>
        /// <returns></returns>
        public IList<ProviderDto> GetByEmailUser(string emailUser)
        {
            var userProviders = userProviderService.GetAll(t => t.EmailUser == emailUser);
            var providerIds = userProviders.Select(userProv => userProv.ProviderId);
            var providers = GetAll(filter: provider => providerIds.Contains(provider.Id), orderBy: prov => prov.OrderBy(t => t.ProviderName));
            return providers.ToList();
        }

        /// <summary>
        /// Obtiene una lista de proveedores por el identificador del usuario
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<ProviderDto> GetByUserId(string userId)
        {
            var userProviders = userProviderService.GetAll(t => t.Id == userId);
            var providerIds = userProviders.Select(userProv => userProv.ProviderId);
            var providers = GetAll(filter: provider => providerIds.Contains(provider.Id), orderBy: prov => prov.OrderBy(t => t.ProviderName));
            return providers.ToList();
        }

        /// <summary>
        /// Obtiene una lista de proveedores, filtrado por el identificador del usuario estraido del token 
        /// y setea en estado seleccionado el proveedor que coincida con el ultimo proovedor que este en la cache relacionado al usuario logueado
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<ProviderDto> GetFromCache(string userId)
        {
            IList<ProviderDto> providers = GetByUserId(userId);

            if (providers.Any())
            {
                var firstSupplierId = providers.FirstOrDefault().Id;
                var supplierId = cache.Find($"{Constant.SUPPLIER_ID}{userId}", () => firstSupplierId);
                var existsSupplier = providers.Any(provider => provider.Id == supplierId);

                if (!existsSupplier)
                {
                    cache.Set($"{Constant.SUPPLIER_ID}{userId}", firstSupplierId);
                    supplierId = firstSupplierId;
                }

                foreach (var provider in providers.Where(provider => provider.Id == supplierId))
                {
                    provider.Selected = true;
                }
            }

            return mapper.Map<IList<ProviderDto>>(providers);
        }
    }
}
