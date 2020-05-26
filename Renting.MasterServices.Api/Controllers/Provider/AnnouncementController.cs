using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Renting.MasterServices.Core.Dtos.Provider;
using Renting.MasterServices.Core.Interfaces.Provider;
using Renting.MasterServices.Infraestructure.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Renting.MasterServices.Api.Controllers.Provider
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AnnouncementController : ControllerBase
    {
        private readonly IAnnouncementService announcementService;
        private readonly ILog log;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnnouncementController"/> class.
        /// </summary>
        /// <param name="announcementService">The announcement service.</param>
        /// <param name="log"></param>
        public AnnouncementController(IAnnouncementService announcementService, ILog log)
        {
            this.announcementService = announcementService;
            this.log = log;
        }

        /// <summary>
        /// Crear un comunicado
        /// </summary>
        /// <param name="announcement">Objeto que contiene la información del comunicado</param>
        /// <param name="file">Imagen del comunicado</param>
        /// <returns></returns>
        [HttpPost("CreateAnnouncement")]
        public async Task<IActionResult> CreateAnnouncement(AnnouncementDto announcement, IFormFile file)
        {
            try
            {
                await announcementService.CreateAnnouncementAsync(announcement, file).ConfigureAwait(false);
                return Ok(announcement);
            }
            catch(InvalidFileExtensionException ex)
            {
                log.Error($"Ocurrió un error al crear el comunicado: {ex.Message}");
                return StatusCode(HttpStatusCode.UnsupportedMediaType.GetHashCode(), ex.Message);
            }
            catch (InvalidFileSizeException ex)
            {
                log.Error($"Ocurrió un error al crear el comunicado: {ex.Message}");
                return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), ex.Message);
            }
            catch (InvalidFileDimensionException ex)
            {
                log.Error($"Ocurrió un error al crear el comunicado: {ex.Message}");
                return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), ex.Message);
            }
            catch (Exception ex)
            {
                log.Error($"Ocurrió un error al crear el comunicado: {ex.Message}");
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        /// <summary>
        /// Obtiene todos los comunicados
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAnnouncements")]
        [Produces(typeof(IList<AnnouncementDto>))]
        public IActionResult GetAnnouncements()
        {
            try
            {
                var announcements = announcementService.GetAll();
                if (announcements.Any())
                {
                    return Ok(announcements);
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                log.Error($"Ocurrió un error al consultar los comunicados: {ex.Message}");
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el comunicado por indentificador.
        /// </summary>
        /// <param name="id">Identificador del comunicado</param>
        /// <returns></returns>
        [HttpGet("GetAnnouncementById/{id}")]
        [Produces(typeof(AnnouncementDto))]
        public IActionResult GetAnnouncementsById(int? id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest();
                }

                var announcements = announcementService.FindById(id);
                if (announcements != null)
                {
                    return Ok(announcements);
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                log.Error($"Ocurrió un error al consultar el comunicado {id}: {ex.Message}");
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message); ;
            }
        }

        /// <summary>
        /// Actualiza el comunicado
        /// </summary>
        /// <param name="id">Identificador del comunicado</param>
        /// <param name="announcement">Objeto que contiene la información del comunicado</param>
        /// <param name="file">Imagen del comunicado</param>
        /// <returns></returns>
        [HttpPost("UpdateAnnouncement")]
        public async Task<IActionResult> UpdateAnnouncement(int? id, AnnouncementDto announcement, IFormFile file)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest();
                }

                if (id == announcement.IdAnnouncement)
                {
                    await announcementService.UpdateAnnouncementAsync(announcement, file).ConfigureAwait(false);
                    return Ok();
                }

                return BadRequest();
            }
            catch (InvalidFileExtensionException ex)
            {
                log.Error($"Ocurrió un error al crear el comunicado: {ex.Message}");
                return StatusCode(HttpStatusCode.UnsupportedMediaType.GetHashCode(), ex.Message);
            }
            catch (InvalidFileSizeException ex)
            {
                log.Error($"Ocurrió un error al crear el comunicado: {ex.Message}");
                return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), ex.Message);
            }
            catch (InvalidFileDimensionException ex)
            {
                log.Error($"Ocurrió un error al crear el comunicado: {ex.Message}");
                return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), ex.Message);
            }
            catch (Exception ex)
            {
                log.Error($"Ocurrió un error al actualizar el comunicado: {ex.Message}");
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message); ;
            }
        }

        /// <summary>
        /// Actualiza el estado del comunicado 
        /// </summary>
        /// <param name="id">Identificador del comunicado</param>
        /// <param name="state">Estado del comunicado</param>
        /// <returns></returns>
        [HttpPost("UpdateAnnouncementState")]
        public async Task<IActionResult> UpdateAnnouncementState(int? id, bool state)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest();
                }

                await announcementService.UpdateAnnouncementStateAsync(id, state).ConfigureAwait(false);
                return Ok();
            }
            catch(ArgumentNullException nEx)
            {
                log.Error($"Ocurrió un error al actualizar el comunicado {id}: {nEx.Message}");
                return NotFound(nEx.Message);
            }   
            catch (Exception ex)
            {
                log.Error($"Ocurrió un error al actualizar el comunicado: {ex.Message}");
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        /// <summary>
        /// Elimina el comunicado
        /// </summary>
        /// <param name="id">Identificador del comunicado</param>
        /// <returns></returns>
        [HttpPost("DeleteAnnouncement")]
        public async Task<IActionResult> DeleteAnnouncement(int? id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest();
                }

                await announcementService.DeleteAnnouncementAsync(id).ConfigureAwait(false);
                return Ok();
            }
            catch (ArgumentNullException aEx)
            {
                log.Error($"Ocurrió un error al eliminar el comunicado {id}: {aEx.Message}");
                return NotFound(aEx.Message);
            }
            catch (Exception ex)
            {
                log.Error($"Ocurrió un error al elimina el comunicado {id}: {ex.Message}");
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }
    }
}
