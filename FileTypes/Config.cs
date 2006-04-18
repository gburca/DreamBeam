using System;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace DreamBeam
{
	/// <summary>     
	/// Zusammenfassende Beschreibung für Class
	/// </summary>
	public class Config
	{

		public int BeamBoxPosX;
		public int BeamBoxPosY;
		public int	BeamBoxSizeX;
		public int BeamBoxSizeY;
		public bool BeamBoxAutoPosSize;
		public int BeamBoxScreenNum;
		public bool PreRender;


		public bool Alphablending;
		public int BlendSpeed;
		public bool useDirect3D;
		public float OutlineSize;
		public string BibleLang;
		public string LastBibleUsed;
		public bool ShowBibleTranslation;
		public string PlayListString;
		public string SwordPath;
		public bool HideMouse;
		public bool AlwaysOnTop;

		private string ProgramDir;
		public System.Drawing.Color BackgroundColor;
		public bool LoopMedia;
		public bool LoopAutoPlay;
		public int AutoPlayChangeTime;

		public string Language;

		public Config()
		{
			ProgramDir = Tools.DreamBeamPath();
			this.Load();
		}

		/// <summary>Saves Program Properties like BeamBox Position and Alphablending to the config.xml. </summary>
		public void Save(string name){

		string filename = ProgramDir+"\\"+name;

		if(System.IO.File.Exists(filename)){
	//		System.IO.File.Delete(filename);
	//        MessageBox.Show (filename);
		}

			XmlTextWriter tw2 = new XmlTextWriter(filename,null);
			tw2.Formatting = Formatting.Indented;
			tw2.WriteStartDocument();
				tw2.WriteStartElement("DreamBeam_Config");
				tw2.WriteElementString("BeamBoxPosX",Convert.ToString(this.BeamBoxPosX));
				tw2.WriteElementString("BeamBoxPosY",Convert.ToString(this.BeamBoxPosY));
				tw2.WriteElementString("BeamBoxSizeX",Convert.ToString(this.BeamBoxSizeX));
				tw2.WriteElementString("BeamBoxSizeY",Convert.ToString(this.BeamBoxSizeY));
				tw2.WriteElementString("BeamBoxAutoPosSize",Convert.ToString(this.BeamBoxAutoPosSize));
				tw2.WriteElementString("BeamBoxScreenNum",Convert.ToString(this.BeamBoxScreenNum));
				tw2.WriteElementString("Alphablending",Convert.ToString(this.Alphablending));
				tw2.WriteElementString("BlendSpeed",Convert.ToString(this.BlendSpeed));
				tw2.WriteElementString("useDirect3D",Convert.ToString(this.useDirect3D));
				tw2.WriteElementString("OutlineSize",Convert.ToString(this.OutlineSize));
				tw2.WriteElementString("HideMouse",Convert.ToString(this.HideMouse));
				tw2.WriteElementString("PreRender",Convert.ToString(this.PreRender));
				tw2.WriteElementString("AlwaysOnTop",Convert.ToString(this.AlwaysOnTop));
				tw2.WriteElementString("BibleLang",Convert.ToString(this.BibleLang));
				tw2.WriteElementString("Show_BibleTranslation",Convert.ToString(this.ShowBibleTranslation));
				tw2.WriteElementString("SwordPath",this.SwordPath);
				tw2.WriteElementString("PlayList",PlayListString);
				tw2.WriteElementString("BackgroundColor",Convert.ToString(this.BackgroundColor.ToArgb()));
				tw2.WriteElementString("LoopMedia",Convert.ToString(this.LoopMedia));
				tw2.WriteElementString("AutoPlayChangeTime",Convert.ToString(this.AutoPlayChangeTime));
				tw2.WriteElementString("LoopAutoPlay",Convert.ToString(this.LoopAutoPlay));
				tw2.WriteElementString("Language",Language);
				tw2.WriteEndElement();
			tw2.WriteEndDocument();
			tw2.Flush();
			tw2.Close();
		}



			/// <summary>Loads Program Properties like BeamBox Position and Alphablending. </summary>
			public void Load(){

				if(!File.Exists(Tools.DreamBeamPath()+"\\config.xml")){
					XmlTextWriter tw = new XmlTextWriter(Tools.DreamBeamPath()+"\\config.xml",null);
					tw.Formatting = Formatting.Indented;
					tw.WriteStartDocument();
					tw.WriteStartElement("DreamBeam_Config");
					tw.WriteEndElement();
					tw.WriteEndDocument();
					tw.Flush();
					tw.Close();
				}else{

					XmlDocument document = new XmlDocument();
					try
					{
					  document.Load(Tools.DreamBeamPath()+"\\config.xml");
					  XmlNodeList list = null;


					  // Get the BeamBox Position
					  // BeamBoxPosX
					  list = document.GetElementsByTagName("BeamBoxPosX");
					  if(list.Count > 0) {
							this.BeamBoxPosX = Convert.ToInt32(list[0].InnerText);
					  }else{
							this.BeamBoxPosX = 0;
					  }

					  // BeamBoxPosY
					  list = document.GetElementsByTagName("BeamBoxPosY");
					  if(list.Count > 0) {
						  this.BeamBoxPosY = Convert.ToInt32(list[0].InnerText);
					  }else{
						this.BeamBoxPosY = 0;
					  }


					  // BeamBoxSizeX
					  list = document.GetElementsByTagName("BeamBoxSizeX");
						  if(list.Count > 0) {
							  this.BeamBoxSizeX = Convert.ToInt32(list[0].InnerText);
						  }else{
							  this.BeamBoxSizeX = 640;
						  }

					  // BeamBoxSizeY
					  list = document.GetElementsByTagName("BeamBoxSizeY");
					  if(list.Count > 0) {
						  this.BeamBoxSizeY = Convert.ToInt32(list[0].InnerText);
					  }else{
						  this.BeamBoxSizeY = 480;
					  }

					  // BeamBoxAutoPosSize
					  list = document.GetElementsByTagName("BeamBoxAutoPosSize");
					  if(list.Count > 0) {
						  this.BeamBoxAutoPosSize = Convert.ToBoolean(list[0].InnerText);
					  }else{
						  this.BeamBoxAutoPosSize = true;
					  }

					  // BeamBoxScreenNum
					  list = document.GetElementsByTagName("BeamBoxScreenNum");
					  if(list.Count > 0) {
						  this.BeamBoxScreenNum = Convert.ToInt32(list[0].InnerText);
					  }else{
						  this.BeamBoxScreenNum = -1;
					  }


					  // HideMouse enabled?
					  list = document.GetElementsByTagName("HideMouse");
					  if(list.Count > 0) {
						  this.HideMouse = Convert.ToBoolean(list[0].InnerText);
					  }else{
						  this.HideMouse = false;
					  }


					  // Do Prerender?
					  list = document.GetElementsByTagName("PreRender");
					  if(list.Count > 0) {
						  this.PreRender = Convert.ToBoolean(list[0].InnerText);
					  }else{
						  this.PreRender = false;
					  }




					  // Alphablending enabled?
					  list = document.GetElementsByTagName("Alphablending");
					  if(list.Count > 0) {
						  this.Alphablending = Convert.ToBoolean(list[0].InnerText);
					  }else{
						  this.Alphablending = false;
					  }

					  // Alphablending enabled?
					  list = document.GetElementsByTagName("BlendSpeed");
					  if(list.Count > 0) {
						  this.BlendSpeed = Convert.ToInt32(list[0].InnerText);
					  }else{
						  this.BlendSpeed = 10;
					  }



					  // Alphablending enabled?
					  list = document.GetElementsByTagName("useDirect3D");
					  if(list.Count > 0) {
						  this.useDirect3D = Convert.ToBoolean(list[0].InnerText);
					  }else{
						  this.useDirect3D = false;
					  }


					  // Outline Size
					  list = document.GetElementsByTagName("OutlineSize");
					  if(list.Count > 0) {
						  this.OutlineSize = (float)Convert.ToDouble(list[0].InnerText);
					  }else{
						  this.OutlineSize = 3;
					  }

					  // BibleLang
					  list = document.GetElementsByTagName("BibleLang");
					  if(list.Count > 0) {
						  this.BibleLang  = list[0].InnerText;
					  }else{
						  this.BibleLang  = "en";
					  }


					  //ShowBibleTranslation
					  list = document.GetElementsByTagName("Show_BibleTranslation");
					  if(list.Count > 0) {
						this.ShowBibleTranslation  = Convert.ToBoolean(list[0].InnerText);
					  }else{
						this.ShowBibleTranslation = false;
					  }

						list = document.GetElementsByTagName("LastBibleUsed");
						if (list.Count > 0) 
						{
							this.LastBibleUsed = list[0].InnerText;
						} 
						else 
						{
							this.LastBibleUsed = "";
						}

					  // Path to Sword.exe
					  list = document.GetElementsByTagName("SwordPath");
					  if(list.Count > 0) {

						  this.SwordPath  = list[0].InnerText;
						  if (this.SwordPath.Length > 11){
							  if (this.SwordPath.Substring(this.SwordPath.Length-9,9)=="sword.exe"){
								 this.SwordPath = this.SwordPath.Substring(0,this.SwordPath.Length-9);
							  }
						  }
					  }else{
						  this.SwordPath = "";
					  }




					  // Playlist
					  list = document.GetElementsByTagName("PlayList");
					  if(list.Count > 0) {
						  this.PlayListString  = list[0].InnerText;
					  }else{
						  this.PlayListString  = "";
					  }

					  // Loop AutoPlay
					  list = document.GetElementsByTagName("AlwaysOnTop");
					  if(list.Count > 0) {
						this.AlwaysOnTop = Convert.ToBoolean(list[0].InnerText);
					  }else{
						this.AlwaysOnTop = false;
					  }


					   // Background
					  list = document.GetElementsByTagName("BackgroundColor");
					  if(list.Count > 0) {
						  this.BackgroundColor = System.Drawing.Color.FromArgb(Convert.ToInt32(list[0].InnerText));
					  }else{
						this.BackgroundColor = Color.FromArgb(0,0,0);
					  }


					   // Loop Media
					  list = document.GetElementsByTagName("LoopMedia");
					  if(list.Count > 0) {
						this.LoopMedia = Convert.ToBoolean(list[0].InnerText);
					  }else{
						this.LoopMedia  = false;
					  }

					  // Loop AutoPlay
					  list = document.GetElementsByTagName("AutoPlayChangeTime");
					  if(list.Count > 0) {
						this.AutoPlayChangeTime = Convert.ToInt32(list[0].InnerText);
					  }else{
						this.AutoPlayChangeTime  = 2;
					  }

					  // Loop AutoPlay
					  list = document.GetElementsByTagName("LoopAutoPlay");
					  if(list.Count > 0) {
						this.LoopAutoPlay = Convert.ToBoolean(list[0].InnerText);
					  }else{
						this.LoopAutoPlay  = false;
					  }

					  // Language
					  list = document.GetElementsByTagName("Language");
					  if(list.Count > 0) {
						this.Language = list[0].InnerText;
					  }else{
						this.Language = "auto";
					  }

					  //split the book list by line into an array
//					  String[] tmpSongs = PlayListString.Substring(0,PlayListString.Length-1).Split(';');
					}
					catch(XmlException xmle)
					{
					  MessageBox.Show(xmle.Message);
					}
				}
			}

	}
}
