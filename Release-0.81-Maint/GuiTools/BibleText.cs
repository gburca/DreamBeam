using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DreamBeam.FileTypes;

namespace DreamBeam {
	public partial class BibleText : UserControl {
		protected MainForm mainForm;
		public BibleVersion bibleVersion;
		private Timer TextTypedTimer = new Timer();
		public EventHandler RegEx_ComboBox_EventHandler;
		protected ListBox BibleText_Bookmarks;
		protected BibleLib bibleLib;

		public BibleText() {
			InitializeComponent();
			RegEx_ComboBox_EventHandler = new EventHandler(RegEx_ComboBox_TimedOut);
		}

		public void Setup(MainForm mf, ListBox btb, BibleLib bibles) {
			mainForm = mf;
			BibleText_Bookmarks = btb;
			this.bibleLib = bibles;
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public BibleVersion BibleVersion {
			get { return bibleVersion; }
			set {
				bibleVersion = value;
				Results_Update();
			}
		}

		public void goToBookmark(String bookmark) {
			if (bibleVersion == null) return;

			RegEx_ComboBox.Text = "^" + this.bibleVersion.GetSimpleRef(bookmark, true) + @"\s+.*";
			int vidx = Query(true);
			if (vidx >= 0) {
				mainForm.DisplayPreview.SetContent(new ABibleVerse(this.bibleVersion, vidx, mainForm.Config));
			}
		}



		#region Results RichTextBox

		private void Results_Update() {
			try {
				Results.Populate(bibleVersion, Results.CurrentVerse);
			} catch { }
		}

		private void Results_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			System.Drawing.Point pt = new System.Drawing.Point(e.X, e.Y);
			if (Results.Text.Length == 0) return;	// Otherwise we get range exceptions in GetCharFromPosition ...
			char c = Results.GetCharFromPosition(pt);
			int idx = Results.GetCharIndexFromPosition(pt);
			int l = Results.GetLineFromCharIndex(idx);
			int vidx = Results.GetVerseIndexFromSelection(idx);
			Results.CurrentVerse = vidx;
			mainForm.DisplayPreview.SetContent(new ABibleVerse(bibleVersion, vidx, mainForm.Config));

			//Console.WriteLine("Clicked " + pt + " on line " + l + " at character number " +idx + "(" + c + ").");
			//Console.WriteLine("Clicked {0} on line {1} at character number {2} ({3}).", pt, l, idx, c);
			//Console.WriteLine("Verse: " + bibleVersion[vidx].ToString());
		}

		private void Results_MouseEnter(object sender, System.EventArgs e) {
			// Allows us to use the scroll wheel by just pointing to the control
			Results.Focus();
		}

		private void Results_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
			if (this.bibleVersion == null) return;
			switch (e.KeyCode) {
				case Keys.A:	// Find first
					FindFirst_button_Click(sender, null);
					break;
				case Keys.N:	// Find next
					FindNext_button_Click(sender, null);
					break;
				case Keys.P:	// Find previous
					FindPrev_button_Click(sender, null);
					break;
				case Keys.Z:	// Find last
					FindLast_button_Click(sender, null);
					break;
				case Keys.Enter:
					int vidx = Results.GetVerseIndexFromSelection(Results.SelectionStart);
					Results.CurrentVerse = vidx;
					mainForm.DisplayPreview.SetContent(new ABibleVerse(this.bibleVersion, vidx, mainForm.Config));
					mainForm.RightDocks_Preview_GoLive_Click(sender, null);
					break;
			}
			Results.Focus(); // The "Click" handlers above switch focus to RegEx input
			e.Handled = true;
		}

		private int Query(bool dirFwd) {
			if (bibleVersion == null) return -1;
			int index = Results.Find(bibleVersion, dirFwd, RegEx_ComboBox.Text);
			// Force an update
			Results.Focus();
			RegEx_ComboBox.Focus();
			// Remove selection, or else we'll type over it
			RegEx_ComboBox.SelectionStart = RegEx_ComboBox.Text.Length;
			return index;
		}
		#endregion

