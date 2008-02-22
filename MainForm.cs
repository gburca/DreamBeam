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
	public partial class MainForm : System.Windows.Forms.Form {

		#region Var and Obj Declarations

		#region Own Vars and Object
		public GuiTools GuiTools = null;

		/// <summary>
		/// A prefix for all configuration files. The application can be started with a "--config" option that overrides this name.
		/// </summary>
		public string ConfigSet = "default";
		public Bitmap memoryBitmap = null;
		public ShowBeam ShowBeam = new ShowBeam();
		public Options Options = null;
		public Config Config;
		public MainTab selectedTab = MainTab.ShowSongs;
		public int SongCount = 0;
		public int Song_Edit_Box = 2;
		public bool beamshowed = false;

		private Splash Splash = null;
		public bool LoadingBGThumbs = false;
		//static Thread Thread_LoadMovie = null;

		public DirectoryInfo folder;

		private System.ComponentModel.IContainer components;
		private int indexOfItemUnderMouseToDrag;
		private Rectangle dragBoxFromMouseDown;
		private Point screenOffset;
		private Cursor MyNoDropCursor;
		private Cursor MyNormalCursor;
		public ImageList MediaList = new ImageList();
		public string MediaFile;
		public MediaOperations MediaFile1;
		bool VideoLoaded = false;
		bool VideoProblem = false;
		bool LoadingVideo = false;
		public Microsoft.DirectX.AudioVideoPlayback.Video video = null;
		int iFilmEnded = 0;
		public Language Lang = new Language();
		public System.Drawing.Font EditorFont = null;

		public BibleLib bibles = new BibleLib();
		public EventHandler BibleText_RegEx_ComboBox_EventHandler;

		private Extensions.Set SongCollections = new Extensions.Set();

		#endregion

		#region BibleText
		BibleVersion BibleText_Bible = null;
		private readonly string bibleLibFile;
		#endregion

		#region SermonTools
		string[] BibleBooks = new string[2];
		public AxACTIVEDIATHEKELib.AxActiveDiatheke Diatheke;
		private string Sermon_BibleLang = "en";
		private bool Sermon_ShowBibleTranslation = false;
		private bool SwordProject_Found = false;
		#endregion

		#region Displays
		public Display DisplayPreview;
		public Display DisplayLiveMini;
		public Display DisplayLiveLocal;
		public Display DisplayLiveClient;
		public Display DisplayLiveServer;
		public XmlRpcServer xmlRpcServer;
		#endregion

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
			ToolBars_MenuBar_Help_Intro.Text = Lang.say("Menu.Help.Intro");
			ToolBars_MenuBar_Help_About.Text = Lang.say("Menu.Help.About");
			#endregion

			#region Right
			//Songs
			DockControl_Songs.Text = Lang.say("Right.Songs");
			btnRightDocks_SongListDelete.Text = Lang.say("Right.Songs.Delete");
			btnRightDocks_SongList2PlayList.Text = Lang.say("Right.Songs.Playlist");

			//Playlist
			DockControl_PlayList.Text = Lang.say("Right.Playlist");
			RightDocks_PlayList_Load_Button.Text = Lang.say("Right.Playlist.Load");
			RightDocks_PlayList_Remove_Button.Text = Lang.say("Right.Playlist.Remove");
			RightDocks_PlayList_Up_Button.Text = Lang.say("Right.Playlist.Up");
			RightDocks_PlayList_Down_Button.Text = Lang.say("Right.Playlist.Down");

			// Displays
			DockControl_PreviewScreen.Text = "Preview";
			DockControl_LiveScreen.Text = "Live";

			//Backgrounds
			DockControl_Backgrounds.Text = Lang.say("Right.Backgrounds");

			//Media
			DockControl_Media.Text = Lang.say("Right.Media");
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
			previewMediaControls.LabelText = Lang.say("MediaPresentation.Preview");
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
			foreach (Song song in this.Config.PlayList) {
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
						Song song = Song.DeserializeFrom(fileName, 0, this.Config) as Song;
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
				Song song = Song.DeserializeFrom(songFile, 0, this.Config) as Song;
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
		public void LoadSongShow(Song song) {
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
			ShowBeam.Song.version = Tools.GetAppVersion();
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
			this.songEditor.Song = new Song();
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
			//					InputBoxResult result = InputBox.Show(Lang.say("Message.EnterSongName"), Lang.say("Message.RenameSongTitle",ShowBeam.OldSong.SongName),"", null);
			//					if (result.OK) {
			//						if (result.Text.Length > 0){
			//							if (!System.IO.File.Exists("Songs\\"+result.Text+".xml")) {
			//								System.IO.File.Move("Songs\\"+ShowBeam.OldSong.SongName+".xml", "Songs\\"+result.Text+".xml");
			//								ShowBeam.OldSong.SongName = result.Text;
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
			this.DockControl_PreviewScreen.Open();
		}

		private void ToolBars_MenuBar_Open_LiveTab_Activate(object sender, System.EventArgs e) {
			this.DockControl_LiveScreen.Open();
		}

		private void ToolBars_MenuBar_Open_BackgroundsTab_Activate(object sender, System.EventArgs e) {
			this.DockControl_Backgrounds.Open();
		}

		private void ToolBars_MenuBar_Open_MediaTab_Activate(object sender, System.EventArgs e) {
			this.DockControl_Media.Open();
		}

		private void ToolBars_MenuBar_Open_PlaylistTab_Activate(object sender, System.EventArgs e) {
			this.DockControl_PlayList.Open();
		}

		private void ToolBars_MenuBar_Open_SongsTab_Activate(object sender, System.EventArgs e) {
			this.DockControl_Songs.Open();
		}

		private void ToolBars_MenuBar_Open_MediaListTab_Activate(object sender, System.EventArgs e) {
			this.DockControl_MediaLists.Open();
		}

		private void ToolBars_MenuBar_Open_BibleToolsTab_Activate(object sender, System.EventArgs e) {
			this.DockControl_BibleTools.Open();
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
			about.version = Tools.GetAppVersion();
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
				this.ToolBars_MainToolbar_HideBG.Checked = !this.ToolBars_MainToolbar_HideBG.Checked;
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
			Song s = this.songEditor.Song;

			if (Tools.FileExists(s.FileName)) {
				Song.SerializeTo(s, s.FileName);
				this.ListSongs();
				this.StatusPanel.Text = Lang.say("Status.SongSavedAs", s.FileName);
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

			Song s = this.songEditor.Song;

			if (!Tools.StringIsNullOrEmptyTrim(s.FileName)) {
				this.SaveFileDialog.FileName = s.FileName;
			} else if (!Tools.StringIsNullOrEmptyTrim(s.Title)) {
				this.SaveFileDialog.FileName = s.Title + ".xml";
			} else {
				this.SaveFileDialog.FileName = "New Song.xml";
			}

			if (this.SaveFileDialog.ShowDialog() == DialogResult.OK) {
				s.FileName = this.SaveFileDialog.FileName;
				try {
					Song.SerializeTo(s, this.SaveFileDialog.FileName);
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
			Song song = (Song)Song.DeserializeFrom(FileName, 0, this.Config);
			this.songEditor.Song = song;
			DisplayPreview.SetContent(song);
			this.LoadSongShow(DisplayPreview.content as Song);
			this.SongShow_HideElement_UpdateButtons();
			this.StatusPanel.Text = Lang.say("Status.SongLoaded", Tools.GetRelativePath(DirType.Songs, FileName));
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
			Song song;
			string FileName = GetSelectedSongFileName();

			try {
				song = Song.DeserializeFrom(FileName, 0, this.Config) as Song;
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
					this.LoadSongFromFile((this.Config.PlayList[i] as Song).FileName);
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
				this.StatusPanel.Text = Lang.say("Status.PlaylistSongRemoved", (this.Config.PlayList[index] as Song).FullName);
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
			Song s = DisplayPreview.content as Song;
			if (s != null) {
				s.CurrentLyric = this.SongShow_StropheList_ListEx.SelectedIndex;
				DisplayPreview.UpdateDisplay(false);
			} else if (this.SongShow_StropheList_ListEx.SelectedIndex >= 0) {
				// If we come back from BibleText to the song,
				// DisplayPreview.content will not be a Song and the
				// typecasting above will give us a null, so we need to re-load
				// the song.
				string FileName = this.songEditor.Song.FileName;

				// We can't use GetSelectedSongFileName because the song could
				// have been loaded from the PlayList, or could no longer be
				// selected (if the user selected a Collection after selecting
				// the song).

				//string FileName = GetSelectedSongFileName();
				if (Tools.FileExists(FileName)) {
					Song song = Song.DeserializeFrom(FileName, this.SongShow_StropheList_ListEx.SelectedIndex, this.Config) as Song;
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
				if (Presentation_MovieControlPanel.Size.Height != 0) {
					Presentation_MovieControlPanel.Size = new System.Drawing.Size(Presentation_MovieControlPanel.Size.Width, 0);
				}
			} else {
				int height = 137;
				Media_TrackBar.Enabled = true;
				if (Presentation_MovieControlPanel.Size.Height != height) {
					Presentation_MovieControlPanel.Size = new System.Drawing.Size(Presentation_MovieControlPanel.Size.Width, height);
				}
			}
		}


		/// <summary>on Progress-Timer Tick, set mediaTrackbar to MediaPlayPosition</summary>
		private void PlayProgress_Tick(object sender, System.EventArgs e) {
			if (this.ShowBeam.strMediaPlaying == null) {
				if (MediaList.GetType(MediaFile) == "flash") {
					Media_TrackBar.Value = axShockwaveFlash.FrameNum;
				} else if (MediaList.GetType(MediaFile) == "movie") {
					try {
						Media_TrackBar.Maximum = (int)video.Duration;
						Media_TrackBar.Value = (int)video.CurrentPosition;
					} catch { }
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
				} else if (MediaList.GetType(MediaName) == "movie") {
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


		/// <summary>If Loop Media Checkbox changed, copy it's value to showbeam</summary>
		private void Presentation_MediaLoop_Checkbox_CheckedChanged(object sender, System.EventArgs e) {
			ShowBeam.LoopMedia = Presentation_MediaLoop_Checkbox.Checked;
			ShowBeam.axShockwaveFlash.Loop = Presentation_MediaLoop_Checkbox.Checked;
		}
		#endregion

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

			axShockwaveFlash.SendToBack();
			axShockwaveFlash.Movie = "";
			MediaFile = mediaFile;
			if (MediaList.GetType(MediaFile) == "image") {
				MovieControlPanelWipe("in");
				Presentation_VideoPanel.Hide();
				axShockwaveFlash.Hide();
				Presentation_PreviewBox.Show();
				Presentation_PreviewBox.BringToFront();
				Presentation_PreviewBox.Image = this.ShowBeam.DrawProportionalBitmap(Presentation_PreviewBox.Size, MediaFile);
				this.MediaFile1 = null;
			} else if (MediaList.GetType(MediaFile) == "flash") {
				MovieControlPanelWipe("out");
				AudioBar.Enabled = false;
				Presentation_PreviewBox.Hide();
				Presentation_VideoPanel.Hide();
				axShockwaveFlash.Show();
				axShockwaveFlash.BringToFront();
				axShockwaveFlash.Movie = MediaFile;
				axShockwaveFlash.Playing = false;
				axShockwaveFlash.Stop();
				axShockwaveFlash.FrameNum = 1;
				this.Presentation_Resize();
				this.MediaFile1 = new MediaFlash(axShockwaveFlash);
			} else if (MediaList.GetType(MediaFile) == "movie") {
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
					video.SeekCurrentPosition(0.1, Microsoft.DirectX.AudioVideoPlayback.SeekPositionFlags.AbsolutePositioning);
					this.MediaFile1 = new MediaMovie(video);
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
			MovieControlPanelWipe("out");
			AudioBar.Enabled = false;
			Media_TrackBar.Enabled = false;
			previewMediaControls.LabelText = "Loading...";
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
				this.previewMediaControls.LabelText = Lang.say("MediaPresentation.Preview");
				AudioBar.Enabled = true;
				Media_TrackBar.Enabled = true;
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
				string temp = Path.GetFileName(dir2);
				this.RightDocks_MediaLists.Items.Add(temp.Substring(0, temp.Length - 4));
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
				DockControl_Media.Open();
			}
		}


		/// <summary>Loads the selected MediaList</summary>
		private void RightDocks_MediaLists_LoadButton_Click(object sender, System.EventArgs e) {
			if (this.RightDocks_MediaLists.SelectedIndex >= 0) {
				GuiTools.RightDock.MediaListTools.LoadSelectedMediaList(RightDocks_MediaLists.SelectedItem.ToString());
				DockControl_Media.Open();
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

			if (MediaList.GetType(MediaFile) == "movie" && this.LoadingVideo) {
				this.VideoLoadTimer.Enabled = true;
			} else {
				if (MediaList.GetType(MediaFile) == "flash") {
					try {
						Media_TrackBar.Maximum = axShockwaveFlash.TotalFrames;
						PlayProgress.Enabled = true;
					} catch { return; }
				} else if (MediaList.GetType(MediaFile) == "movie") {
					try {
						Media_TrackBar.Maximum = (int)video.Duration;
						PlayProgress.Enabled = true;
					} catch { }
				} else if (MediaList.GetType(MediaFile) == "image") {
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
				string strTempText = Tools.Diatheke_ConvertEncoding(Diatheke.value);
				//this.StatusPanel.Text = strTempText.Substring(0, Math.Min(strTempText.Length, 200));

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
				DisplayPreview.content.BGImagePath = null;
				DisplayPreview.content.DefaultBackground(Config);
				DisplayPreview.UpdateDisplay(true);
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
			Song s = DisplayPreview.content as Song;
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
			Song s = DisplayPreview.content as Song;
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
			if (this.BibleText_Bible == null) return;
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
			if (BibleText_Translations.Items.Count == 0 || BibleText_Translations.SelectedIndices.Count == 0) return -1;
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
				if (vidx >= 0) {
					DisplayPreview.SetContent(new ABibleVerse(this.BibleText_Bible, vidx, this.Config));
				}
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
		public void RightDocks_Preview_GoLive_Click(object sender, System.EventArgs e) {
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
						Song song = new Song(fileName);
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
							Song.SerializeTo(song, song.FileName);
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

		private void liveMediaControls_MediaButtonPressed(object sender, MediaControlsEvent e) {
			if (e.button == MediaButton.Play) this.Media2BeamBox();

			MediaOperations media = ShowBeam.MediaFile1;
			if (media == null) return;

			switch (e.button) {
				case MediaButton.Play:
					// handled above by this.Media2BeamBox();
					break;
				case MediaButton.Stop:
					ShowBeam.StopMedia();
					break;
				default:
					media.Operation(e.button);
					break;
			}

			PlayProgress.Enabled = media.Playing;
			try {
				Media_TrackBar.Value = (int)media.CurrentLocation;
			} catch {
				Media_TrackBar.Value = 0;
			}
		}

		private void previewMediaControls_MediaButtonPressed(object sender, MediaControlsEvent e) {
			if (this.MediaFile1 == null) return;

			switch (e.button) {
				case MediaButton.Play:
					Media_TrackBar.Maximum = (int)MediaFile1.Duration();
					MediaFile1.Volume = -10000;
					Thread.Sleep(100);
					MediaFile1.Play();
					break;
				default:
					MediaFile1.Operation(e.button);
					break;
			}

			PlayProgress.Enabled = MediaFile1.Playing;
			try {
				Media_TrackBar.Value = (int)MediaFile1.CurrentLocation;
			} catch {
				Media_TrackBar.Value = 0;
			}

		}

	}

}

