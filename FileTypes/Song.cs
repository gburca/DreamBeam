/*
DreamBeam - a Church Song Presentation Program
Copyright (C) 2008 Gabriel Burca

This program is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation; either version 2
of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.

*/

using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;


namespace DreamBeam {

	#region Enums
	// These are used as array indices so they must be numbered sequentially, from 0
	public enum SongTextType {
		Title,
		Verse,
		Author,
		Key
	}

	public enum SongSearchType {
		/// <summary>The exact phrase must be present</summary>
		Phrase,
		/// <summary>All the words must be present, but not in any particular order</summary>
		Words
	}
	#endregion

	public enum LyricsType {
		Verse,
		Chorus,
		Other
	}

	/// <summary>
	/// LyricsSequenceItem (LSI) is like a pointer to a LyricsItem. The order in
	/// which these LSI objects are added to the Song.Sequence array
	/// determines the order in which the parts of a song are displayed when the
	/// user requests the "Next" slide.
	/// </summary>
	[Serializable()]
	public class LyricsSequenceItem {
		[XmlIgnore()]
		public static Language lang;
		[XmlAttribute]
		public LyricsType Type;
		[XmlAttribute]
		public int Number;

		public LyricsSequenceItem() { }
		public LyricsSequenceItem(LyricsType Type, int Number) {
			this.Type = Type;
			this.Number = Number;
		}
		public LyricsSequenceItem(LyricsSequenceItem item)
			:
			this(item.Type, item.Number) { }

		public override string ToString() {
			return lang.say("EditSongs.LyricType" +
				Enum.GetName(typeof(LyricsType), Type)) + " " + Number.ToString();
		}
	}

	/// <summary>
	/// A LyricsItem represents a verse or chorus. A song's LyricsItem(s) can be
	/// ordered by the user in any sequence desired by using the
	/// LyricsSequenceItem class.
	/// </summary>
	[Serializable()]
	public class LyricsItem : IComparable {
		[XmlAttribute]
		public LyricsType Type;
		/// <summary>The verse (chorus, other) number. One-based.</summary>
		[XmlAttribute]
		public int Number;
		[XmlText]
		public string Lyrics;

		public LyricsItem() { }
		public LyricsItem(LyricsType Type, int Number, string Lyrics) {
			this.Type = Type;
			this.Number = Number;
			this.Lyrics = Lyrics;
		}

		#region IComparable Members
		/// <summary>
		/// Implements the IComparable interface. Allows arrays of LyricsItem's to be sorted.
		/// </summary>
		/// <param name="obj">The type of this object can be another LyricsItem or a LyricsSequenceItem</param>
		/// <returns>-1, 0, or 1 if "this" is less, equal, or greater than "obj"</returns>
		public int CompareTo(object obj) {
			// Return -1 if "this" is less than "other/obj"
			if (obj is LyricsItem) {
				LyricsItem other = obj as LyricsItem;
				if (this.Type == other.Type) {
					return this.Number - other.Number;
				} else {
					return this.Type - other.Type;
				}
			} else {
				LyricsSequenceItem other = obj as LyricsSequenceItem;
				if (this.Type == other.Type) {
					return this.Number - other.Number;
				} else {
					return this.Type - other.Type;
				}
			}
		}
		#endregion
	}

	[Serializable()]
	[XmlRoot(ElementName = "DreamSong")]
	public class Song : Content, IContentOperations {
		// The following setting and properties will be serialized
		public string Version;
		public string Title;
		public string Author;
		public string Collection;
		public string Number;
		public string Notes;
		public string KeyRangeLow, KeyRangeHigh;
		public bool MinorKey;
		public bool DualLanguage;

		[XmlArrayItem(ElementName = "LyricsItem", Type = typeof(LyricsItem))]
		[XmlArray]
		public ArrayList SongLyrics;

		[XmlArrayItem(ElementName = "LyricsSequenceItem", Type = typeof(LyricsSequenceItem))]
		[XmlArray]
		public ArrayList Sequence;


