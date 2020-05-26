using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Renting.MasterServices.Core.Dtos.Client;
using Renting.MasterServices.Core.Interfaces.Client;
using Renting.MasterServices.Infraestructure.Resources;
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
    public class ParameterController : ControllerBase
    {
        private readonly IParameterService parameterService;
        private readonly ILog log;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameterService"></param>
        /// <param name="log"></param>
        public ParameterController(IParameterService parameterService, ILog log)
        {
            this.parameterService = parameterService;
            this.log = log;
        }

        /// <summary>
        /// Retorna el parametro consultado por nombre del parámetro
        /// </summary>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        [HttpGet("GetParameterByName/{parameterName}")]
        [Produces(typeof(ParameterDto))]
        public async Task<ActionResult> GetParameterByName(string parameterName)
        {
            try
            {
                var parameter = await parameterService.GetParameterByNameAsync(parameterName).ConfigureAwait(false);
                if (parameter == null)
                {
                    return StatusCode(HttpStatusCode.NotFound.GetHashCode(), 
                        string.Format(WarningMessages.Parameter_Not_Exist, parameterName));
                }

                return new OkObjectResult(parameter);
            }
            catch (Exception ex)
            {
                log.Error($"Ocurrió un error al consultar el parámetro {parameterName} : {ex.Message}");
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

    }
}
