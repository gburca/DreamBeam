using System;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;


namespace DreamBeam {
	/// <summary>
	/// Zusammenfassende Beschreibung f�r Class
	/// </summary>
	public class Presentation : GuiTemplate {
		public string strMediaPath = "";
		private ImageList MediaList = new ImageList();
		public string[] filetypes = { ".bmp", ".jpg", ".png", ".gif", ".jpeg", ".swf", ".avi", ".mpeg", ".wmv", ".mpg", ".vob", ".mov" };

		public Presentation(MainForm impForm, ShowBeam impShowBeam)
			: base(impForm, impShowBeam) {
			MediaList = _MainForm.MediaList;
		}


		#region FileBrowser

		/// <summary>On ListView Click, Create and Show Preview Thumbnail</summary>
		public void ListView_Click(object sender, System.EventArgs e) {
			if (_MainForm.Presentation_Fade_ListView.SelectedItems.Count > 0) {

				ListViewItem activeItem = new ListViewItem();
				activeItem = _MainForm.Presentation_Fade_ListView.SelectedItems[0];
				if (strMediaPath.Length > 0) {
					if (MediaList.GetType(activeItem.Text) == "image")
						_MainForm.Presentation_Fade_preview.Image = Image.FromFile(strMediaPath + "\\" + activeItem.Text);
					if (MediaList.GetType(activeItem.Text) == "flash")
						_MainForm.Presentation_Fade_preview.Image = _MainForm.Media_Logos.Images[0];
					if (MediaList.GetType(activeItem.Text) == "movie")
						_MainForm.Presentation_Fade_preview.Image = _MainForm.Media_Logos.Images[1];
				}
			}
			return;
		}


		/// <summary>Add all selected Files to Playlist</summary>
		public void Fade_ToPlaylist_Button_Click(object sender, System.EventArgs e) {

			for (int i = 0; i < _MainForm.Presentation_Fade_ListView.SelectedItems.Count; i++) {

				//				   string MediaType = "";
				string sPath = Path.Combine(strMediaPath, _MainForm.Presentation_Fade_ListView.SelectedItems[i].Text);
				_MainForm.GuiTools.RightDock.MediaListTools.AddMedia(sPath);
			}

			_MainForm.GuiTools.RightDock.MediaListTools.Refresh_MediaListBox();

			GC.Collect();
		}


		/// <summary> method treeView1_BeforeSelect
		/// <para>Before we select a tree node we want to make sure that we scan the soon to be selected
		/// tree node for any sub-folders.  this insures proper tree construction on the fly.</para>
		/// <param name="sender">The object that invoked this event</param>
		/// <param name="e">The TreeViewCancelEventArgs event arguments.</param>
		/// <see cref="System.Windows.Forms.TreeViewCancelEventArgs"/>
		/// <see cref="System.Windows.Forms.TreeView"/>
		/// </summary>
		public void treeView1_BeforeSelect(object sender, System.Windows.Forms.TreeViewCancelEventArgs e) {

			getSubDirs(e.Node);     // get the sub-folders for the selected node.

			ListFiles(fixPath(e.Node));
			strMediaPath = fixPath(e.Node);
			_MainForm.folder = new DirectoryInfo(e.Node.FullPath); // get it's Directory info.

		}

		/// <summary> method treeView1_BeforeExpand
		/// <para>Before we expand a tree node we want to make sure that we scan the soon to be expanded
		/// tree node for any sub-folders.  this insures proper tree construction on the fly.</para>
		/// <param name="sender">The object that invoked this event</param>
		/// <param name="e">The TreeViewCancelEventArgs event arguments.</param>
		/// <see cref="System.Windows.Forms.TreeViewCancelEventArgs"/>
		/// <see cref="System.Windows.Forms.TreeView"/>
		/// </summary>
		public void treeView1_BeforeExpand(object sender, System.Windows.Forms.TreeViewCancelEventArgs e) {

			getSubDirs(e.Node);     // get the sub-folders for the selected node.

			_MainForm.folder = new DirectoryInfo(e.Node.FullPath); // get it's Directory info.
		}

