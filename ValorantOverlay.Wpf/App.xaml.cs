using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using ValorantOverlay.Core.Models;
using ValorantOverlay.Core.Services;
using ValorantOverlay.Wpf.Extensions;
using ValorantOverlay.Wpf.Views;

namespace ValorantOverlay.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IConfigurationRoot _configuration;
        private ServiceProvider _serviceProvider;

        protected override async void OnStartup(StartupEventArgs e)
        {
            var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            _configuration = builder.Build();

            var serviceCollection = new ServiceCollection();
            await ConfigureServicesAsync(serviceCollection);

            _serviceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private async Task ConfigureServicesAsync(ServiceCollection services)
        {
            var endpoints = _configuration.GetSection("HttpEndpoints");

            // System.Net
            services.AddSingleton<CookieContainer>();

            // System.Net.Http
            services
                .AddHttpClient(HttpClientName.NorthAmerica, c => c.BaseAddress = new Uri(endpoints[HttpClientName.NorthAmerica]))
                .ConfigurePrimaryHttpMessageHandler(c => new HttpClientHandler { CookieContainer = c.GetRequiredService<CookieContainer>() });
            services
                .AddHttpClient(HttpClientName.Europe, c => c.BaseAddress = new Uri(endpoints[HttpClientName.Europe]))
                .ConfigurePrimaryHttpMessageHandler(c => new HttpClientHandler { CookieContainer = c.GetRequiredService<CookieContainer>() });
            services
                .AddHttpClient(HttpClientName.Korea, c => c.BaseAddress = new Uri(endpoints[HttpClientName.Korea]))
                .ConfigurePrimaryHttpMessageHandler(c => new HttpClientHandler { CookieContainer = c.GetRequiredService<CookieContainer>() });
            services
                .AddHttpClient(HttpClientName.AsiaPacific, c => c.BaseAddress = new Uri(endpoints[HttpClientName.AsiaPacific]))
                .ConfigurePrimaryHttpMessageHandler(c => new HttpClientHandler { CookieContainer = c.GetRequiredService<CookieContainer>() });
            services
                .AddHttpClient(HttpClientName.AuthRiot, c => c.BaseAddress = new Uri(endpoints[HttpClientName.AuthRiot]))
                .ConfigurePrimaryHttpMessageHandler(c => new HttpClientHandler { CookieContainer = c.GetRequiredService<CookieContainer>() });
            services
                .AddHttpClient(HttpClientName.EntitlementsAuthRiot, c => c.BaseAddress = new Uri(endpoints[HttpClientName.EntitlementsAuthRiot]))
                .ConfigurePrimaryHttpMessageHandler(c => new HttpClientHandler { CookieContainer = c.GetRequiredService<CookieContainer>() });

            // ValorantOverlay.Core.Models
            services.AddSingleton<IRankList, RankList>(provider => _configuration.GetSection("Ranks").Get<RankList>());

            // ValorantOverlay.Core.Services
            services.AddSingleton<IRankManager, RankManager>();
            services.AddSingleton<IRiotAuthenticator, RiotAuthenticator>();
            services.AddSingleton<IMatchManager, MatchManager>();
            services.AddSingleton<IStateManager, StateManager>();

            // ValorantOverlay.Wpf.Extensions
            await services.AddAppUserSettingsAsync();

            services.AddTransient<MainWindow>();
            services.AddTransient<SettingsWindow>();
        }
    }
}
