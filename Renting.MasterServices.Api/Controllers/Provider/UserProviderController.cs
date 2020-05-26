using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Renting.MasterServices.Core.Dtos.Provider;
using Renting.MasterServices.Core.Interfaces.Provider;
using System;
using System.Collections.Generic;
using System.Net;

namespace Renting.MasterServices.Api.Controllers.Provider
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserProviderController : ControllerBase
    {
        private readonly IUserProviderService userProviderService;
        private readonly ILog log;

        /// <summary>
        /// 
        /// </summary>
        public UserProviderController(IUserProviderService userProviderService, ILog log)
        {
            this.userProviderService = userProviderService;
            this.log = log;
        }

        /// <summary>
        /// Obtiene una lista de usuarios-proveedores por el id del proveedor.
        /// </summary>
        /// <param name="supplierId">Id del proveedor.</param>
        /// <returns></returns>
        [HttpGet("GetEmailsBySupplierId/{supplierId}")]
        [Produces(typeof(IList<UserSupplierDto>))]
        public ActionResult GetEmailsBySupplierId(int supplierId)
        {
            try
            {
                var providers = userProviderService.GetEmailsBySupplierId(supplierId);
                return new OkObjectResult(providers);
            }
            catch (Exception ex)
            {
                log.Error($"Ocurrió un error al consultar los proveedores del usuario: {ex.Message}");
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        /// <summary>
        /// Obtiene una lista de usuarios-proveedores por email.
        /// </summary>
        /// <param name="email">email.</param>
        /// <returns></returns>
        [HttpGet("GetSuppliersByEmail/{email}")]
        [Produces(typeof(IList<UserSupplierDto>))]
        public ActionResult GetSuppliersByEmail(string email)
        {
            try
            {
                var providers = userProviderService.GetSuppliersByEmail(email);
                return new OkObjectResult(providers);
            }
            catch (Exception ex)
            {
                log.Error($"Ocurrió un error al consultar los proveedores del usuario: {ex.Message}");
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }
    }
}