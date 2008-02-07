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
using System.ComponentModel;
using System.Runtime.Serialization.Formatters.Binary;


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
	public class Config : ICloneable
	{

		#region Variables and Properties
        public string Version;
		public int BeamBoxPosX = 0;
		public int BeamBoxPosY = 0;
		public int	BeamBoxSizeX = 800;
		public int BeamBoxSizeY = 600;
		public bool BeamBoxAutoPosSize = false;
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

		public bool HideMouse = true;
		public bool AlwaysOnTop = false;

		[XmlIgnore] public System.Drawing.Color BackgroundColor = Color.Black;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public string XmlBackgroundColor {
            get { return Tools.SerializeColor(BackgroundColor); }
            set { BackgroundColor = Tools.DeserializeColor(value); }
        }

		public bool LoopMedia = false;
		public bool LoopAutoPlay = false;
		public int AutoPlayChangeTime = 2;
		public string Language = "auto";

        [XmlElement("Theme")]
        public ComboTheme theme;
        #region Theme accessors
        [XmlIgnore]
        public string BibleBGImagePath {
            get { return theme.getTheme(ContentType.BibleVerse).BGImagePath; }
            set { theme.getTheme(ContentType.BibleVerse).BGImagePath = value; }
        }
        [XmlIgnore]
        public string SongBGImagePath {
            get { return theme.getTheme(ContentType.Song).BGImagePath; }
            set { theme.getTheme(ContentType.Song).BGImagePath = value; }
        }
        [XmlIgnore]
        public string TextBGImagePath {
            get { return theme.getTheme(ContentType.PlainText).BGImagePath; }
            set { theme.getTheme(ContentType.PlainText).BGImagePath = value; }
        }
        [XmlIgnore]
        public BeamTextFormat[] BibleTextFormat {
            get { return theme.getTheme(ContentType.BibleVerse).TextFormat; }
        }
        [XmlIgnore]
        public BeamTextFormat[] SongTextFormat {
            get { return theme.getTheme(ContentType.Song).TextFormat; }
        }
        [XmlIgnore]
        public BeamTextFormat[] SermonTextFormat {
            get { return theme.getTheme(ContentType.PlainText).TextFormat; }
        }
        #endregion

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
		[XmlIgnore] public System.Data.DataSet Options_DataSet;

		#endregion

		public Config() {
            Version = Tools.GetAppVersion();
			Init();
		}

		/// <summary>
		/// In case the user deletes the default.config.xml file, or mucks with
        /// it by hand and messes it up, these are the defaults they will end up
        /// with, so we try to create something reasonable here, but nothing too
        /// fancy.
		/// </summary>
		public void Init() {
            theme = new ComboTheme();

			AppOperatingMode = OperatingMode.StandAlone;
			ListeningPort = 50000;
		}

		string GetLocation(Type type) {
			return Tools.CombinePaths(Tools.GetAppDocPath(), type.Name + ".xml");
		}

		/// <summary>
		/// Handles serialization of the Config class, or of any types derived from it.
		/// </summary>
		/// <param name="instance">The instance to serialize</param>
		/// <param name="file">The XML file to serialize to</param>
		public static void SerializeTo(Config instance, string file) {
			Type type = instance.GetType();
			XmlSerializer xs = new XmlSerializer(type);

			file = Path.GetFullPath(file);
			Directory.CreateDirectory(Path.GetDirectoryName(file));
			FileStream fs = null;

            instance.Version = Tools.GetAppVersion();

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

		public static Config DeserializeCleanup(Config config) {
			if (config.BackgroundColor.IsEmpty) config.BackgroundColor = Color.Black;
			return config;
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
            string fullPath = Tools.GetFullPathOrNull(file);


			try {
				xs = new XmlSerializer(type);
			} catch (InvalidOperationException ex) {
				// Invalid class. Does the class have a public constructor?
				Console.WriteLine("DeserializeFrom exception: " + ex.Message);
			}

            if (!Tools.FileExists(fullPath)) {
                // fullPath could be NULL here, so use "file"
                Config.SerializeTo(instance, file);
            } else if (xs != null) {
				try {
					using (FileStream fs = File.Open(fullPath, FileMode.Open, FileAccess.Read)) {
                        Config config = xs.Deserialize(fs) as Config;
                        if (config != null) {
                            return DeserializeCleanup(config);
                        } else {
                            Config.SerializeTo(instance, fullPath);
                        }
					}
				} catch (FileNotFoundException) {
					Config.SerializeTo(instance, fullPath);
				} catch (InvalidOperationException) {
					// Invalid XML code
					Config.SerializeTo(instance, fullPath);
				}
			}

			return DeserializeCleanup(instance);
		}



        #region ICloneable Members

        public virtual object Clone() {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, this);
            ms.Position = 0;
            object clone = bf.Deserialize(ms);
            ms.Close();
            return clone;
        }

        #endregion
    } // End of Config class
}
