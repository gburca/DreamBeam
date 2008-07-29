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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using DreamBeam;
using DreamBeam.FileTypes;
using System.Collections;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using CookComputing.XmlRpc;
using System.Xml.Serialization;

namespace DreamBeam {

	/// <summary>
	/// Each Song, BibleVerse, or SermonText piece of content needs to know how
	/// to draw itself (possibly caching and/or prerendering). It also needs to
	/// know how to switch to the next/previous strophe, verse, sermon tab. We
	/// inherit from ICloneable so that we can pass from a Preview display to a
	/// Live display a clone that is independent.
	/// </summary>
	public interface IContentOperations : ICloneable {
		Bitmap GetBitmap(int Width, int Height);
		bool Next();
		bool Prev();
		Theme Theme {
			get;
			set;
		}
		string BGImagePath {
			get;
			set;
		}
		void DefaultBackground(Config config);

		bool ShowRectangles {
			get;
			set;
		}
		/// <summary>
		/// When asking a remote display to show a piece of content, we don't
		/// want to send the actual content across. Instead we send just enough
		/// information for the remote server to be able to identify the content
		/// and display it.
		/// </summary>
		/// <returns>Information required to uniquely identify the content.</returns>
		ContentIdentity GetIdentity();

		void PreRenderFrames();
	}


	/// <summary>
	/// All content (Songs, BibleVerses, SermonText) has some attributes in
	/// common. They all derive from this class. TODO: The ICloneable should
	/// probably move here, and this base class should implement Clone() itself
	/// also.
	/// </summary>
	[Serializable()]
	public class Content {
		// The following settings will not be saved when we serialize this class
		[XmlIgnore()]
		private Theme theme;
		[XmlIgnore()]
		[NonSerializedAttribute]	// If this is removed, make sure double-click on a song works
		public Image bgImage;
		[XmlIgnore()]
		public bool HideBG = false;
		[XmlIgnore()]
		public bool HideText = false;
		[XmlIgnore()]
		protected bool showRectangles = false;

		/// <summary>The maximum number of pre-rendered frames to retain.</summary>
		[XmlIgnore()]
		public int maxFrames = 10;
		[XmlIgnore()]
		private Hashtable renderedFrames = new Hashtable();
		[XmlIgnore()]
		private ArrayList renderedFramesOrder = new ArrayList(10);
		public bool WordWrap = false;	// Bible text is word-wrapped. Songs are not.

		public virtual string ThemePath {
			get {
				if (theme == null) {
					return null;
				} else {
					// If the theme comes from the default config, it won't have a path
					// That's how it should be!
					return theme.ThemeFile;
				}
			}
			set {
				// Assume it's a song theme.
				// bgImage is no longer valid when the theme is changed
				bgImage = null;
				if (Path.GetExtension(value).EndsWith("xml")) {
					this.Theme = (Theme)Theme.DeserializeFrom(typeof(SongTheme), value);
				} else {
					// This is an image file from the old song file
					//bgImagePath = value;
				}
			}
		}

		[XmlIgnore()]
		public Theme Theme {
			get { return theme; }
			set {
				if (value != null) {
					this.theme = (Theme)value.Clone();
					BGImagePath = theme.BGImagePath;
				} else {
					this.theme = null;
					BGImagePath = null;
				}
			}
		}

		[XmlIgnore()]
		public string BGImagePath {
			get { return theme == null ? null : theme.BGImagePath; }
			set {
				bgImage = null;
				if (theme != null) {
					theme.BGImagePath = value;
				}
			}
		}


		#region RenderedFrames wrapper functions
		public void RenderedFramesClear() {
			renderedFrames.Clear();
			renderedFramesOrder.Clear();
		}

		public bool RenderedFramesContains(object key) {
			return this.renderedFrames.Contains(key);
		}

		public object RenderedFramesGet(object key) {
			return this.renderedFrames[key];
		}

		public void RenderedFramesSet(object key, object value) {
			if (this.renderedFrames.Count > maxFrames) {
				this.renderedFrames.Remove(this.renderedFramesOrder[0]);
				this.renderedFramesOrder.RemoveAt(0);
			}

			this.renderedFrames.Add(key, value);
			this.renderedFramesOrder.Add(key);
		}
		#endregion

