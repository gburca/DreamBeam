using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.IO;

namespace DreamBeam {
	/// <summary>
	/// Zusammenfassende Beschreibung für Class
	/// </summary>
	public class BGImageTools : GuiTemplate {

		public bool LoadingBGThumbs = false;
		private String g_Bg_Directory = null;
		static Thread Thread_BgImage = null;
		string[] filetypes = { ".bmp", ".jpg", ".png", ".gif", ".jpeg" };

		public BGImageTools(MainForm impForm, ShowBeam impShowBeam)
			: base(impForm, impShowBeam) {
		}


		#region Drag and Drop
		// Copies the Draged Files into the current Background Directory</summary>
		public void DragDrop(object sender, System.Windows.Forms.DragEventArgs e) {
			Array a = (Array)e.Data.GetData(DataFormats.FileDrop);
			if (a != null) {
				foreach (string s in a) {

					foreach (string ex in filetypes) {
						if (Path.GetExtension(s).ToLower() == ex) {
							string destination = Path.Combine(GetCurrentPath(), Path.GetFileName(s));
							bool doCopy = true;

							//Check if File Exists
							if (File.Exists(destination) == true) {
								MessageBox.Show("File Already Exists: " + destination);
								doCopy = false;
							}

							//Copy File and Add Image
							if (doCopy) {
								try {
									File.Copy(s, destination, true);
								} catch (Exception doh) { MessageBox.Show(doh.Message); }
								if (File.Exists(destination)) {
									AddImage(destination);
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
		public void ListImages(string directory) {
			if (g_Bg_Directory == null && LoadingBGThumbs == false) {
				this.g_Bg_Directory = directory;
				_MainForm.RightDocks_imageList.Images.Clear();

				Thread_BgImage = new Thread(new ThreadStart(ListImages_Threader));
				Thread_BgImage.IsBackground = true;
				Thread_BgImage.Name = "ListImages";
				Thread_BgImage.Start();
			}
		}

		/// <summary>Loads all images from \Backgrounds, puts them in this.RightDocks_imageList and links those with names in ImageListBox  </summary>
		public void ListImages_Threader() {
			try {
				LoadingBGThumbs = true;

				// _MainForm.RightDocks_ImageListBox.Items.Clear()
				_MainForm.ImageListBoxUpdate(_MainForm.RightDocks_ImageListBox, null);

				string strImageDir = g_Bg_Directory;
				if (!Directory.Exists(strImageDir)) {
					Directory.CreateDirectory(strImageDir);
				}

				int intImageCount = 0;

				// Define Directory and ImageTypes
				int filecount = 0;

				//find the number of Files in Directory
				foreach (string filetype in filetypes) {
					string[] dirs = Directory.GetFiles(strImageDir, "*" + filetype);
					filecount += dirs.Length;
				}

				int i_file = 1;
				//find all files from defined FileTypes
				foreach (string filetype in filetypes) {
					string[] dirs = Directory.GetFiles(strImageDir, "*" + filetype);
					foreach (string dir in dirs) {
						// Add to ImageList
						AddImage(dir);
						intImageCount++;
						_MainForm.UpdateStatusPanel(_MainForm.Lang.say("Status.LoadingImages", Convert.ToString(100 * i_file / filecount)));
						i_file++;
					}
				}
				_MainForm.UpdateStatusPanel(_MainForm.Lang.say("Status.ImagesLoaded"));
				g_Bg_Directory = null;
				LoadingBGThumbs = false;
			} catch (Exception doh) { MessageBox.Show(doh.Message); }
		}

		/// <summary></summary>
		public void ListDirectories() {

			string strImageDir = Tools.GetDirectory(DirType.Backgrounds);

			_MainForm.RightDocks_imageList.Images.Clear();
			_MainForm.RightDocks_ImageListBox.Items.Clear();

			// Define Directory and ImageTypes
			string[] folders = Directory.GetDirectories(@strImageDir);
			_MainForm.RightDocks_FolderDropdown.Items.Clear();
			_MainForm.RightDocks_FolderDropdown.Items.Add("- Top -");
			foreach (string folder in folders) {
				_MainForm.RightDocks_FolderDropdown.Items.Add(Path.GetFileName(folder));
			}
		}
		#endregion

		#region Listeners
		///<summary>If BGImage chosen, update the current component </summary>
		public void SelectedIndexChanged(object sender, System.EventArgs e) {
			string path = Tools.GetDirectory(DirType.Backgrounds);

			if (_MainForm.RightDocks_FolderDropdown.SelectedIndex > 0) {
				path = Path.Combine(path, _MainForm.RightDocks_FolderDropdown.Items[_MainForm.RightDocks_FolderDropdown.SelectedIndex].ToString());
			}

			if (_MainForm.RightDocks_ImageListBox.SelectedIndex >= 0) {
				path = Path.Combine(path, _MainForm.RightDocks_ImageListBox.Items[_MainForm.RightDocks_ImageListBox.SelectedIndex].Text);
				if (_MainForm.selectedTab == MainTab.Presentation) {
					_MainForm.PreviewPresentationMedia(path);
				} else {
					if (_MainForm.DisplayPreview.content != null) {
						_MainForm.DisplayPreview.content.BGImagePath = path;
						_MainForm.DisplayPreview.UpdateDisplay(true);
					}
				}
			}
		}


		public void SelectionChangeCommitted(object sender, System.EventArgs e) {
			/*			   string path = @"Backgrounds\";
						   if (_MainForm.RightDocks_FolderDropdown.SelectedIndex > 0){
								path = path + _MainForm.RightDocks_FolderDropdown.Items[_MainForm.RightDocks_FolderDropdown.SelectedIndex].ToString() + @"\";
						   }*/
			ListImages(GetCurrentPath());
			GC.Collect();
		}
		#endregion

		#region internalTools
		private string GetCurrentPath() {
			string path = Tools.GetDirectory(DirType.Backgrounds);
			if (_MainForm.RightDocks_FolderDropdown.SelectedIndex > 0) {
				path = Path.Combine(path, _MainForm.RightDocks_FolderDropdown.Items[_MainForm.RightDocks_FolderDropdown.SelectedIndex].ToString());
			}
			return path;
		}

		// Adds an Image to the List
		public void AddImage(string dir) {
			try {
				_MainForm.RightDocks_imageList.Images.Add(_ShowBeam.Resizer(dir, 80, 60));
				Controls.Development.ImageListBoxItem x = new Controls.Development.ImageListBoxItem(Path.GetFileName(dir), _MainForm.RightDocks_imageList.Images.Count - 1);
				_MainForm.ImageListBoxUpdate(_MainForm.RightDocks_ImageListBox, x);
			} catch (Exception doh) { MessageBox.Show(doh.Message); }
		}
		#endregion

	}
}
