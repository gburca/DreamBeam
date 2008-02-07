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
using System.Drawing.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;


namespace DreamBeam {

	#region Enums
	// These are used as array indices so they must be numbered sequentially, from 0
	public enum SongTextType {
		Title,
		Verse,
		Author
	}

	public enum SongSearchType {
		/// <summary>The exact phrase must be present</summary>
		Phrase,
		/// <summary>All the words must be present, but not in any particular order</summary>
		Words
	}
	#endregion

	//Contains all Song Information and Methods
	[Serializable()]
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
		/// <summary>Active strophe. Zero-based.</summary>
 		public int strophe = 0;
		public int strophe_count = 0; 		//Number of strophes
		public string version = "0.5";//DreamBeam Version
		public int TextStyle = 0;
		private System.Windows.Forms.Form SizeForm = null; //the Showbeam sizes for size adjustments

		private bool TextChanged = true;
		private string LongestStrophe;
		private string WidestStrophe;
		public string strSeperator = "\n\n\n";
		public bool[] Hide = new bool[Enum.GetValues(typeof(SongTextType)).Length];

		public virtual Song Clone() {
			Song s = new Song();

			s = this.MemberwiseClone() as Song;

			// Do we need to do this for each array?
			//s.Text = this.Text.Clone;
			return s;
		}

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
				//document.Load(Tools.DreamBeamPath() + @"\Songs\" + filename + ".xml");
				document.Load(filename);
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
					string strImageDir = Tools.GetAppDocPath()+@"\Backgrounds";
					string[] folders = Directory.GetDirectories(@strImageDir);