		// The following settings will not be saved when we serialize this class
		private string fileName;
		[NonSerializedAttribute]
		private Thread render;
		private Object renderLock = new Object();
		[XmlIgnore]
		public OldSong song;
		[XmlIgnore]
		public Config config;
		[XmlIgnore]
		protected System.Type enumType;
		/// <summary>Zero based index of the current lyric (used to index this.Sequence)</summary>
		[XmlIgnore]
		public int CurrentLyric;
		/// <summary>We can hide/display each of the SongTextType items individually</summary>
		[XmlIgnore]
		public bool[] Hide = new bool[Enum.GetValues(typeof(SongTextType)).Length];
		[XmlIgnore]
		public string FileName {
			get { return fileName; }
			set { fileName = value; }
		}
		[XmlIgnore]
		public string FullName {
			get {
				if (!Tools.StringIsNullOrEmptyTrim(this.Number)) {
					return this.Number + ". " + this.Title;
				} else {
					return this.Title;
				}
			}
		}

		#region Constructors
		public Song(Config conf, OldSong s) : this(conf) {
			this.Title = s.GetText(0);
			this.FileName = s.SongName;
			this.Author = s.GetText(2);
			this.BGImagePath = s.bg_image;
			this.SetLyrics(LyricsType.Verse, s.GetText(1));
			this.DualLanguage = s.MultiLang;
			this.Collection = "Version 0.60 Format";

			CreateSimpleSequence();
		}

		public Song() {
			this.enumType = typeof(SongTextType);
			this.SongLyrics = new ArrayList();
			this.Sequence = new ArrayList();
			this.CurrentLyric = 0;
			this.Version = Tools.GetAppVersion();
		}

		public Song(Config config) : this() {
			if (config != null && config.theme != null) {
				Theme = config.theme.Song;
				// This is not a custom theme, so null the ThemePath so it doesn't get saved.
				Theme.ThemeFile = null;
			}
		}

		/// <summary>
		/// This constructor creates a new song out of a plain text file. The file must contain a single
		/// song in the following format:
		/// ===
		/// Number. OldSong title (key)
		/// 
		/// verse 1:
		/// Line 1 of verse 1
		/// Line 2 of verse 1
		/// 
		/// verse 2:
		/// Line 1 of verse 2
		///	Line 2 of verse 2
		///	
		///	chorus 1:
		///	Line 1 of chorus 1 ...
		///	===
		///	
		///	This constructor is mainly used when importing songs stored in this text format.
		/// </summary>
		/// <param name="fileName"></param>
		public Song(string fileName) : this() {
			// If BOM is present, read the file as Unicode, else default to UTF-8
			string[] lines;
			Regex r;
			Match m;
			LyricsItem currItem = null;

			using (TextReader input = new StreamReader(fileName, true)) {
				lines = input.ReadToEnd().Split('\n');
			}

			foreach (string rawLine in lines) {
				string line = rawLine.TrimEnd();
				if (Regex.IsMatch(line, @"\s*#")) {
					this.Notes += Regex.Replace(line, @"\s*#\s?(.*)", @"$1");
					continue;
				}

				r = new Regex(@"\s*(verse|chorus)\s+(\d+):", RegexOptions.IgnoreCase);
				m = r.Match(line);

				if (m.Success && m.Groups.Count == 3) {
					// This is the beginning of a verse or chorus
					if (currItem != null) this.SongLyrics.Add(currItem);
					if (m.Groups[1].Value.ToLower() == "verse") {
						currItem = new LyricsItem(LyricsType.Verse, Convert.ToInt16(m.Groups[2].Value), "");
					} else if (m.Groups[1].Value.ToLower() == "chorus") {
						currItem = new LyricsItem(LyricsType.Chorus, Convert.ToInt16(m.Groups[2].Value), "");
					} else {
						currItem = new LyricsItem(LyricsType.Other, Convert.ToInt16(m.Groups[2].Value), "");
					}
				} else if (Regex.IsMatch(line, @"\w+")) {
					if (currItem != null) {
						currItem.Lyrics += line + "\n";
					} else if (this.Title == null) {
						// Look for title of the form: "23. Title, words! (optional key)"
						r = new Regex(@"(\d+)[\.]*\s+(.+)");
						m = r.Match(line);

						if (m.Success && m.Groups.Count == 3) {
							this.Number = m.Groups[1].Value;
							this.Title = m.Groups[2].Value.Trim();

							// If we have a key in the Title, remove it and handle it separately
							// The key looks like: Ab, or F#, or Bbm
							r = new Regex(@"(.+)\s+\(([a-gA-G#]+)\)");
							m = r.Match(this.Title);
							if (m.Success && m.Groups.Count == 3) {
								this.Title = m.Groups[1].Value.Trim();
								string key = m.Groups[2].Value;
								int minor = key.IndexOf("m");
								if (minor > 0) {
									key = key.Substring(0, minor);
									this.MinorKey = true;
								}
								this.KeyRangeHigh = key;
								this.KeyRangeLow = key;
							}
						} else {
							this.Title = line;
						}
					}
				}
			}

			if (currItem != null) this.SongLyrics.Add(currItem);

			// We end up with an extra "\n" at the end of each verse. Trim them off.
			foreach (LyricsItem item in this.SongLyrics) {
				item.Lyrics = item.Lyrics.TrimEnd();
			}

			this.CreateSimpleSequence();

			if (this.SongLyrics.Count > 0) {
				this.FileName = Regex.Replace(fileName, ".txt$", ".xml", RegexOptions.IgnoreCase);
			}
		}
		#endregion

