using System;
using System.IO;
using System.Xml;
using System.Windows.Forms;
namespace DreamBeam {
    /// <summary>
    /// Zusammenfassende Beschreibung f�r Class
    /// </summary>
    public class ImageList {

        public ImageListItem2[] iItem = new ImageListItem2[100];
        private ImageListItem2 tempItem = new ImageListItem2();
        public string Name = "Default";
        private string version = "1";


        public int Count = 0;


        public ImageList() {

            //
            // TODO: Hier die Konstruktorlogik einf�gen
            //
        }

#region ImageList Functions
        public void Insert(int Position, string tsName,string tsPath, int tiThumb) {


            Position++;
            if (Position > Count)
                Position = Count;


            for (int i = Count; i>=Position;i--) {
                this.iItem[i+1] = this.iItem[i];
            }


            iItem[Position].Name = tsName;
            iItem[Position].Path = tsPath;
            iItem[Position].Thumb = tiThumb;

            Count++;
        }

        public void Remove(int Position) {
            if (Position <= Count) {
                for (int i = Position; i<=Count;i++) {
                    this.iItem[i] = this.iItem[i+1];
                }
                Count--;
            }
        }

        public void ShiftUp(int Position) {
            if (Position > 0) {
                tempItem = iItem[Position];
                iItem[Position] = iItem[Position-1];
                iItem[Position-1] = tempItem;
            }
        }


        public void ShiftDown(int Position) {
            if (Position < Count) {
                tempItem = iItem[Position];
                iItem[Position] = iItem[Position+1];
                iItem[Position+1] = tempItem;
            }
        }

#endregion


        public string GetType(string File) {
            if(Path.GetExtension(File).ToLower() ==".jpeg")
                return ("image");
            if(Path.GetExtension(File).ToLower()==".jpg")
                return ("image");
            if(Path.GetExtension(File).ToLower()==".bmp")
                return ("image");
            if(Path.GetExtension(File).ToLower()==".gif")
                return ("image");
            if(Path.GetExtension(File).ToLower()==".png")
                return ("image");
            if(Path.GetExtension(File).ToLower() ==".swf")
				return ("flash");
			if(Path.GetExtension(File).ToLower() ==".avi" || Path.GetExtension(File).ToLower()==".mpeg" || Path.GetExtension(File).ToLower()==".wmv" || Path.GetExtension(File).ToLower()==".mpg" || Path.GetExtension(File).ToLower()==".vob" || Path.GetExtension(File).ToLower()==".mov")
                return ("movie");

            return("junk");
        }



        ///<summary>Saves the ImageList</summary>
        public void Save() {
            XmlTextWriter tw = new XmlTextWriter(Tools.DreamBeamPath()+"\\MediaLists\\"+Name+".xml",null);
            tw.Formatting = Formatting.Indented;
            tw.WriteStartDocument();
            tw.WriteStartElement("MediaList");
            tw.WriteElementString("Version",this.version);
            for (int i = 0; i<Count;i++) {
                tw.WriteStartElement("MediaItem"+i);
                tw.WriteElementString("Path",this.iItem[i].Path);
                tw.WriteEndElement();
            }
            tw.WriteEndElement();
            tw.WriteEndDocument();
            tw.Flush();
            tw.Close();
        }

        ///<summary>Loads the MediaList</summary>
        public void Load(string filename) {

          //  int i;
            Count = 0;
            XmlDocument document = new XmlDocument();
            try {
                //"Songs\\"+filename+".xml"
                document.Load(Tools.DreamBeamPath()+"\\MediaLists\\"+filename+".xml");
            } catch(XmlException xmle) {
                MessageBox.Show(xmle.Message);
            }

            XmlNodeList list = null;

            // Get the Path
            list = document.GetElementsByTagName("Path");
            //i = 0;
            foreach(XmlNode n in list) {
                if(File.Exists(n.InnerText)) {
                    this.iItem[Count].Path = n.InnerText;
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
