using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Windows.Media;

namespace ValorantOverlay.Wpf.Models
{
    [JsonConverter(typeof(SkinConverter))]
    public class Skin
    {
        public Skin(string value)
        {
            var matchingSkin = All.Where(x => x.Text != "Custom").FirstOrDefault(x => x.ToString() == value);
            if (matchingSkin != null)
            {
                Value = matchingSkin.Value;
                Text = matchingSkin.Text;
            } else
            {
                Value = GetBrush(value);
                Text = "Custom";
            }
        }

        public Skin(string value, string text)
        {
            Value = GetBrush(value);
            Text = text;
        }

        public Skin(SolidColorBrush value, string text)
        {
            Value = value;
            Text = text;
        }

        public SolidColorBrush Value { get; set; }
        public string Text { get; set; }

        public bool IsCustom()
        {
            return All.Where(x => x.Text != "Custom").All(x => x.ToString() != ToString());
        }

        public override string ToString()
        {
            var c = Value.Color;
            return $"#{c.A:X2}{c.R:X2}{c.G:X2}{c.B:X2}";
        }

        public override bool Equals(object obj)
        {
            var other = obj as Skin;
            return Value?.Color == other?.Value.Color && Text == other?.Text;
        }

        public override int GetHashCode() => Value.GetHashCode() * 17 + Text.GetHashCode();

        private static SolidColorBrush GetBrush(string hexCode)
        {
            return Regex.IsMatch(hexCode, "^#(?:[0-9a-fA-F]{3}){1,2}$") ? (SolidColorBrush)new BrushConverter().ConvertFrom(hexCode) : Brushes.HotPink;
        }

        public static IList<Skin> All => new List<Skin> { BackgroundRed, BackgroundBlue, BackgroundLightBlue, BackgroundGreen, BackgroundPurple, BackgroundGrey, BackgroundCustom };
        public static Skin GetByName(string name) => All.FirstOrDefault(s => s.Text == name) ?? BackgroundRed;
        public static Skin GetByIndex(int index) => All.ElementAtOrDefault(index) ?? BackgroundRed;

        public static Skin BackgroundRed => new Skin("#F72748", "Red");
        public static Skin BackgroundBlue => new Skin("#0617FC", "Blue");
        public static Skin BackgroundLightBlue => new Skin("#1385FA", "Light Blue");
        public static Skin BackgroundGreen => new Skin("#3EF742", "Green");
        public static Skin BackgroundPurple => new Skin("#7F3EF7", "Purple");
        public static Skin BackgroundGrey => new Skin("#686868", "Grey");
        public static Skin BackgroundCustom => new Skin(Brushes.Transparent, "Custom");

        public static implicit operator int(Skin skin) => All.IndexOf(skin);
        public static implicit operator Skin(int index) => GetByIndex(index);
        public static implicit operator string(Skin skin) => skin.Text;
        public static implicit operator Skin(string name) => GetByName(name);
        public static bool operator ==(Skin skin1, Skin skin2)
        {
            return skin1?.Equals(skin2) ?? skin2 is null;
        }
        public static bool operator !=(Skin skin1, Skin skin2)
        {
            return !skin1?.Equals(skin2) ?? !(skin2 is null);
        }
    }

    internal class SkinConverter : JsonConverter<Skin>
    {
        public override Skin Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options) => new Skin(reader.GetString());

        public override void Write(
            Utf8JsonWriter writer,
            Skin skin,
            JsonSerializerOptions options) => writer.WriteStringValue(skin.ToString());
    }
}
