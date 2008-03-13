using System;
using System.Resources;
using System.Globalization;
using System.Windows.Forms;

namespace DreamBeam {
	/// <summary>
	/// 
	/// </summary>
	public class Language {

		private CultureInfo Culture, CultureEn;
		ResourceManager rm;


		public Language() {
			rm = new ResourceManager("DreamBeam.Language.lang", typeof(Language).Assembly);
			CultureEn = CultureInfo.CreateSpecificCulture("en-US");
		}

		public string say(string what) {
			if (what == "") {
				return "";
			}

			string res = rm.GetString(what, Culture);

			if (res == null || res.Length == 0) {
				return rm.GetString(what, CultureEn);
			} else {
				return res;
			}
		}

		public string say(string what, string arg1) {
			return say(what).Replace(@"%1%", arg1);
		}

		public string say(string what, string arg1, string arg2) {
			return say(what, arg1).Replace(@"%2%", arg2);
		}

		public void setCulture(string Lang) {

			Culture = CultureInfo.CurrentCulture;

			if (Lang == "auto")
				Lang = Culture.ToString();

			Culture = CultureInfo.CreateSpecificCulture(Lang);
		}

	}
}
