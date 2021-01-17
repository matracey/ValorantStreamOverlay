using Microsoft.Extensions.DependencyInjection;
using System.IO;
using ValorantOverlay.Wpf.Models;
using ValorantOverlay.Core.Models;
using System.Threading.Tasks;
using System.Text.Json;

namespace ValorantOverlay.Wpf.Extensions
{
    public static class UserSettingsExtensions
    {
        /// <summary>
        /// Adds a ValorantOverlay.Wpf.Models.AppUserSettings instance which is tied to the ValorantOverlay.Wpf.Properties.Settings type.
        /// </summary>
        public static async Task AddAppUserSettingsAsync(this IServiceCollection services)
        {
            var settings = await ReadFromJsonFile("usersettings.json");
            settings.Save();

            settings.UserSettingsUpdated += async settings =>
            {
                await settings.SaveToJsonFile("usersettings.json");
            };

            services.AddSingleton<IUserSettings, UserSettings>(c => settings);
            services.AddSingleton<IAppUserSettings, AppUserSettings>(c => settings);
        }

        private static async Task SaveToJsonFile(this IAppUserSettings settings, string filename)
        {
            using FileStream createStream = File.Create(filename);
            await JsonSerializer.SerializeAsync(createStream, settings);
        }
        private static async Task<AppUserSettings> ReadFromJsonFile(string filename)
        {
            if (!File.Exists(filename))
            {
                var settings = new AppUserSettings();
                await settings.SaveToJsonFile(filename);
                return settings;
            }
            using FileStream readStream = File.OpenRead(filename);
            return await JsonSerializer.DeserializeAsync<AppUserSettings>(readStream);
        }
    }
}
