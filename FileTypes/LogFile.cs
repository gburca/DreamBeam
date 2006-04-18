using System;
using System.IO;
namespace DreamBeam
{
	/// <summary>
	/// Zusammenfassende Beschreibung für Class
	/// </summary>
	public class LogFile
	{
		public bool doLog;
		public DateTime date;

		public void Logfile()
		{

		  date = new DateTime();
			//
			// TODO: Hier die Konstruktorlogik einfügen
			//

		}

		public string Hashcounter(string Text){
			string hash = "";
			for (int i = 0; i<= Text.Length; i++){
				hash = hash + "#";
			}
			return hash;
		}


		public void BigHeader(string Text)
		{
			if(doLog){
				StreamWriter SW;
				SW=File.AppendText("LogFile.txt");

				SW.WriteLine(date.ToString());
				SW.WriteLine("#####################################################"+Hashcounter(Text));
				SW.WriteLine("########################### "+Text+" #########################");
				SW.WriteLine("#####################################################"+Hashcounter(Text));
				SW.Close();
			}
		}

		public void LogNew(string Text)	{
			this.Log("_________________________________________________________________________");
			this.Log(Text);
		}

		public void Log(string Text)
		{
			if(doLog){
				StreamWriter SW;
				SW=File.AppendText("LogFile.txt");
				SW.WriteLine(Text);
				SW.Close();
			}
		}


	}
}
