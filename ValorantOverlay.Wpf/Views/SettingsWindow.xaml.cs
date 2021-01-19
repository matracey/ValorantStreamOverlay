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
            ToggleEditMode(false);
        }

        private void SkinCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ToggleColorPickerVisibility(e.AddedItems.Cast<Skin>().Any(s => s.Text == "Custom"));
        }

        private void AddAccountButton_Click(object sender, RoutedEventArgs e)
        {
            if (settingsVm.SelectedUserAccount == null)
            {
                settingsVm.UserAccounts.Add(settingsVm.ToUserAccount());
            } else
            {
                settingsVm.SelectedUserAccount.ApplyValues(settingsVm.ToUserAccount());
            }

            ToggleEditMode(false);
            settingsVm.Clear();
        }

        private void CancelEditButton_Click(object sender, RoutedEventArgs e)
        {
            settingsVm.SelectedUserAccount = null;
            ToggleEditMode(false);
            settingsVm.Clear();
        }

        private void EditUserAccountButton_Click(object sender, RoutedEventArgs e)
        {
            var model = (sender as Button).DataContext as UserAccount;
            settingsVm.SelectedUserAccount = model;
            ToggleEditMode(true);
        }

        private void RemoveUserAccountButton_Click(object sender, RoutedEventArgs e)
        {
            var model = (sender as Button).DataContext as UserAccount;
            if (settingsVm.SelectedUserAccount?.Id == model?.Id)
            {
                settingsVm.SelectedUserAccount = null;
                ToggleEditMode(false);
            }
            settingsVm.UserAccounts.Remove(model);
        }

        private void ToggleColorPickerVisibility(bool isShown)
        {
            skinColorPicker.Visibility = isShown ? Visibility.Visible : Visibility.Collapsed;
            skinCombobox.SetValue(Grid.ColumnSpanProperty, isShown ? 1 : 2);
        }

        private void ToggleEditMode(bool isEditing)
        {
            cancelEditButton.Visibility = isEditing ? Visibility.Visible : Visibility.Collapsed;
            addAccountButton.SetValue(Grid.ColumnSpanProperty, isEditing ? 1 : 2);
            addAccountButton.Margin = new Thickness(0, 5, isEditing ? 2.5 : 0, 5);
            addAccountButton.Content = isEditing ? "Save" : "Add Account";
        }
    }
}
