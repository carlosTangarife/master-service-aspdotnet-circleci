using Renting.MasterServices.Domain.Entities.Client;
using System;

namespace Renting.MasterServices.Core.Test.Data
{
    public static class ParameterServiceData
    {
        public static Parameter GetParameterDefined()
        {
            return new Parameter
            {
                Id = 1,
                Description = "Description",
                LoginDate = DateTime.Now,
                Name = "Name",
                Type = "Type",
                UserLoginName = "user.test@ceiba.com.co",
                Value = "http://ceiba.com.co/UrlPdfActaPreoperativo"
            };
        }

        public static Parameter GetParameterUnDefined()
        {
            return default(Parameter);
        }
    }
}
