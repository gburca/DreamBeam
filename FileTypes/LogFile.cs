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
		public string logPrefix;

		public LogFile(string logPrefix)
		{
			this.logPrefix = logPrefix;
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
				SW=File.AppendText(logPrefix + ".LogFile.txt");

				SW.WriteLine("");
				SW.WriteLine(DateTime.Now.ToString());
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
				SW=File.AppendText(logPrefix + ".LogFile.txt");
				SW.WriteLine(DateTime.Now.ToString() + ": " + Text);
				SW.Close();
			}
		}


	}
}
