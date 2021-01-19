using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ValorantOverlay.Wpf.Models;
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

        private void SettingsWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ToggleColorPickerVisibility(false);
        }

        private void SkinCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ToggleColorPickerVisibility(e.AddedItems.Cast<Skin>().Any(s => s.Text == "Custom"));
        }

        private void AddAccountButton_Click(object sender, RoutedEventArgs e)
        {
            settingsVm.UserAccounts.Add(settingsVm.ToUserAccount());
            settingsVm.Clear();
        }

        private void ToggleColorPickerVisibility(bool isShown)
        {
            skinColorPicker.Visibility = isShown ? Visibility.Visible : Visibility.Collapsed;
            skinCombobox.SetValue(Grid.ColumnSpanProperty, isShown ? 1 : 2);
        }
    }
}
