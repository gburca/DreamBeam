using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Collections;



using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.GZip;

using System.Diagnostics;
using System.Globalization;
using System.Threading;



namespace DreamBeam
{
	/// <summary>
	/// Zusammenfassende Beschreibung für Class
	/// </summary>
	public class Exporter
	{
		public Exporter()
		{
			//
			// TODO: Hier die Konstruktorlogik einfügen
			//
		}

		public static string filepath;
		public static string path;
		public static ArrayList List;
		public static ListBox listBox;
		public static ProgressBar progressBar;
		public static Form ExportForm;
		public static bool include_Images;
		public static bool convert_Images;
		public static bool singleFile;
		public static string newLine;
		public static bool includeMultiLanguageInfo;
		public static bool seperateVerses;
		public static string seperator;
		public static string songSeperator;

		#region DreamSongPackage
		public static void CreateDreamSongPackage(Form ExportForm, string filepath, ListBox List, ProgressBar progressBar, bool include_Images, bool convert_Images,bool singleFile){

			ArrayList newList = new ArrayList();
			if(singleFile){
				for (int j = 0; j< List.Items.Count;j++){
					newList.Add (List.Items[j]);
				}
					Exporter.filepath = filepath;
					Exporter.List = newList;
					Exporter.progressBar = progressBar;
					Exporter.include_Images = include_Images;
					Exporter.convert_Images = convert_Images;
					Exporter.singleFile = singleFile;
					Exporter.ExportForm = ExportForm;

			   Thread SongThread;
			   SongThread = new Thread(new ThreadStart(CreateDreamSongPackage));
			   SongThread.IsBackground = true;
			   SongThread.Start();

//				CreateDreamSongPackage(filepath,newList,progressBar, include_Images,convert_Images,singleFile);
//				CreateDreamSongPackage();
			}else{


				Exporter.listBox = List;
				Exporter.progressBar = progressBar;
				Exporter.include_Images = include_Images;
				Exporter.convert_Images = convert_Images;
				Exporter.singleFile = singleFile;
				Exporter.ExportForm = ExportForm;
				Exporter.path = filepath;

			  Thread SongThread;
			   SongThread = new Thread(new ThreadStart(SingleFileDreamSongPackage));
			   SongThread.IsBackground = true;
			   SongThread.Start();


			}
		}


			public static void SingleFileDreamSongPackage(){

				Exporter.ExportForm.Enabled = false;
				Exporter.progressBar.Maximum = Exporter.listBox.Items.Count;
				Exporter.progressBar.Value = 0;
				ArrayList newList = new ArrayList();
				for (int j = 0; j< Exporter.listBox.Items.Count;j++){
					newList.Add (Exporter.listBox.Items[j]);
					Exporter.filepath = Exporter.path +"\\"+Exporter.listBox.Items[j]+".dbsongs";
					Exporter.List = newList;
					CreateDreamSongPackage();
					newList.Clear();
					Exporter.progressBar.Value++;
				}
				Exporter.ExportForm.Enabled = true;

			}


