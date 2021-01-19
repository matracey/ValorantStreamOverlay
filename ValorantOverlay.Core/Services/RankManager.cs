using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Logging;
using ValorantOverlay.Core.Models;

namespace ValorantOverlay.Core.Services
{
    public interface IRankManager
    {
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
        /// Fetches the latest RankedRating details from the Valorant API.
        /// </summary>
        /// <param name="region">The region to query for the given UserId.</param>
        /// <param name="userId">The UserId to fetch the RankedRating details.</param>
        /// <param name="accessToken">The AccessToken to use to authorize the API request.</param>
        /// <param name="entitlementToken">The JWT entitlement token to include in the API request.</param>
        /// <returns>A tuple containing TierAfterUpdate and RankedRatingAfterUpdate</returns>
        Task<(int, int)> UpdateCompetitiveRankAsync(Region region, string userId, string accessToken, string entitlementToken, int skip = 0, int pageSize = 20);
        /// <summary>
        /// Updates the RankedRating details from the specified MatchesUpdate parameter.
        /// </summary>
        /// <param name="matchesUpdate">The MatchesUpdate parameter to process.</param>
        /// <returns>A tuple containing TierAfterUpdate and RankedRatingAfterUpdate</returns>
        (int, int) UpdateCompetitiveRank(MatchesUpdate matchesUpdate);
        /// <summary>
        /// Clears the current Rank state.
        /// </summary>
        void ClearState();
        /// <summary>
        /// An event that is triggered before the Player's updated Rank is loaded.
        /// </summary>
        event Action RankUpdating;
        /// <summary>
        /// An event that is triggered after the Player's updated Rank is loaded.
        /// </summary>
        event Action<(int rankNumber, int currentRp)> RankUpdated;
    }

    public class RankManager : IRankManager
    {
        private readonly IMatchManager _matchManager;
        private readonly ILogger<RankManager> _logger;
        private readonly IRankList _rankList;
        private (int rankNumber, int currentRp) Rank;

        public RankManager(IRankList rankList, IMatchManager matchManager, ILogger<RankManager> logger)
        {
            _matchManager = matchManager;
            _logger = logger;
            _rankList = rankList;
        }

        /// <inheritdoc/>
        public int CurrentRank
        {
            get => Rank.rankNumber;
        }

        /// <inheritdoc/>
        public string CurrentRankName
        {
            get => _rankList.GetRankByIndex(Rank.rankNumber).ToUpper();
        }

        /// <inheritdoc/>
        public int CurrentRp
        {
            get => Rank.currentRp;
        }

        /// <inheritdoc/>
        public int CurrentElo
        {
            get => (Rank.rankNumber * 100) - 300 + Rank.currentRp;
        }

        /// <inheritdoc/>
        public event Action RankUpdating;
        /// <inheritdoc/>
        public event Action<(int rankNumber, int currentRp)> RankUpdated;

        /// <inheritdoc/>
        public async Task<(int, int)> UpdateCompetitiveRankAsync(Region region, string userId, string accessToken, string entitlementToken, int skip = 0, int pageSize = 20)
        {
            RankUpdating?.Invoke();
            var matchesUpdate = await _matchManager.GetAllGameUpdatesAsync(region, userId, accessToken, entitlementToken, skip, pageSize);
            RankUpdated?.Invoke(Rank);
            return UpdateCompetitiveRank(matchesUpdate);
        }

        /// <inheritdoc/>
        public (int, int) UpdateCompetitiveRank(MatchesUpdate matchesUpdate)
        {
            RankUpdating?.Invoke();
            var latestGame = matchesUpdate?.Matches.OrderBy(m => m.StartTime).FirstOrDefault();

            if (latestGame != null)
            {
                Rank = (latestGame.TierAfterUpdate, latestGame.RankedRatingAfterUpdate);
                RankUpdated?.Invoke(Rank);
                return Rank;
            }

            _logger.LogError("An error occurred while attempting to update the competitive rank.");
            return (0, 0);
        }

        public void ClearState()
        {
            Rank = (0, 0);
        }
    }
}
