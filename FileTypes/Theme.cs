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

		public Theme() {
			Version = Tools.GetAppVersion();
		}
		public Theme(int formats)
			: this() {
			CreateTextFormats(formats);
		}

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

		public abstract void SaveAs();

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

			// Override version the theme was deserialized with
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

		public static object DeserializeFrom(Type type, string file) {
			XmlSerializer xs = null;

			file = Tools.GetFullPathOrNull(file);
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

		/// 
		/// Function for doing a deep clone of an object.
		/// 
		/// Returns a deep-copy clone of the object.
		//    public virtual T DeepClone() {
		//        //First do a shallow copy.
		//        THData returnData = (THData)this.MemberwiseClone();

		//        //Get the type.
		//        Type type = returnData.GetType();

		//        //Now get all the member variables.
		//        FieldInfo[] fieldInfoArray = type.GetFields();

		//        //Deepclone members that extend THData.
		//        //This will ensure we get everything we need.
		//        foreach (FieldInfo fieldInfo in fieldInfoArray) {
		//            //This gets the actual object in that field.
		//            object sourceFieldValue = fieldInfo.GetValue(this);

		//            //See if this member is THData
		//            if (sourceFieldValue is THData) {
		//                //If so, cast as a THData.
		//                THData sourceTHData = (THData)sourceFieldValue;

		//                //Create a clone of it.
		//                THData clonedTHData = sourceTHData.DeepClone();

		//                //Within the cloned containig class.
		//                fieldInfo.SetValue(returnData, clonedTHData);
		//            }
		//        }
		//        return returnData;
		//    }
	}

	[Serializable()]
	[XmlRoot(ElementName = "TextFormatTheme")]
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
			TextFormat[(int)SongTextType.Verse].Bounds = new RectangleF(5F, 12F, 90F, 83F);
			TextFormat[(int)SongTextType.Author].Bounds = new RectangleF(80F, 95F, 15F, 4F);
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

}