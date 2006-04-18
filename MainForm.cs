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

Howto Get the Right ToolBars Large:
Search for:
this.Controls.Add(this.rightSandDock);
and move it below this.Controls.Add(this.ToolBars_topSandBarDock);

*/

using System;
using System.IO;
using System.Threading;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Reflection;
using System.Globalization;
using System.Xml;
using Rilling.Common.UI.Forms;
using Microsoft.DirectX;
using System.Text;
using System.Text.RegularExpressions;
//using Microsoft.DirectX.Direct3D;

namespace DreamBeam {

/// <summary>
/// The Main DreamBeam Window with most of it's functions
/// </summary>
public class MainForm : System.Windows.Forms.Form {

	#region Var and Obj Declarations

	#region Own Vars and Object
		public GuiTools GuiTools = null;

		public string version = "0.49";
		public Song[] SongArray = new Song[10];
		private Bitmap memoryBitmap = null;
		private ShowBeam ShowBeam = new ShowBeam();
		private Song Song = new Song();
		public Options Options = new Options();
		public Config Config = new Config();
		public int selectedTab = 0;
		public int SongCount = 0;
		public int Song_Edit_Box = 2;
		public bool beamshowed =false;

		private Splash Splash = null;
		private bool UpdatePreview = false;
		private bool TextTyped = false;
		private bool DrawingPreview = false;
		public bool LoadingBGThumbs = false;

		private bool AllowPreview = true;
		static Thread Thread_BgImage = null;
		static Thread Thread_MediaList = null;
		static Thread Thread_PreviewSong = null;
		static Thread Thread_LoadMovie = null;
		private String g_Bg_Directory = null;

		public DirectoryInfo folder;

		private System.ComponentModel.IContainer components;
		public string strMediaPath = "";
		bool MediaPreview = false;
		private int indexOfItemUnderMouseToDrag;
		private Rectangle dragBoxFromMouseDown;
		private Point screenOffset;
		private Cursor MyNoDropCursor;
		private Cursor MyNormalCursor;
		public ImageList MediaList = new ImageList();
		string MediaFile;
		bool VideoLoaded = false;
		bool VideoProblem = false;
		bool LoadingVideo = false;
		public Microsoft.DirectX.AudioVideoPlayback.Video video = null;
		int iFilmEnded = 0;
		public Language Lang = new Language();
		public System.Drawing.Font EditorFont = null;


#endregion


	#region Toolbars and others Declarations

		private TD.SandBar.SandBarManager ToolBars_sandBarManager1;
		private TD.SandBar.ToolBarContainer ToolBars_leftSandBarDock;
		private TD.SandBar.ToolBarContainer ToolBars_bottomSandBarDock;
		public TD.SandBar.ToolBarContainer ToolBars_topSandBarDock;

		private TD.SandDock.SandDockManager sandDockManager1;
		private TD.SandDock.DockContainer leftSandDock;
		public TD.SandDock.DockContainer rightSandDock;
		private TD.SandDock.DockContainer bottomSandDock;
		private TD.SandDock.DockContainer topSandDock;

		public TD.SandDock.DockControl RightDocks_TopPanel_Songs;
		public TD.SandDock.DockControl RightDocks_BottomPanel_Backgrounds;
		public TD.SandDock.DockControl RightDocks_TopPanel_PlayList;
		public TD.SandDock.DockControl RightDocks_TopPanel_Search;

		public System.Windows.Forms.TabControl tabControl1;
		public OPaC.Themed.Forms.TabPage tabPage1;
		public OPaC.Themed.Forms.TabPage tabPage0;
		public OPaC.Themed.Forms.TabPage tabPage2;
		public OPaC.Themed.Forms.TabPage tabPage3;
		public OPaC.Themed.Forms.TabPage tabPage4;


		#region Menu Bar

			private TD.SandBar.MenuBar ToolBars_MenuBar;
			public TD.SandBar.MenuBarItem ToolBars_MenuBar_Song;
			private TD.SandBar.MenuButtonItem ToolBars_MenuBar_Song_New;
			private TD.SandBar.MenuButtonItem ToolBars_MenuBar_Song_Save;
			private TD.SandBar.MenuButtonItem ToolBars_MenuBar_Song_SaveAs;
			private TD.SandBar.MenuButtonItem ToolBars_MenuBar_Song_Rename;
			private TD.SandBar.MenuButtonItem ToolBars_MenuBar_Song_Exit;

			public TD.SandBar.MenuBarItem ToolBars_MenuBar_MediaList;
			private TD.SandBar.MenuButtonItem ToolBars_MenuBar_MediaList_New;
			private TD.SandBar.MenuButtonItem ToolBars_MenuBar_Media_Save;
			private TD.SandBar.MenuButtonItem ToolBars_MenuBar_Media_SaveAs;
			private TD.SandBar.MenuButtonItem ToolBars_MenuBar_Media_Rename;
			private TD.SandBar.MenuButtonItem ToolBars_MenuBar_MediaList_Exit;

			public TD.SandBar.MenuBarItem ToolBars_MenuBar_Edit;
			private TD.SandBar.MenuButtonItem ToolBars_MenuBar_Edit_Options;
			public TD.SandBar.MenuBarItem ToolBars_MenuBar_View;
			private TD.SandBar.MenuBarItem ToolBars_MenuBar_Help;
			private TD.SandBar.MenuButtonItem HelpIntro;
			private TD.SandBar.MenuButtonItem HelpBeamBox;
			private TD.SandBar.MenuButtonItem HelpShowSongs;
			private TD.SandBar.MenuButtonItem HelpEditSongs;
			private TD.SandBar.MenuButtonItem HelpTextTool;
			private TD.SandBar.MenuButtonItem AboutButton;
		#endregion

		#region Main Toolbar

			public TD.SandBar.ToolBar ToolBars_MainToolbar;
			public TD.SandBar.ButtonItem ToolBars_MainToolbar_ShowBeamBox;
			public TD.SandBar.ButtonItem ToolBars_MainToolbar_SizePosition;
			public TD.SandBar.ButtonItem ToolBars_MainToolbar_SaveSong;
			public TD.SandBar.ButtonItem ToolBars_MainToolbar_SaveMediaList;
			public TD.SandBar.ButtonItem ToolBars_MainToolbar_HideBG;
			public TD.SandBar.ButtonItem ToolBars_MainToolbar_HideText;

		#endregion

		#region Component Bar
			public TD.SandBar.ToolBar ToolBars_ComponentBar;
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
			#endregion

			#region Media List

				private System.Windows.Forms.Panel RightDocks_SongList_ButtonPanel;
				private System.Windows.Forms.Panel RightDocks_Songlist_SearchPanel;
				private System.Windows.Forms.Panel RightDocks_BottomPanel2_TopPanel;

				public System.Windows.Forms.Panel RightDocks_BottomPanel_Media_Bottom;
				private System.Windows.Forms.Button RightDocks_BottomPanel_Media_ShowNext;
				private System.Windows.Forms.Button RightDocks_BottomPanel_Media_Down;
				private System.Windows.Forms.Button RightDocks_BottomPanel_Media_Up;
				private System.Windows.Forms.Button RightDocks_BottomPanel_Media_Remove;

				private System.Windows.Forms.Timer Presentation_AutoPlayTimer;
				private System.Windows.Forms.Button RightDocks_BottomPanel_Media_FadePanelButton;
				public Controls.Development.ImageListBox RightDocks_BottomPanel_MediaList;
				private System.Windows.Forms.Panel RightDocks_TopPanel_PlayList_Button_Panel;
				private System.Windows.Forms.Panel RightDocks_BottomPanel_Media_Top;
				public TD.SandDock.DockControl RightDocks_BottomPanel_MediaLists;
				private System.Windows.Forms.Panel RightDocks_BottomPanel_MediaLists_BottomPanel;
				private System.Windows.Forms.Panel RightDocks_BottomPanel_MediaListsTopPanel;
				private System.Windows.Forms.GroupBox RightDocks_BottomPanel_MediaListsBottomPanel_GroupBox;
				private System.Windows.Forms.Label RightDocks_BottomPanel_MediaList_BottomPanel_Label;
				private System.Windows.Forms.NumericUpDown RightDocks_BottomPanel_MediaLists_Numeric;
				private System.Windows.Forms.Label RightDocks_BottomPanel_MediaLists_BottomPanel_Label2;
				private System.Windows.Forms.Button RightDocks_BottomPanel_Media_AutoPlay;
				private System.Windows.Forms.CheckBox RightDocks_BottomPanel_MediaLists_LoopCheckBox;
				private System.Windows.Forms.Panel RightDocks_BottomPanel_MediaListsTop_Control_Panel;
				private System.Windows.Forms.ListBox RightDocks_MediaLists;
				private System.Windows.Forms.Button RightDocks_MediaLists_LoadButton;
				private System.Windows.Forms.Button RightDocks_MediaLists_DeleteButton;
				private System.Windows.Forms.Button RightDocks_Search_SearchButton;
		#endregion

			#region ImageBox
				public Controls.Development.ImageListBox RightDocks_ImageListBox;
				public System.Windows.Forms.ImageList RightDocks_imageList;

				// Context Menu
				private System.Windows.Forms.ContextMenu ImageContext;
				private System.Windows.Forms.MenuItem ImageContextItemManage;
				private System.Windows.Forms.MenuItem ImageContextItemReload;
				public System.Windows.Forms.ComboBox RightDocks_FolderDropdown;
			#endregion
		#endregion

		private System.Windows.Forms.StatusBar statusBar;
		public System.Windows.Forms.StatusBarPanel StatusPanel;

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
		public System.Windows.Forms.Panel SongEdit_RightPanel;
		private System.Windows.Forms.Panel SongEdit_InputFieldMenuPanelMid;
		private System.Windows.Forms.Panel SongEdit_InputFieldBelowMenuPanelMid;
		private System.Windows.Forms.Panel SongEdit_InputFieldMenuPanelButtom;
		private System.Windows.Forms.Panel SongEdit_InputFieldBelowMenuPanelButtom;
		private System.Windows.Forms.Panel SongEdit_InputFieldBelowMenuPane2lMid;


		//Preview Panel
		public System.Windows.Forms.PictureBox SongEdit_Preview_Panel;

		//The ToolBars
		private TD.SandBar.ToolBar SongEdit_TopTextToolBar;
		private TD.SandBar.ToolBar SongEdit_MidTextToolBar;
		private TD.SandBar.ToolBar SongEdit_BottomTextToolBar;

		// The TextBoxes
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
		private System.Windows.Forms.Button SongEdit_PreviewStropheUp_Button;
		private System.Windows.Forms.Button SongEdit_PreviewStropheDown_Button;
		private System.Windows.Forms.Label SongEdit_BG_DecscriptionLabel;
		public System.Windows.Forms.Label SongEdit_BG_Label;
		private System.Windows.Forms.Button SongEdit_UpdateBeamBox_Button;

		//other Stuff
		public System.Windows.Forms.Timer PreviewUpdateTimer;
		private System.Windows.Forms.Timer TextTypedTimer;

		// link to Sword Project
		private System.Windows.Forms.LinkLabel linkLabel1;

		//CollapsPanels and their Buttons
		private Salamander.Windows.Forms.CollapsiblePanel SongEdit_TextAlignPanel;
		private Salamander.Windows.Forms.CollapsiblePanel SongEdit_BackgroundPanel;
		private ctlLEDRadioButton.LEDradioButton SongEdit_AlignLeft_Button;
		private ctlLEDRadioButton.LEDradioButton SongEdit_AlignCenter_Button;
		private ctlLEDRadioButton.LEDradioButton SongEdit_AlignRight_Button;
		private ctlLEDRadioButton.LEDradioButton SongEdit_ML_Button;

	#endregion

	#region Show Songs Declarations


		//Right Panel
		public System.Windows.Forms.Panel SongShow_Right_Panel;
//		private System.Windows.Forms.Panel SongShow_Preview_Panel;
		public System.Windows.Forms.PictureBox SongShow_Preview_Panel;
		public OPaC.Themed.Forms.Label SongShow_BG_Label;

		private System.Windows.Forms.Button SongShow_ToBeamBox_button;

//		private bool HideTitle = false;
//		private bool HideText = false;
//		private bool HideAuthor = false;
	#endregion

	public TD.SandDock.DockControl RightDocks_BottomPanel_Media;

	#region Presentation

		private System.Windows.Forms.ImageList imageList_Folders;
		public System.Windows.Forms.TreeView treeView1;
		public System.Windows.Forms.PictureBox Presentation_Fade_preview;
		private System.Windows.Forms.Button Presentation_Fade_ToPlaylist_Button;
		private System.Windows.Forms.ImageList Presentation_Fade_ImageList;
		public System.Windows.Forms.ListView Presentation_Fade_ListView;
		private System.Windows.Forms.Panel Fade_panel;
		private TD.SandBar.ButtonItem Presentation_Button;
		private System.Windows.Forms.Panel Presentation_FadePanel;
		public System.Windows.Forms.ImageList Media_ImageList;
		private System.Windows.Forms.Panel Fade_Top_Panel;
		private System.Windows.Forms.Panel Presentation_MainPanel;
		public System.Windows.Forms.PictureBox Presentation_PreviewBox;
		private AxShockwaveFlashObjects.AxShockwaveFlash axShockwaveFlash;
		private System.Windows.Forms.Panel Presentation_PreviewPanel;
		private System.Windows.Forms.Panel Presentation_MovieControlPanel;
		private System.Windows.Forms.TrackBar Media_TrackBar;
		private System.Windows.Forms.Button Presentation_MoviePreviewButton;
		public System.Windows.Forms.ImageList Media_Logos;
		private System.Windows.Forms.Timer PlayProgress;
		private System.Windows.Forms.Panel Presentation_MovieControlPanel_Right;
		private System.Windows.Forms.Panel Presentation_MovieControlPanelBottomLeft;
		private System.Windows.Forms.ToolBar Presentation_PlayBar;
		private System.Windows.Forms.ImageList PlayButtons_ImageList;
		private System.Windows.Forms.ToolBarButton Presentation_PlayButton;
		private System.Windows.Forms.ToolBarButton Presentation_PauseButton;
		private System.Windows.Forms.ToolBarButton Presentation_StopButton;
		private System.Windows.Forms.Panel Presentation_MovieControlPanel_Top;
		private System.Windows.Forms.Panel Presentation_MovieControlPanelBottom;
		private System.Windows.Forms.TrackBar AudioBar;
		private System.Windows.Forms.Panel Presentation_MovieControl_PreviewButtonPanel;
		private System.Windows.Forms.CheckBox Presentation_MediaLoop_Checkbox;
		public System.Windows.Forms.Panel Presentation_VideoPanel;
		private System.Windows.Forms.Timer VideoLoadTimer;
	#endregion

	#region SermonTools

		string[] BibleBooks = new string[2];
		private AxACTIVEDIATHEKELib.AxActiveDiatheke Diatheke;
		private TD.SandBar.ButtonItem Sermon_Button;
		public DocumentManager.DocumentManager Sermon_DocManager;
		private System.Windows.Forms.TabControl Sermon_TabControl;
		private string Sermon_BibleLang = "en";
		private bool Sermon_ShowBibleTranslation = false;
		private System.Windows.Forms.Button Sermon_BeamBox_Button;
		private System.Windows.Forms.Label Sermon_Books_Label;
		private System.Windows.Forms.Label Sermon_Translation_Label;
		private System.Windows.Forms.Label Sermon_Verse_Label;
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

    private System.Windows.Forms.Button Presentation_Fade_Refresh_Button;
    private TD.SandBar.MenuButtonItem HelpPresentation;
    private TD.SandBar.MenuButtonItem HelpComponents;
    private TD.SandBar.MenuButtonItem HelpGetToKnow;
    private Salamander.Windows.Forms.CollapsiblePanelBar SongShow_CollapsPanel;
	private Salamander.Windows.Forms.CollapsiblePanel SongShow_BackgroundPanel;
    private Salamander.Windows.Forms.CollapsiblePanel SongShow_HideElementsPanel;
	private ctlLEDRadioButton.LEDradioButton SongShow_HideTitle_Button;
	private ctlLEDRadioButton.LEDradioButton SongShow_HideText_Button;
	private ctlLEDRadioButton.LEDradioButton SongShow_HideAuthor_Button;
	private ctlLEDRadioButton.LEDradioButton SongShow_OverwriteBG_Button;
	private System.Windows.Forms.Panel SongShow_HideElementsSub1Panel;


	private System.Windows.Forms.Panel panel1;
	private System.Windows.Forms.Panel panel3;

	private Salamander.Windows.Forms.CollapsiblePanelBar collapsiblePanelBar1;

	private System.Windows.Forms.Panel panel2;
	private System.Windows.Forms.Panel panel4;
	public System.Windows.Forms.RichTextBox SongEdit_Mid_TextBox;
	private Salamander.Windows.Forms.CollapsiblePanel SongEdit_MultiLangPanel;
	private TD.SandBar.ToolBar SongEdit_LangTextToolBar;

		private TD.SandBar.ButtonItem SongEdit_ButtonLangOutlineColor;
		private TD.SandBar.ButtonItem SongEdit_ButtonLangFont;
		private TD.SandBar.ButtonItem SongEdit_ButtonLangColor;
		private TD.SandBar.ButtonItem SongEdit_ButtonLangTextOutline;

		private TD.SandBar.MenuButtonItem ToolBars_MenuBar_View_ShowSongs;
		private TD.SandBar.MenuButtonItem ToolBars_MenuBar_View_EditSongs;
		private TD.SandBar.MenuButtonItem ToolBars_MenuBar_View_Presentation;
		private TD.SandBar.MenuButtonItem ToolBars_MenuBar_View_TextTool;
		public System.Windows.Forms.ProgressBar RenderStatus;
	public Lister.ListEx SongShow_StropheList_ListEx;
	public TD.SandBar.MenuBarItem ToolBars_MenuBar_Sermon;
	private TD.SandBar.MenuButtonItem ToolBars_MenuBar_Sermon_Exit;
	private TD.SandBar.ButtonItem buttonItem1;
	private System.Windows.Forms.StatusBarPanel statusBarUpdatePanel;

	#region BibleText controls
	private TD.SandBar.ButtonItem BibleText_Button;
	public System.Windows.Forms.TabPage BibleText_Tab;
	private System.Windows.Forms.Panel BibleText_Tab_Controls_Panel;
	private System.Windows.Forms.ListBox BibleText_Translations;
	private System.Windows.Forms.RichTextBox BibleText_Results;
	private System.Windows.Forms.ComboBox BibleText_RegEx_ComboBox;
	private System.Windows.Forms.Panel BibleText_Results_Panel;
	private Salamander.Windows.Forms.CollapsiblePanel BibleText_Recent_Verses;
	#endregion




	#region MAIN

