using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using DreamBeam.FileTypes;
using Khendys.Controls;

namespace DreamBeam.Bible {

	public class BibleRTF : Khendys.Controls.ExRichTextBox {
		private int currentVerse;
		private int firstVerse, lastVerse;
		private int verseBufferSize = 200;
		private ArrayList rtfFonts = new ArrayList();
		private ArrayList rtfColors = new ArrayList();
		private ArrayList verseIndex = new ArrayList();
		private int matchFound = -1;
		public string lastRegex;

		public BibleRTF() {
			int fSize = 10;

			rtfFonts.Add(new Font("Arial", fSize, FontStyle.Bold));			// font \f0
			rtfFonts.Add(new Font("Times", fSize, FontStyle.Regular));		// font \f1

			rtfColors.Add(RtfColor.White);	// color 1 (\highlight1 or \cf1)
			rtfColors.Add(RtfColor.Black);  // color 2
			rtfColors.Add(RtfColor.Yellow);	// color 3
			rtfColors.Add(RtfColor.Green);	// color 4
			rtfColors.Add(RtfColor.Red);	// color 5
			rtfColors.Add(RtfColor.Blue);	// color 6
			rtfColors.Add(RtfColor.Maroon);	// color 7
			rtfColors.Add(RtfColor.Olive);	// color 8
			rtfColors.Add(RtfColor.Navy);	// color 9
			rtfColors.Add(RtfColor.Purple);	// color 10
			rtfColors.Add(RtfColor.Teal);	// color 11
			rtfColors.Add(RtfColor.Gray);	// color 12
			rtfColors.Add(RtfColor.Silver);	// color 13
			rtfColors.Add(RtfColor.Lime);	// color 14
			rtfColors.Add(RtfColor.Aqua);	// color 15
			rtfColors.Add(RtfColor.Fuchsia);	// color 16

		}

		#region Properties

		public int CurrentVerse {
			get { return currentVerse; }
			set { currentVerse = value; }
		}

		#endregion

		/// <summary>
		/// Returns the range of verses to load, so that the "mid" verse is at the middle and we
		/// stay within the verseBufferSize limits.
		/// </summary>
		/// <param name="mid">The verse index to keep at the middle</param>
		/// <param name="max">The last verse available</param>
		/// <returns></returns>
		private int[] loadRange(int mid, int max) {
			//int[] r = new int[2];
			int first = mid - (verseBufferSize / 2);
			int last = mid + (verseBufferSize / 2);

			if (first < 0) {
				last -= first;
				first = 0;
			}

			if (last > max) {
				first -= last - max;
				last = max;
				if (first < 0) { first = 0; }
			}

			return new int[2] {first, last};
		}

		private static int AppendText(StringBuilder str, string text) {
			int length = text.Length;
			str.Append(text.Replace("\n", @"\par "));
			return length;
		}

		public void Populate(BibleVersion bible, int target) {
			StringBuilder _rtfData = new StringBuilder();

			//Tools.ElapsedTime("Start Populate");

			if (target < 0 || target >= bible.VerseCount) return;

			int[] range = loadRange(target, bible.VerseCount - 1);
			
			verseIndex.Clear();
			firstVerse = range[0];
			lastVerse = range[1];
			currentVerse = target;

			int length = 0;
			for (int i = range[0]; i <= range[1]; i++) {
				verseIndex.Add(length);

				// Set the reference text to background color 2, foreground color 3,
				// bold, font 0, size 10pt
				_rtfData.Append(@"\highlight12\cf3\b\f0\fs20 ");
				// We need to keep track of how many non-control characters we're adding so we
				// can't just use: _rtfData.Append( bible.GetSimpleRef(i) + " " );
				// The text added here should be identical to the text searched for in BibleVersion.Find
				length += BibleRTF.AppendText(_rtfData, bible.GetSimpleRef(i, true) + " " );
				_rtfData.Append(@"\highlight0\cf2\b0\f1\fs20 ");
				length += BibleRTF.AppendText(_rtfData, " " + bible.GetSimpleVerseText(i) + "\n");
				
				// Note: Unicode characters get converted to their ASCII equivalents.
			}
			
			this.SelectAll();
			this.InsertRawRtf(_rtfData.ToString(), rtfFonts, rtfColors);
			
			this.SelectAll();
			this.SelectionIndent = 5;
			this.SelectionHangingIndent = 75;
			this.SelectionRightIndent = 5;
			
			this.ScrollTo( this.GetSelectionFromVerseIndex(target)[0] );
			//Tools.ElapsedTime("Finish Populate");
		}

