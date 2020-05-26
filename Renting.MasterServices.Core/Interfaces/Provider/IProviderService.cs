using Renting.MasterServices.Core.Dtos.Provider;

using System.Collections.Generic;

namespace Renting.MasterServices.Core.Interfaces.Provider
{
    /// <summary>
    /// 
    /// </summary>
    public interface IProviderService : IService<Domain.Entities.Provider.Provider, ProviderDto>
    {
        /// <summary>
        /// Obtiene una lista de proveedores por el identificador del usuario
        /// </summary>
        /// <param name="userId">identificador del usuario</param>
        /// <returns></returns>
        IList<ProviderDto> GetByUserId(string userId);

        /// <summary>
        /// Obtiene una lista de proveedores por el email
        /// </summary>
        /// <param name="emailUser"> email del usuario</param>
        /// <returns></returns>
        IList<ProviderDto> GetByEmailUser(string emailUser);

        /// <summary>
        /// Obtiene una lista de proveedores, filtrado por el identificador del usuario estraido del token , 
        /// y pone en estado seleccionado el proveedor que coincida con el ultimo proovedor que este en la cache relacionado al usuario logueado
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<ProviderDto> GetFromCache(string userId);
    }
}