		/// <summary>
		/// This is not a true hash code. It is only used to determine if any
		/// graphically visible characteristics of the Content object have
		/// changed.
		/// </summary>
		/// <returns></returns>
		public virtual int VisibleHashCode() {
			int fh = 0;
			
			if (theme != null) fh += theme.VisibleHashCode();

			return
				fh + (this.HideBG ? "HideBG".GetHashCode() : "ShowBG".GetHashCode()) +
				(this.HideText ? "HideText".GetHashCode() : "ShowText".GetHashCode()) +
				(this.showRectangles ? "ShowRect".GetHashCode() : "HideRect".GetHashCode()) +
				(this.ThemePath != null ? this.ThemePath.GetHashCode() : "NoBgImg".GetHashCode());
		}

		/// <summary>
		/// Renders the background image for the content (unless HideBG is true).
		/// </summary>
		/// <param name="ConfigBGImagePath">This background is used unless the user selected a custom one.</param>
		/// <param name="graphics">The Graphics object to render the background on</param>
		/// <param name="Width"></param>
		/// <param name="Height"></param>
		public void RenderBGImage(string ConfigBGImagePath, Graphics graphics, int Width, int Height) {
			if (this.HideBG == false) {
				string fullPath = Tools.GetFullPathOrNull(Tools.GetDirectory(DirType.DataRoot), this.BGImagePath);
				if (Tools.FileExists(fullPath)) {
					if (this.bgImage == null) {
						try {
							this.bgImage = Image.FromFile(fullPath);
						} catch { }
					}
				} else if (Tools.FileExists(Tools.GetFullPathOrNull(Tools.GetDirectory(DirType.DataRoot), ConfigBGImagePath))) {
					if (this.bgImage == null) {
						try {
							this.bgImage = Image.FromFile(Tools.GetFullPathOrNull(Tools.GetDirectory(DirType.DataRoot), ConfigBGImagePath));
						} catch { }
					}
				}

				if (this.bgImage != null) {
					graphics.DrawImage(this.bgImage, 0, 0, Width, Height);
				}
			} else {
				// Draw blank rectangle if no image is defined
				graphics.FillRectangle(new SolidBrush(Color.Black), new Rectangle(0, 0, Width, Height));
			}
		}
	}

	/// <summary>
	/// This class represents a simple image/background to be drawn. No text.
	/// It is used for the "image" multimedia type.
	/// </summary>
	public class ImageContent : Content, IContentOperations {
		public ImageContent() { }
		public ImageContent(string Path) {
			this.Theme = new SermonTheme();
			this.BGImagePath = Path;
		}

		#region IContentOperations Members

		public Bitmap GetBitmap(int Width, int Height) {
			if (this.RenderedFramesContains(this.VisibleHashCode())) {
				Console.WriteLine("ImageContent pre-render cache hit.");
				return this.RenderedFramesGet(this.VisibleHashCode()) as Bitmap;
			}

			Bitmap bmp = new Bitmap(Width, Height);
			Graphics graphics = Graphics.FromImage(bmp);

			#region Render background image
			// Draw background image
			if (this.HideBG == false) {
				string fullPath = Tools.GetFullPathOrNull(Tools.GetDirectory(DirType.DataRoot), this.BGImagePath);
				if (Tools.FileExists(fullPath)) {
					if (this.bgImage == null) {
						try {
							this.bgImage = Image.FromFile(fullPath);
						} catch { }
					}
				}

				if (this.bgImage != null) {
					graphics.DrawImage(this.bgImage, 0, 0, Width, Height);
				}
			} else {
				// Draw blank rectangle if no image is defined
				graphics.FillRectangle(new SolidBrush(Color.Black), new Rectangle(0, 0, Width, Height));
			}
			#endregion

			graphics.Dispose();
			this.RenderedFramesSet(this.VisibleHashCode(), bmp);

			return bmp;
		}

		public bool Next() {
			// Nothing to do.
			return true;
		}

		public bool Prev() {
			// Nothing to do.
			return true;
		}

		[XmlIgnore()]
		public bool ShowRectangles {
			get { return showRectangles; }
			set { showRectangles = value; }
		}

		public ContentIdentity GetIdentity() {
			// TODO:  Add ImageContent.GetIdentity implementation
			ContentIdentity ident = new ContentIdentity();
			return ident;
		}

		public void PreRenderFrames() {
			// This function doesn't really need to do anything. We've
			// prerendered the one and only frame as soon as we call GetBitmap
			// the first time.
		}

		public void DefaultBackground(Config conf) {}

		#endregion

		#region ICloneable Members

		object ICloneable.Clone() {
			return this.Clone();
		}

		public virtual ImageContent Clone() {
			ImageContent c = new ImageContent();
			c = this.MemberwiseClone() as ImageContent;
			return c;
		}

		#endregion

	}


