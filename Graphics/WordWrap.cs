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
using System.Text.RegularExpressions;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Collections.Generic;
using System.Collections;

namespace DreamBeam {

	/// <summary>
	/// An interface implemented by word-wrap (line-break) algorithms
	/// </summary>
	public interface IWordWrap {
		GraphicsPath GetPath(string doc, Font font, StringFormat sf, RectangleF bounds);
	}

	/// <summary>
	/// The default word-wrapping used by GraphicsPath. This results in rather poor
	/// word wrapping, because it ignores (non-breaking) hyphens, can insert a break
	/// between a word and the closing or opening quote, etc...
	/// </summary>
	public class WordWrapDefault : IWordWrap {

		public GraphicsPath GetPath(string doc, Font font, StringFormat sf, RectangleF bounds) {
			GraphicsPath pth = new GraphicsPath();
			RectangleF measuredBounds, pathRect;
			Font pthFont;

			// Make a rectangle that is very tall to see how far down the text would stretch.
			pathRect = bounds;
			pathRect.Height *= 2;

			// We start with the user-specified font size ...
			float fontSz = font.Size;

			// ... and decrease the size (if needed) until it fits within the user-specified boundaries
			do {
				pthFont = new Font(font.FontFamily, fontSz, font.Style);
				pth.Reset();
				pth.AddString(doc, font.FontFamily, (int)font.Style, font.Size, pathRect, sf);
				measuredBounds = pth.GetBounds();
				fontSz--;
				if (fontSz == 0) break;
			} while (measuredBounds.Height > bounds.Height);

			// We need to re-create the path. For some reason the do-while loop puts it in the wrong place.
			pth.Reset();
			pth.AddString(doc, font.FontFamily, (int)font.Style, font.Size, bounds, sf);

			return pth;
		}
	}

	/// <summary>
	/// An improved word-wrap algorithm that only allows line-breaks where spaces are located.
	/// Further enhancements could include hyphenation support.
	/// </summary>
	public class WordWrapAtSpace : IWordWrap {

		string[] GetWords(string doc) {
			return doc.Split(' ');
		}

		public GraphicsPath GetPath(string doc, Font f, StringFormat sf, RectangleF bounds) {

			// For debug. Make sure these all work with top/center/bottom alignment
			//doc = "mare era sarea asa ca marea care era masea ca a awuao vara";
			//doc = "T mare era sarea asa ca marea care era masea ca a awuao vara";
			//doc = "mare era sarea asa ca marea care era masea ca a awuao varap";
			//doc = "Tmare era sarea asa ca marea care era masea ca a awuao varap";

			GraphicsPath path = null;
			while (true) {
				path = getPath(doc, f, sf, bounds);

				if (path != null) {
					return path;
				} else {
					if (f.Size > 1) {
						f = new Font(f.FontFamily, f.Size - 1, f.Style);
					} else {
						return new GraphicsPath();
					}
				}
			}
		}

		private GraphicsPath getPath(string doc, Font font, StringFormat sf, RectangleF bounds) {
			GraphicsPath pth = new GraphicsPath();
			if (doc == null || doc.Length == 0) return pth;

			String[] w = GetWords(doc);
			if (w.Length == 0) return pth;

			float lineH = font.GetHeight() * 0.7F;
			if (lineH > bounds.Height) return null;

			List<Line> lines = new List<Line>();
			Line line = new Line();
			lines.Add(line);

			for (int i = 0; i < w.Length; ) {
				if (line.add(w[i], font, bounds.Width)) {
					// Word was successfully added
					i++;

					// Check the height after every word to make sure descenders (gjpqy)
					// don't cause text to be too tall
					float ht = (lines.Count - 1) * lineH;
					ht += line.rect.Height;
					if (ht > bounds.Height) {
						return null;
					}
				} else {
					// Word did not fit on the previous line
					if (line.words.Length == 0) {
						// Font is too big to add this word even by itself
						// i.e. bounds width would be exceeded
						return null;
					} else {
						line = new Line();
						lines.Add(line);
					}
				}
			}

			StringFormat strF = new StringFormat();
			strF.Alignment = StringAlignment.Near;
			strF.LineAlignment = StringAlignment.Near;
			PointF p = new PointF();

			p.Y = 0;
			float y = 0;

			foreach (Line l in lines) {
				switch (sf.Alignment) { // Left, Center, Right
					case StringAlignment.Near:
						p.X = 0;
						break;
					case StringAlignment.Center:
						p.X = (bounds.Width - l.rect.Width) / 2;
						break;
					case StringAlignment.Far:
						p.X = bounds.Width - l.rect.Width;
						break;
				}

				p.Y = y;
				p.X += bounds.X - l.rect.X;
				p.Y += bounds.Y - lines[0].rect.Y;
				// For p.Y use only line[0] to maintain consistent line spacing, or else line spacing
				// will change if there are no descenders, or no ascencers, or both, on the current line.
				//
				// In other words, each line is (lineH * line#) units below line[0].
				// Only line# should change as we iterate through all the lines.

				pth.AddString(l.words, font.FontFamily, (int)font.Style, font.Size, p, strF);

				// For Debug...
				//RectangleF lrect = l.rect;
				//lrect.Offset(p);
				//pth.AddRectangle(lrect);

				y += lineH;
			}

			RectangleF pathBounds = pth.GetBounds();
			Matrix matrix = new Matrix();

			switch (sf.LineAlignment) { // Top, Center, Bottom
				case StringAlignment.Near:
					// Nothing to do
					break;
				case StringAlignment.Center:
					matrix.Translate(0, (bounds.Height - pathBounds.Height) / 2);
					pth.Transform(matrix);
					break;
				case StringAlignment.Far:
					matrix.Translate(0, bounds.Height - pathBounds.Height);
					pth.Transform(matrix);
					break;
			}

			return pth;
		}

		public class Line {
			public String words = "";
			public RectangleF rect = new RectangleF();

			public RectangleF measure(String s, Font f) {
				GraphicsPath p = new GraphicsPath();
				p.AddString(s, f.FontFamily, (int)f.Style, f.Size, new Point(), new StringFormat());
				return p.GetBounds();
			}

			public bool add(String word, Font f, float maxWidth) {
				RectangleF newBounds;
				String newWords;

				if (words.Length == 0) {
					newWords = word;
				} else {
					newWords = words + " " + word;
				}

				newBounds = measure(newWords, f);
				if (newBounds.Width <= maxWidth) {
					words = newWords;
					rect = newBounds;
					return true;
				} else {
					return false;
				}
			}
		}
	}


}
