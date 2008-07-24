namespace DreamBeam {
	partial class ThemeWidget {
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ThemeWidget));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.OpenMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.textFormatOptions1 = new DreamBeam.TextFormatOptions();
            this.TabContainerPanel = new System.Windows.Forms.Panel();
            this.Design_checkBox = new RibbonStyle.RibbonMenuButton();
            this.TextOutlineMenuButton = new RibbonStyle.RibbonMenuButton();
            this.grouper1 = new CodeVendor.Controls.Grouper();
            this.label2 = new System.Windows.Forms.Label();
            this.BGImagePathLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ThemeLabel = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.TabContainerPanel.SuspendLayout();
            this.grouper1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenMenuItem,
            this.saveAsMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(121, 48);
            // 
            // OpenMenuItem
            // 
            this.OpenMenuItem.Name = "OpenMenuItem";
            this.OpenMenuItem.Size = new System.Drawing.Size(120, 22);
            this.OpenMenuItem.Text = "Open..";
            this.OpenMenuItem.Click += new System.EventHandler(this.openBtn_Click);
            // 
            // saveAsMenuItem
            // 
            this.saveAsMenuItem.Name = "saveAsMenuItem";
            this.saveAsMenuItem.Size = new System.Drawing.Size(120, 22);
            this.saveAsMenuItem.Text = "Save As..";
            this.saveAsMenuItem.Click += new System.EventHandler(this.saveAsBtn_Click);
            // 
            // tabControl
            // 
            this.tabControl.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 2);
            this.tabControl.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(243, 325);
            this.tabControl.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tabPage1.BackgroundImage")));
            this.tabPage1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabPage1.Controls.Add(this.textFormatOptions1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(235, 296);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            // 
            // textFormatOptions1
            // 
            this.textFormatOptions1.BackColor = System.Drawing.SystemColors.Control;
            this.textFormatOptions1.Dock = System.Windows.Forms.DockStyle.Top;
            this.textFormatOptions1.Location = new System.Drawing.Point(0, 0);
            this.textFormatOptions1.Margin = new System.Windows.Forms.Padding(0);
            this.textFormatOptions1.Name = "textFormatOptions1";
            this.textFormatOptions1.Size = new System.Drawing.Size(235, 296);
            this.textFormatOptions1.TabIndex = 0;
            // 
            // TabContainerPanel
            // 
            this.TabContainerPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TabContainerPanel.Controls.Add(this.tabControl);
            this.TabContainerPanel.Location = new System.Drawing.Point(2, 73);
            this.TabContainerPanel.Margin = new System.Windows.Forms.Padding(0);
            this.TabContainerPanel.Name = "TabContainerPanel";
            this.TabContainerPanel.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.TabContainerPanel.Size = new System.Drawing.Size(245, 329);
            this.TabContainerPanel.TabIndex = 32;
            // 
            // Design_checkBox
            // 
            this.Design_checkBox.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
            this.Design_checkBox.BackColor = System.Drawing.Color.Transparent;
            this.Design_checkBox.Checked = false;
            this.Design_checkBox.ColorBase = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(209)))), ((int)(((byte)(140)))));
            this.Design_checkBox.ColorBaseStroke = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(76)))), ((int)(((byte)(62)))), ((int)(((byte)(42)))));
            this.Design_checkBox.ColorOn = System.Drawing.Color.Khaki;
            this.Design_checkBox.ColorOnStroke = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(76)))), ((int)(((byte)(73)))), ((int)(((byte)(44)))));
            this.Design_checkBox.ColorPress = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(209)))), ((int)(((byte)(140)))));
            this.Design_checkBox.ColorPressStroke = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(75)))), ((int)(((byte)(76)))), ((int)(((byte)(59)))));
            this.Design_checkBox.FadingSpeed = 35;
            this.Design_checkBox.FlatAppearance.BorderSize = 0;
            this.Design_checkBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Design_checkBox.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.Right;
            this.Design_checkBox.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Design_checkBox.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
            this.Design_checkBox.ImageOffset = 2;
            this.Design_checkBox.IsPressed = false;
            this.Design_checkBox.KeepPress = true;
            this.Design_checkBox.Location = new System.Drawing.Point(81, 0);
            this.Design_checkBox.MaxImageSize = new System.Drawing.Point(24, 24);
            this.Design_checkBox.MenuPos = new System.Drawing.Point(0, 0);
            this.Design_checkBox.Name = "Design_checkBox";
            this.Design_checkBox.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.Design_checkBox.Radius = 6;
            this.Design_checkBox.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
            this.Design_checkBox.SinglePressButton = true;
            this.Design_checkBox.Size = new System.Drawing.Size(147, 29);
            this.Design_checkBox.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
            this.Design_checkBox.SplitDistance = 0;
            this.Design_checkBox.TabIndex = 25;
            this.Design_checkBox.Text = "        Individual Design";
            this.Design_checkBox.Title = "";
            this.Design_checkBox.UseVisualStyleBackColor = true;
            this.Design_checkBox.Click += new System.EventHandler(this.Design_checkBox_Click);
            // 
            // TextOutlineMenuButton
            // 
            this.TextOutlineMenuButton.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.ToDown;
            this.TextOutlineMenuButton.BackColor = System.Drawing.Color.Transparent;
            this.TextOutlineMenuButton.Checked = false;
            this.TextOutlineMenuButton.ColorBase = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(209)))), ((int)(((byte)(140)))));
            this.TextOutlineMenuButton.ColorBaseStroke = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(76)))), ((int)(((byte)(62)))), ((int)(((byte)(42)))));
            this.TextOutlineMenuButton.ColorOn = System.Drawing.Color.Khaki;
            this.TextOutlineMenuButton.ColorOnStroke = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(76)))), ((int)(((byte)(73)))), ((int)(((byte)(44)))));
            this.TextOutlineMenuButton.ColorPress = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(209)))), ((int)(((byte)(140)))));
            this.TextOutlineMenuButton.ColorPressStroke = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(75)))), ((int)(((byte)(76)))), ((int)(((byte)(59)))));
            this.TextOutlineMenuButton.ContextMenuStrip = this.contextMenuStrip1;
            this.TextOutlineMenuButton.FadingSpeed = 35;
            this.TextOutlineMenuButton.FlatAppearance.BorderSize = 0;
            this.TextOutlineMenuButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TextOutlineMenuButton.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.Left;
            this.TextOutlineMenuButton.Image = global::DreamBeam.Properties.Resources.stock_templates;
            this.TextOutlineMenuButton.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
            this.TextOutlineMenuButton.ImageOffset = 2;
            this.TextOutlineMenuButton.IsPressed = false;
            this.TextOutlineMenuButton.KeepPress = false;
            this.TextOutlineMenuButton.Location = new System.Drawing.Point(31, 0);
            this.TextOutlineMenuButton.MaxImageSize = new System.Drawing.Point(24, 24);
            this.TextOutlineMenuButton.MenuPos = new System.Drawing.Point(0, 0);
            this.TextOutlineMenuButton.Name = "TextOutlineMenuButton";
            this.TextOutlineMenuButton.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.TextOutlineMenuButton.Radius = 6;
            this.TextOutlineMenuButton.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
            this.TextOutlineMenuButton.SinglePressButton = false;
            this.TextOutlineMenuButton.Size = new System.Drawing.Size(52, 29);
            this.TextOutlineMenuButton.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.Yes;
            this.TextOutlineMenuButton.SplitDistance = 52;
            this.TextOutlineMenuButton.TabIndex = 24;
            this.TextOutlineMenuButton.Text = "\r\n";
            this.TextOutlineMenuButton.Title = "";
            this.TextOutlineMenuButton.UseVisualStyleBackColor = true;
            // 
            // grouper1
            // 
            this.grouper1.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.grouper1.BackgroundGradientColor = System.Drawing.Color.Linen;
            this.grouper1.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.ForwardDiagonal;
            this.grouper1.BorderColor = System.Drawing.Color.Black;
            this.grouper1.BorderThickness = 1F;
            this.grouper1.Controls.Add(this.label2);
            this.grouper1.Controls.Add(this.BGImagePathLabel);
            this.grouper1.Controls.Add(this.label1);
            this.grouper1.Controls.Add(this.ThemeLabel);
            this.grouper1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grouper1.GroupImage = null;
            this.grouper1.GroupTitle = "";
            this.grouper1.Location = new System.Drawing.Point(3, 27);
            this.grouper1.Name = "grouper1";
            this.grouper1.Padding = new System.Windows.Forms.Padding(20);
            this.grouper1.PaintGroupBox = false;
            this.grouper1.RoundCorners = 2;
            this.grouper1.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouper1.ShadowControl = false;
            this.grouper1.ShadowThickness = 3;
            this.grouper1.Size = new System.Drawing.Size(244, 42);
            this.grouper1.TabIndex = 31;
            this.grouper1.TinyMode = true;
            this.grouper1.TitleBorder = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(2, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 28;
            this.label2.Text = "Theme:";
            // 
            // BGImagePathLabel
            // 
            this.BGImagePathLabel.AutoSize = true;
            this.BGImagePathLabel.BackColor = System.Drawing.Color.Transparent;
            this.BGImagePathLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BGImagePathLabel.Location = new System.Drawing.Point(72, 26);
            this.BGImagePathLabel.Name = "BGImagePathLabel";
            this.BGImagePathLabel.Size = new System.Drawing.Size(31, 12);
            this.BGImagePathLabel.TabIndex = 26;
            this.BGImagePathLabel.Text = "Image";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(1, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "Background:";
            // 
            // ThemeLabel
            // 
            this.ThemeLabel.AutoSize = true;
            this.ThemeLabel.BackColor = System.Drawing.Color.Transparent;
            this.ThemeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ThemeLabel.Location = new System.Drawing.Point(72, 8);
            this.ThemeLabel.Name = "ThemeLabel";
            this.ThemeLabel.Size = new System.Drawing.Size(33, 12);
            this.ThemeLabel.TabIndex = 29;
            this.ThemeLabel.Text = "Theme";
            // 
            // ThemeWidget
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TabContainerPanel);
            this.Controls.Add(this.Design_checkBox);
            this.Controls.Add(this.TextOutlineMenuButton);
            this.Controls.Add(this.grouper1);
            this.Name = "ThemeWidget";
            this.Size = new System.Drawing.Size(250, 483);
            this.contextMenuStrip1.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.TabContainerPanel.ResumeLayout(false);
            this.grouper1.ResumeLayout(false);
            this.grouper1.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.TabControl tabControl;
        //System.Windows.Forms.TabControl
		private System.Windows.Forms.TabPage tabPage1;
        private TextFormatOptions textFormatOptions1;
        private RibbonStyle.RibbonMenuButton TextOutlineMenuButton;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem OpenMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsMenuItem;
        public RibbonStyle.RibbonMenuButton Design_checkBox;
        public System.Windows.Forms.Label BGImagePathLabel;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label ThemeLabel;
        private CodeVendor.Controls.Grouper grouper1;
        private System.Windows.Forms.Panel TabContainerPanel;

	}
}
