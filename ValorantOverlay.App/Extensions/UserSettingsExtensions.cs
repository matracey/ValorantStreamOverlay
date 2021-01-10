using Microsoft.Extensions.DependencyInjection;
using ValorantOverlay.App.Models;
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
                Username = Properties.Settings.Default.username,
                Password = Properties.Settings.Default.password,
                Region = Properties.Settings.Default.region,
                Refresh = Properties.Settings.Default.refresh,
                Skin = Properties.Settings.Default.skin,
                TwitchChannel = Properties.Settings.Default.twitchChannel,
                TwitchBotUsername = Properties.Settings.Default.twitchBotUsername,
                TwitchBotToken = Properties.Settings.Default.twitchBotToken,
                TwitchbotEnabled = Properties.Settings.Default.twitchbotEnabled,
            };
            settings.Save();

            settings.UserSettingsUpdated += settings =>
            {
                Properties.Settings.Default.username = settings.Username;
                Properties.Settings.Default.password = settings.Password;
                Properties.Settings.Default.region = settings.Region;
                Properties.Settings.Default.refresh = settings.Refresh;
                Properties.Settings.Default.skin = settings.Skin;
                Properties.Settings.Default.twitchChannel = settings.TwitchChannel;
                Properties.Settings.Default.twitchBotUsername = settings.TwitchBotUsername;
                Properties.Settings.Default.twitchBotToken = settings.TwitchBotToken;
                Properties.Settings.Default.twitchbotEnabled = settings.TwitchbotEnabled;

                Properties.Settings.Default.Save();
            };

            services.AddSingleton<IUserSettings, UserSettings>(c => settings);
            services.AddSingleton<IAppUserSettings, AppUserSettings>(c => settings);
        }
    }
}
