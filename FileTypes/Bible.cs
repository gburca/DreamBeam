using System;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Threading;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;


namespace DreamBeam.FileTypes {

	#region Enums
	// These are used as array indices so they must be numbered sequentially, from 0
	// They must also match the order of the tabs in the Options dialog box.
	public enum BibleTextType {
		Verse,
		Reference,
		Translation
	}
	#endregion

	#region Structs
	[Serializable]
	public struct BibleBook {
		public string Short;
		public string Long;
	}
	#endregion
	/// <summary>
	/// This class is a wrapper around Diatheke and other bible sources. It
	/// takes a long time to retrieve one bible translation in its entirety from
	/// Diatheke so once we retrieved it we want to save it to speed up
	/// processing subsequently.
	/// </summary>
	[Serializable]
	public class BibleLib {
		private Hashtable versions;
		public bool IsDirty;	// If this is true, we need to serialize this class

		public BibleLib() {
			versions = new Hashtable();
			//IsDirty = false;
		}

		#region Indexers
		public BibleVersion this[string version] {
			get {
				if (!versions.Contains(version)) {
					return null;
				}
				return (BibleVersion)versions[version];
			}

			// Handles: Bible["RomCor"] = new BibleVersion("RomCor", some_bible_text);
			set {
				IsDirty = true;
				if (!versions.Contains(version)) {
					versions.Add(version, value);
				} else {
					versions[version] = value;
				}
			}
		}
		#endregion

		public bool TranslationExists(string translation) {
			return versions.Contains(translation);
		}

		public string[] Translations() {
			string[] tr = new string[versions.Count];
			int i = 0;
			foreach (string key in versions.Keys) {
				tr[i++] = key;
			}
			return tr;
		}

		public void Remove(string version) {
			if (this.TranslationExists(version)) {
				versions.Remove(version);
			}
		}

		public bool Add(AxACTIVEDIATHEKELib.AxActiveDiatheke Diatheke, string version) {
			return Add(Diatheke, version, null);
		}
		public bool Add(AxACTIVEDIATHEKELib.AxActiveDiatheke Diatheke, string version, System.Data.DataTable replacements) {
			return Add(Diatheke, version, replacements, null);
		}
		public bool Add(AxACTIVEDIATHEKELib.AxActiveDiatheke Diatheke, string version, System.Data.DataTable replacements, EventHandler onProgress) {
			//Thread timerThread = null;
			Thread diathekeThread = null;
			//TimerThreadHelper threadHelper;
			DiathekeThreadHelper diathekeHelper;
			bool diathekeAutoupdate = Diatheke.autoupdate;

			//System.EventArgs e = new System.EventArgs();
			Diatheke.autoupdate = false;
			Diatheke.book = version;
			Diatheke.maxverses = -1;
			//Diatheke.key = "Gen 1:1 - Lev 7:3";
			//Diatheke.key = "Gen 1:1 - Gen 1:3";
			Diatheke.key = "Gen 1:1 - Rev 22:21";

			/*
			if (onProgress != null) {
				try {
					threadHelper = new TimerThreadHelper(500, onProgress);
					timerThread = new Thread( new ThreadStart(threadHelper.Run) );
					timerThread.Name = "Timer thread";
					//timerThread.Start();
				} catch (Exception ex) {
					Console.WriteLine("Thread helper: " + ex.Message);
				}
			}
			*/

			Tools.ElapsedTime("Retrieving bible text: " + version + "  " + Diatheke.key);
			diathekeHelper = new DiathekeThreadHelper(Diatheke);
			diathekeThread = new Thread(new ThreadStart(diathekeHelper.Run));
			diathekeThread.Name = "Diatheke reading thread";
			diathekeThread.Start();
			diathekeThread.Join();

			//if (onProgress != null) { Console.WriteLine("Stop thread"); timerThread = null; }
			if (Diatheke.value.Length > 100) {
				BibleVersion b = new BibleVersion(version, Diatheke.value, replacements, onProgress);
				if (b.VerseCount > 0) {
					this.IsDirty = true;
					this.Remove(version);
					versions.Add(version, b);
					return true;
				}
			}

			// Restore the previous setting
			Diatheke.autoupdate = diathekeAutoupdate;

			return false;
		}


