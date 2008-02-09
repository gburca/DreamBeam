using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.IO;

namespace DreamBeam {
	/// <summary>
	/// Zusammenfassende Beschreibung für Class
	/// </summary>




	public class RightDock : GuiTemplate {
		public BGImageTools BGImageTools = null;
		public MediaListTools MediaListTools = null;


		public RightDock(MainForm impForm, ShowBeam impShowBeam)
			: base(impForm, impShowBeam) {
			this.BGImageTools = new BGImageTools(impForm, impShowBeam);
			this.MediaListTools = new MediaListTools(impForm, impShowBeam);
		}
	}
}
