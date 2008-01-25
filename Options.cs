using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Rilling.Common.UI.Forms;
using Lister;
using DreamBeam.FileTypes;
using cs_IniHandlerDevelop;

namespace DreamBeam {

    public class Options : System.Windows.Forms.Form {

		public Config Conf;
		private Language Lang = new Language();
		public MainForm _MainForm = null;
		private const string ImageFileFilter = "Image Files (*.bmp;*.jpg;*.jpeg;*.png;*.gif)|*.bmp;*.jpg;*.jpeg;*.png;*.gif|All files (*.*)|*.*";

		#region Designer variables and objects
		private System.ComponentModel.IContainer components;
        public System.Windows.Forms.TabControl tabControl;
        public OPaC.Themed.Forms.TabPage Common_Tab;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Panel Options_TopPanel;
		private System.Windows.Forms.Button Select_Sword;
        private System.Windows.Forms.Button Options_Cancelbtn;

        private System.Windows.Forms.Button Options_Okaybtn;
        private System.Windows.Forms.TextBox Sword_PathBox;
        private System.Windows.Forms.GroupBox SwordFolderGroupBox;
		public System.Windows.Forms.ComboBox Sword_LanguageBox;
        private System.Windows.Forms.GroupBox SwordLangGroupBox;
        public OPaC.Themed.Forms.TabPage Graphics_tab;

        private System.Windows.Forms.GroupBox Grafics_Outline;
        private System.Windows.Forms.NumericUpDown OutlineSize_UpDown1;
        public OPaC.Themed.Forms.TabPage BeamBox_tab;
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
        public OPaC.Themed.Forms.TabPage Bible_Tab;
        private System.Windows.Forms.GroupBox LanguageBox;
        private System.Windows.Forms.ListBox LanguageList;
        private System.Windows.Forms.Label TranslateLabel;
		private System.Windows.Forms.CheckBox Direct3D_CheckBox;
		private System.Windows.Forms.NumericUpDown Speed_Updown;
		private System.Windows.Forms.Label SpeedLabel;
		private System.Windows.Forms.GroupBox PreRenderBox;
		private System.Windows.Forms.CheckBox PreRendercheckBox;
		private OPaC.Themed.Forms.TabPage BibleCache_Tab;
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
		private OPaC.Themed.Forms.Label label3;
		private OPaC.Themed.Forms.Label BibleCache_Available_Label;
		private OPaC.Themed.Forms.Label BibleCache_Replacements_Label;
		private OPaC.Themed.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.TabPage tabPage5;
		private System.Windows.Forms.CheckBox Options_PanelLocations_checkBox;
		private DreamBeam.TextFormatOptions BibleRef_TextFormat;
		private DreamBeam.TextFormatOptions BibleTransl_TextFormat;
		private DreamBeam.TextFormatOptions BibleVerse_TextFormat;
		private System.Windows.Forms.TextBox BibleBGImagePath;
		private System.Windows.Forms.Button BibleBGImageBrowse;
		private OPaC.Themed.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox ServerAddress;
		private OPaC.Themed.Forms.Label label4;
		private OPaC.Themed.Forms.Label label5;
		private System.Windows.Forms.NumericUpDown ListeningPort;
		private System.Windows.Forms.RadioButton OperatingMode_StandAlone;
		private System.Windows.Forms.RadioButton OperatingMode_Client;
		private System.Windows.Forms.RadioButton OperatingMode_Server;
		private OPaC.Themed.Forms.TabPage BibleFormat_Tab;
		private OPaC.Themed.Forms.TabPage SongFormat_Tab;
		private OPaC.Themed.Forms.GroupBox groupBox3;
		private System.Windows.Forms.TabControl tabControl2;
		private System.Windows.Forms.TabPage SongFormat_Title_Tab;
		private System.Windows.Forms.TabPage SongFormat_Verse_Tab;
		private System.Windows.Forms.TabPage SongFormat_Author_Tab;
		private DreamBeam.TextFormatOptions SongTitle_TextFormat;
		private DreamBeam.TextFormatOptions SongVerse_TextFormat;
		private DreamBeam.TextFormatOptions SongAuthor_TextFormat;
		private System.Windows.Forms.TextBox SongBGImagePath;
		private System.Windows.Forms.Button SongBGImageBrowse;
		private OPaC.Themed.Forms.TabPage TextToolFormat_Tab;
		private System.Windows.Forms.TabControl tabControl3;
		private System.Windows.Forms.TabPage tabPage6;
		private System.Windows.Forms.TabPage tabPage7;
		private OPaC.Themed.Forms.GroupBox groupBox4;
		private DreamBeam.TextFormatOptions TextTool_1stLine_TextFormat;
		private DreamBeam.TextFormatOptions TextTool_OtherLines_TextFormat;
		private System.Windows.Forms.TextBox TextBGImagePath;
		private System.Windows.Forms.Button TextBGImageBrowse;
		private System.Windows.Forms.ToolTip Options_ToolTip;
		private System.Data.DataSet Options_DataSet;
		private System.Windows.Forms.Label BibleCache_Message;
        private Button SaveSongFormatButton;
        private Button OpenSongFormatButton;
        private ThemeWidget songThemeWidget;
		private System.Windows.Forms.Label PreRenderLabel;
		#endregion

        public Options(MainForm mainForm) {
			_MainForm = mainForm;
			this.Conf = _MainForm.Config;

			InitializeComponent();

			PopulateBibleCacheTab();

			string DataSetFile = Tools.GetCommonAppDataPath() + "/" + _MainForm.ConfigSet + ".dataset.config.xml";
			if (Tools.FileExists(DataSetFile)) {
				this.Options_DataSet.ReadXml( DataSetFile, XmlReadMode.ReadSchema );
			}

			SizeColumns(this.BibleConversions_dataGrid);
        }

