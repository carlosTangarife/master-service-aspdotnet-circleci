/*
* Ceiba Software House SAS
* Copyright (C) 2019
* www.ceiba.com.co
* 
* Ceiba is a full-service custom software development 
* house dedicated to delivering maximum possible quality 
* in the minimum possible time. Utilizing agile methodology,
* we create beautiful, usable digital products engineered to perform.
* 
* Calle 8 b 65 191 oficina 409 Centro Empresarial Puertoseco.
* Medellín, Antioquia, Colombia.
* Conmutador: (574) 444 5 111, 
* or write us to the email contacts@ceiba.com.co
*/

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Renting.MasterServices.Api.Filters
{
    /// <summary>
    /// Client Ip Check Filter
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Filters.ActionFilterAttribute" />
    [AttributeUsage(AttributeTargets.All)]
    [ExcludeFromCodeCoverage]
    public sealed class ClientIpCheckFilterAttribute: ActionFilterAttribute
    {

        private readonly IConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientIpCheckFilterAttribute"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public ClientIpCheckFilterAttribute(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        /// <inheritdoc />
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if(context != null)
            {
                var remoteIp = context.HttpContext.Connection.RemoteIpAddress.ToString();
                var Ips = configuration.GetSection("ClientIpWiteList").GetValue<string>("ClientIp");
                IList<string> IpList = Ips.Split(';');

                if (!IpList.Any(option => option.Equals(remoteIp, StringComparison.OrdinalIgnoreCase)))
                {
                    context.Result = new UnauthorizedResult();
                }

                base.OnActionExecuting(context);
            }
        }
    }
}