		#region Various support methods
		/// <summary>
		/// Attempts to auto-create an inteligent sequence of verse/chorus lyrics such as
		/// v1,c1,v2,c1 or v1,c1,v2,c2 depending on how many of each lyric type there are.
		/// </summary>
		public void CreateSimpleSequence() {
			ArrayList verses = new ArrayList();
			ArrayList choruses = new ArrayList();
			ArrayList other = new ArrayList();

			this.Sequence.Clear();
			this.SongLyrics.Sort();
			foreach (LyricsItem item in this.SongLyrics) {
				switch (item.Type) {
					case LyricsType.Verse: verses.Add(item); break;
					case LyricsType.Chorus: choruses.Add(item); break;
					case LyricsType.Other: other.Add(item); break;
				}
			}

			if (verses.Count == choruses.Count) {
				// Each verse has its own chorus. Create v1, c1, v2, c2, ... sequence
				for (int i = 0; i < verses.Count; i++) {
					LyricsItem verse = (LyricsItem)verses[i];
					LyricsItem chorus = (LyricsItem)choruses[i];
					this.Sequence.Add(new LyricsSequenceItem(verse.Type, verse.Number));
					this.Sequence.Add(new LyricsSequenceItem(chorus.Type, chorus.Number));
				}
			} else if (choruses.Count == 1) {
				// Only 1 chorus present. Create v1, c1, v2, c1, ... sequence
				LyricsItem chorus = (LyricsItem)choruses[0];
				foreach (LyricsItem item in verses) {
					this.Sequence.Add(new LyricsSequenceItem(item.Type, item.Number));
					this.Sequence.Add(new LyricsSequenceItem(chorus.Type, chorus.Number));
				}
			} else {
				// We have some strange combination of verses and choirs. Just add them all sequentially.
				foreach (LyricsItem item in verses) {
					this.Sequence.Add(new LyricsSequenceItem(item.Type, item.Number));
				}
				foreach (LyricsItem item in choruses) {
					this.Sequence.Add(new LyricsSequenceItem(item.Type, item.Number));
				}
			}

			// Add the "other" lyric types all at the end.
			foreach (LyricsItem item in other) {
				this.Sequence.Add(new LyricsSequenceItem(item.Type, item.Number));
			}
		}

		public LyricsItem GetLyrics(int seq) {
			if (seq >= this.Sequence.Count) return null;
			LyricsSequenceItem current = (LyricsSequenceItem)this.Sequence[seq];
			foreach (LyricsItem item in this.SongLyrics) {
				if (item.CompareTo(current) == 0) return item;
			}
			return null;
		}

