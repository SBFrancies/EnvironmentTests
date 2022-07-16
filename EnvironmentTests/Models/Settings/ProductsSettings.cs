using EnvironmentTests.Exceptions;

namespace EnvironmentTests.Models.Settings
{
    public class ProductsSettings
    {
        public ProductsSettings(ProductsAppSettings appsettings)
        {
            DbConnectionString = appsettings.DbConnectionString ?? throw new InvalidConfigurationException(nameof(appsettings.DbConnectionString), "DbConnectionString cannot be null");
        }

        public string DbConnectionString { get; }
    }
}
