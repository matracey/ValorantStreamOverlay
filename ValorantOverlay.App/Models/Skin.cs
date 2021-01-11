using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ValorantOverlay.App.Properties;

namespace ValorantOverlay.App.Models
{
    public class Skin
    {
        public Skin(Bitmap value, string name)
        {
            Value = value;
            Name = name;
        }

        public Bitmap Value { get; set; }
        public string Name { get; set; }

        public static List<Skin> All => new List<Skin> { BackgroundRed, BackgroundBlue, BackgroundLightBlue, BackgroundGreen, BackgroundPurple, BackgroundGrey };
        public static Skin GetByName(string name) => All.FirstOrDefault(s => s.Name == name) ?? BackgroundRed;
        public static Skin GetByIndex(int index) => All.ElementAtOrDefault(index) ?? BackgroundRed;

        public static Skin BackgroundRed => new Skin(Resources.BackgroundRed, "BackgroundRed");
        public static Skin BackgroundBlue => new Skin(Resources.BackgroundBlue, "BackgroundBlue");
        public static Skin BackgroundLightBlue => new Skin(Resources.BackgroundLightBlue, "BackgroundLightBlue");
        public static Skin BackgroundGreen => new Skin(Resources.BackgroundGreen, "BackgroundGreen");
        public static Skin BackgroundPurple => new Skin(Resources.BackgroundPurple, "BackgroundPurple");
        public static Skin BackgroundGrey => new Skin(Resources.BackgroundGrey, "BackgroundGrey");
        public static implicit operator int(Skin skin) => All.FindIndex(x => skin.Name == x.Name);
        public static implicit operator Skin(int index) => GetByIndex(index);
        public static implicit operator string(Skin skin) => skin.Name;
        public static implicit operator Skin(string name) => GetByName(name);
    }
}
