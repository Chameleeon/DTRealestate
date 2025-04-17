using Microsoft.Extensions.Configuration;

namespace Nekretnine.Services
{
    public interface IConfigurationService
    {
        string GetConnectionString(string name);
        IConfigurationRoot ConfigurationRoot { get; }
    }

    public class ConfigurationService : IConfigurationService
    {
        public IConfigurationRoot ConfigurationRoot { get; }

        public ConfigurationService()
        {
            ConfigurationRoot = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        public string GetConnectionString(string name)
        {
            return ConfigurationRoot.GetConnectionString(name);
        }
    }
}
