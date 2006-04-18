using System;
using System.Resources;
using System.Globalization;
using System.Windows.Forms;

namespace DreamBeam
{
	/// <summary>
	/// Zusammenfassende Beschreibung für Class
	/// </summary>
	public class Language
	{

//		public string Lang;
		private CultureInfo Culture;
		ResourceManager rm;


		public Language()
		{
			rm = new ResourceManager("DreamBeam.Language.lang",typeof(Language).Assembly);
			//
			// TODO: Hier die Konstruktorlogik einfügen
			//
		}

		public string say(string what){

			if(what==""){
			 return "";
			}
			return rm.GetString(what,Culture);
		}

		public string say(string what, string arg1){
			return  say(what).Replace(@"%1%",arg1);
		}

		public string say(string what, string arg1, string arg2){
			return  say(what,arg1).Replace(@"%2%",arg2);
		}

		public void setCulture(string Lang){

			Culture=CultureInfo.CurrentCulture;

			if(Lang == "auto")
				Lang = Culture.ToString();

			Culture=CultureInfo.CreateSpecificCulture(Lang);
		}

	}
}
