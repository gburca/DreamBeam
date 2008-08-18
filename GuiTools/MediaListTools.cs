using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Threading;

namespace DreamBeam {
	/// <summary>
	/// Zusammenfassende Beschreibung für Class
	/// </summary>
	public class MediaListTools : GuiTemplate {
		ImageList MediaList = null;
		string LoadingMediaList = "";
		static Thread Thread_MediaLoader = null;
		Array DragDropArray = null;

		public MediaListTools(MainForm impForm, ShowBeam impShowBeam)
			: base(impForm, impShowBeam) {
			MediaList = _MainForm.MediaList;

		}


		#region DragnDrop
		public void DragEnter(object sender, System.Windows.Forms.DragEventArgs e) {
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
				e.Effect = DragDropEffects.Copy;
		}


		public void DragDrop(object sender, System.Windows.Forms.DragEventArgs e) {
			DragDropArray = (Array)e.Data.GetData(DataFormats.FileDrop);
			if (DragDropArray != null) {
				if (LoadingMediaList == "") {
					LoadingMediaList = "Import";
					Thread_MediaLoader = new Thread(new ThreadStart(DragDropThread));
					Thread_MediaLoader.IsBackground = true;
					Thread_MediaLoader.Name = "MediaLoader:DragDrop";
					Thread_MediaLoader.Start();
				}
			}
		}

		public void DragDropThread() {
			DialogResult answer = MessageBox.Show(_MainForm.Lang.say("Message.ImportMedia"), _MainForm.Lang.say("Message.ImportMediaTitle"), MessageBoxButtons.YesNo);

			try {
				int i = 0;
				foreach (string dropFile in DragDropArray) {
					foreach (string ex in _MainForm.GuiTools.Presentation.filetypes) {
						if (Path.GetExtension(dropFile).ToLower() == ex) {

							string fileName;
							if (answer == DialogResult.Yes) {
								// Copy the file and change the file name to point to the copy

								string MediaFolder = Tools.GetDirectory(DirType.MediaFiles);
								if (Directory.Exists(MediaFolder) == false) {
									Directory.CreateDirectory(MediaFolder);
								}

								MediaFolder = Path.Combine(MediaFolder, this.MediaList.Name);
								if (Directory.Exists(MediaFolder) == false) {
									Directory.CreateDirectory(MediaFolder);
								}

								fileName = Path.Combine(MediaFolder, Path.GetFileName(dropFile));
								if (File.Exists(fileName) == false) {
									File.Copy(dropFile, fileName, true);
								}
							} else {
								// Link file from the current location
								fileName = dropFile;
							}

							if (File.Exists(fileName)) {
								AddMedia(fileName);
							}

							break;
						}
					}

					_MainForm.UpdateStatusPanel(_MainForm.Lang.say("Status.LoadingMediaFiles", Convert.ToString(100 * i / DragDropArray.Length)));
					i++;
				}

				_MainForm.UpdateStatusPanel(_MainForm.Lang.say("Status.MediaFilesLoaded"));
				LoadingMediaList = "";
				Refresh_MediaListBox();

			} catch (Exception ex) {
				MessageBox.Show(ex.Message);
			}

		}

		#endregion

		#region AddMediaAndLoad
		public void AddMedia(string sPath) {
			int index = 0;
			// TODO: Can't access these from non-UI thread. Must be fixed.
			//if (_MainForm.RightDocks_BottomPanel_MediaList.SelectedIndex > -1) {
			//    index = _MainForm.RightDocks_BottomPanel_MediaList.SelectedIndex;
			//} else {
			    index = MediaList.Count - 1;
			//}

			if (MediaList.GetType(sPath) == "image") {
				try {
					_MainForm.Media_ImageList.Images.Add(_ShowBeam.DrawProportionalBitmap(new Size(100, 75), sPath));
					this.MediaList.Insert(index, Path.GetFileName(sPath), sPath, _MainForm.Media_ImageList.Images.Count - 1);
				} catch { }
			}
			if (MediaList.GetType(sPath) == "flash") {
				_MainForm.Media_ImageList.Images.Add(_MainForm.Media_Logos.Images[0]);
				this.MediaList.Insert(index, Path.GetFileName(sPath), sPath, _MainForm.Media_ImageList.Images.Count - 1);
			}
			if (MediaList.GetType(sPath) == "movie") {
				_MainForm.Media_ImageList.Images.Add(_MainForm.Media_Logos.Images[1]);
				_MainForm.MediaList.Insert(index, Path.GetFileName(sPath), sPath, _MainForm.Media_ImageList.Images.Count - 1);
			}
		}


