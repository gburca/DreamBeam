using System;
using System.Windows.Forms;
namespace DreamBeam {
	/// <summary>
	/// Zusammenfassende Beschreibung f�r Class
	/// </summary>
	public class GuiTemplate {

		protected MainForm _MainForm = null;
		protected ShowBeam _ShowBeam = null;


		public GuiTemplate(MainForm impForm, ShowBeam impShowBeam) {
			this._MainForm = impForm;
			this._ShowBeam = impShowBeam;
		}
	}
}
