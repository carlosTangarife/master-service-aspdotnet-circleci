using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Renting.MasterServices.Api.Filters
{
    /// <summary>
    /// clase que agrega a las cabeceras de respuesta el tiempo transcurrido en regresar una respuesta al cliente
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Filters.ActionFilterAttribute" />
    [ExcludeFromCodeCoverage]
    [AttributeUsage(AttributeTargets.All)]
    public class ProcessTimeAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Gets or sets the start time.
        /// </summary>
        /// <value>
        /// The start time.
        /// </value>
        public DateTime startTime { get; set; }

        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        /// <inheritdoc />
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            startTime = DateTime.Now;
            base.OnActionExecuting(context);
        }

        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        /// <inheritdoc />
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            context.HttpContext.Response.Headers.Add("X-Response-Time-End", (DateTime.Now - startTime).ToString());
            base.OnActionExecuted(context);
        }
    }
}