		/// <summary>
		/// Finds a verse matching the regex, and updates the RTF control to make the verse visible.
		/// </summary>
		/// <param name="bible"></param>
		/// <param name="dirFwd"></param>
		/// <param name="regex"></param>
		/// <returns>The result of calling BibleVersion.Find (negative number to indicate failure, or a 0-based verse index)</returns>
		public int Find(BibleVersion bible, bool dirFwd, string regex) {
			Tools.ElapsedTime("Start regex search");
			
			if (regex.Length == 0) return -1;

			// We need to handle "FindNext" and "FindPrevious" type searches
			if (regex == lastRegex) {
				if (currentVerse == matchFound && dirFwd) {
					currentVerse++;
				} else if (currentVerse == matchFound && !dirFwd) {
					currentVerse--;
				}
			}

			int target = bible.Find(currentVerse, dirFwd, regex);
			Tools.ElapsedTime("End regex search");

			lastRegex = regex;
			if (target >= 0) {
				currentVerse = target;
				matchFound = target;
				Populate(bible, target);
				this.HighlightVerseRegex(target, regex);
			} else if (matchFound >= 0) {
				// Re-populate to remove highlight
				Populate(bible, currentVerse);
				matchFound = -1;
			}
			return target;
		}

		private void HighlightVerseRegex(int verseIdx, string regex) {
			if (regex.Length == 0) return;

			Regex r = new Regex(regex, RegexOptions.IgnoreCase);
			Match m;
			int[] selection = GetSelectionFromVerseIndex(verseIdx);

			m = r.Match(this.Text, selection[0], selection[1] - selection[0]);
			while (m.Success) {
				this.SelectionStart = m.Index;
				this.SelectionLength = m.Length;
				this.SelectionColor = System.Drawing.Color.Red;
				this.ScrollTo(m.Index);
				m = m.NextMatch();
			}

			// If we scroll to the beginning of the verse, the highlighted part could
			// be off the screen for a multiline verse.
			//this.ScrollTo(selection[0]);
		}

		public void ScrollTo(int index) {
			this.Select(index, 0);
			this.ScrollToCaret();
			//Console.WriteLine("Scrolling to " + index + " " + this.GetLineFromCharIndex(index));
			//this.Invalidate();
			//this.InvokePaint(this, null);
			//this.Refresh();			// Doesn't seem to do anything. Why?
			//this.Focus();			// This does the repainting, but now whoever called this.Find needs to regain focus
		}

		#region Accessors

		/// <summary>
		/// Finds the starting and ending point (character index) of a 
		/// verse in the text box.
		/// </summary>
		/// <param name="i">The verse index (0..31102)</param>
		/// <returns>A 2-element array with the start and end point</returns>
		public int[] GetSelectionFromVerseIndex(int verseIdx) {
			int start, end;

			if (verseIdx < firstVerse || verseIdx > lastVerse) {
				return new int[] {-1, -1};
			}

			start = Convert.ToInt32(verseIndex[verseIdx - firstVerse]);
			if ((verseIdx - firstVerse + 1) < verseIndex.Count) {
				end = Convert.ToInt32(verseIndex[verseIdx - firstVerse + 1]);
			} else {
				end = this.TextLength;
			}

			return new int[2] {start, end};
		}

		public int GetVerseIndexFromSelection(int selectionStart) {
			for (int i = 0; i < verseIndex.Count; i++) {
				if (selectionStart < Convert.ToInt32(verseIndex[i])) {
					// i is the verse after the one containing selectionStart
					return i - 1 + firstVerse;
				}
			}
			// selectionStart is in the last verse
			return verseIndex.Count - 1 + firstVerse;
		}


		#endregion

	}
}
