using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Renting.MasterServices.Core.Dtos.Client;
using Renting.MasterServices.Core.Interfaces.Client;
using System;
using System.Collections.Generic;
using System.Net;

namespace Renting.MasterServices.Api.Controllers.Client
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AttributeController : ControllerBase
    {
        private readonly IAttributeService attributeService;
        private readonly ILog log;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attributeService"></param>
        /// <param name="log"></param>
        public AttributeController(IAttributeService attributeService, ILog log)
        {
            this.attributeService = attributeService;
            this.log = log;
        }

        /// <summary>
        /// Retorna la lista de estados de la configuración de preoperativos
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAttributes")]
        [Produces(typeof(IList<AttributeDto>))]
        public ActionResult GetAttributes()
        {
            try
            {
                var attributes = attributeService.GetAttributes();
                return new OkObjectResult(attributes);
            }
            catch (Exception ex)
            {
                log.Error($"Ocurrió un error al consultar los atributos: {ex.Message}");
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }
    }
}