		public void SerializeNow(string file) {
			FileInfo fInfo = new FileInfo(file);
			FileStream fs = null;

			if (!fInfo.Directory.Exists) {
				Directory.CreateDirectory(fInfo.DirectoryName);
			}

			try {
				fs = new FileStream(file, FileMode.Create);
				BinaryFormatter b = new BinaryFormatter();
				b.Serialize(fs, this);
			} catch (Exception ex) {
				Console.WriteLine("Error serializing: " + ex.Message);
			} finally {
				if (fs != null) fs.Close();
			}
		}

		public static BibleLib DeserializeNow(string file) {
			FileStream fs = null;
			BibleLib bLib = null;
			try {
				fs = new FileStream(file, FileMode.Open);
				BinaryFormatter b = new BinaryFormatter();
				try {
					bLib = (BibleLib)b.Deserialize(fs);
				} catch (Exception ex) {
					Console.WriteLine("Error deserializing: " + ex.Message);
				}
				if (bLib == null) {
					fs.Close();
					fs = null;
					File.Delete(file);
				}
				bLib.IsDirty = false;
				return bLib;	// "finally" will execute after the return to close the file
			} catch (Exception ex) {
				Console.WriteLine("Error deserializing: " + ex.Message);
			} finally {
				if (fs != null) fs.Close();
			}
			return null;
		}
	}


	[Serializable]
	public class BibleVersion {
		private string _version;
		private BibleVerse[] verses = new BibleVerse[31102];
		private int _VerseCount;
		//private string[] books = new string[66];
		public BibleBook[] BibleBooks = new BibleBook[66];
		/// <summary>
		/// Sword uses the following book names as standard. They're needed to interpret the localization
		/// files and determine the proper order of books.
		/// </summary>
		public static string[] SwordBookNames = {
			"Genesis", "Exodus", "Leviticus", "Numbers", "Deuteronomy", "Joshua", "Judges", 
			"Ruth", "I Samuel", "II Samuel", "I Kings", "II Kings", "I Chronicles", "II Chronicles", 
			"Ezra", "Nehemiah", "Esther", "Job", "Psalms", "Proverbs", "Ecclesiastes", 
			"Song of Solomon", "Isaiah", "Jeremiah", "Lamentations", "Ezekiel", "Daniel", "Hosea", 
			"Joel", "Amos", "Obadiah", "Jonah", "Micah", "Nahum", "Habakkuk", 
			"Zephaniah", "Haggai", "Zechariah", "Malachi", "Matthew", "Mark", "Luke", 
			"John", "Acts", "Romans", "I Corinthians", "II Corinthians", "Galatians", "Ephesians", 
			"Philippians", "Colossians", "I Thessalonians", "II Thessalonians", "I Timothy", "II Timothy", "Titus", 
			"Philemon", "Hebrews", "James", "I Peter", "II Peter", "I John", "II John", 
			"III John", "Jude", "Revelation of John"};

		private System.Data.DataTable _replacements;

		public BibleVersion(string version) : this(version, null) { }
		public BibleVersion(string version, string diathekeText) : this(version, diathekeText, null) { }
		public BibleVersion(string version, string diathekeText, System.Data.DataTable replacements) : this(version, diathekeText, replacements, null) { }
		public BibleVersion(string version, string diathekeText, System.Data.DataTable replacements, EventHandler onProgress) {
			_version = version;
			_replacements = replacements;
			if (diathekeText != null) {
				ParseDiathekePlain(diathekeText, onProgress);
			}
		}

		#region Properties
		/// <summary>
		/// Sets the verses in this bible translation based on the string returned by Diatheke containing the whole bible text.
		/// </summary>
		public string diatheke {
			set { ParseDiathekePlain(value, null); }
		}

		/// <summary>
		/// Gets or sets the version (or translation) of the bible. Ex. KJV, NIV, RomCor
		/// </summary>
		public string version {
			get { return _version; }
			set { _version = value; }
		}

		public int VerseCount {
			get { return _VerseCount; }
		}
		#endregion

		#region Indexers

