using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DreamBeam
{
	/// <summary>
	/// Zusammenfassende Beschreibung für Class
	/// </summary>
	public class GuiTools: GuiTemplate 
	{
		public RightDock RightDock = null;
		public SongEdit SongEdit = null;
		public Presentation Presentation = null;
		public Help Help = null;
		public ShowBeamTools ShowBeamTools = null;

		public GuiTools(MainForm impForm, ShowBeam impShowBeam) :  base(impForm,impShowBeam)
		{
		   this.RightDock = new RightDock(impForm, impShowBeam);
		   this.SongEdit = new SongEdit(impForm, impShowBeam);
		   this.Presentation = new Presentation(impForm, impShowBeam);
		   this.Help = new Help(impForm, impShowBeam);
		   this.ShowBeamTools = new ShowBeamTools(impForm, impShowBeam);
		}


		#region ShowTab
				//<summary> Shows the assigned Tab </summary>
			public void ShowTab(int Tab){
				bool resize = false;
				if(_MainForm.selectedTab > 1){
					resize = true;
				}

				if(_MainForm.selectedTab == 1){
				 _ShowBeam.Prerenderer.RenderAllThreaded();
				}

				_MainForm.selectedTab = Tab;
				_MainForm.PreviewUpdateTimer.Enabled = false;

				// Hide all Tabs
				for (int i = _MainForm.tabControl1.TabCount-1;i>=0;i--){
					_MainForm.tabControl1.TabPages.RemoveAt(i);
				}
				_MainForm.ToolBars_MainToolbar_SaveSong.Visible = false;
				_MainForm.ToolBars_MainToolbar_SaveMediaList.Visible = false;

				// Hide Right Panels
				_MainForm.RightDocks_TopPanel_Songs.Close();
				_MainForm.RightDocks_TopPanel_PlayList.Close();
				_MainForm.RightDocks_TopPanel_Search.Close();
				_MainForm.RightDocks_BottomPanel_Backgrounds.Close();
				_MainForm.RightDocks_BottomPanel_Media.Close();
				_MainForm.RightDocks_BottomPanel_MediaLists.Close();

				// Show Selected Tab
				switch (Tab){
				case 0:
					_MainForm.getSong();
					_MainForm.RightDocks_TopPanel_Songs.Open();
					_MainForm.RightDocks_TopPanel_PlayList.Open();
					_MainForm.RightDocks_TopPanel_Search.Open();
					_MainForm.RightDocks_BottomPanel_Backgrounds.Open();

					//change Menus
					_MainForm.ToolBars_MenuBar_Song.Visible = true;
					_MainForm.ToolBars_MenuBar_MediaList.Visible = false;

					_MainForm.rightSandDock.LayoutSystem = new TD.SandDock.SplitLayoutSystem(250, 400, System.Windows.Forms.Orientation.Horizontal, new TD.SandDock.LayoutSystemBase[] {
						new TD.SandDock.ControlLayoutSystem(196, 211, new TD.SandDock.DockControl[] {
							_MainForm.RightDocks_TopPanel_Songs,
							_MainForm.RightDocks_TopPanel_PlayList,
							_MainForm.RightDocks_TopPanel_Search}, _MainForm.RightDocks_TopPanel_Songs),
							new TD.SandDock.ControlLayoutSystem(196, 285, new TD.SandDock.DockControl[] {
						_MainForm.RightDocks_BottomPanel_Backgrounds}, _MainForm.RightDocks_BottomPanel_Backgrounds)});
					_MainForm.tabControl1.Controls.Add(_MainForm.tabPage0);
				   //	this.SongShow_Right_Panel.Controls.Add(this.SongShow_Preview_Panel);
				   if(resize)
					this.Resize();
				break;
				case 1:
					_MainForm.RightDocks_TopPanel_Songs.Open();
					_MainForm.RightDocks_TopPanel_PlayList.Open();
					_MainForm.RightDocks_TopPanel_Search.Open();
					_MainForm.RightDocks_BottomPanel_Backgrounds.Open();

					//change Menus
					_MainForm.ToolBars_MenuBar_Song.Visible = true;
					_MainForm.ToolBars_MenuBar_MediaList.Visible = false;

					_MainForm.rightSandDock.LayoutSystem = new TD.SandDock.SplitLayoutSystem(250, 400, System.Windows.Forms.Orientation.Horizontal, new TD.SandDock.LayoutSystemBase[] {
						  new TD.SandDock.ControlLayoutSystem(196, 211, new TD.SandDock.DockControl[] {
						  _MainForm.RightDocks_TopPanel_Songs,
						  _MainForm.RightDocks_TopPanel_PlayList,
						  _MainForm.RightDocks_TopPanel_Search}, _MainForm.RightDocks_TopPanel_Songs),
						  new TD.SandDock.ControlLayoutSystem(196, 285, new TD.SandDock.DockControl[] {
						  _MainForm.RightDocks_BottomPanel_Backgrounds}, _MainForm.RightDocks_BottomPanel_Backgrounds)});
					_MainForm.tabControl1.Controls.Add(_MainForm.tabPage1);
					_MainForm.ToolBars_MainToolbar_SaveSong.Visible = true;
					_MainForm.PreviewUpdateTimer.Enabled = true;
				   if(resize)
					this.Resize();
				break;
				case 2:
					_MainForm.RightDocks_BottomPanel_Backgrounds.Open();
					_MainForm.ToolBars_MenuBar_Song.Visible = false;
					_MainForm.ToolBars_MenuBar_MediaList.Visible = false;

					_MainForm.tabControl1.Controls.Add(_MainForm.tabPage2);
					if(_MainForm.Sermon_DocManager.TabStrips.Count < 1){
						_MainForm.Sermon_NewDocument();
					}
				break;
				case 3:
					_MainForm.RightDocks_TopPanel_Songs.Close();
					_MainForm.RightDocks_TopPanel_PlayList.Close();
					_MainForm.RightDocks_TopPanel_Search.Close();
					_MainForm.RightDocks_BottomPanel_Backgrounds.Close();

					//change Menus

					_MainForm.ToolBars_MainToolbar_SaveMediaList.Visible = true;
					_MainForm.ToolBars_MenuBar_Song.Visible = false;
					_MainForm.ToolBars_MenuBar_MediaList.Visible = true;

					_MainForm.rightSandDock.LayoutSystem = new TD.SandDock.SplitLayoutSystem(250, 400, System.Windows.Forms.Orientation.Horizontal, new TD.SandDock.LayoutSystemBase[] {
						new TD.SandDock.ControlLayoutSystem(196, 210, new TD.SandDock.DockControl[] {
						_MainForm.RightDocks_BottomPanel_Media,
						_MainForm.RightDocks_BottomPanel_MediaLists}, _MainForm.RightDocks_BottomPanel_Media)});
					_MainForm.RightDocks_BottomPanel_Media.Open();
					_MainForm.tabControl1.Controls.Add(_MainForm.tabPage4);
				break;
					case 4:
						_MainForm.RightDocks_BottomPanel_Backgrounds.Open();
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
					this.ShowBeamTools.ShowStrophe(0);
					break;
				case Keys.F2:
					this.ShowBeamTools.ShowStrophe(1);
					break;
				case Keys.F3:
					this.ShowBeamTools.ShowStrophe(2);
					break;
				case Keys.F4:
					this.ShowBeamTools.ShowStrophe(3);
					break;
				case Keys.F5:
					this.ShowBeamTools.ShowStrophe(4);
					break;
				case Keys.F6:
					this.ShowBeamTools.ShowStrophe(5);
					break;
				case Keys.F7:
					this.ShowBeamTools.ShowStrophe(6);
					break;
				case Keys.F8:
					this.ShowBeamTools.ShowStrophe(7);
					break;
				case Keys.F9:
					this.ShowTab(0);
					break;
				case Keys.F10:
					this.ShowTab(1);
					break;
				case Keys.F11:
					this.ShowTab(3);
					break;
				case Keys.F12:
					this.ShowTab(2);
					break;
				case Keys.Escape:
					this.ShowBeamTools.HideText();
					break;
			}

		}

		#endregion


		#region Resize
			public void Resize(){

				int tmpWidth = 0;
				if(_MainForm.selectedTab==0) {
					tmpWidth = _MainForm.SongShow_Right_Panel.Size.Width;
				} else if(_MainForm.selectedTab==1) {
					tmpWidth = _MainForm.SongEdit_RightPanel.Size.Width;
				}
				_MainForm.SongShow_Preview_Panel.Size = new System.Drawing.Size(tmpWidth, (int)System.Math.Round(tmpWidth*0.75));
				_MainForm.SongEdit_Preview_Panel.Size = _MainForm.SongShow_Preview_Panel.Size;

				if(_MainForm.video != null) {
					_MainForm.video.Size = Tools.VideoProportions(_MainForm.Presentation_PreviewBox.Size,_MainForm.video.DefaultSize);
					_MainForm.Presentation_VideoPanel.Size = Tools.VideoProportions(_MainForm.Presentation_PreviewBox.Size,_MainForm.video.DefaultSize);
					_MainForm.Presentation_VideoPanel.Location =  new Point((int)((_MainForm.Presentation_PreviewBox.Size.Width - _MainForm.Presentation_VideoPanel.Size.Width)/2)+10,(int)((_MainForm.Presentation_PreviewBox.Size.Height - _MainForm.Presentation_VideoPanel.Size.Height)/2)+10);
				}

				_MainForm.RenderStatus.Location = new Point(_MainForm.StatusPanel.Width + 5,4);

				//_MainForm.Draw_Song_Preview_Threader();
				_MainForm.Draw_Song_Preview_Image_Threaded();
			}
		#endregion
		public void ChangeTitle(){
			if(_MainForm.selectedTab==0 || _MainForm.selectedTab==1){
				_MainForm.Text = "DreamBeam - "+ _MainForm.Lang.say("Menu.Song") +": " + _ShowBeam.Song.SongName;
			}
			if(_MainForm.selectedTab==2){
				_MainForm.Text = "DreamBeam - Text";
			}
			if(_MainForm.selectedTab==3){
				_MainForm.Text = "DreamBeam - "+ _MainForm.Lang.say("Menu.MediaList") +": " + _ShowBeam.MediaList.Name;
			}

		}
	}
}
