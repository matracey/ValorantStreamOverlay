using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Timers;
using ValorantOverlay.Core.Exceptions;
using ValorantOverlay.Core.Models;

namespace ValorantOverlay.Core.Services
{
    public interface IStateManager
    {
        Region Region { get; }
        int RefreshTimeInSeconds { get; }
        bool BotEnabled { get; }
        string UserId { get; }
        IEnumerable<Match> RecentMatches { get; }
        /// <summary>
        /// The Player's current Rank.
        /// </summary>
        int CurrentRank { get; }
        /// <summary>
        /// The Player's current Rank name.
        /// </summary>
        string CurrentRankName { get; }
        /// <summary>
        /// The Player's current RR.
        /// </summary>
        int CurrentRp { get; }
        /// <summary>
        /// The Player's current Elo rating.
        /// </summary>
        int CurrentElo { get; }

        /// <summary>
        /// An event that is triggered before the Player's Recent Matches are updated.
        /// </summary>
        event Action RecentMatchesUpdating;

        /// <summary>
        /// An event that is triggered after the Player's Recent Matches are updated.
        /// </summary>
        event Action<IEnumerable<Match>> RecentMatchesUpdated;

        /// <summary>
        /// An event that is triggered before the Player's Current Rank is updated.
        /// </summary>
        event Action RankUpdating;

        /// <summary>
        /// An event that is triggered after the Player's Current Rank is updated.
        /// </summary>
        event Action<(int rankNumber, int currentRp)> RankUpdated;

        /// <summary>
        /// An event that is triggered when an Exception is thrown during the StateManager's operation.
        /// </summary>
        event Action<Exception> ExceptionThrown;
        Task InitializeAsync();
        Task RefreshAsync();
    }

    public class StateManager : IStateManager
    {
        private RiotAuthentication _auth;
        private string _entitlementToken;
        private RiotUser _riotUser;
        private MatchesUpdate _games;
        private Timer _matchRefreshTimer;
        private Timer _loginRefreshTimer;

        private readonly ILogger<StateManager> _logger;
        private readonly IUserSettings _userSettings;
        private readonly IMatchManager _matchManager;
        private readonly IRankManager _rankManager;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IRiotAuthenticator _riotAuthenticator;

        public StateManager(ILogger<StateManager> logger, IUserSettings userSettings, IMatchManager matchManager, IRankManager rankManager, IHttpClientFactory httpClientFactory, IRiotAuthenticator riotAuthenticator)
        {
            _logger = logger;
            _userSettings = userSettings;
            _matchManager = matchManager;
            _rankManager = rankManager;
            _httpClientFactory = httpClientFactory;
            _riotAuthenticator = riotAuthenticator;

            _rankManager.RankUpdating += RankManager_RankUpdating;
            _rankManager.RankUpdated += RankManager_RankUpdated;
            _userSettings.UserSettingsUpdated += UserSettings_Updated;
        }

        public Region Region => _userSettings.Region;

        public int RefreshTimeInSeconds => Region == "eu" ? 60 : 30;

        public bool BotEnabled => _userSettings.TwitchbotEnabled && new[] { _userSettings.TwitchBotUsername, _userSettings.TwitchChannel, _userSettings.TwitchBotToken }.Any(x => string.IsNullOrEmpty(x));

        public string UserId => _riotUser?.Id;

        public IEnumerable<Match> RecentMatches => _games?.Matches;

        /// <inheritdoc/>
        public int CurrentRank
        {
            get => _rankManager.CurrentRank;
        }

        /// <inheritdoc/>
        public string CurrentRankName
        {
            get => _rankManager.CurrentRankName;
        }

        /// <inheritdoc/>
        public int CurrentRp
        {
            get => _rankManager.CurrentRp;
        }

        /// <inheritdoc/>
        public int CurrentElo
        {
            get => _rankManager.CurrentElo;
        }

        public event Action RecentMatchesUpdating;
        public event Action<IEnumerable<Match>> RecentMatchesUpdated;

        public event Action RankUpdating;
        public event Action<(int rankNumber, int currentRp)> RankUpdated;

        public event Action<Exception> ExceptionThrown;

