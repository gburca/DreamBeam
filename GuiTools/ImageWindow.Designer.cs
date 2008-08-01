namespace DreamBeam
{
    partial class ImageWindow
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

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageWindow));
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.grouper2 = new CodeVendor.Controls.Grouper();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Bounds4 = new System.Windows.Forms.NumericUpDown();
            this.Bounds3 = new System.Windows.Forms.NumericUpDown();
            this.Bounds2 = new System.Windows.Forms.NumericUpDown();
            this.Bounds1 = new System.Windows.Forms.NumericUpDown();
            this.Cancel_Button = new RibbonStyle.RibbonMenuButton();
            this.OK_button = new RibbonStyle.RibbonMenuButton();
            this.grouper1 = new CodeVendor.Controls.Grouper();
            this.panel2 = new System.Windows.Forms.Panel();
            this.imagePanel = new DreamBeam.ImagePanel();
            this.panel1.SuspendLayout();
            this.grouper2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Bounds4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bounds3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bounds2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bounds1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.grouper2);
            this.panel1.Controls.Add(this.Cancel_Button);
            this.panel1.Controls.Add(this.OK_button);
            this.panel1.Controls.Add(this.grouper1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(516, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(127, 485);
            this.panel1.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(25, 395);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 29;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // grouper2
            // 
            this.grouper2.BackgroundColor = System.Drawing.Color.BlanchedAlmond;
            this.grouper2.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouper2.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.BackwardDiagonal;
            this.grouper2.BorderColor = System.Drawing.Color.Black;
            this.grouper2.BorderThickness = 1F;
            this.grouper2.Controls.Add(this.label7);
            this.grouper2.Controls.Add(this.label6);
            this.grouper2.Controls.Add(this.label5);
            this.grouper2.Controls.Add(this.label4);
            this.grouper2.Controls.Add(this.Bounds4);
            this.grouper2.Controls.Add(this.Bounds3);
            this.grouper2.Controls.Add(this.Bounds2);
            this.grouper2.Controls.Add(this.Bounds1);
            this.grouper2.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grouper2.GroupImage = null;
            this.grouper2.GroupTitle = "Margins (in %)";
            this.grouper2.Location = new System.Drawing.Point(4, 222);
            this.grouper2.Name = "grouper2";
            this.grouper2.Padding = new System.Windows.Forms.Padding(20);
            this.grouper2.PaintGroupBox = false;
            this.grouper2.RoundCorners = 3;
            this.grouper2.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouper2.ShadowControl = false;
            this.grouper2.ShadowThickness = 3;
            this.grouper2.Size = new System.Drawing.Size(118, 150);
            this.grouper2.TabIndex = 28;
            this.grouper2.TinyMode = true;
            this.grouper2.TitleBorder = true;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(41, 102);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 16);
            this.label7.TabIndex = 15;
            this.label7.Text = "Bottom";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(69, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 16);
            this.label6.TabIndex = 14;
            this.label6.Text = "Right";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(41, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 16);
            this.label5.TabIndex = 13;
            this.label5.Text = "Top";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(6, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 16);
            this.label4.TabIndex = 12;
            this.label4.Text = "Left";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Bounds4
            // 
            this.Bounds4.DecimalPlaces = 1;
            this.Bounds4.Location = new System.Drawing.Point(39, 118);
            this.Bounds4.Name = "Bounds4";
            this.Bounds4.Size = new System.Drawing.Size(48, 20);
            this.Bounds4.TabIndex = 11;
            this.Bounds4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Bounds4.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.Bounds4.ValueChanged += new System.EventHandler(this.Bounds_ValueChanged);
            // 
            // Bounds3
            // 
            this.Bounds3.DecimalPlaces = 1;
            this.Bounds3.Location = new System.Drawing.Point(67, 76);
            this.Bounds3.Name = "Bounds3";
            this.Bounds3.Size = new System.Drawing.Size(48, 20);
            this.Bounds3.TabIndex = 10;
            this.Bounds3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Bounds3.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.Bounds3.ValueChanged += new System.EventHandler(this.Bounds_ValueChanged);
            // 
            // Bounds2
            // 
            this.Bounds2.DecimalPlaces = 1;
            this.Bounds2.Location = new System.Drawing.Point(39, 36);
            this.Bounds2.Name = "Bounds2";
            this.Bounds2.Size = new System.Drawing.Size(48, 20);
            this.Bounds2.TabIndex = 9;
            this.Bounds2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Bounds2.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.Bounds2.ValueChanged += new System.EventHandler(this.Bounds_ValueChanged);
            // 
            // Bounds1
            // 
            this.Bounds1.DecimalPlaces = 1;
            this.Bounds1.Location = new System.Drawing.Point(4, 76);
            this.Bounds1.Name = "Bounds1";
            this.Bounds1.Size = new System.Drawing.Size(48, 20);
            this.Bounds1.TabIndex = 8;
            this.Bounds1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Bounds1.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.Bounds1.ValueChanged += new System.EventHandler(this.Bounds_ValueChanged);
            // 
            // Cancel_Button
            // 
            this.Cancel_Button.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
            this.Cancel_Button.BackColor = System.Drawing.Color.Transparent;
            this.Cancel_Button.Checked = false;
            this.Cancel_Button.ColorBase = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(209)))), ((int)(((byte)(140)))));
            this.Cancel_Button.ColorBaseStroke = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(76)))), ((int)(((byte)(62)))), ((int)(((byte)(42)))));
            this.Cancel_Button.ColorOn = System.Drawing.Color.Khaki;
            this.Cancel_Button.ColorOnStroke = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(76)))), ((int)(((byte)(73)))), ((int)(((byte)(44)))));
            this.Cancel_Button.ColorPress = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(209)))), ((int)(((byte)(140)))));
            this.Cancel_Button.ColorPressStroke = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(75)))), ((int)(((byte)(76)))), ((int)(((byte)(59)))));
            this.Cancel_Button.FadingSpeed = 35;
            this.Cancel_Button.FlatAppearance.BorderSize = 0;
            this.Cancel_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Cancel_Button.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
            this.Cancel_Button.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Cancel_Button.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
            this.Cancel_Button.ImageOffset = 2;
            this.Cancel_Button.IsPressed = false;
            this.Cancel_Button.KeepPress = false;
            this.Cancel_Button.Location = new System.Drawing.Point(8, 42);
            this.Cancel_Button.MaxImageSize = new System.Drawing.Point(24, 24);
            this.Cancel_Button.MenuPos = new System.Drawing.Point(0, 0);
            this.Cancel_Button.Name = "Cancel_Button";
            this.Cancel_Button.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.Cancel_Button.Radius = 6;
            this.Cancel_Button.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
            this.Cancel_Button.SinglePressButton = true;
            this.Cancel_Button.Size = new System.Drawing.Size(107, 27);
            this.Cancel_Button.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
            this.Cancel_Button.SplitDistance = 0;
            this.Cancel_Button.TabIndex = 27;
            this.Cancel_Button.Text = "Cancel";
            this.Cancel_Button.Title = "";
            this.Cancel_Button.UseVisualStyleBackColor = true;
            this.Cancel_Button.Click += new System.EventHandler(this.Cancel_Button_Click);
            // 
            // OK_button
            // 
            this.OK_button.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
            this.OK_button.BackColor = System.Drawing.Color.Transparent;
            this.OK_button.Checked = false;
            this.OK_button.ColorBase = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(209)))), ((int)(((byte)(140)))));
            this.OK_button.ColorBaseStroke = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(76)))), ((int)(((byte)(62)))), ((int)(((byte)(42)))));
            this.OK_button.ColorOn = System.Drawing.Color.Khaki;
            this.OK_button.ColorOnStroke = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(76)))), ((int)(((byte)(73)))), ((int)(((byte)(44)))));
            this.OK_button.ColorPress = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(209)))), ((int)(((byte)(140)))));
            this.OK_button.ColorPressStroke = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(75)))), ((int)(((byte)(76)))), ((int)(((byte)(59)))));
            this.OK_button.FadingSpeed = 35;
            this.OK_button.FlatAppearance.BorderSize = 0;
            this.OK_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OK_button.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
            this.OK_button.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.OK_button.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
            this.OK_button.ImageOffset = 2;
            this.OK_button.IsPressed = false;
            this.OK_button.KeepPress = false;
            this.OK_button.Location = new System.Drawing.Point(8, 12);
            this.OK_button.MaxImageSize = new System.Drawing.Point(24, 24);
            this.OK_button.MenuPos = new System.Drawing.Point(0, 0);
            this.OK_button.Name = "OK_button";
            this.OK_button.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.OK_button.Radius = 6;
            this.OK_button.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
            this.OK_button.SinglePressButton = true;
            this.OK_button.Size = new System.Drawing.Size(107, 27);
            this.OK_button.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
            this.OK_button.SplitDistance = 0;
            this.OK_button.TabIndex = 26;
            this.OK_button.Text = "OK";
            this.OK_button.Title = "";
            this.OK_button.UseVisualStyleBackColor = true;
            this.OK_button.Click += new System.EventHandler(this.OK_button_Click);
            // 
            // grouper1
            // 
            this.grouper1.BackgroundColor = System.Drawing.Color.BlanchedAlmond;
            this.grouper1.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouper1.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.BackwardDiagonal;
            this.grouper1.BorderColor = System.Drawing.Color.Black;
            this.grouper1.BorderThickness = 1F;
            this.grouper1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grouper1.GroupImage = null;
            this.grouper1.GroupTitle = "Select Text";
            this.grouper1.Location = new System.Drawing.Point(4, 81);
            this.grouper1.Name = "grouper1";
            this.grouper1.Padding = new System.Windows.Forms.Padding(20);
            this.grouper1.PaintGroupBox = false;
            this.grouper1.RoundCorners = 3;
            this.grouper1.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouper1.ShadowControl = false;
            this.grouper1.ShadowThickness = 3;
            this.grouper1.Size = new System.Drawing.Size(118, 135);
            this.grouper1.TabIndex = 2;
            this.grouper1.TinyMode = true;
            this.grouper1.TitleBorder = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.imagePanel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(516, 485);
            this.panel2.TabIndex = 3;
            // 
            // imagePanel
            // 
            this.imagePanel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.imagePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imagePanel.ImagePack = null;
            this.imagePanel.Location = new System.Drawing.Point(0, 0);
            this.imagePanel.Name = "imagePanel";
            this.imagePanel.RectPosition = ((System.Drawing.RectangleF)(resources.GetObject("imagePanel.RectPosition")));
            this.imagePanel.ShowRect = false;
            this.imagePanel.Size = new System.Drawing.Size(516, 485);
            this.imagePanel.TabIndex = 0;
            this.imagePanel.Click += new System.EventHandler(this.imagePanel_Click);
            this.imagePanel.RectangleChangedEvent += new DreamBeam.RectangleChangeHandler(this.imagePanel_RectangleChangedEvent);
            this.imagePanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.imagePanel_MouseClick);
            // 
            // ImageWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 485);
            this.ControlBox = false;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "ImageWindow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Text Margins";
            this.Shown += new System.EventHandler(this.ImageWindow_Shown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ImageWindow_KeyPress);
            this.panel1.ResumeLayout(false);
            this.grouper2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Bounds4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bounds3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bounds2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bounds1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public ImagePanel imagePanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private CodeVendor.Controls.Grouper grouper1;
        public RibbonStyle.RibbonMenuButton OK_button;
        public RibbonStyle.RibbonMenuButton Cancel_Button;
        private CodeVendor.Controls.Grouper grouper2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown Bounds4;
        private System.Windows.Forms.NumericUpDown Bounds3;
        private System.Windows.Forms.NumericUpDown Bounds2;
        private System.Windows.Forms.NumericUpDown Bounds1;
        private System.Windows.Forms.Button button1;

    }
}