		/// <summary>
		/// Returns the n-th (0-based) verse in the bible
		/// </summary>
		public BibleVerse this[int verseIdx] {
			get {
				if (verseIdx < 0 || verseIdx >= _VerseCount) return null;
				return verses[verseIdx];
			}
		}
		#endregion

		/// <summary>
		/// Returns a reference of the type "1 Corinthians 4:8" using the long bible book
		/// name in whatever language the current Sword locale is set to.
		/// </summary>
		/// <param name="verseIdx">Zero-based verse in the bible to return the reference for</param>
		/// <returns></returns>
		public string GetRef(int verseIdx) {
			if (verseIdx < 0 || verseIdx >= _VerseCount) return "";
			return string.Format("{0} {1}:{2}",
				BibleBooks[verses[verseIdx].b].Long,
				verses[verseIdx].c, verses[verseIdx].v);
		}

		/// <summary>
		/// Returns a bible reference (ex. Genesis 4:7) after removing special
		/// characters from the book name.
		/// </summary>
		/// <param name="verseIdx"></param>
		/// <param name="Abbreviated">If true, returns the abbreviated name of the bible book</param>
		/// <returns></returns>
		public string GetSimpleRef(int verseIdx, bool Abbreviated) {
			if (verseIdx < 0 || verseIdx >= _VerseCount) return "";
			BibleVerse v = verses[verseIdx];

			if (Abbreviated) {
				return string.Format("{0} {1}:{2}", Tools.RemoveDiacritics(BibleBooks[v.b].Short), v.c, v.v);
			} else {
				return string.Format("{0} {1}:{2}", Tools.RemoveDiacritics(BibleBooks[v.b].Long), v.c, v.v);
			}
		}

		public string GetSimpleRef(string fullRef, bool Abbreviated) {
			return GetSimpleRef(GetVerseIndex(fullRef), Abbreviated);
		}

		public string GetVerse(int verseIdx) {
			if (verseIdx < 0 || verseIdx >= _VerseCount) return "";
			return this.GetRef(verseIdx) + "\t" + verses[verseIdx].t;
		}

		public string GetSimpleVerseText(int verseIdx) {
			if (verseIdx < 0 || verseIdx >= _VerseCount) return "";
			if (verses[verseIdx].t2.Length > 0) {
				return verses[verseIdx].t2;
			} else {
				return verses[verseIdx].t;
			}
		}

		public string BookName(int bookNum, bool Abbreviated) {
			if (bookNum >= 0 && bookNum < this.BibleBooks.Length) {
				return Abbreviated ? this.BibleBooks[bookNum].Short : this.BibleBooks[bookNum].Long;
			}
			return "";
		}

		public string BookName(int bookNum) {
			return this.BookName(bookNum, false);
		}

		/// <summary>
		/// Finds the first bible book that starts with the letters passed in.
		/// </summary>
		/// <param name="book">The letters the book starts with.</param>
		/// <returns>A number: 1-66, or -1 if no book starts with the letters passed in.</returns>
		public int BookNumber(string book) {
			int i = 0;
			string bLC;
			book = Tools.RemoveDiacritics(book.ToLower());
			foreach (BibleBook b in BibleBooks) {
				bLC = Tools.RemoveDiacritics(b.Long.ToLower());
				if (bLC.StartsWith(book)) { return i; }
				i++;
			}
			return -1;
		}

		/// <summary>
		/// Take a reference such as "e 4 6" and convert it to "Exodus 4:6"
		/// </summary>
		/// <param name="reference"></param>
		/// <returns></returns>
		public string NormalizeRef(string reference) {
			int b, c, v;
			reference = Regex.Replace(reference.Trim(), @"\s+", " ");
			Regex r = new Regex(@"(.*)\s+(\d+)[\s:]+(\d+)", RegexOptions.IgnoreCase);
			Match m = r.Match(reference);

			if (m.Groups.Count == 4) {
				b = BookNumber(m.Groups[1].Value);
				c = Convert.ToInt32(m.Groups[2].Value);
				v = Convert.ToInt32(m.Groups[3].Value);
				if (b >= 0 && c > 0 && v > 0) {
					return BookName(b) + " " + c.ToString() + ":" + v.ToString();
				}
			}

			return "";
		}

