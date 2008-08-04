
using System;
using System.Text;
using System.Collections;
using System.IO;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data;

using ICSharpCode.SharpZipLib.BZip2;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using ICSharpCode.SharpZipLib.GZip;
using System.Windows.Forms;

namespace DreamBeam
{
	/// <summary>
	/// Zusammenfassende Beschreibung für Class
	/// </summary>
	public class Importer
	{
		public Importer()
		{
			//
			// TODO: Hier die Konstruktorlogik einfügen
			//
		}

		public static string tempDir = "";

		public static void CreateTempDir(){

			Importer.tempDir =  Tools.GetDirectory(DirType.DataRoot)  +"\\temp\\importer_"+ Tools.createTimeStamp()+"\\";
			Directory.CreateDirectory(Importer.tempDir);
		}

		public static void listXMLSongs(){
			
		}

		public static void EmptyTempDir(){
			Tools.delTree(Tools.GetDirectory(DirType.DataRoot)+"\\temp");
		}

		public static void GetDreamSongs(System.Windows.Forms.ListBox listBox, string filename){
			ZipInputStream s = new ZipInputStream(File.OpenRead(filename));
			ZipEntry theEntry;
			while ((theEntry = s.GetNextEntry()) != null) {
				string directoryName = Path.GetDirectoryName(theEntry.Name);
				string fileName      = Path.GetFileName(theEntry.Name);
				if(Path.GetExtension(fileName) == ".xml"){
					 //	MessageBox.Show(fileName);
						listBox.Items.Add(fileName.Replace(".xml",""));
				}
				// create directory
				Directory.CreateDirectory(Importer.tempDir+directoryName);
				if (fileName != String.Empty) {
					FileStream streamWriter = File.Create(Importer.tempDir+theEntry.Name);
					int size = 2048;
					byte[] data = new byte[2048];
					while (true) {
						size = s.Read(data, 0, data.Length);
						if (size > 0) {
							streamWriter.Write(data, 0, size);
						} else {
							break;
						}
					}
					streamWriter.Close();
				}
			}
			s.Close();
		}

			public static void GetTextSongs(System.Windows.Forms.ListBox listBox, string filename, string strSeperator){
			 /* Song song = new Song();
			  song.SongName = "";
			  string tmpText = "";

			  StreamReader sr;
			  using(sr=File.OpenText(filename)){
				string s = "";
				while ((s = sr.ReadLine()) != null)
					{

						tmpText = tmpText + s +"\n";
						if(strSeperator!="" && tmpText.IndexOf(strSeperator) >= 0)
						{
							tmpText= tmpText.Substring(0,tmpText.IndexOf(strSeperator)-1).Trim();
							if(tmpText.Length > 0)
							{
							   while(tmpText.IndexOf("\n") != -1)
							   {
								   s = tmpText.Substring(0,tmpText.IndexOf("\n"));
								   if(song.SongName == "")
								   {
									  if(s.Trim() != "")
									  {
										song.SongName = s;
										song.SetText(s,0);
									  }
								   }else
									{
										song.SetText(song.GetText(1)+"\r\n"+s,1);
									}
								  tmpText = tmpText.Substring(tmpText.IndexOf("\n")+1,tmpText.Length -tmpText.IndexOf("\n")-1);
							   }

							   song.SetText(song.GetText(1)+"\r\n"+tmpText,1);
							   listBox.Items.Add(song.SongName);
							   song.Save(Importer.tempDir);
							   song.SongName = "";
							   song.SetText ("",1);
							   tmpText = "";
							}
						}
					}

					if(strSeperator ==""){
						song.SongName =  Path.GetFileNameWithoutExtension(filename);
						song.SetText(song.SongName,0);
						song.SetText (tmpText,1);
						song.Save(Importer.tempDir);
						listBox.Items.Add(song.SongName);
						}
				}*/
			  }




			public static void GetCSVSongs(System.Windows.Forms.ListBox listBox, string filename, string strSeperator, string strNewLine, bool mlInfo, bool seperateVerses){
			/*  Song song = new Song();
			  song.SongName = "";
			  string tmpText = "";
			  bool firstline = true;
			  StreamReader sr;
			  using(sr=File.OpenText(filename)){
			  	MessageBox.Show (filename);
				string s = "";
				while ((s = sr.ReadLine()) != null)
					{
						if(firstline){
							firstline = false;
							if(s.ToLower().IndexOf("title")>-1 & s.ToLower().IndexOf("author")>-1 & s.ToLower().IndexOf("verses")>-1){

							}else Importer.ParseCSVLine(listBox,s, strSeperator, strNewLine, mlInfo, seperateVerses);
						}
						Importer.ParseCSVLine(listBox,s, strSeperator, strNewLine, mlInfo, seperateVerses);
					}
				}
			  }

			  public static void ParseCSVLine(System.Windows.Forms.ListBox listBox,string line, string strSeperator, string strNewLine, bool mlInfo, bool seperateVerses){
				string tmp;
				Song song = new Song();
				int index = 0;
					while(line.IndexOf(strSeperator)>-1){

						tmp = line.Substring(0,line.IndexOf (strSeperator));
						if(index==0){
							song.SongName = tmp;
							song.SetText (tmp,0);
						}
						if(index==1){
							if(mlInfo){
								if(tmp.Trim() == "0") song.MultiLang = false;
								if(tmp.Trim() == "1") song.MultiLang = true;
								mlInfo = false;
								index--;
							}else{
								song.SetText (tmp,2);
							}
						}
						if(index==2){
							song.SetText(tmp.Trim().Replace(strNewLine,"\r\n"),1);
						}

					   line = line.Substring(line.IndexOf(strSeperator)+1);
					   MessageBox.Show (line);
					   index++;
					   
					}
					song.Save(Importer.tempDir);
					listBox.Items.Add(song.SongName); */
             
				}




	}
}
