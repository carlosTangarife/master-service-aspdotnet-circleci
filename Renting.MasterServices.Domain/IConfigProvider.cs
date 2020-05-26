using Microsoft.Extensions.Configuration;

namespace Renting.MasterServices.Domain
{
    public interface IConfigProvider
    {
        string GetVal(string key);
        IConfigurationSection GetSection(string key);
    }
}
