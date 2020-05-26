using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;

namespace Renting.MasterServices.Domain
{
    public class ConfigProvider : IConfigProvider
    {
        private readonly IConfigurationRoot configuration;

        public ConfigProvider()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            configuration = builder.Build();
        }

        public string GetVal(string key)
        {
            return configuration.GetSection(key).Value;
        }

        public IConfigurationSection GetSection(string key)
        {
            return configuration.GetSection(key);
        }
    }
}
