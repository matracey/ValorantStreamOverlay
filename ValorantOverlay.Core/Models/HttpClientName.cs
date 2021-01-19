using System.Collections.Generic;
using System.Linq;

namespace ValorantOverlay.Core.Models
{
    public class HttpClientName
    {
        public HttpClientName(string value)
        {
            Value = value;
        }

        public string Value { get; set; }

        public static IList<HttpClientName> All => new List<HttpClientName> { AuthRiot, EntitlementsAuthRiot, NorthAmerica, Europe, Korea, AsiaPacific };
        public static HttpClientName GetByIndex(int index) => All.ElementAtOrDefault(index) ?? NorthAmerica;

        public static HttpClientName AuthRiot => new HttpClientName("auth_riot");
        public static HttpClientName EntitlementsAuthRiot => new HttpClientName("entitlements_auth_riot");
        public static HttpClientName NorthAmerica => new HttpClientName(Region.NorthAmerica);
        public static HttpClientName Europe => new HttpClientName(Region.Europe);
        public static HttpClientName Korea => new HttpClientName(Region.Korea);
        public static HttpClientName AsiaPacific => new HttpClientName(Region.AsiaPacific);

        public static implicit operator string(HttpClientName httpClientName) => httpClientName.ToString();

        public override string ToString()
        {
            return Value;
        }
    }
}
