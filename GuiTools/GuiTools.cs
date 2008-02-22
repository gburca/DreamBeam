using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;


namespace DreamBeam {

	public class GuiTools: GuiTemplate {
		public RightDock RightDock = null;
		public SongEdit SongEdit = null;
		public Presentation Presentation = null;
		private Hashtable SandDockLayouts = null;
		private bool firstShowing = true;

		public GuiTools(MainForm impForm, ShowBeam impShowBeam) :  base(impForm,impShowBeam) {
			this.RightDock = new RightDock(impForm, impShowBeam);
			this.SongEdit = new SongEdit(impForm, impShowBeam);
			this.Presentation = new Presentation(impForm, impShowBeam);
			this.SandDockLayouts = new Hashtable();

			ReadSandDockLayouts();
		}


		#region ShowTab
//		public void ShowTab() {
//			foreach (TD.SandBar.ButtonItem b in _MainForm.ToolBars_ComponentBar.Buttons) {
//				if (b.Checked) {
//					if (b == _MainForm.ShowSongs_Button) ShowTab(MainTab.ShowSongs);
//					else if (b == _MainForm.EditSongs_Button) ShowTab(MainTab.EditSongs);
//					else if (b == _MainForm.Presentation_Button) ShowTab(MainTab.Presentation);
//					return;
//				}
//			}
//		}

		/// <summary>
		/// Shows the assigned Tab
		/// </summary>
		/// <param name="Tab"></param>
		public void ShowTab(MainTab	Tab) {

			#region Save and restore SandDock layout
			// If this is the firstShowing, the layout corresponds to that of VisualStudio designer
			// and we don't want to save it.
			if (! firstShowing) {
				// If the users wants us to remember and restore panel locations
				if (_MainForm.Config.RememberPanelLocations) {
					// Save the panel layout of the previous tab before switching to the new tab
					this.SandDockLayouts[_MainForm.selectedTab] = _MainForm.sandDockManager1.GetLayout();
				}
			}
			firstShowing = false;

			// If we're switching tabs, restore the panel layout that corresponds to the new MainTab
			if (this.SandDockLayouts.Contains(Tab) &&
				!Tools.StringIsNullOrEmpty((string)this.SandDockLayouts[Tab]) ) {
				try {
					_MainForm.sandDockManager1.SetLayout( (string)this.SandDockLayouts[Tab] );
				} catch {}
			}

			#endregion

			_MainForm.selectedTab = Tab;

//			_MainForm.PreviewUpdateTimer.Enabled = false;

			// Hide all Tabs
			for	(int i = _MainForm.tabControl1.TabCount-1;i>=0;i--){
				_MainForm.tabControl1.TabPages.RemoveAt(i);
			}
			_MainForm.ToolBars_MainToolbar_SaveSong.Visible = false;
			_MainForm.ToolBars_MainToolbar_SaveMediaList.Visible = false;


			// Show	Selected Tab
			switch (Tab){
				case MainTab.ShowSongs:
					//change Menus
					_MainForm.ToolBars_MainToolbar_HideBG.Visible = true;
					_MainForm.ToolBars_MainToolbar_HideText.Visible = true;

					_MainForm.ToolBars_MenuBar_Song.Visible = true;
					_MainForm.ToolBars_MenuBar_MediaList.Visible = false;
					_MainForm.tabControl1.Controls.Add(_MainForm.ShowSong_Tab);
					break;

				case MainTab.EditSongs:
					//change Menus
					_MainForm.ToolBars_MenuBar_Song.Visible = true;
					_MainForm.ToolBars_MenuBar_MediaList.Visible = false;

					_MainForm.tabControl1.Controls.Add(_MainForm.EditSongs2_Tab);
					//_MainForm.tabControl1.Controls.Add(_MainForm.EditSongs_Tab);
					_MainForm.ToolBars_MainToolbar_SaveSong.Visible = true;
//					_MainForm.PreviewUpdateTimer.Enabled = true;
					break;

				case MainTab.SermonTools:
					_MainForm.ToolBars_MenuBar_Song.Visible	= false;
					_MainForm.ToolBars_MenuBar_MediaList.Visible = false;

					_MainForm.ToolBars_MainToolbar_HideBG.Visible = true;
					_MainForm.ToolBars_MainToolbar_HideText.Visible = true;

					_MainForm.tabControl1.Controls.Add(_MainForm.SermonTools_Tab);
					if(_MainForm.Sermon_DocManager.TabStrips.Count < 1){
						_MainForm.Sermon_NewDocument();
					}
					break;

				case MainTab.Presentation:
					// Show / hide ToolBars and Menus
					_MainForm.ToolBars_MainToolbar_SaveMediaList.Visible = true;
					_MainForm.ToolBars_MainToolbar_HideBG.Visible = false;
					_MainForm.ToolBars_MainToolbar_HideText.Visible = false;

					_MainForm.ToolBars_MenuBar_MediaList.Visible = true;
					_MainForm.ToolBars_MenuBar_Song.Visible	= false;

					_MainForm.tabControl1.Controls.Add(_MainForm.Presentation_Tab);
					break;

				case MainTab.BibleText:
					// Show / hide ToolBars and Menus
					_MainForm.ToolBars_MainToolbar_HideBG.Visible = true;
					_MainForm.ToolBars_MainToolbar_HideText.Visible = true;

					_MainForm.tabControl1.TabPages.Add(_MainForm.BibleText_Tab);
					break;
			}
			ChangeTitle();


		}
		#endregion

