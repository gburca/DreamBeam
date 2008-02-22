namespace DreamBeam {
	partial class BibleText {
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
			this.Results = new DreamBeam.Bible.BibleRTF();
			this.versePanel = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.Verse_ComboBox = new System.Windows.Forms.ComboBox();
			this.FindNext_button = new System.Windows.Forms.Button();
			this.FindPrev_button = new System.Windows.Forms.Button();
			this.FindLast_button = new System.Windows.Forms.Button();
			this.FindFirst_button = new System.Windows.Forms.Button();
			this.searchPanel = new System.Windows.Forms.Panel();
			this.label2 = new System.Windows.Forms.Label();
			this.RegEx_ComboBox = new System.Windows.Forms.ComboBox();
			this.Bookmark_button = new System.Windows.Forms.Button();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.versePanel.SuspendLayout();
			this.searchPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// Results
			// 
			this.Results.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.Results.CurrentVerse = 0;
			this.Results.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.Results.HiglightColor = Khendys.Controls.RtfColor.White;
			this.Results.Location = new System.Drawing.Point(11, 80);
			this.Results.Name = "Results";
			this.Results.ReadOnly = true;
			this.Results.Size = new System.Drawing.Size(484, 602);
			this.Results.TabIndex = 2;
			this.Results.Text = "";
			this.Results.TextColor = Khendys.Controls.RtfColor.Black;
			this.Results.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Results_KeyDown);
			this.Results.MouseEnter += new System.EventHandler(this.Results_MouseEnter);
			this.Results.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Results_MouseDown);
			// 
			// versePanel
			// 
			this.versePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.versePanel.Controls.Add(this.label1);
			this.versePanel.Controls.Add(this.Verse_ComboBox);
			this.versePanel.Controls.Add(this.FindNext_button);
			this.versePanel.Controls.Add(this.FindPrev_button);
			this.versePanel.Controls.Add(this.FindLast_button);
			this.versePanel.Controls.Add(this.FindFirst_button);
			this.versePanel.Location = new System.Drawing.Point(0, 38);
			this.versePanel.Name = "versePanel";
			this.versePanel.Size = new System.Drawing.Size(506, 36);
			this.versePanel.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(8, 11);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(37, 13);
			this.label1.TabIndex = 5;
			this.label1.Text = "Verse:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// Verse_ComboBox
			// 
			this.Verse_ComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.Verse_ComboBox.DropDownWidth = 186;
			this.Verse_ComboBox.Location = new System.Drawing.Point(56, 7);
			this.Verse_ComboBox.Name = "Verse_ComboBox";
			this.Verse_ComboBox.Size = new System.Drawing.Size(274, 21);
			this.Verse_ComboBox.TabIndex = 0;
			this.Verse_ComboBox.SelectedIndexChanged += new System.EventHandler(this.Verse_ComboBox_SelectedIndexChanged);
			this.Verse_ComboBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Verse_ComboBox_KeyUp);
			// 
			// FindNext_button
			// 
			this.FindNext_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.FindNext_button.Image = global::DreamBeam.Properties.Resources.go_down;
			this.FindNext_button.Location = new System.Drawing.Point(340, 2);
			this.FindNext_button.Name = "FindNext_button";
			this.FindNext_button.Size = new System.Drawing.Size(32, 32);
			this.FindNext_button.TabIndex = 1;
			this.toolTip.SetToolTip(this.FindNext_button, "Find next occurance of the RegEx");
			this.FindNext_button.UseVisualStyleBackColor = true;
			this.FindNext_button.Click += new System.EventHandler(this.FindNext_button_Click);
			// 
			// FindPrev_button
			// 
			this.FindPrev_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.FindPrev_button.Image = global::DreamBeam.Properties.Resources.go_up;
			this.FindPrev_button.Location = new System.Drawing.Point(380, 2);
			this.FindPrev_button.Name = "FindPrev_button";
			this.FindPrev_button.Size = new System.Drawing.Size(32, 32);
			this.FindPrev_button.TabIndex = 2;
			this.toolTip.SetToolTip(this.FindPrev_button, "Find previous occurance of the RegEx");
			this.FindPrev_button.UseVisualStyleBackColor = true;
			this.FindPrev_button.Click += new System.EventHandler(this.FindPrev_button_Click);
			// 
			// FindLast_button
			// 
			this.FindLast_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.FindLast_button.Image = global::DreamBeam.Properties.Resources.go_bottom;
			this.FindLast_button.Location = new System.Drawing.Point(460, 2);
			this.FindLast_button.Name = "FindLast_button";
			this.FindLast_button.Size = new System.Drawing.Size(32, 32);
			this.FindLast_button.TabIndex = 4;
			this.toolTip.SetToolTip(this.FindLast_button, "Find last occurance of the RegEx");
			this.FindLast_button.Click += new System.EventHandler(this.FindLast_button_Click);
			// 
			// FindFirst_button
			// 
			this.FindFirst_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.FindFirst_button.Image = global::DreamBeam.Properties.Resources.go_top;
			this.FindFirst_button.Location = new System.Drawing.Point(420, 2);
			this.FindFirst_button.Name = "FindFirst_button";
			this.FindFirst_button.Size = new System.Drawing.Size(32, 32);
			this.FindFirst_button.TabIndex = 3;
			this.toolTip.SetToolTip(this.FindFirst_button, "Find first occurance of the RegEx");
			this.FindFirst_button.Click += new System.EventHandler(this.FindFirst_button_Click);
			// 
			// searchPanel
			// 
			this.searchPanel.Controls.Add(this.label2);
			this.searchPanel.Controls.Add(this.RegEx_ComboBox);
			this.searchPanel.Controls.Add(this.Bookmark_button);
			this.searchPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.searchPanel.Location = new System.Drawing.Point(0, 0);
			this.searchPanel.Name = "searchPanel";
			this.searchPanel.Size = new System.Drawing.Size(506, 38);
			this.searchPanel.TabIndex = 0;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(8, 10);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(44, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Search:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// RegEx_ComboBox
			// 
			this.RegEx_ComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.RegEx_ComboBox.DropDownWidth = 250;
			this.RegEx_ComboBox.Location = new System.Drawing.Point(56, 9);
			this.RegEx_ComboBox.Name = "RegEx_ComboBox";
			this.RegEx_ComboBox.Size = new System.Drawing.Size(338, 21);
			this.RegEx_ComboBox.TabIndex = 0;
			this.RegEx_ComboBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.RegEx_ComboBox_KeyUp);
			this.RegEx_ComboBox.TextChanged += new System.EventHandler(this.RegEx_ComboBox_TextChanged);
			// 
			// Bookmark_button
			// 
			this.Bookmark_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.Bookmark_button.Image = global::DreamBeam.Properties.Resources.bookmark_new;
			this.Bookmark_button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.Bookmark_button.Location = new System.Drawing.Point(404, 4);
			this.Bookmark_button.Name = "Bookmark_button";
			this.Bookmark_button.Size = new System.Drawing.Size(88, 32);
			this.Bookmark_button.TabIndex = 1;
			this.Bookmark_button.Text = "Bookmark";
			this.Bookmark_button.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.toolTip.SetToolTip(this.Bookmark_button, "Bookmark the current verse");
			this.Bookmark_button.UseVisualStyleBackColor = true;
			this.Bookmark_button.Click += new System.EventHandler(this.Bookmark_button_Click);
			// 
			// BibleText
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.searchPanel);
			this.Controls.Add(this.versePanel);
			this.Controls.Add(this.Results);
			this.Name = "BibleText";
			this.Size = new System.Drawing.Size(506, 691);
			this.versePanel.ResumeLayout(false);
			this.versePanel.PerformLayout();
			this.searchPanel.ResumeLayout(false);
			this.searchPanel.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private DreamBeam.Bible.BibleRTF Results;
		private System.Windows.Forms.Panel versePanel;
		private System.Windows.Forms.Button FindLast_button;
		private System.Windows.Forms.Button FindFirst_button;
		private System.Windows.Forms.Button FindNext_button;
		private System.Windows.Forms.Button FindPrev_button;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox Verse_ComboBox;
		private System.Windows.Forms.Panel searchPanel;
		private System.Windows.Forms.Button Bookmark_button;
		private System.Windows.Forms.ComboBox RegEx_ComboBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ToolTip toolTip;
	}
}
