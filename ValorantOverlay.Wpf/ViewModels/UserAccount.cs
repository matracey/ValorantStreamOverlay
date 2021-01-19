using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ValorantOverlay.Core.Models;
using ValorantOverlay.Wpf.Models;

namespace ValorantOverlay.Wpf.ViewModels
{
    internal class UserAccount : INotifyPropertyChanged
    {
        public UserAccount()
        {
            Region = Region.NorthAmerica;
            Skin = Skin.BackgroundRed;
            RefreshInterval = 30;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Guid Id => _id;

        protected string _username;
        public virtual string Username
        {
            get => _username;
            set
            {
                if (value != _username)
                {
                    _username = value;
                    NotifyPropertyChanged();
                }
            }
        }

        protected string _password;
        public virtual string Password
        {
            get => _password;
            set
            {
                if (value != _password)
                {
                    _password = value;
                    NotifyPropertyChanged();
                }
            }
        }
        protected Region _region;
        public virtual Region Region
        {
            get => _region;
            set
            {
                if (value != _region)
                {
                    _region = value;
                    NotifyPropertyChanged();
                }
            }
        }
        protected Skin _skin;
        public virtual Skin Skin
        {
            get => _skin;
            set
            {
                if (value != _skin)
                {
                    _skin = value;
                    NotifyPropertyChanged();
                }
            }
        }
        protected int _refreshInterval;
        private Guid _id = Guid.NewGuid();

        public virtual int RefreshInterval
        {
            get => _refreshInterval;
            set
            {
                if (value != _refreshInterval)
                {
                    _refreshInterval = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public void ApplyValues(UserAccount userAccount)
        {
            Username = userAccount?.Username;
            Password = userAccount?.Password;
            Region = userAccount?.Region ?? Region.NorthAmerica;
            Skin = userAccount?.Skin ?? Skin.BackgroundRed;
            RefreshInterval = userAccount?.RefreshInterval ?? 30;
        }
    }
}
