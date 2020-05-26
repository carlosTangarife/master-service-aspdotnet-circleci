using Renting.MasterServices.Core.Dtos.Client;
using Renting.MasterServices.Domain.Entities.Client;
using System.Collections.Generic;

namespace Renting.MasterServices.Core.Interfaces.Client
{
    /// <summary>
    /// IAttributeService
    /// </summary>
    /// <seealso cref="Renting.MasterServices.Core.Interfaces.IService{Renting.MasterServices.Domain.Entities.Client.Attribute, Renting.MasterServices.Core.Dtos.Client.AttributeDto}" />
    public interface IAttributeService : IService<Attribute, AttributeDto>
    {
        /// <summary>
        /// Gets the attributes.
        /// </summary>
        /// <returns></returns>
        IList<AttributeDto> GetAttributes();
    }
}
