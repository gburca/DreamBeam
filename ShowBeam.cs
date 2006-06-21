/*

DreamBeam - a Church Song Presentation Program
Copyright (C) 2004 Stefan Kaufmann

This program is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation; either version 2
of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.


System.Windows.Forms.Screen.PrimaryScreen.Bounds;

*/


using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Runtime.InteropServices;
using DirectShowLib;
using System.Resources;
using System.Globalization;

namespace DreamBeam {
	/// <summary>
	/// ShowBeam - the BeamBox class. This is a none-borderd Window for the Beamer
	/// </summary>
	public class ShowBeam : System.Windows.Forms.Form {

	#region Variables
		private System.ComponentModel.IContainer components;

		public Config Config;
		public MainForm _MainForm = null;
        ///<summary>The Separator for the strophes - 2 Blank Lines </summary>
//        public string strSeperator = "\r\n\r\n\r\n";

        ///<summary>Has Text changed? Needed to avoid unnecessary Redraws.(</summary>
        public bool newText = false;

        ///<summary>If this is true, take ImageOverWritePath -Image as Background </summary>
        public bool OverWriteBG = false;

        ///<summary>alternate Background Image Path </summary>
        public String ImageOverWritePath = "";

        ///<summary>Background Image Path for Sermon Texts</summary>
		public String SermonImagePath = null;

		///<summary> the size for the Outlines (will be loaded from the MainForm.ConfigSet) </summary>
		public float OutlineSize = 5.0f;

		///<summary>the name of the actually used BG Image </summary>
		public string strImageLoaded = null;


		public Song Song = new Song();  //The Song with all it's information

		///<summary>The Sermon will be a castradet Song</summary>
		public Song Sermon = new Song();

		///<summary>The Background Image </summary>
		public Image curImage;

		///<summary>Graphics</summary>
		public Graphics graphics;

		///<summary>The final Bitmap for the BeamBox </summary>
		public Bitmap memoryBitmap = null;

		///<summary>Use AlphaBlending Transitions </summary>
		public bool transit = false;

		///<summary>Temporary bitmap</summary>
		public Bitmap bmp = null;
        public bool DrawnMainBitmap = false;

		///<summary>User Direct X? (not used yet)</summary>
		public bool useDirectX = false;

		///<summary>Draw Song, Text... </summary>
		public int DrawWhat = 0;

		public bool DrawingSong = false;

		public System.Drawing.Color BackgroundColor = new System.Drawing.Color();

		public bool HideText = false;
		public bool HideBG = false;
		public bool HideMouse = false;

		public bool HideTitle = false;
		public bool HideVerse = false;
		public bool HideAuthor = false;

		public bool Songupdate = true;

		public int[] tmpPosX = new int[3];
		public int[] tmpPosY = new int[3];

		public TextGraphics TextGraphics =null;
        public Prerenderer Prerenderer = null;

		public ImageList MediaList = new ImageList();
		bool FlashPlaying = false;
		bool VideoPlaying = false;
		string PlayWhat = "";
		string PrePlaying = "";

		public string strMediaPlaying = null;
		public Microsoft.DirectX.AudioVideoPlayback.Video video = null;
		public bool VideoProblem = false;
		public int AudioVolume = 0;

		public bool LoopMedia = true;

		public bool useDirect3d;
		public bool BeamBoxAutoPosSize;
		public int BeamBoxScreenNum;

		MyPerPixelAlphaForm AlphaForm = new MyPerPixelAlphaForm();
		byte AlphaOpacity;

		public LogFile LogFile;
		StringFormat sf = new StringFormat();

//		DirectShowLib Lib = new DirectShowLib();

		private FXLib FXLib = new FXLib();
		#endregion

		#region Controls
		private System.Windows.Forms.Panel panel1;
		///<summary>ShowBeam Position Box </summary>
		public System.Windows.Forms.NumericUpDown WindowPosX;
		///<summary>ShowBeam Position Box </summary>
		public System.Windows.Forms.NumericUpDown WindowPosY;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Panel panel2;
        ///<summary>ShowBeam Size Box </summary>
        public System.Windows.Forms.NumericUpDown WindowSizeY;
        ///<summary>ShowBeam Size Box </summary>
        public System.Windows.Forms.NumericUpDown WindowSizeX;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        public System.Windows.Forms.Panel ShowSongPanel;
		public AxShockwaveFlashObjects.AxShockwaveFlash axShockwaveFlash;
		private System.Windows.Forms.Panel VideoPanel;
        private System.Windows.Forms.Timer PlayBackTimer;
        private System.Windows.Forms.TabControl SizePosControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage TabManSize;
        private System.Windows.Forms.ListBox ScreenList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label AutoPosLabelX;
        private System.Windows.Forms.Label AutoSizeLabelW;
        private System.Windows.Forms.Label AutoPosLabelY;
        private System.Windows.Forms.Label AutoSizeLabelH;
        private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button Screen_SetSettingsButton;
		private System.Windows.Forms.PictureBox TestImage;
		private System.Windows.Forms.Timer AlphaTimer;
		#endregion


		/// <summary> Initialize the Class</summary>
		public ShowBeam() {
			InitializeComponent();
			SetSizePosValues();
			//   BackgroundColor =
			//Color.FromArgb(10,11,12);
			Sermon.TextEffect[1] = "Filled Outline";
			Sermon.FontSize[1] = 48;
			Song = new Song(this);
			 TextGraphics = new TextGraphics(this,Song);
			Prerenderer = new Prerenderer(this,Song);
			//   draw = new Device(); // Create a new DrawDevice, using the default device.
			//   draw.SetCooperativeLevel(this, CooperativeLevelFlags.Normal); // Set the coop level to normal windowed mode.
			//   clip = new Clipper(draw); // Create a new clipper.
			//   CreateSurfaces(); // Call the function that creates the surface objects.
		}

