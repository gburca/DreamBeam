using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;

namespace DreamBeam
{
	/// <summary>
	/// Zusammenfassende Beschreibung für WinForm
	/// </summary>
	public class ExportForm : System.Windows.Forms.Form
	{
		/// <summary>
		/// Erforderliche Designervariable
		/// </summary>
		private System.ComponentModel.Container components = null;
		private TD.SandBar.ToolBar toolBar1;
		private System.Windows.Forms.Splitter splitter1;
		private TD.SandBar.ButtonItem Backgrounds_Button;
		private TD.SandBar.ButtonItem MediaList_Button;
		private TD.SandBar.ButtonItem Songs_Button;
		private TD.SandBar.LabelItem labelItem1;
		private System.Windows.Forms.ListBox InnerList;
		private System.Windows.Forms.ListBox ExportList;
		private System.Windows.Forms.Button selectButton;
		private System.Windows.Forms.Button selectAllButton;
		private System.Windows.Forms.Button removeButton;
		private System.Windows.Forms.Button clearButton;
		private CodeVendor.Controls.Grouper songBox;
		private System.Windows.Forms.RadioButton dreamBeamRadio;

		private System.Windows.Forms.RadioButton csvRadio;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.ProgressBar progressBar;
		private System.Windows.Forms.Button exportButton;
		private System.Windows.Forms.RadioButton textFileRadio;
		private System.Windows.Forms.RadioButton singleFileRadio;
		private System.Windows.Forms.RadioButton multipleFilesRadio;
		private System.Windows.Forms.Button closeButton;
		private System.Windows.Forms.CheckBox includeBGcheckBox;
		private System.Windows.Forms.CheckBox convertToJpegcheckBox;
		private System.Windows.Forms.Panel panel3;
		private CodeVendor.Controls.Grouper mediaBox;
		private System.Windows.Forms.CheckBox mediaConvertJpgBox;
		private CodeVendor.Controls.Grouper textFileBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textSongSeperatortext;
		private CodeVendor.Controls.Grouper csvBox;
		private System.Windows.Forms.CheckBox seperateVersesCheckBox;
		private System.Windows.Forms.CheckBox multiLanguageInfoCheckBox;
		private System.Windows.Forms.TextBox newLineText;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox seperatorText;
		private CodeVendor.Controls.Grouper multiFileBox;
		private CodeVendor.Controls.Grouper dreamBeamBox;
		private CodeVendor.Controls.Grouper grouper1;



		public ExportForm()
		{
			//
			// Erforderlich für die Unterstützung des Windows Forms-Designer
			//
			InitializeComponent();

			//
			// TODO: Fügen Sie nach dem Aufruf von InitializeComponent() Konstruktorcode hinzu.
			//
		}

