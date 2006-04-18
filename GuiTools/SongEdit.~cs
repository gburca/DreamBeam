using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DreamBeam
{
	/// <summary>
	/// Zusammenfassende Beschreibung für Class
	/// </summary>
	public class SongEdit : GuiTemplate
	{

		private int xScroll,yScroll;
		private System.Windows.Forms.RichTextBox _RTEbind;

		public SongEdit(MainForm impForm, ShowBeam impShowBeam) : base(impForm,impShowBeam)
		{

		}

		

		#region TextColorizer

			#region ImportSystemFuncs
			[System.Runtime.InteropServices.DllImport("user32.dll")]
			private static extern long LockWindowUpdate(long hwnd);

			[System.Runtime.InteropServices.DllImport("user32.dll")]
			private static extern int GetScrollPos(IntPtr hWnd, int nBar);

			[System.Runtime.InteropServices.DllImport("user32.dll")]
			private static extern int SetScrollPos(IntPtr hWnd, int nBar, int nPos, bool bRedraw);

			[System.Runtime.InteropServices.DllImport("user32.dll")]
			private static extern bool PostMessageA(IntPtr hwnd, int wMsg, int wParam, int lParam);
			#endregion


		private void saveScroll(IntPtr hWnd){
			try{
				LockWindowUpdate(hWnd.ToInt32());
			}catch (Exception e){MessageBox.Show (e.Message);}
			xScroll = GetScrollPos(_RTEbind.Handle, 0);
			yScroll = GetScrollPos(_RTEbind.Handle, 1);
		}

		private void restoreScroll(IntPtr hWnd){
		   //	SetScrollPos(_RTEbind.Handle, 0, xScroll, true);
		   //	PostMessageA(_RTEbind.Handle, 276, 4+ 65536 * xScroll, 0);
		   try{
				SetScrollPos(_RTEbind.Handle, 0, yScroll, true);
				PostMessageA(_RTEbind.Handle, 277, 4 + 65536 * yScroll, 0);
				LockWindowUpdate(0);
			}catch (Exception e){MessageBox.Show (e.Message);}
		}


		public void TextColorizer(System.Windows.Forms.RichTextBox RTE){
			_RTEbind = RTE;
			try{
				saveScroll(_RTEbind.Handle);
				TextRenderer();
				restoreScroll(_RTEbind.Handle);
			}catch (Exception e){MessageBox.Show (e.Message);}
		}

		private void TextRenderer(){
			if(_RTEbind.TextLength > 1) {
				int LineCount = Tools.Count(_RTEbind.Text,"\n");
				int pos = 0;
				int curspos = _RTEbind.SelectionStart;
				bool origLang = true;
				Font fnt = null;
				for (int i = 0; i < LineCount; i++){
					if(_RTEbind.Lines[i].Trim().Length > 0){
						_RTEbind.Select(pos,_RTEbind.Lines[i].Length);
						// if this line is a comment
						if (_RTEbind.Lines[i].Trim().Substring(0,1) == "#"){
							fnt = new Font(_MainForm.EditorFont.FontFamily,_MainForm.EditorFont.Size,FontStyle.Bold);
							_RTEbind.SelectionFont = fnt;
							_RTEbind.SelectionColor = Color.Gray;
						}else{
							// if this line is first language or multilang is disabled
							if(origLang || _Song.MultiLang == false) {
								_RTEbind.SelectionColor = Color.Black;
								fnt = new Font(_MainForm.EditorFont.FontFamily,_MainForm.EditorFont.Size,FontStyle.Regular);
								_RTEbind.SelectionFont = fnt;
								origLang = false;
							// if this line is the 2nd language
							 } else {
								_RTEbind.SelectionColor = Color.Blue;
								fnt = new Font(_MainForm.EditorFont.FontFamily,_MainForm.EditorFont.Size,FontStyle.Italic);
								_RTEbind.SelectionFont = fnt;
								origLang = true;
							 }
						 }
						pos = pos + _RTEbind.Lines[i].Length;
					 }
					 pos = pos+1;
				 }
				 _RTEbind.Select(0,0);
				 _RTEbind.SelectionStart = curspos;
			 }
			 restoreScroll(_RTEbind.Handle);
		}
		#endregion



	}
}
