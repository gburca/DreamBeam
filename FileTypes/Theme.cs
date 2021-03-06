using System;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
using DreamBeam.FileTypes;
using System.Drawing;
using System.Runtime.Serialization.Formatters.Binary;

namespace DreamBeam {

	[Serializable()]
	public abstract class Theme : ICloneable {
		public string Version;
		public string BGImagePath = null;
		public BeamTextFormat[] TextFormat;
		[XmlIgnore]
		public string ThemeFile = null;

		public Theme() {
			Version = DreamTools.GetAppVersion();
		}
		//public Theme(int formats)
		//    : this() {
		//    CreateTextFormats(formats);
		//}

		/// <summary>
		/// When themes are first created, or if the deserialized theme has
		/// fewer elements than needed (ex. older version) this function
		/// ensures that enough array elements are created.
		/// </summary>
		/// <param name="size"></param>
		public void CreateTextFormats(int size) {
			// Create the array
			BeamTextFormat[] textFormat = new BeamTextFormat[size];
			// Populate the array
			for (int i = 0; i < size; i++) {
				textFormat[i] = new BeamTextFormat();
			}

			if (TextFormat != null) {
				for (int i = 0; i < Math.Min(size, TextFormat.Length); i++) {
					if (TextFormat[i] != null) {
						textFormat[i] = TextFormat[i];
					}
				}
			}

			TextFormat = textFormat;
		}
        
		public static Theme OpenFile(string FileDialogFilter, Type type) {
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.DefaultExt = "xml";
			dialog.Filter = FileDialogFilter;
			dialog.FilterIndex = 1;
			dialog.InitialDirectory = DreamTools.GetDirectory(DirType.Themes);
			dialog.Title = "Open Theme";
			dialog.Multiselect = false;

			if (dialog.ShowDialog() == DialogResult.OK) {
				Theme t = DeserializeFrom(type, dialog.FileName) as Theme;
				return t;
			} else {
				return null;
			}
		}

		public abstract void SaveAs();

		public void SaveAs(string FileDialogFilter) {
			SaveFileDialog dialog = new SaveFileDialog();
			dialog.DefaultExt = "xml";
			dialog.Filter = FileDialogFilter;
			dialog.FilterIndex = 1;
			dialog.InitialDirectory = DreamTools.GetDirectory(DirType.Themes);
			dialog.Title = "Save Theme As";

			Directory.CreateDirectory(dialog.InitialDirectory);

			if (dialog.ShowDialog() == DialogResult.OK) {
				string fileName = dialog.FileName;
				try {
					SerializeTo(this, fileName);
					this.ThemeFile = DreamTools.GetRelativePath(DirType.DataRoot, fileName);
					//this.StatusPanel.Text = Lang.say("Status.SongSavedAs", this.SaveFileDialog.FileName);
				} catch (Exception ex) {
					MessageBox.Show("Theme not saved: " + ex.Message);
				}
			}
		}


        /// <summary>
        /// Saves Theme into a Directory
        /// </summary>
        /// <param name="fileName">Path to File</param>
        public void SaveFile(string fileName)
        {         
            try
            {
                SerializeTo(this, fileName);
                this.ThemeFile = DreamTools.GetRelativePath(DirType.DataRoot, fileName);
                //this.StatusPanel.Text = Lang.say("Status.SongSavedAs", this.SaveFileDialog.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Theme not saved: " + ex.Message);
            }
        }


