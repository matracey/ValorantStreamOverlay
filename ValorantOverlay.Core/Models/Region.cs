using System.Collections.Generic;
using System.Linq;

namespace ValorantOverlay.Core.Models
{
    public class Region
    {
        public Region(string value) : this(value, value)
        {
        }

        public Region(string value, string text)
        {
            Value = value;
            Text = text;
        }

        public string Value { get; set; }
        public string Text { get; set; }

        public override string ToString()
        {
            return Value;
        }

        public override bool Equals(object obj)
        {
            var other = obj as Region;
            return Value == other?.Value && Text == other?.Text;
        }

        public override int GetHashCode() => Value.GetHashCode() * 17 + Text.GetHashCode();

        public static List<Region> All => new List<Region> { NorthAmerica, Europe, Korea, AsiaPacific };
        public static Region GetByIndex(int index) => All.ElementAtOrDefault(index) ?? NorthAmerica;
        public static Region GetByString(string str) => All.FirstOrDefault(s => s == str) ?? NorthAmerica;

        public static Region NorthAmerica => new Region("na", "North America | LATAM");
        public static Region Europe => new Region("eu", "Europe");
        public static Region Korea => new Region("kr", "Korea");
        public static Region AsiaPacific => new Region("ap", "Asia Pacific");

        public static implicit operator string(Region region) => region.ToString();
        public static implicit operator Region(string region) => region.ToString();
        public static implicit operator int(Region region) => All.FindIndex(x => region.Value == x.Value);
        public static implicit operator Region(int index) => GetByIndex(index);
        public static bool operator ==(Region region1, Region region2)
        {
            return region1?.Equals(region2) ?? region2 is null;
        }
        public static bool operator !=(Region skin1, Region skin2)
        {
            return !skin1?.Equals(skin2) ?? !(skin2 is null);
        }
    }
}