		public int GetVerseIndex(string reference) {
			int b, c, v;
			reference = Regex.Replace(reference.Trim(), @"\s+", " ");
			Regex r = new Regex(@"(.*)\s+(\d+)[\s:]+(\d+)", RegexOptions.IgnoreCase);
			Match m = r.Match(reference);

			if (m.Groups.Count == 4) {
				// Group[0] is the full match
				b = BookNumber(m.Groups[1].Value);
				c = Convert.ToInt32(m.Groups[2].Value);
				v = Convert.ToInt32(m.Groups[3].Value);
				return GetVerseIndex(b, c, v);
			}

			return -1;
		}

		public int GetVerseIndex(int b, int c, int v) {
			int mid, low = 0;
			int high = _VerseCount - 1;

			if (b < 0 || c < 1 || v < 1) { return -1; }

			while (low <= high) {
				mid = low + (high - low) / 2;

				switch (verses[mid].CompareTo(b, c, v)) {
					case 1:
						high = mid - 1;
						break;
					case -1:
						low = mid + 1;
						break;
					default:
						return mid;
				}
			}

			return -1;
		}

		/// <summary>
		/// Finds a verse that matches the regular expression provided.
		/// </summary>
		/// <param name="start_i">Verse index to start searching from.</param>
		/// <param name="dirFwd">Direction to search in. If "true" searches forward.</param>
		/// <param name="regex">The regular expression to search for.</param>
		/// <returns>A negative number when a matching verse could not be found. The 0-based verse index if the verse was found.</returns>
		public int Find(int start_i, bool dirFwd, string regex) {
			Regex r;
			Match m;
			try {
				r = new Regex(regex, RegexOptions.IgnoreCase);
			} catch {
				// If the user enters a regex that is not syntactically correct, just fail silently
				return -1;
			}

			if (regex.Length == 0 || start_i < 0 || start_i >= _VerseCount) return -1;

			if (dirFwd) {
				for (int i = start_i; i < _VerseCount; i++) {
					m = r.Match(GetSimpleRef(i, true) + " " + verses[i].t2);
					if (m.Success) { return i; }
				}
				for (int i = 0; i < start_i; i++) {
					m = r.Match(GetSimpleRef(i, true) + " " + verses[i].t2);
					if (m.Success) {
						Console.WriteLine("Verse search WRAP");
						return i;
					}
				}
			} else {
				for (int i = start_i; i >= 0; i--) {
					m = r.Match(GetSimpleRef(i, true) + " " + verses[i].t2);
					if (m.Success) return i;
				}
				for (int i = _VerseCount - 1; i > start_i; i--) {
					m = r.Match(GetSimpleRef(i, true) + " " + verses[i].t2);
					if (m.Success) return i;
				}
			}

			Console.WriteLine("Search failed");
			return -1;
		}


		/// <summary>
		/// Searching with regular expressions through bible texts that contain
		/// punctuation and foreign characters is no fun. This function strips
		/// out or replaces all the undesirables to make searching easier.
		/// </summary>
		/// <param name="verses">The bible text</param>
		/// <returns>The converted text</returns>
		private string Replace(string text) {
			if (_replacements == null) return text;
			foreach (System.Data.DataRow r in _replacements.Rows) {
				// If the user entered an invalid regex, we need to trap it
				try {
					text = Regex.Replace(text, r.ItemArray[0].ToString(), r.ItemArray[1].ToString());
				} catch { }
			}
			return text;
		}