	/// <summary>
	/// This class represents a display type. We can create a Preview, Live,
	/// mini-Live and remote-Live object based on this class.
	/// </summary>
	public class Display {

		#region Variables
		public IContentOperations content;

		// A number of display types this display object would render to. Only one of these
		// should be non-null.
		public PictureBox pictureBox;
		public Graphics graphics;
		public ShowBeam showBeam;
        public ImagePanel imagepanel;
		public XmlRpcClient XmlRpcProxy;

		public static BibleLib bibleLib;
		public static Config config;

		private Size size;
		private Point location;

		// We (pre)render everything to this size, then scale down as needed
		public Size Size {
			set {
				size = value;
				if (size.Width < 200) size.Width = 200;
				if (size.Height < 150) size.Height = 150;
				if (showBeam != null) this.showBeam.TopLevelControl.Size = this.size;
			}
			get { return size; }
		}
		public Point Location {
			set {
				location = value;
				if (showBeam != null) this.showBeam.TopLevelControl.Location = this.location;
			}
			get { return location; }
		}

		/// <summary>
		/// Allows display chains to be created. This field determines what the next display
		/// in the chain is. Chains are useful for tying two displays together so that if the
		/// first one in the chain is not updated, the rest of the displays in the chain are
		/// also not updated. Ex. remote live screen can have as a NextDisplay the local
		/// miniLive screen, so that the local miniLive screen accurately represents the state
		/// of the remote Live screen.
		/// </summary>
		public Display NextDisplay;

		/// <summary>Used for debugging purposes only</summary>
		private string DisplayName;
		#endregion

		public Display(string DisplayName, Config config) {
			this.DisplayName = DisplayName;

			if (config != null) {
				this.ChangeDisplayCoord(config);
			}
		}

		/// <summary>
		/// This method is used in StandAlone mode to set the content to be
		/// displayed in various displays.
		/// </summary>
		/// <param name="obj">The content object to display</param>
		/// <returns>Returns true if the entire display chain has successfully
		/// set its content to the object passed in</returns>
		public bool SetContent(IContentOperations obj) {
			bool result = true;

			if (XmlRpcProxy != null) {
				// This is a "client" display, and must relay its content to the "server" peer
				result = XmlRpcProxy.SetContent(obj.GetIdentity());
			}

			// Note: We intentionaly don't pass a clone of the object because we want the whole
			// chain to share a single object reference and be in sync.
			if (result && NextDisplay != null) result = NextDisplay.SetContent(obj);

			if (result) {
				content = obj;
				return this.UpdateDisplay(false);
			} else {
				return false;
			}
		}

		/// <summary>
		/// If this display is a remote server it will receive a description of the content,
		/// and will have to create a content object based on that. We don't want to send
		/// actual objects to remote servers.
		/// </summary>
		/// <param name="identity">A "description" of the content</param>
		public bool SetContent(ContentIdentity identity) {
			IContentOperations newContent = null;
			bool success = true;

			try {
				switch ((ContentType)identity.Type) {
					case ContentType.BibleVerseIdx:
						newContent = new ABibleVerse(bibleLib[identity.BibleTransl], identity.VerseIdx, Display.config);
						break;
					case ContentType.BibleVerseRef:
						BibleVersion bible = bibleLib[identity.BibleTransl];
						int idx = bible.GetVerseIndex(identity.VerseRef);
						newContent = new ABibleVerse(bible, idx, Display.config);
						break;
					case ContentType.PlainText:
						newContent = new TextToolContents(identity.Text, Display.config);
						break;
					case ContentType.Song:
						string songFile = Tools.GetDirectory(DirType.Songs, identity.SongName);
						newContent = (Song)Song.DeserializeFrom(songFile, identity.SongStrophe, Display.config);
						break;
				}
			} catch { }	// Covers a multitude of sins (non-existent translation, or song, or verse, etc...)

			if (newContent != null) {
				if (NextDisplay != null) success = NextDisplay.SetContent(newContent);
				if (success) {
					this.content = newContent;
					success = this.UpdateDisplay(false);
				}
			} else {
				success = false;
			}

			return success;
		}