			public static void CreateDreamSongPackage(){

			string filepath = Exporter.filepath;
			ArrayList List = Exporter.List;
			ProgressBar progressBar = Exporter.progressBar;
			bool include_Images = Exporter.include_Images;
			bool convert_Images = Exporter.convert_Images;
			bool singleFile = Exporter.singleFile;
			
			if(List.Count > 0){
                string strSongDir = DreamTools.GetDirectory(DirType.Songs);
				Crc32 crc = new Crc32();
				ZipOutputStream s = new ZipOutputStream(File.Create(filepath));
				s.SetLevel(9); //0 - store only to 9 - means best compression
				Song song = new Song();
				ArrayList bgList = new ArrayList();
				if(singleFile)progressBar.Maximum = List.Count;
				if(singleFile)progressBar.Value = 0;
				if(singleFile)Exporter.ExportForm.Enabled = false;
				for (int j = 0; j< List.Count;j++){

					   string file = strSongDir+List[j].ToString()+".xml";
					   if(File.Exists(file)){


							// Include BG Image
						   if(include_Images){
							   //song.Load(List[j].ToString());
                               song = new Song(List[j].ToString());
							   /*if(File.Exists(song.bg_image)){
									bool includeBG = true;
								   for (int i = 0; i < bgList.Count;i++){
										if(bgList[i].ToString() == song.bg_image) includeBG = false;
								   }*/
                               /*   if(includeBG){

                                      bgList.Add(song.bgImage);
                                       //DreamTools.DreamBeamPath()+"\\"+
                                       string bgImage = song.bg_image;

                                       if( ((Path.GetExtension(song.bg_image).ToLower() != ".jpg")&(Path.GetExtension(song.bg_image).ToLower() != ".jpeg"))  && convert_Images){
                                           Bitmap bmp = new Bitmap(bgImage);

                                           System.Drawing.Imaging.ImageCodecInfo ici = BitmapManipulator.GetEncoderInfo("image/jpeg");
                                            EncoderParameters eps = new EncoderParameters(1);
                                           eps.Param[0] = new EncoderParameter(Encoder.Quality, 100L);

                                           bgImage = DreamTools.DreamBeamPath()+"\\"+Path.GetFileNameWithoutExtension(bgImage)+".jpg";

                                           bmp.Save(bgImage,ici,eps);
                                           addToZip(s,crc,bgImage,false);

                                           bmp.Dispose();

                                           File.Delete(bgImage);
                                       }

                                       else if(File.Exists(bgImage)){
                                           addToZip(s,crc,bgImage,false);
                                       }
                                   }
                               }*/
                           }

						   addToZip(s,crc,file,false);

					   }
					   if(singleFile) progressBar.Value ++;
				}
				s.Finish();
				s.Close();
				if(singleFile)Exporter.ExportForm.Enabled = true;
			}
		}

		public static void CreateDreamSongPackage(string filepath,ArrayList List, ProgressBar progressBar,bool include_Images, bool convert_Images,bool singleFile){
			if(List.Count > 0){
                string strSongDir = DreamTools.GetDirectory(DirType.Songs);
				Crc32 crc = new Crc32();
				ZipOutputStream s = new ZipOutputStream(File.Create(filepath));
				s.SetLevel(9); //0 - store only to 9 - means best compression
				Song song = new Song();
				ArrayList bgList = new ArrayList();
				if(singleFile)progressBar.Maximum = List.Count;
				if(singleFile)progressBar.Value = 0;
				for (int j = 0; j< List.Count;j++){

					   string file = strSongDir+List[j].ToString()+".xml";
					   if(File.Exists(file)){


							// Include BG Image
						/*   if(include_Images){
							   song.Load(List[j].ToString());
							   if(File.Exists(song.bg_image)){
									bool includeBG = true;
								   for (int i = 0; i < bgList.Count;i++){
										if(bgList[i].ToString() == song.bg_image) includeBG = false;
								   }
								   if(includeBG){

										bgList.Add(song.bg_image);
										//DreamTools.DreamBeamPath()+"\\"+
										string bgImage = song.bg_image;

										if( ((Path.GetExtension(song.bg_image).ToLower() != ".jpg")&(Path.GetExtension(song.bg_image).ToLower() != ".jpeg"))  && convert_Images){
											Bitmap bmp = new Bitmap(bgImage);

											System.Drawing.Imaging.ImageCodecInfo ici = BitmapManipulator.GetEncoderInfo("image/jpeg");
											 EncoderParameters eps = new EncoderParameters(1);
											eps.Param[0] = new EncoderParameter(Encoder.Quality, 100L);

											bgImage = DreamTools.DreamBeamPath()+"\\"+Path.GetFileNameWithoutExtension(bgImage)+".jpg";

											bmp.Save(bgImage,ici,eps);
											addToZip(s,crc,bgImage,true);

											bmp.Dispose();

											File.Delete(bgImage);
										}

										else if(File.Exists(bgImage)){
											addToZip(s,crc,bgImage,true);
										}
									}
								}
						   }*/

						   addToZip(s,crc,file,true);

					   }
					   if(singleFile) progressBar.Value ++;
				}
				s.Finish();
				s.Close();
			}
		}
		#endregion

