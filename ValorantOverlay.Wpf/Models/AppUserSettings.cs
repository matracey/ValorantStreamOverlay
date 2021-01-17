using System;
using System.Linq;
using ValorantOverlay.Core.Models;

namespace ValorantOverlay.Wpf.Models
{
    public interface IAppUserSettings : IUserSettings
    {
        Skin Skin { get; set; }
        new event Action<IAppUserSettings> UserSettingsUpdated;
    }

    public class AppUserSettings : UserSettings, IAppUserSettings
    {
        protected new AppUserSettings _changeStaging;

        public AppUserSettings() : this(true)
        {
        }

        public AppUserSettings(bool isRoot = true) : base (false)
        {
            if (isRoot)
            {
                _changeStaging = new AppUserSettings(false)
                {
                    _username = _username,
                    _password = _password,
                    _region = _region,
                    _refresh = _refresh,
                    _twitchChannel = _twitchChannel,
                    _twitchBotUsername = _twitchBotUsername,
                    _twitchBotToken = _twitchBotToken,
                    _twitchbotEnabled = _twitchbotEnabled,
                    _skin = _skin
                };

                base._changeStaging = _changeStaging;
            }
        }

        private Skin _skin;
        public Skin Skin
        {
            get
            {
                return _skin;
            }
            set
            {
                if (_changeStaging != null)
                {
                    _changeStaging._skin = value;
                }
                else
                {
                    _skin = value;
                }
            }
        }

        public override bool IsDirty => new[]
            {
                base.IsDirty,
                _skin != _changeStaging._skin || _skin?.Value != _changeStaging._skin?.Value
            }.Any();

        public new event Action<IAppUserSettings> UserSettingsUpdated;

        public override void Save(bool invokeEvent = true)
        {
            if (IsDirty)
            {
                base.Save(false);
                _skin = _changeStaging._skin;

                UserSettingsUpdated?.Invoke(this);
            }
        }
    }
}