		/// <summary>
		/// Switch to the next strophe, verse, etc... of the current content, and update the
		/// display (and any other displays that are chained to this one).
		/// </summary>
		/// <returns></returns>
		public bool ContentNext() {
			if (content == null) return false;
			bool success = true;

			// If this is running as a client, only switch to next if the server switched successfully.
			if (XmlRpcProxy != null) {
				success = XmlRpcProxy.ContentNext();
			}

			// Note: No need to do NextDisplay.ContentNext() because this display
			// and nextDisplay share a content object so it suffices to do ContentNext()
			// on this object.
			if (success) {
				success = content.Next();
				if (success) success = this.UpdateDisplay(true);
			}

			return success;
		}
		public bool ContentPrev() {
			if (content == null) return false;
			bool success = true;

			if (XmlRpcProxy != null) {
				success = XmlRpcProxy.ContentPrev();
			}

			if (success) {
				success = content.Prev();
				if (success) success = this.UpdateDisplay(true);
			}

			return success;
		}

		public bool HideText(bool Hidden) {
			if (content == null) return false;
			bool success = true;

			if (XmlRpcProxy != null) {
				success = XmlRpcProxy.HideText(Hidden);
			}

			if (success) {
				(content as Content).HideText = Hidden;
				success = UpdateDisplay(true);
			}
			return success;
		}

		public Bitmap GetBitmap(int width, int height) {
			if (content == null) { return new Bitmap(width, height); }
			return content.GetBitmap(width, height);
		}
		public Bitmap GetBitmap(Size size) {
			if (content == null) { return new Bitmap(size.Width, size.Height); }
			return this.GetBitmap(size.Width, size.Height);
		}

		#region Destinations
		public void SetDestination(PictureBox box) {
			pictureBox = box;
		}
        public void SetDestination(ImagePanel panel)
        {
            this.imagepanel = panel;
        }
		public void SetDestination(Graphics graphics) {
			this.graphics = graphics;
		}
		public void SetDestination(ShowBeam showBeam) {
			this.showBeam = showBeam;
		}

		// This display is the Client end and forwards all requests over XML-RPC to the server.
		public void SetDestination(string XmlRpcURL) {
			XmlRpcProxy = new XmlRpcClient();
			XmlRpcProxy.Timeout = 2000;
			XmlRpcProxy.Url = XmlRpcURL;
		}

		// This display is the Server end.
		public void SetDestination(int ListeningPort) {

			// We chain xml-rpc -> soap so that we can support both XML-RPC and SOAP
			IServerChannelSinkProvider xml_rpc = new XmlRpcServerFormatterSinkProvider(null, null);
			IServerChannelSinkProvider soap = new SoapServerFormatterSinkProvider();
			xml_rpc.Next = soap;

			IDictionary props = new Hashtable();
			props["ref"] = "http";
			props["port"] = ListeningPort;
			HttpChannel channel = new HttpChannel(props, null, xml_rpc);

			ChannelServices.RegisterChannel(channel, false);

			// Not sure how this works if we re-configure and register when OperatingMode changes
			// There might be other ways to do the same thing and allow for de-registration
			RemotingConfiguration.Configure(null, false);

			// Counterintuitive, but when set to FALSE it reports errors across XML-RPC. Keep it this way to simplify debugging.
			RemotingConfiguration.CustomErrorsEnabled(false);

			// Can't figure out how to register at "http://serverip:port/". Must use "http://serverip:port/DreamBeam"
			RemotingConfiguration.RegisterWellKnownServiceType(typeof(XmlRpcServer),
				"DreamBeam", WellKnownObjectMode.Singleton);
		}
		#endregion

		/// <summary>
		/// Updates the windows form that contains the image of the current content.
		/// </summary>
		/// <param name="cascade">If true, it will call UpdateDisplay on NextDisplay as well</param>
		/// <returns></returns>
		public bool UpdateDisplay(bool cascade) {
			if (content != null) {
				if (pictureBox != null) {
					// Preview, or mini-Live display                    
					pictureBox.Image = ShowBeam.DrawProportionalBitmap(pictureBox.Size, GetBitmap(this.Size)).Bitmap;
                }
                else if (imagepanel != null){
                  imagepanel.ImagePack = ShowBeam.DrawProportionalBitmap(imagepanel.Size, GetBitmap(this.Size));
                    
				} else if (showBeam != null && showBeam.TopLevelControl.Visible) {
					// Live display
					showBeam.GDIDraw(this.GetBitmap(Size));

				}
			}
			if (cascade && NextDisplay != null) return NextDisplay.UpdateDisplay(cascade);
			return true;
		}


		public void ChangeDisplayCoord(Config config) {
			if (config == null) return;
			this.Size = new Size(config.BeamBoxSizeX, config.BeamBoxSizeY);
			this.Location = new Point(config.BeamBoxPosX, config.BeamBoxPosY);

			try {
				(content as Content).RenderedFramesClear();
			} catch { }

			this.UpdateDisplay(false);
		}

	}

}
