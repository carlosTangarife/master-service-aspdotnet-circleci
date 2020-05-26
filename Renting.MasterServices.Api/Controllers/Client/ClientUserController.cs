using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Renting.MasterServices.Api.Helpers;
using Renting.MasterServices.Core.Dtos.Client;
using Renting.MasterServices.Core.Interfaces.Client;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Renting.MasterServices.Api.Controllers.Client
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientUserController : ControllerBase
    {
        private readonly IClientUserService clientUserService;
        private readonly ILog log;
        private readonly ITokenHelper tokenHelper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientUserService"></param>
        /// <param name="log"></param>
        /// <param name="tokenHelper"></param>
        public ClientUserController(IClientUserService clientUserService, ILog log, ITokenHelper tokenHelper)
        {
            this.clientUserService = clientUserService;
            this.log = log;
            this.tokenHelper = tokenHelper;
        }

        /// <summary>
        /// Consulta los clientes de un usuario y grupo económico
        /// </summary>
        /// <param name="economicGroupId"></param>
        /// <returns></returns>
        [HttpGet("GetClientsByUserId/{economicGroupId}")]
        [Produces(typeof(EconomicGroupDto))]
        public async Task<ActionResult> GetClientsByUserId(int economicGroupId)
        {
            bool isAdmin = tokenHelper.IsAdmin(User.Claims);
            string userId = tokenHelper.GetUserId(User.Claims);

            try
            {
                var clientsUser = await clientUserService.GetClientsByUserIdAsync(userId, isAdmin, economicGroupId).ConfigureAwait(false);
                return new OkObjectResult(clientsUser);
            }
            catch (Exception ex)
            {
                log.Error($"Ocurrió un error al consultar los clientes del usuario: {ex.Message}");
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }
    }
}
