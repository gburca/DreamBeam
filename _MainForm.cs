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

*/

using System;
using System.IO;
using System.Threading;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Reflection;
using System.Globalization;
using System.Xml;
using Rilling.Common.UI.Forms;


namespace DreamBeam
{

	/// <summary>
	/// The Main DreamBeam Window with most of it's functions
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{

		#region Var and Obj Declarations

			#region Own Vars and Object
					///<summary> </summary>
					public string version = "0.32";

					///<summary> </summary>
					public Song[] SongArray = new Song[10];

					///<summary> </summary>
					private Bitmap memoryBitmap = null;

					///<summary> </summary>
					private ShowBeam ShowBeam = new ShowBeam();

					///<summary> </summary>
					private Song Song = new Song();

					///<summary> </summary>
					private About About = new About();

					///<summary> </summary>
					private Options Options = new Options();

					///<summary> </summary>
					public Config Config = new Config();

					///<summary> </summary>
					private int selectedTab = 0;

					///<summary> </summary>
					public int SongCount = 0;

					///<summary> </summary>
					public int Song_Edit_Box = 2;

					///<summary> </summary>
					public bool beamshowed =false;

					///<summary> </summary>
					private MyBalloonWindow HelpBalloon = new MyBalloonWindow();

					///<summary> </summary>
					private Splash Splash = null;

					///<summary> </summary>
					private bool UpdatePreview = false;
					private bool TextTyped = false;

					///<summery> </summary>

					 private LogFile LogFile;

					 private bool AllowPreview = true;

					static Thread Thread_BgImage = null;
					private String g_Bg_Directory = null;

					///<summer>Directory for Presentation </summary>
					private DirectoryInfo folder;


					///<summer>Driveletters </summary>
					private static string driveLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

					private System.ComponentModel.IContainer components;


					public string strMediaPath = "";


					//Drag&Drop Vars
					private int indexOfItemUnderMouseToDrag;
					private int indexOfItemUnderMouseToDrop;
					private Rectangle dragBoxFromMouseDown;
					private Point screenOffset;
					private Cursor MyNoDropCursor;
					private Cursor MyNormalCursor;

			#endregion

			#region Toolbars and others Declarations

				private TD.SandBar.SandBarManager ToolBars_sandBarManager1;
				private TD.SandBar.ToolBarContainer ToolBars_leftSandBarDock;
				private TD.SandBar.ToolBarContainer ToolBars_bottomSandBarDock;
				private TD.SandBar.ToolBarContainer ToolBars_topSandBarDock;

				private System.Windows.Forms.TabControl tabControl1;
				private OPaC.Themed.Forms.TabPage tabPage1;
				private OPaC.Themed.Forms.TabPage tabPage0;
				private OPaC.Themed.Forms.TabPage tabPage2;
				private OPaC.Themed.Forms.TabPage tabPage3;


				#region Menu Bar
					private TD.SandBar.MenuBar ToolBars_MenuBar;
					private TD.SandBar.MenuBarItem ToolBars_MenuBar_Song;
						private TD.SandBar.MenuButtonItem ToolBars_MenuBar_Song_New;
						private TD.SandBar.MenuButtonItem ToolBars_MenuBar_Song_SaveAs;
						private TD.SandBar.MenuButtonItem ToolBars_MenuBar_Song_Rename;
						private TD.SandBar.MenuButtonItem ToolBars_MenuBar_Song_Exit;
					private TD.SandBar.MenuBarItem ToolBars_MenuBar_Edit;
						private TD.SandBar.MenuButtonItem ToolBars_MenuBar_Edit_Options;
					private TD.SandBar.MenuBarItem ToolBars_MenuBar_View;
					private TD.SandBar.MenuBarItem ToolBars_MenuBar_Help;
						private TD.SandBar.MenuButtonItem HelpIntro;
						private TD.SandBar.MenuButtonItem HelpBeamBox;
						private TD.SandBar.MenuButtonItem HelpShowSongs;
						private TD.SandBar.MenuButtonItem HelpEditSongs;
						private TD.SandBar.MenuButtonItem AboutButton;
				#endregion

				#region Main Toolbar
					private TD.SandBar.ToolBar ToolBars_MainToolbar;
					private TD.SandBar.ButtonItem ToolBars_MainToolbar_ShowBeamBox;
					private TD.SandBar.ButtonItem ToolBars_MainToolbar_SizePosition;
					private TD.SandBar.ButtonItem ToolBars_MainToolbar_SaveSong;
					private TD.SandBar.ButtonItem ToolBars_MainToolbar_HideBG;
					private TD.SandBar.ButtonItem ToolBars_MainToolbar_HideText;
				#endregion

				#region Component Bar
					private TD.SandBar.ToolBar ToolBars_ComponentBar;
						private TD.SandBar.ButtonItem ShowSongs_Button;
						private TD.SandBar.ButtonItem EditSongs_Button;
				#endregion

				#region Right Docks

					#region SongList
						private System.Windows.Forms.ListBox RightDocks_SongList;
						private System.Windows.Forms.TextBox RightDocks_SongListSearch;
						private System.Windows.Forms.Button btnRightDocks_SongList2PlayList;
						private System.Windows.Forms.Button btnRightDocks_SongListDelete;
						private System.Windows.Forms.Button btnRightDocks_SongListLoad;
					#endregion

					#region PlayList
						private System.Windows.Forms.ListBox RightDocks_PlayList;
						private System.Windows.Forms.Button RightDocks_PlayList_Load_Button;
						private System.Windows.Forms.Button RightDocks_PlayList_Remove_Button;
						private System.Windows.Forms.Button RightDocks_PlayList_Up_Button;
						private System.Windows.Forms.Button RightDocks_PlayList_Down_Button;
					#endregion

					#region SearchList
						private System.Windows.Forms.TextBox RightDocks_Search_InputBox;
						private System.Windows.Forms.ListBox RightDocks_Search_ListBox;
						private System.Windows.Forms.Panel RightDocks_SearchBar_TopPanel;
						private System.Windows.Forms.ComboBox RightDocks_Search_DropDown;
						private System.Windows.Forms.RadioButton RightDocks_TopPanel_Search_RadioButton1;
						private System.Windows.Forms.RadioButton RightDocks_TopPanel_Search_RadioButton2;
						private System.Windows.Forms.Panel RightDocks_TopPanel_Search_ButtonPanel;
						private System.Windows.Forms.Button RightDocks_Search_LoadButton;
						private System.Windows.Forms.Button RightDocks_Search_PlaylistButton;
						private System.Windows.Forms.Button RightDocks_Search_SearchButton;
					#endregion

					#region ImageBox
						private Controls.Development.ImageListBox RightDocks_ImageListBox;
						private System.Windows.Forms.ImageList RightDocks_imageList;

						// Context Menu
						private System.Windows.Forms.ContextMenu ImageContext;
						private System.Windows.Forms.MenuItem ImageContextItemManage;
						private System.Windows.Forms.MenuItem ImageContextItemReload;

						private System.Windows.Forms.ComboBox RightDocks_FolderDropdown;
					#endregion
				#endregion

				private System.Windows.Forms.StatusBar statusBar;
				private System.Windows.Forms.StatusBarPanel StatusPanel;

			#endregion

			#region Edit Songs_Declarations

				// The Dialogs
				private System.Windows.Forms.FontDialog SongEdit_fontDialog;
				private System.Windows.Forms.ColorDialog SongEdit_OutlineColorDialog;
				private System.Windows.Forms.ColorDialog SongEdit_TextColorDialog;

				//the Panels
				private System.Windows.Forms.Panel SongEdit_InputFieldPanelTop;
				private System.Windows.Forms.Panel SongEdit_InputFieldPanelButtom;
				private System.Windows.Forms.Panel SongEdit_InputFieldPanelMid;
				private System.Windows.Forms.Panel SongEdit_InputPanelTopMenu;
				private System.Windows.Forms.Panel SongEdit_InputPanelTopBelowMenu;
				private System.Windows.Forms.Panel SongEdit_RightPanel;
				private System.Windows.Forms.Panel SongEdit_InputFieldMenuPanelMid;
				private System.Windows.Forms.Panel SongEdit_InputFieldBelowMenuPanelMid;
				private System.Windows.Forms.Panel SongEdit_InputFieldMenuPanelButtom;
				private System.Windows.Forms.Panel SongEdit_InputFieldBelowMenuPanelButtom;
				private System.Windows.Forms.Panel SongEdit_InputFieldBelowMenuPane2lMid;

				//The ToolBars
				private TD.SandBar.ToolBar SongEdit_TopTextToolBar;
				private TD.SandBar.ToolBar SongEdit_MidTextToolBar;
				private TD.SandBar.ToolBar SongEdit_BottomTextToolBar;

				// The TextBoxes
				private System.Windows.Forms.TextBox SongEdit_Mid_TextBox;
				private System.Windows.Forms.TextBox SongEdit_Header_TextBox;
				private System.Windows.Forms.TextBox Footer_TextBox;

				//The Toolbar Buttons
				private TD.SandBar.ButtonItem SongEdit_ButtonTopFont;
				private TD.SandBar.ButtonItem SongEdit_ButtonTopColor;
				private TD.SandBar.ButtonItem SongEdit_ButtonTopTextOutline;
				private TD.SandBar.ButtonItem SongEdit_ButtonTopOutlineColor;
				private TD.SandBar.ButtonItem SongEdit_ButtonMidFont;
				private TD.SandBar.ButtonItem SongEdit_ButtonMidColor;
				private TD.SandBar.ButtonItem SongEdit_ButtonMidTextOutline;
				private TD.SandBar.ButtonItem SongEdit_ButtonMidOutlineColor;
				private TD.SandBar.ButtonItem SongEdit_ButtonBottomFont;
				private TD.SandBar.ButtonItem SongEdit_ButtonBottomColor;
				private TD.SandBar.ButtonItem SongEdit_ButtonBottomTextOutline;
				private TD.SandBar.ButtonItem SongEdit_ButtonBottomOutlineColor;

				// the Panels with the Position Fields
				private System.Windows.Forms.Panel SongEdit_BigInputFieldPanel;
				private System.Windows.Forms.Panel SongEdit_TopPos_Panel;
				private System.Windows.Forms.NumericUpDown SongEdit_TopPosY_UpDown;
				private System.Windows.Forms.NumericUpDown SongEdit_TopPosX_UpDown;
				private System.Windows.Forms.Label SongEdit_TopPosY_Label;
				private System.Windows.Forms.Label SongEdit_TopPosX_Label;
				private System.Windows.Forms.Panel SongEdit_MidPos_Panel;
				private System.Windows.Forms.Label SongEdit_MidPosX_Label;
				private System.Windows.Forms.NumericUpDown SongEdit_MidPosX_UpDown;
				private System.Windows.Forms.NumericUpDown SongEdit_MidPosY_UpDown;
				private System.Windows.Forms.Label SongEdit_MidPosY_Label;
				private System.Windows.Forms.Panel SongEdit_BottomPos_Panel;
				private System.Windows.Forms.Label SongEdit_BottomPosX_Label;
				private System.Windows.Forms.NumericUpDown SongEdit_BottomPosX_UpDown;
				private System.Windows.Forms.NumericUpDown SongEdit_BottomPosY_UpDown;
				private System.Windows.Forms.Label SongEdit_BottomPosY_Label;

				//AutoPosition CheckBoxes
				private System.Windows.Forms.CheckBox SongEdit_Mid_AutoPos_CheckBox;
				private System.Windows.Forms.CheckBox SongEdit_Bottom_AutoPos_CheckBox;
				private System.Windows.Forms.CheckBox SongEdit_Top_AutoPos_CheckBox;

				private System.Windows.Forms.Label SongEdit_Header_Title;
				private System.Windows.Forms.Label SongEdit_Header_Verses;
				private System.Windows.Forms.Label SongEdit_Header_Author;

				// Right Panel
//				private System.Windows.Forms.Panel SongEdit_Preview_Panel;
				private System.Windows.Forms.Button SongEdit_PreviewStropheUp_Button;
				private System.Windows.Forms.Button SongEdit_PreviewStropheDown_Button;
				private System.Windows.Forms.Panel SongEdit_BG_Panel;
				private System.Windows.Forms.Label SongEdit_BG_DecscriptionLabel;
				private System.Windows.Forms.Label SongEdit_BG_Label;
				private System.Windows.Forms.Button SongEdit_UpdateBeamBox_Button;
		#endregion

			#region Show Songs Declarations

				private Lister.ListEx SongShow_StropheList_ListEx;

				//Right Panel
				private System.Windows.Forms.Panel SongShow_Right_Panel;
				private System.Windows.Forms.Panel SongShow_Preview_Panel;
				private System.Windows.Forms.Panel SongShow_BG_Panel;
					private OPaC.Themed.Forms.Label SongShow_BG_Label;
					private System.Windows.Forms.CheckBox SongShow_BG_CheckBox;

				private System.Windows.Forms.Button SongShow_ToBeamBox_button;

			#endregion

			#region SermonTools
				string[] BibleBooks = new string[2];
				private AxACTIVEDIATHEKELib.AxActiveDiatheke Diatheke;
				private TD.SandBar.ButtonItem Sermon_Button;
				private DocumentManager.DocumentManager Sermon_DocManager;
				private System.Windows.Forms.TabControl Sermon_TabControl;
				private string Sermon_BibleLang = "en";
				private bool Sermon_ShowBibleTranslation = false;
				private System.Windows.Forms.Button Sermon_BeamBox_Button;
				private System.Windows.Forms.Label Sermon_Books_Label;
				private System.Windows.Forms.Label Sermon_Translation_Label;
				private System.Windows.Forms.Label Sermon_Verse_Labe;
				private System.Windows.Forms.Panel Sermon_LeftToolBar_Panel;
				private System.Windows.Forms.Panel Sermon_LeftDoc_Panel;
				private System.Windows.Forms.Panel Sermon_LeftBottom_Panel;
				private TD.SandBar.ButtonItem Sermon_ToolBar_Font_Button;
				private TD.SandBar.ButtonItem Sermon_ToolBar_Color_Button;
				private TD.SandBar.ButtonItem Sermon_ToolBar_Outline_Button;
				private TD.SandBar.ButtonItem Sermon_ToolBar_OutlineColor_Button;
				private bool SwordProject_Found = false;
				private System.Windows.Forms.ListBox Sermon_BookList;
				private System.Windows.Forms.ComboBox Sermon_Books;
				private System.Windows.Forms.Panel Sermon_LeftPanel;
				private TD.SandBar.ToolBar Sermon_ToolBar;
				private TD.SandBar.ButtonItem Sermon_ToolBar_NewDoc_Button;
				private System.Windows.Forms.ListBox Sermon_Testament_ListBox;
				private System.Windows.Forms.TextBox Sermon_BibleKey;
			#endregion
		private TD.SandBar.MenuButtonItem HelpOptions;

		#endregion
		private TD.SandBar.MenuButtonItem HelpTextTool;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Timer PreviewUpdateTimer;
		private System.Windows.Forms.Timer TextTypedTimer;
		private System.Windows.Forms.Panel RightDocks_SongList_ButtonPanel;
		private System.Windows.Forms.Panel RightDocks_Songlist_SearchPanel;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel RightDocks_TopPanel_PlayList_Button_Panel;
		private System.Windows.Forms.TabPage tabPage4;
		private TD.SandDock.SandDockManager sandDockManager1;
		private TD.SandDock.DockContainer leftSandDock;
		private TD.SandDock.DockContainer rightSandDock;
		private TD.SandDock.DockContainer bottomSandDock;
		private TD.SandDock.DockContainer topSandDock;
		private TD.SandDock.DockControl RightDocks_TopPanel_Songs;
		private TD.SandDock.DockControl RightDocks_BottomPanel_Backgrounds;
		private TD.SandDock.DockControl RightDocks_TopPanel_PlayList;
		private TD.SandDock.DockControl RightDocks_TopPanel_Search;
		private System.Windows.Forms.Panel RightDocks_BottomPanel2_TopPanel;
		private System.Windows.Forms.GroupBox SongEdit_TextAlign;
		private System.Windows.Forms.RadioButton radioButton1;
		private System.Windows.Forms.RadioButton radioButton2;
		private TD.SandBar.ButtonItem Presentation_Button;
		private TD.SandDock.DockControl RightDocks_BottomPanel_Media;
		private DragNDrop.DragAndDropListView RightDocks_BottomPanel_MediaListView;
		private System.Windows.Forms.Button RightDocks_BottomPanel_Media_FolderSelectButton;

		public System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.ImageList imageList_Folders;
		private System.Windows.Forms.Panel Presentation_FadePanel;
		private System.Windows.Forms.Button RightDocks_BottomPanel_Media_FadePanelButton;
		private System.Windows.Forms.ListView Presentation_Fade_ListView;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.PictureBox Presentation_Fade_preview;
		private System.Windows.Forms.Button Presentation_Fade_ToPlaylist_Button;
		private System.Windows.Forms.ImageList Presentation_Fade_ImageList;




		#region MAIN

		///<summary> Initialise DreamBeam </summary>
		public MainForm()
		{
			//
			// Erforderlich für die Unterstützung des Windows Forms-Designer
			//
			LogFile = new LogFile();
			LogFile.doLog = true;
			LogFile.BigHeader("Start");


			this.Hide();
			this.SwordProject_Found = this.Check_SwordProject();
				Splash.ShowSplashScreen();
				Splash.SetStatus("Initializing");
			InitializeComponent();
				Splash.SetStatus("Checking for Sword Project");
			InitializeDiatheke();

			Presentation_FadePanel.Size = new System.Drawing.Size (0,Presentation_FadePanel.Size.Height);
			fillTree();

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


		/// <summary>
		/// Der Haupteinsprungspunkt für die Anwendung
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.Run(new MainForm());
		}

		#endregion


		#region Vom Windows Form-Designer erzeugter Code
		/// <summary>
		/// Erforderliche Methode zur Unterstützung des Designers -
		/// ändern Sie die Methode nicht mit dem Quelltext-Editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MainForm));
			this.RightDocks_ImageListBox = new Controls.Development.ImageListBox();
			this.ImageContext = new System.Windows.Forms.ContextMenu();
			this.ImageContextItemManage = new System.Windows.Forms.MenuItem();
			this.ImageContextItemReload = new System.Windows.Forms.MenuItem();
			this.RightDocks_imageList = new System.Windows.Forms.ImageList(this.components);
			this.SongEdit_fontDialog = new System.Windows.Forms.FontDialog();
			this.SongEdit_OutlineColorDialog = new System.Windows.Forms.ColorDialog();
			this.SongEdit_TextColorDialog = new System.Windows.Forms.ColorDialog();
			this.SongEdit_Mid_TextBox = new System.Windows.Forms.TextBox();
			this.RightDocks_SongList = new System.Windows.Forms.ListBox();
			this.RightDocks_Songlist_SearchPanel = new System.Windows.Forms.Panel();
			this.RightDocks_SongListSearch = new System.Windows.Forms.TextBox();
			this.RightDocks_SongList_ButtonPanel = new System.Windows.Forms.Panel();
			this.btnRightDocks_SongListLoad = new System.Windows.Forms.Button();
			this.btnRightDocks_SongListDelete = new System.Windows.Forms.Button();
			this.btnRightDocks_SongList2PlayList = new System.Windows.Forms.Button();
			this.RightDocks_PlayList = new System.Windows.Forms.ListBox();
			this.RightDocks_TopPanel_PlayList_Button_Panel = new System.Windows.Forms.Panel();
			this.RightDocks_PlayList_Down_Button = new System.Windows.Forms.Button();
			this.RightDocks_PlayList_Up_Button = new System.Windows.Forms.Button();
			this.RightDocks_PlayList_Remove_Button = new System.Windows.Forms.Button();
			this.RightDocks_PlayList_Load_Button = new System.Windows.Forms.Button();
			this.RightDocks_Search_ListBox = new System.Windows.Forms.ListBox();
			this.RightDocks_TopPanel_Search_ButtonPanel = new System.Windows.Forms.Panel();
			this.RightDocks_Search_PlaylistButton = new System.Windows.Forms.Button();
			this.RightDocks_Search_LoadButton = new System.Windows.Forms.Button();
			this.RightDocks_SearchBar_TopPanel = new System.Windows.Forms.Panel();
			this.RightDocks_TopPanel_Search_RadioButton2 = new System.Windows.Forms.RadioButton();
			this.RightDocks_TopPanel_Search_RadioButton1 = new System.Windows.Forms.RadioButton();
			this.RightDocks_Search_InputBox = new System.Windows.Forms.TextBox();
			this.RightDocks_Search_DropDown = new System.Windows.Forms.ComboBox();
			this.RightDocks_FolderDropdown = new System.Windows.Forms.ComboBox();
			this.SongEdit_BG_Label = new System.Windows.Forms.Label();
			this.SongEdit_UpdateBeamBox_Button = new System.Windows.Forms.Button();
			this.Footer_TextBox = new System.Windows.Forms.TextBox();
			this.ToolBars_sandBarManager1 = new TD.SandBar.SandBarManager();
			this.ToolBars_bottomSandBarDock = new TD.SandBar.ToolBarContainer();
			this.ToolBars_leftSandBarDock = new TD.SandBar.ToolBarContainer();
			this.ToolBars_ComponentBar = new TD.SandBar.ToolBar();
			this.ShowSongs_Button = new TD.SandBar.ButtonItem();
			this.EditSongs_Button = new TD.SandBar.ButtonItem();
			this.Presentation_Button = new TD.SandBar.ButtonItem();
			this.Sermon_Button = new TD.SandBar.ButtonItem();
			this.ToolBars_topSandBarDock = new TD.SandBar.ToolBarContainer();
			this.ToolBars_MainToolbar = new TD.SandBar.ToolBar();
			this.ToolBars_MainToolbar_ShowBeamBox = new TD.SandBar.ButtonItem();
			this.ToolBars_MainToolbar_SizePosition = new TD.SandBar.ButtonItem();
			this.ToolBars_MainToolbar_HideBG = new TD.SandBar.ButtonItem();
			this.ToolBars_MainToolbar_HideText = new TD.SandBar.ButtonItem();
			this.ToolBars_MainToolbar_SaveSong = new TD.SandBar.ButtonItem();
			this.ToolBars_MenuBar = new TD.SandBar.MenuBar();
			this.ToolBars_MenuBar_Song = new TD.SandBar.MenuBarItem();
			this.ToolBars_MenuBar_Song_New = new TD.SandBar.MenuButtonItem();
			this.ToolBars_MenuBar_Song_SaveAs = new TD.SandBar.MenuButtonItem();
			this.ToolBars_MenuBar_Song_Rename = new TD.SandBar.MenuButtonItem();
			this.ToolBars_MenuBar_Song_Exit = new TD.SandBar.MenuButtonItem();
			this.ToolBars_MenuBar_Edit = new TD.SandBar.MenuBarItem();
			this.ToolBars_MenuBar_Edit_Options = new TD.SandBar.MenuButtonItem();
			this.ToolBars_MenuBar_View = new TD.SandBar.MenuBarItem();
			this.ToolBars_MenuBar_Help = new TD.SandBar.MenuBarItem();
			this.HelpIntro = new TD.SandBar.MenuButtonItem();
			this.HelpBeamBox = new TD.SandBar.MenuButtonItem();
			this.HelpOptions = new TD.SandBar.MenuButtonItem();
			this.HelpShowSongs = new TD.SandBar.MenuButtonItem();
			this.HelpEditSongs = new TD.SandBar.MenuButtonItem();
			this.HelpTextTool = new TD.SandBar.MenuButtonItem();
			this.AboutButton = new TD.SandBar.MenuButtonItem();
			this.statusBar = new System.Windows.Forms.StatusBar();
			this.StatusPanel = new System.Windows.Forms.StatusBarPanel();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage0 = new OPaC.Themed.Forms.TabPage();
			this.SongShow_Right_Panel = new System.Windows.Forms.Panel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.SongShow_ToBeamBox_button = new System.Windows.Forms.Button();
			this.SongShow_BG_Panel = new System.Windows.Forms.Panel();
			this.SongShow_BG_Label = new OPaC.Themed.Forms.Label();
			this.SongShow_BG_CheckBox = new System.Windows.Forms.CheckBox();
			this.SongShow_Preview_Panel = new System.Windows.Forms.Panel();
			this.SongShow_StropheList_ListEx = new Lister.ListEx();
			this.tabPage2 = new OPaC.Themed.Forms.TabPage();
			this.Sermon_LeftPanel = new System.Windows.Forms.Panel();
			this.Sermon_LeftBottom_Panel = new System.Windows.Forms.Panel();
			this.Sermon_BeamBox_Button = new System.Windows.Forms.Button();
			this.Sermon_LeftDoc_Panel = new System.Windows.Forms.Panel();
			this.Sermon_DocManager = new DocumentManager.DocumentManager();
			this.Sermon_LeftToolBar_Panel = new System.Windows.Forms.Panel();
			this.Sermon_ToolBar = new TD.SandBar.ToolBar();
			this.Sermon_ToolBar_NewDoc_Button = new TD.SandBar.ButtonItem();
			this.Sermon_ToolBar_Font_Button = new TD.SandBar.ButtonItem();
			this.Sermon_ToolBar_Color_Button = new TD.SandBar.ButtonItem();
			this.Sermon_ToolBar_Outline_Button = new TD.SandBar.ButtonItem();
			this.Sermon_ToolBar_OutlineColor_Button = new TD.SandBar.ButtonItem();
			this.Sermon_TabControl = new System.Windows.Forms.TabControl();
			this.tabPage3 = new OPaC.Themed.Forms.TabPage();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.Sermon_Verse_Labe = new System.Windows.Forms.Label();
			this.Sermon_Translation_Label = new System.Windows.Forms.Label();
			this.Sermon_Books_Label = new System.Windows.Forms.Label();
			this.Sermon_BibleKey = new System.Windows.Forms.TextBox();
			this.Sermon_Testament_ListBox = new System.Windows.Forms.ListBox();
			this.Sermon_Books = new System.Windows.Forms.ComboBox();
			this.Sermon_BookList = new System.Windows.Forms.ListBox();
			this.tabPage1 = new OPaC.Themed.Forms.TabPage();
			this.SongEdit_RightPanel = new System.Windows.Forms.Panel();
			this.SongEdit_TextAlign = new System.Windows.Forms.GroupBox();
			this.radioButton2 = new System.Windows.Forms.RadioButton();
			this.radioButton1 = new System.Windows.Forms.RadioButton();
			this.panel2 = new System.Windows.Forms.Panel();
			this.SongEdit_BG_Panel = new System.Windows.Forms.Panel();
			this.SongEdit_BG_DecscriptionLabel = new System.Windows.Forms.Label();
			this.SongEdit_PreviewStropheDown_Button = new System.Windows.Forms.Button();
			this.SongEdit_PreviewStropheUp_Button = new System.Windows.Forms.Button();
			this.SongEdit_BigInputFieldPanel = new System.Windows.Forms.Panel();
			this.SongEdit_InputFieldPanelMid = new System.Windows.Forms.Panel();
			this.SongEdit_InputFieldBelowMenuPanelMid = new System.Windows.Forms.Panel();
			this.SongEdit_InputFieldBelowMenuPane2lMid = new System.Windows.Forms.Panel();
			this.SongEdit_MidPos_Panel = new System.Windows.Forms.Panel();
			this.SongEdit_Header_Verses = new System.Windows.Forms.Label();
			this.SongEdit_Mid_AutoPos_CheckBox = new System.Windows.Forms.CheckBox();
			this.SongEdit_MidPosX_Label = new System.Windows.Forms.Label();
			this.SongEdit_MidPosX_UpDown = new System.Windows.Forms.NumericUpDown();
			this.SongEdit_MidPosY_UpDown = new System.Windows.Forms.NumericUpDown();
			this.SongEdit_MidPosY_Label = new System.Windows.Forms.Label();
			this.SongEdit_InputFieldMenuPanelMid = new System.Windows.Forms.Panel();
			this.SongEdit_MidTextToolBar = new TD.SandBar.ToolBar();
			this.SongEdit_ButtonMidFont = new TD.SandBar.ButtonItem();
			this.SongEdit_ButtonMidColor = new TD.SandBar.ButtonItem();
			this.SongEdit_ButtonMidTextOutline = new TD.SandBar.ButtonItem();
			this.SongEdit_ButtonMidOutlineColor = new TD.SandBar.ButtonItem();
			this.SongEdit_InputFieldPanelButtom = new System.Windows.Forms.Panel();
			this.SongEdit_InputFieldBelowMenuPanelButtom = new System.Windows.Forms.Panel();
			this.SongEdit_BottomPos_Panel = new System.Windows.Forms.Panel();
			this.SongEdit_Header_Author = new System.Windows.Forms.Label();
			this.SongEdit_Bottom_AutoPos_CheckBox = new System.Windows.Forms.CheckBox();
			this.SongEdit_BottomPosX_Label = new System.Windows.Forms.Label();
			this.SongEdit_BottomPosX_UpDown = new System.Windows.Forms.NumericUpDown();
			this.SongEdit_BottomPosY_UpDown = new System.Windows.Forms.NumericUpDown();
			this.SongEdit_BottomPosY_Label = new System.Windows.Forms.Label();
			this.SongEdit_InputFieldMenuPanelButtom = new System.Windows.Forms.Panel();
			this.SongEdit_BottomTextToolBar = new TD.SandBar.ToolBar();
			this.SongEdit_ButtonBottomFont = new TD.SandBar.ButtonItem();
			this.SongEdit_ButtonBottomColor = new TD.SandBar.ButtonItem();
			this.SongEdit_ButtonBottomTextOutline = new TD.SandBar.ButtonItem();
			this.SongEdit_ButtonBottomOutlineColor = new TD.SandBar.ButtonItem();
			this.SongEdit_InputFieldPanelTop = new System.Windows.Forms.Panel();
			this.SongEdit_InputPanelTopBelowMenu = new System.Windows.Forms.Panel();
			this.SongEdit_TopPos_Panel = new System.Windows.Forms.Panel();
			this.SongEdit_Header_Title = new System.Windows.Forms.Label();
			this.SongEdit_Top_AutoPos_CheckBox = new System.Windows.Forms.CheckBox();
			this.SongEdit_TopPosX_Label = new System.Windows.Forms.Label();
			this.SongEdit_TopPosX_UpDown = new System.Windows.Forms.NumericUpDown();
			this.SongEdit_TopPosY_UpDown = new System.Windows.Forms.NumericUpDown();
			this.SongEdit_TopPosY_Label = new System.Windows.Forms.Label();
			this.SongEdit_Header_TextBox = new System.Windows.Forms.TextBox();
			this.SongEdit_InputPanelTopMenu = new System.Windows.Forms.Panel();
			this.SongEdit_TopTextToolBar = new TD.SandBar.ToolBar();
			this.SongEdit_ButtonTopFont = new TD.SandBar.ButtonItem();
			this.SongEdit_ButtonTopColor = new TD.SandBar.ButtonItem();
			this.SongEdit_ButtonTopTextOutline = new TD.SandBar.ButtonItem();
			this.SongEdit_ButtonTopOutlineColor = new TD.SandBar.ButtonItem();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.Presentation_FadePanel = new System.Windows.Forms.Panel();
			this.panel3 = new System.Windows.Forms.Panel();
			this.Presentation_Fade_preview = new System.Windows.Forms.PictureBox();
			this.Presentation_Fade_ListView = new System.Windows.Forms.ListView();
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.imageList_Folders = new System.Windows.Forms.ImageList(this.components);
			this.PreviewUpdateTimer = new System.Windows.Forms.Timer(this.components);
			this.TextTypedTimer = new System.Windows.Forms.Timer(this.components);
			this.sandDockManager1 = new TD.SandDock.SandDockManager();
			this.leftSandDock = new TD.SandDock.DockContainer();
			this.rightSandDock = new TD.SandDock.DockContainer();
			this.RightDocks_TopPanel_Songs = new TD.SandDock.DockControl();
			this.RightDocks_TopPanel_PlayList = new TD.SandDock.DockControl();
			this.RightDocks_TopPanel_Search = new TD.SandDock.DockControl();
			this.RightDocks_BottomPanel_Backgrounds = new TD.SandDock.DockControl();
			this.RightDocks_BottomPanel2_TopPanel = new System.Windows.Forms.Panel();
			this.RightDocks_BottomPanel_Media = new TD.SandDock.DockControl();
			this.RightDocks_BottomPanel_Media_FadePanelButton = new System.Windows.Forms.Button();
			this.RightDocks_BottomPanel_Media_FolderSelectButton = new System.Windows.Forms.Button();
			this.RightDocks_BottomPanel_MediaListView = new DragNDrop.DragAndDropListView();
			this.bottomSandDock = new TD.SandDock.DockContainer();
			this.topSandDock = new TD.SandDock.DockContainer();
			this.Presentation_Fade_ToPlaylist_Button = new System.Windows.Forms.Button();
			this.Presentation_Fade_ImageList = new System.Windows.Forms.ImageList(this.components);
			this.RightDocks_Songlist_SearchPanel.SuspendLayout();
			this.RightDocks_SongList_ButtonPanel.SuspendLayout();
			this.RightDocks_TopPanel_PlayList_Button_Panel.SuspendLayout();
			this.RightDocks_TopPanel_Search_ButtonPanel.SuspendLayout();
			this.RightDocks_SearchBar_TopPanel.SuspendLayout();
			this.ToolBars_leftSandBarDock.SuspendLayout();
			this.ToolBars_topSandBarDock.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.StatusPanel)).BeginInit();
			this.tabControl1.SuspendLayout();
			this.tabPage0.SuspendLayout();
			this.SongShow_Right_Panel.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SongShow_BG_Panel.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.Sermon_LeftPanel.SuspendLayout();
			this.Sermon_LeftBottom_Panel.SuspendLayout();
			this.Sermon_LeftDoc_Panel.SuspendLayout();
			this.Sermon_LeftToolBar_Panel.SuspendLayout();
			this.Sermon_TabControl.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.SongEdit_RightPanel.SuspendLayout();
			this.SongEdit_TextAlign.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SongEdit_BG_Panel.SuspendLayout();
			this.SongEdit_BigInputFieldPanel.SuspendLayout();
			this.SongEdit_InputFieldPanelMid.SuspendLayout();
			this.SongEdit_InputFieldBelowMenuPanelMid.SuspendLayout();
			this.SongEdit_InputFieldBelowMenuPane2lMid.SuspendLayout();
			this.SongEdit_MidPos_Panel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.SongEdit_MidPosX_UpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.SongEdit_MidPosY_UpDown)).BeginInit();
			this.SongEdit_InputFieldMenuPanelMid.SuspendLayout();
			this.SongEdit_InputFieldPanelButtom.SuspendLayout();
			this.SongEdit_InputFieldBelowMenuPanelButtom.SuspendLayout();
			this.SongEdit_BottomPos_Panel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.SongEdit_BottomPosX_UpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.SongEdit_BottomPosY_UpDown)).BeginInit();
			this.SongEdit_InputFieldMenuPanelButtom.SuspendLayout();
			this.SongEdit_InputFieldPanelTop.SuspendLayout();
			this.SongEdit_InputPanelTopBelowMenu.SuspendLayout();
			this.SongEdit_TopPos_Panel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.SongEdit_TopPosX_UpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.SongEdit_TopPosY_UpDown)).BeginInit();
			this.SongEdit_InputPanelTopMenu.SuspendLayout();
			this.tabPage4.SuspendLayout();
			this.Presentation_FadePanel.SuspendLayout();
			this.panel3.SuspendLayout();
			this.rightSandDock.SuspendLayout();
			this.RightDocks_TopPanel_Songs.SuspendLayout();
			this.RightDocks_TopPanel_PlayList.SuspendLayout();
			this.RightDocks_TopPanel_Search.SuspendLayout();
			this.RightDocks_BottomPanel_Backgrounds.SuspendLayout();
			this.RightDocks_BottomPanel2_TopPanel.SuspendLayout();
			this.RightDocks_BottomPanel_Media.SuspendLayout();
			this.SuspendLayout();
			// 
			// RightDocks_ImageListBox
			// 
			this.RightDocks_ImageListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.RightDocks_ImageListBox.ContextMenu = this.ImageContext;
			this.RightDocks_ImageListBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.RightDocks_ImageListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.RightDocks_ImageListBox.HorizontalExtent = 10;
			this.RightDocks_ImageListBox.ImageList = this.RightDocks_imageList;
			this.RightDocks_ImageListBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.RightDocks_ImageListBox.ItemHeight = 60;
			this.RightDocks_ImageListBox.Location = new System.Drawing.Point(0, 24);
			this.RightDocks_ImageListBox.Name = "RightDocks_ImageListBox";
			this.RightDocks_ImageListBox.Size = new System.Drawing.Size(196, 182);
			this.RightDocks_ImageListBox.TabIndex = 19;
			this.RightDocks_ImageListBox.SelectedIndexChanged += new System.EventHandler(this.RightDocks_ImageListBox_SelectedIndexChanged);
			// 
			// ImageContext
			// 
			this.ImageContext.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
						this.ImageContextItemManage,
						this.ImageContextItemReload});
			// 
			// ImageContextItemManage
			// 
			this.ImageContextItemManage.Index = 0;
			this.ImageContextItemManage.Text = "Manage Images";
			this.ImageContextItemManage.Click += new System.EventHandler(this.ImageContextItemManage_Click);
			// 
			// ImageContextItemReload
			// 
			this.ImageContextItemReload.Index = 1;
			this.ImageContextItemReload.Text = "Reload Images";
			this.ImageContextItemReload.Click += new System.EventHandler(this.ImageContextItemReload_Click);
			// 
			// RightDocks_imageList
			// 
			this.RightDocks_imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.RightDocks_imageList.ImageSize = new System.Drawing.Size(80, 60);
			this.RightDocks_imageList.TransparentColor = System.Drawing.Color.Pink;
			// 
			// SongEdit_fontDialog
			// 
			this.SongEdit_fontDialog.Font = new System.Drawing.Font("Microsoft Sans Serif", 35F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			// 
			// SongEdit_TextColorDialog
			// 
			this.SongEdit_TextColorDialog.Color = System.Drawing.Color.White;
			// 
			// SongEdit_Mid_TextBox
			// 
			this.SongEdit_Mid_TextBox.AcceptsTab = true;
			this.SongEdit_Mid_TextBox.BackColor = System.Drawing.Color.White;
			this.SongEdit_Mid_TextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.SongEdit_Mid_TextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SongEdit_Mid_TextBox.Location = new System.Drawing.Point(0, 0);
			this.SongEdit_Mid_TextBox.Multiline = true;
			this.SongEdit_Mid_TextBox.Name = "SongEdit_Mid_TextBox";
			this.SongEdit_Mid_TextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.SongEdit_Mid_TextBox.Size = new System.Drawing.Size(320, 275);
			this.SongEdit_Mid_TextBox.TabIndex = 21;
			this.SongEdit_Mid_TextBox.Text = "";
			this.SongEdit_Mid_TextBox.TextChanged += new System.EventHandler(this.SongEdit_Mid_TextBox_TextChanged1);
			this.SongEdit_Mid_TextBox.Enter += new System.EventHandler(this.SongEdit_Mid_TextBox_Enter);
			// 
			// RightDocks_SongList
			// 
			this.RightDocks_SongList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.RightDocks_SongList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.RightDocks_SongList.Location = new System.Drawing.Point(0, 23);
			this.RightDocks_SongList.Name = "RightDocks_SongList";
			this.RightDocks_SongList.Size = new System.Drawing.Size(196, 119);
			this.RightDocks_SongList.TabIndex = 0;
			this.RightDocks_SongList.DoubleClick += new System.EventHandler(this.RightDocks_SongList_DoubleClick);
			// 
			// RightDocks_Songlist_SearchPanel
			// 
			this.RightDocks_Songlist_SearchPanel.Controls.Add(this.RightDocks_SongListSearch);
			this.RightDocks_Songlist_SearchPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.RightDocks_Songlist_SearchPanel.Location = new System.Drawing.Point(0, 0);
			this.RightDocks_Songlist_SearchPanel.Name = "RightDocks_Songlist_SearchPanel";
			this.RightDocks_Songlist_SearchPanel.Size = new System.Drawing.Size(196, 23);
			this.RightDocks_Songlist_SearchPanel.TabIndex = 6;
			// 
			// RightDocks_SongListSearch
			// 
			this.RightDocks_SongListSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.RightDocks_SongListSearch.Dock = System.Windows.Forms.DockStyle.Top;
			this.RightDocks_SongListSearch.Location = new System.Drawing.Point(0, 0);
			this.RightDocks_SongListSearch.Name = "RightDocks_SongListSearch";
			this.RightDocks_SongListSearch.Size = new System.Drawing.Size(196, 20);
			this.RightDocks_SongListSearch.TabIndex = 4;
			this.RightDocks_SongListSearch.Text = "";
//			this.RightDocks_SongListSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RightDocks_SongListSearch_KeyDown);
			this.RightDocks_SongListSearch.TextChanged += new System.EventHandler(this.RightDocks_SongListSearch_TextChanged);
			// 
			// RightDocks_SongList_ButtonPanel
			// 
			this.RightDocks_SongList_ButtonPanel.Controls.Add(this.btnRightDocks_SongListLoad);
			this.RightDocks_SongList_ButtonPanel.Controls.Add(this.btnRightDocks_SongListDelete);
			this.RightDocks_SongList_ButtonPanel.Controls.Add(this.btnRightDocks_SongList2PlayList);
			this.RightDocks_SongList_ButtonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.RightDocks_SongList_ButtonPanel.Location = new System.Drawing.Point(0, 150);
			this.RightDocks_SongList_ButtonPanel.Name = "RightDocks_SongList_ButtonPanel";
			this.RightDocks_SongList_ButtonPanel.Size = new System.Drawing.Size(196, 20);
			this.RightDocks_SongList_ButtonPanel.TabIndex = 5;
			// 
			// btnRightDocks_SongListLoad
			// 
			this.btnRightDocks_SongListLoad.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnRightDocks_SongListLoad.Location = new System.Drawing.Point(2, 1);
			this.btnRightDocks_SongListLoad.Name = "btnRightDocks_SongListLoad";
			this.btnRightDocks_SongListLoad.Size = new System.Drawing.Size(64, 18);
			this.btnRightDocks_SongListLoad.TabIndex = 3;
			this.btnRightDocks_SongListLoad.Text = "<-Load";
			this.btnRightDocks_SongListLoad.Click += new System.EventHandler(this.RightDocks_SongList_DoubleClick);
			// 
			// btnRightDocks_SongListDelete
			// 
			this.btnRightDocks_SongListDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnRightDocks_SongListDelete.Location = new System.Drawing.Point(64, 1);
			this.btnRightDocks_SongListDelete.Name = "btnRightDocks_SongListDelete";
			this.btnRightDocks_SongListDelete.Size = new System.Drawing.Size(64, 18);
			this.btnRightDocks_SongListDelete.TabIndex = 2;
			this.btnRightDocks_SongListDelete.Text = "Delete";
			this.btnRightDocks_SongListDelete.Click += new System.EventHandler(this.btnRightDocks_SongListDelete_Click);
			// 
			// btnRightDocks_SongList2PlayList
			// 
			this.btnRightDocks_SongList2PlayList.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnRightDocks_SongList2PlayList.Location = new System.Drawing.Point(128, 1);
			this.btnRightDocks_SongList2PlayList.Name = "btnRightDocks_SongList2PlayList";
			this.btnRightDocks_SongList2PlayList.Size = new System.Drawing.Size(64, 18);
			this.btnRightDocks_SongList2PlayList.TabIndex = 1;
			this.btnRightDocks_SongList2PlayList.Text = "Playlist ->";
			this.btnRightDocks_SongList2PlayList.Click += new System.EventHandler(this.btnRightDocks_SongList2PlayList_Click);
			// 
			// RightDocks_PlayList
			// 
			this.RightDocks_PlayList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.RightDocks_PlayList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.RightDocks_PlayList.Location = new System.Drawing.Point(0, 0);
			this.RightDocks_PlayList.Name = "RightDocks_PlayList";
			this.RightDocks_PlayList.Size = new System.Drawing.Size(196, 145);
			this.RightDocks_PlayList.TabIndex = 1;
			this.RightDocks_PlayList.DoubleClick += new System.EventHandler(this.RightDocks_PlayList_DoubleClick);
			// 
			// RightDocks_TopPanel_PlayList_Button_Panel
			// 
			this.RightDocks_TopPanel_PlayList_Button_Panel.Controls.Add(this.RightDocks_PlayList_Down_Button);
			this.RightDocks_TopPanel_PlayList_Button_Panel.Controls.Add(this.RightDocks_PlayList_Up_Button);
			this.RightDocks_TopPanel_PlayList_Button_Panel.Controls.Add(this.RightDocks_PlayList_Remove_Button);
			this.RightDocks_TopPanel_PlayList_Button_Panel.Controls.Add(this.RightDocks_PlayList_Load_Button);
			this.RightDocks_TopPanel_PlayList_Button_Panel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.RightDocks_TopPanel_PlayList_Button_Panel.Location = new System.Drawing.Point(0, 150);
			this.RightDocks_TopPanel_PlayList_Button_Panel.Name = "RightDocks_TopPanel_PlayList_Button_Panel";
			this.RightDocks_TopPanel_PlayList_Button_Panel.Size = new System.Drawing.Size(196, 20);
			this.RightDocks_TopPanel_PlayList_Button_Panel.TabIndex = 8;
			// 
			// RightDocks_PlayList_Down_Button
			// 
			this.RightDocks_PlayList_Down_Button.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.RightDocks_PlayList_Down_Button.Location = new System.Drawing.Point(152, 0);
			this.RightDocks_PlayList_Down_Button.Name = "RightDocks_PlayList_Down_Button";
			this.RightDocks_PlayList_Down_Button.Size = new System.Drawing.Size(40, 18);
			this.RightDocks_PlayList_Down_Button.TabIndex = 7;
			this.RightDocks_PlayList_Down_Button.Text = "down";
			this.RightDocks_PlayList_Down_Button.Click += new System.EventHandler(this.RightDocks_PlayList_Down_Button_Click);
			// 
			// RightDocks_PlayList_Up_Button
			// 
			this.RightDocks_PlayList_Up_Button.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.RightDocks_PlayList_Up_Button.Location = new System.Drawing.Point(112, 0);
			this.RightDocks_PlayList_Up_Button.Name = "RightDocks_PlayList_Up_Button";
			this.RightDocks_PlayList_Up_Button.Size = new System.Drawing.Size(40, 18);
			this.RightDocks_PlayList_Up_Button.TabIndex = 6;
			this.RightDocks_PlayList_Up_Button.Text = "up";
			this.RightDocks_PlayList_Up_Button.Click += new System.EventHandler(this.RightDocks_PlayList_Up_Button_Click);
			// 
			// RightDocks_PlayList_Remove_Button
			// 
			this.RightDocks_PlayList_Remove_Button.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.RightDocks_PlayList_Remove_Button.Location = new System.Drawing.Point(56, 0);
			this.RightDocks_PlayList_Remove_Button.Name = "RightDocks_PlayList_Remove_Button";
			this.RightDocks_PlayList_Remove_Button.Size = new System.Drawing.Size(54, 18);
			this.RightDocks_PlayList_Remove_Button.TabIndex = 5;
			this.RightDocks_PlayList_Remove_Button.Text = "Remove";
			this.RightDocks_PlayList_Remove_Button.Click += new System.EventHandler(this.RightDocks_PlayList_Remove_Button_Click);
			// 
			// RightDocks_PlayList_Load_Button
			// 
			this.RightDocks_PlayList_Load_Button.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.RightDocks_PlayList_Load_Button.Location = new System.Drawing.Point(0, 0);
			this.RightDocks_PlayList_Load_Button.Name = "RightDocks_PlayList_Load_Button";
			this.RightDocks_PlayList_Load_Button.Size = new System.Drawing.Size(54, 18);
			this.RightDocks_PlayList_Load_Button.TabIndex = 4;
			this.RightDocks_PlayList_Load_Button.Text = "<- Load";
			this.RightDocks_PlayList_Load_Button.Click += new System.EventHandler(this.RightDocks_PlayList_DoubleClick);
			// 
			// RightDocks_Search_ListBox
			// 
			this.RightDocks_Search_ListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.RightDocks_Search_ListBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.RightDocks_Search_ListBox.Location = new System.Drawing.Point(0, 50);
			this.RightDocks_Search_ListBox.Name = "RightDocks_Search_ListBox";
			this.RightDocks_Search_ListBox.Size = new System.Drawing.Size(196, 93);
			this.RightDocks_Search_ListBox.TabIndex = 6;
//			this.RightDocks_Search_ListBox.DoubleClick += new System.EventHandler(this.RightDocks_Search_ListBox_DoubleClick);
			// 
			// RightDocks_TopPanel_Search_ButtonPanel
			// 
			this.RightDocks_TopPanel_Search_ButtonPanel.Controls.Add(this.RightDocks_Search_PlaylistButton);
			this.RightDocks_TopPanel_Search_ButtonPanel.Controls.Add(this.RightDocks_Search_LoadButton);
			this.RightDocks_TopPanel_Search_ButtonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.RightDocks_TopPanel_Search_ButtonPanel.Location = new System.Drawing.Point(0, 150);
			this.RightDocks_TopPanel_Search_ButtonPanel.Name = "RightDocks_TopPanel_Search_ButtonPanel";
			this.RightDocks_TopPanel_Search_ButtonPanel.Size = new System.Drawing.Size(196, 20);
			this.RightDocks_TopPanel_Search_ButtonPanel.TabIndex = 8;
			// 
			// RightDocks_Search_PlaylistButton
			// 
			this.RightDocks_Search_PlaylistButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.RightDocks_Search_PlaylistButton.Location = new System.Drawing.Point(66, 1);
			this.RightDocks_Search_PlaylistButton.Name = "RightDocks_Search_PlaylistButton";
			this.RightDocks_Search_PlaylistButton.Size = new System.Drawing.Size(62, 18);
			this.RightDocks_Search_PlaylistButton.TabIndex = 5;
			this.RightDocks_Search_PlaylistButton.Text = "<- Playlist";
//			this.RightDocks_Search_PlaylistButton.Click += new System.EventHandler(this.RightDocks_Search_PlaylistButton_Click);
			// 
			// RightDocks_Search_LoadButton
			// 
			this.RightDocks_Search_LoadButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.RightDocks_Search_LoadButton.Location = new System.Drawing.Point(0, 1);
			this.RightDocks_Search_LoadButton.Name = "RightDocks_Search_LoadButton";
			this.RightDocks_Search_LoadButton.Size = new System.Drawing.Size(64, 18);
			this.RightDocks_Search_LoadButton.TabIndex = 4;
			this.RightDocks_Search_LoadButton.Text = "<<-Load";
//			this.RightDocks_Search_LoadButton.Click += new System.EventHandler(this.RightDocks_Search_ListBox_DoubleClick);
			// 
			// RightDocks_SearchBar_TopPanel
			// 
			this.RightDocks_SearchBar_TopPanel.Controls.Add(this.RightDocks_TopPanel_Search_RadioButton2);
			this.RightDocks_SearchBar_TopPanel.Controls.Add(this.RightDocks_TopPanel_Search_RadioButton1);
			this.RightDocks_SearchBar_TopPanel.Controls.Add(this.RightDocks_Search_InputBox);
			this.RightDocks_SearchBar_TopPanel.Controls.Add(this.RightDocks_Search_DropDown);
			this.RightDocks_SearchBar_TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.RightDocks_SearchBar_TopPanel.Location = new System.Drawing.Point(0, 0);
			this.RightDocks_SearchBar_TopPanel.Name = "RightDocks_SearchBar_TopPanel";
			this.RightDocks_SearchBar_TopPanel.Size = new System.Drawing.Size(196, 50);
			this.RightDocks_SearchBar_TopPanel.TabIndex = 7;
			// 
			// RightDocks_TopPanel_Search_RadioButton2
			// 
			this.RightDocks_TopPanel_Search_RadioButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.RightDocks_TopPanel_Search_RadioButton2.Location = new System.Drawing.Point(2, 34);
			this.RightDocks_TopPanel_Search_RadioButton2.Name = "RightDocks_TopPanel_Search_RadioButton2";
			this.RightDocks_TopPanel_Search_RadioButton2.Size = new System.Drawing.Size(94, 16);
			this.RightDocks_TopPanel_Search_RadioButton2.TabIndex = 7;
			this.RightDocks_TopPanel_Search_RadioButton2.Text = "Playlist";
			// 
			// RightDocks_TopPanel_Search_RadioButton1
			// 
			this.RightDocks_TopPanel_Search_RadioButton1.Checked = true;
			this.RightDocks_TopPanel_Search_RadioButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.RightDocks_TopPanel_Search_RadioButton1.Location = new System.Drawing.Point(2, 20);
			this.RightDocks_TopPanel_Search_RadioButton1.Name = "RightDocks_TopPanel_Search_RadioButton1";
			this.RightDocks_TopPanel_Search_RadioButton1.Size = new System.Drawing.Size(86, 16);
			this.RightDocks_TopPanel_Search_RadioButton1.TabIndex = 6;
			this.RightDocks_TopPanel_Search_RadioButton1.TabStop = true;
			this.RightDocks_TopPanel_Search_RadioButton1.Text = "all Songs";
			// 
			// RightDocks_Search_InputBox
			// 
			this.RightDocks_Search_InputBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.RightDocks_Search_InputBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.RightDocks_Search_InputBox.Location = new System.Drawing.Point(0, 0);
			this.RightDocks_Search_InputBox.Name = "RightDocks_Search_InputBox";
			this.RightDocks_Search_InputBox.Size = new System.Drawing.Size(196, 20);
			this.RightDocks_Search_InputBox.TabIndex = 5;
			this.RightDocks_Search_InputBox.Text = "";
//			this.RightDocks_Search_InputBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RightDocks_Search_InputBox_KeyDown);
			// 
			// RightDocks_Search_DropDown
			// 
			this.RightDocks_Search_DropDown.Items.AddRange(new object[] {
						"Phrase",
						"Words"});
			this.RightDocks_Search_DropDown.Location = new System.Drawing.Point(96, 24);
			this.RightDocks_Search_DropDown.Name = "RightDocks_Search_DropDown";
			this.RightDocks_Search_DropDown.Size = new System.Drawing.Size(96, 21);
			this.RightDocks_Search_DropDown.TabIndex = 8;
			// 
			// RightDocks_FolderDropdown
			// 
			this.RightDocks_FolderDropdown.Dock = System.Windows.Forms.DockStyle.Top;
			this.RightDocks_FolderDropdown.Location = new System.Drawing.Point(0, 0);
			this.RightDocks_FolderDropdown.Name = "RightDocks_FolderDropdown";
			this.RightDocks_FolderDropdown.Size = new System.Drawing.Size(196, 21);
			this.RightDocks_FolderDropdown.TabIndex = 21;
			this.RightDocks_FolderDropdown.SelectionChangeCommitted += new System.EventHandler(this.RightDocks_FolderDropdown_SelectionChangeCommitted);
			// 
			// SongEdit_BG_Label
			// 
			this.SongEdit_BG_Label.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SongEdit_BG_Label.Location = new System.Drawing.Point(8, 24);
			this.SongEdit_BG_Label.Name = "SongEdit_BG_Label";
			this.SongEdit_BG_Label.Size = new System.Drawing.Size(176, 24);
			this.SongEdit_BG_Label.TabIndex = 1;
			// 
			// SongEdit_UpdateBeamBox_Button
			// 
			this.SongEdit_UpdateBeamBox_Button.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.SongEdit_UpdateBeamBox_Button.Location = new System.Drawing.Point(48, 112);
			this.SongEdit_UpdateBeamBox_Button.Name = "SongEdit_UpdateBeamBox_Button";
			this.SongEdit_UpdateBeamBox_Button.Size = new System.Drawing.Size(96, 24);
			this.SongEdit_UpdateBeamBox_Button.TabIndex = 23;
			this.SongEdit_UpdateBeamBox_Button.Text = "To Projector";
			this.SongEdit_UpdateBeamBox_Button.Click += new System.EventHandler(this.SongEdit_UpdateBeamBox_Button_Click);
			// 
			// Footer_TextBox
			// 
			this.Footer_TextBox.BackColor = System.Drawing.Color.White;
			this.Footer_TextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Footer_TextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Footer_TextBox.Location = new System.Drawing.Point(0, 0);
			this.Footer_TextBox.Multiline = true;
			this.Footer_TextBox.Name = "Footer_TextBox";
			this.Footer_TextBox.Size = new System.Drawing.Size(320, 66);
			this.Footer_TextBox.TabIndex = 20;
			this.Footer_TextBox.Text = "";
			this.Footer_TextBox.TextChanged += new System.EventHandler(this.Footer_TextBox_TextChanged);
			this.Footer_TextBox.Enter += new System.EventHandler(this.Footer_TextBox_Enter);
			// 
			// ToolBars_sandBarManager1
			// 
			this.ToolBars_sandBarManager1.BottomContainer = this.ToolBars_bottomSandBarDock;
			this.ToolBars_sandBarManager1.LeftContainer = this.ToolBars_leftSandBarDock;
			this.ToolBars_sandBarManager1.OwnerForm = this;
			this.ToolBars_sandBarManager1.RightContainer = null;
			this.ToolBars_sandBarManager1.TopContainer = this.ToolBars_topSandBarDock;
			// 
			// ToolBars_bottomSandBarDock
			// 
			this.ToolBars_bottomSandBarDock.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.ToolBars_bottomSandBarDock.Location = new System.Drawing.Point(0, 549);
			this.ToolBars_bottomSandBarDock.Manager = this.ToolBars_sandBarManager1;
			this.ToolBars_bottomSandBarDock.Name = "ToolBars_bottomSandBarDock";
			this.ToolBars_bottomSandBarDock.Size = new System.Drawing.Size(782, 0);
			this.ToolBars_bottomSandBarDock.TabIndex = 20;
			// 
			// ToolBars_leftSandBarDock
			// 
			this.ToolBars_leftSandBarDock.Controls.Add(this.ToolBars_ComponentBar);
			this.ToolBars_leftSandBarDock.Dock = System.Windows.Forms.DockStyle.Left;
			this.ToolBars_leftSandBarDock.Location = new System.Drawing.Point(0, 48);
			this.ToolBars_leftSandBarDock.Manager = this.ToolBars_sandBarManager1;
			this.ToolBars_leftSandBarDock.Name = "ToolBars_leftSandBarDock";
			this.ToolBars_leftSandBarDock.Size = new System.Drawing.Size(57, 501);
			this.ToolBars_leftSandBarDock.TabIndex = 18;
			// 
			// ToolBars_ComponentBar
			// 
			this.ToolBars_ComponentBar.AddRemoveButtonsVisible = false;
			this.ToolBars_ComponentBar.BackColor = System.Drawing.SystemColors.Control;
			this.ToolBars_ComponentBar.Buttons.AddRange(new TD.SandBar.ToolbarItemBase[] {
						this.ShowSongs_Button,
						this.EditSongs_Button,
						this.Presentation_Button,
						this.Sermon_Button});
			this.ToolBars_ComponentBar.Closable = false;
			this.ToolBars_ComponentBar.DockLine = 1;
			this.ToolBars_ComponentBar.DrawActionsButton = false;
			this.ToolBars_ComponentBar.ForeColor = System.Drawing.SystemColors.ControlText;
			this.ToolBars_ComponentBar.Guid = new System.Guid("bdddaff4-849d-43cd-ab71-bbe084e99803");
			this.ToolBars_ComponentBar.ImageList = null;
			this.ToolBars_ComponentBar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.ToolBars_ComponentBar.Location = new System.Drawing.Point(0, 2);
			this.ToolBars_ComponentBar.Movable = false;
			this.ToolBars_ComponentBar.Name = "ToolBars_ComponentBar";
			this.ToolBars_ComponentBar.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.ToolBars_ComponentBar.Size = new System.Drawing.Size(57, 499);
			this.ToolBars_ComponentBar.Stretch = true;
			this.ToolBars_ComponentBar.TabIndex = 0;
			this.ToolBars_ComponentBar.Tearable = false;
			this.ToolBars_ComponentBar.ButtonClick += new TD.SandBar.ToolBar.ButtonClickEventHandler(this.ToolBars_ComponentBar_ButtonClick);
			// 
			// ShowSongs_Button
			// 
			this.ShowSongs_Button.BuddyMenu = null;
			this.ShowSongs_Button.Icon = ((System.Drawing.Icon)(resources.GetObject("ShowSongs_Button.Icon")));
			this.ShowSongs_Button.IconSize = new System.Drawing.Size(47, 47);
			this.ShowSongs_Button.Tag = null;
			this.ShowSongs_Button.ToolTipText = "Show Songs";
			// 
			// EditSongs_Button
			// 
			this.EditSongs_Button.BuddyMenu = null;
			this.EditSongs_Button.Icon = ((System.Drawing.Icon)(resources.GetObject("EditSongs_Button.Icon")));
			this.EditSongs_Button.IconSize = new System.Drawing.Size(47, 47);
			this.EditSongs_Button.Tag = null;
			this.EditSongs_Button.ToolTipText = "Edit Songs";
			// 
			// Presentation_Button
			// 
			this.Presentation_Button.BuddyMenu = null;
			this.Presentation_Button.Icon = null;
			this.Presentation_Button.IconSize = new System.Drawing.Size(47, 47);
			this.Presentation_Button.Tag = null;
			this.Presentation_Button.Text = "Present";
			// 
			// Sermon_Button
			// 
			this.Sermon_Button.BuddyMenu = null;
			this.Sermon_Button.Icon = ((System.Drawing.Icon)(resources.GetObject("Sermon_Button.Icon")));
			this.Sermon_Button.IconSize = new System.Drawing.Size(47, 47);
			this.Sermon_Button.Tag = null;
			// 
			// ToolBars_topSandBarDock
			// 
			this.ToolBars_topSandBarDock.Controls.Add(this.ToolBars_MainToolbar);
			this.ToolBars_topSandBarDock.Controls.Add(this.ToolBars_MenuBar);
			this.ToolBars_topSandBarDock.Dock = System.Windows.Forms.DockStyle.Top;
			this.ToolBars_topSandBarDock.Location = new System.Drawing.Point(0, 0);
			this.ToolBars_topSandBarDock.Manager = this.ToolBars_sandBarManager1;
			this.ToolBars_topSandBarDock.Name = "ToolBars_topSandBarDock";
			this.ToolBars_topSandBarDock.Size = new System.Drawing.Size(782, 48);
			this.ToolBars_topSandBarDock.TabIndex = 21;
			// 
			// ToolBars_MainToolbar
			// 
			this.ToolBars_MainToolbar.AddRemoveButtonsVisible = false;
			this.ToolBars_MainToolbar.Buttons.AddRange(new TD.SandBar.ToolbarItemBase[] {
						this.ToolBars_MainToolbar_ShowBeamBox,
						this.ToolBars_MainToolbar_SizePosition,
						this.ToolBars_MainToolbar_HideBG,
						this.ToolBars_MainToolbar_HideText,
						this.ToolBars_MainToolbar_SaveSong});
			this.ToolBars_MainToolbar.Closable = false;
			this.ToolBars_MainToolbar.Dock = System.Windows.Forms.DockStyle.Left;
			this.ToolBars_MainToolbar.DockLine = 1;
			this.ToolBars_MainToolbar.DrawActionsButton = false;
			this.ToolBars_MainToolbar.Guid = new System.Guid("8bc9aa4f-e677-49e0-acc1-49086837bb09");
			this.ToolBars_MainToolbar.ImageList = null;
			this.ToolBars_MainToolbar.Location = new System.Drawing.Point(2, 24);
			this.ToolBars_MainToolbar.Movable = false;
			this.ToolBars_MainToolbar.Name = "ToolBars_MainToolbar";
			this.ToolBars_MainToolbar.Size = new System.Drawing.Size(780, 24);
			this.ToolBars_MainToolbar.Stretch = true;
			this.ToolBars_MainToolbar.TabIndex = 1;
			this.ToolBars_MainToolbar.Tearable = false;
			this.ToolBars_MainToolbar.ButtonClick += new TD.SandBar.ToolBar.ButtonClickEventHandler(this.ToolBars_MainToolbar_ButtonClick);
			// 
			// ToolBars_MainToolbar_ShowBeamBox
			// 
			this.ToolBars_MainToolbar_ShowBeamBox.BeginGroup = true;
			this.ToolBars_MainToolbar_ShowBeamBox.BuddyMenu = null;
			this.ToolBars_MainToolbar_ShowBeamBox.Icon = null;
			this.ToolBars_MainToolbar_ShowBeamBox.Tag = null;
			this.ToolBars_MainToolbar_ShowBeamBox.Text = "Show Projector Window";
			// 
			// ToolBars_MainToolbar_SizePosition
			// 
			this.ToolBars_MainToolbar_SizePosition.BuddyMenu = null;
			this.ToolBars_MainToolbar_SizePosition.Icon = null;
			this.ToolBars_MainToolbar_SizePosition.Tag = null;
			this.ToolBars_MainToolbar_SizePosition.Text = "Size/Position";
			this.ToolBars_MainToolbar_SizePosition.ToolTipText = "Change Projector Window Size and Position";
			// 
			// ToolBars_MainToolbar_HideBG
			// 
			this.ToolBars_MainToolbar_HideBG.BeginGroup = true;
			this.ToolBars_MainToolbar_HideBG.BuddyMenu = null;
			this.ToolBars_MainToolbar_HideBG.Icon = null;
			this.ToolBars_MainToolbar_HideBG.Tag = null;
			this.ToolBars_MainToolbar_HideBG.Text = "Hide Background";
			// 
			// ToolBars_MainToolbar_HideText
			// 
			this.ToolBars_MainToolbar_HideText.BuddyMenu = null;
			this.ToolBars_MainToolbar_HideText.Icon = null;
			this.ToolBars_MainToolbar_HideText.Tag = null;
			this.ToolBars_MainToolbar_HideText.Text = "Hide Text";
			// 
			// ToolBars_MainToolbar_SaveSong
			// 
			this.ToolBars_MainToolbar_SaveSong.BeginGroup = true;
			this.ToolBars_MainToolbar_SaveSong.BuddyMenu = null;
			this.ToolBars_MainToolbar_SaveSong.Icon = null;
			this.ToolBars_MainToolbar_SaveSong.Tag = null;
			this.ToolBars_MainToolbar_SaveSong.Text = "Save Song";
			// 
			// ToolBars_MenuBar
			// 
			this.ToolBars_MenuBar.Buttons.AddRange(new TD.SandBar.ToolbarItemBase[] {
						this.ToolBars_MenuBar_Song,
						this.ToolBars_MenuBar_Edit,
						this.ToolBars_MenuBar_View,
						this.ToolBars_MenuBar_Help});
			this.ToolBars_MenuBar.Guid = new System.Guid("cc0de77e-657c-4906-a57a-f2a8d32fe17e");
			this.ToolBars_MenuBar.ImageList = null;
			this.ToolBars_MenuBar.Location = new System.Drawing.Point(2, 0);
			this.ToolBars_MenuBar.Movable = false;
			this.ToolBars_MenuBar.Name = "ToolBars_MenuBar";
			this.ToolBars_MenuBar.Size = new System.Drawing.Size(162, 24);
			this.ToolBars_MenuBar.Stretch = false;
			this.ToolBars_MenuBar.TabIndex = 0;
			this.ToolBars_MenuBar.Tearable = false;
			// 
			// ToolBars_MenuBar_Song
			// 
			this.ToolBars_MenuBar_Song.Icon = null;
			this.ToolBars_MenuBar_Song.MenuItems.AddRange(new TD.SandBar.MenuButtonItem[] {
						this.ToolBars_MenuBar_Song_New,
						this.ToolBars_MenuBar_Song_SaveAs,
						this.ToolBars_MenuBar_Song_Rename,
						this.ToolBars_MenuBar_Song_Exit});
			this.ToolBars_MenuBar_Song.Tag = null;
			this.ToolBars_MenuBar_Song.Text = "&Song";
			// 
			// ToolBars_MenuBar_Song_New
			// 
			this.ToolBars_MenuBar_Song_New.Icon = null;
			this.ToolBars_MenuBar_Song_New.Shortcut = System.Windows.Forms.Shortcut.None;
			this.ToolBars_MenuBar_Song_New.Tag = null;
			this.ToolBars_MenuBar_Song_New.Text = "New...";
			// 
			// ToolBars_MenuBar_Song_SaveAs
			// 
			this.ToolBars_MenuBar_Song_SaveAs.Icon = null;
			this.ToolBars_MenuBar_Song_SaveAs.Shortcut = System.Windows.Forms.Shortcut.None;
			this.ToolBars_MenuBar_Song_SaveAs.Tag = null;
			this.ToolBars_MenuBar_Song_SaveAs.Text = "Save as...";
			this.ToolBars_MenuBar_Song_SaveAs.Activate += new TD.SandBar.MenuButtonItem.ActivateEventHandler(this.ToolBars_MenuBar_Song_SaveAs_Activate);
			// 
			// ToolBars_MenuBar_Song_Rename
			// 
			this.ToolBars_MenuBar_Song_Rename.Icon = null;
			this.ToolBars_MenuBar_Song_Rename.Shortcut = System.Windows.Forms.Shortcut.None;
			this.ToolBars_MenuBar_Song_Rename.Tag = null;
			this.ToolBars_MenuBar_Song_Rename.Text = "Rename";
			this.ToolBars_MenuBar_Song_Rename.Activate += new TD.SandBar.MenuButtonItem.ActivateEventHandler(this.ToolBars_MenuBar_Song_Rename_Activate);
			// 
			// ToolBars_MenuBar_Song_Exit
			// 
			this.ToolBars_MenuBar_Song_Exit.Icon = null;
			this.ToolBars_MenuBar_Song_Exit.Shortcut = System.Windows.Forms.Shortcut.None;
			this.ToolBars_MenuBar_Song_Exit.Tag = null;
			this.ToolBars_MenuBar_Song_Exit.Text = "Exit DreamBeam";
			this.ToolBars_MenuBar_Song_Exit.Activate += new TD.SandBar.MenuButtonItem.ActivateEventHandler(this.ToolBars_MenuBar_Song_Exit_Activate);
			// 
			// ToolBars_MenuBar_Edit
			// 
			this.ToolBars_MenuBar_Edit.Icon = null;
			this.ToolBars_MenuBar_Edit.MenuItems.AddRange(new TD.SandBar.MenuButtonItem[] {
						this.ToolBars_MenuBar_Edit_Options});
			this.ToolBars_MenuBar_Edit.Tag = null;
			this.ToolBars_MenuBar_Edit.Text = "&Edit";
			// 
			// ToolBars_MenuBar_Edit_Options
			// 
			this.ToolBars_MenuBar_Edit_Options.Icon = null;
			this.ToolBars_MenuBar_Edit_Options.Shortcut = System.Windows.Forms.Shortcut.None;
			this.ToolBars_MenuBar_Edit_Options.Tag = null;
			this.ToolBars_MenuBar_Edit_Options.Text = "Options";
			this.ToolBars_MenuBar_Edit_Options.Activate += new TD.SandBar.MenuButtonItem.ActivateEventHandler(this.ToolBars_MenuBar_Edit_Options_Activate);
			// 
			// ToolBars_MenuBar_View
			// 
			this.ToolBars_MenuBar_View.Icon = null;
			this.ToolBars_MenuBar_View.Tag = null;
			this.ToolBars_MenuBar_View.Text = "&View";
			// 
			// ToolBars_MenuBar_Help
			// 
			this.ToolBars_MenuBar_Help.Icon = null;
			this.ToolBars_MenuBar_Help.MenuItems.AddRange(new TD.SandBar.MenuButtonItem[] {
						this.HelpIntro,
						this.HelpBeamBox,
						this.HelpOptions,
						this.HelpShowSongs,
						this.HelpEditSongs,
						this.HelpTextTool,
						this.AboutButton});
			this.ToolBars_MenuBar_Help.Tag = null;
			this.ToolBars_MenuBar_Help.Text = "&Help";
			// 
			// HelpIntro
			// 
			this.HelpIntro.Icon = null;
			this.HelpIntro.Shortcut = System.Windows.Forms.Shortcut.None;
			this.HelpIntro.Tag = null;
			this.HelpIntro.Text = "Intro and Main Components";
			this.HelpIntro.Activate += new TD.SandBar.MenuButtonItem.ActivateEventHandler(this.HelpIntro_Activate);
			// 
			// HelpBeamBox
			// 
			this.HelpBeamBox.Icon = null;
			this.HelpBeamBox.Shortcut = System.Windows.Forms.Shortcut.None;
			this.HelpBeamBox.Tag = null;
			this.HelpBeamBox.Text = "The Projector Window";
			this.HelpBeamBox.Activate += new TD.SandBar.MenuButtonItem.ActivateEventHandler(this.HelpBeamBox_Activate);
			// 
			// HelpOptions
			// 
			this.HelpOptions.Icon = null;
			this.HelpOptions.Shortcut = System.Windows.Forms.Shortcut.None;
			this.HelpOptions.Tag = null;
			this.HelpOptions.Text = "Options";
			this.HelpOptions.Activate += new TD.SandBar.MenuButtonItem.ActivateEventHandler(this.HelpOptions_Activate);
			// 
			// HelpShowSongs
			// 
			this.HelpShowSongs.BeginGroup = true;
			this.HelpShowSongs.Icon = null;
			this.HelpShowSongs.Shortcut = System.Windows.Forms.Shortcut.None;
			this.HelpShowSongs.Tag = null;
			this.HelpShowSongs.Text = "Show Songs";
			this.HelpShowSongs.Activate += new TD.SandBar.MenuButtonItem.ActivateEventHandler(this.HelpShowSongs_Activate);
			// 
			// HelpEditSongs
			// 
			this.HelpEditSongs.Icon = null;
			this.HelpEditSongs.Shortcut = System.Windows.Forms.Shortcut.None;
			this.HelpEditSongs.Tag = null;
			this.HelpEditSongs.Text = "Edit Songs";
			this.HelpEditSongs.Activate += new TD.SandBar.MenuButtonItem.ActivateEventHandler(this.HelpEditSongs_Activate);
			// 
			// HelpTextTool
			// 
			this.HelpTextTool.Icon = null;
			this.HelpTextTool.Shortcut = System.Windows.Forms.Shortcut.None;
			this.HelpTextTool.Tag = null;
			this.HelpTextTool.Text = "Text Tool";
			this.HelpTextTool.Activate += new TD.SandBar.MenuButtonItem.ActivateEventHandler(this.HelpTextTool_Activate);
			// 
			// AboutButton
			// 
			this.AboutButton.BeginGroup = true;
			this.AboutButton.Icon = null;
			this.AboutButton.Shortcut = System.Windows.Forms.Shortcut.None;
			this.AboutButton.Tag = null;
			this.AboutButton.Text = "About";
			this.AboutButton.Activate += new TD.SandBar.MenuButtonItem.ActivateEventHandler(this.AboutButton_Activate);
			// 
			// statusBar
			// 
			this.statusBar.Location = new System.Drawing.Point(57, 527);
			this.statusBar.Name = "statusBar";
			this.statusBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
						this.StatusPanel});
			this.statusBar.ShowPanels = true;
			this.statusBar.Size = new System.Drawing.Size(525, 22);
			this.statusBar.SizingGrip = false;
			this.statusBar.TabIndex = 22;
			// 
			// StatusPanel
			// 
			this.StatusPanel.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
			this.StatusPanel.Icon = ((System.Drawing.Icon)(resources.GetObject("StatusPanel.Icon")));
			this.StatusPanel.MinWidth = 530;
			this.StatusPanel.Width = 530;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage0);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage4);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.ItemSize = new System.Drawing.Size(30, 20);
			this.tabControl1.Location = new System.Drawing.Point(57, 48);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(525, 479);
			this.tabControl1.TabIndex = 2;
			// 
			// tabPage0
			// 
			this.tabPage0.Controls.Add(this.SongShow_Right_Panel);
			this.tabPage0.Controls.Add(this.SongShow_StropheList_ListEx);
			this.tabPage0.Location = new System.Drawing.Point(4, 24);
			this.tabPage0.Name = "tabPage0";
			this.tabPage0.Size = new System.Drawing.Size(517, 451);
			this.tabPage0.TabIndex = 2;
			this.tabPage0.Text = "Show Songs";
			// 
			// SongShow_Right_Panel
			// 
			this.SongShow_Right_Panel.BackColor = System.Drawing.SystemColors.Control;
			this.SongShow_Right_Panel.Controls.Add(this.panel1);
			this.SongShow_Right_Panel.Controls.Add(this.SongShow_Preview_Panel);
			this.SongShow_Right_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SongShow_Right_Panel.Location = new System.Drawing.Point(320, 0);
			this.SongShow_Right_Panel.Name = "SongShow_Right_Panel";
			this.SongShow_Right_Panel.Size = new System.Drawing.Size(197, 451);
			this.SongShow_Right_Panel.TabIndex = 4;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.SongShow_ToBeamBox_button);
			this.panel1.Controls.Add(this.SongShow_BG_Panel);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 307);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(197, 144);
			this.panel1.TabIndex = 26;
			// 
			// SongShow_ToBeamBox_button
			// 
			this.SongShow_ToBeamBox_button.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.SongShow_ToBeamBox_button.Location = new System.Drawing.Point(48, 112);
			this.SongShow_ToBeamBox_button.Name = "SongShow_ToBeamBox_button";
			this.SongShow_ToBeamBox_button.Size = new System.Drawing.Size(96, 24);
			this.SongShow_ToBeamBox_button.TabIndex = 3;
			this.SongShow_ToBeamBox_button.Text = "To Projector";
			this.SongShow_ToBeamBox_button.Click += new System.EventHandler(this.SongShow_StropheList_ListEx_DoubleClick);
			// 
			// SongShow_BG_Panel
			// 
			this.SongShow_BG_Panel.BackColor = System.Drawing.Color.Transparent;
			this.SongShow_BG_Panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.SongShow_BG_Panel.Controls.Add(this.SongShow_BG_Label);
			this.SongShow_BG_Panel.Controls.Add(this.SongShow_BG_CheckBox);
			this.SongShow_BG_Panel.Location = new System.Drawing.Point(0, 8);
			this.SongShow_BG_Panel.Name = "SongShow_BG_Panel";
			this.SongShow_BG_Panel.Size = new System.Drawing.Size(196, 56);
			this.SongShow_BG_Panel.TabIndex = 1;
			// 
			// SongShow_BG_Label
			// 
			this.SongShow_BG_Label.BackColor = System.Drawing.Color.Transparent;
			this.SongShow_BG_Label.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SongShow_BG_Label.Location = new System.Drawing.Point(8, 25);
			this.SongShow_BG_Label.Name = "SongShow_BG_Label";
			this.SongShow_BG_Label.Size = new System.Drawing.Size(176, 24);
			this.SongShow_BG_Label.TabIndex = 3;
			this.SongShow_BG_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// SongShow_BG_CheckBox
			// 
			this.SongShow_BG_CheckBox.Location = new System.Drawing.Point(8, 1);
			this.SongShow_BG_CheckBox.Name = "SongShow_BG_CheckBox";
			this.SongShow_BG_CheckBox.Size = new System.Drawing.Size(136, 24);
			this.SongShow_BG_CheckBox.TabIndex = 2;
			this.SongShow_BG_CheckBox.Text = "Overwrite Background";
			this.SongShow_BG_CheckBox.CheckedChanged += new System.EventHandler(this.SongShow_BG_CheckBox_CheckedChanged);
			// 
			// SongShow_Preview_Panel
			// 
			this.SongShow_Preview_Panel.BackColor = System.Drawing.Color.White;
			this.SongShow_Preview_Panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.SongShow_Preview_Panel.Dock = System.Windows.Forms.DockStyle.Top;
			this.SongShow_Preview_Panel.Location = new System.Drawing.Point(0, 0);
			this.SongShow_Preview_Panel.Name = "SongShow_Preview_Panel";
			this.SongShow_Preview_Panel.Size = new System.Drawing.Size(197, 148);
			this.SongShow_Preview_Panel.TabIndex = 25;
			this.SongShow_Preview_Panel.Click += new System.EventHandler(this.SongShow_Preview_Panel_Click);
			this.SongShow_Preview_Panel.Paint += new System.Windows.Forms.PaintEventHandler(this.SongEdit_Preview_Panel_Paint);
			// 
			// SongShow_StropheList_ListEx
			// 
			this.SongShow_StropheList_ListEx.Dock = System.Windows.Forms.DockStyle.Left;
			this.SongShow_StropheList_ListEx.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
			this.SongShow_StropheList_ListEx.Imgs = null;
			this.SongShow_StropheList_ListEx.LineColor = System.Drawing.Color.FromArgb(((byte)(199)), ((byte)(199)), ((byte)(199)));
			this.SongShow_StropheList_ListEx.Location = new System.Drawing.Point(0, 0);
			this.SongShow_StropheList_ListEx.Name = "SongShow_StropheList_ListEx";
			this.SongShow_StropheList_ListEx.Size = new System.Drawing.Size(320, 451);
			this.SongShow_StropheList_ListEx.TabIndex = 0;
			this.SongShow_StropheList_ListEx.DoubleClick += new System.EventHandler(this.SongShow_StropheList_ListEx_DoubleClick);
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.Sermon_LeftPanel);
			this.tabPage2.Controls.Add(this.Sermon_TabControl);
			this.tabPage2.Location = new System.Drawing.Point(4, 24);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(574, 499);
			this.tabPage2.TabIndex = 3;
			this.tabPage2.Text = "SermonTool";
			// 
			// Sermon_LeftPanel
			// 
			this.Sermon_LeftPanel.Controls.Add(this.Sermon_LeftBottom_Panel);
			this.Sermon_LeftPanel.Controls.Add(this.Sermon_LeftDoc_Panel);
			this.Sermon_LeftPanel.Controls.Add(this.Sermon_LeftToolBar_Panel);
			this.Sermon_LeftPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Sermon_LeftPanel.Location = new System.Drawing.Point(0, 0);
			this.Sermon_LeftPanel.Name = "Sermon_LeftPanel";
			this.Sermon_LeftPanel.Size = new System.Drawing.Size(430, 499);
			this.Sermon_LeftPanel.TabIndex = 3;
			// 
			// Sermon_LeftBottom_Panel
			// 
			this.Sermon_LeftBottom_Panel.Controls.Add(this.Sermon_BeamBox_Button);
			this.Sermon_LeftBottom_Panel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.Sermon_LeftBottom_Panel.Location = new System.Drawing.Point(0, 469);
			this.Sermon_LeftBottom_Panel.Name = "Sermon_LeftBottom_Panel";
			this.Sermon_LeftBottom_Panel.Size = new System.Drawing.Size(430, 30);
			this.Sermon_LeftBottom_Panel.TabIndex = 5;
			// 
			// Sermon_BeamBox_Button
			// 
			this.Sermon_BeamBox_Button.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.Sermon_BeamBox_Button.Location = new System.Drawing.Point(296, 4);
			this.Sermon_BeamBox_Button.Name = "Sermon_BeamBox_Button";
			this.Sermon_BeamBox_Button.TabIndex = 2;
			this.Sermon_BeamBox_Button.Text = "To Projector";
			this.Sermon_BeamBox_Button.Click += new System.EventHandler(this.Sermon_BeamBox_Button_Click);
			// 
			// Sermon_LeftDoc_Panel
			// 
			this.Sermon_LeftDoc_Panel.Controls.Add(this.Sermon_DocManager);
			this.Sermon_LeftDoc_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Sermon_LeftDoc_Panel.Location = new System.Drawing.Point(0, 32);
			this.Sermon_LeftDoc_Panel.Name = "Sermon_LeftDoc_Panel";
			this.Sermon_LeftDoc_Panel.Size = new System.Drawing.Size(430, 467);
			this.Sermon_LeftDoc_Panel.TabIndex = 4;
			// 
			// Sermon_DocManager
			// 
			this.Sermon_DocManager.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Sermon_DocManager.Location = new System.Drawing.Point(0, 0);
			this.Sermon_DocManager.Name = "Sermon_DocManager";
			this.Sermon_DocManager.Size = new System.Drawing.Size(430, 467);
			this.Sermon_DocManager.TabIndex = 1;
			this.Sermon_DocManager.CloseButtonPressed += new DocumentManager.DocumentManager.CloseButtonPressedEventHandler(this.Sermon_DocManager_CloseButtonPressed);
			// 
			// Sermon_LeftToolBar_Panel
			// 
			this.Sermon_LeftToolBar_Panel.Controls.Add(this.Sermon_ToolBar);
			this.Sermon_LeftToolBar_Panel.Dock = System.Windows.Forms.DockStyle.Top;
			this.Sermon_LeftToolBar_Panel.Location = new System.Drawing.Point(0, 0);
			this.Sermon_LeftToolBar_Panel.Name = "Sermon_LeftToolBar_Panel";
			this.Sermon_LeftToolBar_Panel.Size = new System.Drawing.Size(430, 32);
			this.Sermon_LeftToolBar_Panel.TabIndex = 3;
			// 
			// Sermon_ToolBar
			// 
			this.Sermon_ToolBar.Buttons.AddRange(new TD.SandBar.ToolbarItemBase[] {
						this.Sermon_ToolBar_NewDoc_Button,
						this.Sermon_ToolBar_Font_Button,
						this.Sermon_ToolBar_Color_Button,
						this.Sermon_ToolBar_Outline_Button,
						this.Sermon_ToolBar_OutlineColor_Button});
			this.Sermon_ToolBar.Guid = new System.Guid("e8851e50-0b9f-4b93-8a5e-c7a9528f2cf6");
			this.Sermon_ToolBar.ImageList = null;
			this.Sermon_ToolBar.Location = new System.Drawing.Point(0, 0);
			this.Sermon_ToolBar.Name = "Sermon_ToolBar";
			this.Sermon_ToolBar.Size = new System.Drawing.Size(430, 24);
			this.Sermon_ToolBar.TabIndex = 0;
			this.Sermon_ToolBar.Text = "toolBar1";
			this.Sermon_ToolBar.ButtonClick += new TD.SandBar.ToolBar.ButtonClickEventHandler(this.Sermon_ToolBar_ButtonClick);
			// 
			// Sermon_ToolBar_NewDoc_Button
			// 
			this.Sermon_ToolBar_NewDoc_Button.BuddyMenu = null;
			this.Sermon_ToolBar_NewDoc_Button.Icon = null;
			this.Sermon_ToolBar_NewDoc_Button.Tag = null;
			this.Sermon_ToolBar_NewDoc_Button.Text = "New Document";
			// 
			// Sermon_ToolBar_Font_Button
			// 
			this.Sermon_ToolBar_Font_Button.BeginGroup = true;
			this.Sermon_ToolBar_Font_Button.BuddyMenu = null;
			this.Sermon_ToolBar_Font_Button.Icon = null;
			this.Sermon_ToolBar_Font_Button.Tag = null;
			this.Sermon_ToolBar_Font_Button.Text = "Font";
			// 
			// Sermon_ToolBar_Color_Button
			// 
			this.Sermon_ToolBar_Color_Button.BuddyMenu = null;
			this.Sermon_ToolBar_Color_Button.Icon = null;
			this.Sermon_ToolBar_Color_Button.Tag = null;
			this.Sermon_ToolBar_Color_Button.Text = "Color";
			// 
			// Sermon_ToolBar_Outline_Button
			// 
			this.Sermon_ToolBar_Outline_Button.BeginGroup = true;
			this.Sermon_ToolBar_Outline_Button.BuddyMenu = null;
			this.Sermon_ToolBar_Outline_Button.Icon = null;
			this.Sermon_ToolBar_Outline_Button.Tag = null;
			this.Sermon_ToolBar_Outline_Button.Text = "Text Outline";
			// 
			// Sermon_ToolBar_OutlineColor_Button
			// 
			this.Sermon_ToolBar_OutlineColor_Button.BuddyMenu = null;
			this.Sermon_ToolBar_OutlineColor_Button.Icon = null;
			this.Sermon_ToolBar_OutlineColor_Button.Tag = null;
			this.Sermon_ToolBar_OutlineColor_Button.Text = "Outline Color";
			// 
			// Sermon_TabControl
			// 
			this.Sermon_TabControl.Controls.Add(this.tabPage3);
			this.Sermon_TabControl.Dock = System.Windows.Forms.DockStyle.Right;
			this.Sermon_TabControl.Location = new System.Drawing.Point(430, 0);
			this.Sermon_TabControl.Name = "Sermon_TabControl";
			this.Sermon_TabControl.SelectedIndex = 0;
			this.Sermon_TabControl.Size = new System.Drawing.Size(144, 499);
			this.Sermon_TabControl.TabIndex = 2;
			// 
			// tabPage3
			// 
			this.tabPage3.BackColor = System.Drawing.Color.Transparent;
			this.tabPage3.Controls.Add(this.linkLabel1);
			this.tabPage3.Controls.Add(this.Sermon_Verse_Labe);
			this.tabPage3.Controls.Add(this.Sermon_Translation_Label);
			this.tabPage3.Controls.Add(this.Sermon_Books_Label);
			this.tabPage3.Controls.Add(this.Sermon_BibleKey);
			this.tabPage3.Controls.Add(this.Sermon_Testament_ListBox);
			this.tabPage3.Controls.Add(this.Sermon_Books);
			this.tabPage3.Controls.Add(this.Sermon_BookList);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Size = new System.Drawing.Size(136, 425);
			this.tabPage3.TabIndex = 0;
			this.tabPage3.Text = "Bible";
			// 
			// linkLabel1
			// 
			this.linkLabel1.LinkColor = System.Drawing.Color.Black;
			this.linkLabel1.Location = new System.Drawing.Point(24, 400);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(104, 15);
			this.linkLabel1.TabIndex = 7;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "get the Sword Bible";
			this.linkLabel1.Click += new System.EventHandler(this.linkLabel1_Click);
			// 
			// Sermon_Verse_Labe
			// 
			this.Sermon_Verse_Labe.Location = new System.Drawing.Point(8, 8);
			this.Sermon_Verse_Labe.Name = "Sermon_Verse_Labe";
			this.Sermon_Verse_Labe.Size = new System.Drawing.Size(112, 16);
			this.Sermon_Verse_Labe.TabIndex = 6;
			this.Sermon_Verse_Labe.Text = "Find  Verse:";
			// 
			// Sermon_Translation_Label
			// 
			this.Sermon_Translation_Label.Location = new System.Drawing.Point(8, 64);
			this.Sermon_Translation_Label.Name = "Sermon_Translation_Label";
			this.Sermon_Translation_Label.Size = new System.Drawing.Size(100, 16);
			this.Sermon_Translation_Label.TabIndex = 5;
			this.Sermon_Translation_Label.Text = "Translation:";
			// 
			// Sermon_Books_Label
			// 
			this.Sermon_Books_Label.Location = new System.Drawing.Point(8, 112);
			this.Sermon_Books_Label.Name = "Sermon_Books_Label";
			this.Sermon_Books_Label.Size = new System.Drawing.Size(100, 16);
			this.Sermon_Books_Label.TabIndex = 4;
			this.Sermon_Books_Label.Text = "Books:";
			// 
			// Sermon_BibleKey
			// 
			this.Sermon_BibleKey.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Sermon_BibleKey.Location = new System.Drawing.Point(8, 32);
			this.Sermon_BibleKey.Name = "Sermon_BibleKey";
			this.Sermon_BibleKey.Size = new System.Drawing.Size(128, 20);
			this.Sermon_BibleKey.TabIndex = 3;
			this.Sermon_BibleKey.Text = "";
			this.Sermon_BibleKey.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Sermon_BibleKey_KeyDown1);
			this.Sermon_BibleKey.TextChanged += new System.EventHandler(this.Sermon_BibleKey_TextChanged);
			// 
			// Sermon_Testament_ListBox
			// 
			this.Sermon_Testament_ListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Sermon_Testament_ListBox.Items.AddRange(new object[] {
						"Old Testament",
						"New Testament"});
			this.Sermon_Testament_ListBox.Location = new System.Drawing.Point(8, 128);
			this.Sermon_Testament_ListBox.Name = "Sermon_Testament_ListBox";
			this.Sermon_Testament_ListBox.Size = new System.Drawing.Size(128, 28);
			this.Sermon_Testament_ListBox.TabIndex = 2;
			this.Sermon_Testament_ListBox.SelectedIndexChanged += new System.EventHandler(this.Sermon_Testament_ListBox_SelectedIndexChanged);
			// 
			// Sermon_Books
			// 
			this.Sermon_Books.Location = new System.Drawing.Point(8, 80);
			this.Sermon_Books.Name = "Sermon_Books";
			this.Sermon_Books.Size = new System.Drawing.Size(120, 21);
			this.Sermon_Books.TabIndex = 1;
			this.Sermon_Books.SelectedIndexChanged += new System.EventHandler(this.Sermon_Books_SelectedIndexChanged);
			// 
			// Sermon_BookList
			// 
			this.Sermon_BookList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Sermon_BookList.Location = new System.Drawing.Point(8, 160);
			this.Sermon_BookList.Name = "Sermon_BookList";
			this.Sermon_BookList.Size = new System.Drawing.Size(128, 236);
			this.Sermon_BookList.TabIndex = 0;
			this.Sermon_BookList.SelectedIndexChanged += new System.EventHandler(this.Sermon_BookList_SelectedIndexChanged);
			// 
			// tabPage1
			// 
			this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
			this.tabPage1.Controls.Add(this.SongEdit_RightPanel);
			this.tabPage1.Controls.Add(this.SongEdit_BigInputFieldPanel);
			this.tabPage1.ImageIndex = 0;
			this.tabPage1.Location = new System.Drawing.Point(4, 24);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(574, 499);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Edit Songs";
			// 
			// SongEdit_RightPanel
			// 
			this.SongEdit_RightPanel.Controls.Add(this.SongEdit_TextAlign);
			this.SongEdit_RightPanel.Controls.Add(this.panel2);
			this.SongEdit_RightPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SongEdit_RightPanel.Location = new System.Drawing.Point(320, 0);
			this.SongEdit_RightPanel.Name = "SongEdit_RightPanel";
			this.SongEdit_RightPanel.Size = new System.Drawing.Size(254, 499);
			this.SongEdit_RightPanel.TabIndex = 34;
			// 
			// SongEdit_TextAlign
			// 
			this.SongEdit_TextAlign.Controls.Add(this.radioButton2);
			this.SongEdit_TextAlign.Controls.Add(this.radioButton1);
			this.SongEdit_TextAlign.Location = new System.Drawing.Point(8, 216);
			this.SongEdit_TextAlign.Name = "SongEdit_TextAlign";
			this.SongEdit_TextAlign.Size = new System.Drawing.Size(184, 80);
			this.SongEdit_TextAlign.TabIndex = 29;
			this.SongEdit_TextAlign.TabStop = false;
			this.SongEdit_TextAlign.Text = "Text Align";
			this.SongEdit_TextAlign.Visible = false;
			// 
			// radioButton2
			// 
			this.radioButton2.Location = new System.Drawing.Point(8, 48);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.TabIndex = 1;
			this.radioButton2.Text = "Centered";
			// 
			// radioButton1
			// 
			this.radioButton1.Checked = true;
			this.radioButton1.Location = new System.Drawing.Point(8, 24);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.TabIndex = 0;
			this.radioButton1.TabStop = true;
			this.radioButton1.Text = "Left";
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.SongEdit_UpdateBeamBox_Button);
			this.panel2.Controls.Add(this.SongEdit_BG_Panel);
			this.panel2.Controls.Add(this.SongEdit_PreviewStropheDown_Button);
			this.panel2.Controls.Add(this.SongEdit_PreviewStropheUp_Button);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point(0, 355);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(254, 144);
			this.panel2.TabIndex = 28;
			// 
			// SongEdit_BG_Panel
			// 
			this.SongEdit_BG_Panel.BackColor = System.Drawing.Color.Transparent;
			this.SongEdit_BG_Panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.SongEdit_BG_Panel.Controls.Add(this.SongEdit_BG_DecscriptionLabel);
			this.SongEdit_BG_Panel.Controls.Add(this.SongEdit_BG_Label);
			this.SongEdit_BG_Panel.Location = new System.Drawing.Point(0, 24);
			this.SongEdit_BG_Panel.Name = "SongEdit_BG_Panel";
			this.SongEdit_BG_Panel.Size = new System.Drawing.Size(196, 56);
			this.SongEdit_BG_Panel.TabIndex = 27;
			// 
			// SongEdit_BG_DecscriptionLabel
			// 
			this.SongEdit_BG_DecscriptionLabel.Location = new System.Drawing.Point(8, 1);
			this.SongEdit_BG_DecscriptionLabel.Name = "SongEdit_BG_DecscriptionLabel";
			this.SongEdit_BG_DecscriptionLabel.Size = new System.Drawing.Size(104, 23);
			this.SongEdit_BG_DecscriptionLabel.TabIndex = 2;
			this.SongEdit_BG_DecscriptionLabel.Text = "Background Image:";
			// 
			// SongEdit_PreviewStropheDown_Button
			// 
			this.SongEdit_PreviewStropheDown_Button.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.SongEdit_PreviewStropheDown_Button.Location = new System.Drawing.Point(0, 0);
			this.SongEdit_PreviewStropheDown_Button.Name = "SongEdit_PreviewStropheDown_Button";
			this.SongEdit_PreviewStropheDown_Button.Size = new System.Drawing.Size(98, 19);
			this.SongEdit_PreviewStropheDown_Button.TabIndex = 26;
			this.SongEdit_PreviewStropheDown_Button.Text = "Previous Verse";
			this.SongEdit_PreviewStropheDown_Button.Click += new System.EventHandler(this.SongEdit_PreviewStropheDown_Button_Click);
			// 
			// SongEdit_PreviewStropheUp_Button
			// 
			this.SongEdit_PreviewStropheUp_Button.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.SongEdit_PreviewStropheUp_Button.Location = new System.Drawing.Point(96, 0);
			this.SongEdit_PreviewStropheUp_Button.Name = "SongEdit_PreviewStropheUp_Button";
			this.SongEdit_PreviewStropheUp_Button.Size = new System.Drawing.Size(98, 19);
			this.SongEdit_PreviewStropheUp_Button.TabIndex = 25;
			this.SongEdit_PreviewStropheUp_Button.Text = "Next Verse";
			this.SongEdit_PreviewStropheUp_Button.Click += new System.EventHandler(this.SongEdit_PreviewStropheUp_Button_Click);
			// 
			// SongEdit_BigInputFieldPanel
			// 
			this.SongEdit_BigInputFieldPanel.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.SongEdit_BigInputFieldPanel.Controls.Add(this.SongEdit_InputFieldPanelMid);
			this.SongEdit_BigInputFieldPanel.Controls.Add(this.SongEdit_InputFieldPanelButtom);
			this.SongEdit_BigInputFieldPanel.Controls.Add(this.SongEdit_InputFieldPanelTop);
			this.SongEdit_BigInputFieldPanel.Dock = System.Windows.Forms.DockStyle.Left;
			this.SongEdit_BigInputFieldPanel.Location = new System.Drawing.Point(0, 0);
			this.SongEdit_BigInputFieldPanel.Name = "SongEdit_BigInputFieldPanel";
			this.SongEdit_BigInputFieldPanel.Size = new System.Drawing.Size(320, 499);
			this.SongEdit_BigInputFieldPanel.TabIndex = 33;
			// 
			// SongEdit_InputFieldPanelMid
			// 
			this.SongEdit_InputFieldPanelMid.Controls.Add(this.SongEdit_InputFieldBelowMenuPanelMid);
			this.SongEdit_InputFieldPanelMid.Controls.Add(this.SongEdit_InputFieldMenuPanelMid);
			this.SongEdit_InputFieldPanelMid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SongEdit_InputFieldPanelMid.Location = new System.Drawing.Point(0, 90);
			this.SongEdit_InputFieldPanelMid.Name = "SongEdit_InputFieldPanelMid";
			this.SongEdit_InputFieldPanelMid.Size = new System.Drawing.Size(320, 319);
			this.SongEdit_InputFieldPanelMid.TabIndex = 2;
			// 
			// SongEdit_InputFieldBelowMenuPanelMid
			// 
			this.SongEdit_InputFieldBelowMenuPanelMid.Controls.Add(this.SongEdit_InputFieldBelowMenuPane2lMid);
			this.SongEdit_InputFieldBelowMenuPanelMid.Controls.Add(this.SongEdit_MidPos_Panel);
			this.SongEdit_InputFieldBelowMenuPanelMid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SongEdit_InputFieldBelowMenuPanelMid.Location = new System.Drawing.Point(0, 24);
			this.SongEdit_InputFieldBelowMenuPanelMid.Name = "SongEdit_InputFieldBelowMenuPanelMid";
			this.SongEdit_InputFieldBelowMenuPanelMid.Size = new System.Drawing.Size(320, 295);
			this.SongEdit_InputFieldBelowMenuPanelMid.TabIndex = 23;
			// 
			// SongEdit_InputFieldBelowMenuPane2lMid
			// 
			this.SongEdit_InputFieldBelowMenuPane2lMid.Controls.Add(this.SongEdit_Mid_TextBox);
			this.SongEdit_InputFieldBelowMenuPane2lMid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SongEdit_InputFieldBelowMenuPane2lMid.Location = new System.Drawing.Point(0, 0);
			this.SongEdit_InputFieldBelowMenuPane2lMid.Name = "SongEdit_InputFieldBelowMenuPane2lMid";
			this.SongEdit_InputFieldBelowMenuPane2lMid.Size = new System.Drawing.Size(320, 275);
			this.SongEdit_InputFieldBelowMenuPane2lMid.TabIndex = 2;
			// 
			// SongEdit_MidPos_Panel
			// 
			this.SongEdit_MidPos_Panel.BackColor = System.Drawing.Color.White;
			this.SongEdit_MidPos_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.SongEdit_MidPos_Panel.Controls.Add(this.SongEdit_Header_Verses);
			this.SongEdit_MidPos_Panel.Controls.Add(this.SongEdit_Mid_AutoPos_CheckBox);
			this.SongEdit_MidPos_Panel.Controls.Add(this.SongEdit_MidPosX_Label);
			this.SongEdit_MidPos_Panel.Controls.Add(this.SongEdit_MidPosX_UpDown);
			this.SongEdit_MidPos_Panel.Controls.Add(this.SongEdit_MidPosY_UpDown);
			this.SongEdit_MidPos_Panel.Controls.Add(this.SongEdit_MidPosY_Label);
			this.SongEdit_MidPos_Panel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.SongEdit_MidPos_Panel.Location = new System.Drawing.Point(0, 275);
			this.SongEdit_MidPos_Panel.Name = "SongEdit_MidPos_Panel";
			this.SongEdit_MidPos_Panel.Size = new System.Drawing.Size(320, 20);
			this.SongEdit_MidPos_Panel.TabIndex = 1;
			// 
			// SongEdit_Header_Verses
			// 
			this.SongEdit_Header_Verses.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SongEdit_Header_Verses.Location = new System.Drawing.Point(1, 2);
			this.SongEdit_Header_Verses.Name = "SongEdit_Header_Verses";
			this.SongEdit_Header_Verses.Size = new System.Drawing.Size(49, 27);
			this.SongEdit_Header_Verses.TabIndex = 7;
			this.SongEdit_Header_Verses.Text = "Verses";
			// 
			// SongEdit_Mid_AutoPos_CheckBox
			// 
			this.SongEdit_Mid_AutoPos_CheckBox.Location = new System.Drawing.Point(55, -2);
			this.SongEdit_Mid_AutoPos_CheckBox.Name = "SongEdit_Mid_AutoPos_CheckBox";
			this.SongEdit_Mid_AutoPos_CheckBox.Size = new System.Drawing.Size(90, 24);
			this.SongEdit_Mid_AutoPos_CheckBox.TabIndex = 4;
			this.SongEdit_Mid_AutoPos_CheckBox.Text = "Auto Position";
			this.SongEdit_Mid_AutoPos_CheckBox.Click += new System.EventHandler(this.checkBox1_Click);
			// 
			// SongEdit_MidPosX_Label
			// 
			this.SongEdit_MidPosX_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SongEdit_MidPosX_Label.Location = new System.Drawing.Point(152, 0);
			this.SongEdit_MidPosX_Label.Name = "SongEdit_MidPosX_Label";
			this.SongEdit_MidPosX_Label.Size = new System.Drawing.Size(22, 16);
			this.SongEdit_MidPosX_Label.TabIndex = 3;
			this.SongEdit_MidPosX_Label.Text = "X:";
			// 
			// SongEdit_MidPosX_UpDown
			// 
			this.SongEdit_MidPosX_UpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.SongEdit_MidPosX_UpDown.Location = new System.Drawing.Point(176, -1);
			this.SongEdit_MidPosX_UpDown.Maximum = new System.Decimal(new int[] {
						10000,
						0,
						0,
						0});
			this.SongEdit_MidPosX_UpDown.Minimum = new System.Decimal(new int[] {
						10000,
						0,
						0,
						-2147483648});
			this.SongEdit_MidPosX_UpDown.Name = "SongEdit_MidPosX_UpDown";
			this.SongEdit_MidPosX_UpDown.Size = new System.Drawing.Size(57, 20);
			this.SongEdit_MidPosX_UpDown.TabIndex = 1;
			this.SongEdit_MidPosX_UpDown.ValueChanged += new System.EventHandler(this.SongEdit_MidPosX_UpDown_ValueChanged);
			this.SongEdit_MidPosX_UpDown.Leave += new System.EventHandler(this.SongEdit_MidPosX_UpDown_ValueChanged);
			// 
			// SongEdit_MidPosY_UpDown
			// 
			this.SongEdit_MidPosY_UpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.SongEdit_MidPosY_UpDown.Location = new System.Drawing.Point(263, -1);
			this.SongEdit_MidPosY_UpDown.Maximum = new System.Decimal(new int[] {
						1000,
						0,
						0,
						0});
			this.SongEdit_MidPosY_UpDown.Minimum = new System.Decimal(new int[] {
						10000,
						0,
						0,
						-2147483648});
			this.SongEdit_MidPosY_UpDown.Name = "SongEdit_MidPosY_UpDown";
			this.SongEdit_MidPosY_UpDown.Size = new System.Drawing.Size(57, 20);
			this.SongEdit_MidPosY_UpDown.TabIndex = 0;
			this.SongEdit_MidPosY_UpDown.ValueChanged += new System.EventHandler(this.SongEdit_MidPosY_UpDown_ValueChanged);
			this.SongEdit_MidPosY_UpDown.Leave += new System.EventHandler(this.SongEdit_MidPosY_UpDown_ValueChanged);
			// 
			// SongEdit_MidPosY_Label
			// 
			this.SongEdit_MidPosY_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SongEdit_MidPosY_Label.Location = new System.Drawing.Point(238, 0);
			this.SongEdit_MidPosY_Label.Name = "SongEdit_MidPosY_Label";
			this.SongEdit_MidPosY_Label.Size = new System.Drawing.Size(22, 16);
			this.SongEdit_MidPosY_Label.TabIndex = 2;
			this.SongEdit_MidPosY_Label.Text = "Y:";
			// 
			// SongEdit_InputFieldMenuPanelMid
			// 
			this.SongEdit_InputFieldMenuPanelMid.Controls.Add(this.SongEdit_MidTextToolBar);
			this.SongEdit_InputFieldMenuPanelMid.Dock = System.Windows.Forms.DockStyle.Top;
			this.SongEdit_InputFieldMenuPanelMid.Location = new System.Drawing.Point(0, 0);
			this.SongEdit_InputFieldMenuPanelMid.Name = "SongEdit_InputFieldMenuPanelMid";
			this.SongEdit_InputFieldMenuPanelMid.Size = new System.Drawing.Size(320, 24);
			this.SongEdit_InputFieldMenuPanelMid.TabIndex = 22;
			// 
			// SongEdit_MidTextToolBar
			// 
			this.SongEdit_MidTextToolBar.Buttons.AddRange(new TD.SandBar.ToolbarItemBase[] {
						this.SongEdit_ButtonMidFont,
						this.SongEdit_ButtonMidColor,
						this.SongEdit_ButtonMidTextOutline,
						this.SongEdit_ButtonMidOutlineColor});
			this.SongEdit_MidTextToolBar.Guid = new System.Guid("5e4663b1-8020-467d-85b8-24bb2fd034ca");
			this.SongEdit_MidTextToolBar.ImageList = null;
			this.SongEdit_MidTextToolBar.Location = new System.Drawing.Point(0, 0);
			this.SongEdit_MidTextToolBar.Name = "SongEdit_MidTextToolBar";
			this.SongEdit_MidTextToolBar.Size = new System.Drawing.Size(320, 24);
			this.SongEdit_MidTextToolBar.TabIndex = 0;
			this.SongEdit_MidTextToolBar.Text = "SongEdit_MidTextToolBar";
			this.SongEdit_MidTextToolBar.ButtonClick += new TD.SandBar.ToolBar.ButtonClickEventHandler(this.SongEdit_MidTextToolBar_ButtonClick);
			// 
			// SongEdit_ButtonMidFont
			// 
			this.SongEdit_ButtonMidFont.BuddyMenu = null;
			this.SongEdit_ButtonMidFont.Icon = null;
			this.SongEdit_ButtonMidFont.Tag = null;
			this.SongEdit_ButtonMidFont.Text = "Font";
			// 
			// SongEdit_ButtonMidColor
			// 
			this.SongEdit_ButtonMidColor.BuddyMenu = null;
			this.SongEdit_ButtonMidColor.Icon = null;
			this.SongEdit_ButtonMidColor.Tag = null;
			this.SongEdit_ButtonMidColor.Text = "Color";
			// 
			// SongEdit_ButtonMidTextOutline
			// 
			this.SongEdit_ButtonMidTextOutline.BeginGroup = true;
			this.SongEdit_ButtonMidTextOutline.BuddyMenu = null;
			this.SongEdit_ButtonMidTextOutline.Icon = null;
			this.SongEdit_ButtonMidTextOutline.Tag = null;
			this.SongEdit_ButtonMidTextOutline.Text = "TextOutline";
			// 
			// SongEdit_ButtonMidOutlineColor
			// 
			this.SongEdit_ButtonMidOutlineColor.BuddyMenu = null;
			this.SongEdit_ButtonMidOutlineColor.Icon = null;
			this.SongEdit_ButtonMidOutlineColor.Tag = null;
			this.SongEdit_ButtonMidOutlineColor.Text = "Outline Color";
			this.SongEdit_ButtonMidOutlineColor.Visible = false;
			// 
			// SongEdit_InputFieldPanelButtom
			// 
			this.SongEdit_InputFieldPanelButtom.Controls.Add(this.SongEdit_InputFieldBelowMenuPanelButtom);
			this.SongEdit_InputFieldPanelButtom.Controls.Add(this.SongEdit_InputFieldMenuPanelButtom);
			this.SongEdit_InputFieldPanelButtom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.SongEdit_InputFieldPanelButtom.Location = new System.Drawing.Point(0, 409);
			this.SongEdit_InputFieldPanelButtom.Name = "SongEdit_InputFieldPanelButtom";
			this.SongEdit_InputFieldPanelButtom.Size = new System.Drawing.Size(320, 90);
			this.SongEdit_InputFieldPanelButtom.TabIndex = 1;
			// 
			// SongEdit_InputFieldBelowMenuPanelButtom
			// 
			this.SongEdit_InputFieldBelowMenuPanelButtom.Controls.Add(this.SongEdit_BottomPos_Panel);
			this.SongEdit_InputFieldBelowMenuPanelButtom.Controls.Add(this.Footer_TextBox);
			this.SongEdit_InputFieldBelowMenuPanelButtom.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SongEdit_InputFieldBelowMenuPanelButtom.Location = new System.Drawing.Point(0, 24);
			this.SongEdit_InputFieldBelowMenuPanelButtom.Name = "SongEdit_InputFieldBelowMenuPanelButtom";
			this.SongEdit_InputFieldBelowMenuPanelButtom.Size = new System.Drawing.Size(320, 66);
			this.SongEdit_InputFieldBelowMenuPanelButtom.TabIndex = 1;
			// 
			// SongEdit_BottomPos_Panel
			// 
			this.SongEdit_BottomPos_Panel.BackColor = System.Drawing.Color.White;
			this.SongEdit_BottomPos_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.SongEdit_BottomPos_Panel.Controls.Add(this.SongEdit_Header_Author);
			this.SongEdit_BottomPos_Panel.Controls.Add(this.SongEdit_Bottom_AutoPos_CheckBox);
			this.SongEdit_BottomPos_Panel.Controls.Add(this.SongEdit_BottomPosX_Label);
			this.SongEdit_BottomPos_Panel.Controls.Add(this.SongEdit_BottomPosX_UpDown);
			this.SongEdit_BottomPos_Panel.Controls.Add(this.SongEdit_BottomPosY_UpDown);
			this.SongEdit_BottomPos_Panel.Controls.Add(this.SongEdit_BottomPosY_Label);
			this.SongEdit_BottomPos_Panel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.SongEdit_BottomPos_Panel.Location = new System.Drawing.Point(0, 46);
			this.SongEdit_BottomPos_Panel.Name = "SongEdit_BottomPos_Panel";
			this.SongEdit_BottomPos_Panel.Size = new System.Drawing.Size(320, 20);
			this.SongEdit_BottomPos_Panel.TabIndex = 1;
			// 
			// SongEdit_Header_Author
			// 
			this.SongEdit_Header_Author.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SongEdit_Header_Author.Location = new System.Drawing.Point(1, 2);
			this.SongEdit_Header_Author.Name = "SongEdit_Header_Author";
			this.SongEdit_Header_Author.Size = new System.Drawing.Size(49, 27);
			this.SongEdit_Header_Author.TabIndex = 8;
			this.SongEdit_Header_Author.Text = "Author";
			// 
			// SongEdit_Bottom_AutoPos_CheckBox
			// 
			this.SongEdit_Bottom_AutoPos_CheckBox.Location = new System.Drawing.Point(55, -2);
			this.SongEdit_Bottom_AutoPos_CheckBox.Name = "SongEdit_Bottom_AutoPos_CheckBox";
			this.SongEdit_Bottom_AutoPos_CheckBox.Size = new System.Drawing.Size(90, 24);
			this.SongEdit_Bottom_AutoPos_CheckBox.TabIndex = 5;
			this.SongEdit_Bottom_AutoPos_CheckBox.Text = "Auto Position";
			this.SongEdit_Bottom_AutoPos_CheckBox.Click += new System.EventHandler(this.SongEdit_Bottom_AutoPos_CheckBox_Click);
			// 
			// SongEdit_BottomPosX_Label
			// 
			this.SongEdit_BottomPosX_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SongEdit_BottomPosX_Label.Location = new System.Drawing.Point(152, 0);
			this.SongEdit_BottomPosX_Label.Name = "SongEdit_BottomPosX_Label";
			this.SongEdit_BottomPosX_Label.Size = new System.Drawing.Size(22, 16);
			this.SongEdit_BottomPosX_Label.TabIndex = 3;
			this.SongEdit_BottomPosX_Label.Text = "X:";
			// 
			// SongEdit_BottomPosX_UpDown
			// 
			this.SongEdit_BottomPosX_UpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.SongEdit_BottomPosX_UpDown.Location = new System.Drawing.Point(176, -1);
			this.SongEdit_BottomPosX_UpDown.Maximum = new System.Decimal(new int[] {
						10000,
						0,
						0,
						0});
			this.SongEdit_BottomPosX_UpDown.Minimum = new System.Decimal(new int[] {
						10000,
						0,
						0,
						-2147483648});
			this.SongEdit_BottomPosX_UpDown.Name = "SongEdit_BottomPosX_UpDown";
			this.SongEdit_BottomPosX_UpDown.Size = new System.Drawing.Size(57, 20);
			this.SongEdit_BottomPosX_UpDown.TabIndex = 1;
			this.SongEdit_BottomPosX_UpDown.ValueChanged += new System.EventHandler(this.SongEdit_BottomPosX_UpDown_ValueChanged);
			this.SongEdit_BottomPosX_UpDown.Leave += new System.EventHandler(this.SongEdit_BottomPosX_UpDown_ValueChanged);
			// 
			// SongEdit_BottomPosY_UpDown
			// 
			this.SongEdit_BottomPosY_UpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.SongEdit_BottomPosY_UpDown.Location = new System.Drawing.Point(263, -1);
			this.SongEdit_BottomPosY_UpDown.Maximum = new System.Decimal(new int[] {
						1000,
						0,
						0,
						0});
			this.SongEdit_BottomPosY_UpDown.Minimum = new System.Decimal(new int[] {
						10000,
						0,
						0,
						-2147483648});
			this.SongEdit_BottomPosY_UpDown.Name = "SongEdit_BottomPosY_UpDown";
			this.SongEdit_BottomPosY_UpDown.Size = new System.Drawing.Size(57, 20);
			this.SongEdit_BottomPosY_UpDown.TabIndex = 0;
			this.SongEdit_BottomPosY_UpDown.ValueChanged += new System.EventHandler(this.SongEdit_BottomPosY_UpDown_ValueChanged);
			this.SongEdit_BottomPosY_UpDown.Leave += new System.EventHandler(this.SongEdit_BottomPosY_UpDown_ValueChanged);
			// 
			// SongEdit_BottomPosY_Label
			// 
			this.SongEdit_BottomPosY_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SongEdit_BottomPosY_Label.Location = new System.Drawing.Point(238, 0);
			this.SongEdit_BottomPosY_Label.Name = "SongEdit_BottomPosY_Label";
			this.SongEdit_BottomPosY_Label.Size = new System.Drawing.Size(22, 16);
			this.SongEdit_BottomPosY_Label.TabIndex = 2;
			this.SongEdit_BottomPosY_Label.Text = "Y:";
			// 
			// SongEdit_InputFieldMenuPanelButtom
			// 
			this.SongEdit_InputFieldMenuPanelButtom.Controls.Add(this.SongEdit_BottomTextToolBar);
			this.SongEdit_InputFieldMenuPanelButtom.Dock = System.Windows.Forms.DockStyle.Top;
			this.SongEdit_InputFieldMenuPanelButtom.Location = new System.Drawing.Point(0, 0);
			this.SongEdit_InputFieldMenuPanelButtom.Name = "SongEdit_InputFieldMenuPanelButtom";
			this.SongEdit_InputFieldMenuPanelButtom.Size = new System.Drawing.Size(320, 24);
			this.SongEdit_InputFieldMenuPanelButtom.TabIndex = 0;
			// 
			// SongEdit_BottomTextToolBar
			// 
			this.SongEdit_BottomTextToolBar.Buttons.AddRange(new TD.SandBar.ToolbarItemBase[] {
						this.SongEdit_ButtonBottomFont,
						this.SongEdit_ButtonBottomColor,
						this.SongEdit_ButtonBottomTextOutline,
						this.SongEdit_ButtonBottomOutlineColor});
			this.SongEdit_BottomTextToolBar.Guid = new System.Guid("a23bff90-563e-42cb-8b1e-446ec44cb0da");
			this.SongEdit_BottomTextToolBar.ImageList = null;
			this.SongEdit_BottomTextToolBar.Location = new System.Drawing.Point(0, 0);
			this.SongEdit_BottomTextToolBar.Name = "SongEdit_BottomTextToolBar";
			this.SongEdit_BottomTextToolBar.Size = new System.Drawing.Size(320, 24);
			this.SongEdit_BottomTextToolBar.TabIndex = 0;
			this.SongEdit_BottomTextToolBar.Text = "SongEdit_BottomTextToolBar";
			this.SongEdit_BottomTextToolBar.ButtonClick += new TD.SandBar.ToolBar.ButtonClickEventHandler(this.SongEdit_BottomTextToolBar_ButtonClick);
			// 
			// SongEdit_ButtonBottomFont
			// 
			this.SongEdit_ButtonBottomFont.BuddyMenu = null;
			this.SongEdit_ButtonBottomFont.Icon = null;
			this.SongEdit_ButtonBottomFont.Tag = null;
			this.SongEdit_ButtonBottomFont.Text = "Font";
			// 
			// SongEdit_ButtonBottomColor
			// 
			this.SongEdit_ButtonBottomColor.BuddyMenu = null;
			this.SongEdit_ButtonBottomColor.Icon = null;
			this.SongEdit_ButtonBottomColor.Tag = null;
			this.SongEdit_ButtonBottomColor.Text = "Color";
			// 
			// SongEdit_ButtonBottomTextOutline
			// 
			this.SongEdit_ButtonBottomTextOutline.BeginGroup = true;
			this.SongEdit_ButtonBottomTextOutline.BuddyMenu = null;
			this.SongEdit_ButtonBottomTextOutline.Icon = null;
			this.SongEdit_ButtonBottomTextOutline.Tag = null;
			this.SongEdit_ButtonBottomTextOutline.Text = "TextOutline";
			// 
			// SongEdit_ButtonBottomOutlineColor
			// 
			this.SongEdit_ButtonBottomOutlineColor.BuddyMenu = null;
			this.SongEdit_ButtonBottomOutlineColor.Icon = null;
			this.SongEdit_ButtonBottomOutlineColor.Tag = null;
			this.SongEdit_ButtonBottomOutlineColor.Text = "OutlineColor";
			this.SongEdit_ButtonBottomOutlineColor.Visible = false;
			// 
			// SongEdit_InputFieldPanelTop
			// 
			this.SongEdit_InputFieldPanelTop.Controls.Add(this.SongEdit_InputPanelTopBelowMenu);
			this.SongEdit_InputFieldPanelTop.Controls.Add(this.SongEdit_InputPanelTopMenu);
			this.SongEdit_InputFieldPanelTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.SongEdit_InputFieldPanelTop.Location = new System.Drawing.Point(0, 0);
			this.SongEdit_InputFieldPanelTop.Name = "SongEdit_InputFieldPanelTop";
			this.SongEdit_InputFieldPanelTop.Size = new System.Drawing.Size(320, 90);
			this.SongEdit_InputFieldPanelTop.TabIndex = 0;
			// 
			// SongEdit_InputPanelTopBelowMenu
			// 
			this.SongEdit_InputPanelTopBelowMenu.BackColor = System.Drawing.SystemColors.Highlight;
			this.SongEdit_InputPanelTopBelowMenu.Controls.Add(this.SongEdit_TopPos_Panel);
			this.SongEdit_InputPanelTopBelowMenu.Controls.Add(this.SongEdit_Header_TextBox);
			this.SongEdit_InputPanelTopBelowMenu.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SongEdit_InputPanelTopBelowMenu.Location = new System.Drawing.Point(0, 24);
			this.SongEdit_InputPanelTopBelowMenu.Name = "SongEdit_InputPanelTopBelowMenu";
			this.SongEdit_InputPanelTopBelowMenu.Size = new System.Drawing.Size(320, 66);
			this.SongEdit_InputPanelTopBelowMenu.TabIndex = 22;
			// 
			// SongEdit_TopPos_Panel
			// 
			this.SongEdit_TopPos_Panel.BackColor = System.Drawing.Color.White;
			this.SongEdit_TopPos_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.SongEdit_TopPos_Panel.Controls.Add(this.SongEdit_Header_Title);
			this.SongEdit_TopPos_Panel.Controls.Add(this.SongEdit_Top_AutoPos_CheckBox);
			this.SongEdit_TopPos_Panel.Controls.Add(this.SongEdit_TopPosX_Label);
			this.SongEdit_TopPos_Panel.Controls.Add(this.SongEdit_TopPosX_UpDown);
			this.SongEdit_TopPos_Panel.Controls.Add(this.SongEdit_TopPosY_UpDown);
			this.SongEdit_TopPos_Panel.Controls.Add(this.SongEdit_TopPosY_Label);
			this.SongEdit_TopPos_Panel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.SongEdit_TopPos_Panel.Location = new System.Drawing.Point(0, 46);
			this.SongEdit_TopPos_Panel.Name = "SongEdit_TopPos_Panel";
			this.SongEdit_TopPos_Panel.Size = new System.Drawing.Size(320, 20);
			this.SongEdit_TopPos_Panel.TabIndex = 0;
			// 
			// SongEdit_Header_Title
			// 
			this.SongEdit_Header_Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SongEdit_Header_Title.Location = new System.Drawing.Point(1, 2);
			this.SongEdit_Header_Title.Name = "SongEdit_Header_Title";
			this.SongEdit_Header_Title.Size = new System.Drawing.Size(49, 27);
			this.SongEdit_Header_Title.TabIndex = 6;
			this.SongEdit_Header_Title.Text = "Title";
			// 
			// SongEdit_Top_AutoPos_CheckBox
			// 
			this.SongEdit_Top_AutoPos_CheckBox.Location = new System.Drawing.Point(55, -2);
			this.SongEdit_Top_AutoPos_CheckBox.Name = "SongEdit_Top_AutoPos_CheckBox";
			this.SongEdit_Top_AutoPos_CheckBox.Size = new System.Drawing.Size(91, 24);
			this.SongEdit_Top_AutoPos_CheckBox.TabIndex = 5;
			this.SongEdit_Top_AutoPos_CheckBox.Text = "Auto Position";
			this.SongEdit_Top_AutoPos_CheckBox.Click += new System.EventHandler(this.SongEdit_Top_AutoPos_CheckBox_Click);
			// 
			// SongEdit_TopPosX_Label
			// 
			this.SongEdit_TopPosX_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SongEdit_TopPosX_Label.Location = new System.Drawing.Point(152, 0);
			this.SongEdit_TopPosX_Label.Name = "SongEdit_TopPosX_Label";
			this.SongEdit_TopPosX_Label.Size = new System.Drawing.Size(22, 16);
			this.SongEdit_TopPosX_Label.TabIndex = 3;
			this.SongEdit_TopPosX_Label.Text = "X:";
			// 
			// SongEdit_TopPosX_UpDown
			// 
			this.SongEdit_TopPosX_UpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.SongEdit_TopPosX_UpDown.Location = new System.Drawing.Point(176, -1);
			this.SongEdit_TopPosX_UpDown.Maximum = new System.Decimal(new int[] {
						10000,
						0,
						0,
						0});
			this.SongEdit_TopPosX_UpDown.Minimum = new System.Decimal(new int[] {
						10000,
						0,
						0,
						-2147483648});
			this.SongEdit_TopPosX_UpDown.Name = "SongEdit_TopPosX_UpDown";
			this.SongEdit_TopPosX_UpDown.Size = new System.Drawing.Size(57, 20);
			this.SongEdit_TopPosX_UpDown.TabIndex = 1;
			this.SongEdit_TopPosX_UpDown.DragLeave += new System.EventHandler(this.SongEdit_TopPosX_UpDown_ValueChanged);
			this.SongEdit_TopPosX_UpDown.ValueChanged += new System.EventHandler(this.SongEdit_TopPosX_UpDown_ValueChanged);
			this.SongEdit_TopPosX_UpDown.Leave += new System.EventHandler(this.SongEdit_TopPosX_UpDown_ValueChanged);
			// 
			// SongEdit_TopPosY_UpDown
			// 
			this.SongEdit_TopPosY_UpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.SongEdit_TopPosY_UpDown.Location = new System.Drawing.Point(263, -1);
			this.SongEdit_TopPosY_UpDown.Maximum = new System.Decimal(new int[] {
						1000,
						0,
						0,
						0});
			this.SongEdit_TopPosY_UpDown.Minimum = new System.Decimal(new int[] {
						10000,
						0,
						0,
						-2147483648});
			this.SongEdit_TopPosY_UpDown.Name = "SongEdit_TopPosY_UpDown";
			this.SongEdit_TopPosY_UpDown.Size = new System.Drawing.Size(57, 20);
			this.SongEdit_TopPosY_UpDown.TabIndex = 0;
			this.SongEdit_TopPosY_UpDown.ValueChanged += new System.EventHandler(this.SongEdit_TopPosY_UpDown_ValueChanged);
			this.SongEdit_TopPosY_UpDown.Leave += new System.EventHandler(this.SongEdit_TopPosY_UpDown_ValueChanged);
			// 
			// SongEdit_TopPosY_Label
			// 
			this.SongEdit_TopPosY_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SongEdit_TopPosY_Label.Location = new System.Drawing.Point(238, 0);
			this.SongEdit_TopPosY_Label.Name = "SongEdit_TopPosY_Label";
			this.SongEdit_TopPosY_Label.Size = new System.Drawing.Size(22, 16);
			this.SongEdit_TopPosY_Label.TabIndex = 2;
			this.SongEdit_TopPosY_Label.Text = "Y:";
			// 
			// SongEdit_Header_TextBox
			// 
			this.SongEdit_Header_TextBox.BackColor = System.Drawing.Color.White;
			this.SongEdit_Header_TextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.SongEdit_Header_TextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SongEdit_Header_TextBox.Location = new System.Drawing.Point(0, 0);
			this.SongEdit_Header_TextBox.Multiline = true;
			this.SongEdit_Header_TextBox.Name = "SongEdit_Header_TextBox";
			this.SongEdit_Header_TextBox.Size = new System.Drawing.Size(320, 66);
			this.SongEdit_Header_TextBox.TabIndex = 20;
			this.SongEdit_Header_TextBox.Text = "";
			this.SongEdit_Header_TextBox.TextChanged += new System.EventHandler(this.SongEdit_Header_TextBox_TextChanged);
			this.SongEdit_Header_TextBox.Enter += new System.EventHandler(this.SongEdit_Header_TextBox_Enter);
			// 
			// SongEdit_InputPanelTopMenu
			// 
			this.SongEdit_InputPanelTopMenu.Controls.Add(this.SongEdit_TopTextToolBar);
			this.SongEdit_InputPanelTopMenu.Dock = System.Windows.Forms.DockStyle.Top;
			this.SongEdit_InputPanelTopMenu.Location = new System.Drawing.Point(0, 0);
			this.SongEdit_InputPanelTopMenu.Name = "SongEdit_InputPanelTopMenu";
			this.SongEdit_InputPanelTopMenu.Size = new System.Drawing.Size(320, 24);
			this.SongEdit_InputPanelTopMenu.TabIndex = 21;
			// 
			// SongEdit_TopTextToolBar
			// 
			this.SongEdit_TopTextToolBar.Buttons.AddRange(new TD.SandBar.ToolbarItemBase[] {
						this.SongEdit_ButtonTopFont,
						this.SongEdit_ButtonTopColor,
						this.SongEdit_ButtonTopTextOutline,
						this.SongEdit_ButtonTopOutlineColor});
			this.SongEdit_TopTextToolBar.Guid = new System.Guid("580d7392-4fcd-4dbd-bc2a-4ed0234aad2a");
			this.SongEdit_TopTextToolBar.ImageList = null;
			this.SongEdit_TopTextToolBar.Location = new System.Drawing.Point(0, 0);
			this.SongEdit_TopTextToolBar.Name = "SongEdit_TopTextToolBar";
			this.SongEdit_TopTextToolBar.Size = new System.Drawing.Size(320, 24);
			this.SongEdit_TopTextToolBar.TabIndex = 0;
			this.SongEdit_TopTextToolBar.Text = "";
			this.SongEdit_TopTextToolBar.ButtonClick += new TD.SandBar.ToolBar.ButtonClickEventHandler(this.SongEdit_TopTextToolBar_ButtonClick);
			// 
			// SongEdit_ButtonTopFont
			// 
			this.SongEdit_ButtonTopFont.BuddyMenu = null;
			this.SongEdit_ButtonTopFont.Icon = null;
			this.SongEdit_ButtonTopFont.Tag = null;
			this.SongEdit_ButtonTopFont.Text = "Font";
			// 
			// SongEdit_ButtonTopColor
			// 
			this.SongEdit_ButtonTopColor.BuddyMenu = null;
			this.SongEdit_ButtonTopColor.Icon = null;
			this.SongEdit_ButtonTopColor.Tag = null;
			this.SongEdit_ButtonTopColor.Text = "Color";
			// 
			// SongEdit_ButtonTopTextOutline
			// 
			this.SongEdit_ButtonTopTextOutline.BeginGroup = true;
			this.SongEdit_ButtonTopTextOutline.BuddyMenu = null;
			this.SongEdit_ButtonTopTextOutline.Icon = null;
			this.SongEdit_ButtonTopTextOutline.Tag = null;
			this.SongEdit_ButtonTopTextOutline.Text = "TextOutline";
			// 
			// SongEdit_ButtonTopOutlineColor
			// 
			this.SongEdit_ButtonTopOutlineColor.BuddyMenu = null;
			this.SongEdit_ButtonTopOutlineColor.Icon = null;
			this.SongEdit_ButtonTopOutlineColor.Tag = null;
			this.SongEdit_ButtonTopOutlineColor.Text = "Outline Color";
			this.SongEdit_ButtonTopOutlineColor.Visible = false;
			// 
			// tabPage4
			// 
			this.tabPage4.BackColor = System.Drawing.Color.Transparent;
			this.tabPage4.Controls.Add(this.Presentation_FadePanel);
			this.tabPage4.Location = new System.Drawing.Point(4, 24);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Size = new System.Drawing.Size(517, 451);
			this.tabPage4.TabIndex = 4;
			this.tabPage4.Text = "Presentation";
			// 
			// Presentation_FadePanel
			// 
			this.Presentation_FadePanel.BackColor = System.Drawing.Color.Gainsboro;
			this.Presentation_FadePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Presentation_FadePanel.Controls.Add(this.panel3);
			this.Presentation_FadePanel.Controls.Add(this.treeView1);
			this.Presentation_FadePanel.Dock = System.Windows.Forms.DockStyle.Right;
			this.Presentation_FadePanel.DockPadding.All = 2;
			this.Presentation_FadePanel.Location = new System.Drawing.Point(37, 0);
			this.Presentation_FadePanel.Name = "Presentation_FadePanel";
			this.Presentation_FadePanel.Size = new System.Drawing.Size(480, 451);
			this.Presentation_FadePanel.TabIndex = 3;
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.Presentation_Fade_ToPlaylist_Button);
			this.panel3.Controls.Add(this.Presentation_Fade_preview);
			this.panel3.Controls.Add(this.Presentation_Fade_ListView);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel3.Location = new System.Drawing.Point(192, 2);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(284, 445);
			this.panel3.TabIndex = 4;
			// 
			// Presentation_Fade_preview
			// 
			this.Presentation_Fade_preview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Presentation_Fade_preview.Location = new System.Drawing.Point(8, 8);
			this.Presentation_Fade_preview.Name = "Presentation_Fade_preview";
			this.Presentation_Fade_preview.Size = new System.Drawing.Size(184, 136);
			this.Presentation_Fade_preview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.Presentation_Fade_preview.TabIndex = 4;
			this.Presentation_Fade_preview.TabStop = false;
			// 
			// Presentation_Fade_ListView
			// 
			this.Presentation_Fade_ListView.Alignment = System.Windows.Forms.ListViewAlignment.Left;
			this.Presentation_Fade_ListView.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.Presentation_Fade_ListView.Location = new System.Drawing.Point(0, 149);
			this.Presentation_Fade_ListView.Name = "Presentation_Fade_ListView";
			this.Presentation_Fade_ListView.Size = new System.Drawing.Size(284, 296);
			this.Presentation_Fade_ListView.TabIndex = 3;
			this.Presentation_Fade_ListView.View = System.Windows.Forms.View.SmallIcon;
			this.Presentation_Fade_ListView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Presentation_Fade_ListView_MouseDown);
			this.Presentation_Fade_ListView.Click += new System.EventHandler(this.Presentation_Fade_ListView_Click);
			this.Presentation_Fade_ListView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Presentation_Fade_ListView_MouseUp);
			this.Presentation_Fade_ListView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Presentation_Fade_ListView_MouseMove);
			// 
			// treeView1
			// 
			this.treeView1.Dock = System.Windows.Forms.DockStyle.Left;
			this.treeView1.ImageList = this.imageList_Folders;
			this.treeView1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.treeView1.Location = new System.Drawing.Point(2, 2);
			this.treeView1.Name = "treeView1";
			this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
						new System.Windows.Forms.TreeNode("Node0", new System.Windows.Forms.TreeNode[] {
									new System.Windows.Forms.TreeNode("Node1", new System.Windows.Forms.TreeNode[] {
												new System.Windows.Forms.TreeNode("Node2")})})});
			this.treeView1.Size = new System.Drawing.Size(190, 445);
			this.treeView1.TabIndex = 2;
			this.treeView1.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeSelect);
			this.treeView1.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeExpand);
			// 
			// imageList_Folders
			// 
			this.imageList_Folders.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList_Folders.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList_Folders.ImageStream")));
			this.imageList_Folders.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// PreviewUpdateTimer
			// 
			this.PreviewUpdateTimer.Interval = 1000;
			this.PreviewUpdateTimer.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// TextTypedTimer
			// 
			this.TextTypedTimer.Enabled = true;
			this.TextTypedTimer.Interval = 500;
			this.TextTypedTimer.Tick += new System.EventHandler(this.TextTypedTimer_Tick);
			// 
			// sandDockManager1
			// 
			this.sandDockManager1.OwnerForm = this;
			// 
			// leftSandDock
			// 
			this.leftSandDock.Dock = System.Windows.Forms.DockStyle.Left;
			this.leftSandDock.Guid = new System.Guid("4844958a-6b63-4783-8909-9805d07b9fab");
			this.leftSandDock.LayoutSystem = new TD.SandDock.SplitLayoutSystem(250, 400);
			this.leftSandDock.Location = new System.Drawing.Point(57, 48);
			this.leftSandDock.Manager = this.sandDockManager1;
			this.leftSandDock.Name = "leftSandDock";
			this.leftSandDock.Size = new System.Drawing.Size(0, 501);
			this.leftSandDock.TabIndex = 23;
			// 
			// rightSandDock
			// 
			this.rightSandDock.Controls.Add(this.RightDocks_TopPanel_Songs);
			this.rightSandDock.Controls.Add(this.RightDocks_TopPanel_PlayList);
			this.rightSandDock.Controls.Add(this.RightDocks_TopPanel_Search);
			this.rightSandDock.Controls.Add(this.RightDocks_BottomPanel_Backgrounds);
			this.rightSandDock.Controls.Add(this.RightDocks_BottomPanel_Media);
			this.rightSandDock.Dock = System.Windows.Forms.DockStyle.Right;
			this.rightSandDock.Guid = new System.Guid("a6039876-f9a8-471e-b56f-5b1bf7264f06");
			this.rightSandDock.LayoutSystem = new TD.SandDock.SplitLayoutSystem(250, 400, System.Windows.Forms.Orientation.Horizontal, new TD.SandDock.LayoutSystemBase[] {
						new TD.SandDock.ControlLayoutSystem(196, 211, new TD.SandDock.DockControl[] {
									this.RightDocks_TopPanel_Songs,
									this.RightDocks_TopPanel_PlayList,
									this.RightDocks_TopPanel_Search}, this.RightDocks_TopPanel_Songs),
						new TD.SandDock.ControlLayoutSystem(196, 285, new TD.SandDock.DockControl[] {
									this.RightDocks_BottomPanel_Backgrounds,
									this.RightDocks_BottomPanel_Media}, this.RightDocks_BottomPanel_Media)});
			this.rightSandDock.Location = new System.Drawing.Point(582, 48);
			this.rightSandDock.Manager = this.sandDockManager1;
			this.rightSandDock.Name = "rightSandDock";
			this.rightSandDock.Size = new System.Drawing.Size(200, 501);
			this.rightSandDock.TabIndex = 24;
			// 
			// RightDocks_TopPanel_Songs
			// 
			this.RightDocks_TopPanel_Songs.Closable = false;
			this.RightDocks_TopPanel_Songs.Controls.Add(this.RightDocks_SongList);
			this.RightDocks_TopPanel_Songs.Controls.Add(this.RightDocks_Songlist_SearchPanel);
			this.RightDocks_TopPanel_Songs.Controls.Add(this.RightDocks_SongList_ButtonPanel);
			this.RightDocks_TopPanel_Songs.Guid = new System.Guid("6044bb67-05ab-4617-bbfa-99e49388b41f");
			this.RightDocks_TopPanel_Songs.Location = new System.Drawing.Point(4, 18);
			this.RightDocks_TopPanel_Songs.Name = "RightDocks_TopPanel_Songs";
			this.RightDocks_TopPanel_Songs.Size = new System.Drawing.Size(196, 170);
			this.RightDocks_TopPanel_Songs.TabIndex = 0;
			this.RightDocks_TopPanel_Songs.Text = "Songs";
			// 
			// RightDocks_TopPanel_PlayList
			// 
			this.RightDocks_TopPanel_PlayList.Closable = false;
			this.RightDocks_TopPanel_PlayList.Controls.Add(this.RightDocks_PlayList);
			this.RightDocks_TopPanel_PlayList.Controls.Add(this.RightDocks_TopPanel_PlayList_Button_Panel);
			this.RightDocks_TopPanel_PlayList.Guid = new System.Guid("92186926-e7f9-4850-98b8-190a99f81ea6");
			this.RightDocks_TopPanel_PlayList.Location = new System.Drawing.Point(4, 18);
			this.RightDocks_TopPanel_PlayList.Name = "RightDocks_TopPanel_PlayList";
			this.RightDocks_TopPanel_PlayList.Size = new System.Drawing.Size(196, 170);
			this.RightDocks_TopPanel_PlayList.TabIndex = 2;
			this.RightDocks_TopPanel_PlayList.Text = "Playlist";
			// 
			// RightDocks_TopPanel_Search
			// 
			this.RightDocks_TopPanel_Search.Closable = false;
			this.RightDocks_TopPanel_Search.Controls.Add(this.RightDocks_Search_ListBox);
			this.RightDocks_TopPanel_Search.Controls.Add(this.RightDocks_SearchBar_TopPanel);
			this.RightDocks_TopPanel_Search.Controls.Add(this.RightDocks_TopPanel_Search_ButtonPanel);
			this.RightDocks_TopPanel_Search.Guid = new System.Guid("fb38e9af-4631-4988-bf4e-97e1dabc53ef");
			this.RightDocks_TopPanel_Search.Location = new System.Drawing.Point(4, 18);
			this.RightDocks_TopPanel_Search.Name = "RightDocks_TopPanel_Search";
			this.RightDocks_TopPanel_Search.Size = new System.Drawing.Size(196, 170);
			this.RightDocks_TopPanel_Search.TabIndex = 3;
			this.RightDocks_TopPanel_Search.Text = "Search";
			// 
			// RightDocks_BottomPanel_Backgrounds
			// 
			this.RightDocks_BottomPanel_Backgrounds.Closable = false;
			this.RightDocks_BottomPanel_Backgrounds.Controls.Add(this.RightDocks_ImageListBox);
			this.RightDocks_BottomPanel_Backgrounds.Controls.Add(this.RightDocks_BottomPanel2_TopPanel);
			this.RightDocks_BottomPanel_Backgrounds.Guid = new System.Guid("b561dd7f-3e79-4e07-912c-18ac9600db75");
			this.RightDocks_BottomPanel_Backgrounds.Location = new System.Drawing.Point(4, 233);
			this.RightDocks_BottomPanel_Backgrounds.Name = "RightDocks_BottomPanel_Backgrounds";
			this.RightDocks_BottomPanel_Backgrounds.Size = new System.Drawing.Size(196, 245);
			this.RightDocks_BottomPanel_Backgrounds.TabIndex = 1;
			this.RightDocks_BottomPanel_Backgrounds.Text = "Backgrounds";
			// 
			// RightDocks_BottomPanel2_TopPanel
			// 
			this.RightDocks_BottomPanel2_TopPanel.Controls.Add(this.RightDocks_FolderDropdown);
			this.RightDocks_BottomPanel2_TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.RightDocks_BottomPanel2_TopPanel.Location = new System.Drawing.Point(0, 0);
			this.RightDocks_BottomPanel2_TopPanel.Name = "RightDocks_BottomPanel2_TopPanel";
			this.RightDocks_BottomPanel2_TopPanel.Size = new System.Drawing.Size(196, 24);
			this.RightDocks_BottomPanel2_TopPanel.TabIndex = 22;
			// 
			// RightDocks_BottomPanel_Media
			// 
			this.RightDocks_BottomPanel_Media.Controls.Add(this.RightDocks_BottomPanel_Media_FadePanelButton);
			this.RightDocks_BottomPanel_Media.Controls.Add(this.RightDocks_BottomPanel_Media_FolderSelectButton);
			this.RightDocks_BottomPanel_Media.Controls.Add(this.RightDocks_BottomPanel_MediaListView);
			this.RightDocks_BottomPanel_Media.Guid = new System.Guid("c9d617ca-0165-45b7-8e07-329a81273abc");
			this.RightDocks_BottomPanel_Media.Location = new System.Drawing.Point(4, 233);
			this.RightDocks_BottomPanel_Media.Name = "RightDocks_BottomPanel_Media";
			this.RightDocks_BottomPanel_Media.Size = new System.Drawing.Size(196, 245);
			this.RightDocks_BottomPanel_Media.TabIndex = 4;
			this.RightDocks_BottomPanel_Media.Text = "Media";
			// 
			// RightDocks_BottomPanel_Media_FadePanelButton
			// 
			this.RightDocks_BottomPanel_Media_FadePanelButton.Location = new System.Drawing.Point(8, 8);
			this.RightDocks_BottomPanel_Media_FadePanelButton.Name = "RightDocks_BottomPanel_Media_FadePanelButton";
			this.RightDocks_BottomPanel_Media_FadePanelButton.TabIndex = 2;
			this.RightDocks_BottomPanel_Media_FadePanelButton.Text = "Fade";
			this.RightDocks_BottomPanel_Media_FadePanelButton.Click += new System.EventHandler(this.RightDocks_BottomPanel_Media_FadePanelButton_Click);
			// 
			// RightDocks_BottomPanel_Media_FolderSelectButton
			// 
			this.RightDocks_BottomPanel_Media_FolderSelectButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.RightDocks_BottomPanel_Media_FolderSelectButton.Location = new System.Drawing.Point(112, 8);
			this.RightDocks_BottomPanel_Media_FolderSelectButton.Name = "RightDocks_BottomPanel_Media_FolderSelectButton";
			this.RightDocks_BottomPanel_Media_FolderSelectButton.Size = new System.Drawing.Size(80, 18);
			this.RightDocks_BottomPanel_Media_FolderSelectButton.TabIndex = 1;
			this.RightDocks_BottomPanel_Media_FolderSelectButton.Text = "Select Folder";
			this.RightDocks_BottomPanel_Media_FolderSelectButton.Click += new System.EventHandler(this.RightDocks_BottomPanel_Media_FolderSelectButton_Click);
			// 
			// RightDocks_BottomPanel_MediaListView
			// 
			this.RightDocks_BottomPanel_MediaListView.AllowDrop = true;
			this.RightDocks_BottomPanel_MediaListView.AllowReorder = true;
			this.RightDocks_BottomPanel_MediaListView.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.RightDocks_BottomPanel_MediaListView.LineColor = System.Drawing.Color.Red;
			this.RightDocks_BottomPanel_MediaListView.Location = new System.Drawing.Point(0, 45);
			this.RightDocks_BottomPanel_MediaListView.MultiSelect = false;
			this.RightDocks_BottomPanel_MediaListView.Name = "RightDocks_BottomPanel_MediaListView";
			this.RightDocks_BottomPanel_MediaListView.Size = new System.Drawing.Size(196, 200);
			this.RightDocks_BottomPanel_MediaListView.TabIndex = 0;
			this.RightDocks_BottomPanel_MediaListView.DragOver += new System.Windows.Forms.DragEventHandler(this.RightDocks_BottomPanel_MediaListView_DragOver);
			this.RightDocks_BottomPanel_MediaListView.QueryContinueDrag += new System.Windows.Forms.QueryContinueDragEventHandler(this.RightDocks_BottomPanel_MediaListView_QueryContinueDrag);
			// 
			// bottomSandDock
			// 
			this.bottomSandDock.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.bottomSandDock.Guid = new System.Guid("11832edf-4b5f-4911-9e7c-da764193d89f");
			this.bottomSandDock.LayoutSystem = new TD.SandDock.SplitLayoutSystem(250, 400);
			this.bottomSandDock.Location = new System.Drawing.Point(57, 549);
			this.bottomSandDock.Manager = this.sandDockManager1;
			this.bottomSandDock.Name = "bottomSandDock";
			this.bottomSandDock.Size = new System.Drawing.Size(525, 0);
			this.bottomSandDock.TabIndex = 25;
			// 
			// topSandDock
			// 
			this.topSandDock.Dock = System.Windows.Forms.DockStyle.Top;
			this.topSandDock.Guid = new System.Guid("f3084065-73e3-4825-a957-0918e3006a24");
			this.topSandDock.LayoutSystem = new TD.SandDock.SplitLayoutSystem(250, 400);
			this.topSandDock.Location = new System.Drawing.Point(57, 48);
			this.topSandDock.Manager = this.sandDockManager1;
			this.topSandDock.Name = "topSandDock";
			this.topSandDock.Size = new System.Drawing.Size(525, 0);
			this.topSandDock.TabIndex = 26;
			// 
			// Presentation_Fade_ToPlaylist_Button
			// 
			this.Presentation_Fade_ToPlaylist_Button.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.Presentation_Fade_ToPlaylist_Button.Location = new System.Drawing.Point(200, 120);
			this.Presentation_Fade_ToPlaylist_Button.Name = "Presentation_Fade_ToPlaylist_Button";
			this.Presentation_Fade_ToPlaylist_Button.TabIndex = 5;
			this.Presentation_Fade_ToPlaylist_Button.Text = "Add ->";
			// 
			// Presentation_Fade_ImageList
			// 
			this.Presentation_Fade_ImageList.ImageSize = new System.Drawing.Size(16, 16);
			this.Presentation_Fade_ImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("Presentation_Fade_ImageList.ImageStream")));
			this.Presentation_Fade_ImageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(782, 549);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.statusBar);
			this.Controls.Add(this.leftSandDock);
			this.Controls.Add(this.bottomSandDock);
			this.Controls.Add(this.topSandDock);
			this.Controls.Add(this.rightSandDock);
			this.Controls.Add(this.ToolBars_leftSandBarDock);
			this.Controls.Add(this.ToolBars_bottomSandBarDock);
			this.Controls.Add(this.ToolBars_topSandBarDock);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Location = new System.Drawing.Point(50, 0);
			this.Name = "MainForm";
			this.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "DreamBeam";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.MainForm_Closing);
			this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.RightDocks_Songlist_SearchPanel.ResumeLayout(false);
			this.RightDocks_SongList_ButtonPanel.ResumeLayout(false);
			this.RightDocks_TopPanel_PlayList_Button_Panel.ResumeLayout(false);
			this.RightDocks_TopPanel_Search_ButtonPanel.ResumeLayout(false);
			this.RightDocks_SearchBar_TopPanel.ResumeLayout(false);
			this.ToolBars_leftSandBarDock.ResumeLayout(false);
			this.ToolBars_topSandBarDock.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.StatusPanel)).EndInit();
			this.tabControl1.ResumeLayout(false);
			this.tabPage0.ResumeLayout(false);
			this.SongShow_Right_Panel.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.SongShow_BG_Panel.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.Sermon_LeftPanel.ResumeLayout(false);
			this.Sermon_LeftBottom_Panel.ResumeLayout(false);
			this.Sermon_LeftDoc_Panel.ResumeLayout(false);
			this.Sermon_LeftToolBar_Panel.ResumeLayout(false);
			this.Sermon_TabControl.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.SongEdit_RightPanel.ResumeLayout(false);
			this.SongEdit_TextAlign.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.SongEdit_BG_Panel.ResumeLayout(false);
			this.SongEdit_BigInputFieldPanel.ResumeLayout(false);
			this.SongEdit_InputFieldPanelMid.ResumeLayout(false);
			this.SongEdit_InputFieldBelowMenuPanelMid.ResumeLayout(false);
			this.SongEdit_InputFieldBelowMenuPane2lMid.ResumeLayout(false);
			this.SongEdit_MidPos_Panel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.SongEdit_MidPosX_UpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.SongEdit_MidPosY_UpDown)).EndInit();
			this.SongEdit_InputFieldMenuPanelMid.ResumeLayout(false);
			this.SongEdit_InputFieldPanelButtom.ResumeLayout(false);
			this.SongEdit_InputFieldBelowMenuPanelButtom.ResumeLayout(false);
			this.SongEdit_BottomPos_Panel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.SongEdit_BottomPosX_UpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.SongEdit_BottomPosY_UpDown)).EndInit();
			this.SongEdit_InputFieldMenuPanelButtom.ResumeLayout(false);
			this.SongEdit_InputFieldPanelTop.ResumeLayout(false);
			this.SongEdit_InputPanelTopBelowMenu.ResumeLayout(false);
			this.SongEdit_TopPos_Panel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.SongEdit_TopPosX_UpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.SongEdit_TopPosY_UpDown)).EndInit();
			this.SongEdit_InputPanelTopMenu.ResumeLayout(false);
			this.tabPage4.ResumeLayout(false);
			this.Presentation_FadePanel.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.rightSandDock.ResumeLayout(false);
			this.RightDocks_TopPanel_Songs.ResumeLayout(false);
			this.RightDocks_TopPanel_PlayList.ResumeLayout(false);
			this.RightDocks_TopPanel_Search.ResumeLayout(false);
			this.RightDocks_BottomPanel_Backgrounds.ResumeLayout(false);
			this.RightDocks_BottomPanel2_TopPanel.ResumeLayout(false);
			this.RightDocks_BottomPanel_Media.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		#endregion


		#region Methods and Events

			#region Inits

				private void InitializeDiatheke(){
					if (SwordProject_Found){
						System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MainForm));
						this.Diatheke = new AxACTIVEDIATHEKELib.AxActiveDiatheke();
						((System.ComponentModel.ISupportInitialize)(this.Diatheke)).BeginInit();
						this.Sermon_LeftBottom_Panel.Controls.Add(this.Diatheke);
						//
						// Diatheke
						//
						this.Diatheke.ContainingControl = this;
						this.Diatheke.Enabled = true;
						this.Diatheke.Location = new System.Drawing.Point(144, 8);
						this.Diatheke.Name = "Diatheke";
						this.Diatheke.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("Diatheke.OcxState")));
						this.Diatheke.Size = new System.Drawing.Size(100, 40);
						this.Diatheke.TabIndex = 3;
						((System.ComponentModel.ISupportInitialize)(this.Diatheke)).EndInit();
					}else{
						Sermon_BibleKey.Enabled = false;
						Sermon_Books.Enabled = false;
						Sermon_Books_Label.Enabled = false;
						Sermon_Testament_ListBox.Enabled = false;
						Sermon_BookList.Enabled = false;
					}
			}
			#endregion

			#region Form Methods

				#region Save and Load Settings



					/// <summary>Saves Program Properties like BeamBox Position and Alphablending to the config.xml. </summary>
					public void SaveSettings(){


						string PlayListString = "";

						for (int i = 1; i <= RightDocks_PlayList.Items.Count;i++){
						   PlayListString = PlayListString + RightDocks_PlayList.Items[i-1].ToString() + ";";
						}

							this.Config.BeamBoxPosX = (int)ShowBeam.WindowPosX.Value;
							this.Config.BeamBoxPosY = (int)ShowBeam.WindowPosY.Value;
							this.Config.BeamBoxSizeX = (int)ShowBeam.WindowSizeX.Value;
							this.Config.BeamBoxSizeY = (int)ShowBeam.WindowSizeY.Value;

							this.Config.Alphablending = ShowBeam.transit;
							this.Config.OutlineSize = ShowBeam.OutlineSize;
							this.Config.BibleLang =	this.Sermon_BibleLang;
							this.Config.ShowBibleTranslation = this.Sermon_ShowBibleTranslation;
							this.Config.PlayListString = PlayListString;

							this.Config.Save("config.xml");
					}

					/// <summary>Loads Program Properties like BeamBox Position and <Alphablending. </summary>
					public void LoadSettings(){
								
								// Get the BeamBox Position
								ShowBeam.WindowPosX.Value = this.Config.BeamBoxPosX;
								ShowBeam.WindowPosY.Value = this.Config.BeamBoxPosY;
								ShowBeam.WindowSizeX.Value = this.Config.BeamBoxSizeX;
								ShowBeam.WindowSizeY.Value = this.Config.BeamBoxSizeY;
								ShowBeam.HideMouse = this.Config.HideMouse;
								// Alphablending enabled?
								ShowBeam.transit = this.Config.Alphablending;
								// Outline Size
								ShowBeam.OutlineSize = this.Config.OutlineSize;
								// BG Color
								ShowBeam.BackgroundColor = this.Config.BackgroundColor;

								// BibleStuff
								this.Sermon_BibleLang  = this.Config.BibleLang;
								this.Sermon_ShowBibleTranslation  = this.Config.ShowBibleTranslation;

								//split the book list by line into an array
								if (Config.PlayListString.Length > 0) {
									String[] tmpSongs = this.Config.PlayListString.Substring(0,this.Config.PlayListString.Length-1).Split(';');
									foreach (string tmpSong in tmpSongs){
										//Sermon_BookList.Items.Add(Book);
										RightDocks_PlayList.Items.Add(tmpSong);
									}
								}
					}

				#endregion

				#region Get and Set Song Information

					///<summary>Reads all Songs in Directory, validates if it is a Song and put's them into the RightDocks_SongList Box </summary>
					public void ListSongs(){
						this.RightDocks_SongList.Items.Clear();
						string strSongDir = Directory.GetCurrentDirectory()+"\\songs";
							if(!System.IO.Directory.Exists(strSongDir)){
								System.IO.Directory.CreateDirectory(strSongDir);
							}
						string[] dirs2 = Directory.GetFiles(@strSongDir, "*.xml");
						foreach (string dir2 in dirs2)
						{
							if (Song.isSong(Path.GetFileName(dir2))){
								string temp = Path.GetFileName(dir2);
								this.RightDocks_SongList.Items.Add(temp.Substring(0,temp.Length-4));
							}
						}

					}

				/// <summary>Get Song Contents from EditForms to Song</summary>
				private void setSong()
				{
					ShowBeam.Song.Text[0] = SongEdit_Header_TextBox.Text;
					ShowBeam.Song.Text[1] = SongEdit_Mid_TextBox.Text;
					ShowBeam.Song.Text[2] = Footer_TextBox.Text;
				}

				// <summary>Puts the SongInformation into Windows Forms </summary>
				private void getSong(){
					LogFile.Log(" Getting Song! ");
					this.AllowPreview = false;
					// Edit Songs:
					this.SongEdit_Header_TextBox.Text = ShowBeam.Song.Text[0];
					this.SongEdit_Mid_TextBox.Text = ShowBeam.Song.Text[1];
					this.Footer_TextBox.Text = ShowBeam.Song.Text[2];
					this.SongEdit_BG_Label.Text = ShowBeam.Song.bg_image;
					this.SongEdit_TopPosX_UpDown.Value = ShowBeam.Song.posX[0];
					this.SongEdit_TopPosY_UpDown.Value = ShowBeam.Song.posY[0];
					this.SongEdit_MidPosX_UpDown.Value = ShowBeam.Song.posX[1];
					this.SongEdit_MidPosY_UpDown.Value = ShowBeam.Song.posY[1];
					this.SongEdit_BottomPosX_UpDown.Value = ShowBeam.Song.posX[2];
					this.SongEdit_BottomPosY_UpDown.Value = ShowBeam.Song.posY[2];
					SongEdit_Top_AutoPos_CheckBox.Checked = ShowBeam.Song.AutoPos[0];
					SongEdit_Mid_AutoPos_CheckBox.Checked = ShowBeam.Song.AutoPos[1];
					SongEdit_Bottom_AutoPos_CheckBox.Checked = ShowBeam.Song.AutoPos[2];
					string temp =SongEdit_Mid_TextBox.Text;

					this.SetEditButtonsStatus();

					// Show Songs:
					this.SongShow_StropheList_ListEx.Items.Clear();
					int i = 0;
					while (temp.IndexOf(ShowBeam.strSeperator)>= 0) {

						this.SongShow_StropheList_ListEx.Add(temp.Substring(0,temp.IndexOf(ShowBeam.strSeperator)) , i);
						temp=temp.Substring(temp.IndexOf(ShowBeam.strSeperator)+ShowBeam.strSeperator.Length);
						i++;
					}
						this.SongShow_StropheList_ListEx.Add(temp, i);
						ShowBeam.Song.strophe_count = i;
						SongShow_StropheList_ListEx.SelectedIndex = 0;
					this.AllowPreview = true;
					LogFile.LogNew ("Drawing Preview Panel from: GetSong");
					Draw_Song_Preview_Image();

					LogFile.Log(" Done Getting Song! ");
				}

				#endregion


			///<summarize> Initialize DreamBeam while MainForm is loading </summarize>
			private void MainForm_Load(object sender, System.EventArgs e)
			{

				this.Hide();
					Splash.SetStatus("Loading Configuration");
				this.LoadSettings();
				this.ShowTab(0);
					Splash.SetStatus("Reading Background Images");
				this.ListDirectories(@"Backgrounds\");
				this.ListImages(@"Backgrounds\");
					Splash.SetStatus("Reading Songs");
				this.ListSongs();
					Splash.SetStatus("Initializing Bible");
				this.BibleInit();
				ShowBeam.Song.version = this.version;
				Splash.CloseForm();
				System.Threading.Thread.Sleep(900);
				
				this.TopMost = true;
				System.Threading.Thread.Sleep(900);
				this.TopMost = false;

			}

			///<summarize> start SaveSettings while MainForm is closing </summarize>
			private void MainForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
			{
				this.SaveSettings();
//				this.SaveSermon(Directory.GetCurrentDirectory()+"\\sermon.xml");
				LogFile.BigHeader ("Program Ended");
				LogFile.Log("");
				LogFile.Log("");
			}


			//<summary> Shows the assigned Tab </summary>
			private void ShowTab(int Tab){
				this.selectedTab = Tab;
				// Hide all Tabs
				for (int i = this.tabControl1.TabCount-1;i>=0;i--){
					this.tabControl1.TabPages.RemoveAt(i);
				}
				this.ToolBars_MainToolbar_SaveSong.Visible = false;

				// Hide Right Panels
				RightDocks_TopPanel_Songs.Close();
				RightDocks_TopPanel_PlayList.Close();
				RightDocks_TopPanel_Search.Close();
				RightDocks_BottomPanel_Backgrounds.Close();
				RightDocks_BottomPanel_Media.Close();

				// Show Selected Tab
				switch (Tab){
					case 0:
						RightDocks_TopPanel_Songs.Open();
						RightDocks_TopPanel_PlayList.Open();
						RightDocks_TopPanel_Search.Open();
						RightDocks_BottomPanel_Backgrounds.Open();

						this.rightSandDock.LayoutSystem = new TD.SandDock.SplitLayoutSystem(250, 400, System.Windows.Forms.Orientation.Horizontal, new TD.SandDock.LayoutSystemBase[] {
							new TD.SandDock.ControlLayoutSystem(196, 211, new TD.SandDock.DockControl[] {
										this.RightDocks_TopPanel_Songs,
										this.RightDocks_TopPanel_PlayList,
										this.RightDocks_TopPanel_Search}, this.RightDocks_TopPanel_Songs),
							new TD.SandDock.ControlLayoutSystem(196, 285, new TD.SandDock.DockControl[] {
										this.RightDocks_BottomPanel_Backgrounds}, this.RightDocks_BottomPanel_Backgrounds)});



						this.tabControl1.Controls.Add(this.tabPage0);
						this.SongShow_Right_Panel.Controls.Add(this.SongShow_Preview_Panel);
					break;
					case 1:
						RightDocks_TopPanel_Songs.Open();
						RightDocks_TopPanel_PlayList.Open();
						RightDocks_TopPanel_Search.Open();
						RightDocks_BottomPanel_Backgrounds.Open();

						this.rightSandDock.LayoutSystem = new TD.SandDock.SplitLayoutSystem(250, 400, System.Windows.Forms.Orientation.Horizontal, new TD.SandDock.LayoutSystemBase[] {
							new TD.SandDock.ControlLayoutSystem(196, 211, new TD.SandDock.DockControl[] {
										this.RightDocks_TopPanel_Songs,
										this.RightDocks_TopPanel_PlayList,
										this.RightDocks_TopPanel_Search}, this.RightDocks_TopPanel_Songs),
							new TD.SandDock.ControlLayoutSystem(196, 285, new TD.SandDock.DockControl[] {
										this.RightDocks_BottomPanel_Backgrounds}, this.RightDocks_BottomPanel_Backgrounds)});


						this.tabControl1.Controls.Add(this.tabPage1);
						this.SongEdit_RightPanel.Controls.Add(this.SongShow_Preview_Panel);
						this.ToolBars_MainToolbar_SaveSong.Visible = true;
					break;
					case 2:
						RightDocks_BottomPanel_Backgrounds.Open();

						this.tabControl1.Controls.Add(this.tabPage2);
					break;
					case 3:
						RightDocks_TopPanel_Songs.Close();
						RightDocks_TopPanel_PlayList.Close();
						RightDocks_TopPanel_Search.Close();
						RightDocks_BottomPanel_Backgrounds.Close();

						RightDocks_BottomPanel_Media.Open();
						this.tabControl1.Controls.Add(this.tabPage4);
					break;
				}
			}

			#endregion

			#region Toolbars and others
				#region Menu Bar
					#region File

						///<summary> new Song from Menu</summary>
						private void ToolBars_MenuBar_Song_New_Activate(object sender, System.EventArgs e)
						{
							// get song Name
							InputBoxResult result = InputBox.Show("Enter a name for the Song:", "New Song..","", null);
							if (result.OK) {
								bool boolnew = false;
								if (result.Text.Length > 0){
									if (!System.IO.File.Exists("Songs\\"+result.Text+".xml"))					{
										boolnew = true;
									}else{
										MessageBox.Show("Song already exists.");
									}
								}else {
									MessageBox.Show("No name entered.");
								}
							// create New Song
							if (boolnew){
							 ShowBeam.Song.Init(result.Text);
							 this.getSong();
							}
						  }
						}

						///<summary> Save Song AS from Menu</summary>
						private void ToolBars_MenuBar_Song_SaveAs_Activate(object sender, System.EventArgs e)
{
							InputBoxResult result = InputBox.Show("Enter Songname:", "Save Song as..","", null);
							if (result.OK) {
								bool save = false;
								if (result.Text.Length > 0){
									if (!System.IO.File.Exists("Songs\\"+result.Text+".xml"))					{
										save = true;
									}else{
										MessageBox.Show("Song already exists.");
									}
								}else {
									MessageBox.Show("No Name declared - Song hasn't been saved.");
								}

								if (save){
									this.setSong();
									ShowBeam.Song.SongName = result.Text;
									ShowBeam.Song.Save();
									this.ListSongs();
									this.StatusPanel.Text = "Song has been saved as '"+result.Text+"'";
								}

							}
						}

						///<summary>Rename Song from File Menu</summary>
						private void ToolBars_MenuBar_Song_Rename_Activate(object sender, System.EventArgs e)
						{
							InputBoxResult result = InputBox.Show("Enter Songname:", "Rename Song '"+ShowBeam.Song.SongName+"' to..","", null);
							if (result.OK) {
//								bool save = false;
								if (result.Text.Length > 0){
									if (!System.IO.File.Exists("Songs\\"+result.Text+".xml")) {
										System.IO.File.Move("Songs\\"+ShowBeam.Song.SongName+".xml", "Songs\\"+result.Text+".xml");
										ShowBeam.Song.SongName = result.Text;
										this.ListSongs();
										this.StatusPanel.Text = "Song has been renamed to '"+result.Text+"'";

									}else{
										MessageBox.Show("Song is not saved, yet.");
									}
								}else {
									MessageBox.Show("No Name declared - Song hasn't been saved.");
								}
							 }
						}





						//<summary> End Program from Menu</summary>
						private void ToolBars_MenuBar_Song_Exit_Activate(object sender, System.EventArgs e)
						{
							this.Close();
						}
					#endregion

					#region Edit

						private void ToolBars_MenuBar_Edit_Options_Activate(object sender, System.EventArgs e) {
							SaveSettings();
							//this.Config.Save("config.xml");
							Options.ShowDialog();
							this.Config.Load();
							//this.Config.Save("config.xml");
							this.RightDocks_PlayList.Items.Clear();
							LoadSettings();
						}
					#endregion

					#region Help

						#region Intro & Main Components
						///<summary> Displays Help Balloons - with Intro and Main Components </summary>
						private void HelpIntro_Activate(object sender, System.EventArgs e)
						{
							int intHelpNum = 1;
							int intButtonClicked;
							Point pos;
							this.Size = new Size(790,570);

							do{
								switch(intHelpNum){
									case 1:
										 // Start With Introduction
										 HelpBalloon= new MyBalloonWindow();
										 HelpBalloon.AnchorQuadrant = AnchorQuadrant.Auto;
										 HelpBalloon.button3.Hide();
										 pos = new Point(this.Location.X+this.ToolBars_MenuBar_Song.ButtonBounds.X + this.ToolBars_MenuBar_Edit.ButtonBounds.X +this.ToolBars_MenuBar_View.ButtonBounds.X + 20  ,this.Location.Y+30);
										 intButtonClicked = HelpBalloon.ShowHelp(pos,"DreamBeam Help","Welcome to the Dreambeam Help Introduction. This bubble will guide you through all important components of Dreambeam. Use the Buttons below to Navigate through the Tutorials. Click CANCEL to stop, BACK to return to the previous bubble and NEXT to continue with the tour. ");
										 this.Focus();
										 HelpBalloon.Dispose();
										 switch (intButtonClicked){
											case 0:
												intHelpNum = 0;
											break;
											case 1:
												intHelpNum = 0;
											break;
											case 2:
												intHelpNum++;
											break;
										}
									break;
									case 2:

										// Main Toolbar
										 HelpBalloon= new MyBalloonWindow();
										 pos = new Point(this.Location.X+20,this.Location.Y+ToolBars_topSandBarDock.Bounds.Height+10);
										 intButtonClicked = HelpBalloon.ShowHelp(pos,"Main ToolBar","This is the Main Toolbar. Here you can manage the Projector Window (Show, Hide and Change Position), read the Projector Window Help for further information. Also you can save your Songs in the Song-Edit Mode. Other Actions are planned.");
										 this.Focus();
										 HelpBalloon.Dispose();
										 switch (intButtonClicked){
											case 0:
												intHelpNum = 0;
											break;
											case 1:
												intHelpNum--;
											break;
											case 2:
												intHelpNum++;
											break;
										 }
									break;
									case 3:

										// Component ToolBar
										 HelpBalloon= new MyBalloonWindow();
										 HelpBalloon.AnchorQuadrant = AnchorQuadrant.Left;
										 HelpBalloon.AnchorOffset = -1;
										 pos = new Point(this.Location.X+40,this.Location.Y+ToolBars_topSandBarDock.Bounds.Height + Convert.ToInt32(Math.Round(ToolBars_ComponentBar.Size.Height/2d,0)));
										 intButtonClicked = HelpBalloon.ShowHelp(pos,"Component ToolBar","Here you are able to switch between the DreamBeam components. Right in the Moment they are: SHOW SONGS and EDIT SONGS. To learn more about them, select them in the Help Menu.");
										 this.Focus();
										 HelpBalloon.Dispose();
										 switch (intButtonClicked){
											case 0:
												intHelpNum = 0;
											break;
											case 1:
												intHelpNum--;
											break;
											case 2:
												intHelpNum++;
											break;
										 }
									break;
									case 4:
										// Song List
										 HelpBalloon= new MyBalloonWindow();
										 HelpBalloon.AnchorQuadrant = AnchorQuadrant.Right;

										 pos = new Point(this.Location.X+320,this.Location.Y+110);
										 intButtonClicked = HelpBalloon.ShowHelp(pos,"Songlist","The Song List displays all available Songs. Select a Song and click Load, the get it into the ‘Show Songs’ and ‘Edit Songs’ Window or just simply double-click it. If you have a long list of songs, use the Input Field on top to find it. By clicking the ‘Delete’ Button the selected Song will be deleted permanently. If you click the Play List Button, the selected Song comes into the Play List..");
										 HelpBalloon.Dispose();
										 switch (intButtonClicked){
											case 0:
												intHelpNum = 0;
											break;
											case 1:
												intHelpNum--;
											break;
											case 2:
												intHelpNum++;
											break;
										 }
									break;
									case 5:
										// Play List
											//this.RightDocks_TopPanel.SelectedTab = this.RightDocks_TopPanel_PlayList;
										 HelpBalloon= new MyBalloonWindow();
										 HelpBalloon.AnchorQuadrant = AnchorQuadrant.Right;
										 pos = new Point(this.Location.X+320,this.Location.Y+110);
										 intButtonClicked = HelpBalloon.ShowHelp(pos,"PlayList","The ‘PlayList’ is used to put  in all Songs needed for the Worship Session. No need for hectically Song searches. Use the ‘UP’and ‘DOWN’ Buttons to order your PlayList. The ‘REMOVE’ Button removes the Song only from the PlayList.");
										 HelpBalloon.Dispose();
										 //this.RightDocks_TopPanel.SelectedTab = this.RightDocks_TopPanel_Songs;
										 switch (intButtonClicked){
											case 0:
												intHelpNum = 0;
											break;
											case 1:
												intHelpNum--;
											break;
											case 2:
												intHelpNum++;
											break;
										 }
									break;
									case 6:
										// Play List
											//this.RightDocks_TopPanel.SelectedTab = this.RightDocks_TopPanel_PlayList;
										 HelpBalloon= new MyBalloonWindow();
										 HelpBalloon.AnchorQuadrant = AnchorQuadrant.Right;
										 HelpBalloon.AnchorOffset = -2;
										 pos = new Point(this.Location.X+320,this.Location.Y+199);
										 intButtonClicked = HelpBalloon.ShowHelp(pos,"Image Folders - List","Lists all Folders in the 'Backgrounds'-Directory. Select one, to see all Images inside.");
										 HelpBalloon.Dispose();
										 //this.RightDocks_TopPanel.SelectedTab = this.RightDocks_TopPanel_Songs;
										 switch (intButtonClicked){
											case 0:
												intHelpNum = 0;
											break;
											case 1:
												intHelpNum--;
											break;
											case 2:
												intHelpNum++;
											break;
										 }
									break;
									case 7:
										// Play List
										 HelpBalloon= new MyBalloonWindow();
										 HelpBalloon.button1.Text = "Finish";
										 HelpBalloon.AnchorQuadrant = AnchorQuadrant.Right;
										 HelpBalloon.AnchorOffset = -2;
										 pos = new Point(this.Location.X+320,this.Location.Y+250);
										 intButtonClicked = HelpBalloon.ShowHelp(pos,"Image List","Use this list to select Background Images for your Songs. Right Mouse Click -> Manage opens a the Image Folder, where you can add, delete or rename your Images. Right Mouse Click -> Reload refreshes the Image List.");
										 this.Focus();
										 //intButtonClicked = 0;
										 HelpBalloon.Dispose();
										 switch (intButtonClicked){
											case 0:
												intHelpNum = 0;
											break;
											case 1:
												intHelpNum--;
											break;
											case 2:
												intHelpNum = 0;
											break;
										 }
									break;

								}
							}while (intHelpNum != 0);

							//			buttonItem1.ButtonBounds);
							//			this.ToolBars_ComponentBar);

						}
						#endregion

						#region Beambox
						///<summary> Displays Help Balloons - BeamBox Help </summary>
						private void HelpBeamBox_Activate(object sender, System.EventArgs e) {
							int intHelpNum = 1;
							int intButtonClicked;
							Point pos;
							this.Size = new Size(790,570);

							do{
								switch(intHelpNum){
									case 1:
										 // Start With What is BeamBox
										 ShowBeam.Hide();
										 HelpBalloon= new MyBalloonWindow();
										 HelpBalloon.AnchorQuadrant = AnchorQuadrant.Auto;
										 HelpBalloon.button3.Hide();
										 pos = new Point(this.Location.X+this.ToolBars_MenuBar_Song.ButtonBounds.X + this.ToolBars_MenuBar_Edit.ButtonBounds.X +this.ToolBars_MenuBar_View.ButtonBounds.X + 20  ,this.Location.Y+30);
										 intButtonClicked = HelpBalloon.ShowHelp(pos,"Projector Window","The Projector Window is used to display your Song on the Beamer. You can turn it on/off and set its Position.");
										 this.Focus();
										 HelpBalloon.Dispose();
										 switch (intButtonClicked){
											case 0:
												intHelpNum = 0;
											break;
											case 1:
												intHelpNum = 0;
											break;
											case 2:
												intHelpNum++;
											break;
										}
									break;
									case 2:
										 ShowBeam.Show();
										 this.BringToFront();
										 HelpBalloon= new MyBalloonWindow();
										 pos = new Point(this.Location.X+70,this.Location.Y+60);
										 intButtonClicked = HelpBalloon.ShowHelp(pos,"Show Projector Window","Press this Button to show the Projector Box, press it again to hide it.");
										 this.Focus();
										 HelpBalloon.Dispose();
										 switch (intButtonClicked){
											case 0:
												intHelpNum = 0;
											break;
											case 1:
												intHelpNum--;
											break;
											case 2:
												intHelpNum++;
											break;
										 }
									break;
									case 3:
										 ShowBeam.ShowMover();
										 ShowBeam.BringToFront();
										 this.BringToFront();
										 HelpBalloon= new MyBalloonWindow();
										 pos = new Point(this.Location.X+130,this.Location.Y+60);
										 intButtonClicked = HelpBalloon.ShowHelp(pos,"Size/Position Button","Press this Button for beeing able to move the Projector Box.");
										 this.Focus();
										 HelpBalloon.Dispose();
										 switch (intButtonClicked){
											case 0:
												intHelpNum = 0;
											break;
											case 1:
												intHelpNum--;
											break;
											case 2:
												intHelpNum++;
											break;
										 }
									break;
									case 4:
										// Main Toolbar
										 ShowBeam.ShowMover();
										 ShowBeam.BringToFront();
										 HelpBalloon= new MyBalloonWindow();
										 pos = new Point(ShowBeam.Location.X+140,ShowBeam.Location.Y+130);
										 intButtonClicked = HelpBalloon.ShowHelp(pos,"Position Box","Here you set the Projector Box’s position. You can enter the coordinates or use the buttons for a exact positioning. Of course, you can also drag the Projector Window (only if the Size/Position Button has been pressed).");
										 this.Focus();
										 HelpBalloon.Dispose();
										 switch (intButtonClicked){
											case 0:
												intHelpNum = 0;
											break;
											case 1:
												intHelpNum--;
											break;
											case 2:
												intHelpNum++;
											break;
										 }
									break;
									case 5:
										// Play List
										 ShowBeam.BringToFront();
										 HelpBalloon= new MyBalloonWindow();
										 HelpBalloon.button1.Text = "Finish";
										 pos = new Point(ShowBeam.Location.X+310,ShowBeam.Location.Y+130);
										 intButtonClicked = HelpBalloon.ShowHelp(pos,"Size Box","Define the Projector Window size the same way as the position.");
										 this.Focus();
										 HelpBalloon.Dispose();
										 switch (intButtonClicked){
											case 0:
												intHelpNum = 0;
												ShowBeam.HideMover();
												ShowBeam.Hide();
											break;
											case 1:
												intHelpNum--;
											break;
											case 2:
												intHelpNum = 0;
												ShowBeam.HideMover();
												ShowBeam.Hide();
											break;
										 }
									break;
								}
							}while (intHelpNum != 0);
						}
						#endregion

						#region Options
						private void HelpOptions_Activate(object sender, System.EventArgs e) {
							int intHelpNum = 1;
							int intButtonClicked;
							Point pos;
							this.Size = new Size(790,570);

							do{
								switch(intHelpNum){
									case 1:
										 HelpBalloon= new MyBalloonWindow();
										 HelpBalloon.AnchorQuadrant = AnchorQuadrant.Auto;
										 HelpBalloon.button3.Hide();
										 pos = new Point(this.Location.X+70 ,this.Location.Y+35);
										 intButtonClicked = HelpBalloon.ShowHelp(pos,"Options","The 'Options Panel' can be found in the Edit Menu.");
										 this.Focus();
										 HelpBalloon.Dispose();
										 switch (intButtonClicked){
											case 0:
												intHelpNum = 0;
											break;
											case 1:
												intHelpNum = 0;
											break;
											case 2:
												intHelpNum++;
											break;
										}
									break;
									case 2:
										Options.Show();
										Options.Location =  new Point(this.Location.X+80 ,this.Location.Y+70);
										 Options.tabControl.SelectedTab = Options.Bible_Tab;
										 HelpBalloon= new MyBalloonWindow();
										 HelpBalloon.AnchorQuadrant = AnchorQuadrant.Top;
										 pos = new Point(this.Location.X+ 180 ,this.Location.Y+300);
										 intButtonClicked = HelpBalloon.ShowHelp(pos,"Bible Settings","To be able using the Bible in DreamBeam, 'The Sword Project' must be installed. Here you can enter the Path to Sword (if it's installed), and select the language of it's Book titles.");
										 this.Focus();
										 Options.Focus();
										 HelpBalloon.Dispose();
										 switch (intButtonClicked){
											case 0:
												intHelpNum = 0;
												Options.Hide();
											break;
											case 1:
												intHelpNum--;
												Options.Hide();
											break;
											case 2:
												intHelpNum++;
											break;
										}
									break;
									case 3:
										Options.Show();
										Options.Location =  new Point(this.Location.X+80 ,this.Location.Y+70);
										 Options.tabControl.SelectedTab = Options.Graphics_tab;
										 HelpBalloon= new MyBalloonWindow();
										 HelpBalloon.AnchorQuadrant = AnchorQuadrant.Top;
										 pos = new Point(this.Location.X+ 180 ,this.Location.Y+300);
										 intButtonClicked = HelpBalloon.ShowHelp(pos,"Graphic Settings","Here you can change two Graphic Settings. AlphaBlendings are nice transitions between 2 Songs or Pictures, but it only makes sense if your PC is really, really fast. The Outline is a colored line around the text; here you can set its size.");
										 this.Focus();
										 Options.Focus();
										 HelpBalloon.Dispose();
										 switch (intButtonClicked){
											case 0:
												intHelpNum = 0;
												Options.Hide();
											break;
											case 1:
												intHelpNum--;
											break;
											case 2:
												intHelpNum++;
											break;
										}
									break;
									case 4:
										Options.Show();
										Options.Location =  new Point(this.Location.X+80 ,this.Location.Y+70);
										 Options.tabControl.SelectedTab = Options.BeamBox_tab;
										 HelpBalloon= new MyBalloonWindow();
										 HelpBalloon.AnchorQuadrant = AnchorQuadrant.Top;
										 pos = new Point(this.Location.X+ 180 ,this.Location.Y+300);
										 intButtonClicked = HelpBalloon.ShowHelp(pos,"Projector Settings","Like in the ‘Size/Position’ Options in the Projector Box, you can use these panels to set its size and position. (if the Projector Window somehow disappeared off the screen, you will be happy to get it back here ;-). And if you want to hide the MouseCursor on the Projector Window then do it here.");
										 this.Focus();
										 Options.Focus();
										 HelpBalloon.Dispose();
										 switch (intButtonClicked){
											case 0:
												intHelpNum = 0;
												Options.Hide();
											break;
											case 1:
												intHelpNum--;
											break;
											case 2:
												intHelpNum++;
											break;
										}
									break;
									case 5:
										 HelpBalloon= new MyBalloonWindow();
										 HelpBalloon.AnchorQuadrant = AnchorQuadrant.Top;
										 HelpBalloon.button1.Text = "Finish";
										 pos = new Point(this.Location.X+370,this.Location.Y+370);
										 intButtonClicked = HelpBalloon.ShowHelp(pos,"Save or Cancel","Finally, ‘Save’ your changes or 'Cancel' all the things you’ve done.");
										 this.Focus();
										 Options.Focus();
										 HelpBalloon.Dispose();
										 switch (intButtonClicked){
											case 0:
												intHelpNum = 0;
												Options.Hide();
											break;
											case 1:
												intHelpNum--;
											break;
											case 2:
												intHelpNum = 0;
												Options.Hide();
											break;
										 }
									break;
								}
							}while (intHelpNum != 0);



						}
						#endregion

						#region Show Songs
						///<summary> Displays Help Balloons - ShowSongs </summary>
						private void HelpShowSongs_Activate(object sender, System.EventArgs e) {
							int intHelpNum = 1;
							int intButtonClicked;
							Point pos;
							this.Size = new Size(790,570);

							do{
								switch(intHelpNum){
									case 1:
										 HelpBalloon= new MyBalloonWindow();
										 HelpBalloon.AnchorQuadrant = AnchorQuadrant.Auto;
										 HelpBalloon.button3.Hide();
										 pos = new Point(this.Location.X+40 ,this.Location.Y+100);
										 intButtonClicked = HelpBalloon.ShowHelp(pos,"Show Songs","The 'Show Songs' component is used to send songs and verses to the Projector Box. It will be activated with this Button.");
										 this.Focus();
										 HelpBalloon.Dispose();
										 switch (intButtonClicked){
											case 0:
												intHelpNum = 0;
											break;
											case 1:
												intHelpNum = 0;
											break;
											case 2:
												intHelpNum++;
											break;
										}
									break;
									case 2:
										 this.ShowTab(0);
										 HelpBalloon= new MyBalloonWindow();
										 HelpBalloon.AnchorQuadrant = AnchorQuadrant.Auto;
										 pos = new Point(this.Location.X+ 200 ,this.Location.Y+300);
										 intButtonClicked = HelpBalloon.ShowHelp(pos,"Verse Box","After loading a song from the SongList, you select here the verses to be displayed. To send them to the Projector Window click the “To Projector Box” button, or just double-click the verse.");
										 this.Focus();
										 HelpBalloon.Dispose();
										 switch (intButtonClicked){
											case 0:
												intHelpNum = 0;
											break;
											case 1:
												intHelpNum--;
											break;
											case 2:
												intHelpNum++;
											break;
										}
									break;
/*									case 3:
										 this.ShowTab(0);
										 HelpBalloon= new MyBalloonWindow();
										 HelpBalloon.AnchorQuadrant = AnchorQuadrant.Right;
										 pos = new Point(this.Location.X+ 90 ,this.Location.Y+250);
										 intButtonClicked = HelpBalloon.ShowHelp(pos,"Enable Alpha Blending","If you enable Alpha Blending, the Song and Strophe changes on the BeamBox will use a nice, but (in the moment) slow, transition effect.");
										 this.Focus();
										 HelpBalloon.Dispose();
										 switch (intButtonClicked){
											case 0:
												intHelpNum = 0;
											break;
											case 1:
												intHelpNum--;
											break;
											case 2:
												intHelpNum++;
											break;
										}
									break;*/
									case 3:
										 HelpBalloon= new MyBalloonWindow();
										 HelpBalloon.AnchorQuadrant = AnchorQuadrant.Right;
										 HelpBalloon.button1.Text = "Finish";
										 pos = new Point(this.Location.X+90,this.Location.Y+300);
										 intButtonClicked = HelpBalloon.ShowHelp(pos,"Overwrite Background","If you select an image from the ImageList and enable Overwrite, the Songs Background changes to the new one. ");
										 this.Focus();
										 HelpBalloon.Dispose();
										 switch (intButtonClicked){
											case 0:
												intHelpNum = 0;
											break;
											case 1:
												intHelpNum--;
											break;
											case 2:
												intHelpNum = 0;
											break;
										 }
									break;
								}
							}while (intHelpNum != 0);

						}
						#endregion


						#region EditSongs
						///<summary> Displays Help Balloons - EditSongs </summary>
						private void HelpEditSongs_Activate(object sender, System.EventArgs e) {
							int intHelpNum = 1;
							int intButtonClicked;
							Point pos;
							this.Size = new Size(790,570);

							do{
								switch(intHelpNum){
									case 1:
										 HelpBalloon= new MyBalloonWindow();
										 HelpBalloon.AnchorQuadrant = AnchorQuadrant.Auto;
										 HelpBalloon.button3.Hide();
										 pos = new Point(this.Location.X+40 ,this.Location.Y+150);
										 intButtonClicked = HelpBalloon.ShowHelp(pos,"Edit Songs","The ‘Edit Songs’ component is used to edit Song Lyrics, place the them on the Projector Window and select a Background. It will be activated with this Button.");
										 this.Focus();
										 HelpBalloon.Dispose();
										 switch (intButtonClicked){
											case 0:
												intHelpNum = 0;
											break;
											case 1:
												intHelpNum = 0;
											break;
											case 2:
												intHelpNum++;
											break;
										}
									break;
									case 2:
										 this.ShowTab(1);
										 HelpBalloon= new MyBalloonWindow();
										 HelpBalloon.AnchorQuadrant = AnchorQuadrant.Auto;
										 pos = new Point(this.Location.X+ 200 ,this.Location.Y+300);
										 intButtonClicked = HelpBalloon.ShowHelp(pos,"Text Boxes","There are three of them. The first is for the title, the second for the verses and the third one is for the song writer. Of course you can leave any of them free. To start a new verse in the verse box, insert two blank lines.");
										 this.Focus();
										 HelpBalloon.Dispose();
										 switch (intButtonClicked){
											case 0:
												intHelpNum = 0;
											break;
											case 1:
												intHelpNum--;
											break;
											case 2:
												intHelpNum++;
											break;
										}
									break;
									case 3:
										 this.ShowTab(1);
										 HelpBalloon= new MyBalloonWindow();
										 HelpBalloon.AnchorQuadrant = AnchorQuadrant.Auto;
										 pos = new Point(this.Location.X+ 200 ,this.Location.Y+200);
										 intButtonClicked = HelpBalloon.ShowHelp(pos,"Text Toolbars","Here you can select a font, the size, the color and an outline. An outline is a colored line around each letter, to make the text better readable on a image background.");
										 this.Focus();
										 HelpBalloon.Dispose();
										 switch (intButtonClicked){
											case 0:
												intHelpNum = 0;
											break;
											case 1:
												intHelpNum--;
											break;
											case 2:
												intHelpNum++;
											break;
										}
									break;
									case 4:
										 this.ShowTab(1);
										 HelpBalloon= new MyBalloonWindow();
										 HelpBalloon.AnchorQuadrant = AnchorQuadrant.Auto;
										 pos = new Point(this.Location.X+ 250 ,this.Location.Y+175);
										 intButtonClicked = HelpBalloon.ShowHelp(pos,"Position Fields","Use AutoPos to place the Text automatically on the Screen, or use the X-Y Coordinates to do it manually.");
										 this.Focus();
										 HelpBalloon.Dispose();
										 switch (intButtonClicked){
											case 0:
												intHelpNum = 0;
											break;
											case 1:
												intHelpNum--;
											break;
											case 2:
												intHelpNum++;
											break;
										}
									break;
									case 5:
										 this.ShowTab(1);
										 HelpBalloon= new MyBalloonWindow();
										 HelpBalloon.AnchorQuadrant = AnchorQuadrant.Right;
										 pos = new Point(this.Location.X+102,this.Location.Y+150);
										 intButtonClicked = HelpBalloon.ShowHelp(pos,"Preview Pane","This will display a small Preview of your Song. Click on it to enforce a refresh.");
										 this.Focus();
										 HelpBalloon.Dispose();
										 switch (intButtonClicked){
											case 0:
												intHelpNum = 0;
											break;
											case 1:
												intHelpNum--;
											break;
											case 2:
												intHelpNum++;
											break;
										}
									break;
									case 6:
										 this.ShowTab(1);
										 HelpBalloon= new MyBalloonWindow();
										 HelpBalloon.AnchorQuadrant = AnchorQuadrant.Right;
										 pos = new Point(this.Location.X+102,this.Location.Y+235);
										 intButtonClicked = HelpBalloon.ShowHelp(pos,"Next/Previous Verse","Switch the Verses.");
										 this.Focus();
										 HelpBalloon.Dispose();
										 switch (intButtonClicked){
											case 0:
												intHelpNum = 0;
											break;
											case 1:
												intHelpNum--;
											break;
											case 2:
												intHelpNum++;
											break;
										}
									break;
									case 7:
										 HelpBalloon= new MyBalloonWindow();
										 HelpBalloon.AnchorQuadrant = AnchorQuadrant.Right;
										 HelpBalloon.AnchorOffset = -2;
										 HelpBalloon.button1.Text = "Finish";
										 pos = new Point(this.Location.X+160,this.Location.Y+375);
										 intButtonClicked = HelpBalloon.ShowHelp(pos,"To Projector Box","Displays/Updates the Song on the Projector Box.");
										 this.Focus();
										 HelpBalloon.Dispose();
										 switch (intButtonClicked){
											case 0:
												intHelpNum = 0;
											break;
											case 1:
												intHelpNum--;
											break;
											case 2:
												intHelpNum = 0;
											break;
										 }
									break;
								}
							}while (intHelpNum != 0);

						}
						#endregion

						#region TextTool
						private void HelpTextTool_Activate(object sender, System.EventArgs e) {
							int intHelpNum = 1;
							int intButtonClicked;
							Point pos;
							this.Size = new Size(790,570);

							do{
								switch(intHelpNum){
									case 1:
										 HelpBalloon= new MyBalloonWindow();
										 HelpBalloon.AnchorQuadrant = AnchorQuadrant.Auto;
										 HelpBalloon.button3.Hide();
										 pos = new Point(this.Location.X+40 ,this.Location.Y+220);
										 intButtonClicked = HelpBalloon.ShowHelp(pos,"Text Tool","Use the 'Text Tool' to display Instant-Messages like Bible quotes or News. Of course, you are also able to display images without Text.");
										 this.Focus();
										 HelpBalloon.Dispose();
										 switch (intButtonClicked){
											case 0:
												intHelpNum = 0;
											break;
											case 1:
												intHelpNum = 0;
											break;
											case 2:
												intHelpNum++;
											break;
										}
									break;
									case 2:
										 this.ShowTab(2);
											if(Sermon_DocManager.TabStrips.Count < 1){
												this.Sermon_NewDocument();
											}

										 HelpBalloon= new MyBalloonWindow();
										 HelpBalloon.AnchorQuadrant = AnchorQuadrant.Auto;
										 pos = new Point(this.Location.X+ 200 ,this.Location.Y+300);
										 intButtonClicked = HelpBalloon.ShowHelp(pos,"Document Manager","Here you enter you Text Messages. You can use multiple Documents for different Messages. ");
										 this.Focus();
										 HelpBalloon.Dispose();
										 switch (intButtonClicked){
											case 0:
												intHelpNum = 0;
											break;
											case 1:
												intHelpNum--;
											break;
											case 2:
												intHelpNum++;
											break;
										}
									break;
									case 3:
										 HelpBalloon= new MyBalloonWindow();
										 HelpBalloon.AnchorQuadrant = AnchorQuadrant.Auto;
										 pos = new Point(this.Location.X+ 200 ,this.Location.Y+115);
										 intButtonClicked = HelpBalloon.ShowHelp(pos,"Text Toolbar","Create new Documents, Change Font, Color and Size of your Messages. You cannot set individual Font-Settings for different Documents, yet.");
										 this.Focus();
										 HelpBalloon.Dispose();
										 switch (intButtonClicked){
											case 0:
												intHelpNum = 0;
											break;
											case 1:
												intHelpNum--;
											break;
											case 2:
												intHelpNum++;
											break;
										}
									break;
									case 4:
										 HelpBalloon= new MyBalloonWindow();
										 HelpBalloon.AnchorQuadrant = AnchorQuadrant.Right;
										 pos = new Point(this.Location.X+ 150 ,this.Location.Y+160);
										 intButtonClicked = HelpBalloon.ShowHelp(pos,"The Bible Panel","Uses the 'Sword Project' to quote from the Bible. To set it up correctly, you have to install and define the Path to the 'Sword Project' in the 'Options' Menu. To get a result chose a translation, a book and enter a chapter and averse like '1 John 3:16'.");
										 this.Focus();
										 HelpBalloon.Dispose();
										 switch (intButtonClicked){
											case 0:
												intHelpNum = 0;
											break;
											case 1:
												intHelpNum--;
											break;
											case 2:
												intHelpNum++;
											break;
										}
									break;
									case 5:
										 HelpBalloon= new MyBalloonWindow();
										 HelpBalloon.AnchorQuadrant = AnchorQuadrant.Bottom;
										 HelpBalloon.button1.Text = "Finish";
										 pos = new Point(this.Location.X+400,this.Location.Y+320);
										 intButtonClicked = HelpBalloon.ShowHelp(pos,"To Projector Box","Displays/Updates the Text on the Projector Box.");
										 this.Focus();
										 HelpBalloon.Dispose();
										 switch (intButtonClicked){
											case 0:
												intHelpNum = 0;
											break;
											case 1:
												intHelpNum--;
											break;
											case 2:
												intHelpNum = 0;
											break;
										 }
									break;
								}
							}while (intHelpNum != 0);




						}
						#endregion


						///<summary>Displays the About Menu </summary>
						private void AboutButton_Activate(object sender, System.EventArgs e){
							About.version = this.version;
							About.ShowDialog();
						}


					#endregion
				#endregion

				#region Main Toolbar
					private void ToolBars_MainToolbar_ButtonClick(object sender, TD.SandBar.ToolBarItemEventArgs e) {



						// SHOW BEAMBOX
						if (e.Item == ToolBars_MainToolbar_ShowBeamBox){
							if (ToolBars_MainToolbar_ShowBeamBox.Checked){
								ToolBars_MainToolbar_ShowBeamBox.Checked = false;
								ShowBeam.Hide();
							}else{
								ToolBars_MainToolbar_ShowBeamBox.Checked = true;
								ShowBeam.Show();
							}
						}

						// SHOW BEAMBOX
						if (e.Item == ToolBars_MainToolbar_SizePosition){
							if (ToolBars_MainToolbar_SizePosition.Checked){
								ToolBars_MainToolbar_SizePosition.Checked = false;
								ShowBeam.HideMover();
							}else{
								ToolBars_MainToolbar_SizePosition.Checked = true;
								ShowBeam.ShowMover();
							}
						}



						// HIDE BG
						if (e.Item == ToolBars_MainToolbar_HideBG){
							if (ToolBars_MainToolbar_HideBG.Checked){
								ToolBars_MainToolbar_HideBG.Checked = false;
								ShowBeam.HideBG = false;
							}else{
								ToolBars_MainToolbar_HideBG.Checked = true;
								ShowBeam.HideBG = true;
							}
							// Repaint Image
							if (this.selectedTab == 0 || this.selectedTab == 1){
									LogFile.LogNew ("Drawing Preview Panel from: ToolBars_MainToolbar_HideBG / 0");
									Draw_Song_Preview_Image();
									ShowBeam.PaintSong();
							}
							if (this.selectedTab == 2){
//									SongShow_Preview_Panel.Invalidate();
									LogFile.LogNew ("Drawing Preview Panel from: ToolBars_MainToolbar_HideBG / 2");
									 Draw_Song_Preview_Image();
									ShowBeam.PaintSermon();
							}
						}
						// HIDE TEXT
						if (e.Item == ToolBars_MainToolbar_HideText){
							if (ToolBars_MainToolbar_HideText.Checked){
								ToolBars_MainToolbar_HideText.Checked = false;
								ShowBeam.HideText = false;
							}else{
								ToolBars_MainToolbar_HideText.Checked = true;
								ShowBeam.HideText = true;
							}

							// Repaint Image
							if (this.selectedTab == 0 || this.selectedTab == 1){
//									SongShow_Preview_Panel.Invalidate();
									LogFile.LogNew ("Drawing Preview Panel from: ToolBars_MainToolbar_HideText / 0");
									 Draw_Song_Preview_Image();
									ShowBeam.PaintSong();
							}
							if (this.selectedTab == 2){
									LogFile.LogNew ("Drawing Preview Panel from: ToolBars_MainToolbar_HideText / 2");
									 Draw_Song_Preview_Image();
									ShowBeam.PaintSermon();
							}

						}

						//SAVE SONG
						if (e.Item == ToolBars_MainToolbar_SaveSong){
							this.setSong();
							ShowBeam.Song.Save();
							this.ListSongs();
							this.StatusPanel.Text = "Song '"+ ShowBeam.Song.SongName +"'has been saved.";
						}


					}

				#endregion

				#region Component Bar

					///<summarize> Switch Components on Click </summarize>
					private void ToolBars_ComponentBar_ButtonClick(object sender, TD.SandBar.ToolBarItemEventArgs e)
					{
						PreviewUpdateTimer.Enabled = false;
						//MessageBox.Show(e.Item.ButtonBounds.Top.ToString());
						if(e.Item == ShowSongs_Button){
						//.ButtonBounds.Top == 3
							this.getSong();
							this.ShowTab(0);
						}
						if(e.Item == EditSongs_Button){
							this.ShowTab(1);
							PreviewUpdateTimer.Enabled = true;
						}
						if (e.Item == Presentation_Button){
							this.ShowTab(3);
						}
						if(e.Item == Sermon_Button){
							this.ShowTab(2);
							if(Sermon_DocManager.TabStrips.Count < 1){
                        			this.Sermon_NewDocument();
							}
						}

					}




				#endregion

				#region Right Docks

					#region SongList
						///<summary> Load Song from DoubleClicked Listbox</summary>
						private void RightDocks_SongList_DoubleClick(object sender, System.EventArgs e)
						{
						 this.LoadSelectedSong();

						}

						private void LoadSelectedSong(){
							if (this.RightDocks_SongList.SelectedIndex >= 0){
								LogFile.LogNew("Loading Song :" + RightDocks_SongList.SelectedItem.ToString());
								ShowBeam.Song.Load(RightDocks_SongList.SelectedItem.ToString());
								LogFile.Log(" -> BG Image:" + ShowBeam.Song.bg_image);

								this.SongEdit_BG_Label.Text = ShowBeam.Song.bg_image;

								this.getSong();

								this.StatusPanel.Text = "Song '"+ RightDocks_SongList.SelectedItem.ToString()  +"' loaded.";
							}

							GC.Collect();
						}


						///<summary> Delete Song in List </summary>
						private void btnRightDocks_SongListDelete_Click(object sender, System.EventArgs e)
						{
							if (this.RightDocks_SongList.SelectedIndex >= 0){
								if (System.IO.File.Exists("Songs\\"+RightDocks_SongList.SelectedItem.ToString()+".xml")){
									DialogResult answer = MessageBox.Show(RightDocks_SongList.SelectedItem.ToString()+ " wirklich löschen?","Söng löschen..", MessageBoxButtons.YesNo);
									if (answer == DialogResult.Yes){
										System.IO.File.Delete("Songs\\"+RightDocks_SongList.SelectedItem.ToString()+".xml");
									}
								}
								this.ListSongs();
							}
						}

						///<summary> Add a Song to a PlayList</summary>
						private void btnRightDocks_SongList2PlayList_Click(object sender, System.EventArgs e)
						{
							bool addnew = true;
							if (this.RightDocks_SongList.SelectedIndex >= 0){
								for(int i = 0;i < this.RightDocks_PlayList.Items.Count;i++){
								if (this.RightDocks_PlayList.Items[i].ToString() == this.RightDocks_SongList.SelectedItem.ToString()){
									addnew = false;
								}
							 }
							 if (addnew){
								this.RightDocks_PlayList.Items.Add(this.RightDocks_SongList.SelectedItem.ToString());
								this.SaveSettings();
							  }
							}


						}

						///<summary> Search Songlist for entered Characters</summary>
						private void RightDocks_SongListSearch_TextChanged(object sender, System.EventArgs e)
						{
							if(this.RightDocks_SongListSearch.Text.Length > 0){
								for (int i = this.RightDocks_SongList.Items.Count-1; i >= 0;i--){
									if (this.RightDocks_SongList.Items[i].ToString().Substring(0,this.RightDocks_SongListSearch.Text.Length).ToLower() == this.RightDocks_SongListSearch.Text.ToLower()){
										RightDocks_SongList.SelectedIndex = i;
									}
								}
							}
						}
					#endregion

					#region PlayList

						/// <summary>Load Song from Playlist on DoubleClick </summary>
						private void RightDocks_PlayList_DoubleClick(object sender, System.EventArgs e){
							if (this.RightDocks_PlayList.SelectedIndex >= 0){
								string tmp = RightDocks_PlayList.SelectedItem.ToString();
								ShowBeam.Song.Load(tmp);
								this.getSong();
								this.StatusPanel.Text = "Song '"+ tmp  +"' loaded.";
							}

						}

						/// <summary>Remove Selected Playlistitem on Click</summary>
						private void RightDocks_PlayList_Remove_Button_Click(object sender, System.EventArgs e){
							if (this.RightDocks_PlayList.SelectedIndex >= 0){
								int tmp = this.RightDocks_PlayList.SelectedIndex;
								this.RightDocks_PlayList.Items.RemoveAt(this.RightDocks_PlayList.SelectedIndex);
								this.SaveSettings();
								if(this.RightDocks_PlayList.Items.Count > tmp){
									this.RightDocks_PlayList.SelectedIndex = tmp;
								}
							}
						}

						/// <summary>Move selected PlayList Item up, on click </summary>
						private void RightDocks_PlayList_Up_Button_Click(object sender, System.EventArgs e){
							if (this.RightDocks_PlayList.SelectedIndex > 0){
								int i = this.RightDocks_PlayList.SelectedIndex;
								this.RightDocks_PlayList.Items.Insert(i-1,RightDocks_PlayList.SelectedItem);
								this.RightDocks_PlayList.SelectedIndex = i-1;
								this.RightDocks_PlayList.Items.RemoveAt(i+1);
								this.SaveSettings();
							}
						}

						/// <summary>Move selected PlayList Item down, on click </summary>
						private void RightDocks_PlayList_Down_Button_Click(object sender, System.EventArgs e){
							if (this.RightDocks_PlayList.SelectedIndex < this.RightDocks_PlayList.Items.Count-1){
								int i = this.RightDocks_PlayList.SelectedIndex;
								this.RightDocks_PlayList.Items.Insert(i+2,RightDocks_PlayList.SelectedItem);
								this.RightDocks_PlayList.SelectedIndex = i+2;
								this.RightDocks_PlayList.Items.RemoveAt(i);
								this.SaveSettings();
							}
						}

					#endregion

					#region BGImageList

						/// <summary>starts the ListImages_Threader Thread</summary>
						private void ListImages(string directory){
						   if (g_Bg_Directory == null){
								this.g_Bg_Directory = directory;
								Thread_BgImage = new Thread( new ThreadStart(ListImages_Threader));
								Thread_BgImage.IsBackground = true;
								Thread_BgImage.Start();
						   }
						}


						/// <summary>Loads all images from \Backgrounds, puts them in this.RightDocks_imageList and links those with names in ImageListBox  </summary>
						void ListImages_Threader(){
							string directory = g_Bg_Directory;

							LogFile.LogNew("Listung Images for "+directory);

							if(!System.IO.Directory.Exists(directory)){
								System.IO.Directory.CreateDirectory(directory);
							}

							int intImageCount=0;
							RightDocks_imageList.Images.Clear();
							this.RightDocks_ImageListBox.Items.Clear();

							// Define Directory and ImageTypes
							string strImageDir = Directory.GetCurrentDirectory()+"\\"+directory;
							string [] filetypes = {"*.bmp", "*.jpg", "*.png","*.gif","*.jpeg"};
							int filecount = 0;

							//find the number of Files in Directory
							foreach (string filetype in filetypes){
									string[] dirs = Directory.GetFiles(@strImageDir, filetype);
									foreach (string dir in dirs)
									{
										filecount++;
									}
							}

							int i_file = 1;
							//find all files from defined FileTypes
							foreach (string filetype in filetypes){
									string[] dirs = Directory.GetFiles(@strImageDir, filetype);
									foreach (string dir in dirs)
									{
										LogFile.Log(" -> Processing Image"+dir);
										// Add to ImageList
										LogFile.Log("  --> add Image to ImageList");
										this.RightDocks_imageList.Images.Add(ShowBeam.Resizer(dir,80,60));
										Controls.Development.ImageListBoxItem x = new Controls.Development.ImageListBoxItem(Path.GetFileName(dir),intImageCount);
										// Add to ImageListBox
										LogFile.Log("  --> add link to ImageListBox");
										this.RightDocks_ImageListBox.Items.Add(x);
										intImageCount++;
										this.StatusPanel.Text = "Loading Images... " + Convert.ToString(100*i_file/filecount) + "%";
										i_file++;
										//										+Convert.ToString(System.Math.Round((double)(intImageCount/filecount*100)))+"%";
									}
							}
										this.StatusPanel.Text = "Images loaded. ";

										g_Bg_Directory = null;
					  }



						/// <summary></summary>
						private void ListDirectories(string directory){

							if(!System.IO.Directory.Exists(directory)){
								System.IO.Directory.CreateDirectory(directory);
							}

							int intImageCount=0;
							this.RightDocks_imageList.Images.Clear();
							this.RightDocks_ImageListBox.Items.Clear();

							// Define Directory and ImageTypes
							string strImageDir = Directory.GetCurrentDirectory()+"\\"+directory;

							string[] folders = Directory.GetDirectories(@strImageDir);
							RightDocks_FolderDropdown.Items.Clear();
							RightDocks_FolderDropdown.Items.Add("- Top -");
							foreach (string folder in folders){
								RightDocks_FolderDropdown.Items.Add(Tools.Reverse(Tools.Reverse(folder).Substring(0,Tools.Reverse(folder).IndexOf(@"\"))));
							}
						}

						///<summary>If BGImage chosen, update the current component </summary>
						private void RightDocks_ImageListBox_SelectedIndexChanged(object sender, System.EventArgs e)

						{
							string path = @"Backgrounds\";
							if (RightDocks_FolderDropdown.SelectedIndex > 0)   {
									path = path + RightDocks_FolderDropdown.Items[RightDocks_FolderDropdown.SelectedIndex].ToString() + @"\";
							}


							if (this.selectedTab == 1){
								 ShowBeam.Song.bg_image = 	path + this.RightDocks_ImageListBox.Items[RightDocks_ImageListBox.SelectedIndex].Text;
								 this.SongEdit_BG_Label.Text = 	path + this.RightDocks_ImageListBox.Items[RightDocks_ImageListBox.SelectedIndex].Text;
//								 this.SongEdit_Preview_Panel.Invalidate();
							}else if(this.selectedTab == 0){
								ShowBeam.ImageOverWritePath = 	path + this.RightDocks_ImageListBox.Items[RightDocks_ImageListBox.SelectedIndex].Text;
								this.SongShow_BG_Label.Text = 	path + this.RightDocks_ImageListBox.Items[RightDocks_ImageListBox.SelectedIndex].Text;
							}else if(this.selectedTab == 2){
								ShowBeam.SermonImagePath = 	path + this.RightDocks_ImageListBox.Items[RightDocks_ImageListBox.SelectedIndex].Text;
							}
						}


						private void RightDocks_FolderDropdown_SelectionChangeCommitted(object sender, System.EventArgs e) {
							LogFile.Log("\n ImageList Dropdown changed");
							string path = @"Backgrounds\";
							if (RightDocks_FolderDropdown.SelectedIndex > 0)   {
								path = path + RightDocks_FolderDropdown.Items[RightDocks_FolderDropdown.SelectedIndex].ToString() + @"\";
							}
							this.ListImages(path);
							GC.Collect();
						}

						#endregion


				#endregion


			#endregion

			#region SHOW Songs

				///<summary> Update BeamBox </summary>
				private void SongEdit_UpdateBeamBox_Button_Click(object sender, System.EventArgs e)
				{

					if(ToolBars_MainToolbar_ShowBeamBox.Checked == false){
						ToolBars_MainToolbar_ShowBeamBox.Checked = true;
						ShowBeam.Show();
					}

					ShowBeam.PaintSong();
				}

				///<summary> On Click on ListEx with Strophes, the selected strophe will be shown on Beambox </summary>
				private void SongShow_StropheList_ListEx_DoubleClick(object sender, System.EventArgs e)
				{

					if(ToolBars_MainToolbar_ShowBeamBox.Checked == false){
						ToolBars_MainToolbar_ShowBeamBox.Checked = true;
						ShowBeam.Show();
					}

					ShowBeam.Song.strophe = SongShow_StropheList_ListEx.SelectedIndex;
					ShowBeam.newText = true;

					LogFile.LogNew ("Drawing Preview Panel from: SongShow_StropheList_ListEx_DoubleClick");
					Draw_Song_Preview_Image();
					ShowBeam.PaintSong();
				}

				///<summary>On OverWrite Check Change, forward the property to ShowBeam </summary>
				private void SongShow_BG_CheckBox_CheckedChanged(object sender, System.EventArgs e)
				{
					ShowBeam.OverWriteBG = this.SongShow_BG_CheckBox.Checked;
				}


				///<summary>Redraw Panel 8 on Click </summary>
				private void SongShow_Preview_Panel_Click(object sender, System.EventArgs e)
				{
					 LogFile.LogNew ("Drawing Preview Panel from:  SongShow_Preview_Panel_Click");
					 Draw_Song_Preview_Image();
				}

			#endregion

			#region EDIT Songs

				///<summary>Shows the previous strophe on click </summary>
				private void SongEdit_PreviewStropheDown_Button_Click(object sender, System.EventArgs e)
				{
					//initialize Strophes
					this.setSong();
					this.getSong();
					if(ShowBeam.Song.strophe > 0){
						ShowBeam.Song.strophe --;
					 LogFile.LogNew ("Drawing Preview Panel from: SongEdit_PreviewStropheDown_Button_Click");
					 Draw_Song_Preview_Image();
					}
				}

				///<summary> Shows the next strophe on click </summary>
				private void SongEdit_PreviewStropheUp_Button_Click(object sender, System.EventArgs e)
				{
					//initialize Strophes
					this.setSong();
					this.getSong();
					if(ShowBeam.Song.strophe < ShowBeam.Song.strophe_count ){
						ShowBeam.Song.strophe ++;
   					LogFile.LogNew ("Drawing Preview Panel from: SongEdit_PreviewStropheUp_Button_Click");
					 Draw_Song_Preview_Image();
					}
				}


				/// <summary>Sends the Clicked Item and the Textfield Number to TextProperties </summary>
				private void SongEdit_TopTextToolBar_ButtonClick(object sender, TD.SandBar.ToolBarItemEventArgs e)
				{
					this.TextProperties(e,0);
				}

				/// <summary>Sends the Clicked Item and the Textfield Number to TextProperties </summary>
				private void SongEdit_MidTextToolBar_ButtonClick(object sender, TD.SandBar.ToolBarItemEventArgs e)
				{
					this.TextProperties(e,1);
				}

				/// <summary>Sends the Clicked Item and the Textfield Number to TextProperties </summary>
				private void SongEdit_BottomTextToolBar_ButtonClick(object sender, TD.SandBar.ToolBarItemEventArgs e)
				{
					this.TextProperties(e,2);
				}

				/// <summary>If the Header Text has changed, update the Song-Array </summary>
				private void SongEdit_Header_TextBox_TextChanged(object sender, System.EventArgs e)
				{
					ShowBeam.Song.Text[0]=this.SongEdit_Header_TextBox.Text;
				}

				/// <summary>If the Mid Text has changed, update the Song-Array </summary>
				private void SongEdit_Mid_TextBox_TextChanged1(object sender, System.EventArgs e)
				{
					ShowBeam.Song.Text[1]=this.SongEdit_Mid_TextBox.Text;
					this.UpdatePreview = true;
					this.TextTyped = true;
				}


				/// <summary>If the Footer Text has changed, update the Song-Array </summary>
				private void Footer_TextBox_TextChanged(object sender, System.EventArgs e)
				{
					ShowBeam.Song.Text[2]=this.Footer_TextBox.Text;
				}

				/// <summary>Redraw Panel 4 on click </summary>
			  /*	private void SongEdit_Preview_Panel_Click(object sender, System.EventArgs e)
				{
					SongEdit_Preview_Panel.Invalidate();
				}*/


				/// <summary>If the SongEdit_TopPosX_UpDown has changed, update the Song-Array and Redraw Panel4 </summary>
				private void SongEdit_TopPosX_UpDown_ValueChanged(object sender, System.EventArgs e)
				{
					ShowBeam.Song.posX[0] = (int)SongEdit_TopPosX_UpDown.Value;
					 Draw_Song_Preview_Image();
				}

				/// <summary>If the SongEdit_TopPosY_UpDown has changed, update the Song-Array and Redraw Panel4 </summary>
				private void SongEdit_TopPosY_UpDown_ValueChanged(object sender, System.EventArgs e)
				{
					ShowBeam.Song.posY[0] = (int)SongEdit_TopPosY_UpDown.Value;
					 Draw_Song_Preview_Image();
				}

				/// <summary>If the SongEdit_MidPosX_UpDown has changed, update the Song-Array and Redraw Panel4 </summary>
				private void SongEdit_MidPosX_UpDown_ValueChanged(object sender, System.EventArgs e)
				{
					ShowBeam.Song.posX[1] = (int)SongEdit_MidPosX_UpDown.Value;

					 Draw_Song_Preview_Image();
				}

				/// <summary>If the SongEdit_MidPosY_UpDown has changed, update the Song-Array and Redraw Panel4 </summary>
				private void SongEdit_MidPosY_UpDown_ValueChanged(object sender, System.EventArgs e)
				{
					ShowBeam.Song.posY[1] = (int)SongEdit_MidPosY_UpDown.Value;
					 Draw_Song_Preview_Image();
				}

				/// <summary>If the SongEdit_BottomPosX_UpDown has changed, update the Song-Array and Redraw Panel4 </summary>
				private void SongEdit_BottomPosX_UpDown_ValueChanged(object sender, System.EventArgs e)
				{
					ShowBeam.Song.posX[2] = (int)SongEdit_BottomPosX_UpDown.Value;
					 Draw_Song_Preview_Image();
				}

				/// <summary>If the SongEdit_BottomPosY_UpDown has changed, update the Song-Array and Redraw Panel4 </summary>
				private void SongEdit_BottomPosY_UpDown_ValueChanged(object sender, System.EventArgs e)
				{
					ShowBeam.Song.posY[2] = (int)SongEdit_BottomPosY_UpDown.Value;
					 Draw_Song_Preview_Image();
				}


				///<summary>Preview Panel for EDIT Songs </summary>
				private void SongEdit_Preview_Panel_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
				{
				/*	Graphics g = e.Graphics;
					e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
					e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
					e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

					if (memoryBitmap != null){
					   g.DrawImage(memoryBitmap, 0, 0, SongShow_Preview_Panel.Width, SongShow_Preview_Panel.Height);
					}

					ShowBeam.DrawSongBitmap2(ShowBeam.Width,ShowBeam.Height);
					memoryBitmap = ShowBeam.bmp;
					g.DrawImage(memoryBitmap, 0, 0,SongShow_Preview_Panel.Width ,SongShow_Preview_Panel.Height);*/
					 LogFile.LogNew ("Drawing Preview Panel from: SongEdit_Preview_Panel_Paint");
					 Draw_Song_Preview_Image();
				}

				private void Draw_Song_Preview_Image(){
					if(this.AllowPreview){
						LogFile.Log (" -> Doing Preview Image");
						Graphics g = Graphics.FromHwnd(SongShow_Preview_Panel.Handle);
						g.SmoothingMode = SmoothingMode.HighQuality;
						g.InterpolationMode = InterpolationMode.HighQualityBicubic;
						g.PixelOffsetMode = PixelOffsetMode.HighQuality;

					   /*	if (memoryBitmap != null){
						   g.DrawImage(memoryBitmap, 0, 0, SongShow_Preview_Panel.Width, SongShow_Preview_Panel.Height);
						}*/
						ShowBeam.DrawSongBitmap2(ShowBeam.Width,ShowBeam.Height);
						memoryBitmap = ShowBeam.bmp;
						g.DrawImage(memoryBitmap, 0, 0,SongShow_Preview_Panel.Width ,SongShow_Preview_Panel.Height);
					}
				}

				///<summarize>Manages the 3 Text Toolbar ButtonClicks in EDIT Songs </summarize>
				private void TextProperties(TD.SandBar.ToolBarItemEventArgs e, int where){
					 // MessageBox.Show(e.Item.ButtonBounds.X.ToString());


					switch(e.Item.ButtonBounds.X){
					 case 3:
						this.SongEdit_fontDialog.Font = new Font(ShowBeam.Song.FontFace[where],ShowBeam.Song.FontSize[where]);
						this.SongEdit_fontDialog.ShowDialog();
						ShowBeam.Song.FontFace[where]=this.SongEdit_fontDialog.Font.Name;
						ShowBeam.Song.FontSize[where]=this.SongEdit_fontDialog.Font.Size;
					 break;
					 case 37:
						SongEdit_TextColorDialog.Color = ShowBeam.Song.TextColor[where];
						SongEdit_TextColorDialog.ShowDialog();
						ShowBeam.Song.TextColor[where] = this.SongEdit_TextColorDialog.Color;
					 break;
					 case 82:
						if (ShowBeam.Song.TextEffect[where] == "Filled Outline"){
							ShowBeam.Song.TextEffect[where] = "Normal";
						}else {
							ShowBeam.Song.TextEffect[where] = "Filled Outline";
						}
						this.SetEditButtonsStatus();
					 break;
					 case 151:
						SongEdit_TextColorDialog.Color = ShowBeam.Song.OutlineColor[where];
						SongEdit_TextColorDialog.ShowDialog();
						ShowBeam.Song.OutlineColor[where] = this.SongEdit_TextColorDialog.Color;
					 break;
					}
					LogFile.LogNew ("Drawing Preview Panel from: TextProperties");
					 Draw_Song_Preview_Image();
				}


				/// <summary> Gets the the Fontsettings for Font Dialog </summary>
				private void Set_TextProperty_Controls(){
					this.SongEdit_fontDialog.Font = new Font(ShowBeam.Song.FontFace[this.Song_Edit_Box],ShowBeam.Song.FontSize[this.Song_Edit_Box], this.SongEdit_fontDialog.Font.Style);
				}

				///<summary>Shows a FontDialog and Changes the Song FontSettings </summary>
				private void FontButton_Click(object sender, System.EventArgs e)
				{
					this.SongEdit_fontDialog.ShowDialog();
					ShowBeam.Song.FontFace[this.Song_Edit_Box]=this.SongEdit_fontDialog.Font.Name;
					ShowBeam.Song.FontSize[this.Song_Edit_Box]=this.SongEdit_fontDialog.Font.Size;
				}

				///<summary>Shows a ColorDialog and Changes the Song ColorSettings </summary>
				private void ColorButton_Click(object sender, System.EventArgs e)
				{
					SongEdit_TextColorDialog.ShowDialog();
					ShowBeam.Song.TextColor[this.Song_Edit_Box] = this.SongEdit_TextColorDialog.Color;
				}


				/// <summary> If Outline, then enable OutlineButton and Show Outline Color </summary>
				private void SetEditButtonsStatus(){

					// Header TextBox
					if (ShowBeam.Song.TextEffect[0]=="Filled Outline"){
						this.SongEdit_ButtonTopTextOutline.Checked = true;
						this.SongEdit_ButtonTopOutlineColor.Visible = true;
					}else{
						this.SongEdit_ButtonTopTextOutline.Checked = false;
						this.SongEdit_ButtonTopOutlineColor.Visible = false;
					}

					// Main TextBox
					if (ShowBeam.Song.TextEffect[1]=="Filled Outline"){
						this.SongEdit_ButtonMidTextOutline.Checked = true;
						this.SongEdit_ButtonMidOutlineColor.Visible = true;
					}else{
						this.SongEdit_ButtonMidTextOutline.Checked = false;
						this.SongEdit_ButtonMidOutlineColor.Visible = false;
					}

					// Footer TextBox
					if (ShowBeam.Song.TextEffect[2]=="Filled Outline"){
						this.SongEdit_ButtonBottomTextOutline.Checked = true;
						this.SongEdit_ButtonBottomOutlineColor.Visible = true;
					}else{
						this.SongEdit_ButtonBottomTextOutline.Checked = false;
						this.SongEdit_ButtonBottomOutlineColor.Visible = false;
					}
				}

				///<summary>Highlight HeaderTextBox </summary>
				private void SongEdit_Header_TextBox_Enter(object sender, System.EventArgs e)
				{
					this.Song_Edit_Box=0;
					this.SongEdit_Header_TextBox.BackColor = Color.WhiteSmoke;
					this.SongEdit_Mid_TextBox.BackColor = Color.White;
					this.Footer_TextBox.BackColor = Color.White;
					this.Set_TextProperty_Controls();
				}

				///<summary>Highlight MidTextBox </summary>
				private void SongEdit_Mid_TextBox_Enter(object sender, System.EventArgs e)
				{
					this.Song_Edit_Box=1;
					this.SongEdit_Header_TextBox.BackColor = Color.White;
					this.SongEdit_Mid_TextBox.BackColor = Color.WhiteSmoke;
					this.Footer_TextBox.BackColor = Color.White;
					this.Set_TextProperty_Controls();
				}

				///<summary>Highlight FooterTextBox </summary>
				private void Footer_TextBox_Enter(object sender, System.EventArgs e)

				{
					this.Song_Edit_Box=2;
					this.SongEdit_Header_TextBox.BackColor = Color.White;
					this.SongEdit_Mid_TextBox.BackColor = Color.White;
					this.Footer_TextBox.BackColor = Color.WhiteSmoke;
					this.Set_TextProperty_Controls();
				}

				///<summary>On Mid_TextX Change, update Song </summary>
				private void Mid_TextX_ValueChanged(object sender, System.EventArgs e)
				{
//stefan					ShowBeam.Song.posX[this.Song_Edit_Box] = Convert.ToInt32(this.TextX.Value);
				}

				///<summary>On Mid_TextY Change, update Song </summary>
				private void Mid_TextY_ValueChanged(object sender, System.EventArgs e)
				{
//stefan					ShowBeam.Song.posY[this.Song_Edit_Box] = Convert.ToInt32(this.TextY.Value);
				}

				///<summary>On SongEdit_Mid_TextBox_Text Change, update Song </summary>
				private void SongEdit_Mid_TextBox_TextChanged(object sender, System.EventArgs e)
				{
					ShowBeam.Song.Text[this.Song_Edit_Box]= SongEdit_Mid_TextBox.Text;
				}

				///<summary>put Top_AutoPos - Value into Song information </summary>
				private void SongEdit_Top_AutoPos_CheckBox_Click(object sender, System.EventArgs e) {
					ShowBeam.Song.AutoPos[0] = SongEdit_Top_AutoPos_CheckBox.Checked;
					SongEdit_CheckAlignBox();
				}
				///<summary>put Mid_AutoPos - Value into Song information </summary>
				private void checkBox1_Click(object sender, System.EventArgs e) {
					ShowBeam.Song.AutoPos[1] =SongEdit_Mid_AutoPos_CheckBox.Checked;
					SongEdit_CheckAlignBox();
				}
				///<summary>put Bottom_AutoPos - Value into Song information </summary>
				private void SongEdit_Bottom_AutoPos_CheckBox_Click(object sender, System.EventArgs e) {
					ShowBeam.Song.AutoPos[2] = SongEdit_Bottom_AutoPos_CheckBox.Checked;
					SongEdit_CheckAlignBox();
				}

			#endregion

			#region SermonTools
				private void BibleInit(){

					BibleBooks[0] = "Genesis,Exodus,Genesis,Exodus,Leviticus,Numbers,Deuteronomy,Joshua,Judges,Ruth,1 Samuel,2 Samuel,1 Kings,2 Kings,1 Chronicles,2 Chronicles,Ezra,Nehemiah,Esther,Job,Psalm,Proverbs,Ecclesiastes,Song of Solomon,Isaiah,Jeremiah,Lamentations,Ezekiel,Daniel,Hosea,Joel,Amos,Obadiah,Jonah,Micah,Nahum,Habakkuk,Zephaniah,Haggai,Zechariah,Malachi";
					BibleBooks[1] = "Matthew,Mark,Luke,John,Acts,Romans,1 Corinthians,2 Corinthians,Galatians,Ephesians,Philippians,Colossians,1 Thessalonians,2 Thessalonians,1 Timothy,2 Timothy,Titus,Philemon,Hebrews,James,1 Peter,2 Peter,1 John,2 John,3 John,Jude,Revelation";

					if (this.SwordProject_Found){

						Diatheke.locale = this.Sermon_BibleLang;
						Diatheke.book = "system";
						Diatheke.key = "modulelistnames";
						Diatheke.query();

						//split the book list by line into an array
						String[] Booklist = Diatheke.value.Substring(0,Diatheke.value.Length-1).Split((char)10);

						// Do a simple Query
						Diatheke.book = "KJV";
						Diatheke.key = "John 1:1";
						Diatheke.query();

						// and add them each to the list control
						foreach (string Book in Booklist){
							Sermon_Books.Items.Add(Book);
							this.Sermon_Books.SelectedIndex = 0;
							Diatheke.book = Booklist[0];

						}

						Diatheke.autoupdate = true;
						this.Diatheke.ValueChanged += new System.EventHandler(this.Diatheke_ValueChanged);

						//split the book list by line into an array
						String[] Books = BibleBooks[0].Split(',');
						foreach (string Book in Books){
							Sermon_BookList.Items.Add(Book);
						}

					}

				}

				private void Sermon_Testament_ListBox_SelectedIndexChanged(object sender, System.EventArgs e) {

						Sermon_BookList.Items.Clear();
						//split the book list by line into an array
						String[] Books = BibleBooks[Sermon_Testament_ListBox.SelectedIndex].Split(',');
						foreach (string Book in Books){
							Sermon_BookList.Items.Add(Book);
						}

				}

				private void Diatheke_ValueChanged(object sender, System.EventArgs e) {

						if (this.SwordProject_Found){
							this.StatusPanel.Text= Diatheke.value;
							// break lines
							string strTempText = "";
							int i = 0;
							int j= 0;
							do{
								strTempText = strTempText + Diatheke.value.Substring(i,1);
								if(j > 40 ){
									if (Diatheke.value.Substring(i,1) == " "){
										strTempText = strTempText + "\n";
										j= 0;
									}
								}
								i++;
								j++;
							}while (i < Diatheke.value.Length);


							// if not ShowBibleTranslation Hide the Translation Text
							if (this.Sermon_ShowBibleTranslation){
								Sermon_DocManager.FocusedDocument.Control.Text = strTempText;
								//Sermon_Diatheke.value;
							}else{
							Sermon_DocManager.FocusedDocument.Control.Text = strTempText.Substring(0,strTempText.IndexOf("("+Diatheke.book+")")-1);

							}
							Sermon_DocManager.FocusedDocument.Text = Diatheke.key;
						}
				}

				private void Sermon_BibleKey_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
						if (this.SwordProject_Found){
							 if (e.KeyValue == 13){
								Diatheke.query();
							}
						}
				}

				private void Sermon_NewDocument(){
					System.Windows.Forms.RichTextBox t = new System.Windows.Forms.RichTextBox();

					t.BorderStyle = BorderStyle.None;
					t.ScrollBars = RichTextBoxScrollBars.Both;
					t.WordWrap = true;

					t.Multiline = true;
					t.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Both;

					t.Font = new Font("Courier New", 10, FontStyle.Regular);
					DocumentManager.Document document = new DocumentManager.Document(t, "Document ");
					Sermon_DocManager.AddDocument(document);
//					Sermon_DocManager.FocusedDocument = document;


				}

				private void Sermon_ToolBar_ButtonClick(object sender, TD.SandBar.ToolBarItemEventArgs e) {

					// new Document
					if (e.Item == Sermon_ToolBar_NewDoc_Button){
						this.Sermon_NewDocument();
					}

					// Font + Size
					if (e.Item == Sermon_ToolBar_Font_Button){
						this.SongEdit_fontDialog.Font = new Font(ShowBeam.Sermon.FontFace[1],ShowBeam.Sermon.FontSize[1]);
						this.SongEdit_fontDialog.ShowDialog();
						ShowBeam.Sermon.FontFace[1]=this.SongEdit_fontDialog.Font.Name;
						ShowBeam.Sermon.FontSize[1]=this.SongEdit_fontDialog.Font.Size;

					}

					// FontColor
					if (e.Item == Sermon_ToolBar_Color_Button){
						SongEdit_TextColorDialog.Color = ShowBeam.Sermon.TextColor[1];
						SongEdit_TextColorDialog.ShowDialog();
						ShowBeam.Sermon.TextColor[1] = this.SongEdit_TextColorDialog.Color;
					}

					// Outline
					if (e.Item == Sermon_ToolBar_Outline_Button){
						if (ShowBeam.Sermon.TextEffect[1] == "Filled Outline"){
							ShowBeam.Sermon.TextEffect[1] = "Normal";
							Sermon_ToolBar_OutlineColor_Button.Visible = false;
						}else {
							ShowBeam.Sermon.TextEffect[1] = "Filled Outline";
							Sermon_ToolBar_OutlineColor_Button.Visible = true;
						}
					}

					if (e.Item == Sermon_ToolBar_OutlineColor_Button){
						SongEdit_TextColorDialog.Color = ShowBeam.Sermon.OutlineColor[1];
						SongEdit_TextColorDialog.ShowDialog();
						ShowBeam.Sermon.OutlineColor[1] = this.SongEdit_TextColorDialog.Color;
					}



				}

				private void Sermon_DocManager_CloseButtonPressed(object sender, DocumentManager.CloseButtonPressedEventArgs e) {
					Sermon_DocManager.RemoveDocument(Sermon_DocManager.FocusedDocument);
				}

				private void Sermon_BibleKey_TextChanged(object sender, System.EventArgs e) {
					GC.Collect();
					if (this.SwordProject_Found){
						this.Diatheke.autoupdate = false;
						this.Diatheke.key = Sermon_BibleKey.Text;
						this.Diatheke.autoupdate = true;
					}
				}

				private void Sermon_BookList_SelectedIndexChanged(object sender, System.EventArgs e) {
					this.Sermon_BibleKey.Text = Sermon_BookList.SelectedItem.ToString();
				}

				private void Sermon_Books_SelectedIndexChanged(object sender, System.EventArgs e) {
					if (this.SwordProject_Found){
//						this.Diatheke.book = Sermon_Books.Items[Sermon_Books.SelectedIndex].ToString();
					}
				}

				private void Sermon_BeamBox_Button_Click(object sender, System.EventArgs e) {

					if(ToolBars_MainToolbar_ShowBeamBox.Checked == false){
						ToolBars_MainToolbar_ShowBeamBox.Checked = true;
						ShowBeam.Show();
					}

					if(Sermon_DocManager.TabStrips.Count > 0){
						ShowBeam.Sermon.Text[1] = this.Sermon_DocManager.FocusedDocument.Control.Text;
						ShowBeam.Sermon.posX[1] = 200;
						ShowBeam.Sermon.posY[1] = 200;
//						ShowBeam.Sermon.TextEffect[1] = "Filled Outline";
//						ShowBeam.Sermon.OutlineColor[1] = Color.Black;

						ShowBeam.PaintSermon();
					}
				}


				private bool Check_SwordProject(){
					string strSwordConfDir = System.IO.Directory.GetDirectoryRoot(System.IO.Directory.GetCurrentDirectory()) +"etc";
					string strSwordConfPath = strSwordConfDir +"\\sword.conf";

					bool found = false;
					bool CreateConf = false;

					//if /etc directory not found, but config.swordpath is right, then create the /etc directory

					if (System.IO.File.Exists(strSwordConfPath)==false && System.IO.File.Exists(this.Config.SwordPath+"sword.exe")){

							if  (System.IO.Directory.Exists(strSwordConfDir)==false){
								Directory.CreateDirectory(strSwordConfDir);
							}
							StreamWriter OutputFile;
							OutputFile = File.CreateText(strSwordConfPath);

							OutputFile.WriteLine("[Install]");
							OutputFile.WriteLine("DataPath=" + this.Config.SwordPath.Replace("\\","/"));
							OutputFile.Close();
							CreateConf = true;
							found = true;
					}

					//if /etc/sword.conf already exists check if it's configuration is correct.
					 if (System.IO.File.Exists(strSwordConfPath) && CreateConf == false){
						StreamWriter OutputFile;
						OutputFile = File.CreateText(strSwordConfPath + ".new");
						StreamReader InputFile = File.OpenText(strSwordConfPath);
						string input = null;
						while ((input = InputFile.ReadLine()) != null){
							if(input.Substring(0,8) == "DataPath"){
								if(System.IO.File.Exists(input.Substring(9,input.Length-9).Replace("/","\\")+"sword.exe")){
									this.Config.SwordPath = input.Substring(9,input.Length-9).Replace("/","\\");
									 found = true;
									//OutputFile.WriteLine(input);
								}

								 if(System.IO.File.Exists(this.Config.SwordPath+"sword.exe")){
									//OutputFile.WriteLine("DataPath=" + this.Config.SwordPath.Replace("\\","/"));
									input = "DataPath=" + this.Config.SwordPath.Replace("\\","/");
									found = true;
								}
							}
							OutputFile.WriteLine(input);

						}

						InputFile.Close();
						OutputFile.Close();

						// Rename the Temp Sword Config File to the original Sword Config File
						System.IO.File.Delete(strSwordConfPath);
						System.IO.File.Move(strSwordConfPath+".new",strSwordConfPath);
					}

					if (found){
						return true;
					}else {
						return false;
					}
					return false;
				}

				private void Sermon_BibleKey_KeyDown1(object sender, System.Windows.Forms.KeyEventArgs e) {
					if (e.KeyValue == 13){
						this.Diatheke.book = Sermon_Books.Items[Sermon_Books.SelectedIndex].ToString();
						Diatheke.query();
					 }
				}


			#endregion

			#region ContextMenu Things
				private void ImageContextItemManage_Click(object sender, System.EventArgs e)
				{
					System.Diagnostics.Process.Start("explorer"," "+System.IO.Directory.GetCurrentDirectory()+"\\Backgrounds");
				}

				private void ImageContextItemReload_Click(object sender, System.EventArgs e)
				{
					this.ListDirectories(@"Backgrounds\");
					this.ListImages(@"Backgrounds\");
				}
			#endregion

		private void linkLabel1_Click(object sender, System.EventArgs e) {
			System.Diagnostics.Process.Start("http://www.crosswire.org/sword/software/index.jsp");
		}






		#endregion

		private void MainForm_SizeChanged(object sender, System.EventArgs e) {
		int tmpWidth = 0;
		if(this.selectedTab==0){
			tmpWidth = this.SongShow_Right_Panel.Size.Width;
		}else if(this.selectedTab==1){
			tmpWidth = this.SongEdit_RightPanel.Size.Width;
		}

			this.SongShow_Preview_Panel.Size = new System.Drawing.Size(tmpWidth, (int)System.Math.Round(tmpWidth*0.75));
//			this.SongEdit_Preview_Panel.Size = tmpSize;
		}

		private void timer1_Tick(object sender, System.EventArgs e) {

			if(this.UpdatePreview & this.TextTyped == false){
				LogFile.LogNew ("Drawing Preview Panel from: timer1_Tick");
				 Draw_Song_Preview_Image();
				this.UpdatePreview = false;
			}
		}

		private void TextTypedTimer_Tick(object sender, System.EventArgs e) {
			this.TextTyped = false;
		}



		private void RightDocks_BottomPanel_Media_FolderSelectButton_Click(object sender, System.EventArgs e)
		{
						string myPath;

//			BrowseForFolderClass myFolderBrowser = new BrowseForFolderClass();
//			myPath = myFolderBrowser.BrowseForFolder("Please, select a folder");


		}





		#region FileBrowser
				/// <summary> method treeView1_BeforeSelect
		/// <para>Before we select a tree node we want to make sure that we scan the soon to be selected
		/// tree node for any sub-folders.  this insures proper tree construction on the fly.</para>
		/// <param name="sender">The object that invoked this event</param>
		/// <param name="e">The TreeViewCancelEventArgs event arguments.</param>
		/// <see cref="System.Windows.Forms.TreeViewCancelEventArgs"/>
		/// <see cref="System.Windows.Forms.TreeView"/>
		/// </summary>
		private void treeView1_BeforeSelect(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
		{
			getSubDirs(e.Node);					// get the sub-folders for the selected node.
//			textBox1.Text = fixPath(e.Node);	// update the user selection text box.
			ListFiles(fixPath(e.Node));
			strMediaPath = fixPath(e.Node);
			folder = new DirectoryInfo(e.Node.FullPath);	// get it's Directory info.
		}

		/// <summary> method treeView1_BeforeExpand
		/// <para>Before we expand a tree node we want to make sure that we scan the soon to be expanded
		/// tree node for any sub-folders.  this insures proper tree construction on the fly.</para>
		/// <param name="sender">The object that invoked this event</param>
		/// <param name="e">The TreeViewCancelEventArgs event arguments.</param>
		/// <see cref="System.Windows.Forms.TreeViewCancelEventArgs"/>
		/// <see cref="System.Windows.Forms.TreeView"/>
		/// </summary>
		private void treeView1_BeforeExpand(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
		{
			getSubDirs(e.Node);					// get the sub-folders for the selected node.
//			textBox1.Text = fixPath(e.Node);	// update the user selection text box.
			folder = new DirectoryInfo(e.Node.FullPath);	// get it's Directory info.
		}

			/// <summary> method fillTree
		/// <para>This method is used to initially fill the treeView control with a list
		/// of available drives from which you can search for the desired folder.</para>
		/// </summary>
		private void fillTree()
		{
			DirectoryInfo directory;
			string sCurPath = "";

			// clear out the old values
			treeView1.Nodes.Clear();

			// loop through the drive letters and find the available drives.
			foreach( char c in driveLetters )
			{
				sCurPath = c + ":\\";
				try
				{
					// get the directory informaiton for this path.
					directory = new DirectoryInfo(sCurPath);

					// if the retrieved directory information points to a valid
					// directory or drive in this case, add it to the root of the
					// treeView.
					if ( directory.Exists == true )
					{
						TreeNode newNode = new TreeNode(directory.FullName);
						treeView1.Nodes.Add(newNode);	// add the new node to the root level.
						getSubDirs(newNode);			// scan for any sub folders on this drive.
					}
				}
				catch( Exception doh)
				{
					Console.WriteLine(doh.Message);
				}
			}
		}

		private void ListFiles(string directory){

			if(System.IO.Directory.Exists(directory)){
							 Presentation_Fade_ListView.Clear();
							int intImageCount=0;

							// Define Directory and ImageTypes
							string strImageDir = directory;
							string [] filetypes = {"*.bmp", "*.jpg", "*.png","*.gif","*.jpeg"};
							int filecount = 0;

							//find the number of Files in Directory
							foreach (string filetype in filetypes){
									string[] dirs = Directory.GetFiles(@strImageDir, filetype);
									foreach (string dir in dirs)
									{
										filecount++;
									}
							}
							int i_file = 1;
							//find all files from defined FileTypes
							foreach (string filetype in filetypes){
									string[] dirs = Directory.GetFiles(@strImageDir, filetype);
									foreach (string dir in dirs)
									{
										LogFile.Log(" -> Listing Files"+dir);
										// Add to ImageList
//										this.RightDocks_imageList.Images.Add(ShowBeam.Resizer(dir,80,60));
//										Controls.Development.ImageListBoxItem x = new Controls.Development.ImageListBoxItem(Path.GetFileName(dir),intImageCount);
										// Add to ImageListBox
									   //	this.RightDocks_ImageListBox.Items.Add(x);
 									   Presentation_Fade_ListView.Items.Add(Path.GetFileName(dir),0);
										intImageCount++;
										i_file++;
										//										+Convert.ToString(System.Math.Round((double)(intImageCount/filecount*100)))+"%";
									}
							}
						}
			}


		/// <summary> method getSubDirs
		/// <para>this function will scan the specified parent for any subfolders
		/// if they exist.  To minimize the memory usage, we only scan a single
		/// folder level down from the existing, and only if it is needed.</para>
		/// <param name="parent">the parent folder in which to search for sub-folders.</param>
		/// </summary>
		private void getSubDirs( TreeNode parent )
		{
			DirectoryInfo directory;
			try
			{
				// if we have not scanned this folder before
				if ( parent.Nodes.Count == 0 )
				{
					directory = new DirectoryInfo(parent.FullPath);
					foreach( DirectoryInfo dir in directory.GetDirectories())
					{
						TreeNode newNode = new TreeNode(dir.Name);
						parent.Nodes.Add(newNode);
					}
				}

				// now that we have the children of the parent, see if they
				// have any child members that need to be scanned.  Scanning 
				// the first level of sub folders insures that you properly 
				// see the '+' or '-' expanding controls on each node that represents
				// a sub folder with it's own children.
				foreach(TreeNode node in parent.Nodes)
				{
					// if we have not scanned this node before.
					if (node.Nodes.Count == 0)
					{
						// get the folder information for the specified path.
						directory = new DirectoryInfo(node.FullPath);

						// check this folder for any possible sub-folders
						foreach( DirectoryInfo dir in directory.GetDirectories())
						{
							// make a new TreeNode and add it to the treeView.
							TreeNode newNode = new TreeNode(dir.Name);
							node.Nodes.Add(newNode);
						}
					}
				}
			}
			catch( Exception doh )
			{
				Console.WriteLine(doh.Message);
			}
		}

		/// <summary> method fixPath
		/// <para>For some reason, the treeView will only work with paths constructed like the following example.
		/// "c:\\Program Files\Microsoft\...".  What this method does is strip the leading "\\" next to the drive
		/// letter.</para>
		/// <param name="node">the folder that needs it's path fixed for display.</param>
		/// <returns>The correctly formatted full path to the selected folder.</returns>
		/// </summary>
		private string fixPath( TreeNode node )
		{
			string sRet = "";
			try
			{
				sRet = node.FullPath;
				int index = sRet.IndexOf("\\\\");
				if (index > 1 )
				{
					sRet = node.FullPath.Remove(index, 1);
				}
			}
			catch( Exception doh )
			{
				Console.WriteLine(doh.Message);
			}
			return sRet;
		}
		#endregion

		private void RightDocks_BottomPanel_Media_FadePanelButton_Click(object sender, System.EventArgs e)
		{
		  if (Presentation_FadePanel.Size.Width == 510){
			 for (int i = 510; i>=0;i--){
					Presentation_FadePanel.Size = new System.Drawing.Size(i,Presentation_FadePanel.Size.Height);
			 }
		  }

		  else if (Presentation_FadePanel.Size.Width == 0){
			 for (int i = 0; i<=510;i++){
					Presentation_FadePanel.Size = new System.Drawing.Size(i,Presentation_FadePanel.Size.Height);
			 }
		  }

		}

		private void Presentation_Fade_ListView_Click(object sender, System.EventArgs e){
			ListViewItem activeItem = new ListViewItem();
			activeItem = Presentation_Fade_ListView.SelectedItems[0];

			if(strMediaPath.Length >0){
				Presentation_Fade_preview.Image = Image.FromFile(strMediaPath+"\\"+ activeItem.Text);
			}
		}



		#region DragDrop
			private void Presentation_Fade_ListView_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
			{
				// Get the index of the item the mouse is below.

				indexOfItemUnderMouseToDrag = Presentation_Fade_ListView.GetItemAt(e.X, e.Y).Index;
				//				indexOfItemUnderMouseToDrag = Presentation_Fade_ListView.IndexFromPoint(e.X, e.Y);

				if (indexOfItemUnderMouseToDrag != ListBox.NoMatches) {

					// Remember the point where the mouse down occurred. The DragSize indicates
					// the size that the mouse can move before a drag event should be started.
					Size dragSize = SystemInformation.DragSize;

					// Create a rectangle using the DragSize, with the mouse position being
					// at the center of the rectangle.
					dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width /2),
															   e.Y - (dragSize.Height /2)), dragSize);
				} else
					// Reset the rectangle if the mouse is not over an item in the ListBox.
					dragBoxFromMouseDown = Rectangle.Empty;
			}


			private void Presentation_Fade_ListView_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
			{
				// Reset the drag rectangle when the mouse button is raised.
				dragBoxFromMouseDown = Rectangle.Empty;
			}


			private void Presentation_Fade_ListView_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
			{

				if ((e.Button & MouseButtons.Left) == MouseButtons.Left) {

					// If the mouse moves outside the rectangle, start the drag.
					if (dragBoxFromMouseDown != Rectangle.Empty &&
						!dragBoxFromMouseDown.Contains(e.X, e.Y)) {

						// Create custom cursors for the drag-and-drop operation.
						try {
							MyNormalCursor = new Cursor("3dwarro.cur");
							MyNoDropCursor = new Cursor("3dwno.cur");

						} catch {
						 // An error occurred while attempting to load the cursors, so use
						  // standard cursors.
						  //UseCustomCursorsCheck.Checked = false;
					  }finally {

						   // The screenOffset is used to account for any desktop bands
							// that may be at the top or left side of the screen when
						   // determining when to cancel the drag drop operation.
						 screenOffset = SystemInformation.WorkingArea.Location;

							// Proceed with the drag and drop, passing in the list item.
						 DragDropEffects dropEffect = Presentation_Fade_ListView.DoDragDrop(Presentation_Fade_ListView.Items[indexOfItemUnderMouseToDrag], DragDropEffects.All | DragDropEffects.Link);

						  // If the drag operation was a move then remove the item.
						  if (dropEffect == DragDropEffects.Move) {
							Presentation_Fade_ListView.Items.RemoveAt(indexOfItemUnderMouseToDrag);

							// Selects the previous item in the list as long as the list has an item.
							if (indexOfItemUnderMouseToDrag > 0) {}
//								Presentation_Fade_ListView.SelectedIndex = indexOfItemUnderMouseToDrag -1;

//								  Presentation_Fade_ListView.SelectedIndex = 1;}
								  //indexOfItemUnderMouseToDrag -1;
							else if (Presentation_Fade_ListView.Items.Count > 0){}
								// Selects the first item.
//								Presentation_Fade_ListView.SelectedIndex =0;
						  }

							// Dispose of the cursors since they are no longer needed.
							if (MyNormalCursor != null)
								MyNormalCursor.Dispose();

							if (MyNoDropCursor != null)
								MyNoDropCursor.Dispose();
						}
					}
				}
			}

		private void RightDocks_BottomPanel_MediaListView_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
		{
			 // Determine whether string data exists in the drop data. If not, then
			// the drop effect reflects that the drop cannot occur.
			if (!e.Data.GetDataPresent(typeof(System.String))) {

				e.Effect = DragDropEffects.None;

				return;
			}

			// Set the effect based upon the KeyState.
			if ((e.KeyState & (8+32)) == (8+32) &&
				(e.AllowedEffect & DragDropEffects.Link) == DragDropEffects.Link) {
				// KeyState 8 + 32 = CTL + ALT

				// Link drag and drop effect.
				e.Effect = DragDropEffects.Link;

			} else if ((e.KeyState & 32) == 32 &&
				(e.AllowedEffect & DragDropEffects.Link) == DragDropEffects.Link) {

				// ALT KeyState for link.
				e.Effect = DragDropEffects.Link;

			} else if ((e.KeyState & 4) == 4 &&
				(e.AllowedEffect & DragDropEffects.Move) == DragDropEffects.Move) {

				// SHIFT KeyState for move.
				e.Effect = DragDropEffects.Move;

			} else if ((e.KeyState & 8) == 8 &&
				(e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy) {

				// CTL KeyState for copy.
				e.Effect = DragDropEffects.Copy;

			} else if ((e.AllowedEffect & DragDropEffects.Move) == DragDropEffects.Move)  {

				// By default, the drop action should be move, if allowed.
				e.Effect = DragDropEffects.Move;

			} else
				e.Effect = DragDropEffects.None;

			// Get the index of the item the mouse is below.

			// The mouse locations are relative to the screen, so they must be
			// converted to client coordinates.

		   /*	indexOfItemUnderMouseToDrop =
				RightDocks_BottomPanel_MediaListView.IndexFromPoint(RightDocks_BottomPanel_MediaListView.PointToClient(new Point(e.X, e.Y)));*/

		}
		
		private void RightDocks_BottomPanel_MediaListView_QueryContinueDrag(object sender, System.Windows.Forms.QueryContinueDragEventArgs e)
		{
          // Cancel the drag if the mouse moves off the form.
            ListView lb = sender as ListView;

            if (lb != null) {

                Form f = lb.FindForm();

                // Cancel the drag if the mouse moves off the form. The screenOffset
                // takes into account any desktop bands that may be at the top or left
                // side of the screen.
                if (((Control.MousePosition.X - screenOffset.X) < f.DesktopBounds.Left) ||
                    ((Control.MousePosition.X - screenOffset.X) > f.DesktopBounds.Right) ||
                    ((Control.MousePosition.Y - screenOffset.Y) < f.DesktopBounds.Top) ||
                    ((Control.MousePosition.Y - screenOffset.Y) > f.DesktopBounds.Bottom)) {

                    e.Action = DragAction.Cancel;
                }
            }
		}





		#endregion





	}
}