		#region BibleText RegEx and Verse ComboBox

		private void RegEx_ComboBox_TimedOut(Object myObject, EventArgs myEventArgs) {
			Console.WriteLine("Timer fired!");
			TextTypedTimer.Stop();
			TextTypedTimer.Enabled = false;
			TextTypedTimer.Tick -= RegEx_ComboBox_EventHandler;
			Query(true);
		}

		private void RegEx_ComboBox_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e) {

			if (e.KeyCode == Keys.Enter) {
				string searchTerm = RegEx_ComboBox.Text;
				if (!RegEx_ComboBox.Items.Contains(searchTerm)) {
					RegEx_ComboBox.Items.Insert(0, searchTerm);
				}
				Results.Focus();
			} else if (RegEx_ComboBox.Text.Length > 1 && RegEx_ComboBox.Text != Results.lastRegex) {
				if (TextTypedTimer.Enabled) {
					Console.WriteLine("Timer enabled. Restarting");
					// Restart timer
					TextTypedTimer.Stop();
					TextTypedTimer.Start();
				} else {
					Console.WriteLine("Time disabled");
					TextTypedTimer.Tick += RegEx_ComboBox_EventHandler;
					TextTypedTimer.Interval = 300;
					TextTypedTimer.Start();
				}
			}

			//Console.WriteLine("KeyCode = " + e.KeyCode + " KeyValue " + e.KeyValue + " KeyData " + e.KeyData);
		}


		private void RegEx_ComboBox_TextChanged(object sender, System.EventArgs e) {
			Query(true);
		}

		private void Verse_ComboBox_SelectedIndexChanged(object sender, System.EventArgs e) {
			if (bibleVersion == null) return;

			RegEx_ComboBox.Text = "^" + bibleVersion.NormalizeRef(Verse_ComboBox.SelectedIndex.ToString()) + @"\s+.*";
			Query(true);
		}

		private void Verse_ComboBox_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e) {
			if (e.KeyCode == Keys.Enter) {
				if (bibleVersion == null) return;

				string reference = Verse_ComboBox.Text;
				string normReference;
				normReference = bibleVersion.NormalizeRef(reference);
				if (normReference.Length == 0) {
					// Maybe the user forgot to enter the verse. Default to the first verse of the chapter.
					normReference = bibleVersion.NormalizeRef(reference + " 1");
				}

				int index = bibleVersion.GetVerseIndex(normReference);
				if (index >= 0) {
					RegEx_ComboBox.Text = "^" + this.bibleVersion.GetSimpleRef(normReference, true) + @"\s+.*";
					Query(true);
					mainForm.DisplayPreview.SetContent(new ABibleVerse(this.bibleVersion, index, mainForm.Config));
					if (!Verse_ComboBox.Items.Contains(reference)) {
						Verse_ComboBox.Items.Insert(0, reference);
					}
					Results.Focus();
				} else {
					RegEx_ComboBox.Text = "";
					Results.Populate(bibleVersion, 0);
					Results.Focus();
					Verse_ComboBox.Focus();
				}
				e.Handled = true;
			}
		}

		#endregion

		#region BibleText RegEx buttons
		private void Bookmark_button_Click(object sender, System.EventArgs e) {
			if (bibleVersion == null) return;

			string reference = bibleVersion.GetRef(Results.CurrentVerse);
			if (reference.Length > 0) {
				BibleText_Bookmarks.Items.Insert(0, reference);
			}
		}

		private void FindNext_button_Click(object sender, System.EventArgs e) {
			Query(true);
		}

		private void FindPrev_button_Click(object sender, System.EventArgs e) {
			Query(false);
		}

		private void FindFirst_button_Click(object sender, System.EventArgs e) {
			Results.CurrentVerse = 0;
			Query(true);
		}

		private void FindLast_button_Click(object sender, System.EventArgs e) {
			if (bibleVersion == null) return;
			Results.CurrentVerse = bibleVersion.VerseCount - 1;
			Query(false);
		}

		#endregion

	}
}
