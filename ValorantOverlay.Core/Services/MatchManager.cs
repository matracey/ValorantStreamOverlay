using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ValorantOverlay.Core.Models;

namespace ValorantOverlay.Core.Services
{
    public interface IMatchManager
    {
        Task<MatchesUpdate> GetAllGameUpdatesAsync(Region region, string userId, string accessToken, string entitlementToken, int skip = 0, int pageSize = 20);
    }

    public class MatchManager : IMatchManager
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<MatchManager> _logger;

        public MatchManager(IHttpClientFactory clientFactory, ILogger<MatchManager> logger)
        {
            _clientFactory = clientFactory;
            _logger = logger;
        }

        /// <summary>
        /// Fetches the latest RankedRating details from the Valorant API.
        /// </summary>
        /// <param name="region">The region to query for the given UserId.</param>
        /// <param name="userId">The UserId to fetch the RankedRating details.</param>
        /// <param name="accessToken">The AccessToken to use to authorize the API request.</param>
        /// <param name="entitlementToken">The JWT entitlement token to include in the API request.01</param>
        /// <returns>A tuple containing TierAfterUpdate and RankedRatingAfterUpdate</returns>
        public async Task<MatchesUpdate> GetAllGameUpdatesAsync(Region region, string userId, string accessToken, string entitlementToken, int skip = 0, int pageSize = 20)
        {
            var client = _clientFactory.CreateClient(region);
            var request = new HttpRequestMessage(HttpMethod.Get, $"mmr/v1/players/{userId}/competitiveupdates?startIndex={skip}&endIndex={pageSize}");
            request.Headers.Add("Authorization", $"Bearer {accessToken}");
            request.Headers.Add("X-Riot-Entitlements-JWT", entitlementToken);

            try
            {
                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<MatchesUpdate>(content);
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while attempting to update the competitive rank.");
                return null;
            }
        }
    }
}
