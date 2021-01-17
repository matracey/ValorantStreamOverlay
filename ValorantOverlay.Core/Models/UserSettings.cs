using System;
using System.Linq;

namespace ValorantOverlay.Core.Models
{
    public interface IUserSettings
    {
        string Username { get; set; }

        string Password { get; set; }

        Region Region { get; set; }

        int Refresh { get; set; }

        string TwitchChannel { get; set; }

        string TwitchBotUsername { get; set; }

        string TwitchBotToken { get; set; }

        bool TwitchbotEnabled { get; set; }

        bool IsDirty { get; }

        void Save(bool invokeEvent = true);

        event Action<IUserSettings> UserSettingsUpdated;
    }

    public class UserSettings : IUserSettings
    {
        protected UserSettings _changeStaging;

        public UserSettings(bool isRoot = true)
        {
            if (isRoot)
            {
                _changeStaging = new UserSettings(false)
                {
                    _username = _username,
                    _password = _password,
                    _region = _region,
                    _refresh = _refresh,
                    _twitchChannel = _twitchChannel,
                    _twitchBotUsername = _twitchBotUsername,
                    _twitchBotToken = _twitchBotToken,
                    _twitchbotEnabled = _twitchbotEnabled
                };
            }
        }

        protected string _username;
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                if (_changeStaging != null)
                {
                    _changeStaging._username = value;
                }
                else
                {
                    _username = value;
                }
            }
        }

        protected string _password;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                if (_changeStaging != null)
                {
                    _changeStaging._password = value;
                }
                else
                {
                    _password = value;
                }
            }
        }

        protected Region _region;
        public Region Region
        {
            get
            {
                return _region;
            }
            set
            {
                if (_changeStaging != null)
                {
                    _changeStaging._region = value;
                }
                else
                {
                    _region = value;
                }
            }
        }

        protected int _refresh;
        public int Refresh
        {
            get
            {
                return _refresh;
            }
            set
            {
                if (_changeStaging != null)
                {
                    _changeStaging._refresh = value;
                }
                else
                {
                    _refresh = value;
                }
            }
        }

        protected string _twitchChannel;
        public string TwitchChannel
        {
            get
            {
                return _twitchChannel;
            }
            set
            {
                if (_changeStaging != null)
                {
                    _changeStaging._twitchChannel = value;
                }
                else
                {
                    _twitchChannel = value;
                }
            }
        }

        protected string _twitchBotUsername;
        public string TwitchBotUsername
        {
            get
            {
                return _twitchBotUsername;
            }
            set
            {
                if (_changeStaging != null)
                {
                    _changeStaging._twitchBotUsername = value;
                }
                else
                {
                    _twitchBotUsername = value;
                }
            }
        }

        protected string _twitchBotToken;
        public string TwitchBotToken
        {
            get
            {
                return _twitchBotToken;
            }
            set
            {
                if (_changeStaging != null)
                {
                    _changeStaging._twitchBotToken = value;
                }
                else
                {
                    _twitchBotToken = value;
                }
            }
        }

        protected bool _twitchbotEnabled;
        public bool TwitchbotEnabled
        {
            get
            {
                return _twitchbotEnabled;
            }
            set
            {
                if (_changeStaging != null)
                {
                    _changeStaging._twitchbotEnabled = value;
                }
                else
                {
                    _twitchbotEnabled = value;
                }
            }
        }

        public virtual bool IsDirty => new[]
            {
                _username != _changeStaging._username,
                _password != _changeStaging._password,
                _region != _changeStaging._region,
                _refresh != _changeStaging._refresh,
                _twitchChannel != _changeStaging._twitchChannel,
                _twitchBotUsername != _changeStaging._twitchBotUsername,
                _twitchBotToken != _changeStaging._twitchBotToken,
                _twitchbotEnabled != _changeStaging._twitchbotEnabled
            }.Any();

        public virtual event Action<IUserSettings> UserSettingsUpdated;

        public virtual void Save(bool invokeEvent = true)
        {
            if (IsDirty)
            {
                _username = _changeStaging._username;
                _password = _changeStaging._password;
                _region = _changeStaging._region;
                _refresh = _changeStaging._refresh;
                _twitchChannel = _changeStaging._twitchChannel;
                _twitchBotUsername = _changeStaging._twitchBotUsername;
                _twitchBotToken = _changeStaging._twitchBotToken;
                _twitchbotEnabled = _changeStaging._twitchbotEnabled;

                UserSettingsUpdated?.Invoke(this);
            }
        }
    }
}