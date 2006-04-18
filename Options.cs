using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Rilling.Common.UI.Forms;

namespace DreamBeam {
    /// <summary>
    /// Zusammenfassende Beschreibung für WinForm
    /// </summary>
    public class Options : System.Windows.Forms.Form {
        /// <summary>
        /// Erforderliche Designervariable
        /// </summary>
        private System.ComponentModel.Container components = null;
        public System.Windows.Forms.TabControl tabControl;
        //  private System.Windows.Forms.TabPage Bible_Tab;
        public OPaC.Themed.Forms.TabPage Common_Tab;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Panel Options_TopPanel;
		private System.Windows.Forms.Button Select_Sword;
        private System.Windows.Forms.Button Options_Cancelbtn;

        ///<summary> </summary>
        public Config Conf = new Config();
		//Language
		private Language Lang = new Language();

        private System.Windows.Forms.Button Options_Okaybtn;
        private System.Windows.Forms.TextBox Sword_PathBox;
        private System.Windows.Forms.GroupBox SwordFolderGroupBox;
        private System.Windows.Forms.ComboBox Sword_LanguageBox;
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
		private System.Windows.Forms.Label PreRenderLabel;

        public Options() {
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
        protected override void Dispose (bool disposing) {
            if (disposing) {
                if (components != null) {
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
        private void InitializeComponent() {
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.Options_TopPanel = new System.Windows.Forms.Panel();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.Common_Tab = new OPaC.Themed.Forms.TabPage();
			this.LanguageBox = new System.Windows.Forms.GroupBox();
			this.TranslateLabel = new System.Windows.Forms.Label();
			this.LanguageList = new System.Windows.Forms.ListBox();
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
			this.Options_Cancelbtn = new System.Windows.Forms.Button();
			this.Options_Okaybtn = new System.Windows.Forms.Button();
			this.colorDialog1 = new System.Windows.Forms.ColorDialog();
			this.Options_TopPanel.SuspendLayout();
			this.tabControl.SuspendLayout();
			this.Common_Tab.SuspendLayout();
			this.LanguageBox.SuspendLayout();
			this.Graphics_tab.SuspendLayout();
			this.PreRenderBox.SuspendLayout();
			this.Alpha_groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Speed_Updown)).BeginInit();
			this.Grafics_Outline.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.OutlineSize_UpDown1)).BeginInit();
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
			this.SuspendLayout();
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.AddExtension = false;
			this.openFileDialog1.DefaultExt = "exe";
			this.openFileDialog1.Filter = "Sword.exe|sword.exe";
			this.openFileDialog1.Title = "Select Sword.exe File";
			this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
			// 
			// Options_TopPanel
			// 
			this.Options_TopPanel.Controls.Add(this.tabControl);
			this.Options_TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.Options_TopPanel.Location = new System.Drawing.Point(0, 0);
			this.Options_TopPanel.Name = "Options_TopPanel";
			this.Options_TopPanel.Size = new System.Drawing.Size(434, 264);
			this.Options_TopPanel.TabIndex = 0;
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.Common_Tab);
			this.tabControl.Controls.Add(this.Graphics_tab);
			this.tabControl.Controls.Add(this.BeamBox_tab);
			this.tabControl.Controls.Add(this.Bible_Tab);
			this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl.Location = new System.Drawing.Point(0, 0);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(434, 264);
			this.tabControl.TabIndex = 1;
			// 
			// Common_Tab
			// 
			this.Common_Tab.Controls.Add(this.LanguageBox);
			this.Common_Tab.Location = new System.Drawing.Point(4, 22);
			this.Common_Tab.Name = "Common_Tab";
			this.Common_Tab.Size = new System.Drawing.Size(426, 238);
			this.Common_Tab.TabIndex = 0;
			this.Common_Tab.Text = "Common Settings";
			// 
			// LanguageBox
			// 
			this.LanguageBox.Controls.Add(this.TranslateLabel);
			this.LanguageBox.Controls.Add(this.LanguageList);
			this.LanguageBox.Location = new System.Drawing.Point(8, 72);
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
			this.TranslateLabel.Text = "If you like to translate this Software into another Language, you " +  
				"are very welcome!";
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
			// Graphics_tab
			// 
			this.Graphics_tab.Controls.Add(this.PreRenderBox);
			this.Graphics_tab.Controls.Add(this.Alpha_groupBox1);
			this.Graphics_tab.Controls.Add(this.Grafics_Outline);
			this.Graphics_tab.Location = new System.Drawing.Point(4, 22);
			this.Graphics_tab.Name = "Graphics_tab";
			this.Graphics_tab.Size = new System.Drawing.Size(426, 238);
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
			this.Speed_Updown.Maximum = new System.Decimal(new int[] {
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
			// 
			// OutlineSize_UpDown1
			// 
			this.OutlineSize_UpDown1.Location = new System.Drawing.Point(16, 24);
			this.OutlineSize_UpDown1.Maximum = new System.Decimal(new int[] {
						20,
						0,
						0,
						0});
			this.OutlineSize_UpDown1.Name = "OutlineSize_UpDown1";
			this.OutlineSize_UpDown1.Size = new System.Drawing.Size(104, 20);
			this.OutlineSize_UpDown1.TabIndex = 0;
			this.OutlineSize_UpDown1.Tag = "";
			// 
			// BeamBox_tab
			// 
			this.BeamBox_tab.Controls.Add(this.Position_Title_GroupBox);
			this.BeamBox_tab.Controls.Add(this.Background_groupBox3);
			this.BeamBox_tab.Controls.Add(this.Mouse_groupBox2);
			this.BeamBox_tab.Location = new System.Drawing.Point(4, 22);
			this.BeamBox_tab.Name = "BeamBox_tab";
			this.BeamBox_tab.Size = new System.Drawing.Size(426, 238);
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
			this.BeamBox_posX.Maximum = new System.Decimal(new int[] {
						2000,
						0,
						0,
						0});
			this.BeamBox_posX.Minimum = new System.Decimal(new int[] {
						1000,
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
			this.BeamBox_posY.Maximum = new System.Decimal(new int[] {
						10000,
						0,
						0,
						0});
			this.BeamBox_posY.Minimum = new System.Decimal(new int[] {
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
			this.BeamBox_Width.Maximum = new System.Decimal(new int[] {
						10000,
						0,
						0,
						0});
			this.BeamBox_Width.Minimum = new System.Decimal(new int[] {
						200,
						0,
						0,
						0});
			this.BeamBox_Width.Name = "BeamBox_Width";
			this.BeamBox_Width.Size = new System.Drawing.Size(88, 20);
			this.BeamBox_Width.TabIndex = 5;
			this.BeamBox_Width.Value = new System.Decimal(new int[] {
						200,
						0,
						0,
						0});
			// 
			// BeamBox_Height
			// 
			this.BeamBox_Height.Location = new System.Drawing.Point(256, 16);
			this.BeamBox_Height.Maximum = new System.Decimal(new int[] {
						10000,
						0,
						0,
						0});
			this.BeamBox_Height.Minimum = new System.Decimal(new int[] {
						150,
						0,
						0,
						0});
			this.BeamBox_Height.Name = "BeamBox_Height";
			this.BeamBox_Height.Size = new System.Drawing.Size(88, 20);
			this.BeamBox_Height.TabIndex = 4;
			this.BeamBox_Height.Value = new System.Decimal(new int[] {
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
			this.Bible_Tab.Controls.Add(this.SwordLangGroupBox);
			this.Bible_Tab.Controls.Add(this.SwordFolderGroupBox);
			this.Bible_Tab.Location = new System.Drawing.Point(4, 22);
			this.Bible_Tab.Name = "Bible_Tab";
			this.Bible_Tab.Size = new System.Drawing.Size(426, 238);
			this.Bible_Tab.TabIndex = 3;
			this.Bible_Tab.Text = "Bible";
			// 
			// SwordLangGroupBox
			// 
			this.SwordLangGroupBox.Controls.Add(this.Sword_LanguageBox);
			this.SwordLangGroupBox.Location = new System.Drawing.Point(9, 135);
			this.SwordLangGroupBox.Name = "SwordLangGroupBox";
			this.SwordLangGroupBox.Size = new System.Drawing.Size(408, 48);
			this.SwordLangGroupBox.TabIndex = 8;
			this.SwordLangGroupBox.TabStop = false;
			this.SwordLangGroupBox.Text = "Sword Language";
			// 
			// Sword_LanguageBox
			// 
			this.Sword_LanguageBox.Enabled = false;
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
			this.Sword_LanguageBox.Size = new System.Drawing.Size(112, 21);
			this.Sword_LanguageBox.TabIndex = 5;
			// 
			// SwordFolderGroupBox
			// 
			this.SwordFolderGroupBox.Controls.Add(this.Sword_PathBox);
			this.SwordFolderGroupBox.Controls.Add(this.Select_Sword);
			this.SwordFolderGroupBox.Location = new System.Drawing.Point(9, 55);
			this.SwordFolderGroupBox.Name = "SwordFolderGroupBox";
			this.SwordFolderGroupBox.Size = new System.Drawing.Size(408, 72);
			this.SwordFolderGroupBox.TabIndex = 7;
			this.SwordFolderGroupBox.TabStop = false;
			this.SwordFolderGroupBox.Text = "Sword Project Folder";
			// 
			// Sword_PathBox
			// 
			this.Sword_PathBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Sword_PathBox.Location = new System.Drawing.Point(8, 16);
			this.Sword_PathBox.Name = "Sword_PathBox";
			this.Sword_PathBox.Size = new System.Drawing.Size(384, 20);
			this.Sword_PathBox.TabIndex = 1;
			this.Sword_PathBox.Text = "";
			// 
			// Select_Sword
			// 
			this.Select_Sword.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.Select_Sword.Location = new System.Drawing.Point(320, 40);
			this.Select_Sword.Name = "Select_Sword";
			this.Select_Sword.Size = new System.Drawing.Size(72, 24);
			this.Select_Sword.TabIndex = 0;
			this.Select_Sword.Text = "Browse";
			this.Select_Sword.Click += new System.EventHandler(this.Select_Sword_Click);
			// 
			// Options_Cancelbtn
			// 
			this.Options_Cancelbtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.Options_Cancelbtn.Location = new System.Drawing.Point(336, 268);
			this.Options_Cancelbtn.Name = "Options_Cancelbtn";
			this.Options_Cancelbtn.TabIndex = 1;
			this.Options_Cancelbtn.Text = "Cancel";
			this.Options_Cancelbtn.Click += new System.EventHandler(this.Options_Cancelbtn_Click);
			// 
			// Options_Okaybtn
			// 
			this.Options_Okaybtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.Options_Okaybtn.Location = new System.Drawing.Point(248, 268);
			this.Options_Okaybtn.Name = "Options_Okaybtn";
			this.Options_Okaybtn.TabIndex = 2;
			this.Options_Okaybtn.Text = "Okay";
			this.Options_Okaybtn.Click += new System.EventHandler(this.Options_Okaybtn_Click);
			// 
			// Options
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(434, 296);
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
			this.LanguageBox.ResumeLayout(false);
			this.Graphics_tab.ResumeLayout(false);
			this.PreRenderBox.ResumeLayout(false);
			this.Alpha_groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Speed_Updown)).EndInit();
			this.Grafics_Outline.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.OutlineSize_UpDown1)).EndInit();
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
			this.ResumeLayout(false);
		}
#endregion

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e) {
        }

		private void Options_Okaybtn_Click(object sender, System.EventArgs e) {
			this.Conf.Alphablending = this.Alpha_CheckBox.Checked;
			this.Conf.PreRender = this.PreRendercheckBox.Checked;
			this.Conf.BlendSpeed = (int)Speed_Updown.Value;
			this.Conf.useDirect3D = this.Direct3D_CheckBox.Checked;

			this.Conf.BibleLang = this.Sword_LanguageBox.SelectedItem.ToString();
			this.Conf.OutlineSize = (float)this.OutlineSize_UpDown1.Value;


			if (!Conf.BeamBoxAutoPosSize) {
				this.Conf.BeamBoxPosX = (int)this.BeamBox_posX.Value;
				this.Conf.BeamBoxPosY = (int)this.BeamBox_posY.Value;
				this.Conf.BeamBoxSizeX = (int)this.BeamBox_Width.Value;
				this.Conf.BeamBoxSizeY = (int)this.BeamBox_Height.Value;
			} else {
				if(ScreenList.SelectedIndex >=  0) {
					this.Conf.BeamBoxScreenNum = ScreenList.SelectedIndex;
				}
			}


			this.Conf.SwordPath = this.Sword_PathBox.Text;
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

			this.Conf.Save("config.xml");
			this.Close();
		}


		private void Select_Sword_Click(object sender, System.EventArgs e) {
			this.openFileDialog1.FileName = this.Sword_PathBox.Text + "sword.exe";
			this.openFileDialog1.ShowDialog();
			this.Sword_PathBox.Text = this.openFileDialog1.FileName.Substring(0,this.openFileDialog1.FileName.Length-9);
			MessageBox.Show("To use the Bible, you have to restart DreamBeam.");
		}

		private void Options_Load(object sender, System.EventArgs e) {
			this.Conf.Load();
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



        }

        private void Options_Cancelbtn_Click(object sender, System.EventArgs e) {
			this.Close();
        }

        private void BeamBox_ColorButton_Click(object sender, System.EventArgs e) {
            this.colorDialog1.Color = this.Conf.BackgroundColor;
            this.colorDialog1.ShowDialog();
            this.Conf.BackgroundColor = this.colorDialog1.Color;
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
                    this.ScreenList.Items.Add ("Secundary Screen "+ i.ToString());
                    i++;
                    if (FirstSecundary == -1) {
                        FirstSecundary = i;
                    }
                }
                //if no Secundary Found take the Primary (in this case, the only one found)
                if (FirstSecundary == -1) {
                    FirstSecundary = 0;
                }

                if(Conf.BeamBoxAutoPosSize) {
                    if(Conf.BeamBoxScreenNum < ScreenList.Items.Count) {
                        ScreenList.SelectedIndex = Conf.BeamBoxScreenNum;
                    }
                } else {
                    ScreenList.SelectedIndex = FirstSecundary;
                }

            }
        }


        private void ScreenList_SelectedIndexChanged(object sender, System.EventArgs e) {
            if(ScreenList.SelectedIndex >=  0) {
                AutoPosLabelX.Text = "X: " + System.Windows.Forms.Screen.AllScreens[ScreenList.SelectedIndex].Bounds.X.ToString();
                AutoPosLabelY.Text = " Y:"+System.Windows.Forms.Screen.AllScreens[ScreenList.SelectedIndex].Bounds.Y.ToString();
                AutoSizeLabelW.Text = "Width: " + System.Windows.Forms.Screen.AllScreens[ScreenList.SelectedIndex].Bounds.Width.ToString();
                AutoSizeLabelH.Text = "Height: " + System.Windows.Forms.Screen.AllScreens[ScreenList.SelectedIndex].Bounds.Height.ToString();
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
		

	}
}
