using System;

namespace DreamBeam {
	/// <summary>
	/// Zusammenfassende Beschreibung für Class
	/// </summary>
	public class ShowBeamTools : GuiTemplate {


		public ShowBeamTools(MainForm impForm, ShowBeam impShowBeam)
			: base(impForm, impShowBeam) {
			//
			// TODO: Hier die Konstruktorlogik einfügen
			//
		}

		public void HideBg() {
			_ShowBeam.Songupdate = true;

			if (_ShowBeam.HideBG == true) {
				_MainForm.ToolBars_MainToolbar_HideBG.Checked = false;
				_ShowBeam.HideBG = false;
			} else {
				_MainForm.ToolBars_MainToolbar_HideBG.Checked = true;
				_ShowBeam.HideBG = true;
			}
		}

		public void HideText() {
			_ShowBeam.Songupdate = true;
			if (_MainForm.ToolBars_MainToolbar_HideText.Checked) {
				_MainForm.ToolBars_MainToolbar_HideText.Checked = false;
				_ShowBeam.HideText = false;
			} else {
				_MainForm.ToolBars_MainToolbar_HideText.Checked = true;
				_ShowBeam.HideText = true;
			}
		}


		public void ShowStrophe(int Number) {
			if (_MainForm.SongShow_StropheList_ListEx.Items.Count > Number) {
				_MainForm.SongShow_StropheList_ListEx.SetSelected(Number, true);
				_MainForm.RightDocks_Preview_GoLive_Click(null, null);
			}
		}

	}
}
