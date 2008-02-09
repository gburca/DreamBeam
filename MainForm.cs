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
using DreamBeam.FileTypes;
using DreamBeam.Bible;
using Lister;
using CommandLine.Utility;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using CookComputing.XmlRpc;
using Utils;
using Utils.MessageBoxExLib;
using cs_IniHandlerDevelop;
using ControlLib;
using Controls.Development;
//using Microsoft.DirectX.Direct3D;

namespace DreamBeam {

	#region Enums
	public enum MainTab {
		ShowSongs = 0,
		EditSongs = 1,
		SermonTools = 2,
		Presentation = 3,
		BibleText = 4
	}

	public enum DiathekeOutputFormat {
		PlainText,
		ThML,
		GBF,
		RTF,
		HTML
	}
	#endregion

	/// <summary>
	/// The Main DreamBeam Window with most of it's functions
	/// </summary>
	public class MainForm : System.Windows.Forms.Form {

		#region Var and Obj Declarations

		#region Own Vars and Object
		public GuiTools GuiTools = null;

		public string version = "0.49";
		/// <summary>
		/// A prefix for all configuration files. The application can be started with a "--config" option that overrides this name.
		/// </summary>
		public string ConfigSet = "default";
		public Bitmap memoryBitmap = null;
		public ShowBeam ShowBeam = new ShowBeam();
		private Song Song = new Song();
		public Options Options = null;
		public Config Config;
		public MainTab selectedTab = MainTab.ShowSongs;
		public int SongCount = 0;
		public int Song_Edit_Box = 2;
		public bool beamshowed = false;

		private Splash Splash = null;
		public bool LoadingBGThumbs = false;
		static Thread Thread_LoadMovie = null;

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
		public string MediaFile;
		bool VideoLoaded = false;
		bool VideoProblem = false;
		bool LoadingVideo = false;
		public Microsoft.DirectX.AudioVideoPlayback.Video video = null;
		int iFilmEnded = 0;
		public Language Lang = new Language();
		public System.Drawing.Font EditorFont = null;

		public BibleLib bibles = new BibleLib();
		public EventHandler BibleText_RegEx_ComboBox_EventHandler;


		#endregion


		#region Toolbars and others Declarations

		private TD.SandBar.SandBarManager ToolBars_sandBarManager1;
		private TD.SandBar.ToolBarContainer ToolBars_leftSandBarDock;
		private TD.SandBar.ToolBarContainer ToolBars_bottomSandBarDock;
		public TD.SandBar.ToolBarContainer ToolBars_topSandBarDock;

		public TD.SandDock.SandDockManager sandDockManager1;
		private TD.SandDock.DockContainer leftSandDock;
		public TD.SandDock.DockContainer rightSandDock;
		private TD.SandDock.DockContainer bottomSandDock;
		private TD.SandDock.DockContainer topSandDock;

		public TD.SandDock.DockControl RightDocks_TopPanel_Songs;
		public TD.SandDock.DockControl RightDocks_BottomPanel_Backgrounds;
		public TD.SandDock.DockControl RightDocks_TopPanel_PlayList;
		public TD.SandDock.DockControl RightDocks_PreviewScreen;
		public TD.SandDock.DockControl RightDocks_LiveScreen;


		#region Menu Bar
		private TD.SandBar.MenuBar ToolBars_MenuBar;
		public TD.SandBar.MenuBarItem ToolBars_MenuBar_Song;
		private TD.SandBar.MenuButtonItem ToolBars_MenuBar_Song_New;
		private TD.SandBar.MenuButtonItem ToolBars_MenuBar_Song_Save;
		private TD.SandBar.MenuButtonItem ToolBars_MenuBar_Song_SaveAs;
		private TD.SandBar.MenuButtonItem ToolBars_MenuBar_Song_Rename;

		public TD.SandBar.MenuBarItem ToolBars_MenuBar_MediaList;
		private TD.SandBar.MenuButtonItem ToolBars_MenuBar_MediaList_New;
		private TD.SandBar.MenuButtonItem ToolBars_MenuBar_Media_Save;
		private TD.SandBar.MenuButtonItem ToolBars_MenuBar_Media_SaveAs;
		private TD.SandBar.MenuButtonItem ToolBars_MenuBar_Media_Rename;

		public TD.SandBar.MenuBarItem ToolBars_MenuBar_Edit;
		private TD.SandBar.MenuButtonItem ToolBars_MenuBar_Edit_Options;
		public TD.SandBar.MenuBarItem ToolBars_MenuBar_View;
		private TD.SandBar.MenuBarItem ToolBars_MenuBar_Help;
		private TD.SandBar.MenuButtonItem HelpIntro;
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
		public TD.SandBar.ButtonItem ShowSongs_Button;
		public TD.SandBar.ButtonItem EditSongs_Button;
		#endregion

		#region Right Docks

		#region SongList
		private System.Windows.Forms.TextBox RightDocks_SongListSearch;
		private System.Windows.Forms.Button btnRightDocks_SongList2PlayList;
		private System.Windows.Forms.Button btnRightDocks_SongListDelete;
		#endregion

		#region PlayList
		private System.Windows.Forms.ListBox RightDocks_PlayList;
		private System.Windows.Forms.Button RightDocks_PlayList_Load_Button;
		private System.Windows.Forms.Button RightDocks_PlayList_Remove_Button;
		private System.Windows.Forms.Button RightDocks_PlayList_Up_Button;
		private System.Windows.Forms.Button RightDocks_PlayList_Down_Button;
		#endregion

		#region SearchList

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
		private System.Windows.Forms.Timer TextTypedTimer;

		private Extensions.Set SongCollections = new Extensions.Set();

		#endregion

		public TD.SandDock.DockControl RightDocks_BottomPanel_Media;

		#region Presentation

		private System.Windows.Forms.ImageList imageList_Folders;
		private System.Windows.Forms.ImageList Presentation_Fade_ImageList;
		public TD.SandBar.ButtonItem Presentation_Button;
		public System.Windows.Forms.ImageList Media_ImageList;
		public System.Windows.Forms.ImageList Media_Logos;
		private System.Windows.Forms.Timer PlayProgress;
		private System.Windows.Forms.ImageList PlayButtons_ImageList;
		private System.Windows.Forms.Timer VideoLoadTimer;
		#endregion

		#region SermonTools

		string[] BibleBooks = new string[2];
		public AxACTIVEDIATHEKELib.AxActiveDiatheke Diatheke;
		public TD.SandBar.ButtonItem Sermon_Button;
		private string Sermon_BibleLang = "en";
		private bool Sermon_ShowBibleTranslation = false;
		private bool SwordProject_Found = false;
		#endregion

		#region BibleText
		BibleVersion BibleText_Bible = null;
		private readonly string bibleLibFile;
		#endregion

		#region Displays
		public Display DisplayPreview;
		public Display DisplayLiveMini;
		public Display DisplayLiveLocal;
		public Display DisplayLiveClient;
		public Display DisplayLiveServer;
		public XmlRpcServer xmlRpcServer;
		#endregion

		private Control ErrorProvider_LastControl = null;

		#endregion

		#region Designer variables and objects

		private Salamander.Windows.Forms.CollapsiblePanelBar SongShow_CollapsPanel;
		private Salamander.Windows.Forms.CollapsiblePanel SongShow_HideElementsPanel;
		private ctlLEDRadioButton.LEDradioButton SongShow_HideTitle_Button;
		private ctlLEDRadioButton.LEDradioButton SongShow_HideText_Button;
		private ctlLEDRadioButton.LEDradioButton SongShow_HideAuthor_Button;
		private System.Windows.Forms.Panel SongShow_HideElementsSub1Panel;


		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel3;

		private TD.SandBar.MenuButtonItem ToolBars_MenuBar_View_ShowSongs;
		private TD.SandBar.MenuButtonItem ToolBars_MenuBar_View_EditSongs;
		private TD.SandBar.MenuButtonItem ToolBars_MenuBar_View_Presentation;
		private TD.SandBar.MenuButtonItem ToolBars_MenuBar_View_TextTool;
		public System.Windows.Forms.ProgressBar RenderStatus;
		public TD.SandBar.ButtonItem BibleText_Button;
		private System.Windows.Forms.ListBox BibleText_Translations;
		private System.Windows.Forms.ListBox BibleText_Bookmarks;
		private System.Windows.Forms.PictureBox RightDocks_LiveScreen_PictureBox;
		private System.Windows.Forms.PictureBox RightDocks_PreviewScreen_PictureBox;
		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.Panel panel9;
		private TD.SandBar.MenuButtonItem ToolBars_MenuBar_View_BibleText;
		private TD.SandBar.MenuButtonItem ToolBars_MenuBar_Open_PreviewTab;
		private TD.SandBar.MenuButtonItem ToolBars_MenuBar_Open_LiveTab;
		private TD.SandBar.MenuButtonItem ToolBars_MenuBar_Open_BackgroundsTab;
		private TD.SandBar.MenuButtonItem ToolBars_MenuBar_Open_MediaListTab;
		private TD.SandBar.MenuButtonItem ToolBars_MenuBar_Open_SongsTab;
		private TD.SandBar.MenuButtonItem ToolBars_MenuBar_Open_PlaylistTab;
		private TD.SandBar.MenuButtonItem ToolBars_MenuBar_Open_MediaTab;
		private System.Windows.Forms.Button RightDocks_Preview_Next;
		private System.Windows.Forms.Button RightDocks_Preview_GoLive;
		private System.Windows.Forms.Button RightDocks_Preview_Prev;
		private System.Windows.Forms.Button RightDocks_Live_Next;
		private System.Windows.Forms.Button RightDocks_Live_Prev;
		private TD.SandDock.DockControl Dock_BibleTools;
		private Salamander.Windows.Forms.CollapsiblePanel BibleBookmarks_CollapsiblePanel;
		private Salamander.Windows.Forms.CollapsiblePanel BibleTranslations_CollapsiblePanel;
		private TD.SandBar.MenuButtonItem ToolBars_MenuBar_Open_BibleToolsTab;
		private Salamander.Windows.Forms.CollapsiblePanelBar BibleTools_CollapsiblePanelBar;
		private System.Windows.Forms.ToolTip MainForm_ToolTip;
		private TD.SandDock.DockControl Dock_SongTools;
		private TD.SandBar.MenuButtonItem ToolBars_MenuBar_Open_SongToolsTab;
		private System.Windows.Forms.ErrorProvider Main_ErrorProvider;
		private System.Windows.Forms.Button RightDocks_Backgrounds_UseDefault;
		private System.Windows.Forms.Panel panel6;
		private TD.SandBar.MenuBarItem ToolBars_MenuBar_File;
		private TD.SandBar.MenuButtonItem ToolBars_MenuBar_File_Exit;
		private System.Windows.Forms.ImageList SongShow_ImageList;
		private System.Windows.Forms.SaveFileDialog SaveFileDialog;
		public System.Windows.Forms.TabPage ShowSong_Tab;
		public Lister.ListEx SongShow_StropheList_ListEx;
		public System.Windows.Forms.TabPage SermonTools_Tab;
		private System.Windows.Forms.Panel Sermon_LeftPanel;
		private System.Windows.Forms.Panel Sermon_LeftBottom_Panel;
		private System.Windows.Forms.Button Sermon_Preview_Button;
		private System.Windows.Forms.Button Sermon_BeamBox_Button;
		private System.Windows.Forms.Panel Sermon_LeftDoc_Panel;
		public DocumentManager.DocumentManager Sermon_DocManager;
		private System.Windows.Forms.Panel Sermon_LeftToolBar_Panel;
		private TD.SandBar.ToolBar Sermon_ToolBar;
		private TD.SandBar.ButtonItem Sermon_ToolBar_NewDoc_Button;
		private System.Windows.Forms.TabControl Sermon_TabControl;
		public System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.Label Sermon_Verse_Label;
		private System.Windows.Forms.Label Sermon_Translation_Label;
		private System.Windows.Forms.Label Sermon_Books_Label;
		private System.Windows.Forms.TextBox Sermon_BibleKey;
		private System.Windows.Forms.ListBox Sermon_Testament_ListBox;
		private System.Windows.Forms.ComboBox Sermon_Books;
		private System.Windows.Forms.ListBox Sermon_BookList;
		public System.Windows.Forms.TabPage Presentation_Tab;
		private System.Windows.Forms.Panel Presentation_FadePanel;
		private System.Windows.Forms.Panel Fade_panel;
		public System.Windows.Forms.ListView Presentation_Fade_ListView;
		private System.Windows.Forms.Panel Fade_Top_Panel;
		private System.Windows.Forms.Button Presentation_Fade_Refresh_Button;
		private System.Windows.Forms.Button Presentation_Fade_ToPlaylist_Button;
		public System.Windows.Forms.PictureBox Presentation_Fade_preview;
		public System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.Panel Presentation_MainPanel;
		private System.Windows.Forms.Panel Presentation_PreviewPanel;
		public System.Windows.Forms.Panel Presentation_VideoPanel;
		private AxShockwaveFlashObjects.AxShockwaveFlash axShockwaveFlash;
		public System.Windows.Forms.PictureBox Presentation_PreviewBox;
		private System.Windows.Forms.Panel Presentation_MovieControlPanel;
		private System.Windows.Forms.Panel Presentation_MovieControlPanelBottom;
		private System.Windows.Forms.CheckBox Presentation_MediaLoop_Checkbox;
		private System.Windows.Forms.Panel Presentation_MovieControl_PreviewButtonPanel;
		private System.Windows.Forms.Button Presentation_MoviePreviewButton;
		private System.Windows.Forms.Panel Presentation_MovieControlPanelBottomLeft;
		private System.Windows.Forms.ToolBar Presentation_PlayBar;
		private System.Windows.Forms.ToolBarButton Presentation_PlayButton;
		private System.Windows.Forms.ToolBarButton Presentation_PauseButton;
		private System.Windows.Forms.ToolBarButton Presentation_StopButton;
		private System.Windows.Forms.Panel Presentation_MovieControlPanel_Top;
		private System.Windows.Forms.TrackBar Media_TrackBar;
		private System.Windows.Forms.Panel Presentation_MovieControlPanel_Right;
		private System.Windows.Forms.TrackBar AudioBar;
		public System.Windows.Forms.TabPage EditSongs2_Tab;
		public DreamBeam.SongEditor songEditor;
		public System.Windows.Forms.TabPage BibleText_Tab;
		private System.Windows.Forms.Panel BibleText_panelLeft;
		private DreamBeam.Bible.BibleRTF BibleText_Results;
		private System.Windows.Forms.Panel panel8;
		private System.Windows.Forms.Button BibleText_FindLast_button;
		private System.Windows.Forms.Button BibleText_FindFirst_button;
		private System.Windows.Forms.Button BibleText_FindPrev_button;
		private System.Windows.Forms.Button BibleText_FindNext_button;
		private System.Windows.Forms.ComboBox BibleText_Verse_ComboBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Panel panel7;
		private System.Windows.Forms.Button BibleText_Bookmark_button;
		private System.Windows.Forms.ComboBox BibleText_RegEx_ComboBox;
		private System.Windows.Forms.Label label1;
		public System.Windows.Forms.TabControl tabControl1;
		private TD.SandBar.MenuButtonItem menuButtonItem1;
		private TD.SandBar.MenuButtonItem ToolBars_MenuBar_Import_Song;
		private System.Windows.Forms.OpenFileDialog OpenFileDialog;
		private ControlLib.dbTreeViewCtrl SongList_Tree;
		private System.Data.DataSet GlobalDataSet;
		private System.Data.DataTable SongListTable;
		private System.Data.DataColumn NumberColumn;
		private System.Data.DataColumn TitleColumn;
		private System.Data.DataColumn CollectionColumn;
		private System.Data.DataView SongListDataView;
		private System.Data.DataColumn FileNameColumn;
		private System.Data.DataColumn NumberSortColumn;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Data.DataColumn dataColumn1;
		private System.Data.DataColumn dataColumn2;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.StatusBarPanel statusBarUpdatePanel;

		#endregion

		#region MAIN

		///<summary> Initialise DreamBeam </summary>
		public MainForm(string[] args) {
			// Command line parsing
			Arguments CommandLine = new Arguments(args);
			if (CommandLine["config"] != null) { this.ConfigSet = CommandLine["config"]; }

			// Opens a console if the "-console" argumet is given, so that
			// Console.Write or WriteLine output can be seen. VisualStudio takes
			// over the console output, so this only works when not used from VS.
			if (CommandLine["console"] != null) { Tools.AllocConsole(); }

			bibleLibFile = Path.Combine(Tools.GetAppCachePath(), "BibleLib.bin");

			// Make sure we do the same thing when the Options dialog is "cancelled"
			this.Config = (Config)Config.DeserializeFrom(new Config(),
				Tools.GetDirectory(DirType.Config, ConfigSet + ".config.xml"));

			ShowBeam.LogFile = new LogFile(ConfigSet);

			if (CommandLine["log"] != null) {
				ShowBeam.LogFile.doLog = true;
				ShowBeam.LogFile.BigHeader("Start");
			}
			ShowBeam.Config = this.Config;
			ShowBeam._MainForm = this;
			LyricsSequenceItem.lang = this.Lang;
			GuiTools = new GuiTools(this, this.ShowBeam);

			this.Hide();

			this.SwordProject_Found = this.Check_SwordProject(this.Config);
			Splash.ShowSplashScreen();
			Splash.SetStatus("Initializing");
			InitializeComponent();
			Splash.SetStatus("Checking for Sword Project");
			InitializeDiatheke();

			// Options makes use of Diatheke to get the list of bible translations available
			Options = new Options(this);

			Presentation_FadePanel.Size = new System.Drawing.Size(0, Presentation_FadePanel.Size.Height);
			//Presentation_MovieControlPanel.Size =  new System.Drawing.Size (Presentation_MovieControlPanel.Size.Width,0);

			Splash.SetStatus("Initializing Directory Tree");
			//GuiTools.Presentation.fillTree();
			Thread fillTree = new Thread(new ThreadStart(GuiTools.Presentation.fillTree));
			fillTree.IsBackground = true;
			fillTree.Name = "FillTree";
			fillTree.Start();


			BibleText_RegEx_ComboBox_EventHandler = new EventHandler(BibleText_RegEx_ComboBox_TimedOut);

			if (this.Config.RememberPanelLocations) {
				this.StartPosition = FormStartPosition.Manual;
				this.Location = this.Config.AppDesktopLocation.Location;
				this.Size = this.Config.AppDesktopLocation.Size;
			} else {
				this.StartPosition = FormStartPosition.CenterScreen;
			}


			Splash.SetStatus("Initializing displays");
			InitDisplays();

			this.ShowSongs_Button.Checked = true;
			ToolBars_MainToolbar_ShowBeamBox.Checked = true;

			// The tabs on the main tab control are not being used for navigation,
			// but we need them at a decent size during design. Making them
			// disappear at run time.
			this.tabControl1.ItemSize = new Size(1, 1);

			// Restore the SermonTool documents
			SermonToolDocuments sermon = (SermonToolDocuments)SermonToolDocuments.DeserializeFrom(typeof(SermonToolDocuments),
				Tools.GetDirectory(DirType.Sermon, ConfigSet + ".SermonDocs.xml"));
			if (sermon != null) {
				foreach (string doc in sermon.Documents) {
					DocumentManager.Document d = Sermon_NewDocument();
					d.Control.Text = doc;
					d.Text = getSermonDocumentTitle(doc);
				}
				try {
					Sermon_DocManager.FocusedDocument = Sermon_DocManager.TabStrips[0].Documents[0];
				} catch { }
			}

		}


