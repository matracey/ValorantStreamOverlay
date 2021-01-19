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
    }
}
