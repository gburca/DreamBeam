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

	public interface IWordWrap {
		string[] Wrap(string document);
		GraphicsPath GetPath(string doc, Font font, Rectangle bounds);
	}

	public class WordWrapNone /*: IWordWrap*/ {
		public string[] Wrap(string doc) {
			return Regex.Split(doc, "[\r\n]+");
			//return doc.Split(new char[] { '\n', '\r' });

		}

		static string[] GetWords(string doc) {
			return doc.Split(' ');
		}

		public static RectangleF measure(String s, Font f) {
			GraphicsPath p = new GraphicsPath();
			p.AddString(s, f.FontFamily, (int)f.Style, f.Size, new Point(), new StringFormat());
			return p.GetBounds();
		}

		public static void measure(Line line, Font f) {
			line.size = measure(line.words, f).Size;
		}

		public static GraphicsPath GetPath(string doc, Graphics g, Font font, StringFormat sf, Rectangle bounds) {
			GraphicsPath pth = new GraphicsPath();
			if (doc == null || doc.Length == 0) return pth;

			String[] w = GetWords(doc);
			if (w.Length == 0) return pth;

			float lineH;
			SizeF size;
			String test, fullLine;
			List<Line> lines = new List<Line>();

			do {
				lineH = font.GetHeight(g);
				int start = 0;
				int count = 1;
				lines.Clear();

				while ((start + count + 1 <= w.Length) && 
					(lineH * lines.Count <= bounds.Height)) {

					test = String.Join(" ", w, start, count + 1);
					size = measure(test, font).Size;

					if (size.Width <= bounds.Width) {
						count++;
					} else {
						fullLine = String.Join(" ", w, start, count);
						size = measure(fullLine, font).Size;
						lines.Add(new Line(fullLine, size));
						start += count;
						count = 1;
					}
				}

				if (true) {
					fullLine = String.Join(" ", w, start, count);
					size = measure(fullLine, font).Size;
					lines.Add(new Line(fullLine, size));
				}

				if (lineH * lines.Count > bounds.Height) {
					font = new Font(font.FontFamily, font.Size - 1, font.Style);
				}
			} while (font.Size > 0 && lineH * lines.Count > bounds.Height);


			StringFormat strF = new StringFormat();
			strF.Alignment = StringAlignment.Near;
			strF.LineAlignment = StringAlignment.Near;
			PointF p = new PointF();

			p.Y = 0;
			foreach (Line l in lines) {
				switch (sf.LineAlignment) {
					case StringAlignment.Near:
						p.X = 0;
						break;
					case StringAlignment.Center:
						p.X = (bounds.Width - l.size.Width) / 2;
						break;
					case StringAlignment.Far:
						p.X = bounds.Width - l.size.Width;
						break;
				}

				pth.AddString(l.words, font.FontFamily, (int)font.Style, font.Size, p, strF);
				p.Y += lineH;
			}

			return pth;
		}


		public class Line {
			public String words = "";
			public SizeF size;
			public Line(String words, SizeF size) {
				this.words = words;
				this.size = size;
			}
		}
	}


}