		/// <summary>Function to Load the Selected MediaList from the ListBox</summary>
		public void LoadSelectedMediaList(string FileName) {
			if (LoadingMediaList == "") {
				LoadingMediaList = FileName;
				Thread_MediaLoader = new Thread(new ThreadStart(LoaderThread));
				Thread_MediaLoader.IsBackground = true;
				Thread_MediaLoader.Name = "MediaLoader:LoadSelectedMediaList";
				Thread_MediaLoader.Start();
			}
		}

		public void LoaderThread() {
			try {
				string FileName = LoadingMediaList;
				_MainForm.MediaList.Load(FileName);
				_MainForm.Media_ImageList.Images.Clear();

				for (int i = 0; i < _MainForm.MediaList.Count; i++) {

					string sPath = _MainForm.MediaList.iItem[i].Path;

					if (_MainForm.MediaList.GetType(sPath) == "image") {
						_MainForm.Media_ImageList.Images.Add(_ShowBeam.DrawProportionalBitmap(new Size(100, 75), sPath));
						_MainForm.MediaList.iItem[i].Thumb = i;
					}
					if (_MainForm.MediaList.GetType(sPath) == "flash") {
						_MainForm.Media_ImageList.Images.Add(_MainForm.Media_Logos.Images[0]);
						_MainForm.MediaList.iItem[i].Thumb = i;
					}
					if (_MainForm.MediaList.GetType(sPath) == "movie") {
						_MainForm.Media_ImageList.Images.Add(_MainForm.Media_Logos.Images[1]);
						_MainForm.MediaList.iItem[i].Thumb = i;
					}
					_MainForm.UpdateStatusPanel(_MainForm.Lang.say("Status.LoadingMediaFiles", Convert.ToString(100 * i / _MainForm.MediaList.Count)));
				}

				Refresh_MediaListBox();
				GC.Collect();
				_MainForm.GuiTools.ChangeTitle();
				LoadingMediaList = "";
				_MainForm.UpdateStatusPanel(_MainForm.Lang.say("Status.MediaFilesLoaded"));
			} catch (Exception doh) { MessageBox.Show(doh.Message); }
		}
		#endregion

		#region Buttons
		public void Media_Up_Click(object sender, System.EventArgs e) {
			if (_MainForm.RightDocks_BottomPanel_MediaList.SelectedIndex > 0) {
				int i = _MainForm.RightDocks_BottomPanel_MediaList.SelectedIndex;
				this.MediaList.ShiftUp(i);
				this.Refresh_MediaListBox();
				_MainForm.RightDocks_BottomPanel_MediaList.SelectedIndex = i - 1;
			}
		}

		public void Media_Down_Click(object sender, System.EventArgs e) {
			if (_MainForm.RightDocks_BottomPanel_MediaList.SelectedIndex < _MainForm.RightDocks_BottomPanel_MediaList.Items.Count - 1) {
				int i = _MainForm.RightDocks_BottomPanel_MediaList.SelectedIndex;
				this.MediaList.ShiftDown(i);
				this.Refresh_MediaListBox();
				_MainForm.RightDocks_BottomPanel_MediaList.SelectedIndex = i + 1;
			}
		}

		public void Media_Remove_Click(object sender, System.EventArgs e) {
			if (_MainForm.RightDocks_BottomPanel_MediaList.SelectedIndex >= 0) {

				string MediaFolder = Tools.GetDirectory(DirType.MediaFiles);
				string localFile = MediaFolder + "\\" + this.MediaList.Name + "\\" + _MainForm.RightDocks_BottomPanel_MediaList.Items[_MainForm.RightDocks_BottomPanel_MediaList.SelectedIndex].Text;

				int tmp = _MainForm.RightDocks_BottomPanel_MediaList.SelectedIndex;
				MediaList.Remove(tmp);
				this.Refresh_MediaListBox();
				if (_MainForm.RightDocks_BottomPanel_MediaList.Items.Count <= tmp) {
					tmp--;
				}
				if (File.Exists(localFile)) {
					try {
						File.Delete(localFile);
					} catch { }
				}
				if (tmp >= 0)
					_MainForm.RightDocks_BottomPanel_MediaList.SelectedIndex = tmp;
			}
		}
		#endregion

		public void Refresh_MediaListBox() {
			// Clear the ImageListBox
			_MainForm.ImageListBoxUpdate(_MainForm.RightDocks_BottomPanel_MediaList, null);
			for (int i = 0; i < this.MediaList.Count; i++) {
				Controls.Development.ImageListBoxItem x = new Controls.Development.ImageListBoxItem(this.MediaList.iItem[i].Name, this.MediaList.iItem[i].Thumb);
				_MainForm.ImageListBoxUpdate(_MainForm.RightDocks_BottomPanel_MediaList, x);
			}
		}

	}
}