		/// <summary>
		/// Parse the "plain" Diatheke output format into separate verses.
		/// </summary>
		private void ParseDiathekePlain(string diathekeText, EventHandler onProgress) {
			int i = 0, b = -1;
			string book = "no such bible book";
			System.EventArgs e = new System.EventArgs();
			Regex r = new Regex(@"^(\d*[^:\d]+)(\d+):(\d+):\W+(.*)", RegexOptions.Compiled);
			Match m;

			foreach (string verse in Tools.Diatheke_ConvertEncoding(diathekeText).Split('\n')) {
				if (onProgress != null && ((i % 200) == 0)) { onProgress(this, e); }
				if (i >= 31102) {
					break;
				}
				m = r.Match(verse);
				if (m.Groups.Count == 5) {
					// We have a good match
					// Groups[0] is the full match
					if (book != m.Groups[1].Value) {
						b++;
						BibleBooks[b].Long = m.Groups[1].Value;
						book = BibleBooks[b].Long;
						Console.WriteLine("Processing " + book);
					}
					verses[i] = new BibleVerse(i,
						b, Convert.ToInt32(m.Groups[2].Value), Convert.ToInt32(m.Groups[3].Value),
						m.Groups[4].Value.Trim());
					i++;
				}
			}

			this._VerseCount = i;

			for (i = 0; i < this.BibleBooks.Length; i++) {
				if (BibleBooks[i].Long == null) BibleBooks[i].Long = "";
				string bk = BibleBooks[i].Long;
				bk = Regex.Replace(bk.Trim(), @"^III ", "3 ");
				bk = Regex.Replace(bk, @"^II ", "2 ");
				bk = Regex.Replace(bk, @"^I ", "1 ");
				BibleBooks[i].Long = bk;
				BibleBooks[i].Short = bk;
			}

			Tools.ElapsedTime("Starting conversion to ASCII");
			i = 0;
			// We need to do this after we clean up the book names above
			foreach (BibleVerse v in verses) {
				// This needs to be constructed roughly the same as the text
				// displayed in the Results RTF control so that the user can
				// type based on what's displayed.
				if (v != null && _replacements != null) {
					v.t2 = this.Replace(v.t);
				}
				if (onProgress != null && ((i++ % 200) == 0)) { onProgress(this, e); }
			}
			Tools.ElapsedTime("Finished converting to ASCII");
		}
	}

	/// <summary>
	/// Represents a verse. i is the verse index (0..31102), b, c, v are the
	/// book, chapter, verse numbers. t is the actual verse text.
	/// </summary>
	[Serializable]
	public class BibleVerse {
		public int i, b, c, v;
		public string t, t2;

		public BibleVerse() { }
		public BibleVerse(int i, int b, int c, int v, string t) {
			this.i = i;
			this.b = b;
			this.c = c;
			this.v = v;
			this.t = t;
		}

		public override string ToString() {
			return b.ToString() + ":" + c.ToString() + ":" + v.ToString() + "\t" + t;
		}

		public int CompareTo(int b, int c, int v) {
			int cb = this.b.CompareTo(b);
			int cc = this.c.CompareTo(c);
			int cv = this.v.CompareTo(v);

			if (cb != 0) {
				return cb;
			} else if (cc != 0) {
				return cc;
			} else {
				return cv;
			}
		}
	}

	#region Experimental code. Could be removed
	public class TimerThreadHelper {
		private EventHandler onProgress;
		System.Windows.Forms.Timer timer;

		public TimerThreadHelper(int interval, EventHandler onProgress) {
			this.onProgress = onProgress;
			timer = new System.Windows.Forms.Timer();
			timer.Interval = interval;
			timer.Tick += onProgress;
		}

		~TimerThreadHelper() {
			timer.Tick -= onProgress;
			timer.Stop();
		}

		public void Run() {
			Console.WriteLine("About to start timer");
			timer.Enabled = true;
			timer.Start();
			while (timer.Enabled) {
				Thread.Sleep(1000);
			}
		}
	}

	public class DiathekeThreadHelper {
		AxACTIVEDIATHEKELib.AxActiveDiatheke Diatheke;

		public DiathekeThreadHelper(AxACTIVEDIATHEKELib.AxActiveDiatheke Diatheke) {
			this.Diatheke = Diatheke;
		}

		public void Run() {
			Console.WriteLine("About to start query");
			Diatheke.query();
		}
	}
	#endregion

	/// <summary>
	/// This class contains all the details (font, background, etc...) required to properly display a verse.
	/// </summary>
	public class ABibleVerse : Content, IContentOperations {
		public int verseIdx;
		private BibleVersion bible;
		//public Bitmap bitmap;	// This doesn't really belong here...
		public Config config;
		private Thread render;
		private Object renderLock = new Object();
		public override string ThemePath {
			set {
				bgImage = null;
				if (Path.GetExtension(value).EndsWith("xml")) {
					Theme = (Theme)Theme.DeserializeFrom(typeof(BibleTheme), value);
				} else {
				}
			}
		}

