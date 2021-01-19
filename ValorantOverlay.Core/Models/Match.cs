using System;
using System.Text.Json.Serialization;

namespace ValorantOverlay.Core.Models
{
    public class Match
    {
        [JsonPropertyName("MatchID")]
        public string Id { get; set; }

        [JsonPropertyName("MapID")]
        public string MapId { get; set; }

        [JsonPropertyName("MatchStartTime")]
        private long MatchStartTime { get; set; }

        public DateTime StartTime
        {
            get { return DateTimeOffset.FromUnixTimeMilliseconds(MatchStartTime).DateTime; }
            set { MatchStartTime = new DateTimeOffset(value).ToUnixTimeMilliseconds(); }
        }

        [JsonPropertyName("TierAfterUpdate")]
        public int TierAfterUpdate { get; set; }

        [JsonPropertyName("TierBeforeUpdate")]
        public int TierBeforeUpdate { get; set; }

        [JsonPropertyName("RankedRatingAfterUpdate")]
        public int RankedRatingAfterUpdate { get; set; }

        [JsonPropertyName("RankedRatingBeforeUpdate")]
        public int RankedRatingBeforeUpdate { get; set; }

        [JsonPropertyName("RankedRatingEarned")]
        public int RankedRatingEarned { get; set; }

        public int PointsEarned 
        {
            get {
                var grossChange = RankedRatingAfterUpdate - RankedRatingBeforeUpdate;
                if (TierAfterUpdate > TierBeforeUpdate)
                {
                    return grossChange + 100;
                }
                if (TierAfterUpdate < TierBeforeUpdate)
                {
                    return grossChange - 100;
                }
                return TierAfterUpdate == 0 ? 0 : grossChange;
            }
        }
    }
}
