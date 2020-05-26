using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Renting.MasterServices.Api.Helpers;
using Renting.MasterServices.Core.Dtos.Client;
using Renting.MasterServices.Core.Interfaces.Client;
using Renting.MasterServices.Domain;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Renting.MasterServices.Api.Controllers.Client
{
    /// <summary>
    /// 
    /// </summary>
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly IStateService stateService;
        private readonly ILog log;
        private readonly IConfigProvider config;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stateService"></param>
        /// <param name="log"></param>
        public StateController(IStateService stateService, ILog log, IConfigProvider config)
        {
            this.stateService = stateService;
            this.log = log;
            this.config = config;
        }

        /// <summary>
        /// Retorna la lista de estados de la configuración de preoperativos
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetStates")]
        [Produces(typeof(IList<StateDto>))]
        public ActionResult GetStates()
        {
            try
            {
                var parametersStates = config.GetVal("Appsettings:ParametersStates").Split(',');
                var states = stateService.GetStates(parametersStates);
                return new OkObjectResult(states);
            }
            catch (Exception ex)
            {
                log.Error($"Ocurrió un error al consultar los estados: {ex.Message}");
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }
    }
}