        public async Task InitializeAsync()
        {
            ClearState();
            if (string.IsNullOrEmpty(_userSettings.Password) || string.IsNullOrEmpty(_userSettings.Username))
            {
                throw new UserLoginNotSetException();
            }
            await LoginToRiotGamesAsync();
            await UpdateMatchesAsync();

            StartMatchRefreshTimer();
            StartLoginRefreshTimer();
        }

        public async Task RefreshAsync()
        {
            await UpdateMatchesAsync();
        }

        public void ClearState()
        {
            RecentMatchesUpdating?.Invoke();
            _games = new MatchesUpdate
            {
                Matches = new List<Match>()
            };
            _rankManager.ClearState();
            RecentMatchesUpdated?.Invoke(RecentMatches);
        }

        private async Task LoginToRiotGamesAsync()
        {
            _riotAuthenticator.VoidCookies();
            try
            {
                await _riotAuthenticator.GetAuthorizationAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred when attempting to load authorization from Riot.");
                throw;
            }

            try
            {
                _auth = await _riotAuthenticator.GetAuthenticationTokensAsync(_userSettings.Username, _userSettings.Password);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred when attempting to load authentication token from Riot.");
                throw;
            }

            if (_auth == null)
            {
                throw new UserLoginInvalidException();
            }

            try
            {
                _entitlementToken = await _riotAuthenticator.GetEntitlementsTokenAsync(_auth.AccessToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while attempting to load entitlements authentication token from Riot.");
                throw;
            }

            var client = _httpClientFactory.CreateClient(HttpClientName.AuthRiot);
            var request = new HttpRequestMessage(HttpMethod.Post, "/userinfo");

            request.Headers.Add("Authorization", $"Bearer {_auth.AccessToken}");
            request.Content = new StringContent("{}");

            HttpResponseMessage response;
            try
            {
                response = await client.SendAsync(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while attempting to load user info from Riot.");
                throw;
            }

            var responseBody = await response.Content.ReadAsStringAsync();
            _riotUser = JsonSerializer.Deserialize<RiotUser>(responseBody);
        }

        private async Task UpdateMatchesAsync()
        {
            RecentMatchesUpdating?.Invoke();
            _games = await _matchManager.GetAllGameUpdatesAsync(Region, _riotUser.Id, _auth.AccessToken, _entitlementToken);
            _rankManager.UpdateCompetitiveRank(_games);
            RecentMatchesUpdated?.Invoke(RecentMatches);
        }

        private void StartMatchRefreshTimer()
        {
            if (_matchRefreshTimer == null)
            {
                _matchRefreshTimer = new Timer();
                _matchRefreshTimer.Elapsed += MatchRefreshTimer_Elapsed;
            }
            else
            {
                _matchRefreshTimer.Stop();
            }
            _matchRefreshTimer.Interval = RefreshTimeInSeconds * 1000;
            _matchRefreshTimer.Start();
        }

        private async void MatchRefreshTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                await RefreshAsync();
            } catch (Exception ex)
            {
                _logger.LogError(ex, "An error has occurred while attempting to refresh Match details.");
                ExceptionThrown?.Invoke(ex);
                return;
            }
        }

        private void StartLoginRefreshTimer()
        {
            if (_loginRefreshTimer == null)
            {
                _loginRefreshTimer = new Timer();
                _loginRefreshTimer.Elapsed += LoginRefreshTimer_Elapsed;
            }
            else
            {
                _loginRefreshTimer.Stop();
            }
            _loginRefreshTimer.Interval = 2700 * 1000;
            _loginRefreshTimer.Start();

        }

        private async void LoginRefreshTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                await InitializeAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error has occurred while attempting to re-initialize the State Manager.");
                ClearState();
                ExceptionThrown?.Invoke(ex);
                return;
            }
        }

        private void RankManager_RankUpdating()
        {
            RankUpdating?.Invoke();
        }

        private void RankManager_RankUpdated((int rankNumber, int currentRp) rank)
        {
            RankUpdated?.Invoke(rank);
        }

        private async void UserSettings_Updated(IUserSettings obj)
        {
            try
            {
                await InitializeAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error has occurred while attempting to re-initialize the State Manager.");
                ExceptionThrown?.Invoke(ex);
                return;
            }
        }
    }
}
