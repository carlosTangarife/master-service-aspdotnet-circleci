using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleTypeService vehicleTypeService;
        private readonly ILog log;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vehicleTypeService"></param>
        /// <param name="log"></param>
        public VehicleController(IVehicleTypeService vehicleTypeService, ILog log)
        {
            this.vehicleTypeService = vehicleTypeService;
            this.log = log;
        }

        /// <summary>
        /// Consulta los tipos de vehículo
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetVehicleTypes")]
        [Produces(typeof(VehicleTypeDto))]
        public async Task<ActionResult> GetVehicleTypes()
        {
            try
            {
                var vehicleType = await vehicleTypeService.GetVehicleTypesAsync().ConfigureAwait(false);
                return new OkObjectResult(vehicleType);
            }
            catch (Exception ex)
            {
                log.Error($"Ocurrió un error al consultar los tipos de vehículo: {ex.Message}");
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }
    }
}
