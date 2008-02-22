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

		///<summary>Graphics</summary>
		public Graphics graphics;

		///<summary>The final Bitmap for the BeamBox </summary>
		public Bitmap memoryBitmap = null;

		///<summary>Use AlphaBlending Transitions </summary>
		public bool transit = false;

		///<summary>Temporary bitmap</summary>
		public Bitmap bmp = null;

		///<summary>User Direct X? (not used yet)</summary>
		public bool useDirectX = false;

		public bool HideMouse = false;

		public ImageList MediaList = new ImageList();
		string PlayWhat = "";

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

		//		DirectShowLib Lib = new DirectShowLib();
		private FXLib FXLib = new FXLib();

		public MediaOperations liveMedia = null;
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
		private System.Timers.Timer AlphaTimer2;
		private System.Windows.Forms.Timer AlphaTimer;
		#endregion


		/// <summary> Initialize the Class</summary>
		public ShowBeam() {
			InitializeComponent();
			SetSizePosValues();
			//   BackgroundColor =
			//Color.FromArgb(10,11,12);

			ShowSongPanel.Location = new Point(0, 0);
			ShowSongPanel.Size = this.Size;

			axShockwaveFlash.Hide();
			VideoPanel.Hide();
			ShowSongPanel.Show();
		}

		/// <summary>
		/// Dispose components
		/// </summary>
		protected override void Dispose(bool disposing) {
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code
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
			this.AlphaTimer2 = new System.Timers.Timer();
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
			((System.ComponentModel.ISupportInitialize)(this.AlphaTimer2)).BeginInit();
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
			this.ShowSongPanel.BackColor = System.Drawing.Color.Transparent;
			this.ShowSongPanel.Location = new System.Drawing.Point(48, 232);
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
			// AlphaTimer2
			// 
			this.AlphaTimer2.Interval = 1;
			this.AlphaTimer2.SynchronizingObject = this;
			this.AlphaTimer2.Elapsed += new System.Timers.ElapsedEventHandler(this.AlphaTimer2_Elapsed);
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
			this.SizeChanged += new System.EventHandler(this.ShowBeam_SizeChanged);
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
			((System.ComponentModel.ISupportInitialize)(this.AlphaTimer2)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		#region DrawOnBeamBox

		public void GDIDraw(Bitmap b) {
			bmp = b;

			// It is important for the SongShowPanel to have a "BackColor"
			// property of "Transparent" or else as the background is being
			// painted to the non-transparent color we will see the screen
			// flicker. When set to transparent, the OnPaintBackground function
			// call for the control is suppressed.
			Graphics g = Graphics.FromHwnd(ShowSongPanel.Handle);
			if (strMediaPlaying != null) {
				// We capture the last frame of the flash or movie and transition from it to the song bitmap.
				ScreenCapture sc = new ScreenCapture();
				memoryBitmap = new Bitmap(sc.CaptureWindow(this.Handle));
			} else if (memoryBitmap == null) {
				memoryBitmap = b;
			}

			g.SmoothingMode = SmoothingMode.HighQuality;
			g.InterpolationMode = InterpolationMode.HighQualityBicubic;
			g.PixelOffsetMode = PixelOffsetMode.HighQuality;

			if (strMediaPlaying == "flash") {
				if (this.axShockwaveFlash != null) this.axShockwaveFlash.Stop();
				strMediaPlaying = null;
			} else if (strMediaPlaying == "movie") {
				if (this.video != null) this.video.Stop();
				PlayBackTimer.Enabled = false;
				strMediaPlaying = null;
			}

			axShockwaveFlash.Hide();
			VideoPanel.Hide();
			ShowSongPanel.Show();

			//Draw Image on Screen, make a Alphablending if transit == true
			if (transit) {
				// We set the new bitmap before making the form visible or else
				// it shows up with the old bitmap for a fraction of a second
				// and it looks like it flickers.
				AlphaForm.SetBitmap(bmp, 0);
				AlphaForm.setpos(Location.X, Location.Y);
				AlphaForm.Size = this.Size;
				AlphaForm.Visible = true;
				_MainForm.BringToFront();

				g.DrawImage(memoryBitmap, 0, 0, this.Width, this.Height);
				AlphaOpacity = 0;
				//AlphaTimer.Start();
				AlphaTimer2.Start();

			} else {
				memoryBitmap = bmp;
				g.DrawImage(bmp, new Rectangle(0, 0, this.Width, this.Height), 0, 0, this.Width, this.Height, GraphicsUnit.Pixel);
			}

			g.Dispose();
		}

		#endregion

		#region BitmapTools
		///<summary>Paints a Bitmap from the Sermon Contents</summary>
		public Bitmap DrawBlackBitmap(int Width, int Heigt) {

			SolidBrush sdBrush1;
			sdBrush1 = new SolidBrush(Color.Black);
			Bitmap Localbmp = new Bitmap(Width, Height);
			graphics = Graphics.FromImage(Localbmp);
			graphics.FillRectangle(sdBrush1, new Rectangle(0, 0, Width, Height));
			return Localbmp;
		}


		public Bitmap Resizer(string dir, int Width, int Height) {
			Image ResizeImage;
			ResizeImage = Image.FromFile(dir);
			return (Resizer(ResizeImage, Width, Height));
		}

		public Bitmap Resizer(Image ResizeImage, int Width, int Height) {
			Graphics ResizeGraph;
			Bitmap ResizeBMP = new Bitmap(Width, Height);

			ResizeGraph = Graphics.FromImage(ResizeBMP);
			ResizeGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
			ResizeGraph.PixelOffsetMode = PixelOffsetMode.HighQuality;
			ResizeGraph.DrawImage(ResizeImage, 0, 0, Width, Height);
			//ResizeImage.Dispose();
			//	ResizeGraph.
			return ResizeBMP;
		}

		public Bitmap DrawProportionalBitmap(System.Drawing.Size Size, string Path) {
			Bitmap bm = null;

			try {
				Image tmpImage = Image.FromFile(Path);

				bm = DrawProportionalBitmap(Size, tmpImage);
			} catch { }

			return bm;

		}

		public static Bitmap DrawProportionalBitmap(System.Drawing.Size Size, Image MainImage) {
			Graphics graph;

			//	Image MainImage = Image.FromFile(Path);
			if (Size.Height < 1)
				Size.Height = 1;
			if (Size.Width < 1)
				Size.Width = 1;

			Bitmap Canvas = new Bitmap(Size.Width, Size.Height);
			SolidBrush sdBrush1 = new SolidBrush(Color.Black);

			graph = Graphics.FromImage(Canvas);

			graph.FillRectangle(sdBrush1, new Rectangle(0, 0, Size.Width, Size.Height));
			graph.InterpolationMode = InterpolationMode.HighQualityBicubic;
			graph.PixelOffsetMode = PixelOffsetMode.HighQuality;

			Size InputSize = Tools.VideoProportions(Size, MainImage.Size);

			graph.DrawImage(MainImage, (int)((Size.Width - InputSize.Width) / 2), (int)((Size.Height - InputSize.Height) / 2), InputSize.Width, InputSize.Height);



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

			//ResourceManager rm;
			//rm = new ResourceManager("DreamBeam.Images",System.Reflection.Assembly.GetExecutingAssembly());

			TestImage.Show();
			TestImage.Size = this.Size;
			TestImage.Location = new Point(0, 0);

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
			foreach (System.Windows.Forms.Screen s in System.Windows.Forms.Screen.AllScreens) {
				if (s == System.Windows.Forms.Screen.PrimaryScreen) {
					this.ScreenList.Items.Add("Primary Screen");
					// secondary = s;

				} else {
					this.ScreenList.Items.Add("Secundary Screen " + i.ToString());
					i++;
					if (FirstSecundary == -1) {
						FirstSecundary = i;
					}
				}
				//if no Secundary Found take the Primary (in this case, the only one found)
				if (FirstSecundary == -1) {
					FirstSecundary = 0;
				}

				if (BeamBoxAutoPosSize) {
					if (BeamBoxScreenNum < ScreenList.Items.Count) {
						ScreenList.SelectedIndex = BeamBoxScreenNum;
					}
				} else {
					ScreenList.SelectedIndex = FirstSecundary;
				}

			}
		}

		private void ScreenList_SelectedIndexChanged(object sender, System.EventArgs e) {
			if (ScreenList.SelectedIndex >= 0) {
				AutoPosLabelX.Text = "X: " + System.Windows.Forms.Screen.AllScreens[ScreenList.SelectedIndex].Bounds.X.ToString();
				AutoPosLabelY.Text = " Y:" + System.Windows.Forms.Screen.AllScreens[ScreenList.SelectedIndex].Bounds.Y.ToString();
				AutoSizeLabelW.Text = "Width: " + System.Windows.Forms.Screen.AllScreens[ScreenList.SelectedIndex].Bounds.Width.ToString();
				AutoSizeLabelH.Text = "Height: " + System.Windows.Forms.Screen.AllScreens[ScreenList.SelectedIndex].Bounds.Height.ToString();
			}
		}

		private void Screen_SetSettingsButton_Click(object sender, System.EventArgs e) {
			System.Drawing.Size tmpSize = this.Size;
			System.Drawing.Point tmpPoint = this.Location;

			if (ScreenList.SelectedIndex >= 0) {
				this.Bounds = System.Windows.Forms.Screen.AllScreens[ScreenList.SelectedIndex].Bounds;
				DialogResult answer = MessageBox.Show("Do you want to keep this settings?\nPress Yes to accept or No to restore.", "Keep Settings?", MessageBoxButtons.YesNo);
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
			if (BeamBoxAutoPosSize) {
				int i = 0;
				foreach (System.Windows.Forms.Screen s in System.Windows.Forms.Screen.AllScreens) {
					if (i == BeamBoxScreenNum) {
						this.Bounds = s.Bounds;
						SetSizePosValues();
					}
				}
			}
		}


		private void ShowBeam_VisibleChanged(object sender, System.EventArgs e) {
			SetAutoPosition();
			if (this.useDirectX) {
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

		private void ShowBeam_SizeChanged(object sender, System.EventArgs e) {
			this.ShowSongPanel.Size = this.Size;
		}


		///<summary>Update the Window Position</summary>
		private void WindowPosX_ValueChanged(object sender, System.EventArgs e) {
			this.Location = new Point(Convert.ToInt32(this.WindowPosX.Value), this.Location.Y);
		}

		///<summary>Update the Window Position</summary>
		private void WindowPosY_ValueChanged(object sender, System.EventArgs e) {
			this.Location = new Point(this.Location.X, Convert.ToInt32(this.WindowPosY.Value));
		}

		///<summary>Update the Window Position</summary>
		private void button1_Click(object sender, System.EventArgs e) {

			this.WindowPosX.Value--;

		}

		///<summary>Update the Window Position</summary>
		private void button4_Click(object sender, System.EventArgs e) {
			this.WindowPosY.Value++;
		}

		///<summary>Update the Window Position</summary>
		private void button2_Click(object sender, System.EventArgs e) {
			this.WindowPosX.Value++;
		}

		///<summary>Update the Window Position</summary>
		private void button3_Click(object sender, System.EventArgs e) {
			this.WindowPosY.Value--;
		}

		///<summary>Update the Window Position</summary>
		private void button3_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			this.WindowPosY.Value--;
		}

		///<summary>Update the Window Position</summary>
		private void button8_Click(object sender, System.EventArgs e) {
			this.WindowSizeX.Value--;
		}

		///<summary>Update the Window Position</summary>
		private void button7_Click(object sender, System.EventArgs e) {
			this.WindowSizeX.Value++;
		}

		///<summary>Update the Window Position</summary>
		private void button6_Click(object sender, System.EventArgs e) {
			this.WindowSizeY.Value++;
		}

		///<summary>Update the Window Position</summary>
		private void button5_Click(object sender, System.EventArgs e) {
			this.WindowSizeY.Value--;
		}

		///<summary>Update the Window Position</summary>
		private void WindowSizeX_ValueChanged(object sender, System.EventArgs e) {
			if (this.WindowSizeX.Value < 350) {
				this.WindowSizeX.Value = 350;
			}
			this.Size = new Size(Convert.ToInt32(this.WindowSizeX.Value), this.Size.Height);
		}

		///<summary>Update the Window Position</summary>
		private void WindowSizeY_ValueChanged(object sender, System.EventArgs e) {
			if (this.WindowSizeY.Value < 165) {
				this.WindowSizeY.Value = 165;
			}
			this.Size = new Size(this.Size.Width, Convert.ToInt32(this.WindowSizeY.Value));
		}

		#region move window while drag in the client area (thanks to Yiyi Sun's Tutorial)
		/// <summary> Drag/Move the Window on Mouse Down </summary>
		private void ShowBeam_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			if (this.TestImage.Visible == true) {
				ReleaseCapture();
				SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);

				if (this.WindowPosX.Value == this.Location.X && this.WindowPosY.Value == this.Location.Y) {
					if (SizePosControl.Visible) {
						SizePosControl.Hide();
					} else {
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
		[DllImportAttribute("user32.dll")]
		public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
		[DllImportAttribute("user32.dll")]
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
			PlayWhat = MediaList.GetType(Path);
			if (this.strMediaPlaying == null) {

				if (PlayWhat == "flash") {
					strMediaPlaying = MediaList.GetType(Path);
					this.axShockwaveFlash.Show();
					this.ShowSongPanel.Hide();
					this.VideoPanel.Hide();
					this.axShockwaveFlash.BringToFront();
					this.axShockwaveFlash.Size = this.Size;
					this.axShockwaveFlash.Location = new Point(0, 0);
					this.axShockwaveFlash.BGColor = "0";

					this.axShockwaveFlash.Movie = Path;
					this.axShockwaveFlash.Play();
					this.axShockwaveFlash.Loop = this.LoopMedia;

					this.liveMedia = new MediaFlash(axShockwaveFlash);
				} else if (PlayWhat == "movie") {
					this.VideoProblem = false;
					strMediaPlaying = MediaList.GetType(Path);
					axShockwaveFlash.Stop();
					this.axShockwaveFlash.Hide();
					this.ShowSongPanel.Hide();

					// We must paint the whole screen black in case the movie is
					// not the same proportion as the screen, otherwise instead
					// of black bars we end up with garbage where the bars
					// should be.
					Graphics g = Graphics.FromHwnd(this.Handle);
					g.DrawImage(this.DrawBlackBitmap(this.Width, this.Height), 0, 0, this.Width, this.Height);
					this.VideoPanel.Show();

					PlayBackTimer.Enabled = true;
					if (this.video == null) {
						try {
							this.video = new Microsoft.DirectX.AudioVideoPlayback.Video(Path, false);
							this.video.Owner = VideoPanel;
						} catch {
							this.VideoProblem = true;
						}

					} else {
						this.video.Stop();
						try {
							//this.video.Open(Path,false);
							this.video.Dispose();
							this.video = null;
							this.video = new Microsoft.DirectX.AudioVideoPlayback.Video(Path, false);
							this.video.Owner = VideoPanel;
						} catch {
							this.VideoProblem = true;
						}
					}

					if (!VideoProblem) {
						this.VideoPanel.Size = Tools.VideoProportions(this.Size, video.DefaultSize);
						this.video.Size = Tools.VideoProportions(this.Size, video.DefaultSize);
						this.VideoPanel.Location = new Point((int)((this.Size.Width - this.VideoPanel.Size.Width) / 2), (int)((this.Size.Height - this.VideoPanel.Size.Height) / 2));
						try {
							this.video.Audio.Volume = this.AudioVolume;
						} catch { }
						this.video.Play();
					}

					this.liveMedia = new MediaMovie(video);

				} else if (PlayWhat == "image") {
					StopMedia();
					this.ShowSongPanel.Show();
					this.ShowSongPanel.Size = this.Size;
					this.ShowSongPanel.Location = new Point(0, 0);

					if (this.Config.AppOperatingMode == OperatingMode.Client) {
						_MainForm.DisplayLiveClient.SetContent(new ImageContent(Path));
					} else {
						_MainForm.DisplayLiveLocal.SetContent(new ImageContent(Path));
					}

					this.liveMedia = null;
				}

			} else if (this.strMediaPlaying == "flash") {
				this.axShockwaveFlash.Play();
			} else if (this.strMediaPlaying == "movie") {
				this.video.Play();
			}
		}


		public void StopMedia() {
			if (this.liveMedia == null) return;

			this.liveMedia.Stop();
			ShowSongPanel.Show();
			axShockwaveFlash.Hide();
			VideoPanel.Hide();

			this.bmp = this.DrawBlackBitmap(ShowSongPanel.Size.Width, ShowSongPanel.Size.Height);
			this.GDIDraw(bmp);

			PlayBackTimer.Enabled = false;
			strMediaPlaying = null;
		}

		private void PlayBackTimer_Tick(object sender, System.EventArgs e) {
			try {
				if (this.LoopMedia && this.video.CurrentPosition == this.video.Duration) {
					this.video.CurrentPosition = 0;
				}
			} catch { }
		}
		#endregion

		#region Repaints

		protected override void OnPaintBackground(PaintEventArgs e) {
			// Prevent the background from being painted in order to avoid flicker.
		}

		private void ShowSongPanel_Paint(object sender, System.Windows.Forms.PaintEventArgs e) {
			//            if (strMediaPlaying == null)
			//                this.GDIDraw();

			if (memoryBitmap != null) {
				e.Graphics.DrawImage(memoryBitmap, 0, 0, this.Width, this.Height);
			} else {
				// Fill panel with the background color
				memoryBitmap = new Bitmap(this.Width, this.Height);
				Graphics g = Graphics.FromImage(memoryBitmap);
				g.Clear(_MainForm.Config.BackgroundColor);
				e.Graphics.DrawImage(memoryBitmap, 0, 0, this.Width, this.Height);
			}
		}
		#endregion

		private void AlphaTimer_Tick(object sender, System.EventArgs e) {
			if (AlphaOpacity < 255) {

				if (AlphaOpacity + Config.BlendSpeed > 255) AlphaOpacity = (byte)255;
				else AlphaOpacity = (byte)(AlphaOpacity + Config.BlendSpeed);

				AlphaForm.SetBitmap(bmp, AlphaOpacity);
			} else {
				AlphaTimer.Stop();
				Graphics g = Graphics.FromHwnd(ShowSongPanel.Handle);
				memoryBitmap = bmp;
				g.DrawImage(memoryBitmap, new Rectangle(0, 0, this.Width, this.Height), 0, 0, this.Width, this.Height, GraphicsUnit.Pixel);
				AlphaForm.Hide();
			}
		}

		private void AlphaTimer2_Elapsed(object sender, System.Timers.ElapsedEventArgs e) {
			if (AlphaOpacity < 255) {

				if (AlphaOpacity + Config.BlendSpeed > 255) AlphaOpacity = (byte)255;
				else AlphaOpacity = (byte)(AlphaOpacity + Config.BlendSpeed);

				AlphaForm.SetBitmap(bmp, AlphaOpacity);
			} else {
				AlphaTimer2.Stop();
				Graphics g = Graphics.FromHwnd(ShowSongPanel.Handle);
				memoryBitmap = bmp;
				g.DrawImage(memoryBitmap, new Rectangle(0, 0, this.Width, this.Height), 0, 0, this.Width, this.Height, GraphicsUnit.Pixel);
				AlphaForm.Hide();
			}
		}


	}
}