        /// <summary>
        /// Ressourcen nach der Verwendung bereinigen
        /// </summary>
        protected override void Dispose (bool disposing) {
            if (disposing) {
                if (components != null) {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

#region Windows Form-Designer Code
        /// <summary>
        /// Erforderliche Methode zur Unterstützung des Designers -
        /// ändern Sie die Methode nicht mit dem Quelltext-Editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Options));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.Options_TopPanel = new System.Windows.Forms.Panel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.Common_Tab = new OPaC.Themed.Forms.TabPage();
            this.groupBox1 = new OPaC.Themed.Forms.GroupBox();
            this.ListeningPort = new System.Windows.Forms.NumericUpDown();
            this.label5 = new OPaC.Themed.Forms.Label();
            this.label4 = new OPaC.Themed.Forms.Label();
            this.OperatingMode_Server = new System.Windows.Forms.RadioButton();
            this.OperatingMode_Client = new System.Windows.Forms.RadioButton();
            this.OperatingMode_StandAlone = new System.Windows.Forms.RadioButton();
            this.ServerAddress = new System.Windows.Forms.TextBox();
            this.Options_PanelLocations_checkBox = new System.Windows.Forms.CheckBox();
            this.LanguageBox = new System.Windows.Forms.GroupBox();
            this.TranslateLabel = new System.Windows.Forms.Label();
            this.LanguageList = new System.Windows.Forms.ListBox();
            this.BeamBox_tab = new OPaC.Themed.Forms.TabPage();
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
            this.Bible_Tab = new OPaC.Themed.Forms.TabPage();
            this.SwordLangGroupBox = new System.Windows.Forms.GroupBox();
            this.Sword_LanguageBox = new System.Windows.Forms.ComboBox();
            this.SwordFolderGroupBox = new System.Windows.Forms.GroupBox();
            this.Sword_PathBox = new System.Windows.Forms.TextBox();
            this.Select_Sword = new System.Windows.Forms.Button();
            this.BibleCache_Tab = new OPaC.Themed.Forms.TabPage();
            this.BibleCache_Message = new System.Windows.Forms.Label();
            this.BibleCache_Replacements_Label = new OPaC.Themed.Forms.Label();
            this.BibleCache_Available_Label = new OPaC.Themed.Forms.Label();
            this.label3 = new OPaC.Themed.Forms.Label();
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
            this.Graphics_tab = new OPaC.Themed.Forms.TabPage();
            this.PreRenderBox = new System.Windows.Forms.GroupBox();
            this.PreRenderLabel = new System.Windows.Forms.Label();
            this.PreRendercheckBox = new System.Windows.Forms.CheckBox();
            this.Alpha_groupBox1 = new System.Windows.Forms.GroupBox();
            this.SpeedLabel = new System.Windows.Forms.Label();
            this.Speed_Updown = new System.Windows.Forms.NumericUpDown();
            this.Direct3D_CheckBox = new System.Windows.Forms.CheckBox();
            this.Alpha_CheckBox = new System.Windows.Forms.CheckBox();
            this.Grafics_Outline = new System.Windows.Forms.GroupBox();
            this.OutlineSize_UpDown1 = new System.Windows.Forms.NumericUpDown();
            this.SongFormat_Tab = new OPaC.Themed.Forms.TabPage();
            this.OpenSongFormatButton = new System.Windows.Forms.Button();
            this.SaveSongFormatButton = new System.Windows.Forms.Button();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.SongFormat_Title_Tab = new System.Windows.Forms.TabPage();
            this.SongTitle_TextFormat = new DreamBeam.TextFormatOptions();
            this.SongFormat_Verse_Tab = new System.Windows.Forms.TabPage();
            this.SongVerse_TextFormat = new DreamBeam.TextFormatOptions();
            this.SongFormat_Author_Tab = new System.Windows.Forms.TabPage();
            this.SongAuthor_TextFormat = new DreamBeam.TextFormatOptions();
            this.groupBox3 = new OPaC.Themed.Forms.GroupBox();
            this.SongBGImageBrowse = new System.Windows.Forms.Button();
            this.SongBGImagePath = new System.Windows.Forms.TextBox();
            this.BibleFormat_Tab = new OPaC.Themed.Forms.TabPage();
            this.groupBox2 = new OPaC.Themed.Forms.GroupBox();
            this.BibleBGImageBrowse = new System.Windows.Forms.Button();
            this.BibleBGImagePath = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.BibleRef_TextFormat = new DreamBeam.TextFormatOptions();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.BibleVerse_TextFormat = new DreamBeam.TextFormatOptions();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.BibleTransl_TextFormat = new DreamBeam.TextFormatOptions();
            this.TextToolFormat_Tab = new OPaC.Themed.Forms.TabPage();
            this.groupBox4 = new OPaC.Themed.Forms.GroupBox();
            this.TextBGImageBrowse = new System.Windows.Forms.Button();
            this.TextBGImagePath = new System.Windows.Forms.TextBox();
            this.tabControl3 = new System.Windows.Forms.TabControl();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.TextTool_1stLine_TextFormat = new DreamBeam.TextFormatOptions();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.TextTool_OtherLines_TextFormat = new DreamBeam.TextFormatOptions();
            this.Options_Cancelbtn = new System.Windows.Forms.Button();
            this.Options_Okaybtn = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.Options_ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.Options_DataSet = new System.Data.DataSet();
            this.songThemeWidget = new DreamBeam.ThemeWidget();
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
            this.Bible_Tab.SuspendLayout();
            this.SwordLangGroupBox.SuspendLayout();
            this.SwordFolderGroupBox.SuspendLayout();
            this.BibleCache_Tab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BibleConversions_dataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Options_RegEx_Table)).BeginInit();
            this.Graphics_tab.SuspendLayout();
            this.PreRenderBox.SuspendLayout();
            this.Alpha_groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Speed_Updown)).BeginInit();
            this.Grafics_Outline.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OutlineSize_UpDown1)).BeginInit();
            this.SongFormat_Tab.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.SongFormat_Title_Tab.SuspendLayout();
            this.SongFormat_Verse_Tab.SuspendLayout();
            this.SongFormat_Author_Tab.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.BibleFormat_Tab.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.TextToolFormat_Tab.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tabControl3.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.tabPage7.SuspendLayout();
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
            this.Options_TopPanel.Size = new System.Drawing.Size(656, 346);
            this.Options_TopPanel.TabIndex = 0;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.Common_Tab);
            this.tabControl.Controls.Add(this.BeamBox_tab);
            this.tabControl.Controls.Add(this.Bible_Tab);
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
            this.tabControl.Size = new System.Drawing.Size(656, 346);
            this.tabControl.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tabControl.TabIndex = 1;
            // 
            // Common_Tab
            // 
            this.Common_Tab.Controls.Add(this.groupBox1);
            this.Common_Tab.Controls.Add(this.Options_PanelLocations_checkBox);
            this.Common_Tab.Controls.Add(this.LanguageBox);
            this.Common_Tab.Location = new System.Drawing.Point(4, 22);
            this.Common_Tab.Name = "Common_Tab";
            this.Common_Tab.Size = new System.Drawing.Size(648, 320);
            this.Common_Tab.TabIndex = 0;
            this.Common_Tab.Text = "Common Settings";
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
            this.TranslateLabel.Text = "If you like to translate this Software into another Language, you are very welcom" +
                "e!";
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
            this.BeamBox_tab.Location = new System.Drawing.Point(4, 22);
            this.BeamBox_tab.Name = "BeamBox_tab";
            this.BeamBox_tab.Size = new System.Drawing.Size(648, 320);
            this.BeamBox_tab.TabIndex = 2;
            this.BeamBox_tab.Text = "Projector Window";
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
            this.SizePosControl.Click += new System.EventHandler(this.SizePosControl_Click);
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
            // Bible_Tab
            // 
            this.Bible_Tab.Controls.Add(this.songThemeWidget);
            this.Bible_Tab.Controls.Add(this.SwordLangGroupBox);
            this.Bible_Tab.Controls.Add(this.SwordFolderGroupBox);
            this.Bible_Tab.Location = new System.Drawing.Point(4, 22);
            this.Bible_Tab.Name = "Bible_Tab";
            this.Bible_Tab.Size = new System.Drawing.Size(648, 320);
            this.Bible_Tab.TabIndex = 3;
            this.Bible_Tab.Text = "Bible";
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
            this.BibleCache_Tab.Controls.Add(this.BibleCache_Message);
            this.BibleCache_Tab.Controls.Add(this.BibleCache_Replacements_Label);
            this.BibleCache_Tab.Controls.Add(this.BibleCache_Available_Label);
            this.BibleCache_Tab.Controls.Add(this.label3);
            this.BibleCache_Tab.Controls.Add(this.BibleConversions_dataGrid);
            this.BibleCache_Tab.Controls.Add(this.BibleCache_progressBar);
            this.BibleCache_Tab.Controls.Add(this.BiblesAvailable_listEx);
            this.BibleCache_Tab.Controls.Add(this.BiblesCached_listEx);
            this.BibleCache_Tab.Location = new System.Drawing.Point(4, 22);
            this.BibleCache_Tab.Name = "BibleCache_Tab";
            this.BibleCache_Tab.Size = new System.Drawing.Size(648, 320);
            this.BibleCache_Tab.TabIndex = 4;
            this.BibleCache_Tab.Text = "Bible Cache";
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
            // BibleCache_Replacements_Label
            // 
            this.BibleCache_Replacements_Label.BackColor = System.Drawing.Color.Transparent;
            this.BibleCache_Replacements_Label.Location = new System.Drawing.Point(268, 6);
            this.BibleCache_Replacements_Label.Name = "BibleCache_Replacements_Label";
            this.BibleCache_Replacements_Label.Size = new System.Drawing.Size(176, 14);
            this.BibleCache_Replacements_Label.TabIndex = 9;
            this.BibleCache_Replacements_Label.Text = "Replacements when searching";
            // 
            // BibleCache_Available_Label
            // 
            this.BibleCache_Available_Label.BackColor = System.Drawing.Color.Transparent;
            this.BibleCache_Available_Label.Location = new System.Drawing.Point(136, 6);
            this.BibleCache_Available_Label.Name = "BibleCache_Available_Label";
            this.BibleCache_Available_Label.Size = new System.Drawing.Size(110, 14);
            this.BibleCache_Available_Label.TabIndex = 8;
            this.BibleCache_Available_Label.Text = "Not yet cached";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(8, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 14);
            this.label3.TabIndex = 7;
            this.label3.Text = "Cached translations";
            // 
            // BibleConversions_dataGrid
            // 
            this.BibleConversions_dataGrid.AllowSorting = false;
            this.BibleConversions_dataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.BibleConversions_dataGrid.CaptionVisible = false;
            this.BibleConversions_dataGrid.DataMember = "";
            this.BibleConversions_dataGrid.DataSource = this.Options_RegEx_Table;
            this.BibleConversions_dataGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.BibleConversions_dataGrid.Location = new System.Drawing.Point(266, 24);
            this.BibleConversions_dataGrid.Name = "BibleConversions_dataGrid";
            this.BibleConversions_dataGrid.RowHeadersVisible = false;
            this.BibleConversions_dataGrid.Size = new System.Drawing.Size(376, 290);
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
            this.BibleCache_progressBar.Maximum = 35000;
            this.BibleCache_progressBar.Name = "BibleCache_progressBar";
            this.BibleCache_progressBar.Size = new System.Drawing.Size(242, 23);
            this.BibleCache_progressBar.TabIndex = 2;
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
            this.Graphics_tab.Controls.Add(this.Grafics_Outline);
            this.Graphics_tab.Location = new System.Drawing.Point(4, 22);
            this.Graphics_tab.Name = "Graphics_tab";
            this.Graphics_tab.Size = new System.Drawing.Size(648, 320);
            this.Graphics_tab.TabIndex = 1;
            this.Graphics_tab.Text = "Graphics";
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
            50,
            0,
            0,
            0});
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
            // Grafics_Outline
            // 
            this.Grafics_Outline.Controls.Add(this.OutlineSize_UpDown1);
            this.Grafics_Outline.Location = new System.Drawing.Point(8, 72);
            this.Grafics_Outline.Name = "Grafics_Outline";
            this.Grafics_Outline.Size = new System.Drawing.Size(408, 64);
            this.Grafics_Outline.TabIndex = 0;
            this.Grafics_Outline.TabStop = false;
            this.Grafics_Outline.Text = "Outline Size";
            this.Grafics_Outline.Visible = false;
            // 
            // OutlineSize_UpDown1
            // 
            this.OutlineSize_UpDown1.Location = new System.Drawing.Point(16, 24);
            this.OutlineSize_UpDown1.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.OutlineSize_UpDown1.Name = "OutlineSize_UpDown1";
            this.OutlineSize_UpDown1.Size = new System.Drawing.Size(104, 20);
            this.OutlineSize_UpDown1.TabIndex = 0;
            this.OutlineSize_UpDown1.Tag = "";
            // 
            // SongFormat_Tab
            // 
            this.SongFormat_Tab.Controls.Add(this.OpenSongFormatButton);
            this.SongFormat_Tab.Controls.Add(this.SaveSongFormatButton);
            this.SongFormat_Tab.Controls.Add(this.tabControl2);
            this.SongFormat_Tab.Controls.Add(this.groupBox3);
            this.SongFormat_Tab.Location = new System.Drawing.Point(4, 22);
            this.SongFormat_Tab.Name = "SongFormat_Tab";
            this.SongFormat_Tab.Size = new System.Drawing.Size(648, 320);
            this.SongFormat_Tab.TabIndex = 6;
            this.SongFormat_Tab.Text = "Song Format";
            // 
            // OpenSongFormatButton
            // 
            this.OpenSongFormatButton.Location = new System.Drawing.Point(425, 95);
            this.OpenSongFormatButton.Name = "OpenSongFormatButton";
            this.OpenSongFormatButton.Size = new System.Drawing.Size(75, 23);
            this.OpenSongFormatButton.TabIndex = 3;
            this.OpenSongFormatButton.Text = "Open...";
            this.OpenSongFormatButton.UseVisualStyleBackColor = true;
            this.OpenSongFormatButton.Click += new System.EventHandler(this.OpenSongFormatButton_Click);
            // 
            // SaveSongFormatButton
            // 
            this.SaveSongFormatButton.Location = new System.Drawing.Point(425, 66);
            this.SaveSongFormatButton.Name = "SaveSongFormatButton";
            this.SaveSongFormatButton.Size = new System.Drawing.Size(75, 23);
            this.SaveSongFormatButton.TabIndex = 2;
            this.SaveSongFormatButton.Text = "Save As...";
            this.SaveSongFormatButton.UseVisualStyleBackColor = true;
            this.SaveSongFormatButton.Click += new System.EventHandler(this.SaveSongFormatButton_Click);
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.SongFormat_Title_Tab);
            this.tabControl2.Controls.Add(this.SongFormat_Verse_Tab);
            this.tabControl2.Controls.Add(this.SongFormat_Author_Tab);
            this.tabControl2.Location = new System.Drawing.Point(10, 66);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(400, 233);
            this.tabControl2.TabIndex = 1;
            // 
            // SongFormat_Title_Tab
            // 
            this.SongFormat_Title_Tab.Controls.Add(this.SongTitle_TextFormat);
            this.SongFormat_Title_Tab.Location = new System.Drawing.Point(4, 22);
            this.SongFormat_Title_Tab.Name = "SongFormat_Title_Tab";
            this.SongFormat_Title_Tab.Size = new System.Drawing.Size(392, 207);
            this.SongFormat_Title_Tab.TabIndex = 0;
            this.SongFormat_Title_Tab.Text = "Title (header)";
            // 
            // SongTitle_TextFormat
            // 
            this.SongTitle_TextFormat.Dock = System.Windows.Forms.DockStyle.Fill;
            //this.SongTitle_TextFormat.Format = ((DreamBeam.BeamTextFormat)(resources.GetObject("SongTitle_TextFormat.Format")));
            this.SongTitle_TextFormat.Location = new System.Drawing.Point(0, 0);
            this.SongTitle_TextFormat.Name = "SongTitle_TextFormat";
            this.SongTitle_TextFormat.Size = new System.Drawing.Size(392, 207);
            this.SongTitle_TextFormat.TabIndex = 0;
            // 
            // SongFormat_Verse_Tab
            // 
            this.SongFormat_Verse_Tab.Controls.Add(this.SongVerse_TextFormat);
            this.SongFormat_Verse_Tab.Location = new System.Drawing.Point(4, 22);
            this.SongFormat_Verse_Tab.Name = "SongFormat_Verse_Tab";
            this.SongFormat_Verse_Tab.Size = new System.Drawing.Size(392, 207);
            this.SongFormat_Verse_Tab.TabIndex = 1;
            this.SongFormat_Verse_Tab.Text = "Verse";
            // 
            // SongVerse_TextFormat
            // 
            this.SongVerse_TextFormat.Dock = System.Windows.Forms.DockStyle.Fill;
            //this.SongVerse_TextFormat.Format = ((DreamBeam.BeamTextFormat)(resources.GetObject("SongVerse_TextFormat.Format")));
            this.SongVerse_TextFormat.Location = new System.Drawing.Point(0, 0);
            this.SongVerse_TextFormat.Name = "SongVerse_TextFormat";
            this.SongVerse_TextFormat.Size = new System.Drawing.Size(392, 207);
            this.SongVerse_TextFormat.TabIndex = 0;
            // 
            // SongFormat_Author_Tab
            // 
            this.SongFormat_Author_Tab.Controls.Add(this.SongAuthor_TextFormat);
            this.SongFormat_Author_Tab.Location = new System.Drawing.Point(4, 22);
            this.SongFormat_Author_Tab.Name = "SongFormat_Author_Tab";
            this.SongFormat_Author_Tab.Size = new System.Drawing.Size(392, 207);
            this.SongFormat_Author_Tab.TabIndex = 2;
            this.SongFormat_Author_Tab.Text = "Author (footer)";
            // 
            // SongAuthor_TextFormat
            // 
            this.SongAuthor_TextFormat.Dock = System.Windows.Forms.DockStyle.Fill;
            //this.SongAuthor_TextFormat.Format = ((DreamBeam.BeamTextFormat)(resources.GetObject("SongAuthor_TextFormat.Format")));
            this.SongAuthor_TextFormat.Location = new System.Drawing.Point(0, 0);
            this.SongAuthor_TextFormat.Name = "SongAuthor_TextFormat";
            this.SongAuthor_TextFormat.Size = new System.Drawing.Size(392, 207);
            this.SongAuthor_TextFormat.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.Transparent;
            this.groupBox3.Controls.Add(this.SongBGImageBrowse);
            this.groupBox3.Controls.Add(this.SongBGImagePath);
            this.groupBox3.Location = new System.Drawing.Point(10, 10);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(400, 48);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Default Background Image";
            // 
            // SongBGImageBrowse
            // 
            this.SongBGImageBrowse.Location = new System.Drawing.Point(318, 20);
            this.SongBGImageBrowse.Name = "SongBGImageBrowse";
            this.SongBGImageBrowse.Size = new System.Drawing.Size(75, 23);
            this.SongBGImageBrowse.TabIndex = 1;
            this.SongBGImageBrowse.Text = "Browse...";
            this.SongBGImageBrowse.Click += new System.EventHandler(this.SongBGImageBrowse_Click);
            // 
            // SongBGImagePath
            // 
            this.SongBGImagePath.Location = new System.Drawing.Point(10, 20);
            this.SongBGImagePath.Name = "SongBGImagePath";
            this.SongBGImagePath.Size = new System.Drawing.Size(300, 20);
            this.SongBGImagePath.TabIndex = 0;
            // 
            // BibleFormat_Tab
            // 
            this.BibleFormat_Tab.Controls.Add(this.groupBox2);
            this.BibleFormat_Tab.Controls.Add(this.tabControl1);
            this.BibleFormat_Tab.Location = new System.Drawing.Point(4, 22);
            this.BibleFormat_Tab.Name = "BibleFormat_Tab";
            this.BibleFormat_Tab.Size = new System.Drawing.Size(648, 320);
            this.BibleFormat_Tab.TabIndex = 5;
            this.BibleFormat_Tab.Text = "Bible Format";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.BibleBGImageBrowse);
            this.groupBox2.Controls.Add(this.BibleBGImagePath);
            this.groupBox2.Location = new System.Drawing.Point(10, 10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(400, 48);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Default Background Image";
            // 
            // BibleBGImageBrowse
            // 
            this.BibleBGImageBrowse.Location = new System.Drawing.Point(318, 20);
            this.BibleBGImageBrowse.Name = "BibleBGImageBrowse";
            this.BibleBGImageBrowse.Size = new System.Drawing.Size(75, 23);
            this.BibleBGImageBrowse.TabIndex = 1;
            this.BibleBGImageBrowse.Text = "Browse...";
            this.BibleBGImageBrowse.Click += new System.EventHandler(this.BibleBGImageBrowse_Click);
            // 
            // BibleBGImagePath
            // 
            this.BibleBGImagePath.Location = new System.Drawing.Point(10, 20);
            this.BibleBGImagePath.Name = "BibleBGImagePath";
            this.BibleBGImagePath.Size = new System.Drawing.Size(300, 20);
            this.BibleBGImagePath.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.ItemSize = new System.Drawing.Size(62, 18);
            this.tabControl1.Location = new System.Drawing.Point(10, 66);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(400, 233);
            this.tabControl1.TabIndex = 11;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.BibleRef_TextFormat);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(392, 207);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "Reference";
            // 
            // BibleRef_TextFormat
            // 
            this.BibleRef_TextFormat.Dock = System.Windows.Forms.DockStyle.Fill;
            //this.BibleRef_TextFormat.Format = ((DreamBeam.BeamTextFormat)(resources.GetObject("BibleRef_TextFormat.Format")));
            this.BibleRef_TextFormat.Location = new System.Drawing.Point(0, 0);
            this.BibleRef_TextFormat.Name = "BibleRef_TextFormat";
            this.BibleRef_TextFormat.Size = new System.Drawing.Size(392, 207);
            this.BibleRef_TextFormat.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.BibleVerse_TextFormat);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(392, 207);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "Verse";
            // 
            // BibleVerse_TextFormat
            // 
            this.BibleVerse_TextFormat.Dock = System.Windows.Forms.DockStyle.Fill;
            //this.BibleVerse_TextFormat.Format = ((DreamBeam.BeamTextFormat)(resources.GetObject("BibleVerse_TextFormat.Format")));
            this.BibleVerse_TextFormat.Location = new System.Drawing.Point(0, 0);
            this.BibleVerse_TextFormat.Name = "BibleVerse_TextFormat";
            this.BibleVerse_TextFormat.Size = new System.Drawing.Size(392, 207);
            this.BibleVerse_TextFormat.TabIndex = 0;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.BibleTransl_TextFormat);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(392, 207);
            this.tabPage5.TabIndex = 2;
            this.tabPage5.Text = "Translation";
            // 
            // BibleTransl_TextFormat
            // 
            this.BibleTransl_TextFormat.Dock = System.Windows.Forms.DockStyle.Fill;
            //this.BibleTransl_TextFormat.Format = ((DreamBeam.BeamTextFormat)(resources.GetObject("BibleTransl_TextFormat.Format")));
            this.BibleTransl_TextFormat.Location = new System.Drawing.Point(0, 0);
            this.BibleTransl_TextFormat.Name = "BibleTransl_TextFormat";
            this.BibleTransl_TextFormat.Size = new System.Drawing.Size(392, 207);
            this.BibleTransl_TextFormat.TabIndex = 0;
            // 
            // TextToolFormat_Tab
            // 
            this.TextToolFormat_Tab.Controls.Add(this.groupBox4);
            this.TextToolFormat_Tab.Controls.Add(this.tabControl3);
            this.TextToolFormat_Tab.Location = new System.Drawing.Point(4, 22);
            this.TextToolFormat_Tab.Name = "TextToolFormat_Tab";
            this.TextToolFormat_Tab.Size = new System.Drawing.Size(648, 320);
            this.TextToolFormat_Tab.TabIndex = 7;
            this.TextToolFormat_Tab.Text = "Sermon Tool Format";
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.Transparent;
            this.groupBox4.Controls.Add(this.TextBGImageBrowse);
            this.groupBox4.Controls.Add(this.TextBGImagePath);
            this.groupBox4.Location = new System.Drawing.Point(10, 10);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(400, 48);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Default Background Image";
            // 
            // TextBGImageBrowse
            // 
            this.TextBGImageBrowse.Location = new System.Drawing.Point(318, 20);
            this.TextBGImageBrowse.Name = "TextBGImageBrowse";
            this.TextBGImageBrowse.Size = new System.Drawing.Size(75, 23);
            this.TextBGImageBrowse.TabIndex = 1;
            this.TextBGImageBrowse.Text = "Browse...";
            this.TextBGImageBrowse.Click += new System.EventHandler(this.TextBGImageBrowse_Click);
            // 
            // TextBGImagePath
            // 
            this.TextBGImagePath.Location = new System.Drawing.Point(10, 20);
            this.TextBGImagePath.Name = "TextBGImagePath";
            this.TextBGImagePath.Size = new System.Drawing.Size(300, 20);
            this.TextBGImagePath.TabIndex = 0;
            // 
            // tabControl3
            // 
            this.tabControl3.Controls.Add(this.tabPage6);
            this.tabControl3.Controls.Add(this.tabPage7);
            this.tabControl3.Location = new System.Drawing.Point(10, 66);
            this.tabControl3.Name = "tabControl3";
            this.tabControl3.SelectedIndex = 0;
            this.tabControl3.Size = new System.Drawing.Size(400, 233);
            this.tabControl3.TabIndex = 0;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.TextTool_1stLine_TextFormat);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(392, 207);
            this.tabPage6.TabIndex = 0;
            this.tabPage6.Text = "1st Line";
            // 
            // TextTool_1stLine_TextFormat
            // 
            this.TextTool_1stLine_TextFormat.Dock = System.Windows.Forms.DockStyle.Fill;
            //this.TextTool_1stLine_TextFormat.Format = ((DreamBeam.BeamTextFormat)(resources.GetObject("TextTool_1stLine_TextFormat.Format")));
            this.TextTool_1stLine_TextFormat.Location = new System.Drawing.Point(0, 0);
            this.TextTool_1stLine_TextFormat.Name = "TextTool_1stLine_TextFormat";
            this.TextTool_1stLine_TextFormat.Size = new System.Drawing.Size(392, 207);
            this.TextTool_1stLine_TextFormat.TabIndex = 0;
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.TextTool_OtherLines_TextFormat);
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Size = new System.Drawing.Size(392, 207);
            this.tabPage7.TabIndex = 1;
            this.tabPage7.Text = "Other Lines";
            // 
            // TextTool_OtherLines_TextFormat
            // 
            this.TextTool_OtherLines_TextFormat.Dock = System.Windows.Forms.DockStyle.Fill;
            //this.TextTool_OtherLines_TextFormat.Format = ((DreamBeam.BeamTextFormat)(resources.GetObject("TextTool_OtherLines_TextFormat.Format")));
            this.TextTool_OtherLines_TextFormat.Location = new System.Drawing.Point(0, 0);
            this.TextTool_OtherLines_TextFormat.Name = "TextTool_OtherLines_TextFormat";
            this.TextTool_OtherLines_TextFormat.Size = new System.Drawing.Size(392, 207);
            this.TextTool_OtherLines_TextFormat.TabIndex = 0;
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
            this.Options_Okaybtn.Click += new System.EventHandler(this.Options_Okaybtn_Click);
            // 
            // Options_DataSet
            // 
            this.Options_DataSet.DataSetName = "Options_DataSet";
            this.Options_DataSet.Locale = new System.Globalization.CultureInfo("en-US");
            this.Options_DataSet.Tables.AddRange(new System.Data.DataTable[] {
            this.Options_RegEx_Table});
            // 
            // songThemeWidget
            // 
            this.songThemeWidget.Location = new System.Drawing.Point(24, 70);
            this.songThemeWidget.Name = "songThemeWidget";
            this.songThemeWidget.Size = new System.Drawing.Size(619, 312);
            this.songThemeWidget.TabIndex = 9;
            // 
            // Options
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(656, 384);
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
            this.Bible_Tab.ResumeLayout(false);
            this.SwordLangGroupBox.ResumeLayout(false);
            this.SwordFolderGroupBox.ResumeLayout(false);
            this.SwordFolderGroupBox.PerformLayout();
            this.BibleCache_Tab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BibleConversions_dataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Options_RegEx_Table)).EndInit();
            this.Graphics_tab.ResumeLayout(false);
            this.PreRenderBox.ResumeLayout(false);
            this.Alpha_groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Speed_Updown)).EndInit();
            this.Grafics_Outline.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.OutlineSize_UpDown1)).EndInit();
            this.SongFormat_Tab.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.SongFormat_Title_Tab.ResumeLayout(false);
            this.SongFormat_Verse_Tab.ResumeLayout(false);
            this.SongFormat_Author_Tab.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.BibleFormat_Tab.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.TextToolFormat_Tab.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tabControl3.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.tabPage7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Options_DataSet)).EndInit();
            this.ResumeLayout(false);

		}
