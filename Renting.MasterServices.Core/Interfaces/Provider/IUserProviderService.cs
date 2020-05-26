using Renting.MasterServices.Core.Dtos.Provider;
using Renting.MasterServices.Domain.Entities.Provider;
using System.Collections.Generic;

namespace Renting.MasterServices.Core.Interfaces.Provider
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUserProviderService : IService<UserProvider, UserProviderDto>
    {
        /// <summary>
        /// Obtiene una lista de usuarios-proveedores por el id del proveedor.
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        IList<UserSupplierDto> GetEmailsBySupplierId(long supplierId);
        
        /// <summary>
        /// Obtiene una lista de usuarios-proveedores por email.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        IList<UserSupplierDto> GetSuppliersByEmail(string email);
    }
}
