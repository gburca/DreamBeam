using System;
using System.Drawing;

namespace DreamBeam
{
	/// <summary>
	/// Zusammenfassende Beschreibung für Class
	/// </summary>
	public class SongSettings
	{
			private string strFontFace = "";
			private System.Drawing.Font Fonter = new System.Drawing.Font("Times New Roman",22f);

			public string FontFace
		{
			get { return strFontFace; }
			set { strFontFace = value; }
		}

		public System.Drawing.Font Fontex
		{
			get { return Fonter; }
			set { Fonter = value; }
		}

		public SongSettings()
		{
			//
			// TODO: Hier die Konstruktorlogik einfügen
			//
		}
	}
}
