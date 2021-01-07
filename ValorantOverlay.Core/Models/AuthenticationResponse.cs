using System.Text.Json.Serialization;

namespace ValorantOverlay.Core.Models
{
    internal class Parameters
    {
        [JsonPropertyName("uri")]
        public string Uri { get; set; }
    }

    internal class Response
    {
        [JsonPropertyName("mode")]
        public string Mode { get; set; }

        [JsonPropertyName("parameters")]
        public Parameters Parameters { get; set; }
    }

    internal class AuthenticationResponse
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("error")]
        public string Error { get; set; }

        [JsonPropertyName("response")]
        public Response Response { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }
    }

    internal class EntitlementsAuthenticationResponse
    {
        [JsonPropertyName("entitlements_token")]
        public string Token { get; set; }
    }

    public class RiotAuthentication
    {
        public string AccessToken { get; set; }
        public string IdToken { get; set; }
    }
}
