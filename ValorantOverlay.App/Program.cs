using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Windows.Forms;
using ValorantOverlay.App.Forms;
using ValorantOverlay.App.Services;
using ValorantOverlay.Core.Models;
using ValorantOverlay.Core.Services;

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
            Application.SetCompatibleTextRenderingDefault(true);

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
            // ValorantOverlay.Core.Models
            services.AddSingleton<IRankList, RankList>(provider =>
            {
                RankList result = new RankList();
                using (StreamReader r = new StreamReader($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\RankInfo.json"))
                {
                    string json = r.ReadToEnd();
                    var rootElement = (JsonElement)JsonSerializer.Deserialize<object>(json);
                    var ranksJson = rootElement.GetProperty("Ranks").GetRawText();
                    var items = JsonSerializer.Deserialize<Dictionary<string, string>>(ranksJson);
                    result.AddRange(items.Values);
                }
                return result;
            });

            // ValorantOverlay.Core.Services
            services.AddSingleton<IRankManager, RankManager>();

            // ValorantOverlay.App.Services
            services.AddSingleton<IUpdateService, UpdateService>();

            // ValorantOverlay.App.Forms
            services.AddScoped<ValorantStreamOverlay>();
        }
    }
}
