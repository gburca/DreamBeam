namespace DreamBeam {
	partial class MediaControls {
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
            this.label = new System.Windows.Forms.Label();
            this.skipFwButton = new RibbonStyle.RibbonMenuButton();
            this.seekFwButton = new RibbonStyle.RibbonMenuButton();
            this.stopButton = new RibbonStyle.RibbonMenuButton();
            this.pauseButton = new RibbonStyle.RibbonMenuButton();
            this.playButton = new RibbonStyle.RibbonMenuButton();
            this.seekBkButton = new RibbonStyle.RibbonMenuButton();
            this.skipBkButton = new RibbonStyle.RibbonMenuButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label
            // 
            this.label.Dock = System.Windows.Forms.DockStyle.Top;
            this.label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label.Location = new System.Drawing.Point(0, 0);
            this.label.Margin = new System.Windows.Forms.Padding(0);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(229, 15);
            this.label.TabIndex = 2;
            this.label.Text = "Label";
            this.label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // skipFwButton
            // 
            this.skipFwButton.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
            this.skipFwButton.BackColor = System.Drawing.Color.Transparent;
            this.skipFwButton.Checked = false;
            this.skipFwButton.ColorBase = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(209)))), ((int)(((byte)(240)))));
            this.skipFwButton.ColorBaseStroke = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(59)))), ((int)(((byte)(66)))), ((int)(((byte)(76)))));
            this.skipFwButton.ColorOn = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.skipFwButton.ColorOnStroke = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.skipFwButton.ColorPress = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.skipFwButton.ColorPressStroke = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(58)))), ((int)(((byte)(67)))), ((int)(((byte)(76)))));
            this.skipFwButton.FadingSpeed = 35;
            this.skipFwButton.FlatAppearance.BorderSize = 0;
            this.skipFwButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.skipFwButton.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.Right;
            this.skipFwButton.Image = global::DreamBeam.Properties.Resources.media_skip_forward;
            this.skipFwButton.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
            this.skipFwButton.ImageOffset = 2;
            this.skipFwButton.IsPressed = false;
            this.skipFwButton.KeepPress = false;
            this.skipFwButton.Location = new System.Drawing.Point(180, 2);
            this.skipFwButton.MaxImageSize = new System.Drawing.Point(0, 0);
            this.skipFwButton.MenuPos = new System.Drawing.Point(0, 0);
            this.skipFwButton.Name = "skipFwButton";
            this.skipFwButton.Radius = 6;
            this.skipFwButton.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
            this.skipFwButton.SinglePressButton = false;
            this.skipFwButton.Size = new System.Drawing.Size(26, 26);
            this.skipFwButton.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
            this.skipFwButton.SplitDistance = 0;
            this.skipFwButton.TabIndex = 12;
            this.skipFwButton.Title = "";
            this.skipFwButton.UseVisualStyleBackColor = true;
            this.skipFwButton.Click += new System.EventHandler(this.skipFwButton_Click);
            // 
            // seekFwButton
            // 
            this.seekFwButton.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
            this.seekFwButton.BackColor = System.Drawing.Color.Transparent;
            this.seekFwButton.Checked = false;
            this.seekFwButton.ColorBase = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(209)))), ((int)(((byte)(240)))));
            this.seekFwButton.ColorBaseStroke = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(59)))), ((int)(((byte)(66)))), ((int)(((byte)(76)))));
            this.seekFwButton.ColorOn = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.seekFwButton.ColorOnStroke = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.seekFwButton.ColorPress = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.seekFwButton.ColorPressStroke = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(58)))), ((int)(((byte)(67)))), ((int)(((byte)(76)))));
            this.seekFwButton.FadingSpeed = 35;
            this.seekFwButton.FlatAppearance.BorderSize = 0;
            this.seekFwButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.seekFwButton.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.Center;
            this.seekFwButton.Image = global::DreamBeam.Properties.Resources.media_seek_forward;
            this.seekFwButton.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
            this.seekFwButton.ImageOffset = 2;
            this.seekFwButton.IsPressed = false;
            this.seekFwButton.KeepPress = false;
            this.seekFwButton.Location = new System.Drawing.Point(155, 2);
            this.seekFwButton.MaxImageSize = new System.Drawing.Point(0, 0);
            this.seekFwButton.MenuPos = new System.Drawing.Point(0, 0);
            this.seekFwButton.Name = "seekFwButton";
            this.seekFwButton.Radius = 6;
            this.seekFwButton.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
            this.seekFwButton.SinglePressButton = false;
            this.seekFwButton.Size = new System.Drawing.Size(26, 26);
            this.seekFwButton.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
            this.seekFwButton.SplitDistance = 0;
            this.seekFwButton.TabIndex = 11;
            this.seekFwButton.Title = "";
            this.seekFwButton.UseVisualStyleBackColor = true;
            this.seekFwButton.Click += new System.EventHandler(this.seekFwButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
            this.stopButton.BackColor = System.Drawing.Color.Transparent;
            this.stopButton.Checked = false;
            this.stopButton.ColorBase = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(209)))), ((int)(((byte)(240)))));
            this.stopButton.ColorBaseStroke = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(59)))), ((int)(((byte)(66)))), ((int)(((byte)(76)))));
            this.stopButton.ColorOn = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.stopButton.ColorOnStroke = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.stopButton.ColorPress = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.stopButton.ColorPressStroke = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(58)))), ((int)(((byte)(67)))), ((int)(((byte)(76)))));
            this.stopButton.FadingSpeed = 35;
            this.stopButton.FlatAppearance.BorderSize = 0;
            this.stopButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.stopButton.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.Right;
            this.stopButton.Image = global::DreamBeam.Properties.Resources.media_playback_stop;
            this.stopButton.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
            this.stopButton.ImageOffset = 2;
            this.stopButton.IsPressed = false;
            this.stopButton.KeepPress = false;
            this.stopButton.Location = new System.Drawing.Point(126, 0);
            this.stopButton.MaxImageSize = new System.Drawing.Point(22, 22);
            this.stopButton.MenuPos = new System.Drawing.Point(0, 0);
            this.stopButton.Name = "stopButton";
            this.stopButton.Radius = 6;
            this.stopButton.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
            this.stopButton.SinglePressButton = false;
            this.stopButton.Size = new System.Drawing.Size(30, 30);
            this.stopButton.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
            this.stopButton.SplitDistance = 0;
            this.stopButton.TabIndex = 10;
            this.stopButton.Title = "";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // pauseButton
            // 
            this.pauseButton.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
            this.pauseButton.BackColor = System.Drawing.Color.Transparent;
            this.pauseButton.Checked = false;
            this.pauseButton.ColorBase = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(209)))), ((int)(((byte)(240)))));
            this.pauseButton.ColorBaseStroke = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(59)))), ((int)(((byte)(66)))), ((int)(((byte)(76)))));
            this.pauseButton.ColorOn = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.pauseButton.ColorOnStroke = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.pauseButton.ColorPress = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.pauseButton.ColorPressStroke = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(58)))), ((int)(((byte)(67)))), ((int)(((byte)(76)))));
            this.pauseButton.FadingSpeed = 35;
            this.pauseButton.FlatAppearance.BorderSize = 0;
            this.pauseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pauseButton.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.Center;
            this.pauseButton.Image = global::DreamBeam.Properties.Resources.media_playback_pause;
            this.pauseButton.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
            this.pauseButton.ImageOffset = 4;
            this.pauseButton.IsPressed = false;
            this.pauseButton.KeepPress = false;
            this.pauseButton.Location = new System.Drawing.Point(97, 0);
            this.pauseButton.MaxImageSize = new System.Drawing.Point(22, 22);
            this.pauseButton.MenuPos = new System.Drawing.Point(0, 0);
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Radius = 6;
            this.pauseButton.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
            this.pauseButton.SinglePressButton = false;
            this.pauseButton.Size = new System.Drawing.Size(30, 30);
            this.pauseButton.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
            this.pauseButton.SplitDistance = 0;
            this.pauseButton.TabIndex = 9;
            this.pauseButton.Title = "";
            this.pauseButton.UseVisualStyleBackColor = true;
            this.pauseButton.Click += new System.EventHandler(this.pauseButton_Click);
            // 
            // playButton
            // 
            this.playButton.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
            this.playButton.BackColor = System.Drawing.Color.Transparent;
            this.playButton.Checked = false;
            this.playButton.ColorBase = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(209)))), ((int)(((byte)(240)))));
            this.playButton.ColorBaseStroke = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(59)))), ((int)(((byte)(66)))), ((int)(((byte)(76)))));
            this.playButton.ColorOn = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.playButton.ColorOnStroke = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.playButton.ColorPress = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.playButton.ColorPressStroke = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(58)))), ((int)(((byte)(67)))), ((int)(((byte)(76)))));
            this.playButton.FadingSpeed = 35;
            this.playButton.FlatAppearance.BorderSize = 0;
            this.playButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.playButton.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.Left;
            this.playButton.Image = global::DreamBeam.Properties.Resources.media_playback_start;
            this.playButton.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
            this.playButton.ImageOffset = 4;
            this.playButton.IsPressed = false;
            this.playButton.KeepPress = false;
            this.playButton.Location = new System.Drawing.Point(68, 0);
            this.playButton.MaxImageSize = new System.Drawing.Point(22, 22);
            this.playButton.MenuPos = new System.Drawing.Point(0, 0);
            this.playButton.Name = "playButton";
            this.playButton.Radius = 6;
            this.playButton.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
            this.playButton.SinglePressButton = false;
            this.playButton.Size = new System.Drawing.Size(30, 30);
            this.playButton.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
            this.playButton.SplitDistance = 0;
            this.playButton.TabIndex = 8;
            this.playButton.Title = "";
            this.playButton.UseVisualStyleBackColor = true;
            this.playButton.Click += new System.EventHandler(this.playButton_Click);
            // 
            // seekBkButton
            // 
            this.seekBkButton.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
            this.seekBkButton.BackColor = System.Drawing.Color.Transparent;
            this.seekBkButton.Checked = false;
            this.seekBkButton.ColorBase = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(209)))), ((int)(((byte)(240)))));
            this.seekBkButton.ColorBaseStroke = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(59)))), ((int)(((byte)(66)))), ((int)(((byte)(76)))));
            this.seekBkButton.ColorOn = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.seekBkButton.ColorOnStroke = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.seekBkButton.ColorPress = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.seekBkButton.ColorPressStroke = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(58)))), ((int)(((byte)(67)))), ((int)(((byte)(76)))));
            this.seekBkButton.FadingSpeed = 35;
            this.seekBkButton.FlatAppearance.BorderSize = 0;
            this.seekBkButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.seekBkButton.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.Center;
            this.seekBkButton.Image = global::DreamBeam.Properties.Resources.media_seek_backward;
            this.seekBkButton.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
            this.seekBkButton.ImageOffset = 2;
            this.seekBkButton.IsPressed = false;
            this.seekBkButton.KeepPress = false;
            this.seekBkButton.Location = new System.Drawing.Point(43, 2);
            this.seekBkButton.MaxImageSize = new System.Drawing.Point(0, 0);
            this.seekBkButton.MenuPos = new System.Drawing.Point(0, 0);
            this.seekBkButton.Name = "seekBkButton";
            this.seekBkButton.Radius = 6;
            this.seekBkButton.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
            this.seekBkButton.SinglePressButton = false;
            this.seekBkButton.Size = new System.Drawing.Size(26, 26);
            this.seekBkButton.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
            this.seekBkButton.SplitDistance = 0;
            this.seekBkButton.TabIndex = 7;
            this.seekBkButton.Title = "";
            this.seekBkButton.UseVisualStyleBackColor = true;
            this.seekBkButton.Click += new System.EventHandler(this.seekBkButton_Click);
            // 
            // skipBkButton
            // 
            this.skipBkButton.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
            this.skipBkButton.BackColor = System.Drawing.Color.Transparent;
            this.skipBkButton.Checked = false;
            this.skipBkButton.ColorBase = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(209)))), ((int)(((byte)(240)))));
            this.skipBkButton.ColorBaseStroke = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(59)))), ((int)(((byte)(66)))), ((int)(((byte)(76)))));
            this.skipBkButton.ColorOn = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.skipBkButton.ColorOnStroke = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.skipBkButton.ColorPress = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.skipBkButton.ColorPressStroke = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(58)))), ((int)(((byte)(67)))), ((int)(((byte)(76)))));
            this.skipBkButton.FadingSpeed = 35;
            this.skipBkButton.FlatAppearance.BorderSize = 0;
            this.skipBkButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.skipBkButton.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.Left;
            this.skipBkButton.Image = global::DreamBeam.Properties.Resources.media_skip_backward;
            this.skipBkButton.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
            this.skipBkButton.ImageOffset = 2;
            this.skipBkButton.IsPressed = false;
            this.skipBkButton.KeepPress = false;
            this.skipBkButton.Location = new System.Drawing.Point(18, 2);
            this.skipBkButton.MaxImageSize = new System.Drawing.Point(0, 0);
            this.skipBkButton.MenuPos = new System.Drawing.Point(0, 0);
            this.skipBkButton.Name = "skipBkButton";
            this.skipBkButton.Radius = 6;
            this.skipBkButton.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
            this.skipBkButton.SinglePressButton = false;
            this.skipBkButton.Size = new System.Drawing.Size(26, 26);
            this.skipBkButton.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
            this.skipBkButton.SplitDistance = 0;
            this.skipBkButton.TabIndex = 6;
            this.skipBkButton.Title = "";
            this.skipBkButton.UseVisualStyleBackColor = true;
            this.skipBkButton.Click += new System.EventHandler(this.skipBkButton_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pauseButton);
            this.panel1.Controls.Add(this.skipFwButton);
            this.panel1.Controls.Add(this.skipBkButton);
            this.panel1.Controls.Add(this.seekFwButton);
            this.panel1.Controls.Add(this.seekBkButton);
            this.panel1.Controls.Add(this.stopButton);
            this.panel1.Controls.Add(this.playButton);
            this.panel1.Location = new System.Drawing.Point(0, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(226, 31);
            this.panel1.TabIndex = 13;
            // 
            // MediaControls
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label);
            this.Name = "MediaControls";
            this.Size = new System.Drawing.Size(229, 49);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.Label label;
        private RibbonStyle.RibbonMenuButton skipBkButton;
        private RibbonStyle.RibbonMenuButton seekBkButton;
        private RibbonStyle.RibbonMenuButton playButton;
        private RibbonStyle.RibbonMenuButton pauseButton;
        private RibbonStyle.RibbonMenuButton stopButton;
        private RibbonStyle.RibbonMenuButton seekFwButton;
        private RibbonStyle.RibbonMenuButton skipFwButton;
        private System.Windows.Forms.Panel panel1;

	}
}
