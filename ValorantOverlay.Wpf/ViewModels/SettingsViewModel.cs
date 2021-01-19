using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using ValorantOverlay.Core.Models;
using ValorantOverlay.Wpf.Models;

namespace ValorantOverlay.Wpf.ViewModels
{
    internal class SettingsViewModel : UserAccount
    {
        public IList<Region> Regions => Region.All;

        public IList<Skin> Skins => Skin.All;

        public IDictionary<string, int> RefreshIntervals => _refreshIntervals.ToDictionary(x => $"{x} seconds");

        public Color? SelectedColor
        {
            get => Skin?.Value?.Color;
            set
            {
                if (Skin?.Text == "Custom" && value.HasValue)
                {
                    Skin.Value = new SolidColorBrush(value.Value);
                } else
                {
                    Skin = value.HasValue ? new Skin(new SolidColorBrush(value.Value), "Custom") : Skin.BackgroundRed;
                }
            }
        }

        public int SelectedSkin
        {
            get => Skin.All.ToList().FindIndex(s => s.Text == Skin.Text);
            set
            {
                var newSkin = Skin.GetByIndex(value);
                Skin = Skin.Text != newSkin.Text ? newSkin : Skin;
            }
        }

        public override Skin Skin
        {
            get => _skin;
            set
            {
                if (value != _skin)
                {
                    _skin = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged("SelectedSkin");
                    NotifyPropertyChanged("SelectedColor");
                }
            }
        }

        public KeyValuePair<string, int> SelectedRefreshInterval
        {
            get => new KeyValuePair<string, int>($"{RefreshInterval} seconds", RefreshInterval);
            set => RefreshInterval = value.Value;
        }

        public override int RefreshInterval
        {
            get => _refreshInterval;
            set
            {
                if (value != _refreshInterval)
                {
                    _refreshInterval = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged("SelectedRefreshInterval");
                }
            }
        }

        private readonly IList<int> _refreshIntervals = new List<int> { 30, 60 };
    }
}
