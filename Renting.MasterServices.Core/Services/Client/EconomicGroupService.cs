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
    /// EconomicGroupService
    /// </summary>
    /// <seealso cref="Services.Service{EconomicGroup, EconomicGroupDto}" />
    /// <seealso cref="IEconomicGroupService" />
    public class EconomicGroupService : Service<EconomicGroup, EconomicGroupDto>, IEconomicGroupService
    {
        private readonly IEconomicGroupRepository economicGroupRepository;
        private readonly IMapper mapper;
        private readonly ILog log;
        private readonly ICacheService cache;

        /// <summary>
        /// Initializes a new instance of the <see cref="EconomicGroupService"/> class.
        /// </summary>
        /// <param name="economicGroupRepository">The economic group repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="log">The log.</param>
        /// <param name="cache">The cache.</param>
        public EconomicGroupService(IEconomicGroupRepository economicGroupRepository,
            IMapper mapper, ILog log, ICacheService cache) : base(economicGroupRepository, log, mapper)
        {
            this.economicGroupRepository = economicGroupRepository;
            this.mapper = mapper;
            this.log = log;
            this.cache = cache;
        }

        /// <summary>
        /// Gets the economics group asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="isAdmin">if set to <c>true</c> [is admin].</param>
        /// <returns></returns>
        public async Task<IList<EconomicGroupDto>> GetEconomicsGroupAsync(string userId, bool isAdmin)
        {
            IList<EconomicGroup> economicGroups = isAdmin ? 
                await economicGroupRepository.GetEconomicsGroupAsync().ConfigureAwait(false) :
                await economicGroupRepository.GetEconomicsGroupAsync(userId).ConfigureAwait(false);
            if (economicGroups.Any())
            {
                var firstGroupId = economicGroups.OrderBy(x => x.EconomicGroupName).FirstOrDefault().Id;
                var groupId = cache.Find($"{Constant.ECONMIC_GROUP_ID_CACHE_NAME}{userId}", () => firstGroupId);
                var existGroup = economicGroups.Any(t => t.Id == groupId);
                if (!existGroup)
                {
                    cache.Set($"{Constant.ECONMIC_GROUP_ID_CACHE_NAME}{userId}", groupId);
                    groupId = firstGroupId;
                }

                foreach (var economicGroup in economicGroups.Where(economicGroup => economicGroup.Id == groupId))
                {
                    economicGroup.Selected = true;
                }
            }

            return mapper.Map<IList<EconomicGroupDto>>(economicGroups);
        }
    }
}
