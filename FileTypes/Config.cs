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
    public class Theme {
        public string BGImagePath = null;
        public BeamTextFormat[] TextFormat;

        public Theme(ContentType type) {
            switch (type) {
                case ContentType.Song:
                    CreateDefaultSongTheme();
                    break;
                case ContentType.BibleVerse:
                    CreateDefaultBibleTheme();
                    break;
                case ContentType.PlainText:
                    CreateDefaultSermonTextTheme();
                    break;
            }
        }

        private void CreateTextFormats(int size) {
            // Create the array
            TextFormat = new BeamTextFormat[size];
            // Populate the array
            for (int i = 0; i < size; i++) {
                TextFormat[i] = new BeamTextFormat();
            }
        }

        private void CreateDefaultSongTheme() {
            CreateTextFormats(Enum.GetValues(typeof(SongTextType)).Length);
            TextFormat[(int)SongTextType.Title].Bounds = new RectangleF(5F, 2F, 90F, 8F);
            TextFormat[(int)SongTextType.Verse].Bounds = new RectangleF(5F, 12F, 90F, 83F);
            TextFormat[(int)SongTextType.Author].Bounds = new RectangleF(80F, 95F, 15F, 4F);
            TextFormat[(int)SongTextType.Verse].HAlignment = StringAlignment.Near;
        }

        private void CreateDefaultBibleTheme() {
            CreateTextFormats(Enum.GetValues(typeof(BibleTextType)).Length);
            TextFormat[(int)BibleTextType.Reference].Bounds = new RectangleF(5F, 2F, 90F, 8F);
            TextFormat[(int)BibleTextType.Verse].Bounds = new RectangleF(5F, 12F, 90F, 83F);
            TextFormat[(int)BibleTextType.Translation].Bounds = new RectangleF(80F, 95F, 15F, 4F);
        }

        private void CreateDefaultSermonTextTheme() {
            CreateTextFormats(Enum.GetValues(typeof(TextToolType)).Length);
            TextFormat[(int)TextToolType.FirstLine].Bounds = new RectangleF(5F, 2F, 90F, 8F);
            TextFormat[(int)TextToolType.OtherLines].Bounds = new RectangleF(5F, 12F, 90F, 85F);
            TextFormat[(int)TextToolType.OtherLines].HAlignment = StringAlignment.Near;
        }
    }

    [Serializable()]
    public class Themes {
        public Theme[] themes;
    }

	[Serializable()]
	public class Config
	{

		#region Variables and Properties
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

        public Theme BibleTheme, SongTheme, SermonTextTheme;

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

		/// <summary>
		/// In case the user deletes the default.config.xml file, or mucks with
        /// it by hand and messes it up, these are the defaults they will end up
        /// with, so we try to create something reasonable here, but nothing too
        /// fancy.
		/// </summary>
		public void Init() {
			int formats;
			
			// Create the array
			formats = Enum.GetValues(typeof(BibleTextType)).Length;
			BibleTextFormat = new BeamTextFormat[formats];
			// Populate the array
			for (int i = 0; i < formats; i++) {
				BibleTextFormat[i] = new BeamTextFormat();
			}
			BibleTextFormat[(int)BibleTextType.Reference].Bounds = new RectangleF(5F, 2F, 90F, 8F);
			BibleTextFormat[(int)BibleTextType.Verse].Bounds = new RectangleF(5F, 12F, 90F, 83F);
			BibleTextFormat[(int)BibleTextType.Translation].Bounds = new RectangleF(80F, 95F, 15F, 4F);

			formats = Enum.GetValues(typeof(SongTextType)).Length;
			SongTextFormat = new BeamTextFormat[formats];
			for (int i = 0; i < formats; i++) {
				SongTextFormat[i] = new BeamTextFormat();
			}
			SongTextFormat[(int)SongTextType.Title].Bounds = BibleTextFormat[(int)BibleTextType.Reference].Bounds;
			SongTextFormat[(int)SongTextType.Verse].Bounds = BibleTextFormat[(int)BibleTextType.Verse].Bounds;
			SongTextFormat[(int)SongTextType.Author].Bounds = BibleTextFormat[(int)BibleTextType.Translation].Bounds;
			SongTextFormat[(int)SongTextType.Verse].HAlignment = StringAlignment.Near;

			formats = Enum.GetValues(typeof(TextToolType)).Length;
			SermonTextFormat = new BeamTextFormat[formats];
			for (int i = 0; i < formats; i++) {
				SermonTextFormat[i] = new BeamTextFormat();
			}
			SermonTextFormat[(int)TextToolType.FirstLine].Bounds = new RectangleF(5F, 2F, 90F, 8F);
			SermonTextFormat[(int)TextToolType.OtherLines].Bounds = new RectangleF(5F, 12F, 90F, 85F);
			SermonTextFormat[(int)TextToolType.OtherLines].HAlignment = StringAlignment.Near;

			AppOperatingMode = OperatingMode.StandAlone;
			ListeningPort = 50000;
		}

		string GetLocation(Type type) {
			return Tools.CombinePaths(Tools.GetAppDocPath(), type.Name + ".xml");
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
				return Path.Combine(Tools.GetAppDocPath(), file);
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
						return DeserializeCleanup(xs.Deserialize(fs) as Config);
					}
				} catch (FileNotFoundException) {
					Config.SerializeTo(instance, file);
				} catch (InvalidOperationException) {
					// Invalid XML code
					Config.SerializeTo(instance, file);
				}
			}

			return DeserializeCleanup(instance);
		}


	} // End of Config class
}
