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

        public Theme() {}

        protected void CreateTextFormats(int size) {
            // Create the array
            TextFormat = new BeamTextFormat[size];
            // Populate the array
            for (int i = 0; i < size; i++) {
                TextFormat[i] = new BeamTextFormat();
            }
        }

        public static Theme OpenFile(string FileDialogFilter, Type type) {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.DefaultExt = "xml";
            dialog.Filter = FileDialogFilter;
            dialog.FilterIndex = 1;
            dialog.InitialDirectory = Path.Combine(Tools.GetAppDocPath(), "Themes");
            dialog.Title = "Open Theme";
            dialog.Multiselect = false;

            if (dialog.ShowDialog() == DialogResult.OK) {
                return DeserializeFrom(type, dialog.FileName) as Theme;
            } else {
                return null;
            }
        }

        public void SaveAs(string FileDialogFilter) {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.DefaultExt = "xml";
            dialog.Filter = FileDialogFilter;
            dialog.FilterIndex = 1;
            dialog.InitialDirectory = Path.Combine(Tools.GetAppDocPath(), "Themes");
            dialog.Title = "Save Theme As";

            Directory.CreateDirectory(dialog.InitialDirectory);

            if (dialog.ShowDialog() == DialogResult.OK) {
                string fileName = dialog.FileName;
                try {
                    SerializeTo(this, fileName);
                    //this.StatusPanel.Text = Lang.say("Status.SongSavedAs", this.SaveFileDialog.FileName);
                } catch (Exception ex) {
                    MessageBox.Show("Theme not saved: " + ex.Message);
                }
            }
        }

        public static void SerializeTo(Theme instance, string file) {
            XmlSerializer xs = new XmlSerializer(instance.GetType());

            file = Path.GetFullPath(file);
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

        public static object DeserializeFrom(Type type, string file) {
            XmlSerializer xs = null;

            file = Tools.GetFullPath(file);
            if (file == null) { return null; }
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
                    return null;
                } catch (InvalidOperationException) {
                    // Invalid XML code
                    return null;
                }
            }

            return null;
        }

    }

    [Serializable()]
    public class BibleTheme : Theme {
        protected static string FileDialogFilter = @"DreamBeam Bible themes (*.BibleTheme.xml)|*.BibleTheme.xml|All (*.*)|*.*";

        public BibleTheme() {
            CreateTextFormats(Enum.GetValues(typeof(BibleTextType)).Length);
            TextFormat[(int)BibleTextType.Reference].Bounds = new RectangleF(5F, 2F, 90F, 8F);
            TextFormat[(int)BibleTextType.Verse].Bounds = new RectangleF(5F, 12F, 90F, 83F);
            TextFormat[(int)BibleTextType.Translation].Bounds = new RectangleF(80F, 95F, 15F, 4F);
        }

        public static BibleTheme OpenFile() {
            return Theme.OpenFile(FileDialogFilter, typeof(BibleTheme)) as BibleTheme;
        }
        public void SaveAs() {
            SaveAs(FileDialogFilter);
        }
    }

    [Serializable()]
    public class SongTheme : Theme {
        protected static string FileDialogFilter = @"DreamBeam song themes (*.SongTheme.xml)|*.SongTheme.xml|All (*.*)|*.*";

        public SongTheme() {
            CreateTextFormats(Enum.GetValues(typeof(SongTextType)).Length);
            TextFormat[(int)SongTextType.Title].Bounds = new RectangleF(5F, 2F, 90F, 8F);
            TextFormat[(int)SongTextType.Verse].Bounds = new RectangleF(5F, 12F, 90F, 83F);
            TextFormat[(int)SongTextType.Author].Bounds = new RectangleF(80F, 95F, 15F, 4F);
            TextFormat[(int)SongTextType.Verse].HAlignment = StringAlignment.Near;
        }

        public static SongTheme OpenFile() {
            return Theme.OpenFile(FileDialogFilter, typeof(SongTheme)) as SongTheme;
        }
        public void SaveAs() {
            SaveAs(FileDialogFilter);
        }
    }

    [Serializable()]
    public class SermonTheme : Theme {
        protected static string FileDialogFilter = @"DreamBeam sermon themes (*.SermonTheme.xml)|*.SermonTheme.xml|All (*.*)|*.*";

        public SermonTheme() {
            CreateTextFormats(Enum.GetValues(typeof(TextToolType)).Length);
            TextFormat[(int)TextToolType.FirstLine].Bounds = new RectangleF(5F, 2F, 90F, 8F);
            TextFormat[(int)TextToolType.OtherLines].Bounds = new RectangleF(5F, 12F, 90F, 85F);
            TextFormat[(int)TextToolType.OtherLines].HAlignment = StringAlignment.Near;
        }

        public static SermonTheme OpenFile() {
            return Theme.OpenFile(FileDialogFilter, typeof(SermonTheme)) as SermonTheme;
        }
        public void SaveAs() {
            SaveAs(FileDialogFilter);
        }
    }

    [Serializable()]
    public class ComboTheme {
        public SongTheme Song;
        public BibleTheme Bible;
        public SermonTheme Sermon;

        public ComboTheme() {
            Song = new SongTheme();
            Bible = new BibleTheme();
            Sermon = new SermonTheme();
        }

        public Theme getTheme(ContentType type) {
            switch (type) {
                case ContentType.Song:
                    return Song;
                case ContentType.PlainText:
                    return Sermon;
                //case ContentType.BibleVerse:
                default:
                    return Bible;
            }
        }
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
            get { return theme.getTheme(ContentType.Song).TextFormat; }
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

			file = Tools.GetFullPath(file);
			try {
				xs = new XmlSerializer(type);
			} catch (InvalidOperationException ex) {
				// Invalid class. Does the class have a public constructor?
				Console.WriteLine("DeserializeFrom exception: " + ex.Message);
			}
			
			if (xs != null) {
				try {
					using (FileStream fs = File.Open(file, FileMode.Open, FileAccess.Read)) {
                        Config config = xs.Deserialize(fs) as Config;
                        if (config != null) {
                            return DeserializeCleanup(config);
                        } else {
                            Config.SerializeTo(instance, file);
                        }
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