		#region TextFiles
		public static void CreateTextSongFile(Form ExportForm, string filepath, ListBox List, ProgressBar progressBar,string songSeperator){

				Exporter.path = filepath;
				Exporter.listBox = List;
				Exporter.progressBar = progressBar;
				Exporter.songSeperator = songSeperator;
				Exporter.ExportForm = ExportForm;
				
			   Thread TextThread;
			   TextThread = new Thread(new ThreadStart(CreateTextSongFile));
			   TextThread.IsBackground = true;
			   TextThread.Start();

		}

		public static void CreateTextSongFile(){


				string filepath =	Exporter.path;
				ListBox List = Exporter.listBox;
				ProgressBar progressBar = Exporter.progressBar;
				string songSeperator = Exporter.songSeperator;

				Exporter.ExportForm.Enabled = false;

				Song song = new Song();
				StreamWriter SW;
				SW=File.CreateText(filepath);
				progressBar.Maximum = List.Items.Count;
				progressBar.Value = 0;
			/*	for (int j = 0; j< List.Items.Count;j++){
					song.Load(List.Items[j].ToString());

					if(song.GetText(0).Length==0){
						SW.Write(List.Items[j].ToString());
					}else{
						SW.Write(song.GetText(0).Replace("\n","\r\n"));
					}
					SW.Write("\r\n");
					SW.Write("("+song.GetText(2).Replace("\n","\r\n")+")");
					SW.Write("\r\n\r\n");
					SW.Write(song.GetText(1).Replace("\n","\r\n"));
					SW.Write("\r\n");
					SW.Write(songSeperator.Replace(@"\n","\r\n"));
					progressBar.Value++;
				}*/
				SW.Close();

				Exporter.ExportForm.Enabled = true;
		}

		public static void CreateTextSongFiles(Form ExportForm, string filepath, ListBox List, ProgressBar progressBar){

				Exporter.path = filepath;
				Exporter.listBox = List;
				Exporter.progressBar = progressBar;
				Exporter.ExportForm = ExportForm;


			   Thread TextThread;
			   TextThread = new Thread(new ThreadStart(CreateTextSongFiles));
			   TextThread.IsBackground = true;
			   TextThread.Start();


		}

		public static void CreateTextSongFiles(){
		
				string filepath =	Exporter.path;
				ListBox List = Exporter.listBox;
				ProgressBar progressBar = Exporter.progressBar;
				Exporter.ExportForm.Enabled = false;

				Song song = new Song();
				StreamWriter SW;
				progressBar.Maximum = List.Items.Count;
				progressBar.Value = 0;
				/*for (int j = 0; j< List.Items.Count;j++){
					SW=File.CreateText(filepath+"\\"+List.Items[j].ToString()+".txt");
					song.Load(List.Items[j].ToString());

					if(song.GetText(0).Length==0){
						SW.Write(List.Items[j].ToString());
					}else{
						SW.Write(song.GetText(0).Replace("\n","\r\n"));
					}
					SW.Write("\r\n");
					SW.Write("("+song.GetText(2).Replace("\n","\r\n")+")");
					SW.Write("\r\n\r\n");
					SW.Write(song.GetText(1).Replace("\n","\r\n"));
					SW.Write("\r\n");
					SW.Close();
					progressBar.Value++;
				}*/
					Exporter.ExportForm.Enabled = true;

		}

		#endregion

		#region CSVFiles
		public static void CreateCSVFile(Form ExportForm, string filepath, ListBox List, ProgressBar progressBar, string seperator, string newLine,bool includeMultiLanguageInfo,bool seperateVerses){

				Exporter.path = filepath;
				Exporter.listBox = List;
				Exporter.progressBar = progressBar;
				Exporter.seperator = seperator;
				Exporter.songSeperator = songSeperator;
				Exporter.newLine = newLine;
				Exporter.ExportForm = ExportForm;
				Exporter.includeMultiLanguageInfo = includeMultiLanguageInfo;
				Exporter.seperateVerses = seperateVerses;

			   Thread CSVThread;
			   CSVThread = new Thread(new ThreadStart(CreateCSVFile));
			   CSVThread.IsBackground = true;
			   CSVThread.Start();

		}

