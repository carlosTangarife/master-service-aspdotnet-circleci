using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Renting.MasterServices.Api.Helpers;
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
    public class ProviderController : ControllerBase
    {
        private readonly IProviderService providerService;
        private readonly ILog log;
        private readonly ITokenHelper tokenHelper;

        /// <summary>
        /// 
        /// </summary>
        public ProviderController(IProviderService providerService, ILog log, ITokenHelper tokenHelper)
        {
            this.providerService = providerService;
            this.log = log;
            this.tokenHelper = tokenHelper;
        }

        /// <summary>
        /// Get providers by user identifier
        /// </summary>
        /// <param name="userId">user identifier</param>
        /// <returns></returns>
        [HttpGet("GetByUserId/{userId}")]
        [Produces(typeof(IList<ProviderDto>))]
        public ActionResult GetByUserId(string userId)
        {
            try
            {
                var providers = providerService.GetByUserId(userId);
                return new OkObjectResult(providers);
            }
            catch (Exception ex)
            {
                log.Error($"Ocurrió un error al consultar los proveedores del usuario: {ex.Message}");
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        /// <summary>
        /// Obtiene una lista de proveedores, filtrado por el identificador del usuario estraido del token , 
        /// y pone en estado seleccionado el proveedor que coincida con el ultimo proovedor que este en la cache relacionado al usuario logueado 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetFromCache")]
        [Produces(typeof(IList<ProviderDto>))]
        public ActionResult GetFromCache()
        {
            try
            {
                string userId = tokenHelper.GetUserId(User.Claims);
                var providers = providerService.GetFromCache(userId);
                return new OkObjectResult(providers);
            }
            catch (Exception ex)
            {
                log.Error($"Ocurrió un error al consultar los proveedores del usuario: {ex.Message}");
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        /// <summary>
        /// /// Obtiene una lista de proveedores por el email
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetByEmailUser/emailUser")]
        [Produces(typeof(IList<ProviderDto>))]
        public ActionResult GetByEmail(string emailUser)
        {
            try
            {
                var providers = providerService.GetByEmailUser(emailUser);
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