		#region KeyListner
		public void KeyListner(System.Windows.Forms.KeyEventArgs e){
			switch(e.KeyCode){
				case Keys.F1:
					ShowStrophe(0);			e.Handled = true;
					break;
				case Keys.F2:
					ShowStrophe(1);			e.Handled = true;
					break;
				case Keys.F3:
					ShowStrophe(2);			e.Handled = true;
					break;
				case Keys.F4:
					ShowStrophe(3);			e.Handled = true;
					break;
				case Keys.F5:
					ShowStrophe(4);			e.Handled = true;
					break;
				case Keys.F6:
					ShowStrophe(5);			e.Handled = true;
					break;
				case Keys.F7:
					ShowStrophe(6);			e.Handled = true;
					break;
				case Keys.F8:
					ShowStrophe(7);			e.Handled = true;
					break;
				case Keys.F9:
					this.ShowTab(MainTab.ShowSongs);			e.Handled = true;
					break;
				case Keys.F10:
					this.ShowTab(MainTab.EditSongs);			e.Handled = true;
					break;
				case Keys.F11:
					this.ShowTab(MainTab.Presentation);			e.Handled = true;
					break;
				case Keys.F12:
					this.ShowTab(MainTab.SermonTools);			e.Handled = true;
					break;
				case Keys.Escape:
					if (_MainForm.Config.AppOperatingMode == OperatingMode.Client) {
						_MainForm.DisplayLiveClient.HideText(true);
					} else {
						_MainForm.DisplayLiveLocal.HideText(true);
					}
					_MainForm.ToolBars_MainToolbar_HideText.Checked = true;
					e.Handled = true;
					break;
				case Keys.PageDown:
					_MainForm.DisplayLive_Next();			e.Handled = true;
					break;
				case Keys.PageUp:
					_MainForm.DisplayLive_Prev();			e.Handled = true;
					break;
			}


		}

		public void ShowStrophe(int Number) {
			if (_MainForm.SongShow_StropheList_ListEx.Items.Count > Number) {
				_MainForm.SongShow_StropheList_ListEx.SetSelected(Number, true);
				_MainForm.RightDocks_Preview_GoLive_Click(null, null);
			}
		}

		#endregion