		public static void CreateCSVFile(){
				 string filepath = Exporter.path;
				 ListBox List = Exporter.listBox;
				 ProgressBar progressBar = Exporter.progressBar;
				 string seperator = Exporter.seperator;
				 string songSeperator = Exporter.songSeperator;
				 string newLine = Exporter.newLine;
				 bool includeMultiLanguageInfo = Exporter.includeMultiLanguageInfo;
				 bool seperateVerses = Exporter.seperateVerses;


				Exporter.ExportForm.Enabled = false;
				Song song = new Song();
				StreamWriter SW;
				SW=File.CreateText(filepath);
				progressBar.Maximum = List.Items.Count;
				progressBar.Value = 0;


				SW.Write("Title");
				SW.Write(seperator);
				if(includeMultiLanguageInfo){
					SW.Write("Multi Language");
					SW.Write(seperator);
				}
				SW.Write("Author");
				SW.Write(seperator);
				if(seperateVerses){
					SW.Write("Verse 1");
					SW.Write(seperator);
					SW.Write("Verse 2");
					SW.Write(seperator);
					SW.Write("Verse 3");
					SW.Write(seperator);
					SW.Write("Verse 4");
					SW.Write(seperator);
					SW.Write("Verse 5");
					SW.Write(seperator);
					SW.Write("Verse 6");
					SW.Write(seperator);
					SW.Write("Verse 7");
					SW.Write(seperator);
					SW.Write("Verse 8");
					SW.Write(seperator);
					SW.Write("Verse 9");
					SW.Write(seperator);
					SW.Write("Verse 10");
				}else{
					SW.Write("Verses");
				}
				SW.Write(seperator);
				SW.Write("\r\n");


				
			/*	for (int j = 0; j< List.Items.Count;j++){
					song.Load(List.Items[j].ToString());

					if(song.GetText(0).Length==0){
						SW.Write(List.Items[j].ToString());
					}else{
						SW.Write(song.GetText(0).Replace("\n",newLine));
					}
					SW.Write(seperator);
					if(includeMultiLanguageInfo){
					SW.Write( Convert.ToInt32(song.MultiLang).ToString());
					SW.Write(seperator);
					}
					SW.Write(song.GetText(2).Replace("\n",newLine));
					SW.Write(seperator);
					if(seperateVerses){
								SW.Write(song.GetText(1).Replace("\n\n\n",";").Replace("\n",newLine));
					}else{
						SW.Write(song.GetText(1).Replace("\n",newLine));
					}

					SW.Write(seperator);

					SW.Write("\r\n");
					progressBar.Value++;
				}*/
				SW.Close();
				Exporter.ExportForm.Enabled = true;
		}
		#endregion

		#region MediaLists
		public static void CreateDreamMediaPackage(Form ExportForm, string filepath, ListBox List, ProgressBar progressBar, bool convert_Images,bool singleFile){


				Exporter.path = filepath;
				Exporter.filepath = filepath;
				Exporter.listBox = List;
				Exporter.progressBar = progressBar;
				Exporter.convert_Images = convert_Images;
				Exporter.singleFile = singleFile;
				Exporter.ExportForm = ExportForm;




			ArrayList newList = new ArrayList();
		   Thread MediaThread;

			if(singleFile){
				for (int j = 0; j< List.Items.Count;j++){
					newList.Add (List.Items[j]);
				}
				Exporter.List = newList;

			   MediaThread = new Thread(new ThreadStart(CreateDreamMediaPackage));
               CreateDreamMediaPackage();
			}else{
			   MediaThread = new Thread(new ThreadStart(CreateDreamMediaMultiPackage));
			}
			   //MediaThread.IsBackground = true;
			   //MediaThread.Start();
		}

		public static void CreateDreamMediaMultiPackage(){

				ListBox List = Exporter.listBox;
				ArrayList newList = new ArrayList();
				string filepath = Exporter.path;
				Exporter.progressBar.Maximum = List.Items.Count;
				Exporter.progressBar.Value = 0;

				Exporter.ExportForm.Enabled = false;
				for (int j = 0; j< List.Items.Count;j++){
					newList.Add (List.Items[j]);
					Exporter.List = newList;
					Exporter.filepath = filepath+"\\"+List.Items[j]+".dbmedia";
					CreateDreamMediaPackage();
					newList.Clear();
					Exporter.progressBar.Value++;
				}
				Exporter.ExportForm.Enabled = true;
		}


