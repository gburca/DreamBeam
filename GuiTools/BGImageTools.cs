using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.IO;

namespace DreamBeam
{
	/// <summary>
	/// Zusammenfassende Beschreibung für Class
	/// </summary>
	public class BGImageTools
	{

	  public bool LoadingBGThumbs = false;
		private String g_Bg_Directory = null;
		static Thread Thread_BgImage = null;


		public BGImageTools()  :  base(impForm,impShowBeam)
		{
			//
			// TODO: Hier die Konstruktorlogik einfügen
			//
		}


		

		public void ImageListBox_DragDrop(object sender, System.Windows.Forms.DragEventArgs e) {
			 Array a = (Array)e.Data.GetData(DataFormats.FileDrop);
				if ( a != null )
				{	foreach(string s in a )
					{
						MessageBox.Show(s);
					}
				}
		}

				/// <summary>starts the ListImages_Threader Thread</summary>
				public void ListImages(string directory){
					if (g_Bg_Directory == null){
						this.g_Bg_Directory = directory;
						Thread_BgImage = new Thread( new ThreadStart(ListImages_Threader));
						Thread_BgImage.IsBackground = true;
						Thread_BgImage.Start();
					}
				}


				/// <summary>Loads all images from \Backgrounds, puts them in this.RightDocks_imageList and links those with names in ImageListBox  </summary>
				public void ListImages_Threader(){
					LoadingBGThumbs = true;
					string directory = g_Bg_Directory;

					_ShowBeam.LogFile.LogNew("Listung Images for "+directory);

					if(!System.IO.Directory.Exists(directory)){
						System.IO.Directory.CreateDirectory(directory);
					}

					int intImageCount=0;

					_MainForm.RightDocks_imageList.Images.Clear();
					_MainForm.RightDocks_ImageListBox.Items.Clear();

					// Define Directory and ImageTypes
					string strImageDir = Tools.DreamBeamPath()+"\\"+directory;
					string [] filetypes = {"*.bmp", "*.jpg", "*.png","*.gif","*.jpeg"};
					int filecount = 0;

					//find the number of Files in Directory
					 foreach (string filetype in filetypes){
						string[] dirs = Directory.GetFiles(@strImageDir, filetype);
						foreach (string dir in dirs){
							filecount++;
						}
					}

					int i_file = 1;
					//find all files from defined FileTypes
					foreach (string filetype in filetypes){
						string[] dirs = Directory.GetFiles(@strImageDir, filetype);
						foreach (string dir in dirs){
							_ShowBeam.LogFile.Log(" -> Processing Image"+dir);
							// Add to ImageList
							_ShowBeam.LogFile.Log("  --> add Image to ImageList");
							_MainForm.RightDocks_imageList.Images.Add(_ShowBeam.Resizer(dir,80,60));
							Controls.Development.ImageListBoxItem x = new Controls.Development.ImageListBoxItem(Path.GetFileName(dir),intImageCount);
							// Add to ImageListBox
							_ShowBeam.LogFile.Log("  --> add link to ImageListBox");
							_MainForm.RightDocks_ImageListBox.Items.Add(x);
							intImageCount++;
							_MainForm.StatusPanel.Text = "Loading Images... " + Convert.ToString(100*i_file/filecount) + "%";
							i_file++;
						}
					}
					_MainForm.StatusPanel.Text = "Images loaded. ";
                                          	 /*   */
					g_Bg_Directory = null;
					LoadingBGThumbs = false;
				}


				/// <summary></summary>
				private void ListDirectories(string directory){

					if(!System.IO.Directory.Exists(directory)){
						System.IO.Directory.CreateDirectory(directory);
					}

					int intImageCount=0;
					this.RightDocks_imageList.Images.Clear();
					this.RightDocks_ImageListBox.Items.Clear();

					// Define Directory and ImageTypes
					string strImageDir = Tools.DreamBeamPath()+"\\"+directory;
					string[] folders = Directory.GetDirectories(@strImageDir);
					RightDocks_FolderDropdown.Items.Clear();
					RightDocks_FolderDropdown.Items.Add("- Top -");
					foreach (string folder in folders){
						RightDocks_FolderDropdown.Items.Add(Tools.Reverse(Tools.Reverse(folder).Substring(0,Tools.Reverse(folder).IndexOf(@"\"))));
					}
			   }


			   
	}
}

