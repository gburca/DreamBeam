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
		public BibleVersion BibleText_Bible;
		private Timer TextTypedTimer = new Timer();
		public EventHandler BibleText_RegEx_ComboBox_EventHandler;
		protected ListBox BibleText_Bookmarks, BibleText_Translations;
		protected BibleLib bibles;

		public BibleText() {
			InitializeComponent();
			BibleText_RegEx_ComboBox_EventHandler = new EventHandler(BibleText_RegEx_ComboBox_TimedOut);
		}

		public void Setup(MainForm mf, ListBox btb, ListBox btt, BibleLib bibles) {
			mainForm = mf;
			BibleText_Bookmarks = btb;
			BibleText_Translations = btt;
			this.bibles = bibles;
		}

		public BibleVersion BibleVersion {
			get { return BibleText_Bible; }
			set {
				BibleText_Bible = value;
				BibleText_Results_Update();
			}
		}

		public void goToBookmark(String bookmark) {
			if (BibleText_Bible == null) return;

			BibleText_RegEx_ComboBox.Text = "^" + this.BibleText_Bible.GetSimpleRef(bookmark, true) + @"\s+.*";
			int vidx = BibleText_Query(true);
			if (vidx >= 0) {
				mainForm.DisplayPreview.SetContent(new ABibleVerse(this.BibleText_Bible, vidx, mainForm.Config));
			}
		}



		#region BibleText_Results RichTextBox

		private void BibleText_Results_Update() {
			try {
				string translation = BibleText_Translations.SelectedItem.ToString();
				if (bibles.TranslationExists(translation)) {
					BibleText_Results.Populate(bibles[translation], BibleText_Results.CurrentVerse);
				}
			} catch { }
		}

		private void BibleText_Results_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			System.Drawing.Point pt = new System.Drawing.Point(e.X, e.Y);
			if (BibleText_Results.Text.Length == 0) return;	// Otherwise we get range exceptions in GetCharFromPosition ...
			char c = BibleText_Results.GetCharFromPosition(pt);
			int idx = BibleText_Results.GetCharIndexFromPosition(pt);
			int l = BibleText_Results.GetLineFromCharIndex(idx);
			int vidx = BibleText_Results.GetVerseIndexFromSelection(idx);
			BibleText_Results.CurrentVerse = vidx;
			mainForm.DisplayPreview.SetContent(new ABibleVerse(BibleText_Bible, vidx, mainForm.Config));

			//Console.WriteLine("Clicked " + pt + " on line " + l + " at character number " +idx + "(" + c + ").");
			//Console.WriteLine("Clicked {0} on line {1} at character number {2} ({3}).", pt, l, idx, c);
			//Console.WriteLine("Verse: " + BibleText_Bible[vidx].ToString());
		}

		private void BibleText_Results_MouseEnter(object sender, System.EventArgs e) {
			// Allows us to use the scroll wheel by just pointing to the control
			BibleText_Results.Focus();
		}

		private void BibleText_Results_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
			if (this.BibleText_Bible == null) return;
			switch (e.KeyCode) {
				case Keys.A:	// Find first
					BibleText_FindFirst_button_Click(sender, null);
					break;
				case Keys.N:	// Find next
					BibleText_FindNext_button_Click(sender, null);
					break;
				case Keys.P:	// Find previous
					BibleText_FindFirst_button_Click(sender, null);
					break;
				case Keys.Z:	// Find last
					BibleText_FindLast_button_Click(sender, null);
					break;
				case Keys.Enter:
					int vidx = BibleText_Results.GetVerseIndexFromSelection(BibleText_Results.SelectionStart);
					BibleText_Results.CurrentVerse = vidx;
					mainForm.DisplayPreview.SetContent(new ABibleVerse(this.BibleText_Bible, vidx, mainForm.Config));
					mainForm.RightDocks_Preview_GoLive_Click(sender, null);
					break;
			}
			BibleText_Results.Focus(); // The "Click" handlers above switch focus to RegEx input
			e.Handled = true;
		}

		private int BibleText_Query(bool dirFwd) {
			if (BibleText_Translations.Items.Count == 0 || BibleText_Translations.SelectedIndices.Count == 0) return -1;
			string translation = BibleText_Translations.SelectedItem.ToString();
			if (!bibles.TranslationExists(translation)) return -1;
			int index = BibleText_Results.Find(bibles[translation], dirFwd, BibleText_RegEx_ComboBox.Text);
			// Force an update
			BibleText_Results.Focus();
			BibleText_RegEx_ComboBox.Focus();
			// Remove selection, or else we'll type over it
			BibleText_RegEx_ComboBox.SelectionStart = BibleText_RegEx_ComboBox.Text.Length;
			return index;
		}
		#endregion

		#region BibleText RegEx and Verse ComboBox

		private void BibleText_RegEx_ComboBox_TimedOut(Object myObject, EventArgs myEventArgs) {
			Console.WriteLine("Timer fired!");
			TextTypedTimer.Stop();
			TextTypedTimer.Enabled = false;
			TextTypedTimer.Tick -= BibleText_RegEx_ComboBox_EventHandler;
			BibleText_Query(true);
		}

		private void BibleText_RegEx_ComboBox_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e) {

			if (e.KeyCode == Keys.Enter) {
				string searchTerm = BibleText_RegEx_ComboBox.Text;
				if (!BibleText_RegEx_ComboBox.Items.Contains(searchTerm)) {
					BibleText_RegEx_ComboBox.Items.Insert(0, searchTerm);
				}
				BibleText_Results.Focus();
			} else if (BibleText_RegEx_ComboBox.Text.Length > 1 && BibleText_RegEx_ComboBox.Text != BibleText_Results.lastRegex) {
				if (TextTypedTimer.Enabled) {
					Console.WriteLine("Timer enabled. Restarting");
					// Restart timer
					TextTypedTimer.Stop();
					TextTypedTimer.Start();
				} else {
					Console.WriteLine("Time disabled");
					TextTypedTimer.Tick += BibleText_RegEx_ComboBox_EventHandler;
					TextTypedTimer.Interval = 300;
					TextTypedTimer.Start();
				}
			}

			//Console.WriteLine("KeyCode = " + e.KeyCode + " KeyValue " + e.KeyValue + " KeyData " + e.KeyData);
		}


		private void BibleText_RegEx_ComboBox_TextChanged(object sender, System.EventArgs e) {
			BibleText_Query(true);
		}

		private void BibleText_Verse_ComboBox_SelectedIndexChanged(object sender, System.EventArgs e) {
			if (BibleText_Bible == null) return;

			BibleText_RegEx_ComboBox.Text = "^" + BibleText_Bible.NormalizeRef(BibleText_Verse_ComboBox.SelectedIndex.ToString()) + @"\s+.*";
			BibleText_Query(true);
		}

		private void BibleText_Verse_ComboBox_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e) {
			if (e.KeyCode == Keys.Enter) {
				if (BibleText_Bible == null) return;

				string reference = BibleText_Verse_ComboBox.Text;
				string normReference;
				normReference = BibleText_Bible.NormalizeRef(reference);
				if (normReference.Length == 0) {
					// Maybe the user forgot to enter the verse. Default to the first verse of the chapter.
					normReference = BibleText_Bible.NormalizeRef(reference + " 1");
				}

				int index = BibleText_Bible.GetVerseIndex(normReference);
				if (index >= 0) {
					BibleText_RegEx_ComboBox.Text = "^" + this.BibleText_Bible.GetSimpleRef(normReference, true) + @"\s+.*";
					BibleText_Query(true);
					mainForm.DisplayPreview.SetContent(new ABibleVerse(this.BibleText_Bible, index, mainForm.Config));
					if (!BibleText_Verse_ComboBox.Items.Contains(reference)) {
						BibleText_Verse_ComboBox.Items.Insert(0, reference);
					}
					BibleText_Results.Focus();
				} else {
					BibleText_RegEx_ComboBox.Text = "";
					BibleText_Results.Populate(BibleText_Bible, 0);
					BibleText_Results.Focus();
					BibleText_Verse_ComboBox.Focus();
				}
				e.Handled = true;
			}
		}

		#endregion

		#region BibleText RegEx buttons
		private void BibleText_Bookmark_button_Click(object sender, System.EventArgs e) {
			if (BibleText_Bible == null) return;

			string reference = BibleText_Bible.GetRef(BibleText_Results.CurrentVerse);
			if (reference.Length > 0) {
				BibleText_Bookmarks.Items.Insert(0, reference);
			}
		}

		private void BibleText_FindNext_button_Click(object sender, System.EventArgs e) {
			BibleText_Query(true);
		}

		private void BibleText_FindPrev_button_Click(object sender, System.EventArgs e) {
			BibleText_Query(false);
		}

		private void BibleText_FindFirst_button_Click(object sender, System.EventArgs e) {
			BibleText_Results.CurrentVerse = 0;
			BibleText_Query(true);
		}

		private void BibleText_FindLast_button_Click(object sender, System.EventArgs e) {
			if (BibleText_Bible == null) return;
			BibleText_Results.CurrentVerse = BibleText_Bible.VerseCount - 1;
			BibleText_Query(false);
		}

		#endregion

	}
}
