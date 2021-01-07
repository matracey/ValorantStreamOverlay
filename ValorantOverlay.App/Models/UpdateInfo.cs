using System.Text.Json.Serialization;

namespace ValorantOverlay.App.Models
{
    public class UpdateInfo
    {
        [JsonPropertyName("version")]
        public string Version { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("changelog")]
        public string Changelog { get; set; }

        [JsonPropertyName("mandatory")]
        public MandatoryUpdate Mandatory { get; set; }
    }

    public class MandatoryUpdate
    {
        [JsonPropertyName("value")]
        public bool Value { get; set; }

        [JsonPropertyName("MinimumVersion")]
        public string MinimumVersion { get; set; }

        [JsonPropertyName("UpdateMode")]
        public int UpdateMode { get; set; }
    }
}
