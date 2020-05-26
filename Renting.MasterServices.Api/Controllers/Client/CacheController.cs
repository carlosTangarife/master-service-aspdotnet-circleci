using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Renting.MasterServices.Core.Dtos.Client;
using Renting.MasterServices.Core.Interfaces;
using Renting.MasterServices.Infraestructure.Resources;
using System;
using System.Net;

namespace Renting.MasterServices.Api.Controllers.Client
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CacheController : ControllerBase
    {
        private readonly ICacheService cacheService;
        private readonly ILog log;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cacheService"></param>
        /// <param name="log"></param>
        public CacheController(ICacheService cacheService, ILog log)
        {
            this.cacheService = cacheService;
            this.log = log;
        }

        /// <summary>
        /// Consulta el valor de una llave en la cache
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet("GetCacheByKey/{key}")]
        public ActionResult GetCacheByKey(string key)
        {
            try
            {
                var response = cacheService.Find(key);
                if (response == null)
                {
                    return StatusCode(HttpStatusCode.NotFound.GetHashCode(),
                       string.Format(WarningMessages.CacheKey_Not_Exist, key));
                }
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                log.Error($"Ocurrió un error al consultar la llave {key}: {ex.Message}");
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        /// <summary>
        ///  Guarda una llave y valor en la cache
        /// </summary>
        /// <param name="cacheRequest"></param>
        /// <returns></returns>
        [HttpPost("SetCacheByKey")]
        public ActionResult SetCacheByKey(CacheRequestDto cacheRequest)
        {
            try
            {
                cacheService.Set(cacheRequest.Key, cacheRequest.Value);
                return Ok();
            }
            catch (Exception ex)
            {
                log.Error($"Ocurrió un error al consultar la llave {cacheRequest.Key}: {ex.Message}");
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }
    }
}