		public static void CreateDreamMediaPackage(){


				string filepath = Exporter.filepath;
				ArrayList List = Exporter.List;
				ProgressBar progressBar =Exporter.progressBar;
				bool convert_Images = Exporter.convert_Images;
				bool singleFile = Exporter.singleFile;



			if(List.Count > 0){
                string strMediaListDir = DreamTools.GetDirectory(DirType.MediaLists);
				Crc32 crc = new Crc32();
				ZipOutputStream s = new ZipOutputStream(File.Create(filepath));
				s.SetLevel(9); //0 - store only to 9 - means best compression

				ImageList mediaList = new ImageList();

				ArrayList bgList = new ArrayList();
				if(singleFile)progressBar.Maximum = List.Count;
                
				if(singleFile)progressBar.Value = 0;
				if(singleFile)Exporter.ExportForm.Enabled = false;

				for (int j = 0; j< List.Count;j++){

					   string file = strMediaListDir+List[j].ToString()+".xml";
					   if(File.Exists(file)){

							// Include BG Image
						   mediaList.Load(List[j].ToString());
						   for (int k =0; k < mediaList.Count;k++){
							   if(File.Exists(mediaList.iItem[k].Path)){

								   bool includeBG = true;
								   for (int i = 0; i < bgList.Count;i++){
										if(bgList[i].ToString() == mediaList.iItem[k].Path) includeBG = false;
								   }
								   if(includeBG){

										bgList.Add(mediaList.iItem[k].Path);
										//DreamTools.DreamBeamPath()+"\\"+
										string bgImage = mediaList.iItem[k].Path;

										if( ((Path.GetExtension(bgImage).ToLower() != ".jpg")&(Path.GetExtension(bgImage).ToLower() != ".jpeg"))  && convert_Images){
											Bitmap bmp = new Bitmap(bgImage);

											System.Drawing.Imaging.ImageCodecInfo ici = BitmapManipulator.GetEncoderInfo("image/jpeg");
											 EncoderParameters eps = new EncoderParameters(1);
											eps.Param[0] = new EncoderParameter(Encoder.Quality, 100L);

											bgImage = DreamTools.GetDirectory(DirType.DataRoot)+"\\"+Path.GetFileNameWithoutExtension(bgImage)+".jpg";

											bmp.Save(bgImage,ici,eps);
											addToZip(s,crc,bgImage,true);

											bmp.Dispose();

											File.Delete(bgImage);
										}

										else if(File.Exists(bgImage)){
											addToZip(s,crc,bgImage,true);
										}
								   }
								}
							}

						   addToZip(s,crc,file,true);

					   }
					   if(singleFile) progressBar.Value ++;
				}
				s.Finish();
				s.Close();
				if(singleFile)Exporter.ExportForm.Enabled = true;
			}
		}
		#endregion MediaLists


			public static void CreateDreamBGPackage(Form ExportForm, string filepath, ListBox List, ProgressBar progressBar, bool convert_Images,bool singleFile){


				Exporter.path = filepath;
				Exporter.filepath = filepath;
				Exporter.listBox = List;
				Exporter.progressBar = progressBar;
				Exporter.convert_Images = convert_Images;
				Exporter.singleFile = singleFile;
				Exporter.ExportForm = ExportForm;


			ArrayList newList = new ArrayList();
		   Thread BGThread;

			if(singleFile){
				for (int j = 0; j< List.Items.Count;j++){
					newList.Add (List.Items[j]);
				}
				Exporter.List = newList;
			   BGThread = new Thread(new ThreadStart(CreateDreamBGPackage));
			}else{
			    BGThread = new Thread(new ThreadStart(CreateDreamBGMultiPackage));
			}
			   BGThread.IsBackground = true;
			   BGThread.Start();
		}



		
		public static void CreateDreamBGMultiPackage(){



				ListBox List = Exporter.listBox;
				ArrayList newList = new ArrayList();
				string filepath = Exporter.path;
				Exporter.progressBar.Maximum = List.Items.Count;
				Exporter.progressBar.Value = 0;

				Exporter.ExportForm.Enabled = false;
				for (int j = 0; j< List.Items.Count;j++){
					newList.Add (List.Items[j]);
					Exporter.List = newList;
					Exporter.filepath = filepath+"\\"+List.Items[j]+".dbbg";
					CreateDreamBGPackage();
					newList.Clear();
					Exporter.progressBar.Value++;
				}
				Exporter.ExportForm.Enabled = true;
		}


