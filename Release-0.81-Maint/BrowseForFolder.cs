/* *************************************** 
 *           BrowseForFolder.cs
 * ---------------------------------------
 *            - Requirements -
 * 
 * You have to include the
 * System.Design.dll in your References
 * (Project/Add Reference from menu)
 * ---------------------------------------
 *		        -  Example -
 * 
   string myPath;

   BrowseForFolderClass myFolderBrowser = new BrowseForFolderClass();
   myPath = myFolderBrowser.BrowseForFolder("Please, select a folder");
   if (myPath.Length > 0) 
       txtPath.Text = myPath; 
 * ****************************************/

using System;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace DreamBeam
{
	public class BrowseForFolderClass : FolderNameEditor
	{
		FolderNameEditor.FolderBrowser myFolderBrowser;

		public string BrowseForFolder(string title)
		{
			string myPath;
			
			myFolderBrowser = new FolderNameEditor.FolderBrowser();
			
			// Description
			myFolderBrowser.Description = title;
			// ShowDialog
			myFolderBrowser.ShowDialog();
			// DirectoryPath
			myPath = myFolderBrowser.DirectoryPath;
			// Shall I add the "\" character at the end of the path ?
			if (myPath.Length > 0) 
			{
				if (myPath.Substring((myPath.Length - 1),1) != "\\") 
					myPath += "\\";
			}
			// Return correct path
			return myPath;
		}
		
		~BrowseForFolderClass()
		{
			myFolderBrowser.Dispose(); // Dispose
		}
	}
}
