using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ValorantOverlay.Core.Exceptions;
using ValorantOverlay.Core.Models;
using ValorantOverlay.Core.Services;

namespace ValorantOverlay.Wpf.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly SettingsWindow _settingsWindow;
        private readonly IStateManager _stateManager;

        public MainWindow(SettingsWindow settingsWindow, IStateManager stateManager)
        {
            InitializeComponent();

            _settingsWindow = settingsWindow;

            _stateManager = stateManager;
            _stateManager.RecentMatchesUpdating += StateManager_RecentMatchesUpdating;
            _stateManager.RecentMatchesUpdated += StateManager_RecentMatchesUpdated;
            _stateManager.RankUpdating += StateManager_RankUpdating;
            _stateManager.RankUpdated += StateManager_RankUpdated;

            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                await _stateManager.InitializeAsync();
            }
            catch (UserLoginNotSetException)
            {
                MessageBox.Show("Welcome! You must set your username and password in the settings menu.");
                _settingsWindow.Show();
            }
            catch (UserLoginInvalidException)
            {
                MessageBox.Show("Login is invalid. Please fix login in settings.");
                _settingsWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error has occurred. Technical details: {ex.Message}");
            }
        }

        #region StateManager Events

        private void StateManager_RecentMatchesUpdating()
        {
        }

        private void StateManager_RecentMatchesUpdated(IEnumerable<Match> recentMatches)
        {
            var competitiveMatches = recentMatches.Where(m => m.TierAfterUpdate != 0).ToList();
            var labels = new List<TextBlock> { recentGame1, recentGame2, recentGame3 };
            for (int i = 0; i < labels.Count(); i++)
            {
                var match = competitiveMatches.ElementAtOrDefault(i);
                var label = labels.ElementAtOrDefault(i);
                var isLoss = match?.PointsEarned < 0;
                var points = isLoss ? match.PointsEarned * -1 : match?.PointsEarned ?? 0;


                if (points == 0)
                {
                    label.Foreground = Brushes.SlateGray;
                    label.Text = $"{points:00}";
                    continue;
                }
                label.Text = $"{(isLoss ? "-" : "+")}{points:00}";
                label.Foreground = isLoss ? Brushes.Red : Brushes.LimeGreen;
            }
        }

        private void StateManager_RankUpdating()
        {
        }

        private void StateManager_RankUpdated((int rankNumber, int currentRp) obj)
        {
            rankingLabel.Text = _stateManager.CurrentRankName;

            var bitmap = (BitmapImage)FindResource($"TX_CompetitiveTier_Large_{_stateManager.CurrentRank:00}");
            rankIconBox.Source = bitmap;

            rankPointsElo.Text = $"{_stateManager.CurrentRp} RR | {_stateManager.CurrentElo} TRR";
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
    }
}
