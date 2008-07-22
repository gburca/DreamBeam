using System.Windows.Forms;
namespace DreamBeam {
	partial class Options {

		#region Designer variables and objects
		private System.ComponentModel.IContainer components;
		public System.Windows.Forms.TabControl tabControl;
		public System.Windows.Forms.TabPage Common_Tab;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.Panel Options_TopPanel;
		private System.Windows.Forms.Button Select_Sword;
		private System.Windows.Forms.Button Options_Cancelbtn;

		private System.Windows.Forms.Button Options_Okaybtn;
		private System.Windows.Forms.TextBox Sword_PathBox;
		private System.Windows.Forms.GroupBox SwordFolderGroupBox;
		public System.Windows.Forms.ComboBox Sword_LanguageBox;
		private System.Windows.Forms.GroupBox SwordLangGroupBox;
		public System.Windows.Forms.TabPage Graphics_tab;
		public System.Windows.Forms.TabPage BeamBox_tab;
		private System.Windows.Forms.GroupBox BeamBox_PosBox;
		private System.Windows.Forms.GroupBox BeamBox_SizeBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label WidthLabel;
		private System.Windows.Forms.Label HeightLabel;
		private System.Windows.Forms.NumericUpDown BeamBox_posX;
		private System.Windows.Forms.NumericUpDown BeamBox_Height;
		private System.Windows.Forms.NumericUpDown BeamBox_Width;
		private System.Windows.Forms.NumericUpDown BeamBox_posY;
		private System.Windows.Forms.GroupBox Alpha_groupBox1;
		private System.Windows.Forms.CheckBox Alpha_CheckBox;
		private System.Windows.Forms.GroupBox Mouse_groupBox2;
		private System.Windows.Forms.CheckBox BeamBox_HideMouse;
		private System.Windows.Forms.ColorDialog colorDialog1;
		private System.Windows.Forms.Button BeamBox_ColorButton;
		private System.Windows.Forms.GroupBox Background_groupBox3;
		private System.Windows.Forms.CheckBox BeamBox_AlwaysOnTop;
		private System.Windows.Forms.TabControl SizePosControl;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;