		/// <summary>
		/// Replaces all existing lyrics of the type with new ones obtained by
		/// breaking the (user) provided text at blank lines and creating
		/// verses/choruses/etc... out of it
		/// </summary>
		/// <param name="type"></param>
		/// <param name="text"></param>
		public void SetLyrics(LyricsType type, string text) {
			ArrayList toRemove = new ArrayList();
			foreach (LyricsItem item in this.SongLyrics) {
				if (item.Type == type) toRemove.Add(item);
			}
			foreach (LyricsItem item in toRemove) {
				this.SongLyrics.Remove(item);
			}

			int num = 1;
			foreach (string v in Regex.Split(text, "\n\n")) {
				string l = v.Trim();
				if (l.Length == 0) continue;	// Don't add blank verses
				this.SongLyrics.Add(new LyricsItem(type, num++, l));
			}
		}

		/// <summary>
		/// Returns a string containing all the verses (or choruses, or other),
		/// the way a user would have typed them in.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public string GetLyrics(LyricsType type) {
			StringBuilder s = new StringBuilder("");

			this.SongLyrics.Sort();
			foreach (LyricsItem item in this.SongLyrics) {
				if (item.Type == type) s.Append(item.Lyrics + "\n\n");
			}

			return SanitizeLyrics(s.ToString());
		}

		/// <summary>
		/// Returns the words of a single verse/chorus.
		/// </summary>
		/// <param name="Item"></param>
		/// <returns></returns>
		public string GetLyrics(LyricsSequenceItem Item) {
			foreach (LyricsItem l in this.SongLyrics) {
				if (l.CompareTo(Item) == 0) return SanitizeLyrics(l.Lyrics);
			}
			return "";
		}

		/// <summary>
		/// The RichTextBox control does not show non-breaking hyphens
		/// properly, even though they show up just fine on the projector.
		/// We need to replace them with regular hyphens. 
		/// See also: http://thedotnet.com/nntp/404109/showpost.aspx and
		/// http://www.dotnet247.com/247reference/msgs/58/291787.aspx
		/// </summary>
		/// <param name="lyrics"></param>
		/// <returns></returns>
		public string SanitizeLyrics(string lyrics) {
			return lyrics;
			//return Regex.Replace(lyrics, "\u00ad", "-");
		}

		/// <summary>
		/// Searches through all the text of a song (title, notes, verses) for a phrase or a collection of words.
		/// </summary>
		/// <param name="Search">Indicates how the "Text" argument should be treated</param>
		/// <param name="Text">The phrase or words to search for</param>
		/// <returns></returns>
		public bool ContainsText(SongSearchType Search, string Text) {
			StringBuilder s = new StringBuilder("");

			s.Append(Tools.StringIsNullOrEmpty(this.Title) ? " " : this.Title + " ");
			s.Append(Tools.StringIsNullOrEmpty(this.Notes) ? " " : this.Notes + " ");
			foreach (LyricsType type in Enum.GetValues(typeof(LyricsType))) {
				s.Append(this.GetLyrics(type));
			}

			string fullText = s.ToString().ToLower();
			Text = Regex.Replace(Text, @"[\s\n]+", " ").ToLower();	// Remove multiple spaces, returns, etc...

			switch (Search) {
				case SongSearchType.Phrase:
					return fullText.IndexOf(Text) >= 0;
				case SongSearchType.Words:
					foreach (string word in Text.Split(' ')) {
						if (fullText.IndexOf(word) < 0) return false;
					}
					return true;
			}

			return false;
		}

		public string GetKey() {
			String low = NormalizeKey(KeyRangeLow);
			String high = NormalizeKey(KeyRangeHigh);

			String key;
			if (low.Length > 0 && high.Length > 0) {
				key = low + " - " + high;
			} else if (low.Length > 0) {
				key = low;
			} else {
				key = high;
			}

			return key + (this.MinorKey ? "m" : "");
		}

