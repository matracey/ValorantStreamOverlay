using Microsoft.Extensions.DependencyInjection;
using ValorantOverlay.App.Models;
using ValorantOverlay.App.Properties;
using ValorantOverlay.Core.Models;

namespace ValorantOverlay.App.Extensions
{
    public static class UserSettingsExtensions
    {
        /// <summary>
        /// Adds a ValorantOverlay.App.Models.AppUserSettings instance which is tied to the ValorantOverlay.App.Properties.Settings type.
        /// </summary>
        public static void AddAppUserSettings(this IServiceCollection services)
        {
            var settings = new AppUserSettings
            {
                Username = Settings.Default.username,
                Password = Settings.Default.password,
                Region = Settings.Default.region,
                Refresh = Settings.Default.refresh,
                Skin = Settings.Default.skin,
                TwitchChannel = Settings.Default.twitchChannel,
                TwitchBotUsername = Settings.Default.twitchBotUsername,
                TwitchBotToken = Settings.Default.twitchBotToken,
                TwitchbotEnabled = Settings.Default.twitchbotEnabled,
            };
            settings.Save();

            settings.UserSettingsUpdated += settings =>
            {
                Settings.Default.username = settings.Username;
                Settings.Default.password = settings.Password;
                Settings.Default.region = settings.Region;
                Settings.Default.refresh = settings.Refresh;
                Settings.Default.skin = settings.Skin;
                Settings.Default.twitchChannel = settings.TwitchChannel;
                Settings.Default.twitchBotUsername = settings.TwitchBotUsername;
                Settings.Default.twitchBotToken = settings.TwitchBotToken;
                Settings.Default.twitchbotEnabled = settings.TwitchbotEnabled;

                Settings.Default.Save();
            };

            services.AddSingleton<IUserSettings, UserSettings>(c => settings);
            services.AddSingleton<IAppUserSettings, AppUserSettings>(c => settings);
        }
    }
}
