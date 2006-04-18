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
	public class BGImageTools : GuiTemplate
	{

		public bool LoadingBGThumbs = false;
		private String g_Bg_Directory = null;
		static Thread Thread_BgImage = null;
		string [] filetypes = {".bmp", ".jpg", ".png",".gif",".jpeg"};

		public BGImageTools(MainForm impForm, ShowBeam impShowBeam)  :  base(impForm,impShowBeam)
		{
		}


		#region Drag and Drop
		// Copies the Draged Files into the current Background Directory</summary>
		public void DragDrop(object sender, System.Windows.Forms.DragEventArgs e) {
			 Array a = (Array)e.Data.GetData(DataFormats.FileDrop);
				if ( a != null ){
					foreach(string s in a )	{

						foreach(string ex in filetypes){
							if(Path.GetExtension(s).ToLower() == ex){
								string strImageDir = Tools.DreamBeamPath()+"\\"+GetCurrentPath();
								bool doCopy = true;

								//Check if File Exists
								if(System.IO.File.Exists(strImageDir+Path.GetFileName(s)) == false){
								}else{
									MessageBox.Show ("File Already Exists");
									doCopy = false;
								}

								//Copy File and Add Image
								if(doCopy){
									try{
										System.IO.File.Copy(s,strImageDir+Path.GetFileName(s),true);
									}catch (Exception doh){MessageBox.Show(doh.Message);}
									if(System.IO.File.Exists(strImageDir+Path.GetFileName(s))){
										AddImage(strImageDir+Path.GetFileName(s));
									}

								}

							}
						}
					}
				}
		}
		// <summary> Changes the Icon on Drag Enter   </summary>
		public void DragEnter(object sender, System.Windows.Forms.DragEventArgs e) {

			if (e.Data.GetDataPresent(DataFormats.FileDrop))
				e.Effect = DragDropEffects.Copy;
		}
		#endregion

		#region Image Loader
		/// <summary>starts the ListImages_Threader Thread</summary>
		public void ListImages(string directory){
			if (g_Bg_Directory == null && LoadingBGThumbs == false){
				this.g_Bg_Directory = directory;
				Thread_BgImage = new Thread( new ThreadStart(ListImages_Threader));
				Thread_BgImage.IsBackground = true;
				Thread_BgImage.Start();
			}
		}


		/// <summary>Loads all images from \Backgrounds, puts them in this.RightDocks_imageList and links those with names in ImageListBox  </summary>
		public void ListImages_Threader(){
			try{
				LoadingBGThumbs = true;
				string directory = g_Bg_Directory;
				if(!System.IO.Directory.Exists(directory)){
					System.IO.Directory.CreateDirectory(directory);
				}

				int intImageCount=0;

				_MainForm.RightDocks_imageList.Images.Clear();
				_MainForm.RightDocks_ImageListBox.Items.Clear();

				// Define Directory and ImageTypes
				string strImageDir = Tools.DreamBeamPath()+"\\"+directory;
				int filecount = 0;

				//find the number of Files in Directory
				 foreach (string filetype in filetypes){
					string[] dirs = Directory.GetFiles(@strImageDir, "*"+filetype);
					foreach (string dir in dirs){
						filecount++;
					}
				}

				int i_file = 1;
				//find all files from defined FileTypes
				foreach (string filetype in filetypes){
					string[] dirs = Directory.GetFiles(@strImageDir, "*"+filetype);
					foreach (string dir in dirs){
						// Add to ImageList
						AddImage(dir);
						intImageCount++;
						_MainForm.StatusPanel.Text = _MainForm.Lang.say("Status.LoadingImages",Convert.ToString(100*i_file/filecount));
						i_file++;
					}
				}
				_MainForm.StatusPanel.Text = _MainForm.Lang.say("Status.ImagesLoaded");
				g_Bg_Directory = null;
				LoadingBGThumbs = false;
			}catch(Exception doh){MessageBox.Show (doh.Message);}
		}




				/// <summary></summary>
				public void ListDirectories(string directory){

					if(!System.IO.Directory.Exists(directory)){
						System.IO.Directory.CreateDirectory(directory);
					}

					int intImageCount=0;
					_MainForm.RightDocks_imageList.Images.Clear();
					_MainForm.RightDocks_ImageListBox.Items.Clear();

					// Define Directory and ImageTypes
					string strImageDir = Tools.DreamBeamPath()+"\\"+directory;
					string[] folders = Directory.GetDirectories(@strImageDir);
					_MainForm.RightDocks_FolderDropdown.Items.Clear();
					_MainForm.RightDocks_FolderDropdown.Items.Add("- Top -");
					foreach (string folder in folders){
						_MainForm.RightDocks_FolderDropdown.Items.Add(Tools.Reverse(Tools.Reverse(folder).Substring(0,Tools.Reverse(folder).IndexOf(@"\"))));
					}
			   }
		  #endregion

		#region Listeners
			///<summary>If BGImage chosen, update the current component </summary>
			public void SelectedIndexChanged(object sender, System.EventArgs e){
				string path = @"Backgrounds\";
				if (_MainForm.RightDocks_FolderDropdown.SelectedIndex > 0)   {
					path = path + _MainForm.RightDocks_FolderDropdown.Items[_MainForm.RightDocks_FolderDropdown.SelectedIndex].ToString() + @"\";
				}
				if (_MainForm.selectedTab == 1){
					_ShowBeam.Song.bg_image =  path + _MainForm.RightDocks_ImageListBox.Items[_MainForm.RightDocks_ImageListBox.SelectedIndex].Text;
				   _MainForm.SongEdit_BG_Label.Text =  path + _MainForm.RightDocks_ImageListBox.Items[_MainForm.RightDocks_ImageListBox.SelectedIndex].Text;
				}else if(_MainForm.selectedTab == 0){
					_ShowBeam.ImageOverWritePath =  path + _MainForm.RightDocks_ImageListBox.Items[_MainForm.RightDocks_ImageListBox.SelectedIndex].Text;
				   _MainForm.SongShow_BG_Label.Text =  path + _MainForm.RightDocks_ImageListBox.Items[_MainForm.RightDocks_ImageListBox.SelectedIndex].Text;
					if(_ShowBeam.OverWriteBG)
						_ShowBeam.Prerenderer.RenderAllThreaded();
				}else if(_MainForm.selectedTab == 2){
					_ShowBeam.SermonImagePath =  path + _MainForm.RightDocks_ImageListBox.Items[_MainForm.RightDocks_ImageListBox.SelectedIndex].Text;
			   }
			   _MainForm.Draw_Song_Preview_Image_Threaded();
			}


			public void SelectionChangeCommitted(object sender, System.EventArgs e){
			   _ShowBeam.LogFile.Log("\n ImageList Dropdown changed");
/*			   string path = @"Backgrounds\";
			   if (_MainForm.RightDocks_FolderDropdown.SelectedIndex > 0){
					path = path + _MainForm.RightDocks_FolderDropdown.Items[_MainForm.RightDocks_FolderDropdown.SelectedIndex].ToString() + @"\";
			   }*/
			   ListImages(GetCurrentPath());
			   GC.Collect();
			}
		 #endregion

		#region internalTools

			private string GetCurrentPath(){
			   string path = @"Backgrounds\";
			   if (_MainForm.RightDocks_FolderDropdown.SelectedIndex > 0){
					path = path + _MainForm.RightDocks_FolderDropdown.Items[_MainForm.RightDocks_FolderDropdown.SelectedIndex].ToString() + @"\";
			   }
			   return path;
			}

			// Adds an Image to the List
			public void AddImage(string dir){
				try{
					_MainForm.RightDocks_imageList.Images.Add(_ShowBeam.Resizer(dir,80,60));
					Controls.Development.ImageListBoxItem x = new Controls.Development.ImageListBoxItem(Path.GetFileName(dir),_MainForm.RightDocks_imageList.Images.Count-1);
					_MainForm.RightDocks_ImageListBox.Items.Add(x);
				}catch (Exception doh){MessageBox.Show(doh.Message);}
			}
		 #endregion


			   
	}
}
