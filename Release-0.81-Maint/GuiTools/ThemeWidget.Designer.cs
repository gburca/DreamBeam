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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.bgImageBrowse = new System.Windows.Forms.Button();
			this.BgImagePath = new System.Windows.Forms.TextBox();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.textFormatOptions1 = new DreamBeam.TextFormatOptions();
			this.saveAsBtn = new System.Windows.Forms.Button();
			this.openBtn = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.tabControl.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.bgImageBrowse);
			this.groupBox1.Controls.Add(this.BgImagePath);
			this.groupBox1.Location = new System.Drawing.Point(3, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(416, 49);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Default Background Image";
			// 
			// bgImageBrowse
			// 
			this.bgImageBrowse.Location = new System.Drawing.Point(333, 17);
			this.bgImageBrowse.Name = "bgImageBrowse";
			this.bgImageBrowse.Size = new System.Drawing.Size(75, 23);
			this.bgImageBrowse.TabIndex = 1;
			this.bgImageBrowse.Text = "Browse...";
			this.bgImageBrowse.UseVisualStyleBackColor = true;
			this.bgImageBrowse.Click += new System.EventHandler(this.bgImageBrowse_Click);
			// 
			// BgImagePath
			// 
			this.BgImagePath.Location = new System.Drawing.Point(6, 19);
			this.BgImagePath.Name = "BgImagePath";
			this.BgImagePath.Size = new System.Drawing.Size(321, 20);
			this.BgImagePath.TabIndex = 0;
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.tabPage1);
			this.tabControl.Location = new System.Drawing.Point(9, 58);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(414, 243);
			this.tabControl.TabIndex = 1;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.textFormatOptions1);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(406, 217);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "tabPage1";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// textFormatOptions1
			// 
			this.textFormatOptions1.Location = new System.Drawing.Point(6, 5);
			this.textFormatOptions1.Name = "textFormatOptions1";
			this.textFormatOptions1.Size = new System.Drawing.Size(392, 206);
			this.textFormatOptions1.TabIndex = 0;
			// 
			// saveAsBtn
			// 
			this.saveAsBtn.Location = new System.Drawing.Point(429, 80);
			this.saveAsBtn.Name = "saveAsBtn";
			this.saveAsBtn.Size = new System.Drawing.Size(75, 23);
			this.saveAsBtn.TabIndex = 2;
			this.saveAsBtn.Text = "Save As...";
			this.saveAsBtn.UseVisualStyleBackColor = true;
			this.saveAsBtn.Click += new System.EventHandler(this.saveAsBtn_Click);
			// 
			// openBtn
			// 
			this.openBtn.Location = new System.Drawing.Point(429, 109);
			this.openBtn.Name = "openBtn";
			this.openBtn.Size = new System.Drawing.Size(75, 23);
			this.openBtn.TabIndex = 3;
			this.openBtn.Text = "Open...";
			this.openBtn.UseVisualStyleBackColor = true;
			this.openBtn.Click += new System.EventHandler(this.openBtn_Click);
			// 
			// ThemeWidget
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.openBtn);
			this.Controls.Add(this.saveAsBtn);
			this.Controls.Add(this.tabControl);
			this.Controls.Add(this.groupBox1);
			this.Name = "ThemeWidget";
			this.Size = new System.Drawing.Size(513, 312);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.tabControl.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox BgImagePath;
		private System.Windows.Forms.Button bgImageBrowse;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tabPage1;
		private TextFormatOptions textFormatOptions1;
		private System.Windows.Forms.Button saveAsBtn;
		private System.Windows.Forms.Button openBtn;

	}
}
