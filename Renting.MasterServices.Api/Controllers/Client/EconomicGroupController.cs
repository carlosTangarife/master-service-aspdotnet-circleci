using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Renting.MasterServices.Api.Helpers;
using Renting.MasterServices.Core.Dtos.Client;
using Renting.MasterServices.Core.Interfaces.Client;
using System;
using System.Net;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace Renting.MasterServices.Api.Controllers.Client
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EconomicGroupController : ControllerBase
    {
        private readonly IEconomicGroupService economicGroupService;
        private readonly ILog log;
        private readonly ITokenHelper tokenHelper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="economicGroupService"></param>
        /// <param name="log"></param>
        /// <param name="tokenHelper"></param>
        public EconomicGroupController(IEconomicGroupService economicGroupService, ILog log, ITokenHelper tokenHelper)
        {
            this.economicGroupService = economicGroupService;
            this.log = log;
            this.tokenHelper = tokenHelper;
        }

        /// <summary>
        /// Consulta los grupos económicos por usuario
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetEconomicsGroupByUserId")]
        [Produces(typeof(EconomicGroupDto))]
        public async Task<ActionResult> GetEconomicsGroupByUserId()
        {
            bool isAdmin = tokenHelper.IsAdmin(User.Claims);
            string userId = tokenHelper.GetUserId(User.Claims);
            try
            {
                var economicGroups = await economicGroupService.GetEconomicsGroupAsync(userId, isAdmin).ConfigureAwait(false);
                return new OkObjectResult(economicGroups);
            }
            catch (Exception ex)
            {
                log.Error($"Ocurrió un error al consultar los grupos economicos: {ex.Message}");
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }
    }
}