		/// <summary>
		/// Dispose components
		/// </summary>
		protected override void Dispose (bool disposing) {
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

	#region Vom Windo ws Form-Designer erzeugter Code
		/// <summary>
		/// Form Disigner Method
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ShowBeam));
			this.panel1 = new System.Windows.Forms.Panel();
			this.WindowPosY = new System.Windows.Forms.NumericUpDown();
			this.WindowPosX = new System.Windows.Forms.NumericUpDown();
			this.button4 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.panel2 = new System.Windows.Forms.Panel();
			this.WindowSizeY = new System.Windows.Forms.NumericUpDown();
			this.WindowSizeX = new System.Windows.Forms.NumericUpDown();
			this.button5 = new System.Windows.Forms.Button();
			this.button6 = new System.Windows.Forms.Button();
			this.button7 = new System.Windows.Forms.Button();
			this.button8 = new System.Windows.Forms.Button();
			this.ShowSongPanel = new System.Windows.Forms.Panel();
			this.axShockwaveFlash = new AxShockwaveFlashObjects.AxShockwaveFlash();
			this.VideoPanel = new System.Windows.Forms.Panel();
			this.PlayBackTimer = new System.Windows.Forms.Timer(this.components);
			this.SizePosControl = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.Screen_SetSettingsButton = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.AutoPosLabelY = new System.Windows.Forms.Label();
			this.AutoPosLabelX = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.AutoSizeLabelW = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.AutoSizeLabelH = new System.Windows.Forms.Label();
			this.ScreenList = new System.Windows.Forms.ListBox();
			this.TabManSize = new System.Windows.Forms.TabPage();
			this.TestImage = new System.Windows.Forms.PictureBox();
			this.AlphaTimer = new System.Windows.Forms.Timer(this.components);
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.WindowPosY)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.WindowPosX)).BeginInit();
			this.panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.WindowSizeY)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.WindowSizeX)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.axShockwaveFlash)).BeginInit();
			this.SizePosControl.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.TabManSize.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.Maroon;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel1.Controls.Add(this.WindowPosY);
			this.panel1.Controls.Add(this.WindowPosX);
			this.panel1.Controls.Add(this.button4);
			this.panel1.Controls.Add(this.button3);
			this.panel1.Controls.Add(this.button1);
			this.panel1.Controls.Add(this.button2);
			this.panel1.Location = new System.Drawing.Point(2, 2);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(147, 135);
			this.panel1.TabIndex = 0;
			// 
			// WindowPosY
			// 
			this.WindowPosY.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.WindowPosY.Location = new System.Drawing.Point(44, 65);
			this.WindowPosY.Maximum = new System.Decimal(new int[] {
																	   10000,
																	   0,
																	   0,
																	   0});
			this.WindowPosY.Minimum = new System.Decimal(new int[] {
																	   10000,
																	   0,
																	   0,
																	   -2147483648});
			this.WindowPosY.Name = "WindowPosY";
			this.WindowPosY.Size = new System.Drawing.Size(54, 20);
			this.WindowPosY.TabIndex = 5;
			this.WindowPosY.ValueChanged += new System.EventHandler(this.WindowPosY_ValueChanged);
			this.WindowPosY.Leave += new System.EventHandler(this.WindowPosY_ValueChanged);
			// 
			// WindowPosX
			// 
			this.WindowPosX.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.WindowPosX.Location = new System.Drawing.Point(44, 44);
			this.WindowPosX.Maximum = new System.Decimal(new int[] {
																	   10000,
																	   0,
																	   0,
																	   0});
			this.WindowPosX.Minimum = new System.Decimal(new int[] {
																	   10000,
																	   0,
																	   0,
																	   -2147483648});
			this.WindowPosX.Name = "WindowPosX";
			this.WindowPosX.Size = new System.Drawing.Size(54, 20);
			this.WindowPosX.TabIndex = 4;
			this.WindowPosX.ValueChanged += new System.EventHandler(this.WindowPosX_ValueChanged);
			this.WindowPosX.Leave += new System.EventHandler(this.WindowPosX_ValueChanged);
			// 
			// button4
			// 
			this.button4.Image = ((System.Drawing.Image)(resources.GetObject("button4.Image")));
			this.button4.Location = new System.Drawing.Point(50, 88);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(42, 42);
			this.button4.TabIndex = 3;
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// button3
			// 
			this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
			this.button3.Location = new System.Drawing.Point(50, 1);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(42, 42);
			this.button3.TabIndex = 2;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// button1
			// 
			this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
			this.button1.Location = new System.Drawing.Point(2, 44);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(42, 42);
			this.button1.TabIndex = 0;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
			this.button2.Location = new System.Drawing.Point(100, 44);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(42, 42);
			this.button2.TabIndex = 1;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// panel2
			// 
			this.panel2.BackColor = System.Drawing.Color.Maroon;
			this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel2.Controls.Add(this.WindowSizeY);
			this.panel2.Controls.Add(this.WindowSizeX);
			this.panel2.Controls.Add(this.button5);
			this.panel2.Controls.Add(this.button6);
			this.panel2.Controls.Add(this.button7);
			this.panel2.Controls.Add(this.button8);
			this.panel2.Location = new System.Drawing.Point(154, 2);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(147, 135);
			this.panel2.TabIndex = 1;
			// 
			// WindowSizeY
			// 
			this.WindowSizeY.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.WindowSizeY.Location = new System.Drawing.Point(44, 65);
			this.WindowSizeY.Maximum = new System.Decimal(new int[] {
																		10000,
																		0,
																		0,
																		0});
			this.WindowSizeY.Minimum = new System.Decimal(new int[] {
																		160,
																		0,
																		0,
																		0});
			this.WindowSizeY.Name = "WindowSizeY";
			this.WindowSizeY.Size = new System.Drawing.Size(54, 20);
			this.WindowSizeY.TabIndex = 5;
			this.WindowSizeY.Value = new System.Decimal(new int[] {
																	  200,
																	  0,
																	  0,
																	  0});
			this.WindowSizeY.ValueChanged += new System.EventHandler(this.WindowSizeY_ValueChanged);
			this.WindowSizeY.Leave += new System.EventHandler(this.WindowSizeY_ValueChanged);
			// 
			// WindowSizeX
			// 
			this.WindowSizeX.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.WindowSizeX.Location = new System.Drawing.Point(44, 44);
			this.WindowSizeX.Maximum = new System.Decimal(new int[] {
																		10000,
																		0,
																		0,
																		0});
			this.WindowSizeX.Minimum = new System.Decimal(new int[] {
																		150,
																		0,
																		0,
																		0});
			this.WindowSizeX.Name = "WindowSizeX";
			this.WindowSizeX.Size = new System.Drawing.Size(54, 20);
			this.WindowSizeX.TabIndex = 4;
			this.WindowSizeX.Value = new System.Decimal(new int[] {
																	  350,
																	  0,
																	  0,
																	  0});
			this.WindowSizeX.ValueChanged += new System.EventHandler(this.WindowSizeX_ValueChanged);
			this.WindowSizeX.Leave += new System.EventHandler(this.WindowSizeX_ValueChanged);
			// 
			// button5
			// 
			this.button5.Image = ((System.Drawing.Image)(resources.GetObject("button5.Image")));
			this.button5.Location = new System.Drawing.Point(50, 88);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(42, 42);
			this.button5.TabIndex = 3;
			this.button5.Click += new System.EventHandler(this.button5_Click);
			// 
			// button6
			// 
			this.button6.Image = ((System.Drawing.Image)(resources.GetObject("button6.Image")));
			this.button6.Location = new System.Drawing.Point(50, 1);
			this.button6.Name = "button6";
			this.button6.Size = new System.Drawing.Size(42, 42);
			this.button6.TabIndex = 2;
			this.button6.Click += new System.EventHandler(this.button6_Click);
			// 
			// button7
			// 
			this.button7.Image = ((System.Drawing.Image)(resources.GetObject("button7.Image")));
			this.button7.Location = new System.Drawing.Point(2, 44);
			this.button7.Name = "button7";
			this.button7.Size = new System.Drawing.Size(42, 42);
			this.button7.TabIndex = 0;
			this.button7.Click += new System.EventHandler(this.button7_Click);
			// 
			// button8
			// 
			this.button8.Image = ((System.Drawing.Image)(resources.GetObject("button8.Image")));
			this.button8.Location = new System.Drawing.Point(100, 44);
			this.button8.Name = "button8";
			this.button8.Size = new System.Drawing.Size(42, 42);
			this.button8.TabIndex = 1;
			this.button8.Click += new System.EventHandler(this.button8_Click);
			// 
			// ShowSongPanel
			// 
			this.ShowSongPanel.BackColor = System.Drawing.Color.Black;
			this.ShowSongPanel.Location = new System.Drawing.Point(48, 240);
			this.ShowSongPanel.Name = "ShowSongPanel";
			this.ShowSongPanel.Size = new System.Drawing.Size(176, 80);
			this.ShowSongPanel.TabIndex = 2;
			this.ShowSongPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.ShowSongPanel_Paint);
			this.ShowSongPanel.MouseEnter += new System.EventHandler(this.ShowBeam_MouseEnter);
			this.ShowSongPanel.MouseLeave += new System.EventHandler(this.ShowBeam_MouseLeave);
			this.ShowSongPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ShowBeam_MouseDown);
			// 
			// axShockwaveFlash
			// 
			this.axShockwaveFlash.Enabled = true;
			this.axShockwaveFlash.Location = new System.Drawing.Point(408, 32);
			this.axShockwaveFlash.Name = "axShockwaveFlash";
			this.axShockwaveFlash.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axShockwaveFlash.OcxState")));
			this.axShockwaveFlash.Size = new System.Drawing.Size(192, 192);
			this.axShockwaveFlash.TabIndex = 0;
			// 
			// VideoPanel
			// 
			this.VideoPanel.Location = new System.Drawing.Point(248, 232);
			this.VideoPanel.Name = "VideoPanel";
			this.VideoPanel.Size = new System.Drawing.Size(184, 96);
			this.VideoPanel.TabIndex = 3;
			this.VideoPanel.MouseEnter += new System.EventHandler(this.ShowBeam_MouseEnter);
			this.VideoPanel.MouseLeave += new System.EventHandler(this.ShowBeam_MouseLeave);
			this.VideoPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ShowBeam_MouseDown);
			// 
			// PlayBackTimer
			// 
			this.PlayBackTimer.Tick += new System.EventHandler(this.PlayBackTimer_Tick);
			// 
			// SizePosControl
			// 
			this.SizePosControl.Appearance = System.Windows.Forms.TabAppearance.Buttons;
			this.SizePosControl.Controls.Add(this.tabPage1);
			this.SizePosControl.Controls.Add(this.TabManSize);
			this.SizePosControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.SizePosControl.ItemSize = new System.Drawing.Size(152, 21);
			this.SizePosControl.Location = new System.Drawing.Point(0, 0);
			this.SizePosControl.Name = "SizePosControl";
			this.SizePosControl.SelectedIndex = 0;
			this.SizePosControl.Size = new System.Drawing.Size(313, 167);
			this.SizePosControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			this.SizePosControl.TabIndex = 4;
			this.SizePosControl.Visible = false;
			this.SizePosControl.Click += new System.EventHandler(this.SizePosControl_Click);
			// 
			// tabPage1
			// 
			this.tabPage1.BackColor = System.Drawing.Color.Maroon;
			this.tabPage1.Controls.Add(this.Screen_SetSettingsButton);
			this.tabPage1.Controls.Add(this.groupBox1);
			this.tabPage1.Controls.Add(this.ScreenList);
			this.tabPage1.ForeColor = System.Drawing.Color.White;
			this.tabPage1.Location = new System.Drawing.Point(4, 25);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(305, 138);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Auto";
			// 
			// Screen_SetSettingsButton
			// 
			this.Screen_SetSettingsButton.Location = new System.Drawing.Point(192, 104);
			this.Screen_SetSettingsButton.Name = "Screen_SetSettingsButton";
			this.Screen_SetSettingsButton.TabIndex = 8;
			this.Screen_SetSettingsButton.Text = "Set";
			this.Screen_SetSettingsButton.Click += new System.EventHandler(this.Screen_SetSettingsButton_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.AutoPosLabelY);
			this.groupBox1.Controls.Add(this.AutoPosLabelX);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.AutoSizeLabelW);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.AutoSizeLabelH);
			this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.groupBox1.Location = new System.Drawing.Point(144, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(160, 86);
			this.groupBox1.TabIndex = 7;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Info";
			// 
			// AutoPosLabelY
			// 
			this.AutoPosLabelY.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.AutoPosLabelY.Location = new System.Drawing.Point(82, 28);
			this.AutoPosLabelY.Name = "AutoPosLabelY";
			this.AutoPosLabelY.Size = new System.Drawing.Size(70, 16);
			this.AutoPosLabelY.TabIndex = 5;
			// 
			// AutoPosLabelX
			// 
			this.AutoPosLabelX.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.AutoPosLabelX.Location = new System.Drawing.Point(8, 28);
			this.AutoPosLabelX.Name = "AutoPosLabelX";
			this.AutoPosLabelX.Size = new System.Drawing.Size(70, 16);
			this.AutoPosLabelX.TabIndex = 3;
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Location = new System.Drawing.Point(6, 14);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(88, 15);
			this.label1.TabIndex = 1;
			this.label1.Text = "Position:";
			// 
			// AutoSizeLabelW
			// 
			this.AutoSizeLabelW.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.AutoSizeLabelW.Location = new System.Drawing.Point(8, 60);
			this.AutoSizeLabelW.Name = "AutoSizeLabelW";
			this.AutoSizeLabelW.Size = new System.Drawing.Size(70, 16);
			this.AutoSizeLabelW.TabIndex = 4;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 45);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(88, 15);
			this.label2.TabIndex = 2;
			this.label2.Text = "Size:";
			// 
			// AutoSizeLabelH
			// 
			this.AutoSizeLabelH.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.AutoSizeLabelH.Location = new System.Drawing.Point(82, 60);
			this.AutoSizeLabelH.Name = "AutoSizeLabelH";
			this.AutoSizeLabelH.Size = new System.Drawing.Size(70, 16);
			this.AutoSizeLabelH.TabIndex = 6;
			// 
			// ScreenList
			// 
			this.ScreenList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ScreenList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ScreenList.Location = new System.Drawing.Point(2, 2);
			this.ScreenList.Name = "ScreenList";
			this.ScreenList.Size = new System.Drawing.Size(142, 119);
			this.ScreenList.TabIndex = 0;
			this.ScreenList.SelectedIndexChanged += new System.EventHandler(this.ScreenList_SelectedIndexChanged);
			// 
			// TabManSize
			// 
			this.TabManSize.BackColor = System.Drawing.Color.Maroon;
			this.TabManSize.Controls.Add(this.panel1);
			this.TabManSize.Controls.Add(this.panel2);
			this.TabManSize.Location = new System.Drawing.Point(4, 25);
			this.TabManSize.Name = "TabManSize";
			this.TabManSize.Size = new System.Drawing.Size(305, 138);
			this.TabManSize.TabIndex = 1;
			this.TabManSize.Text = "Manual";
			// 
			// TestImage
			// 
			this.TestImage.Location = new System.Drawing.Point(472, 304);
			this.TestImage.Name = "TestImage";
			this.TestImage.Size = new System.Drawing.Size(120, 80);
			this.TestImage.TabIndex = 5;
			this.TestImage.TabStop = false;
			this.TestImage.Visible = false;
			this.TestImage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ShowBeam_MouseDown);
			// 
			// AlphaTimer
			// 
			this.AlphaTimer.Interval = 1;
			this.AlphaTimer.Tick += new System.EventHandler(this.AlphaTimer_Tick);
			// 
			// ShowBeam
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(632, 437);
			this.Controls.Add(this.TestImage);
			this.Controls.Add(this.SizePosControl);
			this.Controls.Add(this.VideoPanel);
			this.Controls.Add(this.ShowSongPanel);
			this.Controls.Add(this.axShockwaveFlash);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Location = new System.Drawing.Point(100, 100);
			this.Name = "ShowBeam";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "ShowBeam";
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ShowBeam_MouseDown);
			this.VisibleChanged += new System.EventHandler(this.ShowBeam_VisibleChanged);
			this.MouseEnter += new System.EventHandler(this.ShowBeam_MouseEnter);
			this.MouseLeave += new System.EventHandler(this.ShowBeam_MouseLeave);
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.WindowPosY)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.WindowPosX)).EndInit();
			this.panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.WindowSizeY)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.WindowSizeX)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.axShockwaveFlash)).EndInit();
			this.SizePosControl.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.TabManSize.ResumeLayout(false);
			this.ResumeLayout(false);

		}
	#endregion




	#region Song
		/// <summary> paints a Song to the BeamBox </summary>
		public void PaintSong() {
			this.DrawWhat = 0;

			this.GDIDraw();
			//this.DXDraw();
		}

		public bool DrawSongItem(int j){
			if(j == 0 && this.HideTitle){
				return false;
			}
			if(j == 1 && this.HideVerse){
				return false;
			}
			if(j == 2 && this.HideAuthor){
				return false;
			}
			return true;
		}

	#endregion

	#region Sermon
		/// <summary> paints a Sermon to the BeamBox </summary>
        public void PaintSermon() {
            this.DrawWhat = 1;
            this.GDIDraw();
            //this.DXDraw();
        }

        ///<summary>Paints a Bitmap from the Sermon Contents</summary>
        public Bitmap DrawSermonBitmap(int Width,int Height) {
			Pen p;
            SolidBrush sdBrush1;
            sdBrush1 = new SolidBrush(this.BackgroundColor);
            bmp =  new Bitmap(Width,Height);
            graphics = Graphics.FromImage(bmp);
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			graphics.SmoothingMode = SmoothingMode.HighQuality;
			graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
			Rectangle bRef, bVerse, rect;
						
			// These need to be configuration parameters. They are similar to Sermon.posX and Sermon.posY
			// Percentage x, y, width, height of the box for Ref, Verse, Transl
			RectangleF boundsRef = new RectangleF(0.05F, 0.05F, 0.95F, 0.18F);
			RectangleF boundsVerse = new RectangleF(0.05F, 0.20F, 0.95F, 0.95F);
			bRef = new System.Drawing.Rectangle((int)(boundsRef.X * Width), (int)(boundsRef.Y * Height),
				(int)((boundsRef.Width - boundsRef.X)* Width), (int)((boundsRef.Height - boundsRef.Y) * Height));
			bVerse = new System.Drawing.Rectangle((int)(boundsVerse.X * Width), (int)(boundsVerse.Y * Height),
				(int)((boundsVerse.Width - boundsVerse.X) * Width), (int)((boundsVerse.Height - boundsVerse.Y) * Height));
								
			#region care about BG Image

            if (this.SermonImagePath != null  && HideBG == false) {
                if (this.SermonImagePath.Length > 0) {
                    if (this.strImageLoaded != this.SermonImagePath) {
                        curImage = Image.FromFile(this.SermonImagePath);
                    }
                    this.strImageLoaded = this.SermonImagePath;
                }
                graphics.DrawImage(curImage, 0, 0, Width, Height);
            } else {
				//draw a blank rectangle if no image is defined.
                graphics.FillRectangle(sdBrush1, new Rectangle(0,0,this.Width, this.Height));
            }

			#endregion

			/*
			graphics.DrawRectangle(new Pen(Color.Green, 2), bRef);
			graphics.DrawRectangle(new Pen(Color.Yellow, 2), bVerse);
			rect = new Rectangle((int)(Width/2), (int)(Height/2), 0, 0);
			int i = 25;
			while (rect.Width < Width) {
				graphics.DrawRectangle(new Pen(Color.Gray, 1), rect);
				rect.Inflate(i, i);
			}
			*/

			
			GraphicsPath pth, pth2;
			pth = new GraphicsPath();
			string text;

            if (!HideText) {
                for (int j = 0; j <=2; j++) {
					text = Sermon.GetText(j);
					if (text.Length > 0) {

						p = new Pen(Sermon.TextColor[j], 1.0f);
						p.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
						SizeF stringSize = new SizeF();
						sf = new StringFormat();
                        
						switch (j) {
							case 0:
								sf.Alignment = StringAlignment.Near;
								sf.LineAlignment = StringAlignment.Center;
								rect = bRef;
								break;
							case 1:
								sf.Alignment = StringAlignment.Center;
								sf.LineAlignment = StringAlignment.Center;
								//rect = new System.Drawing.Rectangle(bVerse.X, bVerse.Y, bVerse.Width, bVerse.Height);
								rect = bVerse;
								break;
							default:
								sf.Alignment = StringAlignment.Far;
								sf.LineAlignment = StringAlignment.Center;
								rect = bVerse;
								break;
						}		


						// Make a rectangle that is very tall to see how far down the text would stretch.
						Rectangle pathRect = rect;
						pathRect.Height = 10000;

						RectangleF pathBounds;
						float fontSz = Sermon.FontSize[j];
						Font font;

						// If the font size is too big, find the next size down that fits.
						// Esther 8:9 is one of the longest verses in the bible. See if it fits.
						do {
							font = new Font(Sermon.FontFace[j], fontSz, Sermon.FontStyle[j]);
							pth.Reset();
							pth.AddString(text, font.FontFamily, (int)font.Style, font.Size, pathRect, sf);
							pathBounds = pth.GetBounds();
							fontSz--;
						} while (pathBounds.Height > bVerse.Height);

						// For some reason, the path gets drawn at some silly Y location, nowhere close to pathRect.Y.
						// See pathBounds.Y. We re-draw it where it belongs.
						pth.Reset();
						pth.AddString(text, font.FontFamily, (int)font.Style, font.Size, rect, sf);
						pathBounds = pth.GetBounds();

						sdBrush1 = new SolidBrush(Sermon.TextColor[j]);


						if (Sermon.TextEffect[j] == "Normal" || Sermon.TextEffect[j] == "") {

							graphics.FillPath(sdBrush1,pth);
							graphics.DrawPath(p,pth);

						} else if (Sermon.TextEffect[j] == "Filled Outline") {

							pth2 = (GraphicsPath)pth.Clone();
							graphics.FillPath(sdBrush1,pth);
							graphics.DrawPath(p,pth);

							// Widen the path.
							Pen widenPen = new Pen(Sermon.OutlineColor[j], OutlineSize);
							widenPen.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
							pth2.Widen(widenPen);
							graphics.FillPath(new SolidBrush(Sermon.OutlineColor[j]), pth2);
							graphics.DrawPath(p,pth);
							graphics.FillPath(sdBrush1,pth);


						} else  if (Sermon.TextEffect[j] == "Outline") {
                            p=new Pen(Sermon.OutlineColor[j],1.5f);
                            pth.AddString(text,
                                          new FontFamily(Sermon.FontFace[j]),0,Sermon.FontSize[j],
                                          new Point(Sermon.posX[j],Sermon.posY[j]),StringFormat.GenericTypographic);

                            graphics.DrawPath(p,pth);
                        }
                    }
                }
            }

            return bmp;
		}



	#endregion

	#region DrawOnBeamBox
		///<summary>Selects which sort of Bitmap has to be painted</summary>
		public void DrawBitmap(int Width,int Height) {
			switch(this.DrawWhat) {
			case 0:
//			do {}
				//while(DrawingSong);
				bmp = new Bitmap(Width,Height,PixelFormat.Format32bppArgb);
				//TextGraphics.DrawSongBitmap(bmp,Song.strophe,Width,Height);
				bmp = Prerenderer.GetStrophe(Song.strophe);
				DrawnMainBitmap = true;
				break;
			case 1:
				bmp = DrawSermonBitmap(Width,Height);
				break;
			}
		}


		///<summary>Draws the Image on the BeamBox</summary>
		public void GDIDraw() {

            Graphics g = Graphics.FromHwnd(ShowSongPanel.Handle);
            axShockwaveFlash.Hide();
			VideoPanel.Hide();
            ShowSongPanel.Show();
            if (strMediaPlaying == "flash") {
                this.axShockwaveFlash.Stop();
                strMediaPlaying = null;
            }
            if (strMediaPlaying == "movie") {
                this.video.Stop();
                PlayBackTimer.Enabled = false;
                strMediaPlaying = null;
			}

			ShowSongPanel.Size = this.Size;
			ShowSongPanel.Location = new Point (0,0);


			g.SmoothingMode = SmoothingMode.HighQuality;
			g.InterpolationMode = InterpolationMode.HighQualityBicubic;
			g.PixelOffsetMode = PixelOffsetMode.HighQuality;

			if (memoryBitmap != null) {
				g.DrawImage(memoryBitmap, 0, 0, this.Width, this.Height);
			}

			if (Songupdate){
				Songupdate = false;
				DrawBitmap(this.Width,this.Height);
				memoryBitmap = bmp;

				//Draw Image on Screen, make a Alphablending if transit == true

				if(transit & newText) {

					//MyPerPixelAlphaForm AlphaForm;	// our test form

					AlphaForm.setpos(Location.X,Location.Y);
					//AlphaForm.Show();
                    AlphaForm.Visible = true;
					_MainForm.BringToFront();
					
					AlphaForm.SetBitmap(bmp, 0);
					AlphaOpacity = 0;
					AlphaTimer.Start();

				} else {
					g.DrawImage(memoryBitmap, new Rectangle(0,0,this.Width, this.Height), 0,0,this.Width, this.Height, GraphicsUnit.Pixel);
				}

			}
				g.Dispose();

		}
	#endregion

	#region BitmapTools
        ///<summary>Paints a Bitmap from the Sermon Contents</summary>
        public Bitmap DrawBlackBitmap(int Width,int Heigt) {

            SolidBrush sdBrush1;
            sdBrush1 = new SolidBrush(Color.Black);
            Bitmap Localbmp =  new Bitmap(Width,Height);
            graphics = Graphics.FromImage(Localbmp);
            graphics.FillRectangle(sdBrush1, new Rectangle(0,0,Width,Height));
            return Localbmp;
		}


		public Bitmap Resizer(string dir,int Width,int Height) {
				Image ResizeImage;
				ResizeImage = Image.FromFile(dir);
				return(Resizer(ResizeImage,Width,Height));
		}

		public Bitmap Resizer(Image ResizeImage, int Width, int Height){
			Graphics ResizeGraph;
			Bitmap ResizeBMP = new Bitmap(Width,Height);

			ResizeGraph = Graphics.FromImage(ResizeBMP);
			ResizeGraph.InterpolationMode =InterpolationMode.HighQualityBicubic;
			ResizeGraph.PixelOffsetMode = PixelOffsetMode.HighQuality;
			ResizeGraph.DrawImage(ResizeImage, 0, 0, Width, Height);
			 //ResizeImage.Dispose();
		   //	ResizeGraph.
			return ResizeBMP;
		}

		public void GetVideoFrame(string Path){
			Microsoft.DirectX.AudioVideoPlayback.Video tmpvideo = null;
			
			 tmpvideo = new Microsoft.DirectX.AudioVideoPlayback.Video(Path, false);
			tmpvideo.Dispose();
		   tmpvideo = null;
		}

		public Bitmap DrawProportionalBitmap(System.Drawing.Size Size ,string Path) {
			Bitmap bm = null;


		   try{
			Image tmpImage = Image.FromFile(Path);

			bm =  DrawProportionalBitmap(Size, tmpImage);
		   } catch {}
			return bm;

		}

		public static Bitmap DrawProportionalBitmap(System.Drawing.Size Size ,Image MainImage) {
			Graphics graph;

		   //	Image MainImage = Image.FromFile(Path);
		   if(Size.Height < 1)
			Size.Height = 1;
		   if(Size.Width < 1)
			Size.Width = 1;
			
			Bitmap Canvas =  new Bitmap(Size.Width , Size.Height);
			SolidBrush sdBrush1 = new SolidBrush(Color.Black);

			graph = Graphics.FromImage(Canvas);

			graph.FillRectangle(sdBrush1, new Rectangle(0,0,Size.Width, Size.Height));
			graph.InterpolationMode =InterpolationMode.HighQualityBicubic;
			graph.PixelOffsetMode = PixelOffsetMode.HighQuality;

			Size InputSize = Tools.VideoProportions(Size,MainImage.Size);

			graph.DrawImage(MainImage, (int)((Size.Width - InputSize.Width)/2), (int)((Size.Height - InputSize.Height)/2),InputSize.Width, InputSize.Height);



			return Canvas;
		}

	#endregion

	#region Size/Position

        private void SetSizePosValues() {
            this.WindowPosX.Value = this.Location.X;
            this.WindowPosY.Value = this.Location.Y;
            this.WindowSizeX.Value = this.Size.Width;
            this.WindowSizeY.Value = this.Size.Height;
        }

		///<summary>Make the BeamBox moveable</summary>
		public void ShowMover() {
			GetScreens();
			if (this.BeamBoxAutoPosSize) {
				SizePosControl.SelectedIndex = 0;
            } else {
                SizePosControl.SelectedIndex = 1;
			}

			ResourceManager rm;
			rm = new ResourceManager("DreamBeam.Images",System.Reflection.Assembly.GetExecutingAssembly());

			TestImage.Show();
			TestImage.Size = this.Size;
			TestImage.Location = new Point (0,0);

			//TestImage.Image = Resizer((Image)rm.GetObject("Dreambeam_testbild.jpg"),this.Width,this.Height) ;


            SizePosControl.Show();
            SizePosControl.BringToFront();
        }
        ///<summary>Set the BeamBox</summary>
		public void HideMover() {
			TestImage.Hide();
			SizePosControl.Hide();

			// Save the new position to the config object
			this._MainForm.Config.BeamBoxPosX = this.Location.X;
			this._MainForm.Config.BeamBoxPosY = this.Location.Y;
			this._MainForm.Config.BeamBoxSizeX = this.Size.Width;
			this._MainForm.Config.BeamBoxSizeY = this.Size.Height;

			// Update the local display
			this._MainForm.DisplayLiveLocal.Size = this.Size;
			this._MainForm.DisplayLiveLocal.Location = this.Location;
		}


        public void GetScreens() {
            this.ScreenList.Items.Clear();
            //  if (System.Windows.Forms.Screen.AllScreens.Length>1) // If there are at least 2 monitors

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

                if(BeamBoxAutoPosSize) {
                    if(BeamBoxScreenNum < ScreenList.Items.Count) {
                        ScreenList.SelectedIndex = BeamBoxScreenNum;
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

        private void Screen_SetSettingsButton_Click(object sender, System.EventArgs e) {
            System.Drawing.Size tmpSize = this.Size;
            System.Drawing.Point tmpPoint = this.Location;

            if(ScreenList.SelectedIndex >=  0) {
                this.Bounds = System.Windows.Forms.Screen.AllScreens[ScreenList.SelectedIndex].Bounds;
                DialogResult answer = MessageBox.Show("Do you want to keep this settings?\nPress Yes to accept or No to restore.","Keep Settings?", MessageBoxButtons.YesNo);
                if (answer == DialogResult.No) {
                    this.Size = tmpSize;
                    this.Location = tmpPoint;
                } else {
                    BeamBoxScreenNum = ScreenList.SelectedIndex;
                    SetSizePosValues();
                }
            }
        }

        public void SetAutoPosition() {
            if(BeamBoxAutoPosSize) {
                int i = 0;
                foreach(System.Windows.Forms.Screen s in System.Windows.Forms.Screen.AllScreens) {
                    if(i == BeamBoxScreenNum) {
                        this.Bounds = s.Bounds;
                        SetSizePosValues();
                    }
                }
            }
        }


        private void ShowBeam_VisibleChanged(object sender, System.EventArgs e) {
			SetAutoPosition();
			if(this.useDirectX)
			{
				FXLib.Init3D(this.ShowSongPanel);
			}
        }


        private void SizePosControl_Click(object sender, System.EventArgs e) {
            if (SizePosControl.SelectedIndex == 0) {
                this.BeamBoxAutoPosSize = true;
            } else {
                this.BeamBoxAutoPosSize = false;
            }
        }


        ///<summary>Update the Window Position</summary>
        private void WindowPosX_ValueChanged(object sender, System.EventArgs e) {
            this.Location = new Point(Convert.ToInt32(this.WindowPosX.Value) ,this.Location.Y );
        }

        ///<summary>Update the Window Position</summary>
        private void WindowPosY_ValueChanged(object sender, System.EventArgs e) {
            this.Location = new Point(this.Location.X ,Convert.ToInt32(this.WindowPosY.Value) );
        }

        ///<summary>Update the Window Position</summary>
        private void button1_Click(object sender, System.EventArgs e) {

            this.WindowPosX.Value --;

        }

        ///<summary>Update the Window Position</summary>
        private void button4_Click(object sender, System.EventArgs e) {
            this.WindowPosY.Value ++;
        }

        ///<summary>Update the Window Position</summary>
        private void button2_Click(object sender, System.EventArgs e) {
            this.WindowPosX.Value ++;
        }

        ///<summary>Update the Window Position</summary>
        private void button3_Click(object sender, System.EventArgs e) {
            this.WindowPosY.Value --;
        }

        ///<summary>Update the Window Position</summary>
        private void button3_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
            this.WindowPosY.Value --;
        }

        ///<summary>Update the Window Position</summary>
        private void button8_Click(object sender, System.EventArgs e) {
            this.WindowSizeX.Value --;
        }

        ///<summary>Update the Window Position</summary>
        private void button7_Click(object sender, System.EventArgs e) {
            this.WindowSizeX.Value ++;
        }

        ///<summary>Update the Window Position</summary>
        private void button6_Click(object sender, System.EventArgs e) {
            this.WindowSizeY.Value ++;
        }

        ///<summary>Update the Window Position</summary>
        private void button5_Click(object sender, System.EventArgs e) {
            this.WindowSizeY.Value --;
        }

        ///<summary>Update the Window Position</summary>
        private void WindowSizeX_ValueChanged(object sender, System.EventArgs e) {
            if (this.WindowSizeX.Value < 350) {
                this.WindowSizeX.Value = 350;
            }
            this.Size = new Size(Convert.ToInt32(this.WindowSizeX.Value),this.Size.Height );
		}

        ///<summary>Update the Window Position</summary>
        private void WindowSizeY_ValueChanged(object sender, System.EventArgs e) {
            if (this.WindowSizeY.Value < 165) {
                this.WindowSizeY.Value = 165;
            }
            this.Size = new Size(this.Size.Width ,Convert.ToInt32(this.WindowSizeY.Value));
        }

	#region move window while drag in the client area (thanks to Yiyi Sun's Tutorial)
        /// <summary> Drag/Move the Window on Mouse Down </summary>
        private void ShowBeam_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
            if (this.TestImage.Visible == true) {
				ReleaseCapture();
				SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);

				if(this.WindowPosX.Value == this.Location.X && this.WindowPosY.Value == this.Location.Y){
					if(SizePosControl.Visible){
						SizePosControl.Hide();
					}else{
						SizePosControl.Show();
					}
				}
				this.WindowPosX.Value = this.Location.X;
				this.WindowPosY.Value = this.Location.Y;
				this.WindowSizeX.Value = this.Size.Width;
				this.WindowSizeY.Value = this.Size.Height;
			}
		}

        ///<summary>needed for drag and drop</summary>
        public const int WM_NCLBUTTONDOWN = 0xA1;
        ///<summary>needed for drag and drop</summary>
        public const int HTCAPTION = 0x2;
        [DllImportAttribute ("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute ("user32.dll")]
		public static extern bool ReleaseCapture();
	#endregion


	#endregion

	#region HideMouse

        private void ShowBeam_MouseEnter(object sender, System.EventArgs e) {
            if (this.HideMouse) {
                Cursor.Hide();
            }
        }

        private void ShowBeam_MouseLeave(object sender, System.EventArgs e) {
            if (this.HideMouse) {
                Cursor.Show();
            }
		}
	#endregion

	#region MediaStuff
        public void ShowMedia(string Path) {
			//this.StopMedia();
			PrePlaying = PlayWhat;
			PlayWhat = MediaList.GetType(Path);
			if(this.strMediaPlaying == null) {
				if(PlayWhat == "flash") {

					strMediaPlaying = MediaList.GetType(Path);
					this.FlashPlaying = true;
                    this.axShockwaveFlash.Show();
                    this.ShowSongPanel.Hide();
                    this.VideoPanel.Hide();
                    this.axShockwaveFlash.BringToFront();
                    this.axShockwaveFlash.Size = this.Size;
                    this.axShockwaveFlash.Location = new Point(0,0);
                    this.axShockwaveFlash.BGColor = "0";

                    this.axShockwaveFlash.Movie = Path;
                    this.axShockwaveFlash.Play();
                    this.axShockwaveFlash.Loop = this.LoopMedia ;

                }
				if(PlayWhat == "movie") {
                    this.VideoProblem = false;
                    strMediaPlaying = MediaList.GetType(Path);
                    axShockwaveFlash.Stop();
                    this.axShockwaveFlash.Hide();
                    this.ShowSongPanel.Hide();
                    this.VideoPanel.Show();

                    PlayBackTimer.Enabled = true;
                    if (this.video == null) {
                        try {
							this.video = new Microsoft.DirectX.AudioVideoPlayback.Video(Path,false);
							this.video.Owner = VideoPanel;
                        } catch{
                            this.VideoProblem = true;
                        }

                    } else {
						this.video.Stop();
                        try {
							//this.video.Open(Path,false);
							this.video.Dispose();
							this.video = null;
							this.video = new Microsoft.DirectX.AudioVideoPlayback.Video(Path,false);
							this.video.Owner = VideoPanel;
                        } catch{
                            this.VideoProblem = true;
                        }
                    }

					if(!VideoProblem) {
                        this.VideoPanel.Size = Tools.VideoProportions(this.Size,video.DefaultSize);
                        this.video.Size = Tools.VideoProportions(this.Size,video.DefaultSize);
                        this.VideoPanel.Location =  new Point((int)((this.Size.Width - this.VideoPanel.Size.Width)/2),(int)((this.Size.Height - this.VideoPanel.Size.Height)/2));
                        try {
                            this.video.Audio.Volume = this.AudioVolume;
                        } catch{}
						this.video.Play();
						this.VideoPlaying = true;
					}

                }
				if(PlayWhat == "image") {
                    if (video != null) {
                        video.Stop();
                        PlayBackTimer.Enabled = false;
                    }
                    StopMedia();
					this.axShockwaveFlash.Hide();
					this.VideoPanel.Hide();
					this.ShowSongPanel.Show();
					this.ShowSongPanel.Size = this.Size;
					this.ShowSongPanel.Location = new Point (0,0);

					if (this.Config.AppOperatingMode == OperatingMode.Client) {
						_MainForm.DisplayLiveClient.SetContent(new ImageContent(Path));
					} else {
						_MainForm.DisplayLiveLocal.SetContent(new ImageContent(Path));
					}
                }

            } else if (this.strMediaPlaying == "flash") {
                this.axShockwaveFlash.Play();
            } else if (this.strMediaPlaying == "movie") {
                this.video.Play();
            }
        }


		public void StopMedia() {

			if(this.FlashPlaying){
				axShockwaveFlash.Stop();
				axShockwaveFlash.Rewind();
				this.FlashPlaying = false;
				if(PrePlaying != PlayWhat){
					axShockwaveFlash.Hide();
				}
			}
			if(PlayWhat != "image"){
				if(PrePlaying != PlayWhat){
					this.bmp = this.DrawBlackBitmap(ShowSongPanel.Size.Width,ShowSongPanel.Size.Height);
					this.DrawWhat = 777;
					this.GDIDraw();
				}
			}



			if(this.VideoPlaying){
				video.Stop();
				PlayBackTimer.Enabled = false;
				VideoPlaying = false;
				if(PrePlaying != PlayWhat){
					this.VideoPanel.Hide();
				}
            }


            strMediaPlaying = null;
		}

        public void PauseMedia() {
            if(strMediaPlaying == "flash") {
                axShockwaveFlash.Stop();
            }
            if(strMediaPlaying == "movie") {
                try {
                    video.Pause();
                } catch{}
            }
    }


    private void PlayBackTimer_Tick(object sender, System.EventArgs e) {
            try {
                if (this.LoopMedia  && this.video.CurrentPosition == this.video.Duration) {
                    this.video.CurrentPosition = 0;
                }
            } catch{}
        }


	#endregion

	#region Repaints
    private void ShowSongPanel_Paint(object sender, System.Windows.Forms.PaintEventArgs e) {

            if(strMediaPlaying == null)
                this.GDIDraw();
        }

	#endregion
		
		private void AlphaTimer_Tick(object sender, System.EventArgs e) {

			if(AlphaOpacity<255){

				if(AlphaOpacity+ Config.BlendSpeed > 255) AlphaOpacity = (byte)255;
				else AlphaOpacity = (byte)(AlphaOpacity+Config.BlendSpeed);
				
				AlphaForm.SetBitmap(bmp,AlphaOpacity);
			}else{
				AlphaTimer.Stop();
				Graphics g = Graphics.FromHwnd(ShowSongPanel.Handle);
				g.DrawImage(memoryBitmap, new Rectangle(0,0,this.Width, this.Height), 0,0,this.Width, this.Height, GraphicsUnit.Pixel);
				AlphaForm.Hide();
			}

		}


	}
}
