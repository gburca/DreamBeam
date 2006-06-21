using System;

namespace DreamBeam.FileTypes
{

	#region Enums
	public enum TextToolType {
		FirstLine,
		OtherLines
	}
	#endregion

	/// <summary>
	/// Summary description for TextTool.
	/// </summary>
	public class TextToolContents : NewSong
	{

		public TextToolContents(string fullText, Config config) {
			this.enumType = typeof(TextToolType);
			this.config = config;
			this.format = config.SermonTextFormat;
			this.BGImagePath = config.TextBGImagePath;
			this.WordWrap = true;

			int lineBreak = fullText.IndexOf("\n\n");

			if (lineBreak > 1) {
				this.Title = fullText.Substring(0, lineBreak);
				lineBreak += 2;
				this.SetLyrics(LyricsType.Verse, fullText.Substring(lineBreak, fullText.Length - lineBreak));
			} else {
				this.Title = "";
				this.SetLyrics(LyricsType.Verse, fullText);
			}

			for (int num = 1; num <= this.SongLyrics.Count; num++) {
				this.Sequence.Add(new LyricsSequenceItem(LyricsType.Verse, num));
			}
		}

		#region IContentOperations Members

//		public System.Drawing.Bitmap GetBitmap(int width, int height) {
//			// TODO:  Add TextTool.GetBitmap implementation
//			return null;
//		}
//
//		public void Next() {
//			// TODO:  Add TextTool.Next implementation
//		}
//
//		public void Prev() {
//			// TODO:  Add TextTool.Prev implementation
//		}
//
//		public void ChangeBGImagePath(string newPath) {
//			// TODO:  Add TextTool.ChangeBGImagePath implementation
//		}
//
//		public string GetIdentity() {
//			// TODO:  Add TextTool.GetIdentity implementation
//			return null;
//		}

		public override ContentIdentity GetIdentity() {
			ContentIdentity ident = new ContentIdentity();
			ident.Type = (int)ContentType.PlainText;
			ident.Text = this.Title + "\n\n" + this.GetLyrics(LyricsType.Verse);
			ident.SongStrophe = this.CurrentLyric;
			return ident;
		}

		#endregion

		#region ICloneable Members

//		public object Clone() {
//			// TODO:  Add TextTool.Clone implementation
//			return null;
//		}

		#endregion
	}
}
