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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MediaControls));
			this.buttons = new System.Windows.Forms.ToolStrip();
			this.skipBkButton = new System.Windows.Forms.ToolStripButton();
			this.seekBkButton = new System.Windows.Forms.ToolStripButton();
			this.playButton = new System.Windows.Forms.ToolStripButton();
			this.pauseButton = new System.Windows.Forms.ToolStripButton();
			this.stopButton = new System.Windows.Forms.ToolStripButton();
			this.seekFwButton = new System.Windows.Forms.ToolStripButton();
			this.skipFwButton = new System.Windows.Forms.ToolStripButton();
			this.label = new System.Windows.Forms.Label();
			this.buttons.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttons
			// 
			this.buttons.AllowMerge = false;
			this.buttons.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
			this.buttons.AutoSize = false;
			this.buttons.Dock = System.Windows.Forms.DockStyle.None;
			this.buttons.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.buttons.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.skipBkButton,
            this.seekBkButton,
            this.playButton,
            this.pauseButton,
            this.stopButton,
            this.seekFwButton,
            this.skipFwButton});
			this.buttons.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
			this.buttons.Location = new System.Drawing.Point(6, 24);
			this.buttons.Name = "buttons";
			this.buttons.Size = new System.Drawing.Size(186, 33);
			this.buttons.Stretch = true;
			this.buttons.TabIndex = 1;
			this.buttons.Text = "buttons";
			// 
			// skipBkButton
			// 
			this.skipBkButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.skipBkButton.Image = ((System.Drawing.Image)(resources.GetObject("skipBkButton.Image")));
			this.skipBkButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.skipBkButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.skipBkButton.Name = "skipBkButton";
			this.skipBkButton.Size = new System.Drawing.Size(26, 26);
			this.skipBkButton.Text = "toolStripButton1";
			this.skipBkButton.ToolTipText = "Skip back";
			this.skipBkButton.Click += new System.EventHandler(this.skipBkButton_Click);
			// 
			// seekBkButton
			// 
			this.seekBkButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.seekBkButton.Image = ((System.Drawing.Image)(resources.GetObject("seekBkButton.Image")));
			this.seekBkButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.seekBkButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.seekBkButton.Name = "seekBkButton";
			this.seekBkButton.Size = new System.Drawing.Size(26, 26);
			this.seekBkButton.Text = "toolStripButton2";
			this.seekBkButton.ToolTipText = "Seek back";
			this.seekBkButton.Click += new System.EventHandler(this.seekBkButton_Click);
			// 
			// playButton
			// 
			this.playButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.playButton.Image = ((System.Drawing.Image)(resources.GetObject("playButton.Image")));
			this.playButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.playButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.playButton.Name = "playButton";
			this.playButton.Size = new System.Drawing.Size(26, 26);
			this.playButton.Text = "toolStripButton3";
			this.playButton.ToolTipText = "Play";
			this.playButton.Click += new System.EventHandler(this.playButton_Click);
			// 
			// pauseButton
			// 
			this.pauseButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.pauseButton.Image = ((System.Drawing.Image)(resources.GetObject("pauseButton.Image")));
			this.pauseButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.pauseButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.pauseButton.Name = "pauseButton";
			this.pauseButton.Size = new System.Drawing.Size(26, 26);
			this.pauseButton.Text = "toolStripButton4";
			this.pauseButton.ToolTipText = "Pause";
			this.pauseButton.Click += new System.EventHandler(this.pauseButton_Click);
			// 
			// stopButton
			// 
			this.stopButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.stopButton.Image = ((System.Drawing.Image)(resources.GetObject("stopButton.Image")));
			this.stopButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.stopButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.stopButton.Name = "stopButton";
			this.stopButton.Size = new System.Drawing.Size(26, 26);
			this.stopButton.Text = "toolStripButton5";
			this.stopButton.ToolTipText = "Stop";
			this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
			// 
			// seekFwButton
			// 
			this.seekFwButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.seekFwButton.Image = ((System.Drawing.Image)(resources.GetObject("seekFwButton.Image")));
			this.seekFwButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.seekFwButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.seekFwButton.Name = "seekFwButton";
			this.seekFwButton.Size = new System.Drawing.Size(26, 26);
			this.seekFwButton.Text = "toolStripButton6";
			this.seekFwButton.ToolTipText = "Seek forward";
			this.seekFwButton.Click += new System.EventHandler(this.seekFwButton_Click);
			// 
			// skipFwButton
			// 
			this.skipFwButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.skipFwButton.Image = ((System.Drawing.Image)(resources.GetObject("skipFwButton.Image")));
			this.skipFwButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.skipFwButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.skipFwButton.Name = "skipFwButton";
			this.skipFwButton.Size = new System.Drawing.Size(26, 26);
			this.skipFwButton.Text = "toolStripButton7";
			this.skipFwButton.ToolTipText = "Skip forward";
			this.skipFwButton.Click += new System.EventHandler(this.skipFwButton_Click);
			// 
			// label
			// 
			this.label.Dock = System.Windows.Forms.DockStyle.Top;
			this.label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label.Location = new System.Drawing.Point(0, 0);
			this.label.Name = "label";
			this.label.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
			this.label.Size = new System.Drawing.Size(199, 20);
			this.label.TabIndex = 2;
			this.label.Text = "Label";
			this.label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// MediaControls
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.Controls.Add(this.label);
			this.Controls.Add(this.buttons);
			this.Name = "MediaControls";
			this.Size = new System.Drawing.Size(199, 69);
			this.buttons.ResumeLayout(false);
			this.buttons.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ToolStrip buttons;
		private System.Windows.Forms.ToolStripButton skipBkButton;
		private System.Windows.Forms.ToolStripButton seekBkButton;
		private System.Windows.Forms.ToolStripButton playButton;
		private System.Windows.Forms.ToolStripButton pauseButton;
		private System.Windows.Forms.ToolStripButton stopButton;
		private System.Windows.Forms.ToolStripButton seekFwButton;
		private System.Windows.Forms.ToolStripButton skipFwButton;
		private System.Windows.Forms.Label label;

	}
}
