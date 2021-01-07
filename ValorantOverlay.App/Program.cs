using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Windows.Forms;
using ValorantOverlay.App.Forms;

namespace ValorantOverlay.App
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var builder = new HostBuilder()
               .ConfigureServices((hostContext, services) =>
               {
                   ConfigureServices(services);
               });

            var host = builder.ConfigureLogging(config => config.AddConsole()).Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                try
                {
                    var bootstrap = services.GetRequiredService<ValorantStreamOverlay>();
                    Application.Run(bootstrap);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<ValorantStreamOverlay>>();
                    logger.LogError(ex, "An error occurred.");
                }
            }
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // ValorantOverlay.App.Forms
            services.AddScoped<ValorantStreamOverlay>();
        }
    }
}
