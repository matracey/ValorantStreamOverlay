using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text.Json;
using System.Windows.Forms;
using ValorantOverlay.App.Extensions;
using ValorantOverlay.App.Fonts;
using ValorantOverlay.App.Forms;
using ValorantOverlay.App.Helpers;
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
            // System.Net
            services.AddSingleton<CookieContainer>();

            // System.Net.Http
            services.AddHttpClient(HttpClientName.NorthAmerica , c => c.BaseAddress = new Uri("https://pd.na.a.pvp.net")).ConfigurePrimaryHttpMessageHandler(c => new HttpClientHandler { CookieContainer = c.GetRequiredService<CookieContainer>() });
            services.AddHttpClient(HttpClientName.Europe, c => c.BaseAddress = new Uri("https://pd.eu.a.pvp.net")).ConfigurePrimaryHttpMessageHandler(c => new HttpClientHandler { CookieContainer = c.GetRequiredService<CookieContainer>() });
            services.AddHttpClient(HttpClientName.Korea, c => c.BaseAddress = new Uri("https://pd.kr.a.pvp.net")).ConfigurePrimaryHttpMessageHandler(c => new HttpClientHandler { CookieContainer = c.GetRequiredService<CookieContainer>() });
            services.AddHttpClient(HttpClientName.AsiaPacific, c => c.BaseAddress = new Uri("https://pd.ap.a.pvp.net")).ConfigurePrimaryHttpMessageHandler(c => new HttpClientHandler { CookieContainer = c.GetRequiredService<CookieContainer>() });
            services.AddHttpClient(HttpClientName.AuthRiot, c => c.BaseAddress = new Uri("https://auth.riotgames.com/")).ConfigurePrimaryHttpMessageHandler(c => new HttpClientHandler { CookieContainer = c.GetRequiredService<CookieContainer>() });
            services.AddHttpClient(HttpClientName.EntitlementsAuthRiot, c => c.BaseAddress = new Uri("https://entitlements.auth.riotgames.com/")).ConfigurePrimaryHttpMessageHandler(c => new HttpClientHandler { CookieContainer = c.GetRequiredService<CookieContainer>() });

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
            services.AddSingleton<IRiotAuthenticator, RiotAuthenticator>();
            services.AddSingleton<IMatchManager, MatchManager>();
            services.AddSingleton<IStateManager, StateManager>();

            // ValorantOverlay.App.Extensions
            services.AddAppUserSettings();

            // ValorantOverlay.App.Services
            services.AddSingleton<IUpdateService, UpdateService>();

            // ValorantOverlay.App.Fonts
            services.AddSingleton(svc => {
                var fonts = new PrivateFontCollection();
                fonts.AddFontFromResource(Assembly.GetExecutingAssembly(), "ValorantOverlay.App.Resources.Anton-Regular.ttf");
                return fonts;
            });
            services.AddSingleton<AntonRegular>();

            // ValorantOverlay.App.Forms
            services.AddScoped<SettingsForm>();
            services.AddScoped<ValorantStreamOverlay>();
        }
    }
}