					string tmpfilename = Path.GetFileName(n.InnerText);
					if (System.IO.File.Exists(Tools.GetAppDocPath()+@"\Backgrounds\"+tmpfilename))
					{
						this.bg_image = Tools.GetAppDocPath()+@"\Backgrounds\"+tmpfilename;
					} else {
						foreach (string folder in folders)
						{
							if (System.IO.File.Exists(folder+@"\"+tmpfilename))
							{
								this.bg_image = Tools.GetAppDocPath()+@"\Backgrounds\"+Tools.Reverse(Tools.Reverse(folder).Substring(0,Tools.Reverse(folder).IndexOf(@"\"))) + "\\" +tmpfilename;
							}
						}
					 }
				}
			}

			this.SongName = filename;

			if(this.SizeForm != null)
				CalculateSizes();

			this.strophe_count = CountStrophes();
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

		#region Constructors
		///<summary>Main Class</summary>
		public Song(){
			this.Init("New Song");
		}

		public Song(System.Windows.Forms.Form Form){
			this.SizeForm = Form;
			this.Init("New Song");
		}

		public Song(string filename) {
			this.Init("New Song");
			this.Load(filename);
		}
		
		#endregion

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
						if (StropheNumber >= strophes) StropheNumber = strophes - 1;
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

	public enum LyricsType {
		Verse,
		Chorus,
		Other
	}

	/// <summary>
	/// LyricsSequenceItem (LSI) is like a pointer to a LyricsItem. The order in
	/// which these LSI objects are added to the NewSong.Sequence array
    /// determines the order in which the parts of a song are displayed when the
    /// user requests the "Next" slide.
	/// </summary>
	[Serializable()]
	public class LyricsSequenceItem {
		[XmlIgnore()] public static Language lang;
		[XmlAttribute] public LyricsType Type;
		[XmlAttribute] public int Number;

		public LyricsSequenceItem() {}
		public LyricsSequenceItem(LyricsType Type, int Number) {
			this.Type = Type;
			this.Number = Number;
		}
		public LyricsSequenceItem(LyricsSequenceItem item) :
			this(item.Type, item.Number) {}

		public override string ToString() {
			return lang.say("EditSongs.LyricType" + 
				Enum.GetName(typeof(LyricsType), Type)) + " " + Number.ToString();
		}
	}

	/// <summary>
	/// A LyricsItem represents a verse or chorus. A song's LyricsItem(s) can be
    /// ordered by the user in any sequence desired by using the
    /// LyricsSequenceItem class.
	/// </summary>
	[Serializable()]
	public class LyricsItem : IComparable {
		[XmlAttribute] public LyricsType Type;
		/// <summary>The verse (chorus, other) number. One-based.</summary>
		[XmlAttribute] public int Number;
		[XmlText] public string Lyrics;

		public LyricsItem() {}
		public LyricsItem(LyricsType Type, int Number, string Lyrics) {
			this.Type = Type;
			this.Number = Number;
			this.Lyrics = Lyrics;
		}

		#region IComparable Members
		/// <summary>
		/// Implements the IComparable interface. Allows arrays of LyricsItem's to be sorted.
		/// </summary>
		/// <param name="obj">The type of this object can be another LyricsItem or a LyricsSequenceItem</param>
		/// <returns>-1, 0, or 1 if "this" is less, equal, or greater than "obj"</returns>
		public int CompareTo(object obj) {
			// Return -1 if "this" is less than "other/obj"
			if (obj is LyricsItem) {
				LyricsItem other = obj as LyricsItem;
				if (this.Type == other.Type) {
					return this.Number - other.Number;
				} else {
					return this.Type - other.Type;
				}
			} else {
				LyricsSequenceItem other = obj as LyricsSequenceItem;
				if (this.Type == other.Type) {
					return this.Number - other.Number;
				} else {
					return this.Type - other.Type;
				}
			}
		}
		#endregion
	}

	[Serializable()]
    [XmlRoot(ElementName="DreamSong")]
	public class NewSong : Content, IContentOperations {
		// The following setting and properties will be serialized
		public string Version;
		public string Title;
		public string Author;
		public string Collection;
		public string Number;
		public string Notes;
        public string KeyRangeLow, KeyRangeHigh;
		public bool MinorKey;
		public bool DualLanguage;

		[XmlArrayItem(ElementName = "LyricsItem", Type = typeof(LyricsItem))]
		[XmlArray] public ArrayList SongLyrics;

		[XmlArrayItem(ElementName = "LyricsSequenceItem", Type = typeof(LyricsSequenceItem))]
		[XmlArray] public ArrayList Sequence;


		// The following settings will not be saved when we serialize this class
		private string fileName;
		private Thread render;
		private Object renderLock = new Object();
		[XmlIgnore] public Song song;
		[XmlIgnore] public Config config;
		[XmlIgnore] protected System.Type enumType;
		/// <summary>Zero based index of the current lyric (used to index this.Sequence)</summary>
		[XmlIgnore] public int CurrentLyric;
		/// <summary>We can hide/display each of the SongTextType items individually</summary>
		[XmlIgnore] public bool[] Hide = new bool[Enum.GetValues(typeof(SongTextType)).Length];
		[XmlIgnore] public string FileName {
			get { return fileName; }
			set { fileName = value; }
		}
		[XmlIgnore] public string FullName {
			get {
				if (! Tools.StringIsNullOrEmptyTrim(this.Number)) {
					return this.Number + ". " + this.Title;
				} else {
					return this.Title;
				}
			}
		}

		#region Constructors
		public NewSong(Song s) : this() {
			this.Title = s.GetText(0);
			this.FileName = s.SongName;
			this.Author = s.GetText(2);
			this.BGImagePath = s.bg_image;
			this.SetLyrics(LyricsType.Verse, s.GetText(1));
			this.DualLanguage = s.MultiLang;
			this.Collection = "Version 0.60 Format";

			CreateSimpleSequence();
		}

		public NewSong() {
			this.enumType = typeof(SongTextType);
			this.SongLyrics = new ArrayList();
			this.Sequence = new ArrayList();
			this.CurrentLyric = 0;
            this.Version = Tools.GetAppVersion();
		}

		/// <summary>
		/// This constructor creates a new song out of a plain text file. The file must contain a single
		/// song in the following format:
		/// ===
		/// Number. Song title (key)
		/// 
		/// verse 1:
		/// Line 1 of verse 1
		/// Line 2 of verse 1
		/// 
		/// verse 2:
		/// Line 1 of verse 2
		///	Line 2 of verse 2
		///	
		///	chorus 1:
		///	Line 1 of chorus 1 ...
		///	===
		///	
		///	This constructor is mainly used when importing songs stored in this text format.
		/// </summary>
		/// <param name="fileName"></param>
		public NewSong(string fileName) : this() {
			// If BOM is present, read the file as Unicode, else default to UTF-8
			string[] lines;
			Regex r;
			Match m;
			LyricsItem currItem = null;

			using (TextReader input = new StreamReader(fileName, true)) {
				lines = input.ReadToEnd().Split('\n');
			}

			foreach (string rawLine in lines) {
				string line = rawLine.TrimEnd();
				if (Regex.IsMatch(line, @"\s*#")) {
					this.Notes += Regex.Replace(line, @"\s*#\s?(.*)", @"$1");
					continue;
				}
				
				r = new Regex(@"\s*(verse|chorus)\s+(\d+):", RegexOptions.IgnoreCase);
				m = r.Match(line);

				if (m.Success && m.Groups.Count == 3) {
					// This is the beginning of a verse or chorus
					if (currItem != null) this.SongLyrics.Add(currItem);
					if (m.Groups[1].Value.ToLower() == "verse") {
						currItem = new LyricsItem(LyricsType.Verse, Convert.ToInt16(m.Groups[2].Value), "");
					} else if (m.Groups[1].Value.ToLower() == "chorus") {
						currItem = new LyricsItem(LyricsType.Chorus, Convert.ToInt16(m.Groups[2].Value), "");
					} else {
						currItem = new LyricsItem(LyricsType.Other, Convert.ToInt16(m.Groups[2].Value), "");
					}
				} else if (Regex.IsMatch(line, @"\w+")) {
					if (currItem != null) {
						currItem.Lyrics += line + "\n";
					} else if (this.Title == null) {
						// Look for title of the form: "23. Title, words! (optional key)"
						r = new Regex(@"(\d+)[\.]*\s+(.+)");
						m = r.Match(line);

						if (m.Success && m.Groups.Count == 3) {
							this.Number = m.Groups[1].Value;
							this.Title = m.Groups[2].Value.Trim();

							// If we have a key in the Title, remove it and handle it separately
							// The key looks like: Ab, or F#, or Bbm
							r = new Regex(@"(.+)\s+\(([a-gA-G#]+)\)");
							m = r.Match(this.Title);
							if (m.Success && m.Groups.Count == 3) {
								this.Title = m.Groups[1].Value.Trim();
								string key = m.Groups[2].Value;
								int minor = key.IndexOf("m");
								if (minor > 0) {
									key = key.Substring(0, minor);
									this.MinorKey = true;
								}
								this.KeyRangeHigh = key;
								this.KeyRangeLow = key;
							}
						} else {
							this.Title = line;
						}
					}
				}
			}

			if (currItem != null) this.SongLyrics.Add(currItem);

			// We end up with an extra "\n" at the end of each verse. Trim them off.
			foreach (LyricsItem item in this.SongLyrics) {
				item.Lyrics = item.Lyrics.TrimEnd();
			}

			this.CreateSimpleSequence();

			if (this.SongLyrics.Count > 0) {
				this.FileName = Regex.Replace(fileName, ".txt$", ".xml", RegexOptions.IgnoreCase);
			}
		}
		#endregion

		#region Various support methods
		/// <summary>
		/// Attempts to auto-create an inteligent sequence of verse/chorus lyrics such as
		/// v1,c1,v2,c1 or v1,c1,v2,c2 depending on how many of each lyric type there are.
		/// </summary>
		public void CreateSimpleSequence() {
			ArrayList verses = new ArrayList();
			ArrayList choruses = new ArrayList();
			ArrayList other = new ArrayList();

			this.Sequence.Clear();
			this.SongLyrics.Sort();
			foreach (LyricsItem item in this.SongLyrics) {
				switch (item.Type) {
					case LyricsType.Verse: verses.Add(item); break;
					case LyricsType.Chorus: choruses.Add(item); break;
					case LyricsType.Other: other.Add(item); break;
				}
			}

			if (verses.Count == choruses.Count) {
				// Each verse has its own chorus. Create v1, c1, v2, c2, ... sequence
				for (int i = 0; i < verses.Count; i++) {
					LyricsItem verse = (LyricsItem)verses[i];
					LyricsItem chorus = (LyricsItem)choruses[i];
					this.Sequence.Add(new LyricsSequenceItem(verse.Type, verse.Number));
					this.Sequence.Add(new LyricsSequenceItem(chorus.Type, chorus.Number));
				}
			} else if (choruses.Count == 1) {
				// Only 1 chorus present. Create v1, c1, v2, c1, ... sequence
				LyricsItem chorus = (LyricsItem)choruses[0];
				foreach (LyricsItem item in verses) {
					this.Sequence.Add(new LyricsSequenceItem(item.Type, item.Number));
					this.Sequence.Add(new LyricsSequenceItem(chorus.Type, chorus.Number));
				}
			} else {
				// We have some strange combination of verses and choirs. Just add them all sequentially.
				foreach (LyricsItem item in verses) {
					this.Sequence.Add(new LyricsSequenceItem(item.Type, item.Number));
				}
				foreach (LyricsItem item in choruses) {
					this.Sequence.Add(new LyricsSequenceItem(item.Type, item.Number));
				}					
			}

			// Add the "other" lyric types all at the end.
			foreach (LyricsItem item in other) {
				this.Sequence.Add(new LyricsSequenceItem(item.Type, item.Number));
			}
		}

		public void Init(int strophe, Config config) {
			this.config = config;
			if (this.BGImagePath == null) {
				this.BGImagePath = config.SongBGImagePath;
			}
			this.format = config.SongTextFormat;
			this.CurrentLyric = strophe;

            // Version 0.71 allowed the user to save files with keys such as "D# / Eb".
            this.KeyRangeLow = this.NormalizeKey(this.KeyRangeLow);
            this.KeyRangeHigh = this.NormalizeKey(this.KeyRangeHigh);
        }

		
		public LyricsItem GetLyrics(int seq) {
			if (seq >= this.Sequence.Count) return null;
			LyricsSequenceItem current = (LyricsSequenceItem)this.Sequence[seq];
			foreach (LyricsItem item in this.SongLyrics) {
				if (item.CompareTo(current) == 0) return item;
			}
			return null;
		}

		/// <summary>
		/// Replaces all existing lyrics of the type with new ones obtained by
		/// breaking the (user) provided text at blank lines and creating
        /// verses/choruses/etc... out of it
		/// </summary>
		/// <param name="type"></param>
		/// <param name="text"></param>
		public void SetLyrics(LyricsType type, string text) {
			ArrayList toRemove = new ArrayList();
			foreach (LyricsItem item in this.SongLyrics) {
				if (item.Type == type) toRemove.Add(item);
			}
			foreach (LyricsItem item in toRemove) {
				this.SongLyrics.Remove(item);
			}

			int num = 1;
			foreach (string v in Regex.Split(text, "\n\n")) {
				string l = v.Trim();
				if (l.Length == 0) continue;	// Don't add blank verses
				this.SongLyrics.Add(new LyricsItem(type, num++, l));
			}
		}

		/// <summary>
		/// Returns a string containing all the verses (or choruses, or other),
        /// the way a user would have typed them in.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public string GetLyrics(LyricsType type) {
			StringBuilder s = new StringBuilder("");

			this.SongLyrics.Sort();
			foreach (LyricsItem item in this.SongLyrics) {
				if (item.Type == type) s.Append(item.Lyrics + "\n\n");
			}

			return SanitizeLyrics(s.ToString());
		}

		/// <summary>
		/// Returns the words of a single verse/chorus.
		/// </summary>
		/// <param name="Item"></param>
		/// <returns></returns>
		public string GetLyrics(LyricsSequenceItem Item) {
			foreach (LyricsItem l in this.SongLyrics) {
				if (l.CompareTo(Item) == 0) return SanitizeLyrics(l.Lyrics);
			}
			return "";
		}

		/// <summary>
		/// The RichTextBox control does not show non-breaking hyphens
		/// properly, even though they show up just fine on the projector.
		/// We need to replace them with regular hyphens. 
		/// See also: http://thedotnet.com/nntp/404109/showpost.aspx and
		/// http://www.dotnet247.com/247reference/msgs/58/291787.aspx
		/// </summary>
		/// <param name="lyrics"></param>
		/// <returns></returns>
		public string SanitizeLyrics(string lyrics) {
            return lyrics;
			//return Regex.Replace(lyrics, "\u00ad", "-");
		}

		/// <summary>
		/// Searches through all the text of a song (title, notes, verses) for a phrase or a collection of words.
		/// </summary>
		/// <param name="Search">Indicates how the "Text" argument should be treated</param>
		/// <param name="Text">The phrase or words to search for</param>
		/// <returns></returns>
		public bool ContainsText(SongSearchType Search, string Text) {
			StringBuilder s = new StringBuilder("");

			s.Append( Tools.StringIsNullOrEmpty(this.Title) ? " " : this.Title + " ");
			s.Append( Tools.StringIsNullOrEmpty(this.Notes) ? " " : this.Notes + " ");
			foreach (LyricsType type in Enum.GetValues(typeof(LyricsType))) {
				s.Append(this.GetLyrics(type));
			}

			string fullText = s.ToString().ToLower();
			Text = Regex.Replace(Text, @"[\s\n]+", " ").ToLower();	// Remove multiple spaces, returns, etc...

			switch (Search) {
				case SongSearchType.Phrase:
					return fullText.IndexOf(Text) >= 0;
				case SongSearchType.Words:
					foreach (string word in Text.Split(' ')) {
						if (fullText.IndexOf(word) < 0) return false;
					}
					return true;
			}

			return false;
		}

        public string GetKey() {
            if (this.KeyRangeLow == null) { return ""; }
            return NormalizeKey(this.KeyRangeLow) + (this.MinorKey ? "m" : "");
        }

		/// <summary>
		/// Returns a normalized textual song key (converts "D# / Eb" to Eb)...
		/// </summary>
		/// <returns></returns>
		public string NormalizeKey(string keyStr) {
			if (keyStr == null) { return ""; }

            if (keyStr.Contains(@"/")) {
                string key = keyStr.Split('/')[0];
                key = key.Trim();

                switch (key) {
                    case "C#": key = "Db"; break;
                    case "D#": key = "Eb"; break;
                    case "G#": key = "Ab"; break;
                    case "A#": key = "Bb"; break;
                }
                return key;
            } else {
                return keyStr;
            }
		}
		#endregion

		/// <summary>
		/// The boolean properties are converted to a string so that we get a
		/// different hash code if they change. Hide.GetHashCode() returns the
        /// same number regardless of what the array elements contain.
		/// </summary>
		/// <returns>A hash code representing the current verse</returns>
		public int VisibleHashCode(int seq) {
			string h = "Hide";
			foreach (bool b in Hide) {
				h += (b ? "1" : "0");
			}

			return
				base.VisibleHashCode() + this.GetLyrics(seq).Lyrics.GetHashCode() +
				h.GetHashCode();
		}

		#region IContentOperations Members
		/// <summary>
		/// Gets the bitmap for the CurrentLyric
		/// </summary>
		/// <param name="Width"></param>
		/// <param name="Height"></param>
		/// <returns></returns>
		public Bitmap GetBitmap(int Width, int Height) {
			return this.GetBitmap(Width, Height, this.CurrentLyric);
		}

		public Bitmap GetBitmap(int Width, int Height, int seq) {
			if (this.RenderedFramesContains(this.VisibleHashCode(seq))) {
				Console.WriteLine("Found pre-rendered frame with: {0}", this.VisibleHashCode(seq));
				return this.RenderedFramesGet(this.VisibleHashCode(seq)) as Bitmap;
			} else {
				Console.WriteLine("Rendering song frame for {0}", this.VisibleHashCode(seq));
			}

			lock(renderLock) {
				Pen p;
				SolidBrush brush;
				Bitmap bmp = new Bitmap(Width, Height);
				Graphics graphics = Graphics.FromImage(bmp);
				GraphicsPath pth, pth2;
				Rectangle pathRect, bounds;
				RectangleF measuredBounds;
				Font font;
				float fontSz;
				string[] text = new string[ Enum.GetValues(typeof(SongTextType)).Length ];

				graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
				graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
				graphics.SmoothingMode = SmoothingMode.AntiAlias;
				graphics.SmoothingMode = SmoothingMode.HighQuality;
				graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

				this.RenderBGImage(config.SongBGImagePath, graphics, Width, Height);

				if (HideText) {
					graphics.Dispose();
					return bmp;
				}

				LyricsItem lyrics = this.GetLyrics(seq);
				text[ (int)SongTextType.Title ] = (this.Title == null) ? "" : this.Title;
				text[ (int)SongTextType.Verse ] = (lyrics == null) ? "" : lyrics.Lyrics;
				text[ (int)SongTextType.Author ] = (this.Author == null) ? "" : this.Author;

			
				string songKey = this.GetKey();
				if (songKey.Length > 0) {
					// Add the key to the author field?
					text[ (int)SongTextType.Author ] += " (" + songKey + ")";
				}

				pth = new GraphicsPath();

				foreach (int type in Enum.GetValues( this.enumType )) {
					if (this.Hide[type]) continue;
					// We have to keep the text within these user-specified boundaries
					bounds = new System.Drawing.Rectangle(
						(int)(format[type].Bounds.X / 100 * Width),
						(int)(format[type].Bounds.Y / 100 * Height),
						(int)(format[type].Bounds.Width / 100 * Width),
						(int)(format[type].Bounds.Height / 100 * Height));

					if (text[type].Length > 0) {
						p = new Pen(format[type].TextColor, 1.0F);
						p.LineJoin = LineJoin.Round;
						StringFormat sf = new StringFormat();
						sf.Alignment = format[type].HAlignment;
						sf.LineAlignment = format[type].VAlignment;

						if (this.WordWrap) {
							// Make a rectangle that is very tall to see how far down the text would stretch.
							pathRect = bounds;
							pathRect.Height *= 2;
				
							// We start with the user-specified font size ...
							fontSz = format[type].TextFont.Size;
					
							// ... and decrease the size (if needed) until it fits within the user-specified boundaries
							do {
								font = new Font(format[type].TextFont.FontFamily, fontSz, format[type].TextFont.Style);
								pth.Reset();
								pth.AddString(text[type], font.FontFamily, (int)font.Style, font.Size, pathRect, sf);
								measuredBounds = pth.GetBounds();
								fontSz--;
								if (fontSz == 0) break;
							} while (measuredBounds.Height > bounds.Height);

							// We need to re-create the path. For some reason the do-while loop puts it in the wrong place.
							pth.Reset();
							pth.AddString(text[type], font.FontFamily, (int)font.Style, font.Size, bounds, sf);

						} else {
							// Tab characters are ignored by AddString below. Converting them to spaces.
							text[type] = Regex.Replace(text[type], "\t", "        ");
							pth = new GraphicsPath();
							font = new Font(format[type].TextFont.FontFamily, format[type].TextFont.Size, format[type].TextFont.Style);
							pth.AddString(text[type], font.FontFamily, (int)font.Style, font.Size, new Point(0,0), sf);

							pth.Transform( Tools.FitContents(bounds, pth.GetBounds(), sf));
						}

						brush = new SolidBrush(format[type].TextColor);

						#region Add special effects
						if (format[type].Effects == "Outline") {
							p = new Pen(format[type].OutlineColor, format[type].OutlineSize);
							graphics.DrawPath(p, pth);

						} else if (format[type].Effects == "Filled Outline") {
							pth2 = (GraphicsPath)pth.Clone();
							graphics.FillPath(brush, pth);
							graphics.DrawPath(p, pth);

							// Widen the path
							Pen widenPen = new Pen(format[type].OutlineColor, format[type].OutlineSize);
							widenPen.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
							pth2.Widen(widenPen);
							graphics.FillPath(new SolidBrush(format[type].OutlineColor), pth2);
							graphics.DrawPath(p, pth);
							graphics.FillPath(brush, pth);

						} else { //if (format[type].Effects == "Normal") {
							graphics.FillPath(brush, pth);
							graphics.DrawPath(p, pth);

						}
						#endregion
					}
				}

				graphics.Dispose();
				if (this.config != null && this.config.PreRender) {
					this.RenderedFramesSet(this.VisibleHashCode(seq), bmp);
				}

				return bmp;
			}
		}

		public bool Next() {
			if (this.CurrentLyric < this.Sequence.Count - 1) {
				this.CurrentLyric++;
				return true;
			}
			return false;
		}

		public bool Prev() {
			if (this.CurrentLyric > 0) {
				this.CurrentLyric--;
				return true;
			}
			return false;
		}

		public void ChangeBGImagePath(string newPath) {
			this.BGImagePath = newPath;
		}
        public void ChangeTheme(Theme t) {
            ChangeBGImagePath(t.BGImagePath);
            this.format = t.TextFormat;
        }

		public virtual ContentIdentity GetIdentity() {
			ContentIdentity ident = new ContentIdentity();
			ident.Type = (int)ContentType.Song;
			ident.SongName = Path.GetFileName(this.FileName);
			ident.SongStrophe = this.CurrentLyric;
			return ident;
		}

		private void PreRenderFramesThreaded() {
			if (this.config != null && this.config.BeamBoxSizeX > 1 && this.config.BeamBoxSizeY > 1) {
				for (int i = 0; i < this.Sequence.Count; i++) {
					this.GetBitmap(this.config.BeamBoxSizeX, this.config.BeamBoxSizeY, i);
				}
			}
		}

		public void PreRenderFrames() {
			if (render != null && render.IsAlive) {
				render.Abort();
			}

			render = new Thread(new ThreadStart(PreRenderFramesThreaded));
			render.IsBackground = true;
			render.Name = "PreRender Song";
			render.Start();
		}

		#endregion

		#region ICloneable Members

		object ICloneable.Clone() {
			return this.Clone();
		}

		public virtual NewSong Clone() {
			// TODO: Make a proper clone, or else the Live screen will show updates made to preview screen.
			NewSong s = this.MemberwiseClone() as NewSong;
			s.SongLyrics = (ArrayList)this.SongLyrics.Clone();
			s.Sequence = (ArrayList)this.Sequence.Clone();

			// We must use a separate song, so that we can change strophes independently. Otherwise the
			// MemberwiseClone above will give us two content objects that refer to the same song object.
			//s.song = this.song.Clone();

			return s;
		}

		#endregion

		#region Serialization and Deserialization
		/// <summary>
		/// Handles serialization of the Config class, or of any types derived from it.
		/// </summary>
		/// <param name="instance">The instance to serialize</param>
		/// <param name="file">The XML file to serialize to</param>
		public static void SerializeTo(NewSong instance, string file) {
			// We need to save these and restore them after serializing, or else whoever is holding
			// a reference to this "instance" will end up with a broken object because we set these
			// to "null" below so that we don't serialize them.
			BeamTextFormat[] savedFormat = instance.format;
			string savedBGImagePath = instance.BGImagePath;

			Type type = instance.GetType();
			XmlSerializer xs = new XmlSerializer(type);

			Directory.CreateDirectory(Path.GetDirectoryName(file));
			FileStream fs = null;

			if (! instance.CustomFormat) {
				instance.format = null;
				instance.BGImagePath = null;
			}

            // When files are de-serialized, they retain the version they were
            // originally serialized under. We need to update it here.
            instance.Version = Tools.GetAppVersion();

			try {
				fs = File.Open(file, FileMode.Create, FileAccess.Write, FileShare.Read);
				xs.Serialize(fs, instance);
			} finally {
				if (fs != null) {
					fs.Close();
				}
				instance.format = savedFormat;
				instance.BGImagePath = savedBGImagePath;
			}
		}

		/// <summary>
		/// Handles deserialization of the NewSong class, or of any types derived from it.
		/// </summary>
		/// <param name="type">The type of class to deserialize. Allows this function to be used in derived classes.</param>
		/// <param name="tr">A TextReader containing the document to deserialize</param>
		/// <returns></returns>
		public static object DeserializeFrom(System.Type type, TextReader tr) {
			XmlSerializer xs = null;

			try {
				xs = new XmlSerializer(type);
			} catch (InvalidOperationException ex) {
				// Invalid class. Does the class have a public constructor?
				Console.WriteLine("DeserializeFrom exception: " + ex.Message);
			}
			
			if (xs != null) {
				try {
					return xs.Deserialize(tr);
				} catch (InvalidOperationException e) {
					Console.WriteLine("Exception deserializing (Invalid XML or old format): " + e.Message);
				}
			}

			return null;
		}

		/// <summary>
		/// Handles deserialization of the NewSong class, or of any types derived from it.
		/// </summary>
		/// <param name="type">The type of class to deserialize. Allows this function to be used in derived classes.</param>
		/// <param name="file">The full path to the XML file to deserialize</param>
		/// <returns></returns>
        public static object DeserializeFrom(System.Type type, string file) {
            try {
                using (FileStream fs = File.Open(file, FileMode.Open, FileAccess.Read)) {
                    return DeserializeFrom(type, new StreamReader(fs));
                }
            } catch (FileNotFoundException e) {
				Console.WriteLine("Exception deserializing (FileNotFound): " + e.Message);
			}
            return null;
        }

		public static object DeserializeFrom(string file, int strophe, Config config) {
            XmlDocument xmlDoc = new XmlDocument();
            try {
                xmlDoc.Load(file);
            } catch {
                return new NewSong();
            }

            NewSong s = null;
            //XmlNodeList nodes = xmlDoc.GetElementsByTagName("Version");
            XmlNode version = xmlDoc.SelectSingleNode(@"/DreamSong/Version");
            if (version == null) {
                version = xmlDoc.SelectSingleNode(@"/NewSong/Version");
            }
            if (version != null) {                
                switch (version.InnerText) {
                    case "0.49":
				        Song oldS = new Song(file);
				        if (oldS.strophe_count > 0) {
					        s = new NewSong(oldS);
				        } else {
					        s = new NewSong();
				        }
                        break;

                    case "0.60":
                        // Fix old songs that were saved with NewSong XML root instead of DreamSong
                        XmlNode root = xmlDoc.DocumentElement;
                        if (root.Name.Equals("NewSong")) {
                            Tools.RenameXmlNode(root, root.NamespaceURI, "DreamSong");
                            s = (NewSong)NewSong.DeserializeFrom(typeof(NewSong), new StringReader(xmlDoc.OuterXml));
                        } else {
                            s = (NewSong)NewSong.DeserializeFrom(typeof(NewSong), file);
                        }
                        break;

                    //case "0.72":
                    default:
		                s = (NewSong)NewSong.DeserializeFrom(typeof(NewSong), file);
                        break;
                }
            }

            if (s != null) {
                if (!s.CustomFormat) {
                    s.Init(strophe, config);
                } else {
                    s.config = config;
                    s.CurrentLyric = strophe;
                }
                s.FileName = file;
                return s;
            } else {
                return new NewSong();
            }
		}
		#endregion

	}

}