		public static void SerializeTo(Theme instance, string file) {
			XmlSerializer xs = new XmlSerializer(instance.GetType());

			file = Path.GetFullPath(file);
			Directory.CreateDirectory(Path.GetDirectoryName(file));
			FileStream fs = null;

			// Override version the theme was deserialized with
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

		public static object DeserializeFrom(Type type, string file) {
			XmlSerializer xs = null;

			file = DreamTools.GetFullPathOrNull(DreamTools.GetDirectory(DirType.DataRoot), file);
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
						Theme t = (Theme)xs.Deserialize(fs);
						if (t != null) {
							t.ThemeFile = DreamTools.GetRelativePath(DirType.DataRoot, file);
							if (type == typeof(SongTheme)) {
								t.CreateTextFormats(Enum.GetValues(typeof(SongTextType)).Length);
							} else if (type == typeof(BibleTheme)) {
								t.CreateTextFormats(Enum.GetValues(typeof(BibleTextType)).Length);
							} else if (type == typeof(SermonTheme)) {
								t.CreateTextFormats(Enum.GetValues(typeof(TextToolType)).Length);
							}
						}
						return t;
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

		/// <summary>
		/// This is not a true hash code. It is only used to determine if any
		/// graphically visible characteristics of the object have changed.
		/// </summary>
		/// <returns></returns>
		public virtual int VisibleHashCode() {
			int fh = 0;
			if (BGImagePath != null) fh += BGImagePath.GetHashCode();
			if (TextFormat != null) {
				foreach (BeamTextFormat f in TextFormat) {
					fh += f.VisibleHashCode();
				}
			}
			return fh;
		}


		#region ICloneable Members

		public object Clone() {
			MemoryStream ms = new MemoryStream();
			BinaryFormatter bf = new BinaryFormatter();
			bf.Serialize(ms, this);
			ms.Position = 0;
			object clone = bf.Deserialize(ms);
			ms.Close();
			return clone;
		}

		#endregion
	}

	[Serializable()]
	[XmlRoot(ElementName = "TextFormatTheme")]
	public class BibleTheme : Theme {
		protected static string FileDialogFilter = @"DreamBeam Bible themes (*.BibleTheme.xml)|*.BibleTheme.xml|All (*.*)|*.*";

		public BibleTheme() {
			CreateTextFormats(Enum.GetValues(typeof(BibleTextType)).Length);
			TextFormat[(int)BibleTextType.Verse].Bounds = new RectangleF(5F, 12F, 90F, 83F);
			TextFormat[(int)BibleTextType.Reference].Bounds = new RectangleF(5F, 2F, 90F, 8F);
			TextFormat[(int)BibleTextType.Translation].Bounds = new RectangleF(80F, 96F, 15F, 4F);
		}

		public static BibleTheme OpenFile() {
			return Theme.OpenFile(FileDialogFilter, typeof(BibleTheme)) as BibleTheme;
		}
		public override void SaveAs() {
			SaveAs(FileDialogFilter);
		}
	}

	[Serializable()]
	[XmlRoot(ElementName = "TextFormatTheme")]
	public class SongTheme : Theme {
		protected static string FileDialogFilter = @"DreamBeam song themes (*.SongTheme.xml)|*.SongTheme.xml|All (*.*)|*.*";

		public SongTheme() {
			CreateTextFormats(Enum.GetValues(typeof(SongTextType)).Length);
			TextFormat[(int)SongTextType.Title].Bounds = new RectangleF(5F, 2F, 90F, 8F);
			TextFormat[(int)SongTextType.Verse].Bounds = new RectangleF(5F, 12F, 90F, 81F);
			TextFormat[(int)SongTextType.Author].Bounds = new RectangleF(80F, 95F, 15F, 4F);
			TextFormat[(int)SongTextType.Key].Bounds = new RectangleF(0F, 95F, 10F, 5F);
			TextFormat[(int)SongTextType.Verse].HAlignment = StringAlignment.Near;
		}

		public static SongTheme OpenFile() {
			return Theme.OpenFile(FileDialogFilter, typeof(SongTheme)) as SongTheme;
		}
		public override void SaveAs() {
			SaveAs(FileDialogFilter);
		}
	}

	[Serializable()]
	[XmlRoot(ElementName = "TextFormatTheme")]
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
		public override void SaveAs() {
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
	}

    [Serializable()]
    public class ComboThemePaths
    {
        public string SongThemePath;
        public string BibleThemePath;
        public string SermonThemePath;

        public ComboThemePaths()
        {
            SongThemePath = "";
            BibleThemePath = "";
            SermonThemePath = "";
        }
    }

}