		public static void CreateDreamBGPackage(){

				string filepath = Exporter.filepath;
				ArrayList List = Exporter.List;
				ProgressBar progressBar =Exporter.progressBar;
				bool convert_Images = Exporter.convert_Images;
				bool singleFile = Exporter.singleFile;



			if(List.Count > 0){
                string strMediaListDir = DreamTools.GetDirectory(DirType.Backgrounds);
				Crc32 crc = new Crc32();
				ZipOutputStream s = new ZipOutputStream(File.Create(filepath));
				s.SetLevel(9); //0 - store only to 9 - means best compression


				ArrayList bgList = new ArrayList();
				if(singleFile)progressBar.Maximum = List.Count;
				if(singleFile)progressBar.Value = 0;
				if(singleFile)Exporter.ExportForm.Enabled = false;

				for (int j = 0; j< List.Count;j++){

					   string [] filetypes = {".bmp", ".jpg", ".png",".gif",".jpeg"};
					   string strImageDir = strMediaListDir+List[j].ToString();

					   if(Directory.Exists(strImageDir)){


					//find all files from defined FileTypes
					foreach (string filetype in filetypes){
						string[] dirs = Directory.GetFiles(@strImageDir, "*"+filetype);
						foreach (string dir in dirs){

										string bgImage = dir;
										//mediaList.iItem[k].Path;

										if( ((Path.GetExtension(bgImage).ToLower() != ".jpg")&(Path.GetExtension(bgImage).ToLower() != ".jpeg"))  && convert_Images){
											Bitmap bmp = new Bitmap(bgImage);

											System.Drawing.Imaging.ImageCodecInfo ici = BitmapManipulator.GetEncoderInfo("image/jpeg");
											 EncoderParameters eps = new EncoderParameters(1);
											eps.Param[0] = new EncoderParameter(Encoder.Quality, 100L);

                                            bgImage = DreamTools.GetDirectory(DirType.DataRoot)+"\\" + Path.GetFileNameWithoutExtension(bgImage) + ".jpg";

											bmp.Save(bgImage,ici,eps);
											addToZip(s,crc,bgImage,true);

											bmp.Dispose();

											File.Delete(bgImage);
										}

										else if(File.Exists(bgImage)){
											addToZip(s,crc,bgImage,true);
										}


								}
						   }
						  // addToZip(s,crc,file);

					   }
					   if(singleFile) progressBar.Value ++;
				}
				s.Finish();
				s.Close();
				if(singleFile)Exporter.ExportForm.Enabled = true;
			}
		}



		public static void addToZip(ZipOutputStream s,Crc32 crc,string file, bool putInFolder){
		   FileStream fs = File.OpenRead(file);
		   byte[] buffer = new byte[fs.Length];
		   fs.Read(buffer, 0, buffer.Length);
		   string filename = Path.GetFileName(file);
		   if(putInFolder) filename = DreamTools.ReplaceSpecialChars(DreamTools.Reverse(DreamTools.Reverse(Path.GetDirectoryName(file)).Substring(0,DreamTools.Reverse(Path.GetDirectoryName(file)).IndexOf("\\")))+"\\"+filename);

//           MessageBox.Show(filename);
		   ZipEntry entry = new ZipEntry(filename);
		   entry.DateTime = DateTime.Now;
		   entry.Size = fs.Length;
		   fs.Close();
		   crc.Reset();
		   crc.Update(buffer);
		   entry.Crc  = crc.Value;
		   s.PutNextEntry(entry);
		   s.Write(buffer, 0, buffer.Length);
		}




	}
}