		public ABibleVerse() : this(null) { }
		public ABibleVerse(BibleVersion bible) : this(bible, 1) { }
		public ABibleVerse(BibleVersion bible, int verseIdx) : this(bible, verseIdx, null) { }
		public ABibleVerse(BibleVersion bible, int verseIdx, Config config) {
			this.bible = bible;
			this.verseIdx = verseIdx;
			this.config = config;
			if (config != null) this.Theme = config.theme.Bible;
			this.PreRenderFrames();
		}

		public int VisibleHashCode(int vIdx) {
			return base.VisibleHashCode() + vIdx.ToString().GetHashCode();
		}

		#region IContentOperations Members

		public Bitmap GetBitmap(int Width, int Height) {
			return GetBitmap(Width, Height, this.verseIdx);
		}

		/// <summary>
		/// Draws the contents of the current verse on a bitmap
		/// </summary>
		/// <param name="Width"></param>
		/// <param name="Height"></param>
		/// <returns></returns>
		public Bitmap GetBitmap(int Width, int Height, int vIdx) {
			if (this.RenderedFramesContains(this.VisibleHashCode(vIdx))) {
				Console.WriteLine("ABibleVerse.GetBitmap pre-render cache hit.");
				return this.RenderedFramesGet(this.VisibleHashCode(vIdx)) as Bitmap;
			}

			lock (renderLock) {
				Pen p;
				SolidBrush brush;
				Bitmap bmp = new Bitmap(Width, Height);
				Graphics graphics = Graphics.FromImage(bmp);
				GraphicsPath pth, pth2;
				Rectangle pathRect, bounds;
				RectangleF measuredBounds;
				Font font;
				float fontSz;
				BeamTextFormat[] format = this.Theme.TextFormat;
				string[] text = new string[Enum.GetValues(typeof(BibleTextType)).Length];

				graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
				graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
				graphics.SmoothingMode = SmoothingMode.AntiAlias;
				graphics.SmoothingMode = SmoothingMode.HighQuality;
				graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

				this.RenderBGImage(config.theme.Bible.BGImagePath, graphics, Width, Height);

				if (HideText) {
					return bmp;
				}

				string vText = bible[vIdx].t;
				// Non-breaking hyphen gets displayed with too much space after it. Change it to regular hyphen.
				vText = Regex.Replace(vText, "\u2011", "\u00ad");
				text[(int)BibleTextType.Reference] = bible.GetRef(vIdx);
				text[(int)BibleTextType.Verse] = vText;
				text[(int)BibleTextType.Translation] = bible.version;
				pth = new GraphicsPath();

				foreach (int type in Enum.GetValues(typeof(BibleTextType))) {
					// We have to keep the text within these user-specified boundaries
					bounds = new System.Drawing.Rectangle(
						(int)(format[type].Bounds.X / 100 * Width),
						(int)(format[type].Bounds.Y / 100 * Height),
						(int)(format[type].Bounds.Width / 100 * Width),
						(int)(format[type].Bounds.Height / 100 * Height));

					/*
					// For debug:
					graphics.DrawRectangle(new Pen(format[type].OutlineColor, 2), bounds);
					Rectangle rect = new Rectangle((int)(Width/2), (int)(Height/2), 0, 0);
					int i = 25;
					while (rect.Width < Width) {
						graphics.DrawRectangle(new Pen(Color.Gray, 1), rect);
						rect.Inflate(i, i);
					};
					*/

					p = new Pen(format[type].TextColor, 1.0F);
					if (showRectangles == true) graphics.DrawRectangle(p, bounds);

					if (text[type].Length > 0) {
						p.LineJoin = LineJoin.Round;
						StringFormat sf = new StringFormat();
						sf.Alignment = format[type].HAlignment;
						sf.LineAlignment = format[type].VAlignment;
						sf.FormatFlags = StringFormatFlags.NoFontFallback;

						// Make a rectangle that is very tall to see how far down the text would stretch.
						pathRect = bounds;
						pathRect.Height *= 2;

						// We start with the user-specified font size ...
						fontSz = format[type].TextFont.Size;

						// ... and decrease the size (if needed) until it fits within the user-specified boundaries
						do {
							font = new Font(format[type].TextFont.FontFamily, fontSz, format[type].TextFont.Style);
							pth.Reset();
							pth.AddString(text[type], font.FontFamily, (int)font.Style, font.Size, pathRect, sf);
							measuredBounds = pth.GetBounds();
							fontSz--;
							if (fontSz == 0) break;
						} while (measuredBounds.Height > bounds.Height);

						// We need to re-create the path. For some reason the do-while loop puts it in the wrong place.
						pth.Reset();
						pth.AddString(text[type], font.FontFamily, (int)font.Style, font.Size, bounds, sf);

						brush = new SolidBrush(format[type].TextColor);

						#region Add special effects
						if (format[type].Effects == "Outline") {
							p = new Pen(format[type].OutlineColor, format[type].OutlineSize);
							//pth.AddString(text[type],
							//font.FontFamily, 0, font.Size,
							//bounds, StringFormat.GenericTypographic);
							graphics.DrawPath(p, pth);

						} else if (format[type].Effects == "Filled Outline") {
							pth2 = (GraphicsPath)pth.Clone();
							graphics.FillPath(brush, pth);
							graphics.DrawPath(p, pth);

							// Widen the path
							Pen widenPen = new Pen(format[type].OutlineColor, format[type].OutlineSize);
							widenPen.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
							pth2.Widen(widenPen);
							graphics.FillPath(new SolidBrush(format[type].OutlineColor), pth2);
							graphics.DrawPath(p, pth);
							graphics.FillPath(brush, pth);

						} else { //if (format[type].Effects == "Normal") {
							graphics.FillPath(brush, pth);
							graphics.DrawPath(p, pth);

						}
						#endregion

					}
				}

				graphics.Dispose();
				if (this.config != null && this.config.PreRender) {
					this.RenderedFramesSet(this.VisibleHashCode(vIdx), bmp);
				}

				return bmp;
			}
		}

