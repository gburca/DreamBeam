using System;
using System.Windows.Forms;
namespace DreamBeam {
	/// <summary>
	/// Zusammenfassende Beschreibung für Class
	/// </summary>
	public class GuiTemplate {

		protected MainForm _MainForm = null;
		protected OldSong _Song = null;
		protected ShowBeam _ShowBeam = null;


		public GuiTemplate(MainForm impForm, ShowBeam impShowBeam) {
			this._MainForm = impForm;
			this._Song = impShowBeam.Song;
			this._ShowBeam = impShowBeam;
		}
	}
}