	///<summary> Initialise DreamBeam </summary>
	public MainForm() {
		//
		// Erforderlich für die Unterstützung des Windows Forms-Designer
		//
		ShowBeam.LogFile = new LogFile();
		ShowBeam.LogFile.doLog = false;
		ShowBeam.LogFile.BigHeader("Start");
		ShowBeam.Config = this.Config;
		ShowBeam._MainForm = this; 
		GuiTools = new GuiTools(this,this.ShowBeam);

		this.Hide();
		this.SwordProject_Found = this.Check_SwordProject();
		Splash.ShowSplashScreen();
		Splash.SetStatus("Initializing");
		InitializeComponent();
		Splash.SetStatus("Checking for Sword Project");
		InitializeDiatheke();

		Presentation_FadePanel.Size = new System.Drawing.Size (0,Presentation_FadePanel.Size.Height);
		//Presentation_MovieControlPanel.Size =  new System.Drawing.Size (Presentation_MovieControlPanel.Size.Width,0);
		GuiTools.Presentation.fillTree();
		EditorFont = SongEdit_Mid_TextBox.Font;

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


	/// <summary>
	/// Der Haupteinsprungspunkt für die Anwendung
	/// </summary>
	[STAThread]
	static void Main() {
		Application.Run(new MainForm());
	}

	#endregion


	#region Vom Windows Form-Designer erzeugter Code
    /// <summary>
    /// Erforderliche Methode zur Unterstützung des Designers -
    /// ändern Sie die Methode nicht mit dem Quelltext-Editor.
    /// </summary>
    private void InitializeComponent() {
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
		this.RightDocks_Search_SearchButton = new System.Windows.Forms.Button();
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
		this.BibleText_Button = new TD.SandBar.ButtonItem();
		this.ToolBars_topSandBarDock = new TD.SandBar.ToolBarContainer();
		this.ToolBars_MainToolbar = new TD.SandBar.ToolBar();
		this.ToolBars_MainToolbar_ShowBeamBox = new TD.SandBar.ButtonItem();
		this.ToolBars_MainToolbar_SizePosition = new TD.SandBar.ButtonItem();
		this.ToolBars_MainToolbar_HideBG = new TD.SandBar.ButtonItem();
		this.ToolBars_MainToolbar_HideText = new TD.SandBar.ButtonItem();
		this.ToolBars_MainToolbar_SaveMediaList = new TD.SandBar.ButtonItem();
		this.ToolBars_MainToolbar_SaveSong = new TD.SandBar.ButtonItem();
		this.ToolBars_MenuBar = new TD.SandBar.MenuBar();
		this.ToolBars_MenuBar_Song = new TD.SandBar.MenuBarItem();
		this.ToolBars_MenuBar_Song_New = new TD.SandBar.MenuButtonItem();
		this.ToolBars_MenuBar_Song_Save = new TD.SandBar.MenuButtonItem();
		this.ToolBars_MenuBar_Song_SaveAs = new TD.SandBar.MenuButtonItem();
		this.ToolBars_MenuBar_Song_Rename = new TD.SandBar.MenuButtonItem();
		this.ToolBars_MenuBar_Song_Exit = new TD.SandBar.MenuButtonItem();
		this.ToolBars_MenuBar_MediaList = new TD.SandBar.MenuBarItem();
		this.ToolBars_MenuBar_MediaList_New = new TD.SandBar.MenuButtonItem();
		this.ToolBars_MenuBar_Media_Save = new TD.SandBar.MenuButtonItem();
		this.ToolBars_MenuBar_Media_SaveAs = new TD.SandBar.MenuButtonItem();
		this.ToolBars_MenuBar_Media_Rename = new TD.SandBar.MenuButtonItem();
		this.ToolBars_MenuBar_MediaList_Exit = new TD.SandBar.MenuButtonItem();
		this.ToolBars_MenuBar_Sermon = new TD.SandBar.MenuBarItem();
		this.ToolBars_MenuBar_Sermon_Exit = new TD.SandBar.MenuButtonItem();
		this.ToolBars_MenuBar_Edit = new TD.SandBar.MenuBarItem();
		this.ToolBars_MenuBar_Edit_Options = new TD.SandBar.MenuButtonItem();
		this.ToolBars_MenuBar_View = new TD.SandBar.MenuBarItem();
		this.ToolBars_MenuBar_View_ShowSongs = new TD.SandBar.MenuButtonItem();
		this.ToolBars_MenuBar_View_EditSongs = new TD.SandBar.MenuButtonItem();
		this.ToolBars_MenuBar_View_Presentation = new TD.SandBar.MenuButtonItem();
		this.ToolBars_MenuBar_View_TextTool = new TD.SandBar.MenuButtonItem();
		this.ToolBars_MenuBar_Help = new TD.SandBar.MenuBarItem();
		this.HelpGetToKnow = new TD.SandBar.MenuButtonItem();
		this.HelpIntro = new TD.SandBar.MenuButtonItem();
		this.HelpBeamBox = new TD.SandBar.MenuButtonItem();
		this.HelpOptions = new TD.SandBar.MenuButtonItem();
		this.HelpComponents = new TD.SandBar.MenuButtonItem();
		this.HelpShowSongs = new TD.SandBar.MenuButtonItem();
		this.HelpEditSongs = new TD.SandBar.MenuButtonItem();
		this.HelpPresentation = new TD.SandBar.MenuButtonItem();
		this.HelpTextTool = new TD.SandBar.MenuButtonItem();
		this.AboutButton = new TD.SandBar.MenuButtonItem();
		this.statusBar = new System.Windows.Forms.StatusBar();
		this.RenderStatus = new System.Windows.Forms.ProgressBar();
		this.StatusPanel = new System.Windows.Forms.StatusBarPanel();
		this.statusBarUpdatePanel = new System.Windows.Forms.StatusBarPanel();
		this.tabControl1 = new System.Windows.Forms.TabControl();
		this.tabPage0 = new OPaC.Themed.Forms.TabPage();
		this.SongShow_Right_Panel = new System.Windows.Forms.Panel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.SongShow_ToBeamBox_button = new System.Windows.Forms.Button();
		this.SongShow_CollapsPanel = new Salamander.Windows.Forms.CollapsiblePanelBar();
		this.SongShow_HideElementsPanel = new Salamander.Windows.Forms.CollapsiblePanel();
		this.panel3 = new System.Windows.Forms.Panel();
		this.SongShow_HideAuthor_Button = new ctlLEDRadioButton.LEDradioButton();
		this.panel1 = new System.Windows.Forms.Panel();
		this.SongShow_HideText_Button = new ctlLEDRadioButton.LEDradioButton();
		this.SongShow_HideElementsSub1Panel = new System.Windows.Forms.Panel();
		this.SongShow_HideTitle_Button = new ctlLEDRadioButton.LEDradioButton();
		this.SongShow_BackgroundPanel = new Salamander.Windows.Forms.CollapsiblePanel();
		this.SongShow_OverwriteBG_Button = new ctlLEDRadioButton.LEDradioButton();
		this.SongShow_BG_Label = new OPaC.Themed.Forms.Label();
		this.SongShow_Preview_Panel = new System.Windows.Forms.PictureBox();
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
		this.Sermon_Verse_Label = new System.Windows.Forms.Label();
		this.Sermon_Translation_Label = new System.Windows.Forms.Label();
		this.Sermon_Books_Label = new System.Windows.Forms.Label();
		this.Sermon_BibleKey = new System.Windows.Forms.TextBox();
		this.Sermon_Testament_ListBox = new System.Windows.Forms.ListBox();
		this.Sermon_Books = new System.Windows.Forms.ComboBox();
		this.Sermon_BookList = new System.Windows.Forms.ListBox();
		this.tabPage4 = new OPaC.Themed.Forms.TabPage();
		this.Presentation_FadePanel = new System.Windows.Forms.Panel();
		this.Fade_panel = new System.Windows.Forms.Panel();
		this.Presentation_Fade_ListView = new System.Windows.Forms.ListView();
		this.Presentation_Fade_ImageList = new System.Windows.Forms.ImageList(this.components);
		this.Fade_Top_Panel = new System.Windows.Forms.Panel();
		this.Presentation_Fade_Refresh_Button = new System.Windows.Forms.Button();
		this.Presentation_Fade_ToPlaylist_Button = new System.Windows.Forms.Button();
		this.Presentation_Fade_preview = new System.Windows.Forms.PictureBox();
		this.treeView1 = new System.Windows.Forms.TreeView();
		this.imageList_Folders = new System.Windows.Forms.ImageList(this.components);
		this.Presentation_MainPanel = new System.Windows.Forms.Panel();
		this.Presentation_PreviewPanel = new System.Windows.Forms.Panel();
		this.Presentation_VideoPanel = new System.Windows.Forms.Panel();
		this.axShockwaveFlash = new AxShockwaveFlashObjects.AxShockwaveFlash();
		this.Presentation_PreviewBox = new System.Windows.Forms.PictureBox();
		this.Presentation_MovieControlPanel = new System.Windows.Forms.Panel();
		this.Presentation_MovieControlPanelBottom = new System.Windows.Forms.Panel();
		this.Presentation_MediaLoop_Checkbox = new System.Windows.Forms.CheckBox();
		this.Presentation_MovieControl_PreviewButtonPanel = new System.Windows.Forms.Panel();
		this.Presentation_MoviePreviewButton = new System.Windows.Forms.Button();
		this.Presentation_MovieControlPanelBottomLeft = new System.Windows.Forms.Panel();
		this.Presentation_PlayBar = new System.Windows.Forms.ToolBar();
		this.Presentation_PlayButton = new System.Windows.Forms.ToolBarButton();
		this.Presentation_PauseButton = new System.Windows.Forms.ToolBarButton();
		this.Presentation_StopButton = new System.Windows.Forms.ToolBarButton();
		this.PlayButtons_ImageList = new System.Windows.Forms.ImageList(this.components);
		this.Presentation_MovieControlPanel_Top = new System.Windows.Forms.Panel();
		this.Media_TrackBar = new System.Windows.Forms.TrackBar();
		this.Presentation_MovieControlPanel_Right = new System.Windows.Forms.Panel();
		this.AudioBar = new System.Windows.Forms.TrackBar();
		this.tabPage1 = new OPaC.Themed.Forms.TabPage();
		this.SongEdit_RightPanel = new System.Windows.Forms.Panel();
		this.collapsiblePanelBar1 = new Salamander.Windows.Forms.CollapsiblePanelBar();
		this.SongEdit_MultiLangPanel = new Salamander.Windows.Forms.CollapsiblePanel();
		this.SongEdit_ML_Button = new ctlLEDRadioButton.LEDradioButton();
		this.SongEdit_LangTextToolBar = new TD.SandBar.ToolBar();
		this.SongEdit_ButtonLangFont = new TD.SandBar.ButtonItem();
		this.SongEdit_ButtonLangColor = new TD.SandBar.ButtonItem();
		this.SongEdit_ButtonLangTextOutline = new TD.SandBar.ButtonItem();
		this.SongEdit_ButtonLangOutlineColor = new TD.SandBar.ButtonItem();
		this.SongEdit_TextAlignPanel = new Salamander.Windows.Forms.CollapsiblePanel();
		this.SongEdit_AlignLeft_Button = new ctlLEDRadioButton.LEDradioButton();
		this.SongEdit_AlignCenter_Button = new ctlLEDRadioButton.LEDradioButton();
		this.SongEdit_AlignRight_Button = new ctlLEDRadioButton.LEDradioButton();
		this.SongEdit_BackgroundPanel = new Salamander.Windows.Forms.CollapsiblePanel();
		this.SongEdit_BG_DecscriptionLabel = new System.Windows.Forms.Label();
		this.SongEdit_Preview_Panel = new System.Windows.Forms.PictureBox();
		this.panel4 = new System.Windows.Forms.Panel();
		this.SongEdit_PreviewStropheUp_Button = new System.Windows.Forms.Button();
		this.SongEdit_PreviewStropheDown_Button = new System.Windows.Forms.Button();
		this.SongEdit_BigInputFieldPanel = new System.Windows.Forms.Panel();
		this.SongEdit_InputFieldPanelMid = new System.Windows.Forms.Panel();
		this.SongEdit_InputFieldBelowMenuPanelMid = new System.Windows.Forms.Panel();
		this.SongEdit_InputFieldBelowMenuPane2lMid = new System.Windows.Forms.Panel();
		this.SongEdit_Mid_TextBox = new System.Windows.Forms.RichTextBox();
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
		this.BibleText_Tab = new System.Windows.Forms.TabPage();
		this.BibleText_Results_Panel = new System.Windows.Forms.Panel();
		this.BibleText_Results = new System.Windows.Forms.RichTextBox();
		this.BibleText_RegEx_ComboBox = new System.Windows.Forms.ComboBox();
		this.BibleText_Tab_Controls_Panel = new System.Windows.Forms.Panel();
		this.BibleText_Recent_Verses = new Salamander.Windows.Forms.CollapsiblePanel();
		this.BibleText_Translations = new System.Windows.Forms.ListBox();
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
		this.RightDocks_BottomPanel_MediaList = new Controls.Development.ImageListBox();
		this.Media_ImageList = new System.Windows.Forms.ImageList(this.components);
		this.RightDocks_BottomPanel_Media_Bottom = new System.Windows.Forms.Panel();
		this.RightDocks_BottomPanel_Media_AutoPlay = new System.Windows.Forms.Button();
		this.RightDocks_BottomPanel_Media_Remove = new System.Windows.Forms.Button();
		this.RightDocks_BottomPanel_Media_Down = new System.Windows.Forms.Button();
		this.RightDocks_BottomPanel_Media_Up = new System.Windows.Forms.Button();
		this.RightDocks_BottomPanel_Media_ShowNext = new System.Windows.Forms.Button();
		this.RightDocks_BottomPanel_Media_Top = new System.Windows.Forms.Panel();
		this.RightDocks_BottomPanel_Media_FadePanelButton = new System.Windows.Forms.Button();
		this.RightDocks_BottomPanel_MediaLists = new TD.SandDock.DockControl();
		this.RightDocks_BottomPanel_MediaListsTopPanel = new System.Windows.Forms.Panel();
		this.RightDocks_MediaLists = new System.Windows.Forms.ListBox();
		this.RightDocks_BottomPanel_MediaListsTop_Control_Panel = new System.Windows.Forms.Panel();
		this.RightDocks_MediaLists_DeleteButton = new System.Windows.Forms.Button();
		this.RightDocks_MediaLists_LoadButton = new System.Windows.Forms.Button();
		this.RightDocks_BottomPanel_MediaLists_BottomPanel = new System.Windows.Forms.Panel();
		this.RightDocks_BottomPanel_MediaListsBottomPanel_GroupBox = new System.Windows.Forms.GroupBox();
		this.RightDocks_BottomPanel_MediaLists_LoopCheckBox = new System.Windows.Forms.CheckBox();
		this.RightDocks_BottomPanel_MediaLists_BottomPanel_Label2 = new System.Windows.Forms.Label();
		this.RightDocks_BottomPanel_MediaLists_Numeric = new System.Windows.Forms.NumericUpDown();
		this.RightDocks_BottomPanel_MediaList_BottomPanel_Label = new System.Windows.Forms.Label();
		this.bottomSandDock = new TD.SandDock.DockContainer();
		this.topSandDock = new TD.SandDock.DockContainer();
		this.Media_Logos = new System.Windows.Forms.ImageList(this.components);
		this.PlayProgress = new System.Windows.Forms.Timer(this.components);
		this.VideoLoadTimer = new System.Windows.Forms.Timer(this.components);
		this.Presentation_AutoPlayTimer = new System.Windows.Forms.Timer(this.components);
		this.buttonItem1 = new TD.SandBar.ButtonItem();
		this.RightDocks_Songlist_SearchPanel.SuspendLayout();
		this.RightDocks_SongList_ButtonPanel.SuspendLayout();
		this.RightDocks_TopPanel_PlayList_Button_Panel.SuspendLayout();
		this.RightDocks_TopPanel_Search_ButtonPanel.SuspendLayout();
		this.RightDocks_SearchBar_TopPanel.SuspendLayout();
		this.ToolBars_leftSandBarDock.SuspendLayout();
		this.ToolBars_topSandBarDock.SuspendLayout();
		this.statusBar.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)(this.StatusPanel)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.statusBarUpdatePanel)).BeginInit();
		this.tabControl1.SuspendLayout();
		this.tabPage0.SuspendLayout();
		this.SongShow_Right_Panel.SuspendLayout();
		this.panel2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)(this.SongShow_CollapsPanel)).BeginInit();
		this.SongShow_CollapsPanel.SuspendLayout();
		this.SongShow_HideElementsPanel.SuspendLayout();
		this.panel3.SuspendLayout();
		this.panel1.SuspendLayout();
		this.SongShow_HideElementsSub1Panel.SuspendLayout();
		this.SongShow_BackgroundPanel.SuspendLayout();
		this.tabPage2.SuspendLayout();
		this.Sermon_LeftPanel.SuspendLayout();
		this.Sermon_LeftBottom_Panel.SuspendLayout();
		this.Sermon_LeftDoc_Panel.SuspendLayout();
		this.Sermon_LeftToolBar_Panel.SuspendLayout();
		this.Sermon_TabControl.SuspendLayout();
		this.tabPage3.SuspendLayout();
		this.tabPage4.SuspendLayout();
		this.Presentation_FadePanel.SuspendLayout();
		this.Fade_panel.SuspendLayout();
		this.Fade_Top_Panel.SuspendLayout();
		this.Presentation_MainPanel.SuspendLayout();
		this.Presentation_PreviewPanel.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)(this.axShockwaveFlash)).BeginInit();
		this.Presentation_MovieControlPanel.SuspendLayout();
		this.Presentation_MovieControlPanelBottom.SuspendLayout();
		this.Presentation_MovieControl_PreviewButtonPanel.SuspendLayout();
		this.Presentation_MovieControlPanelBottomLeft.SuspendLayout();
		this.Presentation_MovieControlPanel_Top.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)(this.Media_TrackBar)).BeginInit();
		this.Presentation_MovieControlPanel_Right.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)(this.AudioBar)).BeginInit();
		this.tabPage1.SuspendLayout();
		this.SongEdit_RightPanel.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)(this.collapsiblePanelBar1)).BeginInit();
		this.collapsiblePanelBar1.SuspendLayout();
		this.SongEdit_MultiLangPanel.SuspendLayout();
		this.SongEdit_TextAlignPanel.SuspendLayout();
		this.SongEdit_BackgroundPanel.SuspendLayout();
		this.panel4.SuspendLayout();
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
		this.BibleText_Tab.SuspendLayout();
		this.BibleText_Results_Panel.SuspendLayout();
		this.BibleText_Tab_Controls_Panel.SuspendLayout();
		this.rightSandDock.SuspendLayout();
		this.RightDocks_TopPanel_Songs.SuspendLayout();
		this.RightDocks_TopPanel_PlayList.SuspendLayout();
		this.RightDocks_TopPanel_Search.SuspendLayout();
		this.RightDocks_BottomPanel_Backgrounds.SuspendLayout();
		this.RightDocks_BottomPanel2_TopPanel.SuspendLayout();
		this.RightDocks_BottomPanel_Media.SuspendLayout();
		this.RightDocks_BottomPanel_Media_Bottom.SuspendLayout();
		this.RightDocks_BottomPanel_Media_Top.SuspendLayout();
		this.RightDocks_BottomPanel_MediaLists.SuspendLayout();
		this.RightDocks_BottomPanel_MediaListsTopPanel.SuspendLayout();
		this.RightDocks_BottomPanel_MediaListsTop_Control_Panel.SuspendLayout();
		this.RightDocks_BottomPanel_MediaLists_BottomPanel.SuspendLayout();
		this.RightDocks_BottomPanel_MediaListsBottomPanel_GroupBox.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)(this.RightDocks_BottomPanel_MediaLists_Numeric)).BeginInit();
		this.SuspendLayout();
		// 
		// RightDocks_ImageListBox
		// 
		this.RightDocks_ImageListBox.AllowDrop = true;
		this.RightDocks_ImageListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.RightDocks_ImageListBox.ContextMenu = this.ImageContext;
		this.RightDocks_ImageListBox.Dock = System.Windows.Forms.DockStyle.Fill;
		this.RightDocks_ImageListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
		this.RightDocks_ImageListBox.HorizontalExtent = 10;
		this.RightDocks_ImageListBox.ImageList = this.RightDocks_imageList;
		this.RightDocks_ImageListBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.RightDocks_ImageListBox.ItemHeight = 61;
		this.RightDocks_ImageListBox.Location = new System.Drawing.Point(0, 24);
		this.RightDocks_ImageListBox.Name = "RightDocks_ImageListBox";
		this.RightDocks_ImageListBox.Size = new System.Drawing.Size(196, 185);
		this.RightDocks_ImageListBox.TabIndex = 19;
		this.RightDocks_ImageListBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.RightDocks_ImageListBox_DragDrop);
		this.RightDocks_ImageListBox.SelectedIndexChanged += new System.EventHandler(this.RightDocks_ImageListBox_SelectedIndexChanged);
		this.RightDocks_ImageListBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.RightDocks_ImageListBox_DragEnter);
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
		this.SongEdit_fontDialog.AllowVectorFonts = false;
		this.SongEdit_fontDialog.Font = new System.Drawing.Font("Microsoft Sans Serif", 35F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
		this.SongEdit_fontDialog.ShowEffects = false;
		// 
		// SongEdit_TextColorDialog
		// 
		this.SongEdit_TextColorDialog.Color = System.Drawing.Color.White;
		// 
		// RightDocks_SongList
		// 
		this.RightDocks_SongList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.RightDocks_SongList.Dock = System.Windows.Forms.DockStyle.Fill;
		this.RightDocks_SongList.Location = new System.Drawing.Point(0, 23);
		this.RightDocks_SongList.Name = "RightDocks_SongList";
		this.RightDocks_SongList.Size = new System.Drawing.Size(196, 106);
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
		this.RightDocks_SongListSearch.TextChanged += new System.EventHandler(this.RightDocks_SongListSearch_TextChanged);
		// 
		// RightDocks_SongList_ButtonPanel
		// 
		this.RightDocks_SongList_ButtonPanel.Controls.Add(this.btnRightDocks_SongListLoad);
		this.RightDocks_SongList_ButtonPanel.Controls.Add(this.btnRightDocks_SongListDelete);
		this.RightDocks_SongList_ButtonPanel.Controls.Add(this.btnRightDocks_SongList2PlayList);
		this.RightDocks_SongList_ButtonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.RightDocks_SongList_ButtonPanel.Location = new System.Drawing.Point(0, 139);
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
		this.RightDocks_PlayList.Size = new System.Drawing.Size(196, 132);
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
		this.RightDocks_TopPanel_PlayList_Button_Panel.Location = new System.Drawing.Point(0, 139);
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
		this.RightDocks_Search_ListBox.Size = new System.Drawing.Size(196, 80);
		this.RightDocks_Search_ListBox.TabIndex = 6;
		this.RightDocks_Search_ListBox.DoubleClick += new System.EventHandler(this.RightDocks_Search_ListBox_DoubleClick);
		// 
		// RightDocks_TopPanel_Search_ButtonPanel
		// 
		this.RightDocks_TopPanel_Search_ButtonPanel.Controls.Add(this.RightDocks_Search_SearchButton);
		this.RightDocks_TopPanel_Search_ButtonPanel.Controls.Add(this.RightDocks_Search_PlaylistButton);
		this.RightDocks_TopPanel_Search_ButtonPanel.Controls.Add(this.RightDocks_Search_LoadButton);
		this.RightDocks_TopPanel_Search_ButtonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.RightDocks_TopPanel_Search_ButtonPanel.Location = new System.Drawing.Point(0, 139);
		this.RightDocks_TopPanel_Search_ButtonPanel.Name = "RightDocks_TopPanel_Search_ButtonPanel";
		this.RightDocks_TopPanel_Search_ButtonPanel.Size = new System.Drawing.Size(196, 20);
		this.RightDocks_TopPanel_Search_ButtonPanel.TabIndex = 8;
		// 
		// RightDocks_Search_SearchButton
		// 
		this.RightDocks_Search_SearchButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
		this.RightDocks_Search_SearchButton.Location = new System.Drawing.Point(129, 1);
		this.RightDocks_Search_SearchButton.Name = "RightDocks_Search_SearchButton";
		this.RightDocks_Search_SearchButton.Size = new System.Drawing.Size(62, 18);
		this.RightDocks_Search_SearchButton.TabIndex = 7;
		this.RightDocks_Search_SearchButton.Text = "Search!";
		this.RightDocks_Search_SearchButton.Click += new System.EventHandler(this.RightDocks_Search_SearchButton_Click);
		// 
		// RightDocks_Search_PlaylistButton
		// 
		this.RightDocks_Search_PlaylistButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
		this.RightDocks_Search_PlaylistButton.Location = new System.Drawing.Point(66, 1);
		this.RightDocks_Search_PlaylistButton.Name = "RightDocks_Search_PlaylistButton";
		this.RightDocks_Search_PlaylistButton.Size = new System.Drawing.Size(62, 18);
		this.RightDocks_Search_PlaylistButton.TabIndex = 5;
		this.RightDocks_Search_PlaylistButton.Text = "<- Playlist";
		this.RightDocks_Search_PlaylistButton.Click += new System.EventHandler(this.RightDocks_Search_PlaylistButton_Click);
		// 
		// RightDocks_Search_LoadButton
		// 
		this.RightDocks_Search_LoadButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
		this.RightDocks_Search_LoadButton.Location = new System.Drawing.Point(0, 1);
		this.RightDocks_Search_LoadButton.Name = "RightDocks_Search_LoadButton";
		this.RightDocks_Search_LoadButton.Size = new System.Drawing.Size(64, 18);
		this.RightDocks_Search_LoadButton.TabIndex = 4;
		this.RightDocks_Search_LoadButton.Text = "<<-Load";
		this.RightDocks_Search_LoadButton.Click += new System.EventHandler(this.RightDocks_Search_LoadButton_Click);
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
		this.RightDocks_TopPanel_Search_RadioButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
		this.RightDocks_TopPanel_Search_RadioButton2.Location = new System.Drawing.Point(2, 34);
		this.RightDocks_TopPanel_Search_RadioButton2.Name = "RightDocks_TopPanel_Search_RadioButton2";
		this.RightDocks_TopPanel_Search_RadioButton2.Size = new System.Drawing.Size(94, 16);
		this.RightDocks_TopPanel_Search_RadioButton2.TabIndex = 7;
		this.RightDocks_TopPanel_Search_RadioButton2.Text = "Playlist";
		// 
		// RightDocks_TopPanel_Search_RadioButton1
		// 
		this.RightDocks_TopPanel_Search_RadioButton1.Checked = true;
		this.RightDocks_TopPanel_Search_RadioButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
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
		this.RightDocks_Search_InputBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RightDocks_Search_InputBox_KeyDown);
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
		this.SongEdit_BG_Label.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
		this.SongEdit_BG_Label.Location = new System.Drawing.Point(16, 48);
		this.SongEdit_BG_Label.Name = "SongEdit_BG_Label";
		this.SongEdit_BG_Label.Size = new System.Drawing.Size(176, 24);
		this.SongEdit_BG_Label.TabIndex = 1;
		// 
		// SongEdit_UpdateBeamBox_Button
		// 
		this.SongEdit_UpdateBeamBox_Button.FlatStyle = System.Windows.Forms.FlatStyle.System;
		this.SongEdit_UpdateBeamBox_Button.Location = new System.Drawing.Point(48, 30);
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
		this.ToolBars_bottomSandBarDock.Location = new System.Drawing.Point(0, 541);
		this.ToolBars_bottomSandBarDock.Manager = this.ToolBars_sandBarManager1;
		this.ToolBars_bottomSandBarDock.Name = "ToolBars_bottomSandBarDock";
		this.ToolBars_bottomSandBarDock.Size = new System.Drawing.Size(782, 0);
		this.ToolBars_bottomSandBarDock.TabIndex = 20;
		// 
		// ToolBars_leftSandBarDock
		// 
		this.ToolBars_leftSandBarDock.Controls.Add(this.ToolBars_ComponentBar);
		this.ToolBars_leftSandBarDock.Dock = System.Windows.Forms.DockStyle.Left;
		this.ToolBars_leftSandBarDock.Location = new System.Drawing.Point(0, 50);
		this.ToolBars_leftSandBarDock.Manager = this.ToolBars_sandBarManager1;
		this.ToolBars_leftSandBarDock.Name = "ToolBars_leftSandBarDock";
		this.ToolBars_leftSandBarDock.Size = new System.Drawing.Size(58, 491);
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
																						 this.Sermon_Button,
																						 this.BibleText_Button});
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
		this.ToolBars_ComponentBar.Size = new System.Drawing.Size(58, 489);
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
		this.Presentation_Button.Icon = ((System.Drawing.Icon)(resources.GetObject("Presentation_Button.Icon")));
		this.Presentation_Button.IconSize = new System.Drawing.Size(47, 47);
		this.Presentation_Button.Tag = null;
		this.Presentation_Button.ToolTipText = "Presentation";
		// 
		// Sermon_Button
		// 
		this.Sermon_Button.BuddyMenu = null;
		this.Sermon_Button.Icon = ((System.Drawing.Icon)(resources.GetObject("Sermon_Button.Icon")));
		this.Sermon_Button.IconSize = new System.Drawing.Size(47, 47);
		this.Sermon_Button.Tag = null;
		this.Sermon_Button.ToolTipText = "Sermon Tool";
		// 
		// BibleText_Button
		// 
		this.BibleText_Button.BuddyMenu = null;
		this.BibleText_Button.Icon = ((System.Drawing.Icon)(resources.GetObject("BibleText_Button.Icon")));
		this.BibleText_Button.IconSize = new System.Drawing.Size(48, 48);
		this.BibleText_Button.Tag = null;
		this.BibleText_Button.ToolTipText = "Bible text";
		// 
		// ToolBars_topSandBarDock
		// 
		this.ToolBars_topSandBarDock.Controls.Add(this.ToolBars_MainToolbar);
		this.ToolBars_topSandBarDock.Controls.Add(this.ToolBars_MenuBar);
		this.ToolBars_topSandBarDock.Dock = System.Windows.Forms.DockStyle.Top;
		this.ToolBars_topSandBarDock.Location = new System.Drawing.Point(0, 0);
		this.ToolBars_topSandBarDock.Manager = this.ToolBars_sandBarManager1;
		this.ToolBars_topSandBarDock.Name = "ToolBars_topSandBarDock";
		this.ToolBars_topSandBarDock.Size = new System.Drawing.Size(782, 50);
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
																						this.ToolBars_MainToolbar_SaveMediaList,
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
		this.ToolBars_MainToolbar.Size = new System.Drawing.Size(780, 26);
		this.ToolBars_MainToolbar.Stretch = true;
		this.ToolBars_MainToolbar.TabIndex = 1;
		this.ToolBars_MainToolbar.Tearable = false;
		this.ToolBars_MainToolbar.ButtonClick += new TD.SandBar.ToolBar.ButtonClickEventHandler(this.ToolBars_MainToolbar_ButtonClick);
		// 
		// ToolBars_MainToolbar_ShowBeamBox
		// 
		this.ToolBars_MainToolbar_ShowBeamBox.BeginGroup = true;
		this.ToolBars_MainToolbar_ShowBeamBox.BuddyMenu = null;
		this.ToolBars_MainToolbar_ShowBeamBox.Icon = ((System.Drawing.Icon)(resources.GetObject("ToolBars_MainToolbar_ShowBeamBox.Icon")));
		this.ToolBars_MainToolbar_ShowBeamBox.Tag = null;
		this.ToolBars_MainToolbar_ShowBeamBox.Text = "Show Projector Window";
		// 
		// ToolBars_MainToolbar_SizePosition
		// 
		this.ToolBars_MainToolbar_SizePosition.BuddyMenu = null;
		this.ToolBars_MainToolbar_SizePosition.Icon = ((System.Drawing.Icon)(resources.GetObject("ToolBars_MainToolbar_SizePosition.Icon")));
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
		// ToolBars_MainToolbar_SaveMediaList
		// 
		this.ToolBars_MainToolbar_SaveMediaList.BeginGroup = true;
		this.ToolBars_MainToolbar_SaveMediaList.BuddyMenu = null;
		this.ToolBars_MainToolbar_SaveMediaList.Icon = ((System.Drawing.Icon)(resources.GetObject("ToolBars_MainToolbar_SaveMediaList.Icon")));
		this.ToolBars_MainToolbar_SaveMediaList.Tag = null;
		this.ToolBars_MainToolbar_SaveMediaList.Text = "Save MediaList";
		this.ToolBars_MainToolbar_SaveMediaList.Visible = false;
		// 
		// ToolBars_MainToolbar_SaveSong
		// 
		this.ToolBars_MainToolbar_SaveSong.BeginGroup = true;
		this.ToolBars_MainToolbar_SaveSong.BuddyMenu = null;
		this.ToolBars_MainToolbar_SaveSong.Icon = ((System.Drawing.Icon)(resources.GetObject("ToolBars_MainToolbar_SaveSong.Icon")));
		this.ToolBars_MainToolbar_SaveSong.Tag = null;
		this.ToolBars_MainToolbar_SaveSong.Text = "Save Song";
		// 
		// ToolBars_MenuBar
		// 
		this.ToolBars_MenuBar.Buttons.AddRange(new TD.SandBar.ToolbarItemBase[] {
																					this.ToolBars_MenuBar_Song,
																					this.ToolBars_MenuBar_MediaList,
																					this.ToolBars_MenuBar_Sermon,
																					this.ToolBars_MenuBar_Edit,
																					this.ToolBars_MenuBar_View,
																					this.ToolBars_MenuBar_Help});
		this.ToolBars_MenuBar.Guid = new System.Guid("cc0de77e-657c-4906-a57a-f2a8d32fe17e");
		this.ToolBars_MenuBar.ImageList = null;
		this.ToolBars_MenuBar.Location = new System.Drawing.Point(2, 0);
		this.ToolBars_MenuBar.Movable = false;
		this.ToolBars_MenuBar.Name = "ToolBars_MenuBar";
		this.ToolBars_MenuBar.Size = new System.Drawing.Size(222, 24);
		this.ToolBars_MenuBar.Stretch = false;
		this.ToolBars_MenuBar.TabIndex = 0;
		this.ToolBars_MenuBar.Tearable = false;
		// 
		// ToolBars_MenuBar_Song
		// 
		this.ToolBars_MenuBar_Song.Icon = null;
		this.ToolBars_MenuBar_Song.MenuItems.AddRange(new TD.SandBar.MenuButtonItem[] {
																						  this.ToolBars_MenuBar_Song_New,
																						  this.ToolBars_MenuBar_Song_Save,
																						  this.ToolBars_MenuBar_Song_SaveAs,
																						  this.ToolBars_MenuBar_Song_Rename,
																						  this.ToolBars_MenuBar_Song_Exit});
		this.ToolBars_MenuBar_Song.Tag = null;
		this.ToolBars_MenuBar_Song.Text = "&File";
		// 
		// ToolBars_MenuBar_Song_New
		// 
		this.ToolBars_MenuBar_Song_New.Icon = ((System.Drawing.Icon)(resources.GetObject("ToolBars_MenuBar_Song_New.Icon")));
		this.ToolBars_MenuBar_Song_New.Shortcut = System.Windows.Forms.Shortcut.None;
		this.ToolBars_MenuBar_Song_New.Tag = null;
		this.ToolBars_MenuBar_Song_New.Text = "New...";
		this.ToolBars_MenuBar_Song_New.Activate += new TD.SandBar.MenuButtonItem.ActivateEventHandler(this.ToolBars_MenuBar_Song_New_Activate);
		// 
		// ToolBars_MenuBar_Song_Save
		// 
		this.ToolBars_MenuBar_Song_Save.Icon = ((System.Drawing.Icon)(resources.GetObject("ToolBars_MenuBar_Song_Save.Icon")));
		this.ToolBars_MenuBar_Song_Save.Shortcut = System.Windows.Forms.Shortcut.None;
		this.ToolBars_MenuBar_Song_Save.Tag = null;
		this.ToolBars_MenuBar_Song_Save.Text = "Save";
		this.ToolBars_MenuBar_Song_Save.Activate += new TD.SandBar.MenuButtonItem.ActivateEventHandler(this.ToolBars_MenuBar_Song_Save_Activate);
		// 
		// ToolBars_MenuBar_Song_SaveAs
		// 
		this.ToolBars_MenuBar_Song_SaveAs.Icon = ((System.Drawing.Icon)(resources.GetObject("ToolBars_MenuBar_Song_SaveAs.Icon")));
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
		this.ToolBars_MenuBar_Song_Rename.Text = "Rename...";
		this.ToolBars_MenuBar_Song_Rename.Activate += new TD.SandBar.MenuButtonItem.ActivateEventHandler(this.ToolBars_MenuBar_Song_Rename_Activate);
		// 
		// ToolBars_MenuBar_Song_Exit
		// 
		this.ToolBars_MenuBar_Song_Exit.Icon = ((System.Drawing.Icon)(resources.GetObject("ToolBars_MenuBar_Song_Exit.Icon")));
		this.ToolBars_MenuBar_Song_Exit.Shortcut = System.Windows.Forms.Shortcut.AltF4;
		this.ToolBars_MenuBar_Song_Exit.Tag = null;
		this.ToolBars_MenuBar_Song_Exit.Text = "Exit DreamBeam";
		this.ToolBars_MenuBar_Song_Exit.Activate += new TD.SandBar.MenuButtonItem.ActivateEventHandler(this.ToolBars_MenuBar_Song_Exit_Activate);
		// 
		// ToolBars_MenuBar_MediaList
		// 
		this.ToolBars_MenuBar_MediaList.Icon = null;
		this.ToolBars_MenuBar_MediaList.MenuItems.AddRange(new TD.SandBar.MenuButtonItem[] {
																							   this.ToolBars_MenuBar_MediaList_New,
																							   this.ToolBars_MenuBar_Media_Save,
																							   this.ToolBars_MenuBar_Media_SaveAs,
																							   this.ToolBars_MenuBar_Media_Rename,
																							   this.ToolBars_MenuBar_MediaList_Exit});
		this.ToolBars_MenuBar_MediaList.Tag = null;
		this.ToolBars_MenuBar_MediaList.Text = "&File";
		this.ToolBars_MenuBar_MediaList.Visible = false;
		// 
		// ToolBars_MenuBar_MediaList_New
		// 
		this.ToolBars_MenuBar_MediaList_New.Icon = ((System.Drawing.Icon)(resources.GetObject("ToolBars_MenuBar_MediaList_New.Icon")));
		this.ToolBars_MenuBar_MediaList_New.Shortcut = System.Windows.Forms.Shortcut.None;
		this.ToolBars_MenuBar_MediaList_New.Tag = null;
		this.ToolBars_MenuBar_MediaList_New.Text = "New...";
		this.ToolBars_MenuBar_MediaList_New.Activate += new TD.SandBar.MenuButtonItem.ActivateEventHandler(this.ToolBars_MenuBar_MediaList_New_Activate);
		// 
		// ToolBars_MenuBar_Media_Save
		// 
		this.ToolBars_MenuBar_Media_Save.Icon = ((System.Drawing.Icon)(resources.GetObject("ToolBars_MenuBar_Media_Save.Icon")));
		this.ToolBars_MenuBar_Media_Save.Shortcut = System.Windows.Forms.Shortcut.None;
		this.ToolBars_MenuBar_Media_Save.Tag = null;
		this.ToolBars_MenuBar_Media_Save.Text = "Save";
		// 
		// ToolBars_MenuBar_Media_SaveAs
		// 
		this.ToolBars_MenuBar_Media_SaveAs.Icon = ((System.Drawing.Icon)(resources.GetObject("ToolBars_MenuBar_Media_SaveAs.Icon")));
		this.ToolBars_MenuBar_Media_SaveAs.Shortcut = System.Windows.Forms.Shortcut.None;
		this.ToolBars_MenuBar_Media_SaveAs.Tag = null;
		this.ToolBars_MenuBar_Media_SaveAs.Text = "SaveAs..";
		this.ToolBars_MenuBar_Media_SaveAs.Activate += new TD.SandBar.MenuButtonItem.ActivateEventHandler(this.ToolBars_MenuBar_Media_SaveAs_Activate);
		// 
		// ToolBars_MenuBar_Media_Rename
		// 
		this.ToolBars_MenuBar_Media_Rename.Icon = null;
		this.ToolBars_MenuBar_Media_Rename.Shortcut = System.Windows.Forms.Shortcut.None;
		this.ToolBars_MenuBar_Media_Rename.Tag = null;
		this.ToolBars_MenuBar_Media_Rename.Text = "Rename...";
		this.ToolBars_MenuBar_Media_Rename.Activate += new TD.SandBar.MenuButtonItem.ActivateEventHandler(this.ToolBars_MenuBar_Media_Rename_Activate);
		// 
		// ToolBars_MenuBar_MediaList_Exit
		// 
		this.ToolBars_MenuBar_MediaList_Exit.Icon = ((System.Drawing.Icon)(resources.GetObject("ToolBars_MenuBar_MediaList_Exit.Icon")));
		this.ToolBars_MenuBar_MediaList_Exit.Shortcut = System.Windows.Forms.Shortcut.AltF4;
		this.ToolBars_MenuBar_MediaList_Exit.Tag = null;
		this.ToolBars_MenuBar_MediaList_Exit.Text = "Exit DreamBeam";
		this.ToolBars_MenuBar_MediaList_Exit.Activate += new TD.SandBar.MenuButtonItem.ActivateEventHandler(this.ToolBars_MenuBar_Song_Exit_Activate);
		// 
		// ToolBars_MenuBar_Sermon
		// 
		this.ToolBars_MenuBar_Sermon.Icon = null;
		this.ToolBars_MenuBar_Sermon.MenuItems.AddRange(new TD.SandBar.MenuButtonItem[] {
																							this.ToolBars_MenuBar_Sermon_Exit});
		this.ToolBars_MenuBar_Sermon.Tag = null;
		this.ToolBars_MenuBar_Sermon.Text = "&File";
		this.ToolBars_MenuBar_Sermon.Visible = false;
		// 
		// ToolBars_MenuBar_Sermon_Exit
		// 
		this.ToolBars_MenuBar_Sermon_Exit.Icon = null;
		this.ToolBars_MenuBar_Sermon_Exit.Shortcut = System.Windows.Forms.Shortcut.AltF4;
		this.ToolBars_MenuBar_Sermon_Exit.Tag = null;
		this.ToolBars_MenuBar_Sermon_Exit.Text = "Exit DreamBeam";
		this.ToolBars_MenuBar_Sermon_Exit.Activate += new TD.SandBar.MenuButtonItem.ActivateEventHandler(this.ToolBars_MenuBar_Sermon_Exit_Activate);
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
		this.ToolBars_MenuBar_Edit_Options.Icon = ((System.Drawing.Icon)(resources.GetObject("ToolBars_MenuBar_Edit_Options.Icon")));
		this.ToolBars_MenuBar_Edit_Options.Shortcut = System.Windows.Forms.Shortcut.None;
		this.ToolBars_MenuBar_Edit_Options.Tag = null;
		this.ToolBars_MenuBar_Edit_Options.Text = "Options";
		this.ToolBars_MenuBar_Edit_Options.Activate += new TD.SandBar.MenuButtonItem.ActivateEventHandler(this.ToolBars_MenuBar_Edit_Options_Activate);
		// 
		// ToolBars_MenuBar_View
		// 
		this.ToolBars_MenuBar_View.Icon = null;
		this.ToolBars_MenuBar_View.MenuItems.AddRange(new TD.SandBar.MenuButtonItem[] {
																						  this.ToolBars_MenuBar_View_ShowSongs,
																						  this.ToolBars_MenuBar_View_EditSongs,
																						  this.ToolBars_MenuBar_View_Presentation,
																						  this.ToolBars_MenuBar_View_TextTool});
		this.ToolBars_MenuBar_View.Tag = null;
		this.ToolBars_MenuBar_View.Text = "&View";
		// 
		// ToolBars_MenuBar_View_ShowSongs
		// 
		this.ToolBars_MenuBar_View_ShowSongs.Icon = ((System.Drawing.Icon)(resources.GetObject("ToolBars_MenuBar_View_ShowSongs.Icon")));
		this.ToolBars_MenuBar_View_ShowSongs.Shortcut = System.Windows.Forms.Shortcut.None;
		this.ToolBars_MenuBar_View_ShowSongs.Tag = null;
		this.ToolBars_MenuBar_View_ShowSongs.Text = "Show Songs";
		this.ToolBars_MenuBar_View_ShowSongs.Activate += new TD.SandBar.MenuButtonItem.ActivateEventHandler(this.ToolBars_MenuBar_View_ShowSongs_Activate);
		// 
		// ToolBars_MenuBar_View_EditSongs
		// 
		this.ToolBars_MenuBar_View_EditSongs.Icon = ((System.Drawing.Icon)(resources.GetObject("ToolBars_MenuBar_View_EditSongs.Icon")));
		this.ToolBars_MenuBar_View_EditSongs.Shortcut = System.Windows.Forms.Shortcut.None;
		this.ToolBars_MenuBar_View_EditSongs.Tag = null;
		this.ToolBars_MenuBar_View_EditSongs.Text = "Edit Songs";
		this.ToolBars_MenuBar_View_EditSongs.Activate += new TD.SandBar.MenuButtonItem.ActivateEventHandler(this.ToolBars_MenuBar_View_EditSongs_Activate);
		// 
		// ToolBars_MenuBar_View_Presentation
		// 
		this.ToolBars_MenuBar_View_Presentation.Icon = ((System.Drawing.Icon)(resources.GetObject("ToolBars_MenuBar_View_Presentation.Icon")));
		this.ToolBars_MenuBar_View_Presentation.Shortcut = System.Windows.Forms.Shortcut.None;
		this.ToolBars_MenuBar_View_Presentation.Tag = null;
		this.ToolBars_MenuBar_View_Presentation.Text = "Presentation";
		this.ToolBars_MenuBar_View_Presentation.Activate += new TD.SandBar.MenuButtonItem.ActivateEventHandler(this.ToolBars_MenuBar_View_Presentation_Activate);
		// 
		// ToolBars_MenuBar_View_TextTool
		// 
		this.ToolBars_MenuBar_View_TextTool.Icon = ((System.Drawing.Icon)(resources.GetObject("ToolBars_MenuBar_View_TextTool.Icon")));
		this.ToolBars_MenuBar_View_TextTool.Shortcut = System.Windows.Forms.Shortcut.None;
		this.ToolBars_MenuBar_View_TextTool.Tag = null;
		this.ToolBars_MenuBar_View_TextTool.Text = "Text Tool";
		this.ToolBars_MenuBar_View_TextTool.Activate += new TD.SandBar.MenuButtonItem.ActivateEventHandler(this.ToolBars_MenuBar_View_TextTool_Activate);
		// 
		// ToolBars_MenuBar_Help
		// 
		this.ToolBars_MenuBar_Help.Icon = null;
		this.ToolBars_MenuBar_Help.MenuItems.AddRange(new TD.SandBar.MenuButtonItem[] {
																						  this.HelpGetToKnow,
																						  this.HelpIntro,
																						  this.HelpBeamBox,
																						  this.HelpOptions,
																						  this.HelpComponents,
																						  this.HelpShowSongs,
																						  this.HelpEditSongs,
																						  this.HelpPresentation,
																						  this.HelpTextTool,
																						  this.AboutButton});
		this.ToolBars_MenuBar_Help.Tag = null;
		this.ToolBars_MenuBar_Help.Text = "&Help";
		// 
		// HelpGetToKnow
		// 
		this.HelpGetToKnow.BeginGroup = true;
		this.HelpGetToKnow.Enabled = false;
		this.HelpGetToKnow.Icon = null;
		this.HelpGetToKnow.Shortcut = System.Windows.Forms.Shortcut.None;
		this.HelpGetToKnow.Tag = null;
		this.HelpGetToKnow.Text = "Get To Know Dreambeam:";
		// 
		// HelpIntro
		// 
		this.HelpIntro.BeginGroup = true;
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
		// HelpComponents
		// 
		this.HelpComponents.BeginGroup = true;
		this.HelpComponents.Enabled = false;
		this.HelpComponents.Icon = null;
		this.HelpComponents.Shortcut = System.Windows.Forms.Shortcut.None;
		this.HelpComponents.Tag = null;
		this.HelpComponents.Text = "Dreambeam Components:";
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
		// HelpPresentation
		// 
		this.HelpPresentation.Icon = null;
		this.HelpPresentation.Shortcut = System.Windows.Forms.Shortcut.None;
		this.HelpPresentation.Tag = null;
		this.HelpPresentation.Text = "Presentation";
		this.HelpPresentation.Activate += new TD.SandBar.MenuButtonItem.ActivateEventHandler(this.HelpPresentation_Activate);
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
		this.statusBar.Controls.Add(this.RenderStatus);
		this.statusBar.Location = new System.Drawing.Point(58, 519);
		this.statusBar.Name = "statusBar";
		this.statusBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
																					 this.StatusPanel,
																					 this.statusBarUpdatePanel});
		this.statusBar.ShowPanels = true;
		this.statusBar.Size = new System.Drawing.Size(524, 22);
		this.statusBar.SizingGrip = false;
		this.statusBar.TabIndex = 22;
		// 
		// RenderStatus
		// 
		this.RenderStatus.Location = new System.Drawing.Point(418, 4);
		this.RenderStatus.Name = "RenderStatus";
		this.RenderStatus.Size = new System.Drawing.Size(100, 16);
		this.RenderStatus.TabIndex = 7;
		// 
		// StatusPanel
		// 
		this.StatusPanel.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
		this.StatusPanel.Icon = ((System.Drawing.Icon)(resources.GetObject("StatusPanel.Icon")));
		this.StatusPanel.MinWidth = 330;
		this.StatusPanel.Width = 414;
		// 
		// statusBarUpdatePanel
		// 
		this.statusBarUpdatePanel.MinWidth = 110;
		this.statusBarUpdatePanel.Width = 110;
		// 
		// tabControl1
		// 
		this.tabControl1.Controls.Add(this.tabPage0);
		this.tabControl1.Controls.Add(this.tabPage1);
		this.tabControl1.Controls.Add(this.tabPage2);
		this.tabControl1.Controls.Add(this.tabPage4);
		this.tabControl1.Controls.Add(this.BibleText_Tab);
		this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tabControl1.ItemSize = new System.Drawing.Size(30, 20);
		this.tabControl1.Location = new System.Drawing.Point(58, 50);
		this.tabControl1.Name = "tabControl1";
		this.tabControl1.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.tabControl1.SelectedIndex = 0;
		this.tabControl1.Size = new System.Drawing.Size(524, 469);
		this.tabControl1.TabIndex = 2;
		this.tabControl1.Click += new System.EventHandler(this.SongShow_Preview_Panel_Click);
		// 
		// tabPage0
		// 
		this.tabPage0.Controls.Add(this.SongShow_Right_Panel);
		this.tabPage0.Controls.Add(this.SongShow_StropheList_ListEx);
		this.tabPage0.Location = new System.Drawing.Point(4, 24);
		this.tabPage0.Name = "tabPage0";
		this.tabPage0.Size = new System.Drawing.Size(516, 441);
		this.tabPage0.TabIndex = 2;
		this.tabPage0.Text = "Show Songs";
		// 
		// SongShow_Right_Panel
		// 
		this.SongShow_Right_Panel.BackColor = System.Drawing.SystemColors.Control;
		this.SongShow_Right_Panel.Controls.Add(this.panel2);
		this.SongShow_Right_Panel.Controls.Add(this.SongShow_CollapsPanel);
		this.SongShow_Right_Panel.Controls.Add(this.SongShow_Preview_Panel);
		this.SongShow_Right_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
		this.SongShow_Right_Panel.Location = new System.Drawing.Point(320, 0);
		this.SongShow_Right_Panel.Name = "SongShow_Right_Panel";
		this.SongShow_Right_Panel.Size = new System.Drawing.Size(196, 441);
		this.SongShow_Right_Panel.TabIndex = 4;
		// 
		// panel2
		// 
		this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
		this.panel2.Controls.Add(this.SongShow_ToBeamBox_button);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel2.Location = new System.Drawing.Point(0, 393);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(196, 48);
		this.panel2.TabIndex = 28;
		// 
		// SongShow_ToBeamBox_button
		// 
		this.SongShow_ToBeamBox_button.FlatStyle = System.Windows.Forms.FlatStyle.System;
		this.SongShow_ToBeamBox_button.Location = new System.Drawing.Point(48, 12);
		this.SongShow_ToBeamBox_button.Name = "SongShow_ToBeamBox_button";
		this.SongShow_ToBeamBox_button.Size = new System.Drawing.Size(96, 24);
		this.SongShow_ToBeamBox_button.TabIndex = 3;
		this.SongShow_ToBeamBox_button.Text = "To Projector";
		this.SongShow_ToBeamBox_button.Click += new System.EventHandler(this.SongShow_StropheList_ListEx_DoubleClick);
		// 
		// SongShow_CollapsPanel
		// 
		this.SongShow_CollapsPanel.BackColor = System.Drawing.Color.LightSteelBlue;
		this.SongShow_CollapsPanel.Border = 4;
		this.SongShow_CollapsPanel.Controls.Add(this.SongShow_HideElementsPanel);
		this.SongShow_CollapsPanel.Controls.Add(this.SongShow_BackgroundPanel);
		this.SongShow_CollapsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
		this.SongShow_CollapsPanel.Location = new System.Drawing.Point(0, 148);
		this.SongShow_CollapsPanel.Name = "SongShow_CollapsPanel";
		this.SongShow_CollapsPanel.Size = new System.Drawing.Size(196, 293);
		this.SongShow_CollapsPanel.Spacing = 4;
		this.SongShow_CollapsPanel.TabIndex = 27;
		// 
		// SongShow_HideElementsPanel
		// 
		this.SongShow_HideElementsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
		this.SongShow_HideElementsPanel.BackColor = System.Drawing.Color.AliceBlue;
		this.SongShow_HideElementsPanel.Controls.Add(this.panel3);
		this.SongShow_HideElementsPanel.Controls.Add(this.panel1);
		this.SongShow_HideElementsPanel.Controls.Add(this.SongShow_HideElementsSub1Panel);
		this.SongShow_HideElementsPanel.EndColour = System.Drawing.Color.FromArgb(((System.Byte)(199)), ((System.Byte)(212)), ((System.Byte)(247)));
		this.SongShow_HideElementsPanel.Image = null;
		this.SongShow_HideElementsPanel.Location = new System.Drawing.Point(4, 92);
		this.SongShow_HideElementsPanel.Name = "SongShow_HideElementsPanel";
		this.SongShow_HideElementsPanel.PanelState = Salamander.Windows.Forms.PanelState.Expanded;
		this.SongShow_HideElementsPanel.Size = new System.Drawing.Size(188, 116);
		this.SongShow_HideElementsPanel.StartColour = System.Drawing.Color.White;
		this.SongShow_HideElementsPanel.TabIndex = 4;
		this.SongShow_HideElementsPanel.TitleFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
		this.SongShow_HideElementsPanel.TitleFontColour = System.Drawing.Color.Navy;
		this.SongShow_HideElementsPanel.TitleText = "Hide Elements";
		// 
		// panel3
		// 
		this.panel3.Controls.Add(this.SongShow_HideAuthor_Button);
		this.panel3.Location = new System.Drawing.Point(1, 85);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(185, 28);
		this.panel3.TabIndex = 5;
		// 
		// SongShow_HideAuthor_Button
		// 
		this.SongShow_HideAuthor_Button.bottomColor = System.Drawing.Color.DarkBlue;
		this.SongShow_HideAuthor_Button.BottomTransparent = 64;
		this.SongShow_HideAuthor_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.SongShow_HideAuthor_Button.LEDColor = System.Drawing.Color.SteelBlue;
		this.SongShow_HideAuthor_Button.Location = new System.Drawing.Point(40, 2);
		this.SongShow_HideAuthor_Button.Name = "SongShow_HideAuthor_Button";
		this.SongShow_HideAuthor_Button.TabIndex = 3;
		this.SongShow_HideAuthor_Button.Text = "Hide Author";
		this.SongShow_HideAuthor_Button.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.SongShow_HideAuthor_Button.topColor = System.Drawing.Color.Aquamarine;
		this.SongShow_HideAuthor_Button.TopTransparent = 64;
		this.SongShow_HideAuthor_Button.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SongShow_HideAuthor_Button_MouseUp);
		this.SongShow_HideAuthor_Button.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SongShow_HideAuthor_Button_MouseDown);
		// 
		// panel1
		// 
		this.panel1.Controls.Add(this.SongShow_HideText_Button);
		this.panel1.Location = new System.Drawing.Point(1, 55);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(185, 28);
		this.panel1.TabIndex = 4;
		// 
		// SongShow_HideText_Button
		// 
		this.SongShow_HideText_Button.bottomColor = System.Drawing.Color.DarkBlue;
		this.SongShow_HideText_Button.BottomTransparent = 64;
		this.SongShow_HideText_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.SongShow_HideText_Button.LEDColor = System.Drawing.Color.SteelBlue;
		this.SongShow_HideText_Button.Location = new System.Drawing.Point(40, 2);
		this.SongShow_HideText_Button.Name = "SongShow_HideText_Button";
		this.SongShow_HideText_Button.TabIndex = 2;
		this.SongShow_HideText_Button.Text = "Hide Verses";
		this.SongShow_HideText_Button.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.SongShow_HideText_Button.topColor = System.Drawing.Color.Aquamarine;
		this.SongShow_HideText_Button.TopTransparent = 64;
		this.SongShow_HideText_Button.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SongShow_HideText_Button_MouseUp);
		this.SongShow_HideText_Button.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SongShow_HideText_Button_MouseDown);
		// 
		// SongShow_HideElementsSub1Panel
		// 
		this.SongShow_HideElementsSub1Panel.Controls.Add(this.SongShow_HideTitle_Button);
		this.SongShow_HideElementsSub1Panel.Location = new System.Drawing.Point(1, 25);
		this.SongShow_HideElementsSub1Panel.Name = "SongShow_HideElementsSub1Panel";
		this.SongShow_HideElementsSub1Panel.Size = new System.Drawing.Size(185, 28);
		this.SongShow_HideElementsSub1Panel.TabIndex = 3;
		// 
		// SongShow_HideTitle_Button
		// 
		this.SongShow_HideTitle_Button.bottomColor = System.Drawing.Color.DarkBlue;
		this.SongShow_HideTitle_Button.BottomTransparent = 64;
		this.SongShow_HideTitle_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.SongShow_HideTitle_Button.LEDColor = System.Drawing.Color.SteelBlue;
		this.SongShow_HideTitle_Button.Location = new System.Drawing.Point(40, 2);
		this.SongShow_HideTitle_Button.Name = "SongShow_HideTitle_Button";
		this.SongShow_HideTitle_Button.TabIndex = 1;
		this.SongShow_HideTitle_Button.Text = "Hide Title";
		this.SongShow_HideTitle_Button.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.SongShow_HideTitle_Button.topColor = System.Drawing.Color.Aquamarine;
		this.SongShow_HideTitle_Button.TopTransparent = 64;
		this.SongShow_HideTitle_Button.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SongShow_HideTitle_Button_MouseUp);
		this.SongShow_HideTitle_Button.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SongShow_HideTitle_Button_MouseDown);
		// 
		// SongShow_BackgroundPanel
		// 
		this.SongShow_BackgroundPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
		this.SongShow_BackgroundPanel.BackColor = System.Drawing.Color.AliceBlue;
		this.SongShow_BackgroundPanel.Controls.Add(this.SongShow_OverwriteBG_Button);
		this.SongShow_BackgroundPanel.Controls.Add(this.SongShow_BG_Label);
		this.SongShow_BackgroundPanel.EndColour = System.Drawing.Color.FromArgb(((System.Byte)(199)), ((System.Byte)(212)), ((System.Byte)(247)));
		this.SongShow_BackgroundPanel.Image = null;
		this.SongShow_BackgroundPanel.Location = new System.Drawing.Point(4, 4);
		this.SongShow_BackgroundPanel.Name = "SongShow_BackgroundPanel";
		this.SongShow_BackgroundPanel.PanelState = Salamander.Windows.Forms.PanelState.Expanded;
		this.SongShow_BackgroundPanel.Size = new System.Drawing.Size(188, 84);
		this.SongShow_BackgroundPanel.StartColour = System.Drawing.Color.White;
		this.SongShow_BackgroundPanel.TabIndex = 0;
		this.SongShow_BackgroundPanel.TitleFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
		this.SongShow_BackgroundPanel.TitleFontColour = System.Drawing.Color.Navy;
		this.SongShow_BackgroundPanel.TitleText = "Background";
		// 
		// SongShow_OverwriteBG_Button
		// 
		this.SongShow_OverwriteBG_Button.bottomColor = System.Drawing.Color.DarkBlue;
		this.SongShow_OverwriteBG_Button.BottomTransparent = 64;
		this.SongShow_OverwriteBG_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.SongShow_OverwriteBG_Button.LEDColor = System.Drawing.Color.SteelBlue;
		this.SongShow_OverwriteBG_Button.Location = new System.Drawing.Point(40, 32);
		this.SongShow_OverwriteBG_Button.Name = "SongShow_OverwriteBG_Button";
		this.SongShow_OverwriteBG_Button.Size = new System.Drawing.Size(102, 24);
		this.SongShow_OverwriteBG_Button.TabIndex = 4;
		this.SongShow_OverwriteBG_Button.Text = "Overwrite";
		this.SongShow_OverwriteBG_Button.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.SongShow_OverwriteBG_Button.topColor = System.Drawing.Color.Aquamarine;
		this.SongShow_OverwriteBG_Button.TopTransparent = 64;
		this.SongShow_OverwriteBG_Button.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SongShow_OverwriteBG_Button_MouseUp);
		this.SongShow_OverwriteBG_Button.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SongShow_OverwriteBG_Button_MouseDown);
		// 
		// SongShow_BG_Label
		// 
		this.SongShow_BG_Label.BackColor = System.Drawing.Color.Transparent;
		this.SongShow_BG_Label.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.SongShow_BG_Label.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
		this.SongShow_BG_Label.Location = new System.Drawing.Point(0, 60);
		this.SongShow_BG_Label.Name = "SongShow_BG_Label";
		this.SongShow_BG_Label.Size = new System.Drawing.Size(188, 24);
		this.SongShow_BG_Label.TabIndex = 3;
		this.SongShow_BG_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		// 
		// SongShow_Preview_Panel
		// 
		this.SongShow_Preview_Panel.BackColor = System.Drawing.Color.White;
		this.SongShow_Preview_Panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.SongShow_Preview_Panel.Dock = System.Windows.Forms.DockStyle.Top;
		this.SongShow_Preview_Panel.Location = new System.Drawing.Point(0, 0);
		this.SongShow_Preview_Panel.Name = "SongShow_Preview_Panel";
		this.SongShow_Preview_Panel.Size = new System.Drawing.Size(196, 148);
		this.SongShow_Preview_Panel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.SongShow_Preview_Panel.TabIndex = 25;
		this.SongShow_Preview_Panel.TabStop = false;
		this.SongShow_Preview_Panel.Click += new System.EventHandler(this.SongShow_Preview_Panel_Click);
		this.SongShow_Preview_Panel.Paint += new System.Windows.Forms.PaintEventHandler(this.SongEdit_Preview_Panel_Paint);
		// 
		// SongShow_StropheList_ListEx
		// 
		this.SongShow_StropheList_ListEx.Dock = System.Windows.Forms.DockStyle.Left;
		this.SongShow_StropheList_ListEx.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
		this.SongShow_StropheList_ListEx.Imgs = null;
		this.SongShow_StropheList_ListEx.LineColor = System.Drawing.Color.FromArgb(((System.Byte)(199)), ((System.Byte)(199)), ((System.Byte)(199)));
		this.SongShow_StropheList_ListEx.Location = new System.Drawing.Point(0, 0);
		this.SongShow_StropheList_ListEx.Name = "SongShow_StropheList_ListEx";
		this.SongShow_StropheList_ListEx.Size = new System.Drawing.Size(320, 441);
		this.SongShow_StropheList_ListEx.TabIndex = 1;
		this.SongShow_StropheList_ListEx.DoubleClick += new System.EventHandler(this.SongShow_StropheList_ListEx_DoubleClick);
		// 
		// tabPage2
		// 
		this.tabPage2.Controls.Add(this.Sermon_LeftPanel);
		this.tabPage2.Controls.Add(this.Sermon_TabControl);
		this.tabPage2.Location = new System.Drawing.Point(4, 24);
		this.tabPage2.Name = "tabPage2";
		this.tabPage2.Size = new System.Drawing.Size(574, 491);
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
		this.Sermon_LeftPanel.Size = new System.Drawing.Size(414, 491);
		this.Sermon_LeftPanel.TabIndex = 3;
		// 
		// Sermon_LeftBottom_Panel
		// 
		this.Sermon_LeftBottom_Panel.Controls.Add(this.Sermon_BeamBox_Button);
		this.Sermon_LeftBottom_Panel.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.Sermon_LeftBottom_Panel.Location = new System.Drawing.Point(0, 461);
		this.Sermon_LeftBottom_Panel.Name = "Sermon_LeftBottom_Panel";
		this.Sermon_LeftBottom_Panel.Size = new System.Drawing.Size(414, 30);
		this.Sermon_LeftBottom_Panel.TabIndex = 5;
		// 
		// Sermon_BeamBox_Button
		// 
		this.Sermon_BeamBox_Button.FlatStyle = System.Windows.Forms.FlatStyle.System;
		this.Sermon_BeamBox_Button.Location = new System.Drawing.Point(136, 0);
		this.Sermon_BeamBox_Button.Name = "Sermon_BeamBox_Button";
		this.Sermon_BeamBox_Button.Size = new System.Drawing.Size(80, 23);
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
		this.Sermon_LeftDoc_Panel.Size = new System.Drawing.Size(414, 459);
		this.Sermon_LeftDoc_Panel.TabIndex = 4;
		// 
		// Sermon_DocManager
		// 
		this.Sermon_DocManager.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Sermon_DocManager.Location = new System.Drawing.Point(0, 0);
		this.Sermon_DocManager.Name = "Sermon_DocManager";
		this.Sermon_DocManager.Size = new System.Drawing.Size(414, 459);
		this.Sermon_DocManager.TabIndex = 1;
		this.Sermon_DocManager.CloseButtonPressed += new DocumentManager.DocumentManager.CloseButtonPressedEventHandler(this.Sermon_DocManager_CloseButtonPressed);
		// 
		// Sermon_LeftToolBar_Panel
		// 
		this.Sermon_LeftToolBar_Panel.Controls.Add(this.Sermon_ToolBar);
		this.Sermon_LeftToolBar_Panel.Dock = System.Windows.Forms.DockStyle.Top;
		this.Sermon_LeftToolBar_Panel.Location = new System.Drawing.Point(0, 0);
		this.Sermon_LeftToolBar_Panel.Name = "Sermon_LeftToolBar_Panel";
		this.Sermon_LeftToolBar_Panel.Size = new System.Drawing.Size(414, 32);
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
		this.Sermon_ToolBar.Size = new System.Drawing.Size(414, 26);
		this.Sermon_ToolBar.TabIndex = 0;
		this.Sermon_ToolBar.Text = "toolBar1";
		this.Sermon_ToolBar.ButtonClick += new TD.SandBar.ToolBar.ButtonClickEventHandler(this.Sermon_ToolBar_ButtonClick);
		// 
		// Sermon_ToolBar_NewDoc_Button
		// 
		this.Sermon_ToolBar_NewDoc_Button.BuddyMenu = null;
		this.Sermon_ToolBar_NewDoc_Button.Icon = null;
		this.Sermon_ToolBar_NewDoc_Button.Tag = null;
		this.Sermon_ToolBar_NewDoc_Button.Text = "New";
		// 
		// Sermon_ToolBar_Font_Button
		// 
		this.Sermon_ToolBar_Font_Button.BeginGroup = true;
		this.Sermon_ToolBar_Font_Button.BuddyMenu = null;
		this.Sermon_ToolBar_Font_Button.Icon = ((System.Drawing.Icon)(resources.GetObject("Sermon_ToolBar_Font_Button.Icon")));
		this.Sermon_ToolBar_Font_Button.Tag = null;
		this.Sermon_ToolBar_Font_Button.Text = "Font";
		// 
		// Sermon_ToolBar_Color_Button
		// 
		this.Sermon_ToolBar_Color_Button.BuddyMenu = null;
		this.Sermon_ToolBar_Color_Button.Icon = ((System.Drawing.Icon)(resources.GetObject("Sermon_ToolBar_Color_Button.Icon")));
		this.Sermon_ToolBar_Color_Button.Tag = null;
		this.Sermon_ToolBar_Color_Button.Text = "Color";
		// 
		// Sermon_ToolBar_Outline_Button
		// 
		this.Sermon_ToolBar_Outline_Button.BeginGroup = true;
		this.Sermon_ToolBar_Outline_Button.BuddyMenu = null;
		this.Sermon_ToolBar_Outline_Button.Icon = ((System.Drawing.Icon)(resources.GetObject("Sermon_ToolBar_Outline_Button.Icon")));
		this.Sermon_ToolBar_Outline_Button.Tag = null;
		this.Sermon_ToolBar_Outline_Button.Text = "Text Outline";
		// 
		// Sermon_ToolBar_OutlineColor_Button
		// 
		this.Sermon_ToolBar_OutlineColor_Button.BuddyMenu = null;
		this.Sermon_ToolBar_OutlineColor_Button.Icon = ((System.Drawing.Icon)(resources.GetObject("Sermon_ToolBar_OutlineColor_Button.Icon")));
		this.Sermon_ToolBar_OutlineColor_Button.Tag = null;
		this.Sermon_ToolBar_OutlineColor_Button.Text = "Outline Color";
		// 
		// Sermon_TabControl
		// 
		this.Sermon_TabControl.Controls.Add(this.tabPage3);
		this.Sermon_TabControl.Dock = System.Windows.Forms.DockStyle.Right;
		this.Sermon_TabControl.Location = new System.Drawing.Point(414, 0);
		this.Sermon_TabControl.Name = "Sermon_TabControl";
		this.Sermon_TabControl.SelectedIndex = 0;
		this.Sermon_TabControl.Size = new System.Drawing.Size(160, 491);
		this.Sermon_TabControl.TabIndex = 2;
		// 
		// tabPage3
		// 
		this.tabPage3.BackColor = System.Drawing.Color.Transparent;
		this.tabPage3.Controls.Add(this.linkLabel1);
		this.tabPage3.Controls.Add(this.Sermon_Verse_Label);
		this.tabPage3.Controls.Add(this.Sermon_Translation_Label);
		this.tabPage3.Controls.Add(this.Sermon_Books_Label);
		this.tabPage3.Controls.Add(this.Sermon_BibleKey);
		this.tabPage3.Controls.Add(this.Sermon_Testament_ListBox);
		this.tabPage3.Controls.Add(this.Sermon_Books);
		this.tabPage3.Controls.Add(this.Sermon_BookList);
		this.tabPage3.Location = new System.Drawing.Point(4, 22);
		this.tabPage3.Name = "tabPage3";
		this.tabPage3.Size = new System.Drawing.Size(152, 415);
		this.tabPage3.TabIndex = 0;
		this.tabPage3.Text = "Bible";
		// 
		// linkLabel1
		// 
		this.linkLabel1.LinkColor = System.Drawing.Color.Black;
		this.linkLabel1.Location = new System.Drawing.Point(8, 400);
		this.linkLabel1.Name = "linkLabel1";
		this.linkLabel1.Size = new System.Drawing.Size(120, 15);
		this.linkLabel1.TabIndex = 7;
		this.linkLabel1.TabStop = true;
		this.linkLabel1.Text = "get the Sword Bible";
		this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
		this.linkLabel1.Click += new System.EventHandler(this.linkLabel1_Click);
		// 
		// Sermon_Verse_Label
		// 
		this.Sermon_Verse_Label.Location = new System.Drawing.Point(8, 8);
		this.Sermon_Verse_Label.Name = "Sermon_Verse_Label";
		this.Sermon_Verse_Label.Size = new System.Drawing.Size(112, 16);
		this.Sermon_Verse_Label.TabIndex = 6;
		this.Sermon_Verse_Label.Text = "Find  Verse:";
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
		this.Sermon_BibleKey.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Sermon_BibleKey_KeyDown);
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
		// tabPage4
		// 
		this.tabPage4.BackColor = System.Drawing.Color.Transparent;
		this.tabPage4.Controls.Add(this.Presentation_FadePanel);
		this.tabPage4.Controls.Add(this.Presentation_MainPanel);
		this.tabPage4.Location = new System.Drawing.Point(4, 24);
		this.tabPage4.Name = "tabPage4";
		this.tabPage4.Size = new System.Drawing.Size(574, 491);
		this.tabPage4.TabIndex = 4;
		this.tabPage4.Text = "Presentation";
		// 
		// Presentation_FadePanel
		// 
		this.Presentation_FadePanel.BackColor = System.Drawing.Color.Gainsboro;
		this.Presentation_FadePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.Presentation_FadePanel.Controls.Add(this.Fade_panel);
		this.Presentation_FadePanel.Controls.Add(this.treeView1);
		this.Presentation_FadePanel.Dock = System.Windows.Forms.DockStyle.Right;
		this.Presentation_FadePanel.DockPadding.All = 2;
		this.Presentation_FadePanel.Location = new System.Drawing.Point(566, 0);
		this.Presentation_FadePanel.Name = "Presentation_FadePanel";
		this.Presentation_FadePanel.Size = new System.Drawing.Size(8, 491);
		this.Presentation_FadePanel.TabIndex = 3;
		// 
		// Fade_panel
		// 
		this.Fade_panel.Controls.Add(this.Presentation_Fade_ListView);
		this.Fade_panel.Controls.Add(this.Fade_Top_Panel);
		this.Fade_panel.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Fade_panel.Location = new System.Drawing.Point(192, 2);
		this.Fade_panel.Name = "Fade_panel";
		this.Fade_panel.Size = new System.Drawing.Size(-188, 485);
		this.Fade_panel.TabIndex = 4;
		// 
		// Presentation_Fade_ListView
		// 
		this.Presentation_Fade_ListView.Alignment = System.Windows.Forms.ListViewAlignment.Left;
		this.Presentation_Fade_ListView.AllowColumnReorder = true;
		this.Presentation_Fade_ListView.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Presentation_Fade_ListView.FullRowSelect = true;
		this.Presentation_Fade_ListView.GridLines = true;
		this.Presentation_Fade_ListView.Location = new System.Drawing.Point(0, 152);
		this.Presentation_Fade_ListView.Name = "Presentation_Fade_ListView";
		this.Presentation_Fade_ListView.Size = new System.Drawing.Size(-188, 333);
		this.Presentation_Fade_ListView.SmallImageList = this.Presentation_Fade_ImageList;
		this.Presentation_Fade_ListView.TabIndex = 3;
		this.Presentation_Fade_ListView.View = System.Windows.Forms.View.List;
		this.Presentation_Fade_ListView.Click += new System.EventHandler(this.Presentation_Fade_ListView_Click);
		this.Presentation_Fade_ListView.DoubleClick += new System.EventHandler(this.Presentation_Fade_ListView_DoubleClick);
		// 
		// Presentation_Fade_ImageList
		// 
		this.Presentation_Fade_ImageList.ImageSize = new System.Drawing.Size(16, 16);
		this.Presentation_Fade_ImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("Presentation_Fade_ImageList.ImageStream")));
		this.Presentation_Fade_ImageList.TransparentColor = System.Drawing.Color.Transparent;
		// 
		// Fade_Top_Panel
		// 
		this.Fade_Top_Panel.Controls.Add(this.Presentation_Fade_Refresh_Button);
		this.Fade_Top_Panel.Controls.Add(this.Presentation_Fade_ToPlaylist_Button);
		this.Fade_Top_Panel.Controls.Add(this.Presentation_Fade_preview);
		this.Fade_Top_Panel.Dock = System.Windows.Forms.DockStyle.Top;
		this.Fade_Top_Panel.Location = new System.Drawing.Point(0, 0);
		this.Fade_Top_Panel.Name = "Fade_Top_Panel";
		this.Fade_Top_Panel.Size = new System.Drawing.Size(-188, 152);
		this.Fade_Top_Panel.TabIndex = 6;
		// 
		// Presentation_Fade_Refresh_Button
		// 
		this.Presentation_Fade_Refresh_Button.FlatStyle = System.Windows.Forms.FlatStyle.System;
		this.Presentation_Fade_Refresh_Button.Location = new System.Drawing.Point(208, 80);
		this.Presentation_Fade_Refresh_Button.Name = "Presentation_Fade_Refresh_Button";
		this.Presentation_Fade_Refresh_Button.Size = new System.Drawing.Size(96, 23);
		this.Presentation_Fade_Refresh_Button.TabIndex = 6;
		this.Presentation_Fade_Refresh_Button.Text = "Refresh Drives";
		this.Presentation_Fade_Refresh_Button.Click += new System.EventHandler(this.Presentation_Fade_Refresh_Button_Click);
		// 
		// Presentation_Fade_ToPlaylist_Button
		// 
		this.Presentation_Fade_ToPlaylist_Button.FlatStyle = System.Windows.Forms.FlatStyle.System;
		this.Presentation_Fade_ToPlaylist_Button.Location = new System.Drawing.Point(208, 112);
		this.Presentation_Fade_ToPlaylist_Button.Name = "Presentation_Fade_ToPlaylist_Button";
		this.Presentation_Fade_ToPlaylist_Button.Size = new System.Drawing.Size(96, 24);
		this.Presentation_Fade_ToPlaylist_Button.TabIndex = 5;
		this.Presentation_Fade_ToPlaylist_Button.Text = "Add ->";
		this.Presentation_Fade_ToPlaylist_Button.Click += new System.EventHandler(this.Presentation_Fade_ToPlaylist_Button_Click);
		// 
		// Presentation_Fade_preview
		// 
		this.Presentation_Fade_preview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.Presentation_Fade_preview.Location = new System.Drawing.Point(2, 0);
		this.Presentation_Fade_preview.Name = "Presentation_Fade_preview";
		this.Presentation_Fade_preview.Size = new System.Drawing.Size(200, 150);
		this.Presentation_Fade_preview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.Presentation_Fade_preview.TabIndex = 4;
		this.Presentation_Fade_preview.TabStop = false;
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
		this.treeView1.Size = new System.Drawing.Size(190, 485);
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
		// Presentation_MainPanel
		// 
		this.Presentation_MainPanel.BackColor = System.Drawing.Color.Transparent;
		this.Presentation_MainPanel.Controls.Add(this.Presentation_PreviewPanel);
		this.Presentation_MainPanel.Controls.Add(this.Presentation_MovieControlPanel);
		this.Presentation_MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Presentation_MainPanel.Location = new System.Drawing.Point(0, 0);
		this.Presentation_MainPanel.Name = "Presentation_MainPanel";
		this.Presentation_MainPanel.Size = new System.Drawing.Size(574, 491);
		this.Presentation_MainPanel.TabIndex = 4;
		// 
		// Presentation_PreviewPanel
		// 
		this.Presentation_PreviewPanel.BackColor = System.Drawing.Color.Black;
		this.Presentation_PreviewPanel.Controls.Add(this.Presentation_VideoPanel);
		this.Presentation_PreviewPanel.Controls.Add(this.axShockwaveFlash);
		this.Presentation_PreviewPanel.Controls.Add(this.Presentation_PreviewBox);
		this.Presentation_PreviewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Presentation_PreviewPanel.DockPadding.All = 10;
		this.Presentation_PreviewPanel.Location = new System.Drawing.Point(0, 0);
		this.Presentation_PreviewPanel.Name = "Presentation_PreviewPanel";
		this.Presentation_PreviewPanel.Size = new System.Drawing.Size(574, 431);
		this.Presentation_PreviewPanel.TabIndex = 2;
		// 
		// Presentation_VideoPanel
		// 
		this.Presentation_VideoPanel.BackColor = System.Drawing.Color.White;
		this.Presentation_VideoPanel.Location = new System.Drawing.Point(32, 32);
		this.Presentation_VideoPanel.Name = "Presentation_VideoPanel";
		this.Presentation_VideoPanel.Size = new System.Drawing.Size(448, 328);
		this.Presentation_VideoPanel.TabIndex = 2;
		// 
		// axShockwaveFlash
		// 
		this.axShockwaveFlash.ContainingControl = this;
		this.axShockwaveFlash.Dock = System.Windows.Forms.DockStyle.Fill;
		this.axShockwaveFlash.Enabled = true;
		this.axShockwaveFlash.ImeMode = System.Windows.Forms.ImeMode.On;
		this.axShockwaveFlash.Location = new System.Drawing.Point(10, 10);
		this.axShockwaveFlash.Name = "axShockwaveFlash";
		this.axShockwaveFlash.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axShockwaveFlash.OcxState")));
		this.axShockwaveFlash.Size = new System.Drawing.Size(554, 411);
		this.axShockwaveFlash.TabIndex = 1;
		// 
		// Presentation_PreviewBox
		// 
		this.Presentation_PreviewBox.BackColor = System.Drawing.Color.White;
		this.Presentation_PreviewBox.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Presentation_PreviewBox.Location = new System.Drawing.Point(10, 10);
		this.Presentation_PreviewBox.Name = "Presentation_PreviewBox";
		this.Presentation_PreviewBox.Size = new System.Drawing.Size(554, 411);
		this.Presentation_PreviewBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.Presentation_PreviewBox.TabIndex = 0;
		this.Presentation_PreviewBox.TabStop = false;
		// 
		// Presentation_MovieControlPanel
		// 
		this.Presentation_MovieControlPanel.BackColor = System.Drawing.Color.Gainsboro;
		this.Presentation_MovieControlPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.Presentation_MovieControlPanel.Controls.Add(this.Presentation_MovieControlPanelBottom);
		this.Presentation_MovieControlPanel.Controls.Add(this.Presentation_MovieControlPanel_Top);
		this.Presentation_MovieControlPanel.Controls.Add(this.Presentation_MovieControlPanel_Right);
		this.Presentation_MovieControlPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.Presentation_MovieControlPanel.Location = new System.Drawing.Point(0, 431);
		this.Presentation_MovieControlPanel.Name = "Presentation_MovieControlPanel";
		this.Presentation_MovieControlPanel.Size = new System.Drawing.Size(574, 60);
		this.Presentation_MovieControlPanel.TabIndex = 3;
		// 
		// Presentation_MovieControlPanelBottom
		// 
		this.Presentation_MovieControlPanelBottom.Controls.Add(this.Presentation_MediaLoop_Checkbox);
		this.Presentation_MovieControlPanelBottom.Controls.Add(this.Presentation_MovieControl_PreviewButtonPanel);
		this.Presentation_MovieControlPanelBottom.Controls.Add(this.Presentation_MovieControlPanelBottomLeft);
		this.Presentation_MovieControlPanelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.Presentation_MovieControlPanelBottom.Location = new System.Drawing.Point(0, 28);
		this.Presentation_MovieControlPanelBottom.Name = "Presentation_MovieControlPanelBottom";
		this.Presentation_MovieControlPanelBottom.Size = new System.Drawing.Size(524, 30);
		this.Presentation_MovieControlPanelBottom.TabIndex = 4;
		// 
		// Presentation_MediaLoop_Checkbox
		// 
		this.Presentation_MediaLoop_Checkbox.Checked = true;
		this.Presentation_MediaLoop_Checkbox.CheckState = System.Windows.Forms.CheckState.Checked;
		this.Presentation_MediaLoop_Checkbox.Location = new System.Drawing.Point(80, 5);
		this.Presentation_MediaLoop_Checkbox.Name = "Presentation_MediaLoop_Checkbox";
		this.Presentation_MediaLoop_Checkbox.Size = new System.Drawing.Size(96, 24);
		this.Presentation_MediaLoop_Checkbox.TabIndex = 4;
		this.Presentation_MediaLoop_Checkbox.Text = "Loop";
		this.Presentation_MediaLoop_Checkbox.CheckedChanged += new System.EventHandler(this.Presentation_MediaLoop_Checkbox_CheckedChanged);
		// 
		// Presentation_MovieControl_PreviewButtonPanel
		// 
		this.Presentation_MovieControl_PreviewButtonPanel.Controls.Add(this.Presentation_MoviePreviewButton);
		this.Presentation_MovieControl_PreviewButtonPanel.Dock = System.Windows.Forms.DockStyle.Right;
		this.Presentation_MovieControl_PreviewButtonPanel.Location = new System.Drawing.Point(324, 0);
		this.Presentation_MovieControl_PreviewButtonPanel.Name = "Presentation_MovieControl_PreviewButtonPanel";
		this.Presentation_MovieControl_PreviewButtonPanel.Size = new System.Drawing.Size(200, 30);
		this.Presentation_MovieControl_PreviewButtonPanel.TabIndex = 3;
		// 
		// Presentation_MoviePreviewButton
		// 
		this.Presentation_MoviePreviewButton.Enabled = false;
		this.Presentation_MoviePreviewButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
		this.Presentation_MoviePreviewButton.Location = new System.Drawing.Point(128, 1);
		this.Presentation_MoviePreviewButton.Name = "Presentation_MoviePreviewButton";
		this.Presentation_MoviePreviewButton.Size = new System.Drawing.Size(72, 23);
		this.Presentation_MoviePreviewButton.TabIndex = 1;
		this.Presentation_MoviePreviewButton.Text = "Preview";
		this.Presentation_MoviePreviewButton.Click += new System.EventHandler(this.Presentation_MoviePreviewButton_Click);
		// 
		// Presentation_MovieControlPanelBottomLeft
		// 
		this.Presentation_MovieControlPanelBottomLeft.Controls.Add(this.Presentation_PlayBar);
		this.Presentation_MovieControlPanelBottomLeft.Dock = System.Windows.Forms.DockStyle.Left;
		this.Presentation_MovieControlPanelBottomLeft.Location = new System.Drawing.Point(0, 0);
		this.Presentation_MovieControlPanelBottomLeft.Name = "Presentation_MovieControlPanelBottomLeft";
		this.Presentation_MovieControlPanelBottomLeft.Size = new System.Drawing.Size(72, 30);
		this.Presentation_MovieControlPanelBottomLeft.TabIndex = 2;
		// 
		// Presentation_PlayBar
		// 
		this.Presentation_PlayBar.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
		this.Presentation_PlayBar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																								this.Presentation_PlayButton,
																								this.Presentation_PauseButton,
																								this.Presentation_StopButton});
		this.Presentation_PlayBar.DropDownArrows = true;
		this.Presentation_PlayBar.ImageList = this.PlayButtons_ImageList;
		this.Presentation_PlayBar.Location = new System.Drawing.Point(0, 0);
		this.Presentation_PlayBar.Name = "Presentation_PlayBar";
		this.Presentation_PlayBar.ShowToolTips = true;
		this.Presentation_PlayBar.Size = new System.Drawing.Size(72, 42);
		this.Presentation_PlayBar.TabIndex = 0;
		this.Presentation_PlayBar.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.Presentation_PlayBar_ButtonClick);
		// 
		// Presentation_PlayButton
		// 
		this.Presentation_PlayButton.Enabled = false;
		this.Presentation_PlayButton.ImageIndex = 0;
		this.Presentation_PlayButton.ToolTipText = "Play On Projector";
		// 
		// Presentation_PauseButton
		// 
		this.Presentation_PauseButton.Enabled = false;
		this.Presentation_PauseButton.ImageIndex = 1;
		// 
		// Presentation_StopButton
		// 
		this.Presentation_StopButton.Enabled = false;
		this.Presentation_StopButton.ImageIndex = 2;
		// 
		// PlayButtons_ImageList
		// 
		this.PlayButtons_ImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
		this.PlayButtons_ImageList.ImageSize = new System.Drawing.Size(16, 16);
		this.PlayButtons_ImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("PlayButtons_ImageList.ImageStream")));
		this.PlayButtons_ImageList.TransparentColor = System.Drawing.Color.Red;
		// 
		// Presentation_MovieControlPanel_Top
		// 
		this.Presentation_MovieControlPanel_Top.Controls.Add(this.Media_TrackBar);
		this.Presentation_MovieControlPanel_Top.Dock = System.Windows.Forms.DockStyle.Top;
		this.Presentation_MovieControlPanel_Top.Location = new System.Drawing.Point(0, 0);
		this.Presentation_MovieControlPanel_Top.Name = "Presentation_MovieControlPanel_Top";
		this.Presentation_MovieControlPanel_Top.Size = new System.Drawing.Size(524, 30);
		this.Presentation_MovieControlPanel_Top.TabIndex = 3;
		// 
		// Media_TrackBar
		// 
		this.Media_TrackBar.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Media_TrackBar.Enabled = false;
		this.Media_TrackBar.Location = new System.Drawing.Point(0, 0);
		this.Media_TrackBar.Name = "Media_TrackBar";
		this.Media_TrackBar.Size = new System.Drawing.Size(524, 30);
		this.Media_TrackBar.TabIndex = 0;
		this.Media_TrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
		this.Media_TrackBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Media_TrackBar_Up);
		// 
		// Presentation_MovieControlPanel_Right
		// 
		this.Presentation_MovieControlPanel_Right.Controls.Add(this.AudioBar);
		this.Presentation_MovieControlPanel_Right.Dock = System.Windows.Forms.DockStyle.Right;
		this.Presentation_MovieControlPanel_Right.Location = new System.Drawing.Point(524, 0);
		this.Presentation_MovieControlPanel_Right.Name = "Presentation_MovieControlPanel_Right";
		this.Presentation_MovieControlPanel_Right.Size = new System.Drawing.Size(48, 58);
		this.Presentation_MovieControlPanel_Right.TabIndex = 2;
		// 
		// AudioBar
		// 
		this.AudioBar.Enabled = false;
		this.AudioBar.Location = new System.Drawing.Point(10, -6);
		this.AudioBar.Maximum = 0;
		this.AudioBar.Minimum = -10000;
		this.AudioBar.Name = "AudioBar";
		this.AudioBar.Orientation = System.Windows.Forms.Orientation.Vertical;
		this.AudioBar.Size = new System.Drawing.Size(45, 71);
		this.AudioBar.TabIndex = 0;
		this.AudioBar.TickStyle = System.Windows.Forms.TickStyle.None;
		this.AudioBar.ValueChanged += new System.EventHandler(this.AudioBar_ValueChanged);
		// 
		// tabPage1
		// 
		this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
		this.tabPage1.Controls.Add(this.SongEdit_RightPanel);
		this.tabPage1.Controls.Add(this.SongEdit_BigInputFieldPanel);
		this.tabPage1.ImageIndex = 0;
		this.tabPage1.Location = new System.Drawing.Point(4, 24);
		this.tabPage1.Name = "tabPage1";
		this.tabPage1.Size = new System.Drawing.Size(516, 441);
		this.tabPage1.TabIndex = 0;
		this.tabPage1.Text = "Edit Songs";
		// 
		// SongEdit_RightPanel
		// 
		this.SongEdit_RightPanel.Controls.Add(this.collapsiblePanelBar1);
		this.SongEdit_RightPanel.Controls.Add(this.SongEdit_Preview_Panel);
		this.SongEdit_RightPanel.Controls.Add(this.panel4);
		this.SongEdit_RightPanel.Dock = System.Windows.Forms.DockStyle.Fill;
		this.SongEdit_RightPanel.Location = new System.Drawing.Point(320, 0);
		this.SongEdit_RightPanel.Name = "SongEdit_RightPanel";
		this.SongEdit_RightPanel.Size = new System.Drawing.Size(196, 441);
		this.SongEdit_RightPanel.TabIndex = 34;
		// 
		// collapsiblePanelBar1
		// 
		this.collapsiblePanelBar1.BackColor = System.Drawing.Color.LightSteelBlue;
		this.collapsiblePanelBar1.Border = 4;
		this.collapsiblePanelBar1.Controls.Add(this.SongEdit_MultiLangPanel);
		this.collapsiblePanelBar1.Controls.Add(this.SongEdit_TextAlignPanel);
		this.collapsiblePanelBar1.Controls.Add(this.SongEdit_BackgroundPanel);
		this.collapsiblePanelBar1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.collapsiblePanelBar1.Location = new System.Drawing.Point(0, 148);
		this.collapsiblePanelBar1.Name = "collapsiblePanelBar1";
		this.collapsiblePanelBar1.Size = new System.Drawing.Size(196, 237);
		this.collapsiblePanelBar1.Spacing = 4;
		this.collapsiblePanelBar1.TabIndex = 30;
		// 
		// SongEdit_MultiLangPanel
		// 
		this.SongEdit_MultiLangPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
		this.SongEdit_MultiLangPanel.BackColor = System.Drawing.Color.AliceBlue;
		this.SongEdit_MultiLangPanel.Controls.Add(this.SongEdit_ML_Button);
		this.SongEdit_MultiLangPanel.Controls.Add(this.SongEdit_LangTextToolBar);
		this.SongEdit_MultiLangPanel.EndColour = System.Drawing.Color.FromArgb(((System.Byte)(199)), ((System.Byte)(212)), ((System.Byte)(247)));
		this.SongEdit_MultiLangPanel.Image = null;
		this.SongEdit_MultiLangPanel.Location = new System.Drawing.Point(4, 164);
		this.SongEdit_MultiLangPanel.Name = "SongEdit_MultiLangPanel";
		this.SongEdit_MultiLangPanel.PanelState = Salamander.Windows.Forms.PanelState.Expanded;
		this.SongEdit_MultiLangPanel.Size = new System.Drawing.Size(188, 76);
		this.SongEdit_MultiLangPanel.StartColour = System.Drawing.Color.White;
		this.SongEdit_MultiLangPanel.TabIndex = 5;
		this.SongEdit_MultiLangPanel.TitleFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
		this.SongEdit_MultiLangPanel.TitleFontColour = System.Drawing.Color.Navy;
		this.SongEdit_MultiLangPanel.TitleText = "Multi-Language";
		// 
		// SongEdit_ML_Button
		// 
		this.SongEdit_ML_Button.bottomColor = System.Drawing.Color.DarkBlue;
		this.SongEdit_ML_Button.BottomTransparent = 64;
		this.SongEdit_ML_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.SongEdit_ML_Button.LEDColor = System.Drawing.Color.SteelBlue;
		this.SongEdit_ML_Button.Location = new System.Drawing.Point(48, 24);
		this.SongEdit_ML_Button.Name = "SongEdit_ML_Button";
		this.SongEdit_ML_Button.TabIndex = 6;
		this.SongEdit_ML_Button.Text = "Enable";
		this.SongEdit_ML_Button.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.SongEdit_ML_Button.topColor = System.Drawing.Color.Aquamarine;
		this.SongEdit_ML_Button.TopTransparent = 64;
		this.SongEdit_ML_Button.Click += new System.EventHandler(this.SongEdit_ML_Button_Click);
		// 
		// SongEdit_LangTextToolBar
		// 
		this.SongEdit_LangTextToolBar.AllowHorizontalDock = false;
		this.SongEdit_LangTextToolBar.AllowVerticalDock = false;
		this.SongEdit_LangTextToolBar.Buttons.AddRange(new TD.SandBar.ToolbarItemBase[] {
																							this.SongEdit_ButtonLangFont,
																							this.SongEdit_ButtonLangColor,
																							this.SongEdit_ButtonLangTextOutline,
																							this.SongEdit_ButtonLangOutlineColor});
		this.SongEdit_LangTextToolBar.Closable = false;
		this.SongEdit_LangTextToolBar.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.SongEdit_LangTextToolBar.Guid = new System.Guid("db4d90f8-7c61-4d40-b5c5-9b111ba4e44c");
		this.SongEdit_LangTextToolBar.ImageList = null;
		this.SongEdit_LangTextToolBar.Location = new System.Drawing.Point(0, 50);
		this.SongEdit_LangTextToolBar.Movable = false;
		this.SongEdit_LangTextToolBar.Name = "SongEdit_LangTextToolBar";
		this.SongEdit_LangTextToolBar.Size = new System.Drawing.Size(188, 26);
		this.SongEdit_LangTextToolBar.TabIndex = 1;
		this.SongEdit_LangTextToolBar.Tearable = false;
		this.SongEdit_LangTextToolBar.Text = "";
		this.SongEdit_LangTextToolBar.ButtonClick += new TD.SandBar.ToolBar.ButtonClickEventHandler(this.SongEdit_LangTextToolBar_ButtonClick);
		// 
		// SongEdit_ButtonLangFont
		// 
		this.SongEdit_ButtonLangFont.BuddyMenu = null;
		this.SongEdit_ButtonLangFont.Icon = ((System.Drawing.Icon)(resources.GetObject("SongEdit_ButtonLangFont.Icon")));
		this.SongEdit_ButtonLangFont.Tag = null;
		// 
		// SongEdit_ButtonLangColor
		// 
		this.SongEdit_ButtonLangColor.BuddyMenu = null;
		this.SongEdit_ButtonLangColor.Icon = ((System.Drawing.Icon)(resources.GetObject("SongEdit_ButtonLangColor.Icon")));
		this.SongEdit_ButtonLangColor.Tag = null;
		// 
		// SongEdit_ButtonLangTextOutline
		// 
		this.SongEdit_ButtonLangTextOutline.BuddyMenu = null;
		this.SongEdit_ButtonLangTextOutline.Icon = ((System.Drawing.Icon)(resources.GetObject("SongEdit_ButtonLangTextOutline.Icon")));
		this.SongEdit_ButtonLangTextOutline.Tag = null;
		this.SongEdit_ButtonLangTextOutline.Visible = false;
		// 
		// SongEdit_ButtonLangOutlineColor
		// 
		this.SongEdit_ButtonLangOutlineColor.BuddyMenu = null;
		this.SongEdit_ButtonLangOutlineColor.Icon = ((System.Drawing.Icon)(resources.GetObject("SongEdit_ButtonLangOutlineColor.Icon")));
		this.SongEdit_ButtonLangOutlineColor.Tag = null;
		// 
		// SongEdit_TextAlignPanel
		// 
		this.SongEdit_TextAlignPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
		this.SongEdit_TextAlignPanel.BackColor = System.Drawing.Color.AliceBlue;
		this.SongEdit_TextAlignPanel.Controls.Add(this.SongEdit_AlignLeft_Button);
		this.SongEdit_TextAlignPanel.Controls.Add(this.SongEdit_AlignCenter_Button);
		this.SongEdit_TextAlignPanel.Controls.Add(this.SongEdit_AlignRight_Button);
		this.SongEdit_TextAlignPanel.EndColour = System.Drawing.Color.FromArgb(((System.Byte)(199)), ((System.Byte)(212)), ((System.Byte)(247)));
		this.SongEdit_TextAlignPanel.Image = null;
		this.SongEdit_TextAlignPanel.Location = new System.Drawing.Point(4, 92);
		this.SongEdit_TextAlignPanel.Name = "SongEdit_TextAlignPanel";
		this.SongEdit_TextAlignPanel.PanelState = Salamander.Windows.Forms.PanelState.Expanded;
		this.SongEdit_TextAlignPanel.Size = new System.Drawing.Size(188, 68);
		this.SongEdit_TextAlignPanel.StartColour = System.Drawing.Color.White;
		this.SongEdit_TextAlignPanel.TabIndex = 4;
		this.SongEdit_TextAlignPanel.TitleFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
		this.SongEdit_TextAlignPanel.TitleFontColour = System.Drawing.Color.Navy;
		this.SongEdit_TextAlignPanel.TitleText = "Text Align";
		// 
		// SongEdit_AlignLeft_Button
		// 
		this.SongEdit_AlignLeft_Button.bottomColor = System.Drawing.Color.DarkBlue;
		this.SongEdit_AlignLeft_Button.BottomTransparent = 64;
		this.SongEdit_AlignLeft_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.SongEdit_AlignLeft_Button.Image = ((System.Drawing.Image)(resources.GetObject("SongEdit_AlignLeft_Button.Image")));
		this.SongEdit_AlignLeft_Button.LEDColor = System.Drawing.Color.SteelBlue;
		this.SongEdit_AlignLeft_Button.LEDHeight = 50;
		this.SongEdit_AlignLeft_Button.LEDOffset = 4;
		this.SongEdit_AlignLeft_Button.LEDWidth = 22;
		this.SongEdit_AlignLeft_Button.Location = new System.Drawing.Point(16, 32);
		this.SongEdit_AlignLeft_Button.Name = "SongEdit_AlignLeft_Button";
		this.SongEdit_AlignLeft_Button.Size = new System.Drawing.Size(48, 24);
		this.SongEdit_AlignLeft_Button.TabIndex = 6;
		this.SongEdit_AlignLeft_Button.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.SongEdit_AlignLeft_Button.topColor = System.Drawing.Color.Aquamarine;
		this.SongEdit_AlignLeft_Button.TopTransparent = 64;
		this.SongEdit_AlignLeft_Button.CheckedChanged += new System.EventHandler(this.SongEdit_AlignLeft_Button_CheckedChanged);
		// 
		// SongEdit_AlignCenter_Button
		// 
		this.SongEdit_AlignCenter_Button.bottomColor = System.Drawing.Color.DarkBlue;
		this.SongEdit_AlignCenter_Button.BottomTransparent = 64;
		this.SongEdit_AlignCenter_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.SongEdit_AlignCenter_Button.Image = ((System.Drawing.Image)(resources.GetObject("SongEdit_AlignCenter_Button.Image")));
		this.SongEdit_AlignCenter_Button.LEDColor = System.Drawing.Color.SteelBlue;
		this.SongEdit_AlignCenter_Button.LEDHeight = 50;
		this.SongEdit_AlignCenter_Button.LEDOffset = 4;
		this.SongEdit_AlignCenter_Button.LEDWidth = 22;
		this.SongEdit_AlignCenter_Button.Location = new System.Drawing.Point(72, 32);
		this.SongEdit_AlignCenter_Button.Name = "SongEdit_AlignCenter_Button";
		this.SongEdit_AlignCenter_Button.Size = new System.Drawing.Size(48, 24);
		this.SongEdit_AlignCenter_Button.TabIndex = 4;
		this.SongEdit_AlignCenter_Button.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.SongEdit_AlignCenter_Button.topColor = System.Drawing.Color.Aquamarine;
		this.SongEdit_AlignCenter_Button.TopTransparent = 64;
		this.SongEdit_AlignCenter_Button.CheckedChanged += new System.EventHandler(this.SongEdit_AlignLeft_Button_CheckedChanged);
		// 
		// SongEdit_AlignRight_Button
		// 
		this.SongEdit_AlignRight_Button.bottomColor = System.Drawing.Color.DarkBlue;
		this.SongEdit_AlignRight_Button.BottomTransparent = 64;
		this.SongEdit_AlignRight_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.SongEdit_AlignRight_Button.Image = ((System.Drawing.Image)(resources.GetObject("SongEdit_AlignRight_Button.Image")));
		this.SongEdit_AlignRight_Button.LEDColor = System.Drawing.Color.SteelBlue;
		this.SongEdit_AlignRight_Button.LEDHeight = 50;
		this.SongEdit_AlignRight_Button.LEDOffset = 3;
		this.SongEdit_AlignRight_Button.LEDWidth = 22;
		this.SongEdit_AlignRight_Button.Location = new System.Drawing.Point(128, 32);
		this.SongEdit_AlignRight_Button.Name = "SongEdit_AlignRight_Button";
		this.SongEdit_AlignRight_Button.Size = new System.Drawing.Size(48, 24);
		this.SongEdit_AlignRight_Button.TabIndex = 5;
		this.SongEdit_AlignRight_Button.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.SongEdit_AlignRight_Button.topColor = System.Drawing.Color.Aquamarine;
		this.SongEdit_AlignRight_Button.TopTransparent = 64;
		this.SongEdit_AlignRight_Button.CheckedChanged += new System.EventHandler(this.SongEdit_AlignLeft_Button_CheckedChanged);
		// 
		// SongEdit_BackgroundPanel
		// 
		this.SongEdit_BackgroundPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
		this.SongEdit_BackgroundPanel.BackColor = System.Drawing.Color.AliceBlue;
		this.SongEdit_BackgroundPanel.Controls.Add(this.SongEdit_BG_Label);
		this.SongEdit_BackgroundPanel.Controls.Add(this.SongEdit_BG_DecscriptionLabel);
		this.SongEdit_BackgroundPanel.EndColour = System.Drawing.Color.FromArgb(((System.Byte)(199)), ((System.Byte)(212)), ((System.Byte)(247)));
		this.SongEdit_BackgroundPanel.Image = null;
		this.SongEdit_BackgroundPanel.Location = new System.Drawing.Point(4, 4);
		this.SongEdit_BackgroundPanel.Name = "SongEdit_BackgroundPanel";
		this.SongEdit_BackgroundPanel.PanelState = Salamander.Windows.Forms.PanelState.Expanded;
		this.SongEdit_BackgroundPanel.Size = new System.Drawing.Size(188, 84);
		this.SongEdit_BackgroundPanel.StartColour = System.Drawing.Color.White;
		this.SongEdit_BackgroundPanel.TabIndex = 0;
		this.SongEdit_BackgroundPanel.TitleFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
		this.SongEdit_BackgroundPanel.TitleFontColour = System.Drawing.Color.Navy;
		this.SongEdit_BackgroundPanel.TitleText = "Background";
		// 
		// SongEdit_BG_DecscriptionLabel
		// 
		this.SongEdit_BG_DecscriptionLabel.Location = new System.Drawing.Point(16, 24);
		this.SongEdit_BG_DecscriptionLabel.Name = "SongEdit_BG_DecscriptionLabel";
		this.SongEdit_BG_DecscriptionLabel.Size = new System.Drawing.Size(104, 23);
		this.SongEdit_BG_DecscriptionLabel.TabIndex = 2;
		this.SongEdit_BG_DecscriptionLabel.Text = "Background Image:";
		// 
		// SongEdit_Preview_Panel
		// 
		this.SongEdit_Preview_Panel.BackColor = System.Drawing.Color.White;
		this.SongEdit_Preview_Panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.SongEdit_Preview_Panel.Dock = System.Windows.Forms.DockStyle.Top;
		this.SongEdit_Preview_Panel.Location = new System.Drawing.Point(0, 0);
		this.SongEdit_Preview_Panel.Name = "SongEdit_Preview_Panel";
		this.SongEdit_Preview_Panel.Size = new System.Drawing.Size(196, 148);
		this.SongEdit_Preview_Panel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.SongEdit_Preview_Panel.TabIndex = 32;
		this.SongEdit_Preview_Panel.TabStop = false;
		this.SongEdit_Preview_Panel.Click += new System.EventHandler(this.SongShow_Preview_Panel_Click);
		this.SongEdit_Preview_Panel.Paint += new System.Windows.Forms.PaintEventHandler(this.SongEdit_Preview_Panel_Paint);
		// 
		// panel4
		// 
		this.panel4.BackColor = System.Drawing.SystemColors.ControlLight;
		this.panel4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel4.BackgroundImage")));
		this.panel4.Controls.Add(this.SongEdit_UpdateBeamBox_Button);
		this.panel4.Controls.Add(this.SongEdit_PreviewStropheUp_Button);
		this.panel4.Controls.Add(this.SongEdit_PreviewStropheDown_Button);
		this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel4.Location = new System.Drawing.Point(0, 385);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(196, 56);
		this.panel4.TabIndex = 31;
		// 
		// SongEdit_PreviewStropheUp_Button
		// 
		this.SongEdit_PreviewStropheUp_Button.FlatStyle = System.Windows.Forms.FlatStyle.System;
		this.SongEdit_PreviewStropheUp_Button.Location = new System.Drawing.Point(98, 6);
		this.SongEdit_PreviewStropheUp_Button.Name = "SongEdit_PreviewStropheUp_Button";
		this.SongEdit_PreviewStropheUp_Button.Size = new System.Drawing.Size(98, 19);
		this.SongEdit_PreviewStropheUp_Button.TabIndex = 25;
		this.SongEdit_PreviewStropheUp_Button.Text = "Next Verse";
		this.SongEdit_PreviewStropheUp_Button.Click += new System.EventHandler(this.SongEdit_PreviewStropheUp_Button_Click);
		// 
		// SongEdit_PreviewStropheDown_Button
		// 
		this.SongEdit_PreviewStropheDown_Button.FlatStyle = System.Windows.Forms.FlatStyle.System;
		this.SongEdit_PreviewStropheDown_Button.Location = new System.Drawing.Point(0, 6);
		this.SongEdit_PreviewStropheDown_Button.Name = "SongEdit_PreviewStropheDown_Button";
		this.SongEdit_PreviewStropheDown_Button.Size = new System.Drawing.Size(98, 19);
		this.SongEdit_PreviewStropheDown_Button.TabIndex = 26;
		this.SongEdit_PreviewStropheDown_Button.Text = "Previous Verse";
		this.SongEdit_PreviewStropheDown_Button.Click += new System.EventHandler(this.SongEdit_PreviewStropheDown_Button_Click);
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
		this.SongEdit_BigInputFieldPanel.Size = new System.Drawing.Size(320, 441);
		this.SongEdit_BigInputFieldPanel.TabIndex = 33;
		// 
		// SongEdit_InputFieldPanelMid
		// 
		this.SongEdit_InputFieldPanelMid.Controls.Add(this.SongEdit_InputFieldBelowMenuPanelMid);
		this.SongEdit_InputFieldPanelMid.Controls.Add(this.SongEdit_InputFieldMenuPanelMid);
		this.SongEdit_InputFieldPanelMid.Dock = System.Windows.Forms.DockStyle.Fill;
		this.SongEdit_InputFieldPanelMid.Location = new System.Drawing.Point(0, 90);
		this.SongEdit_InputFieldPanelMid.Name = "SongEdit_InputFieldPanelMid";
		this.SongEdit_InputFieldPanelMid.Size = new System.Drawing.Size(320, 261);
		this.SongEdit_InputFieldPanelMid.TabIndex = 2;
		// 
		// SongEdit_InputFieldBelowMenuPanelMid
		// 
		this.SongEdit_InputFieldBelowMenuPanelMid.Controls.Add(this.SongEdit_InputFieldBelowMenuPane2lMid);
		this.SongEdit_InputFieldBelowMenuPanelMid.Controls.Add(this.SongEdit_MidPos_Panel);
		this.SongEdit_InputFieldBelowMenuPanelMid.Dock = System.Windows.Forms.DockStyle.Fill;
		this.SongEdit_InputFieldBelowMenuPanelMid.Location = new System.Drawing.Point(0, 24);
		this.SongEdit_InputFieldBelowMenuPanelMid.Name = "SongEdit_InputFieldBelowMenuPanelMid";
		this.SongEdit_InputFieldBelowMenuPanelMid.Size = new System.Drawing.Size(320, 237);
		this.SongEdit_InputFieldBelowMenuPanelMid.TabIndex = 23;
		// 
		// SongEdit_InputFieldBelowMenuPane2lMid
		// 
		this.SongEdit_InputFieldBelowMenuPane2lMid.Controls.Add(this.SongEdit_Mid_TextBox);
		this.SongEdit_InputFieldBelowMenuPane2lMid.Dock = System.Windows.Forms.DockStyle.Fill;
		this.SongEdit_InputFieldBelowMenuPane2lMid.Location = new System.Drawing.Point(0, 0);
		this.SongEdit_InputFieldBelowMenuPane2lMid.Name = "SongEdit_InputFieldBelowMenuPane2lMid";
		this.SongEdit_InputFieldBelowMenuPane2lMid.Size = new System.Drawing.Size(320, 217);
		this.SongEdit_InputFieldBelowMenuPane2lMid.TabIndex = 2;
		// 
		// SongEdit_Mid_TextBox
		// 
		this.SongEdit_Mid_TextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.SongEdit_Mid_TextBox.Dock = System.Windows.Forms.DockStyle.Fill;
		this.SongEdit_Mid_TextBox.Location = new System.Drawing.Point(0, 0);
		this.SongEdit_Mid_TextBox.Name = "SongEdit_Mid_TextBox";
		this.SongEdit_Mid_TextBox.Size = new System.Drawing.Size(320, 217);
		this.SongEdit_Mid_TextBox.TabIndex = 22;
		this.SongEdit_Mid_TextBox.Text = "";
		this.SongEdit_Mid_TextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SongEdit_Mid_TextBox_KeyDown);
		this.SongEdit_Mid_TextBox.TextChanged += new System.EventHandler(this.SongEdit_Mid_TextBox_TextChanged1);
		this.SongEdit_Mid_TextBox.Enter += new System.EventHandler(this.SongEdit_Mid_TextBox_Enter);
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
		this.SongEdit_MidPos_Panel.Location = new System.Drawing.Point(0, 217);
		this.SongEdit_MidPos_Panel.Name = "SongEdit_MidPos_Panel";
		this.SongEdit_MidPos_Panel.Size = new System.Drawing.Size(320, 20);
		this.SongEdit_MidPos_Panel.TabIndex = 1;
		// 
		// SongEdit_Header_Verses
		// 
		this.SongEdit_Header_Verses.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
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
		this.SongEdit_MidPosX_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
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
		this.SongEdit_MidPosY_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
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
		this.SongEdit_MidTextToolBar.Size = new System.Drawing.Size(320, 26);
		this.SongEdit_MidTextToolBar.TabIndex = 0;
		this.SongEdit_MidTextToolBar.Text = "SongEdit_MidTextToolBar";
		this.SongEdit_MidTextToolBar.ButtonClick += new TD.SandBar.ToolBar.ButtonClickEventHandler(this.SongEdit_MidTextToolBar_ButtonClick);
		// 
		// SongEdit_ButtonMidFont
		// 
		this.SongEdit_ButtonMidFont.BuddyMenu = null;
		this.SongEdit_ButtonMidFont.Icon = ((System.Drawing.Icon)(resources.GetObject("SongEdit_ButtonMidFont.Icon")));
		this.SongEdit_ButtonMidFont.Tag = null;
		this.SongEdit_ButtonMidFont.Text = "Font";
		// 
		// SongEdit_ButtonMidColor
		// 
		this.SongEdit_ButtonMidColor.BuddyMenu = null;
		this.SongEdit_ButtonMidColor.Icon = ((System.Drawing.Icon)(resources.GetObject("SongEdit_ButtonMidColor.Icon")));
		this.SongEdit_ButtonMidColor.Tag = null;
		this.SongEdit_ButtonMidColor.Text = "Color";
		// 
		// SongEdit_ButtonMidTextOutline
		// 
		this.SongEdit_ButtonMidTextOutline.BeginGroup = true;
		this.SongEdit_ButtonMidTextOutline.BuddyMenu = null;
		this.SongEdit_ButtonMidTextOutline.Icon = ((System.Drawing.Icon)(resources.GetObject("SongEdit_ButtonMidTextOutline.Icon")));
		this.SongEdit_ButtonMidTextOutline.Tag = null;
		this.SongEdit_ButtonMidTextOutline.Text = "TextOutline";
		// 
		// SongEdit_ButtonMidOutlineColor
		// 
		this.SongEdit_ButtonMidOutlineColor.BuddyMenu = null;
		this.SongEdit_ButtonMidOutlineColor.Icon = ((System.Drawing.Icon)(resources.GetObject("SongEdit_ButtonMidOutlineColor.Icon")));
		this.SongEdit_ButtonMidOutlineColor.Tag = null;
		this.SongEdit_ButtonMidOutlineColor.Text = "Outline Color";
		this.SongEdit_ButtonMidOutlineColor.Visible = false;
		// 
		// SongEdit_InputFieldPanelButtom
		// 
		this.SongEdit_InputFieldPanelButtom.Controls.Add(this.SongEdit_InputFieldBelowMenuPanelButtom);
		this.SongEdit_InputFieldPanelButtom.Controls.Add(this.SongEdit_InputFieldMenuPanelButtom);
		this.SongEdit_InputFieldPanelButtom.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.SongEdit_InputFieldPanelButtom.Location = new System.Drawing.Point(0, 351);
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
		this.SongEdit_Header_Author.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
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
		this.SongEdit_BottomPosX_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
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
		this.SongEdit_BottomPosY_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
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
		this.SongEdit_BottomTextToolBar.Size = new System.Drawing.Size(320, 26);
		this.SongEdit_BottomTextToolBar.TabIndex = 0;
		this.SongEdit_BottomTextToolBar.Text = "SongEdit_BottomTextToolBar";
		this.SongEdit_BottomTextToolBar.ButtonClick += new TD.SandBar.ToolBar.ButtonClickEventHandler(this.SongEdit_BottomTextToolBar_ButtonClick);
		// 
		// SongEdit_ButtonBottomFont
		// 
		this.SongEdit_ButtonBottomFont.BuddyMenu = null;
		this.SongEdit_ButtonBottomFont.Icon = ((System.Drawing.Icon)(resources.GetObject("SongEdit_ButtonBottomFont.Icon")));
		this.SongEdit_ButtonBottomFont.Tag = null;
		this.SongEdit_ButtonBottomFont.Text = "Font";
		// 
		// SongEdit_ButtonBottomColor
		// 
		this.SongEdit_ButtonBottomColor.BuddyMenu = null;
		this.SongEdit_ButtonBottomColor.Icon = ((System.Drawing.Icon)(resources.GetObject("SongEdit_ButtonBottomColor.Icon")));
		this.SongEdit_ButtonBottomColor.Tag = null;
		this.SongEdit_ButtonBottomColor.Text = "Color";
		// 
		// SongEdit_ButtonBottomTextOutline
		// 
		this.SongEdit_ButtonBottomTextOutline.BeginGroup = true;
		this.SongEdit_ButtonBottomTextOutline.BuddyMenu = null;
		this.SongEdit_ButtonBottomTextOutline.Icon = ((System.Drawing.Icon)(resources.GetObject("SongEdit_ButtonBottomTextOutline.Icon")));
		this.SongEdit_ButtonBottomTextOutline.Tag = null;
		this.SongEdit_ButtonBottomTextOutline.Text = "TextOutline";
		// 
		// SongEdit_ButtonBottomOutlineColor
		// 
		this.SongEdit_ButtonBottomOutlineColor.BuddyMenu = null;
		this.SongEdit_ButtonBottomOutlineColor.Icon = ((System.Drawing.Icon)(resources.GetObject("SongEdit_ButtonBottomOutlineColor.Icon")));
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
		this.SongEdit_Header_Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
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
		this.SongEdit_TopPosX_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
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
		this.SongEdit_TopPosY_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
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
		this.SongEdit_TopTextToolBar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
		this.SongEdit_TopTextToolBar.Guid = new System.Guid("580d7392-4fcd-4dbd-bc2a-4ed0234aad2a");
		this.SongEdit_TopTextToolBar.ImageList = null;
		this.SongEdit_TopTextToolBar.Location = new System.Drawing.Point(0, 0);
		this.SongEdit_TopTextToolBar.Name = "SongEdit_TopTextToolBar";
		this.SongEdit_TopTextToolBar.Size = new System.Drawing.Size(320, 26);
		this.SongEdit_TopTextToolBar.TabIndex = 0;
		this.SongEdit_TopTextToolBar.Text = "";
		this.SongEdit_TopTextToolBar.ButtonClick += new TD.SandBar.ToolBar.ButtonClickEventHandler(this.SongEdit_TopTextToolBar_ButtonClick);
		// 
		// SongEdit_ButtonTopFont
		// 
		this.SongEdit_ButtonTopFont.BuddyMenu = null;
		this.SongEdit_ButtonTopFont.Icon = ((System.Drawing.Icon)(resources.GetObject("SongEdit_ButtonTopFont.Icon")));
		this.SongEdit_ButtonTopFont.Tag = null;
		this.SongEdit_ButtonTopFont.Text = "Font";
		// 
		// SongEdit_ButtonTopColor
		// 
		this.SongEdit_ButtonTopColor.BuddyMenu = null;
		this.SongEdit_ButtonTopColor.Icon = ((System.Drawing.Icon)(resources.GetObject("SongEdit_ButtonTopColor.Icon")));
		this.SongEdit_ButtonTopColor.Tag = null;
		this.SongEdit_ButtonTopColor.Text = "Color";
		// 
		// SongEdit_ButtonTopTextOutline
		// 
		this.SongEdit_ButtonTopTextOutline.BeginGroup = true;
		this.SongEdit_ButtonTopTextOutline.BuddyMenu = null;
		this.SongEdit_ButtonTopTextOutline.Icon = ((System.Drawing.Icon)(resources.GetObject("SongEdit_ButtonTopTextOutline.Icon")));
		this.SongEdit_ButtonTopTextOutline.Tag = null;
		this.SongEdit_ButtonTopTextOutline.Text = "TextOutline";
		// 
		// SongEdit_ButtonTopOutlineColor
		// 
		this.SongEdit_ButtonTopOutlineColor.BuddyMenu = null;
		this.SongEdit_ButtonTopOutlineColor.Icon = ((System.Drawing.Icon)(resources.GetObject("SongEdit_ButtonTopOutlineColor.Icon")));
		this.SongEdit_ButtonTopOutlineColor.Tag = null;
		this.SongEdit_ButtonTopOutlineColor.Text = "Outline Color";
		this.SongEdit_ButtonTopOutlineColor.Visible = false;
		// 
		// BibleText_Tab
		// 
		this.BibleText_Tab.Controls.Add(this.BibleText_Results_Panel);
		this.BibleText_Tab.Controls.Add(this.BibleText_Tab_Controls_Panel);
		this.BibleText_Tab.Location = new System.Drawing.Point(4, 24);
		this.BibleText_Tab.Name = "BibleText_Tab";
		this.BibleText_Tab.Size = new System.Drawing.Size(516, 441);
		this.BibleText_Tab.TabIndex = 5;
		this.BibleText_Tab.Text = "Bible Text";
		// 
		// BibleText_Results_Panel
		// 
		this.BibleText_Results_Panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
		this.BibleText_Results_Panel.Controls.Add(this.BibleText_Results);
		this.BibleText_Results_Panel.Controls.Add(this.BibleText_RegEx_ComboBox);
		this.BibleText_Results_Panel.Location = new System.Drawing.Point(0, 0);
		this.BibleText_Results_Panel.Name = "BibleText_Results_Panel";
		this.BibleText_Results_Panel.Size = new System.Drawing.Size(320, 448);
		this.BibleText_Results_Panel.TabIndex = 1;
		// 
		// BibleText_Results
		// 
		this.BibleText_Results.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
		this.BibleText_Results.Cursor = System.Windows.Forms.Cursors.Default;
		this.BibleText_Results.DetectUrls = false;
		this.BibleText_Results.Location = new System.Drawing.Point(0, 24);
		this.BibleText_Results.Name = "BibleText_Results";
		this.BibleText_Results.ReadOnly = true;
		this.BibleText_Results.Size = new System.Drawing.Size(320, 416);
		this.BibleText_Results.TabIndex = 1;
		this.BibleText_Results.Text = "";
		// 
		// BibleText_RegEx_ComboBox
		// 
		this.BibleText_RegEx_ComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
		this.BibleText_RegEx_ComboBox.Location = new System.Drawing.Point(0, 0);
		this.BibleText_RegEx_ComboBox.Name = "BibleText_RegEx_ComboBox";
		this.BibleText_RegEx_ComboBox.Size = new System.Drawing.Size(320, 21);
		this.BibleText_RegEx_ComboBox.TabIndex = 0;
		this.BibleText_RegEx_ComboBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.BibleText_RegEx_ComboBox_KeyUp);
		// 
		// BibleText_Tab_Controls_Panel
		// 
		this.BibleText_Tab_Controls_Panel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Right)));
		this.BibleText_Tab_Controls_Panel.Controls.Add(this.BibleText_Recent_Verses);
		this.BibleText_Tab_Controls_Panel.Controls.Add(this.BibleText_Translations);
		this.BibleText_Tab_Controls_Panel.Location = new System.Drawing.Point(328, 0);
		this.BibleText_Tab_Controls_Panel.Name = "BibleText_Tab_Controls_Panel";
		this.BibleText_Tab_Controls_Panel.Size = new System.Drawing.Size(184, 441);
		this.BibleText_Tab_Controls_Panel.TabIndex = 0;
		// 
		// BibleText_Recent_Verses
		// 
		this.BibleText_Recent_Verses.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Right)));
		this.BibleText_Recent_Verses.BackColor = System.Drawing.Color.AliceBlue;
		this.BibleText_Recent_Verses.EndColour = System.Drawing.Color.FromArgb(((System.Byte)(199)), ((System.Byte)(212)), ((System.Byte)(247)));
		this.BibleText_Recent_Verses.Image = null;
		this.BibleText_Recent_Verses.Location = new System.Drawing.Point(8, 72);
		this.BibleText_Recent_Verses.Name = "BibleText_Recent_Verses";
		this.BibleText_Recent_Verses.PanelState = Salamander.Windows.Forms.PanelState.Expanded;
		this.BibleText_Recent_Verses.Size = new System.Drawing.Size(168, 360);
		this.BibleText_Recent_Verses.StartColour = System.Drawing.Color.White;
		this.BibleText_Recent_Verses.TabIndex = 2;
		this.BibleText_Recent_Verses.TitleFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
		this.BibleText_Recent_Verses.TitleFontColour = System.Drawing.Color.Navy;
		this.BibleText_Recent_Verses.TitleText = "Recent verses";
		// 
		// BibleText_Translations
		// 
		this.BibleText_Translations.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
		this.BibleText_Translations.Location = new System.Drawing.Point(0, 0);
		this.BibleText_Translations.Name = "BibleText_Translations";
		this.BibleText_Translations.Size = new System.Drawing.Size(192, 69);
		this.BibleText_Translations.TabIndex = 0;
		this.BibleText_Translations.SelectedIndexChanged += new System.EventHandler(this.BibleText_Translations_SelectedIndexChanged);
		// 
		// PreviewUpdateTimer
		// 
		this.PreviewUpdateTimer.Interval = 250;
		this.PreviewUpdateTimer.Tick += new System.EventHandler(this.timer1_Tick);
		// 
		// TextTypedTimer
		// 
		this.TextTypedTimer.Enabled = true;
		this.TextTypedTimer.Interval = 5;
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
		this.leftSandDock.Location = new System.Drawing.Point(58, 50);
		this.leftSandDock.Manager = this.sandDockManager1;
		this.leftSandDock.Name = "leftSandDock";
		this.leftSandDock.Size = new System.Drawing.Size(0, 491);
		this.leftSandDock.TabIndex = 23;
		// 
		// rightSandDock
		// 
		this.rightSandDock.Controls.Add(this.RightDocks_TopPanel_Songs);
		this.rightSandDock.Controls.Add(this.RightDocks_TopPanel_PlayList);
		this.rightSandDock.Controls.Add(this.RightDocks_TopPanel_Search);
		this.rightSandDock.Controls.Add(this.RightDocks_BottomPanel_Backgrounds);
		this.rightSandDock.Controls.Add(this.RightDocks_BottomPanel_Media);
		this.rightSandDock.Controls.Add(this.RightDocks_BottomPanel_MediaLists);
		this.rightSandDock.Dock = System.Windows.Forms.DockStyle.Right;
		this.rightSandDock.Guid = new System.Guid("a6039876-f9a8-471e-b56f-5b1bf7264f06");
		this.rightSandDock.Location = new System.Drawing.Point(582, 50);
		this.rightSandDock.Manager = this.sandDockManager1;
		this.rightSandDock.Name = "rightSandDock";
		this.rightSandDock.Size = new System.Drawing.Size(200, 491);
		this.rightSandDock.TabIndex = 24;
		// 
		// RightDocks_TopPanel_Songs
		// 
		this.RightDocks_TopPanel_Songs.Closable = false;
		this.RightDocks_TopPanel_Songs.Controls.Add(this.RightDocks_SongList);
		this.RightDocks_TopPanel_Songs.Controls.Add(this.RightDocks_Songlist_SearchPanel);
		this.RightDocks_TopPanel_Songs.Controls.Add(this.RightDocks_SongList_ButtonPanel);
		this.RightDocks_TopPanel_Songs.Guid = new System.Guid("6044bb67-05ab-4617-bbfa-99e49388b41f");
		this.RightDocks_TopPanel_Songs.Location = new System.Drawing.Point(4, 25);
		this.RightDocks_TopPanel_Songs.Name = "RightDocks_TopPanel_Songs";
		this.RightDocks_TopPanel_Songs.Size = new System.Drawing.Size(196, 159);
		this.RightDocks_TopPanel_Songs.TabImage = ((System.Drawing.Image)(resources.GetObject("RightDocks_TopPanel_Songs.TabImage")));
		this.RightDocks_TopPanel_Songs.TabIndex = 0;
		this.RightDocks_TopPanel_Songs.Text = "Songs";
		// 
		// RightDocks_TopPanel_PlayList
		// 
		this.RightDocks_TopPanel_PlayList.Closable = false;
		this.RightDocks_TopPanel_PlayList.Controls.Add(this.RightDocks_PlayList);
		this.RightDocks_TopPanel_PlayList.Controls.Add(this.RightDocks_TopPanel_PlayList_Button_Panel);
		this.RightDocks_TopPanel_PlayList.Guid = new System.Guid("92186926-e7f9-4850-98b8-190a99f81ea6");
		this.RightDocks_TopPanel_PlayList.Location = new System.Drawing.Point(4, 25);
		this.RightDocks_TopPanel_PlayList.Name = "RightDocks_TopPanel_PlayList";
		this.RightDocks_TopPanel_PlayList.Size = new System.Drawing.Size(196, 159);
		this.RightDocks_TopPanel_PlayList.TabImage = ((System.Drawing.Image)(resources.GetObject("RightDocks_TopPanel_PlayList.TabImage")));
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
		this.RightDocks_TopPanel_Search.Location = new System.Drawing.Point(4, 25);
		this.RightDocks_TopPanel_Search.Name = "RightDocks_TopPanel_Search";
		this.RightDocks_TopPanel_Search.Size = new System.Drawing.Size(196, 159);
		this.RightDocks_TopPanel_Search.TabImage = ((System.Drawing.Image)(resources.GetObject("RightDocks_TopPanel_Search.TabImage")));
		this.RightDocks_TopPanel_Search.TabIndex = 3;
		this.RightDocks_TopPanel_Search.Text = "Search";
		// 
		// RightDocks_BottomPanel_Backgrounds
		// 
		this.RightDocks_BottomPanel_Backgrounds.Closable = false;
		this.RightDocks_BottomPanel_Backgrounds.Controls.Add(this.RightDocks_ImageListBox);
		this.RightDocks_BottomPanel_Backgrounds.Controls.Add(this.RightDocks_BottomPanel2_TopPanel);
		this.RightDocks_BottomPanel_Backgrounds.Guid = new System.Guid("b561dd7f-3e79-4e07-912c-18ac9600db75");
		this.RightDocks_BottomPanel_Backgrounds.Location = new System.Drawing.Point(4, 236);
		this.RightDocks_BottomPanel_Backgrounds.Name = "RightDocks_BottomPanel_Backgrounds";
		this.RightDocks_BottomPanel_Backgrounds.Size = new System.Drawing.Size(196, 232);
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
		this.RightDocks_BottomPanel_Media.Closable = false;
		this.RightDocks_BottomPanel_Media.Controls.Add(this.RightDocks_BottomPanel_MediaList);
		this.RightDocks_BottomPanel_Media.Controls.Add(this.RightDocks_BottomPanel_Media_Bottom);
		this.RightDocks_BottomPanel_Media.Controls.Add(this.RightDocks_BottomPanel_Media_Top);
		this.RightDocks_BottomPanel_Media.Guid = new System.Guid("c9d617ca-0165-45b7-8e07-329a81273abc");
		this.RightDocks_BottomPanel_Media.Location = new System.Drawing.Point(4, 236);
		this.RightDocks_BottomPanel_Media.Name = "RightDocks_BottomPanel_Media";
		this.RightDocks_BottomPanel_Media.Size = new System.Drawing.Size(196, 232);
		this.RightDocks_BottomPanel_Media.TabIndex = 4;
		this.RightDocks_BottomPanel_Media.Text = "Media";
		// 
		// RightDocks_BottomPanel_MediaList
		// 
		this.RightDocks_BottomPanel_MediaList.AllowDrop = true;
		this.RightDocks_BottomPanel_MediaList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.RightDocks_BottomPanel_MediaList.Dock = System.Windows.Forms.DockStyle.Fill;
		this.RightDocks_BottomPanel_MediaList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
		this.RightDocks_BottomPanel_MediaList.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
		this.RightDocks_BottomPanel_MediaList.ImageList = this.Media_ImageList;
		this.RightDocks_BottomPanel_MediaList.ItemHeight = 76;
		this.RightDocks_BottomPanel_MediaList.Location = new System.Drawing.Point(0, 32);
		this.RightDocks_BottomPanel_MediaList.Name = "RightDocks_BottomPanel_MediaList";
		this.RightDocks_BottomPanel_MediaList.Size = new System.Drawing.Size(196, 78);
		this.RightDocks_BottomPanel_MediaList.TabIndex = 3;
		this.RightDocks_BottomPanel_MediaList.DoubleClick += new System.EventHandler(this.RightDocks_BottomPanel_MediaList_DoubleClick);
		this.RightDocks_BottomPanel_MediaList.DragDrop += new System.Windows.Forms.DragEventHandler(this.RightDocks_BottomPanel_MediaList_DragDrop);
		this.RightDocks_BottomPanel_MediaList.SelectedIndexChanged += new System.EventHandler(this.RightDocks_BottomPanel_MediaList_SelectedIndexChanged);
		this.RightDocks_BottomPanel_MediaList.DragEnter += new System.Windows.Forms.DragEventHandler(this.RightDocks_BottomPanel_MediaList_DragEnter);
		// 
		// Media_ImageList
		// 
		this.Media_ImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
		this.Media_ImageList.ImageSize = new System.Drawing.Size(100, 75);
		this.Media_ImageList.TransparentColor = System.Drawing.Color.Transparent;
		// 
		// RightDocks_BottomPanel_Media_Bottom
		// 
		this.RightDocks_BottomPanel_Media_Bottom.Controls.Add(this.RightDocks_BottomPanel_Media_AutoPlay);
		this.RightDocks_BottomPanel_Media_Bottom.Controls.Add(this.RightDocks_BottomPanel_Media_Remove);
		this.RightDocks_BottomPanel_Media_Bottom.Controls.Add(this.RightDocks_BottomPanel_Media_Down);
		this.RightDocks_BottomPanel_Media_Bottom.Controls.Add(this.RightDocks_BottomPanel_Media_Up);
		this.RightDocks_BottomPanel_Media_Bottom.Controls.Add(this.RightDocks_BottomPanel_Media_ShowNext);
		this.RightDocks_BottomPanel_Media_Bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.RightDocks_BottomPanel_Media_Bottom.Location = new System.Drawing.Point(0, 176);
		this.RightDocks_BottomPanel_Media_Bottom.Name = "RightDocks_BottomPanel_Media_Bottom";
		this.RightDocks_BottomPanel_Media_Bottom.Size = new System.Drawing.Size(196, 56);
		this.RightDocks_BottomPanel_Media_Bottom.TabIndex = 5;
		// 
		// RightDocks_BottomPanel_Media_AutoPlay
		// 
		this.RightDocks_BottomPanel_Media_AutoPlay.FlatStyle = System.Windows.Forms.FlatStyle.System;
		this.RightDocks_BottomPanel_Media_AutoPlay.Location = new System.Drawing.Point(104, 32);
		this.RightDocks_BottomPanel_Media_AutoPlay.Name = "RightDocks_BottomPanel_Media_AutoPlay";
		this.RightDocks_BottomPanel_Media_AutoPlay.TabIndex = 11;
		this.RightDocks_BottomPanel_Media_AutoPlay.Text = "Auto Play";
		this.RightDocks_BottomPanel_Media_AutoPlay.Click += new System.EventHandler(this.RightDocks_BottomPanel_Media_AutoPlay_Click);
		// 
		// RightDocks_BottomPanel_Media_Remove
		// 
		this.RightDocks_BottomPanel_Media_Remove.FlatStyle = System.Windows.Forms.FlatStyle.System;
		this.RightDocks_BottomPanel_Media_Remove.Location = new System.Drawing.Point(56, 8);
		this.RightDocks_BottomPanel_Media_Remove.Name = "RightDocks_BottomPanel_Media_Remove";
		this.RightDocks_BottomPanel_Media_Remove.Size = new System.Drawing.Size(56, 18);
		this.RightDocks_BottomPanel_Media_Remove.TabIndex = 10;
		this.RightDocks_BottomPanel_Media_Remove.Text = "Remove";
		this.RightDocks_BottomPanel_Media_Remove.Click += new System.EventHandler(this.RightDocks_BottomPanel_Media_Remove_Click);
		// 
		// RightDocks_BottomPanel_Media_Down
		// 
		this.RightDocks_BottomPanel_Media_Down.FlatStyle = System.Windows.Forms.FlatStyle.System;
		this.RightDocks_BottomPanel_Media_Down.Location = new System.Drawing.Point(152, 8);
		this.RightDocks_BottomPanel_Media_Down.Name = "RightDocks_BottomPanel_Media_Down";
		this.RightDocks_BottomPanel_Media_Down.Size = new System.Drawing.Size(40, 18);
		this.RightDocks_BottomPanel_Media_Down.TabIndex = 9;
		this.RightDocks_BottomPanel_Media_Down.Text = "down";
		this.RightDocks_BottomPanel_Media_Down.Click += new System.EventHandler(this.RightDocks_BottomPanel_Media_Down_Click);
		// 
		// RightDocks_BottomPanel_Media_Up
		// 
		this.RightDocks_BottomPanel_Media_Up.FlatStyle = System.Windows.Forms.FlatStyle.System;
		this.RightDocks_BottomPanel_Media_Up.Location = new System.Drawing.Point(112, 8);
		this.RightDocks_BottomPanel_Media_Up.Name = "RightDocks_BottomPanel_Media_Up";
		this.RightDocks_BottomPanel_Media_Up.Size = new System.Drawing.Size(40, 18);
		this.RightDocks_BottomPanel_Media_Up.TabIndex = 8;
		this.RightDocks_BottomPanel_Media_Up.Text = "up";
		this.RightDocks_BottomPanel_Media_Up.Click += new System.EventHandler(this.RightDocks_BottomPanel_Media_Up_Click);
		// 
		// RightDocks_BottomPanel_Media_ShowNext
		// 
		this.RightDocks_BottomPanel_Media_ShowNext.FlatStyle = System.Windows.Forms.FlatStyle.System;
		this.RightDocks_BottomPanel_Media_ShowNext.Location = new System.Drawing.Point(16, 32);
		this.RightDocks_BottomPanel_Media_ShowNext.Name = "RightDocks_BottomPanel_Media_ShowNext";
		this.RightDocks_BottomPanel_Media_ShowNext.TabIndex = 0;
		this.RightDocks_BottomPanel_Media_ShowNext.Text = "Show&&Next";
		this.RightDocks_BottomPanel_Media_ShowNext.Click += new System.EventHandler(this.RightDocks_BottomPanel_Media_ShowNext_Click);
		// 
		// RightDocks_BottomPanel_Media_Top
		// 
		this.RightDocks_BottomPanel_Media_Top.Controls.Add(this.RightDocks_BottomPanel_Media_FadePanelButton);
		this.RightDocks_BottomPanel_Media_Top.Dock = System.Windows.Forms.DockStyle.Top;
		this.RightDocks_BottomPanel_Media_Top.Location = new System.Drawing.Point(0, 0);
		this.RightDocks_BottomPanel_Media_Top.Name = "RightDocks_BottomPanel_Media_Top";
		this.RightDocks_BottomPanel_Media_Top.Size = new System.Drawing.Size(196, 32);
		this.RightDocks_BottomPanel_Media_Top.TabIndex = 4;
		// 
		// RightDocks_BottomPanel_Media_FadePanelButton
		// 
		this.RightDocks_BottomPanel_Media_FadePanelButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
		this.RightDocks_BottomPanel_Media_FadePanelButton.Location = new System.Drawing.Point(8, 4);
		this.RightDocks_BottomPanel_Media_FadePanelButton.Name = "RightDocks_BottomPanel_Media_FadePanelButton";
		this.RightDocks_BottomPanel_Media_FadePanelButton.Size = new System.Drawing.Size(112, 23);
		this.RightDocks_BottomPanel_Media_FadePanelButton.TabIndex = 2;
		this.RightDocks_BottomPanel_Media_FadePanelButton.Text = "Add Media...";
		this.RightDocks_BottomPanel_Media_FadePanelButton.Click += new System.EventHandler(this.RightDocks_BottomPanel_Media_FadePanelButton_Click);
		// 
		// RightDocks_BottomPanel_MediaLists
		// 
		this.RightDocks_BottomPanel_MediaLists.Closable = false;
		this.RightDocks_BottomPanel_MediaLists.Controls.Add(this.RightDocks_BottomPanel_MediaListsTopPanel);
		this.RightDocks_BottomPanel_MediaLists.Controls.Add(this.RightDocks_BottomPanel_MediaLists_BottomPanel);
		this.RightDocks_BottomPanel_MediaLists.Guid = new System.Guid("3429dfd5-f5ba-4785-ac79-49140d88b66b");
		this.RightDocks_BottomPanel_MediaLists.Location = new System.Drawing.Point(4, 236);
		this.RightDocks_BottomPanel_MediaLists.Name = "RightDocks_BottomPanel_MediaLists";
		this.RightDocks_BottomPanel_MediaLists.Size = new System.Drawing.Size(196, 232);
		this.RightDocks_BottomPanel_MediaLists.TabIndex = 5;
		this.RightDocks_BottomPanel_MediaLists.Text = "MediaLists";
		// 
		// RightDocks_BottomPanel_MediaListsTopPanel
		// 
		this.RightDocks_BottomPanel_MediaListsTopPanel.Controls.Add(this.RightDocks_MediaLists);
		this.RightDocks_BottomPanel_MediaListsTopPanel.Controls.Add(this.RightDocks_BottomPanel_MediaListsTop_Control_Panel);
		this.RightDocks_BottomPanel_MediaListsTopPanel.Dock = System.Windows.Forms.DockStyle.Fill;
		this.RightDocks_BottomPanel_MediaListsTopPanel.Location = new System.Drawing.Point(0, 0);
		this.RightDocks_BottomPanel_MediaListsTopPanel.Name = "RightDocks_BottomPanel_MediaListsTopPanel";
		this.RightDocks_BottomPanel_MediaListsTopPanel.Size = new System.Drawing.Size(196, 132);
		this.RightDocks_BottomPanel_MediaListsTopPanel.TabIndex = 1;
		// 
		// RightDocks_MediaLists
		// 
		this.RightDocks_MediaLists.Dock = System.Windows.Forms.DockStyle.Fill;
		this.RightDocks_MediaLists.Location = new System.Drawing.Point(0, 0);
		this.RightDocks_MediaLists.Name = "RightDocks_MediaLists";
		this.RightDocks_MediaLists.Size = new System.Drawing.Size(196, 108);
		this.RightDocks_MediaLists.TabIndex = 1;
		this.RightDocks_MediaLists.DoubleClick += new System.EventHandler(this.RightDocks_MediaLists_DoubleClick);
		// 
		// RightDocks_BottomPanel_MediaListsTop_Control_Panel
		// 
		this.RightDocks_BottomPanel_MediaListsTop_Control_Panel.Controls.Add(this.RightDocks_MediaLists_DeleteButton);
		this.RightDocks_BottomPanel_MediaListsTop_Control_Panel.Controls.Add(this.RightDocks_MediaLists_LoadButton);
		this.RightDocks_BottomPanel_MediaListsTop_Control_Panel.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.RightDocks_BottomPanel_MediaListsTop_Control_Panel.Location = new System.Drawing.Point(0, 108);
		this.RightDocks_BottomPanel_MediaListsTop_Control_Panel.Name = "RightDocks_BottomPanel_MediaListsTop_Control_Panel";
		this.RightDocks_BottomPanel_MediaListsTop_Control_Panel.Size = new System.Drawing.Size(196, 24);
		this.RightDocks_BottomPanel_MediaListsTop_Control_Panel.TabIndex = 0;
		// 
		// RightDocks_MediaLists_DeleteButton
		// 
		this.RightDocks_MediaLists_DeleteButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
		this.RightDocks_MediaLists_DeleteButton.Location = new System.Drawing.Point(66, 1);
		this.RightDocks_MediaLists_DeleteButton.Name = "RightDocks_MediaLists_DeleteButton";
		this.RightDocks_MediaLists_DeleteButton.Size = new System.Drawing.Size(64, 18);
		this.RightDocks_MediaLists_DeleteButton.TabIndex = 5;
		this.RightDocks_MediaLists_DeleteButton.Text = "Delete";
		this.RightDocks_MediaLists_DeleteButton.Click += new System.EventHandler(this.RightDocks_MediaLists_DeleteButton_Click);
		// 
		// RightDocks_MediaLists_LoadButton
		// 
		this.RightDocks_MediaLists_LoadButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
		this.RightDocks_MediaLists_LoadButton.Location = new System.Drawing.Point(2, 1);
		this.RightDocks_MediaLists_LoadButton.Name = "RightDocks_MediaLists_LoadButton";
		this.RightDocks_MediaLists_LoadButton.Size = new System.Drawing.Size(64, 18);
		this.RightDocks_MediaLists_LoadButton.TabIndex = 4;
		this.RightDocks_MediaLists_LoadButton.Text = "<-Load";
		this.RightDocks_MediaLists_LoadButton.Click += new System.EventHandler(this.RightDocks_MediaLists_LoadButton_Click);
		// 
		// RightDocks_BottomPanel_MediaLists_BottomPanel
		// 
		this.RightDocks_BottomPanel_MediaLists_BottomPanel.Controls.Add(this.RightDocks_BottomPanel_MediaListsBottomPanel_GroupBox);
		this.RightDocks_BottomPanel_MediaLists_BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.RightDocks_BottomPanel_MediaLists_BottomPanel.Location = new System.Drawing.Point(0, 132);
		this.RightDocks_BottomPanel_MediaLists_BottomPanel.Name = "RightDocks_BottomPanel_MediaLists_BottomPanel";
		this.RightDocks_BottomPanel_MediaLists_BottomPanel.Size = new System.Drawing.Size(196, 100);
		this.RightDocks_BottomPanel_MediaLists_BottomPanel.TabIndex = 0;
		// 
		// RightDocks_BottomPanel_MediaListsBottomPanel_GroupBox
		// 
		this.RightDocks_BottomPanel_MediaListsBottomPanel_GroupBox.Controls.Add(this.RightDocks_BottomPanel_MediaLists_LoopCheckBox);
		this.RightDocks_BottomPanel_MediaListsBottomPanel_GroupBox.Controls.Add(this.RightDocks_BottomPanel_MediaLists_BottomPanel_Label2);
		this.RightDocks_BottomPanel_MediaListsBottomPanel_GroupBox.Controls.Add(this.RightDocks_BottomPanel_MediaLists_Numeric);
		this.RightDocks_BottomPanel_MediaListsBottomPanel_GroupBox.Controls.Add(this.RightDocks_BottomPanel_MediaList_BottomPanel_Label);
		this.RightDocks_BottomPanel_MediaListsBottomPanel_GroupBox.Location = new System.Drawing.Point(32, 7);
		this.RightDocks_BottomPanel_MediaListsBottomPanel_GroupBox.Name = "RightDocks_BottomPanel_MediaListsBottomPanel_GroupBox";
		this.RightDocks_BottomPanel_MediaListsBottomPanel_GroupBox.Size = new System.Drawing.Size(142, 89);
		this.RightDocks_BottomPanel_MediaListsBottomPanel_GroupBox.TabIndex = 0;
		this.RightDocks_BottomPanel_MediaListsBottomPanel_GroupBox.TabStop = false;
		this.RightDocks_BottomPanel_MediaListsBottomPanel_GroupBox.Text = "Set Auto Play -Timer";
		// 
		// RightDocks_BottomPanel_MediaLists_LoopCheckBox
		// 
		this.RightDocks_BottomPanel_MediaLists_LoopCheckBox.Location = new System.Drawing.Point(8, 60);
		this.RightDocks_BottomPanel_MediaLists_LoopCheckBox.Name = "RightDocks_BottomPanel_MediaLists_LoopCheckBox";
		this.RightDocks_BottomPanel_MediaLists_LoopCheckBox.Size = new System.Drawing.Size(128, 24);
		this.RightDocks_BottomPanel_MediaLists_LoopCheckBox.TabIndex = 3;
		this.RightDocks_BottomPanel_MediaLists_LoopCheckBox.Text = "Loop MediaList";
		// 
		// RightDocks_BottomPanel_MediaLists_BottomPanel_Label2
		// 
		this.RightDocks_BottomPanel_MediaLists_BottomPanel_Label2.Location = new System.Drawing.Point(72, 43);
		this.RightDocks_BottomPanel_MediaLists_BottomPanel_Label2.Name = "RightDocks_BottomPanel_MediaLists_BottomPanel_Label2";
		this.RightDocks_BottomPanel_MediaLists_BottomPanel_Label2.Size = new System.Drawing.Size(64, 16);
		this.RightDocks_BottomPanel_MediaLists_BottomPanel_Label2.TabIndex = 2;
		this.RightDocks_BottomPanel_MediaLists_BottomPanel_Label2.Text = "seconds.";
		// 
		// RightDocks_BottomPanel_MediaLists_Numeric
		// 
		this.RightDocks_BottomPanel_MediaLists_Numeric.Location = new System.Drawing.Point(8, 40);
		this.RightDocks_BottomPanel_MediaLists_Numeric.Maximum = new System.Decimal(new int[] {
																								  1000000000,
																								  0,
																								  0,
																								  0});
		this.RightDocks_BottomPanel_MediaLists_Numeric.Minimum = new System.Decimal(new int[] {
																								  1,
																								  0,
																								  0,
																								  0});
		this.RightDocks_BottomPanel_MediaLists_Numeric.Name = "RightDocks_BottomPanel_MediaLists_Numeric";
		this.RightDocks_BottomPanel_MediaLists_Numeric.Size = new System.Drawing.Size(64, 20);
		this.RightDocks_BottomPanel_MediaLists_Numeric.TabIndex = 1;
		this.RightDocks_BottomPanel_MediaLists_Numeric.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
		this.RightDocks_BottomPanel_MediaLists_Numeric.Value = new System.Decimal(new int[] {
																								1,
																								0,
																								0,
																								0});
		this.RightDocks_BottomPanel_MediaLists_Numeric.ValueChanged += new System.EventHandler(this.RightDocks_BottomPanel_MediaLists_Numeric_ValueChanged);
		// 
		// RightDocks_BottomPanel_MediaList_BottomPanel_Label
		// 
		this.RightDocks_BottomPanel_MediaList_BottomPanel_Label.Location = new System.Drawing.Point(4, 16);
		this.RightDocks_BottomPanel_MediaList_BottomPanel_Label.Name = "RightDocks_BottomPanel_MediaList_BottomPanel_Label";
		this.RightDocks_BottomPanel_MediaList_BottomPanel_Label.Size = new System.Drawing.Size(135, 24);
		this.RightDocks_BottomPanel_MediaList_BottomPanel_Label.TabIndex = 0;
		this.RightDocks_BottomPanel_MediaList_BottomPanel_Label.Text = "Change Media Item after:";
		// 
		// bottomSandDock
		// 
		this.bottomSandDock.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.bottomSandDock.Guid = new System.Guid("11832edf-4b5f-4911-9e7c-da764193d89f");
		this.bottomSandDock.Location = new System.Drawing.Point(58, 541);
		this.bottomSandDock.Manager = this.sandDockManager1;
		this.bottomSandDock.Name = "bottomSandDock";
		this.bottomSandDock.Size = new System.Drawing.Size(524, 0);
		this.bottomSandDock.TabIndex = 25;
		// 
		// topSandDock
		// 
		this.topSandDock.Dock = System.Windows.Forms.DockStyle.Top;
		this.topSandDock.Guid = new System.Guid("f3084065-73e3-4825-a957-0918e3006a24");
		this.topSandDock.Location = new System.Drawing.Point(58, 50);
		this.topSandDock.Manager = this.sandDockManager1;
		this.topSandDock.Name = "topSandDock";
		this.topSandDock.Size = new System.Drawing.Size(524, 0);
		this.topSandDock.TabIndex = 26;
		// 
		// Media_Logos
		// 
		this.Media_Logos.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
		this.Media_Logos.ImageSize = new System.Drawing.Size(100, 75);
		this.Media_Logos.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("Media_Logos.ImageStream")));
		this.Media_Logos.TransparentColor = System.Drawing.Color.Transparent;
		// 
		// PlayProgress
		// 
		this.PlayProgress.Interval = 1000;
		this.PlayProgress.Tick += new System.EventHandler(this.PlayProgress_Tick);
		// 
		// VideoLoadTimer
		// 
		this.VideoLoadTimer.Tick += new System.EventHandler(this.VideoLoadTimer_Tick);
		// 
		// Presentation_AutoPlayTimer
		// 
		this.Presentation_AutoPlayTimer.Tick += new System.EventHandler(this.Presentation_AutoPlayTimer_Tick);
		// 
		// buttonItem1
		// 
		this.buttonItem1.BuddyMenu = null;
		this.buttonItem1.Icon = null;
		this.buttonItem1.IconSize = new System.Drawing.Size(47, 47);
		this.buttonItem1.Tag = null;
		this.buttonItem1.ToolTipText = "Sermon Tool";
		// 
		// MainForm
		// 
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
		this.BackColor = System.Drawing.SystemColors.Control;
		this.ClientSize = new System.Drawing.Size(782, 541);
		this.Controls.Add(this.tabControl1);
		this.Controls.Add(this.statusBar);
		this.Controls.Add(this.leftSandDock);
		this.Controls.Add(this.bottomSandDock);
		this.Controls.Add(this.topSandDock);
		this.Controls.Add(this.rightSandDock);
		this.Controls.Add(this.ToolBars_leftSandBarDock);
		this.Controls.Add(this.ToolBars_bottomSandBarDock);
		this.Controls.Add(this.ToolBars_topSandBarDock);
		this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
		this.ForeColor = System.Drawing.SystemColors.ControlText;
		this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
		this.KeyPreview = true;
		this.Location = new System.Drawing.Point(50, 0);
		this.Name = "MainForm";
		this.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "DreamBeam";
		this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
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
		this.statusBar.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)(this.StatusPanel)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.statusBarUpdatePanel)).EndInit();
		this.tabControl1.ResumeLayout(false);
		this.tabPage0.ResumeLayout(false);
		this.SongShow_Right_Panel.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)(this.SongShow_CollapsPanel)).EndInit();
		this.SongShow_CollapsPanel.ResumeLayout(false);
		this.SongShow_HideElementsPanel.ResumeLayout(false);
		this.panel3.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
		this.SongShow_HideElementsSub1Panel.ResumeLayout(false);
		this.SongShow_BackgroundPanel.ResumeLayout(false);
		this.tabPage2.ResumeLayout(false);
		this.Sermon_LeftPanel.ResumeLayout(false);
		this.Sermon_LeftBottom_Panel.ResumeLayout(false);
		this.Sermon_LeftDoc_Panel.ResumeLayout(false);
		this.Sermon_LeftToolBar_Panel.ResumeLayout(false);
		this.Sermon_TabControl.ResumeLayout(false);
		this.tabPage3.ResumeLayout(false);
		this.tabPage4.ResumeLayout(false);
		this.Presentation_FadePanel.ResumeLayout(false);
		this.Fade_panel.ResumeLayout(false);
		this.Fade_Top_Panel.ResumeLayout(false);
		this.Presentation_MainPanel.ResumeLayout(false);
		this.Presentation_PreviewPanel.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)(this.axShockwaveFlash)).EndInit();
		this.Presentation_MovieControlPanel.ResumeLayout(false);
		this.Presentation_MovieControlPanelBottom.ResumeLayout(false);
		this.Presentation_MovieControl_PreviewButtonPanel.ResumeLayout(false);
		this.Presentation_MovieControlPanelBottomLeft.ResumeLayout(false);
		this.Presentation_MovieControlPanel_Top.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)(this.Media_TrackBar)).EndInit();
		this.Presentation_MovieControlPanel_Right.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)(this.AudioBar)).EndInit();
		this.tabPage1.ResumeLayout(false);
		this.SongEdit_RightPanel.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)(this.collapsiblePanelBar1)).EndInit();
		this.collapsiblePanelBar1.ResumeLayout(false);
		this.SongEdit_MultiLangPanel.ResumeLayout(false);
		this.SongEdit_TextAlignPanel.ResumeLayout(false);
		this.SongEdit_BackgroundPanel.ResumeLayout(false);
		this.panel4.ResumeLayout(false);
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
		this.BibleText_Tab.ResumeLayout(false);
		this.BibleText_Results_Panel.ResumeLayout(false);
		this.BibleText_Tab_Controls_Panel.ResumeLayout(false);
		this.rightSandDock.ResumeLayout(false);
		this.RightDocks_TopPanel_Songs.ResumeLayout(false);
		this.RightDocks_TopPanel_PlayList.ResumeLayout(false);
		this.RightDocks_TopPanel_Search.ResumeLayout(false);
		this.RightDocks_BottomPanel_Backgrounds.ResumeLayout(false);
		this.RightDocks_BottomPanel2_TopPanel.ResumeLayout(false);
		this.RightDocks_BottomPanel_Media.ResumeLayout(false);
		this.RightDocks_BottomPanel_Media_Bottom.ResumeLayout(false);
		this.RightDocks_BottomPanel_Media_Top.ResumeLayout(false);
		this.RightDocks_BottomPanel_MediaLists.ResumeLayout(false);
		this.RightDocks_BottomPanel_MediaListsTopPanel.ResumeLayout(false);
		this.RightDocks_BottomPanel_MediaListsTop_Control_Panel.ResumeLayout(false);
		this.RightDocks_BottomPanel_MediaLists_BottomPanel.ResumeLayout(false);
		this.RightDocks_BottomPanel_MediaListsBottomPanel_GroupBox.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)(this.RightDocks_BottomPanel_MediaLists_Numeric)).EndInit();
		this.ResumeLayout(false);

	}