		#region Resize
			public void Resize(){
//
//				int tmpWidth = 0;
//				if (_MainForm.selectedTab == MainTab.ShowSongs) {
//					tmpWidth = _MainForm.SongShow_Right_Panel.Size.Width;
//				} else if (_MainForm.selectedTab == MainTab.EditSongs) {
//					tmpWidth = _MainForm.SongEdit_RightPanel.Size.Width;
//				}
//				_MainForm.SongShow_Preview_Panel.Size = new System.Drawing.Size(tmpWidth, (int)System.Math.Round(tmpWidth*0.75));
//				_MainForm.SongEdit_Preview_Panel.Size = _MainForm.SongShow_Preview_Panel.Size;
//
//				if(_MainForm.video != null) {
//					_MainForm.video.Size = Tools.VideoProportions(_MainForm.Presentation_PreviewBox.Size,_MainForm.video.DefaultSize);
//					_MainForm.Presentation_VideoPanel.Size = Tools.VideoProportions(_MainForm.Presentation_PreviewBox.Size,_MainForm.video.DefaultSize);
//					_MainForm.Presentation_VideoPanel.Location = new Point((int)((_MainForm.Presentation_PreviewBox.Size.Width - _MainForm.Presentation_VideoPanel.Size.Width)/2)+10,(int)((_MainForm.Presentation_PreviewBox.Size.Height - _MainForm.Presentation_VideoPanel.Size.Height)/2)+10);
//				}
//
//				_MainForm.RenderStatus.Location = new Point(_MainForm.StatusPanel.Width + 5,4);
//
//				//_MainForm.Draw_Song_Preview_Threader();
//				_MainForm.Draw_Song_Preview_Image_Threaded();
			}
		#endregion

		public void	ChangeTitle(){
			switch (_MainForm.selectedTab) {
				case MainTab.ShowSongs:
				case MainTab.EditSongs:
					_MainForm.Text = "DreamBeam - " + Regex.Replace(_MainForm.Lang.say("Menu.Song"), @"&", "");
					break;
				case MainTab.SermonTools:
					_MainForm.Text = "DreamBeam - Sermon";
					break;
				case MainTab.BibleText:
					_MainForm.Text = "DreamBeam - Bible";
					break;
				case MainTab.Presentation:
					_MainForm.Text = "DreamBeam - " + Regex.Replace(_MainForm.Lang.say("Menu.MediaList"), @"&", "");
					break;
				default:
					_MainForm.Text = "DreamBeam";
					break;
			}
		}

		#region SandDoc persistance functions

		/// <summary>
		/// This function should be called when shutting down to save all the layouts to config files.
		/// </summary>
		public void SaveSandDockLayouts() {
			// We only save layouts when switching tabs, so save the current (last) layout
			this.SandDockLayouts[_MainForm.selectedTab] = _MainForm.sandDockManager1.GetLayout();

			foreach (MainTab tab in this.SandDockLayouts.Keys) {
				SaveTabLayout(tab);
			}
		}

		private void ReadSandDockLayouts() {
			foreach (MainTab tab in Enum.GetValues(typeof(MainTab))) {
				string layout = ReadTabLayout(tab);
				if (layout != null) {
					this.SandDockLayouts[tab] = layout;
				}
			}
		}

		private void SaveTabLayout(MainTab tab) {
			if (!this.SandDockLayouts.Contains(tab) ||
				Tools.StringIsNullOrEmpty((string)this.SandDockLayouts[tab])) { return; }

			using (FileStream fs = File.Open(GetTabLayoutFile(tab), FileMode.Create, FileAccess.Write)) {
				using (StreamWriter sw = new StreamWriter(fs)) {
					sw.Write((string)this.SandDockLayouts[tab]);
				}
			}
		}

		private string ReadTabLayout(MainTab tab) {
			string file = GetTabLayoutFile(tab);

			if (!File.Exists(file)) { return null; }

			using (FileStream fs = File.Open(file, FileMode.OpenOrCreate, FileAccess.Read)) {
				using (StreamReader sr = new StreamReader(fs)) {
					return sr.ReadToEnd();
				}
			}
		}

		/// <summary>
		/// The SandDoc layout for each tab is stored in a separate file. This function tells us the name of that file.
		/// </summary>
		/// <param name="tab"></param>
		/// <returns></returns>
		private string GetTabLayoutFile(MainTab tab) {
			return Tools.GetDirectory(DirType.Config, _MainForm.ConfigSet + ".Tab_" + Enum.GetName(typeof(MainTab), tab) + ".xml");
		}
		#endregion

	} // class
} // namespace
