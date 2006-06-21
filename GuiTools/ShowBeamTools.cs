using System;

namespace DreamBeam
{
	/// <summary>
	/// Zusammenfassende Beschreibung f�r Class
	/// </summary>
	public class ShowBeamTools : GuiTemplate
	{


		public ShowBeamTools(MainForm impForm, ShowBeam impShowBeam) :  base(impForm,impShowBeam)
		{
			//
			// TODO: Hier die Konstruktorlogik einf�gen
			//
		}

		public void HideBg(){
			_ShowBeam.Songupdate = true;

						if (_ShowBeam.HideBG == true) {
							_MainForm.ToolBars_MainToolbar_HideBG.Checked = false;
							_ShowBeam.HideBG = false;
						} else {
							_MainForm.ToolBars_MainToolbar_HideBG.Checked = true;
							_ShowBeam.HideBG = true;
						}

						// Repaint Image
						if (_MainForm.selectedTab == MainTab.ShowSongs || _MainForm.selectedTab == MainTab.EditSongs){
							_MainForm.Draw_Song_Preview_Image();
							_ShowBeam.PaintSong();
						}
						if (_MainForm.selectedTab == MainTab.SermonTools){
							_MainForm.Draw_Song_Preview_Image();
							_ShowBeam.PaintSermon();
						}


		}

		public void HideText(){
					  _ShowBeam.Songupdate = true;
						if (_MainForm.ToolBars_MainToolbar_HideText.Checked){
							_MainForm.ToolBars_MainToolbar_HideText.Checked = false;
							_ShowBeam.HideText = false;
						}else{
							_MainForm.ToolBars_MainToolbar_HideText.Checked = true;
							_ShowBeam.HideText = true;
						}

						// Repaint Image
						if (_MainForm.selectedTab == MainTab.ShowSongs || _MainForm.selectedTab == MainTab.EditSongs){
							_MainForm.Draw_Song_Preview_Image();
							_ShowBeam.PaintSong();
						}
						if (_MainForm.selectedTab == MainTab.SermonTools){
							_MainForm.Draw_Song_Preview_Image();
							_ShowBeam.PaintSermon();
						}
		}


		public void ShowSong(){
		   _ShowBeam.Songupdate = true;
		   if(_MainForm.ToolBars_MainToolbar_ShowBeamBox.Checked == false){
			   _MainForm.ToolBars_MainToolbar_ShowBeamBox.Checked = true;
			   _ShowBeam.Show();
		   }
		   _ShowBeam.Song.strophe = _MainForm.SongShow_StropheList_ListEx.SelectedIndex;
		   _ShowBeam.newText = true;
		   _ShowBeam.Songupdate = true;
		   _ShowBeam.PaintSong();
		   _MainForm.Draw_Song_Preview_Image();
		}

	   public void ShowStrophe(int Number){
			if(_MainForm.SongShow_StropheList_ListEx.Items.Count > Number){
				_MainForm.SongShow_StropheList_ListEx.SetSelected(Number,true);
				ShowSong();
			}
		}

	}
}
