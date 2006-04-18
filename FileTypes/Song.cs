/*

DreamBeam - a Church Song Presentation Program
Copyright (C) 2004 Stefan Kaufmann
 
This program is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation; either version 2
of the License, or (at your option) any later version.
 
This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.
 
You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
 
*/

using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Xml;


namespace DreamBeam {

	//Contains all Song Information and Methods
	public class Song {


		public string SongName;//The Song's Name and Filename (+.xml)
		private string[] Text = new string[3];//The TextArray for Header, Strophes and Footer and Multilang
		public string[] FontFace = new string[4];//The FontFaceArray for Header, Strophes and Footer and Multilang
		public System.Drawing.FontStyle[] FontStyle = new System.Drawing.FontStyle[4];//The FontStyleArray (bold, regular..) for Header, Strophes and Footer and MulitLang
		public float[] FontSize = new float[4];//The FontSizeArray for Header, Strophes and Footer
		public int[] posX = new int[3];//The PositionX Array for Header, Strophes and Footer
		public int[] posY = new int[3];//The PositionY Array for Header, Strophes and Footer
		public int windowWidth = 0;//The wide of the ShowBeam Window
		public int windowHeight = 0;//The height of the ShowBeam Window
		public System.Drawing.Color[] TextColor = new System.Drawing.Color[4];//The TextColor Array for Header, Strophes and Footer
		public System.Drawing.Color[] OutlineColor = new System.Drawing.Color[4];//<summary>The OutlineColor Array for Header, Strophes and Footer
		public string[] TextEffect = new string[4];//The TextEffect like Outline Array for Header, Strophes and Footer
		public string TextAlign = "";//Alignment of Text
		public bool MultiLang = false;//Alignment of Text
		public bool[] AutoPos = new bool[3];//Autoposition ?
		public string bg_image;//The Song's BG Image
		public int strophe = 0;		//active strophe
		public int strophe_count = 0; 		//Number of strophes
		public string version = "0.5";//DreamBeam Version
		public int TextStyle = 0;
		private System.Windows.Forms.Form SizeForm = null; //the Showbeam sizes for size adjustments

		private bool TextChanged = true;
		private string LongestStrophe;
		private string WidestStrophe;
		public string strSeperator = "\n\n\n";

	#region Save
		///<summary>Saves the Song</summary>
		public void Save() {
			XmlTextWriter tw = new XmlTextWriter("Songs\\"+SongName+".xml",null);
			tw.Formatting = Formatting.Indented;
			tw.WriteStartDocument();
			tw.WriteStartElement("DreamSong");
			tw.WriteElementString("Version",this.version);
			tw.WriteElementString("image",bg_image);
			tw.WriteElementString("TextAlign",TextAlign);
			tw.WriteElementString("MultiLang",MultiLang.ToString());
			tw.WriteElementString("TextStyle",TextStyle.ToString());
			tw.WriteElementString("windowHeight",windowHeight.ToString());
			tw.WriteElementString("windowWidth",windowWidth.ToString());

			for (int i = 0; i<3;i++) {
				tw.WriteStartElement("Text"+i);
				tw.WriteElementString("Text",this.Text[i]);
				tw.WriteElementString("Font",this.FontFace[i]);
				tw.WriteElementString("FontStyle", Convert.ToString(Convert.ToInt32(FontStyle[i])));

				tw.WriteElementString("FontSize",((int)this.FontSize[i]).ToString());
				tw.WriteElementString("TextColor",Convert.ToString(this.TextColor[i].ToArgb()));
				tw.WriteElementString("OutlineColor",Convert.ToString(this.OutlineColor[i].ToArgb()));
				tw.WriteElementString("posX",Convert.ToString(this.posX[i]));
				tw.WriteElementString("posY",Convert.ToString(this.posY[i]));
				tw.WriteElementString("TextEffect",this.TextEffect[i]);
				tw.WriteElementString("AutoPos",AutoPos[i].ToString());
				tw.WriteEndElement();
			}

			// Multilanguage
			tw.WriteStartElement("MultiLangSettings");
				tw.WriteElementString("Font",this.FontFace[3]);
				tw.WriteElementString("FontStyle", Convert.ToString(Convert.ToInt32(FontStyle[3])));
				tw.WriteElementString("FontSize",((int)this.FontSize[3]).ToString());
				tw.WriteElementString("TextColor",Convert.ToString(this.TextColor[3].ToArgb()));
				tw.WriteElementString("OutlineColor",Convert.ToString(this.OutlineColor[3].ToArgb()));
				tw.WriteElementString("TextEffect",this.TextEffect[3]);
			tw.WriteEndElement();

			tw.WriteEndElement();
			tw.WriteEndDocument();
			tw.Flush();
			tw.Close();
		}
	#endregion

