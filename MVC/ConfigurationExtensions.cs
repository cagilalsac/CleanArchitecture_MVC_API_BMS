using MVC.Settings;

namespace MVC
{
    public static class ConfigurationExtensions
    {
        public static void ConfigureMVC(this IConfiguration configuration)
        {
            configuration.GetSection(nameof(AppSettings)).Bind(new AppSettings());
        }
    }
}