#endregion


	#region Methods and Events

		#region Inits
		   #region Diatheke
				private void InitializeDiatheke() {
					if (SwordProject_Found) {
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
					} else {
						Sermon_BibleKey.Enabled = false;
						Sermon_Books.Enabled = false;
						Sermon_Books_Label.Enabled = false;
						Sermon_Testament_ListBox.Enabled = false;
						Sermon_BookList.Enabled = false;
					}
				}
		#endregion

		   #region Language
				private void SetLanguage() {

				#region MainWindow
					#region Menu
						ToolBars_MenuBar_Song.Text = Lang.say("Menu.Song");
						ToolBars_MenuBar_Song_New.Text = Lang.say("Menu.Song.New");
						ToolBars_MenuBar_Song_Save.Text = Lang.say("Menu.Song.Save");
						ToolBars_MenuBar_Song_SaveAs.Text = Lang.say("Menu.Song.SaveAs");
						ToolBars_MenuBar_Song_Rename.Text = Lang.say("Menu.Song.Rename");
						ToolBars_MenuBar_Song_Exit.Text = Lang.say("Menu.Song.Exit");

						ToolBars_MenuBar_MediaList.Text = Lang.say("Menu.MediaList");
						ToolBars_MenuBar_MediaList_New.Text = Lang.say("Menu.MediaList.New");
						ToolBars_MenuBar_Media_Save.Text = Lang.say("Menu.MediaList.Save");
						ToolBars_MenuBar_Media_SaveAs.Text = Lang.say("Menu.MediaList.SaveAs");
						ToolBars_MenuBar_Media_Rename.Text = Lang.say("Menu.MediaList.Rename");
						ToolBars_MenuBar_MediaList_Exit.Text = Lang.say("Menu.MediaList.Exit");

						ToolBars_MenuBar_Edit.Text = Lang.say("Menu.Edit");
						ToolBars_MenuBar_Edit_Options.Text = Lang.say("Menu.Edit.Options");
						ToolBars_MenuBar_View.Text = Lang.say("Menu.View");

						ToolBars_MenuBar_Help.Text = Lang.say("Menu.Help");
						HelpGetToKnow.Text = Lang.say("Menu.Help.GetToKnow");
						HelpIntro.Text = Lang.say("Menu.Help.Intro");
						HelpBeamBox.Text = Lang.say("Menu.Help.ProjectorWindow");
						HelpOptions.Text = Lang.say("Menu.Help.Options");
						HelpComponents.Text = Lang.say("Menu.Help.Components");
						HelpShowSongs.Text = Lang.say("Menu.Help.ShowSongs");
						HelpEditSongs.Text = Lang.say("Menu.Help.EditSongs");
						HelpPresentation.Text = Lang.say("Menu.Help.Presentation");
						HelpTextTool.Text = Lang.say("Menu.Help.TextTool");
						AboutButton.Text = Lang.say("Menu.Help.About");
					#endregion

					#region Right
						//Songs
						RightDocks_TopPanel_Songs.Text = Lang.say("Right.Songs");
						btnRightDocks_SongListLoad.Text = Lang.say("Right.Songs.Load");
						btnRightDocks_SongListDelete.Text = Lang.say("Right.Songs.Delete");
						btnRightDocks_SongList2PlayList.Text = Lang.say("Right.Songs.Playlist");

						//Playlist
						RightDocks_TopPanel_PlayList.Text = Lang.say("Right.Playlist");
						RightDocks_PlayList_Load_Button.Text = Lang.say("Right.Playlist.Load");
						RightDocks_PlayList_Remove_Button.Text = Lang.say("Right.Playlist.Remove");
						RightDocks_PlayList_Up_Button.Text = Lang.say("Right.Playlist.Up");
						RightDocks_PlayList_Down_Button.Text = Lang.say("Right.Playlist.Down");

						//Search
						RightDocks_TopPanel_Search.Text = Lang.say("Right.Search");
						RightDocks_TopPanel_Search_RadioButton1.Text = Lang.say("Right.Search.sAllSongs");
						RightDocks_TopPanel_Search_RadioButton2.Text = Lang.say("Right.Search.sPlaylist");
						RightDocks_Search_DropDown.Items.Clear();
						RightDocks_Search_DropDown.Items.Add(Lang.say("Right.Search.Words"));
						RightDocks_Search_DropDown.Items.Add(Lang.say("Right.Search.Phrase"));
						RightDocks_Search_LoadButton.Text = Lang.say("Right.Search.Load");
						RightDocks_Search_PlaylistButton.Text = Lang.say("Right.Search.Playlist");
						RightDocks_Search_SearchButton.Text = Lang.say("Right.Search.Search");

						//Backgrounds
						RightDocks_BottomPanel_Backgrounds.Text = Lang.say("Right.Backgrounds");

						//Media
						RightDocks_BottomPanel_Media.Text = Lang.say("Right.Media");
						RightDocks_BottomPanel_Media_FadePanelButton.Text = Lang.say("Right.Media.Add");
						RightDocks_BottomPanel_Media_Remove.Text = Lang.say("Right.Media.Remove");
						RightDocks_BottomPanel_Media_Up.Text = Lang.say("Right.Media.Up");
						RightDocks_BottomPanel_Media_Down.Text = Lang.say("Right.Media.Down");
						RightDocks_BottomPanel_Media_ShowNext.Text = Lang.say("Right.Media.Show");
						RightDocks_BottomPanel_Media_AutoPlay.Text = Lang.say("Right.Media.AutoPlay");

						RightDocks_MediaLists_LoadButton.Text = Lang.say("Right.MediaLists.Load");
						RightDocks_MediaLists_DeleteButton.Text = Lang.say("Right.MediaLists.Delete");
						RightDocks_BottomPanel_MediaListsBottomPanel_GroupBox.Text = Lang.say("Right.MediaLists.TimerCaption");
						RightDocks_BottomPanel_MediaList_BottomPanel_Label.Text = Lang.say ("Right.MediaLists.TimerText");
						RightDocks_BottomPanel_MediaLists_BottomPanel_Label2.Text = Lang.say ("Right.MediaLists.Seconds");
						RightDocks_BottomPanel_MediaLists_LoopCheckBox.Text = Lang.say ("Right.MediaLists.Loop");

					#endregion

					#region Left
						ShowSongs_Button.ToolTipText = Lang.say("TabPages.ShowSongs");
						EditSongs_Button.ToolTipText = Lang.say("TabPages.EditSongs");
						Presentation_Button.ToolTipText = Lang.say("TabPages.Presentation");
						Sermon_Button.ToolTipText = Lang.say("TabPages.TextTool");
					#endregion

					#region TabPages
						tabPage0.Text = Lang.say("TabPages.ShowSongs");
						tabPage1.Text = Lang.say("TabPages.EditSongs");
						tabPage2.Text = Lang.say("TabPages.TextTool");
						tabPage3.Text = Lang.say("TabPages.Bible");
						tabPage4.Text = Lang.say("TabPages.Presentation");
					#endregion

					#region Toolbars
						ToolBars_MainToolbar_ShowBeamBox.Text = Lang.say("Toolbar.ShowBeamBox");
						ToolBars_MainToolbar_SizePosition.Text = Lang.say("Toolbar.SizePosition");
						ToolBars_MainToolbar_HideBG.Text = Lang.say("Toolbar.HideBackground");
						ToolBars_MainToolbar_HideText.Text = Lang.say("Toolbar.HideText");
						ToolBars_MainToolbar_SaveMediaList.Text = Lang.say("Toolbar.SaveMediaList");
						ToolBars_MainToolbar_SaveSong.Text = Lang.say("Toolbar.SaveSong");
					#endregion

					#region ShowSongs
						this.SongShow_ToBeamBox_button.Text = Lang.say("ShowSongs.ToProjector");
						this.SongShow_BackgroundPanel.TitleText = Lang.say ("ShowSongs.BackgroundPanel");
						this.SongShow_HideElementsPanel.TitleText = Lang.say ("ShowSongs.HideElementsPanel");
						this.SongShow_OverwriteBG_Button.Text = Lang.say("ShowSongs.OverwriteBG_Button");
						this.SongShow_HideTitle_Button.Text = Lang.say("ShowSongs.HideTitle_Button");
						this.SongShow_HideText_Button.Text = Lang.say ("ShowSongs.HideText_Button");
						this.SongShow_HideAuthor_Button.Text = Lang.say ("ShowSongs.HideAuthor_Button");
					#endregion

					#region EditSongs
						SongEdit_ButtonTopFont.Text =  Lang.say("EditSongs.FontButton");
						SongEdit_ButtonMidFont.Text =  Lang.say("EditSongs.FontButton");
						SongEdit_ButtonBottomFont.Text  =  Lang.say("EditSongs.FontButton");

						SongEdit_ButtonTopColor.Text  =  Lang.say("EditSongs.ColorButton");
						SongEdit_ButtonMidColor.Text =  Lang.say("EditSongs.ColorButton");
						SongEdit_ButtonBottomColor.Text =  Lang.say("EditSongs.ColorButton");

						SongEdit_ButtonTopTextOutline.Text = Lang.say("EditSongs.TextOutline");
						SongEdit_ButtonMidTextOutline.Text =  Lang.say("EditSongs.TextOutline");
						SongEdit_ButtonBottomTextOutline.Text =  Lang.say("EditSongs.TextOutline");

						SongEdit_ButtonTopOutlineColor.Text = Lang.say("EditSongs.OutlineColor");
						SongEdit_ButtonMidOutlineColor.Text =  Lang.say("EditSongs.OutlineColor");
						SongEdit_ButtonBottomOutlineColor.Text =  Lang.say("EditSongs.OutlineColor");

						SongEdit_Top_AutoPos_CheckBox.Text =  Lang.say("EditSongs.AutoPos");
						SongEdit_Header_Title.Text =  Lang.say("EditSongs.Title");
						SongEdit_Header_Verses.Text =  Lang.say("EditSongs.Verses");
						SongEdit_Header_Author.Text =  Lang.say("EditSongs.Author");

						SongEdit_PreviewStropheDown_Button.Text =  Lang.say("EditSongs.PreviousVerse");
						SongEdit_PreviewStropheUp_Button.Text =  Lang.say("EditSongs.NextVerse");
						SongEdit_BG_DecscriptionLabel.Text =  Lang.say("EditSongs.BackgroundImage");
						SongEdit_UpdateBeamBox_Button.Text = Lang.say("ShowSongs.ToProjector");

						this.SongEdit_BackgroundPanel.TitleText = Lang.say ("ShowSongs.BackgroundPanel");
						this.SongEdit_TextAlignPanel.TitleText = Lang.say ("SongEdit.TextAlignPanel");
						this.SongEdit_MultiLangPanel.TitleText = Lang.say ("SongEdit.MultiLangPanel");
						this.SongEdit_BG_DecscriptionLabel.Text = Lang.say ("SongEdit.BG_DecscriptionLabel");
						this.SongEdit_ML_Button.Text = Lang.say ("SongEdit.ML_Button");
					#endregion

					#region MediaPresentation
						Presentation_MediaLoop_Checkbox.Text = Lang.say("MediaPresentation.Loop");
						Presentation_MoviePreviewButton.Text = Lang.say("MediaPresentation.Preview");
					#endregion

					#region TextTool
						Sermon_ToolBar_NewDoc_Button.Text = Lang.say("TextTool.NewDocument");
						Sermon_ToolBar_Font_Button.Text =  Lang.say("EditSongs.FontButton");
						Sermon_ToolBar_Color_Button.Text =  Lang.say("EditSongs.ColorButton");
						Sermon_ToolBar_Outline_Button.Text = Lang.say("EditSongs.TextOutline");
						Sermon_ToolBar_OutlineColor_Button.Text = Lang.say("EditSongs.OutlineColor");

						Sermon_Verse_Label.Text = Lang.say("TextTool.Bible.FindVerse");
						Sermon_Translation_Label.Text = Lang.say("TextTool.Bible.Translation");
						Sermon_Books_Label.Text = Lang.say("TextTool.Bible.Books");
						Sermon_Testament_ListBox.Items.Clear();
						Sermon_Testament_ListBox.Items.Add(Lang.say("TextTool.Bible.OldTestament"));
						Sermon_Testament_ListBox.Items.Add(Lang.say("TextTool.Bible.NewTestament"));
						linkLabel1.Text = Lang.say("TextTool.Bible.GetSword");
//						Presentation_MoviePreviewButton.Text = ;
						Sermon_BeamBox_Button.Text = Lang.say("ShowSongs.ToProjector");
					#endregion

				#endregion
			   }
		   #endregion

		#endregion

		#region Form Methods

			#region Save and Load Settings


				/// <summary>Saves Program Properties like BeamBox Position and Alphablending to the config.xml. </summary>
			public void SaveSettings() {

				string PlayListString = "";
				for (int i = 1; i <= RightDocks_PlayList.Items.Count;i++) {
					PlayListString = PlayListString + RightDocks_PlayList.Items[i-1].ToString() + ";";
				}
				this.Config.BeamBoxPosX = (int)ShowBeam.WindowPosX.Value;
				this.Config.BeamBoxPosY = (int)ShowBeam.WindowPosY.Value;
				this.Config.BeamBoxSizeX = (int)ShowBeam.WindowSizeX.Value;
				this.Config.BeamBoxSizeY = (int)ShowBeam.WindowSizeY.Value;
				this.Config.BeamBoxAutoPosSize = ShowBeam.BeamBoxAutoPosSize;
				this.Config.BeamBoxScreenNum = ShowBeam.BeamBoxScreenNum;

				this.Config.Alphablending = ShowBeam.transit;
				this.Config.OutlineSize = ShowBeam.OutlineSize;
				this.Config.BibleLang = this.Sermon_BibleLang;
				this.Config.ShowBibleTranslation = this.Sermon_ShowBibleTranslation;
				this.Config.PlayListString = PlayListString;

				//Presentation
				this.Config.LoopMedia = Presentation_MediaLoop_Checkbox.Checked;
				this.Config.AutoPlayChangeTime = (int)RightDocks_BottomPanel_MediaLists_Numeric.Value;
				this.Config.LoopAutoPlay = RightDocks_BottomPanel_MediaLists_LoopCheckBox.Checked;
				this.Config.Save("config.xml");
			}

			/// <summary>Loads Program Properties like BeamBox Position and <Alphablending. </summary>
			public void LoadSettings() {

				// Get the BeamBox Position
				ShowBeam.WindowPosX.Value = this.Config.BeamBoxPosX;
				ShowBeam.WindowPosY.Value = this.Config.BeamBoxPosY;
				ShowBeam.WindowSizeX.Value = this.Config.BeamBoxSizeX;
				ShowBeam.WindowSizeY.Value = this.Config.BeamBoxSizeY;
				ShowBeam.BeamBoxScreenNum = this.Config.BeamBoxScreenNum;
				ShowBeam.BeamBoxAutoPosSize = this.Config.BeamBoxAutoPosSize;

				ShowBeam.HideMouse = this.Config.HideMouse;
				ShowBeam.TopMost = this.Config.AlwaysOnTop;

				// Alphablending enabled?
				ShowBeam.transit = this.Config.Alphablending;
				// Direct3D
				ShowBeam.useDirect3d = this.Config.useDirect3D;
				// Outline Size
				ShowBeam.OutlineSize = this.Config.OutlineSize;
				// BG Color
				ShowBeam.BackgroundColor = this.Config.BackgroundColor;

				// BibleStuff
				this.Sermon_BibleLang  = this.Config.BibleLang;
				this.Sermon_ShowBibleTranslation  = this.Config.ShowBibleTranslation;

				//Presentation
				Presentation_MediaLoop_Checkbox.Checked = Config.LoopMedia;
				RightDocks_BottomPanel_MediaLists_Numeric.Value = Config.AutoPlayChangeTime;
				RightDocks_BottomPanel_MediaLists_LoopCheckBox.Checked = Config.LoopAutoPlay;

				//split the book list by line into an array
				if (Config.PlayListString.Length > 0) {
					String[] tmpSongs = this.Config.PlayListString.Substring(0,this.Config.PlayListString.Length-1).Split(';');
					foreach (string tmpSong in tmpSongs) {
						//Sermon_BookList.Items.Add(Book);
						RightDocks_PlayList.Items.Add(tmpSong);
					}
				}

				if(Config.PreRender)
					this.RenderStatus.Visible = true;
				else
					this.RenderStatus.Visible = false;

				Lang.setCulture(Config.Language);
				SetLanguage();

			}

		#endregion

			#region Get and Set Song Information

			///<summary>Reads all Songs in Directory, validates if it is a Song and put's them into the RightDocks_SongList Box </summary>
			public void ListSongs() {
				this.RightDocks_SongList.Items.Clear();
				string strSongDir = Tools.DreamBeamPath()+"\\Songs";
				if(!System.IO.Directory.Exists(strSongDir)) {
					System.IO.Directory.CreateDirectory(strSongDir);
				}
				string[] dirs2 = Directory.GetFiles(@strSongDir, "*.xml");
				foreach (string dir2 in dirs2) {
					if (Song.isSong(Path.GetFileName(dir2))) {
						string temp = Path.GetFileName(dir2);
						this.RightDocks_SongList.Items.Add(temp.Substring(0,temp.Length-4));
					}
				}

			}

			/// <summary>Get Song Contents from EditForms to Song</summary>
			private void setSong() {

				ShowBeam.Song.SetText(SongEdit_Header_TextBox.Text,0);
				ShowBeam.Song.SetText(SongEdit_Mid_TextBox.Text,1);
				ShowBeam.Song.SetText(Footer_TextBox.Text,2);

			}

			// <summary>Puts the SongInformation into Windows Forms </summary>
			public void getSong() {
				try {
					this.AllowPreview = false;
					// Edit Songs:

					this.SongEdit_Header_TextBox.Text = ShowBeam.Song.GetText(0);
					this.SongEdit_Mid_TextBox.Text = ShowBeam.Song.GetText(1);
					this.Footer_TextBox.Text = ShowBeam.Song.GetText(2);
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
					while (temp.IndexOf(ShowBeam.Song.strSeperator)>= 0) {
						this.SongShow_StropheList_ListEx.Add(temp.Substring(0,temp.IndexOf(ShowBeam.Song.strSeperator)) , i);
						temp=temp.Substring(temp.IndexOf(ShowBeam.Song.strSeperator)+ShowBeam.Song.strSeperator.Length);
						i++;
					}
					this.SongShow_StropheList_ListEx.Add(temp, i);
					ShowBeam.Song.strophe_count = i;
					SongShow_StropheList_ListEx.SelectedIndex = 0;

					if(ShowBeam.Song.TextAlign == "left") {
						SongEdit_AlignLeft_Button.Checked = true;
					}
					if(ShowBeam.Song.TextAlign == "center") {
						SongEdit_AlignCenter_Button.Checked = true;
					}
					if(ShowBeam.Song.TextAlign == "right") {
						SongEdit_AlignRight_Button.Checked = true;
						}
					CheckML();
					ShowBeam.Prerenderer.RenderAllThreaded();
					this.AllowPreview = true;
					Draw_Song_Preview_Image();
					GuiTools.ChangeTitle();

				} catch(Exception e) {}
				
			}

			#endregion

			///<summary> Adjust Panel Sizes on Mainform Size changed </summary>
			private void MainForm_SizeChanged(object sender, System.EventArgs e) {
				this.GuiTools.Resize();
			}



			private void MainForm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
				GuiTools.KeyListner(e);
			}


			///<summarize> Initialize DreamBeam while MainForm is loading </summarize>
			private void MainForm_Load(object sender, System.EventArgs e) {

				this.Hide();
				Splash.SetStatus("Loading Configuration");
				this.LoadSettings();
				GuiTools.ShowTab(0);
				Splash.SetStatus("Reading Background Images");
				this.GuiTools.RightDock.BGImageTools.ListDirectories(@"Backgrounds\");

//				this.ListImages(@"Backgrounds\");
				this.GuiTools.RightDock.BGImageTools.ListImages(@"Backgrounds\");
				Splash.SetStatus("Reading Songs");
				this.ListSongs();
				Splash.SetStatus("Reading MediaLists");
				this.ListMediaLists();
				Splash.SetStatus("Loading Default MediaList");
				this.LoadDefaultMediaList();
				Splash.SetStatus("Initializing Bible");
				this.BibleInit();
				ShowBeam.Song.version = this.version;
				Splash.CloseForm();
				System.Threading.Thread.Sleep(900);
				this.getSong();
				this.TopMost = true;
				System.Threading.Thread.Sleep(900);
				this.TopMost = false;

				this.SetLanguage();
			}

			///<summarize> start SaveSettings while MainForm is closing </summarize>
			private void MainForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
			{
				this.SaveSettings();
				//    this.SaveSermon(Tools.DreamBeamPath()+"\\sermon.xml");

			}


		#endregion

		#region Toolbars and others

			#region Menu Bar

				#region Song

				///<summary> new Song from Menu</summary>
				private void ToolBars_MenuBar_Song_New_Activate(object sender, System.EventArgs e){

					// get song Name
					InputBoxResult result = InputBox.Show(Lang.say("Message.EnterSongName"),Lang.say("Message.NewSongTitle"),"", null);
					if (result.OK) {
						bool boolnew = false;
						if (result.Text.Length > 0){
							if (!System.IO.File.Exists("Songs\\"+result.Text+".xml"))     {
								boolnew = true;
							}else{
								MessageBox.Show(Lang.say("Message.SongExists"));
							}
						}else {
							MessageBox.Show(Lang.say("Message.NoNameEntered"));
						}
						// create New Song
						if (boolnew){
							ShowBeam.Song.Init(result.Text);
							this.getSong();
						}
					}
				}

				/// <summary>Saves the Selected Song</summary>
				private void ToolBars_MenuBar_Song_Save_Activate(object sender, System.EventArgs e){
					SaveSong();
				}


				///<summary> Save Song AS from Menu</summary>
				private void ToolBars_MenuBar_Song_SaveAs_Activate(object sender, System.EventArgs e){
					InputBoxResult result = InputBox.Show(Lang.say("Message.EnterSongName"), Lang.say("Message.SaveSongAs"),"", null);
					if (result.OK) {
						bool save = false;
						if (result.Text.Length > 0){
							if (!System.IO.File.Exists("Songs\\"+result.Text+".xml"))     {
								save = true;
							}else{
								MessageBox.Show(Lang.say("Message.SongExists"));
							}
						}else {
							MessageBox.Show(Lang.say("Message.SongNotSaved"));
						}
						if (save){
							this.setSong();
							ShowBeam.Song.SongName = result.Text;
							ShowBeam.Song.Save();
							this.ListSongs();
							this.StatusPanel.Text =  Lang.say("Status.SongSavedAs",result.Text);
						}
					}
				}

				///<summary>Rename Song from File Menu</summary>
				private void ToolBars_MenuBar_Song_Rename_Activate(object sender, System.EventArgs e){
					InputBoxResult result = InputBox.Show(Lang.say("Message.EnterSongName"), Lang.say("Message.RenameSongTitle",ShowBeam.Song.SongName),"", null);
					if (result.OK) {
						if (result.Text.Length > 0){
							if (!System.IO.File.Exists("Songs\\"+result.Text+".xml")) {
								System.IO.File.Move("Songs\\"+ShowBeam.Song.SongName+".xml", "Songs\\"+result.Text+".xml");
								ShowBeam.Song.SongName = result.Text;
								this.ListSongs();
								this.StatusPanel.Text = Lang.say("Status.SongRenamed",result.Text);
							}else{
								MessageBox.Show(Lang.say("Message.SongNotSavedYet"));
							}
						}else {
							MessageBox.Show(Lang.say("Message.SongNotRenamed"));

						}
					}
				}





				//<summary> End Program from Menu</summary>
				private void ToolBars_MenuBar_Song_Exit_Activate(object sender, System.EventArgs e){
					this.Close();
				}
			#endregion

				#region MediaList

					///<summary>Rename MediaList from File Menu</summary>
					private void ToolBars_MenuBar_Media_Rename_Activate(object sender, System.EventArgs e){
						InputBoxResult result = InputBox.Show(Lang.say("Message.MediaListName"), Lang.say("Message.RenameMediaListTitle",MediaList.Name),"", null);
						if (result.OK) {
							if (result.Text.Length > 0){
								if (!System.IO.File.Exists("MediaLists\\"+result.Text+".xml") && System.IO.File.Exists("MediaLists\\"+MediaList.Name+".xml")) {
									try{
									File.Move(Tools.DreamBeamPath()+"\\MediaLists\\"+MediaList.Name+".xml", "MediaLists\\"+result.Text+".xml");
									MediaList.Name = result.Text;
									this.ListMediaLists();

									string MediaFolder = Tools.DreamBeamPath()+"\\MediaFiles\\";
									//rename MediaFolder if existing
									if(File.Exists (MediaFolder+MediaList.Name)){
										try{
											File.Move(MediaFolder+MediaList.Name,MediaFolder+result.Text);
										}catch(Exception ex){}
									}
									
									this.StatusPanel.Text = Lang.say("Status.MediaListRenamed",result.Text);
									}catch(Exception doh){MessageBox.Show(doh.Message);}
								}else{
									MessageBox.Show(Lang.say("Message.MediaListNotSavedYet"));
								}
							}else {
								MessageBox.Show(Lang.say("Message.MediaListNotRenamed"));
							}
						}
					}


					/// <summary>Saves the MediaList after FileName Dialog</summary>
					private void ToolBars_MenuBar_Media_SaveAs_Activate(object sender, System.EventArgs e){
						InputBoxResult result = InputBox.Show(Lang.say("Message.MediaListName"), Lang.say("Message.SaveMediaListAs"),"", null);
						if (result.OK) {
							bool save = false;
							if (result.Text.Length > 0){
								if (!System.IO.File.Exists("MediaLists\\"+result.Text+".xml")){
									save = true;
								}else{
									MessageBox.Show(Lang.say("Message.MediaListExits"));
								}
							}else {
								MessageBox.Show(Lang.say("Message.MediaListNotSaved"));
							}
							if (save){
								MediaList.Name = result.Text;
								MediaList.Save();
								this.ListMediaLists();
								this.StatusPanel.Text = Lang.say("Status.MediaListSavedAs",result.Text);
							}
						}
					}

					/// <summary>Starts a new MediaList</summary>
					private void ToolBars_MenuBar_MediaList_New_Activate(object sender, System.EventArgs e){
						// get MediaList Name
						InputBoxResult result = InputBox.Show(Lang.say("Message.MediaListName"),Lang.say("Message.NewMediaListTitle"),"", null);
						if (result.OK) {
							bool boolnew = false;
							if (result.Text.Length > 0){
								if (!System.IO.File.Exists("MediaLists\\"+result.Text+".xml")){
									boolnew = true;
								}else{
									MessageBox.Show(Lang.say("Message.MediaListExits"));
								}
							}else {
								MessageBox.Show(Lang.say("Message.NoNameEntered"));
							}
							// create New MediaList
							if (boolnew){
								MediaList.Count = 0;
								MediaList.Name = result.Text;
								GuiTools.RightDock.MediaListTools.Refresh_MediaListBox();

							}
						}
						GuiTools.ChangeTitle();
					}

				#endregion

				#region Edit
					private void ToolBars_MenuBar_Edit_Options_Activate(object sender, System.EventArgs e) {
						SaveSettings();
						Options.ShowDialog();
						this.Config.Load();
						this.RightDocks_PlayList.Items.Clear();
						LoadSettings();
					}
				#endregion

				#region view
				private void ToolBars_MenuBar_View_ShowSongs_Activate(object sender, System.EventArgs e) {
					this.GuiTools.ShowTab(0);
				}

				private void ToolBars_MenuBar_View_EditSongs_Activate(object sender, System.EventArgs e) {
					this.GuiTools.ShowTab(1);
				}

				private void ToolBars_MenuBar_View_Presentation_Activate(object sender, System.EventArgs e) {
					this.GuiTools.ShowTab(3);
				}

				private void ToolBars_MenuBar_View_TextTool_Activate(object sender, System.EventArgs e) {
					this.GuiTools.ShowTab(2);
				}
				#endregion

				#region Help
				private void HelpIntro_Activate(object sender, System.EventArgs e) {
					this.GuiTools.Help.HelpIntro();
				}

				private void HelpBeamBox_Activate(object sender, System.EventArgs e) {
					this.GuiTools.Help.HelpBeamBox();
				}

				private void HelpOptions_Activate(object sender, System.EventArgs e) {
					this.GuiTools.Help.HelpOptions();
				}

				private void HelpShowSongs_Activate(object sender, System.EventArgs e) {
					this.GuiTools.Help.HelpShowSongs();
				}

				private void HelpEditSongs_Activate(object sender, System.EventArgs e) {
					this.GuiTools.Help.HelpEditSongs();
				}

				private void HelpPresentation_Activate(object sender, System.EventArgs e) {
					this.GuiTools.Help.HelpPresentation();
				}

				private void HelpTextTool_Activate(object sender, System.EventArgs e) {
					this.GuiTools.Help.HelpTextTool();
				}


				private void AboutButton_Activate(object sender, System.EventArgs e) {
					this.GuiTools.Help.AboutButton();
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
					 this.GuiTools.ShowBeamTools.HideBg();
					}

					// HIDE TEXT
					if (e.Item == ToolBars_MainToolbar_HideText){
					 this.GuiTools.ShowBeamTools.HideText();
					}

					//SAVE SONG
					if (e.Item == ToolBars_MainToolbar_SaveSong){
						SaveSong();
					}

					//SAVE MEDIALIST
					if (e.Item == ToolBars_MainToolbar_SaveMediaList){
						SaveMediaList();
					}
				}

				void SaveSong(){

					this.setSong();

					ShowBeam.Song.Save();
					this.ListSongs();
					this.StatusPanel.Text = Lang.say("Status.SongSavedAs", ShowBeam.Song.SongName);
				}
			#endregion

			#region Component Bar

			///<summarize> Switch Components on Click </summarize>
			private void ToolBars_ComponentBar_ButtonClick(object sender, TD.SandBar.ToolBarItemEventArgs e){

				if (e.Item == ShowSongs_Button)
				{
					GuiTools.ShowTab(0);
				} 
				else if (e.Item == EditSongs_Button)
				{
					GuiTools.ShowTab(1);
				} 
				else if (e.Item == Presentation_Button)
				{
					GuiTools.ShowTab(3);
				} 
				else if(e.Item == Sermon_Button)
				{
					GuiTools.ShowTab(2);
				} 
				else if (e.Item == BibleText_Button) 
				{
					GuiTools.ShowTab(4);
				}

			}
		#endregion

			#region Right Docks

				#region SongList
				///<summary> Load Song from DoubleClicked Listbox</summary>
				private void RightDocks_SongList_DoubleClick(object sender, System.EventArgs e){
					this.LoadSelectedSong();
				}

				private void LoadSelectedSong(){
					if (this.RightDocks_SongList.SelectedIndex >= 0){
						ShowBeam.Song.Load(RightDocks_SongList.SelectedItem.ToString());


						this.SongEdit_BG_Label.Text = ShowBeam.Song.bg_image;
						this.getSong();
						this.StatusPanel.Text = Lang.say("Status.SongLoaded",RightDocks_SongList.SelectedItem.ToString());
					}
					GC.Collect();
				}


				///<summary> Delete Song in List </summary>
				private void btnRightDocks_SongListDelete_Click(object sender, System.EventArgs e) {
					if (this.RightDocks_SongList.SelectedIndex >= 0){
						string name = RightDocks_SongList.SelectedItem.ToString();
						if (System.IO.File.Exists("Songs\\"+RightDocks_SongList.SelectedItem.ToString()+".xml")){
							DialogResult answer = MessageBox.Show(Lang.say("Message.WantDeleteSong",RightDocks_SongList.SelectedItem.ToString()),Lang.say("Message.DeleteSongTitle"), MessageBoxButtons.YesNo);
							if (answer == DialogResult.Yes){
								System.IO.File.Delete("Songs\\"+RightDocks_SongList.SelectedItem.ToString()+".xml");
							}
						}
						this.ListSongs();
						this.StatusPanel.Text = Lang.say("Status.SongDeleted",name);
					}
				}

				///<summary> Add a Song to a PlayList</summary>
				private void btnRightDocks_SongList2PlayList_Click(object sender, System.EventArgs e) {
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
							this.StatusPanel.Text = Lang.say("Status.SongToPlayList",RightDocks_SongList.SelectedItem.ToString());
						}
					}
				}

				///<summary> Search Songlist for entered Characters</summary>
				private void RightDocks_SongListSearch_TextChanged(object sender, System.EventArgs e){
					if(this.RightDocks_SongListSearch.Text.Length > 0){
						for (int i = this.RightDocks_SongList.Items.Count-1; i >= 0;i--){
							if(this.RightDocks_SongList.Items[i].ToString().Length >= this.RightDocks_SongListSearch.Text.Length){
								if (this.RightDocks_SongList.Items[i].ToString().Substring(0,this.RightDocks_SongListSearch.Text.Length).ToLower() == this.RightDocks_SongListSearch.Text.ToLower()){
									RightDocks_SongList.SelectedIndex = i;
								}
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
						this.StatusPanel.Text = Lang.say("Status.SongLoaded",RightDocks_PlayList.SelectedItem.ToString());
					}
				}

				/// <summary>Remove Selected Playlistitem on Click</summary>
				private void RightDocks_PlayList_Remove_Button_Click(object sender, System.EventArgs e){

					if (this.RightDocks_PlayList.SelectedIndex >= 0){
						string name = RightDocks_PlayList.SelectedItem.ToString();
						int tmp = this.RightDocks_PlayList.SelectedIndex;
						this.RightDocks_PlayList.Items.RemoveAt(this.RightDocks_PlayList.SelectedIndex);
						this.SaveSettings();
						this.StatusPanel.Text = Lang.say("Status.PlaylistSongRemoved",name);
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

				#region SearchPanel

				/// <summary>starts Function StartSearch</summary>
				private void RightDocks_Search_SearchButton_Click(object sender, System.EventArgs e){
					StartSearch();
				}


				/// <summary>Add a selected found Song to Playlist</summary>
				private void RightDocks_Search_PlaylistButton_Click(object sender, System.EventArgs e){
					bool addnew = true;
					if (this.RightDocks_Search_ListBox.SelectedIndex >= 0){
						for(int i = 0;i < this.RightDocks_PlayList.Items.Count;i++){
							if (this.RightDocks_PlayList.Items[i].ToString() == RightDocks_Search_ListBox.SelectedItem.ToString()){
								addnew = false;
							}
						}
						if (addnew){
							this.RightDocks_PlayList.Items.Add(RightDocks_Search_ListBox.SelectedItem.ToString());
							this.SaveSettings();
						}
					}
				}


				private void RightDocks_Search_ListBox_DoubleClick(object sender, System.EventArgs e) {
					LoadFoundSong();
				}

				/// <summary>Loads a found Song</summary>
				private void RightDocks_Search_LoadButton_Click(object sender, System.EventArgs e){
					LoadFoundSong();
				}


				private void LoadFoundSong(){
					if (this.RightDocks_Search_ListBox.SelectedIndex >= 0){
						string tmp = RightDocks_Search_ListBox.SelectedItem.ToString();
						ShowBeam.Song.Load(tmp);
						this.getSong();
						this.StatusPanel.Text = "Song '"+ tmp  +"' loaded.";
					}
				}

				/// <summary>Searches through a Song for SearchPhrase and returns true, if Phrase found</summary>
				private bool SearchInSong(string SongName,string SearchPhrase){
					if(System.IO.File.Exists(Tools.DreamBeamPath()+"\\Songs\\"+SongName+".xml")){
						Song tmpSong = new Song();
						tmpSong.Load(SongName);
						int SearchType = RightDocks_Search_DropDown.SelectedIndex;
						string Fulltext = tmpSong.GetText(0) + tmpSong.GetText(1) + tmpSong.GetText(2);

						if(SearchType==1){
							if (Fulltext.ToUpper().IndexOf(SearchPhrase.ToUpper())>=0){
								return true;
							}           
						}else{
							while(SearchPhrase.Trim().Length>0){
								string needle;
								//extract all words and use each as needle
								if(SearchPhrase.Trim().IndexOf(" ") < 0){
									needle = SearchPhrase;
									SearchPhrase = "";
								}else{
									needle = SearchPhrase.Trim().Substring(0,SearchPhrase.Trim().IndexOf(" "));
									SearchPhrase = SearchPhrase.Trim().Substring(SearchPhrase.Trim().IndexOf(" "),SearchPhrase.Trim().Length - SearchPhrase.Trim().IndexOf(" "));
								}
								// test if needle not found, then return false
								if (Fulltext.ToUpper().IndexOf(needle.ToUpper())<0){
									return false;
								}
							}
							return true; // if all needles found
						}
					}
					return false;
				}



			/// <summary>Search trough Songs</summary>
			private void StartSearch(){
				this.RightDocks_Search_ListBox.Items.Clear();
				string strSongDir = Tools.DreamBeamPath()+"\\songs";

				//care about all songs
				if(RightDocks_TopPanel_Search_RadioButton1.Checked){
					if(!System.IO.Directory.Exists(strSongDir)){
						System.IO.Directory.CreateDirectory(strSongDir);
					}
					string[] dirs2 = Directory.GetFiles(@strSongDir, "*.xml");
					foreach (string dir2 in dirs2){
						if (Song.isSong(Path.GetFileName(dir2))){
							string temp = Path.GetFileName(dir2);
							if (this.SearchInSong(temp.Substring(0,temp.Length-4),RightDocks_Search_InputBox.Text)){
								this.RightDocks_Search_ListBox.Items.Add(temp.Substring(0,temp.Length-4));
							}
						}
					}
				}else{
					for (int i = 1; i <= RightDocks_PlayList.Items.Count;i++){
						if (this.SearchInSong(RightDocks_PlayList.Items[i-1].ToString(),RightDocks_Search_InputBox.Text)){
							this.RightDocks_Search_ListBox.Items.Add(RightDocks_PlayList.Items[i-1].ToString());
						 }
					}
				}
			}


			/// <summary> Starts Search function if Enter key pressed</summary>
			private void RightDocks_Search_InputBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e){
				if (e.KeyValue == 13){
					StartSearch();
				}
			}

			#endregion

				#region Background

				private void RightDocks_FolderDropdown_SelectionChangeCommitted(object sender, System.EventArgs e) {
					this.GuiTools.RightDock.BGImageTools.SelectionChangeCommitted(sender,e);
				}

				private void RightDocks_ImageListBox_SelectedIndexChanged(object sender, System.EventArgs e) {
					this.GuiTools.RightDock.BGImageTools.SelectedIndexChanged(sender,e);
				}

				private void RightDocks_ImageListBox_DragEnter(object sender, System.Windows.Forms.DragEventArgs e) {
					this.GuiTools.RightDock.BGImageTools.DragEnter(sender,e);
				}

				private void RightDocks_ImageListBox_DragDrop(object sender, System.Windows.Forms.DragEventArgs e) {
					this.GuiTools.RightDock.BGImageTools.DragDrop(sender,e);
				}

				#endregion

			#endregion

		#endregion

		#region SHOW Songs
		///<summary> Update BeamBox </summary>
		private void SongEdit_UpdateBeamBox_Button_Click(object sender, System.EventArgs e){
		   ShowBeam.Songupdate = true;
		   if(ToolBars_MainToolbar_ShowBeamBox.Checked == false){
			   ToolBars_MainToolbar_ShowBeamBox.Checked = true;
			   ShowBeam.Show();
		   }
		   ShowBeam.PaintSong();
		}

		///<summary> On Click on ListEx with Strophes, the selected strophe will be shown on Beambox </summary>
		private void SongShow_StropheList_ListEx_DoubleClick(object sender, System.EventArgs e){
			this.GuiTools.ShowBeamTools.ShowSong();
		}

		///<summary>On OverWrite Check Change, forward the property to ShowBeam </summary>
		private void SongShow_BG_CheckBox_CheckedChanged(object sender, System.EventArgs e){
			// ShowBeam.OverWriteBG = this.SongShow_BG_CheckBox.Checked;
		}

		///<summary>Redraw Panel 8 on Click </summary>
		private void SongShow_Preview_Panel_Click(object sender, System.EventArgs e){

		   Draw_Song_Preview_Image_Threaded();
	   }


		private void SongShow_HideText_Button_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e){
			if(ShowBeam.HideVerse)
				 ShowBeam.HideVerse = false;
			else
				 ShowBeam.HideVerse = true;

			ShowBeam.Prerenderer.RenderAllThreaded();
		}

		private void SongShow_HideText_Button_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e){
			if(!ShowBeam.HideVerse)
			 SongShow_HideText_Button.Checked = false;
		 }

		 private void SongShow_HideAuthor_Button_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e){
			 if(ShowBeam.HideAuthor)
				 ShowBeam.HideAuthor = false;
			 else
				ShowBeam.HideAuthor = true;

			ShowBeam.Prerenderer.RenderAllThreaded();
		}

		private void SongShow_HideAuthor_Button_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e){
			if(!ShowBeam.HideAuthor)
				SongShow_HideAuthor_Button.Checked = false;
		}

		private void SongShow_OverwriteBG_Button_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e){
			if(ShowBeam.OverWriteBG)
				ShowBeam.OverWriteBG = false;
			else
				 ShowBeam.OverWriteBG = true;

			ShowBeam.Prerenderer.RenderAllThreaded();
		}

		private void SongShow_OverwriteBG_Button_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e){
			if(!ShowBeam.OverWriteBG)
				SongShow_OverwriteBG_Button.Checked = false;
		}

	   #endregion

		#region EDIT Songs

		///<summary>Update Preview Panel on Song Text Typed</summary>
		private void timer1_Tick(object sender, System.EventArgs e) {
			if(this.UpdatePreview & this.TextTyped == false){
				//ShowBeam.Prerenderer.RenderAllThreaded();
				Draw_Song_Preview_Image_Threaded();
				this.UpdatePreview = false;
			}
		}

		 ///<summary></summary>
		private void TextTypedTimer_Tick(object sender, System.EventArgs e) {
			this.TextTyped = false;
		}

		///<summary>Shows the previous strophe on click </summary>
		private void SongEdit_PreviewStropheDown_Button_Click(object sender, System.EventArgs e){
			this.setSong();
			this.getSong();
			if(ShowBeam.Song.strophe > 0){
				ShowBeam.Song.strophe --;

				Draw_Song_Preview_Image_Threaded();
		   }
		 }

		///<summary> Shows the next strophe on click </summary>
		private void SongEdit_PreviewStropheUp_Button_Click(object sender, System.EventArgs e){
			//initialize Strophes
			this.setSong();
			this.getSong();
			if(ShowBeam.Song.strophe < ShowBeam.Song.strophe_count ){
				ShowBeam.Song.strophe ++;

				Draw_Song_Preview_Image_Threaded();
			}
		}

		/// <summary>Sends the Clicked Item and the Textfield Number to TextProperties </summary>
		private void SongEdit_TopTextToolBar_ButtonClick(object sender, TD.SandBar.ToolBarItemEventArgs e){
			this.TextProperties(e,0);
		}

		/// <summary>Sends the Clicked Item and the Textfield Number to TextProperties </summary>
		private void SongEdit_MidTextToolBar_ButtonClick(object sender, TD.SandBar.ToolBarItemEventArgs e){
		   this.TextProperties(e,1);
		}

		/// <summary>Sends the Clicked Item and the Textfield Number to TextProperties </summary>
		private void SongEdit_BottomTextToolBar_ButtonClick(object sender, TD.SandBar.ToolBarItemEventArgs e){
			this.TextProperties(e,2);
		}

		private void SongEdit_LangTextToolBar_ButtonClick(object sender, TD.SandBar.ToolBarItemEventArgs e)
		{
			this.TextProperties(e,3);
		}


		/// <summary>If the Header Text has changed, update the Song-Array </summary>
		private void SongEdit_Header_TextBox_TextChanged(object sender, System.EventArgs e){
			ShowBeam.Song.SetText(this.SongEdit_Header_TextBox.Text,0);
			this.UpdatePreview = true;
			this.TextTyped = true;
		}

		/// <summary>If the Mid Text has changed, update the Song-Array </summary>
		private void SongEdit_Mid_TextBox_TextChanged1(object sender, System.EventArgs e){
			ShowBeam.Song.SetText(this.SongEdit_Mid_TextBox.Text,1);
			this.UpdatePreview = true;
			this.TextTyped = true;
		}


		/// <summary>If the Footer Text has changed, update the Song-Array </summary>
		private void Footer_TextBox_TextChanged(object sender, System.EventArgs e){
			ShowBeam.Song.SetText(this.Footer_TextBox.Text,2);
			this.UpdatePreview = true;
			this.TextTyped = true;
		}

		/// <summary>If the SongEdit_TopPosX_UpDown has changed, update the Song-Array and Redraw Panel4 </summary>
		private void SongEdit_TopPosX_UpDown_ValueChanged(object sender, System.EventArgs e){
			ShowBeam.Song.posX[0] = (int)SongEdit_TopPosX_UpDown.Value;
			this.UpdatePreview = true;
		}

		/// <summary>If the SongEdit_TopPosY_UpDown has changed, update the Song-Array and Redraw Panel4 </summary>
		private void SongEdit_TopPosY_UpDown_ValueChanged(object sender, System.EventArgs e){
			ShowBeam.Song.posY[0] = (int)SongEdit_TopPosY_UpDown.Value;
			this.UpdatePreview = true;
	   }

		/// <summary>If the SongEdit_MidPosX_UpDown has changed, update the Song-Array and Redraw Panel4 </summary>
		private void SongEdit_MidPosX_UpDown_ValueChanged(object sender, System.EventArgs e){
			ShowBeam.Song.posX[1] = (int)SongEdit_MidPosX_UpDown.Value;
			this.UpdatePreview = true;
		}

		/// <summary>If the SongEdit_MidPosY_UpDown has changed, update the Song-Array and Redraw Panel4 </summary>
		private void SongEdit_MidPosY_UpDown_ValueChanged(object sender, System.EventArgs e){
			ShowBeam.Song.posY[1] = (int)SongEdit_MidPosY_UpDown.Value;
			this.UpdatePreview = true;
	   }

		/// <summary>If the SongEdit_BottomPosX_UpDown has changed, update the Song-Array and Redraw Panel4 </summary>
		private void SongEdit_BottomPosX_UpDown_ValueChanged(object sender, System.EventArgs e){
			ShowBeam.Song.posX[2] = (int)SongEdit_BottomPosX_UpDown.Value;
			this.UpdatePreview = true;
	   }

		/// <summary>If the SongEdit_BottomPosY_UpDown has changed, update the Song-Array and Redraw Panel4 </summary>
		private void SongEdit_BottomPosY_UpDown_ValueChanged(object sender, System.EventArgs e){
			ShowBeam.Song.posY[2] = (int)SongEdit_BottomPosY_UpDown.Value;
			this.UpdatePreview = true;
	   }

		///<summary>Preview Panel for EDIT Songs </summary>
		private void SongEdit_Preview_Panel_Paint(object sender, System.Windows.Forms.PaintEventArgs e){

				try{


				 if(memoryBitmap != null){
						//g.DrawImage(memoryBitmap, 0, 0,SongShow_Preview_Panel.Width ,SongShow_Preview_Panel.Height);
//						pictureBox1.Image = memoryBitmap;
					}

//					Draw_Song_Preview_Image();
				}catch(Exception doh){}
		}

		public void Draw_Song_Preview_Image(){
			Draw_Song_Preview_Threader();
		}

		public void Draw_Song_Preview_Image_Threaded(){
		   if(!this.DrawingPreview & !LoadingBGThumbs & !ShowBeam.DrawingSong){
			   this.DrawingPreview = true;
			   Thread_PreviewSong = new Thread(new ThreadStart(Draw_Song_Preview_Threader));
			   Thread_PreviewSong.IsBackground = true;
			   Thread_PreviewSong.Start();
		   }
		}


		void Draw_Song_Preview_Threader(){
		   if(this.AllowPreview){


				if(ShowBeam.DrawnMainBitmap)
					ShowBeam.DrawnMainBitmap = false;
				else{
					ShowBeam.bmp = new Bitmap(ShowBeam.Width,ShowBeam.Height,PixelFormat.Format32bppArgb);
					ShowBeam.bmp = ShowBeam.Prerenderer.GetStrophe(ShowBeam.Song.strophe);
					//ShowBeam.TextGraphics.DrawSongBitmap(ShowBeam.bmp,ShowBeam.Song.strophe,ShowBeam.Width,ShowBeam.Height);
					
				}

				try{
					if(selectedTab == 0)
						memoryBitmap = ShowBeam.DrawProportionalBitmap(SongShow_Preview_Panel.Size,ShowBeam.bmp);
					else
						memoryBitmap = ShowBeam.DrawProportionalBitmap(SongEdit_Preview_Panel.Size,ShowBeam.bmp);
				}catch(Exception doh){MessageBox.Show(doh.Message);}
				SongEdit_Preview_Panel.Image = memoryBitmap;
				SongShow_Preview_Panel.Image = memoryBitmap;
		   }
		   this.DrawingPreview = false;
		   GC.Collect();
		}

		///<summarize>Manages the 3 Text Toolbar ButtonClicks in EDIT Songs </summarize>
		private void TextProperties(TD.SandBar.ToolBarItemEventArgs e, int where){
		   if(e.Item == SongEdit_ButtonTopFont || e.Item == SongEdit_ButtonMidFont || e.Item == SongEdit_ButtonBottomFont || e.Item == SongEdit_ButtonLangFont){
			  try{
			   this.SongEdit_fontDialog.Font = new Font(ShowBeam.Song.FontFace[where],ShowBeam.Song.FontSize[where],ShowBeam.Song.FontStyle[where]);

			   }catch(Exception doh){MessageBox.Show (doh.Message);}

			  try{
			   this.SongEdit_fontDialog.ShowDialog();
			   }catch(Exception doh){MessageBox.Show (doh.Message);}
			   ShowBeam.Song.FontFace[where]=this.SongEdit_fontDialog.Font.Name;
			   ShowBeam.Song.FontSize[where]=this.SongEdit_fontDialog.Font.Size;
			   ShowBeam.Song.FontStyle[where]=this.SongEdit_fontDialog.Font.Style;
			}
		   if(e.Item == SongEdit_ButtonTopColor || e.Item == SongEdit_ButtonMidColor || e.Item == SongEdit_ButtonBottomColor || e.Item == SongEdit_ButtonLangColor){
			   SongEdit_TextColorDialog.Color = ShowBeam.Song.TextColor[where];
			   SongEdit_TextColorDialog.ShowDialog();
			   ShowBeam.Song.TextColor[where] = this.SongEdit_TextColorDialog.Color;
		   }
		   if(e.Item == SongEdit_ButtonTopTextOutline || e.Item == SongEdit_ButtonMidTextOutline || e.Item == SongEdit_ButtonBottomTextOutline || e.Item == SongEdit_ButtonLangTextOutline){
			   if (ShowBeam.Song.TextEffect[where] == "Filled Outline"){
				   ShowBeam.Song.TextEffect[where] = "Normal";
			   }else {
				   ShowBeam.Song.TextEffect[where] = "Filled Outline";
			   }
			   this.SetEditButtonsStatus();
		   }
		   if (e.Item == SongEdit_ButtonTopOutlineColor || e.Item == SongEdit_ButtonMidOutlineColor || e.Item == SongEdit_ButtonBottomOutlineColor || e.Item == SongEdit_ButtonLangOutlineColor){
			   SongEdit_TextColorDialog.Color = ShowBeam.Song.OutlineColor[where];
			   SongEdit_TextColorDialog.ShowDialog();
			   ShowBeam.Song.OutlineColor[where] = this.SongEdit_TextColorDialog.Color;
		   }


			this.UpdatePreview = true;
		}


		/// <summary> Gets the the Fontsettings for Font Dialog </summary>
		private void Set_TextProperty_Controls(){
			this.SongEdit_fontDialog.Font = new Font(ShowBeam.Song.FontFace[this.Song_Edit_Box],ShowBeam.Song.FontSize[this.Song_Edit_Box], this.SongEdit_fontDialog.Font.Style);
		}

		///<summary>Shows a FontDialog and Changes the Song FontSettings </summary>
		private void FontButton_Click(object sender, System.EventArgs e){
		   this.SongEdit_fontDialog.ShowDialog();
		   ShowBeam.Song.FontFace[this.Song_Edit_Box]=this.SongEdit_fontDialog.Font.Name;
		   ShowBeam.Song.FontSize[this.Song_Edit_Box]=this.SongEdit_fontDialog.Font.Size;
		}

		///<summary>Shows a ColorDialog and Changes the Song ColorSettings </summary>
		private void ColorButton_Click(object sender, System.EventArgs e){
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

		  // Footer TextBox
		  if (ShowBeam.Song.TextEffect[3]=="Filled Outline"){
			   this.SongEdit_ButtonLangTextOutline.Checked = true;
			   this.SongEdit_ButtonLangOutlineColor.Visible = true;
		  }else{
			   this.SongEdit_ButtonLangTextOutline.Checked = false;
			   this.SongEdit_ButtonLangOutlineColor.Visible = false;
			   }
		}

		///<summary>Highlight HeaderTextBox </summary>
		private void SongEdit_Header_TextBox_Enter(object sender, System.EventArgs e){
		   this.Song_Edit_Box=0;
		   this.SongEdit_Header_TextBox.BackColor = Color.WhiteSmoke;
		   this.SongEdit_Mid_TextBox.BackColor = Color.White;
		   this.Footer_TextBox.BackColor = Color.White;
		   this.Set_TextProperty_Controls();
		}

		///<summary>Highlight MidTextBox </summary>
		private void SongEdit_Mid_TextBox_Enter(object sender, System.EventArgs e){
		   this.Song_Edit_Box=1;
		   this.SongEdit_Header_TextBox.BackColor = Color.White;
		   this.SongEdit_Mid_TextBox.BackColor = Color.WhiteSmoke;
		   this.Footer_TextBox.BackColor = Color.White;
		   this.Set_TextProperty_Controls();
		}

		///<summary>Highlight FooterTextBox </summary>
		private void Footer_TextBox_Enter(object sender, System.EventArgs e){
		   this.Song_Edit_Box=2;
		   this.SongEdit_Header_TextBox.BackColor = Color.White;
		   this.SongEdit_Mid_TextBox.BackColor = Color.White;
		   this.Footer_TextBox.BackColor = Color.WhiteSmoke;
		   this.Set_TextProperty_Controls();
		}

		///<summary>On Mid_TextX Change, update Song </summary>
		private void Mid_TextX_ValueChanged(object sender, System.EventArgs e){
		}

		///<summary>On Mid_TextY Change, update Song </summary>
		private void Mid_TextY_ValueChanged(object sender, System.EventArgs e) {
		}

		///<summary>On SongEdit_Mid_TextBox_Text Change, update Song </summary>
		private void SongEdit_Mid_TextBox_TextChanged(object sender, System.EventArgs e){
		   ShowBeam.Song.SetText(SongEdit_Mid_TextBox.Text,this.Song_Edit_Box);
		}

		///<summary>put Top_AutoPos - Value into Song information </summary>
		private void SongEdit_Top_AutoPos_CheckBox_Click(object sender, System.EventArgs e) {
		   ShowBeam.Song.AutoPos[0] = SongEdit_Top_AutoPos_CheckBox.Checked;
		   this.UpdatePreview = true;
		}

		///<summary>put Mid_AutoPos - Value into Song information </summary>
		private void checkBox1_Click(object sender, System.EventArgs e) {
		   ShowBeam.Song.AutoPos[1] =SongEdit_Mid_AutoPos_CheckBox.Checked;
		   this.UpdatePreview = true;
		}

		///<summary>put Bottom_AutoPos - Value into Song information </summary>
		private void SongEdit_Bottom_AutoPos_CheckBox_Click(object sender, System.EventArgs e) {
			   ShowBeam.Song.AutoPos[2] = SongEdit_Bottom_AutoPos_CheckBox.Checked;
			   this.UpdatePreview = true;
		}



		private void SongEdit_AlignLeft_Button_CheckedChanged(object sender, System.EventArgs e){
			if(SongEdit_AlignLeft_Button.Checked)
				ShowBeam.Song.TextAlign = "left";
			if(SongEdit_AlignCenter_Button.Checked)
				ShowBeam.Song.TextAlign = "center";
			if(SongEdit_AlignRight_Button.Checked)
				ShowBeam.Song.TextAlign = "right";
			this.UpdatePreview = true;
		}


		private void SongEdit_ML_Button_Click(object sender, System.EventArgs e) {
			if( ShowBeam.Song.MultiLang)
				 ShowBeam.Song.MultiLang = false;
			else
				 ShowBeam.Song.MultiLang = true;
			CheckML();
		}

		private void CheckML() {
			if(ShowBeam.Song.MultiLang == false) {
				SongEdit_ML_Button.Checked = false;
				SongEdit_LangTextToolBar.Visible = false;
			 } else {
				SongEdit_ML_Button.Checked = true;
				SongEdit_LangTextToolBar.Visible = true;
			 }
			GuiTools.SongEdit.TextColorizer(this.SongEdit_Mid_TextBox);
		 }

		private void SongEdit_Mid_TextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
			if(e.KeyValue == 13 || e.KeyValue == 32 || e.KeyValue == 192) {
				AllowPreview = false;
					GuiTools.SongEdit.TextColorizer(this.SongEdit_Mid_TextBox);
				AllowPreview = true;
			 }
		}
		#endregion

		#region Presentation


			#region Preview & Control Window


				#region Control Window
					/// <summary>Wipe Movie Control Panel in and out</summary>
					private void MovieControlPanelWipe(string direction) {
						if (direction == "in"){
							Media_TrackBar.Enabled = false;
							Presentation_MoviePreviewButton.Enabled = false;
							Presentation_MoviePreviewButton.Enabled = false;
							Presentation_PlayButton.Enabled = false;
							Presentation_PauseButton.Enabled = false;
							Presentation_StopButton.Enabled = false;
							if(Presentation_MovieControlPanel.Size.Height != 0){
								Presentation_MovieControlPanel.Size = new System.Drawing.Size(Presentation_MovieControlPanel.Size.Width,0);
						   }
					   }else{
						   Media_TrackBar.Enabled = true;
						   Presentation_MoviePreviewButton.Enabled = true;
						   Presentation_PlayButton.Enabled = true;
						   Presentation_MoviePreviewButton.Enabled = true;
						   if(Presentation_MovieControlPanel.Size.Height != 60){
							   Presentation_MovieControlPanel.Size = new System.Drawing.Size(Presentation_MovieControlPanel.Size.Width,60);
						   }
					   }
				   }


				   /// <summary>on Progress-Timer Tick, set mediaTrackbar to MediaPlayPosition</summary>
				   private void PlayProgress_Tick(object sender, System.EventArgs e){
					   if(this.ShowBeam.strMediaPlaying == null){
						   if(MediaPreview){
							   if(MediaList.GetType(MediaFile)=="flash"){
								   Media_TrackBar.Value = axShockwaveFlash.FrameNum;
							   }
							   if(MediaList.GetType(MediaFile)=="movie"){
								   try{
									   Media_TrackBar.Maximum = (int)video.Duration;
									   Media_TrackBar.Value = (int)video.CurrentPosition;
								   }catch{}
							   }
						   }
					   }else if(this.ShowBeam.strMediaPlaying == "flash"){
						   Media_TrackBar.Value = this.ShowBeam.axShockwaveFlash.FrameNum;
					   }else if(this.ShowBeam.strMediaPlaying == "movie"){
						   try{
							   Media_TrackBar.Maximum = (int)this.ShowBeam.video.Duration;
							   Media_TrackBar.Value = (int)this.ShowBeam.video.CurrentPosition;
						   }catch{}
					   }
				   }


			   /// <summary>On Mouse Release, set Media Position to Trackbar Position</summary>
			   private void Media_TrackBar_Up(object sender, System.Windows.Forms.MouseEventArgs e){
				   if(this.ShowBeam.strMediaPlaying == null){
					   string MediaName = this.MediaList.iItem[RightDocks_BottomPanel_MediaList.SelectedIndex].Path;
					   if(MediaList.GetType(MediaName)=="flash"){
						axShockwaveFlash.GotoFrame(Media_TrackBar.Value);
					   }
					   if(MediaList.GetType(MediaName)=="movie"){
						   video.CurrentPosition = (double)Media_TrackBar.Value;
					   }
				   }else if(this.ShowBeam.strMediaPlaying == "flash"){
					   this.ShowBeam.axShockwaveFlash.GotoFrame(Media_TrackBar.Value);
				   }else if (this.ShowBeam.strMediaPlaying == "movie"){
					   this.ShowBeam.video.CurrentPosition = (double)Media_TrackBar.Value;
				   }
			   }


			   /// <summary>Adjust Volume on Volume Trackbar change</summary>
			   private void AudioBar_ValueChanged(object sender, System.EventArgs e){
				   if(this.ShowBeam.video != null){
					   try{
						   this.ShowBeam.video.Audio.Volume = AudioBar.Value;
					   }
					   catch{}
				   }
			   }

			   /// <summary>On Click, Play, Stop or Pause Media</summary>
			   private void Presentation_PlayBar_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e){
				   string MediaName = this.MediaList.iItem[RightDocks_BottomPanel_MediaList.SelectedIndex].Path;
				   if (e.Button == Presentation_PlayButton){
					   this.Media2BeamBox();
				   }
				   if (e.Button == Presentation_PauseButton){
					   PlayProgress.Enabled = false;
					   Presentation_PauseButton.Enabled = false;
					   Presentation_PlayButton.Enabled = true;
					   this.ShowBeam.PauseMedia();
				   }
				   if (e.Button == Presentation_StopButton){
					   PlayProgress.Enabled = false;
					   Presentation_StopButton.Enabled = false;
					   Presentation_PauseButton.Enabled = false;
					   Presentation_PlayButton.Enabled = true;
					   ShowBeam.StopMedia();
				   }
			   }


			   /// <summary>If Loop Media Checkbox changed, copy it's value to showbeam</summary>
			   private void Presentation_MediaLoop_Checkbox_CheckedChanged(object sender, System.EventArgs e){
				   ShowBeam.LoopMedia = Presentation_MediaLoop_Checkbox.Checked;
				   ShowBeam.axShockwaveFlash.Loop = Presentation_MediaLoop_Checkbox.Checked;
			   }
			#endregion

				/// <summary>Starts and Stop's the local Media Preview</summary>
			   private void Presentation_MoviePreviewButton_Click(object sender, System.EventArgs e){
				   if(!MediaPreview){
					   if(MediaList.GetType(MediaFile)=="flash"){
						   try{
							   Media_TrackBar.Maximum = axShockwaveFlash.TotalFrames;
							   PlayProgress.Enabled = true;
							   axShockwaveFlash.Play();
						   }catch{MessageBox.Show("Can not play this Flash File");}
					   }
					   if(MediaList.GetType(MediaFile)=="movie"){
						   Media_TrackBar.Maximum = (int)video.Duration;
						   PlayProgress.Enabled = true;
						   try{
							   video.Audio.Volume = -10000;
						   }catch {}
						   Thread.Sleep(1000);
						   this.video.Play();
					   }
					   MediaPreview = true;
				   }else{
					   if(MediaList.GetType(MediaFile)=="flash"){
						   axShockwaveFlash.Stop();
						   axShockwaveFlash.Back();
						   PlayProgress.Enabled = false;
					   }
					   if(MediaList.GetType(MediaFile)=="movie"){
						   this.video.Stop();
					   }
					   MediaPreview = false;
				   }
			  }
			#endregion


			#region RightDock MediaList Panels

			private void RightDocks_BottomPanel_MediaList_DragDrop(object sender, System.Windows.Forms.DragEventArgs e) {
				this.GuiTools.RightDock.MediaListTools.DragDrop(sender,e);
			}

			private void RightDocks_BottomPanel_MediaList_DragEnter(object sender, System.Windows.Forms.DragEventArgs e) {
				this.GuiTools.RightDock.MediaListTools.DragEnter (sender,e);
			}

			private void RightDocks_BottomPanel_Media_Remove_Click(object sender, System.EventArgs e) {
				this.GuiTools.RightDock.MediaListTools.Media_Remove_Click(sender,e);
			}

			private void RightDocks_BottomPanel_Media_Up_Click(object sender, System.EventArgs e) {
				this.GuiTools.RightDock.MediaListTools.Media_Up_Click (sender,e);
			}

			private void RightDocks_BottomPanel_Media_Down_Click(object sender, System.EventArgs e) {
				this.GuiTools.RightDock.MediaListTools.Media_Down_Click (sender,e);
			}

			   /// <summary>start Media Autoplay on Click</summary>
			   private void RightDocks_BottomPanel_Media_AutoPlay_Click(object sender, System.EventArgs e){
				   if(!this.Presentation_AutoPlayTimer.Enabled){
					   this.Presentation_AutoPlayTimer.Interval = (int)RightDocks_BottomPanel_MediaLists_Numeric.Value * 1000;
					   this.Presentation_AutoPlayTimer.Enabled = true;
					   RightDocks_BottomPanel_Media_AutoPlay.Text = "Stop";
					   this.ShowBeam.axShockwaveFlash.Loop = false;
					   Presentation_MediaLoop_Checkbox.Checked = false;
				   }else{
					   this.Presentation_AutoPlayTimer.Enabled = false;
					   RightDocks_BottomPanel_Media_AutoPlay.Text = "Auto Play";
				   }
			   }

			   /// <summary>On Change, copy Numeric-Value to AutoPlayTimer </summary>
			   private void RightDocks_BottomPanel_MediaLists_Numeric_ValueChanged(object sender, System.EventArgs e){
				   this.Presentation_AutoPlayTimer.Interval = (int)RightDocks_BottomPanel_MediaLists_Numeric.Value * 1000;
			   }


			   /// <summary>On Media ListBox doubleclick run Media2Beambox function</summary>
			   private void RightDocks_BottomPanel_MediaList_DoubleClick(object sender, System.EventArgs e){
				   this.Media2BeamBox();
			   }


				/// <summary> if Preview Video Loaded, Load and show the Video</summary>
				private void VideoLoadTimer_Tick(object sender, System.EventArgs e){
				   if(this.VideoLoaded){
					   VideoLoadTimer.Enabled = false;
					   this.VideoLoaded = false;
					   try{
						   Media_TrackBar.Maximum = (int)video.Duration;
					   }catch{}
					   PlayProgress.Enabled = true;
					   ShowBeam.ShowMedia(MediaFile);
				   }
				}


				/// <summary>Show selected Media and select next Item</summary>
				private void RightDocks_BottomPanel_Media_ShowNext_Click(object sender, System.EventArgs e) {
				   if (this.RightDocks_BottomPanel_MediaList.Items.Count != -1){
					   this.Media2BeamBox();
					   int tmp = this.RightDocks_BottomPanel_MediaList.SelectedIndex;
					   if(tmp < this.RightDocks_BottomPanel_MediaList.Items.Count -1){
						   this.RightDocks_BottomPanel_MediaList.SelectedIndex = tmp+1;
					   }
				   }
			   }

			   /// <summary>On tick Show Media and select next</summary>
			   private void Presentation_AutoPlayTimer_Tick(object sender, System.EventArgs e){
				   if(this.RightDocks_BottomPanel_MediaList.SelectedIndex == -1 && this.RightDocks_BottomPanel_MediaList.Items.Count > 0){
					   this.RightDocks_BottomPanel_MediaList.SelectedIndex = 0;
					}
				   if(ShowBeam.strMediaPlaying == "movie"){
					   iFilmEnded = 1;
					   if(this.ShowBeam.video.CurrentPosition == this.ShowBeam.video.Duration){
						   iFilmEnded = 0;
					   }
				   }
				   if(ShowBeam.strMediaPlaying == "flash"){
					   this.StatusPanel.Text = this.ShowBeam.axShockwaveFlash.CurrentFrame().ToString() + " / " +  this.ShowBeam.axShockwaveFlash.TotalFrames.ToString();
					   if(iFilmEnded == 0){
						   iFilmEnded = this.ShowBeam.axShockwaveFlash.CurrentFrame();
						   if(this.ShowBeam.axShockwaveFlash.CurrentFrame() == this.ShowBeam.axShockwaveFlash.TotalFrames -1){
							   iFilmEnded = 0;
						   }
					   }else{
							if(this.ShowBeam.axShockwaveFlash.CurrentFrame() == iFilmEnded){
								iFilmEnded = 0;
						   }else{
							   iFilmEnded =  ShowBeam.axShockwaveFlash.CurrentFrame();
						   }
					   }
				   }
				   if (this.RightDocks_BottomPanel_MediaList.SelectedIndex != -1 && iFilmEnded == 0){
					   this.Media2BeamBox();
					   int tmp = this.RightDocks_BottomPanel_MediaList.SelectedIndex;
					   if(tmp < this.RightDocks_BottomPanel_MediaList.Items.Count -1){
						   this.RightDocks_BottomPanel_MediaList.SelectedIndex = tmp+1;
					   }else if(RightDocks_BottomPanel_MediaLists_LoopCheckBox.Checked) {
							this.RightDocks_BottomPanel_MediaList.SelectedIndex = 0;
					   }
				   }
				}

			private void Presentation_Fade_ListView_Click(object sender, System.EventArgs e) {
				this.GuiTools.Presentation.ListView_Click(sender,e);
			}

				private void Presentation_Fade_ToPlaylist_Button_Click(object sender, System.EventArgs e) {
					this.GuiTools.Presentation.Fade_ToPlaylist_Button_Click(sender,e);
			}
			#endregion


			#region MediaList
			   private void RightDocks_BottomPanel_Media_FadePanelButton_Click(object sender, System.EventArgs e){
				   if (Presentation_FadePanel.Size.Width == 510){
					   Presentation_FadePanel.Size = new System.Drawing.Size(0,Presentation_FadePanel.Size.Height);
				   }else if (Presentation_FadePanel.Size.Width == 0){
					   Presentation_FadePanel.Size = new System.Drawing.Size(510,Presentation_FadePanel.Size.Height);
				   }
			   }



			   private void RightDocks_BottomPanel_MediaList_SelectedIndexChanged(object sender, System.EventArgs e){
				   if(video != null)
					   video.Stop();
				   if(axShockwaveFlash.Playing)
					   axShockwaveFlash.Stop();

				   Presentation_StopButton.Enabled = false;
				   Presentation_PauseButton.Enabled = false;
				   axShockwaveFlash.SendToBack();
				   axShockwaveFlash.Movie = "";
				   this.MediaPreview = false;
				   MediaFile = this.MediaList.iItem[RightDocks_BottomPanel_MediaList.SelectedIndex].Path;
				   if(MediaList.GetType(MediaFile) == "image"){
					   MovieControlPanelWipe("in");
					   Presentation_PreviewBox.BringToFront();
					   Presentation_PreviewBox.Image = this.ShowBeam.DrawProportionalBitmap(Presentation_PreviewBox.Size,MediaFile);
				   }
				   if(MediaList.GetType(MediaFile) == "flash"){
					   MovieControlPanelWipe("out");
					   AudioBar.Enabled=false;
					   Presentation_PreviewBox.SendToBack();
					   axShockwaveFlash.BringToFront();
					   axShockwaveFlash.Movie = MediaFile;
					   axShockwaveFlash.Playing = false;
					   axShockwaveFlash.Stop();
				   }
				   if(MediaList.GetType(MediaFile) == "movie"){
					   if (!LoadingVideo){
						   Thread_LoadMovie = new Thread( new ThreadStart(LoadVideo));
						   Thread_LoadMovie.IsBackground = true;
						   Thread_LoadMovie.Start();
					   }
				   }
			   		GC.Collect();
			   }

			   private void LoadVideo(){
				   LoadingVideo = true;
				   VideoProblem = false;
				   if (this.ShowBeam.strMediaPlaying != null){
					   Presentation_StopButton.Enabled = true;
				   }
				   MovieControlPanelWipe("out");
				   Presentation_MoviePreviewButton.Enabled = false;
				   AudioBar.Enabled=false;
				   Media_TrackBar.Enabled = false;
				   Presentation_MoviePreviewButton.Text = "Loading...";
				   Presentation_VideoPanel.BringToFront();

				   if (this.video == null){
					   try{
						   this.video = new Microsoft.DirectX.AudioVideoPlayback.Video(MediaFile,false);
					   }catch{
							MessageBox.Show ("Can not load Video.");
							VideoProblem = true;
					   }
				   }else{
					   this.video.Stop();
					   try{
						   this.video.Dispose();
						   this.video = null;
						   this.video = new Microsoft.DirectX.AudioVideoPlayback.Video(MediaFile,false);
					   }catch{
						   MessageBox.Show ("Could not load Video.");
						   VideoProblem = true;
					   }
				   }
				   if(!VideoProblem){
					   this.video.Owner = Presentation_VideoPanel;
					   this.video.Size = Tools.VideoProportions(Presentation_PreviewBox.Size,this.video.DefaultSize);
					   this.Presentation_VideoPanel.Size = Tools.VideoProportions(Presentation_PreviewBox.Size,this.video.DefaultSize);
					   this.Presentation_VideoPanel.Location =  new Point((int)((this.axShockwaveFlash.Width - this.Presentation_VideoPanel.Size.Width)/2)+10,(int)((this.axShockwaveFlash.Height - this.Presentation_VideoPanel.Size.Height)/2)+10);
					   Presentation_PreviewBox.Image = ShowBeam.DrawBlackBitmap(Presentation_PreviewBox.Size.Width ,Presentation_PreviewBox.Size.Height);
					   Presentation_MoviePreviewButton.Text = "Preview";
					   Presentation_MoviePreviewButton.Enabled = true;
					   AudioBar.Enabled=true;
					   Media_TrackBar.Enabled = true;
				   }else{
					   Presentation_StopButton.Enabled = false;
					   Presentation_PauseButton.Enabled = false;
					   Presentation_PlayButton.Enabled = false;
				   }
				   this.VideoLoaded = true;
				   LoadingVideo = false;
			   }



		#endregion


			#region MediaList

				///<summary>Reads all MediaLists in Directory, validates if it is a MediaList and put's them into the RightDocks_SongList Box </summary>
				public void ListMediaLists(){
					this.RightDocks_MediaLists.Items.Clear();
					string strSongDir = Tools.DreamBeamPath()+"\\MediaLists";
					if(!System.IO.Directory.Exists(strSongDir)){
						System.IO.Directory.CreateDirectory(strSongDir);
					}
					string[] dirs2 = Directory.GetFiles(@strSongDir, "*.xml");
					foreach (string dir2 in dirs2){
					   if (Song.isSong(Path.GetFileName(dir2))){
						   string temp = Path.GetFileName(dir2);
						   this.RightDocks_MediaLists.Items.Add(temp.Substring(0,temp.Length-4));
					   }
					}
				}





			   /// <summary>Loads Default MediaList on Startup</summary>
			   void LoadDefaultMediaList(){
				   if(File.Exists("MediaLists\\Default.xml")){
					   GuiTools.RightDock.MediaListTools.LoadSelectedMediaList("Default");
				   }
			   }

			   /// <summary>Loads a MediaList after DoubleClick on ListBox</summary>
			   private void RightDocks_MediaLists_DoubleClick(object sender, System.EventArgs e){
				   if (this.RightDocks_MediaLists.SelectedIndex >= 0){
					   GuiTools.RightDock.MediaListTools.LoadSelectedMediaList(RightDocks_MediaLists.SelectedItem.ToString());
					   RightDocks_BottomPanel_Media.Open();
				   }
			   }


			   /// <summary>Loads the selected MediaList</summary>
			   private void RightDocks_MediaLists_LoadButton_Click(object sender, System.EventArgs e){
				   if (this.RightDocks_MediaLists.SelectedIndex >= 0){
					   GuiTools.RightDock.MediaListTools.LoadSelectedMediaList(RightDocks_MediaLists.SelectedItem.ToString());
					   RightDocks_BottomPanel_Media.Open();
				   }
			   }

			   /// <summary>Deletes the Selected MediaList, after Userconfirmation</summary>
			   private void RightDocks_MediaLists_DeleteButton_Click(object sender, System.EventArgs e){
				   if (this.RightDocks_MediaLists.SelectedIndex >= 0){
					   if (System.IO.File.Exists("MediaLists\\"+RightDocks_MediaLists.SelectedItem.ToString()+".xml")){
							DialogResult answer = MessageBox.Show(RightDocks_MediaLists.SelectedItem.ToString()+ " wirklich löschen?","MediaList löschen..", MessageBoxButtons.YesNo);
							if (answer == DialogResult.Yes){
								System.IO.File.Delete("MediaLists\\"+RightDocks_MediaLists.SelectedItem.ToString()+".xml");
						   }
					   }
					   this.ListMediaLists();
				   }
			   }


			   /// <summary>Function to Save the current MediaList</summary>
			   void SaveMediaList(){
				   MediaList.Save();
				   this.ListMediaLists();
				   this.StatusPanel.Text = "MediaList '"+ MediaList.Name  +"' saved.";
			   }

			#endregion

			private void Presentation_Fade_Refresh_Button_Click(object sender, System.EventArgs e){
				GuiTools.Presentation.fillTree();
			 }

			private void treeView1_BeforeExpand(object sender, System.Windows.Forms.TreeViewCancelEventArgs e) {
				this.GuiTools.Presentation.treeView1_BeforeExpand (sender,e);
			}

			private void treeView1_BeforeSelect(object sender, System.Windows.Forms.TreeViewCancelEventArgs e) {
				   GuiTools.Presentation.getSubDirs(e.Node);     // get the sub-folders for the selected node.
				   GuiTools.Presentation.ListFiles(GuiTools.Presentation.fixPath(e.Node));
				   GuiTools.Presentation.strMediaPath = GuiTools.Presentation.fixPath(e.Node);
				   folder = new DirectoryInfo(e.Node.FullPath); // get it's Directory info.
			}

			private void Presentation_Fade_ListView_DoubleClick(object sender, System.EventArgs e) {
				this.GuiTools.Presentation.Fade_ToPlaylist_Button_Click(sender,e);
			}

			/// <summary>Show the Media on Projector Window</summary>
			private void Media2BeamBox(){
				// send Loop Video bool to ShowBeam
				this.ShowBeam.LoopMedia = Presentation_MediaLoop_Checkbox.Checked;
				// check if presentation Window is open
				 if(ToolBars_MainToolbar_ShowBeamBox.Checked == false){
					 ToolBars_MainToolbar_ShowBeamBox.Checked = true;
					 ShowBeam.Show();
				 }

				 ShowBeam.strMediaPlaying = null;
				 Presentation_PauseButton.Enabled = true;
				 Presentation_StopButton.Enabled = true;
				 Presentation_PlayButton.Enabled = false;

				 if(MediaList.GetType(MediaFile) == "movie" && this.LoadingVideo){
					 this.VideoLoadTimer.Enabled = true;
				 }else{
					 if(MediaList.GetType(MediaFile) == "flash"){
						 try{
							Media_TrackBar.Maximum = axShockwaveFlash.TotalFrames;
							PlayProgress.Enabled = true;
						 }catch{return;}
					 }
					 if(MediaList.GetType(MediaFile) == "movie"){
						 try{
							Media_TrackBar.Maximum = (int)video.Duration;
							PlayProgress.Enabled = true;
						 }catch{}
					 }
					 if(MediaList.GetType(MediaFile) == "image"){
					   ShowBeam.Songupdate = true;
					 }
					ShowBeam.AudioVolume = AudioBar.Value;
					ShowBeam.ShowMedia(MediaFile);
				 }
				 GC.Collect();
			 }

		#endregion

		#region SermonTools

		 private void linkLabel1_Click(object sender, System.EventArgs e) {
			 System.Diagnostics.Process.Start("http://www.crosswire.org/sword/software/index.jsp");
		 }

		 private void BibleInit() {

			if(Config.BibleLang == "de"){
			 BibleBooks[0] = "1. Mose,2. Mose,3. Mose,4. Mose,5. Mose,Josua,Richter,Rut,1. Samuel,2. Samuel,1. Könige,2. Könige,1. Chronik,2. Chronik,Esra,Nehemia,Ester,Hiob,Psalmen,Prediger,Prediger,Hoheslied,Jesaja,Jeremia,Klagelieder,Hesekiel,Daniel,Hosea,Joel,Amos,Obadja,Jona,Micha,Nahum,Habakuk,Zefanja,Haggai,Sacharja,Maleachi";
			 BibleBooks[1] = "Matthäus,Markus,Lukas,Johannes,Apostelgeschichte,Römer,1. Korinther,2. Korinther,Galater,Epheser,Philipper,Kolosser,1. Thessalonicher,2. Thessalonicher,1. Timotheus,2. Timotheus,Titus,Philemon,Hebräer,Jakobus,1. Petrus,2. Petrus,1. Johannes,2. Johannes,3. Johannes,Judas,Offenbahrung";
			}
			 BibleBooks[0] = "Genesis,Exodus,Leviticus,Numbers,Deuteronomy,Joshua,Judges,Ruth,1 Samuel,2 Samuel,I Kings,II Kings,1 Chronicles,2 Chronicles,Ezra,Nehemiah,Esther,Job,Psalm,Proverbs,Ecclesiastes,Song of Solomon,Isaiah,Jeremiah,Lamentations,Ezekiel,Daniel,Hosea,Joel,Amos,Obadiah,Jonah,Micah,Nahum,Habakkuk,Zephaniah,Haggai,Zechariah,Malachi";
			 BibleBooks[1] = "Matthew,Mark,Luke,John,Acts,Romans,1 Corinthians,2 Corinthians,Galatians,Ephesians,Philippians,Colossians,1 Thessalonians,2 Thessalonians,1 Timothy,2 Timothy,Titus,Philemon,Hebrews,James,1 Peter,2 Peter,1 John,2 John,3 John,Jude,Revelation";

			 if (this.SwordProject_Found) {
				 Diatheke.locale = this.Sermon_BibleLang;
				 Diatheke.book = "system";
				 Diatheke.key = "modulelistnames";
				 Diatheke.maxverses = 20;
				 Diatheke.query();
				 //split the book list by line into an array
				 String[] Booklist = Diatheke.value.Substring(0,Diatheke.value.Length-1).Split((char)10);
				 // Do a simple Query
				 Diatheke.book = "KJV";
				 Diatheke.key = "John 1:1";
				 Diatheke.query();
			 	 Diatheke.outputformat = Convert.ToInt16(5);
				 // and add them each to the list control
				 int i = 0;
				 int match = 0;
				 foreach (string Book in Booklist) {
					 Sermon_Books.Items.Add(Book);
					 BibleText_Translations.Items.Add(Book);
					 //Diatheke.book = Booklist[0];
					 if (Book == "RomCor") 
					 {
						 match = i;
					 }
					 i++;
				 }
				 this.Sermon_Books.SelectedIndex = match;
				 BibleText_Translations.SelectedIndex = match;
				 BibleText_Results_Update();
				 Diatheke.autoupdate = true;
				 this.Diatheke.ValueChanged += new System.EventHandler(this.Diatheke_ValueChanged);
				 BibleText_Translations.SelectedIndexChanged += new EventHandler(BibleText_Translations_SelectedIndexChanged);
				 //split the book list by line into an array
				 String[] Books = BibleBooks[0].Split(',');
				 foreach (string Book in Books) {
					 Sermon_BookList.Items.Add(Book);
				 }
			 }
		 }

		 private void Sermon_Testament_ListBox_SelectedIndexChanged(object sender, System.EventArgs e) {
			 Sermon_BookList.Items.Clear();
			 //split the book list by line into an array
			 String[] Books = BibleBooks[Sermon_Testament_ListBox.SelectedIndex].Split(',');
			 foreach (string Book in Books) {
				 Sermon_BookList.Items.Add(Book);
			 }
		 }

	private void Diatheke_ValueChanged(object sender, System.EventArgs e) 
	{
		if (this.SwordProject_Found) 
		{
			Encoding utf8 = Encoding.GetEncoding("UTF-8");
			Encoding win1252 = Encoding.GetEncoding("Windows-1252");

			string strTText = Diatheke.value;

			//byte[] utf8Bytes = utf8.GetBytes(strTText);
			byte[] win1252Bytes = win1252.GetBytes(strTText);

			//string strCorrect = utf8.GetString(win1252Bytes);

			string strCorrect = Diatheke_ConvertEncoding(Diatheke.value);
			string strTempText = strCorrect;
			this.StatusPanel.Text = strCorrect;

			// filter out the Verses
				string needle = strTempText.Substring(0,strTempText.IndexOf(":"));
				int pos;
				while(strTempText.IndexOf(needle) >= 0){
					pos = strTempText.IndexOf(needle);
					string tmp1 = strTempText.Substring(pos,strTempText.IndexOf(":",pos+needle.Length+1)-pos+1);
					strTempText = strTempText.Replace(tmp1+"   ","");
					strTempText = strTempText.Replace(tmp1+"  ","");
					strTempText = strTempText.Replace(tmp1+" ","");
					strTempText = strTempText.Replace(tmp1,"");
				}
				 strTempText.Replace("{~}","");
			
			// if not ShowBibleTranslation Hide the Translation Text
			if (this.Sermon_ShowBibleTranslation) 
			{
				Sermon_DocManager.FocusedDocument.Control.Text = strTempText;
			} 
			else 
			{
				Sermon_DocManager.FocusedDocument.Control.Text = strTempText.Substring(0,strTempText.IndexOf("("+Diatheke.book+")")-1);
			}
			// Tab text
			Sermon_DocManager.FocusedDocument.Text = Diatheke.key;
		}
	}

		 private void Sermon_BibleKey_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
			 if (this.SwordProject_Found) {
				 if (e.KeyValue == 13) {
					 this.Diatheke.book = Sermon_Books.Items[Sermon_Books.SelectedIndex].ToString();
					 Diatheke.query();
				 }
			 }
		 }

		 public void Sermon_NewDocument() {
			 System.Windows.Forms.RichTextBox t = new System.Windows.Forms.RichTextBox();
			 t.BorderStyle = BorderStyle.None;
			 t.ScrollBars = RichTextBoxScrollBars.Both;
			 t.WordWrap = true;
			 t.Multiline = true;
			 t.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Both;
			 t.Font = new Font("Courier New", 10, FontStyle.Regular);
			 DocumentManager.Document document = new DocumentManager.Document(t, "Document ");
			 Sermon_DocManager.AddDocument(document);
		 }

		 private void Sermon_ToolBar_ButtonClick(object sender, TD.SandBar.ToolBarItemEventArgs e) {
			 // new Document
			 if (e.Item == Sermon_ToolBar_NewDoc_Button) {
				 this.Sermon_NewDocument();
			 }
			 // Font + Size
			 if (e.Item == Sermon_ToolBar_Font_Button) {
				 this.SongEdit_fontDialog.Font = new Font(ShowBeam.Sermon.FontFace[1],ShowBeam.Sermon.FontSize[1]);
				 this.SongEdit_fontDialog.ShowDialog();
				 ShowBeam.Sermon.FontFace[1]=this.SongEdit_fontDialog.Font.Name;
				 ShowBeam.Sermon.FontSize[1]=this.SongEdit_fontDialog.Font.Size;
			 }
			 // FontColor
			 if (e.Item == Sermon_ToolBar_Color_Button) {
				 SongEdit_TextColorDialog.Color = ShowBeam.Sermon.TextColor[1];
				 SongEdit_TextColorDialog.ShowDialog();
				 ShowBeam.Sermon.TextColor[1] = this.SongEdit_TextColorDialog.Color;
			 }
			 // Outline
			 if (e.Item == Sermon_ToolBar_Outline_Button) {
				 if (ShowBeam.Sermon.TextEffect[1] == "Filled Outline") {
					 ShowBeam.Sermon.TextEffect[1] = "Normal";
					 Sermon_ToolBar_OutlineColor_Button.Visible = false;
				 } else {
					 ShowBeam.Sermon.TextEffect[1] = "Filled Outline";
					 Sermon_ToolBar_OutlineColor_Button.Visible = true;
				}
			}

			if (e.Item == Sermon_ToolBar_OutlineColor_Button) {
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
			 if (this.SwordProject_Found) {
				 this.Diatheke.autoupdate = false;
				 this.Diatheke.key = Sermon_BibleKey.Text;
				 this.Diatheke.autoupdate = true;
			 }
		 }

		 private void Sermon_BookList_SelectedIndexChanged(object sender, System.EventArgs e) {
			 this.Sermon_BibleKey.Text = Sermon_BookList.SelectedItem.ToString();
		 }

		 private void Sermon_Books_SelectedIndexChanged(object sender, System.EventArgs e) {
			if (this.SwordProject_Found) {
			 //      this.Diatheke.book = Sermon_Books.Items[Sermon_Books.SelectedIndex].ToString();
			 }
		 }

		 private void Sermon_BeamBox_Button_Click(object sender, System.EventArgs e) {
			 if(ToolBars_MainToolbar_ShowBeamBox.Checked == false) {
				 ToolBars_MainToolbar_ShowBeamBox.Checked = true;
				 ShowBeam.Show();
			 }

			 if(Sermon_DocManager.TabStrips.Count > 0) {
				 ShowBeam.Sermon.SetText(this.Sermon_DocManager.FocusedDocument.Control.Text,1);
				 ShowBeam.Sermon.posX[1] = 200;
				 ShowBeam.Sermon.posY[1] = 200;
				 //      ShowBeam.Sermon.TextEffect[1] = "Filled Outline";
				//      ShowBeam.Sermon.OutlineColor[1] = Color.Black;
			   ShowBeam.Songupdate = true;
				 ShowBeam.PaintSermon();
			 }
		 }


		 private bool Check_SwordProject() {
			 string strSwordConfDir = System.IO.Directory.GetDirectoryRoot(Tools.DreamBeamPath()) +"etc";
			 string strSwordConfPath = strSwordConfDir +"\\sword.conf";
			 bool found = false;
			 bool CreateConf = false;
			 //if /etc directory not found, but config.swordpath is right, then create the /etc directory
			 if (System.IO.File.Exists(strSwordConfPath)==false && System.IO.File.Exists(this.Config.SwordPath+"sword.exe")) {
				 if  (System.IO.Directory.Exists(strSwordConfDir)==false) {
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
			 if (System.IO.File.Exists(strSwordConfPath) && CreateConf == false) {
				 StreamWriter OutputFile;
				 OutputFile = File.CreateText(strSwordConfPath + ".new");
				 StreamReader InputFile = File.OpenText(strSwordConfPath);
				 string input = null;
				 while ((input = InputFile.ReadLine()) != null) {
					 if(input.Substring(0,8) == "DataPath") {
						 if(System.IO.File.Exists(input.Substring(9,input.Length-9).Replace("/","\\")+"sword.exe")) {
							 this.Config.SwordPath = input.Substring(9,input.Length-9).Replace("/","\\");
							 found = true;
							 //OutputFile.WriteLine(input);
						 }
						 if(System.IO.File.Exists(this.Config.SwordPath+"sword.exe")) {
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
			 if (found) {
				 return true;
			 } else {
				 return false;
			 }
		 }
/*
		 private void Sermon_BibleKey_KeyDown1(object sender, System.Windows.Forms.KeyEventArgs e) {
			 if (e.KeyValue == 13) {
				 this.Diatheke.book = Sermon_Books.Items[Sermon_Books.SelectedIndex].ToString();
				 Diatheke.query();
			 }
		 }
*/

		#endregion

		#region ContextMenu Things

		 private void ImageContextItemManage_Click(object sender, System.EventArgs e) {
			 System.Diagnostics.Process.Start("explorer"," "+Tools.DreamBeamPath()+"\\Backgrounds");
		 }

		 private void ImageContextItemReload_Click(object sender, System.EventArgs e) {
			GuiTools.RightDock.BGImageTools.ListDirectories(@"Backgrounds\");
			GuiTools.RightDock.BGImageTools.ListImages(@"Backgrounds\");
		 }

		 #endregion

	#endregion
																						 //under development
	#region DragDrop
	private void Presentation_Fade_ListView_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e){
		// Get the index of the item the mouse is below.
		indexOfItemUnderMouseToDrag = Presentation_Fade_ListView.GetItemAt(e.X, e.Y).Index;
		if (indexOfItemUnderMouseToDrag != ListBox.NoMatches) {
			// Remember the point where the mouse down occurred. The DragSize indicates
			// the size that the mouse can move before a drag event should be started.
			Size dragSize = SystemInformation.DragSize;
			// Create a rectangle using the DragSize, with the mouse position being
			// at the center of the rectangle.
			dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width /2),e.Y - (dragSize.Height /2)), dragSize);
		 } else
			 // Reset the rectangle if the mouse is not over an item in the ListBox.
			 dragBoxFromMouseDown = Rectangle.Empty;
	 }

	private void Presentation_Fade_ListView_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e){
		// Reset the drag rectangle when the mouse button is raised.
		dragBoxFromMouseDown = Rectangle.Empty;
	}

	private void Presentation_Fade_ListView_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e){
		if ((e.Button & MouseButtons.Left) == MouseButtons.Left) {
			 // If the mouse moves outside the rectangle, start the drag.
			 if (dragBoxFromMouseDown != Rectangle.Empty && !dragBoxFromMouseDown.Contains(e.X, e.Y)) {
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
						 //        Presentation_Fade_ListView.SelectedIndex = indexOfItemUnderMouseToDrag -1;
						 //        Presentation_Fade_ListView.SelectedIndex = 1;}
						 //indexOfItemUnderMouseToDrag -1;
						 else if (Presentation_Fade_ListView.Items.Count > 0){}
						 // Selects the first item.
						 //        Presentation_Fade_ListView.SelectedIndex =0;
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

	private void RightDocks_BottomPanel_MediaListView_DragOver(object sender, System.Windows.Forms.DragEventArgs e){
		// Determine whether string data exists in the drop data. If not, then
		// the drop effect reflects that the drop cannot occur.
		 if (!e.Data.GetDataPresent(typeof(System.String))) {
			 e.Effect = DragDropEffects.None;
			 return;
		 }
		 // Set the effect based upon the KeyState.
		 if ((e.KeyState & (8+32)) == (8+32) &&(e.AllowedEffect & DragDropEffects.Link) == DragDropEffects.Link) {
			// KeyState 8 + 32 = CTL + ALT
			// Link drag and drop effect.
			e.Effect = DragDropEffects.Link;
		 } else if ((e.KeyState & 32) == 32 && (e.AllowedEffect & DragDropEffects.Link) == DragDropEffects.Link) {
			// ALT KeyState for link.
			e.Effect = DragDropEffects.Link;
		} else if ((e.KeyState & 4) == 4 &&	(e.AllowedEffect & DragDropEffects.Move) == DragDropEffects.Move) {
			 // SHIFT KeyState for move.
			 e.Effect = DragDropEffects.Move;
		 } else if ((e.KeyState & 8) == 8 && (e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy) {
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
		 /* indexOfItemUnderMouseToDrop =
		 RightDocks_BottomPanel_MediaListView.IndexFromPoint(RightDocks_BottomPanel_MediaListView.PointToClient(new Point(e.X, e.Y)));*/
	 }

	private void RightDocks_BottomPanel_MediaListView_QueryContinueDrag(object sender, System.Windows.Forms.QueryContinueDragEventArgs e){
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


	#region Hide Elements

	private void SongShow_HideTitle_Button_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e){
		if(!ShowBeam.HideTitle)
			SongShow_HideTitle_Button.Checked = false;
	}

	private void SongShow_HideTitle_Button_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e){
		if(ShowBeam.HideTitle)
			ShowBeam.HideTitle = false;
		else
			ShowBeam.HideTitle = true;

		ShowBeam.Prerenderer.RenderAllThreaded();
	 }




	#endregion

	private void ToolBars_MenuBar_Sermon_Exit_Activate(object sender, System.EventArgs e)
	{
		this.Close();
	}

	private void BibleText_Translations_SelectedIndexChanged(object sender, System.EventArgs e)
	{
		BibleText_Results_Update();
	}

	private string Diatheke_ConvertEncoding(string text) 
	{
		Encoding utf8 = Encoding.GetEncoding("UTF-8");
		Encoding win1252 = Encoding.GetEncoding("Windows-1252");

		byte[] rawBytes = win1252.GetBytes(text);
		return utf8.GetString(rawBytes);
	}

	private void BibleText_Results_Update()
	{
		string book = BibleText_Translations.SelectedItem.ToString();
		if (SwordProject_Found && (book.Length > 1))
		{
			Diatheke.autoupdate = false;
			Diatheke.book = book;
			//Diatheke.key = "localelist";
			//Diatheke.key = "modulelist";
			//Diatheke.key = "modulelistnames";
			//Diatheke.key = "modulelistdescriptions";
			Diatheke.key = "2 Cor 1-10";
			Diatheke.query();
			
			BibleText_Results.Text = "";
			BibleText_Results.SelectionStart = 0;
			BibleText_Results.SelectionLength = 0;
			Font refFont = new Font(BibleText_Results.SelectionFont, FontStyle.Bold);
			Font verseFont = new Font(BibleText_Results.SelectionFont, FontStyle.Regular);

			// We need to match things like "II Corinthians 1:7"
			Regex r = new Regex(@"^([\d\w]*\s*\w+ \d+:\d+)\W+(.*)", RegexOptions.Compiled);
			Regex r2 = new Regex(@"^([I]*)", RegexOptions.Compiled);
			Match m;
			string reference, verse;

			string[] verses = Diatheke_ConvertEncoding(Diatheke.value).Split('\n');
			foreach (string v in verses)
			{
				m = r.Match(v);
				/*
				BibleText_Results.AppendText("Groups: " + m.Groups.Count + "\n");
				for (int i = 0; m.Groups[i].Value != ""; i++) 
				{
					BibleText_Results.AppendText("Group: " + i.ToString() +
						"  Index: " + m.Groups[i].Index.ToString() +
						"  Value: " + m.Groups[i].Value.ToString() +
						"\n"
						);
				}
				*/

				if (m.Groups.Count == 3) 
				{
					// We have a good match
					// Groups[0] is the full match
					reference = m.Groups[1].Value;
					reference = Regex.Replace(reference, @"^II", "2");
					reference = Regex.Replace(reference, @"^I", "1");

					verse = m.Groups[2].Value;

					BibleText_Results.SelectionColor = System.Drawing.Color.Navy;
					BibleText_Results.SelectionColor = System.Drawing.Color.Green;
					BibleText_Results.SelectionFont = refFont;
					BibleText_Results.AppendText(reference);

					BibleText_Results.SelectionColor = System.Drawing.Color.Black;
					BibleText_Results.SelectionFont = verseFont;
					BibleText_Results.AppendText(" " + verse + "\n");
				}
				else
				{
					// We don't know how to handle this so let's just show the text as is
					BibleText_Results.AppendText(v);
				}
			}

			BibleText_Results.SelectAll();
			BibleText_Results.SelectionIndent = 5;
			BibleText_Results.SelectionHangingIndent = 50;
			BibleText_Results.SelectionRightIndent = 5;

			Diatheke.autoupdate = true;
		}
	}

	private void BibleText_Results_Highlight(string regex)
	{
		Regex r = new Regex(regex, RegexOptions.Multiline);
		Match m;

		for (m = r.Match(BibleText_Results.Text); m.Success; m = m.NextMatch())
		{
			//m.Value;
			BibleText_Results.SelectionStart = m.Index;
			BibleText_Results.SelectionLength = m.Length;
			BibleText_Results.SelectionColor = System.Drawing.Color.Red;
		}

	}

	private void BibleText_RegEx_ComboBox_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
	{
		BibleText_Results_Update(); // Clear old highlights
		if (BibleText_RegEx_ComboBox.Text.Length > 2)
		{
			BibleText_Results_Highlight(BibleText_RegEx_ComboBox.Text);
		}

		/*
		if (e.KeyValue = 13) 
		{
			// Switch focus to the RTF component and handle forward and backward searching
		}
		*/	
	}




 }
}