		/// <summary> method fillTree
		/// <para>This method is used to initially fill the treeView control with a list
		/// of available drives from which you can search for the desired folder.</para>
		/// </summary>
		public void fillTree() {
			DirectoryInfo directory;
			// clear out the old values
			_MainForm.treeView1.Nodes.Clear();
			// loop through the drive letters and find the available drives.
			foreach (string drive in Directory.GetLogicalDrives()) {
				try {
					// get the directory informaiton for this path.
					directory = new DirectoryInfo(drive);
					// if the retrieved directory information points to a valid
					// directory or drive in this case, add it to the root of the
					// treeView.
					if (directory.Exists == true) {
						TreeNode newNode = new TreeNode(directory.FullName);
						_MainForm.treeView1.Nodes.Add(newNode); // add the new node to the root level.
						getSubDirs(newNode);   // scan for any sub folders on this drive.
					}
				} catch (Exception doh) {
					Console.WriteLine(doh.Message);
				}
			}
		}

		public void ListFiles(string directory) {
			if (System.IO.Directory.Exists(directory)) {
				_MainForm.Presentation_Fade_ListView.Clear();

				int intImageCount = 0;
				// Define Directory and ImageTypes
				string strImageDir = directory;
				int filecount = 0;

				//find the number of Files in Directory
				foreach (string filetype in filetypes) {
					string[] dirs = Directory.GetFiles(@strImageDir, "*" + filetype);
					filecount += dirs.Length;
				}
				int i_file = 1;
				//find all files from defined FileTypes
				foreach (string filetype in filetypes) {
					string[] dirs = Directory.GetFiles(@strImageDir, "*" + filetype);
					foreach (string dir in dirs) {
						//						   _ShowBeam.LogFile.Log(dir +" = " + MediaList.GetType(dir));
						if (MediaList.GetType(dir) == "image")
							_MainForm.Presentation_Fade_ListView.Items.Add(Path.GetFileName(dir), 0);
						if (MediaList.GetType(dir) == "flash")
							_MainForm.Presentation_Fade_ListView.Items.Add(Path.GetFileName(dir), 1);
						if (MediaList.GetType(dir) == "movie")
							_MainForm.Presentation_Fade_ListView.Items.Add(Path.GetFileName(dir), 2);
						intImageCount++;
						i_file++;
					}
				}
			}
		}


		/// <summary> method getSubDirs
		/// <para>this function will scan the specified parent for any subfolders
		/// if they exist.  To minimize the memory usage, we only scan a single
		/// folder level down from the existing, and only if it is needed.</para>
		/// <param name="parent">the parent folder in which to search for sub-folders.</param>
		/// </summary>
		public void getSubDirs(TreeNode parent) {
			DirectoryInfo directory;
			try {
				// if we have not scanned this folder before
				if (parent.Nodes.Count == 0) {
					directory = new DirectoryInfo(parent.FullPath);
					foreach (DirectoryInfo dir in directory.GetDirectories()) {
						TreeNode newNode = new TreeNode(dir.Name);
						parent.Nodes.Add(newNode);
					}
				}
				// now that we have the children of the parent, see if they
				// have any child members that need to be scanned.  Scanning
				// the first level of sub folders insures that you properly
				// see the '+' or '-' expanding controls on each node that represents
				// a sub folder with it's own children.
				foreach (TreeNode node in parent.Nodes) {
					// if we have not scanned this node before.
					if (node.Nodes.Count == 0) {
						// get the folder information for the specified path.
						directory = new DirectoryInfo(node.FullPath);
						// check this folder for any possible sub-folders
						foreach (DirectoryInfo dir in directory.GetDirectories()) {
							// make a new TreeNode and add it to the treeView.
							TreeNode newNode = new TreeNode(dir.Name);
							node.Nodes.Add(newNode);
						}
					}
				}
			} catch { }
		}

		/// <summary> method fixPath
		/// <para>For some reason, the treeView will only work with paths constructed like the following example.
		/// "c:\\Program Files\Microsoft\...".  What this method does is strip the leading "\\" next to the drive
		/// letter.</para>
		/// <param name="node">the folder that needs it's path fixed for display.</param>
		/// <returns>The correctly formatted full path to the selected folder.</returns>
		/// </summary>
		public string fixPath(TreeNode node) {
			string sRet = "";
			try {
				sRet = node.FullPath;
				int index = sRet.IndexOf("\\\\");
				if (index > 1) {
					sRet = node.FullPath.Remove(index, 1);
				}
			} catch { }
			return sRet;
		}
		#endregion



	}
}
