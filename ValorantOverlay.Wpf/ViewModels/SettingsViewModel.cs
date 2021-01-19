using System.Collections.Generic;
using ValorantOverlay.Core.Models;

namespace ValorantOverlay.Wpf.ViewModels
{
    internal class SettingsViewModel : UserAccount
    {
        public IList<Region> Regions => Region.All;
    }
}
