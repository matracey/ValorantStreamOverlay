using System.Windows;
using ValorantOverlay.Wpf.ViewModels;

namespace ValorantOverlay.Wpf.Views
{

    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private readonly SettingsViewModel settingsVm = new SettingsViewModel();

        public SettingsWindow()
        {
            InitializeComponent();
            DataContext = settingsVm;
        }
    }
}
