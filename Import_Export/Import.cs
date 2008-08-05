using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace DreamBeam
{
	/// <summary>
	/// Zusammenfassende Beschreibung für WinForm
	/// </summary>
	public class ImportForm : System.Windows.Forms.Form
	{
		/// <summary>
		/// Erforderliche Designervariable
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ListBox outerList;
		private System.Windows.Forms.ListBox importList;
		private System.Windows.Forms.Button selectButton;
		private System.Windows.Forms.Button selectAllButton;
		private System.Windows.Forms.Button removeButton;
		private System.Windows.Forms.Button clearButton;
		private TD.SandBar.ToolBar toolBar1;
		private TD.SandBar.LabelItem labelItem1;
		private TD.SandBar.ButtonItem Songs_Button;
		private TD.SandBar.ButtonItem MediaList_Button;
		private TD.SandBar.ButtonItem Backgrounds_Button;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button ImportButton;
		private System.Windows.Forms.ProgressBar progressBar;
		private System.Windows.Forms.Button closeButton;
		private System.Windows.Forms.Panel panel3;
		
//		private CodeVendor.Controls.Grouper textFileBox;
//		private System.Windows.Forms.TextBox textSongSeperatortext;
		private CodeVendor.Controls.Grouper songBox;
		private System.Windows.Forms.RadioButton textFileRadio;
		private System.Windows.Forms.RadioButton csvRadio;
		private System.Windows.Forms.RadioButton dreamBeamRadio;
		private CodeVendor.Controls.Grouper csvBox;
		private System.Windows.Forms.CheckBox seperateVersesCheckbox;
		private System.Windows.Forms.CheckBox includeMLInfoCheckbox;
		private System.Windows.Forms.TextBox newLineText;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox csvSeperatorText;
		private CodeVendor.Controls.Grouper textFileBox;
		private System.Windows.Forms.TextBox textSongSeperatortext;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button loadFileButton;
		private CodeVendor.Controls.Grouper grouper1;


		private System.Windows.Forms.Panel panel4;
		private CodeVendor.Controls.Grouper previewGrouper;
		private CodeVendor.Controls.Grouper rightBG_grouper;
		private System.Windows.Forms.ListView listView;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.TextBox previewTitleText;
		private System.Windows.Forms.TextBox previewAuthorText;

		private Song song;
		Previewer previewForm;
		bool allSongs = false;

		private Language Lang = new Language();
		private System.Windows.Forms.Button saveButton;
		private System.Windows.Forms.Button renameButton;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage songToImportTabPage;
		private System.Windows.Forms.TabPage allSongsTabPage;
		private System.Windows.Forms.Panel BlackImportBGPanel;
		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.ListBox allSongsList;
		private System.Windows.Forms.RadioButton multiTextFilesRadio;
		private System.Windows.Forms.CheckBox checkBox3;
		private System.Windows.Forms.CheckBox checkBox4;
		private System.Windows.Forms.RadioButton radioButton1;
		private System.Windows.Forms.RadioButton radioButton2;
		private System.Windows.Forms.CheckBox checkBox5;
		private System.Windows.Forms.TextBox previewVerseText;
		public ImportForm()
		{
			//
			// Erforderlich für die Unterstützung des Windows Forms-Designer
			//
			InitializeComponent();
			previewForm = new Previewer(this, 0.1f);

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
		
		private void ImportForm_Load(object sender, System.EventArgs e)
		{
			this.textFileBox.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.ForwardDiagonal;
			this.csvBox.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.ForwardDiagonal;
			this.songBox.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.ForwardDiagonal;
			this.grouper1.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.Vertical;
			rightBG_grouper.DockPadding.All = 0;
			
			rightBG_grouper.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.Vertical;
			previewGrouper.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.ForwardDiagonal;
		}

		private void toolBar1_ButtonClick(object sender, TD.SandBar.ToolBarItemEventArgs e)
		{
				if (e.Item == Songs_Button ){
					Songs_Button.Checked=true;
					MediaList_Button.Checked = false;
					Backgrounds_Button.Checked  = false;
					songBox.Show();
					setSongMenus();
					ListSongs();
					listView.Hide();
				}else{

					listView.Height = 112;
					listView.Width = 430;
					listView.Location = new Point (10,20);
					listView.Show();
				}
				if (e.Item == MediaList_Button ){
					Songs_Button.Checked=false;
					MediaList_Button.Checked = true;
					Backgrounds_Button.Checked  = false;
					
					songBox.Hide();
					textFileBox.Hide();
					csvBox.Hide();

				}
				if (e.Item == Backgrounds_Button ){
					Songs_Button.Checked=false;
					MediaList_Button.Checked = false;
					Backgrounds_Button.Checked  = true;




					songBox.Hide();
					textFileBox.Hide();
					csvBox.Hide();
				}
		}

		private void setSongMenus(){
			if(dreamBeamRadio.Checked){

				textFileBox.Hide();
				csvBox.Hide();
			}
			if(textFileRadio.Checked){
			   //	textFileBox.Location = dreamBeamBox.Location;

				textFileBox.Show();
				csvBox.Hide();

			}
			if(csvRadio.Checked){
				csvBox.Location = textFileBox.Location;

				textFileBox.Hide();
				csvBox.Show();

			}
		}
		
		private void dreamBeamRadio_CheckedChanged(object sender, System.EventArgs e)
		{
			setSongMenus();
		}
		
		private void loadFileButton_Click(object sender, System.EventArgs e)
		{
			if(Songs_Button.Checked){
				if(dreamBeamRadio.Checked){
					string filepath = "";
					OpenFileDialog openFileDialog = new OpenFileDialog();
					openFileDialog.Filter = "DreamBeam Song Package|*.dbsongs";
					openFileDialog.Multiselect = true;
					outerList.Items.Clear();
					if(openFileDialog.ShowDialog() == DialogResult.OK){
						   //	filePath = Path.GetDirectoryName(openFileDialog.FileName)+"\\"+Path.GetFileNameWithoutExtension(saveFileDialog.FileName)+".dbsongs";
						if( openFileDialog.FileNames.Length > 0 ){
								Importer.EmptyTempDir();
								Importer.CreateTempDir();
							foreach( string filename in openFileDialog.FileNames){
							   //	MessageBox.Show(filename);
								Importer.GetDreamSongs(outerList,filename);
							}
						}
					}
					openFileDialog.Dispose();
				}
				if(textFileRadio.Checked){
					string filepath = "";
					OpenFileDialog openFileDialog = new OpenFileDialog();
					openFileDialog.Filter = "Multiple Songs in Text File|*.txt";
					openFileDialog.Multiselect = true;
					outerList.Items.Clear();
					if(openFileDialog.ShowDialog() == DialogResult.OK){
						   //	filePath = Path.GetDirectoryName(openFileDialog.FileName)+"\\"+Path.GetFileNameWithoutExtension(saveFileDialog.FileName)+".dbsongs";
						if( openFileDialog.FileNames.Length > 0 ){
								Importer.EmptyTempDir();
								Importer.CreateTempDir();
							foreach( string filename in openFileDialog.FileNames){
							   //	MessageBox.Show(filename);
								Importer.GetTextSongs(outerList,filename,textSongSeperatortext.Text);
							}
						}
					}
					openFileDialog.Dispose();
				}
				if(multiTextFilesRadio.Checked){
						outerList.Items.Clear();
						FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();;
						folderBrowserDialog.Description = "Please select the Folder containing the songs to import.";
						if(folderBrowserDialog.ShowDialog()== DialogResult.OK){
							  folderBrowserDialog.Dispose();
								Importer.EmptyTempDir();
								Importer.CreateTempDir();
							string[] dirs2 = Directory.GetFiles(folderBrowserDialog.SelectedPath, "*.txt");
							foreach (string filename in dirs2) {

								Importer.GetTextSongs(outerList,filename,"");
							}
						}
				}
				if(csvRadio.Checked){
					string filepath = "";
					OpenFileDialog openFileDialog = new OpenFileDialog();
					openFileDialog.Filter = "CSV File|*.csv";
					openFileDialog.Multiselect = true;
					outerList.Items.Clear();
					if(openFileDialog.ShowDialog() == DialogResult.OK){
						   //	filePath = Path.GetDirectoryName(openFileDialog.FileName)+"\\"+Path.GetFileNameWithoutExtension(saveFileDialog.FileName)+".dbsongs";
						if( openFileDialog.FileNames.Length > 0 ){
								Importer.EmptyTempDir();
								Importer.CreateTempDir();
							foreach( string filename in openFileDialog.FileNames){
							   //	MessageBox.Show(filename);
								Importer.GetCSVSongs(outerList,filename,csvSeperatorText.Text,newLineText.Text,includeMLInfoCheckbox.Checked,seperateVersesCheckbox.Checked);
							}
						}
					}
					openFileDialog.Dispose();
				}



			}
//			previewForm.SlideDirection = Previewer.SLIDE_DIRECTION.RIGHT;
//			previewForm.Slide();
		}

		private void closeButton_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
		
	

		#region Vom Windows Form-Designer erzeugter Code
		/// <summary>
		/// Erforderliche Methode zur Unterstützung des Designers -
		/// ändern Sie die Methode nicht mit dem Quelltext-Editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.outerList = new System.Windows.Forms.ListBox();
            this.importList = new System.Windows.Forms.ListBox();
            this.selectButton = new System.Windows.Forms.Button();
            this.selectAllButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.toolBar1 = new TD.SandBar.ToolBar();
            this.labelItem1 = new TD.SandBar.LabelItem();
            this.Songs_Button = new TD.SandBar.ButtonItem();
            this.MediaList_Button = new TD.SandBar.ButtonItem();
            this.Backgrounds_Button = new TD.SandBar.ButtonItem();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ImportButton = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.closeButton = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.listView = new System.Windows.Forms.ListView();
            this.textFileBox = new CodeVendor.Controls.Grouper();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textSongSeperatortext = new System.Windows.Forms.TextBox();
            this.csvBox = new CodeVendor.Controls.Grouper();
            this.seperateVersesCheckbox = new System.Windows.Forms.CheckBox();
            this.includeMLInfoCheckbox = new System.Windows.Forms.CheckBox();
            this.newLineText = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.csvSeperatorText = new System.Windows.Forms.TextBox();
            this.songBox = new CodeVendor.Controls.Grouper();
            this.multiTextFilesRadio = new System.Windows.Forms.RadioButton();
            this.textFileRadio = new System.Windows.Forms.RadioButton();
            this.csvRadio = new System.Windows.Forms.RadioButton();
            this.dreamBeamRadio = new System.Windows.Forms.RadioButton();
            this.loadFileButton = new System.Windows.Forms.Button();
            this.grouper1 = new CodeVendor.Controls.Grouper();
            this.panel5 = new System.Windows.Forms.Panel();
            this.BlackImportBGPanel = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.songToImportTabPage = new System.Windows.Forms.TabPage();
            this.allSongsTabPage = new System.Windows.Forms.TabPage();
            this.allSongsList = new System.Windows.Forms.ListBox();
            this.renameButton = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.rightBG_grouper = new CodeVendor.Controls.Grouper();
            this.previewGrouper = new CodeVendor.Controls.Grouper();
            this.previewVerseText = new System.Windows.Forms.TextBox();
            this.previewTitleText = new System.Windows.Forms.TextBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.previewAuthorText = new System.Windows.Forms.TextBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.textFileBox.SuspendLayout();
            this.csvBox.SuspendLayout();
            this.songBox.SuspendLayout();
            this.grouper1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.BlackImportBGPanel.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.songToImportTabPage.SuspendLayout();
            this.allSongsTabPage.SuspendLayout();
            this.panel4.SuspendLayout();
            this.rightBG_grouper.SuspendLayout();
            this.previewGrouper.SuspendLayout();
            this.SuspendLayout();
            // 
            // outerList
            // 
            this.outerList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.outerList.Location = new System.Drawing.Point(4, 25);
            this.outerList.Name = "outerList";
            this.outerList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.outerList.Size = new System.Drawing.Size(208, 262);
            this.outerList.TabIndex = 1;
            this.outerList.SelectedIndexChanged += new System.EventHandler(this.outerList_SelectedIndexChanged);
            this.outerList.DoubleClick += new System.EventHandler(this.selectButton_Click);
            // 
            // importList
            // 
            this.importList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.importList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.importList.Location = new System.Drawing.Point(0, 0);
            this.importList.Name = "importList";
            this.importList.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.importList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.importList.Size = new System.Drawing.Size(210, 262);
            this.importList.TabIndex = 2;
            this.importList.SelectedIndexChanged += new System.EventHandler(this.outerList_SelectedIndexChanged);
            this.importList.DoubleClick += new System.EventHandler(this.removeButton_Click);
            // 
            // selectButton
            // 
            this.selectButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.selectButton.Location = new System.Drawing.Point(80, 320);
            this.selectButton.Name = "selectButton";
            this.selectButton.Size = new System.Drawing.Size(64, 18);
            this.selectButton.TabIndex = 5;
            this.selectButton.Text = "Select ->";
            this.selectButton.Click += new System.EventHandler(this.selectButton_Click);
            // 
            // selectAllButton
            // 
            this.selectAllButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.selectAllButton.Location = new System.Drawing.Point(152, 320);
            this.selectAllButton.Name = "selectAllButton";
            this.selectAllButton.Size = new System.Drawing.Size(64, 18);
            this.selectAllButton.TabIndex = 6;
            this.selectAllButton.Text = "All ->>";
            this.selectAllButton.Click += new System.EventHandler(this.selectAllButton_Click);
            // 
            // removeButton
            // 
            this.removeButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.removeButton.Location = new System.Drawing.Point(240, 320);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(64, 18);
            this.removeButton.TabIndex = 7;
            this.removeButton.Text = "<- Remove";
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // clearButton
            // 
            this.clearButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.clearButton.Location = new System.Drawing.Point(304, 320);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(64, 18);
            this.clearButton.TabIndex = 8;
            this.clearButton.Text = "Clear";
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
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
            this.toolBar1.Size = new System.Drawing.Size(718, 24);
            this.toolBar1.TabIndex = 11;
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
            // panel2
            // 
            this.panel2.Controls.Add(this.ImportButton);
            this.panel2.Controls.Add(this.progressBar);
            this.panel2.Controls.Add(this.closeButton);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 533);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(718, 24);
            this.panel2.TabIndex = 19;
            // 
            // ImportButton
            // 
            this.ImportButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ImportButton.Location = new System.Drawing.Point(0, 0);
            this.ImportButton.Name = "ImportButton";
            this.ImportButton.Size = new System.Drawing.Size(72, 24);
            this.ImportButton.TabIndex = 13;
            this.ImportButton.Text = "Import";
            this.ImportButton.Click += new System.EventHandler(this.ImportButton_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(152, 0);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(560, 23);
            this.progressBar.TabIndex = 12;
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
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel3.Controls.Add(this.listView);
            this.panel3.Controls.Add(this.textFileBox);
            this.panel3.Controls.Add(this.csvBox);
            this.panel3.Controls.Add(this.songBox);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 24);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(454, 160);
            this.panel3.TabIndex = 22;
            // 
            // listView
            // 
            this.listView.Location = new System.Drawing.Point(24, 304);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(272, 112);
            this.listView.TabIndex = 26;
            this.listView.UseCompatibleStateImageBehavior = false;
            // 
            // textFileBox
            // 
            this.textFileBox.BackgroundColor = System.Drawing.Color.White;
            this.textFileBox.BackgroundGradientColor = System.Drawing.Color.SteelBlue;
            this.textFileBox.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.None;
            this.textFileBox.BorderColor = System.Drawing.Color.Black;
            this.textFileBox.BorderThickness = 1F;
            this.textFileBox.Controls.Add(this.checkBox5);
            this.textFileBox.Controls.Add(this.radioButton2);
            this.textFileBox.Controls.Add(this.radioButton1);
            this.textFileBox.Controls.Add(this.checkBox4);
            this.textFileBox.Controls.Add(this.checkBox3);
            this.textFileBox.Controls.Add(this.label6);
            this.textFileBox.Controls.Add(this.textSongSeperatortext);
            this.textFileBox.CustomGroupBoxColor = System.Drawing.Color.White;
            this.textFileBox.GroupImage = null;
            this.textFileBox.GroupTitle = "File Options";
            this.textFileBox.Location = new System.Drawing.Point(232, 8);
            this.textFileBox.Name = "textFileBox";
            this.textFileBox.Padding = new System.Windows.Forms.Padding(20);
            this.textFileBox.PaintGroupBox = false;
            this.textFileBox.RoundCorners = 10;
            this.textFileBox.ShadowColor = System.Drawing.Color.DarkGray;
            this.textFileBox.ShadowControl = true;
            this.textFileBox.ShadowThickness = 2;
            this.textFileBox.Size = new System.Drawing.Size(208, 144);
            this.textFileBox.TabIndex = 24;
            this.textFileBox.TinyMode = false;
            this.textFileBox.TitleBorder = true;
            this.textFileBox.Visible = false;
            // 
            // checkBox5
            // 
            this.checkBox5.Location = new System.Drawing.Point(8, 120);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(104, 16);
            this.checkBox5.TabIndex = 23;
            this.checkBox5.Text = "checkBox5";
            // 
            // radioButton2
            // 
            this.radioButton2.Location = new System.Drawing.Point(112, 102);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(88, 16);
            this.radioButton2.TabIndex = 22;
            this.radioButton2.Text = "Last Line";
            // 
            // radioButton1
            // 
            this.radioButton1.Location = new System.Drawing.Point(112, 86);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(88, 16);
            this.radioButton1.TabIndex = 21;
            this.radioButton1.Text = "2nd Line";
            // 
            // checkBox4
            // 
            this.checkBox4.Location = new System.Drawing.Point(8, 80);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(104, 24);
            this.checkBox4.TabIndex = 20;
            this.checkBox4.Text = "Import Author";
            // 
            // checkBox3
            // 
            this.checkBox3.Location = new System.Drawing.Point(8, 60);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(144, 24);
            this.checkBox3.TabIndex = 19;
            this.checkBox3.Text = "Import 1st line as title";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(3, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 16);
            this.label6.TabIndex = 18;
            this.label6.Text = "Song Seperator";
            // 
            // textSongSeperatortext
            // 
            this.textSongSeperatortext.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textSongSeperatortext.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textSongSeperatortext.Location = new System.Drawing.Point(4, 40);
            this.textSongSeperatortext.Name = "textSongSeperatortext";
            this.textSongSeperatortext.Size = new System.Drawing.Size(192, 20);
            this.textSongSeperatortext.TabIndex = 17;
            this.textSongSeperatortext.Text = "----------------------------------------";
            // 
            // csvBox
            // 
            this.csvBox.BackgroundColor = System.Drawing.Color.White;
            this.csvBox.BackgroundGradientColor = System.Drawing.Color.SteelBlue;
            this.csvBox.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.None;
            this.csvBox.BorderColor = System.Drawing.Color.Black;
            this.csvBox.BorderThickness = 1F;
            this.csvBox.Controls.Add(this.seperateVersesCheckbox);
            this.csvBox.Controls.Add(this.includeMLInfoCheckbox);
            this.csvBox.Controls.Add(this.newLineText);
            this.csvBox.Controls.Add(this.label4);
            this.csvBox.Controls.Add(this.label5);
            this.csvBox.Controls.Add(this.csvSeperatorText);
            this.csvBox.CustomGroupBoxColor = System.Drawing.Color.White;
            this.csvBox.GroupImage = null;
            this.csvBox.GroupTitle = "Format";
            this.csvBox.Location = new System.Drawing.Point(232, 192);
            this.csvBox.Name = "csvBox";
            this.csvBox.Padding = new System.Windows.Forms.Padding(20);
            this.csvBox.PaintGroupBox = false;
            this.csvBox.RoundCorners = 10;
            this.csvBox.ShadowColor = System.Drawing.Color.DarkGray;
            this.csvBox.ShadowControl = true;
            this.csvBox.ShadowThickness = 2;
            this.csvBox.Size = new System.Drawing.Size(200, 120);
            this.csvBox.TabIndex = 23;
            this.csvBox.TinyMode = false;
            this.csvBox.TitleBorder = true;
            // 
            // seperateVersesCheckbox
            // 
            this.seperateVersesCheckbox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.seperateVersesCheckbox.Location = new System.Drawing.Point(8, 88);
            this.seperateVersesCheckbox.Name = "seperateVersesCheckbox";
            this.seperateVersesCheckbox.Size = new System.Drawing.Size(176, 24);
            this.seperateVersesCheckbox.TabIndex = 27;
            this.seperateVersesCheckbox.Text = "Seperate Verses";
            // 
            // includeMLInfoCheckbox
            // 
            this.includeMLInfoCheckbox.Checked = true;
            this.includeMLInfoCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.includeMLInfoCheckbox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.includeMLInfoCheckbox.Location = new System.Drawing.Point(8, 68);
            this.includeMLInfoCheckbox.Name = "includeMLInfoCheckbox";
            this.includeMLInfoCheckbox.Size = new System.Drawing.Size(176, 24);
            this.includeMLInfoCheckbox.TabIndex = 26;
            this.includeMLInfoCheckbox.Text = "Include Multi Language Info";
            // 
            // newLineText
            // 
            this.newLineText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.newLineText.Location = new System.Drawing.Point(128, 48);
            this.newLineText.Name = "newLineText";
            this.newLineText.Size = new System.Drawing.Size(64, 20);
            this.newLineText.TabIndex = 25;
            this.newLineText.Text = "\\";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(8, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 16);
            this.label4.TabIndex = 24;
            this.label4.Text = "New Line ";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(8, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 16);
            this.label5.TabIndex = 23;
            this.label5.Text = "Seperator";
            // 
            // csvSeperatorText
            // 
            this.csvSeperatorText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.csvSeperatorText.Location = new System.Drawing.Point(128, 30);
            this.csvSeperatorText.Name = "csvSeperatorText";
            this.csvSeperatorText.Size = new System.Drawing.Size(64, 20);
            this.csvSeperatorText.TabIndex = 22;
            this.csvSeperatorText.Text = ";";
            // 
            // songBox
            // 
            this.songBox.BackgroundColor = System.Drawing.Color.White;
            this.songBox.BackgroundGradientColor = System.Drawing.Color.SteelBlue;
            this.songBox.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.None;
            this.songBox.BorderColor = System.Drawing.Color.Black;
            this.songBox.BorderThickness = 1F;
            this.songBox.Controls.Add(this.multiTextFilesRadio);
            this.songBox.Controls.Add(this.textFileRadio);
            this.songBox.Controls.Add(this.csvRadio);
            this.songBox.Controls.Add(this.dreamBeamRadio);
            this.songBox.CustomGroupBoxColor = System.Drawing.Color.White;
            this.songBox.GroupImage = null;
            this.songBox.GroupTitle = "Format";
            this.songBox.Location = new System.Drawing.Point(16, 8);
            this.songBox.Name = "songBox";
            this.songBox.Padding = new System.Windows.Forms.Padding(20);
            this.songBox.PaintGroupBox = false;
            this.songBox.RoundCorners = 10;
            this.songBox.ShadowColor = System.Drawing.Color.DarkGray;
            this.songBox.ShadowControl = true;
            this.songBox.ShadowThickness = 2;
            this.songBox.Size = new System.Drawing.Size(200, 136);
            this.songBox.TabIndex = 22;
            this.songBox.TinyMode = false;
            this.songBox.TitleBorder = true;
            // 
            // multiTextFilesRadio
            // 
            this.multiTextFilesRadio.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.multiTextFilesRadio.Location = new System.Drawing.Point(16, 80);
            this.multiTextFilesRadio.Name = "multiTextFilesRadio";
            this.multiTextFilesRadio.Size = new System.Drawing.Size(168, 24);
            this.multiTextFilesRadio.TabIndex = 7;
            this.multiTextFilesRadio.Text = "Text - one Song each File";
            // 
            // textFileRadio
            // 
            this.textFileRadio.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.textFileRadio.Location = new System.Drawing.Point(16, 56);
            this.textFileRadio.Name = "textFileRadio";
            this.textFileRadio.Size = new System.Drawing.Size(168, 24);
            this.textFileRadio.TabIndex = 6;
            this.textFileRadio.Text = "Text - multiple Songs in File";
            this.textFileRadio.CheckedChanged += new System.EventHandler(this.dreamBeamRadio_CheckedChanged);
            // 
            // csvRadio
            // 
            this.csvRadio.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.csvRadio.Location = new System.Drawing.Point(16, 104);
            this.csvRadio.Name = "csvRadio";
            this.csvRadio.Size = new System.Drawing.Size(136, 24);
            this.csvRadio.TabIndex = 5;
            this.csvRadio.Text = "CSV";
            this.csvRadio.CheckedChanged += new System.EventHandler(this.dreamBeamRadio_CheckedChanged);
            // 
            // dreamBeamRadio
            // 
            this.dreamBeamRadio.Checked = true;
            this.dreamBeamRadio.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.dreamBeamRadio.Location = new System.Drawing.Point(16, 32);
            this.dreamBeamRadio.Name = "dreamBeamRadio";
            this.dreamBeamRadio.Size = new System.Drawing.Size(168, 24);
            this.dreamBeamRadio.TabIndex = 4;
            this.dreamBeamRadio.TabStop = true;
            this.dreamBeamRadio.Text = "Dreambeam Song Package";
            this.dreamBeamRadio.CheckedChanged += new System.EventHandler(this.dreamBeamRadio_CheckedChanged);
            // 
            // loadFileButton
            // 
            this.loadFileButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.loadFileButton.Location = new System.Drawing.Point(4, 0);
            this.loadFileButton.Name = "loadFileButton";
            this.loadFileButton.Size = new System.Drawing.Size(75, 23);
            this.loadFileButton.TabIndex = 25;
            this.loadFileButton.Text = "Load Files...";
            this.loadFileButton.Click += new System.EventHandler(this.loadFileButton_Click);
            // 
            // grouper1
            // 
            this.grouper1.BackgroundColor = System.Drawing.Color.LightSteelBlue;
            this.grouper1.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouper1.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouper1.BorderColor = System.Drawing.Color.Transparent;
            this.grouper1.BorderThickness = 1F;
            this.grouper1.Controls.Add(this.panel5);
            this.grouper1.Controls.Add(this.BlackImportBGPanel);
            this.grouper1.Controls.Add(this.renameButton);
            this.grouper1.Controls.Add(this.selectButton);
            this.grouper1.Controls.Add(this.selectAllButton);
            this.grouper1.Controls.Add(this.removeButton);
            this.grouper1.Controls.Add(this.clearButton);
            this.grouper1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grouper1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grouper1.GroupImage = null;
            this.grouper1.GroupTitle = "";
            this.grouper1.Location = new System.Drawing.Point(0, 181);
            this.grouper1.Name = "grouper1";
            this.grouper1.Padding = new System.Windows.Forms.Padding(20);
            this.grouper1.PaintGroupBox = false;
            this.grouper1.RoundCorners = 1;
            this.grouper1.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouper1.ShadowControl = false;
            this.grouper1.ShadowThickness = 3;
            this.grouper1.Size = new System.Drawing.Size(454, 352);
            this.grouper1.TabIndex = 23;
            this.grouper1.TinyMode = false;
            this.grouper1.TitleBorder = true;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.outerList);
            this.panel5.Controls.Add(this.loadFileButton);
            this.panel5.Location = new System.Drawing.Point(3, 16);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(220, 296);
            this.panel5.TabIndex = 27;
            // 
            // BlackImportBGPanel
            // 
            this.BlackImportBGPanel.BackColor = System.Drawing.Color.Transparent;
            this.BlackImportBGPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BlackImportBGPanel.Controls.Add(this.tabControl1);
            this.BlackImportBGPanel.Location = new System.Drawing.Point(230, 16);
            this.BlackImportBGPanel.Name = "BlackImportBGPanel";
            this.BlackImportBGPanel.Size = new System.Drawing.Size(220, 296);
            this.BlackImportBGPanel.TabIndex = 26;
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.songToImportTabPage);
            this.tabControl1.Controls.Add(this.allSongsTabPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ItemSize = new System.Drawing.Size(50, 21);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(0, 0);
            this.tabControl1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(218, 294);
            this.tabControl1.TabIndex = 10;
            this.tabControl1.Click += new System.EventHandler(this.tabControl1_Click);
            // 
            // songToImportTabPage
            // 
            this.songToImportTabPage.BackColor = System.Drawing.Color.Transparent;
            this.songToImportTabPage.Controls.Add(this.importList);
            this.songToImportTabPage.Location = new System.Drawing.Point(4, 25);
            this.songToImportTabPage.Name = "songToImportTabPage";
            this.songToImportTabPage.Size = new System.Drawing.Size(210, 265);
            this.songToImportTabPage.TabIndex = 0;
            this.songToImportTabPage.Text = "Songs to Import";
            // 
            // allSongsTabPage
            // 
            this.allSongsTabPage.Controls.Add(this.allSongsList);
            this.allSongsTabPage.Location = new System.Drawing.Point(4, 25);
            this.allSongsTabPage.Name = "allSongsTabPage";
            this.allSongsTabPage.Size = new System.Drawing.Size(210, 265);
            this.allSongsTabPage.TabIndex = 1;
            this.allSongsTabPage.Text = "All Songs";
            // 
            // allSongsList
            // 
            this.allSongsList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.allSongsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.allSongsList.Location = new System.Drawing.Point(0, 0);
            this.allSongsList.Name = "allSongsList";
            this.allSongsList.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.allSongsList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.allSongsList.Size = new System.Drawing.Size(210, 262);
            this.allSongsList.TabIndex = 3;
            this.allSongsList.SelectedIndexChanged += new System.EventHandler(this.outerList_SelectedIndexChanged);
            // 
            // renameButton
            // 
            this.renameButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.renameButton.Location = new System.Drawing.Point(16, 320);
            this.renameButton.Name = "renameButton";
            this.renameButton.Size = new System.Drawing.Size(64, 18);
            this.renameButton.TabIndex = 9;
            this.renameButton.Text = "Rename";
            this.renameButton.Click += new System.EventHandler(this.renameButton_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel4.Controls.Add(this.rightBG_grouper);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(454, 24);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(264, 509);
            this.panel4.TabIndex = 24;
            // 
            // rightBG_grouper
            // 
            this.rightBG_grouper.BackgroundColor = System.Drawing.Color.LightSteelBlue;
            this.rightBG_grouper.BackgroundGradientColor = System.Drawing.Color.White;
            this.rightBG_grouper.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.None;
            this.rightBG_grouper.BorderColor = System.Drawing.Color.Transparent;
            this.rightBG_grouper.BorderThickness = 1F;
            this.rightBG_grouper.Controls.Add(this.previewGrouper);
            this.rightBG_grouper.CustomGroupBoxColor = System.Drawing.Color.White;
            this.rightBG_grouper.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightBG_grouper.GroupImage = null;
            this.rightBG_grouper.GroupTitle = "";
            this.rightBG_grouper.Location = new System.Drawing.Point(0, 0);
            this.rightBG_grouper.Name = "rightBG_grouper";
            this.rightBG_grouper.Padding = new System.Windows.Forms.Padding(20);
            this.rightBG_grouper.PaintGroupBox = false;
            this.rightBG_grouper.RoundCorners = 1;
            this.rightBG_grouper.ShadowColor = System.Drawing.Color.DarkGray;
            this.rightBG_grouper.ShadowControl = false;
            this.rightBG_grouper.ShadowThickness = 1;
            this.rightBG_grouper.Size = new System.Drawing.Size(264, 509);
            this.rightBG_grouper.TabIndex = 2;
            this.rightBG_grouper.TinyMode = false;
            this.rightBG_grouper.TitleBorder = true;
            // 
            // previewGrouper
            // 
            this.previewGrouper.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.previewGrouper.BackgroundGradientColor = System.Drawing.Color.White;
            this.previewGrouper.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.None;
            this.previewGrouper.BorderColor = System.Drawing.Color.MidnightBlue;
            this.previewGrouper.BorderThickness = 1F;
            this.previewGrouper.Controls.Add(this.previewVerseText);
            this.previewGrouper.Controls.Add(this.previewTitleText);
            this.previewGrouper.Controls.Add(this.saveButton);
            this.previewGrouper.Controls.Add(this.previewAuthorText);
            this.previewGrouper.CustomGroupBoxColor = System.Drawing.Color.White;
            this.previewGrouper.Dock = System.Windows.Forms.DockStyle.Top;
            this.previewGrouper.GroupImage = null;
            this.previewGrouper.GroupTitle = "Preview";
            this.previewGrouper.Location = new System.Drawing.Point(20, 20);
            this.previewGrouper.Name = "previewGrouper";
            this.previewGrouper.Padding = new System.Windows.Forms.Padding(20);
            this.previewGrouper.PaintGroupBox = false;
            this.previewGrouper.RoundCorners = 10;
            this.previewGrouper.ShadowColor = System.Drawing.Color.DarkGray;
            this.previewGrouper.ShadowControl = true;
            this.previewGrouper.ShadowThickness = 1;
            this.previewGrouper.Size = new System.Drawing.Size(224, 484);
            this.previewGrouper.TabIndex = 0;
            this.previewGrouper.TinyMode = false;
            this.previewGrouper.TitleBorder = true;
            // 
            // previewVerseText
            // 
            this.previewVerseText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.previewVerseText.Location = new System.Drawing.Point(8, 64);
            this.previewVerseText.Multiline = true;
            this.previewVerseText.Name = "previewVerseText";
            this.previewVerseText.Size = new System.Drawing.Size(248, 336);
            this.previewVerseText.TabIndex = 6;
            // 
            // previewTitleText
            // 
            this.previewTitleText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.previewTitleText.Location = new System.Drawing.Point(8, 32);
            this.previewTitleText.Name = "previewTitleText";
            this.previewTitleText.Size = new System.Drawing.Size(213, 20);
            this.previewTitleText.TabIndex = 0;
            // 
            // saveButton
            // 
            this.saveButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.saveButton.Location = new System.Drawing.Point(96, 440);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 24);
            this.saveButton.TabIndex = 4;
            this.saveButton.Text = "Save";
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // previewAuthorText
            // 
            this.previewAuthorText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.previewAuthorText.Location = new System.Drawing.Point(8, 408);
            this.previewAuthorText.Name = "previewAuthorText";
            this.previewAuthorText.Size = new System.Drawing.Size(248, 20);
            this.previewAuthorText.TabIndex = 2;
            // 
            // openFileDialog
            // 
            this.openFileDialog.Multiselect = true;
            // 
            // ImportForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(718, 557);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.grouper1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.toolBar1);
            this.Controls.Add(this.panel2);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ImportForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Import";
            this.Load += new System.EventHandler(this.ImportForm_Load);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.textFileBox.ResumeLayout(false);
            this.textFileBox.PerformLayout();
            this.csvBox.ResumeLayout(false);
            this.csvBox.PerformLayout();
            this.songBox.ResumeLayout(false);
            this.grouper1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.BlackImportBGPanel.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.songToImportTabPage.ResumeLayout(false);
            this.allSongsTabPage.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.rightBG_grouper.ResumeLayout(false);
            this.previewGrouper.ResumeLayout(false);
            this.previewGrouper.PerformLayout();
            this.ResumeLayout(false);

		}


		#endregion

		private void outerList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string path="";
			if(sender == outerList || sender == importList){
				path = Importer.tempDir;
			}
			if(sender == allSongsList){
                path = DreamTools.GetDirectory(DirType.Songs);
			}
			if(Songs_Button.Checked){
				if(((ListBox)sender).SelectedItems.Count >0){
				if(File.Exists (path + ((ListBox)sender).SelectedItem.ToString()+".xml")){
						song =new Song();

                    /*
						song.Load(((ListBox)sender).SelectedItem.ToString(),path);
						previewTitleText.Text = song.GetText(0);
						previewVerseText.Text = song.GetText(1).Replace("\r\n","~~").Replace("\n","~~").Replace("~~","\r\n");

						previewAuthorText.Text = song.GetText(2);
                    */
                        previewTitleText.Text = "Title";
                        previewVerseText.Text = "Replace this Text";
                        previewAuthorText.Text = "author";

					}
				}
			}
		}


		private void renameButton_Click(object sender, System.EventArgs e)
		{
			if(Songs_Button.Checked){

				if(outerList.SelectedItems.Count ==1){
					InputBoxResult result = InputBox.Show("","","", null);
					if (result.OK) {
							if (result.Text.Length > 0){
								if(File.Exists(Importer.tempDir+result.Text+".xml") == false){
									try{
										File.Move(Importer.tempDir+outerList.SelectedItem.ToString()+".xml",Importer.tempDir+result.Text+".xml");
									}catch(Exception ex){}
									if(File.Exists(Importer.tempDir+result.Text+".xml")){
										int x = outerList.SelectedIndex;
										outerList.Items.Insert(x,result.Text);
										outerList.Items.Remove(outerList.SelectedItem);
										outerList.SelectedIndex = x;
									}else{
										MessageBox.Show ("Rename failed.");
									}
								}else{
									MessageBox.Show ("File already exists.");
								}


							}else {
								MessageBox.Show(Lang.say("Message.NoNameEntered"));
							}
					}
				}
			}
		}
		
		private void saveButton_Click(object sender, System.EventArgs e)
		{

			if(song != null){
					/*(song.SetText(previewTitleText.Text,0);
					 song.SetText(previewVerseText.Text.Replace("\r\n","\n"),1);
					song.SetText(previewAuthorText.Text,2);
					song.Save();*/
			}

		}




			///<summary>Reads all Songs in Directory, validates if it is a Song and put's them into the RightDocks_SongList Box </summary>
			public void ListSongs() {
				allSongsList.Items.Clear();
                string strSongDir = DreamTools.GetDirectory(DirType.Songs);
				if(!System.IO.Directory.Exists(strSongDir)) {
					System.IO.Directory.CreateDirectory(strSongDir);
				}
				string[] dirs2 = Directory.GetFiles(@strSongDir, "*.xml");
				Song Song = new Song();
				foreach (string dir2 in dirs2) {
				/*	if (Song.isSong(Path.GetFileName(dir2))) {
						string temp = Path.GetFileName(dir2);
						allSongsList.Items.Add(temp.Substring(0,temp.Length-4));
					}*/
				}
			}
		
		private void selectButton_Click(object sender, System.EventArgs e)
		{
			bool insertItem;
			ArrayList tmp = new ArrayList();
			for (int i =0; i<outerList.SelectedItems.Count;i++){
				insertItem = true;
				for (int j = 0; j< importList.Items.Count;j++){
					if(outerList.SelectedItems[i].ToString() == importList.Items[j].ToString()){
						insertItem = false;
					}
				}
				if(insertItem){
					importList.Items.Add(outerList.SelectedItems[i].ToString());
					tmp.Add (outerList.SelectedIndices[i]);
				}
			}
			outerList.SelectedIndex = -1;
			for (int j = 0; j < tmp.Count;j++){
				outerList.Items.RemoveAt((int)tmp[j]-j);
			}
			GuiTools.ListBoxSorter(ref importList);
		}
		
		private void removeButton_Click(object sender, System.EventArgs e)
		{
			bool insertItem;
			ArrayList tmp = new ArrayList();
			for (int i =0; i<importList.SelectedItems.Count;i++){
				insertItem = true;
				for (int j = 0; j< outerList.Items.Count;j++){
					if(importList.SelectedItems[i].ToString() == outerList.Items[j].ToString()){
						insertItem = false;
					}
				}
				if(insertItem){
					outerList.Items.Add(importList.SelectedItems[i].ToString());
					tmp.Add (importList.SelectedIndices[i]);
				}
			}
			importList.SelectedIndex = -1;
			for (int j = 0; j < tmp.Count;j++){
				importList.Items.RemoveAt((int)tmp[j]-j);
			}
		   GuiTools.ListBoxSorter(ref outerList);
		}



		private void selectAllButton_Click(object sender, System.EventArgs e)
		{
			outerList.SelectedIndex = -1;
			for (int i =0; i<outerList.Items.Count;i++){
				outerList.SelectedItem = outerList.Items[i];
			}
			selectButton_Click(sender,e);
		}
		
		private void clearButton_Click(object sender, System.EventArgs e)
		{
			importList.SelectedIndex = -1;
			for (int i =0; i<importList.Items.Count;i++){
				importList.SelectedItem = importList.Items[i];
			}
			removeButton_Click(sender,e);
		}
		
		private void tabControl1_Click(object sender, System.EventArgs e)
		{

			if(tabControl1.SelectedTab == songToImportTabPage){
				removeButton.Enabled = true;
				clearButton.Enabled = true;
			}else{
				removeButton.Enabled = false;
				clearButton.Enabled = false;
			}
		}
		
		private void ImportButton_Click(object sender, System.EventArgs e)
		{

		}



	}
}
