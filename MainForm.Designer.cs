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


namespace DreamBeam {
	public partial class MainForm : System.Windows.Forms.Form {


		#region Toolbars and other System.Windows.Forms Declarations

		private System.Windows.Forms.StatusBar statusBar;
		public System.Windows.Forms.StatusBarPanel StatusPanel;
		private System.Windows.Forms.Timer TextTypedTimer;

		#region Menu Bar
		private TD.SandBar.MenuBar ToolBars_MenuBar;

		private TD.SandBar.MenuBarItem ToolBars_MenuBar_File;
		private TD.SandBar.MenuButtonItem ToolBars_MenuBar_File_Import;
		private TD.SandBar.MenuButtonItem ToolBars_MenuBar_Import_Song;
		private TD.SandBar.MenuButtonItem ToolBars_MenuBar_File_Exit;

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

		private TD.SandBar.MenuBarItem ToolBars_MenuBar_Edit;
		private TD.SandBar.MenuButtonItem ToolBars_MenuBar_Edit_Options;

		private TD.SandBar.MenuBarItem ToolBars_MenuBar_View;
		private TD.SandBar.MenuButtonItem ToolBars_MenuBar_View_ShowSongs;
		private TD.SandBar.MenuButtonItem ToolBars_MenuBar_View_EditSongs;
		private TD.SandBar.MenuButtonItem ToolBars_MenuBar_View_Multimedia;
		private TD.SandBar.MenuButtonItem ToolBars_MenuBar_View_SermonTool;
		private TD.SandBar.MenuButtonItem ToolBars_MenuBar_View_BibleText;
		private TD.SandBar.MenuButtonItem ToolBars_MenuBar_Open_PreviewTab;
		private TD.SandBar.MenuButtonItem ToolBars_MenuBar_Open_LiveTab;
		private TD.SandBar.MenuButtonItem ToolBars_MenuBar_Open_BackgroundsTab;
		private TD.SandBar.MenuButtonItem ToolBars_MenuBar_Open_MediaListTab;
		private TD.SandBar.MenuButtonItem ToolBars_MenuBar_Open_SongsTab;
		private TD.SandBar.MenuButtonItem ToolBars_MenuBar_Open_PlaylistTab;
		private TD.SandBar.MenuButtonItem ToolBars_MenuBar_Open_MediaTab;
		private TD.SandBar.MenuButtonItem ToolBars_MenuBar_Open_BibleToolsTab;
		private TD.SandBar.MenuButtonItem ToolBars_MenuBar_Open_SongToolsTab;

		private TD.SandBar.MenuBarItem ToolBars_MenuBar_Help;
		private TD.SandBar.MenuButtonItem ToolBars_MenuBar_Help_Intro;
		private TD.SandBar.MenuButtonItem ToolBars_MenuBar_Help_About;
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
		public TD.SandBar.ButtonItem Presentation_Button;
		public TD.SandBar.ButtonItem Sermon_Button;
		public TD.SandBar.ButtonItem BibleText_Button;
		#endregion

		#region Docking panel controls

		#region Docking panels
		private TD.SandBar.SandBarManager ToolBars_sandBarManager1;
		private TD.SandBar.ToolBarContainer ToolBars_leftSandBarDock;
		private TD.SandBar.ToolBarContainer ToolBars_bottomSandBarDock;
		private TD.SandBar.ToolBarContainer ToolBars_topSandBarDock;

		public TD.SandDock.SandDockManager sandDockManager1;
		private TD.SandDock.DockContainer leftSandDock;
		private TD.SandDock.DockContainer rightSandDock;
		private TD.SandDock.DockContainer bottomSandDock;
		private TD.SandDock.DockContainer topSandDock;

		public TD.SandDock.DockControl DockControl_Songs;
		public TD.SandDock.DockControl DockControl_Backgrounds;
		public TD.SandDock.DockControl DockControl_PlayList;
		public TD.SandDock.DockControl DockControl_PreviewScreen;
		public TD.SandDock.DockControl DockControl_LiveScreen;
		public TD.SandDock.DockControl DockControl_MediaLists;
		public TD.SandDock.DockControl DockControl_Media;
		#endregion

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

		#region ImageBox (Backgrounds)
		public Controls.Development.ImageListBox RightDocks_ImageListBox;
		public System.Windows.Forms.ImageList RightDocks_imageList;

		// Context Menu
		private System.Windows.Forms.ContextMenu ImageContext;
		private System.Windows.Forms.MenuItem ImageContextItemManage;
		private System.Windows.Forms.MenuItem ImageContextItemReload;
		public System.Windows.Forms.ComboBox RightDocks_FolderDropdown;
		#endregion

		#endregion // Docking panel controls

		#region Presentation (video preview)
		private System.Windows.Forms.ImageList imageList_Folders;
		private System.Windows.Forms.ImageList Presentation_Fade_ImageList;
		public System.Windows.Forms.ImageList Media_ImageList;
		public System.Windows.Forms.ImageList Media_Logos;
		private System.Windows.Forms.Timer PlayProgress;
		private System.Windows.Forms.ImageList PlayButtons_ImageList;
		private System.Windows.Forms.Timer VideoLoadTimer;
		#endregion

		private System.Windows.Forms.Control ErrorProvider_LastControl = null;

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