		public bool Next() {
			if (verseIdx < bible.VerseCount - 1) {
				verseIdx++;
				this.PreRenderFrames();
				return true;
			}
			return false;
		}

		public bool Prev() {
			if (verseIdx > 0) {
				verseIdx--;
				return true;
			}
			return false;
		}

		public bool ShowRectangles {
			get { return showRectangles; }
			set { showRectangles = value; }
		}

		//		public string GetIdentity() {
		//			return this.bible.version + "\t" + this.verseIdx + " DEBUG: " + this.bible[this.verseIdx].t;
		//		}

		public ContentIdentity GetIdentity() {
			ContentIdentity ident = new ContentIdentity();
			ident.Type = (int)ContentType.BibleVerseIdx;
			ident.BibleTransl = bible.version;
			ident.VerseIdx = verseIdx;
			return ident;
		}

		private void PreRenderFramesThreaded() {
			// We only pre-render one frame in advance
			if (bible == null) return;
			if (this.config != null && (this.verseIdx + 1 < bible.VerseCount)) {
				this.GetBitmap(this.config.BeamBoxSizeX, this.config.BeamBoxSizeY, this.verseIdx + 1);
			}
		}

		public void DefaultBackground(Config conf) {
			this.BGImagePath = conf.theme.Bible.BGImagePath;
		}

		public void PreRenderFrames() {
			if (render != null && render.IsAlive) {
				render.Abort();
			}

			render = new Thread(new ThreadStart(PreRenderFramesThreaded));
			render.IsBackground = true;
			render.Name = "PreRender Verse";
			render.Start();
		}

		/// <summary>
		/// Implements the ICloneable interface
		/// </summary>
		/// <returns></returns>
		object ICloneable.Clone() {
			// simply delegate to our type-safe cousin
			return this.Clone();
		}

		public virtual ABibleVerse Clone() {
			ABibleVerse v = new ABibleVerse(this.bible, this.verseIdx, this.config);

			// Start with a flat, memberwise copy
			v = this.MemberwiseClone() as ABibleVerse;

			// Copy things that need special attention
			// Maybe Config?
			//v.config = (Config)this.config.Clone();
			return v;
		}

		#endregion
	}


}