		/// <summary>
		/// Ressourcen nach der Verwendung bereinigen
		/// </summary>
		protected override void Dispose(bool disposing) {
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
		static void Main(string[] args) {
			try {
				Application.Run(new MainForm(args));
			} catch (Exception e) {
				StreamWriter SW;
				SW = File.AppendText(Tools.GetDirectory(DirType.Logs, "LogFile.txt"));
				SW.WriteLine(e.Message);
				SW.WriteLine(e.StackTrace);
				if (e.InnerException != null) {
					SW.WriteLine(e.InnerException.Message);
					SW.WriteLine(e.InnerException.StackTrace);
				}
				SW.Close();
			}
		}

		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Node2");
			System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Node1", new System.Windows.Forms.TreeNode[] {
            treeNode4});
			System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Node0", new System.Windows.Forms.TreeNode[] {
            treeNode5});
			this.RightDocks_ImageListBox = new Controls.Development.ImageListBox();
			this.ImageContext = new System.Windows.Forms.ContextMenu();
			this.ImageContextItemManage = new System.Windows.Forms.MenuItem();
			this.ImageContextItemReload = new System.Windows.Forms.MenuItem();
			this.RightDocks_imageList = new System.Windows.Forms.ImageList(this.components);
			this.SongEdit_fontDialog = new System.Windows.Forms.FontDialog();
			this.SongEdit_OutlineColorDialog = new System.Windows.Forms.ColorDialog();
			this.SongEdit_TextColorDialog = new System.Windows.Forms.ColorDialog();
			this.RightDocks_Songlist_SearchPanel = new System.Windows.Forms.Panel();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.RightDocks_SongListSearch = new System.Windows.Forms.TextBox();
			this.RightDocks_SongList_ButtonPanel = new System.Windows.Forms.Panel();
			this.btnRightDocks_SongListDelete = new System.Windows.Forms.Button();
			this.btnRightDocks_SongList2PlayList = new System.Windows.Forms.Button();
			this.RightDocks_PlayList = new System.Windows.Forms.ListBox();
			this.RightDocks_TopPanel_PlayList_Button_Panel = new System.Windows.Forms.Panel();
			this.RightDocks_PlayList_Down_Button = new System.Windows.Forms.Button();
			this.RightDocks_PlayList_Up_Button = new System.Windows.Forms.Button();
			this.RightDocks_PlayList_Remove_Button = new System.Windows.Forms.Button();
			this.RightDocks_PlayList_Load_Button = new System.Windows.Forms.Button();
			this.RightDocks_FolderDropdown = new System.Windows.Forms.ComboBox();
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
			this.ToolBars_MenuBar_File = new TD.SandBar.MenuBarItem();
			this.menuButtonItem1 = new TD.SandBar.MenuButtonItem();
			this.ToolBars_MenuBar_Import_Song = new TD.SandBar.MenuButtonItem();
			this.ToolBars_MenuBar_File_Exit = new TD.SandBar.MenuButtonItem();
			this.ToolBars_MenuBar_Edit = new TD.SandBar.MenuBarItem();
			this.ToolBars_MenuBar_Edit_Options = new TD.SandBar.MenuButtonItem();
			this.ToolBars_MenuBar_Song = new TD.SandBar.MenuBarItem();
			this.ToolBars_MenuBar_Song_New = new TD.SandBar.MenuButtonItem();
			this.ToolBars_MenuBar_Song_Save = new TD.SandBar.MenuButtonItem();
			this.ToolBars_MenuBar_Song_SaveAs = new TD.SandBar.MenuButtonItem();
			this.ToolBars_MenuBar_Song_Rename = new TD.SandBar.MenuButtonItem();
			this.ToolBars_MenuBar_MediaList = new TD.SandBar.MenuBarItem();
			this.ToolBars_MenuBar_MediaList_New = new TD.SandBar.MenuButtonItem();
			this.ToolBars_MenuBar_Media_Save = new TD.SandBar.MenuButtonItem();
			this.ToolBars_MenuBar_Media_SaveAs = new TD.SandBar.MenuButtonItem();
			this.ToolBars_MenuBar_Media_Rename = new TD.SandBar.MenuButtonItem();
			this.ToolBars_MenuBar_View = new TD.SandBar.MenuBarItem();
			this.ToolBars_MenuBar_View_ShowSongs = new TD.SandBar.MenuButtonItem();
			this.ToolBars_MenuBar_View_EditSongs = new TD.SandBar.MenuButtonItem();
			this.ToolBars_MenuBar_View_Presentation = new TD.SandBar.MenuButtonItem();
			this.ToolBars_MenuBar_View_TextTool = new TD.SandBar.MenuButtonItem();
			this.ToolBars_MenuBar_View_BibleText = new TD.SandBar.MenuButtonItem();
			this.ToolBars_MenuBar_Open_SongsTab = new TD.SandBar.MenuButtonItem();
			this.ToolBars_MenuBar_Open_PlaylistTab = new TD.SandBar.MenuButtonItem();
			this.ToolBars_MenuBar_Open_BibleToolsTab = new TD.SandBar.MenuButtonItem();
			this.ToolBars_MenuBar_Open_SongToolsTab = new TD.SandBar.MenuButtonItem();
			this.ToolBars_MenuBar_Open_BackgroundsTab = new TD.SandBar.MenuButtonItem();
			this.ToolBars_MenuBar_Open_MediaListTab = new TD.SandBar.MenuButtonItem();
			this.ToolBars_MenuBar_Open_MediaTab = new TD.SandBar.MenuButtonItem();
			this.ToolBars_MenuBar_Open_PreviewTab = new TD.SandBar.MenuButtonItem();
			this.ToolBars_MenuBar_Open_LiveTab = new TD.SandBar.MenuButtonItem();
			this.ToolBars_MenuBar_Help = new TD.SandBar.MenuBarItem();
			this.HelpIntro = new TD.SandBar.MenuButtonItem();
			this.AboutButton = new TD.SandBar.MenuButtonItem();
			this.statusBar = new System.Windows.Forms.StatusBar();
			this.RenderStatus = new System.Windows.Forms.ProgressBar();
			this.StatusPanel = new System.Windows.Forms.StatusBarPanel();
			this.statusBarUpdatePanel = new System.Windows.Forms.StatusBarPanel();
			this.SongShow_ImageList = new System.Windows.Forms.ImageList(this.components);
			this.Presentation_Fade_ImageList = new System.Windows.Forms.ImageList(this.components);
			this.imageList_Folders = new System.Windows.Forms.ImageList(this.components);
			this.PlayButtons_ImageList = new System.Windows.Forms.ImageList(this.components);
			this.SongShow_CollapsPanel = new Salamander.Windows.Forms.CollapsiblePanelBar();
			this.SongShow_HideElementsPanel = new Salamander.Windows.Forms.CollapsiblePanel();
			this.panel3 = new System.Windows.Forms.Panel();
			this.SongShow_HideAuthor_Button = new ctlLEDRadioButton.LEDradioButton();
			this.panel1 = new System.Windows.Forms.Panel();
			this.SongShow_HideText_Button = new ctlLEDRadioButton.LEDradioButton();
			this.SongShow_HideElementsSub1Panel = new System.Windows.Forms.Panel();
			this.SongShow_HideTitle_Button = new ctlLEDRadioButton.LEDradioButton();
			this.BibleBookmarks_CollapsiblePanel = new Salamander.Windows.Forms.CollapsiblePanel();
			this.BibleText_Bookmarks = new System.Windows.Forms.ListBox();
			this.BibleTranslations_CollapsiblePanel = new Salamander.Windows.Forms.CollapsiblePanel();
			this.BibleText_Translations = new System.Windows.Forms.ListBox();
			this.TextTypedTimer = new System.Windows.Forms.Timer(this.components);
			this.sandDockManager1 = new TD.SandDock.SandDockManager();
			this.leftSandDock = new TD.SandDock.DockContainer();
			this.rightSandDock = new TD.SandDock.DockContainer();
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
			this.Dock_BibleTools = new TD.SandDock.DockControl();
			this.BibleTools_CollapsiblePanelBar = new Salamander.Windows.Forms.CollapsiblePanelBar();
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
			this.RightDocks_BottomPanel_Backgrounds = new TD.SandDock.DockControl();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panel6 = new System.Windows.Forms.Panel();
			this.RightDocks_Backgrounds_UseDefault = new System.Windows.Forms.Button();
			this.RightDocks_BottomPanel2_TopPanel = new System.Windows.Forms.Panel();
			this.RightDocks_TopPanel_Songs = new TD.SandDock.DockControl();
			this.SongList_Tree = new ControlLib.dbTreeViewCtrl();
			this.RightDocks_TopPanel_PlayList = new TD.SandDock.DockControl();
			this.RightDocks_PreviewScreen = new TD.SandDock.DockControl();
			this.RightDocks_PreviewScreen_PictureBox = new System.Windows.Forms.PictureBox();
			this.panel5 = new System.Windows.Forms.Panel();
			this.RightDocks_Preview_Next = new System.Windows.Forms.Button();
			this.RightDocks_Preview_GoLive = new System.Windows.Forms.Button();
			this.RightDocks_Preview_Prev = new System.Windows.Forms.Button();
			this.RightDocks_LiveScreen = new TD.SandDock.DockControl();
			this.RightDocks_LiveScreen_PictureBox = new System.Windows.Forms.PictureBox();
			this.panel9 = new System.Windows.Forms.Panel();
			this.RightDocks_Live_Next = new System.Windows.Forms.Button();
			this.RightDocks_Live_Prev = new System.Windows.Forms.Button();
			this.Dock_SongTools = new TD.SandDock.DockControl();
			this.bottomSandDock = new TD.SandDock.DockContainer();
			this.topSandDock = new TD.SandDock.DockContainer();
			this.Media_Logos = new System.Windows.Forms.ImageList(this.components);
			this.PlayProgress = new System.Windows.Forms.Timer(this.components);
			this.VideoLoadTimer = new System.Windows.Forms.Timer(this.components);
			this.Presentation_AutoPlayTimer = new System.Windows.Forms.Timer(this.components);
			this.MainForm_ToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.Sermon_BibleKey = new System.Windows.Forms.TextBox();
			this.BibleText_FindLast_button = new System.Windows.Forms.Button();
			this.BibleText_FindFirst_button = new System.Windows.Forms.Button();
			this.BibleText_FindPrev_button = new System.Windows.Forms.Button();
			this.BibleText_FindNext_button = new System.Windows.Forms.Button();
			this.BibleText_Bookmark_button = new System.Windows.Forms.Button();
			this.Main_ErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
			this.SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.ShowSong_Tab = new System.Windows.Forms.TabPage();
			this.SongShow_StropheList_ListEx = new Lister.ListEx();
			this.SermonTools_Tab = new System.Windows.Forms.TabPage();
			this.Sermon_LeftPanel = new System.Windows.Forms.Panel();
			this.Sermon_LeftDoc_Panel = new System.Windows.Forms.Panel();
			this.Sermon_DocManager = new DocumentManager.DocumentManager();
			this.Sermon_LeftToolBar_Panel = new System.Windows.Forms.Panel();
			this.Sermon_ToolBar = new TD.SandBar.ToolBar();
			this.Sermon_ToolBar_NewDoc_Button = new TD.SandBar.ButtonItem();
			this.Sermon_LeftBottom_Panel = new System.Windows.Forms.Panel();
			this.Sermon_Preview_Button = new System.Windows.Forms.Button();
			this.Sermon_BeamBox_Button = new System.Windows.Forms.Button();
			this.Sermon_TabControl = new System.Windows.Forms.TabControl();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.Sermon_Verse_Label = new System.Windows.Forms.Label();
			this.Sermon_Translation_Label = new System.Windows.Forms.Label();
			this.Sermon_Books_Label = new System.Windows.Forms.Label();
			this.Sermon_Testament_ListBox = new System.Windows.Forms.ListBox();
			this.Sermon_Books = new System.Windows.Forms.ComboBox();
			this.Sermon_BookList = new System.Windows.Forms.ListBox();
			this.Presentation_Tab = new System.Windows.Forms.TabPage();
			this.Presentation_FadePanel = new System.Windows.Forms.Panel();
			this.Fade_panel = new System.Windows.Forms.Panel();
			this.Presentation_Fade_ListView = new System.Windows.Forms.ListView();
			this.Fade_Top_Panel = new System.Windows.Forms.Panel();
			this.Presentation_Fade_Refresh_Button = new System.Windows.Forms.Button();
			this.Presentation_Fade_ToPlaylist_Button = new System.Windows.Forms.Button();
			this.Presentation_Fade_preview = new System.Windows.Forms.PictureBox();
			this.treeView1 = new System.Windows.Forms.TreeView();
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
			this.Presentation_MovieControlPanel_Top = new System.Windows.Forms.Panel();
			this.Media_TrackBar = new System.Windows.Forms.TrackBar();
			this.Presentation_MovieControlPanel_Right = new System.Windows.Forms.Panel();
			this.AudioBar = new System.Windows.Forms.TrackBar();
			this.EditSongs2_Tab = new System.Windows.Forms.TabPage();
			this.songEditor = new DreamBeam.SongEditor();
			this.BibleText_Tab = new System.Windows.Forms.TabPage();
			this.BibleText_panelLeft = new System.Windows.Forms.Panel();
			this.BibleText_Results = new DreamBeam.Bible.BibleRTF();
			this.panel8 = new System.Windows.Forms.Panel();
			this.BibleText_Verse_ComboBox = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.panel7 = new System.Windows.Forms.Panel();
			this.BibleText_RegEx_ComboBox = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.GlobalDataSet = new System.Data.DataSet();
			this.SongListTable = new System.Data.DataTable();
			this.NumberColumn = new System.Data.DataColumn();
			this.TitleColumn = new System.Data.DataColumn();
			this.CollectionColumn = new System.Data.DataColumn();
			this.FileNameColumn = new System.Data.DataColumn();
			this.NumberSortColumn = new System.Data.DataColumn();
			this.dataColumn1 = new System.Data.DataColumn();
			this.dataColumn2 = new System.Data.DataColumn();
			this.SongListDataView = new System.Data.DataView();
			this.RightDocks_Songlist_SearchPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.RightDocks_SongList_ButtonPanel.SuspendLayout();
			this.RightDocks_TopPanel_PlayList_Button_Panel.SuspendLayout();
			this.ToolBars_leftSandBarDock.SuspendLayout();
			this.ToolBars_topSandBarDock.SuspendLayout();
			this.statusBar.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.StatusPanel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarUpdatePanel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.SongShow_CollapsPanel)).BeginInit();
			this.SongShow_CollapsPanel.SuspendLayout();
			this.SongShow_HideElementsPanel.SuspendLayout();
			this.panel3.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SongShow_HideElementsSub1Panel.SuspendLayout();
			this.BibleBookmarks_CollapsiblePanel.SuspendLayout();
			this.BibleTranslations_CollapsiblePanel.SuspendLayout();
			this.rightSandDock.SuspendLayout();
			this.RightDocks_BottomPanel_Media.SuspendLayout();
			this.RightDocks_BottomPanel_Media_Bottom.SuspendLayout();
			this.RightDocks_BottomPanel_Media_Top.SuspendLayout();
			this.Dock_BibleTools.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.BibleTools_CollapsiblePanelBar)).BeginInit();
			this.BibleTools_CollapsiblePanelBar.SuspendLayout();
			this.RightDocks_BottomPanel_MediaLists.SuspendLayout();
			this.RightDocks_BottomPanel_MediaListsTopPanel.SuspendLayout();
			this.RightDocks_BottomPanel_MediaListsTop_Control_Panel.SuspendLayout();
			this.RightDocks_BottomPanel_MediaLists_BottomPanel.SuspendLayout();
			this.RightDocks_BottomPanel_MediaListsBottomPanel_GroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.RightDocks_BottomPanel_MediaLists_Numeric)).BeginInit();
			this.RightDocks_BottomPanel_Backgrounds.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel6.SuspendLayout();
			this.RightDocks_BottomPanel2_TopPanel.SuspendLayout();
			this.RightDocks_TopPanel_Songs.SuspendLayout();
			this.RightDocks_TopPanel_PlayList.SuspendLayout();
			this.RightDocks_PreviewScreen.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.RightDocks_PreviewScreen_PictureBox)).BeginInit();
			this.panel5.SuspendLayout();
			this.RightDocks_LiveScreen.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.RightDocks_LiveScreen_PictureBox)).BeginInit();
			this.panel9.SuspendLayout();
			this.Dock_SongTools.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Main_ErrorProvider)).BeginInit();
			this.ShowSong_Tab.SuspendLayout();
			this.SermonTools_Tab.SuspendLayout();
			this.Sermon_LeftPanel.SuspendLayout();
			this.Sermon_LeftDoc_Panel.SuspendLayout();
			this.Sermon_LeftToolBar_Panel.SuspendLayout();
			this.Sermon_LeftBottom_Panel.SuspendLayout();
			this.Sermon_TabControl.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.Presentation_Tab.SuspendLayout();
			this.Presentation_FadePanel.SuspendLayout();
			this.Fade_panel.SuspendLayout();
			this.Fade_Top_Panel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Presentation_Fade_preview)).BeginInit();
			this.Presentation_MainPanel.SuspendLayout();
			this.Presentation_PreviewPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.axShockwaveFlash)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Presentation_PreviewBox)).BeginInit();
			this.Presentation_MovieControlPanel.SuspendLayout();
			this.Presentation_MovieControlPanelBottom.SuspendLayout();
			this.Presentation_MovieControl_PreviewButtonPanel.SuspendLayout();
			this.Presentation_MovieControlPanelBottomLeft.SuspendLayout();
			this.Presentation_MovieControlPanel_Top.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Media_TrackBar)).BeginInit();
			this.Presentation_MovieControlPanel_Right.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.AudioBar)).BeginInit();
			this.EditSongs2_Tab.SuspendLayout();
			this.BibleText_Tab.SuspendLayout();
			this.BibleText_panelLeft.SuspendLayout();
			this.panel8.SuspendLayout();
			this.panel7.SuspendLayout();
			this.tabControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.GlobalDataSet)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.SongListTable)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.SongListDataView)).BeginInit();
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
			this.RightDocks_ImageListBox.Location = new System.Drawing.Point(0, 0);
			this.RightDocks_ImageListBox.Name = "RightDocks_ImageListBox";
			this.RightDocks_ImageListBox.Size = new System.Drawing.Size(196, 307);
			this.RightDocks_ImageListBox.TabIndex = 19;
			this.RightDocks_ImageListBox.SelectedIndexChanged += new System.EventHandler(this.RightDocks_ImageListBox_SelectedIndexChanged);
			this.RightDocks_ImageListBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.RightDocks_ImageListBox_DragDrop);
			this.RightDocks_ImageListBox.DoubleClick += new System.EventHandler(this.RightDocks_ImageListBox_DoubleClick);
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
			this.SongEdit_fontDialog.Font = new System.Drawing.Font("Microsoft Sans Serif", 35F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SongEdit_fontDialog.ShowEffects = false;
			// 
			// SongEdit_TextColorDialog
			// 
			this.SongEdit_TextColorDialog.Color = System.Drawing.Color.White;
			// 
			// RightDocks_Songlist_SearchPanel
			// 
			this.RightDocks_Songlist_SearchPanel.Controls.Add(this.pictureBox1);
			this.RightDocks_Songlist_SearchPanel.Controls.Add(this.RightDocks_SongListSearch);
			this.RightDocks_Songlist_SearchPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.RightDocks_Songlist_SearchPanel.Location = new System.Drawing.Point(0, 0);
			this.RightDocks_Songlist_SearchPanel.Name = "RightDocks_Songlist_SearchPanel";
			this.RightDocks_Songlist_SearchPanel.Size = new System.Drawing.Size(196, 23);
			this.RightDocks_Songlist_SearchPanel.TabIndex = 6;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(22, 22);
			this.pictureBox1.TabIndex = 5;
			this.pictureBox1.TabStop = false;
			// 
			// RightDocks_SongListSearch
			// 
			this.RightDocks_SongListSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.RightDocks_SongListSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.RightDocks_SongListSearch.Location = new System.Drawing.Point(22, 0);
			this.RightDocks_SongListSearch.Name = "RightDocks_SongListSearch";
			this.RightDocks_SongListSearch.Size = new System.Drawing.Size(174, 20);
			this.RightDocks_SongListSearch.TabIndex = 4;
			this.RightDocks_SongListSearch.TextChanged += new System.EventHandler(this.RightDocks_SongListSearch_TextChanged);
			// 
			// RightDocks_SongList_ButtonPanel
			// 
			this.RightDocks_SongList_ButtonPanel.Controls.Add(this.btnRightDocks_SongListDelete);
			this.RightDocks_SongList_ButtonPanel.Controls.Add(this.btnRightDocks_SongList2PlayList);
			this.RightDocks_SongList_ButtonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.RightDocks_SongList_ButtonPanel.Location = new System.Drawing.Point(0, 246);
			this.RightDocks_SongList_ButtonPanel.Name = "RightDocks_SongList_ButtonPanel";
			this.RightDocks_SongList_ButtonPanel.Size = new System.Drawing.Size(196, 20);
			this.RightDocks_SongList_ButtonPanel.TabIndex = 5;
			// 
			// btnRightDocks_SongListDelete
			// 
			this.btnRightDocks_SongListDelete.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.btnRightDocks_SongListDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnRightDocks_SongListDelete.Location = new System.Drawing.Point(16, 1);
			this.btnRightDocks_SongListDelete.Name = "btnRightDocks_SongListDelete";
			this.btnRightDocks_SongListDelete.Size = new System.Drawing.Size(62, 18);
			this.btnRightDocks_SongListDelete.TabIndex = 2;
			this.btnRightDocks_SongListDelete.Text = "Delete";
			this.MainForm_ToolTip.SetToolTip(this.btnRightDocks_SongListDelete, "Delete selected song");
			this.btnRightDocks_SongListDelete.Click += new System.EventHandler(this.btnRightDocks_SongListDelete_Click);
			// 
			// btnRightDocks_SongList2PlayList
			// 
			this.btnRightDocks_SongList2PlayList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRightDocks_SongList2PlayList.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnRightDocks_SongList2PlayList.Location = new System.Drawing.Point(96, 1);
			this.btnRightDocks_SongList2PlayList.Name = "btnRightDocks_SongList2PlayList";
			this.btnRightDocks_SongList2PlayList.Size = new System.Drawing.Size(88, 18);
			this.btnRightDocks_SongList2PlayList.TabIndex = 1;
			this.btnRightDocks_SongList2PlayList.Text = "Add to Playlist";
			this.MainForm_ToolTip.SetToolTip(this.btnRightDocks_SongList2PlayList, "Add selected song to the playlist");
			this.btnRightDocks_SongList2PlayList.Click += new System.EventHandler(this.btnRightDocks_SongList2PlayList_Click);
			// 
			// RightDocks_PlayList
			// 
			this.RightDocks_PlayList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.RightDocks_PlayList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.RightDocks_PlayList.Location = new System.Drawing.Point(0, 0);
			this.RightDocks_PlayList.Name = "RightDocks_PlayList";
			this.RightDocks_PlayList.Size = new System.Drawing.Size(196, 236);
			this.RightDocks_PlayList.TabIndex = 1;
			this.RightDocks_PlayList.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightDocks_PlayList_MouseUp);
			this.RightDocks_PlayList.DoubleClick += new System.EventHandler(this.RightDocks_PlayList_DoubleClick);
			// 
			// RightDocks_TopPanel_PlayList_Button_Panel
			// 
			this.RightDocks_TopPanel_PlayList_Button_Panel.Controls.Add(this.RightDocks_PlayList_Down_Button);
			this.RightDocks_TopPanel_PlayList_Button_Panel.Controls.Add(this.RightDocks_PlayList_Up_Button);
			this.RightDocks_TopPanel_PlayList_Button_Panel.Controls.Add(this.RightDocks_PlayList_Remove_Button);
			this.RightDocks_TopPanel_PlayList_Button_Panel.Controls.Add(this.RightDocks_PlayList_Load_Button);
			this.RightDocks_TopPanel_PlayList_Button_Panel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.RightDocks_TopPanel_PlayList_Button_Panel.Location = new System.Drawing.Point(0, 246);
			this.RightDocks_TopPanel_PlayList_Button_Panel.Name = "RightDocks_TopPanel_PlayList_Button_Panel";
			this.RightDocks_TopPanel_PlayList_Button_Panel.Size = new System.Drawing.Size(196, 20);
			this.RightDocks_TopPanel_PlayList_Button_Panel.TabIndex = 8;
			// 
			// RightDocks_PlayList_Down_Button
			// 
			this.RightDocks_PlayList_Down_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.RightDocks_PlayList_Down_Button.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.RightDocks_PlayList_Down_Button.Location = new System.Drawing.Point(154, 2);
			this.RightDocks_PlayList_Down_Button.Name = "RightDocks_PlayList_Down_Button";
			this.RightDocks_PlayList_Down_Button.Size = new System.Drawing.Size(40, 18);
			this.RightDocks_PlayList_Down_Button.TabIndex = 7;
			this.RightDocks_PlayList_Down_Button.Text = "Down";
			this.RightDocks_PlayList_Down_Button.Click += new System.EventHandler(this.RightDocks_PlayList_Down_Button_Click);
			// 
			// RightDocks_PlayList_Up_Button
			// 
			this.RightDocks_PlayList_Up_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.RightDocks_PlayList_Up_Button.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.RightDocks_PlayList_Up_Button.Location = new System.Drawing.Point(114, 2);
			this.RightDocks_PlayList_Up_Button.Name = "RightDocks_PlayList_Up_Button";
			this.RightDocks_PlayList_Up_Button.Size = new System.Drawing.Size(40, 18);
			this.RightDocks_PlayList_Up_Button.TabIndex = 6;
			this.RightDocks_PlayList_Up_Button.Text = "Up";
			this.RightDocks_PlayList_Up_Button.Click += new System.EventHandler(this.RightDocks_PlayList_Up_Button_Click);
			// 
			// RightDocks_PlayList_Remove_Button
			// 
			this.RightDocks_PlayList_Remove_Button.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.RightDocks_PlayList_Remove_Button.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.RightDocks_PlayList_Remove_Button.Location = new System.Drawing.Point(57, 2);
			this.RightDocks_PlayList_Remove_Button.Name = "RightDocks_PlayList_Remove_Button";
			this.RightDocks_PlayList_Remove_Button.Size = new System.Drawing.Size(54, 18);
			this.RightDocks_PlayList_Remove_Button.TabIndex = 5;
			this.RightDocks_PlayList_Remove_Button.Text = "Remove";
			this.MainForm_ToolTip.SetToolTip(this.RightDocks_PlayList_Remove_Button, "Remove selected song from playlist");
			this.RightDocks_PlayList_Remove_Button.Click += new System.EventHandler(this.RightDocks_PlayList_Remove_Button_Click);
			// 
			// RightDocks_PlayList_Load_Button
			// 
			this.RightDocks_PlayList_Load_Button.Enabled = false;
			this.RightDocks_PlayList_Load_Button.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.RightDocks_PlayList_Load_Button.Location = new System.Drawing.Point(2, 2);
			this.RightDocks_PlayList_Load_Button.Name = "RightDocks_PlayList_Load_Button";
			this.RightDocks_PlayList_Load_Button.Size = new System.Drawing.Size(54, 18);
			this.RightDocks_PlayList_Load_Button.TabIndex = 4;
			this.RightDocks_PlayList_Load_Button.Text = "<- Load";
			this.MainForm_ToolTip.SetToolTip(this.RightDocks_PlayList_Load_Button, "Preview selected song");
			// 
			// RightDocks_FolderDropdown
			// 
			this.RightDocks_FolderDropdown.Dock = System.Windows.Forms.DockStyle.Fill;
			this.RightDocks_FolderDropdown.Location = new System.Drawing.Point(0, 0);
			this.RightDocks_FolderDropdown.Name = "RightDocks_FolderDropdown";
			this.RightDocks_FolderDropdown.Size = new System.Drawing.Size(196, 21);
			this.RightDocks_FolderDropdown.TabIndex = 21;
			this.RightDocks_FolderDropdown.SelectionChangeCommitted += new System.EventHandler(this.RightDocks_FolderDropdown_SelectionChangeCommitted);
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
			this.ToolBars_bottomSandBarDock.Location = new System.Drawing.Point(0, 781);
			this.ToolBars_bottomSandBarDock.Manager = this.ToolBars_sandBarManager1;
			this.ToolBars_bottomSandBarDock.Name = "ToolBars_bottomSandBarDock";
			this.ToolBars_bottomSandBarDock.Size = new System.Drawing.Size(976, 0);
			this.ToolBars_bottomSandBarDock.TabIndex = 20;
			// 
			// ToolBars_leftSandBarDock
			// 
			this.ToolBars_leftSandBarDock.Controls.Add(this.ToolBars_ComponentBar);
			this.ToolBars_leftSandBarDock.Dock = System.Windows.Forms.DockStyle.Left;
			this.ToolBars_leftSandBarDock.Location = new System.Drawing.Point(0, 50);
			this.ToolBars_leftSandBarDock.Manager = this.ToolBars_sandBarManager1;
			this.ToolBars_leftSandBarDock.Name = "ToolBars_leftSandBarDock";
			this.ToolBars_leftSandBarDock.Size = new System.Drawing.Size(72, 731);
			this.ToolBars_leftSandBarDock.TabIndex = 18;
			// 
			// ToolBars_ComponentBar
			// 
			this.ToolBars_ComponentBar.BackColor = System.Drawing.SystemColors.Control;
			this.ToolBars_ComponentBar.Buttons.AddRange(new TD.SandBar.ToolbarItemBase[] {
            this.ShowSongs_Button,
            this.EditSongs_Button,
            this.Presentation_Button,
            this.Sermon_Button,
            this.BibleText_Button});
			this.ToolBars_ComponentBar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ToolBars_ComponentBar.DockLine = 1;
			this.ToolBars_ComponentBar.ForeColor = System.Drawing.SystemColors.ControlText;
			this.ToolBars_ComponentBar.Guid = new System.Guid("bdddaff4-849d-43cd-ab71-bbe084e99803");
			this.ToolBars_ComponentBar.ImageList = null;
			this.ToolBars_ComponentBar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.ToolBars_ComponentBar.Location = new System.Drawing.Point(0, 2);
			this.ToolBars_ComponentBar.Name = "ToolBars_ComponentBar";
			this.ToolBars_ComponentBar.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.ToolBars_ComponentBar.ShowShortcutsInToolTips = true;
			this.ToolBars_ComponentBar.Size = new System.Drawing.Size(72, 729);
			this.ToolBars_ComponentBar.Stretch = true;
			this.ToolBars_ComponentBar.TabIndex = 0;
			this.ToolBars_ComponentBar.Text = "Main Tool Bar";
			this.ToolBars_ComponentBar.TextAlign = TD.SandBar.ToolBarTextAlign.Underneath;
			this.ToolBars_ComponentBar.ButtonClick += new TD.SandBar.ToolBar.ButtonClickEventHandler(this.ToolBars_ComponentBar_ButtonClick);
			// 
			// ShowSongs_Button
			// 
			this.ShowSongs_Button.BuddyMenu = null;
			this.ShowSongs_Button.Icon = ((System.Drawing.Icon)(resources.GetObject("ShowSongs_Button.Icon")));
			this.ShowSongs_Button.IconSize = new System.Drawing.Size(47, 47);
			this.ShowSongs_Button.Tag = null;
			this.ShowSongs_Button.Text = "Show Songs";
			this.ShowSongs_Button.ToolTipText = "Show Songs";
			// 
			// EditSongs_Button
			// 
			this.EditSongs_Button.BuddyMenu = null;
			this.EditSongs_Button.Icon = ((System.Drawing.Icon)(resources.GetObject("EditSongs_Button.Icon")));
			this.EditSongs_Button.IconSize = new System.Drawing.Size(47, 47);
			this.EditSongs_Button.Tag = null;
			this.EditSongs_Button.Text = "Edit Songs";
			this.EditSongs_Button.ToolTipText = "Edit Songs";
			// 
			// Presentation_Button
			// 
			this.Presentation_Button.BuddyMenu = null;
			this.Presentation_Button.Icon = ((System.Drawing.Icon)(resources.GetObject("Presentation_Button.Icon")));
			this.Presentation_Button.IconSize = new System.Drawing.Size(47, 47);
			this.Presentation_Button.Tag = null;
			this.Presentation_Button.Text = "Multimedia";
			this.Presentation_Button.ToolTipText = "Presentation";
			// 
			// Sermon_Button
			// 
			this.Sermon_Button.BuddyMenu = null;
			this.Sermon_Button.Icon = ((System.Drawing.Icon)(resources.GetObject("Sermon_Button.Icon")));
			this.Sermon_Button.IconSize = new System.Drawing.Size(47, 47);
			this.Sermon_Button.Tag = null;
			this.Sermon_Button.Text = "Sermon Tool";
			this.Sermon_Button.ToolTipText = "Sermon Tool";
			// 
			// BibleText_Button
			// 
			this.BibleText_Button.BuddyMenu = null;
			this.BibleText_Button.Icon = ((System.Drawing.Icon)(resources.GetObject("BibleText_Button.Icon")));
			this.BibleText_Button.IconSize = new System.Drawing.Size(48, 48);
			this.BibleText_Button.Tag = null;
			this.BibleText_Button.Text = "BibleText";
			this.BibleText_Button.ToolTipText = "Bible Text";
			// 
			// ToolBars_topSandBarDock
			// 
			this.ToolBars_topSandBarDock.Controls.Add(this.ToolBars_MainToolbar);
			this.ToolBars_topSandBarDock.Controls.Add(this.ToolBars_MenuBar);
			this.ToolBars_topSandBarDock.Dock = System.Windows.Forms.DockStyle.Top;
			this.ToolBars_topSandBarDock.Location = new System.Drawing.Point(0, 0);
			this.ToolBars_topSandBarDock.Manager = this.ToolBars_sandBarManager1;
			this.ToolBars_topSandBarDock.Name = "ToolBars_topSandBarDock";
			this.ToolBars_topSandBarDock.Size = new System.Drawing.Size(976, 50);
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
			this.ToolBars_MainToolbar.Dock = System.Windows.Forms.DockStyle.Left;
			this.ToolBars_MainToolbar.DockLine = 1;
			this.ToolBars_MainToolbar.DrawActionsButton = false;
			this.ToolBars_MainToolbar.Guid = new System.Guid("8bc9aa4f-e677-49e0-acc1-49086837bb09");
			this.ToolBars_MainToolbar.ImageList = null;
			this.ToolBars_MainToolbar.Location = new System.Drawing.Point(2, 24);
			this.ToolBars_MainToolbar.Name = "ToolBars_MainToolbar";
			this.ToolBars_MainToolbar.ShowShortcutsInToolTips = true;
			this.ToolBars_MainToolbar.Size = new System.Drawing.Size(974, 26);
			this.ToolBars_MainToolbar.Stretch = true;
			this.ToolBars_MainToolbar.TabIndex = 1;
			this.ToolBars_MainToolbar.Text = "Action Tool Bar";
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
            this.ToolBars_MenuBar_File,
            this.ToolBars_MenuBar_Edit,
            this.ToolBars_MenuBar_Song,
            this.ToolBars_MenuBar_MediaList,
            this.ToolBars_MenuBar_View,
            this.ToolBars_MenuBar_Help});
			this.ToolBars_MenuBar.Guid = new System.Guid("cc0de77e-657c-4906-a57a-f2a8d32fe17e");
			this.ToolBars_MenuBar.ImageList = null;
			this.ToolBars_MenuBar.Location = new System.Drawing.Point(2, 0);
			this.ToolBars_MenuBar.Movable = false;
			this.ToolBars_MenuBar.Name = "ToolBars_MenuBar";
			this.ToolBars_MenuBar.Size = new System.Drawing.Size(242, 24);
			this.ToolBars_MenuBar.Stretch = false;
			this.ToolBars_MenuBar.TabIndex = 0;
			this.ToolBars_MenuBar.Tearable = false;
			// 
			// ToolBars_MenuBar_File
			// 
			this.ToolBars_MenuBar_File.Icon = null;
			this.ToolBars_MenuBar_File.MenuItems.AddRange(new TD.SandBar.MenuButtonItem[] {
            this.menuButtonItem1,
            this.ToolBars_MenuBar_File_Exit});
			this.ToolBars_MenuBar_File.Tag = null;
			this.ToolBars_MenuBar_File.Text = "&File";
			// 
			// menuButtonItem1
			// 
			this.menuButtonItem1.Icon = null;
			this.menuButtonItem1.MenuItems.AddRange(new TD.SandBar.MenuButtonItem[] {
            this.ToolBars_MenuBar_Import_Song});
			this.menuButtonItem1.Shortcut = System.Windows.Forms.Shortcut.None;
			this.menuButtonItem1.Tag = null;
			this.menuButtonItem1.Text = "Import";
			// 
			// ToolBars_MenuBar_Import_Song
			// 
			this.ToolBars_MenuBar_Import_Song.Icon = null;
			this.ToolBars_MenuBar_Import_Song.Shortcut = System.Windows.Forms.Shortcut.None;
			this.ToolBars_MenuBar_Import_Song.Tag = null;
			this.ToolBars_MenuBar_Import_Song.Text = "Songs";
			this.ToolBars_MenuBar_Import_Song.Activate += new TD.SandBar.MenuButtonItem.ActivateEventHandler(this.ToolBars_MenuBar_Import_Song_Activate);
			// 
			// ToolBars_MenuBar_File_Exit
			// 
			this.ToolBars_MenuBar_File_Exit.Icon = ((System.Drawing.Icon)(resources.GetObject("ToolBars_MenuBar_File_Exit.Icon")));
			this.ToolBars_MenuBar_File_Exit.Shortcut = System.Windows.Forms.Shortcut.AltF4;
			this.ToolBars_MenuBar_File_Exit.Tag = null;
			this.ToolBars_MenuBar_File_Exit.Text = "E&xit DreamBeam";
			this.ToolBars_MenuBar_File_Exit.Activate += new TD.SandBar.MenuButtonItem.ActivateEventHandler(this.ToolBars_MenuBar_File_Exit_Activate);
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
			// ToolBars_MenuBar_Song
			// 
			this.ToolBars_MenuBar_Song.Icon = null;
			this.ToolBars_MenuBar_Song.MenuItems.AddRange(new TD.SandBar.MenuButtonItem[] {
            this.ToolBars_MenuBar_Song_New,
            this.ToolBars_MenuBar_Song_Save,
            this.ToolBars_MenuBar_Song_SaveAs,
            this.ToolBars_MenuBar_Song_Rename});
			this.ToolBars_MenuBar_Song.Tag = null;
			this.ToolBars_MenuBar_Song.Text = "&Song";
			// 
			// ToolBars_MenuBar_Song_New
			// 
			this.ToolBars_MenuBar_Song_New.Icon = ((System.Drawing.Icon)(resources.GetObject("ToolBars_MenuBar_Song_New.Icon")));
			this.ToolBars_MenuBar_Song_New.Shortcut = System.Windows.Forms.Shortcut.None;
			this.ToolBars_MenuBar_Song_New.Tag = null;
			this.ToolBars_MenuBar_Song_New.Text = "New Song...";
			this.ToolBars_MenuBar_Song_New.Activate += new TD.SandBar.MenuButtonItem.ActivateEventHandler(this.ToolBars_MenuBar_Song_New_Activate);
			// 
			// ToolBars_MenuBar_Song_Save
			// 
			this.ToolBars_MenuBar_Song_Save.Icon = ((System.Drawing.Icon)(resources.GetObject("ToolBars_MenuBar_Song_Save.Icon")));
			this.ToolBars_MenuBar_Song_Save.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
			this.ToolBars_MenuBar_Song_Save.Tag = null;
			this.ToolBars_MenuBar_Song_Save.Text = "Save Song";
			this.ToolBars_MenuBar_Song_Save.Activate += new TD.SandBar.MenuButtonItem.ActivateEventHandler(this.ToolBars_MenuBar_Song_Save_Activate);
			// 
			// ToolBars_MenuBar_Song_SaveAs
			// 
			this.ToolBars_MenuBar_Song_SaveAs.Icon = ((System.Drawing.Icon)(resources.GetObject("ToolBars_MenuBar_Song_SaveAs.Icon")));
			this.ToolBars_MenuBar_Song_SaveAs.Shortcut = System.Windows.Forms.Shortcut.None;
			this.ToolBars_MenuBar_Song_SaveAs.Tag = null;
			this.ToolBars_MenuBar_Song_SaveAs.Text = "Save Song As...";
			this.ToolBars_MenuBar_Song_SaveAs.Activate += new TD.SandBar.MenuButtonItem.ActivateEventHandler(this.ToolBars_MenuBar_Song_SaveAs_Activate);
			// 
			// ToolBars_MenuBar_Song_Rename
			// 
			this.ToolBars_MenuBar_Song_Rename.Icon = null;
			this.ToolBars_MenuBar_Song_Rename.Shortcut = System.Windows.Forms.Shortcut.None;
			this.ToolBars_MenuBar_Song_Rename.Tag = null;
			this.ToolBars_MenuBar_Song_Rename.Text = "Rename Song...";
			this.ToolBars_MenuBar_Song_Rename.Visible = false;
			this.ToolBars_MenuBar_Song_Rename.Activate += new TD.SandBar.MenuButtonItem.ActivateEventHandler(this.ToolBars_MenuBar_Song_Rename_Activate);
			// 
			// ToolBars_MenuBar_MediaList
			// 
			this.ToolBars_MenuBar_MediaList.Icon = null;
			this.ToolBars_MenuBar_MediaList.MenuItems.AddRange(new TD.SandBar.MenuButtonItem[] {
            this.ToolBars_MenuBar_MediaList_New,
            this.ToolBars_MenuBar_Media_Save,
            this.ToolBars_MenuBar_Media_SaveAs,
            this.ToolBars_MenuBar_Media_Rename});
			this.ToolBars_MenuBar_MediaList.Tag = null;
			this.ToolBars_MenuBar_MediaList.Text = "&Media";
			this.ToolBars_MenuBar_MediaList.Visible = false;
			// 
			// ToolBars_MenuBar_MediaList_New
			// 
			this.ToolBars_MenuBar_MediaList_New.Icon = ((System.Drawing.Icon)(resources.GetObject("ToolBars_MenuBar_MediaList_New.Icon")));
			this.ToolBars_MenuBar_MediaList_New.Shortcut = System.Windows.Forms.Shortcut.None;
			this.ToolBars_MenuBar_MediaList_New.Tag = null;
			this.ToolBars_MenuBar_MediaList_New.Text = "New Media List...";
			this.ToolBars_MenuBar_MediaList_New.Activate += new TD.SandBar.MenuButtonItem.ActivateEventHandler(this.ToolBars_MenuBar_MediaList_New_Activate);
			// 
			// ToolBars_MenuBar_Media_Save
			// 
			this.ToolBars_MenuBar_Media_Save.Icon = ((System.Drawing.Icon)(resources.GetObject("ToolBars_MenuBar_Media_Save.Icon")));
			this.ToolBars_MenuBar_Media_Save.Shortcut = System.Windows.Forms.Shortcut.None;
			this.ToolBars_MenuBar_Media_Save.Tag = null;
			this.ToolBars_MenuBar_Media_Save.Text = "Save Media List";
			this.ToolBars_MenuBar_Media_Save.Activate += new TD.SandBar.MenuButtonItem.ActivateEventHandler(this.ToolBars_MenuBar_Media_Save_Activate);
			// 
			// ToolBars_MenuBar_Media_SaveAs
			// 
			this.ToolBars_MenuBar_Media_SaveAs.Icon = ((System.Drawing.Icon)(resources.GetObject("ToolBars_MenuBar_Media_SaveAs.Icon")));
			this.ToolBars_MenuBar_Media_SaveAs.Shortcut = System.Windows.Forms.Shortcut.None;
			this.ToolBars_MenuBar_Media_SaveAs.Tag = null;
			this.ToolBars_MenuBar_Media_SaveAs.Text = "Save Media List As..";
			this.ToolBars_MenuBar_Media_SaveAs.Activate += new TD.SandBar.MenuButtonItem.ActivateEventHandler(this.ToolBars_MenuBar_Media_SaveAs_Activate);
			// 
			// ToolBars_MenuBar_Media_Rename
			// 
			this.ToolBars_MenuBar_Media_Rename.Icon = null;
			this.ToolBars_MenuBar_Media_Rename.Shortcut = System.Windows.Forms.Shortcut.None;
			this.ToolBars_MenuBar_Media_Rename.Tag = null;
			this.ToolBars_MenuBar_Media_Rename.Text = "Rename Media List...";
			this.ToolBars_MenuBar_Media_Rename.Activate += new TD.SandBar.MenuButtonItem.ActivateEventHandler(this.ToolBars_MenuBar_Media_Rename_Activate);
			// 
			// ToolBars_MenuBar_View
			// 
			this.ToolBars_MenuBar_View.Icon = null;
			this.ToolBars_MenuBar_View.MenuItems.AddRange(new TD.SandBar.MenuButtonItem[] {
            this.ToolBars_MenuBar_View_ShowSongs,
            this.ToolBars_MenuBar_View_EditSongs,
            this.ToolBars_MenuBar_View_Presentation,
            this.ToolBars_MenuBar_View_TextTool,
            this.ToolBars_MenuBar_View_BibleText,
            this.ToolBars_MenuBar_Open_SongsTab,
            this.ToolBars_MenuBar_Open_PlaylistTab,
            this.ToolBars_MenuBar_Open_BibleToolsTab,
            this.ToolBars_MenuBar_Open_SongToolsTab,
            this.ToolBars_MenuBar_Open_BackgroundsTab,
            this.ToolBars_MenuBar_Open_MediaListTab,
            this.ToolBars_MenuBar_Open_MediaTab,
            this.ToolBars_MenuBar_Open_PreviewTab,
            this.ToolBars_MenuBar_Open_LiveTab});
			this.ToolBars_MenuBar_View.Tag = null;
			this.ToolBars_MenuBar_View.Text = "&View";
			// 
			// ToolBars_MenuBar_View_ShowSongs
			// 
			this.ToolBars_MenuBar_View_ShowSongs.Icon = ((System.Drawing.Icon)(resources.GetObject("ToolBars_MenuBar_View_ShowSongs.Icon")));
			this.ToolBars_MenuBar_View_ShowSongs.Shortcut = System.Windows.Forms.Shortcut.F9;
			this.ToolBars_MenuBar_View_ShowSongs.Tag = null;
			this.ToolBars_MenuBar_View_ShowSongs.Text = "Show Songs";
			this.ToolBars_MenuBar_View_ShowSongs.Activate += new TD.SandBar.MenuButtonItem.ActivateEventHandler(this.ToolBars_MenuBar_View_ShowSongs_Activate);
			// 
			// ToolBars_MenuBar_View_EditSongs
			// 
			this.ToolBars_MenuBar_View_EditSongs.Icon = ((System.Drawing.Icon)(resources.GetObject("ToolBars_MenuBar_View_EditSongs.Icon")));
			this.ToolBars_MenuBar_View_EditSongs.Shortcut = System.Windows.Forms.Shortcut.F10;
			this.ToolBars_MenuBar_View_EditSongs.Tag = null;
			this.ToolBars_MenuBar_View_EditSongs.Text = "Edit Songs";
			this.ToolBars_MenuBar_View_EditSongs.Activate += new TD.SandBar.MenuButtonItem.ActivateEventHandler(this.ToolBars_MenuBar_View_EditSongs_Activate);
			// 
			// ToolBars_MenuBar_View_Presentation
			// 
			this.ToolBars_MenuBar_View_Presentation.Icon = ((System.Drawing.Icon)(resources.GetObject("ToolBars_MenuBar_View_Presentation.Icon")));
			this.ToolBars_MenuBar_View_Presentation.Shortcut = System.Windows.Forms.Shortcut.F11;
			this.ToolBars_MenuBar_View_Presentation.Tag = null;
			this.ToolBars_MenuBar_View_Presentation.Text = "Presentation";
			this.ToolBars_MenuBar_View_Presentation.Activate += new TD.SandBar.MenuButtonItem.ActivateEventHandler(this.ToolBars_MenuBar_View_Presentation_Activate);
			// 
			// ToolBars_MenuBar_View_TextTool
			// 
			this.ToolBars_MenuBar_View_TextTool.Icon = ((System.Drawing.Icon)(resources.GetObject("ToolBars_MenuBar_View_TextTool.Icon")));
			this.ToolBars_MenuBar_View_TextTool.Shortcut = System.Windows.Forms.Shortcut.F12;
			this.ToolBars_MenuBar_View_TextTool.Tag = null;
			this.ToolBars_MenuBar_View_TextTool.Text = "Text Tool";
			this.ToolBars_MenuBar_View_TextTool.Activate += new TD.SandBar.MenuButtonItem.ActivateEventHandler(this.ToolBars_MenuBar_View_TextTool_Activate);
			// 
			// ToolBars_MenuBar_View_BibleText
			// 
			this.ToolBars_MenuBar_View_BibleText.Icon = ((System.Drawing.Icon)(resources.GetObject("ToolBars_MenuBar_View_BibleText.Icon")));
			this.ToolBars_MenuBar_View_BibleText.Shortcut = System.Windows.Forms.Shortcut.None;
			this.ToolBars_MenuBar_View_BibleText.Tag = null;
			this.ToolBars_MenuBar_View_BibleText.Text = "Bible Text";
			this.ToolBars_MenuBar_View_BibleText.Activate += new TD.SandBar.MenuButtonItem.ActivateEventHandler(this.ToolBars_MenuBar_View_BibleText_Activate);
			// 
			// ToolBars_MenuBar_Open_SongsTab
			// 
			this.ToolBars_MenuBar_Open_SongsTab.BeginGroup = true;
			this.ToolBars_MenuBar_Open_SongsTab.Icon = null;
			this.ToolBars_MenuBar_Open_SongsTab.Shortcut = System.Windows.Forms.Shortcut.None;
			this.ToolBars_MenuBar_Open_SongsTab.Tag = null;
			this.ToolBars_MenuBar_Open_SongsTab.Text = "Songs tab";
			this.ToolBars_MenuBar_Open_SongsTab.Activate += new TD.SandBar.MenuButtonItem.ActivateEventHandler(this.ToolBars_MenuBar_Open_SongsTab_Activate);
			// 
			// ToolBars_MenuBar_Open_PlaylistTab
			// 
			this.ToolBars_MenuBar_Open_PlaylistTab.Icon = null;
			this.ToolBars_MenuBar_Open_PlaylistTab.Shortcut = System.Windows.Forms.Shortcut.None;
			this.ToolBars_MenuBar_Open_PlaylistTab.Tag = null;
			this.ToolBars_MenuBar_Open_PlaylistTab.Text = "Playlist tab";
			this.ToolBars_MenuBar_Open_PlaylistTab.Activate += new TD.SandBar.MenuButtonItem.ActivateEventHandler(this.ToolBars_MenuBar_Open_PlaylistTab_Activate);
			// 
			// ToolBars_MenuBar_Open_BibleToolsTab
			// 
			this.ToolBars_MenuBar_Open_BibleToolsTab.BeginGroup = true;
			this.ToolBars_MenuBar_Open_BibleToolsTab.Icon = null;
			this.ToolBars_MenuBar_Open_BibleToolsTab.Shortcut = System.Windows.Forms.Shortcut.None;
			this.ToolBars_MenuBar_Open_BibleToolsTab.Tag = null;
			this.ToolBars_MenuBar_Open_BibleToolsTab.Text = "Bible Tools tab";
			this.ToolBars_MenuBar_Open_BibleToolsTab.Activate += new TD.SandBar.MenuButtonItem.ActivateEventHandler(this.ToolBars_MenuBar_Open_BibleToolsTab_Activate);
			// 
			// ToolBars_MenuBar_Open_SongToolsTab
			// 
			this.ToolBars_MenuBar_Open_SongToolsTab.Icon = null;
			this.ToolBars_MenuBar_Open_SongToolsTab.Shortcut = System.Windows.Forms.Shortcut.None;
			this.ToolBars_MenuBar_Open_SongToolsTab.Tag = null;
			this.ToolBars_MenuBar_Open_SongToolsTab.Text = "Song Tools tab";
			this.ToolBars_MenuBar_Open_SongToolsTab.Activate += new TD.SandBar.MenuButtonItem.ActivateEventHandler(this.ToolBars_MenuBar_Open_SongToolsTab_Activate);
			// 
			// ToolBars_MenuBar_Open_BackgroundsTab
			// 
			this.ToolBars_MenuBar_Open_BackgroundsTab.BeginGroup = true;
			this.ToolBars_MenuBar_Open_BackgroundsTab.Icon = ((System.Drawing.Icon)(resources.GetObject("ToolBars_MenuBar_Open_BackgroundsTab.Icon")));
			this.ToolBars_MenuBar_Open_BackgroundsTab.Shortcut = System.Windows.Forms.Shortcut.None;
			this.ToolBars_MenuBar_Open_BackgroundsTab.Tag = null;
			this.ToolBars_MenuBar_Open_BackgroundsTab.Text = "Backgrounds tab";
			this.ToolBars_MenuBar_Open_BackgroundsTab.Activate += new TD.SandBar.MenuButtonItem.ActivateEventHandler(this.ToolBars_MenuBar_Open_BackgroundsTab_Activate);
			// 
			// ToolBars_MenuBar_Open_MediaListTab
			// 
			this.ToolBars_MenuBar_Open_MediaListTab.Icon = null;
			this.ToolBars_MenuBar_Open_MediaListTab.Shortcut = System.Windows.Forms.Shortcut.None;
			this.ToolBars_MenuBar_Open_MediaListTab.Tag = null;
			this.ToolBars_MenuBar_Open_MediaListTab.Text = "Media List tab";
			this.ToolBars_MenuBar_Open_MediaListTab.Activate += new TD.SandBar.MenuButtonItem.ActivateEventHandler(this.ToolBars_MenuBar_Open_MediaListTab_Activate);
			// 
			// ToolBars_MenuBar_Open_MediaTab
			// 
			this.ToolBars_MenuBar_Open_MediaTab.Icon = ((System.Drawing.Icon)(resources.GetObject("ToolBars_MenuBar_Open_MediaTab.Icon")));
			this.ToolBars_MenuBar_Open_MediaTab.Shortcut = System.Windows.Forms.Shortcut.None;
			this.ToolBars_MenuBar_Open_MediaTab.Tag = null;
			this.ToolBars_MenuBar_Open_MediaTab.Text = "Media tab";
			this.ToolBars_MenuBar_Open_MediaTab.Activate += new TD.SandBar.MenuButtonItem.ActivateEventHandler(this.ToolBars_MenuBar_Open_MediaTab_Activate);
			// 
			// ToolBars_MenuBar_Open_PreviewTab
			// 
			this.ToolBars_MenuBar_Open_PreviewTab.BeginGroup = true;
			this.ToolBars_MenuBar_Open_PreviewTab.Icon = ((System.Drawing.Icon)(resources.GetObject("ToolBars_MenuBar_Open_PreviewTab.Icon")));
			this.ToolBars_MenuBar_Open_PreviewTab.Shortcut = System.Windows.Forms.Shortcut.None;
			this.ToolBars_MenuBar_Open_PreviewTab.Tag = null;
			this.ToolBars_MenuBar_Open_PreviewTab.Text = "Preview tab";
			this.ToolBars_MenuBar_Open_PreviewTab.Activate += new TD.SandBar.MenuButtonItem.ActivateEventHandler(this.ToolBars_MenuBar_Open_PreviewTab_Activate);
			// 
			// ToolBars_MenuBar_Open_LiveTab
			// 
			this.ToolBars_MenuBar_Open_LiveTab.Icon = ((System.Drawing.Icon)(resources.GetObject("ToolBars_MenuBar_Open_LiveTab.Icon")));
			this.ToolBars_MenuBar_Open_LiveTab.Shortcut = System.Windows.Forms.Shortcut.None;
			this.ToolBars_MenuBar_Open_LiveTab.Tag = null;
			this.ToolBars_MenuBar_Open_LiveTab.Text = "Live screen tab";
			this.ToolBars_MenuBar_Open_LiveTab.Activate += new TD.SandBar.MenuButtonItem.ActivateEventHandler(this.ToolBars_MenuBar_Open_LiveTab_Activate);
			// 
			// ToolBars_MenuBar_Help
			// 
			this.ToolBars_MenuBar_Help.Icon = null;
			this.ToolBars_MenuBar_Help.MenuItems.AddRange(new TD.SandBar.MenuButtonItem[] {
            this.HelpIntro,
            this.AboutButton});
			this.ToolBars_MenuBar_Help.Tag = null;
			this.ToolBars_MenuBar_Help.Text = "&Help";
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
			this.statusBar.Location = new System.Drawing.Point(72, 759);
			this.statusBar.Name = "statusBar";
			this.statusBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.StatusPanel,
            this.statusBarUpdatePanel});
			this.statusBar.ShowPanels = true;
			this.statusBar.Size = new System.Drawing.Size(504, 22);
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
			this.StatusPanel.Name = "StatusPanel";
			this.StatusPanel.Width = 394;
			// 
			// statusBarUpdatePanel
			// 
			this.statusBarUpdatePanel.MinWidth = 110;
			this.statusBarUpdatePanel.Name = "statusBarUpdatePanel";
			this.statusBarUpdatePanel.Width = 110;
			// 
			// SongShow_ImageList
			// 
			this.SongShow_ImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("SongShow_ImageList.ImageStream")));
			this.SongShow_ImageList.TransparentColor = System.Drawing.Color.Transparent;
			this.SongShow_ImageList.Images.SetKeyName(0, "");
			// 
			// Presentation_Fade_ImageList
			// 
			this.Presentation_Fade_ImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("Presentation_Fade_ImageList.ImageStream")));
			this.Presentation_Fade_ImageList.TransparentColor = System.Drawing.Color.Transparent;
			this.Presentation_Fade_ImageList.Images.SetKeyName(0, "");
			this.Presentation_Fade_ImageList.Images.SetKeyName(1, "");
			this.Presentation_Fade_ImageList.Images.SetKeyName(2, "");
			// 
			// imageList_Folders
			// 
			this.imageList_Folders.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList_Folders.ImageStream")));
			this.imageList_Folders.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList_Folders.Images.SetKeyName(0, "");
			this.imageList_Folders.Images.SetKeyName(1, "");
			// 
			// PlayButtons_ImageList
			// 
			this.PlayButtons_ImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("PlayButtons_ImageList.ImageStream")));
			this.PlayButtons_ImageList.TransparentColor = System.Drawing.Color.Red;
			this.PlayButtons_ImageList.Images.SetKeyName(0, "");
			this.PlayButtons_ImageList.Images.SetKeyName(1, "");
			this.PlayButtons_ImageList.Images.SetKeyName(2, "");
			// 
			// SongShow_CollapsPanel
			// 
			this.SongShow_CollapsPanel.BackColor = System.Drawing.Color.LightSteelBlue;
			this.SongShow_CollapsPanel.Border = 4;
			this.SongShow_CollapsPanel.Controls.Add(this.SongShow_HideElementsPanel);
			this.SongShow_CollapsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SongShow_CollapsPanel.Location = new System.Drawing.Point(0, 0);
			this.SongShow_CollapsPanel.Name = "SongShow_CollapsPanel";
			this.SongShow_CollapsPanel.Size = new System.Drawing.Size(196, 385);
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
			this.SongShow_HideElementsPanel.EndColour = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(212)))), ((int)(((byte)(247)))));
			this.SongShow_HideElementsPanel.Image = null;
			this.SongShow_HideElementsPanel.Location = new System.Drawing.Point(4, 4);
			this.SongShow_HideElementsPanel.Name = "SongShow_HideElementsPanel";
			this.SongShow_HideElementsPanel.PanelState = Salamander.Windows.Forms.PanelState.Expanded;
			this.SongShow_HideElementsPanel.Size = new System.Drawing.Size(188, 116);
			this.SongShow_HideElementsPanel.StartColour = System.Drawing.Color.White;
			this.SongShow_HideElementsPanel.TabIndex = 4;
			this.SongShow_HideElementsPanel.TitleFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SongShow_HideElementsPanel.TitleFontColour = System.Drawing.Color.Navy;
			this.SongShow_HideElementsPanel.TitleText = "Hide Elements";
			// 
			// panel3
			// 
			this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.panel3.Controls.Add(this.SongShow_HideAuthor_Button);
			this.panel3.Location = new System.Drawing.Point(5, 85);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(180, 28);
			this.panel3.TabIndex = 5;
			// 
			// SongShow_HideAuthor_Button
			// 
			this.SongShow_HideAuthor_Button.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.SongShow_HideAuthor_Button.bottomColor = System.Drawing.Color.DarkBlue;
			this.SongShow_HideAuthor_Button.BottomTransparent = 64;
			this.SongShow_HideAuthor_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.SongShow_HideAuthor_Button.LEDColor = System.Drawing.Color.SteelBlue;
			this.SongShow_HideAuthor_Button.Location = new System.Drawing.Point(39, 2);
			this.SongShow_HideAuthor_Button.Name = "SongShow_HideAuthor_Button";
			this.SongShow_HideAuthor_Button.Size = new System.Drawing.Size(104, 24);
			this.SongShow_HideAuthor_Button.TabIndex = 3;
			this.SongShow_HideAuthor_Button.Text = "Hide Author";
			this.SongShow_HideAuthor_Button.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.SongShow_HideAuthor_Button.topColor = System.Drawing.Color.Aquamarine;
			this.SongShow_HideAuthor_Button.TopTransparent = 64;
			this.SongShow_HideAuthor_Button.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SongShow_HideAuthor_Button_MouseDown);
			this.SongShow_HideAuthor_Button.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SongShow_HideAuthor_Button_MouseUp);
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.Controls.Add(this.SongShow_HideText_Button);
			this.panel1.Location = new System.Drawing.Point(5, 55);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(180, 28);
			this.panel1.TabIndex = 4;
			// 
			// SongShow_HideText_Button
			// 
			this.SongShow_HideText_Button.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.SongShow_HideText_Button.bottomColor = System.Drawing.Color.DarkBlue;
			this.SongShow_HideText_Button.BottomTransparent = 64;
			this.SongShow_HideText_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.SongShow_HideText_Button.LEDColor = System.Drawing.Color.SteelBlue;
			this.SongShow_HideText_Button.Location = new System.Drawing.Point(39, 2);
			this.SongShow_HideText_Button.Name = "SongShow_HideText_Button";
			this.SongShow_HideText_Button.Size = new System.Drawing.Size(104, 24);
			this.SongShow_HideText_Button.TabIndex = 2;
			this.SongShow_HideText_Button.Text = "Hide Verses";
			this.SongShow_HideText_Button.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.SongShow_HideText_Button.topColor = System.Drawing.Color.Aquamarine;
			this.SongShow_HideText_Button.TopTransparent = 64;
			this.SongShow_HideText_Button.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SongShow_HideText_Button_MouseDown);
			this.SongShow_HideText_Button.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SongShow_HideText_Button_MouseUp);
			// 
			// SongShow_HideElementsSub1Panel
			// 
			this.SongShow_HideElementsSub1Panel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.SongShow_HideElementsSub1Panel.Controls.Add(this.SongShow_HideTitle_Button);
			this.SongShow_HideElementsSub1Panel.Location = new System.Drawing.Point(5, 25);
			this.SongShow_HideElementsSub1Panel.Name = "SongShow_HideElementsSub1Panel";
			this.SongShow_HideElementsSub1Panel.Size = new System.Drawing.Size(180, 28);
			this.SongShow_HideElementsSub1Panel.TabIndex = 3;
			// 
			// SongShow_HideTitle_Button
			// 
			this.SongShow_HideTitle_Button.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.SongShow_HideTitle_Button.bottomColor = System.Drawing.Color.DarkBlue;
			this.SongShow_HideTitle_Button.BottomTransparent = 64;
			this.SongShow_HideTitle_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.SongShow_HideTitle_Button.LEDColor = System.Drawing.Color.SteelBlue;
			this.SongShow_HideTitle_Button.Location = new System.Drawing.Point(39, 2);
			this.SongShow_HideTitle_Button.Name = "SongShow_HideTitle_Button";
			this.SongShow_HideTitle_Button.Size = new System.Drawing.Size(104, 24);
			this.SongShow_HideTitle_Button.TabIndex = 1;
			this.SongShow_HideTitle_Button.Text = "Hide Title";
			this.SongShow_HideTitle_Button.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.SongShow_HideTitle_Button.topColor = System.Drawing.Color.Aquamarine;
			this.SongShow_HideTitle_Button.TopTransparent = 64;
			this.SongShow_HideTitle_Button.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SongShow_HideTitle_Button_MouseDown);
			this.SongShow_HideTitle_Button.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SongShow_HideTitle_Button_MouseUp);
			// 
			// BibleBookmarks_CollapsiblePanel
			// 
			this.BibleBookmarks_CollapsiblePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.BibleBookmarks_CollapsiblePanel.BackColor = System.Drawing.Color.AliceBlue;
			this.BibleBookmarks_CollapsiblePanel.Controls.Add(this.BibleText_Bookmarks);
			this.BibleBookmarks_CollapsiblePanel.EndColour = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(212)))), ((int)(((byte)(247)))));
			this.BibleBookmarks_CollapsiblePanel.Image = null;
			this.BibleBookmarks_CollapsiblePanel.Location = new System.Drawing.Point(8, 124);
			this.BibleBookmarks_CollapsiblePanel.Name = "BibleBookmarks_CollapsiblePanel";
			this.BibleBookmarks_CollapsiblePanel.PanelState = Salamander.Windows.Forms.PanelState.Expanded;
			this.BibleBookmarks_CollapsiblePanel.Size = new System.Drawing.Size(163, 262);
			this.BibleBookmarks_CollapsiblePanel.StartColour = System.Drawing.Color.White;
			this.BibleBookmarks_CollapsiblePanel.TabIndex = 1;
			this.BibleBookmarks_CollapsiblePanel.TitleFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.BibleBookmarks_CollapsiblePanel.TitleFontColour = System.Drawing.Color.Navy;
			this.BibleBookmarks_CollapsiblePanel.TitleText = "Bookmarks";
			// 
			// BibleText_Bookmarks
			// 
			this.BibleText_Bookmarks.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.BibleText_Bookmarks.Location = new System.Drawing.Point(0, 24);
			this.BibleText_Bookmarks.Name = "BibleText_Bookmarks";
			this.BibleText_Bookmarks.Size = new System.Drawing.Size(163, 238);
			this.BibleText_Bookmarks.TabIndex = 1;
			this.MainForm_ToolTip.SetToolTip(this.BibleText_Bookmarks, "Click to preview this verse, or right-click to remove it.");
			this.BibleText_Bookmarks.SelectedIndexChanged += new System.EventHandler(this.BibleText_Bookmarks_SelectedIndexChanged);
			this.BibleText_Bookmarks.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BibleText_Bookmarks_MouseDown);
			// 
			// BibleTranslations_CollapsiblePanel
			// 
			this.BibleTranslations_CollapsiblePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.BibleTranslations_CollapsiblePanel.BackColor = System.Drawing.Color.AliceBlue;
			this.BibleTranslations_CollapsiblePanel.Controls.Add(this.BibleText_Translations);
			this.BibleTranslations_CollapsiblePanel.EndColour = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(212)))), ((int)(((byte)(247)))));
			this.BibleTranslations_CollapsiblePanel.Image = null;
			this.BibleTranslations_CollapsiblePanel.Location = new System.Drawing.Point(8, 8);
			this.BibleTranslations_CollapsiblePanel.Name = "BibleTranslations_CollapsiblePanel";
			this.BibleTranslations_CollapsiblePanel.PanelState = Salamander.Windows.Forms.PanelState.Expanded;
			this.BibleTranslations_CollapsiblePanel.Size = new System.Drawing.Size(163, 108);
			this.BibleTranslations_CollapsiblePanel.StartColour = System.Drawing.Color.White;
			this.BibleTranslations_CollapsiblePanel.TabIndex = 0;
			this.BibleTranslations_CollapsiblePanel.TitleFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.BibleTranslations_CollapsiblePanel.TitleFontColour = System.Drawing.Color.Navy;
			this.BibleTranslations_CollapsiblePanel.TitleText = "Translations";
			// 
			// BibleText_Translations
			// 
			this.BibleText_Translations.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.BibleText_Translations.Location = new System.Drawing.Point(0, 26);
			this.BibleText_Translations.Name = "BibleText_Translations";
			this.BibleText_Translations.Size = new System.Drawing.Size(163, 82);
			this.BibleText_Translations.TabIndex = 1;
			this.BibleText_Translations.SelectedIndexChanged += new System.EventHandler(this.BibleText_Translations_SelectedIndexChanged);
			// 
			// TextTypedTimer
			// 
			this.TextTypedTimer.Interval = 5;
			this.TextTypedTimer.Tick += new System.EventHandler(this.TextTypedTimer_Tick);
			// 
			// sandDockManager1
			// 
			this.sandDockManager1.DockingManager = TD.SandDock.DockingManager.Whidbey;
			this.sandDockManager1.OwnerForm = this;
			this.sandDockManager1.Renderer = new TD.SandDock.Rendering.Office2003Renderer();
			// 
			// leftSandDock
			// 
			this.leftSandDock.Dock = System.Windows.Forms.DockStyle.Left;
			this.leftSandDock.Guid = new System.Guid("4844958a-6b63-4783-8909-9805d07b9fab");
			this.leftSandDock.LayoutSystem = new TD.SandDock.SplitLayoutSystem(250, 400);
			this.leftSandDock.Location = new System.Drawing.Point(72, 50);
			this.leftSandDock.Manager = this.sandDockManager1;
			this.leftSandDock.Name = "leftSandDock";
			this.leftSandDock.Size = new System.Drawing.Size(0, 731);
			this.leftSandDock.TabIndex = 23;
			// 
			// rightSandDock
			// 
			this.rightSandDock.Controls.Add(this.RightDocks_BottomPanel_Media);
			this.rightSandDock.Controls.Add(this.Dock_BibleTools);
			this.rightSandDock.Controls.Add(this.RightDocks_BottomPanel_MediaLists);
			this.rightSandDock.Controls.Add(this.RightDocks_BottomPanel_Backgrounds);
			this.rightSandDock.Controls.Add(this.RightDocks_TopPanel_Songs);
			this.rightSandDock.Controls.Add(this.RightDocks_TopPanel_PlayList);
			this.rightSandDock.Controls.Add(this.RightDocks_PreviewScreen);
			this.rightSandDock.Controls.Add(this.RightDocks_LiveScreen);
			this.rightSandDock.Controls.Add(this.Dock_SongTools);
			this.rightSandDock.Dock = System.Windows.Forms.DockStyle.Right;
			this.rightSandDock.Guid = new System.Guid("a6039876-f9a8-471e-b56f-5b1bf7264f06");
			this.rightSandDock.LayoutSystem = new TD.SandDock.SplitLayoutSystem(250, 400, System.Windows.Forms.Orientation.Horizontal, new TD.SandDock.LayoutSystemBase[] {
            ((TD.SandDock.LayoutSystemBase)(new TD.SandDock.SplitLayoutSystem(396, 731, System.Windows.Forms.Orientation.Vertical, new TD.SandDock.LayoutSystemBase[] {
                        ((TD.SandDock.LayoutSystemBase)(new TD.SandDock.SplitLayoutSystem(196, 731, System.Windows.Forms.Orientation.Horizontal, new TD.SandDock.LayoutSystemBase[] {
                                    ((TD.SandDock.LayoutSystemBase)(new TD.SandDock.ControlLayoutSystem(196, 294, new TD.SandDock.DockControl[] {
                                                this.RightDocks_PreviewScreen,
                                                this.RightDocks_LiveScreen}, this.RightDocks_PreviewScreen))),
                                    ((TD.SandDock.LayoutSystemBase)(new TD.SandDock.ControlLayoutSystem(196, 432, new TD.SandDock.DockControl[] {
                                                this.Dock_BibleTools,
                                                this.Dock_SongTools}, this.Dock_BibleTools)))}))),
                        ((TD.SandDock.LayoutSystemBase)(new TD.SandDock.SplitLayoutSystem(196, 731, System.Windows.Forms.Orientation.Horizontal, new TD.SandDock.LayoutSystemBase[] {
                                    ((TD.SandDock.LayoutSystemBase)(new TD.SandDock.ControlLayoutSystem(196, 314, new TD.SandDock.DockControl[] {
                                                this.RightDocks_TopPanel_Songs,
                                                this.RightDocks_TopPanel_PlayList}, this.RightDocks_TopPanel_Songs))),
                                    ((TD.SandDock.LayoutSystemBase)(new TD.SandDock.ControlLayoutSystem(196, 412, new TD.SandDock.DockControl[] {
                                                this.RightDocks_BottomPanel_Backgrounds,
                                                this.RightDocks_BottomPanel_Media,
                                                this.RightDocks_BottomPanel_MediaLists}, this.RightDocks_BottomPanel_Backgrounds)))})))})))});
			this.rightSandDock.Location = new System.Drawing.Point(576, 50);
			this.rightSandDock.Manager = this.sandDockManager1;
			this.rightSandDock.MaximumSize = 600;
			this.rightSandDock.Name = "rightSandDock";
			this.rightSandDock.Size = new System.Drawing.Size(400, 731);
			this.rightSandDock.TabIndex = 24;
			// 
			// RightDocks_BottomPanel_Media
			// 
			this.RightDocks_BottomPanel_Media.Controls.Add(this.RightDocks_BottomPanel_MediaList);
			this.RightDocks_BottomPanel_Media.Controls.Add(this.RightDocks_BottomPanel_Media_Bottom);
			this.RightDocks_BottomPanel_Media.Controls.Add(this.RightDocks_BottomPanel_Media_Top);
			this.RightDocks_BottomPanel_Media.Guid = new System.Guid("c9d617ca-0165-45b7-8e07-329a81273abc");
			this.RightDocks_BottomPanel_Media.Location = new System.Drawing.Point(204, 343);
			this.RightDocks_BottomPanel_Media.Name = "RightDocks_BottomPanel_Media";
			this.RightDocks_BottomPanel_Media.Size = new System.Drawing.Size(196, 365);
			this.RightDocks_BottomPanel_Media.TabImage = ((System.Drawing.Image)(resources.GetObject("RightDocks_BottomPanel_Media.TabImage")));
			this.RightDocks_BottomPanel_Media.TabIndex = 4;
			this.RightDocks_BottomPanel_Media.Text = "Media";
			// 
			// RightDocks_BottomPanel_MediaList
			// 
			this.RightDocks_BottomPanel_MediaList.AllowDrop = true;
			this.RightDocks_BottomPanel_MediaList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.RightDocks_BottomPanel_MediaList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.RightDocks_BottomPanel_MediaList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.RightDocks_BottomPanel_MediaList.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.RightDocks_BottomPanel_MediaList.ImageList = this.Media_ImageList;
			this.RightDocks_BottomPanel_MediaList.ItemHeight = 76;
			this.RightDocks_BottomPanel_MediaList.Location = new System.Drawing.Point(0, 32);
			this.RightDocks_BottomPanel_MediaList.Name = "RightDocks_BottomPanel_MediaList";
			this.RightDocks_BottomPanel_MediaList.Size = new System.Drawing.Size(196, 230);
			this.RightDocks_BottomPanel_MediaList.TabIndex = 3;
			this.RightDocks_BottomPanel_MediaList.SelectedIndexChanged += new System.EventHandler(this.RightDocks_BottomPanel_MediaList_SelectedIndexChanged);
			this.RightDocks_BottomPanel_MediaList.DragDrop += new System.Windows.Forms.DragEventHandler(this.RightDocks_BottomPanel_MediaList_DragDrop);
			this.RightDocks_BottomPanel_MediaList.DoubleClick += new System.EventHandler(this.RightDocks_BottomPanel_MediaList_DoubleClick);
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
			this.RightDocks_BottomPanel_Media_Bottom.Location = new System.Drawing.Point(0, 309);
			this.RightDocks_BottomPanel_Media_Bottom.Name = "RightDocks_BottomPanel_Media_Bottom";
			this.RightDocks_BottomPanel_Media_Bottom.Size = new System.Drawing.Size(196, 56);
			this.RightDocks_BottomPanel_Media_Bottom.TabIndex = 5;
			// 
			// RightDocks_BottomPanel_Media_AutoPlay
			// 
			this.RightDocks_BottomPanel_Media_AutoPlay.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.RightDocks_BottomPanel_Media_AutoPlay.Location = new System.Drawing.Point(104, 32);
			this.RightDocks_BottomPanel_Media_AutoPlay.Name = "RightDocks_BottomPanel_Media_AutoPlay";
			this.RightDocks_BottomPanel_Media_AutoPlay.Size = new System.Drawing.Size(75, 23);
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
			this.RightDocks_BottomPanel_Media_ShowNext.Size = new System.Drawing.Size(75, 23);
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
			this.RightDocks_BottomPanel_Media_FadePanelButton.Size = new System.Drawing.Size(136, 23);
			this.RightDocks_BottomPanel_Media_FadePanelButton.TabIndex = 2;
			this.RightDocks_BottomPanel_Media_FadePanelButton.Text = "Add Media...";
			this.RightDocks_BottomPanel_Media_FadePanelButton.Click += new System.EventHandler(this.RightDocks_BottomPanel_Media_FadePanelButton_Click);
			// 
			// Dock_BibleTools
			// 
			this.Dock_BibleTools.Controls.Add(this.BibleTools_CollapsiblePanelBar);
			this.Dock_BibleTools.Guid = new System.Guid("226af261-a2b0-437c-8c85-a1c4d4b57298");
			this.Dock_BibleTools.Location = new System.Drawing.Point(4, 323);
			this.Dock_BibleTools.Name = "Dock_BibleTools";
			this.Dock_BibleTools.Size = new System.Drawing.Size(196, 385);
			this.Dock_BibleTools.TabImage = ((System.Drawing.Image)(resources.GetObject("Dock_BibleTools.TabImage")));
			this.Dock_BibleTools.TabIndex = 6;
			this.Dock_BibleTools.Text = "Bible Tools";
			// 
			// BibleTools_CollapsiblePanelBar
			// 
			this.BibleTools_CollapsiblePanelBar.AutoScroll = true;
			this.BibleTools_CollapsiblePanelBar.BackColor = System.Drawing.Color.CornflowerBlue;
			this.BibleTools_CollapsiblePanelBar.Border = 8;
			this.BibleTools_CollapsiblePanelBar.Controls.Add(this.BibleBookmarks_CollapsiblePanel);
			this.BibleTools_CollapsiblePanelBar.Controls.Add(this.BibleTranslations_CollapsiblePanel);
			this.BibleTools_CollapsiblePanelBar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.BibleTools_CollapsiblePanelBar.Location = new System.Drawing.Point(0, 0);
			this.BibleTools_CollapsiblePanelBar.Name = "BibleTools_CollapsiblePanelBar";
			this.BibleTools_CollapsiblePanelBar.Size = new System.Drawing.Size(196, 385);
			this.BibleTools_CollapsiblePanelBar.Spacing = 8;
			this.BibleTools_CollapsiblePanelBar.TabIndex = 2;
			// 
			// RightDocks_BottomPanel_MediaLists
			// 
			this.RightDocks_BottomPanel_MediaLists.BackColor = System.Drawing.SystemColors.Control;
			this.RightDocks_BottomPanel_MediaLists.Controls.Add(this.RightDocks_BottomPanel_MediaListsTopPanel);
			this.RightDocks_BottomPanel_MediaLists.Controls.Add(this.RightDocks_BottomPanel_MediaLists_BottomPanel);
			this.RightDocks_BottomPanel_MediaLists.Guid = new System.Guid("3429dfd5-f5ba-4785-ac79-49140d88b66b");
			this.RightDocks_BottomPanel_MediaLists.Location = new System.Drawing.Point(204, 343);
			this.RightDocks_BottomPanel_MediaLists.Name = "RightDocks_BottomPanel_MediaLists";
			this.RightDocks_BottomPanel_MediaLists.Size = new System.Drawing.Size(196, 365);
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
			this.RightDocks_BottomPanel_MediaListsTopPanel.Size = new System.Drawing.Size(196, 265);
			this.RightDocks_BottomPanel_MediaListsTopPanel.TabIndex = 1;
			// 
			// RightDocks_MediaLists
			// 
			this.RightDocks_MediaLists.Dock = System.Windows.Forms.DockStyle.Fill;
			this.RightDocks_MediaLists.Location = new System.Drawing.Point(0, 0);
			this.RightDocks_MediaLists.Name = "RightDocks_MediaLists";
			this.RightDocks_MediaLists.Size = new System.Drawing.Size(196, 238);
			this.RightDocks_MediaLists.TabIndex = 1;
			this.RightDocks_MediaLists.DoubleClick += new System.EventHandler(this.RightDocks_MediaLists_DoubleClick);
			// 
			// RightDocks_BottomPanel_MediaListsTop_Control_Panel
			// 
			this.RightDocks_BottomPanel_MediaListsTop_Control_Panel.Controls.Add(this.RightDocks_MediaLists_DeleteButton);
			this.RightDocks_BottomPanel_MediaListsTop_Control_Panel.Controls.Add(this.RightDocks_MediaLists_LoadButton);
			this.RightDocks_BottomPanel_MediaListsTop_Control_Panel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.RightDocks_BottomPanel_MediaListsTop_Control_Panel.Location = new System.Drawing.Point(0, 241);
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
			this.RightDocks_BottomPanel_MediaLists_BottomPanel.Location = new System.Drawing.Point(0, 265);
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
			this.RightDocks_BottomPanel_MediaLists_Numeric.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
			this.RightDocks_BottomPanel_MediaLists_Numeric.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.RightDocks_BottomPanel_MediaLists_Numeric.Name = "RightDocks_BottomPanel_MediaLists_Numeric";
			this.RightDocks_BottomPanel_MediaLists_Numeric.Size = new System.Drawing.Size(64, 20);
			this.RightDocks_BottomPanel_MediaLists_Numeric.TabIndex = 1;
			this.RightDocks_BottomPanel_MediaLists_Numeric.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
			this.RightDocks_BottomPanel_MediaLists_Numeric.Value = new decimal(new int[] {
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
			// RightDocks_BottomPanel_Backgrounds
			// 
			this.RightDocks_BottomPanel_Backgrounds.Controls.Add(this.panel2);
			this.RightDocks_BottomPanel_Backgrounds.Controls.Add(this.panel6);
			this.RightDocks_BottomPanel_Backgrounds.Controls.Add(this.RightDocks_BottomPanel2_TopPanel);
			this.RightDocks_BottomPanel_Backgrounds.Guid = new System.Guid("b561dd7f-3e79-4e07-912c-18ac9600db75");
			this.RightDocks_BottomPanel_Backgrounds.Location = new System.Drawing.Point(204, 343);
			this.RightDocks_BottomPanel_Backgrounds.Name = "RightDocks_BottomPanel_Backgrounds";
			this.RightDocks_BottomPanel_Backgrounds.Size = new System.Drawing.Size(196, 365);
			this.RightDocks_BottomPanel_Backgrounds.TabImage = ((System.Drawing.Image)(resources.GetObject("RightDocks_BottomPanel_Backgrounds.TabImage")));
			this.RightDocks_BottomPanel_Backgrounds.TabIndex = 1;
			this.RightDocks_BottomPanel_Backgrounds.Text = "Backgrounds";
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.RightDocks_ImageListBox);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(0, 24);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(196, 317);
			this.panel2.TabIndex = 25;
			// 
			// panel6
			// 
			this.panel6.Controls.Add(this.RightDocks_Backgrounds_UseDefault);
			this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel6.Location = new System.Drawing.Point(0, 341);
			this.panel6.Name = "panel6";
			this.panel6.Padding = new System.Windows.Forms.Padding(2);
			this.panel6.Size = new System.Drawing.Size(196, 24);
			this.panel6.TabIndex = 24;
			// 
			// RightDocks_Backgrounds_UseDefault
			// 
			this.RightDocks_Backgrounds_UseDefault.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.RightDocks_Backgrounds_UseDefault.Location = new System.Drawing.Point(60, 3);
			this.RightDocks_Backgrounds_UseDefault.Name = "RightDocks_Backgrounds_UseDefault";
			this.RightDocks_Backgrounds_UseDefault.Size = new System.Drawing.Size(75, 18);
			this.RightDocks_Backgrounds_UseDefault.TabIndex = 23;
			this.RightDocks_Backgrounds_UseDefault.Text = "Use Default";
			this.RightDocks_Backgrounds_UseDefault.Click += new System.EventHandler(this.RightDocks_Backgrounds_UseDefault_Click);
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
			// RightDocks_TopPanel_Songs
			// 
			this.RightDocks_TopPanel_Songs.Controls.Add(this.SongList_Tree);
			this.RightDocks_TopPanel_Songs.Controls.Add(this.RightDocks_Songlist_SearchPanel);
			this.RightDocks_TopPanel_Songs.Controls.Add(this.RightDocks_SongList_ButtonPanel);
			this.RightDocks_TopPanel_Songs.Guid = new System.Guid("6044bb67-05ab-4617-bbfa-99e49388b41f");
			this.RightDocks_TopPanel_Songs.Location = new System.Drawing.Point(204, 25);
			this.RightDocks_TopPanel_Songs.Name = "RightDocks_TopPanel_Songs";
			this.RightDocks_TopPanel_Songs.Size = new System.Drawing.Size(196, 266);
			this.RightDocks_TopPanel_Songs.TabImage = ((System.Drawing.Image)(resources.GetObject("RightDocks_TopPanel_Songs.TabImage")));
			this.RightDocks_TopPanel_Songs.TabIndex = 0;
			this.RightDocks_TopPanel_Songs.Text = "Songs";
			// 
			// SongList_Tree
			// 
			this.SongList_Tree.AutoBuildTree = true;
			this.SongList_Tree.DataSource = null;
			this.SongList_Tree.DisplayMember = null;
			this.SongList_Tree.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SongList_Tree.Location = new System.Drawing.Point(0, 23);
			this.SongList_Tree.Name = "SongList_Tree";
			this.SongList_Tree.Size = new System.Drawing.Size(196, 223);
			this.SongList_Tree.TabIndex = 7;
			this.SongList_Tree.ValueMember = null;
			this.SongList_Tree.DoubleClick += new System.EventHandler(this.SongList_Tree_DoubleClick);
			this.SongList_Tree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.SongList_Tree_AfterSelect);
			// 
			// RightDocks_TopPanel_PlayList
			// 
			this.RightDocks_TopPanel_PlayList.Controls.Add(this.RightDocks_PlayList);
			this.RightDocks_TopPanel_PlayList.Controls.Add(this.RightDocks_TopPanel_PlayList_Button_Panel);
			this.RightDocks_TopPanel_PlayList.Guid = new System.Guid("92186926-e7f9-4850-98b8-190a99f81ea6");
			this.RightDocks_TopPanel_PlayList.Location = new System.Drawing.Point(204, 25);
			this.RightDocks_TopPanel_PlayList.Name = "RightDocks_TopPanel_PlayList";
			this.RightDocks_TopPanel_PlayList.Size = new System.Drawing.Size(196, 266);
			this.RightDocks_TopPanel_PlayList.TabImage = ((System.Drawing.Image)(resources.GetObject("RightDocks_TopPanel_PlayList.TabImage")));
			this.RightDocks_TopPanel_PlayList.TabIndex = 2;
			this.RightDocks_TopPanel_PlayList.Text = "Playlist";
			// 
			// RightDocks_PreviewScreen
			// 
			this.RightDocks_PreviewScreen.Controls.Add(this.RightDocks_PreviewScreen_PictureBox);
			this.RightDocks_PreviewScreen.Controls.Add(this.panel5);
			this.RightDocks_PreviewScreen.Guid = new System.Guid("8a7e4f9a-a6a1-48b7-a273-8494de07e6b2");
			this.RightDocks_PreviewScreen.Location = new System.Drawing.Point(4, 25);
			this.RightDocks_PreviewScreen.Name = "RightDocks_PreviewScreen";
			this.RightDocks_PreviewScreen.Size = new System.Drawing.Size(196, 246);
			this.RightDocks_PreviewScreen.TabImage = ((System.Drawing.Image)(resources.GetObject("RightDocks_PreviewScreen.TabImage")));
			this.RightDocks_PreviewScreen.TabIndex = 1;
			this.RightDocks_PreviewScreen.Text = "Preview";
			// 
			// RightDocks_PreviewScreen_PictureBox
			// 
			this.RightDocks_PreviewScreen_PictureBox.BackColor = System.Drawing.Color.Black;
			this.RightDocks_PreviewScreen_PictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.RightDocks_PreviewScreen_PictureBox.Location = new System.Drawing.Point(0, 0);
			this.RightDocks_PreviewScreen_PictureBox.Name = "RightDocks_PreviewScreen_PictureBox";
			this.RightDocks_PreviewScreen_PictureBox.Size = new System.Drawing.Size(196, 216);
			this.RightDocks_PreviewScreen_PictureBox.TabIndex = 0;
			this.RightDocks_PreviewScreen_PictureBox.TabStop = false;
			this.RightDocks_PreviewScreen_PictureBox.SizeChanged += new System.EventHandler(this.RightDocks_PreviewScreen_PictureBox_SizeChanged);
			// 
			// panel5
			// 
			this.panel5.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.panel5.Controls.Add(this.RightDocks_Preview_Next);
			this.panel5.Controls.Add(this.RightDocks_Preview_GoLive);
			this.panel5.Controls.Add(this.RightDocks_Preview_Prev);
			this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel5.Location = new System.Drawing.Point(0, 216);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(196, 30);
			this.panel5.TabIndex = 1;
			// 
			// RightDocks_Preview_Next
			// 
			this.RightDocks_Preview_Next.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.RightDocks_Preview_Next.BackColor = System.Drawing.SystemColors.Control;
			this.RightDocks_Preview_Next.Location = new System.Drawing.Point(144, 6);
			this.RightDocks_Preview_Next.Name = "RightDocks_Preview_Next";
			this.RightDocks_Preview_Next.Size = new System.Drawing.Size(50, 20);
			this.RightDocks_Preview_Next.TabIndex = 2;
			this.RightDocks_Preview_Next.Text = "Next ->";
			this.RightDocks_Preview_Next.UseVisualStyleBackColor = false;
			this.RightDocks_Preview_Next.Click += new System.EventHandler(this.RightDocks_Preview_Next_Click);
			// 
			// RightDocks_Preview_GoLive
			// 
			this.RightDocks_Preview_GoLive.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.RightDocks_Preview_GoLive.BackColor = System.Drawing.Color.Maroon;
			this.RightDocks_Preview_GoLive.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.RightDocks_Preview_GoLive.ForeColor = System.Drawing.Color.White;
			this.RightDocks_Preview_GoLive.Location = new System.Drawing.Point(70, 6);
			this.RightDocks_Preview_GoLive.Name = "RightDocks_Preview_GoLive";
			this.RightDocks_Preview_GoLive.Size = new System.Drawing.Size(55, 20);
			this.RightDocks_Preview_GoLive.TabIndex = 1;
			this.RightDocks_Preview_GoLive.Text = "Live";
			this.MainForm_ToolTip.SetToolTip(this.RightDocks_Preview_GoLive, "Show the contents on the projector");
			this.RightDocks_Preview_GoLive.UseVisualStyleBackColor = false;
			this.RightDocks_Preview_GoLive.Click += new System.EventHandler(this.RightDocks_Preview_GoLive_Click);
			// 
			// RightDocks_Preview_Prev
			// 
			this.RightDocks_Preview_Prev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.RightDocks_Preview_Prev.BackColor = System.Drawing.SystemColors.Control;
			this.RightDocks_Preview_Prev.Location = new System.Drawing.Point(2, 6);
			this.RightDocks_Preview_Prev.Name = "RightDocks_Preview_Prev";
			this.RightDocks_Preview_Prev.Size = new System.Drawing.Size(50, 20);
			this.RightDocks_Preview_Prev.TabIndex = 0;
			this.RightDocks_Preview_Prev.Text = "<- Prev";
			this.RightDocks_Preview_Prev.UseVisualStyleBackColor = false;
			this.RightDocks_Preview_Prev.Click += new System.EventHandler(this.RightDocks_Preview_Prev_Click);
			// 
			// RightDocks_LiveScreen
			// 
			this.RightDocks_LiveScreen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.RightDocks_LiveScreen.Controls.Add(this.RightDocks_LiveScreen_PictureBox);
			this.RightDocks_LiveScreen.Controls.Add(this.panel9);
			this.RightDocks_LiveScreen.Guid = new System.Guid("e37c7ea7-844c-4a80-baba-1b5d7ddb42e7");
			this.RightDocks_LiveScreen.Location = new System.Drawing.Point(4, 25);
			this.RightDocks_LiveScreen.Name = "RightDocks_LiveScreen";
			this.RightDocks_LiveScreen.Size = new System.Drawing.Size(196, 246);
			this.RightDocks_LiveScreen.TabImage = ((System.Drawing.Image)(resources.GetObject("RightDocks_LiveScreen.TabImage")));
			this.RightDocks_LiveScreen.TabIndex = 2;
			this.RightDocks_LiveScreen.Text = "Live";
			// 
			// RightDocks_LiveScreen_PictureBox
			// 
			this.RightDocks_LiveScreen_PictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.RightDocks_LiveScreen_PictureBox.Location = new System.Drawing.Point(0, 0);
			this.RightDocks_LiveScreen_PictureBox.Name = "RightDocks_LiveScreen_PictureBox";
			this.RightDocks_LiveScreen_PictureBox.Size = new System.Drawing.Size(196, 216);
			this.RightDocks_LiveScreen_PictureBox.TabIndex = 0;
			this.RightDocks_LiveScreen_PictureBox.TabStop = false;
			this.RightDocks_LiveScreen_PictureBox.SizeChanged += new System.EventHandler(this.RightDocks_LiveScreen_PictureBox_SizeChanged);
			// 
			// panel9
			// 
			this.panel9.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.panel9.Controls.Add(this.RightDocks_Live_Next);
			this.panel9.Controls.Add(this.RightDocks_Live_Prev);
			this.panel9.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel9.Location = new System.Drawing.Point(0, 216);
			this.panel9.Name = "panel9";
			this.panel9.Size = new System.Drawing.Size(196, 30);
			this.panel9.TabIndex = 1;
			// 
			// RightDocks_Live_Next
			// 
			this.RightDocks_Live_Next.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.RightDocks_Live_Next.BackColor = System.Drawing.SystemColors.Control;
			this.RightDocks_Live_Next.Location = new System.Drawing.Point(116, 6);
			this.RightDocks_Live_Next.Name = "RightDocks_Live_Next";
			this.RightDocks_Live_Next.Size = new System.Drawing.Size(75, 20);
			this.RightDocks_Live_Next.TabIndex = 1;
			this.RightDocks_Live_Next.Text = "Next ->";
			this.RightDocks_Live_Next.UseVisualStyleBackColor = false;
			this.RightDocks_Live_Next.Click += new System.EventHandler(this.RightDocks_Live_Next_Click);
			// 
			// RightDocks_Live_Prev
			// 
			this.RightDocks_Live_Prev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.RightDocks_Live_Prev.BackColor = System.Drawing.SystemColors.Control;
			this.RightDocks_Live_Prev.Location = new System.Drawing.Point(6, 6);
			this.RightDocks_Live_Prev.Name = "RightDocks_Live_Prev";
			this.RightDocks_Live_Prev.Size = new System.Drawing.Size(75, 20);
			this.RightDocks_Live_Prev.TabIndex = 0;
			this.RightDocks_Live_Prev.Text = "<- Prev";
			this.RightDocks_Live_Prev.UseVisualStyleBackColor = false;
			this.RightDocks_Live_Prev.Click += new System.EventHandler(this.RightDocks_Live_Prev_Click);
			// 
			// Dock_SongTools
			// 
			this.Dock_SongTools.Controls.Add(this.SongShow_CollapsPanel);
			this.Dock_SongTools.Guid = new System.Guid("7bae0dc0-4ef3-4da4-a888-a9cafaee19c6");
			this.Dock_SongTools.Location = new System.Drawing.Point(4, 323);
			this.Dock_SongTools.Name = "Dock_SongTools";
			this.Dock_SongTools.Size = new System.Drawing.Size(196, 385);
			this.Dock_SongTools.TabIndex = 7;
			this.Dock_SongTools.Text = "Song Tools";
			// 
			// bottomSandDock
			// 
			this.bottomSandDock.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.bottomSandDock.Guid = new System.Guid("11832edf-4b5f-4911-9e7c-da764193d89f");
			this.bottomSandDock.LayoutSystem = new TD.SandDock.SplitLayoutSystem(250, 400);
			this.bottomSandDock.Location = new System.Drawing.Point(72, 781);
			this.bottomSandDock.Manager = this.sandDockManager1;
			this.bottomSandDock.Name = "bottomSandDock";
			this.bottomSandDock.Size = new System.Drawing.Size(504, 0);
			this.bottomSandDock.TabIndex = 25;
			// 
			// topSandDock
			// 
			this.topSandDock.Dock = System.Windows.Forms.DockStyle.Top;
			this.topSandDock.Guid = new System.Guid("f3084065-73e3-4825-a957-0918e3006a24");
			this.topSandDock.LayoutSystem = new TD.SandDock.SplitLayoutSystem(250, 400);
			this.topSandDock.Location = new System.Drawing.Point(72, 50);
			this.topSandDock.Manager = this.sandDockManager1;
			this.topSandDock.Name = "topSandDock";
			this.topSandDock.Size = new System.Drawing.Size(504, 0);
			this.topSandDock.TabIndex = 26;
			// 
			// Media_Logos
			// 
			this.Media_Logos.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("Media_Logos.ImageStream")));
			this.Media_Logos.TransparentColor = System.Drawing.Color.Transparent;
			this.Media_Logos.Images.SetKeyName(0, "");
			this.Media_Logos.Images.SetKeyName(1, "");
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
			// Sermon_BibleKey
			// 
			this.Sermon_BibleKey.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Sermon_BibleKey.Location = new System.Drawing.Point(8, 32);
			this.Sermon_BibleKey.Name = "Sermon_BibleKey";
			this.Sermon_BibleKey.Size = new System.Drawing.Size(128, 20);
			this.Sermon_BibleKey.TabIndex = 3;
			this.MainForm_ToolTip.SetToolTip(this.Sermon_BibleKey, "You can enter a reference, or a range: \"Mat 1:1 - 1:8\"");
			this.Sermon_BibleKey.TextChanged += new System.EventHandler(this.Sermon_BibleKey_TextChanged);
			this.Sermon_BibleKey.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Sermon_BibleKey_KeyDown);
			// 
			// BibleText_FindLast_button
			// 
			this.BibleText_FindLast_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BibleText_FindLast_button.BackColor = System.Drawing.SystemColors.Control;
			this.BibleText_FindLast_button.Image = ((System.Drawing.Image)(resources.GetObject("BibleText_FindLast_button.Image")));
			this.BibleText_FindLast_button.Location = new System.Drawing.Point(450, 2);
			this.BibleText_FindLast_button.Name = "BibleText_FindLast_button";
			this.BibleText_FindLast_button.Size = new System.Drawing.Size(32, 32);
			this.BibleText_FindLast_button.TabIndex = 5;
			this.MainForm_ToolTip.SetToolTip(this.BibleText_FindLast_button, "Find last occurance of the RegEx");
			this.BibleText_FindLast_button.UseVisualStyleBackColor = false;
			this.BibleText_FindLast_button.Click += new System.EventHandler(this.BibleText_FindLast_button_Click);
			// 
			// BibleText_FindFirst_button
			// 
			this.BibleText_FindFirst_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BibleText_FindFirst_button.Image = ((System.Drawing.Image)(resources.GetObject("BibleText_FindFirst_button.Image")));
			this.BibleText_FindFirst_button.Location = new System.Drawing.Point(410, 2);
			this.BibleText_FindFirst_button.Name = "BibleText_FindFirst_button";
			this.BibleText_FindFirst_button.Size = new System.Drawing.Size(32, 32);
			this.BibleText_FindFirst_button.TabIndex = 4;
			this.MainForm_ToolTip.SetToolTip(this.BibleText_FindFirst_button, "Find first occurance of the RegEx");
			this.BibleText_FindFirst_button.Click += new System.EventHandler(this.BibleText_FindFirst_button_Click);
			// 
			// BibleText_FindPrev_button
			// 
			this.BibleText_FindPrev_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BibleText_FindPrev_button.Image = ((System.Drawing.Image)(resources.GetObject("BibleText_FindPrev_button.Image")));
			this.BibleText_FindPrev_button.Location = new System.Drawing.Point(370, 2);
			this.BibleText_FindPrev_button.Name = "BibleText_FindPrev_button";
			this.BibleText_FindPrev_button.Size = new System.Drawing.Size(32, 32);
			this.BibleText_FindPrev_button.TabIndex = 3;
			this.MainForm_ToolTip.SetToolTip(this.BibleText_FindPrev_button, "Find previous occurance of the RegEx");
			this.BibleText_FindPrev_button.Click += new System.EventHandler(this.BibleText_FindPrev_button_Click);
			// 
			// BibleText_FindNext_button
			// 
			this.BibleText_FindNext_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BibleText_FindNext_button.BackColor = System.Drawing.SystemColors.Control;
			this.BibleText_FindNext_button.Image = ((System.Drawing.Image)(resources.GetObject("BibleText_FindNext_button.Image")));
			this.BibleText_FindNext_button.Location = new System.Drawing.Point(330, 2);
			this.BibleText_FindNext_button.Name = "BibleText_FindNext_button";
			this.BibleText_FindNext_button.Size = new System.Drawing.Size(32, 32);
			this.BibleText_FindNext_button.TabIndex = 2;
			this.MainForm_ToolTip.SetToolTip(this.BibleText_FindNext_button, "Find next occurance of the RegEx");
			this.BibleText_FindNext_button.UseVisualStyleBackColor = false;
			this.BibleText_FindNext_button.Click += new System.EventHandler(this.BibleText_FindNext_button_Click);
			// 
			// BibleText_Bookmark_button
			// 
			this.BibleText_Bookmark_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BibleText_Bookmark_button.Image = ((System.Drawing.Image)(resources.GetObject("BibleText_Bookmark_button.Image")));
			this.BibleText_Bookmark_button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.BibleText_Bookmark_button.Location = new System.Drawing.Point(394, 4);
			this.BibleText_Bookmark_button.Name = "BibleText_Bookmark_button";
			this.BibleText_Bookmark_button.Size = new System.Drawing.Size(88, 32);
			this.BibleText_Bookmark_button.TabIndex = 2;
			this.BibleText_Bookmark_button.Text = "Bookmark";
			this.BibleText_Bookmark_button.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.MainForm_ToolTip.SetToolTip(this.BibleText_Bookmark_button, "Bookmark the current verse");
			this.BibleText_Bookmark_button.Click += new System.EventHandler(this.BibleText_Bookmark_button_Click);
			// 
			// Main_ErrorProvider
			// 
			this.Main_ErrorProvider.ContainerControl = this;
			// 
			// SaveFileDialog
			// 
			this.SaveFileDialog.DefaultExt = "xml";
			// 
			// ShowSong_Tab
			// 
			this.ShowSong_Tab.Controls.Add(this.SongShow_StropheList_ListEx);
			this.ShowSong_Tab.Location = new System.Drawing.Point(4, 14);
			this.ShowSong_Tab.Name = "ShowSong_Tab";
			this.ShowSong_Tab.Size = new System.Drawing.Size(496, 691);
			this.ShowSong_Tab.TabIndex = 2;
			this.ShowSong_Tab.Text = "Show Songs";
			// 
			// SongShow_StropheList_ListEx
			// 
			this.SongShow_StropheList_ListEx.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SongShow_StropheList_ListEx.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
			this.SongShow_StropheList_ListEx.Imgs = this.SongShow_ImageList;
			this.SongShow_StropheList_ListEx.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(199)))), ((int)(((byte)(199)))));
			this.SongShow_StropheList_ListEx.Location = new System.Drawing.Point(0, 0);
			this.SongShow_StropheList_ListEx.Name = "SongShow_StropheList_ListEx";
			this.SongShow_StropheList_ListEx.ReadOnly = true;
			this.SongShow_StropheList_ListEx.ShowBullets = true;
			this.SongShow_StropheList_ListEx.Size = new System.Drawing.Size(496, 691);
			this.SongShow_StropheList_ListEx.TabIndex = 0;
			this.SongShow_StropheList_ListEx.SelectedIndexChanged += new System.EventHandler(this.SongShow_StropheList_ListEx_SelectedIndexChanged);
			this.SongShow_StropheList_ListEx.PressIcon += new Lister.ListEx.EventHandler(this.SongShow_StropheList_ListEx_PressIcon);
			// 
			// SermonTools_Tab
			// 
			this.SermonTools_Tab.Controls.Add(this.Sermon_LeftPanel);
			this.SermonTools_Tab.Controls.Add(this.Sermon_TabControl);
			this.SermonTools_Tab.Location = new System.Drawing.Point(4, 14);
			this.SermonTools_Tab.Name = "SermonTools_Tab";
			this.SermonTools_Tab.Size = new System.Drawing.Size(496, 691);
			this.SermonTools_Tab.TabIndex = 3;
			this.SermonTools_Tab.Text = "SermonTool";
			// 
			// Sermon_LeftPanel
			// 
			this.Sermon_LeftPanel.Controls.Add(this.Sermon_LeftDoc_Panel);
			this.Sermon_LeftPanel.Controls.Add(this.Sermon_LeftToolBar_Panel);
			this.Sermon_LeftPanel.Controls.Add(this.Sermon_LeftBottom_Panel);
			this.Sermon_LeftPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Sermon_LeftPanel.Location = new System.Drawing.Point(0, 0);
			this.Sermon_LeftPanel.Name = "Sermon_LeftPanel";
			this.Sermon_LeftPanel.Size = new System.Drawing.Size(352, 691);
			this.Sermon_LeftPanel.TabIndex = 3;
			// 
			// Sermon_LeftDoc_Panel
			// 
			this.Sermon_LeftDoc_Panel.Controls.Add(this.Sermon_DocManager);
			this.Sermon_LeftDoc_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Sermon_LeftDoc_Panel.Location = new System.Drawing.Point(0, 24);
			this.Sermon_LeftDoc_Panel.Name = "Sermon_LeftDoc_Panel";
			this.Sermon_LeftDoc_Panel.Size = new System.Drawing.Size(352, 637);
			this.Sermon_LeftDoc_Panel.TabIndex = 4;
			// 
			// Sermon_DocManager
			// 
			this.Sermon_DocManager.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Sermon_DocManager.Location = new System.Drawing.Point(0, 0);
			this.Sermon_DocManager.Name = "Sermon_DocManager";
			this.Sermon_DocManager.Size = new System.Drawing.Size(352, 637);
			this.Sermon_DocManager.TabIndex = 1;
			this.Sermon_DocManager.CloseButtonPressed += new DocumentManager.DocumentManager.CloseButtonPressedEventHandler(this.Sermon_DocManager_CloseButtonPressed);
			// 
			// Sermon_LeftToolBar_Panel
			// 
			this.Sermon_LeftToolBar_Panel.Controls.Add(this.Sermon_ToolBar);
			this.Sermon_LeftToolBar_Panel.Dock = System.Windows.Forms.DockStyle.Top;
			this.Sermon_LeftToolBar_Panel.Location = new System.Drawing.Point(0, 0);
			this.Sermon_LeftToolBar_Panel.Name = "Sermon_LeftToolBar_Panel";
			this.Sermon_LeftToolBar_Panel.Size = new System.Drawing.Size(352, 24);
			this.Sermon_LeftToolBar_Panel.TabIndex = 3;
			// 
			// Sermon_ToolBar
			// 
			this.Sermon_ToolBar.Buttons.AddRange(new TD.SandBar.ToolbarItemBase[] {
            this.Sermon_ToolBar_NewDoc_Button});
			this.Sermon_ToolBar.Guid = new System.Guid("e8851e50-0b9f-4b93-8a5e-c7a9528f2cf6");
			this.Sermon_ToolBar.ImageList = null;
			this.Sermon_ToolBar.Location = new System.Drawing.Point(0, 0);
			this.Sermon_ToolBar.Name = "Sermon_ToolBar";
			this.Sermon_ToolBar.Size = new System.Drawing.Size(352, 24);
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
			// Sermon_LeftBottom_Panel
			// 
			this.Sermon_LeftBottom_Panel.Controls.Add(this.Sermon_Preview_Button);
			this.Sermon_LeftBottom_Panel.Controls.Add(this.Sermon_BeamBox_Button);
			this.Sermon_LeftBottom_Panel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.Sermon_LeftBottom_Panel.Location = new System.Drawing.Point(0, 661);
			this.Sermon_LeftBottom_Panel.Name = "Sermon_LeftBottom_Panel";
			this.Sermon_LeftBottom_Panel.Size = new System.Drawing.Size(352, 30);
			this.Sermon_LeftBottom_Panel.TabIndex = 5;
			// 
			// Sermon_Preview_Button
			// 
			this.Sermon_Preview_Button.Location = new System.Drawing.Point(16, 4);
			this.Sermon_Preview_Button.Name = "Sermon_Preview_Button";
			this.Sermon_Preview_Button.Size = new System.Drawing.Size(75, 23);
			this.Sermon_Preview_Button.TabIndex = 3;
			this.Sermon_Preview_Button.Text = "Preview";
			this.Sermon_Preview_Button.Click += new System.EventHandler(this.Sermon_Preview_Button_Click);
			// 
			// Sermon_BeamBox_Button
			// 
			this.Sermon_BeamBox_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.Sermon_BeamBox_Button.BackColor = System.Drawing.Color.Maroon;
			this.Sermon_BeamBox_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Sermon_BeamBox_Button.ForeColor = System.Drawing.Color.White;
			this.Sermon_BeamBox_Button.Location = new System.Drawing.Point(258, 4);
			this.Sermon_BeamBox_Button.Name = "Sermon_BeamBox_Button";
			this.Sermon_BeamBox_Button.Size = new System.Drawing.Size(80, 23);
			this.Sermon_BeamBox_Button.TabIndex = 2;
			this.Sermon_BeamBox_Button.Text = "Live";
			this.Sermon_BeamBox_Button.UseVisualStyleBackColor = false;
			this.Sermon_BeamBox_Button.Click += new System.EventHandler(this.Sermon_BeamBox_Button_Click);
			// 
			// Sermon_TabControl
			// 
			this.Sermon_TabControl.Controls.Add(this.tabPage3);
			this.Sermon_TabControl.Dock = System.Windows.Forms.DockStyle.Right;
			this.Sermon_TabControl.ItemSize = new System.Drawing.Size(42, 18);
			this.Sermon_TabControl.Location = new System.Drawing.Point(352, 0);
			this.Sermon_TabControl.Name = "Sermon_TabControl";
			this.Sermon_TabControl.SelectedIndex = 0;
			this.Sermon_TabControl.Size = new System.Drawing.Size(144, 691);
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
			this.tabPage3.Size = new System.Drawing.Size(136, 665);
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
			this.linkLabel1.Text = "Get the Sword Bible";
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
			this.Sermon_Books.ItemHeight = 13;
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
			// Presentation_Tab
			// 
			this.Presentation_Tab.BackColor = System.Drawing.Color.Transparent;
			this.Presentation_Tab.Controls.Add(this.Presentation_FadePanel);
			this.Presentation_Tab.Controls.Add(this.Presentation_MainPanel);
			this.Presentation_Tab.Location = new System.Drawing.Point(4, 14);
			this.Presentation_Tab.Name = "Presentation_Tab";
			this.Presentation_Tab.Size = new System.Drawing.Size(496, 691);
			this.Presentation_Tab.TabIndex = 4;
			this.Presentation_Tab.Text = "Presentation";
			// 
			// Presentation_FadePanel
			// 
			this.Presentation_FadePanel.BackColor = System.Drawing.Color.Gainsboro;
			this.Presentation_FadePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Presentation_FadePanel.Controls.Add(this.Fade_panel);
			this.Presentation_FadePanel.Controls.Add(this.treeView1);
			this.Presentation_FadePanel.Dock = System.Windows.Forms.DockStyle.Right;
			this.Presentation_FadePanel.Location = new System.Drawing.Point(488, 0);
			this.Presentation_FadePanel.Name = "Presentation_FadePanel";
			this.Presentation_FadePanel.Padding = new System.Windows.Forms.Padding(2);
			this.Presentation_FadePanel.Size = new System.Drawing.Size(8, 691);
			this.Presentation_FadePanel.TabIndex = 3;
			// 
			// Fade_panel
			// 
			this.Fade_panel.Controls.Add(this.Presentation_Fade_ListView);
			this.Fade_panel.Controls.Add(this.Fade_Top_Panel);
			this.Fade_panel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Fade_panel.Location = new System.Drawing.Point(192, 2);
			this.Fade_panel.Name = "Fade_panel";
			this.Fade_panel.Size = new System.Drawing.Size(0, 685);
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
			this.Presentation_Fade_ListView.Size = new System.Drawing.Size(0, 533);
			this.Presentation_Fade_ListView.SmallImageList = this.Presentation_Fade_ImageList;
			this.Presentation_Fade_ListView.TabIndex = 3;
			this.Presentation_Fade_ListView.UseCompatibleStateImageBehavior = false;
			this.Presentation_Fade_ListView.View = System.Windows.Forms.View.List;
			this.Presentation_Fade_ListView.DoubleClick += new System.EventHandler(this.Presentation_Fade_ListView_DoubleClick);
			this.Presentation_Fade_ListView.Click += new System.EventHandler(this.Presentation_Fade_ListView_Click);
			// 
			// Fade_Top_Panel
			// 
			this.Fade_Top_Panel.Controls.Add(this.Presentation_Fade_Refresh_Button);
			this.Fade_Top_Panel.Controls.Add(this.Presentation_Fade_ToPlaylist_Button);
			this.Fade_Top_Panel.Controls.Add(this.Presentation_Fade_preview);
			this.Fade_Top_Panel.Dock = System.Windows.Forms.DockStyle.Top;
			this.Fade_Top_Panel.Location = new System.Drawing.Point(0, 0);
			this.Fade_Top_Panel.Name = "Fade_Top_Panel";
			this.Fade_Top_Panel.Size = new System.Drawing.Size(0, 152);
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
			this.treeView1.ImageIndex = 0;
			this.treeView1.ImageList = this.imageList_Folders;
			this.treeView1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.treeView1.Location = new System.Drawing.Point(2, 2);
			this.treeView1.Name = "treeView1";
			treeNode4.Name = "";
			treeNode4.Text = "Node2";
			treeNode5.Name = "";
			treeNode5.Text = "Node1";
			treeNode6.Name = "";
			treeNode6.Text = "Node0";
			this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode6});
			this.treeView1.SelectedImageIndex = 0;
			this.treeView1.Size = new System.Drawing.Size(190, 685);
			this.treeView1.TabIndex = 2;
			this.treeView1.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeExpand);
			this.treeView1.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeSelect);
			// 
			// Presentation_MainPanel
			// 
			this.Presentation_MainPanel.BackColor = System.Drawing.Color.Transparent;
			this.Presentation_MainPanel.Controls.Add(this.Presentation_PreviewPanel);
			this.Presentation_MainPanel.Controls.Add(this.Presentation_MovieControlPanel);
			this.Presentation_MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Presentation_MainPanel.Location = new System.Drawing.Point(0, 0);
			this.Presentation_MainPanel.Name = "Presentation_MainPanel";
			this.Presentation_MainPanel.Size = new System.Drawing.Size(496, 691);
			this.Presentation_MainPanel.TabIndex = 4;
			// 
			// Presentation_PreviewPanel
			// 
			this.Presentation_PreviewPanel.BackColor = System.Drawing.Color.Black;
			this.Presentation_PreviewPanel.Controls.Add(this.Presentation_VideoPanel);
			this.Presentation_PreviewPanel.Controls.Add(this.axShockwaveFlash);
			this.Presentation_PreviewPanel.Controls.Add(this.Presentation_PreviewBox);
			this.Presentation_PreviewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Presentation_PreviewPanel.Location = new System.Drawing.Point(0, 0);
			this.Presentation_PreviewPanel.Name = "Presentation_PreviewPanel";
			this.Presentation_PreviewPanel.Padding = new System.Windows.Forms.Padding(10);
			this.Presentation_PreviewPanel.Size = new System.Drawing.Size(496, 631);
			this.Presentation_PreviewPanel.TabIndex = 2;
			this.Presentation_PreviewPanel.Resize += new System.EventHandler(this.Presentation_PreviewPanel_Resize);
			// 
			// Presentation_VideoPanel
			// 
			this.Presentation_VideoPanel.BackColor = System.Drawing.Color.White;
			this.Presentation_VideoPanel.Location = new System.Drawing.Point(32, 32);
			this.Presentation_VideoPanel.Name = "Presentation_VideoPanel";
			this.Presentation_VideoPanel.Size = new System.Drawing.Size(424, 176);
			this.Presentation_VideoPanel.TabIndex = 2;
			this.Presentation_VideoPanel.Visible = false;
			// 
			// axShockwaveFlash
			// 
			this.axShockwaveFlash.Enabled = true;
			this.axShockwaveFlash.Location = new System.Drawing.Point(16, 16);
			this.axShockwaveFlash.Name = "axShockwaveFlash";
			this.axShockwaveFlash.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axShockwaveFlash.OcxState")));
			this.axShockwaveFlash.Size = new System.Drawing.Size(497, 369);
			this.axShockwaveFlash.TabIndex = 1;
			this.axShockwaveFlash.TabStop = false;
			this.axShockwaveFlash.Visible = false;
			// 
			// Presentation_PreviewBox
			// 
			this.Presentation_PreviewBox.BackColor = System.Drawing.Color.White;
			this.Presentation_PreviewBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Presentation_PreviewBox.Location = new System.Drawing.Point(10, 10);
			this.Presentation_PreviewBox.Name = "Presentation_PreviewBox";
			this.Presentation_PreviewBox.Size = new System.Drawing.Size(476, 611);
			this.Presentation_PreviewBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.Presentation_PreviewBox.TabIndex = 0;
			this.Presentation_PreviewBox.TabStop = false;
			this.Presentation_PreviewBox.Visible = false;
			// 
			// Presentation_MovieControlPanel
			// 
			this.Presentation_MovieControlPanel.BackColor = System.Drawing.Color.Gainsboro;
			this.Presentation_MovieControlPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Presentation_MovieControlPanel.Controls.Add(this.Presentation_MovieControlPanelBottom);
			this.Presentation_MovieControlPanel.Controls.Add(this.Presentation_MovieControlPanel_Top);
			this.Presentation_MovieControlPanel.Controls.Add(this.Presentation_MovieControlPanel_Right);
			this.Presentation_MovieControlPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.Presentation_MovieControlPanel.Location = new System.Drawing.Point(0, 631);
			this.Presentation_MovieControlPanel.Name = "Presentation_MovieControlPanel";
			this.Presentation_MovieControlPanel.Size = new System.Drawing.Size(496, 60);
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
			this.Presentation_MovieControlPanelBottom.Size = new System.Drawing.Size(446, 30);
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
			this.Presentation_MovieControl_PreviewButtonPanel.Location = new System.Drawing.Point(246, 0);
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
			this.Presentation_PlayBar.ButtonSize = new System.Drawing.Size(23, 22);
			this.Presentation_PlayBar.DropDownArrows = true;
			this.Presentation_PlayBar.ImageList = this.PlayButtons_ImageList;
			this.Presentation_PlayBar.Location = new System.Drawing.Point(0, 0);
			this.Presentation_PlayBar.Name = "Presentation_PlayBar";
			this.Presentation_PlayBar.ShowToolTips = true;
			this.Presentation_PlayBar.Size = new System.Drawing.Size(72, 28);
			this.Presentation_PlayBar.TabIndex = 0;
			this.Presentation_PlayBar.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.Presentation_PlayBar_ButtonClick);
			// 
			// Presentation_PlayButton
			// 
			this.Presentation_PlayButton.Enabled = false;
			this.Presentation_PlayButton.ImageIndex = 0;
			this.Presentation_PlayButton.Name = "Presentation_PlayButton";
			this.Presentation_PlayButton.ToolTipText = "Play On Projector";
			// 
			// Presentation_PauseButton
			// 
			this.Presentation_PauseButton.Enabled = false;
			this.Presentation_PauseButton.ImageIndex = 1;
			this.Presentation_PauseButton.Name = "Presentation_PauseButton";
			// 
			// Presentation_StopButton
			// 
			this.Presentation_StopButton.Enabled = false;
			this.Presentation_StopButton.ImageIndex = 2;
			this.Presentation_StopButton.Name = "Presentation_StopButton";
			// 
			// Presentation_MovieControlPanel_Top
			// 
			this.Presentation_MovieControlPanel_Top.Controls.Add(this.Media_TrackBar);
			this.Presentation_MovieControlPanel_Top.Dock = System.Windows.Forms.DockStyle.Top;
			this.Presentation_MovieControlPanel_Top.Location = new System.Drawing.Point(0, 0);
			this.Presentation_MovieControlPanel_Top.Name = "Presentation_MovieControlPanel_Top";
			this.Presentation_MovieControlPanel_Top.Size = new System.Drawing.Size(446, 30);
			this.Presentation_MovieControlPanel_Top.TabIndex = 3;
			// 
			// Media_TrackBar
			// 
			this.Media_TrackBar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Media_TrackBar.Enabled = false;
			this.Media_TrackBar.Location = new System.Drawing.Point(0, 0);
			this.Media_TrackBar.Name = "Media_TrackBar";
			this.Media_TrackBar.Size = new System.Drawing.Size(446, 30);
			this.Media_TrackBar.TabIndex = 0;
			this.Media_TrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
			this.Media_TrackBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Media_TrackBar_Up);
			// 
			// Presentation_MovieControlPanel_Right
			// 
			this.Presentation_MovieControlPanel_Right.Controls.Add(this.AudioBar);
			this.Presentation_MovieControlPanel_Right.Dock = System.Windows.Forms.DockStyle.Right;
			this.Presentation_MovieControlPanel_Right.Location = new System.Drawing.Point(446, 0);
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
			// EditSongs2_Tab
			// 
			this.EditSongs2_Tab.Controls.Add(this.songEditor);
			this.EditSongs2_Tab.Location = new System.Drawing.Point(4, 14);
			this.EditSongs2_Tab.Name = "EditSongs2_Tab";
			this.EditSongs2_Tab.Size = new System.Drawing.Size(496, 691);
			this.EditSongs2_Tab.TabIndex = 6;
			this.EditSongs2_Tab.Text = "Edit Songs";
			// 
			// songEditor
			// 
			this.songEditor.Collections = new string[0];
			this.songEditor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.songEditor.Location = new System.Drawing.Point(0, 0);
			this.songEditor.Name = "songEditor";
			this.songEditor.Size = new System.Drawing.Size(496, 691);
			this.songEditor.TabIndex = 0;
			// 
			// BibleText_Tab
			// 
			this.BibleText_Tab.Controls.Add(this.BibleText_panelLeft);
			this.BibleText_Tab.Location = new System.Drawing.Point(4, 14);
			this.BibleText_Tab.Name = "BibleText_Tab";
			this.BibleText_Tab.Size = new System.Drawing.Size(496, 691);
			this.BibleText_Tab.TabIndex = 5;
			this.BibleText_Tab.Text = "Bible Text";
			// 
			// BibleText_panelLeft
			// 
			this.BibleText_panelLeft.Controls.Add(this.BibleText_Results);
			this.BibleText_panelLeft.Controls.Add(this.panel8);
			this.BibleText_panelLeft.Controls.Add(this.panel7);
			this.BibleText_panelLeft.Dock = System.Windows.Forms.DockStyle.Fill;
			this.BibleText_panelLeft.Location = new System.Drawing.Point(0, 0);
			this.BibleText_panelLeft.Name = "BibleText_panelLeft";
			this.BibleText_panelLeft.Size = new System.Drawing.Size(496, 691);
			this.BibleText_panelLeft.TabIndex = 0;
			// 
			// BibleText_Results
			// 
			this.BibleText_Results.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.BibleText_Results.CurrentVerse = 0;
			this.BibleText_Results.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.BibleText_Results.HiglightColor = Khendys.Controls.RtfColor.White;
			this.BibleText_Results.Location = new System.Drawing.Point(8, 80);
			this.BibleText_Results.Name = "BibleText_Results";
			this.BibleText_Results.ReadOnly = true;
			this.BibleText_Results.Size = new System.Drawing.Size(474, 602);
			this.BibleText_Results.TabIndex = 2;
			this.BibleText_Results.Text = "";
			this.BibleText_Results.TextColor = Khendys.Controls.RtfColor.Black;
			this.BibleText_Results.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BibleText_Results_KeyDown);
			this.BibleText_Results.MouseEnter += new System.EventHandler(this.BibleText_Results_MouseEnter);
			this.BibleText_Results.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BibleText_Results_MouseDown);
			// 
			// panel8
			// 
			this.panel8.BackColor = System.Drawing.SystemColors.Control;
			this.panel8.Controls.Add(this.BibleText_FindLast_button);
			this.panel8.Controls.Add(this.BibleText_FindFirst_button);
			this.panel8.Controls.Add(this.BibleText_FindPrev_button);
			this.panel8.Controls.Add(this.BibleText_FindNext_button);
			this.panel8.Controls.Add(this.BibleText_Verse_ComboBox);
			this.panel8.Controls.Add(this.label2);
			this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel8.Location = new System.Drawing.Point(0, 38);
			this.panel8.Name = "panel8";
			this.panel8.Size = new System.Drawing.Size(496, 36);
			this.panel8.TabIndex = 1;
			// 
			// BibleText_Verse_ComboBox
			// 
			this.BibleText_Verse_ComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.BibleText_Verse_ComboBox.DropDownWidth = 186;
			this.BibleText_Verse_ComboBox.Location = new System.Drawing.Point(56, 7);
			this.BibleText_Verse_ComboBox.Name = "BibleText_Verse_ComboBox";
			this.BibleText_Verse_ComboBox.Size = new System.Drawing.Size(264, 21);
			this.BibleText_Verse_ComboBox.TabIndex = 1;
			this.BibleText_Verse_ComboBox.SelectedIndexChanged += new System.EventHandler(this.BibleText_Verse_ComboBox_SelectedIndexChanged);
			this.BibleText_Verse_ComboBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.BibleText_Verse_ComboBox_KeyUp);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 11);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(40, 16);
			this.label2.TabIndex = 0;
			this.label2.Text = "Verse:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// panel7
			// 
			this.panel7.BackColor = System.Drawing.SystemColors.Control;
			this.panel7.Controls.Add(this.BibleText_Bookmark_button);
			this.panel7.Controls.Add(this.BibleText_RegEx_ComboBox);
			this.panel7.Controls.Add(this.label1);
			this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel7.Location = new System.Drawing.Point(0, 0);
			this.panel7.Name = "panel7";
			this.panel7.Size = new System.Drawing.Size(496, 38);
			this.panel7.TabIndex = 0;
			// 
			// BibleText_RegEx_ComboBox
			// 
			this.BibleText_RegEx_ComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.BibleText_RegEx_ComboBox.DropDownWidth = 250;
			this.BibleText_RegEx_ComboBox.Location = new System.Drawing.Point(56, 9);
			this.BibleText_RegEx_ComboBox.Name = "BibleText_RegEx_ComboBox";
			this.BibleText_RegEx_ComboBox.Size = new System.Drawing.Size(328, 21);
			this.BibleText_RegEx_ComboBox.TabIndex = 1;
			this.BibleText_RegEx_ComboBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.BibleText_RegEx_ComboBox_KeyUp);
			this.BibleText_RegEx_ComboBox.TextChanged += new System.EventHandler(this.BibleText_RegEx_ComboBox_TextChanged);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 10);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 24);
			this.label1.TabIndex = 0;
			this.label1.Text = "Search:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.ShowSong_Tab);
			this.tabControl1.Controls.Add(this.EditSongs2_Tab);
			this.tabControl1.Controls.Add(this.Presentation_Tab);
			this.tabControl1.Controls.Add(this.SermonTools_Tab);
			this.tabControl1.Controls.Add(this.BibleText_Tab);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
			this.tabControl1.ItemSize = new System.Drawing.Size(20, 10);
			this.tabControl1.Location = new System.Drawing.Point(72, 50);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(504, 709);
			this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			this.tabControl1.TabIndex = 2;
			// 
			// OpenFileDialog
			// 
			this.OpenFileDialog.AddExtension = false;
			// 
			// GlobalDataSet
			// 
			this.GlobalDataSet.DataSetName = "GlobalDataSet";
			this.GlobalDataSet.Locale = new System.Globalization.CultureInfo("en-US");
			this.GlobalDataSet.Tables.AddRange(new System.Data.DataTable[] {
            this.SongListTable});
			// 
			// SongListTable
			// 
			this.SongListTable.Columns.AddRange(new System.Data.DataColumn[] {
            this.NumberColumn,
            this.TitleColumn,
            this.CollectionColumn,
            this.FileNameColumn,
            this.NumberSortColumn,
            this.dataColumn1,
            this.dataColumn2});
			this.SongListTable.TableName = "SongListTable";
			// 
			// NumberColumn
			// 
			this.NumberColumn.ColumnName = "Number";
			// 
			// TitleColumn
			// 
			this.TitleColumn.ColumnName = "Title";
			// 
			// CollectionColumn
			// 
			this.CollectionColumn.ColumnName = "Collection";
			// 
			// FileNameColumn
			// 
			this.FileNameColumn.ColumnName = "FileName";
			// 
			// NumberSortColumn
			// 
			this.NumberSortColumn.ColumnName = "NumberSort";
			// 
			// dataColumn1
			// 
			this.dataColumn1.Caption = "FoldedTitle";
			this.dataColumn1.ColumnName = "FoldedTitle";
			// 
			// dataColumn2
			// 
			this.dataColumn2.Caption = "FoldedText";
			this.dataColumn2.ColumnName = "FoldedText";
			// 
			// SongListDataView
			// 
			this.SongListDataView.Table = this.SongListTable;
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(976, 781);
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
			this.KeyPreview = true;
			this.Location = new System.Drawing.Point(50, 0);
			this.Name = "MainForm";
			this.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Text = "DreamBeam";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.MainForm_Closing);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
			this.RightDocks_Songlist_SearchPanel.ResumeLayout(false);
			this.RightDocks_Songlist_SearchPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.RightDocks_SongList_ButtonPanel.ResumeLayout(false);
			this.RightDocks_TopPanel_PlayList_Button_Panel.ResumeLayout(false);
			this.ToolBars_leftSandBarDock.ResumeLayout(false);
			this.ToolBars_topSandBarDock.ResumeLayout(false);
			this.statusBar.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.StatusPanel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarUpdatePanel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.SongShow_CollapsPanel)).EndInit();
			this.SongShow_CollapsPanel.ResumeLayout(false);
			this.SongShow_HideElementsPanel.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.SongShow_HideElementsSub1Panel.ResumeLayout(false);
			this.BibleBookmarks_CollapsiblePanel.ResumeLayout(false);
			this.BibleTranslations_CollapsiblePanel.ResumeLayout(false);
			this.rightSandDock.ResumeLayout(false);
			this.RightDocks_BottomPanel_Media.ResumeLayout(false);
			this.RightDocks_BottomPanel_Media_Bottom.ResumeLayout(false);
			this.RightDocks_BottomPanel_Media_Top.ResumeLayout(false);
			this.Dock_BibleTools.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.BibleTools_CollapsiblePanelBar)).EndInit();
			this.BibleTools_CollapsiblePanelBar.ResumeLayout(false);
			this.RightDocks_BottomPanel_MediaLists.ResumeLayout(false);
			this.RightDocks_BottomPanel_MediaListsTopPanel.ResumeLayout(false);
			this.RightDocks_BottomPanel_MediaListsTop_Control_Panel.ResumeLayout(false);
			this.RightDocks_BottomPanel_MediaLists_BottomPanel.ResumeLayout(false);
			this.RightDocks_BottomPanel_MediaListsBottomPanel_GroupBox.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.RightDocks_BottomPanel_MediaLists_Numeric)).EndInit();
			this.RightDocks_BottomPanel_Backgrounds.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel6.ResumeLayout(false);
			this.RightDocks_BottomPanel2_TopPanel.ResumeLayout(false);
			this.RightDocks_TopPanel_Songs.ResumeLayout(false);
			this.RightDocks_TopPanel_PlayList.ResumeLayout(false);
			this.RightDocks_PreviewScreen.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.RightDocks_PreviewScreen_PictureBox)).EndInit();
			this.panel5.ResumeLayout(false);
			this.RightDocks_LiveScreen.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.RightDocks_LiveScreen_PictureBox)).EndInit();
			this.panel9.ResumeLayout(false);
			this.Dock_SongTools.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Main_ErrorProvider)).EndInit();
			this.ShowSong_Tab.ResumeLayout(false);
			this.SermonTools_Tab.ResumeLayout(false);
			this.Sermon_LeftPanel.ResumeLayout(false);
			this.Sermon_LeftDoc_Panel.ResumeLayout(false);
			this.Sermon_LeftToolBar_Panel.ResumeLayout(false);
			this.Sermon_LeftBottom_Panel.ResumeLayout(false);
			this.Sermon_TabControl.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			this.tabPage3.PerformLayout();
			this.Presentation_Tab.ResumeLayout(false);
			this.Presentation_FadePanel.ResumeLayout(false);
			this.Fade_panel.ResumeLayout(false);
			this.Fade_Top_Panel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Presentation_Fade_preview)).EndInit();
			this.Presentation_MainPanel.ResumeLayout(false);
			this.Presentation_PreviewPanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.axShockwaveFlash)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Presentation_PreviewBox)).EndInit();
			this.Presentation_MovieControlPanel.ResumeLayout(false);
			this.Presentation_MovieControlPanelBottom.ResumeLayout(false);
			this.Presentation_MovieControl_PreviewButtonPanel.ResumeLayout(false);
			this.Presentation_MovieControlPanelBottomLeft.ResumeLayout(false);
			this.Presentation_MovieControlPanelBottomLeft.PerformLayout();
			this.Presentation_MovieControlPanel_Top.ResumeLayout(false);
			this.Presentation_MovieControlPanel_Top.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.Media_TrackBar)).EndInit();
			this.Presentation_MovieControlPanel_Right.ResumeLayout(false);
			this.Presentation_MovieControlPanel_Right.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.AudioBar)).EndInit();
			this.EditSongs2_Tab.ResumeLayout(false);
			this.BibleText_Tab.ResumeLayout(false);
			this.BibleText_panelLeft.ResumeLayout(false);
			this.panel8.ResumeLayout(false);
			this.panel7.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.GlobalDataSet)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.SongListTable)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.SongListDataView)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		#region Methods and Events

		#region Inits

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


		#region Language
		private void SetLanguage() {

			#region Menu
			ToolBars_MenuBar_File_Exit.Text = Lang.say("Menu.Song.Exit");

			ToolBars_MenuBar_Song.Text = Lang.say("Menu.Song");
			ToolBars_MenuBar_Song_New.Text = Lang.say("Menu.Song.New");
			ToolBars_MenuBar_Song_Save.Text = Lang.say("Menu.Song.Save");
			ToolBars_MenuBar_Song_SaveAs.Text = Lang.say("Menu.Song.SaveAs");
			ToolBars_MenuBar_Song_Rename.Text = Lang.say("Menu.Song.Rename");

			ToolBars_MenuBar_MediaList.Text = Lang.say("Menu.MediaList");
			ToolBars_MenuBar_MediaList_New.Text = Lang.say("Menu.MediaList.New");
			ToolBars_MenuBar_Media_Save.Text = Lang.say("Menu.MediaList.Save");
			ToolBars_MenuBar_Media_SaveAs.Text = Lang.say("Menu.MediaList.SaveAs");
			ToolBars_MenuBar_Media_Rename.Text = Lang.say("Menu.MediaList.Rename");

			ToolBars_MenuBar_Edit.Text = Lang.say("Menu.Edit");
			ToolBars_MenuBar_Edit_Options.Text = Lang.say("Menu.Edit.Options");
			ToolBars_MenuBar_View.Text = Lang.say("Menu.View");

			ToolBars_MenuBar_Help.Text = Lang.say("Menu.Help");
			HelpIntro.Text = Lang.say("Menu.Help.Intro");
			AboutButton.Text = Lang.say("Menu.Help.About");
			#endregion

			#region Right
			//Songs
			RightDocks_TopPanel_Songs.Text = Lang.say("Right.Songs");
			btnRightDocks_SongListDelete.Text = Lang.say("Right.Songs.Delete");
			btnRightDocks_SongList2PlayList.Text = Lang.say("Right.Songs.Playlist");

			//Playlist
			RightDocks_TopPanel_PlayList.Text = Lang.say("Right.Playlist");
			RightDocks_PlayList_Load_Button.Text = Lang.say("Right.Playlist.Load");
			RightDocks_PlayList_Remove_Button.Text = Lang.say("Right.Playlist.Remove");
			RightDocks_PlayList_Up_Button.Text = Lang.say("Right.Playlist.Up");
			RightDocks_PlayList_Down_Button.Text = Lang.say("Right.Playlist.Down");

			// Displays
			RightDocks_PreviewScreen.Text = "Preview";
			RightDocks_LiveScreen.Text = "Live";

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
			RightDocks_BottomPanel_MediaList_BottomPanel_Label.Text = Lang.say("Right.MediaLists.TimerText");
			RightDocks_BottomPanel_MediaLists_BottomPanel_Label2.Text = Lang.say("Right.MediaLists.Seconds");
			RightDocks_BottomPanel_MediaLists_LoopCheckBox.Text = Lang.say("Right.MediaLists.Loop");

			#endregion

			#region Left
			ShowSongs_Button.ToolTipText = Lang.say("TabPages.ShowSongs");
			EditSongs_Button.ToolTipText = Lang.say("TabPages.EditSongs");
			Presentation_Button.ToolTipText = Lang.say("TabPages.Presentation");
			Sermon_Button.ToolTipText = Lang.say("TabPages.TextTool");
			#endregion

			#region TabPages
			ShowSong_Tab.Text = Lang.say("TabPages.ShowSongs");
			SermonTools_Tab.Text = Lang.say("TabPages.TextTool");
			tabPage3.Text = Lang.say("TabPages.Bible");
			Presentation_Tab.Text = Lang.say("TabPages.Presentation");
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
			this.SongShow_HideElementsPanel.TitleText = Lang.say("ShowSongs.HideElementsPanel");
			this.SongShow_HideTitle_Button.Text = Lang.say("ShowSongs.HideTitle_Button");
			this.SongShow_HideText_Button.Text = Lang.say("ShowSongs.HideText_Button");
			this.SongShow_HideAuthor_Button.Text = Lang.say("ShowSongs.HideAuthor_Button");
			#endregion

			#region MediaPresentation
			Presentation_MediaLoop_Checkbox.Text = Lang.say("MediaPresentation.Loop");
			Presentation_MoviePreviewButton.Text = Lang.say("MediaPresentation.Preview");
			#endregion

			#region TextTool
			Sermon_ToolBar_NewDoc_Button.Text = Lang.say("TextTool.NewDocument");

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

		}
		#endregion

		public void InitDisplays() {
			// This function is called on startup, and when the configuration changes

			// Delete all displays. We will re-create only the needed ones below.
			DisplayPreview = null;
			DisplayLiveMini = null;
			DisplayLiveLocal = null;
			DisplayLiveClient = null;
			DisplayLiveServer = null;

			// The OperatingMode could have changed we need to reconfigure the channels in case
			// we were previously configured as a server. A restart of the application is safer.
			foreach (IChannel c in ChannelServices.RegisteredChannels) {
				ChannelServices.UnregisterChannel(c);
			}

			XmlRpcServer.mainForm = this;

			DisplayPreview = new Display("DisplayPreview", this.Config);
			DisplayPreview.SetDestination(RightDocks_PreviewScreen_PictureBox);

			DisplayLiveMini = new Display("DisplayLiveMini", this.Config);
			DisplayLiveMini.SetDestination(RightDocks_LiveScreen_PictureBox);

			// In StandAlone mode we update the LiveMini only if the main screen is updated
			//		DisplayLiveLocal -> DisplayLiveMini
			// In Client mode we update the LiveMini only if the client proxy was successful
			// in sending the changes to the remote server (i.e. if remote screen was updated)
			//		DisplayLiveClient -> DisplayLiveMini
			// In Server mode, LiveServer receives the remote request, and responds with success
			// only if it successfully updated the LiveLocal display (which in turn first updates
			// the LiveMini display.
			//		DisplayLiveServer -> DisplayLiveLocal -> DisplayLiveMini
			switch (this.Config.AppOperatingMode) {
				case OperatingMode.StandAlone:
					DisplayLiveLocal = new Display("DisplayLiveLocal", this.Config);
					DisplayLiveLocal.SetDestination(ShowBeam);
					DisplayLiveLocal.NextDisplay = DisplayLiveMini;
					break;
				case OperatingMode.Client:
					DisplayLiveClient = new Display("DisplayLiveClient", this.Config);
					DisplayLiveClient.SetDestination(this.Config.ServerAddress);
					DisplayLiveClient.NextDisplay = DisplayLiveMini;
					break;
				case OperatingMode.Server:
					// Server mode has a local live display, just like StandAlone
					DisplayLiveLocal = new Display("DisplayLiveLocal", this.Config);
					DisplayLiveLocal.SetDestination(ShowBeam);
					DisplayLiveLocal.NextDisplay = DisplayLiveMini;
					// But also has a LiveServer display
					DisplayLiveServer = new Display("DisplayLiveServer", this.Config);
					DisplayLiveServer.SetDestination(this.Config.ListeningPort);
					DisplayLiveServer.NextDisplay = DisplayLiveLocal;
					break;
			}
		}


		#endregion

		#region Form Methods

		#region Save and Load Settings


		/// <summary>Saves Program Properties like BeamBox Position and Alphablending to the current ConfigSet. </summary>
		public void SaveSettings() {

			this.Config.PlayListString = "";
			foreach (NewSong song in this.Config.PlayList) {
				this.Config.PlayListString += song.FileName + "\n";
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

			//Presentation
			this.Config.LoopMedia = Presentation_MediaLoop_Checkbox.Checked;
			this.Config.AutoPlayChangeTime = (int)RightDocks_BottomPanel_MediaLists_Numeric.Value;
			this.Config.LoopAutoPlay = RightDocks_BottomPanel_MediaLists_LoopCheckBox.Checked;

			this.Config.AppDesktopLocation.Size = this.Size;
			this.Config.AppDesktopLocation.Location = this.Location;
		}

		/// <summary>Loads Program Properties like BeamBox Position and Alphablending. </summary>
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
			this.Sermon_BibleLang = this.Config.BibleLang;
			this.Sermon_ShowBibleTranslation = this.Config.ShowBibleTranslation;

			//Presentation
			Presentation_MediaLoop_Checkbox.Checked = Config.LoopMedia;
			RightDocks_BottomPanel_MediaLists_Numeric.Value = Config.AutoPlayChangeTime;
			RightDocks_BottomPanel_MediaLists_LoopCheckBox.Checked = Config.LoopAutoPlay;

			this.Config.PlayList.Clear();
			if (this.Config.PlayListString != null && this.Config.PlayListString.Length > 0) {
				foreach (string fileName in this.Config.PlayListString.Split('\n')) {
					try {
						NewSong song = NewSong.DeserializeFrom(fileName, 0, this.Config) as NewSong;
						if (song != null) this.Config.PlayList.Add(song);
					} catch { }
				}
			}
			this.RightDocks_PlayList_Reload();

			if (Config.PreRender)
				this.RenderStatus.Visible = true;
			else
				this.RenderStatus.Visible = false;

			Lang.setCulture(Config.Language);
			SetLanguage();

		}

		#endregion

		#region Get and Set Song Information

		///<summary>Reads all Songs in Directory, validates if it is a Song and put's them into the SongList_Tree </summary>
		public void ListSongs() {
			string[] songFiles;
			DataTable t = this.GlobalDataSet.Tables["SongListTable"];
			t.Clear();
			this.SongCollections.Clear();

			// We first load the relevant information from each song into the SongListTable of the GlobalDataSet
			try {
				songFiles = Directory.GetFiles(Tools.GetDirectory(DirType.Songs), "*.xml");
			} catch { return; }
			foreach (string songFile in songFiles) {
				NewSong song = NewSong.DeserializeFrom(songFile, 0, this.Config) as NewSong;
				if (song != null) {
					DataRow r = t.NewRow();
					r["Number"] = song.Number;
					r["FileName"] = song.FileName;

					if (!Tools.StringIsNullOrEmpty(song.Number)) {
						r["Title"] = song.Number + ". " + song.Title;
					} else {
						r["Title"] = song.Title;
					}

					if (Tools.StringIsNullOrEmpty(r["Title"].ToString()))
						r["Title"] = songFile;

					// Create FoldedTitle by folding (removing) diacritics
					// and removing anything that's not an ASCII letter (or
					// space) from the Title
					r["FoldedTitle"] = Regex.Replace(Tools.RemoveDiacritics(r["Title"] as string), @"[^a-zA-Z ]", "");

					// Create FoldedText by folding (removing) diacritics
					// and anything that's not an ASCII letter (or space)
					// from the lyrics.
					StringBuilder lyrics = new StringBuilder();
					foreach (LyricsItem l in song.SongLyrics) {
						lyrics.Append(l.Lyrics + " ");
					}
					r["FoldedText"] = Regex.Replace(Tools.RemoveDiacritics(lyrics.ToString()), @"[^a-zA-Z ]", "");

					if (!Tools.StringIsNullOrEmptyTrim(song.Collection)) {
						r["Collection"] = song.Collection;
					} else {
						r["Collection"] = "Unspecified Collection";
					}
					if (!this.SongCollections.ContainsKey(r["Collection"].ToString()))
						this.SongCollections.Add(r["Collection"].ToString());

					// DataView does not support custom sorting, so we convert numbers such as
					// "57a" into "0057 57a" so that we can sort strings numerically.
					if (!Tools.StringIsNullOrEmptyTrim(song.Number)) {
						r["NumberSort"] = string.Format("{0:0000} {1}",
							Convert.ToInt32(Regex.Replace(song.Number, @"[^\d]+", "")),
							song.Number);
					} else {
						r["NumberSort"] = song.Title;
					}

					t.Rows.Add(r);
				}
			}

			// The SongListDataView is the view for the SongListTable
			this.SongListDataView.Sort = "Collection, NumberSort, Title";

			// We data-bind the SongListDataView to the TreeView control, and group the data by Collection
			this.SongList_Tree.RemoveAllGroups();
			this.SongList_Tree.AddGroup("Collection", "Collection", "Collection", "Collection", 0, 0);
			this.SongList_Tree.SetLeafData("Title", "Title", "Title", 0, 0);
			this.SongList_Tree.DataSource = this.SongListDataView;
			if (this.SongCollections.Count > 1) {
				this.SongList_Tree.CollapseAll();
			} else {
				this.SongList_Tree.ExpandAll();
			}
			if (this.SongList_Tree.Nodes.Count > 0) this.SongList_Tree.Nodes[0].EnsureVisible();

			string[] collections = new string[this.SongCollections.Count];
			int i = 0;
			foreach (string collection in this.SongCollections.Keys) collections[i++] = collection.ToString();
			Array.Sort(collections);
			this.songEditor.Collections = collections;
		}

		/// <summary>Loads the song into the ListEx control</summary>
		/// <param name="song">The song to load</param>
		public void LoadSongShow(NewSong song) {
			this.SongShow_StropheList_ListEx.Items.Clear();
			foreach (LyricsSequenceItem item in song.Sequence) {
				this.SongShow_StropheList_ListEx.Add(song.GetLyrics(item), 0);
			}

			if (this.SongShow_StropheList_ListEx.Items.Count > 0) {
				SongShow_StropheList_ListEx.SelectedIndex = 0;
			}
		}

		// <summary>Puts the SongInformation into Windows Forms </summary>
		public void getSong() {
			try {
				// Edit Songs:

				// Show Songs:
				this.SongShow_StropheList_ListEx.Items.Clear();

				GuiTools.ChangeTitle();

			} catch { }

		}

		#endregion

		///<summary> Adjust Panel Sizes on Mainform Size changed </summary>
		private void MainForm_SizeChanged(object sender, System.EventArgs e) {
			this.GuiTools.Resize();
		}


		private void MainForm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
			//if (this.ErrorProvider_LastControl != null) this.Main_ErrorProvider.SetError(this.ErrorProvider_LastControl, "");
			GuiTools.KeyListner(e);
		}


		///<summarize> Initialize DreamBeam while MainForm is loading </summarize>
		private void MainForm_Load(object sender, System.EventArgs e) {

			this.Hide();
			Splash.SetStatus("Loading Configuration");
			this.LoadSettings();
			GuiTools.ShowTab(MainTab.ShowSongs);
			Splash.SetStatus("Reading Background Images");
			this.GuiTools.RightDock.BGImageTools.ListDirectories();

			//				this.ListImages(@"Backgrounds\");
			this.GuiTools.RightDock.BGImageTools.ListImages(Tools.GetDirectory(DirType.Backgrounds));
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
			Display.bibleLib = this.bibles;
			Display.config = this.Config;

			ShowBeam.Show();
		}

		///<summarize> start SaveSettings while MainForm is closing </summarize>
		private void MainForm_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			this.SaveSettings();

			// Save the SermonTool text so it can be restored on start-up
			SermonToolDocuments sermons = new SermonToolDocuments();
			if (Sermon_DocManager.TabStrips.Count > 0) {
				foreach (DocumentManager.Document d in Sermon_DocManager.TabStrips[0].Documents) {
					sermons.Documents.Add(d.Control.Text);
				}
			}
			SermonToolDocuments.SerializeTo(sermons, Tools.GetDirectory(DirType.Sermon, ConfigSet + ".SermonDocs.xml"));

			if (bibles.IsDirty) {
				bibles.SerializeNow(this.bibleLibFile);
			}

			if (this.Config.RememberPanelLocations) {
				this.GuiTools.SaveSandDockLayouts();
			}

			// This is only needed if we make changes to the conf programatically.
			Config.SerializeTo(this.Config, Tools.GetDirectory(DirType.Config, ConfigSet + ".config.xml"));
		}


		#endregion

		#region Menus, Toolbars and others

		#region Menu Bar

		#region File
		/// <summary>End Program from Menu</summary>
		private void ToolBars_MenuBar_File_Exit_Activate(object sender, System.EventArgs e) {
			this.Close();
		}
		#endregion

		#region Song

		///<summary> new Song from Menu</summary>
		private void ToolBars_MenuBar_Song_New_Activate(object sender, System.EventArgs e) {
			this.songEditor.Song = new NewSong();
		}

		/// <summary>Saves the Selected Song</summary>
		private void ToolBars_MenuBar_Song_Save_Activate(object sender, System.EventArgs e) {
			this.SaveSong();
		}


		///<summary> Save Song AS from Menu</summary>
		private void ToolBars_MenuBar_Song_SaveAs_Activate(object sender, System.EventArgs e) {
			this.SaveSongAs();
		}

		// TODO: Remove this function. Let users rename things using the OS.
		///<summary>Rename Song from File Menu</summary>
		private void ToolBars_MenuBar_Song_Rename_Activate(object sender, System.EventArgs e) {

			// TODO: Remove or update this to match the NewSong way of doing things.
			return;
			//					InputBoxResult result = InputBox.Show(Lang.say("Message.EnterSongName"), Lang.say("Message.RenameSongTitle",ShowBeam.Song.SongName),"", null);
			//					if (result.OK) {
			//						if (result.Text.Length > 0){
			//							if (!System.IO.File.Exists("Songs\\"+result.Text+".xml")) {
			//								System.IO.File.Move("Songs\\"+ShowBeam.Song.SongName+".xml", "Songs\\"+result.Text+".xml");
			//								ShowBeam.Song.SongName = result.Text;
			//								this.ListSongs();
			//								this.StatusPanel.Text = Lang.say("Status.SongRenamed",result.Text);
			//							}else{
			//								MessageBox.Show(Lang.say("Message.SongNotSavedYet"));
			//							}
			//						}else {
			//							MessageBox.Show(Lang.say("Message.SongNotRenamed"));
			//
			//						}
			//					}
		}

		#endregion

		#region MediaList

		///<summary>Rename MediaList from File Menu</summary>
		private void ToolBars_MenuBar_Media_Rename_Activate(object sender, System.EventArgs e) {
			InputBoxResult result = InputBox.Show(Lang.say("Message.MediaListName"), Lang.say("Message.RenameMediaListTitle", MediaList.Name), "", null);
			if (result.OK) {
				if (result.Text.Length > 0) {
					if (!File.Exists(Tools.GetDirectory(DirType.MediaLists, result.Text + ".xml"))
						&& File.Exists(Tools.GetDirectory(DirType.MediaLists, MediaList.Name + ".xml"))) {
						try {
							File.Move(Tools.GetDirectory(DirType.MediaLists, MediaList.Name + ".xml"),
								Tools.GetDirectory(DirType.MediaLists, result.Text + ".xml"));
							MediaList.Name = result.Text;
							this.ListMediaLists();

							string MediaFolder = Tools.GetDirectory(DirType.MediaFiles);
							//rename MediaFolder if existing
							if (File.Exists(MediaFolder + MediaList.Name)) {
								try {
									File.Move(MediaFolder + MediaList.Name, MediaFolder + result.Text);
								} catch { }
							}

							this.StatusPanel.Text = Lang.say("Status.MediaListRenamed", result.Text);
						} catch (Exception doh) { MessageBox.Show(doh.Message); }
					} else {
						MessageBox.Show(Lang.say("Message.MediaListNotSavedYet"));
					}
				} else {
					MessageBox.Show(Lang.say("Message.MediaListNotRenamed"));
				}
			}
		}


		/// <summary>Saves the MediaList after FileName Dialog</summary>
		private void ToolBars_MenuBar_Media_SaveAs_Activate(object sender, System.EventArgs e) {
			InputBoxResult result = InputBox.Show(Lang.say("Message.MediaListName"), Lang.say("Message.SaveMediaListAs"), "", null);
			if (result.OK) {
				bool save = false;
				if (result.Text.Length > 0) {
					if (!System.IO.File.Exists("MediaLists\\" + result.Text + ".xml")) {
						save = true;
					} else {
						MessageBox.Show(Lang.say("Message.MediaListExits"));
					}
				} else {
					MessageBox.Show(Lang.say("Message.MediaListNotSaved"));
				}
				if (save) {
					MediaList.Name = result.Text;
					MediaList.Save();
					this.ListMediaLists();
					this.StatusPanel.Text = Lang.say("Status.MediaListSavedAs", result.Text);
				}
			}
		}


		private void ToolBars_MenuBar_Media_Save_Activate(object sender, System.EventArgs e) {
			SaveMediaList();
		}


		/// <summary>Starts a new MediaList</summary>
		private void ToolBars_MenuBar_MediaList_New_Activate(object sender, System.EventArgs e) {
			// get MediaList Name
			InputBoxResult result = InputBox.Show(Lang.say("Message.MediaListName"), Lang.say("Message.NewMediaListTitle"), "", null);
			if (result.OK) {
				bool boolnew = false;
				if (result.Text.Length > 0) {
					if (!System.IO.File.Exists("MediaLists\\" + result.Text + ".xml")) {
						boolnew = true;
					} else {
						MessageBox.Show(Lang.say("Message.MediaListExits"));
					}
				} else {
					MessageBox.Show(Lang.say("Message.NoNameEntered"));
				}
				// create New MediaList
				if (boolnew) {
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
			LoadSettings();
		}
		#endregion

		#region View
		private void ToolBars_MenuBar_View_ShowSongs_Activate(object sender, System.EventArgs e) {
			this.GuiTools.ShowTab(MainTab.ShowSongs);
		}

		private void ToolBars_MenuBar_View_EditSongs_Activate(object sender, System.EventArgs e) {
			this.GuiTools.ShowTab(MainTab.EditSongs);
		}

		private void ToolBars_MenuBar_View_Presentation_Activate(object sender, System.EventArgs e) {
			this.GuiTools.ShowTab(MainTab.Presentation);
		}

		private void ToolBars_MenuBar_View_TextTool_Activate(object sender, System.EventArgs e) {
			this.GuiTools.ShowTab(MainTab.SermonTools);
		}

		private void ToolBars_MenuBar_View_BibleText_Activate(object sender, System.EventArgs e) {
			this.GuiTools.ShowTab(MainTab.BibleText);
		}

		#region Open SandDock panels
		private void ToolBars_MenuBar_Open_PreviewTab_Activate(object sender, System.EventArgs e) {
			this.RightDocks_PreviewScreen.Open();
		}

		private void ToolBars_MenuBar_Open_LiveTab_Activate(object sender, System.EventArgs e) {
			this.RightDocks_LiveScreen.Open();
		}

		private void ToolBars_MenuBar_Open_BackgroundsTab_Activate(object sender, System.EventArgs e) {
			this.RightDocks_BottomPanel_Backgrounds.Open();
		}

		private void ToolBars_MenuBar_Open_MediaTab_Activate(object sender, System.EventArgs e) {
			this.RightDocks_BottomPanel_Media.Open();
		}

		private void ToolBars_MenuBar_Open_PlaylistTab_Activate(object sender, System.EventArgs e) {
			this.RightDocks_TopPanel_PlayList.Open();
		}

		private void ToolBars_MenuBar_Open_SongsTab_Activate(object sender, System.EventArgs e) {
			this.RightDocks_TopPanel_Songs.Open();
		}

		private void ToolBars_MenuBar_Open_MediaListTab_Activate(object sender, System.EventArgs e) {
			this.RightDocks_BottomPanel_MediaLists.Open();
		}

		private void ToolBars_MenuBar_Open_BibleToolsTab_Activate(object sender, System.EventArgs e) {
			this.Dock_BibleTools.Open();
		}

		private void ToolBars_MenuBar_Open_SongToolsTab_Activate(object sender, System.EventArgs e) {
			this.Dock_SongTools.Open();
		}
		#endregion

		#endregion

		#region Help
		private void HelpIntro_Activate(object sender, System.EventArgs e) {
			string helpFile = Tools.CombinePaths(Application.StartupPath, "Help", "DreamBeam.html");
			if (Tools.FileExists(helpFile)) {
				System.Diagnostics.Process.Start(helpFile);
			}
		}

		private void AboutButton_Activate(object sender, System.EventArgs e) {
			About about = new About();
			about.version = this.version;
			about.ShowDialog();
		}

		#endregion

		#endregion

		#region Main Toolbar
		private void ToolBars_MainToolbar_ButtonClick(object sender, TD.SandBar.ToolBarItemEventArgs e) {

			// SHOW BEAMBOX
			if (e.Item == ToolBars_MainToolbar_ShowBeamBox) {
				if (ToolBars_MainToolbar_ShowBeamBox.Checked) {
					ToolBars_MainToolbar_ShowBeamBox.Checked = false;
					ShowBeam.Hide();
				} else {
					ToolBars_MainToolbar_ShowBeamBox.Checked = true;
					ShowBeam.Show();
				}
			}

			// Size / position
			if (e.Item == ToolBars_MainToolbar_SizePosition) {
				if (ToolBars_MainToolbar_SizePosition.Checked) {
					ToolBars_MainToolbar_SizePosition.Checked = false;
					ShowBeam.HideMover();
				} else {
					ToolBars_MainToolbar_SizePosition.Checked = true;
					ShowBeam.ShowMover();
				}
			}

			// HIDE BG
			if (e.Item == ToolBars_MainToolbar_HideBG) {
				this.GuiTools.ShowBeamTools.HideBg();
				// TODO: Make this work in all AppOperatingModes
				if (this.DisplayLiveLocal.content != null) {
					(this.DisplayLiveLocal.content as Content).HideBG =
						this.ToolBars_MainToolbar_HideBG.Checked;
					this.DisplayLiveLocal.UpdateDisplay(true);
				}
			}

			// HIDE TEXT
			if (e.Item == ToolBars_MainToolbar_HideText) {
				this.ToolBars_MainToolbar_HideText.Checked = !this.ToolBars_MainToolbar_HideText.Checked;
				if (this.Config.AppOperatingMode == OperatingMode.Client) {
					this.DisplayLiveClient.HideText(this.ToolBars_MainToolbar_HideText.Checked);
				} else {
					this.DisplayLiveLocal.HideText(this.ToolBars_MainToolbar_HideText.Checked);
				}
			}

			//SAVE SONG
			if (e.Item == ToolBars_MainToolbar_SaveSong) {
				SaveSong();
			}

			//SAVE MEDIALIST
			if (e.Item == ToolBars_MainToolbar_SaveMediaList) {
				SaveMediaList();
			}
		}

		void SaveSong() {
			if (Tools.FileExists(this.songEditor.Song.FileName)) {
				NewSong.SerializeTo(this.songEditor.Song, this.songEditor.Song.FileName);
				this.ListSongs();
				this.StatusPanel.Text = Lang.say("Status.SongSavedAs", this.songEditor.Song.FileName);
			} else {
				SaveSongAs();
			}
		}

		void SaveSongAs() {
			this.SaveFileDialog.DefaultExt = "xml";
			this.SaveFileDialog.Filter = @"DreamBeam songs (*.xml)|*.xml|All (*.*)|*.*";
			this.SaveFileDialog.FilterIndex = 1;
			this.SaveFileDialog.InitialDirectory = Tools.GetDirectory(DirType.Songs);
			this.SaveFileDialog.Title = "Save Song As";

			if (!Tools.StringIsNullOrEmptyTrim(this.songEditor.Song.FileName)) {
				this.SaveFileDialog.FileName = this.songEditor.Song.FileName;
			} else if (!Tools.StringIsNullOrEmptyTrim(this.songEditor.Song.Title)) {
				this.SaveFileDialog.FileName = this.songEditor.Song.Title + ".xml";
			} else {
				this.SaveFileDialog.FileName = "New Song.xml";
			}

			if (this.SaveFileDialog.ShowDialog() == DialogResult.OK) {
				this.songEditor.Song.FileName = this.SaveFileDialog.FileName;
				try {
					NewSong.SerializeTo(this.songEditor.Song, this.SaveFileDialog.FileName);
					this.StatusPanel.Text = Lang.say("Status.SongSavedAs", this.SaveFileDialog.FileName);
				} catch (Exception ex) {
					MessageBox.Show(Lang.say("Message.SongNotSaved") + "\nReason: " + ex.Message);
				}
				this.ListSongs();
			} else {
				MessageBox.Show(Lang.say("Message.SongNotSaved"));
			}
		}
		#endregion

		#region Component Bar

		///<summarize> Switch Components on Click </summarize>
		private void ToolBars_ComponentBar_ButtonClick(object sender, TD.SandBar.ToolBarItemEventArgs e) {
			foreach (TD.SandBar.ButtonItem button in ToolBars_ComponentBar.Buttons) {
				button.Checked = false;
			}

			(e.Item as TD.SandBar.ButtonItem).Checked = true;

			if (e.Item == ShowSongs_Button) {
				GuiTools.ShowTab(MainTab.ShowSongs);
				this.SongShow_HideElement_UpdateButtons();
			} else if (e.Item == EditSongs_Button) {
				GuiTools.ShowTab(MainTab.EditSongs);
			} else if (e.Item == Presentation_Button) {
				GuiTools.ShowTab(MainTab.Presentation);
				this.Presentation_Resize();
			} else if (e.Item == Sermon_Button) {
				GuiTools.ShowTab(MainTab.SermonTools);
			} else if (e.Item == BibleText_Button) {
				GuiTools.ShowTab(MainTab.BibleText);
				BibleText_RegEx_ComboBox.Focus();
			}

		}
		#endregion

		#region Right Docks

		#region SongList

		private void SongList_Tree_DoubleClick(object sender, System.EventArgs e) {
			this.RightDocks_Preview_GoLive_Click(sender, e);
			this.SongShow_HideElement_UpdateButtons();
		}

		private void SongList_Tree_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e) {
			//ControlLib.dbTreeNode node = (ControlLib.dbTreeNode) e.Node;
			this.LoadSongFromFile(GetSelectedSongFileName());
		}

		private string GetSelectedSongFileName() {
			ControlLib.dbTreeNode node = this.SongList_Tree.SelectedNode as ControlLib.dbTreeNode;

			if (node == null || node.Nodes.Count != 0) return null; // We only want leaf nodes
			DataRowView rv = node.Item as DataRowView;
			DataRow r = rv.Row;

			if (r != null) {
				return r["FileName"].ToString();
			} else {
				return null;
			}
		}

		private void LoadSongFromFile(string FileName) {
			if (FileName == null) return;
			NewSong song = (NewSong)NewSong.DeserializeFrom(FileName, 0, this.Config);
			this.songEditor.Song = song;
			DisplayPreview.SetContent(song);
			this.LoadSongShow(DisplayPreview.content as NewSong);
			this.SongShow_HideElement_UpdateButtons();
			this.StatusPanel.Text = Lang.say("Status.SongLoaded", FileName);
			GC.Collect();
			song.PreRenderFrames();
		}

		///<summary> Delete Song in List </summary>
		private void btnRightDocks_SongListDelete_Click(object sender, System.EventArgs e) {
			string FileName = GetSelectedSongFileName();
			if (!Tools.FileExists(FileName)) return;

			DialogResult answer = MessageBox.Show(
				Lang.say("Message.WantDeleteSong", FileName),
				Lang.say("Message.DeleteSongTitle"), MessageBoxButtons.YesNo);

			if (answer == DialogResult.Yes) {
				System.IO.File.Delete(FileName);
				this.ListSongs();
				this.StatusPanel.Text = Lang.say("Status.SongDeleted", FileName);
			}
		}

		///<summary> Add a Song to a PlayList</summary>
		private void btnRightDocks_SongList2PlayList_Click(object sender, System.EventArgs e) {
			NewSong song;
			string FileName = GetSelectedSongFileName();

			try {
				song = NewSong.DeserializeFrom(FileName, 0, this.Config) as NewSong;
			} catch { return; }

			if (song == null) return;
			this.Config.PlayList.Add(song);

			this.RightDocks_PlayList_Reload();
			this.SaveSettings();
			this.StatusPanel.Text = Lang.say("Status.SongToPlayList", song.FullName);
		}

		///<summary> Search Songlist for entered Characters</summary>
		private void RightDocks_SongListSearch_TextChanged(object sender, System.EventArgs e) {
			string searchFor = this.RightDocks_SongListSearch.Text.Trim();

			if (searchFor.Length > 0) {
				// We try to do some intelligent searching.
				// TODO: Treat words in quotes as phrases
				if (Regex.IsMatch(searchFor, @"^\d+")) {
					// The user is searching using a song number
					this.SongListDataView.RowFilter = "Number LIKE '" + searchFor + "%'";
				} else {
					//this.SongListDataView.RowFilter = "Title LIKE '%" + searchFor + "%' OR FoldedTitle LIKE '%" + searchFor + "%'";
					this.SongListDataView.RowFilter =
						"Title LIKE '%" + searchFor + "%' " +
						"OR FoldedTitle LIKE '%" + searchFor + "%' " +
						"OR FoldedText LIKE '%" + searchFor + "%' ";
				}
			} else {
				this.SongListDataView.RowFilter = "";
			}

			this.SongList_Tree.BuildTree();
			if (this.SongListDataView.RowFilter.Length == 0) {
				this.SongList_Tree.CollapseAll();
			} else {
				this.SongList_Tree.ExpandAll();
			}
			if (this.SongList_Tree.Nodes.Count > 0) this.SongList_Tree.Nodes[0].EnsureVisible();
		}

		#endregion

		#region PlayList

		private void RightDocks_PlayList_Reload() {
			// Why do we have to set it to null first?
			this.RightDocks_PlayList.DataSource = null;
			this.RightDocks_PlayList.Items.Clear();
			if (this.Config.PlayList.Count > 0) {
				this.RightDocks_PlayList.DataSource = this.Config.PlayList;
				this.RightDocks_PlayList.DisplayMember = "FullName";
				this.RightDocks_PlayList.ValueMember = "FileName";
			}
		}

		/// <summary>Load Song from Playlist on DoubleClick </summary>
		private void RightDocks_PlayList_DoubleClick(object sender, System.EventArgs e) {
			this.RightDocks_Preview_GoLive_Click(sender, e);
			this.SongShow_HideElement_UpdateButtons();
		}

		private void RightDocks_PlayList_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e) {
			try {
				int i = this.RightDocks_PlayList.SelectedIndex;
				if (i >= 0 && i < this.Config.PlayList.Count) {
					this.LoadSongFromFile((this.Config.PlayList[i] as NewSong).FileName);
				}
			} catch (Exception ex) {
				Console.WriteLine("PlayList_MouseUp exception: " + ex.Message);
			}
		}
		/// <summary>Remove Selected Playlistitem on Click</summary>
		private void RightDocks_PlayList_Remove_Button_Click(object sender, System.EventArgs e) {
			int index = this.RightDocks_PlayList.SelectedIndex;
			if (index >= 0 && index < this.Config.PlayList.Count) {
				this.RightDocks_PlayList.SetSelected(index, false);	// If we don't do this, BAD things happen
				this.StatusPanel.Text = Lang.say("Status.PlaylistSongRemoved", (this.Config.PlayList[index] as NewSong).FullName);
				this.Config.PlayList.RemoveAt(index);
				this.SaveSettings();
				this.RightDocks_PlayList_Reload();
				if (this.RightDocks_PlayList.Items.Count > index) {
					this.RightDocks_PlayList.SelectedIndex = index;
				} else if (index > 0) { // We removed the bottom item
					this.RightDocks_PlayList.SelectedIndex = index - 1;
				}
			}
		}

		/// <summary>Move selected PlayList Item up, on click </summary>
		private void RightDocks_PlayList_Up_Button_Click(object sender, System.EventArgs e) {
			int i = this.RightDocks_PlayList.SelectedIndex;
			if (i > 0 && i < this.Config.PlayList.Count) {
				object o = this.Config.PlayList[i - 1];
				this.Config.PlayList[i - 1] = this.Config.PlayList[i];
				this.Config.PlayList[i] = o;
				this.RightDocks_PlayList.SelectedIndex = i - 1;
				this.RightDocks_PlayList_Reload();
				this.SaveSettings();
			}
		}

		/// <summary>Move selected PlayList Item down, on click </summary>
		private void RightDocks_PlayList_Down_Button_Click(object sender, System.EventArgs e) {
			int i = this.RightDocks_PlayList.SelectedIndex;
			if (i < this.Config.PlayList.Count - 1) {
				object o = this.Config.PlayList[i + 1];
				this.Config.PlayList[i + 1] = this.Config.PlayList[i];
				this.Config.PlayList[i] = o;
				this.RightDocks_PlayList.SelectedIndex = i + 1;
				this.RightDocks_PlayList_Reload();
				this.SaveSettings();
			}
		}

		#endregion

		#region Background

		private void RightDocks_FolderDropdown_SelectionChangeCommitted(object sender, System.EventArgs e) {
			this.GuiTools.RightDock.BGImageTools.SelectionChangeCommitted(sender, e);
		}

		private void RightDocks_ImageListBox_SelectedIndexChanged(object sender, System.EventArgs e) {
			this.GuiTools.RightDock.BGImageTools.SelectedIndexChanged(sender, e);
		}

		private void RightDocks_ImageListBox_DragEnter(object sender, System.Windows.Forms.DragEventArgs e) {
			this.GuiTools.RightDock.BGImageTools.DragEnter(sender, e);
		}

		private void RightDocks_ImageListBox_DragDrop(object sender, System.Windows.Forms.DragEventArgs e) {
			this.GuiTools.RightDock.BGImageTools.DragDrop(sender, e);
		}

		#endregion

		#endregion

		#endregion

		#region SHOW Songs

		private void SongShow_StropheList_ListEx_PressIcon(int Index) {
			RightDocks_Preview_GoLive_Click(null, null);
		}

		private void SongShow_StropheList_ListEx_SelectedIndexChanged(object sender, System.EventArgs e) {
			// Update the preview screen when the index changes
			NewSong s = DisplayPreview.content as NewSong;
			if (s != null) {
				s.CurrentLyric = this.SongShow_StropheList_ListEx.SelectedIndex;
				DisplayPreview.UpdateDisplay(false);
			} else if (this.SongShow_StropheList_ListEx.SelectedIndex >= 0) {
				// If we come back from BibleText to the song,
				// DisplayPreview.content will not be a NewSong and the
				// typecasting above will give us a null, so we need to re-load
				// the song.
				string FileName = this.songEditor.Song.FileName;

				// We can't use GetSelectedSongFileName because the song could
				// have been loaded from the PlayList, or could no longer be
				// selected (if the user selected a Collection after selecting
				// the song).

				//string FileName = GetSelectedSongFileName();
				if (Tools.FileExists(FileName)) {
					NewSong song = NewSong.DeserializeFrom(FileName, this.SongShow_StropheList_ListEx.SelectedIndex, this.Config) as NewSong;
					DisplayPreview.SetContent(song);
					this.SongShow_HideElement_UpdateButtons();
				}
				GC.Collect();
			}
		}

		//		///<summary>Redraw Panel 8 on Click </summary>
		//		private void SongShow_Preview_Panel_Click(object sender, System.EventArgs e){
		//
		//			Draw_Song_Preview_Image_Threaded();
		//		}



		#endregion

		#region EDIT Songs

		///<summary></summary>
		private void TextTypedTimer_Tick(object sender, System.EventArgs e) {
		}

		#endregion

		#region Presentation


		#region Preview & Control Window


		#region Control Window
		/// <summary>Wipe Movie Control Panel in and out</summary>
		private void MovieControlPanelWipe(string direction) {
			if (direction == "in") {
				Media_TrackBar.Enabled = false;
				Presentation_MoviePreviewButton.Enabled = false;
				Presentation_MoviePreviewButton.Enabled = false;
				Presentation_PlayButton.Enabled = false;
				Presentation_PauseButton.Enabled = false;
				Presentation_StopButton.Enabled = false;
				if (Presentation_MovieControlPanel.Size.Height != 0) {
					Presentation_MovieControlPanel.Size = new System.Drawing.Size(Presentation_MovieControlPanel.Size.Width, 0);
				}
			} else {
				Media_TrackBar.Enabled = true;
				Presentation_MoviePreviewButton.Enabled = true;
				Presentation_PlayButton.Enabled = true;
				Presentation_MoviePreviewButton.Enabled = true;
				if (Presentation_MovieControlPanel.Size.Height != 60) {
					Presentation_MovieControlPanel.Size = new System.Drawing.Size(Presentation_MovieControlPanel.Size.Width, 60);
				}
			}
		}


		/// <summary>on Progress-Timer Tick, set mediaTrackbar to MediaPlayPosition</summary>
		private void PlayProgress_Tick(object sender, System.EventArgs e) {
			if (this.ShowBeam.strMediaPlaying == null) {
				if (MediaPreview) {
					if (MediaList.GetType(MediaFile) == "flash") {
						Media_TrackBar.Value = axShockwaveFlash.FrameNum;
					}
					if (MediaList.GetType(MediaFile) == "movie") {
						try {
							Media_TrackBar.Maximum = (int)video.Duration;
							Media_TrackBar.Value = (int)video.CurrentPosition;
						} catch { }
					}
				}
			} else if (this.ShowBeam.strMediaPlaying == "flash") {
				Media_TrackBar.Value = this.ShowBeam.axShockwaveFlash.FrameNum;
			} else if (this.ShowBeam.strMediaPlaying == "movie") {
				try {
					Media_TrackBar.Maximum = (int)this.ShowBeam.video.Duration;
					Media_TrackBar.Value = (int)this.ShowBeam.video.CurrentPosition;
				} catch { }
			}
		}


		/// <summary>On Mouse Release, set Media Position to Trackbar Position</summary>
		private void Media_TrackBar_Up(object sender, System.Windows.Forms.MouseEventArgs e) {
			if (this.ShowBeam.strMediaPlaying == null) {
				string MediaName = this.MediaList.iItem[RightDocks_BottomPanel_MediaList.SelectedIndex].Path;
				if (MediaList.GetType(MediaName) == "flash") {
					axShockwaveFlash.GotoFrame(Media_TrackBar.Value);
				}
				if (MediaList.GetType(MediaName) == "movie") {
					video.CurrentPosition = (double)Media_TrackBar.Value;
				}
			} else if (this.ShowBeam.strMediaPlaying == "flash") {
				this.ShowBeam.axShockwaveFlash.GotoFrame(Media_TrackBar.Value);
			} else if (this.ShowBeam.strMediaPlaying == "movie") {
				this.ShowBeam.video.CurrentPosition = (double)Media_TrackBar.Value;
			}
		}


		/// <summary>Adjust Volume on Volume Trackbar change</summary>
		private void AudioBar_ValueChanged(object sender, System.EventArgs e) {
			if (this.ShowBeam.video != null) {
				try {
					this.ShowBeam.video.Audio.Volume = AudioBar.Value;
				} catch { }
			}
		}

		/// <summary>On Click, Play, Stop or Pause Media</summary>
		private void Presentation_PlayBar_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e) {
			string MediaName = this.MediaList.iItem[RightDocks_BottomPanel_MediaList.SelectedIndex].Path;
			if (e.Button == Presentation_PlayButton) {
				this.Media2BeamBox();
			}
			if (e.Button == Presentation_PauseButton) {
				PlayProgress.Enabled = false;
				Presentation_PauseButton.Enabled = false;
				Presentation_PlayButton.Enabled = true;
				this.ShowBeam.PauseMedia();
			}
			if (e.Button == Presentation_StopButton) {
				PlayProgress.Enabled = false;
				Presentation_StopButton.Enabled = false;
				Presentation_PauseButton.Enabled = false;
				Presentation_PlayButton.Enabled = true;
				ShowBeam.StopMedia();
			}
		}


		/// <summary>If Loop Media Checkbox changed, copy it's value to showbeam</summary>
		private void Presentation_MediaLoop_Checkbox_CheckedChanged(object sender, System.EventArgs e) {
			ShowBeam.LoopMedia = Presentation_MediaLoop_Checkbox.Checked;
			ShowBeam.axShockwaveFlash.Loop = Presentation_MediaLoop_Checkbox.Checked;
		}
		#endregion

		/// <summary>Starts and Stop's the local Media Preview</summary>
		private void Presentation_MoviePreviewButton_Click(object sender, System.EventArgs e) {
			if (!MediaPreview) {
				if (MediaList.GetType(MediaFile) == "flash") {
					try {
						Media_TrackBar.Maximum = axShockwaveFlash.TotalFrames;
						PlayProgress.Enabled = true;
						axShockwaveFlash.Play();
					} catch { MessageBox.Show("Can not play this Flash File"); }
				}
				if (MediaList.GetType(MediaFile) == "movie") {
					Media_TrackBar.Maximum = (int)video.Duration;
					PlayProgress.Enabled = true;
					try {
						video.Audio.Volume = -10000;
					} catch { }
					Thread.Sleep(1000);
					this.video.Play();
				}
				MediaPreview = true;
			} else {
				if (MediaList.GetType(MediaFile) == "flash") {
					axShockwaveFlash.Stop();
					axShockwaveFlash.Back();
					PlayProgress.Enabled = false;
				}
				if (MediaList.GetType(MediaFile) == "movie") {
					this.video.Stop();
				}
				MediaPreview = false;
			}
		}
		#endregion


		#region RightDock MediaList Panels

		private void RightDocks_BottomPanel_MediaList_DragDrop(object sender, System.Windows.Forms.DragEventArgs e) {
			this.GuiTools.RightDock.MediaListTools.DragDrop(sender, e);
		}

		private void RightDocks_BottomPanel_MediaList_DragEnter(object sender, System.Windows.Forms.DragEventArgs e) {
			this.GuiTools.RightDock.MediaListTools.DragEnter(sender, e);
		}

		private void RightDocks_BottomPanel_Media_Remove_Click(object sender, System.EventArgs e) {
			this.GuiTools.RightDock.MediaListTools.Media_Remove_Click(sender, e);
		}

		private void RightDocks_BottomPanel_Media_Up_Click(object sender, System.EventArgs e) {
			this.GuiTools.RightDock.MediaListTools.Media_Up_Click(sender, e);
		}

		private void RightDocks_BottomPanel_Media_Down_Click(object sender, System.EventArgs e) {
			this.GuiTools.RightDock.MediaListTools.Media_Down_Click(sender, e);
		}

		/// <summary>start Media Autoplay on Click</summary>
		private void RightDocks_BottomPanel_Media_AutoPlay_Click(object sender, System.EventArgs e) {
			if (!this.Presentation_AutoPlayTimer.Enabled) {
				this.Presentation_AutoPlayTimer.Interval = (int)RightDocks_BottomPanel_MediaLists_Numeric.Value * 1000;
				this.Presentation_AutoPlayTimer.Enabled = true;
				RightDocks_BottomPanel_Media_AutoPlay.Text = "Stop";
				this.ShowBeam.axShockwaveFlash.Loop = false;
				Presentation_MediaLoop_Checkbox.Checked = false;
			} else {
				this.Presentation_AutoPlayTimer.Enabled = false;
				RightDocks_BottomPanel_Media_AutoPlay.Text = "Auto Play";
			}
		}

		/// <summary>On Change, copy Numeric-Value to AutoPlayTimer </summary>
		private void RightDocks_BottomPanel_MediaLists_Numeric_ValueChanged(object sender, System.EventArgs e) {
			this.Presentation_AutoPlayTimer.Interval = (int)RightDocks_BottomPanel_MediaLists_Numeric.Value * 1000;
		}


		/// <summary>On Media ListBox doubleclick run Media2Beambox function</summary>
		private void RightDocks_BottomPanel_MediaList_DoubleClick(object sender, System.EventArgs e) {
			this.Media2BeamBox();
		}


		/// <summary> if Preview Video Loaded, Load and show the Video</summary>
		private void VideoLoadTimer_Tick(object sender, System.EventArgs e) {
			if (this.VideoLoaded) {
				VideoLoadTimer.Enabled = false;
				this.VideoLoaded = false;
				try {
					Media_TrackBar.Maximum = (int)video.Duration;
				} catch { }
				PlayProgress.Enabled = true;
				ShowBeam.ShowMedia(MediaFile);
			}
		}


		/// <summary>Show selected Media and select next Item</summary>
		private void RightDocks_BottomPanel_Media_ShowNext_Click(object sender, System.EventArgs e) {
			if (this.RightDocks_BottomPanel_MediaList.Items.Count != -1) {
				this.Media2BeamBox();
				int tmp = this.RightDocks_BottomPanel_MediaList.SelectedIndex;
				if (tmp < this.RightDocks_BottomPanel_MediaList.Items.Count - 1) {
					this.RightDocks_BottomPanel_MediaList.SelectedIndex = tmp + 1;
				}
			}
		}

		/// <summary>On tick Show Media and select next</summary>
		private void Presentation_AutoPlayTimer_Tick(object sender, System.EventArgs e) {
			if (this.RightDocks_BottomPanel_MediaList.SelectedIndex == -1 && this.RightDocks_BottomPanel_MediaList.Items.Count > 0) {
				this.RightDocks_BottomPanel_MediaList.SelectedIndex = 0;
			}
			if (ShowBeam.strMediaPlaying == "movie") {
				iFilmEnded = 1;
				if (this.ShowBeam.video.CurrentPosition == this.ShowBeam.video.Duration) {
					iFilmEnded = 0;
				}
			}
			if (ShowBeam.strMediaPlaying == "flash") {
				this.StatusPanel.Text = this.ShowBeam.axShockwaveFlash.CurrentFrame().ToString() + " / " + this.ShowBeam.axShockwaveFlash.TotalFrames.ToString();
				if (iFilmEnded == 0) {
					iFilmEnded = this.ShowBeam.axShockwaveFlash.CurrentFrame();
					if (this.ShowBeam.axShockwaveFlash.CurrentFrame() == this.ShowBeam.axShockwaveFlash.TotalFrames - 1) {
						iFilmEnded = 0;
					}
				} else {
					if (this.ShowBeam.axShockwaveFlash.CurrentFrame() == iFilmEnded) {
						iFilmEnded = 0;
					} else {
						iFilmEnded = ShowBeam.axShockwaveFlash.CurrentFrame();
					}
				}
			}
			if (this.RightDocks_BottomPanel_MediaList.SelectedIndex != -1 && iFilmEnded == 0) {
				this.Media2BeamBox();
				int tmp = this.RightDocks_BottomPanel_MediaList.SelectedIndex;
				if (tmp < this.RightDocks_BottomPanel_MediaList.Items.Count - 1) {
					this.RightDocks_BottomPanel_MediaList.SelectedIndex = tmp + 1;
				} else if (RightDocks_BottomPanel_MediaLists_LoopCheckBox.Checked) {
					this.RightDocks_BottomPanel_MediaList.SelectedIndex = 0;
				}
			}
		}

		private void Presentation_Fade_ListView_Click(object sender, System.EventArgs e) {
			this.GuiTools.Presentation.ListView_Click(sender, e);
		}

		private void Presentation_Fade_ToPlaylist_Button_Click(object sender, System.EventArgs e) {
			this.GuiTools.Presentation.Fade_ToPlaylist_Button_Click(sender, e);
		}
		#endregion


		#region MediaList
		private void RightDocks_BottomPanel_Media_FadePanelButton_Click(object sender, System.EventArgs e) {
			if (Presentation_FadePanel.Size.Width == 510) {
				Presentation_FadePanel.Size = new System.Drawing.Size(0, Presentation_FadePanel.Size.Height);
				RightDocks_BottomPanel_Media_FadePanelButton.Text = Lang.say("Right.Media.Add");
			} else if (Presentation_FadePanel.Size.Width == 0) {
				Presentation_FadePanel.Size = new System.Drawing.Size(510, Presentation_FadePanel.Size.Height);
				RightDocks_BottomPanel_Media_FadePanelButton.Text = Lang.say("Right.Media.Add.Close");
			}
		}


		private void Presentation_PreviewPanel_Resize(object sender, System.EventArgs e) {
			this.Presentation_Resize();
		}


		public void PreviewPresentationMedia(string mediaFile) {
			if (video != null)
				video.Stop();
			if (axShockwaveFlash.Playing)
				axShockwaveFlash.Stop();

			Presentation_StopButton.Enabled = false;
			Presentation_PauseButton.Enabled = false;
			axShockwaveFlash.SendToBack();
			axShockwaveFlash.Movie = "";
			this.MediaPreview = false;
			MediaFile = mediaFile;
			if (MediaList.GetType(MediaFile) == "image") {
				MovieControlPanelWipe("in");
				Presentation_VideoPanel.Hide();
				axShockwaveFlash.Hide();
				Presentation_PreviewBox.Show();
				Presentation_PreviewBox.BringToFront();
				Presentation_PreviewBox.Image = this.ShowBeam.DrawProportionalBitmap(Presentation_PreviewBox.Size, MediaFile);
			}
			if (MediaList.GetType(MediaFile) == "flash") {
				MovieControlPanelWipe("out");
				AudioBar.Enabled = false;
				Presentation_PreviewBox.Hide();
				Presentation_VideoPanel.Hide();
				axShockwaveFlash.Show();
				axShockwaveFlash.BringToFront();
				axShockwaveFlash.Movie = MediaFile;
				axShockwaveFlash.Playing = false;
				axShockwaveFlash.Stop();
				this.Presentation_Resize();
			}
			if (MediaList.GetType(MediaFile) == "movie") {
				this.Presentation_PreviewBox.Hide();
				this.axShockwaveFlash.Hide();
				if (!LoadingVideo) {
					LoadVideo();

					/* Can't use a separate thread without ensuring that all UI interaction
					 * is done on the main UI thread.
					 */
					//Thread_LoadMovie = new Thread(new ThreadStart(LoadVideo));
					//Thread_LoadMovie.IsBackground = true;
					//Thread_LoadMovie.Name = "LoadVideo";
					//Thread_LoadMovie.Start();
				}
			}
			GC.Collect();
		}

		private void RightDocks_BottomPanel_MediaList_SelectedIndexChanged(object sender, System.EventArgs e) {
			PreviewPresentationMedia(this.MediaList.iItem[RightDocks_BottomPanel_MediaList.SelectedIndex].Path);
		}

		private void LoadVideo() {
			LoadingVideo = true;
			VideoProblem = false;
			if (this.ShowBeam.strMediaPlaying != null) {
				Presentation_StopButton.Enabled = true;
			}
			MovieControlPanelWipe("out");
			Presentation_MoviePreviewButton.Enabled = false;
			AudioBar.Enabled = false;
			Media_TrackBar.Enabled = false;
			Presentation_MoviePreviewButton.Text = "Loading...";
			Presentation_VideoPanel.BringToFront();

			if (this.video == null) {
				try {
					this.video = new Microsoft.DirectX.AudioVideoPlayback.Video(MediaFile, false);
				} catch {
					MessageBox.Show("Can not load Video.");
					VideoProblem = true;
				}
			} else {
				this.video.Stop();
				try {
					this.video.Dispose();
					this.video = null;
					this.video = new Microsoft.DirectX.AudioVideoPlayback.Video(MediaFile, false);
				} catch {
					MessageBox.Show("Could not load Video.");
					VideoProblem = true;
				}
			}
			if (!VideoProblem) {
				this.video.Owner = Presentation_VideoPanel;
				this.Presentation_VideoPanel.Show();
				this.Presentation_VideoPanel.BringToFront();
				this.Presentation_Resize();
				Presentation_MoviePreviewButton.Text = "Preview";
				Presentation_MoviePreviewButton.Enabled = true;
				AudioBar.Enabled = true;
				Media_TrackBar.Enabled = true;
			} else {
				Presentation_StopButton.Enabled = false;
				Presentation_PauseButton.Enabled = false;
				Presentation_PlayButton.Enabled = false;
			}
			this.VideoLoaded = true;
			LoadingVideo = false;
		}

		private Rectangle Presentation_Rectangle(Size ContainerSize, Size ContentSize, int Padding) {
			Rectangle r = new Rectangle();
			ContainerSize.Width -= Padding * 2;
			ContainerSize.Height -= Padding * 2;
			r.Size = Tools.VideoProportions(ContainerSize, ContentSize);
			r.X = (ContainerSize.Width - r.Size.Width) / 2 + Padding;
			r.Y = (ContainerSize.Height - r.Size.Height) / 2 + Padding;
			return r;
		}

		private void Presentation_Resize() {
			Rectangle r;
			Size ContainerSize = this.Presentation_PreviewPanel.Size;
			int Padding = this.Presentation_PreviewPanel.DockPadding.All;

			if (this.Presentation_VideoPanel.Visible) {
				r = this.Presentation_Rectangle(ContainerSize, this.video.DefaultSize, Padding);
				this.video.Size = r.Size;
				this.Presentation_VideoPanel.Size = r.Size;
				this.Presentation_VideoPanel.Location = r.Location;
			} else if (this.axShockwaveFlash.Visible) {
				r = this.Presentation_Rectangle(ContainerSize, new Size(1600, 1200), Padding);
				this.axShockwaveFlash.Size = r.Size;
				this.axShockwaveFlash.Location = r.Location;
			} else if (this.Presentation_PreviewBox.Visible) {
				this.Presentation_PreviewBox.Size = new Size(ContainerSize.Width - 2 * Padding, ContainerSize.Height - 2 * Padding);
				this.Presentation_PreviewBox.Location = new Point(Padding, Padding);
				this.Presentation_PreviewBox.Image = this.ShowBeam.DrawProportionalBitmap(Presentation_PreviewBox.Size,
					this.MediaFile);
			}
		}



		#endregion


		#region MediaList

		///<summary>Reads all MediaLists in Directory, validates if it is a MediaList and put's them into the RightDocks_SongList Box </summary>
		public void ListMediaLists() {
			this.RightDocks_MediaLists.Items.Clear();
			string strSongDir = Tools.GetDirectory(DirType.MediaLists);

			// TODO: What is this code doing?
			string[] dirs2 = Directory.GetFiles(strSongDir, "*.xml");
			foreach (string dir2 in dirs2) {
				if (Song.isSong(Path.GetFileName(dir2))) {
					string temp = Path.GetFileName(dir2);
					this.RightDocks_MediaLists.Items.Add(temp.Substring(0, temp.Length - 4));
				}
			}
		}





		/// <summary>Loads Default MediaList on Startup</summary>
		void LoadDefaultMediaList() {
			if (File.Exists("MediaLists\\Default.xml")) {
				GuiTools.RightDock.MediaListTools.LoadSelectedMediaList("Default");
			}
		}

		/// <summary>Loads a MediaList after DoubleClick on ListBox</summary>
		private void RightDocks_MediaLists_DoubleClick(object sender, System.EventArgs e) {
			if (this.RightDocks_MediaLists.SelectedIndex >= 0) {
				GuiTools.RightDock.MediaListTools.LoadSelectedMediaList(RightDocks_MediaLists.SelectedItem.ToString());
				RightDocks_BottomPanel_Media.Open();
			}
		}


		/// <summary>Loads the selected MediaList</summary>
		private void RightDocks_MediaLists_LoadButton_Click(object sender, System.EventArgs e) {
			if (this.RightDocks_MediaLists.SelectedIndex >= 0) {
				GuiTools.RightDock.MediaListTools.LoadSelectedMediaList(RightDocks_MediaLists.SelectedItem.ToString());
				RightDocks_BottomPanel_Media.Open();
			}
		}

		/// <summary>Deletes the Selected MediaList, after Userconfirmation</summary>
		private void RightDocks_MediaLists_DeleteButton_Click(object sender, System.EventArgs e) {
			if (this.RightDocks_MediaLists.SelectedIndex >= 0) {
				if (System.IO.File.Exists("MediaLists\\" + RightDocks_MediaLists.SelectedItem.ToString() + ".xml")) {
					DialogResult answer = MessageBox.Show(RightDocks_MediaLists.SelectedItem.ToString() + " wirklich löschen?", "MediaList löschen..", MessageBoxButtons.YesNo);
					if (answer == DialogResult.Yes) {
						System.IO.File.Delete("MediaLists\\" + RightDocks_MediaLists.SelectedItem.ToString() + ".xml");
					}
				}
				this.ListMediaLists();
			}
		}


		/// <summary>Function to Save the current MediaList</summary>
		void SaveMediaList() {
			MediaList.Save();
			this.ListMediaLists();
			this.StatusPanel.Text = "MediaList '" + MediaList.Name + "' saved.";
		}

		#endregion

		private void Presentation_Fade_Refresh_Button_Click(object sender, System.EventArgs e) {
			GuiTools.Presentation.fillTree();
		}

		private void treeView1_BeforeExpand(object sender, System.Windows.Forms.TreeViewCancelEventArgs e) {
			this.GuiTools.Presentation.treeView1_BeforeExpand(sender, e);
		}

		private void treeView1_BeforeSelect(object sender, System.Windows.Forms.TreeViewCancelEventArgs e) {
			GuiTools.Presentation.getSubDirs(e.Node);     // get the sub-folders for the selected node.
			GuiTools.Presentation.ListFiles(GuiTools.Presentation.fixPath(e.Node));
			GuiTools.Presentation.strMediaPath = GuiTools.Presentation.fixPath(e.Node);
			folder = new DirectoryInfo(e.Node.FullPath); // get it's Directory info.
		}

		private void Presentation_Fade_ListView_DoubleClick(object sender, System.EventArgs e) {
			this.GuiTools.Presentation.Fade_ToPlaylist_Button_Click(sender, e);
		}

		/// <summary>Show the Media on Projector Window</summary>
		private void Media2BeamBox() {
			// send Loop Video bool to ShowBeam
			this.ShowBeam.LoopMedia = Presentation_MediaLoop_Checkbox.Checked;
			// check if presentation Window is open
			if (ToolBars_MainToolbar_ShowBeamBox.Checked == false) {
				ToolBars_MainToolbar_ShowBeamBox.Checked = true;
				ShowBeam.Show();
			}

			ShowBeam.strMediaPlaying = null;
			Presentation_PauseButton.Enabled = true;
			Presentation_StopButton.Enabled = true;
			Presentation_PlayButton.Enabled = false;

			if (MediaList.GetType(MediaFile) == "movie" && this.LoadingVideo) {
				this.VideoLoadTimer.Enabled = true;
			} else {
				if (MediaList.GetType(MediaFile) == "flash") {
					try {
						Media_TrackBar.Maximum = axShockwaveFlash.TotalFrames;
						PlayProgress.Enabled = true;
					} catch { return; }
				}
				if (MediaList.GetType(MediaFile) == "movie") {
					try {
						Media_TrackBar.Maximum = (int)video.Duration;
						PlayProgress.Enabled = true;
					} catch { }
				}
				if (MediaList.GetType(MediaFile) == "image") {
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

			if (Config.BibleLang == "de") {
				BibleBooks[0] = "1. Mose,2. Mose,3. Mose,4. Mose,5. Mose,Josua,Richter,Rut,1. Samuel,2. Samuel,1. Könige,2. Könige,1. Chronik,2. Chronik,Esra,Nehemia,Ester,Hiob,Psalmen,Prediger,Prediger,Hoheslied,Jesaja,Jeremia,Klagelieder,Hesekiel,Daniel,Hosea,Joel,Amos,Obadja,Jona,Micha,Nahum,Habakuk,Zefanja,Haggai,Sacharja,Maleachi";
				BibleBooks[1] = "Matthäus,Markus,Lukas,Johannes,Apostelgeschichte,Römer,1. Korinther,2. Korinther,Galater,Epheser,Philipper,Kolosser,1. Thessalonicher,2. Thessalonicher,1. Timotheus,2. Timotheus,Titus,Philemon,Hebräer,Jakobus,1. Petrus,2. Petrus,1. Johannes,2. Johannes,3. Johannes,Judas,Offenbahrung";
			}
			BibleBooks[0] = "Genesis,Exodus,Leviticus,Numbers,Deuteronomy,Joshua,Judges,Ruth,1 Samuel,2 Samuel,I Kings,II Kings,1 Chronicles,2 Chronicles,Ezra,Nehemiah,Esther,Job,Psalm,Proverbs,Ecclesiastes,Song of Solomon,Isaiah,Jeremiah,Lamentations,Ezekiel,Daniel,Hosea,Joel,Amos,Obadiah,Jonah,Micah,Nahum,Habakkuk,Zephaniah,Haggai,Zechariah,Malachi";
			BibleBooks[1] = "Matthew,Mark,Luke,John,Acts,Romans,1 Corinthians,2 Corinthians,Galatians,Ephesians,Philippians,Colossians,1 Thessalonians,2 Thessalonians,1 Timothy,2 Timothy,Titus,Philemon,Hebrews,James,1 Peter,2 Peter,1 John,2 John,3 John,Jude,Revelation";

			bibles = BibleLib.DeserializeNow(bibleLibFile);

			if (bibles == null) {
				bibles = new BibleLib();
			}

			Options.PopulateBibleCacheTab();	// We want to pick up the deserialized bibles
			Options.SetBibleLocale(this.bibles, this.Config.SwordPath, this.Config.BibleLang);
			BibleText_Translations_Populate();

			if (this.SwordProject_Found) {

				// Configure the locale. This causes Diatheke to die. Why?
				//				 this.Options.Sword_LanguageBox.Items.Clear();
				//				 Diatheke.book = "system";
				//				 Diatheke.key = "localelist";
				//				 Diatheke.locale = "en";
				//				 Diatheke.query();
				//				 foreach (string lang in Diatheke.value.Split('\n')) {
				//					 if (lang != "abbr") this.Options.Sword_LanguageBox.Items.Add(lang);
				//				 }

				this.Options.Sword_LanguageBox.Items.Clear();
				foreach (string locale in this.GetLocales()) {
					this.Options.Sword_LanguageBox.Items.Add(locale);
				}
				try {
					this.Options.Sword_LanguageBox.SelectedItem = this.Config.BibleLang;
				} catch { }

				Diatheke.locale = this.Sermon_BibleLang;
				Diatheke.outputformat = Convert.ToInt16(DiathekeOutputFormat.PlainText);
				// and add them each to the list control
				int i = 0;
				int match = 0;
				foreach (string Book in this.DiathekeBooks(false)) {
					Sermon_Books.Items.Add(Book);
					if (Book == this.Config.LastBibleUsed) {
						match = i;
					}
					i++;
				}
				if (this.Sermon_Books.Items.Count > 0) {
					this.Sermon_Books.SelectedIndex = match;
				}
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

		private void Diatheke_ValueChanged(object sender, System.EventArgs e) {
			if (this.SwordProject_Found) {
				string strTempText = Diatheke_ConvertEncoding(Diatheke.value);
				this.StatusPanel.Text = strTempText.Substring(0, Math.Min(strTempText.Length, 200));

				// filter out the Verses
				string needle = strTempText.Substring(0, strTempText.IndexOf(":"));
				int pos;
				while (strTempText.IndexOf(needle) >= 0) {
					pos = strTempText.IndexOf(needle);
					string tmp1 = strTempText.Substring(pos, strTempText.IndexOf(":", pos + needle.Length + 1) - pos + 1);
					strTempText = strTempText.Replace(tmp1 + "   ", "");
					strTempText = strTempText.Replace(tmp1 + "  ", "");
					strTempText = strTempText.Replace(tmp1 + " ", "");
					strTempText = strTempText.Replace(tmp1, "");
				}
				strTempText.Replace("{~}", "");

				// if not ShowBibleTranslation Hide the Translation Text
				if (this.Sermon_ShowBibleTranslation) {
					Sermon_DocManager.FocusedDocument.Control.Text += "\n" + strTempText;
				} else {
					Sermon_DocManager.FocusedDocument.Control.Text += "\n" + strTempText.Substring(0, strTempText.IndexOf("(" + Diatheke.book + ")") - 1);
				}
				// Tab text
				Sermon_DocManager.FocusedDocument.Text = Diatheke.key;
			}
		}

		private void Sermon_BibleKey_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
			if (this.SwordProject_Found) {
				if (e.KeyValue == 13) {
					this.Diatheke.book = Sermon_Books.Items[Sermon_Books.SelectedIndex].ToString();
					Diatheke.autoupdate = true;
				}
			}
		}

		public DocumentManager.Document Sermon_NewDocument() {
			System.Windows.Forms.RichTextBox t = new System.Windows.Forms.RichTextBox();
			t.BorderStyle = BorderStyle.None;
			t.ScrollBars = RichTextBoxScrollBars.Both;
			t.WordWrap = true;
			t.Multiline = true;
			t.AcceptsTab = true;
			t.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Both;
			t.Font = new Font("Courier New", 10, FontStyle.Regular);
			t.TextChanged += new System.EventHandler(this.Sermon_DocManager_Control_TextChanged);
			DocumentManager.Document document = new DocumentManager.Document(t, "Document ");
			Sermon_DocManager.AddDocument(document);
			Sermon_DocManager.FocusedDocument = document;
			return document;
		}

		private void Sermon_DocManager_Control_TextChanged(object sender, System.EventArgs e) {
			DocumentManager.Document d = this.Sermon_DocManager.FocusedDocument;
			if (d != null && d.Control != null && d.Control.Text != null) {
				d.Text = this.getSermonDocumentTitle(d.Control.Text);
			}
		}

		private string getSermonDocumentTitle(string text) {
			const int MAX_TITLE_LEN = 20;
			text = text.Trim().Split('\n')[0];
			text = Regex.Replace(text, @"\s+", " ");
			return text.Substring(0, Math.Min(MAX_TITLE_LEN, text.Length));
		}

		private void Sermon_ToolBar_ButtonClick(object sender, TD.SandBar.ToolBarItemEventArgs e) {
			// new Document
			if (e.Item == Sermon_ToolBar_NewDoc_Button) {
				this.Sermon_NewDocument();
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
				//this.Diatheke.query();
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
			//			if(ToolBars_MainToolbar_ShowBeamBox.Checked == false) {
			//				ToolBars_MainToolbar_ShowBeamBox.Checked = true;
			//				ShowBeam.Show();
			//			}

			if (Sermon_DocManager.TabStrips.Count > 0) {
				string fullText = "";
				if (Sermon_DocManager.FocusedDocument != null) {
					fullText = this.Sermon_DocManager.FocusedDocument.Control.Text;
				} else if (Sermon_DocManager.TabStrips[0].Documents.Count > 0) {
					fullText = this.Sermon_DocManager.TabStrips[0].Documents[0].Control.Text;
				}

				if (this.Config.AppOperatingMode == OperatingMode.Client) {
					DisplayLiveClient.SetContent(new TextToolContents(fullText, this.Config));
				} else {
					DisplayLiveLocal.SetContent(new TextToolContents(fullText, this.Config));
				}
			}
		}

		private void Sermon_Preview_Button_Click(object sender, System.EventArgs e) {
			if (Sermon_DocManager.TabStrips.Count > 0) {
				string fullText = "";
				if (Sermon_DocManager.FocusedDocument != null) {
					fullText = this.Sermon_DocManager.FocusedDocument.Control.Text;
				} else if (Sermon_DocManager.TabStrips[0].Documents.Count > 0) {
					fullText = this.Sermon_DocManager.TabStrips[0].Documents[0].Control.Text;
				}
				DisplayPreview.SetContent(new TextToolContents(fullText, this.Config));
			}
		}

		/// <summary>
		/// Diatheke expects to find a sword.conf file in "C:\etc" or "D:\etc" (depending on where the DLL is located).
		/// If it's not found, it panics and crashes DreamBeam.
		/// For a default installation of Sword, the sword.conf file should look like:
		///		[Install]
		///		DataPath=C:/Program Files/CrossWire/The SWORD Project/
		///	
		///	This function both sets the SwordProject_Found private variable and returns its value.
		/// </summary>
		/// <returns></returns>
		public bool Check_SwordProject(Config config) {
			string strSwordConfDir = Path.Combine(Directory.GetDirectoryRoot(Application.StartupPath), "etc");
			string strSwordConfPath = Path.Combine(strSwordConfDir, "sword.conf");
			string pathSeparator = Path.DirectorySeparatorChar.ToString();

			// Create the directory, or else creating the file (if needed) will fail.
			if (!Directory.Exists(strSwordConfDir)) Directory.CreateDirectory(strSwordConfDir);

			IniStructure swordConf = IniStructure.ReadIni(strSwordConfPath);
			if (swordConf != null) {
				// File exists, and contains valid "ini" style configuration
				string swordDir = swordConf.GetValue("Install", "DataPath");
				if (swordDir != null) {
					string swordExe = Path.Combine(swordDir.Replace("/", pathSeparator), "sword.exe");
					if (File.Exists(swordExe)) {
						return this.SwordProject_Found = true;
					}
				}

				if (File.Exists(Path.Combine(config.SwordPath, "sword.exe"))) {
					// swordDir was null, or did not contain sword.exe, but we have the proper path in Config.SwordPath
					swordConf.AddCategory("Install");
					if (swordDir == null) {
						swordConf.AddValue("Install", "DataPath", config.SwordPath.Replace(pathSeparator, "/"));
					} else {
						swordConf.ModifyValue("Install", "DataPath", config.SwordPath.Replace(pathSeparator, "/"));
					}
					return this.SwordProject_Found = IniStructure.WriteIni(swordConf, strSwordConfPath);
				}
			} else if (File.Exists(Path.Combine(config.SwordPath, "sword.exe"))) {
				swordConf = new IniStructure();
				swordConf.AddCategory("Install");
				swordConf.AddValue("Install", "DataPath", config.SwordPath.Replace(pathSeparator, "/"));
				return this.SwordProject_Found = IniStructure.WriteIni(swordConf, strSwordConfPath);
			}

			return this.SwordProject_Found = false;
		}

		#endregion

		#region ContextMenu Things

		private void ImageContextItemManage_Click(object sender, System.EventArgs e) {
			System.Diagnostics.Process.Start("explorer", Tools.GetDirectory(DirType.Backgrounds));
		}

		private void ImageContextItemReload_Click(object sender, System.EventArgs e) {
			GuiTools.RightDock.BGImageTools.ListDirectories();
			GuiTools.RightDock.BGImageTools.ListImages(Tools.GetDirectory(DirType.Backgrounds));
		}

		#endregion

		private void RightDocks_Backgrounds_UseDefault_Click(object sender, System.EventArgs e) {
			if (DisplayPreview.content != null) {
				DisplayPreview.content.ChangeBGImagePath(null);
				DisplayPreview.UpdateDisplay(true);
			}

			if (this.selectedTab == MainTab.EditSongs) {
				this.songEditor.Background.Text = "";
			}

			if (this.RightDocks_ImageListBox.SelectedIndex >= 0) {
				this.RightDocks_ImageListBox.SetSelected(this.RightDocks_ImageListBox.SelectedIndex, false);
			}
		}

		#endregion
		//under development
		#region DragDrop
		private void Presentation_Fade_ListView_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			// Get the index of the item the mouse is below.
			indexOfItemUnderMouseToDrag = Presentation_Fade_ListView.GetItemAt(e.X, e.Y).Index;
			if (indexOfItemUnderMouseToDrag != ListBox.NoMatches) {
				// Remember the point where the mouse down occurred. The DragSize indicates
				// the size that the mouse can move before a drag event should be started.
				Size dragSize = SystemInformation.DragSize;
				// Create a rectangle using the DragSize, with the mouse position being
				// at the center of the rectangle.
				dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2), e.Y - (dragSize.Height / 2)), dragSize);
			} else
				// Reset the rectangle if the mouse is not over an item in the ListBox.
				dragBoxFromMouseDown = Rectangle.Empty;
		}

		private void Presentation_Fade_ListView_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e) {
			// Reset the drag rectangle when the mouse button is raised.
			dragBoxFromMouseDown = Rectangle.Empty;
		}

		private void Presentation_Fade_ListView_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e) {
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
					} finally {
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
							if (indexOfItemUnderMouseToDrag > 0) { }
								//        Presentation_Fade_ListView.SelectedIndex = indexOfItemUnderMouseToDrag -1;
								//        Presentation_Fade_ListView.SelectedIndex = 1;}
								//indexOfItemUnderMouseToDrag -1;
							else if (Presentation_Fade_ListView.Items.Count > 0) { }
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

		private void RightDocks_BottomPanel_MediaListView_DragOver(object sender, System.Windows.Forms.DragEventArgs e) {
			// Determine whether string data exists in the drop data. If not, then
			// the drop effect reflects that the drop cannot occur.
			if (!e.Data.GetDataPresent(typeof(System.String))) {
				e.Effect = DragDropEffects.None;
				return;
			}
			// Set the effect based upon the KeyState.
			if ((e.KeyState & (8 + 32)) == (8 + 32) && (e.AllowedEffect & DragDropEffects.Link) == DragDropEffects.Link) {
				// KeyState 8 + 32 = CTL + ALT
				// Link drag and drop effect.
				e.Effect = DragDropEffects.Link;
			} else if ((e.KeyState & 32) == 32 && (e.AllowedEffect & DragDropEffects.Link) == DragDropEffects.Link) {
				// ALT KeyState for link.
				e.Effect = DragDropEffects.Link;
			} else if ((e.KeyState & 4) == 4 && (e.AllowedEffect & DragDropEffects.Move) == DragDropEffects.Move) {
				// SHIFT KeyState for move.
				e.Effect = DragDropEffects.Move;
			} else if ((e.KeyState & 8) == 8 && (e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy) {
				// CTL KeyState for copy.
				e.Effect = DragDropEffects.Copy;
			} else if ((e.AllowedEffect & DragDropEffects.Move) == DragDropEffects.Move) {
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

		private void RightDocks_BottomPanel_MediaListView_QueryContinueDrag(object sender, System.Windows.Forms.QueryContinueDragEventArgs e) {
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

		private bool SongShow_HideElement(SongTextType type) {
			NewSong s = DisplayPreview.content as NewSong;
			if (s != null) {
				if (s.Hide[(int)type]) {
					s.Hide[(int)type] = false;
				} else {
					s.Hide[(int)type] = true;
				}
				DisplayPreview.UpdateDisplay(true);
				return s.Hide[(int)type];
			}
			return false;
		}

		/// <summary>
		/// Updates the HideElement radio boxes to match what's in the DisplayPreview content
		/// </summary>
		private void SongShow_HideElement_UpdateButtons() {
			NewSong s = DisplayPreview.content as NewSong;
			if (s == null) {
				SongShow_HideTitle_Button.Checked = false;
				SongShow_HideText_Button.Checked = false;
				SongShow_HideAuthor_Button.Checked = false;
			} else {
				SongShow_HideTitle_Button.Checked = s.Hide[(int)SongTextType.Title];
				SongShow_HideText_Button.Checked = s.Hide[(int)SongTextType.Verse];
				SongShow_HideAuthor_Button.Checked = s.Hide[(int)SongTextType.Author];
			}
		}

		private void SongShow_HideTitle_Button_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e) {
			SongShow_HideElement_UpdateButtons();
		}

		private void SongShow_HideTitle_Button_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			SongShow_HideElement(SongTextType.Title);
		}

		private void SongShow_HideText_Button_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			SongShow_HideElement(SongTextType.Verse);
		}

		private void SongShow_HideText_Button_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e) {
			SongShow_HideElement_UpdateButtons();
		}

		private void SongShow_HideAuthor_Button_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			SongShow_HideElement(SongTextType.Author);
		}

		private void SongShow_HideAuthor_Button_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e) {
			SongShow_HideElement_UpdateButtons();
		}

		#endregion

		#region Diatheke and Sword
		private string Diatheke_ConvertEncoding(string text) {
			Encoding utf8 = Encoding.GetEncoding("UTF-8");
			Encoding win1252 = Encoding.GetEncoding("Windows-1252");

			byte[] rawBytes = win1252.GetBytes(text);
			return utf8.GetString(rawBytes);
		}

		public ArrayList DiathekeBooks(bool BiblesOnly) {
			ArrayList BookList = new ArrayList();
			if (Diatheke == null) return BookList;

			Diatheke.book = "system";
			if (BiblesOnly) {
				Diatheke.key = "modulelist";
				Diatheke.query();
				foreach (string book in Diatheke.value.Trim().Split('\n')) {
					if (Regex.IsMatch(book, "Biblical Texts")) continue;
					if (Regex.IsMatch(book, "Commentaries|Dictionaries")) break;
					BookList.Add(Regex.Split(book, " : ")[0]);
				}
			} else {
				Diatheke.key = "modulelistnames";
				Diatheke.query();
				foreach (string book in Diatheke.value.Trim().Split('\n')) {
					BookList.Add(book);
				}
			}
			return BookList;
		}
		#endregion

		#region Bible Text Component

		#region BibleText_Results RichTextBox
		private void BibleText_Results_Update() {
			string translation = BibleText_Translations.SelectedItem.ToString();
			if (bibles.TranslationExists(translation)) {
				BibleText_Results.Populate(bibles[translation], BibleText_Results.CurrentVerse);
			}
		}

		private void BibleText_Results_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			System.Drawing.Point pt = new System.Drawing.Point(e.X, e.Y);
			if (BibleText_Results.Text.Length == 0) return;	// Otherwise we get range exceptions in GetCharFromPosition ...
			char c = BibleText_Results.GetCharFromPosition(pt);
			int idx = BibleText_Results.GetCharIndexFromPosition(pt);
			int l = BibleText_Results.GetLineFromCharIndex(idx);
			int vidx = BibleText_Results.GetVerseIndexFromSelection(idx);
			BibleText_Results.CurrentVerse = vidx;
			DisplayPreview.SetContent(new ABibleVerse(BibleText_Bible, vidx, this.Config));

			//Console.WriteLine("Clicked " + pt + " on line " + l + " at character number " +idx + "(" + c + ").");
			//Console.WriteLine("Clicked {0} on line {1} at character number {2} ({3}).", pt, l, idx, c);
			//Console.WriteLine("Verse: " + BibleText_Bible[vidx].ToString());
		}

		private void BibleText_Results_MouseEnter(object sender, System.EventArgs e) {
			// Allows us to use the scroll wheel by just pointing to the control
			BibleText_Results.Focus();
		}

		private void BibleText_Results_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
			switch (e.KeyCode) {
				case Keys.A:	// Find first
					BibleText_FindFirst_button_Click(sender, null);
					break;
				case Keys.N:	// Find next
					BibleText_FindNext_button_Click(sender, null);
					break;
				case Keys.P:	// Find previous
					BibleText_FindFirst_button_Click(sender, null);
					break;
				case Keys.Z:	// Find last
					BibleText_FindLast_button_Click(sender, null);
					break;
				case Keys.Enter:
					int vidx = BibleText_Results.GetVerseIndexFromSelection(BibleText_Results.SelectionStart);
					BibleText_Results.CurrentVerse = vidx;
					DisplayPreview.SetContent(new ABibleVerse(this.BibleText_Bible, vidx, this.Config));
					RightDocks_Preview_GoLive_Click(sender, null);
					break;
			}
			BibleText_Results.Focus(); // The "Click" handlers above switch focus to RegEx input
			e.Handled = true;
		}

		private int BibleText_Query(bool dirFwd) {
			string translation = BibleText_Translations.SelectedItem.ToString();
			if (!bibles.TranslationExists(translation)) return -1;
			int index = BibleText_Results.Find(bibles[translation], dirFwd, BibleText_RegEx_ComboBox.Text);
			// Force an update
			BibleText_Results.Focus();
			BibleText_RegEx_ComboBox.Focus();
			// Remove selection, or else we'll type over it
			BibleText_RegEx_ComboBox.SelectionStart = BibleText_RegEx_ComboBox.Text.Length;
			return index;
		}
		#endregion

		#region BibleText RegEx and Verse ComboBox
		private void BibleText_RegEx_ComboBox_TimedOut(Object myObject, EventArgs myEventArgs) {
			Console.WriteLine("Timer fired!");
			TextTypedTimer.Stop();
			TextTypedTimer.Enabled = false;
			TextTypedTimer.Tick -= BibleText_RegEx_ComboBox_EventHandler;
			BibleText_Query(true);
		}

		private void BibleText_RegEx_ComboBox_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e) {

			if (e.KeyCode == Keys.Enter) {
				string searchTerm = BibleText_RegEx_ComboBox.Text;
				if (!BibleText_RegEx_ComboBox.Items.Contains(searchTerm)) {
					BibleText_RegEx_ComboBox.Items.Insert(0, searchTerm);
				}
				BibleText_Results.Focus();
			} else if (BibleText_RegEx_ComboBox.Text.Length > 1 && BibleText_RegEx_ComboBox.Text != BibleText_Results.lastRegex) {
				if (TextTypedTimer.Enabled) {
					Console.WriteLine("Timer enabled. Restarting");
					// Restart timer
					TextTypedTimer.Stop();
					TextTypedTimer.Start();
				} else {
					Console.WriteLine("Time disabled");
					TextTypedTimer.Tick += BibleText_RegEx_ComboBox_EventHandler;
					TextTypedTimer.Interval = 300;
					TextTypedTimer.Start();
				}
			}

			//Console.WriteLine("KeyCode = " + e.KeyCode + " KeyValue " + e.KeyValue + " KeyData " + e.KeyData);
		}

		private void BibleText_RegEx_ComboBox_TextChanged(object sender, System.EventArgs e) {
			BibleText_Query(true);
		}

		private void BibleText_Verse_ComboBox_SelectedIndexChanged(object sender, System.EventArgs e) {
			if (BibleText_Bible == null) return;

			BibleText_RegEx_ComboBox.Text = "^" + BibleText_Bible.NormalizeRef(BibleText_Verse_ComboBox.SelectedIndex.ToString()) + @"\s+.*";
			BibleText_Query(true);
		}

		private void BibleText_Verse_ComboBox_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e) {
			if (e.KeyCode == Keys.Enter) {
				if (BibleText_Bible == null) return;

				string reference = BibleText_Verse_ComboBox.Text;
				string normReference;
				normReference = BibleText_Bible.NormalizeRef(reference);
				if (normReference.Length == 0) {
					// Maybe the user forgot to enter the verse. Default to the first verse of the chapter.
					normReference = BibleText_Bible.NormalizeRef(reference + " 1");
				}

				int index = BibleText_Bible.GetVerseIndex(normReference);
				if (index >= 0) {
					BibleText_RegEx_ComboBox.Text = "^" + this.BibleText_Bible.GetSimpleRef(normReference, true) + @"\s+.*";
					BibleText_Query(true);
					DisplayPreview.SetContent(new ABibleVerse(this.BibleText_Bible, index, this.Config));
					if (!BibleText_Verse_ComboBox.Items.Contains(reference)) {
						BibleText_Verse_ComboBox.Items.Insert(0, reference);
					}
					BibleText_Results.Focus();
				} else {
					BibleText_RegEx_ComboBox.Text = "";
					BibleText_Results.Populate(BibleText_Bible, 0);
					BibleText_Results.Focus();
					BibleText_Verse_ComboBox.Focus();
				}
				e.Handled = true;
			}
		}

		#endregion

		#region BibleText RegEx buttons
		private void BibleText_Bookmark_button_Click(object sender, System.EventArgs e) {
			if (BibleText_Bible == null) return;

			string reference = BibleText_Bible.GetRef(BibleText_Results.CurrentVerse);
			if (reference.Length > 0) {
				BibleText_Bookmarks.Items.Insert(0, reference);
			}
		}

		private void BibleText_FindNext_button_Click(object sender, System.EventArgs e) {
			BibleText_Query(true);
		}

		private void BibleText_FindPrev_button_Click(object sender, System.EventArgs e) {
			BibleText_Query(false);
		}

		private void BibleText_FindFirst_button_Click(object sender, System.EventArgs e) {
			BibleText_Results.CurrentVerse = 0;
			BibleText_Query(true);
		}

		private void BibleText_FindLast_button_Click(object sender, System.EventArgs e) {
			if (BibleText_Bible == null) return;
			BibleText_Results.CurrentVerse = BibleText_Bible.VerseCount - 1;
			BibleText_Query(false);
		}

		#endregion

		#region BibleText Translations and Bookmarks ListBox
		private void BibleText_Translations_SelectedIndexChanged(object sender, System.EventArgs e) {
			Console.WriteLine("Selected index changed sender: " + sender.ToString() + " Event: " + e.ToString() + " Type: " + e.GetType().ToString());
			BibleText_Bible = bibles[BibleText_Translations.SelectedItem.ToString()];
			BibleText_Results_Update();
			this.Config.LastBibleUsed = BibleText_Translations.SelectedItem.ToString();
		}

		private void BibleText_Bookmarks_SelectedIndexChanged(object sender, System.EventArgs e) {
			if (BibleText_Bible == null) return;

			//BibleText_Results.Populate(BibleText_Bible, BibleText_Bible.GetVerseIndex(BibleText_Bookmarks.SelectedItem.ToString()));
			//BibleText_Results.Focus();
			//BibleText_Bookmarks.Focus();
			// OR
			if (BibleText_Bookmarks.SelectedItems.Count == 1) {
				BibleText_RegEx_ComboBox.Text = "^" + this.BibleText_Bible.GetSimpleRef(BibleText_Bookmarks.SelectedItem.ToString(), true) + @"\s+.*";
				int vidx = BibleText_Query(true);
				DisplayPreview.SetContent(new ABibleVerse(this.BibleText_Bible, vidx, this.Config));
			}
		}

		private void BibleText_Bookmarks_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			if (e.Button == MouseButtons.Right) {
				int index = BibleText_Bookmarks.IndexFromPoint(e.X, e.Y);
				if (index >= 0) {
					BibleText_Bookmarks.Items.RemoveAt(index);
				}
			}
		}

		#endregion

		public void BibleText_Translations_Populate() {
			int match = 0, i = 0;
			BibleText_Translations.Items.Clear();
			foreach (string tr in bibles.Translations()) {
				BibleText_Translations.Items.Add(tr);
				if (tr == this.Config.LastBibleUsed) {
					match = i;
				}
				i++;
			}
			if (BibleText_Translations.Items.Count > match) BibleText_Translations.SelectedIndex = match;
		}
		#endregion

		#region Preview and Live screen mini-windows


		#region Preview and Live mini-screen buttons
		private void RightDocks_Preview_Next_Click(object sender, System.EventArgs e) {
			bool success = DisplayPreview.ContentNext();
			//		this.Main_ErrorProvider.SetIconAlignment(this.RightDocks_Preview_Next, ErrorIconAlignment.MiddleLeft);
			//		if (! success) this.ShowError(this.RightDocks_Preview_Next, "Could not switch to the next one");
		}
		private void RightDocks_Preview_Prev_Click(object sender, System.EventArgs e) {
			bool success = DisplayPreview.ContentPrev();
			//		if (! success) this.ShowError(this.RightDocks_Preview_Prev, "Could not switch to the previous one");
		}
		private void RightDocks_Preview_GoLive_Click(object sender, System.EventArgs e) {
			if (DisplayPreview.content != null) {
				ToolBars_MainToolbar_HideBG.Checked = false;
				ToolBars_MainToolbar_HideText.Checked = false;
				switch (Config.AppOperatingMode) {
					case OperatingMode.StandAlone:
					case OperatingMode.Server:
						DisplayLiveLocal.SetContent(DisplayPreview.content.Clone() as IContentOperations);
						break;
					case OperatingMode.Client:
						DisplayLiveClient.SetContent(DisplayPreview.content.Clone() as IContentOperations);
						break;
				}
			}
		}
		private void RightDocks_Live_Prev_Click(object sender, System.EventArgs e) {
			this.DisplayLive_Prev();
		}
		private void RightDocks_Live_Next_Click(object sender, System.EventArgs e) {
			this.DisplayLive_Next();
		}

		public void DisplayLive_Prev() {
			if (Config.AppOperatingMode == OperatingMode.Client) {
				DisplayLiveClient.ContentPrev();
			} else {
				DisplayLiveLocal.ContentPrev();
			}
		}
		public void DisplayLive_Next() {
			if (Config.AppOperatingMode == OperatingMode.Client) {
				DisplayLiveClient.ContentNext();
			} else {
				DisplayLiveLocal.ContentNext();
			}
		}

		#endregion

		private void RightDocks_LiveScreen_PictureBox_SizeChanged(object sender, System.EventArgs e) {
			if (DisplayLiveMini != null) DisplayLiveMini.UpdateDisplay(false);
		}

		private void RightDocks_PreviewScreen_PictureBox_SizeChanged(object sender, System.EventArgs e) {
			if (DisplayPreview != null) DisplayPreview.UpdateDisplay(false);
		}

		#endregion



		public void ShowError(Control control, string errorMsg) {
			this.ErrorProvider_LastControl = control;
			this.Main_ErrorProvider.SetError(control, errorMsg);
		}


		//	private void MainForm_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
		//		if (this.ErrorProvider_LastControl != null) {
		//			this.Main_ErrorProvider.SetError(this.ErrorProvider_LastControl, "");
		//		}
		//	}

		/// <summary>
		/// Finds a list of all the locales supported by Sword by looking for the locale .conf files
		/// in the Sword locales.d directory.
		/// </summary>
		/// <returns></returns>
		public ArrayList GetLocales() {
			ArrayList locales = new ArrayList();
			DirectoryInfo dir = new DirectoryInfo(Path.Combine(this.Config.SwordPath, "locales.d"));
			if (!dir.Exists) return locales;
			foreach (FileInfo f in dir.GetFiles("*.conf")) {
				string locale = Regex.Replace(f.Name, @"\.conf$", "");
				if (!Regex.IsMatch(locale, "abbr")) {
					locales.Add(locale);
				}
			}
			return locales;
		}

		private void ToolBars_MenuBar_Import_Song_Activate(object sender, System.EventArgs e) {
			this.OpenFileDialog.Multiselect = true;
			this.OpenFileDialog.Title = "Import songs";
			if (this.OpenFileDialog.ShowDialog() == DialogResult.OK) {
				foreach (string fileName in this.OpenFileDialog.FileNames) {
					Console.WriteLine("Importing from: " + fileName);
					this.StatusPanel.Text = "Importing from: " + fileName;
					if (Tools.FileExists(fileName)) {
						NewSong song = new NewSong(fileName);
						if (Tools.FileExists(song.FileName)) {
							MessageBoxEx msgBox = MessageBoxExManager.GetMessageBox("SongImportOverwrite");
							if (msgBox == null) {
								msgBox = MessageBoxExManager.CreateMessageBox("SongImportOverwrite");
								msgBox.Caption = "Question";

								msgBox.AddButtons(MessageBoxButtons.YesNo);
								msgBox.Icon = (MessageBoxExIcon)MessageBoxIcon.Question;

								msgBox.SaveResponseText = "Don't ask me again";
								msgBox.AllowSaveResponse = true;
							}

							msgBox.Text = "A file named " + song.FileName + " already exists.\nDo you want to overwrite it?";
							string result = msgBox.Show();
							if (result == MessageBoxExResult.No) continue;
						}

						if (song.FileName != null && song.SongLyrics.Count > 0) {
							NewSong.SerializeTo(song, song.FileName);
						} else {
							DialogResult result = MessageBox.Show("No song lyrics could be found in " + fileName +
								"\nMake sure the file has the correct format" +
								"\nPress Cancel to abort song import, or OK to continue to the next file",
								"Song import error", MessageBoxButtons.OKCancel);
							if (result == DialogResult.Cancel) break;
						}
					}
				}

				MessageBoxExManager.ResetSavedResponse("SongImportOverwrite");
				this.StatusPanel.Text = "Finished importing.";
			}
		}

		private void RightDocks_ImageListBox_DoubleClick(object sender, System.EventArgs e) {
			if (this.selectedTab == MainTab.Presentation) {
				this.Media2BeamBox();
			}
		}

		#region UI thread access
		// We can't modify UI controls from a different thread, so we do it like this:

		private delegate void ImageListBoxUpdateDelegate(ImageListBox box, ImageListBoxItem item);
		/// <summary>
		/// Ensures that the ImageListBox is only updated on the UI thread.
		/// </summary>
		/// <param name="box">The box to update</param>
		/// <param name="item">If the item parameter is null, the items will be cleared</param>
		public void ImageListBoxUpdate(ImageListBox box, ImageListBoxItem item) {
			if (box.InvokeRequired) {
				box.Invoke(new ImageListBoxUpdateDelegate(this.ImageListBoxUpdate), new object[]{box, item});
			} else {
				if (item == null) {
					box.Items.Clear();
				} else {
					box.Items.Add(item);
				}
			}
		}

		private delegate void UpdateStatusPanelDelegate(string msg);
		public void UpdateStatusPanel(String msg) {
			if (this.InvokeRequired) {
				this.Invoke(new UpdateStatusPanelDelegate(this.UpdateStatusPanel), msg);
			} else {
				this.StatusPanel.Text = msg;
			}
		}
		#endregion

	}

}

