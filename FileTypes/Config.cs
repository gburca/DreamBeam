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


namespace DreamBeam {

	#region Enums
	public enum OperatingMode {
		StandAlone,
		Client,
		Server
	}

	public enum SongVerseSeparator {
		OneBlankLine,
		TwoBlankLines
	}
	#endregion


	[Serializable()]
	public class Config : ICloneable {

		#region Variables and Properties
		public string Version;
		public int BeamBoxPosX = 0;
		public int BeamBoxPosY = 0;
		public int BeamBoxSizeX = 800;
		public int BeamBoxSizeY = 600;
		public bool BeamBoxAutoPosSize = false;
		public int BeamBoxScreenNum = -1;
		public bool PreRender = false;
		public bool Alphablending = false;
		private int blendSpeed = 10;
        public int BlendSpeed {
            get { return blendSpeed; }
            set { blendSpeed = Math.Min(Math.Max(1, value), 255); }
        }
		public bool useDirect3D = false;		
		public string BibleLang = "en";
		public string LastBibleUsed = "";
		public bool ShowBibleTranslation = false;
		public string PlayListString = "";
		public bool RememberPanelLocations = false;
        public bool useModernSongEditor = true;

		[XmlIgnore]
		public ArrayList PlayList = new ArrayList();

		[XmlIgnore]
		public static string defaultSwordPath = @"C:\Program Files\CrossWire\The SWORD Project";
		private string swordPath = "";
		public string SwordPath {
			get {
				if (swordPath == null || swordPath.Length < 4) {
					// We don't have a reasonable path for Sword. Try using the default.
					this.SwordPath = defaultSwordPath;
				}
				return swordPath;
			}
			set {
				// The "value" could be null, or not of a legal form
				try {
					DirectoryInfo dir = new DirectoryInfo(value);
					if (dir.Exists) {
						FileInfo f = new FileInfo(Path.Combine(dir.FullName, "sword.exe"));
						if (f.Exists) {
							swordPath = f.DirectoryName;
							return;
						}
					}
				} catch { }
			}
		}

		public bool HideMouse = true;
		public bool AlwaysOnTop = false;

		[XmlIgnore]
		public System.Drawing.Color BackgroundColor = Color.Black;
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public string XmlBackgroundColor {
			get { return DreamTools.SerializeColor(BackgroundColor); }
			set { BackgroundColor = DreamTools.DeserializeColor(value); }
		}

		public bool LoopMedia = false;
		public bool LoopAutoPlay = false;
		public int AutoPlayChangeTime = 2;
		public string Language = "auto";

		[XmlElement("Theme")]
		public ComboTheme theme;

        [XmlElement("ThemePaths")]
        public ComboThemePaths DefaultThemes;

		public SongVerseSeparator SongVerseSeparator = SongVerseSeparator.OneBlankLine;
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
		[XmlIgnore]
		public System.Data.DataSet Options_DataSet;

		#endregion

		public Config() {
			Version = DreamTools.GetAppVersion();
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
            DefaultThemes = new ComboThemePaths();
			AppOperatingMode = OperatingMode.StandAlone;
			ListeningPort = 50000;
		}

		string GetLocation(Type type) {
			return DreamTools.GetDirectory(DirType.Config, type.Name + ".xml");
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

			instance.Version = DreamTools.GetAppVersion();

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

			// Versions prior to 0.72 had fewer TextFormats
			config.theme.Bible.CreateTextFormats(Enum.GetValues(typeof(BibleTextType)).Length);
			config.theme.Song.CreateTextFormats(Enum.GetValues(typeof(SongTextType)).Length);
			config.theme.Sermon.CreateTextFormats(Enum.GetValues(typeof(TextToolType)).Length);


            
            if (config.DefaultThemes.SongThemePath != "" && File.Exists(Path.Combine(DreamTools.GetDirectory(DirType.DataRoot), config.DefaultThemes.SongThemePath)))
            {
                config.theme.Song.ThemeFile = Path.Combine(DreamTools.GetDirectory(DirType.DataRoot), config.DefaultThemes.SongThemePath);
            }
            else if (File.Exists(Path.Combine(DreamTools.GetDirectory(DirType.DataRoot), "Themes\\Default.SongTheme.xml")))
            {                
                config.DefaultThemes.SongThemePath = "Themes\\Default.SongTheme.xml";
                config.theme.Song.ThemeFile = Path.Combine(DreamTools.GetDirectory(DirType.DataRoot), config.DefaultThemes.SongThemePath);
            }




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
			string fullPath = DreamTools.GetFullPathOrNull(DreamTools.GetDirectory(DirType.Config), file);


			try {
				xs = new XmlSerializer(type);
			} catch (InvalidOperationException ex) {
				// Invalid class. Does the class have a public constructor?
				Console.WriteLine("DeserializeFrom exception: " + ex.Message);
			}

			if (!DreamTools.FileExists(fullPath)) {
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
