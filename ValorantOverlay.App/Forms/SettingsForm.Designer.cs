
namespace ValorantOverlay.App.Forms
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.usernameTextBox = new System.Windows.Forms.TextBox();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.regionLabel = new System.Windows.Forms.Label();
            this.regionDropdown = new System.Windows.Forms.ComboBox();
            this.skinLabel = new System.Windows.Forms.Label();
            this.skinDropdown = new System.Windows.Forms.ComboBox();
            this.refreshLabel = new System.Windows.Forms.Label();
            this.refreshDropdown = new System.Windows.Forms.ComboBox();
            this.twitchBotSettingsSectionLabel = new System.Windows.Forms.Label();
            this.twitchBotCheckbox = new System.Windows.Forms.CheckBox();
            this.twitchChannelNameTextbox = new System.Windows.Forms.TextBox();
            this.twitchbotUsernameTextbox = new System.Windows.Forms.TextBox();
            this.twitchBotTokenTextbox = new System.Windows.Forms.TextBox();
            this.applyButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // usernameTextBox
            // 
            this.usernameTextBox.Location = new System.Drawing.Point(20, 10);
            this.usernameTextBox.Name = "usernameTextBox";
            this.usernameTextBox.PlaceholderText = "Riot Games Username";
            this.usernameTextBox.Size = new System.Drawing.Size(200, 23);
            this.usernameTextBox.TabIndex = 0;
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(20, 40);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.PasswordChar = '*';
            this.passwordTextBox.PlaceholderText = "Riot Games Password";
            this.passwordTextBox.Size = new System.Drawing.Size(200, 23);
            this.passwordTextBox.TabIndex = 3;
            this.passwordTextBox.UseSystemPasswordChar = true;
            // 
            // regionLabel
            // 
            this.regionLabel.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.regionLabel.Location = new System.Drawing.Point(20, 70);
            this.regionLabel.Name = "regionLabel";
            this.regionLabel.Size = new System.Drawing.Size(65, 23);
            this.regionLabel.TabIndex = 5;
            this.regionLabel.Text = "Region:";
            // 
            // regionDropdown
            // 
            this.regionDropdown.FormattingEnabled = true;
            this.regionDropdown.Items.AddRange(new object[] {
            "North America | LATAM",
            "Europe",
            "Korea",
            "Asia Pacific"});
            this.regionDropdown.Location = new System.Drawing.Point(90, 70);
            this.regionDropdown.Name = "regionDropdown";
            this.regionDropdown.Size = new System.Drawing.Size(130, 23);
            this.regionDropdown.TabIndex = 4;
            // 
            // skinLabel
            // 
            this.skinLabel.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.skinLabel.Location = new System.Drawing.Point(20, 100);
            this.skinLabel.Name = "skinLabel";
            this.skinLabel.Size = new System.Drawing.Size(65, 23);
            this.skinLabel.TabIndex = 6;
            this.skinLabel.Text = "Skin:";
            // 
            // skinDropdown
            // 
            this.skinDropdown.FormattingEnabled = true;
            this.skinDropdown.Items.AddRange(new object[] {
            "Default Red",
            "Blue",
            "Light Blue",
            "Green",
            "Purple",
            "Gray",
            "Custom"});
            this.skinDropdown.Location = new System.Drawing.Point(90, 100);
            this.skinDropdown.Name = "skinDropdown";
            this.skinDropdown.Size = new System.Drawing.Size(130, 23);
            this.skinDropdown.TabIndex = 7;
            // 
            // refreshLabel
            // 
            this.refreshLabel.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.refreshLabel.Location = new System.Drawing.Point(20, 130);
            this.refreshLabel.Name = "refreshLabel";
            this.refreshLabel.Size = new System.Drawing.Size(65, 23);
            this.refreshLabel.TabIndex = 8;
            this.refreshLabel.Text = "Refresh:";
            // 
            // refreshDropdown
            // 
            this.refreshDropdown.FormattingEnabled = true;
            this.refreshDropdown.Items.AddRange(new object[] {
            "30 Seconds",
            "60 Seconds"});
            this.refreshDropdown.Location = new System.Drawing.Point(90, 130);
            this.refreshDropdown.Name = "refreshDropdown";
            this.refreshDropdown.Size = new System.Drawing.Size(130, 23);
            this.refreshDropdown.TabIndex = 9;
            // 
            // twitchBotSettingsSectionLabel
            // 
            this.twitchBotSettingsSectionLabel.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point);
            this.twitchBotSettingsSectionLabel.Location = new System.Drawing.Point(20, 160);
            this.twitchBotSettingsSectionLabel.Name = "twitchBotSettingsSectionLabel";
            this.twitchBotSettingsSectionLabel.Size = new System.Drawing.Size(200, 20);
            this.twitchBotSettingsSectionLabel.TabIndex = 10;
            this.twitchBotSettingsSectionLabel.Text = "Twitch Bot Settings:";
            this.twitchBotSettingsSectionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // twitchBotCheckbox
            // 
            this.twitchBotCheckbox.Location = new System.Drawing.Point(20, 190);
            this.twitchBotCheckbox.Name = "twitchBotCheckbox";
            this.twitchBotCheckbox.Size = new System.Drawing.Size(200, 20);
            this.twitchBotCheckbox.TabIndex = 18;
            this.twitchBotCheckbox.Text = "Enable Twitch Bot?";
            this.twitchBotCheckbox.UseVisualStyleBackColor = true;
            // 
            // twitchChannelNameTextbox
            // 
            this.twitchChannelNameTextbox.Location = new System.Drawing.Point(20, 220);
            this.twitchChannelNameTextbox.Name = "twitchChannelNameTextbox";
            this.twitchChannelNameTextbox.PlaceholderText = "Twitch Channel Name";
            this.twitchChannelNameTextbox.Size = new System.Drawing.Size(200, 23);
            this.twitchChannelNameTextbox.TabIndex = 11;
            // 
            // twitchbotUsernameTextbox
            // 
            this.twitchbotUsernameTextbox.Location = new System.Drawing.Point(20, 250);
            this.twitchbotUsernameTextbox.Name = "twitchbotUsernameTextbox";
            this.twitchbotUsernameTextbox.PlaceholderText = "Twitch Bot Username";
            this.twitchbotUsernameTextbox.Size = new System.Drawing.Size(200, 23);
            this.twitchbotUsernameTextbox.TabIndex = 12;
            // 
            // twitchBotTokenTextbox
            // 
            this.twitchBotTokenTextbox.Location = new System.Drawing.Point(20, 280);
            this.twitchBotTokenTextbox.Name = "twitchBotTokenTextbox";
            this.twitchBotTokenTextbox.PasswordChar = '*';
            this.twitchBotTokenTextbox.PlaceholderText = "Twitch Bot Token";
            this.twitchBotTokenTextbox.Size = new System.Drawing.Size(200, 23);
            this.twitchBotTokenTextbox.TabIndex = 13;
            this.twitchBotTokenTextbox.UseSystemPasswordChar = true;
            // 
            // applyButton
            // 
            this.applyButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.applyButton.Location = new System.Drawing.Point(40, 310);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(75, 23);
            this.applyButton.TabIndex = 2;
            this.applyButton.Text = "APPLY";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.ApplyButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(120, 310);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(234, 350);
            this.Controls.Add(this.usernameTextBox);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.regionLabel);
            this.Controls.Add(this.regionDropdown);
            this.Controls.Add(this.skinLabel);
            this.Controls.Add(this.skinDropdown);
            this.Controls.Add(this.refreshLabel);
            this.Controls.Add(this.refreshDropdown);
            this.Controls.Add(this.twitchBotSettingsSectionLabel);
            this.Controls.Add(this.twitchBotCheckbox);
            this.Controls.Add(this.twitchChannelNameTextbox);
            this.Controls.Add(this.twitchbotUsernameTextbox);
            this.Controls.Add(this.twitchBotTokenTextbox);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.cancelButton);
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.Settings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox usernameTextBox;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Label regionLabel;
        private System.Windows.Forms.ComboBox regionDropdown;
        private System.Windows.Forms.Label skinLabel;
        private System.Windows.Forms.ComboBox skinDropdown;
        private System.Windows.Forms.Label refreshLabel;
        private System.Windows.Forms.ComboBox refreshDropdown;
        private System.Windows.Forms.Label twitchBotSettingsSectionLabel;
        private System.Windows.Forms.CheckBox twitchBotCheckbox;
        private System.Windows.Forms.TextBox twitchChannelNameTextbox;
        private System.Windows.Forms.TextBox twitchbotUsernameTextbox;
        private System.Windows.Forms.TextBox twitchBotTokenTextbox;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.Button cancelButton;
    }
}