using System.Drawing;
using System.Drawing.Text;

namespace ValorantOverlay.App.Fonts
{
    public class AntonRegular
    {
        private PrivateFontCollection _fonts;
        private enum Size
        {
            Regular = 14,
            RegularPlus = 18,
            Heading1 = 28
        }

        public AntonRegular(PrivateFontCollection fonts)
        {
            _fonts = fonts;
                new Font(_fonts.Families[0], 14, FontStyle.Regular);
        }

        public Font Regular { get { return new Font(_fonts.Families[0], (float)Size.Regular, FontStyle.Regular); } }
        public Font RegularPlus { get { return new Font(_fonts.Families[0], (float)Size.RegularPlus, FontStyle.Regular); } }
        public Font Heading1 { get { return new Font(_fonts.Families[0], (float)Size.Heading1, FontStyle.Regular); } }
    }
}
