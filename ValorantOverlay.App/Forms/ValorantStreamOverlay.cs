using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ValorantOverlay.App.Fonts;
using ValorantOverlay.App.Properties;
using ValorantOverlay.App.Services;
using ValorantOverlay.Core.Exceptions;
using ValorantOverlay.Core.Models;
using ValorantOverlay.Core.Services;
using LoadingIndicator.WinForms;
using ValorantOverlay.App.Models;

namespace ValorantOverlay.App.Forms
{
    public partial class ValorantStreamOverlay : Form
    {
        private readonly SettingsForm _settingsForm;
        private readonly IAppUserSettings _userSettings;
        private readonly IUpdateService _updateService;
        private readonly AntonRegular _font;
        private readonly IStateManager _stateManager;
        private readonly ITwitchManager _twitchService;
        private LongOperation _longOperation;

        public ValorantStreamOverlay(SettingsForm settingsForm, IAppUserSettings userSettings, IUpdateService updateService, AntonRegular font, IStateManager stateManager, ITwitchManager twitchService)
        {
            _settingsForm = settingsForm;
            _userSettings = userSettings;
            _updateService = updateService;
            _font = font;
            _stateManager = stateManager;
            _twitchService = twitchService;
            _userSettings.UserSettingsUpdated += UserSettings_Updated;
            _stateManager.RecentMatchesUpdating += StateManager_RecentMatchesUpdating;
            _stateManager.RecentMatchesUpdated += StateManager_RecentMatchesUpdated;
            _stateManager.RankUpdating += StateManager_RankUpdating;
            _stateManager.RankUpdated += StateManager_RankUpdated;
            _twitchService.ExceptionThrown += TwitchService_ExceptionThrown;
            Shown += ValorantStreamOverlay_Shown;
            InitializeComponent();
        }

        private void UserSettings_Updated(IAppUserSettings userSettings)
        {
            backgroundPic.Image = userSettings.Skin?.Value ?? Skin.BackgroundRed.Value;
            _twitchService.Terminate();
            if (userSettings.TwitchbotEnabled)
            {
                try
                {
                    _twitchService.Initialize();
                } catch (Exception ex)
                {
                    _userSettings.TwitchbotEnabled = false;
                    _userSettings.Save();
                    MessageBox.Show($"Error connecting to Twitch. Make sure token and username are correct! Technical details: {ex.Message}");
                }
            }
        }

        #region Form Events
        private void ValorantStreamOverlay_Load(object sender, EventArgs e)
        {
            _longOperation = new LongOperation(mainPanel);
            backgroundPic.ContextMenuStrip = contextMenu;
            rankingLabel.Parent = backgroundPic;
            rankingLabel.BackColor = Color.Transparent;

            //On Load, Set backing and Fonts to labels displaying Rank changes.
            Label[] rankChanges = { recentGame1, recentGame2, recentGame3 };
            foreach (var recentC in rankChanges)
            {
                recentC.Font = _font.Regular;
                recentC.Parent = backgroundPic;
                recentC.BackColor = Color.Transparent;
            }
            rankingLabel.Font = _font.Heading1;
            rankIconBox.Parent = backgroundPic;
            rankIconBox.BackColor = Color.Transparent;

            //Add Rank elo point label, set font and parent..
            rankPointsElo.Font = _font.RegularPlus;
            rankPointsElo.BackColor = Color.Transparent;
            rankPointsElo.Parent = backgroundPic;
            UserSettings_Updated(_userSettings);
        }

        private async void ValorantStreamOverlay_Shown(object sender, EventArgs e)
        {
            using (_longOperation.Start())
            {
                _updateService.Start();
                try
                {
                    await _stateManager.InitializeAsync();
                }
                catch (UserLoginNotSetException)
                {
                    MessageBox.Show("Welcome! You must set your username and password in the settings menu.");
                    _settingsForm.ShowDialog();
                }
                catch (UserLoginInvalidException)
                {
                    MessageBox.Show("Login is invalid. Please fix login in settings.");
                    _settingsForm.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error has occurred. Technical details: {ex.Message}");
                }
            }
            _stateManager.ExceptionThrown += StateManager_ExceptionThrown;
        }

        private void SettingsMenuItem_Click(object sender, EventArgs e)
        {
            _settingsForm.ShowDialog();
        }
        #endregion

        #region StateManager Events

        private void StateManager_RecentMatchesUpdating()
        {
            _longOperation.Start();
        }

        private void StateManager_RecentMatchesUpdated(IEnumerable<Match> recentMatches)
        {
            var competitiveMatches = recentMatches.Where(m => m.TierAfterUpdate != 0).ToList();
            var labels = new List<Label> { recentGame1, recentGame2, recentGame3 };
            for (int i = 0; i < labels.Count; i++)
            {
                var match = competitiveMatches.ElementAtOrDefault(i);
                var label = labels.ElementAtOrDefault(i);
                var isLoss = match?.PointsEarned < 0;
                var points = isLoss ? match.PointsEarned * -1 : match?.PointsEarned ?? 0;


                if (points == 0)
                {
                    label.ForeColor = Color.SlateGray;
                    label.Text = $"{points:00}";
                    continue;
                }
                label.Text = $"{(isLoss ? "-" : "+")}{points:00}";
                label.ForeColor = isLoss ? Color.Red : Color.LimeGreen;
            }
            _longOperation.Stop();
        }

        private void StateManager_RankUpdating()
        {
            _longOperation.Start();
        }

        private void StateManager_RankUpdated((int rankNumber, int currentRp) obj)
        {
            rankingLabel.Text = _stateManager.CurrentRankName;

            var resource = Resources.ResourceManager.GetObject($"TX_CompetitiveTier_Large_{_stateManager.CurrentRank}");
            rankIconBox.Image = (Bitmap)resource;

            rankPointsElo.Text = $"{_stateManager.CurrentRp} RR | {_stateManager.CurrentElo} TRR";
            Cursor.Current = Cursors.Arrow;
            _longOperation.Stop();
        }

        private void StateManager_ExceptionThrown(Exception ex)
        {
            if (ex is UserLoginNotSetException)
            {
                MessageBox.Show("Welcome! You must set your username and password in the settings menu.");
                return;
            }
            if (ex is UserLoginInvalidException)
            {
                MessageBox.Show("Login is invalid. Please fix login in settings.");
                return;
            }
            MessageBox.Show($"An error has occurred. Technical details: {ex.Message}");
        }
        #endregion

        #region TwitchService Events
        private void TwitchService_ExceptionThrown(Exception ex)
        {
            MessageBox.Show($"Error connecting to Twitch. Make sure token and username are correct! Technical details: {ex.Message}");
        }
        #endregion
    }
}