		private System.Windows.Forms.ListBox ScreenList;
		private System.Windows.Forms.Label AutoPosLabelX;
		private System.Windows.Forms.Label AutoPosLabelY;
		private System.Windows.Forms.GroupBox Position_groupBox4;
		private System.Windows.Forms.GroupBox Size_groupBox5;
		private System.Windows.Forms.Label AutoSizeLabelW;
		private System.Windows.Forms.Label AutoSizeLabelH;
		private System.Windows.Forms.GroupBox Position_Title_GroupBox;
		public System.Windows.Forms.TabPage Misc_Tab;
		private System.Windows.Forms.GroupBox LanguageBox;
		private System.Windows.Forms.ListBox LanguageList;
		private System.Windows.Forms.Label TranslateLabel;
		private System.Windows.Forms.CheckBox Direct3D_CheckBox;
		private System.Windows.Forms.NumericUpDown Speed_Updown;
		private System.Windows.Forms.Label SpeedLabel;
		private System.Windows.Forms.GroupBox PreRenderBox;
		private System.Windows.Forms.CheckBox PreRendercheckBox;
		private System.Windows.Forms.TabPage BibleCache_Tab;
		private Lister.ListEx BiblesCached_listEx;
		private Lister.ListEx BiblesAvailable_listEx;
		private System.Windows.Forms.DataGrid BibleConversions_dataGrid;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn1;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn2;
		private System.Data.DataTable Options_RegEx_Table;
		private System.Data.DataColumn RegEx_Find;
		private System.Data.DataColumn RegEx_Replace;
		private System.Windows.Forms.ProgressBar BibleCache_progressBar;
		private System.Windows.Forms.ImageList ListEx_ImageList;
		private System.Windows.Forms.DataGridTableStyle BibleConversions_GridTableStyle;
		private System.Windows.Forms.FontDialog fontDialog1;
		private System.Windows.Forms.CheckBox Options_PanelLocations_checkBox;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox ServerAddress;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.NumericUpDown ListeningPort;
		private System.Windows.Forms.RadioButton OperatingMode_StandAlone;
		private System.Windows.Forms.RadioButton OperatingMode_Client;
		private System.Windows.Forms.RadioButton OperatingMode_Server;
		private System.Windows.Forms.TabPage BibleFormat_Tab;
		private System.Windows.Forms.TabPage SongFormat_Tab;
		private System.Windows.Forms.TabPage TextToolFormat_Tab;
		private System.Windows.Forms.ToolTip Options_ToolTip;
		private System.Data.DataSet Options_DataSet;
		private System.Windows.Forms.Label BibleCache_Message;
		private System.Windows.Forms.Label PreRenderLabel;
		#endregion


		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Options));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.Options_TopPanel = new System.Windows.Forms.Panel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.Common_Tab = new System.Windows.Forms.TabPage();
            this.DataDirectory = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ListeningPort = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.OperatingMode_Server = new System.Windows.Forms.RadioButton();
            this.OperatingMode_Client = new System.Windows.Forms.RadioButton();
            this.OperatingMode_StandAlone = new System.Windows.Forms.RadioButton();
            this.ServerAddress = new System.Windows.Forms.TextBox();
            this.Options_PanelLocations_checkBox = new System.Windows.Forms.CheckBox();
            this.LanguageBox = new System.Windows.Forms.GroupBox();
            this.TranslateLabel = new System.Windows.Forms.Label();
            this.LanguageList = new System.Windows.Forms.ListBox();
            this.BeamBox_tab = new System.Windows.Forms.TabPage();
            this.Position_Title_GroupBox = new System.Windows.Forms.GroupBox();
            this.SizePosControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.Size_groupBox5 = new System.Windows.Forms.GroupBox();
            this.AutoSizeLabelW = new System.Windows.Forms.Label();
            this.AutoSizeLabelH = new System.Windows.Forms.Label();
            this.Position_groupBox4 = new System.Windows.Forms.GroupBox();
            this.AutoPosLabelX = new System.Windows.Forms.Label();
            this.AutoPosLabelY = new System.Windows.Forms.Label();
            this.ScreenList = new System.Windows.Forms.ListBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.BeamBox_PosBox = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.BeamBox_posX = new System.Windows.Forms.NumericUpDown();
            this.BeamBox_posY = new System.Windows.Forms.NumericUpDown();
            this.BeamBox_SizeBox = new System.Windows.Forms.GroupBox();
            this.HeightLabel = new System.Windows.Forms.Label();
            this.WidthLabel = new System.Windows.Forms.Label();
            this.BeamBox_Width = new System.Windows.Forms.NumericUpDown();
            this.BeamBox_Height = new System.Windows.Forms.NumericUpDown();
            this.Background_groupBox3 = new System.Windows.Forms.GroupBox();
            this.BeamBox_ColorButton = new System.Windows.Forms.Button();
            this.Mouse_groupBox2 = new System.Windows.Forms.GroupBox();
            this.BeamBox_AlwaysOnTop = new System.Windows.Forms.CheckBox();
            this.BeamBox_HideMouse = new System.Windows.Forms.CheckBox();
            this.Misc_Tab = new System.Windows.Forms.TabPage();
            this.songVerseSeparatorGroup = new System.Windows.Forms.GroupBox();
            this.verseSep2L = new System.Windows.Forms.RadioButton();
            this.verseSep1L = new System.Windows.Forms.RadioButton();
            this.SwordLangGroupBox = new System.Windows.Forms.GroupBox();
            this.Sword_LanguageBox = new System.Windows.Forms.ComboBox();
            this.SwordFolderGroupBox = new System.Windows.Forms.GroupBox();
            this.Sword_PathBox = new System.Windows.Forms.TextBox();
            this.Select_Sword = new System.Windows.Forms.Button();
            this.BibleCache_Tab = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.BibleConversions_Custom = new System.Windows.Forms.RadioButton();
            this.BibleConversions_Default = new System.Windows.Forms.RadioButton();
            this.BibleCache_Cached_Label = new System.Windows.Forms.Label();
            this.BibleCache_Available_Label = new System.Windows.Forms.Label();
            this.BibleCache_Replacements_Label = new System.Windows.Forms.Label();
            this.BibleCache_Message = new System.Windows.Forms.Label();
            this.BibleConversions_dataGrid = new System.Windows.Forms.DataGrid();
            this.Options_RegEx_Table = new System.Data.DataTable();
            this.RegEx_Find = new System.Data.DataColumn();
            this.RegEx_Replace = new System.Data.DataColumn();
            this.BibleConversions_GridTableStyle = new System.Windows.Forms.DataGridTableStyle();
            this.dataGridTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn2 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.BibleCache_progressBar = new System.Windows.Forms.ProgressBar();
            this.BiblesAvailable_listEx = new Lister.ListEx();
            this.ListEx_ImageList = new System.Windows.Forms.ImageList(this.components);
            this.BiblesCached_listEx = new Lister.ListEx();
            this.Graphics_tab = new System.Windows.Forms.TabPage();
            this.PreRenderBox = new System.Windows.Forms.GroupBox();
            this.PreRenderLabel = new System.Windows.Forms.Label();
            this.PreRendercheckBox = new System.Windows.Forms.CheckBox();
            this.Alpha_groupBox1 = new System.Windows.Forms.GroupBox();
            this.SpeedLabel = new System.Windows.Forms.Label();
            this.Speed_Updown = new System.Windows.Forms.NumericUpDown();
            this.Direct3D_CheckBox = new System.Windows.Forms.CheckBox();
            this.Alpha_CheckBox = new System.Windows.Forms.CheckBox();
            this.SongFormat_Tab = new System.Windows.Forms.TabPage();
            this.songThemeWidget = new DreamBeam.ThemeWidget();
            this.BibleFormat_Tab = new System.Windows.Forms.TabPage();
            this.bibleFormatWidget = new DreamBeam.ThemeWidget();
            this.TextToolFormat_Tab = new System.Windows.Forms.TabPage();
            this.sermonThemeWidget = new DreamBeam.ThemeWidget();
            this.Options_Cancelbtn = new System.Windows.Forms.Button();
            this.Options_Okaybtn = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.Options_ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.Options_DataSet = new System.Data.DataSet();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.Options_TopPanel.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.Common_Tab.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ListeningPort)).BeginInit();
            this.LanguageBox.SuspendLayout();
            this.BeamBox_tab.SuspendLayout();
            this.Position_Title_GroupBox.SuspendLayout();
            this.SizePosControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.Size_groupBox5.SuspendLayout();
            this.Position_groupBox4.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.BeamBox_PosBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BeamBox_posX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BeamBox_posY)).BeginInit();
            this.BeamBox_SizeBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BeamBox_Width)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BeamBox_Height)).BeginInit();
            this.Background_groupBox3.SuspendLayout();
            this.Mouse_groupBox2.SuspendLayout();
            this.Misc_Tab.SuspendLayout();
            this.songVerseSeparatorGroup.SuspendLayout();
            this.SwordLangGroupBox.SuspendLayout();
            this.SwordFolderGroupBox.SuspendLayout();
            this.BibleCache_Tab.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BibleConversions_dataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Options_RegEx_Table)).BeginInit();
            this.Graphics_tab.SuspendLayout();
            this.PreRenderBox.SuspendLayout();
            this.Alpha_groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Speed_Updown)).BeginInit();
            this.SongFormat_Tab.SuspendLayout();
            this.BibleFormat_Tab.SuspendLayout();
            this.TextToolFormat_Tab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Options_DataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.AddExtension = false;
            this.openFileDialog1.DefaultExt = "exe";
            this.openFileDialog1.Filter = "Sword.exe|sword.exe";
            this.openFileDialog1.Title = "Select Sword.exe File";
            // 
            // Options_TopPanel
            // 
            this.Options_TopPanel.Controls.Add(this.tabControl);
            this.Options_TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.Options_TopPanel.Location = new System.Drawing.Point(0, 0);
            this.Options_TopPanel.Name = "Options_TopPanel";
            this.Options_TopPanel.Size = new System.Drawing.Size(523, 346);
            this.Options_TopPanel.TabIndex = 0;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.Common_Tab);
            this.tabControl.Controls.Add(this.BeamBox_tab);
            this.tabControl.Controls.Add(this.Misc_Tab);
            this.tabControl.Controls.Add(this.BibleCache_Tab);
            this.tabControl.Controls.Add(this.Graphics_tab);
            this.tabControl.Controls.Add(this.SongFormat_Tab);
            this.tabControl.Controls.Add(this.BibleFormat_Tab);
            this.tabControl.Controls.Add(this.TextToolFormat_Tab);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Multiline = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(523, 346);
            this.tabControl.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tabControl.TabIndex = 1;
            // 
            // Common_Tab
            // 
            this.Common_Tab.Controls.Add(this.DataDirectory);
            this.Common_Tab.Controls.Add(this.label3);
            this.Common_Tab.Controls.Add(this.groupBox1);
            this.Common_Tab.Controls.Add(this.Options_PanelLocations_checkBox);
            this.Common_Tab.Controls.Add(this.LanguageBox);
            this.Common_Tab.Location = new System.Drawing.Point(4, 40);
            this.Common_Tab.Name = "Common_Tab";
            this.Common_Tab.Size = new System.Drawing.Size(515, 302);
            this.Common_Tab.TabIndex = 0;
            this.Common_Tab.Text = "Common Settings";
            this.Common_Tab.UseVisualStyleBackColor = true;
            // 
            // DataDirectory
            // 
            this.DataDirectory.Location = new System.Drawing.Point(97, 241);
            this.DataDirectory.Name = "DataDirectory";
            this.DataDirectory.ReadOnly = true;
            this.DataDirectory.Size = new System.Drawing.Size(319, 20);
            this.DataDirectory.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 244);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Data Directory:";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.ListeningPort);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.OperatingMode_Server);
            this.groupBox1.Controls.Add(this.OperatingMode_Client);
            this.groupBox1.Controls.Add(this.OperatingMode_StandAlone);
            this.groupBox1.Controls.Add(this.ServerAddress);
            this.groupBox1.Location = new System.Drawing.Point(8, 126);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(414, 106);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Operating Mode (Requires a restart)";
            // 
            // ListeningPort
            // 
            this.ListeningPort.Location = new System.Drawing.Point(206, 76);
            this.ListeningPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.ListeningPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ListeningPort.Name = "ListeningPort";
            this.ListeningPort.Size = new System.Drawing.Size(92, 20);
            this.ListeningPort.TabIndex = 6;
            this.ListeningPort.Value = new decimal(new int[] {
            50000,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(104, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 23);
            this.label5.TabIndex = 5;
            this.label5.Text = "Listening on port:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(104, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 23);
            this.label4.TabIndex = 4;
            this.label4.Text = "Server address:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // OperatingMode_Server
            // 
            this.OperatingMode_Server.Location = new System.Drawing.Point(10, 74);
            this.OperatingMode_Server.Name = "OperatingMode_Server";
            this.OperatingMode_Server.Size = new System.Drawing.Size(84, 24);
            this.OperatingMode_Server.TabIndex = 2;
            this.OperatingMode_Server.Text = "Server";
            this.Options_ToolTip.SetToolTip(this.OperatingMode_Server, "Makes this instance of DreamBeam a server that listens for commands on the design" +
                    "ated port.");
            // 
            // OperatingMode_Client
            // 
            this.OperatingMode_Client.Location = new System.Drawing.Point(10, 48);
            this.OperatingMode_Client.Name = "OperatingMode_Client";
            this.OperatingMode_Client.Size = new System.Drawing.Size(84, 24);
            this.OperatingMode_Client.TabIndex = 1;
            this.OperatingMode_Client.Text = "Client";
            this.Options_ToolTip.SetToolTip(this.OperatingMode_Client, "Makes this instance of DreamBeam act as a client and send all its actions to the " +
                    "server.");
            // 
            // OperatingMode_StandAlone
            // 
            this.OperatingMode_StandAlone.Checked = true;
            this.OperatingMode_StandAlone.Location = new System.Drawing.Point(10, 22);
            this.OperatingMode_StandAlone.Name = "OperatingMode_StandAlone";
            this.OperatingMode_StandAlone.Size = new System.Drawing.Size(84, 24);
            this.OperatingMode_StandAlone.TabIndex = 0;
            this.OperatingMode_StandAlone.TabStop = true;
            this.OperatingMode_StandAlone.Text = "Stand alone";
            this.Options_ToolTip.SetToolTip(this.OperatingMode_StandAlone, "Use this mode unless you know what you\'re doing.");
            // 
            // ServerAddress
            // 
            this.ServerAddress.Location = new System.Drawing.Point(206, 50);
            this.ServerAddress.Name = "ServerAddress";
            this.ServerAddress.Size = new System.Drawing.Size(196, 20);
            this.ServerAddress.TabIndex = 3;
            this.Options_ToolTip.SetToolTip(this.ServerAddress, "This should look like \"http://server/DreamBeam");
            // 
            // Options_PanelLocations_checkBox
            // 
            this.Options_PanelLocations_checkBox.Location = new System.Drawing.Point(16, 94);
            this.Options_PanelLocations_checkBox.Name = "Options_PanelLocations_checkBox";
            this.Options_PanelLocations_checkBox.Size = new System.Drawing.Size(160, 24);
            this.Options_PanelLocations_checkBox.TabIndex = 1;
            this.Options_PanelLocations_checkBox.Text = "Remember panel locations";
            // 
            // LanguageBox
            // 
            this.LanguageBox.Controls.Add(this.TranslateLabel);
            this.LanguageBox.Controls.Add(this.LanguageList);
            this.LanguageBox.Location = new System.Drawing.Point(8, 8);
            this.LanguageBox.Name = "LanguageBox";
            this.LanguageBox.Size = new System.Drawing.Size(408, 80);
            this.LanguageBox.TabIndex = 0;
            this.LanguageBox.TabStop = false;
            this.LanguageBox.Text = "Language";
            // 
            // TranslateLabel
            // 
            this.TranslateLabel.Location = new System.Drawing.Point(152, 24);
            this.TranslateLabel.Name = "TranslateLabel";
            this.TranslateLabel.Size = new System.Drawing.Size(248, 48);
            this.TranslateLabel.TabIndex = 1;
            this.TranslateLabel.Text = "If you like to translate this software into another language, please contact the " +
                "developers.";
            // 
            // LanguageList
            // 
            this.LanguageList.Items.AddRange(new object[] {
            "English",
            "German"});
            this.LanguageList.Location = new System.Drawing.Point(16, 16);
            this.LanguageList.Name = "LanguageList";
            this.LanguageList.Size = new System.Drawing.Size(120, 56);
            this.LanguageList.TabIndex = 0;
            // 
            // BeamBox_tab
            // 
            this.BeamBox_tab.Controls.Add(this.Position_Title_GroupBox);
            this.BeamBox_tab.Controls.Add(this.Background_groupBox3);
            this.BeamBox_tab.Controls.Add(this.Mouse_groupBox2);
            this.BeamBox_tab.Location = new System.Drawing.Point(4, 40);
            this.BeamBox_tab.Name = "BeamBox_tab";
            this.BeamBox_tab.Size = new System.Drawing.Size(515, 302);
            this.BeamBox_tab.TabIndex = 2;
            this.BeamBox_tab.Text = "Projector Window";
            this.BeamBox_tab.UseVisualStyleBackColor = true;
            // 
            // Position_Title_GroupBox
            // 
            this.Position_Title_GroupBox.Controls.Add(this.SizePosControl);
            this.Position_Title_GroupBox.Location = new System.Drawing.Point(8, 8);
            this.Position_Title_GroupBox.Name = "Position_Title_GroupBox";
            this.Position_Title_GroupBox.Size = new System.Drawing.Size(408, 148);
            this.Position_Title_GroupBox.TabIndex = 7;
            this.Position_Title_GroupBox.TabStop = false;
            this.Position_Title_GroupBox.Text = "Set Position";
            // 
            // SizePosControl
            // 
            this.SizePosControl.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.SizePosControl.Controls.Add(this.tabPage1);
            this.SizePosControl.Controls.Add(this.tabPage2);
            this.SizePosControl.ItemSize = new System.Drawing.Size(190, 18);
            this.SizePosControl.Location = new System.Drawing.Point(8, 16);
            this.SizePosControl.Name = "SizePosControl";
            this.SizePosControl.SelectedIndex = 0;
            this.SizePosControl.Size = new System.Drawing.Size(392, 129);
            this.SizePosControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.SizePosControl.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.Size_groupBox5);
            this.tabPage1.Controls.Add(this.Position_groupBox4);
            this.tabPage1.Controls.Add(this.ScreenList);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(384, 103);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Auto";
            // 
            // Size_groupBox5
            // 
            this.Size_groupBox5.Controls.Add(this.AutoSizeLabelW);
            this.Size_groupBox5.Controls.Add(this.AutoSizeLabelH);
            this.Size_groupBox5.Location = new System.Drawing.Point(136, 52);
            this.Size_groupBox5.Name = "Size_groupBox5";
            this.Size_groupBox5.Size = new System.Drawing.Size(240, 40);
            this.Size_groupBox5.TabIndex = 6;
            this.Size_groupBox5.TabStop = false;
            this.Size_groupBox5.Text = "Size:";
            // 
            // AutoSizeLabelW
            // 
            this.AutoSizeLabelW.Location = new System.Drawing.Point(8, 16);
            this.AutoSizeLabelW.Name = "AutoSizeLabelW";
            this.AutoSizeLabelW.Size = new System.Drawing.Size(104, 16);
            this.AutoSizeLabelW.TabIndex = 7;
            // 
            // AutoSizeLabelH
            // 
            this.AutoSizeLabelH.Location = new System.Drawing.Point(128, 16);
            this.AutoSizeLabelH.Name = "AutoSizeLabelH";
            this.AutoSizeLabelH.Size = new System.Drawing.Size(104, 16);
            this.AutoSizeLabelH.TabIndex = 8;
            // 
            // Position_groupBox4
            // 
            this.Position_groupBox4.Controls.Add(this.AutoPosLabelX);
            this.Position_groupBox4.Controls.Add(this.AutoPosLabelY);
            this.Position_groupBox4.Location = new System.Drawing.Point(136, 8);
            this.Position_groupBox4.Name = "Position_groupBox4";
            this.Position_groupBox4.Size = new System.Drawing.Size(240, 40);
            this.Position_groupBox4.TabIndex = 5;
            this.Position_groupBox4.TabStop = false;
            this.Position_groupBox4.Text = "Position:";
            // 
            // AutoPosLabelX
            // 
            this.AutoPosLabelX.Location = new System.Drawing.Point(8, 16);
            this.AutoPosLabelX.Name = "AutoPosLabelX";
            this.AutoPosLabelX.Size = new System.Drawing.Size(104, 16);
            this.AutoPosLabelX.TabIndex = 3;
            // 
            // AutoPosLabelY
            // 
            this.AutoPosLabelY.Location = new System.Drawing.Point(128, 16);
            this.AutoPosLabelY.Name = "AutoPosLabelY";
            this.AutoPosLabelY.Size = new System.Drawing.Size(104, 16);
            this.AutoPosLabelY.TabIndex = 4;
            // 
            // ScreenList
            // 
            this.ScreenList.Location = new System.Drawing.Point(8, 8);
            this.ScreenList.Name = "ScreenList";
            this.ScreenList.Size = new System.Drawing.Size(120, 82);
            this.ScreenList.TabIndex = 0;
            this.ScreenList.SelectedIndexChanged += new System.EventHandler(this.ScreenList_SelectedIndexChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.BeamBox_PosBox);
            this.tabPage2.Controls.Add(this.BeamBox_SizeBox);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(384, 103);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Manual";
            // 
            // BeamBox_PosBox
            // 
            this.BeamBox_PosBox.Controls.Add(this.label1);
            this.BeamBox_PosBox.Controls.Add(this.label2);
            this.BeamBox_PosBox.Controls.Add(this.BeamBox_posX);
            this.BeamBox_PosBox.Controls.Add(this.BeamBox_posY);
            this.BeamBox_PosBox.Location = new System.Drawing.Point(8, 8);
            this.BeamBox_PosBox.Name = "BeamBox_PosBox";
            this.BeamBox_PosBox.Size = new System.Drawing.Size(368, 43);
            this.BeamBox_PosBox.TabIndex = 0;
            this.BeamBox_PosBox.TabStop = false;
            this.BeamBox_PosBox.Text = "Position";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(32, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "X:";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(224, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Y:";
            // 
            // BeamBox_posX
            // 
            this.BeamBox_posX.Location = new System.Drawing.Point(64, 16);
            this.BeamBox_posX.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.BeamBox_posX.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.BeamBox_posX.Name = "BeamBox_posX";
            this.BeamBox_posX.Size = new System.Drawing.Size(88, 20);
            this.BeamBox_posX.TabIndex = 3;
            // 
            // BeamBox_posY
            // 
            this.BeamBox_posY.Location = new System.Drawing.Point(256, 16);
            this.BeamBox_posY.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.BeamBox_posY.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.BeamBox_posY.Name = "BeamBox_posY";
            this.BeamBox_posY.Size = new System.Drawing.Size(88, 20);
            this.BeamBox_posY.TabIndex = 6;
            // 
            // BeamBox_SizeBox
            // 
            this.BeamBox_SizeBox.Controls.Add(this.HeightLabel);
            this.BeamBox_SizeBox.Controls.Add(this.WidthLabel);
            this.BeamBox_SizeBox.Controls.Add(this.BeamBox_Width);
            this.BeamBox_SizeBox.Controls.Add(this.BeamBox_Height);
            this.BeamBox_SizeBox.Location = new System.Drawing.Point(8, 56);
            this.BeamBox_SizeBox.Name = "BeamBox_SizeBox";
            this.BeamBox_SizeBox.Size = new System.Drawing.Size(368, 43);
            this.BeamBox_SizeBox.TabIndex = 2;
            this.BeamBox_SizeBox.TabStop = false;
            this.BeamBox_SizeBox.Text = "Size";
            // 
            // HeightLabel
            // 
            this.HeightLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HeightLabel.Location = new System.Drawing.Point(200, 16);
            this.HeightLabel.Name = "HeightLabel";
            this.HeightLabel.Size = new System.Drawing.Size(56, 16);
            this.HeightLabel.TabIndex = 4;
            this.HeightLabel.Text = "Height:";
            // 
            // WidthLabel
            // 
            this.WidthLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WidthLabel.Location = new System.Drawing.Point(8, 16);
            this.WidthLabel.Name = "WidthLabel";
            this.WidthLabel.Size = new System.Drawing.Size(56, 23);
            this.WidthLabel.TabIndex = 3;
            this.WidthLabel.Text = "Width:";
            // 
            // BeamBox_Width
            // 
            this.BeamBox_Width.Location = new System.Drawing.Point(64, 16);
            this.BeamBox_Width.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.BeamBox_Width.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.BeamBox_Width.Name = "BeamBox_Width";
            this.BeamBox_Width.Size = new System.Drawing.Size(88, 20);
            this.BeamBox_Width.TabIndex = 5;
            this.BeamBox_Width.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // BeamBox_Height
            // 
            this.BeamBox_Height.Location = new System.Drawing.Point(256, 16);
            this.BeamBox_Height.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.BeamBox_Height.Minimum = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.BeamBox_Height.Name = "BeamBox_Height";
            this.BeamBox_Height.Size = new System.Drawing.Size(88, 20);
            this.BeamBox_Height.TabIndex = 4;
            this.BeamBox_Height.Value = new decimal(new int[] {
            350,
            0,
            0,
            0});
            // 
            // Background_groupBox3
            // 
            this.Background_groupBox3.Controls.Add(this.BeamBox_ColorButton);
            this.Background_groupBox3.Location = new System.Drawing.Point(248, 160);
            this.Background_groupBox3.Name = "Background_groupBox3";
            this.Background_groupBox3.Size = new System.Drawing.Size(168, 72);
            this.Background_groupBox3.TabIndex = 5;
            this.Background_groupBox3.TabStop = false;
            this.Background_groupBox3.Text = "Background Color";
            // 
            // BeamBox_ColorButton
            // 
            this.BeamBox_ColorButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.BeamBox_ColorButton.Location = new System.Drawing.Point(40, 28);
            this.BeamBox_ColorButton.Name = "BeamBox_ColorButton";
            this.BeamBox_ColorButton.Size = new System.Drawing.Size(96, 24);
            this.BeamBox_ColorButton.TabIndex = 4;
            this.BeamBox_ColorButton.Text = "Set Color";
            this.BeamBox_ColorButton.Click += new System.EventHandler(this.BeamBox_ColorButton_Click);
            // 
            // Mouse_groupBox2
            // 
            this.Mouse_groupBox2.Controls.Add(this.BeamBox_AlwaysOnTop);
            this.Mouse_groupBox2.Controls.Add(this.BeamBox_HideMouse);
            this.Mouse_groupBox2.Location = new System.Drawing.Point(8, 160);
            this.Mouse_groupBox2.Name = "Mouse_groupBox2";
            this.Mouse_groupBox2.Size = new System.Drawing.Size(176, 72);
            this.Mouse_groupBox2.TabIndex = 3;
            this.Mouse_groupBox2.TabStop = false;
            this.Mouse_groupBox2.Text = "Mouse Cursor";
            // 
            // BeamBox_AlwaysOnTop
            // 
            this.BeamBox_AlwaysOnTop.Location = new System.Drawing.Point(8, 40);
            this.BeamBox_AlwaysOnTop.Name = "BeamBox_AlwaysOnTop";
            this.BeamBox_AlwaysOnTop.Size = new System.Drawing.Size(160, 24);
            this.BeamBox_AlwaysOnTop.TabIndex = 1;
            this.BeamBox_AlwaysOnTop.Text = "Always on top";
            // 
            // BeamBox_HideMouse
            // 
            this.BeamBox_HideMouse.Location = new System.Drawing.Point(8, 16);
            this.BeamBox_HideMouse.Name = "BeamBox_HideMouse";
            this.BeamBox_HideMouse.Size = new System.Drawing.Size(160, 24);
            this.BeamBox_HideMouse.TabIndex = 0;
            this.BeamBox_HideMouse.Text = "Hide Mouse Cursor";
            // 
            // Misc_Tab
            // 
            this.Misc_Tab.Controls.Add(this.songVerseSeparatorGroup);
            this.Misc_Tab.Controls.Add(this.SwordLangGroupBox);
            this.Misc_Tab.Controls.Add(this.SwordFolderGroupBox);
            this.Misc_Tab.Location = new System.Drawing.Point(4, 40);
            this.Misc_Tab.Name = "Misc_Tab";
            this.Misc_Tab.Size = new System.Drawing.Size(515, 302);
            this.Misc_Tab.TabIndex = 3;
            this.Misc_Tab.Text = "Misc";
            this.Misc_Tab.UseVisualStyleBackColor = true;
            // 
            // songVerseSeparatorGroup
            // 
            this.songVerseSeparatorGroup.Controls.Add(this.verseSep2L);
            this.songVerseSeparatorGroup.Controls.Add(this.verseSep1L);
            this.songVerseSeparatorGroup.Location = new System.Drawing.Point(8, 62);
            this.songVerseSeparatorGroup.Name = "songVerseSeparatorGroup";
            this.songVerseSeparatorGroup.Size = new System.Drawing.Size(200, 68);
            this.songVerseSeparatorGroup.TabIndex = 9;
            this.songVerseSeparatorGroup.TabStop = false;
            this.songVerseSeparatorGroup.Text = "Song Verse Separator";
            // 
            // verseSep2L
            // 
            this.verseSep2L.AutoSize = true;
            this.verseSep2L.Location = new System.Drawing.Point(8, 42);
            this.verseSep2L.Name = "verseSep2L";
            this.verseSep2L.Size = new System.Drawing.Size(99, 17);
            this.verseSep2L.TabIndex = 1;
            this.verseSep2L.Text = "Two blank lines";
            this.Options_ToolTip.SetToolTip(this.verseSep2L, "A single blank line will show up on the live screen as a blank line.");
            this.verseSep2L.UseVisualStyleBackColor = true;
            // 
            // verseSep1L
            // 
            this.verseSep1L.AutoSize = true;
            this.verseSep1L.Checked = true;
            this.verseSep1L.Location = new System.Drawing.Point(8, 19);
            this.verseSep1L.Name = "verseSep1L";
            this.verseSep1L.Size = new System.Drawing.Size(93, 17);
            this.verseSep1L.TabIndex = 0;
            this.verseSep1L.TabStop = true;
            this.verseSep1L.Text = "One blank line";
            this.Options_ToolTip.SetToolTip(this.verseSep1L, "To create blank lines that don\'t split the verse, add a space at the beginning of" +
                    " the line.");
            this.verseSep1L.UseVisualStyleBackColor = true;
            // 
            // SwordLangGroupBox
            // 
            this.SwordLangGroupBox.Controls.Add(this.Sword_LanguageBox);
            this.SwordLangGroupBox.Location = new System.Drawing.Point(328, 8);
            this.SwordLangGroupBox.Name = "SwordLangGroupBox";
            this.SwordLangGroupBox.Size = new System.Drawing.Size(112, 48);
            this.SwordLangGroupBox.TabIndex = 8;
            this.SwordLangGroupBox.TabStop = false;
            this.SwordLangGroupBox.Text = "Sword Language";
            // 
            // Sword_LanguageBox
            // 
            this.Sword_LanguageBox.Items.AddRange(new object[] {
            "af",
            "cs",
            "da",
            "de",
            "en",
            "es",
            "et",
            "fi",
            "fr",
            "hu",
            "id",
            "it",
            "la",
            "nl",
            "no",
            "pl",
            "pt",
            "ro",
            "ru",
            "sk",
            "sl"});
            this.Sword_LanguageBox.Location = new System.Drawing.Point(8, 16);
            this.Sword_LanguageBox.Name = "Sword_LanguageBox";
            this.Sword_LanguageBox.Size = new System.Drawing.Size(96, 21);
            this.Sword_LanguageBox.TabIndex = 5;
            // 
            // SwordFolderGroupBox
            // 
            this.SwordFolderGroupBox.Controls.Add(this.Sword_PathBox);
            this.SwordFolderGroupBox.Controls.Add(this.Select_Sword);
            this.SwordFolderGroupBox.Location = new System.Drawing.Point(8, 8);
            this.SwordFolderGroupBox.Name = "SwordFolderGroupBox";
            this.SwordFolderGroupBox.Size = new System.Drawing.Size(312, 48);
            this.SwordFolderGroupBox.TabIndex = 7;
            this.SwordFolderGroupBox.TabStop = false;
            this.SwordFolderGroupBox.Text = "Sword Project Folder";
            // 
            // Sword_PathBox
            // 
            this.Sword_PathBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Sword_PathBox.Location = new System.Drawing.Point(8, 16);
            this.Sword_PathBox.Name = "Sword_PathBox";
            this.Sword_PathBox.Size = new System.Drawing.Size(216, 20);
            this.Sword_PathBox.TabIndex = 1;
            // 
            // Select_Sword
            // 
            this.Select_Sword.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.Select_Sword.Location = new System.Drawing.Point(232, 16);
            this.Select_Sword.Name = "Select_Sword";
            this.Select_Sword.Size = new System.Drawing.Size(72, 24);
            this.Select_Sword.TabIndex = 0;
            this.Select_Sword.Text = "Browse";
            this.Select_Sword.Click += new System.EventHandler(this.Select_Sword_Click);
            // 
            // BibleCache_Tab
            // 
            this.BibleCache_Tab.Controls.Add(this.groupBox2);
            this.BibleCache_Tab.Controls.Add(this.BibleCache_Cached_Label);
            this.BibleCache_Tab.Controls.Add(this.BibleCache_Available_Label);
            this.BibleCache_Tab.Controls.Add(this.BibleCache_Replacements_Label);
            this.BibleCache_Tab.Controls.Add(this.BibleCache_Message);
            this.BibleCache_Tab.Controls.Add(this.BibleConversions_dataGrid);
            this.BibleCache_Tab.Controls.Add(this.BibleCache_progressBar);
            this.BibleCache_Tab.Controls.Add(this.BiblesAvailable_listEx);
            this.BibleCache_Tab.Controls.Add(this.BiblesCached_listEx);
            this.BibleCache_Tab.Location = new System.Drawing.Point(4, 40);
            this.BibleCache_Tab.Name = "BibleCache_Tab";
            this.BibleCache_Tab.Size = new System.Drawing.Size(515, 302);
            this.BibleCache_Tab.TabIndex = 4;
            this.BibleCache_Tab.Text = "Bible Cache";
            this.BibleCache_Tab.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.BibleConversions_Custom);
            this.groupBox2.Controls.Add(this.BibleConversions_Default);
            this.groupBox2.Location = new System.Drawing.Point(266, 244);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(241, 48);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Replacement type";
            // 
            // BibleConversions_Custom
            // 
            this.BibleConversions_Custom.AutoSize = true;
            this.BibleConversions_Custom.Location = new System.Drawing.Point(110, 19);
            this.BibleConversions_Custom.Name = "BibleConversions_Custom";
            this.BibleConversions_Custom.Size = new System.Drawing.Size(122, 17);
            this.BibleConversions_Custom.TabIndex = 1;
            this.BibleConversions_Custom.TabStop = true;
            this.BibleConversions_Custom.Text = "Custom (from above)";
            this.BibleConversions_Custom.UseVisualStyleBackColor = true;
            this.BibleConversions_Custom.Click += new System.EventHandler(this.BibleConversions_Type_Click);
            // 
            // BibleConversions_Default
            // 
            this.BibleConversions_Default.AutoSize = true;
            this.BibleConversions_Default.Location = new System.Drawing.Point(6, 19);
            this.BibleConversions_Default.Name = "BibleConversions_Default";
            this.BibleConversions_Default.Size = new System.Drawing.Size(59, 17);
            this.BibleConversions_Default.TabIndex = 0;
            this.BibleConversions_Default.TabStop = true;
            this.BibleConversions_Default.Text = "Default";
            this.BibleConversions_Default.UseVisualStyleBackColor = true;
            this.BibleConversions_Default.Click += new System.EventHandler(this.BibleConversions_Type_Click);
            // 
            // BibleCache_Cached_Label
            // 
            this.BibleCache_Cached_Label.AutoSize = true;
            this.BibleCache_Cached_Label.Location = new System.Drawing.Point(6, 7);
            this.BibleCache_Cached_Label.Name = "BibleCache_Cached_Label";
            this.BibleCache_Cached_Label.Size = new System.Drawing.Size(100, 13);
            this.BibleCache_Cached_Label.TabIndex = 13;
            this.BibleCache_Cached_Label.Text = "Cached translations";
            // 
            // BibleCache_Available_Label
            // 
            this.BibleCache_Available_Label.AutoSize = true;
            this.BibleCache_Available_Label.Location = new System.Drawing.Point(134, 7);
            this.BibleCache_Available_Label.Name = "BibleCache_Available_Label";
            this.BibleCache_Available_Label.Size = new System.Drawing.Size(80, 13);
            this.BibleCache_Available_Label.TabIndex = 12;
            this.BibleCache_Available_Label.Text = "Not yet cached";
            // 
            // BibleCache_Replacements_Label
            // 
            this.BibleCache_Replacements_Label.AutoSize = true;
            this.BibleCache_Replacements_Label.Location = new System.Drawing.Point(263, 7);
            this.BibleCache_Replacements_Label.Name = "BibleCache_Replacements_Label";
            this.BibleCache_Replacements_Label.Size = new System.Drawing.Size(153, 13);
            this.BibleCache_Replacements_Label.TabIndex = 11;
            this.BibleCache_Replacements_Label.Text = "Replacements when searching";
            // 
            // BibleCache_Message
            // 
            this.BibleCache_Message.Location = new System.Drawing.Point(8, 244);
            this.BibleCache_Message.Name = "BibleCache_Message";
            this.BibleCache_Message.Size = new System.Drawing.Size(232, 48);
            this.BibleCache_Message.TabIndex = 10;
            this.BibleCache_Message.Text = "Please wait. Extracting bible verses from Sword. Operation could take a few minut" +
                "es.";
            this.BibleCache_Message.Visible = false;
            // 
            // BibleConversions_dataGrid
            // 
            this.BibleConversions_dataGrid.AllowSorting = false;
            this.BibleConversions_dataGrid.AlternatingBackColor = System.Drawing.SystemColors.Window;
            this.BibleConversions_dataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.BibleConversions_dataGrid.CaptionBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BibleConversions_dataGrid.CaptionVisible = false;
            this.BibleConversions_dataGrid.DataMember = "";
            this.BibleConversions_dataGrid.DataSource = this.Options_RegEx_Table;
            this.BibleConversions_dataGrid.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.BibleConversions_dataGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.BibleConversions_dataGrid.Location = new System.Drawing.Point(266, 24);
            this.BibleConversions_dataGrid.Name = "BibleConversions_dataGrid";
            this.BibleConversions_dataGrid.ParentRowsBackColor = System.Drawing.SystemColors.Control;
            this.BibleConversions_dataGrid.RowHeadersVisible = false;
            this.BibleConversions_dataGrid.Size = new System.Drawing.Size(243, 217);
            this.BibleConversions_dataGrid.TabIndex = 3;
            this.BibleConversions_dataGrid.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.BibleConversions_GridTableStyle});
            this.Options_ToolTip.SetToolTip(this.BibleConversions_dataGrid, "Your last pair should be \"[^a-zA-Z0-9 ]\" -> \"\"");
            // 
            // Options_RegEx_Table
            // 
            this.Options_RegEx_Table.Columns.AddRange(new System.Data.DataColumn[] {
            this.RegEx_Find,
            this.RegEx_Replace});
            this.Options_RegEx_Table.TableName = "Options_RegEx";
            // 
            // RegEx_Find
            // 
            this.RegEx_Find.Caption = "Find";
            this.RegEx_Find.ColumnName = "Find";
            // 
            // RegEx_Replace
            // 
            this.RegEx_Replace.Caption = "Replace";
            this.RegEx_Replace.ColumnName = "Replace";
            // 
            // BibleConversions_GridTableStyle
            // 
            this.BibleConversions_GridTableStyle.DataGrid = this.BibleConversions_dataGrid;
            this.BibleConversions_GridTableStyle.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
            this.dataGridTextBoxColumn1,
            this.dataGridTextBoxColumn2});
            this.BibleConversions_GridTableStyle.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.BibleConversions_GridTableStyle.PreferredColumnWidth = 45;
            // 
            // dataGridTextBoxColumn1
            // 
            this.dataGridTextBoxColumn1.Format = "";
            this.dataGridTextBoxColumn1.FormatInfo = null;
            this.dataGridTextBoxColumn1.Width = 45;
            // 
            // dataGridTextBoxColumn2
            // 
            this.dataGridTextBoxColumn2.Format = "";
            this.dataGridTextBoxColumn2.FormatInfo = null;
            this.dataGridTextBoxColumn2.Width = 45;
            // 
            // BibleCache_progressBar
            // 
            this.BibleCache_progressBar.Location = new System.Drawing.Point(6, 206);
            this.BibleCache_progressBar.Name = "BibleCache_progressBar";
            this.BibleCache_progressBar.Size = new System.Drawing.Size(242, 23);
            this.BibleCache_progressBar.TabIndex = 2;
            this.BibleCache_progressBar.Visible = false;
            // 
            // BiblesAvailable_listEx
            // 
            this.BiblesAvailable_listEx.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.BiblesAvailable_listEx.Imgs = this.ListEx_ImageList;
            this.BiblesAvailable_listEx.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(199)))), ((int)(((byte)(199)))));
            this.BiblesAvailable_listEx.Location = new System.Drawing.Point(134, 24);
            this.BiblesAvailable_listEx.Name = "BiblesAvailable_listEx";
            this.BiblesAvailable_listEx.ReadOnly = true;
            this.BiblesAvailable_listEx.ShowBullets = true;
            this.BiblesAvailable_listEx.Size = new System.Drawing.Size(112, 176);
            this.BiblesAvailable_listEx.TabIndex = 1;
            this.BiblesAvailable_listEx.DoubleClick += new System.EventHandler(this.BiblesAvailable_listEx_DoubleClick);
            this.BiblesAvailable_listEx.PressIcon += new Lister.ListEx.EventHandler(this.BiblesAvailable_listEx_PressIcon);
            // 
            // ListEx_ImageList
            // 
            this.ListEx_ImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ListEx_ImageList.ImageStream")));
            this.ListEx_ImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.ListEx_ImageList.Images.SetKeyName(0, "");
            this.ListEx_ImageList.Images.SetKeyName(1, "");
            // 
            // BiblesCached_listEx
            // 
            this.BiblesCached_listEx.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.BiblesCached_listEx.Imgs = this.ListEx_ImageList;
            this.BiblesCached_listEx.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(199)))), ((int)(((byte)(199)))));
            this.BiblesCached_listEx.Location = new System.Drawing.Point(6, 24);
            this.BiblesCached_listEx.Name = "BiblesCached_listEx";
            this.BiblesCached_listEx.ReadOnly = true;
            this.BiblesCached_listEx.ShowBullets = true;
            this.BiblesCached_listEx.Size = new System.Drawing.Size(112, 176);
            this.BiblesCached_listEx.TabIndex = 0;
            this.BiblesCached_listEx.DoubleClick += new System.EventHandler(this.BiblesCached_listEx_DoubleClick);
            this.BiblesCached_listEx.PressIcon += new Lister.ListEx.EventHandler(this.BiblesCached_listEx_PressIcon);
            // 
            // Graphics_tab
            // 
            this.Graphics_tab.Controls.Add(this.PreRenderBox);
            this.Graphics_tab.Controls.Add(this.Alpha_groupBox1);
            this.Graphics_tab.Location = new System.Drawing.Point(4, 22);
            this.Graphics_tab.Name = "Graphics_tab";
            this.Graphics_tab.Size = new System.Drawing.Size(515, 302);
            this.Graphics_tab.TabIndex = 1;
            this.Graphics_tab.Text = "Graphics";
            this.Graphics_tab.UseVisualStyleBackColor = true;
            // 
            // PreRenderBox
            // 
            this.PreRenderBox.Controls.Add(this.PreRenderLabel);
            this.PreRenderBox.Controls.Add(this.PreRendercheckBox);
            this.PreRenderBox.Location = new System.Drawing.Point(8, 136);
            this.PreRenderBox.Name = "PreRenderBox";
            this.PreRenderBox.Size = new System.Drawing.Size(408, 96);
            this.PreRenderBox.TabIndex = 2;
            this.PreRenderBox.TabStop = false;
            this.PreRenderBox.Text = "PreRender Verses";
            // 
            // PreRenderLabel
            // 
            this.PreRenderLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PreRenderLabel.Location = new System.Drawing.Point(120, 12);
            this.PreRenderLabel.Name = "PreRenderLabel";
            this.PreRenderLabel.Size = new System.Drawing.Size(280, 78);
            this.PreRenderLabel.TabIndex = 1;
            this.PreRenderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PreRendercheckBox
            // 
            this.PreRendercheckBox.Location = new System.Drawing.Point(16, 40);
            this.PreRendercheckBox.Name = "PreRendercheckBox";
            this.PreRendercheckBox.Size = new System.Drawing.Size(96, 24);
            this.PreRendercheckBox.TabIndex = 0;
            this.PreRendercheckBox.Text = "PreRender";
            // 
            // Alpha_groupBox1
            // 
            this.Alpha_groupBox1.Controls.Add(this.SpeedLabel);
            this.Alpha_groupBox1.Controls.Add(this.Speed_Updown);
            this.Alpha_groupBox1.Controls.Add(this.Direct3D_CheckBox);
            this.Alpha_groupBox1.Controls.Add(this.Alpha_CheckBox);
            this.Alpha_groupBox1.Location = new System.Drawing.Point(8, 8);
            this.Alpha_groupBox1.Name = "Alpha_groupBox1";
            this.Alpha_groupBox1.Size = new System.Drawing.Size(408, 64);
            this.Alpha_groupBox1.TabIndex = 1;
            this.Alpha_groupBox1.TabStop = false;
            this.Alpha_groupBox1.Text = "Alpha Blending";
            // 
            // SpeedLabel
            // 
            this.SpeedLabel.Location = new System.Drawing.Point(192, 24);
            this.SpeedLabel.Name = "SpeedLabel";
            this.SpeedLabel.Size = new System.Drawing.Size(88, 24);
            this.SpeedLabel.TabIndex = 7;
            this.SpeedLabel.Text = "Speed";
            this.SpeedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Speed_Updown
            // 
            this.Speed_Updown.Location = new System.Drawing.Point(296, 24);
            this.Speed_Updown.Maximum = new decimal(new int[] {
            75,
            0,
            0,
            0});
            this.Speed_Updown.Minimum = 1;
            this.Speed_Updown.Name = "Speed_Updown";
            this.Speed_Updown.Size = new System.Drawing.Size(64, 20);
            this.Speed_Updown.TabIndex = 6;
            // 
            // Direct3D_CheckBox
            // 
            this.Direct3D_CheckBox.Location = new System.Drawing.Point(376, 24);
            this.Direct3D_CheckBox.Name = "Direct3D_CheckBox";
            this.Direct3D_CheckBox.Size = new System.Drawing.Size(24, 24);
            this.Direct3D_CheckBox.TabIndex = 5;
            this.Direct3D_CheckBox.Text = "use Direct3D";
            this.Direct3D_CheckBox.Visible = false;
            // 
            // Alpha_CheckBox
            // 
            this.Alpha_CheckBox.Location = new System.Drawing.Point(16, 24);
            this.Alpha_CheckBox.Name = "Alpha_CheckBox";
            this.Alpha_CheckBox.Size = new System.Drawing.Size(176, 24);
            this.Alpha_CheckBox.TabIndex = 4;
            this.Alpha_CheckBox.Text = "AlphaBlending";
            // 
            // SongFormat_Tab
            // 
            this.SongFormat_Tab.Controls.Add(this.listBox1);
            this.SongFormat_Tab.Controls.Add(this.songThemeWidget);
            this.SongFormat_Tab.Location = new System.Drawing.Point(4, 40);
            this.SongFormat_Tab.Name = "SongFormat_Tab";
            this.SongFormat_Tab.Size = new System.Drawing.Size(515, 302);
            this.SongFormat_Tab.TabIndex = 6;
            this.SongFormat_Tab.Text = "Song Format";
            this.SongFormat_Tab.UseVisualStyleBackColor = true;
            // 
            // songThemeWidget
            // 
            this.songThemeWidget.BGImagePath = "Image";
            this.songThemeWidget.Location = new System.Drawing.Point(0, 3);
            this.songThemeWidget.Name = "songThemeWidget";
            this.songThemeWidget.Size = new System.Drawing.Size(266, 309);
            this.songThemeWidget.TabIndex = 0;
            this.songThemeWidget.TabNames = new string[] {
        "Title",
        "Verse",
        "Author",
        "Key"};
            this.songThemeWidget.ThemePath = "";
            this.songThemeWidget.UseDesign = false;
            this.songThemeWidget.ControlChangedEvent += new System.EventHandler(this.songThemeWidget_ControlChangedEvent);
            // 
            // BibleFormat_Tab
            // 
            this.BibleFormat_Tab.Controls.Add(this.bibleFormatWidget);
            this.BibleFormat_Tab.Location = new System.Drawing.Point(4, 40);
            this.BibleFormat_Tab.Name = "BibleFormat_Tab";
            this.BibleFormat_Tab.Size = new System.Drawing.Size(515, 302);
            this.BibleFormat_Tab.TabIndex = 5;
            this.BibleFormat_Tab.Text = "Bible Format";
            this.BibleFormat_Tab.UseVisualStyleBackColor = true;
            // 
            // bibleFormatWidget
            // 
            this.bibleFormatWidget.BGImagePath = "Image";
            this.bibleFormatWidget.Location = new System.Drawing.Point(0, 0);
            this.bibleFormatWidget.Name = "bibleFormatWidget";
            this.bibleFormatWidget.Size = new System.Drawing.Size(515, 312);
            this.bibleFormatWidget.TabIndex = 0;
            this.bibleFormatWidget.TabNames = new string[] {
        "Verse",
        "Reference",
        "Translation"};
            this.bibleFormatWidget.ThemePath = "";
            this.bibleFormatWidget.UseDesign = false;
            this.bibleFormatWidget.ControlChangedEvent += new System.EventHandler(this.bibleFormatWidget_ControlChangedEvent);
            // 
            // TextToolFormat_Tab
            // 
            this.TextToolFormat_Tab.Controls.Add(this.sermonThemeWidget);
            this.TextToolFormat_Tab.Location = new System.Drawing.Point(4, 40);
            this.TextToolFormat_Tab.Name = "TextToolFormat_Tab";
            this.TextToolFormat_Tab.Size = new System.Drawing.Size(515, 302);
            this.TextToolFormat_Tab.TabIndex = 7;
            this.TextToolFormat_Tab.Text = "Sermon Tool Format";
            this.TextToolFormat_Tab.UseVisualStyleBackColor = true;
            // 
            // sermonThemeWidget
            // 
            this.sermonThemeWidget.BGImagePath = "Image";
            this.sermonThemeWidget.Location = new System.Drawing.Point(0, 0);
            this.sermonThemeWidget.Name = "sermonThemeWidget";
            this.sermonThemeWidget.Size = new System.Drawing.Size(515, 312);
            this.sermonThemeWidget.TabIndex = 0;
            this.sermonThemeWidget.TabNames = new string[] {
        "1st line",
        "Other lines"};
            this.sermonThemeWidget.ThemePath = "";
            this.sermonThemeWidget.UseDesign = false;
            this.sermonThemeWidget.ControlChangedEvent += new System.EventHandler(this.sermonThemeWidget_ControlChangedEvent);
            // 
            // Options_Cancelbtn
            // 
            this.Options_Cancelbtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.Options_Cancelbtn.Location = new System.Drawing.Point(248, 354);
            this.Options_Cancelbtn.Name = "Options_Cancelbtn";
            this.Options_Cancelbtn.Size = new System.Drawing.Size(75, 23);
            this.Options_Cancelbtn.TabIndex = 1;
            this.Options_Cancelbtn.Text = "Cancel";
            this.Options_Cancelbtn.Click += new System.EventHandler(this.Options_Cancelbtn_Click);
            // 
            // Options_Okaybtn
            // 
            this.Options_Okaybtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.Options_Okaybtn.Location = new System.Drawing.Point(160, 354);
            this.Options_Okaybtn.Name = "Options_Okaybtn";
            this.Options_Okaybtn.Size = new System.Drawing.Size(75, 23);
            this.Options_Okaybtn.TabIndex = 2;
            this.Options_Okaybtn.Text = "OK";
            this.Options_Okaybtn.Click += new System.EventHandler(this.Options_OkBtn_Click);
            // 
            // Options_DataSet
            // 
            this.Options_DataSet.DataSetName = "Options_DataSet";
            this.Options_DataSet.Locale = new System.Globalization.CultureInfo("en-US");
            this.Options_DataSet.Tables.AddRange(new System.Data.DataTable[] {
            this.Options_RegEx_Table});
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Items.AddRange(new object[] {
            "hhhhhhhhhhhhhhh",
            "pppppppppppppppp",
            "iiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiii"});
            this.listBox1.Location = new System.Drawing.Point(325, 42);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(111, 69);
            this.listBox1.TabIndex = 1;
            
            // 
            // Options
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(523, 384);
            this.Controls.Add(this.Options_Okaybtn);
            this.Controls.Add(this.Options_Cancelbtn);
            this.Controls.Add(this.Options_TopPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Options";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.Load += new System.EventHandler(this.Options_Load);
            this.Options_TopPanel.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.Common_Tab.ResumeLayout(false);
            this.Common_Tab.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ListeningPort)).EndInit();
            this.LanguageBox.ResumeLayout(false);
            this.BeamBox_tab.ResumeLayout(false);
            this.Position_Title_GroupBox.ResumeLayout(false);
            this.SizePosControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.Size_groupBox5.ResumeLayout(false);
            this.Position_groupBox4.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.BeamBox_PosBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BeamBox_posX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BeamBox_posY)).EndInit();
            this.BeamBox_SizeBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BeamBox_Width)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BeamBox_Height)).EndInit();
            this.Background_groupBox3.ResumeLayout(false);
            this.Mouse_groupBox2.ResumeLayout(false);
            this.Misc_Tab.ResumeLayout(false);
            this.songVerseSeparatorGroup.ResumeLayout(false);
            this.songVerseSeparatorGroup.PerformLayout();
            this.SwordLangGroupBox.ResumeLayout(false);
            this.SwordFolderGroupBox.ResumeLayout(false);
            this.SwordFolderGroupBox.PerformLayout();
            this.BibleCache_Tab.ResumeLayout(false);
            this.BibleCache_Tab.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BibleConversions_dataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Options_RegEx_Table)).EndInit();
            this.Graphics_tab.ResumeLayout(false);
            this.PreRenderBox.ResumeLayout(false);
            this.Alpha_groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Speed_Updown)).EndInit();
            this.SongFormat_Tab.ResumeLayout(false);
            this.BibleFormat_Tab.ResumeLayout(false);
            this.TextToolFormat_Tab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Options_DataSet)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		private ThemeWidget songThemeWidget;
		private ThemeWidget bibleFormatWidget;
		private ThemeWidget sermonThemeWidget;
		private Label BibleCache_Replacements_Label;
		private Label BibleCache_Available_Label;
		private Label BibleCache_Cached_Label;
		private Label label3;
		private TextBox DataDirectory;
		private GroupBox songVerseSeparatorGroup;
		private RadioButton verseSep2L;
		private RadioButton verseSep1L;
        private GroupBox groupBox2;
        private RadioButton BibleConversions_Custom;
        private RadioButton BibleConversions_Default;
        private ListBox listBox1;

	}
}
