using System;
using System.IO;
using System.Xml;
using System.Windows.Forms;
namespace DreamBeam {
	/// <summary>
	/// Zusammenfassende Beschreibung für Class
	/// </summary>
	public class ImageList {

		// TODO: Change this to a variable size structure
		public ImageListItem2[] iItem = new ImageListItem2[5000];
		private ImageListItem2 tempItem = new ImageListItem2();
		public string Name = "Default";
		public int Count = 0;

		public ImageList() {

			//
			// TODO: Hier die Konstruktorlogik einfügen
			//
		}

		#region ImageList Functions
		public void Insert(int Position, string tsName, string tsPath, int tiThumb) {


			Position++;
			if (Position > Count)
				Position = Count;


			for (int i = Count; i >= Position; i--) {
				this.iItem[i + 1] = this.iItem[i];
			}


			iItem[Position].Name = tsName;
			iItem[Position].Path = tsPath;
			iItem[Position].Thumb = tiThumb;

			Count++;
		}

		public void Remove(int Position) {
			if (Position <= Count) {
				for (int i = Position; i <= Count; i++) {
					this.iItem[i] = this.iItem[i + 1];
				}
				Count--;
			}
		}

		public void ShiftUp(int Position) {
			if (Position > 0) {
				tempItem = iItem[Position];
				iItem[Position] = iItem[Position - 1];
				iItem[Position - 1] = tempItem;
			}
		}


		public void ShiftDown(int Position) {
			if (Position < Count) {
				tempItem = iItem[Position];
				iItem[Position] = iItem[Position + 1];
				iItem[Position + 1] = tempItem;
			}
		}

		#endregion


		public string GetType(string File) {
			if (Path.GetExtension(File).ToLower() == ".jpeg")
				return ("image");
			if (Path.GetExtension(File).ToLower() == ".jpg")
				return ("image");
			if (Path.GetExtension(File).ToLower() == ".bmp")
				return ("image");
			if (Path.GetExtension(File).ToLower() == ".gif")
				return ("image");
			if (Path.GetExtension(File).ToLower() == ".png")
				return ("image");
			if (Path.GetExtension(File).ToLower() == ".swf")
				return ("flash");
			if (Path.GetExtension(File).ToLower() == ".avi" || Path.GetExtension(File).ToLower() == ".mpeg" || Path.GetExtension(File).ToLower() == ".wmv" || Path.GetExtension(File).ToLower() == ".mpg" || Path.GetExtension(File).ToLower() == ".vob" || Path.GetExtension(File).ToLower() == ".mov")
				return ("movie");

			return ("junk");
		}



		///<summary>Saves the ImageList</summary>
		public void Save() {
			XmlTextWriter tw = new XmlTextWriter(Tools.GetDirectory(DirType.MediaLists, Name + ".xml"), null);
			tw.Formatting = Formatting.Indented;
			tw.WriteStartDocument();
			tw.WriteStartElement("MediaList");
			tw.WriteElementString("Version", Tools.GetAppVersion());
			for (int i = 0; i < Count; i++) {
				tw.WriteStartElement("MediaItem");
				tw.WriteElementString("Path", Tools.GetRelativePath(DirType.DataRoot, this.iItem[i].Path));
				tw.WriteEndElement();
			}
			tw.WriteEndElement();
			tw.WriteEndDocument();
			tw.Flush();
			tw.Close();
		}

		///<summary>Loads the MediaList. The "filename" must be relative to
		///the MediaLists directory.</summary>
		public void Load(string filename) {
			Count = 0;
			XmlDocument document = new XmlDocument();
			try {
				document.Load(Tools.GetDirectory(DirType.MediaLists, filename + ".xml"));
			} catch (XmlException xmle) {
				MessageBox.Show(xmle.Message);
			}

			XmlNodeList list = null;
			string fullPath;

			// Get the Path
			list = document.GetElementsByTagName("Path");
			foreach (XmlNode n in list) {
				fullPath = Tools.GetFullPathOrNull(Tools.GetDirectory(DirType.DataRoot), n.InnerText);
				if (fullPath != null && File.Exists(fullPath)) {
					this.iItem[Count].Path = fullPath;
					this.iItem[Count].Name = Path.GetFileName(this.iItem[Count].Path);
					Count++;
				}
			}
			this.Name = filename;
		}
	}




	public struct ImageListItem2 {
		public int Thumb;
		public string Name, Path;

	}


}
