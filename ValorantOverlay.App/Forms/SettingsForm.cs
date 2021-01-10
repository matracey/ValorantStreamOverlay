using System;
using System.Windows.Forms;
using ValorantOverlay.App.Models;

namespace ValorantOverlay.App.Forms
{
    public partial class SettingsForm : Form
    {
        private IAppUserSettings _userSettings;

        public SettingsForm(IAppUserSettings userSettings)
        {
            _userSettings = userSettings;
            InitializeComponent();
        }

        void ApplySettings()
        {
            // Save Overlay Settings
            _userSettings.Username = usernameTextBox.Text;
            _userSettings.Password = passwordTextBox.Text;
            _userSettings.Region = regionDropdown.SelectedIndex;
            _userSettings.Skin = skinDropdown.SelectedIndex;
            _userSettings.Refresh = refreshDropdown.SelectedIndex;

            // Save Twitch bot settings
            _userSettings.TwitchbotEnabled = twitchBotCheckbox.Checked;
            _userSettings.TwitchBotToken = twitchBotTokenTextbox.Text;
            _userSettings.TwitchBotUsername = twitchbotUsernameTextbox.Text;
            _userSettings.TwitchChannel = twitchChannelNameTextbox.Text;

            _userSettings.Save();
            Close();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            //Overlay Loading
            usernameTextBox.Text = _userSettings.Username;
            passwordTextBox.Text = _userSettings.Password;
            regionDropdown.SelectedIndex = _userSettings.Region;
            skinDropdown.SelectedIndex = _userSettings.Skin;
            refreshDropdown.SelectedIndex = _userSettings.Refresh;
            
            //Twitch Bot Loading
            twitchChannelNameTextbox.Text = _userSettings.TwitchChannel;
            twitchBotTokenTextbox.Text = _userSettings.TwitchBotToken;
            twitchbotUsernameTextbox.Text = _userSettings.TwitchBotUsername;
            twitchBotCheckbox.Checked = _userSettings.TwitchbotEnabled;
        }

        private void ApplyButton_Click(object sender, EventArgs e)
        {
            ApplySettings();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
