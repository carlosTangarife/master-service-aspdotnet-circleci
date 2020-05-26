using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Renting.MasterServices.Api.Helpers;
using Renting.MasterServices.Core.Dtos.Client;
using Renting.MasterServices.Core.Interfaces.Client;
using System;
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
    public class PlateController : ControllerBase
    {
        private readonly IPlateService plateByClientService;
        private readonly ILog log;
        private readonly ITokenHelper tokenHelper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="plateByClientService"></param>
        /// <param name="log"></param>
        /// <param name="tokenHelper"></param>
        public PlateController(IPlateService plateByClientService, ILog log, ITokenHelper tokenHelper)
        {
            this.plateByClientService = plateByClientService;
            this.log = log;
            this.tokenHelper = tokenHelper;
        }

        /// <summary>
        /// Retorna las placas de un cliente
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        [HttpGet("GetPlatesByClient/{clientId}")]
        [Produces(typeof(PlateDto))]
        public async Task<ActionResult> GetPlatesByClient(int clientId)
        {
            try
            {
                var plates = await plateByClientService.GetPlatesByClientAsync(clientId).ConfigureAwait(false);
                return new OkObjectResult(plates);
            }
            catch (Exception ex)
            {
                log.Error($"Ocurrió un error al consultar las placas para el cliente {clientId}: {ex.Message}");
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        /// <summary>
        /// Actualiza los kilometrajes de una placa
        /// </summary>
        /// <param name="plateKmRequest"></param>
        /// <returns></returns>
        [HttpPost("UpdatePlateKm")]
        public async Task<ActionResult> UpdatePlateKm(PlateKmRequestDto plateKmRequest)
        {
            try
            {
                string userEmail = tokenHelper.GetUserEmail(User.Claims);
                await plateByClientService.UpdatePlateKmAsync(plateKmRequest, userEmail);
                return new OkResult();
            }
            catch (Exception ex)
            {
                log.Error($"Se ha generado un error actualizando los kilometrajes de la placa {plateKmRequest.PlateCode}: {ex.Message}", ex);
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }
    }
}
