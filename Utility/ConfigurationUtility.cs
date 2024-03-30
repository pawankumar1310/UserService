namespace Utility
{
    public static class ConfigurationUtility
    {
        public static string GetConnectionString()
        {
            var builder = new ConfigurationBuilder()
                              .SetBasePath(Directory.GetCurrentDirectory())
                              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            return configuration.GetConnectionString("UserDB") ?? string.Empty;
        }
    }
}