		public System.Windows.Forms.ProgressBar RenderStatus;
		private System.Windows.Forms.ListBox BibleText_Translations;
		private System.Windows.Forms.ListBox BibleText_Bookmarks;
		private System.Windows.Forms.PictureBox RightDocks_LiveScreen_PictureBox;
		//private System.Windows.Forms.PictureBox RightDocks_PreviewScreen_PictureBox;
        private ImagePanel RightDocks_PreviewScreen_PictureBox;
		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.Panel panel9;
		private System.Windows.Forms.Button RightDocks_Preview_Next;
		private System.Windows.Forms.Button RightDocks_Preview_GoLive;
		private System.Windows.Forms.Button RightDocks_Preview_Prev;
		private System.Windows.Forms.Button RightDocks_Live_Next;
		private System.Windows.Forms.Button RightDocks_Live_Prev;
		private TD.SandDock.DockControl DockControl_BibleTools;
		private Salamander.Windows.Forms.CollapsiblePanel BibleBookmarks_CollapsiblePanel;
		private Salamander.Windows.Forms.CollapsiblePanel BibleTranslations_CollapsiblePanel;
		private Salamander.Windows.Forms.CollapsiblePanelBar BibleTools_CollapsiblePanelBar;
		private System.Windows.Forms.ToolTip MainForm_ToolTip;
		private TD.SandDock.DockControl Dock_SongTools;
		private System.Windows.Forms.ErrorProvider Main_ErrorProvider;
		private System.Windows.Forms.Button RightDocks_Backgrounds_UseDefault;
		private System.Windows.Forms.Panel panel6;
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
		private System.Windows.Forms.Panel Presentation_MovieControlPanel_Top;
		private System.Windows.Forms.TrackBar Media_TrackBar;
		private System.Windows.Forms.Panel Presentation_MovieControlPanel_Right;
		private System.Windows.Forms.TrackBar AudioBar;
		public System.Windows.Forms.TabPage EditSongs_Tab;
		public DreamBeam.SongEditor songEditor;
		public System.Windows.Forms.TabPage BibleText_Tab;
		public System.Windows.Forms.TabControl tabControl1;
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


		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Panel Presentation_MovieControl_LiveButtonPanel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Node0");
            this.Presentation_MediaLoop_Checkbox = new System.Windows.Forms.CheckBox();
            this.RightDocks_ImageListBox = new Controls.Development.ImageListBox();
            this.ImageContext = new System.Windows.Forms.ContextMenu();
            this.ImageContextItemManage = new System.Windows.Forms.MenuItem();
            this.ImageContextItemReload = new System.Windows.Forms.MenuItem();
            this.RightDocks_imageList = new System.Windows.Forms.ImageList(this.components);
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
            this.ToolBars_MenuBar_File_Import = new TD.SandBar.MenuButtonItem();
            this.ToolBars_MenuBar_Import_Song = new TD.SandBar.MenuButtonItem();
            this.ToolBars_MenuBar_File_Exit = new TD.SandBar.MenuButtonItem();
            this.ToolBars_MenuBar_Edit = new TD.SandBar.MenuBarItem();
            this.ToolBars_MenuBar_Edit_Options = new TD.SandBar.MenuButtonItem();
            this.ToolBars_MenuBar_Song = new TD.SandBar.MenuBarItem();
            this.ToolBars_MenuBar_Song_New = new TD.SandBar.MenuButtonItem();
            this.ToolBars_MenuBar_Song_Save = new TD.SandBar.MenuButtonItem();
            this.ToolBars_MenuBar_Song_SaveAs = new TD.SandBar.MenuButtonItem();
            this.ToolBars_MenuBar_Song_Rename = new TD.SandBar.MenuButtonItem();
            this.menuButtonItem1 = new TD.SandBar.MenuButtonItem();
            this.ToolBars_MenuBar_MediaList = new TD.SandBar.MenuBarItem();
            this.ToolBars_MenuBar_MediaList_New = new TD.SandBar.MenuButtonItem();
            this.ToolBars_MenuBar_Media_Save = new TD.SandBar.MenuButtonItem();
            this.ToolBars_MenuBar_Media_SaveAs = new TD.SandBar.MenuButtonItem();
            this.ToolBars_MenuBar_Media_Rename = new TD.SandBar.MenuButtonItem();
            this.ToolBars_MenuBar_View = new TD.SandBar.MenuBarItem();
            this.ToolBars_MenuBar_View_ShowSongs = new TD.SandBar.MenuButtonItem();
            this.ToolBars_MenuBar_View_EditSongs = new TD.SandBar.MenuButtonItem();
            this.ToolBars_MenuBar_View_Multimedia = new TD.SandBar.MenuButtonItem();
            this.ToolBars_MenuBar_View_SermonTool = new TD.SandBar.MenuButtonItem();
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
            this.ToolBars_MenuBar_Open_DesignTab = new TD.SandBar.MenuButtonItem();
            this.ToolBars_MenuBar_Help = new TD.SandBar.MenuBarItem();
            this.ToolBars_MenuBar_Help_Intro = new TD.SandBar.MenuButtonItem();
            this.ToolBars_MenuBar_Help_About = new TD.SandBar.MenuButtonItem();
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.SongShow_HideElementsSub1Panel = new System.Windows.Forms.Panel();
            this.BibleBookmarks_CollapsiblePanel = new Salamander.Windows.Forms.CollapsiblePanel();
            this.BibleText_Bookmarks = new System.Windows.Forms.ListBox();
            this.BibleTranslations_CollapsiblePanel = new Salamander.Windows.Forms.CollapsiblePanel();
            this.BibleText_Translations = new System.Windows.Forms.ListBox();
            this.TextTypedTimer = new System.Windows.Forms.Timer(this.components);
            this.sandDockManager1 = new TD.SandDock.SandDockManager();
            this.leftSandDock = new TD.SandDock.DockContainer();
            this.rightSandDock = new TD.SandDock.DockContainer();
            this.DockControl_PreviewScreen = new TD.SandDock.DockControl();
            this.panel5 = new System.Windows.Forms.Panel();
            this.RightDocks_Preview_Next = new System.Windows.Forms.Button();
            this.RightDocks_Preview_GoLive = new System.Windows.Forms.Button();
            this.RightDocks_Preview_Prev = new System.Windows.Forms.Button();
            this.DockControl_LiveScreen = new TD.SandDock.DockControl();
            this.RightDocks_LiveScreen_PictureBox = new System.Windows.Forms.PictureBox();
            this.panel9 = new System.Windows.Forms.Panel();
            this.RightDocks_Live_Next = new System.Windows.Forms.Button();
            this.RightDocks_Live_Prev = new System.Windows.Forms.Button();
            this.Dock_SongTools = new TD.SandDock.DockControl();
            this.DockControl_BibleTools = new TD.SandDock.DockControl();
            this.BibleTools_CollapsiblePanelBar = new Salamander.Windows.Forms.CollapsiblePanelBar();
            this.DockControl_DesignEditor = new TD.SandDock.DockControl();
            this.DesignTabControl = new System.Windows.Forms.TabControl();
            this.SongDesignTab = new System.Windows.Forms.TabPage();
            this.SermonDesignTab = new System.Windows.Forms.TabPage();
            this.BibleDesignTab = new System.Windows.Forms.TabPage();
            this.DockControl_Songs = new TD.SandDock.DockControl();
            this.DockControl_PlayList = new TD.SandDock.DockControl();
            this.DockControl_Backgrounds = new TD.SandDock.DockControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.RightDocks_Backgrounds_UseDefault = new System.Windows.Forms.Button();
            this.RightDocks_BottomPanel2_TopPanel = new System.Windows.Forms.Panel();
            this.DockControl_Media = new TD.SandDock.DockControl();
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
            this.DockControl_MediaLists = new TD.SandDock.DockControl();
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
            this.MainForm_ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.Sermon_BibleKey = new System.Windows.Forms.TextBox();
            this.Main_ErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.ShowSong_Tab = new System.Windows.Forms.TabPage();
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
            this.Fade_Top_Panel = new System.Windows.Forms.Panel();
            this.Presentation_Fade_ListView = new System.Windows.Forms.ListView();
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
            this.Presentation_MovieControl_PreviewButtonPanel = new System.Windows.Forms.Panel();
            this.Presentation_MovieControlPanel_Top = new System.Windows.Forms.Panel();
            this.Media_TrackBar = new System.Windows.Forms.TrackBar();
            this.Presentation_MovieControlPanel_Right = new System.Windows.Forms.Panel();
            this.muteButton = new System.Windows.Forms.Button();
            this.AudioBar = new System.Windows.Forms.TrackBar();
            this.EditSongs_Tab = new System.Windows.Forms.TabPage();
            this.BibleText_Tab = new System.Windows.Forms.TabPage();
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
            this.HideRectanglesTimer = new System.Windows.Forms.Timer(this.components);
            this.SongShow_StropheList_ListEx = new Lister.ListEx();
            this.songEditor = new DreamBeam.SongEditor();
            this.liveMediaControls = new DreamBeam.MediaControls();
            this.previewMediaControls = new DreamBeam.MediaControls();
            this.bibleTextControl = new DreamBeam.BibleText();
            this.RightDocks_PreviewScreen_PictureBox = new DreamBeam.ImagePanel();
            this.SongShow_HideAuthor_Button = new ctlLEDRadioButton.LEDradioButton();
            this.SongShow_HideText_Button = new ctlLEDRadioButton.LEDradioButton();
            this.SongShow_HideTitle_Button = new ctlLEDRadioButton.LEDradioButton();
            this.songThemeWidget = new DreamBeam.ThemeWidget();
            this.sermonThemeWidget = new DreamBeam.ThemeWidget();
            this.bibleThemeWidget = new DreamBeam.ThemeWidget();
            this.SongList_Tree = new ControlLib.dbTreeViewCtrl();
            Presentation_MovieControl_LiveButtonPanel = new System.Windows.Forms.Panel();
            Presentation_MovieControl_LiveButtonPanel.SuspendLayout();
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
            this.DockControl_PreviewScreen.SuspendLayout();
            this.panel5.SuspendLayout();
            this.DockControl_LiveScreen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RightDocks_LiveScreen_PictureBox)).BeginInit();
            this.panel9.SuspendLayout();
            this.Dock_SongTools.SuspendLayout();
            this.DockControl_BibleTools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BibleTools_CollapsiblePanelBar)).BeginInit();
            this.BibleTools_CollapsiblePanelBar.SuspendLayout();
            this.DockControl_DesignEditor.SuspendLayout();
            this.DesignTabControl.SuspendLayout();
            this.SongDesignTab.SuspendLayout();
            this.SermonDesignTab.SuspendLayout();
            this.BibleDesignTab.SuspendLayout();
            this.DockControl_Songs.SuspendLayout();
            this.DockControl_PlayList.SuspendLayout();
            this.DockControl_Backgrounds.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel6.SuspendLayout();
            this.RightDocks_BottomPanel2_TopPanel.SuspendLayout();
            this.DockControl_Media.SuspendLayout();
            this.RightDocks_BottomPanel_Media_Bottom.SuspendLayout();
            this.RightDocks_BottomPanel_Media_Top.SuspendLayout();
            this.DockControl_MediaLists.SuspendLayout();
            this.RightDocks_BottomPanel_MediaListsTopPanel.SuspendLayout();
            this.RightDocks_BottomPanel_MediaListsTop_Control_Panel.SuspendLayout();
            this.RightDocks_BottomPanel_MediaLists_BottomPanel.SuspendLayout();
            this.RightDocks_BottomPanel_MediaListsBottomPanel_GroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RightDocks_BottomPanel_MediaLists_Numeric)).BeginInit();
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
            this.Presentation_MovieControlPanel_Top.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Media_TrackBar)).BeginInit();
            this.Presentation_MovieControlPanel_Right.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AudioBar)).BeginInit();
            this.EditSongs_Tab.SuspendLayout();
            this.BibleText_Tab.SuspendLayout();
            this.tabControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GlobalDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SongListTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SongListDataView)).BeginInit();
            this.SuspendLayout();
            // 
            // Presentation_MovieControl_LiveButtonPanel
            // 
            Presentation_MovieControl_LiveButtonPanel.Controls.Add(this.Presentation_MediaLoop_Checkbox);
            Presentation_MovieControl_LiveButtonPanel.Controls.Add(this.liveMediaControls);
            Presentation_MovieControl_LiveButtonPanel.Dock = System.Windows.Forms.DockStyle.Left;
            Presentation_MovieControl_LiveButtonPanel.Location = new System.Drawing.Point(0, 0);
            Presentation_MovieControl_LiveButtonPanel.Name = "Presentation_MovieControl_LiveButtonPanel";
            Presentation_MovieControl_LiveButtonPanel.Size = new System.Drawing.Size(234, 107);
            Presentation_MovieControl_LiveButtonPanel.TabIndex = 6;
            // 
            // Presentation_MediaLoop_Checkbox
            // 
            this.Presentation_MediaLoop_Checkbox.Checked = true;
            this.Presentation_MediaLoop_Checkbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Presentation_MediaLoop_Checkbox.Location = new System.Drawing.Point(5, 0);
            this.Presentation_MediaLoop_Checkbox.Name = "Presentation_MediaLoop_Checkbox";
            this.Presentation_MediaLoop_Checkbox.Size = new System.Drawing.Size(59, 24);
            this.Presentation_MediaLoop_Checkbox.TabIndex = 4;
            this.Presentation_MediaLoop_Checkbox.Text = "Loop";
            this.Presentation_MediaLoop_Checkbox.CheckedChanged += new System.EventHandler(this.Presentation_MediaLoop_Checkbox_CheckedChanged);
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
            this.RightDocks_ImageListBox.Size = new System.Drawing.Size(158, 185);
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
            // RightDocks_Songlist_SearchPanel
            // 
            this.RightDocks_Songlist_SearchPanel.Controls.Add(this.pictureBox1);
            this.RightDocks_Songlist_SearchPanel.Controls.Add(this.RightDocks_SongListSearch);
            this.RightDocks_Songlist_SearchPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.RightDocks_Songlist_SearchPanel.Location = new System.Drawing.Point(0, 0);
            this.RightDocks_Songlist_SearchPanel.Name = "RightDocks_Songlist_SearchPanel";
            this.RightDocks_Songlist_SearchPanel.Size = new System.Drawing.Size(158, 23);
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
            this.RightDocks_SongListSearch.Size = new System.Drawing.Size(136, 20);
            this.RightDocks_SongListSearch.TabIndex = 4;
            this.RightDocks_SongListSearch.TextChanged += new System.EventHandler(this.RightDocks_SongListSearch_TextChanged);
            // 
            // RightDocks_SongList_ButtonPanel
            // 
            this.RightDocks_SongList_ButtonPanel.Controls.Add(this.btnRightDocks_SongListDelete);
            this.RightDocks_SongList_ButtonPanel.Controls.Add(this.btnRightDocks_SongList2PlayList);
            this.RightDocks_SongList_ButtonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.RightDocks_SongList_ButtonPanel.Location = new System.Drawing.Point(0, 160);
            this.RightDocks_SongList_ButtonPanel.Name = "RightDocks_SongList_ButtonPanel";
            this.RightDocks_SongList_ButtonPanel.Size = new System.Drawing.Size(158, 20);
            this.RightDocks_SongList_ButtonPanel.TabIndex = 5;
            // 
            // btnRightDocks_SongListDelete
            // 
            this.btnRightDocks_SongListDelete.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnRightDocks_SongListDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnRightDocks_SongListDelete.Location = new System.Drawing.Point(-3, 1);
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
            this.btnRightDocks_SongList2PlayList.Location = new System.Drawing.Point(58, 1);
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
            this.RightDocks_PlayList.Size = new System.Drawing.Size(158, 158);
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
            this.RightDocks_TopPanel_PlayList_Button_Panel.Location = new System.Drawing.Point(0, 160);
            this.RightDocks_TopPanel_PlayList_Button_Panel.Name = "RightDocks_TopPanel_PlayList_Button_Panel";
            this.RightDocks_TopPanel_PlayList_Button_Panel.Size = new System.Drawing.Size(158, 20);
            this.RightDocks_TopPanel_PlayList_Button_Panel.TabIndex = 8;
            // 
            // RightDocks_PlayList_Down_Button
            // 
            this.RightDocks_PlayList_Down_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RightDocks_PlayList_Down_Button.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.RightDocks_PlayList_Down_Button.Location = new System.Drawing.Point(116, 2);
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
            this.RightDocks_PlayList_Up_Button.Location = new System.Drawing.Point(76, 2);
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
            this.RightDocks_PlayList_Remove_Button.Location = new System.Drawing.Point(38, 2);
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
            this.RightDocks_FolderDropdown.Size = new System.Drawing.Size(158, 21);
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
            this.ToolBars_bottomSandBarDock.Location = new System.Drawing.Point(0, 582);
            this.ToolBars_bottomSandBarDock.Manager = this.ToolBars_sandBarManager1;
            this.ToolBars_bottomSandBarDock.Name = "ToolBars_bottomSandBarDock";
            this.ToolBars_bottomSandBarDock.Size = new System.Drawing.Size(853, 0);
            this.ToolBars_bottomSandBarDock.TabIndex = 20;
            // 
            // ToolBars_leftSandBarDock
            // 
            this.ToolBars_leftSandBarDock.Controls.Add(this.ToolBars_ComponentBar);
            this.ToolBars_leftSandBarDock.Dock = System.Windows.Forms.DockStyle.Left;
            this.ToolBars_leftSandBarDock.Location = new System.Drawing.Point(0, 50);
            this.ToolBars_leftSandBarDock.Manager = this.ToolBars_sandBarManager1;
            this.ToolBars_leftSandBarDock.Name = "ToolBars_leftSandBarDock";
            this.ToolBars_leftSandBarDock.Size = new System.Drawing.Size(72, 532);
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
            this.ToolBars_ComponentBar.Size = new System.Drawing.Size(72, 530);
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
            this.ToolBars_topSandBarDock.Size = new System.Drawing.Size(853, 50);
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
            this.ToolBars_MainToolbar.Size = new System.Drawing.Size(851, 26);
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
            this.ToolBars_MenuBar_File_Import,
            this.ToolBars_MenuBar_File_Exit});
            this.ToolBars_MenuBar_File.Tag = null;
            this.ToolBars_MenuBar_File.Text = "&File";
            // 
            // ToolBars_MenuBar_File_Import
            // 
            this.ToolBars_MenuBar_File_Import.Icon = null;
            this.ToolBars_MenuBar_File_Import.MenuItems.AddRange(new TD.SandBar.MenuButtonItem[] {
            this.ToolBars_MenuBar_Import_Song});
            this.ToolBars_MenuBar_File_Import.Shortcut = System.Windows.Forms.Shortcut.None;
            this.ToolBars_MenuBar_File_Import.Tag = null;
            this.ToolBars_MenuBar_File_Import.Text = "Import";
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
            this.ToolBars_MenuBar_Song_Rename,
            this.menuButtonItem1});
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
            // menuButtonItem1
            // 
            this.menuButtonItem1.Icon = null;
            this.menuButtonItem1.Shortcut = System.Windows.Forms.Shortcut.None;
            this.menuButtonItem1.Tag = null;
            this.menuButtonItem1.Text = "menuButtonItem1";
            this.menuButtonItem1.Activate += new TD.SandBar.MenuButtonItem.ActivateEventHandler(this.menuButtonItem1_Activate);
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
            this.ToolBars_MenuBar_View_Multimedia,
            this.ToolBars_MenuBar_View_SermonTool,
            this.ToolBars_MenuBar_View_BibleText,
            this.ToolBars_MenuBar_Open_SongsTab,
            this.ToolBars_MenuBar_Open_PlaylistTab,
            this.ToolBars_MenuBar_Open_BibleToolsTab,
            this.ToolBars_MenuBar_Open_SongToolsTab,
            this.ToolBars_MenuBar_Open_BackgroundsTab,
            this.ToolBars_MenuBar_Open_MediaListTab,
            this.ToolBars_MenuBar_Open_MediaTab,
            this.ToolBars_MenuBar_Open_PreviewTab,
            this.ToolBars_MenuBar_Open_LiveTab,
            this.ToolBars_MenuBar_Open_DesignTab});
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
            // ToolBars_MenuBar_View_Multimedia
            // 
            this.ToolBars_MenuBar_View_Multimedia.Icon = ((System.Drawing.Icon)(resources.GetObject("ToolBars_MenuBar_View_Multimedia.Icon")));
            this.ToolBars_MenuBar_View_Multimedia.Shortcut = System.Windows.Forms.Shortcut.F11;
            this.ToolBars_MenuBar_View_Multimedia.Tag = null;
            this.ToolBars_MenuBar_View_Multimedia.Text = "Multimedia";
            this.ToolBars_MenuBar_View_Multimedia.Activate += new TD.SandBar.MenuButtonItem.ActivateEventHandler(this.ToolBars_MenuBar_View_Presentation_Activate);
            // 
            // ToolBars_MenuBar_View_SermonTool
            // 
            this.ToolBars_MenuBar_View_SermonTool.Icon = ((System.Drawing.Icon)(resources.GetObject("ToolBars_MenuBar_View_SermonTool.Icon")));
            this.ToolBars_MenuBar_View_SermonTool.Shortcut = System.Windows.Forms.Shortcut.F12;
            this.ToolBars_MenuBar_View_SermonTool.Tag = null;
            this.ToolBars_MenuBar_View_SermonTool.Text = "Sermon Tool";
            this.ToolBars_MenuBar_View_SermonTool.Activate += new TD.SandBar.MenuButtonItem.ActivateEventHandler(this.ToolBars_MenuBar_View_TextTool_Activate);
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
            // ToolBars_MenuBar_Open_DesignTab
            // 
            this.ToolBars_MenuBar_Open_DesignTab.BeginGroup = true;
            this.ToolBars_MenuBar_Open_DesignTab.Icon = null;
            this.ToolBars_MenuBar_Open_DesignTab.Shortcut = System.Windows.Forms.Shortcut.None;
            this.ToolBars_MenuBar_Open_DesignTab.Tag = null;
            this.ToolBars_MenuBar_Open_DesignTab.Text = "Design tab";
            this.ToolBars_MenuBar_Open_DesignTab.Activate += new TD.SandBar.MenuButtonItem.ActivateEventHandler(this.ToolBars_MenuBar_Open_DesignTab_Activate);
            // 
            // ToolBars_MenuBar_Help
            // 
            this.ToolBars_MenuBar_Help.Icon = null;
            this.ToolBars_MenuBar_Help.MenuItems.AddRange(new TD.SandBar.MenuButtonItem[] {
            this.ToolBars_MenuBar_Help_Intro,
            this.ToolBars_MenuBar_Help_About});
            this.ToolBars_MenuBar_Help.Tag = null;
            this.ToolBars_MenuBar_Help.Text = "&Help";
            // 
            // ToolBars_MenuBar_Help_Intro
            // 
            this.ToolBars_MenuBar_Help_Intro.BeginGroup = true;
            this.ToolBars_MenuBar_Help_Intro.Icon = null;
            this.ToolBars_MenuBar_Help_Intro.Shortcut = System.Windows.Forms.Shortcut.None;
            this.ToolBars_MenuBar_Help_Intro.Tag = null;
            this.ToolBars_MenuBar_Help_Intro.Text = "Help Documentation";
            this.ToolBars_MenuBar_Help_Intro.Activate += new TD.SandBar.MenuButtonItem.ActivateEventHandler(this.HelpIntro_Activate);
            // 
            // ToolBars_MenuBar_Help_About
            // 
            this.ToolBars_MenuBar_Help_About.BeginGroup = true;
            this.ToolBars_MenuBar_Help_About.Icon = null;
            this.ToolBars_MenuBar_Help_About.Shortcut = System.Windows.Forms.Shortcut.None;
            this.ToolBars_MenuBar_Help_About.Tag = null;
            this.ToolBars_MenuBar_Help_About.Text = "About";
            this.ToolBars_MenuBar_Help_About.Activate += new TD.SandBar.MenuButtonItem.ActivateEventHandler(this.AboutButton_Activate);
            // 
            // statusBar
            // 
            this.statusBar.Controls.Add(this.RenderStatus);
            this.statusBar.Location = new System.Drawing.Point(72, 560);
            this.statusBar.Name = "statusBar";
            this.statusBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.StatusPanel,
            this.statusBarUpdatePanel});
            this.statusBar.ShowPanels = true;
            this.statusBar.Size = new System.Drawing.Size(391, 22);
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
            this.StatusPanel.Width = 330;
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
            this.SongShow_CollapsPanel.Size = new System.Drawing.Size(224, 268);
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
            this.SongShow_HideElementsPanel.Size = new System.Drawing.Size(216, 116);
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
            this.panel3.Size = new System.Drawing.Size(208, 28);
            this.panel3.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.SongShow_HideText_Button);
            this.panel1.Location = new System.Drawing.Point(5, 55);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(208, 28);
            this.panel1.TabIndex = 4;
            // 
            // SongShow_HideElementsSub1Panel
            // 
            this.SongShow_HideElementsSub1Panel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.SongShow_HideElementsSub1Panel.Controls.Add(this.SongShow_HideTitle_Button);
            this.SongShow_HideElementsSub1Panel.Location = new System.Drawing.Point(5, 25);
            this.SongShow_HideElementsSub1Panel.Name = "SongShow_HideElementsSub1Panel";
            this.SongShow_HideElementsSub1Panel.Size = new System.Drawing.Size(208, 28);
            this.SongShow_HideElementsSub1Panel.TabIndex = 3;
            // 
            // BibleBookmarks_CollapsiblePanel
            // 
            this.BibleBookmarks_CollapsiblePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.BibleBookmarks_CollapsiblePanel.BackColor = System.Drawing.Color.AliceBlue;
            this.BibleBookmarks_CollapsiblePanel.Controls.Add(this.BibleText_Bookmarks);
            this.BibleBookmarks_CollapsiblePanel.EndColour = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(212)))), ((int)(((byte)(247)))));
            this.BibleBookmarks_CollapsiblePanel.Image = null;
            this.BibleBookmarks_CollapsiblePanel.Location = new System.Drawing.Point(8, 121);
            this.BibleBookmarks_CollapsiblePanel.Name = "BibleBookmarks_CollapsiblePanel";
            this.BibleBookmarks_CollapsiblePanel.PanelState = Salamander.Windows.Forms.PanelState.Expanded;
            this.BibleBookmarks_CollapsiblePanel.Size = new System.Drawing.Size(191, 209);
            this.BibleBookmarks_CollapsiblePanel.StartColour = System.Drawing.Color.White;
            this.BibleBookmarks_CollapsiblePanel.TabIndex = 1;
            this.BibleBookmarks_CollapsiblePanel.TitleFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BibleBookmarks_CollapsiblePanel.TitleFontColour = System.Drawing.Color.Navy;
            this.BibleBookmarks_CollapsiblePanel.TitleText = "Bookmarks";
            // 
            // BibleText_Bookmarks
            // 
            this.BibleText_Bookmarks.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BibleText_Bookmarks.Location = new System.Drawing.Point(0, 23);
            this.BibleText_Bookmarks.Name = "BibleText_Bookmarks";
            this.BibleText_Bookmarks.Size = new System.Drawing.Size(191, 186);
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
            this.BibleTranslations_CollapsiblePanel.Size = new System.Drawing.Size(191, 105);
            this.BibleTranslations_CollapsiblePanel.StartColour = System.Drawing.Color.White;
            this.BibleTranslations_CollapsiblePanel.TabIndex = 0;
            this.BibleTranslations_CollapsiblePanel.TitleFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BibleTranslations_CollapsiblePanel.TitleFontColour = System.Drawing.Color.Navy;
            this.BibleTranslations_CollapsiblePanel.TitleText = "Translations";
            // 
            // BibleText_Translations
            // 
            this.BibleText_Translations.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BibleText_Translations.Location = new System.Drawing.Point(0, 23);
            this.BibleText_Translations.Name = "BibleText_Translations";
            this.BibleText_Translations.Size = new System.Drawing.Size(191, 82);
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
            this.leftSandDock.Size = new System.Drawing.Size(0, 532);
            this.leftSandDock.TabIndex = 23;
            // 
            // rightSandDock
            // 
            this.rightSandDock.Controls.Add(this.DockControl_PreviewScreen);
            this.rightSandDock.Controls.Add(this.DockControl_LiveScreen);
            this.rightSandDock.Controls.Add(this.Dock_SongTools);
            this.rightSandDock.Controls.Add(this.DockControl_BibleTools);
            this.rightSandDock.Controls.Add(this.DockControl_DesignEditor);
            this.rightSandDock.Controls.Add(this.DockControl_Songs);
            this.rightSandDock.Controls.Add(this.DockControl_PlayList);
            this.rightSandDock.Controls.Add(this.DockControl_Backgrounds);
            this.rightSandDock.Controls.Add(this.DockControl_Media);
            this.rightSandDock.Controls.Add(this.DockControl_MediaLists);
            this.rightSandDock.Dock = System.Windows.Forms.DockStyle.Right;
            this.rightSandDock.Guid = new System.Guid("a6039876-f9a8-471e-b56f-5b1bf7264f06");
            this.rightSandDock.LayoutSystem = new TD.SandDock.SplitLayoutSystem(250, 400, System.Windows.Forms.Orientation.Horizontal, new TD.SandDock.LayoutSystemBase[] {
            ((TD.SandDock.LayoutSystemBase)(new TD.SandDock.SplitLayoutSystem(386, 532, System.Windows.Forms.Orientation.Vertical, new TD.SandDock.LayoutSystemBase[] {
                        ((TD.SandDock.LayoutSystemBase)(new TD.SandDock.SplitLayoutSystem(223, 532, System.Windows.Forms.Orientation.Horizontal, new TD.SandDock.LayoutSystemBase[] {
                                    ((TD.SandDock.LayoutSystemBase)(new TD.SandDock.ControlLayoutSystem(224, 212, new TD.SandDock.DockControl[] {
                                                this.DockControl_PreviewScreen,
                                                this.DockControl_LiveScreen}, this.DockControl_PreviewScreen))),
                                    ((TD.SandDock.LayoutSystemBase)(new TD.SandDock.ControlLayoutSystem(224, 315, new TD.SandDock.DockControl[] {
                                                this.Dock_SongTools,
                                                this.DockControl_BibleTools,
                                                this.DockControl_DesignEditor}, this.DockControl_DesignEditor)))}))),
                        ((TD.SandDock.LayoutSystemBase)(new TD.SandDock.SplitLayoutSystem(158, 532, System.Windows.Forms.Orientation.Horizontal, new TD.SandDock.LayoutSystemBase[] {
                                    ((TD.SandDock.LayoutSystemBase)(new TD.SandDock.ControlLayoutSystem(158, 228, new TD.SandDock.DockControl[] {
                                                this.DockControl_Songs,
                                                this.DockControl_PlayList}, this.DockControl_PlayList))),
                                    ((TD.SandDock.LayoutSystemBase)(new TD.SandDock.ControlLayoutSystem(158, 299, new TD.SandDock.DockControl[] {
                                                this.DockControl_Backgrounds,
                                                this.DockControl_Media,
                                                this.DockControl_MediaLists}, this.DockControl_Backgrounds)))})))})))});
            this.rightSandDock.Location = new System.Drawing.Point(463, 50);
            this.rightSandDock.Manager = this.sandDockManager1;
            this.rightSandDock.MaximumSize = 600;
            this.rightSandDock.Name = "rightSandDock";
            this.rightSandDock.Size = new System.Drawing.Size(390, 532);
            this.rightSandDock.TabIndex = 24;
            // 
            // DockControl_PreviewScreen
            // 
            this.DockControl_PreviewScreen.Controls.Add(this.RightDocks_PreviewScreen_PictureBox);
            this.DockControl_PreviewScreen.Controls.Add(this.panel5);
            this.DockControl_PreviewScreen.Guid = new System.Guid("8a7e4f9a-a6a1-48b7-a273-8494de07e6b2");
            this.DockControl_PreviewScreen.Location = new System.Drawing.Point(4, 25);
            this.DockControl_PreviewScreen.Name = "DockControl_PreviewScreen";
            this.DockControl_PreviewScreen.Size = new System.Drawing.Size(224, 164);
            this.DockControl_PreviewScreen.TabImage = ((System.Drawing.Image)(resources.GetObject("DockControl_PreviewScreen.TabImage")));
            this.DockControl_PreviewScreen.TabIndex = 1;
            this.DockControl_PreviewScreen.Text = "Preview";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel5.Controls.Add(this.RightDocks_Preview_Next);
            this.panel5.Controls.Add(this.RightDocks_Preview_GoLive);
            this.panel5.Controls.Add(this.RightDocks_Preview_Prev);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 134);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(224, 30);
            this.panel5.TabIndex = 1;
            // 
            // RightDocks_Preview_Next
            // 
            this.RightDocks_Preview_Next.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.RightDocks_Preview_Next.BackColor = System.Drawing.SystemColors.Control;
            this.RightDocks_Preview_Next.Location = new System.Drawing.Point(172, 6);
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
            this.RightDocks_Preview_GoLive.Size = new System.Drawing.Size(83, 20);
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
            // DockControl_LiveScreen
            // 
            this.DockControl_LiveScreen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.DockControl_LiveScreen.Controls.Add(this.RightDocks_LiveScreen_PictureBox);
            this.DockControl_LiveScreen.Controls.Add(this.panel9);
            this.DockControl_LiveScreen.Guid = new System.Guid("e37c7ea7-844c-4a80-baba-1b5d7ddb42e7");
            this.DockControl_LiveScreen.Location = new System.Drawing.Point(4, 25);
            this.DockControl_LiveScreen.Name = "DockControl_LiveScreen";
            this.DockControl_LiveScreen.Size = new System.Drawing.Size(224, 164);
            this.DockControl_LiveScreen.TabImage = ((System.Drawing.Image)(resources.GetObject("DockControl_LiveScreen.TabImage")));
            this.DockControl_LiveScreen.TabIndex = 2;
            this.DockControl_LiveScreen.Text = "Live";
            // 
            // RightDocks_LiveScreen_PictureBox
            // 
            this.RightDocks_LiveScreen_PictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RightDocks_LiveScreen_PictureBox.Location = new System.Drawing.Point(0, 0);
            this.RightDocks_LiveScreen_PictureBox.Name = "RightDocks_LiveScreen_PictureBox";
            this.RightDocks_LiveScreen_PictureBox.Size = new System.Drawing.Size(224, 134);
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
            this.panel9.Location = new System.Drawing.Point(0, 134);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(224, 30);
            this.panel9.TabIndex = 1;
            // 
            // RightDocks_Live_Next
            // 
            this.RightDocks_Live_Next.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.RightDocks_Live_Next.BackColor = System.Drawing.SystemColors.Control;
            this.RightDocks_Live_Next.Location = new System.Drawing.Point(144, 6);
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
            this.Dock_SongTools.Location = new System.Drawing.Point(4, 241);
            this.Dock_SongTools.Name = "Dock_SongTools";
            this.Dock_SongTools.Size = new System.Drawing.Size(224, 268);
            this.Dock_SongTools.TabIndex = 7;
            this.Dock_SongTools.Text = "Song Tools";
            // 
            // DockControl_BibleTools
            // 
            this.DockControl_BibleTools.Controls.Add(this.BibleTools_CollapsiblePanelBar);
            this.DockControl_BibleTools.Guid = new System.Guid("226af261-a2b0-437c-8c85-a1c4d4b57298");
            this.DockControl_BibleTools.Location = new System.Drawing.Point(4, 241);
            this.DockControl_BibleTools.Name = "DockControl_BibleTools";
            this.DockControl_BibleTools.Size = new System.Drawing.Size(224, 268);
            this.DockControl_BibleTools.TabImage = ((System.Drawing.Image)(resources.GetObject("DockControl_BibleTools.TabImage")));
            this.DockControl_BibleTools.TabIndex = 6;
            this.DockControl_BibleTools.Text = "Bible Tools";
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
            this.BibleTools_CollapsiblePanelBar.Size = new System.Drawing.Size(224, 268);
            this.BibleTools_CollapsiblePanelBar.Spacing = 8;
            this.BibleTools_CollapsiblePanelBar.TabIndex = 2;
            // 
            // DockControl_DesignEditor
            // 
            this.DockControl_DesignEditor.Controls.Add(this.DesignTabControl);
            this.DockControl_DesignEditor.Guid = new System.Guid("69c6f3d1-3cc1-404c-ae1c-cf6db365a077");
            this.DockControl_DesignEditor.Location = new System.Drawing.Point(4, 241);
            this.DockControl_DesignEditor.Name = "DockControl_DesignEditor";
            this.DockControl_DesignEditor.Size = new System.Drawing.Size(224, 268);
            this.DockControl_DesignEditor.TabIndex = 8;
            this.DockControl_DesignEditor.Text = "Song Design";
            // 
            // DesignTabControl
            // 
            this.DesignTabControl.Controls.Add(this.SongDesignTab);
            this.DesignTabControl.Controls.Add(this.SermonDesignTab);
            this.DesignTabControl.Controls.Add(this.BibleDesignTab);
            this.DesignTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DesignTabControl.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.DesignTabControl.Location = new System.Drawing.Point(0, 0);
            this.DesignTabControl.Name = "DesignTabControl";
            this.DesignTabControl.Padding = new System.Drawing.Point(0, 0);
            this.DesignTabControl.SelectedIndex = 0;
            this.DesignTabControl.Size = new System.Drawing.Size(224, 268);
            this.DesignTabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.DesignTabControl.TabIndex = 3;
            // 
            // SongDesignTab
            // 
            this.SongDesignTab.BackColor = System.Drawing.Color.AntiqueWhite;
            this.SongDesignTab.Controls.Add(this.songThemeWidget);
            this.SongDesignTab.Location = new System.Drawing.Point(4, 22);
            this.SongDesignTab.Name = "SongDesignTab";
            this.SongDesignTab.Padding = new System.Windows.Forms.Padding(3);
            this.SongDesignTab.Size = new System.Drawing.Size(216, 242);
            this.SongDesignTab.TabIndex = 0;
            this.SongDesignTab.SizeChanged += new System.EventHandler(this.SongDesignTab_SizeChanged);
            // 
            // SermonDesignTab
            // 
            this.SermonDesignTab.BackColor = System.Drawing.Color.AntiqueWhite;
            this.SermonDesignTab.Controls.Add(this.sermonThemeWidget);
            this.SermonDesignTab.Location = new System.Drawing.Point(4, 22);
            this.SermonDesignTab.Name = "SermonDesignTab";
            this.SermonDesignTab.Padding = new System.Windows.Forms.Padding(3);
            this.SermonDesignTab.Size = new System.Drawing.Size(216, 242);
            this.SermonDesignTab.TabIndex = 1;
            this.SermonDesignTab.SizeChanged += new System.EventHandler(this.SermonDesignTab_SizeChanged);
            // 
            // BibleDesignTab
            // 
            this.BibleDesignTab.BackColor = System.Drawing.Color.AntiqueWhite;
            this.BibleDesignTab.Controls.Add(this.bibleThemeWidget);
            this.BibleDesignTab.Location = new System.Drawing.Point(4, 22);
            this.BibleDesignTab.Name = "BibleDesignTab";
            this.BibleDesignTab.Padding = new System.Windows.Forms.Padding(3);
            this.BibleDesignTab.Size = new System.Drawing.Size(216, 242);
            this.BibleDesignTab.TabIndex = 2;
            this.BibleDesignTab.SizeChanged += new System.EventHandler(this.BibleDesignTab_SizeChanged);
            // 
            // DockControl_Songs
            // 
            this.DockControl_Songs.Controls.Add(this.SongList_Tree);
            this.DockControl_Songs.Controls.Add(this.RightDocks_Songlist_SearchPanel);
            this.DockControl_Songs.Controls.Add(this.RightDocks_SongList_ButtonPanel);
            this.DockControl_Songs.Guid = new System.Guid("6044bb67-05ab-4617-bbfa-99e49388b41f");
            this.DockControl_Songs.Location = new System.Drawing.Point(232, 25);
            this.DockControl_Songs.Name = "DockControl_Songs";
            this.DockControl_Songs.Size = new System.Drawing.Size(158, 180);
            this.DockControl_Songs.TabImage = ((System.Drawing.Image)(resources.GetObject("DockControl_Songs.TabImage")));
            this.DockControl_Songs.TabIndex = 0;
            this.DockControl_Songs.Text = "Songs";
            // 
            // DockControl_PlayList
            // 
            this.DockControl_PlayList.Controls.Add(this.RightDocks_PlayList);
            this.DockControl_PlayList.Controls.Add(this.RightDocks_TopPanel_PlayList_Button_Panel);
            this.DockControl_PlayList.Guid = new System.Guid("92186926-e7f9-4850-98b8-190a99f81ea6");
            this.DockControl_PlayList.Location = new System.Drawing.Point(232, 25);
            this.DockControl_PlayList.Name = "DockControl_PlayList";
            this.DockControl_PlayList.Size = new System.Drawing.Size(158, 180);
            this.DockControl_PlayList.TabImage = ((System.Drawing.Image)(resources.GetObject("DockControl_PlayList.TabImage")));
            this.DockControl_PlayList.TabIndex = 2;
            this.DockControl_PlayList.Text = "Playlist";
            // 
            // DockControl_Backgrounds
            // 
            this.DockControl_Backgrounds.Controls.Add(this.panel2);
            this.DockControl_Backgrounds.Controls.Add(this.panel6);
            this.DockControl_Backgrounds.Controls.Add(this.RightDocks_BottomPanel2_TopPanel);
            this.DockControl_Backgrounds.Guid = new System.Guid("b561dd7f-3e79-4e07-912c-18ac9600db75");
            this.DockControl_Backgrounds.Location = new System.Drawing.Point(232, 257);
            this.DockControl_Backgrounds.Name = "DockControl_Backgrounds";
            this.DockControl_Backgrounds.Size = new System.Drawing.Size(158, 252);
            this.DockControl_Backgrounds.TabIndex = 1;
            this.DockControl_Backgrounds.Text = "Backgrounds";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.RightDocks_ImageListBox);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 24);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(158, 204);
            this.panel2.TabIndex = 25;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.RightDocks_Backgrounds_UseDefault);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel6.Location = new System.Drawing.Point(0, 228);
            this.panel6.Name = "panel6";
            this.panel6.Padding = new System.Windows.Forms.Padding(2);
            this.panel6.Size = new System.Drawing.Size(158, 24);
            this.panel6.TabIndex = 24;
            // 
            // RightDocks_Backgrounds_UseDefault
            // 
            this.RightDocks_Backgrounds_UseDefault.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.RightDocks_Backgrounds_UseDefault.Location = new System.Drawing.Point(41, 3);
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
            this.RightDocks_BottomPanel2_TopPanel.Size = new System.Drawing.Size(158, 24);
            this.RightDocks_BottomPanel2_TopPanel.TabIndex = 22;
            // 
            // DockControl_Media
            // 
            this.DockControl_Media.Controls.Add(this.RightDocks_BottomPanel_MediaList);
            this.DockControl_Media.Controls.Add(this.RightDocks_BottomPanel_Media_Bottom);
            this.DockControl_Media.Controls.Add(this.RightDocks_BottomPanel_Media_Top);
            this.DockControl_Media.Guid = new System.Guid("c9d617ca-0165-45b7-8e07-329a81273abc");
            this.DockControl_Media.Location = new System.Drawing.Point(232, 257);
            this.DockControl_Media.Name = "DockControl_Media";
            this.DockControl_Media.Size = new System.Drawing.Size(158, 252);
            this.DockControl_Media.TabImage = ((System.Drawing.Image)(resources.GetObject("DockControl_Media.TabImage")));
            this.DockControl_Media.TabIndex = 4;
            this.DockControl_Media.Text = "Media";
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
            this.RightDocks_BottomPanel_MediaList.Size = new System.Drawing.Size(158, 154);
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
            this.RightDocks_BottomPanel_Media_Bottom.Location = new System.Drawing.Point(0, 196);
            this.RightDocks_BottomPanel_Media_Bottom.Name = "RightDocks_BottomPanel_Media_Bottom";
            this.RightDocks_BottomPanel_Media_Bottom.Size = new System.Drawing.Size(158, 56);
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
            this.RightDocks_BottomPanel_Media_Top.Size = new System.Drawing.Size(158, 32);
            this.RightDocks_BottomPanel_Media_Top.TabIndex = 4;
            // 
            // RightDocks_BottomPanel_Media_FadePanelButton
            // 
            this.RightDocks_BottomPanel_Media_FadePanelButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.RightDocks_BottomPanel_Media_FadePanelButton.Location = new System.Drawing.Point(23, 5);
            this.RightDocks_BottomPanel_Media_FadePanelButton.Name = "RightDocks_BottomPanel_Media_FadePanelButton";
            this.RightDocks_BottomPanel_Media_FadePanelButton.Size = new System.Drawing.Size(136, 23);
            this.RightDocks_BottomPanel_Media_FadePanelButton.TabIndex = 2;
            this.RightDocks_BottomPanel_Media_FadePanelButton.Text = "Add Media...";
            this.RightDocks_BottomPanel_Media_FadePanelButton.Click += new System.EventHandler(this.RightDocks_BottomPanel_Media_FadePanelButton_Click);
            // 
            // DockControl_MediaLists
            // 
            this.DockControl_MediaLists.BackColor = System.Drawing.SystemColors.Control;
            this.DockControl_MediaLists.Controls.Add(this.RightDocks_BottomPanel_MediaListsTopPanel);
            this.DockControl_MediaLists.Controls.Add(this.RightDocks_BottomPanel_MediaLists_BottomPanel);
            this.DockControl_MediaLists.Guid = new System.Guid("3429dfd5-f5ba-4785-ac79-49140d88b66b");
            this.DockControl_MediaLists.Location = new System.Drawing.Point(232, 257);
            this.DockControl_MediaLists.Name = "DockControl_MediaLists";
            this.DockControl_MediaLists.Size = new System.Drawing.Size(158, 252);
            this.DockControl_MediaLists.TabIndex = 5;
            this.DockControl_MediaLists.Text = "MediaLists";
            // 
            // RightDocks_BottomPanel_MediaListsTopPanel
            // 
            this.RightDocks_BottomPanel_MediaListsTopPanel.Controls.Add(this.RightDocks_MediaLists);
            this.RightDocks_BottomPanel_MediaListsTopPanel.Controls.Add(this.RightDocks_BottomPanel_MediaListsTop_Control_Panel);
            this.RightDocks_BottomPanel_MediaListsTopPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RightDocks_BottomPanel_MediaListsTopPanel.Location = new System.Drawing.Point(0, 0);
            this.RightDocks_BottomPanel_MediaListsTopPanel.Name = "RightDocks_BottomPanel_MediaListsTopPanel";
            this.RightDocks_BottomPanel_MediaListsTopPanel.Size = new System.Drawing.Size(158, 152);
            this.RightDocks_BottomPanel_MediaListsTopPanel.TabIndex = 1;
            // 
            // RightDocks_MediaLists
            // 
            this.RightDocks_MediaLists.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RightDocks_MediaLists.Location = new System.Drawing.Point(0, 0);
            this.RightDocks_MediaLists.Name = "RightDocks_MediaLists";
            this.RightDocks_MediaLists.Size = new System.Drawing.Size(158, 121);
            this.RightDocks_MediaLists.TabIndex = 1;
            this.RightDocks_MediaLists.DoubleClick += new System.EventHandler(this.RightDocks_MediaLists_DoubleClick);
            // 
            // RightDocks_BottomPanel_MediaListsTop_Control_Panel
            // 
            this.RightDocks_BottomPanel_MediaListsTop_Control_Panel.Controls.Add(this.RightDocks_MediaLists_DeleteButton);
            this.RightDocks_BottomPanel_MediaListsTop_Control_Panel.Controls.Add(this.RightDocks_MediaLists_LoadButton);
            this.RightDocks_BottomPanel_MediaListsTop_Control_Panel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.RightDocks_BottomPanel_MediaListsTop_Control_Panel.Location = new System.Drawing.Point(0, 128);
            this.RightDocks_BottomPanel_MediaListsTop_Control_Panel.Name = "RightDocks_BottomPanel_MediaListsTop_Control_Panel";
            this.RightDocks_BottomPanel_MediaListsTop_Control_Panel.Size = new System.Drawing.Size(158, 24);
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
            this.RightDocks_BottomPanel_MediaLists_BottomPanel.Location = new System.Drawing.Point(0, 152);
            this.RightDocks_BottomPanel_MediaLists_BottomPanel.Name = "RightDocks_BottomPanel_MediaLists_BottomPanel";
            this.RightDocks_BottomPanel_MediaLists_BottomPanel.Size = new System.Drawing.Size(158, 100);
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
            // bottomSandDock
            // 
            this.bottomSandDock.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomSandDock.Guid = new System.Guid("11832edf-4b5f-4911-9e7c-da764193d89f");
            this.bottomSandDock.LayoutSystem = new TD.SandDock.SplitLayoutSystem(250, 400);
            this.bottomSandDock.Location = new System.Drawing.Point(72, 582);
            this.bottomSandDock.Manager = this.sandDockManager1;
            this.bottomSandDock.Name = "bottomSandDock";
            this.bottomSandDock.Size = new System.Drawing.Size(391, 0);
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
            this.topSandDock.Size = new System.Drawing.Size(391, 0);
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
            this.PlayProgress.Interval = 500;
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
            this.Sermon_BibleKey.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Sermon_BibleKey_KeyDown);
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
            this.ShowSong_Tab.Size = new System.Drawing.Size(383, 492);
            this.ShowSong_Tab.TabIndex = 2;
            this.ShowSong_Tab.Text = "Show Songs";
            // 
            // SermonTools_Tab
            // 
            this.SermonTools_Tab.Controls.Add(this.Sermon_LeftPanel);
            this.SermonTools_Tab.Controls.Add(this.Sermon_TabControl);
            this.SermonTools_Tab.Location = new System.Drawing.Point(4, 14);
            this.SermonTools_Tab.Name = "SermonTools_Tab";
            this.SermonTools_Tab.Size = new System.Drawing.Size(383, 492);
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
            this.Sermon_LeftPanel.Size = new System.Drawing.Size(239, 492);
            this.Sermon_LeftPanel.TabIndex = 3;
            // 
            // Sermon_LeftDoc_Panel
            // 
            this.Sermon_LeftDoc_Panel.Controls.Add(this.Sermon_DocManager);
            this.Sermon_LeftDoc_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Sermon_LeftDoc_Panel.Location = new System.Drawing.Point(0, 24);
            this.Sermon_LeftDoc_Panel.Name = "Sermon_LeftDoc_Panel";
            this.Sermon_LeftDoc_Panel.Size = new System.Drawing.Size(239, 438);
            this.Sermon_LeftDoc_Panel.TabIndex = 4;
            // 
            // Sermon_DocManager
            // 
            this.Sermon_DocManager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Sermon_DocManager.Location = new System.Drawing.Point(0, 0);
            this.Sermon_DocManager.Name = "Sermon_DocManager";
            this.Sermon_DocManager.Size = new System.Drawing.Size(239, 438);
            this.Sermon_DocManager.TabIndex = 1;
            this.Sermon_DocManager.CloseButtonPressed += new DocumentManager.DocumentManager.CloseButtonPressedEventHandler(this.Sermon_DocManager_CloseButtonPressed);
            // 
            // Sermon_LeftToolBar_Panel
            // 
            this.Sermon_LeftToolBar_Panel.Controls.Add(this.Sermon_ToolBar);
            this.Sermon_LeftToolBar_Panel.Dock = System.Windows.Forms.DockStyle.Top;
            this.Sermon_LeftToolBar_Panel.Location = new System.Drawing.Point(0, 0);
            this.Sermon_LeftToolBar_Panel.Name = "Sermon_LeftToolBar_Panel";
            this.Sermon_LeftToolBar_Panel.Size = new System.Drawing.Size(239, 24);
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
            this.Sermon_ToolBar.Size = new System.Drawing.Size(239, 24);
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
            this.Sermon_LeftBottom_Panel.Location = new System.Drawing.Point(0, 462);
            this.Sermon_LeftBottom_Panel.Name = "Sermon_LeftBottom_Panel";
            this.Sermon_LeftBottom_Panel.Size = new System.Drawing.Size(239, 30);
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
            this.Sermon_BeamBox_Button.Location = new System.Drawing.Point(145, 4);
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
            this.Sermon_TabControl.Location = new System.Drawing.Point(239, 0);
            this.Sermon_TabControl.Name = "Sermon_TabControl";
            this.Sermon_TabControl.SelectedIndex = 0;
            this.Sermon_TabControl.Size = new System.Drawing.Size(144, 492);
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
            this.tabPage3.Size = new System.Drawing.Size(136, 466);
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
            this.Presentation_Tab.Size = new System.Drawing.Size(383, 492);
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
            this.Presentation_FadePanel.Location = new System.Drawing.Point(382, 0);
            this.Presentation_FadePanel.Name = "Presentation_FadePanel";
            this.Presentation_FadePanel.Padding = new System.Windows.Forms.Padding(2);
            this.Presentation_FadePanel.Size = new System.Drawing.Size(1, 492);
            this.Presentation_FadePanel.TabIndex = 3;
            // 
            // Fade_panel
            // 
            this.Fade_panel.Controls.Add(this.Fade_Top_Panel);
            this.Fade_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Fade_panel.Location = new System.Drawing.Point(192, 2);
            this.Fade_panel.Name = "Fade_panel";
            this.Fade_panel.Size = new System.Drawing.Size(0, 486);
            this.Fade_panel.TabIndex = 4;
            // 
            // Fade_Top_Panel
            // 
            this.Fade_Top_Panel.Controls.Add(this.Presentation_Fade_ListView);
            this.Fade_Top_Panel.Controls.Add(this.Presentation_Fade_Refresh_Button);
            this.Fade_Top_Panel.Controls.Add(this.Presentation_Fade_ToPlaylist_Button);
            this.Fade_Top_Panel.Controls.Add(this.Presentation_Fade_preview);
            this.Fade_Top_Panel.Dock = System.Windows.Forms.DockStyle.Top;
            this.Fade_Top_Panel.Location = new System.Drawing.Point(0, 0);
            this.Fade_Top_Panel.Name = "Fade_Top_Panel";
            this.Fade_Top_Panel.Size = new System.Drawing.Size(0, 152);
            this.Fade_Top_Panel.TabIndex = 6;
            // 
            // Presentation_Fade_ListView
            // 
            this.Presentation_Fade_ListView.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.Presentation_Fade_ListView.AllowColumnReorder = true;
            this.Presentation_Fade_ListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Presentation_Fade_ListView.FullRowSelect = true;
            this.Presentation_Fade_ListView.GridLines = true;
            this.Presentation_Fade_ListView.Location = new System.Drawing.Point(0, 0);
            this.Presentation_Fade_ListView.Name = "Presentation_Fade_ListView";
            this.Presentation_Fade_ListView.Size = new System.Drawing.Size(0, 152);
            this.Presentation_Fade_ListView.SmallImageList = this.Presentation_Fade_ImageList;
            this.Presentation_Fade_ListView.TabIndex = 3;
            this.Presentation_Fade_ListView.UseCompatibleStateImageBehavior = false;
            this.Presentation_Fade_ListView.View = System.Windows.Forms.View.List;
            this.Presentation_Fade_ListView.DoubleClick += new System.EventHandler(this.Presentation_Fade_ListView_DoubleClick);
            this.Presentation_Fade_ListView.Click += new System.EventHandler(this.Presentation_Fade_ListView_Click);
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
            treeNode1.Name = "";
            treeNode1.Text = "Node0";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(190, 486);
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
            this.Presentation_MainPanel.Size = new System.Drawing.Size(383, 492);
            this.Presentation_MainPanel.TabIndex = 4;
            // 
            // Presentation_PreviewPanel
            // 
            this.Presentation_PreviewPanel.BackColor = System.Drawing.Color.DarkRed;
            this.Presentation_PreviewPanel.Controls.Add(this.Presentation_VideoPanel);
            this.Presentation_PreviewPanel.Controls.Add(this.axShockwaveFlash);
            this.Presentation_PreviewPanel.Controls.Add(this.Presentation_PreviewBox);
            this.Presentation_PreviewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Presentation_PreviewPanel.Location = new System.Drawing.Point(0, 0);
            this.Presentation_PreviewPanel.Name = "Presentation_PreviewPanel";
            this.Presentation_PreviewPanel.Padding = new System.Windows.Forms.Padding(10);
            this.Presentation_PreviewPanel.Size = new System.Drawing.Size(383, 355);
            this.Presentation_PreviewPanel.TabIndex = 2;
            this.Presentation_PreviewPanel.Resize += new System.EventHandler(this.Presentation_PreviewPanel_Resize);
            // 
            // Presentation_VideoPanel
            // 
            this.Presentation_VideoPanel.BackColor = System.Drawing.Color.Silver;
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
            this.Presentation_PreviewBox.BackColor = System.Drawing.Color.PeachPuff;
            this.Presentation_PreviewBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Presentation_PreviewBox.Location = new System.Drawing.Point(10, 10);
            this.Presentation_PreviewBox.Name = "Presentation_PreviewBox";
            this.Presentation_PreviewBox.Size = new System.Drawing.Size(363, 335);
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
            this.Presentation_MovieControlPanel.Location = new System.Drawing.Point(0, 355);
            this.Presentation_MovieControlPanel.Name = "Presentation_MovieControlPanel";
            this.Presentation_MovieControlPanel.Size = new System.Drawing.Size(383, 137);
            this.Presentation_MovieControlPanel.TabIndex = 3;
            // 
            // Presentation_MovieControlPanelBottom
            // 
            this.Presentation_MovieControlPanelBottom.Controls.Add(Presentation_MovieControl_LiveButtonPanel);
            this.Presentation_MovieControlPanelBottom.Controls.Add(this.Presentation_MovieControl_PreviewButtonPanel);
            this.Presentation_MovieControlPanelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Presentation_MovieControlPanelBottom.Location = new System.Drawing.Point(0, 28);
            this.Presentation_MovieControlPanelBottom.Name = "Presentation_MovieControlPanelBottom";
            this.Presentation_MovieControlPanelBottom.Size = new System.Drawing.Size(346, 107);
            this.Presentation_MovieControlPanelBottom.TabIndex = 4;
            // 
            // Presentation_MovieControl_PreviewButtonPanel
            // 
            this.Presentation_MovieControl_PreviewButtonPanel.Controls.Add(this.previewMediaControls);
            this.Presentation_MovieControl_PreviewButtonPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.Presentation_MovieControl_PreviewButtonPanel.Location = new System.Drawing.Point(112, 0);
            this.Presentation_MovieControl_PreviewButtonPanel.Name = "Presentation_MovieControl_PreviewButtonPanel";
            this.Presentation_MovieControl_PreviewButtonPanel.Size = new System.Drawing.Size(234, 107);
            this.Presentation_MovieControl_PreviewButtonPanel.TabIndex = 3;
            // 
            // Presentation_MovieControlPanel_Top
            // 
            this.Presentation_MovieControlPanel_Top.Controls.Add(this.Media_TrackBar);
            this.Presentation_MovieControlPanel_Top.Dock = System.Windows.Forms.DockStyle.Top;
            this.Presentation_MovieControlPanel_Top.Location = new System.Drawing.Point(0, 0);
            this.Presentation_MovieControlPanel_Top.Name = "Presentation_MovieControlPanel_Top";
            this.Presentation_MovieControlPanel_Top.Size = new System.Drawing.Size(346, 30);
            this.Presentation_MovieControlPanel_Top.TabIndex = 3;
            // 
            // Media_TrackBar
            // 
            this.Media_TrackBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Media_TrackBar.Enabled = false;
            this.Media_TrackBar.Location = new System.Drawing.Point(0, 0);
            this.Media_TrackBar.Name = "Media_TrackBar";
            this.Media_TrackBar.Size = new System.Drawing.Size(346, 30);
            this.Media_TrackBar.TabIndex = 0;
            this.Media_TrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.Media_TrackBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Media_TrackBar_Up);
            // 
            // Presentation_MovieControlPanel_Right
            // 
            this.Presentation_MovieControlPanel_Right.Controls.Add(this.muteButton);
            this.Presentation_MovieControlPanel_Right.Controls.Add(this.AudioBar);
            this.Presentation_MovieControlPanel_Right.Dock = System.Windows.Forms.DockStyle.Right;
            this.Presentation_MovieControlPanel_Right.Location = new System.Drawing.Point(346, 0);
            this.Presentation_MovieControlPanel_Right.Name = "Presentation_MovieControlPanel_Right";
            this.Presentation_MovieControlPanel_Right.Size = new System.Drawing.Size(35, 135);
            this.Presentation_MovieControlPanel_Right.TabIndex = 2;
            // 
            // muteButton
            // 
            this.muteButton.BackgroundImage = global::DreamBeam.Properties.Resources.audio_volume_high;
            this.muteButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.muteButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.muteButton.Enabled = false;
            this.muteButton.FlatAppearance.BorderSize = 0;
            this.muteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.muteButton.Location = new System.Drawing.Point(0, 112);
            this.muteButton.Name = "muteButton";
            this.muteButton.Size = new System.Drawing.Size(35, 23);
            this.muteButton.TabIndex = 1;
            this.muteButton.UseVisualStyleBackColor = true;
            // 
            // AudioBar
            // 
            this.AudioBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.AudioBar.AutoSize = false;
            this.AudioBar.Enabled = false;
            this.AudioBar.Location = new System.Drawing.Point(5, 0);
            this.AudioBar.Maximum = 0;
            this.AudioBar.Minimum = -10000;
            this.AudioBar.Name = "AudioBar";
            this.AudioBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.AudioBar.Size = new System.Drawing.Size(25, 109);
            this.AudioBar.TabIndex = 0;
            this.AudioBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.AudioBar.ValueChanged += new System.EventHandler(this.AudioBar_ValueChanged);
            // 
            // EditSongs_Tab
            // 
            this.EditSongs_Tab.Controls.Add(this.songEditor);
            this.EditSongs_Tab.Location = new System.Drawing.Point(4, 14);
            this.EditSongs_Tab.Name = "EditSongs_Tab";
            this.EditSongs_Tab.Size = new System.Drawing.Size(383, 492);
            this.EditSongs_Tab.TabIndex = 6;
            this.EditSongs_Tab.Text = "Edit Songs";
            // 
            // BibleText_Tab
            // 
            this.BibleText_Tab.Controls.Add(this.bibleTextControl);
            this.BibleText_Tab.Location = new System.Drawing.Point(4, 14);
            this.BibleText_Tab.Name = "BibleText_Tab";
            this.BibleText_Tab.Size = new System.Drawing.Size(383, 492);
            this.BibleText_Tab.TabIndex = 5;
            this.BibleText_Tab.Text = "Bible Text";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.ShowSong_Tab);
            this.tabControl1.Controls.Add(this.EditSongs_Tab);
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
            this.tabControl1.Size = new System.Drawing.Size(391, 510);
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
            // HideRectanglesTimer
            // 
            this.HideRectanglesTimer.Interval = 4000;
            this.HideRectanglesTimer.Tick += new System.EventHandler(this.HideRectanglesTimer_Tick);
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
            this.SongShow_StropheList_ListEx.Size = new System.Drawing.Size(383, 492);
            this.SongShow_StropheList_ListEx.TabIndex = 0;
            this.SongShow_StropheList_ListEx.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.SongShow_StropheList_ListEx_MouseDoubleClick);
            this.SongShow_StropheList_ListEx.SelectedIndexChanged += new System.EventHandler(this.SongShow_StropheList_ListEx_SelectedIndexChanged);
            this.SongShow_StropheList_ListEx.PressIcon += new Lister.ListEx.EventHandler(this.SongShow_StropheList_ListEx_PressIcon);
            // 
            // songEditor
            // 
            this.songEditor.Collections = new string[0];
            this.songEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.songEditor.Location = new System.Drawing.Point(0, 0);
            this.songEditor.Name = "songEditor";
            this.songEditor.Size = new System.Drawing.Size(383, 492);
            this.songEditor.TabIndex = 0;
            // 
            // liveMediaControls
            // 
            this.liveMediaControls.BackColor = System.Drawing.Color.Maroon;
            this.liveMediaControls.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.liveMediaControls.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.liveMediaControls.LabelColor = System.Drawing.Color.White;
            this.liveMediaControls.LabelText = "Live Window";
            this.liveMediaControls.Location = new System.Drawing.Point(0, 26);
            this.liveMediaControls.Name = "liveMediaControls";
            this.liveMediaControls.Size = new System.Drawing.Size(234, 81);
            this.liveMediaControls.TabIndex = 5;
            this.liveMediaControls.MediaButtonPressed += new DreamBeam.MediaControlsChanged(this.liveMediaControls_MediaButtonPressed);
            // 
            // previewMediaControls
            // 
            this.previewMediaControls.BackColor = System.Drawing.SystemColors.Control;
            this.previewMediaControls.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.previewMediaControls.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.previewMediaControls.LabelColor = System.Drawing.SystemColors.ControlText;
            this.previewMediaControls.LabelText = "Preview Window";
            this.previewMediaControls.Location = new System.Drawing.Point(0, 26);
            this.previewMediaControls.Name = "previewMediaControls";
            this.previewMediaControls.Size = new System.Drawing.Size(234, 81);
            this.previewMediaControls.TabIndex = 2;
            this.previewMediaControls.MediaButtonPressed += new DreamBeam.MediaControlsChanged(this.previewMediaControls_MediaButtonPressed);
            // 
            // bibleTextControl
            // 
            this.bibleTextControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bibleTextControl.Location = new System.Drawing.Point(0, 0);
            this.bibleTextControl.Name = "bibleTextControl";
            this.bibleTextControl.Size = new System.Drawing.Size(383, 492);
            this.bibleTextControl.TabIndex = 3;
            // 
            // RightDocks_PreviewScreen_PictureBox
            // 
            this.RightDocks_PreviewScreen_PictureBox.BackColor = System.Drawing.Color.Black;
            this.RightDocks_PreviewScreen_PictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RightDocks_PreviewScreen_PictureBox.ImagePack = null;
            this.RightDocks_PreviewScreen_PictureBox.Location = new System.Drawing.Point(0, 0);
            this.RightDocks_PreviewScreen_PictureBox.Name = "RightDocks_PreviewScreen_PictureBox";
            this.RightDocks_PreviewScreen_PictureBox.RectPosition = ((System.Drawing.RectangleF)(resources.GetObject("RightDocks_PreviewScreen_PictureBox.RectPosition")));
            this.RightDocks_PreviewScreen_PictureBox.ShowRect = false;
            this.RightDocks_PreviewScreen_PictureBox.Size = new System.Drawing.Size(224, 134);
            this.RightDocks_PreviewScreen_PictureBox.TabIndex = 0;
            this.MainForm_ToolTip.SetToolTip(this.RightDocks_PreviewScreen_PictureBox, "RightClick for Large TextPlacement");
            this.RightDocks_PreviewScreen_PictureBox.RectangleChangedEvent += new DreamBeam.RectangleChangeHandler(this.RightDocks_PreviewScreen_PictureBox_RectangleChangedEvent);
            this.RightDocks_PreviewScreen_PictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RightDocks_PreviewScreen_PictureBox_MouseDown);
            this.RightDocks_PreviewScreen_PictureBox.SizeChanged += new System.EventHandler(this.RightDocks_PreviewScreen_PictureBox_SizeChanged);
            // 
            // SongShow_HideAuthor_Button
            // 
            this.SongShow_HideAuthor_Button.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.SongShow_HideAuthor_Button.Appearance = System.Windows.Forms.Appearance.Button;
            this.SongShow_HideAuthor_Button.bottomColor = System.Drawing.Color.DarkBlue;
            this.SongShow_HideAuthor_Button.BottomTransparent = 64;
            this.SongShow_HideAuthor_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SongShow_HideAuthor_Button.LEDColor = System.Drawing.Color.SteelBlue;
            this.SongShow_HideAuthor_Button.Location = new System.Drawing.Point(53, 2);
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
            // SongShow_HideText_Button
            // 
            this.SongShow_HideText_Button.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.SongShow_HideText_Button.Appearance = System.Windows.Forms.Appearance.Button;
            this.SongShow_HideText_Button.bottomColor = System.Drawing.Color.DarkBlue;
            this.SongShow_HideText_Button.BottomTransparent = 64;
            this.SongShow_HideText_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SongShow_HideText_Button.LEDColor = System.Drawing.Color.SteelBlue;
            this.SongShow_HideText_Button.Location = new System.Drawing.Point(53, 2);
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
            // SongShow_HideTitle_Button
            // 
            this.SongShow_HideTitle_Button.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.SongShow_HideTitle_Button.Appearance = System.Windows.Forms.Appearance.Button;
            this.SongShow_HideTitle_Button.bottomColor = System.Drawing.Color.DarkBlue;
            this.SongShow_HideTitle_Button.BottomTransparent = 64;
            this.SongShow_HideTitle_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SongShow_HideTitle_Button.LEDColor = System.Drawing.Color.SteelBlue;
            this.SongShow_HideTitle_Button.Location = new System.Drawing.Point(53, 2);
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
            // songThemeWidget
            // 
            this.songThemeWidget.BGImagePath = "Image";
            this.songThemeWidget.Location = new System.Drawing.Point(31, 0);
            this.songThemeWidget.Name = "songThemeWidget";
            this.songThemeWidget.SetTextPosition = false;
            this.songThemeWidget.Size = new System.Drawing.Size(255, 320);
            this.songThemeWidget.TabIndex = 2;
            this.songThemeWidget.TabNames = new string[] {
        "Title",
        "Verse",
        "Author",
        "Key"};
            this.songThemeWidget.ThemePath = "";
            this.songThemeWidget.UseDesign = false;
            this.songThemeWidget.MouseInsideEvent += new System.EventHandler(this.songThemeWidget_MouseInsideEvent);
            this.songThemeWidget.ControlChangedEvent += new System.EventHandler(this.songThemeWidget_ControlChangedEvent);
            // 
            // sermonThemeWidget
            // 
            this.sermonThemeWidget.BGImagePath = "Image";
            this.sermonThemeWidget.Location = new System.Drawing.Point(29, 0);
            this.sermonThemeWidget.Name = "sermonThemeWidget";
            this.sermonThemeWidget.SetTextPosition = false;
            this.sermonThemeWidget.Size = new System.Drawing.Size(255, 320);
            this.sermonThemeWidget.TabIndex = 1;
            this.sermonThemeWidget.TabNames = new string[] {
        "1st line",
        "Other lines"};
            this.sermonThemeWidget.ThemePath = "";
            this.sermonThemeWidget.UseDesign = false;
            this.sermonThemeWidget.ControlChangedEvent += new System.EventHandler(this.songThemeWidget_ControlChangedEvent);
            // 
            // bibleThemeWidget
            // 
            this.bibleThemeWidget.BGImagePath = "Image";
            this.bibleThemeWidget.Location = new System.Drawing.Point(31, 0);
            this.bibleThemeWidget.Name = "bibleThemeWidget";
            this.bibleThemeWidget.SetTextPosition = false;
            this.bibleThemeWidget.Size = new System.Drawing.Size(255, 320);
            this.bibleThemeWidget.TabIndex = 1;
            this.bibleThemeWidget.TabNames = new string[] {
        "Verse",
        "Reference",
        "Translation"};
            this.bibleThemeWidget.ThemePath = "";
            this.bibleThemeWidget.UseDesign = false;
            this.bibleThemeWidget.ControlChangedEvent += new System.EventHandler(this.songThemeWidget_ControlChangedEvent);
            // 
            // SongList_Tree
            // 
            this.SongList_Tree.AutoBuildTree = true;
            this.SongList_Tree.DataSource = null;
            this.SongList_Tree.DisplayMember = null;
            this.SongList_Tree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SongList_Tree.Location = new System.Drawing.Point(0, 23);
            this.SongList_Tree.Name = "SongList_Tree";
            this.SongList_Tree.Size = new System.Drawing.Size(158, 137);
            this.SongList_Tree.TabIndex = 7;
            this.SongList_Tree.ValueMember = null;
            this.SongList_Tree.MouseClick += new System.Windows.Forms.MouseEventHandler(this.SongList_Tree_MouseClick);
            this.SongList_Tree.DoubleClick += new System.EventHandler(this.SongList_Tree_DoubleClick);
            this.SongList_Tree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.SongList_Tree_AfterSelect);
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(853, 582);
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
            Presentation_MovieControl_LiveButtonPanel.ResumeLayout(false);
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
            this.DockControl_PreviewScreen.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.DockControl_LiveScreen.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RightDocks_LiveScreen_PictureBox)).EndInit();
            this.panel9.ResumeLayout(false);
            this.Dock_SongTools.ResumeLayout(false);
            this.DockControl_BibleTools.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BibleTools_CollapsiblePanelBar)).EndInit();
            this.BibleTools_CollapsiblePanelBar.ResumeLayout(false);
            this.DockControl_DesignEditor.ResumeLayout(false);
            this.DesignTabControl.ResumeLayout(false);
            this.SongDesignTab.ResumeLayout(false);
            this.SermonDesignTab.ResumeLayout(false);
            this.BibleDesignTab.ResumeLayout(false);
            this.DockControl_Songs.ResumeLayout(false);
            this.DockControl_PlayList.ResumeLayout(false);
            this.DockControl_Backgrounds.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.RightDocks_BottomPanel2_TopPanel.ResumeLayout(false);
            this.DockControl_Media.ResumeLayout(false);
            this.RightDocks_BottomPanel_Media_Bottom.ResumeLayout(false);
            this.RightDocks_BottomPanel_Media_Top.ResumeLayout(false);
            this.DockControl_MediaLists.ResumeLayout(false);
            this.RightDocks_BottomPanel_MediaListsTopPanel.ResumeLayout(false);
            this.RightDocks_BottomPanel_MediaListsTop_Control_Panel.ResumeLayout(false);
            this.RightDocks_BottomPanel_MediaLists_BottomPanel.ResumeLayout(false);
            this.RightDocks_BottomPanel_MediaListsBottomPanel_GroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RightDocks_BottomPanel_MediaLists_Numeric)).EndInit();
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
            this.Presentation_MovieControlPanel_Top.ResumeLayout(false);
            this.Presentation_MovieControlPanel_Top.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Media_TrackBar)).EndInit();
            this.Presentation_MovieControlPanel_Right.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.AudioBar)).EndInit();
            this.EditSongs_Tab.ResumeLayout(false);
            this.BibleText_Tab.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GlobalDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SongListTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SongListDataView)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		private MediaControls previewMediaControls;
		private System.Windows.Forms.Button muteButton;
		private MediaControls liveMediaControls;
        private BibleText bibleTextControl;
        private TD.SandBar.MenuButtonItem menuButtonItem1;
        private TD.SandDock.DockControl DockControl_DesignEditor;
        private TD.SandBar.MenuButtonItem ToolBars_MenuBar_Open_DesignTab;
        public ThemeWidget songThemeWidget;
        public System.Windows.Forms.TabPage SongDesignTab;
        public System.Windows.Forms.TabControl DesignTabControl;
        public System.Windows.Forms.TabPage SermonDesignTab;
        public System.Windows.Forms.TabPage BibleDesignTab;
        public ThemeWidget sermonThemeWidget;
        public ThemeWidget bibleThemeWidget;
        private System.Windows.Forms.Timer HideRectanglesTimer;


	}
}