		/// <summary>
		/// Returns a normalized textual song key (converts "D# / Eb" to Eb) for
		/// older song versions:
		///   In version 0.7x, the key was saved as "D# / Eb".
		///   In version 0.8x there is a separate emtry for D# and Eb.
		/// </summary>
		/// <returns></returns>
		public static string NormalizeKey(string keyStr) {
			if (keyStr == null) { return ""; }

			if (keyStr.Contains(@"/")) {
				string key = keyStr.Split('/')[0];
				key = key.Trim();

				switch (key) {
					case "C#": key = "Db"; break;
					case "D#": key = "Eb"; break;
					case "G#": key = "Ab"; break;
					case "A#": key = "Bb"; break;
				}
				return key;
			} else {
				return keyStr;
			}
		}
		#endregion

		/// <summary>
		/// The boolean properties are converted to a string so that we get a
		/// different hash code if they change. Hide.GetHashCode() returns the
		/// same number regardless of what the array elements contain.
		/// </summary>
		/// <returns>A hash code representing the current verse</returns>
		public int VisibleHashCode(int seq) {
			int hash = 0;
			string h = "Hide";
			foreach (bool b in Hide) {
				h += (b ? "1" : "0");
			}

			hash += h.GetHashCode() + base.VisibleHashCode();
			if (this.GetLyrics(seq) != null) {
				hash += this.GetLyrics(seq).Lyrics.GetHashCode();
			}

			return hash;
		}

		#region IContentOperations Members
		/// <summary>
		/// Gets the bitmap for the CurrentLyric
		/// </summary>
		/// <param name="Width"></param>
		/// <param name="Height"></param>
		/// <returns></returns>
		public Bitmap GetBitmap(int Width, int Height) {
			return this.GetBitmap(Width, Height, this.CurrentLyric);
		}

