using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ValorantOverlay.Core.Models
{
    public class MatchesUpdate
    {
        [JsonPropertyName("Version")]
        public int Version { get; set; }

        [JsonPropertyName("Subject")]
        public string Subject { get; set; }

        [JsonPropertyName("Matches")]
        public List<Match> Matches { get; set; }
    }
}