	#region Load
		///<summary>Loads the Song</summary>
		public void Load(string filename) {
            Init(filename);
			int i;
			this.strophe = 0;
			XmlDocument document = new XmlDocument();
			try {
				//"Songs\\"+filename+".xml"
				document.Load(Tools.DreamBeamPath()+"\\Songs\\"+filename+".xml");
			} catch(XmlException xmle) {
				MessageBox.Show(xmle.Message);
			}

			XmlNodeList list = null;

			// Get This.Text[*]
			list = document.GetElementsByTagName("Text");
			i = 0;
			foreach(XmlNode n in list) {
				this.Text[i]=n.InnerText;
				i++;
			}


			// Get This.FontFace[*]
			list = document.GetElementsByTagName("Font");
			i = 0;

			foreach(XmlNode n in list) {
				this.FontFace[i]=n.InnerText;
				i++;
			}

			// Get This.TextStyle
			list = document.GetElementsByTagName("FontStyle");
			i = 0;
			foreach(XmlNode n in list) {
				this.FontStyle[i] = (System.Drawing.FontStyle)Convert.ToInt32(n.InnerText);
				i++;
			}

			// Get This.FontSize[*]
			list = document.GetElementsByTagName("FontSize");
			double x;
			i = 0;
			foreach(XmlNode n in list) {
				x = Convert.ToDouble(n.InnerText.Trim());
				this.FontSize[i] = (float)x;
				i++;
			}

			// Get This.posX[*]
			list = document.GetElementsByTagName("posX");
			i = 0;
			foreach(XmlNode n in list) {
				this.posX[i] = Convert.ToInt32(n.InnerText);
				i++;
			}

			// Get This.posX[*]
			list = document.GetElementsByTagName("posY");
			i = 0;
			foreach(XmlNode n in list) {
				this.posY[i] = Convert.ToInt32(n.InnerText);
				i++;
			}

			// Get This.TextColor[*]
			list = document.GetElementsByTagName("TextColor");
			i = 0;
			foreach(XmlNode n in list) {
				this.TextColor[i] = System.Drawing.Color.FromArgb(Convert.ToInt32(n.InnerText));
				i++;
			}

			// Get This.OutlineColor[*]
			list = document.GetElementsByTagName("OutlineColor");
			i = 0;
			foreach(XmlNode n in list) {
				this.OutlineColor[i] = System.Drawing.Color.FromArgb(Convert.ToInt32(n.InnerText));
				i++;
			}

			// Get This.TextStyle
			list = document.GetElementsByTagName("TextStyle");
			i = 0;
			foreach(XmlNode n in list) {
				this.TextStyle = Convert.ToInt32(n.InnerText);
				i++;
			}



			   // Get This.TextAlign
			list = document.GetElementsByTagName("TextAlign");
			TextAlign = "left";
			foreach(XmlNode n in list) {
				TextAlign = n.InnerText;
			}


			   // Get Multilang
			list = document.GetElementsByTagName("MultiLang");
			MultiLang = false;
			foreach(XmlNode n in list) {
				MultiLang = Convert.ToBoolean(n.InnerText);
			}


			// Get This.TextEffect[*]
			list = document.GetElementsByTagName("TextEffect");
			i = 0;
			foreach(XmlNode n in list) {
				this.TextEffect[i] = n.InnerText;
				i++;
			}



			// Get AutoPos
			list = document.GetElementsByTagName("AutoPos");
			i = 0;
			AutoPos[0] = false;
			AutoPos[1] = false;
			AutoPos[2] = false;
			foreach(XmlNode n in list) {
				this.AutoPos[i] = Convert.ToBoolean(n.InnerText);
				i++;
			}

			// Get AutoPos
			list = document.GetElementsByTagName("windowHeight");
			i = 0;
			foreach(XmlNode n in list) {
				windowHeight = Convert.ToInt32(n.InnerText);
				i++;
			}

			// Get AutoPos
			list = document.GetElementsByTagName("windowWidth");
			i = 0;
			foreach(XmlNode n in list) {
				windowWidth = Convert.ToInt32(n.InnerText);
				i++;
			}

			// Get This.bg_image
			list = document.GetElementsByTagName("image");
			foreach(XmlNode n in list) {
				if (System.IO.File.Exists(n.InnerText)) {
					this.bg_image = n.InnerText;
				} else {
					this.bg_image = null;
					// Define Directory and ImageTypes
					string strImageDir = Tools.DreamBeamPath()+"\\Backgrounds";
					string[] folders = Directory.GetDirectories(@strImageDir);

					string tmpfilename = Path.GetFileName(n.InnerText);
					if (System.IO.File.Exists(Tools.DreamBeamPath()+"\\Backgrounds\\"+tmpfilename))
					{
						this.bg_image = Tools.DreamBeamPath()+"\\Backgrounds\\"+tmpfilename;
					} else {
						foreach (string folder in folders)
						{
							if (System.IO.File.Exists(folder+"\\"+tmpfilename))
							{
								this.bg_image = Tools.DreamBeamPath()+"\\Backgrounds\\"+Tools.Reverse(Tools.Reverse(folder).Substring(0,Tools.Reverse(folder).IndexOf(@"\"))) + "\\" +tmpfilename;
							}
						}
					 }
				}
			}

			this.SongName = filename;

			if(this.SizeForm != null)
				CalculateSizes();
		  }
	#endregion

		private void CalculateSizes(){

			if(windowHeight != 0 && windowWidth != 0){

				for (int i = 0; i<= 2; i++){
					this.posX[i] = 	(int)System.Math.Round((float)SizeForm.Size.Width/(float)windowWidth * this.posX[i]);
					this.posY[i] = (int)System.Math.Round((float)SizeForm.Size.Height/(float)windowHeight * this.posY[i]);
				}


				if(windowWidth/windowHeight >= SizeForm.Size.Width/SizeForm.Size.Height){  //
				   //windowWidth is smaller or equal
					for (int i = 0; i<4; i++){
						this.FontSize[i] = (float)SizeForm.Size.Width/(float)windowWidth * this.FontSize[i];
					}
				}else{
				   //windowHeight is smaller
					for (int i = 0; i<4; i++){
						this.FontSize[i] = (float)SizeForm.Size.Height/(float)windowHeight * this.FontSize[i];
					}
				}
			}

			windowHeight = SizeForm.Size.Height;
			windowWidth = SizeForm.Size.Width;
		   
		}

		///<summary>Checks if XML File is a song (nothing yet)</summary>
		public bool isSong(string filename){
			return true;
		}

		///<summary>Init the Song</summary>
		public void Init(string strName){
			for (int i = 0; i<4; i++){
				this.FontFace[i] = "Arial";
				this.FontSize[i] = 48;
				this.FontStyle[i] = System.Drawing.FontStyle.Regular;
				this.TextEffect[i] = "Normal";
				this.TextColor[i] = Color.White;
				this.OutlineColor[i] = Color.Black;
			}

			for (int i = 0; i<3; i++){
				this.Text[i] = "";
				this.posX[i] = 10;
				this.posY[i] = 10+(100*i);
				this.AutoPos[i] = true;
			}
			SongName = strName;
			bg_image = null;
		}


		public void SetText(string Content,int x){
		 Text[x] = Content;
		}

		public string GetText(int x){
			 return Text[x];
		}

		///<summary>Main Class</summary>
		public Song(){
			this.Init("New Song");
		}

		public Song(System.Windows.Forms.Form Form){
			this.SizeForm = Form;
			this.Init("New Song");
		}

	#region StropheTools

		public int CountStrophes(){
			return(Tools.Count(Text[1],strSeperator));
		}

		public string GetStrophe(int StropheNumber){

						if(StropheNumber == -1)
							StropheNumber = this.strophe;

						string temp = GetText(1);
						int x = 0;
						int intSLength = 0; //(height)length of the biggest strophe
						int intSWidth = 0;
						LongestStrophe = GetText(1); //Text of the Biggest_Strophe
						WidestStrophe = GetText(1);
						string strCurrentStrophe = temp;
						int strophes;


						//get strophe and search for the longest one
						strophes = CountStrophes();
						for (x=0;x < strophes;x++) {

							// find all strophes, exept the last one
							if (x < (strophes -1)) {

								// if selected strophe, then copy this into the Textfield
								if(x == StropheNumber ) {
									strCurrentStrophe = temp.Substring(0,temp.IndexOf(strSeperator));
								}

								//check if this is the longest strophe
								int tmp = Tools.Count(temp.Substring(0,temp.IndexOf(strSeperator)),"\n");
								if(tmp > intSLength) {
									intSLength = tmp;
									LongestStrophe = temp.Substring(0,temp.IndexOf(strSeperator));
								}

								//check if this is the widest strophe
								tmp = TextGraphics.GetLongestLine(temp.Substring(0,temp.IndexOf(strSeperator)));
								if( tmp > intSWidth){
								  intSWidth = tmp;
								  WidestStrophe =  temp.Substring(0,temp.IndexOf(strSeperator));
								}

								// cut the first strophe out of the list
								temp=temp.Substring(temp.IndexOf(strSeperator)+strSeperator.Length);


							} else {
								// get the last strophe
								// if selected strophe, then copy this into the Textfield
								if (x == StropheNumber) {
									strCurrentStrophe = temp;
								}

								//check if this is the longest strophe
								int tmp = Tools.Count(temp,"\n");
								if(tmp > intSLength) {
									intSLength = tmp;
									LongestStrophe = temp;
								}

								//check if this is the widest strophe
								tmp = TextGraphics.GetLongestLine(temp);
								if( tmp > intSWidth){
								 intSWidth = tmp;
								 WidestStrophe =  temp;
								}
							}
						}
					   this.TextChanged = false;
					   return strCurrentStrophe;
		}

		public string GetLongestStrophe(){
			string tmp;
			if(TextChanged)
				 tmp = this.GetStrophe(-1);
			return this.LongestStrophe;
		}


		public string GetWidestStrophe(){
			string tmp;
			if(this.TextChanged )
				tmp = this.GetStrophe(-1);
			return this.WidestStrophe;
		}

	#endregion

	#region SerializeStropheSettings
	public string SerializeStropheSettings(int strophenumber){
		String StropheString = "";


			 StropheString = StropheString + bg_image;
			 StropheString = StropheString+ TextAlign;
			 StropheString = StropheString+ TextStyle.ToString();

			for (int i = 0; i<3;i++) {

//				tw.WriteElementString("Text",this.Text[i]);
				 if(i == 1)
					StropheString += this.GetStrophe(strophenumber);
				 else
					StropheString += this.Text[i];

				 StropheString += this.FontFace[i];
				 StropheString += Convert.ToString(Convert.ToInt32(FontStyle[i]));
				 StropheString += this.FontSize[i].ToString();
				 StropheString += Convert.ToString(this.TextColor[i].ToArgb());
				 StropheString += Convert.ToString(this.OutlineColor[i].ToArgb());

				 if(AutoPos[i] == false){
					 StropheString += Convert.ToString(this.posX[i]);
					 StropheString += Convert.ToString(this.posY[i]);
				 }
					StropheString  += this.TextEffect[i];
			}



		   if(MultiLang){
			 StropheString += this.FontFace[3];
			 StropheString += Convert.ToString(Convert.ToInt32(FontStyle[3]));
			 StropheString += this.FontSize[3].ToString();
			 StropheString +=Convert.ToString(this.TextColor[3].ToArgb());
			 StropheString +=Convert.ToString(this.OutlineColor[3].ToArgb());
			 StropheString += this.TextEffect[3];
			}



		return StropheString;
	}
	#endregion

	}
}
