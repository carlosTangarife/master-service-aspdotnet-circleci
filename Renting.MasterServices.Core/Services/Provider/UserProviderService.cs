using System.Collections.Generic;
using AutoMapper;
using log4net;
using Renting.MasterServices.Core.Dtos.Provider;
using Renting.MasterServices.Core.Interfaces.Provider;
using Renting.MasterServices.Domain.Entities.Provider;
using Renting.MasterServices.Domain.IRepository.Provider;

namespace Renting.MasterServices.Core.Services.Provider
{
    /// <summary>
    /// 
    /// </summary>
    public class UserProviderService : Service<UserProvider, UserProviderDto>, IUserProviderService
    {
        private readonly IUserProviderRepository userProviderRepository;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserProviderService"/> class.
        /// </summary>
        /// <param name="userProviderRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="log"></param>
        public UserProviderService(IUserProviderRepository userProviderRepository,
            IMapper mapper, ILog log) : base(userProviderRepository, log, mapper)
        {
            this.userProviderRepository = userProviderRepository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Obtiene una lista de usuarios-proveedores por el id del proveedor.
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        public IList<UserSupplierDto> GetEmailsBySupplierId(long supplierId)
        {
            var userSuppliers = GetAll(x => x.ProviderId == supplierId);
            return mapper.Map<IList<UserSupplierDto>>(userSuppliers);
        }

        /// <summary>
        /// Obtiene una lista de usuarios-proveedores por emails.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public IList<UserSupplierDto> GetSuppliersByEmail(string email)
        {
            var userSuppliers = GetAll(x => x.EmailUser == email);
            return mapper.Map<IList<UserSupplierDto>>(userSuppliers);
        }
    }
}