		/// <summary>
		/// Ressourcen nach der Verwendung bereinigen
		/// </summary>
		protected override void Dispose (bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Vom Windows Form-Designer erzeugter Code
		/// <summary>
		/// Erforderliche Methode zur Unterstützung des Designers -
		/// ändern Sie die Methode nicht mit dem Quelltext-Editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.toolBar1 = new TD.SandBar.ToolBar();
            this.labelItem1 = new TD.SandBar.LabelItem();
            this.Songs_Button = new TD.SandBar.ButtonItem();
            this.MediaList_Button = new TD.SandBar.ButtonItem();
            this.Backgrounds_Button = new TD.SandBar.ButtonItem();
            this.InnerList = new System.Windows.Forms.ListBox();
            this.ExportList = new System.Windows.Forms.ListBox();
            this.selectButton = new System.Windows.Forms.Button();
            this.selectAllButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.textFileRadio = new System.Windows.Forms.RadioButton();
            this.csvRadio = new System.Windows.Forms.RadioButton();
            this.dreamBeamRadio = new System.Windows.Forms.RadioButton();
            this.convertToJpegcheckBox = new System.Windows.Forms.CheckBox();
            this.includeBGcheckBox = new System.Windows.Forms.CheckBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.exportButton = new System.Windows.Forms.Button();
            this.multipleFilesRadio = new System.Windows.Forms.RadioButton();
            this.singleFileRadio = new System.Windows.Forms.RadioButton();
            this.closeButton = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.csvBox = new CodeVendor.Controls.Grouper();
            this.seperateVersesCheckBox = new System.Windows.Forms.CheckBox();
            this.multiLanguageInfoCheckBox = new System.Windows.Forms.CheckBox();
            this.newLineText = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.seperatorText = new System.Windows.Forms.TextBox();
            this.mediaBox = new CodeVendor.Controls.Grouper();
            this.mediaConvertJpgBox = new System.Windows.Forms.CheckBox();
            this.textFileBox = new CodeVendor.Controls.Grouper();
            this.label4 = new System.Windows.Forms.Label();
            this.textSongSeperatortext = new System.Windows.Forms.TextBox();
            this.songBox = new CodeVendor.Controls.Grouper();
            this.dreamBeamBox = new CodeVendor.Controls.Grouper();
            this.multiFileBox = new CodeVendor.Controls.Grouper();
            this.grouper1 = new CodeVendor.Controls.Grouper();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.csvBox.SuspendLayout();
            this.mediaBox.SuspendLayout();
            this.textFileBox.SuspendLayout();
            this.songBox.SuspendLayout();
            this.dreamBeamBox.SuspendLayout();
            this.multiFileBox.SuspendLayout();
            this.grouper1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolBar1
            // 
            this.toolBar1.Buttons.AddRange(new TD.SandBar.ToolbarItemBase[] {
            this.labelItem1,
            this.Songs_Button,
            this.MediaList_Button,
            this.Backgrounds_Button});
            this.toolBar1.Closable = false;
            this.toolBar1.Guid = new System.Guid("abab9fbf-d432-4286-abd7-90e687b93cc9");
            this.toolBar1.ImageList = null;
            this.toolBar1.Location = new System.Drawing.Point(0, 0);
            this.toolBar1.Movable = false;
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.Size = new System.Drawing.Size(456, 24);
            this.toolBar1.TabIndex = 0;
            this.toolBar1.Text = "toolBar1";
            this.toolBar1.ButtonClick += new TD.SandBar.ToolBar.ButtonClickEventHandler(this.toolBar1_ButtonClick);
            // 
            // labelItem1
            // 
            this.labelItem1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelItem1.Tag = null;
            this.labelItem1.Text = "Export: ";
            // 
            // Songs_Button
            // 
            this.Songs_Button.BuddyMenu = null;
            this.Songs_Button.Checked = true;
            this.Songs_Button.Icon = null;
            this.Songs_Button.Tag = null;
            this.Songs_Button.Text = "Songs";
            // 
            // MediaList_Button
            // 
            this.MediaList_Button.BuddyMenu = null;
            this.MediaList_Button.Icon = null;
            this.MediaList_Button.Tag = null;
            this.MediaList_Button.Text = "MediaList";
            // 
            // Backgrounds_Button
            // 
            this.Backgrounds_Button.BuddyMenu = null;
            this.Backgrounds_Button.Icon = null;
            this.Backgrounds_Button.Tag = null;
            this.Backgrounds_Button.Text = "Backgrounds";
            // 
            // InnerList
            // 
            this.InnerList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.InnerList.Location = new System.Drawing.Point(8, 16);
            this.InnerList.Name = "InnerList";
            this.InnerList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.InnerList.Size = new System.Drawing.Size(216, 314);
            this.InnerList.TabIndex = 1;
            this.InnerList.DoubleClick += new System.EventHandler(this.selectButton_Click);
            // 
            // ExportList
            // 
            this.ExportList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ExportList.Location = new System.Drawing.Point(232, 16);
            this.ExportList.Name = "ExportList";
            this.ExportList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.ExportList.Size = new System.Drawing.Size(216, 314);
            this.ExportList.TabIndex = 2;
            this.ExportList.DoubleClick += new System.EventHandler(this.removeButton_Click);
            // 
            // selectButton
            // 
            this.selectButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.selectButton.Location = new System.Drawing.Point(32, 336);
            this.selectButton.Name = "selectButton";
            this.selectButton.Size = new System.Drawing.Size(64, 18);
            this.selectButton.TabIndex = 5;
            this.selectButton.Text = "Select ->";
            this.selectButton.Click += new System.EventHandler(this.selectButton_Click);
            // 
            // selectAllButton
            // 
            this.selectAllButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.selectAllButton.Location = new System.Drawing.Point(128, 336);
            this.selectAllButton.Name = "selectAllButton";
            this.selectAllButton.Size = new System.Drawing.Size(64, 18);
            this.selectAllButton.TabIndex = 6;
            this.selectAllButton.Text = "All ->>";
            this.selectAllButton.Click += new System.EventHandler(this.selectAllButton_Click);
            // 
            // removeButton
            // 
            this.removeButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.removeButton.Location = new System.Drawing.Point(264, 336);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(64, 18);
            this.removeButton.TabIndex = 7;
            this.removeButton.Text = "<- Remove";
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // clearButton
            // 
            this.clearButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.clearButton.Location = new System.Drawing.Point(344, 336);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(64, 18);
            this.clearButton.TabIndex = 8;
            this.clearButton.Text = "Clear";
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // textFileRadio
            // 
            this.textFileRadio.Location = new System.Drawing.Point(16, 48);
            this.textFileRadio.Name = "textFileRadio";
            this.textFileRadio.Size = new System.Drawing.Size(168, 24);
            this.textFileRadio.TabIndex = 3;
            this.textFileRadio.Text = "Text File";
            this.textFileRadio.CheckedChanged += new System.EventHandler(this.dreamBeamRadio_CheckedChanged);
            // 
            // csvRadio
            // 
            this.csvRadio.Location = new System.Drawing.Point(16, 72);
            this.csvRadio.Name = "csvRadio";
            this.csvRadio.Size = new System.Drawing.Size(136, 24);
            this.csvRadio.TabIndex = 2;
            this.csvRadio.Text = "CSV";
            this.csvRadio.CheckedChanged += new System.EventHandler(this.dreamBeamRadio_CheckedChanged);
            // 
            // dreamBeamRadio
            // 
            this.dreamBeamRadio.Checked = true;
            this.dreamBeamRadio.Location = new System.Drawing.Point(16, 24);
            this.dreamBeamRadio.Name = "dreamBeamRadio";
            this.dreamBeamRadio.Size = new System.Drawing.Size(168, 24);
            this.dreamBeamRadio.TabIndex = 0;
            this.dreamBeamRadio.TabStop = true;
            this.dreamBeamRadio.Text = "Dreambeam Song Package";
            this.dreamBeamRadio.CheckedChanged += new System.EventHandler(this.dreamBeamRadio_CheckedChanged);
            // 
            // convertToJpegcheckBox
            // 
            this.convertToJpegcheckBox.Checked = true;
            this.convertToJpegcheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.convertToJpegcheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.convertToJpegcheckBox.Location = new System.Drawing.Point(24, 56);
            this.convertToJpegcheckBox.Name = "convertToJpegcheckBox";
            this.convertToJpegcheckBox.Size = new System.Drawing.Size(168, 24);
            this.convertToJpegcheckBox.TabIndex = 3;
            this.convertToJpegcheckBox.Text = "Convert to JPEG";
            // 
            // includeBGcheckBox
            // 
            this.includeBGcheckBox.Checked = true;
            this.includeBGcheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.includeBGcheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.includeBGcheckBox.Location = new System.Drawing.Point(8, 32);
            this.includeBGcheckBox.Name = "includeBGcheckBox";
            this.includeBGcheckBox.Size = new System.Drawing.Size(184, 24);
            this.includeBGcheckBox.TabIndex = 2;
            this.includeBGcheckBox.Text = "Include Backgrounds";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(147, 0);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(307, 23);
            this.progressBar.TabIndex = 12;
            // 
            // exportButton
            // 
            this.exportButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.exportButton.Location = new System.Drawing.Point(0, 0);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(72, 24);
            this.exportButton.TabIndex = 13;
            this.exportButton.Text = "Export";
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // multipleFilesRadio
            // 
            this.multipleFilesRadio.Location = new System.Drawing.Point(104, 13);
            this.multipleFilesRadio.Name = "multipleFilesRadio";
            this.multipleFilesRadio.Size = new System.Drawing.Size(88, 24);
            this.multipleFilesRadio.TabIndex = 1;
            this.multipleFilesRadio.Text = "Multiple Files";
            // 
            // singleFileRadio
            // 
            this.singleFileRadio.Checked = true;
            this.singleFileRadio.Location = new System.Drawing.Point(8, 13);
            this.singleFileRadio.Name = "singleFileRadio";
            this.singleFileRadio.Size = new System.Drawing.Size(88, 24);
            this.singleFileRadio.TabIndex = 0;
            this.singleFileRadio.TabStop = true;
            this.singleFileRadio.Text = "Single File";
            this.singleFileRadio.CheckedChanged += new System.EventHandler(this.singleFileRadio_CheckedChanged);
            // 
            // closeButton
            // 
            this.closeButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.closeButton.Location = new System.Drawing.Point(73, 0);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(72, 24);
            this.closeButton.TabIndex = 15;
            this.closeButton.Text = "Close";
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.exportButton);
            this.panel2.Controls.Add(this.progressBar);
            this.panel2.Controls.Add(this.closeButton);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 501);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(456, 24);
            this.panel2.TabIndex = 18;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.csvBox);
            this.panel3.Controls.Add(this.mediaBox);
            this.panel3.Controls.Add(this.textFileBox);
            this.panel3.Controls.Add(this.songBox);
            this.panel3.Controls.Add(this.dreamBeamBox);
            this.panel3.Controls.Add(this.multiFileBox);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 373);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(456, 128);
            this.panel3.TabIndex = 21;
            // 
            // csvBox
            // 
            this.csvBox.BackgroundColor = System.Drawing.Color.White;
            this.csvBox.BackgroundGradientColor = System.Drawing.Color.SteelBlue;
            this.csvBox.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.None;
            this.csvBox.BorderColor = System.Drawing.Color.Black;
            this.csvBox.BorderThickness = 1F;
            this.csvBox.Controls.Add(this.seperateVersesCheckBox);
            this.csvBox.Controls.Add(this.multiLanguageInfoCheckBox);
            this.csvBox.Controls.Add(this.newLineText);
            this.csvBox.Controls.Add(this.label5);
            this.csvBox.Controls.Add(this.label6);
            this.csvBox.Controls.Add(this.seperatorText);
            this.csvBox.CustomGroupBoxColor = System.Drawing.Color.White;
            this.csvBox.GroupImage = null;
            this.csvBox.GroupTitle = "File Options";
            this.csvBox.Location = new System.Drawing.Point(240, 288);
            this.csvBox.Name = "csvBox";
            this.csvBox.Padding = new System.Windows.Forms.Padding(20);
            this.csvBox.PaintGroupBox = false;
            this.csvBox.RoundCorners = 10;
            this.csvBox.ShadowColor = System.Drawing.Color.DarkGray;
            this.csvBox.ShadowControl = true;
            this.csvBox.ShadowThickness = 3;
            this.csvBox.Size = new System.Drawing.Size(208, 120);
            this.csvBox.TabIndex = 23;
            this.csvBox.TinyMode = false;
            this.csvBox.TitleBorder = true;
            this.csvBox.Visible = false;
            // 
            // seperateVersesCheckBox
            // 
            this.seperateVersesCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.seperateVersesCheckBox.Location = new System.Drawing.Point(12, 90);
            this.seperateVersesCheckBox.Name = "seperateVersesCheckBox";
            this.seperateVersesCheckBox.Size = new System.Drawing.Size(176, 24);
            this.seperateVersesCheckBox.TabIndex = 27;
            this.seperateVersesCheckBox.Text = "Seperate Verses";
            // 
            // multiLanguageInfoCheckBox
            // 
            this.multiLanguageInfoCheckBox.Checked = true;
            this.multiLanguageInfoCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.multiLanguageInfoCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.multiLanguageInfoCheckBox.Location = new System.Drawing.Point(12, 70);
            this.multiLanguageInfoCheckBox.Name = "multiLanguageInfoCheckBox";
            this.multiLanguageInfoCheckBox.Size = new System.Drawing.Size(176, 24);
            this.multiLanguageInfoCheckBox.TabIndex = 26;
            this.multiLanguageInfoCheckBox.Text = "Include Multi Language Info";
            // 
            // newLineText
            // 
            this.newLineText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.newLineText.Location = new System.Drawing.Point(132, 48);
            this.newLineText.Name = "newLineText";
            this.newLineText.Size = new System.Drawing.Size(64, 20);
            this.newLineText.TabIndex = 25;
            this.newLineText.Text = "\\";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(12, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 16);
            this.label5.TabIndex = 24;
            this.label5.Text = "New Line ";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(12, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 16);
            this.label6.TabIndex = 23;
            this.label6.Text = "Seperator";
            // 
            // seperatorText
            // 
            this.seperatorText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.seperatorText.Location = new System.Drawing.Point(132, 30);
            this.seperatorText.Name = "seperatorText";
            this.seperatorText.Size = new System.Drawing.Size(64, 20);
            this.seperatorText.TabIndex = 22;
            this.seperatorText.Text = ";";
            // 
            // mediaBox
            // 
            this.mediaBox.BackgroundColor = System.Drawing.Color.White;
            this.mediaBox.BackgroundGradientColor = System.Drawing.Color.SteelBlue;
            this.mediaBox.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.None;
            this.mediaBox.BorderColor = System.Drawing.Color.Black;
            this.mediaBox.BorderThickness = 1F;
            this.mediaBox.Controls.Add(this.mediaConvertJpgBox);
            this.mediaBox.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mediaBox.GroupImage = null;
            this.mediaBox.GroupTitle = "File Options";
            this.mediaBox.Location = new System.Drawing.Point(232, 152);
            this.mediaBox.Name = "mediaBox";
            this.mediaBox.Padding = new System.Windows.Forms.Padding(20);
            this.mediaBox.PaintGroupBox = false;
            this.mediaBox.RoundCorners = 10;
            this.mediaBox.ShadowColor = System.Drawing.Color.DarkGray;
            this.mediaBox.ShadowControl = true;
            this.mediaBox.ShadowThickness = 3;
            this.mediaBox.Size = new System.Drawing.Size(208, 88);
            this.mediaBox.TabIndex = 26;
            this.mediaBox.TinyMode = false;
            this.mediaBox.TitleBorder = true;
            this.mediaBox.Visible = false;
            // 
            // mediaConvertJpgBox
            // 
            this.mediaConvertJpgBox.Checked = true;
            this.mediaConvertJpgBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mediaConvertJpgBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.mediaConvertJpgBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mediaConvertJpgBox.Location = new System.Drawing.Point(16, 40);
            this.mediaConvertJpgBox.Name = "mediaConvertJpgBox";
            this.mediaConvertJpgBox.Size = new System.Drawing.Size(168, 24);
            this.mediaConvertJpgBox.TabIndex = 3;
            this.mediaConvertJpgBox.Text = "Convert to JPEG";
            // 
            // textFileBox
            // 
            this.textFileBox.BackgroundColor = System.Drawing.Color.White;
            this.textFileBox.BackgroundGradientColor = System.Drawing.Color.SteelBlue;
            this.textFileBox.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.None;
            this.textFileBox.BorderColor = System.Drawing.Color.Black;
            this.textFileBox.BorderThickness = 1F;
            this.textFileBox.Controls.Add(this.label4);
            this.textFileBox.Controls.Add(this.textSongSeperatortext);
            this.textFileBox.CustomGroupBoxColor = System.Drawing.Color.White;
            this.textFileBox.GroupImage = null;
            this.textFileBox.GroupTitle = "File Options";
            this.textFileBox.Location = new System.Drawing.Point(16, 184);
            this.textFileBox.Name = "textFileBox";
            this.textFileBox.Padding = new System.Windows.Forms.Padding(20);
            this.textFileBox.PaintGroupBox = false;
            this.textFileBox.RoundCorners = 10;
            this.textFileBox.ShadowColor = System.Drawing.Color.DarkGray;
            this.textFileBox.ShadowControl = true;
            this.textFileBox.ShadowThickness = 3;
            this.textFileBox.Size = new System.Drawing.Size(208, 88);
            this.textFileBox.TabIndex = 22;
            this.textFileBox.TinyMode = false;
            this.textFileBox.TitleBorder = true;
            this.textFileBox.Visible = false;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(8, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 16);
            this.label4.TabIndex = 19;
            this.label4.Text = "Song Seperator";
            // 
            // textSongSeperatortext
            // 
            this.textSongSeperatortext.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textSongSeperatortext.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textSongSeperatortext.Location = new System.Drawing.Point(8, 43);
            this.textSongSeperatortext.Name = "textSongSeperatortext";
            this.textSongSeperatortext.Size = new System.Drawing.Size(192, 20);
            this.textSongSeperatortext.TabIndex = 18;
            this.textSongSeperatortext.Text = "\\n\\n\\n----------------------------------------\\n\\n\\n\\n";
            // 
            // songBox
            // 
            this.songBox.BackgroundColor = System.Drawing.Color.White;
            this.songBox.BackgroundGradientColor = System.Drawing.Color.SteelBlue;
            this.songBox.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.None;
            this.songBox.BorderColor = System.Drawing.Color.Black;
            this.songBox.BorderThickness = 1F;
            this.songBox.Controls.Add(this.textFileRadio);
            this.songBox.Controls.Add(this.csvRadio);
            this.songBox.Controls.Add(this.dreamBeamRadio);
            this.songBox.CustomGroupBoxColor = System.Drawing.Color.White;
            this.songBox.GroupImage = null;
            this.songBox.GroupTitle = "Format";
            this.songBox.Location = new System.Drawing.Point(16, 4);
            this.songBox.Name = "songBox";
            this.songBox.Padding = new System.Windows.Forms.Padding(20);
            this.songBox.PaintGroupBox = false;
            this.songBox.RoundCorners = 10;
            this.songBox.ShadowColor = System.Drawing.Color.DarkGray;
            this.songBox.ShadowControl = true;
            this.songBox.ShadowThickness = 2;
            this.songBox.Size = new System.Drawing.Size(208, 120);
            this.songBox.TabIndex = 27;
            this.songBox.TinyMode = false;
            this.songBox.TitleBorder = true;
            // 
            // dreamBeamBox
            // 
            this.dreamBeamBox.BackgroundColor = System.Drawing.Color.White;
            this.dreamBeamBox.BackgroundGradientColor = System.Drawing.Color.SteelBlue;
            this.dreamBeamBox.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.None;
            this.dreamBeamBox.BorderColor = System.Drawing.Color.Black;
            this.dreamBeamBox.BorderThickness = 1F;
            this.dreamBeamBox.Controls.Add(this.convertToJpegcheckBox);
            this.dreamBeamBox.Controls.Add(this.includeBGcheckBox);
            this.dreamBeamBox.CustomGroupBoxColor = System.Drawing.Color.White;
            this.dreamBeamBox.GroupImage = null;
            this.dreamBeamBox.GroupTitle = "File Options";
            this.dreamBeamBox.Location = new System.Drawing.Point(232, 4);
            this.dreamBeamBox.Name = "dreamBeamBox";
            this.dreamBeamBox.Padding = new System.Windows.Forms.Padding(20);
            this.dreamBeamBox.PaintGroupBox = false;
            this.dreamBeamBox.RoundCorners = 10;
            this.dreamBeamBox.ShadowColor = System.Drawing.Color.DarkGray;
            this.dreamBeamBox.ShadowControl = true;
            this.dreamBeamBox.ShadowThickness = 2;
            this.dreamBeamBox.Size = new System.Drawing.Size(208, 88);
            this.dreamBeamBox.TabIndex = 25;
            this.dreamBeamBox.TinyMode = false;
            this.dreamBeamBox.TitleBorder = true;
            // 
            // multiFileBox
            // 
            this.multiFileBox.BackgroundColor = System.Drawing.Color.White;
            this.multiFileBox.BackgroundGradientColor = System.Drawing.Color.SteelBlue;
            this.multiFileBox.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.None;
            this.multiFileBox.BorderColor = System.Drawing.Color.Black;
            this.multiFileBox.BorderThickness = 1F;
            this.multiFileBox.Controls.Add(this.multipleFilesRadio);
            this.multiFileBox.Controls.Add(this.singleFileRadio);
            this.multiFileBox.CustomGroupBoxColor = System.Drawing.Color.White;
            this.multiFileBox.GroupImage = null;
            this.multiFileBox.GroupTitle = "";
            this.multiFileBox.Location = new System.Drawing.Point(232, 86);
            this.multiFileBox.Name = "multiFileBox";
            this.multiFileBox.Padding = new System.Windows.Forms.Padding(20);
            this.multiFileBox.PaintGroupBox = false;
            this.multiFileBox.RoundCorners = 10;
            this.multiFileBox.ShadowColor = System.Drawing.Color.DarkGray;
            this.multiFileBox.ShadowControl = true;
            this.multiFileBox.ShadowThickness = 2;
            this.multiFileBox.Size = new System.Drawing.Size(208, 40);
            this.multiFileBox.TabIndex = 24;
            this.multiFileBox.TinyMode = false;
            this.multiFileBox.TitleBorder = true;
            // 
            // grouper1
            // 
            this.grouper1.BackgroundColor = System.Drawing.Color.White;
            this.grouper1.BackgroundGradientColor = System.Drawing.Color.LightSteelBlue;
            this.grouper1.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouper1.BorderColor = System.Drawing.Color.Transparent;
            this.grouper1.BorderThickness = 1F;
            this.grouper1.Controls.Add(this.InnerList);
            this.grouper1.Controls.Add(this.ExportList);
            this.grouper1.Controls.Add(this.selectButton);
            this.grouper1.Controls.Add(this.selectAllButton);
            this.grouper1.Controls.Add(this.removeButton);
            this.grouper1.Controls.Add(this.clearButton);
            this.grouper1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grouper1.GroupImage = null;
            this.grouper1.GroupTitle = "";
            this.grouper1.Location = new System.Drawing.Point(0, 14);
            this.grouper1.Name = "grouper1";
            this.grouper1.Padding = new System.Windows.Forms.Padding(20);
            this.grouper1.PaintGroupBox = false;
            this.grouper1.RoundCorners = 1;
            this.grouper1.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouper1.ShadowControl = false;
            this.grouper1.ShadowThickness = 3;
            this.grouper1.Size = new System.Drawing.Size(456, 354);
            this.grouper1.TabIndex = 22;
            this.grouper1.TinyMode = false;
            this.grouper1.TitleBorder = true;
            // 
            // ExportForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(456, 525);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.toolBar1);
            this.Controls.Add(this.grouper1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ExportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Export";
            this.Load += new System.EventHandler(this.ExportForm_Load);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.csvBox.ResumeLayout(false);
            this.csvBox.PerformLayout();
            this.mediaBox.ResumeLayout(false);
            this.textFileBox.ResumeLayout(false);
            this.textFileBox.PerformLayout();
            this.songBox.ResumeLayout(false);
            this.dreamBeamBox.ResumeLayout(false);
            this.multiFileBox.ResumeLayout(false);
            this.grouper1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

			///<summary>Reads all Songs in Directory, validates if it is a Song and put's them into the RightDocks_SongList Box </summary>
			public void ListSongs() {
				InnerList.Items.Clear();

                string strSongDir = Tools.GetDirectory(DirType.Songs);
                    //Tools.DreamBeamPath()+"\\Songs";
				if(!System.IO.Directory.Exists(strSongDir)) {
					System.IO.Directory.CreateDirectory(strSongDir);
				}
				string[] dirs2 = Directory.GetFiles(@strSongDir, "*.xml");
				Song Song = new Song();
				foreach (string dir2 in dirs2) {
	//TODO				if (Song.isSong(Path.GetFileName(dir2))) {
						string temp = Path.GetFileName(dir2);
						InnerList.Items.Add(temp.Substring(0,temp.Length-4));
					}
	//			}
			}

				///<summary>Reads all MediaLists in Directory, validates if it is a MediaList and put's them into the RightDocks_SongList Box </summary>
				public void ListMediaLists(){
					InnerList.Items.Clear();
                    string strSongDir = Tools.GetDirectory(DirType.MediaLists);
					if(!System.IO.Directory.Exists(strSongDir)){
						System.IO.Directory.CreateDirectory(strSongDir);
					}
					Song Song = new Song();
					string[] dirs2 = Directory.GetFiles(@strSongDir, "*.xml");
					foreach (string dir2 in dirs2){
// TODO					   if (Song.isSong(Path.GetFileName(dir2))){
						   string temp = Path.GetFileName(dir2);
						   InnerList.Items.Add(temp.Substring(0,temp.Length-4));
//					   }
					}
				}

						/// <summary></summary>
				public void ListDirectories(){
					string directory = @"Backgrounds\";


					int intImageCount=0;
				  //	_MainForm.RightDocks_imageList.Images.Clear();
				  //	_MainForm.RightDocks_ImageListBox.Items.Clear();

					// Define Directory and ImageTypes

                    string strImageDir = Tools.GetDirectory(DirType.DataRoot) +"\\" + directory;
						if(!System.IO.Directory.Exists(strImageDir)){
						System.IO.Directory.CreateDirectory(strImageDir);
					}
					string[] folders = Directory.GetDirectories(@strImageDir);
					InnerList.Items.Clear();

					foreach (string folder in folders){
						InnerList.Items.Add(Tools.Reverse(Tools.Reverse(folder).Substring(0,Tools.Reverse(folder).IndexOf(@"\"))));
					}
			   }

		private void toolBar1_ButtonClick(object sender, TD.SandBar.ToolBarItemEventArgs e)
		{
			if (e.Item == Songs_Button ){
				if(Songs_Button.Checked == false)ExportList.Items.Clear();
				Songs_Button.Checked = true;
				MediaList_Button.Checked = false;
				Backgrounds_Button.Checked = false;

				multiFileBox.Location = new Point(232, 80);
				songBox.Show();
				mediaBox.Hide();
				setSongMenus();

				ListSongs();
			}
			if (e.Item == MediaList_Button ){
				if(MediaList_Button.Checked == false)ExportList.Items.Clear();
				Songs_Button.Checked = false;
				MediaList_Button.Checked = true;
				Backgrounds_Button.Checked = false;

				multiFileBox.Location = dreamBeamBox.Location;
				multiFileBox.Show();
				mediaBox.Location = songBox.Location;
				mediaBox.Show();
				songBox.Hide();
				textFileBox.Hide();
				dreamBeamBox.Hide();
				csvBox.Hide();

				ListMediaLists();
			}
			if (e.Item == Backgrounds_Button ){
				if(Backgrounds_Button.Checked == false)ExportList.Items.Clear();
				Songs_Button.Checked = false;
				MediaList_Button.Checked = false;
				Backgrounds_Button.Checked = true;



				multiFileBox.Location = dreamBeamBox.Location;
				multiFileBox.Show();
				mediaBox.Location = songBox.Location;
				mediaBox.Show();
				textFileBox.Hide();
				songBox.Hide();
				dreamBeamBox.Hide();
				csvBox.Hide();

				ListDirectories();
			}
		}

		private void selectButton_Click(object sender, System.EventArgs e)
		{
			bool insertItem;

			for (int i =0; i<InnerList.SelectedItems.Count;i++){
				insertItem = true;
				for (int j = 0; j< ExportList.Items.Count;j++){

					if(InnerList.SelectedItems[i].ToString() == ExportList.Items[j].ToString()){
						insertItem = false;
					}
				}
				if(insertItem){
                	ExportList.Items.Add(InnerList.SelectedItems[i].ToString());
				}
			}

			ArrayList tempArray = new ArrayList();
			for (int j = 0; j< ExportList.Items.Count;j++){
				tempArray.Add (ExportList.Items[j].ToString());
			}
			tempArray.Sort();
			ExportList.Items.Clear();
			for (int j = 0; j < tempArray.Count;j++){
				ExportList.Items.Add(tempArray[j]);
			}
		}
		
		private void selectAllButton_Click(object sender, System.EventArgs e)
		{
			ExportList.Items.Clear();
			for (int j = 0; j< InnerList.Items.Count;j++){
				   ExportList.Items.Add(InnerList.Items[j].ToString());

			}

		}
		
		private void clearButton_Click(object sender, System.EventArgs e)
		{
			ExportList.Items.Clear();
		}
		
		private void removeButton_Click(object sender, System.EventArgs e)
		{
				ArrayList tempArray = new ArrayList();
				bool keepItem;
				for (int j = 0; j< ExportList.Items.Count;j++){
					keepItem = true;
					for (int i =0; i<ExportList.SelectedItems.Count;i++){
						if(ExportList.Items[j].ToString()==ExportList.SelectedItems[i].ToString()){
							keepItem = false;
						}
					}
					if (keepItem){
						tempArray.Add(ExportList.Items[j].ToString());
					}
				}
				ExportList.Items.Clear();
				for (int j = 0; j < tempArray.Count;j++){
					ExportList.Items.Add(tempArray[j]);
				}
		}

		private void dreamBeamRadio_CheckedChanged(object sender, System.EventArgs e)
		{
			setSongMenus();

		}

		private void setSongMenus(){
			if(dreamBeamRadio.Checked){
				dreamBeamBox.Show();
				textFileBox.Hide();
				csvBox.Hide();
				multiFileBox.Show();
			}
			if(textFileRadio.Checked){
				textFileBox.Location = dreamBeamBox.Location;
				dreamBeamBox.Hide();
				textFileBox.Show();
				csvBox.Hide();
				multiFileBox.Show();
			}
			if(csvRadio.Checked){
				csvBox.Location = dreamBeamBox.Location;
				dreamBeamBox.Hide();
				textFileBox.Hide();
				csvBox.Show();
				multiFileBox.Hide();
			}
		}
		private void exportButton_Click(object sender, System.EventArgs e)
		{
			#region Songs
			if(Songs_Button.Checked){
				if(dreamBeamRadio.Checked){
					string filePath = "";
					if(singleFileRadio.Checked){
						SaveFileDialog saveFileDialog = new SaveFileDialog();
	//					saveFileDialog.Title = "
						saveFileDialog.Filter = "DreamBeam Song Package|*.dbsongs*";
						if(saveFileDialog.ShowDialog() == DialogResult.OK){
							filePath = Path.GetDirectoryName(saveFileDialog.FileName)+"\\"+Path.GetFileNameWithoutExtension(saveFileDialog.FileName)+".dbsongs";
						}
						saveFileDialog.Dispose();
					}else{
						FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();;

						folderBrowserDialog.Description = "Please select the Folder where to save all exportet songs.";
						folderBrowserDialog.ShowNewFolderButton = true;
						if(folderBrowserDialog.ShowDialog()== DialogResult.OK){
							filePath = folderBrowserDialog.SelectedPath;
						}
						folderBrowserDialog.Dispose();
					}
					if(filePath!="") Exporter.CreateDreamSongPackage(this,filePath,this.ExportList, progressBar, includeBGcheckBox.Checked,convertToJpegcheckBox.Checked,singleFileRadio.Checked);
				}

				if(textFileRadio.Checked){
					string filePath = "";
					if(singleFileRadio.Checked){
						SaveFileDialog saveFileDialog = new SaveFileDialog();
	//					saveFileDialog.Title = "
						saveFileDialog.Filter = "Text File|*.txt";
						if(saveFileDialog.ShowDialog() == DialogResult.OK){
							filePath = Path.GetDirectoryName(saveFileDialog.FileName)+"\\"+Path.GetFileNameWithoutExtension(saveFileDialog.FileName)+".txt";
						}
						saveFileDialog.Dispose();
						if(filePath!="") Exporter.CreateTextSongFile(this,filePath,this.ExportList, progressBar,textSongSeperatortext.Text);
					}else{
						FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();;
						folderBrowserDialog.Description = "Please select a Folder where to save all exported songs.";
						folderBrowserDialog.ShowNewFolderButton = true;
						if(folderBrowserDialog.ShowDialog()== DialogResult.OK){
							filePath = folderBrowserDialog.SelectedPath;
						}

						folderBrowserDialog.Dispose();
						if(filePath!="") Exporter.CreateTextSongFiles(this,filePath,this.ExportList, progressBar);
					}

				}

				if(csvRadio.Checked){
					string filePath = "";
						SaveFileDialog saveFileDialog = new SaveFileDialog();
						saveFileDialog.Filter = "CSV File|*.csv";
						if(saveFileDialog.ShowDialog() == DialogResult.OK){
							filePath = Path.GetDirectoryName(saveFileDialog.FileName)+"\\"+Path.GetFileNameWithoutExtension(saveFileDialog.FileName)+".csv";
						}
						saveFileDialog.Dispose();
						if(filePath!="") Exporter.CreateCSVFile(this, filePath,this.ExportList, progressBar,seperatorText.Text,newLineText.Text,multiLanguageInfoCheckBox.Checked,seperateVersesCheckBox.Checked);
				}
			}
			#endregion

			#region MediaList
			if(this.MediaList_Button.Checked){
					string filePath = "";
					if(singleFileRadio.Checked){
						SaveFileDialog saveFileDialog = new SaveFileDialog();
	//					saveFileDialog.Title = "
						saveFileDialog.Filter = "DreamBeam Media Package|*.dbmedia";
						if(saveFileDialog.ShowDialog() == DialogResult.OK){
							filePath = Path.GetDirectoryName(saveFileDialog.FileName)+"\\"+Path.GetFileNameWithoutExtension(saveFileDialog.FileName)+".dbmedia";
						}
						saveFileDialog.Dispose();
					}else{
						FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();;
						folderBrowserDialog.Description = "Please select a Folder where to save all exported Media Lists.";
						folderBrowserDialog.ShowNewFolderButton = true;
						if(folderBrowserDialog.ShowDialog()== DialogResult.OK){
							filePath = folderBrowserDialog.SelectedPath;
						}
						folderBrowserDialog.Dispose();
					}
					if(filePath!="") Exporter.CreateDreamMediaPackage(this, filePath,this.ExportList, progressBar,mediaConvertJpgBox.Checked,singleFileRadio.Checked);
				
			}
			#endregion

			#region Backgrounds
			if (this.Backgrounds_Button.Checked){
					string filePath = "";
					if(singleFileRadio.Checked){
						SaveFileDialog saveFileDialog = new SaveFileDialog();
						saveFileDialog.Filter = "DreamBeam Background Package|*.dbbg";
						if(saveFileDialog.ShowDialog() == DialogResult.OK){
							filePath = Path.GetDirectoryName(saveFileDialog.FileName)+"\\"+Path.GetFileNameWithoutExtension(saveFileDialog.FileName)+".dbbg";
						}
						saveFileDialog.Dispose();
					}else{
						FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();;
						folderBrowserDialog.Description = "Please select a Folder where to save all exported Backgrounds.";
						folderBrowserDialog.ShowNewFolderButton = true;
						if(folderBrowserDialog.ShowDialog()== DialogResult.OK){
							filePath = folderBrowserDialog.SelectedPath;
						}
						folderBrowserDialog.Dispose();
					}
					if(filePath!="") Exporter.CreateDreamBGPackage(this, filePath,this.ExportList, progressBar,mediaConvertJpgBox.Checked,singleFileRadio.Checked);
			}
			#endregion

		}

		private void closeButton_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
		
		private void ExportForm_Load(object sender, System.EventArgs e)
		{
			ListSongs();
			this.dreamBeamBox.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.ForwardDiagonal;
			this.multiFileBox.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.ForwardDiagonal;
			this.csvBox.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.ForwardDiagonal;
			this.textFileBox.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.ForwardDiagonal;
			this.mediaBox.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.ForwardDiagonal;
			this.songBox.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.ForwardDiagonal;
			this.grouper1.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.Vertical;
		}

		private void singleFileRadio_CheckedChanged(object sender, System.EventArgs e)
		{
			textSongSeperatortext.Enabled = singleFileRadio.Checked;
		}
		
		private void button1_Click(object sender, System.EventArgs e)
		{
			this.Enabled = false;
		}
		


		
	}
}
