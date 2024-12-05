using Microsoft.Extensions.Configuration;

public static class ConfigHelper
{
    private static IConfigurationRoot _configuration;

    static ConfigHelper()
    {
        _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
    }

    public static string Get(string key)
    {
        return _configuration[key];
    }
}