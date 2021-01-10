using System;
using System.Text.Json.Serialization;

namespace ValorantOverlay.Core.Models
{
    public class RiotUser
    {
        [JsonPropertyName("sub")]
        public string Id { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("email_verified")]
        public bool EmailVerified { get; set; }

        [JsonPropertyName("player_plocale")]
        public string PlayerPLocale { get; set; }

        [JsonPropertyName("country_at")]
        private long CountryAtEpoch { get; set; }

        public DateTime CountryAt
        {
            get { return DateTimeOffset.FromUnixTimeMilliseconds(CountryAtEpoch).DateTime; }
            set { CountryAtEpoch = new DateTimeOffset(value).ToUnixTimeMilliseconds(); }
        }

        [JsonPropertyName("player_locale")]
        public string PlayerLocale { get; set; }

        [JsonPropertyName("pw")]
        public PasswordState PasswordState { get; set; }

        [JsonPropertyName("phone_number_verified")]
        public bool PhoneNumberVerified { get; set; }

        [JsonPropertyName("acct")]
        public Account Account { get; set; }

        [JsonPropertyName("jti")]
        public string JTI { get; set; }

        [JsonPropertyName("ppid")]
        public string PPID { get; set; }
    }

    public class PasswordState
    {
        [JsonPropertyName("cng_at")]
        private long ChangedAtEpoch { get; set; }

        public DateTime ChangedAt
        {
            get { return DateTimeOffset.FromUnixTimeMilliseconds(ChangedAtEpoch).DateTime; }
            set { ChangedAtEpoch = new DateTimeOffset(value).ToUnixTimeMilliseconds(); }
        }

        [JsonPropertyName("reset")]
        public bool IsResetting { get; set; }

        [JsonPropertyName("must_reset")]
        public bool MustReset { get; set; }
    }

    public class Account
    {
        [JsonPropertyName("type")]
        public int Type { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("adm")]
        public bool IsAdministrator { get; set; }

        [JsonPropertyName("game_name")]
        public string InGameName { get; set; }

        [JsonPropertyName("tag_line")]
        public string TagLine { get; set; }

        [JsonPropertyName("created_at")]
        private long CreatedAtEpoch { get; set; }

        public DateTime CreatedAt
        {
            get { return DateTimeOffset.FromUnixTimeMilliseconds(CreatedAtEpoch).DateTime; }
            set { CreatedAtEpoch = new DateTimeOffset(value).ToUnixTimeMilliseconds(); }
        }
    }
}
