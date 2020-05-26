using AutoMapper;
using log4net;
using Renting.MasterServices.Core.Dtos.Client;
using Renting.MasterServices.Core.Interfaces.Client;
using Renting.MasterServices.Domain.IRepository.Client;
using System.Collections.Generic;

namespace Renting.MasterServices.Core.Services.Client
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Services.Service{Domain.Entities.Client.Attribute, AttributeDto}" />
    /// <seealso cref="IAttributeService" />
    public class AttributeService : Service<Domain.Entities.Client.Attribute, AttributeDto>, IAttributeService
    {
        private readonly IAttributeRepository attributeRepository;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeService"/> class.
        /// </summary>
        /// <param name="attributeRepository">The attribute repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="log">The log.</param>
        public AttributeService(IAttributeRepository attributeRepository,
            IMapper mapper, ILog log) : base(attributeRepository, log, mapper)
        {
            this.attributeRepository = attributeRepository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Gets the attributes.
        /// </summary>
        /// <returns></returns>
        public IList<AttributeDto> GetAttributes()
        {
            var attributes = attributeRepository.GetAll();
            return mapper.Map<IList<AttributeDto>>(attributes);
        }
    }
}
