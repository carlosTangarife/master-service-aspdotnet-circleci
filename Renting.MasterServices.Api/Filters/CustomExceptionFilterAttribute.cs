using log4net;
using log4net.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Renting.MasterServices.Infraestructure.Resources;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Renting.MasterServices.Api.Filters
{
    /// <summary>
    /// CustomExceptionFilterAttribute
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Filters.ExceptionFilterAttribute" />
    [ExcludeFromCodeCoverage]
    [AttributeUsage(AttributeTargets.All)]
    public sealed class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ILog logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomExceptionFilterAttribute"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public CustomExceptionFilterAttribute(ILog logger)
        {
            this.logger = logger;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(ExceptionContext context)
        {
            if(context != null)
            {
                if (context.Exception is ArgumentException ||
                    context.Exception is ArgumentNullException ||
                    context.Exception is ArgumentOutOfRangeException)
                {
                    Core.Dtos.ResponseGenericMessage msg = CreateResponseMessage(ErrorMessages.Generic_Error, MessageTypes.Error);
                    context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                    context.Result = new ObjectResult(msg);
                }
                else
                {
                    Core.Dtos.ResponseGenericMessage msg = CreateResponseMessage(ErrorMessages.Generic_Error, MessageTypes.Error);
                    context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Result = new ObjectResult(msg);
                }

                logger.Error(context.Exception.Message, context.Exception);
            }
        }

        private Core.Dtos.ResponseGenericMessage CreateResponseMessage(string message, string messageType)
        {
            return new Core.Dtos.ResponseGenericMessage
            {
                Message = message,
                MessageType = messageType
            };
        }
    }
}