#endregion

		private void Options_Okaybtn_Click(object sender, System.EventArgs e) {
			this.Conf.Alphablending = this.Alpha_CheckBox.Checked;
			this.Conf.PreRender = this.PreRendercheckBox.Checked;
			this.Conf.BlendSpeed = (int)Speed_Updown.Value;
			this.Conf.useDirect3D = this.Direct3D_CheckBox.Checked;
			this.Conf.OutlineSize = (float)this.OutlineSize_UpDown1.Value;


			if (Conf.BeamBoxAutoPosSize && ScreenList.SelectedIndex >= 0) {
				this.Conf.BeamBoxScreenNum = ScreenList.SelectedIndex;
				Rectangle r = System.Windows.Forms.Screen.AllScreens[ScreenList.SelectedIndex].Bounds;

				this.Conf.BeamBoxPosX = r.X;
				this.Conf.BeamBoxPosY = r.Y;
				this.Conf.BeamBoxSizeX = r.Width;
				this.Conf.BeamBoxSizeY = r.Height;				
			} else {
				this.Conf.BeamBoxPosX = (int)this.BeamBox_posX.Value;
				this.Conf.BeamBoxPosY = (int)this.BeamBox_posY.Value;
				this.Conf.BeamBoxSizeX = (int)this.BeamBox_Width.Value;
				this.Conf.BeamBoxSizeY = (int)this.BeamBox_Height.Value;
			}

			if (this._MainForm.DisplayLiveLocal != null) {
				this._MainForm.DisplayLiveLocal.ChangeDisplayCoord(this.Conf);
			}

			this.Conf.SwordPath = this.Sword_PathBox.Text;
			this._MainForm.Check_SwordProject();

			if (! Tools.StringIsNullOrEmpty( this.Sword_LanguageBox.Text ) ) {
				this.Conf.BibleLang = this.Sword_LanguageBox.Text;
				SetBibleLocale(this._MainForm.bibles, this.Conf.SwordPath, this.Conf.BibleLang);
			}
			
			this.Conf.HideMouse = this.BeamBox_HideMouse.Checked;
			this.Conf.AlwaysOnTop = this.BeamBox_AlwaysOnTop.Checked;

			switch( LanguageList.SelectedIndex) {
			case 0:
				Conf.Language = "en";
				break;
			case 1:
				Conf.Language = "de";
				break;
			}

			Conf.RememberPanelLocations = this.Options_PanelLocations_checkBox.Checked;
			Conf.BibleBGImagePath = this.BibleBGImagePath.Text;
			Conf.SongBGImagePath = this.SongBGImagePath.Text;
			Conf.TextBGImagePath = this.TextBGImagePath.Text;
			
			// It is possible we deserialized the Conf object and there was no BibleTextFormat array.
			// This would normally happen when someone edited the file by hand and removed sections.
			try {
				Conf.BibleTextFormat[ (int)BibleTextType.Reference ] = this.BibleRef_TextFormat.Format;
				Conf.BibleTextFormat[ (int)BibleTextType.Verse ] = this.BibleVerse_TextFormat.Format;
				Conf.BibleTextFormat[ (int)BibleTextType.Translation ] = this.BibleTransl_TextFormat.Format;

				Conf.SongTextFormat[ (int)SongTextType.Title ] = this.SongTitle_TextFormat.Format;
				Conf.SongTextFormat[ (int)SongTextType.Author ] = this.SongAuthor_TextFormat.Format;
				Conf.SongTextFormat[ (int)SongTextType.Verse ] = this.SongVerse_TextFormat.Format;

				Conf.SermonTextFormat[ (int)TextToolType.FirstLine ] = this.TextTool_1stLine_TextFormat.Format;
				Conf.SermonTextFormat[ (int)TextToolType.OtherLines ] = this.TextTool_OtherLines_TextFormat.Format;
			} catch {
				// We probably got a null exception, or index out of range. Re-initialize.
				Conf.Init();
			}

			#region Update Preview display chain with the new background image
			IContentOperations content = _MainForm.DisplayPreview.content;
			if (content != null) {
				// First, let's clear the pre-render cache
				try {
					(content as Content).RenderedFramesClear();
				} catch {}

				switch ((ContentType)content.GetIdentity().Type) {
					case ContentType.Song:
						content.ChangeBGImagePath(Conf.SongBGImagePath);
						break;
					case ContentType.PlainText:
						content.ChangeBGImagePath(Conf.TextBGImagePath);
						break;
					case ContentType.BibleVerseIdx:
					case ContentType.BibleVerseRef:
                    case ContentType.BibleVerse:
						content.ChangeBGImagePath(Conf.BibleBGImagePath);
						break;
				}
				_MainForm.DisplayPreview.UpdateDisplay(true);
			}
			#endregion

			Conf.ServerAddress = this.ServerAddress.Text;
			Conf.ListeningPort = (int)this.ListeningPort.Value;
			OperatingMode oldMode = Conf.AppOperatingMode;
			if (this.OperatingMode_StandAlone.Checked) {
				Conf.AppOperatingMode = OperatingMode.StandAlone;
			} else if (this.OperatingMode_Client.Checked) {
				Conf.AppOperatingMode = OperatingMode.Client;
			} else if (this.OperatingMode_Server.Checked) {
				Conf.AppOperatingMode = OperatingMode.Server;
			}

			if (oldMode != Conf.AppOperatingMode) {
				_MainForm.InitDisplays();
			}

			Conf.Options_DataSet = this.Options_DataSet;
			Directory.CreateDirectory( Tools.GetCommonAppDataPath() );
			this.Options_DataSet.WriteXml( Path.Combine(Tools.GetCommonAppDataPath(), _MainForm.ConfigSet + ".dataset.config.xml"), XmlWriteMode.WriteSchema );

			Config.SerializeTo(this.Conf, _MainForm.ConfigSet + ".config.xml");
			this.Close();
		}


		private void Select_Sword_Click(object sender, System.EventArgs e) {
			this.openFileDialog1.FileName = Path.Combine(this.Sword_PathBox.Text, "sword.exe");
			this.openFileDialog1.Filter = "sword.exe|sword.exe";
			if (this.openFileDialog1.ShowDialog() != DialogResult.Cancel) {
				this.Sword_PathBox.Text = this.openFileDialog1.FileName.Substring(0,this.openFileDialog1.FileName.Length-9);
				MessageBox.Show("To use the Bible, you have to restart DreamBeam.");
			}
		}

		private void Options_Load(object sender, System.EventArgs e) {
			this.PopulateControls();
		}

		private void PopulateControls() {
			this.Conf = _MainForm.Config;
			this.SetLanguage();
			this.Sword_PathBox.Text = this.Conf.SwordPath;
			this.Sword_LanguageBox.SelectedItem = this.Conf.BibleLang;
			this.OutlineSize_UpDown1.Value = (int)this.Conf.OutlineSize;
			this.BeamBox_posX.Value = (int)this.Conf.BeamBoxPosX;
			this.BeamBox_posY.Value = (int)this.Conf.BeamBoxPosY;
			this.BeamBox_Width.Value = (int)this.Conf.BeamBoxSizeX;
			this.BeamBox_Height.Value = (int)this.Conf.BeamBoxSizeY;
			this.BeamBox_HideMouse.Checked = this.Conf.HideMouse;
			this.Alpha_CheckBox.Checked = this.Conf.Alphablending;
			this.PreRendercheckBox.Checked = this.Conf.PreRender;
			this.Direct3D_CheckBox.Checked = this.Conf.useDirect3D;
			this.BeamBox_AlwaysOnTop.Checked = this.Conf.AlwaysOnTop;
			Speed_Updown.Value = this.Conf.BlendSpeed;

			switch(Conf.Language.Substring(0,2)) {
			case "en":
				LanguageList.SelectedIndex = 0;
				break;
			case "de":
				LanguageList.SelectedIndex = 1;
				break;
			}

			if(this.Conf.BeamBoxAutoPosSize) {
				this.SizePosControl.SelectedIndex = 0;
			} else {
				this.SizePosControl.SelectedIndex = 1;
			}
			GetScreens();


            if(ScreenList.Items.Count > this.Conf.BeamBoxScreenNum) {
                ScreenList.SelectedIndex = this.Conf.BeamBoxScreenNum;
            }

			this.Options_PanelLocations_checkBox.Checked = this.Conf.RememberPanelLocations;
			this.BibleBGImagePath.Text = this.Conf.BibleBGImagePath;
			this.SongBGImagePath.Text = this.Conf.SongBGImagePath;
			this.TextBGImagePath.Text = this.Conf.TextBGImagePath;

			if (this.Conf.BibleTextFormat != null & this.Conf.BibleTextFormat.Length == Enum.GetValues(typeof(BibleTextType)).Length) {
				this.BibleRef_TextFormat.Format = Conf.BibleTextFormat[ (int)BibleTextType.Reference ];
				this.BibleVerse_TextFormat.Format = Conf.BibleTextFormat[ (int)BibleTextType.Verse ];
				this.BibleTransl_TextFormat.Format = Conf.BibleTextFormat[ (int)BibleTextType.Translation ];
			}
			if (this.Conf.SongTextFormat != null & this.Conf.SongTextFormat.Length == Enum.GetValues(typeof(SongTextType)).Length) {
				this.SongTitle_TextFormat.Format = Conf.SongTextFormat[ (int)SongTextType.Title ];
				this.SongVerse_TextFormat.Format = Conf.SongTextFormat[ (int)SongTextType.Verse ];
				this.SongAuthor_TextFormat.Format = Conf.SongTextFormat[ (int)SongTextType.Author ];
			}
			if (this.Conf.SermonTextFormat != null & this.Conf.SermonTextFormat.Length == Enum.GetValues(typeof(TextToolType)).Length) {
				this.TextTool_1stLine_TextFormat.Format = Conf.SermonTextFormat[ (int)TextToolType.FirstLine ];
				this.TextTool_OtherLines_TextFormat.Format = Conf.SermonTextFormat[ (int)TextToolType.OtherLines ];
			}

			this.ServerAddress.Text = this.Conf.ServerAddress;
			this.ListeningPort.Value = this.Conf.ListeningPort;
			switch( this.Conf.AppOperatingMode ) {
				case OperatingMode.StandAlone:
					this.OperatingMode_StandAlone.Checked = true;
					break;
				case OperatingMode.Client:
					this.OperatingMode_Client.Checked = true;
					break;
				case OperatingMode.Server:
					this.OperatingMode_Server.Checked = true;
					break;
			}

			if ( !Tools.StringIsNullOrEmpty(this.Sword_LanguageBox.Text) ) {
				SetBibleLocale(this._MainForm.bibles, this.Conf.SwordPath, this.Sword_LanguageBox.Text);
			}

            this.songThemeWidget.setTabs(new string[] {"Title", "Verse", "Author"});
            SongTheme theme = new SongTheme();
            theme.BGImagePath = this.SongBGImagePath.Text;
            theme.TextFormat[(int)SongTextType.Title] = this.SongTitle_TextFormat.Format;
            theme.TextFormat[(int)SongTextType.Author] = this.SongAuthor_TextFormat.Format;
            theme.TextFormat[(int)SongTextType.Verse] = this.SongVerse_TextFormat.Format;
            this.songThemeWidget.Theme = theme;
        }

        private void Options_Cancelbtn_Click(object sender, System.EventArgs e) {
			// Restore original settings in case the user made changes
			this.PopulateControls();
			this.Close();
        }

        private void BeamBox_ColorButton_Click(object sender, System.EventArgs e) {
            this.colorDialog1.Color = this.Conf.BackgroundColor;
			if (this.colorDialog1.ShowDialog() == DialogResult.OK) {
				this.Conf.BackgroundColor = this.colorDialog1.Color;
			}
			if (this.Conf.BackgroundColor.IsEmpty) this.Conf.BackgroundColor = Color.Black;
        }

		#region SizePosition

        public void GetScreens() {
            this.ScreenList.Items.Clear();

            // Find second monitor
            int i = 0;
            int FirstSecundary = -1;
            foreach(System.Windows.Forms.Screen s in System.Windows.Forms.Screen.AllScreens) {
                if (s == System.Windows.Forms.Screen.PrimaryScreen) {
                    this.ScreenList.Items.Add ("Primary Screen");
                    // secondary = s;
                } else {
					this.ScreenList.Items.Add ("Secondary Screen " + (i + 1).ToString());
					i++;
                    if (FirstSecundary == -1) {
                        FirstSecundary = i;
                    }
                }
                //if no Secundary Found take the Primary (in this case, the only one found)
				if (FirstSecundary == -1) {
                    FirstSecundary = 0;
                }

				if (Conf.BeamBoxScreenNum < 0) { Conf.BeamBoxScreenNum = 0; }

                if (Conf.BeamBoxAutoPosSize) {
                    if (Conf.BeamBoxScreenNum < ScreenList.Items.Count) {
                        ScreenList.SelectedIndex = Conf.BeamBoxScreenNum;
                    }
                } else {
                    ScreenList.SelectedIndex = FirstSecundary - 1;
                }

            }
        }


        private void ScreenList_SelectedIndexChanged(object sender, System.EventArgs e) {
            if(ScreenList.SelectedIndex >=  0) {
				Rectangle r = System.Windows.Forms.Screen.AllScreens[ScreenList.SelectedIndex].Bounds;
                AutoPosLabelX.Text = "X: " + r.X.ToString();
                AutoPosLabelY.Text = " Y: " + r.Y.ToString();
                AutoSizeLabelW.Text = "Width: " + r.Width.ToString();
                AutoSizeLabelH.Text = "Height: " + r.Height.ToString();
            }
        }

        private void SizePosControl_Click(object sender, System.EventArgs e) {
            if (SizePosControl.SelectedIndex == 0) {
                Conf.BeamBoxAutoPosSize = true;
            } else {
                Conf.BeamBoxAutoPosSize = false;
            }
        }


		#endregion

		#region Language
		void SetLanguage(){

			Lang.setCulture(Conf.Language);
			#region Tabs
			Common_Tab.Text = Lang.say("Options.Tabs.Common");
			Graphics_tab.Text = Lang.say("Options.Tabs.Graphics");
			BeamBox_tab.Text = Lang.say("Options.Tabs.BeamBox");
			Bible_Tab.Text = Lang.say("Options.Tabs.Bible");
			#endregion

			#region Common
			LanguageBox.Text = Lang.say("Options.Common.Language");
			TranslateLabel.Text = Lang.say("Options.Common.Translate");
			#endregion

			#region Graphics
			 Alpha_groupBox1.Text = Lang.say("Options.Graphics.AlphaBlending");
			 Alpha_CheckBox.Text = Lang.say("Options.Graphics.AlphaCheckBox");
			 Grafics_Outline.Text = Lang.say("Options.Graphics.Outline");
			 SpeedLabel.Text = Lang.say("Options.Graphics.AlphaSpeed");

			 PreRenderBox.Text = Lang.say("Options.Graphics.PreRenderTitle");
			 PreRendercheckBox.Text = Lang.say("Options.Graphics.PreRender");
			 PreRenderLabel.Text = Lang.say("Options.Graphics.PreRenderDescription");
			#endregion

			#region ProjectorWindow
			 Position_Title_GroupBox.Text = Lang.say("Options.BeamBox.PositionTitle");

			 BeamBox_PosBox.Text = Lang.say("Options.BeamBox.PositionGroup");
			 Position_groupBox4.Text = Lang.say("Options.BeamBox.PositionGroup");

			 BeamBox_SizeBox.Text = Lang.say("Options.BeamBox.SizeGroup");
			 Size_groupBox5.Text = Lang.say("Options.BeamBox.SizeGroup");

			 Mouse_groupBox2.Text = Lang.say("Options.BeamBox.MouseGroup");
			 BeamBox_HideMouse.Text = Lang.say("Options.BeamBox.HideMouseCheckBox");
			 BeamBox_AlwaysOnTop.Text = Lang.say("Options.BeamBox.OnTopCheckBox");
			 Background_groupBox3.Text = Lang.say("Options.BeamBox.BackgroundGroup");
			 BeamBox_ColorButton.Text = Lang.say("Options.BeamBox.BGColorButton");
			 tabPage1.Text = Lang.say("Options.BeamBox.tabpage1");
			 tabPage2.Text = Lang.say("Options.BeamBox.tabpage2");
			 WidthLabel.Text = Lang.say("Options.BeamBox.Width");
			 HeightLabel.Text = Lang.say("Options.BeamBox.Height");
			#endregion

			#region Bible
			SwordFolderGroupBox.Text = Lang.say("Options.Bible.SwordFolderGroup");
			Select_Sword.Text = Lang.say("Options.Bible.SwordFolderButton");
			SwordLangGroupBox.Text = Lang.say("Options.Bible.SwordLanguage");
			#endregion

			#region Buttons
			Options_Okaybtn.Text = Lang.say("Options.Okay");
			Options_Cancelbtn.Text = Lang.say("Options.Cancel");
			#endregion
		}
		#endregion

		#region BibleCache tab

		public void PopulateBibleCacheTab() {
			Extensions.Set available = new Extensions.Set();
			Extensions.Set cached = new Extensions.Set();

			BiblesAvailable_listEx.Items.Clear();
			BiblesCached_listEx.Items.Clear();

			foreach (string book in _MainForm.bibles.Translations()) {
				cached.Add(book);
				BiblesCached_listEx.Add(book, 1);
			}

			foreach (string book in _MainForm.DiathekeBooks(true)) {
				available.Add(book);	
			}

			available = available - cached;

			foreach (string book in available.Keys) {
				BiblesAvailable_listEx.Add(book, 0);
			}
			
		}

		private void onProgress(object sender, EventArgs sent) {
			Console.WriteLine("onProgress");
			if (BibleCache_progressBar.Value == BibleCache_progressBar.Maximum) {
				BibleCache_progressBar.Value = BibleCache_progressBar.Minimum;
			} else {
				BibleCache_progressBar.Value++;
			}
		}

		/// <summary>
		/// Adds a bible translation to the cache
		/// 
		/// TODO: Make use of the progress bar because this is a very slow process.
		/// </summary>
		/// <param name="Index"></param>
		private void BiblesAvailable_listEx_PressIcon(int Index) {
			string tr = BiblesAvailable_listEx.Items[Index].ToString();

			BibleCache_progressBar.Value = BibleCache_progressBar.Maximum / 10;
			BibleCache_Message.Visible = true;

			if (_MainForm.bibles.Add(_MainForm.Diatheke, tr, Options_RegEx_Table, new EventHandler(this.onProgress)) ) {
				BiblesAvailable_listEx.Remove(Index);
				_MainForm.BibleText_Translations_Populate();
				BiblesCached_listEx.Add(tr, 1);
			}

			BibleCache_progressBar.Value = BibleCache_progressBar.Maximum;
			BibleCache_Message.Visible = false;
		}
		private void BiblesAvailable_listEx_DoubleClick(object sender, System.EventArgs e) {
			BiblesAvailable_listEx_PressIcon( BiblesAvailable_listEx.SelectedIndex );
		}

		/// <summary>
		/// Removes a bible translation from the cache
		/// </summary>
		/// <param name="Index"></param>
		private void BiblesCached_listEx_PressIcon(int Index) {
			string tr = BiblesCached_listEx.Items[Index].ToString();
			_MainForm.bibles.Remove(tr);
			BiblesCached_listEx.Remove(Index);
			BiblesAvailable_listEx.Add(tr);
			_MainForm.BibleText_Translations_Populate();
		}
		private void BiblesCached_listEx_DoubleClick(object sender, System.EventArgs e) {
			BiblesCached_listEx_PressIcon( BiblesCached_listEx.SelectedIndex );
		}

		#endregion


		private void BibleBGImageBrowse_Click(object sender, System.EventArgs e) {
			this.openFileDialog1.Filter = ImageFileFilter;
            this.openFileDialog1.InitialDirectory = Path.Combine(Tools.GetAppDocPath(), "Backgrounds");
            if (this.openFileDialog1.ShowDialog() != DialogResult.Cancel) {
				this.BibleBGImagePath.Text = this.openFileDialog1.FileName;
			}
		}

		private void SongBGImageBrowse_Click(object sender, System.EventArgs e) {
			this.openFileDialog1.Filter = ImageFileFilter;
            this.openFileDialog1.InitialDirectory = Path.Combine(Tools.GetAppDocPath(), "Backgrounds");
			if (this.openFileDialog1.ShowDialog() != DialogResult.Cancel) {
				this.SongBGImagePath.Text = this.openFileDialog1.FileName;
			}
		}

		private void TextBGImageBrowse_Click(object sender, System.EventArgs e) {
			this.openFileDialog1.Filter = ImageFileFilter;
            this.openFileDialog1.InitialDirectory = Path.Combine(Tools.GetAppDocPath(), "Backgrounds");
            if (this.openFileDialog1.ShowDialog() != DialogResult.Cancel) {
				this.TextBGImagePath.Text = this.openFileDialog1.FileName;
			}		
		}

		protected void SizeColumns(DataGrid grid) {
			Graphics g = CreateGraphics();  

			DataTable dataTable = (DataTable)grid.DataSource;

			DataGridTableStyle dataGridTableStyle = new DataGridTableStyle();

			dataGridTableStyle.MappingName = dataTable.TableName;

			foreach(DataColumn dataColumn in dataTable.Columns) {
				int maxSize = 0;

				SizeF size = g.MeasureString(
					dataColumn.ColumnName,
					grid.Font
					);

				if(size.Width > maxSize)
					maxSize = (int)size.Width;

				foreach(DataRow row in dataTable.Rows) {
					size = g.MeasureString(
						row[dataColumn.ColumnName].ToString(),
						grid.Font
						);

					if(size.Width > maxSize)
						maxSize = (int)size.Width;
				}

				DataGridColumnStyle dataGridColumnStyle =  new DataGridTextBoxColumn();
				dataGridColumnStyle.MappingName = dataColumn.ColumnName;
				dataGridColumnStyle.HeaderText = dataColumn.ColumnName;
				dataGridColumnStyle.Width = maxSize + 5;
				dataGridTableStyle.GridColumnStyles.Add(dataGridColumnStyle);
			}
			grid.TableStyles.Add(dataGridTableStyle);          

			g.Dispose();
		}

		public static void SetBibleLocale(BibleLib Bibles, string SwordPath, string locale) {
			string localeDir = Path.Combine(SwordPath, "locales.d");
			string longFile = Path.Combine(localeDir, locale + ".conf");
			string abbrFile = Path.Combine(localeDir, locale + "_abbr.conf");
			IniStructure longNames = IniStructure.ReadIni(longFile);
			IniStructure abbrNames = IniStructure.ReadIni(abbrFile);

			if (longNames == null) return;
			foreach (string translation in Bibles.Translations()) {
				BibleVersion bible = Bibles[translation];
				int i = 0;
				foreach (string book in BibleVersion.SwordBookNames) {
					string localizedName = longNames.GetValue("Text", book);
					bible.BibleBooks[i].Long = longNames.GetValue("Text", book);
					if (abbrNames != null) {
						bible.BibleBooks[i].Short = abbrNames.GetValue("Text", book);
					} else {
						bible.BibleBooks[i].Short = longNames.GetValue("Text", book);
					}
					i++;
				}
			}
		}

        private void SaveSongFormatButton_Click(object sender, EventArgs e) {
            SongTheme theme = new SongTheme();
            theme.BGImagePath = this.SongBGImagePath.Text;
            theme.TextFormat[(int)SongTextType.Title] = this.SongTitle_TextFormat.Format;
            theme.TextFormat[(int)SongTextType.Author] = this.SongAuthor_TextFormat.Format;
            theme.TextFormat[(int)SongTextType.Verse] = this.SongVerse_TextFormat.Format;
            theme.SaveAs();
        }

        private void OpenSongFormatButton_Click(object sender, EventArgs e) {
            SongTheme theme = SongTheme.OpenFile();
            if (theme == null) return;

            this.SongBGImagePath.Text = theme.BGImagePath;
            this.SongTitle_TextFormat.Format = theme.TextFormat[(int)SongTextType.Title];
            this.SongAuthor_TextFormat.Format = theme.TextFormat[(int)SongTextType.Author];
            this.SongVerse_TextFormat.Format = theme.TextFormat[(int)SongTextType.Verse];
        }

	}
}
