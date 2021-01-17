using System;
using TwitchLib.Client;
using TwitchLib.Client.Models;
using TwitchLib.Client.Events;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Models;
using ValorantOverlay.Core.Models;
using Microsoft.Extensions.Logging;

namespace ValorantOverlay.Core.Services
{
    public interface ITwitchManager
    {
        /// <summary>
        /// An event that is triggered when an Exception is thrown during the TwitchService's operation.
        /// </summary>
        event Action<Exception> ExceptionThrown;

        /// <summary>
        /// Initializes the Twitch service.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Terminates the Twitch service.
        /// </summary>
        void Terminate();
    }

    public class TwitchManager : ITwitchManager
    {
        private readonly IUserSettings _settings;
        private readonly IStateManager _stateManager;
        private readonly ILogger<TwitchManager> _logger;

        private TwitchClient _client;

        private static bool s_botHasAnnounced = false;

        public event Action<Exception> ExceptionThrown;

        public TwitchManager(IUserSettings settings, IStateManager stateManager, ILogger<TwitchManager> logger)
        {
            _settings = settings;
            _stateManager = stateManager;
            _logger = logger;
        }

        public void Initialize()
        {
            ConnectionCredentials credentials = new ConnectionCredentials(_settings.TwitchBotUsername, _settings.TwitchBotToken);
            ClientOptions clientOptions = new ClientOptions
            {
                MessagesAllowedInPeriod = 700,
                ThrottlingPeriod = TimeSpan.FromSeconds(30)
            };

            WebSocketClient socketClient = new WebSocketClient(clientOptions);
            _client = new TwitchClient(socketClient);
            _client.OnConnected += Client_OnConnected;
            _client.OnMessageReceived += Client_OnMessageRecieved;

            try
            {
                _logger.LogInformation("Twitch service initializing.");
                _client.Initialize(credentials, _settings.TwitchChannel);
            }
            catch (Exception e)
            {
                _settings.TwitchbotEnabled = false;
                _settings.Save();
                ExceptionThrown?.Invoke(e);
                _logger.LogError(e, "An error ocurred while initializing the Twitch service.");
            }

            // Connect Client to twitch chat.
            _client.Connect();
        }

        public void Terminate()
        {
            if (_client != null && _client.IsConnected)
            {
                _client.LeaveChannel(_settings.TwitchChannel);
                _client.Disconnect();
            }
        }

        void Client_OnConnected(object sender, OnConnectedArgs e)
        {
            if (!s_botHasAnnounced)
            {
                _client.SendMessage(e.AutoJoinChannel, "Valor Stream Overlay has Connected.");
                s_botHasAnnounced = true;
            }
        }

        void Client_OnMessageRecieved(object sender, OnMessageReceivedArgs e)
        {
            switch (e.ChatMessage.Message.ToLower())
            {
                case "!elo":
                    _client.SendMessage(e.ChatMessage.Channel, $"{e.ChatMessage.Channel} currently has {_stateManager.CurrentElo} Total Rank Rating");
                    break;
                case "!overlay":
                    _client.SendMessage(e.ChatMessage.Channel, $"Overlay and Bot were created by @Rumblemikee, you can find the program here: https://github.com/RumbleMike/ValorantStreamOverlay/releases/latest");
                    break;
                case "!rp":
                    _client.SendMessage(e.ChatMessage.Channel, $"{e.ChatMessage.Channel} currently has {_stateManager.CurrentRp} Rating in {_stateManager.CurrentRankName}");
                    break;
            }

        }

    }
}