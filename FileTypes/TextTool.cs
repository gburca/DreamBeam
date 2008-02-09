using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;


namespace DreamBeam.FileTypes {

	#region Enums
	public enum TextToolType {
		FirstLine,
		OtherLines
	}
	#endregion

	/// <summary>
	/// Summary description for TextTool.
	/// </summary>
	public class TextToolContents : NewSong {

		public TextToolContents(string fullText, Config config) {
			this.enumType = typeof(TextToolType);
			this.config = config;
			this.format = config.SermonTextFormat;
			this.BGImagePath = config.TextBGImagePath;
			this.WordWrap = true;

			int lineBreak = fullText.IndexOf("\n\n");

			if (lineBreak > 1) {
				this.Title = fullText.Substring(0, lineBreak);
				lineBreak += 2;
				this.SetLyrics(LyricsType.Verse, fullText.Substring(lineBreak, fullText.Length - lineBreak));
			} else {
				this.Title = "";
				this.SetLyrics(LyricsType.Verse, fullText);
			}

			for (int num = 1; num <= this.SongLyrics.Count; num++) {
				this.Sequence.Add(new LyricsSequenceItem(LyricsType.Verse, num));
			}
		}

		#region IContentOperations Members

		//		public System.Drawing.Bitmap GetBitmap(int width, int height) {
		//			// TODO:  Add TextTool.GetBitmap implementation
		//			return null;
		//		}
		//
		//		public void Next() {
		//			// TODO:  Add TextTool.Next implementation
		//		}
		//
		//		public void Prev() {
		//			// TODO:  Add TextTool.Prev implementation
		//		}
		//
		//		public void ChangeBGImagePath(string newPath) {
		//			// TODO:  Add TextTool.ChangeBGImagePath implementation
		//		}
		//
		//		public string GetIdentity() {
		//			// TODO:  Add TextTool.GetIdentity implementation
		//			return null;
		//		}

		public override ContentIdentity GetIdentity() {
			ContentIdentity ident = new ContentIdentity();
			ident.Type = (int)ContentType.PlainText;
			ident.Text = this.Title + "\n\n" + this.GetLyrics(LyricsType.Verse);
			ident.SongStrophe = this.CurrentLyric;
			return ident;
		}

		#endregion

		#region ICloneable Members

		//		public object Clone() {
		//			// TODO:  Add TextTool.Clone implementation
		//			return null;
		//		}

		#endregion
	}

	/// <summary>
	/// On power-down, all the documents contained in Sermon_DocManager are
	/// saved to this class and this class is serialized to disk. On start-up,
	/// this class is deserialized, and all the documents it contains are added
	/// to Sermon_DocManager.
	/// </summary>
	public class SermonToolDocuments {
		[XmlArrayItem(ElementName = "Document", Type = typeof(string))]
		[XmlArray]
		public ArrayList Documents;

		public SermonToolDocuments() {
			Documents = new ArrayList();
		}

		#region Serialization and Deserialization
		public static void SerializeTo(SermonToolDocuments instance, string file) {
			Type type = instance.GetType();
			XmlSerializer xs = new XmlSerializer(type);

			Directory.CreateDirectory(Path.GetDirectoryName(file));
			FileStream fs = null;

			try {
				fs = File.Open(file, FileMode.Create, FileAccess.Write, FileShare.Read);
				xs.Serialize(fs, instance);
			} finally {
				if (fs != null) {
					fs.Close();
				}
			}
		}

		public static object DeserializeFrom(System.Type type, string file) {
			XmlSerializer xs = null;

			try {
				xs = new XmlSerializer(type);
			} catch (InvalidOperationException ex) {
				// Invalid class. We need to have a default public constructor.
				Console.WriteLine("DeserializeFrom exception: " + ex.Message);
			}

			if (xs != null) {
				try {
					using (FileStream fs = File.Open(file, FileMode.Open, FileAccess.Read)) {
						return xs.Deserialize(fs);
					}
				} catch (FileNotFoundException e) {
					Console.WriteLine("Exception deserializing (FileNotFound): " + e.Message);
				} catch (InvalidOperationException e) {
					Console.WriteLine("Exception deserializing (Invalid XML or old format): " + e.Message);
				}
			}

			return null;
		}

		#endregion
	}
}