		public Bitmap GetBitmap(int Width, int Height, int seq) {
			if (this.RenderedFramesContains(this.VisibleHashCode(seq))) {
				Console.WriteLine("Found pre-rendered frame with: {0}", this.VisibleHashCode(seq));
				return this.RenderedFramesGet(this.VisibleHashCode(seq)) as Bitmap;
			} else {
				Console.WriteLine("Rendering song frame for {0}", this.VisibleHashCode(seq));
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
				string[] text = new string[Enum.GetValues(typeof(SongTextType)).Length];

				graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
				graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
				graphics.SmoothingMode = SmoothingMode.AntiAlias;
				graphics.SmoothingMode = SmoothingMode.HighQuality;
				graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

				this.RenderBGImage(this.BGImagePath, graphics, Width, Height);

				if (HideText) {
					graphics.Dispose();
					return bmp;
				}

				LyricsItem lyrics = this.GetLyrics(seq);
				text[(int)SongTextType.Title] = (this.Title == null) ? "" : this.Title;
				text[(int)SongTextType.Verse] = (lyrics == null) ? "" : lyrics.Lyrics;
				text[(int)SongTextType.Author] = (this.Author == null) ? "" : this.Author;
				text[(int)SongTextType.Key] = this.GetKey();

				pth = new GraphicsPath();

				foreach (int type in Enum.GetValues(this.enumType)) {
					if (this.Hide[type]) continue;
					// We have to keep the text within these user-specified boundaries
					bounds = new System.Drawing.Rectangle(
						(int)(format[type].Bounds.X / 100 * Width),
						(int)(format[type].Bounds.Y / 100 * Height),
						(int)(format[type].Bounds.Width / 100 * Width),
						(int)(format[type].Bounds.Height / 100 * Height));

					p = new Pen(format[type].TextColor, 1.0F);
					if (showRectangles == true) graphics.DrawRectangle(p, bounds);

					if (text[type].Length > 0) {
						p.LineJoin = LineJoin.Round;
						StringFormat sf = new StringFormat();
						sf.Alignment = format[type].HAlignment;
						sf.LineAlignment = format[type].VAlignment;

						if (this.WordWrap) {
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

						} else {
							// Tab characters are ignored by AddString below. Converting them to spaces.
							text[type] = Regex.Replace(text[type], "\t", "        ");
							pth = new GraphicsPath();
							font = new Font(format[type].TextFont.FontFamily, format[type].TextFont.Size, format[type].TextFont.Style);
							pth.AddString(text[type], font.FontFamily, (int)font.Style, font.Size, new Point(0, 0), sf);

							pth.Transform(Tools.FitContents(bounds, pth.GetBounds(), sf));
						}

						brush = new SolidBrush(format[type].TextColor);

						#region Add special effects
						if (format[type].Effects == "Outline") {
							p = new Pen(format[type].OutlineColor, format[type].OutlineSize);
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
					this.RenderedFramesSet(this.VisibleHashCode(seq), bmp);
				}

				return bmp;
			}
		}

		public bool Next() {
			if (this.CurrentLyric < this.Sequence.Count - 1) {
				this.CurrentLyric++;
				return true;
			}
			return false;
		}

		public bool Prev() {
			if (this.CurrentLyric > 0) {
				this.CurrentLyric--;
				return true;
			}
			return false;
		}

		public bool ShowRectangles {
			get { return showRectangles; }
			set { showRectangles = value; }
		}

		public virtual ContentIdentity GetIdentity() {
			ContentIdentity ident = new ContentIdentity();
			ident.Type = (int)ContentType.Song;
			ident.SongName = Path.GetFileName(this.FileName);
			ident.SongStrophe = this.CurrentLyric;
			return ident;
		}

		private void PreRenderFramesThreaded() {
			if (this.config != null && this.config.BeamBoxSizeX > 1 && this.config.BeamBoxSizeY > 1) {
				for (int i = 0; i < this.Sequence.Count; i++) {
					this.GetBitmap(this.config.BeamBoxSizeX, this.config.BeamBoxSizeY, i);
				}
			}
		}

		public void PreRenderFrames() {
			if (render != null && render.IsAlive) {
				render.Abort();
			}

			render = new Thread(new ThreadStart(PreRenderFramesThreaded));
			render.IsBackground = true;
			render.Name = "PreRender Song";
			render.Start();
		}

		public virtual void DefaultBackground(Config conf) {
			this.BGImagePath = conf.theme.Song.BGImagePath;
		}
		#endregion

		#region ICloneable Members

		object ICloneable.Clone() {
			return this.Clone();
		}

		public virtual Song Clone() {
			// Make a proper clone, or else the Live screen will show updates made to preview screen.
			return DeepClone();
		}

		#endregion


		/// Returns a deep-copy clone of the object.
		public virtual Song DeepClone() {
			MemoryStream ms = new MemoryStream();
			BinaryFormatter bf = new BinaryFormatter();
			bf.Serialize(ms, this);
			ms.Position = 0;
			object clone = bf.Deserialize(ms);
			ms.Close();
			return (Song)clone;
		}


		#region Serialization and Deserialization
		/// <summary>
		/// Handles serialization of the Config class, or of any types derived from it.
		/// </summary>
		/// <param name="instance">The instance to serialize</param>
		/// <param name="file">The XML file to serialize to</param>
		public static void SerializeTo(Song instance, string file) {
			// We need to save these and restore them after serializing, or else whoever is holding
			// a reference to this "instance" will end up with a broken object because we set these
			// to "null" below so that we don't serialize them.
			string savedBGImagePath = instance.BGImagePath;

			Type type = instance.GetType();
			XmlSerializer xs = new XmlSerializer(type);

			Directory.CreateDirectory(Path.GetDirectoryName(file));
			FileStream fs = null;

			// When files are de-serialized, they retain the version they were
			// originally serialized under. We need to update it here.
			instance.Version = Tools.GetAppVersion();

			try {
				fs = File.Open(file, FileMode.Create, FileAccess.Write, FileShare.Read);
				xs.Serialize(fs, instance);
			} finally {
				if (fs != null) {
					fs.Close();
				}
				instance.BGImagePath = savedBGImagePath;
			}
		}

		/// <summary>
		/// Handles deserialization of the Song class, or of any types derived from it.
		/// </summary>
		/// <param name="type">The type of class to deserialize. Allows this function to be used in derived classes.</param>
		/// <param name="tr">A TextReader containing the document to deserialize</param>
		/// <returns></returns>
		public static object DeserializeFrom(System.Type type, TextReader tr) {
			XmlSerializer xs = null;

			try {
				xs = new XmlSerializer(type);
			} catch (InvalidOperationException ex) {
				// Invalid class. Does the class have a public constructor?
				Console.WriteLine("DeserializeFrom exception: " + ex.Message);
			}

			if (xs != null) {
				try {
					return xs.Deserialize(tr);
				} catch (InvalidOperationException e) {
					Console.WriteLine("Exception deserializing (Invalid XML or old format): " + e.Message);
				}
			}

			return null;
		}

		/// <summary>
		/// Handles deserialization of the Song class, or of any types derived from it.
		/// </summary>
		/// <param name="type">The type of class to deserialize. Allows this function to be used in derived classes.</param>
		/// <param name="file">The full path to the XML file to deserialize</param>
		/// <returns></returns>
		public static object DeserializeFrom(System.Type type, string file) {
			try {
				using (FileStream fs = File.Open(file, FileMode.Open, FileAccess.Read)) {
					return DeserializeFrom(type, new StreamReader(fs));
				}
			} catch (FileNotFoundException e) {
				Console.WriteLine("Exception deserializing (FileNotFound): " + e.Message);
			}
			return null;
		}

		public static object DeserializeFrom(string file, int strophe, Config config) {
			XmlDocument xmlDoc = new XmlDocument();
			try {
				xmlDoc.Load(file);
			} catch {
				return new Song(config);
			}

			Song s = null;
			//XmlNodeList nodes = xmlDoc.GetElementsByTagName("Version");
			XmlNode versionNode = xmlDoc.SelectSingleNode(@"/DreamSong/Version");
			if (versionNode == null) {
				versionNode = xmlDoc.SelectSingleNode(@"/NewSong/Version");
			}
			if (versionNode != null) {
				float version;
				try {
					version = float.Parse(versionNode.InnerText);
				} catch {
					version = 0;
				}

				if (version <= 0.49F) {
					OldSong oldS = new OldSong(file);
					if (oldS.strophe_count > 0) {
						s = new Song(config, oldS);
					} else {
						s = new Song(config);
					}
				} else if (version <= 0.71F) {
					/* Prior to version 0.72 old songs were saved with NewSong XML root
					 * instead of DreamSong and contained an node called BGImagePath. The
					 * BGImagePath has now been generalized to ThemePath.
					 */
					XmlNode root = xmlDoc.DocumentElement;
					if (root.Name.Equals("NewSong")) {
						Tools.RenameXmlNode(root, root.NamespaceURI, "DreamSong");
						XmlNode bgImageNode = xmlDoc.SelectSingleNode(@"/DreamSong/BGImagePath");
						if (bgImageNode != null) {
							Tools.RenameXmlNode(bgImageNode, root.NamespaceURI, "ThemePath");
						}
						s = (Song)Song.DeserializeFrom(typeof(Song), new StringReader(xmlDoc.OuterXml));
					} else {
						s = (Song)Song.DeserializeFrom(typeof(Song), file);
					}
					if (s != null) {
						// Version 0.71 allowed the user to save files with keys such as "D# / Eb".
						s.KeyRangeLow = NormalizeKey(s.KeyRangeLow);
						s.KeyRangeHigh = NormalizeKey(s.KeyRangeHigh);
					}
				} else { // version >= 0.72
					s = (Song)Song.DeserializeFrom(typeof(Song), file);
				}
			}

			if (s != null) {
				if (s.Theme == null) {
					// OldSong did not have a custom theme. Give it the default theme.
					// If we don't clone the theme, background changes will be saved as part of the default theme.
					s.Theme = (Theme)config.theme.Song.Clone();
					// Hide the theme path so it doesn't look like the song has a custom theme.
					s.Theme.ThemeFile = null;
				}
				s.config = config;
				s.CurrentLyric = strophe;
				s.FileName = file;
				return s;
			} else {
				return new Song(config);
			}
		}
		#endregion

	}

}
