namespace DreamBeam{

    partial class ModernSongEditor
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.Toppanel = new System.Windows.Forms.Panel();
            this.SequenceButton = new RibbonStyle.RibbonMenuButton();
            this.AdditionalInformationButton = new RibbonStyle.RibbonMenuButton();
            this.Title = new System.Windows.Forms.TextBox();
            this.Design_checkBox = new RibbonStyle.RibbonMenuButton();
            this.EditorPanel = new System.Windows.Forms.Panel();
            this.coloredTextBoxPanel1 = new DreamBeam.ColoredTextBoxPanel();
            this.Toppanel.SuspendLayout();
            this.EditorPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // Toppanel
            // 
            this.Toppanel.Controls.Add(this.SequenceButton);
            this.Toppanel.Controls.Add(this.AdditionalInformationButton);
            this.Toppanel.Controls.Add(this.Title);
            this.Toppanel.Controls.Add(this.Design_checkBox);
            this.Toppanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.Toppanel.Location = new System.Drawing.Point(0, 0);
            this.Toppanel.Margin = new System.Windows.Forms.Padding(0);
            this.Toppanel.Name = "Toppanel";
            this.Toppanel.Size = new System.Drawing.Size(348, 29);
            this.Toppanel.TabIndex = 0;
            // 
            // SequenceButton
            // 
            this.SequenceButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SequenceButton.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.ToDown;
            this.SequenceButton.BackColor = System.Drawing.Color.Transparent;
            this.SequenceButton.Checked = false;
            this.SequenceButton.ColorBase = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(209)))), ((int)(((byte)(140)))));
            this.SequenceButton.ColorBaseStroke = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(76)))), ((int)(((byte)(62)))), ((int)(((byte)(42)))));
            this.SequenceButton.ColorOn = System.Drawing.Color.LemonChiffon;
            this.SequenceButton.ColorOnStroke = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(76)))), ((int)(((byte)(74)))), ((int)(((byte)(61)))));
            this.SequenceButton.ColorPress = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(177)))), ((int)(((byte)(86)))));
            this.SequenceButton.ColorPressStroke = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(76)))), ((int)(((byte)(54)))), ((int)(((byte)(26)))));
            this.SequenceButton.FadingSpeed = 35;
            this.SequenceButton.FlatAppearance.BorderSize = 0;
            this.SequenceButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SequenceButton.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.Right;
            this.SequenceButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.SequenceButton.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
            this.SequenceButton.ImageOffset = 2;
            this.SequenceButton.IsPressed = false;
            this.SequenceButton.KeepPress = true;
            this.SequenceButton.Location = new System.Drawing.Point(278, 0);
            this.SequenceButton.MaxImageSize = new System.Drawing.Point(24, 24);
            this.SequenceButton.MenuPos = new System.Drawing.Point(0, 0);
            this.SequenceButton.Name = "SequenceButton";
            this.SequenceButton.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.SequenceButton.Radius = 6;
            this.SequenceButton.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
            this.SequenceButton.SinglePressButton = true;
            this.SequenceButton.Size = new System.Drawing.Size(70, 29);
            this.SequenceButton.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
            this.SequenceButton.SplitDistance = 0;
            this.SequenceButton.TabIndex = 32;
            this.SequenceButton.Text = "Sequence..";
            this.SequenceButton.Title = "";
            this.SequenceButton.UseVisualStyleBackColor = true;
            this.SequenceButton.Click += new System.EventHandler(this.SequenceButton_Click);
            // 
            // AdditionalInformationButton
            // 
            this.AdditionalInformationButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AdditionalInformationButton.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.ToDown;
            this.AdditionalInformationButton.BackColor = System.Drawing.Color.Transparent;
            this.AdditionalInformationButton.Checked = false;
            this.AdditionalInformationButton.ColorBase = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(209)))), ((int)(((byte)(140)))));
            this.AdditionalInformationButton.ColorBaseStroke = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(76)))), ((int)(((byte)(62)))), ((int)(((byte)(42)))));
            this.AdditionalInformationButton.ColorOn = System.Drawing.Color.LemonChiffon;
            this.AdditionalInformationButton.ColorOnStroke = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(76)))), ((int)(((byte)(74)))), ((int)(((byte)(61)))));
            this.AdditionalInformationButton.ColorPress = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(209)))), ((int)(((byte)(140)))));
            this.AdditionalInformationButton.ColorPressStroke = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(75)))), ((int)(((byte)(76)))), ((int)(((byte)(59)))));
            this.AdditionalInformationButton.FadingSpeed = 35;
            this.AdditionalInformationButton.FlatAppearance.BorderSize = 0;
            this.AdditionalInformationButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AdditionalInformationButton.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.Center;
            this.AdditionalInformationButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.AdditionalInformationButton.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
            this.AdditionalInformationButton.ImageOffset = 2;
            this.AdditionalInformationButton.IsPressed = false;
            this.AdditionalInformationButton.KeepPress = true;
            this.AdditionalInformationButton.Location = new System.Drawing.Point(210, 0);
            this.AdditionalInformationButton.MaxImageSize = new System.Drawing.Point(24, 24);
            this.AdditionalInformationButton.MenuPos = new System.Drawing.Point(0, 0);
            this.AdditionalInformationButton.Name = "AdditionalInformationButton";
            this.AdditionalInformationButton.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.AdditionalInformationButton.Radius = 6;
            this.AdditionalInformationButton.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
            this.AdditionalInformationButton.SinglePressButton = true;
            this.AdditionalInformationButton.Size = new System.Drawing.Size(69, 29);
            this.AdditionalInformationButton.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
            this.AdditionalInformationButton.SplitDistance = 0;
            this.AdditionalInformationButton.TabIndex = 31;
            this.AdditionalInformationButton.Text = "More...";
            this.AdditionalInformationButton.Title = "";
            this.AdditionalInformationButton.UseVisualStyleBackColor = true;
            this.AdditionalInformationButton.Click += new System.EventHandler(this.ribbonMenuButton2_Click);
            // 
            // Title
            // 
            this.Title.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Title.Location = new System.Drawing.Point(34, 5);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(173, 20);
            this.Title.TabIndex = 12;
            // 
            // Design_checkBox
            // 
            this.Design_checkBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Design_checkBox.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
            this.Design_checkBox.BackColor = System.Drawing.Color.Transparent;
            this.Design_checkBox.Checked = false;
            this.Design_checkBox.ColorBase = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(209)))), ((int)(((byte)(140)))));
            this.Design_checkBox.ColorBaseStroke = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(76)))), ((int)(((byte)(62)))), ((int)(((byte)(42)))));
            this.Design_checkBox.ColorOn = System.Drawing.Color.Khaki;
            this.Design_checkBox.ColorOnStroke = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(76)))), ((int)(((byte)(73)))), ((int)(((byte)(44)))));
            this.Design_checkBox.ColorPress = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(209)))), ((int)(((byte)(140)))));
            this.Design_checkBox.ColorPressStroke = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(75)))), ((int)(((byte)(76)))), ((int)(((byte)(59)))));
            this.Design_checkBox.Enabled = false;
            this.Design_checkBox.FadingSpeed = 35;
            this.Design_checkBox.FlatAppearance.BorderSize = 0;
            this.Design_checkBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Design_checkBox.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.Left;
            this.Design_checkBox.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Design_checkBox.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
            this.Design_checkBox.ImageOffset = 2;
            this.Design_checkBox.IsPressed = false;
            this.Design_checkBox.KeepPress = true;
            this.Design_checkBox.Location = new System.Drawing.Point(0, 0);
            this.Design_checkBox.MaxImageSize = new System.Drawing.Point(24, 24);
            this.Design_checkBox.MenuPos = new System.Drawing.Point(0, 0);
            this.Design_checkBox.Name = "Design_checkBox";
            this.Design_checkBox.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.Design_checkBox.Radius = 6;
            this.Design_checkBox.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
            this.Design_checkBox.SinglePressButton = true;
            this.Design_checkBox.Size = new System.Drawing.Size(211, 29);
            this.Design_checkBox.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
            this.Design_checkBox.SplitDistance = 0;
            this.Design_checkBox.TabIndex = 30;
            this.Design_checkBox.Text = "Title:";
            this.Design_checkBox.Title = "";
            this.Design_checkBox.UseVisualStyleBackColor = true;
            // 
            // EditorPanel
            // 
            this.EditorPanel.Controls.Add(this.coloredTextBoxPanel1);
            this.EditorPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EditorPanel.Location = new System.Drawing.Point(0, 29);
            this.EditorPanel.Name = "EditorPanel";
            this.EditorPanel.Size = new System.Drawing.Size(348, 369);
            this.EditorPanel.TabIndex = 1;
            // 
            // coloredTextBoxPanel1
            // 
            this.coloredTextBoxPanel1.BackColor = System.Drawing.SystemColors.Control;
            this.coloredTextBoxPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.coloredTextBoxPanel1.Location = new System.Drawing.Point(0, 0);
            this.coloredTextBoxPanel1.Name = "coloredTextBoxPanel1";
            this.coloredTextBoxPanel1.Size = new System.Drawing.Size(348, 369);
            this.coloredTextBoxPanel1.TabIndex = 0;
            // 
            // ModernSongEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.EditorPanel);
            this.Controls.Add(this.Toppanel);
            this.Name = "ModernSongEditor";
            this.Size = new System.Drawing.Size(348, 398);            
            this.Toppanel.ResumeLayout(false);
            this.Toppanel.PerformLayout();
            this.EditorPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Toppanel;
        private System.Windows.Forms.Panel EditorPanel;
        private System.Windows.Forms.TextBox Title;
        public RibbonStyle.RibbonMenuButton Design_checkBox;
        public RibbonStyle.RibbonMenuButton AdditionalInformationButton;
        public RibbonStyle.RibbonMenuButton SequenceButton;
        private ColoredTextBoxPanel coloredTextBoxPanel1;
    }
}
