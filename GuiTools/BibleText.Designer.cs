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
			this.BibleText_Results = new DreamBeam.Bible.BibleRTF();
			this.versePanel = new System.Windows.Forms.Panel();
			this.BibleText_FindLast_button = new System.Windows.Forms.Button();
			this.BibleText_FindFirst_button = new System.Windows.Forms.Button();
			this.BibleText_FindPrev_button = new System.Windows.Forms.Button();
			this.BibleText_FindNext_button = new System.Windows.Forms.Button();
			this.BibleText_Verse_ComboBox = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.searchPanel = new System.Windows.Forms.Panel();
			this.BibleText_Bookmark_button = new System.Windows.Forms.Button();
			this.BibleText_RegEx_ComboBox = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.versePanel.SuspendLayout();
			this.searchPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// BibleText_Results
			// 
			this.BibleText_Results.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.BibleText_Results.CurrentVerse = 0;
			this.BibleText_Results.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.BibleText_Results.HiglightColor = Khendys.Controls.RtfColor.White;
			this.BibleText_Results.Location = new System.Drawing.Point(11, 80);
			this.BibleText_Results.Name = "BibleText_Results";
			this.BibleText_Results.ReadOnly = true;
			this.BibleText_Results.Size = new System.Drawing.Size(484, 602);
			this.BibleText_Results.TabIndex = 2;
			this.BibleText_Results.Text = "";
			this.BibleText_Results.TextColor = Khendys.Controls.RtfColor.Black;
			this.BibleText_Results.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BibleText_Results_KeyDown);
			this.BibleText_Results.MouseEnter += new System.EventHandler(this.BibleText_Results_MouseEnter);
			this.BibleText_Results.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BibleText_Results_MouseDown);
			// 
			// versePanel
			// 
			this.versePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.versePanel.Controls.Add(this.label1);
			this.versePanel.Controls.Add(this.BibleText_Verse_ComboBox);
			this.versePanel.Controls.Add(this.BibleText_FindNext_button);
			this.versePanel.Controls.Add(this.BibleText_FindPrev_button);
			this.versePanel.Controls.Add(this.BibleText_FindLast_button);
			this.versePanel.Controls.Add(this.BibleText_FindFirst_button);
			this.versePanel.Location = new System.Drawing.Point(0, 38);
			this.versePanel.Name = "versePanel";
			this.versePanel.Size = new System.Drawing.Size(506, 36);
			this.versePanel.TabIndex = 3;
			// 
			// BibleText_FindLast_button
			// 
			this.BibleText_FindLast_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BibleText_FindLast_button.Location = new System.Drawing.Point(460, 2);
			this.BibleText_FindLast_button.Name = "BibleText_FindLast_button";
			this.BibleText_FindLast_button.Size = new System.Drawing.Size(32, 32);
			this.BibleText_FindLast_button.TabIndex = 0;
			this.BibleText_FindLast_button.Text = "button1";
			this.BibleText_FindLast_button.Click += new System.EventHandler(this.BibleText_FindLast_button_Click);

			// 
			// BibleText_FindFirst_button
			// 
			this.BibleText_FindFirst_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BibleText_FindFirst_button.Location = new System.Drawing.Point(420, 2);
			this.BibleText_FindFirst_button.Name = "BibleText_FindFirst_button";
			this.BibleText_FindFirst_button.Size = new System.Drawing.Size(32, 32);
			this.BibleText_FindFirst_button.TabIndex = 1;
			this.BibleText_FindFirst_button.Text = "button2";
			this.BibleText_FindFirst_button.Click += new System.EventHandler(this.BibleText_FindFirst_button_Click);
			// 
			// BibleText_FindPrev_button
			// 
			this.BibleText_FindPrev_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BibleText_FindPrev_button.Location = new System.Drawing.Point(380, 2);
			this.BibleText_FindPrev_button.Name = "BibleText_FindPrev_button";
			this.BibleText_FindPrev_button.Size = new System.Drawing.Size(32, 32);
			this.BibleText_FindPrev_button.TabIndex = 2;
			this.BibleText_FindPrev_button.Text = "button1";
			this.BibleText_FindPrev_button.UseVisualStyleBackColor = true;
			this.BibleText_FindPrev_button.Click += new System.EventHandler(this.BibleText_FindPrev_button_Click);
			// 
			// BibleText_FindNext_button
			// 
			this.BibleText_FindNext_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BibleText_FindNext_button.Location = new System.Drawing.Point(340, 2);
			this.BibleText_FindNext_button.Name = "BibleText_FindNext_button";
			this.BibleText_FindNext_button.Size = new System.Drawing.Size(32, 32);
			this.BibleText_FindNext_button.TabIndex = 3;
			this.BibleText_FindNext_button.Text = "button1";
			this.BibleText_FindNext_button.UseVisualStyleBackColor = true;
			this.BibleText_FindNext_button.Click += new System.EventHandler(this.BibleText_FindNext_button_Click);
			// 
			// BibleText_Verse_ComboBox
			// 
			this.BibleText_Verse_ComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.BibleText_Verse_ComboBox.DropDownWidth = 186;
			this.BibleText_Verse_ComboBox.Location = new System.Drawing.Point(56, 7);
			this.BibleText_Verse_ComboBox.Name = "BibleText_Verse_ComboBox";
			this.BibleText_Verse_ComboBox.Size = new System.Drawing.Size(274, 21);
			this.BibleText_Verse_ComboBox.TabIndex = 1;
			this.BibleText_Verse_ComboBox.SelectedIndexChanged += new System.EventHandler(this.BibleText_Verse_ComboBox_SelectedIndexChanged);
			this.BibleText_Verse_ComboBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.BibleText_Verse_ComboBox_KeyUp);
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
			// searchPanel
			// 
			this.searchPanel.Controls.Add(this.label2);
			this.searchPanel.Controls.Add(this.BibleText_RegEx_ComboBox);
			this.searchPanel.Controls.Add(this.BibleText_Bookmark_button);
			this.searchPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.searchPanel.Location = new System.Drawing.Point(0, 0);
			this.searchPanel.Name = "searchPanel";
			this.searchPanel.Size = new System.Drawing.Size(506, 38);
			this.searchPanel.TabIndex = 4;
			// 
			// BibleText_Bookmark_button
			// 
			this.BibleText_Bookmark_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BibleText_Bookmark_button.Location = new System.Drawing.Point(404, 4);
			this.BibleText_Bookmark_button.Name = "BibleText_Bookmark_button";
			this.BibleText_Bookmark_button.Size = new System.Drawing.Size(88, 32);
			this.BibleText_Bookmark_button.TabIndex = 0;
			this.BibleText_Bookmark_button.Text = "Bookmark";
			this.BibleText_Bookmark_button.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.BibleText_Bookmark_button.UseVisualStyleBackColor = true;
			this.BibleText_Bookmark_button.Click += new System.EventHandler(this.BibleText_Bookmark_button_Click);
			// 
			// BibleText_RegEx_ComboBox
			// 
			this.BibleText_RegEx_ComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.BibleText_RegEx_ComboBox.DropDownWidth = 250;
			this.BibleText_RegEx_ComboBox.Location = new System.Drawing.Point(56, 9);
			this.BibleText_RegEx_ComboBox.Name = "BibleText_RegEx_ComboBox";
			this.BibleText_RegEx_ComboBox.Size = new System.Drawing.Size(338, 21);
			this.BibleText_RegEx_ComboBox.TabIndex = 1;
			this.BibleText_RegEx_ComboBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.BibleText_RegEx_ComboBox_KeyUp);
			this.BibleText_RegEx_ComboBox.TextChanged += new System.EventHandler(this.BibleText_RegEx_ComboBox_TextChanged);
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
			// BibleText
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.searchPanel);
			this.Controls.Add(this.versePanel);
			this.Controls.Add(this.BibleText_Results);
			this.Name = "BibleText";
			this.Size = new System.Drawing.Size(506, 691);
			this.versePanel.ResumeLayout(false);
			this.versePanel.PerformLayout();
			this.searchPanel.ResumeLayout(false);
			this.searchPanel.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private DreamBeam.Bible.BibleRTF BibleText_Results;
		private System.Windows.Forms.Panel versePanel;
		private System.Windows.Forms.Button BibleText_FindLast_button;
		private System.Windows.Forms.Button BibleText_FindFirst_button;
		private System.Windows.Forms.Button BibleText_FindNext_button;
		private System.Windows.Forms.Button BibleText_FindPrev_button;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox BibleText_Verse_ComboBox;
		private System.Windows.Forms.Panel searchPanel;
		private System.Windows.Forms.Button BibleText_Bookmark_button;
		private System.Windows.Forms.ComboBox BibleText_RegEx_ComboBox;
		private System.Windows.Forms.Label label2;
	}
}
