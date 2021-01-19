
namespace ValorantOverlay.App.Forms
{
    partial class ValorantStreamOverlay
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ValorantStreamOverlay));
            this.mainPanel = new System.Windows.Forms.Panel();
            this.recentGame1 = new System.Windows.Forms.Label();
            this.recentGame2 = new System.Windows.Forms.Label();
            this.recentGame3 = new System.Windows.Forms.Label();
            this.rankIconBox = new System.Windows.Forms.PictureBox();
            this.rankPointsElo = new System.Windows.Forms.Label();
            this.rankingLabel = new System.Windows.Forms.Label();
            this.backgroundPic = new System.Windows.Forms.PictureBox();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.settingsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rankIconBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.backgroundPic)).BeginInit();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.recentGame1);
            this.mainPanel.Controls.Add(this.recentGame2);
            this.mainPanel.Controls.Add(this.recentGame3);
            this.mainPanel.Controls.Add(this.rankIconBox);
            this.mainPanel.Controls.Add(this.rankPointsElo);
            this.mainPanel.Controls.Add(this.rankingLabel);
            this.mainPanel.Controls.Add(this.backgroundPic);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(398, 143);
            this.mainPanel.TabIndex = 0;
            // 
            // recentGame1
            // 
            this.recentGame1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.recentGame1.BackColor = System.Drawing.Color.Black;
            this.recentGame1.Font = new System.Drawing.Font("Anton", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.recentGame1.ForeColor = System.Drawing.Color.White;
            this.recentGame1.Location = new System.Drawing.Point(221, 77);
            this.recentGame1.Name = "recentGame1";
            this.recentGame1.Size = new System.Drawing.Size(46, 50);
            this.recentGame1.TabIndex = 103;
            this.recentGame1.Text = "+00";
            this.recentGame1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // recentGame2
            // 
            this.recentGame2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.recentGame2.BackColor = System.Drawing.Color.Black;
            this.recentGame2.Font = new System.Drawing.Font("Anton", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.recentGame2.ForeColor = System.Drawing.Color.White;
            this.recentGame2.Location = new System.Drawing.Point(284, 77);
            this.recentGame2.Name = "recentGame2";
            this.recentGame2.Size = new System.Drawing.Size(46, 50);
            this.recentGame2.TabIndex = 102;
            this.recentGame2.Text = "+00";
            this.recentGame2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // recentGame3
            // 
            this.recentGame3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.recentGame3.BackColor = System.Drawing.Color.Black;
            this.recentGame3.Font = new System.Drawing.Font("Anton", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.recentGame3.ForeColor = System.Drawing.Color.White;
            this.recentGame3.Location = new System.Drawing.Point(345, 77);
            this.recentGame3.Margin = new System.Windows.Forms.Padding(0);
            this.recentGame3.Name = "recentGame3";
            this.recentGame3.Size = new System.Drawing.Size(46, 50);
            this.recentGame3.TabIndex = 101;
            this.recentGame3.Text = "+00";
            this.recentGame3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rankIconBox
            // 
            this.rankIconBox.BackColor = System.Drawing.Color.Transparent;
            this.rankIconBox.Image = ((System.Drawing.Image)(resources.GetObject("rankIconBox.Image")));
            this.rankIconBox.Location = new System.Drawing.Point(15, 67);
            this.rankIconBox.Name = "rankIconBox";
            this.rankIconBox.Size = new System.Drawing.Size(65, 66);
            this.rankIconBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.rankIconBox.TabIndex = 3;
            this.rankIconBox.TabStop = false;
            // 
            // rankPointsElo
            // 
            this.rankPointsElo.Font = new System.Drawing.Font("Anton", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rankPointsElo.ForeColor = System.Drawing.Color.White;
            this.rankPointsElo.Location = new System.Drawing.Point(213, 0);
            this.rankPointsElo.Name = "rankPointsElo";
            this.rankPointsElo.Size = new System.Drawing.Size(185, 57);
            this.rankPointsElo.TabIndex = 2;
            this.rankPointsElo.Text = "000 RR | 0000 TRR";
            this.rankPointsElo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // rankingLabel
            // 
            this.rankingLabel.BackColor = System.Drawing.Color.Transparent;
            this.rankingLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rankingLabel.Font = new System.Drawing.Font("Anton", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rankingLabel.ForeColor = System.Drawing.Color.White;
            this.rankingLabel.Location = new System.Drawing.Point(0, 0);
            this.rankingLabel.Name = "rankingLabel";
            this.rankingLabel.Size = new System.Drawing.Size(150, 57);
            this.rankingLabel.TabIndex = 1;
            this.rankingLabel.Text = "INVALID";
            this.rankingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // backgroundPic
            // 
            this.backgroundPic.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.backgroundPic.BackColor = System.Drawing.Color.White;
            this.backgroundPic.Image = ((System.Drawing.Image)(resources.GetObject("backgroundPic.Image")));
            this.backgroundPic.Location = new System.Drawing.Point(0, 0);
            this.backgroundPic.Name = "backgroundPic";
            this.backgroundPic.Size = new System.Drawing.Size(398, 143);
            this.backgroundPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.backgroundPic.TabIndex = 0;
            this.backgroundPic.TabStop = false;
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsMenuItem});
            this.contextMenu.Name = "contextMenuStrip1";
            this.contextMenu.Size = new System.Drawing.Size(117, 26);
            // 
            // settingsMenuItem
            // 
            this.settingsMenuItem.CheckOnClick = true;
            this.settingsMenuItem.Name = "settingsMenuItem";
            this.settingsMenuItem.Size = new System.Drawing.Size(116, 22);
            this.settingsMenuItem.Text = "Settings";
            this.settingsMenuItem.Click += new System.EventHandler(this.SettingsMenuItem_Click);
            // 
            // ValorantStreamOverlay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(398, 143);
            this.Controls.Add(this.mainPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ValorantStreamOverlay";
            this.Text = "Valorant Overlay";
            this.Load += new System.EventHandler(this.ValorantStreamOverlay_Load);
            this.mainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rankIconBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.backgroundPic)).EndInit();
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.Label rankLabel;
        private System.Windows.Forms.Panel mainPanel;
        public System.Windows.Forms.Label rankingLabel;
        public System.Windows.Forms.PictureBox rankIconBox;
        public System.Windows.Forms.Label rankPointsElo;
        public System.Windows.Forms.Label recentGame1;
        public System.Windows.Forms.Label recentGame2;
        public System.Windows.Forms.Label recentGame3;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        public System.Windows.Forms.ToolStripMenuItem settingsMenuItem;
        public System.Windows.Forms.PictureBox backgroundPic;
    }
}

