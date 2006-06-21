using System;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Xml.Serialization;
using System.Text.RegularExpressions;
using System.Collections;
using DreamBeam.FileTypes;


namespace DreamBeam
{

	#region Enums
	public enum OperatingMode {
		StandAlone,
		Client,
		Server
	}
	#endregion

	[Serializable()]
	public class Config
	{

		#region Variables and Properties
		public int BeamBoxPosX = 0;
		public int BeamBoxPosY = 0;
		public int	BeamBoxSizeX = 640;
		public int BeamBoxSizeY = 480;
		public bool BeamBoxAutoPosSize = true;
		public int BeamBoxScreenNum = -1;
		public bool PreRender = false;
		public bool Alphablending = false;
		public int BlendSpeed = 10;
		public bool useDirect3D = false;
		public float OutlineSize = 3;
		public string BibleLang = "en";
		public string LastBibleUsed = "";
		public bool ShowBibleTranslation = false;
		public string PlayListString = "";
		public bool RememberPanelLocations = false;

		[XmlIgnore] public ArrayList PlayList = new ArrayList();
		
//		[XmlArrayItem(ElementName = "ContentIdentity", Type = typeof(ContentIdentity))]
//		[XmlArray] public ArrayList PlayListSequence {
//			get {
//				ArrayList list = new ArrayList(this.PlayList.Count);
//				foreach (IContentItem item in this.PlayList) {
//					list.Add(item.GetIdentity());
//				}
//				return list;
//			}
//			set {
//				ArrayList list = value as ArrayList;
//				if (list == null) return;
//				foreach (ContentIdentity item in list) {
//					//this.PlayList.Add();
//				}
//			}
//		}


		private string swordPath = "";
		public string SwordPath {
			get { return swordPath; }
			set {
				// The "value" could be null, or not of a legal form
				try {
					DirectoryInfo dir = new DirectoryInfo(value);
					if (dir.Exists) {
						FileInfo f = new FileInfo( Path.Combine(dir.FullName, "sword.exe") );
						if (f.Exists) {
							swordPath = f.DirectoryName;
							return;
						}
					}
				} catch {}
			}
		}

		public bool HideMouse = false;
		public bool AlwaysOnTop = false;

		[XmlIgnore()] public static readonly string ProgramDir = Tools.DreamBeamPath();
		public System.Drawing.Color BackgroundColor = Color.Black;
		public bool LoopMedia = false;
		public bool LoopAutoPlay = false;
		public int AutoPlayChangeTime = 2;
		public string Language = "auto";
		public string BibleBGImagePath = null;
		public string SongBGImagePath = null;
		public string TextBGImagePath = null;

		public BeamTextFormat[] BibleTextFormat;
		public BeamTextFormat[] SongTextFormat;
		public BeamTextFormat[] SermonTextFormat;

		public OperatingMode AppOperatingMode;

		/// <summary>
		/// When operating in client mode, this is the server address the client will attempt
		/// to connect to. It is of the form: http://SomeLocation.com:port/some/path
		/// </summary>
		public string ServerAddress;

		/// <summary>
		/// When operating in server mode, this is the port we listen on for client connections.
		/// </summary>
		public int ListeningPort;

		public Rectangle AppDesktopLocation = new Rectangle(50, 50, 800, 600);
		public System.Data.DataSet Options_DataSet;

		#endregion

		public Config() {
			Init();
		}

		public void Init() {
			int formats;
			
			// Create the array
			formats = Enum.GetValues(typeof(BibleTextType)).Length;
			BibleTextFormat = new BeamTextFormat[formats];
			// Populate the array
			for (int i = 0; i < formats; i++) {
				BibleTextFormat[i] = new BeamTextFormat();
			}

			formats = Enum.GetValues(typeof(SongTextType)).Length;
			SongTextFormat = new BeamTextFormat[formats];
			for (int i = 0; i < formats; i++) {
				SongTextFormat[i] = new BeamTextFormat();
			}

			formats = Enum.GetValues(typeof(TextToolType)).Length;
			SermonTextFormat = new BeamTextFormat[formats];
			for (int i = 0; i < formats; i++) {
				SermonTextFormat[i] = new BeamTextFormat();
			}

			AppOperatingMode = OperatingMode.StandAlone;
			ListeningPort = 50000;
		}

		string GetLocation(Type type) {
			return ProgramDir + type.Name + ".xml";
		}

		/// <summary>
		/// If the file has an absolute path, it returns the file, otherwise it constructs
		/// an absolute path relative to the ProgramDir value.
		/// </summary>
		/// <param name="file"></param>
		/// <returns></returns>
		public static string GetAbsoluteLocation(string file) {
			if (Path.IsPathRooted(file)) {
				return file;
			} else {
				return Path.Combine(Config.ProgramDir, file);
			}
		}


		/// <summary>
		/// Handles serialization of the Config class, or of any types derived from it.
		/// </summary>
		/// <param name="instance">The instance to serialize</param>
		/// <param name="file">The XML file to serialize to</param>
		public static void SerializeTo(Config instance, string file) {
			Type type = instance.GetType();
			XmlSerializer xs = new XmlSerializer(type);

			file = GetAbsoluteLocation(file);
			Directory.CreateDirectory(Path.GetDirectoryName(file));
			FileStream fs = null;

			try {
				fs = File.Open(file, FileMode.Create, FileAccess.Write, FileShare.Read);
				xs.Serialize(fs, instance);
			} catch (Exception e) {
				Console.WriteLine(e.Message);
			} finally {
				if (fs != null) {
					fs.Close();
				}
			}
		}


		/// <summary>
		/// Handles deserialization of the Config class, or of any types derived from it.
		/// </summary>
		/// <param name="instance">An instance of the class to deserialize</param>
		/// <param name="file">The XML file to deserialize</param>
		/// <returns></returns>
		public static object DeserializeFrom(Config instance, string file) {
			Type type = instance.GetType();
			XmlSerializer xs = null;

			file = GetAbsoluteLocation(file);
			try {
				xs = new XmlSerializer(type);
			} catch (InvalidOperationException ex) {
				// Invalid class. Does the class have a public constructor?
				Console.WriteLine("DeserializeFrom exception: " + ex.Message);
			}
			
			if (xs != null) {
				try {
					using (FileStream fs = File.Open(file, FileMode.Open, FileAccess.Read)) {
						return xs.Deserialize(fs);
					}
				} catch (FileNotFoundException) {
					Config.SerializeTo(instance, file);
				} catch (InvalidOperationException) {
					// Invalid XML code
					Config.SerializeTo(instance, file);
				}
			}

			return instance;
		}


	} // End of Config class
}
