using System;
using System.Drawing;
using System.Windows.Forms;
using ValorantOverlay.App.Fonts;
using ValorantOverlay.App.Services;

namespace ValorantOverlay.App.Forms
{
    public partial class ValorantStreamOverlay : Form
    {
        private readonly IUpdateService _updateService;
        private readonly AntonRegular _font;

        public ValorantStreamOverlay(UpdateService updateService, AntonRegular font)
        {
            _updateService = updateService;
            _font = font;
            InitializeComponent();
        }

        private void ValorantStreamOverlay_Load(object sender, EventArgs e)
        {
            backgroundPic.ContextMenuStrip = contextMenu;
            rankingLabel.Parent = backgroundPic;
            rankingLabel.BackColor = Color.Transparent;

            //On Load, Set backing and Fonts to labels displaying Rank changes.
            Label[] rankChanges = { recentGame1, recentGame2, recentGame3 };
            foreach (var recentC in rankChanges)
            {
                recentC.Font = _font.Regular;
                recentC.Parent = backgroundPic;
                recentC.BackColor = Color.Transparent;
            }
            rankingLabel.Font = _font.Heading1;
            rankIconBox.Parent = backgroundPic;
            rankIconBox.BackColor = Color.Transparent;

            //Add Rank elo point label, set font and parent..
            rankPointsElo.Font = _font.RegularPlus;
            rankPointsElo.BackColor = Color.Transparent;
            rankPointsElo.Parent = backgroundPic;

            ValorantStreamOverlay local = this;
            LogicHandler logic = new LogicHandler(local);
        }

        private void SettingsMenuItem_Click(object sender, EventArgs e)
        {
            Settings settingsPage = new Settings();
            settingsPage.ShowDialog();
        }
    }
}
