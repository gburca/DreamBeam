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

namespace DreamBeam	{

	/// <summary>
	/// Each Song, BibleVerse, or SermonText piece of content needs to know how to draw itself (possibly caching
	/// and/or prerendering). It also needs to know how to switch to the next/previous strophe, verse, sermon tab
	/// We inherit from ICloneable so that we can pass from a Preview display to a Live display a clone that is independent.
	/// </summary>
	public interface IContentOperations : ICloneable {
		Bitmap GetBitmap(int Width, int Height);
		bool Next();
		bool Prev();
		void ChangeBGImagePath(string newPath);

		/// <summary>
		/// When asking a remote display to show a piece of content, we don't want to send the actual content
		/// across. Instead we send just enough information for the remote server to be able to identify the
		/// content and display it.
		/// </summary>
		/// <returns>Information required to uniquely identify the content.</returns>
		ContentIdentity GetIdentity();
	}

//	public interface IContentItem {
//		/// <summary>
//		/// When asking a remote display to show a piece of content, we don't want to send the actual content
//		/// across. Instead we send just enough information for the remote server to be able to identify the
//		/// content and display it.
//		/// </summary>
//		/// <returns>Information required to uniquely identify the content.</returns>
//		ContentIdentity GetIdentity();
//	}

	/// <summary>
	/// All content (Songs, BibleVerses, SermonText) has some attributes in common. They all derive from this class.
	/// TODO: The ICloneable should probably move here, and this base class should implement Clone() itself also.
	/// </summary>
	[Serializable()]
	public class Content {
		// The following settings will not be saved when we serialize this class
		[XmlIgnore()] private string bgImagePath;
		[XmlIgnore()] public Image bgImage;
		[XmlIgnore()] public bool HideBG = false;
		[XmlIgnore()] public bool HideText = false;

		// The following setting and properties will be serialized
		public BeamTextFormat[] format;
		/// <summary>Should we use the global or custom font/background/etc... format for this content</summary>
		public bool CustomFormat = false;
		public bool WordWrap = false;	// Bible text is word-wrapped. Songs are not.

		public string BGImagePath {
			get { return this.bgImagePath; }
			set {
				// bgImage is no longer valid when the path is changed
				bgImage = null;
				this.bgImagePath = value;
			}
		}
	}

	/// <summary>
	/// This class represents a simple image/background to be drawn. No text.
	/// It is used for the "image" multimedia type.
	/// </summary>
	public class ImageContent : Content, IContentOperations {
		public ImageContent () {}
		public ImageContent (string Path) {
			this.BGImagePath = Path;
		}

		#region IContentOperations Members

		public Bitmap GetBitmap(int Width, int Height) {
			Bitmap bmp = new Bitmap(Width, Height);
			Graphics graphics = Graphics.FromImage(bmp);

			#region Render background image
			// Draw background image
			if (this.HideBG == false) {
				string fullPath = Tools.GetFullPath( this.BGImagePath );
				if (Tools.FileExists(fullPath)) {
					if (this.bgImage == null) {
						try {
							this.bgImage = Image.FromFile(fullPath);
						} catch {}
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

		public void ChangeBGImagePath(string newPath) {
			this.BGImagePath = newPath;
		}

		public ContentIdentity GetIdentity() {
			// TODO:  Add ImageContent.GetIdentity implementation
			ContentIdentity ident = new ContentIdentity();			
			return ident;
		}

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
		public Panel panel;
		public XmlRpcClient XmlRpcProxy;

		public static BibleLib bibleLib;
		public static Config config;

		// Alpha blending
		MyPerPixelAlphaForm AlphaForm;
		byte AlphaOpacity;
		Timer AlphaTimer;
		Bitmap bmp;

		private Size size;
		private Point location;

		// We (pre)render everything to this size, then scale down as needed
		public Size Size {
			set {
				size = value;
				if (size.Width < 200) size.Width = 200;
				if (size.Height < 150) size.Height = 150;
				if (panel != null) this.panel.TopLevelControl.Size = this.size;
			}
			get { return size; }
		}
		public Point Location {
			set {
				location = value;
				if (panel != null) this.panel.TopLevelControl.Location = this.location;
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
		/// This method is used in StandAlone mode to set the content to be displayed in various displays.
		/// </summary>
		/// <param name="obj">The content object to display</param>
		/// <returns>Returns true if the entire display chain has successfully set its content to the object passed in</returns>
		public bool SetContent(IContentOperations obj) {
			bool result = true;

			if (XmlRpcProxy != null) {
				// This is a "client" display, and must relay its content to the "server" peer
				result = XmlRpcProxy.SetContent( obj.GetIdentity() );
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
						(newContent as TextToolContents).song.strophe = identity.SongStrophe;
						break;
					case ContentType.Song:
						string songFile = Path.Combine(Tools.DreamBeamPath(), Path.Combine("Songs", identity.SongName));
						newContent = (NewSong)NewSong.DeserializeFrom(songFile, identity.SongStrophe, Display.config);
						break;
				}
			} catch {}	// Covers a multitude of sins (non-existent translation, or song, or verse, etc...)

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
		public void SetDestination(Graphics graphics) {
			this.graphics = graphics;
		}
		public void SetDestination(Panel panel) {
			this.panel = panel;

			if (AlphaForm == null) AlphaForm = new MyPerPixelAlphaForm();
			if (AlphaTimer == null) {
				AlphaTimer = new Timer();
				AlphaTimer.Interval = 1;
				AlphaTimer.Tick += new System.EventHandler(this.AlphaTimer_Tick);
			}
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

			ChannelServices.RegisterChannel(channel);
				
			// Not sure how this works if we re-configure and register when OperatingMode changes
			// There might be other ways to do the same thing and allow for de-registration
			RemotingConfiguration.Configure(null);

			// Counterintuitive, but when set to FALSE it reports errors across XML-RPC. Keep it this way to simplify debugging.
			RemotingConfiguration.CustomErrorsEnabled(false);

			// Can't figure out how to register at "http://serverip:port/". Must use "http://serverip:port/DreamBeam"
			RemotingConfiguration.RegisterWellKnownServiceType( typeof(XmlRpcServer),
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
					pictureBox.Image = ShowBeam.DrawProportionalBitmap(pictureBox.Size, GetBitmap(this.Size));

				} else if (panel != null && panel.TopLevelControl.Visible) {
					// Live display
					if (config.Alphablending) {
						this.bmp = GetBitmap(Size);
						AlphaForm.setpos(Location.X, Location.Y);
						AlphaForm.Visible = true;
						AlphaForm.SetBitmap(bmp);
						AlphaOpacity = 0;
						AlphaTimer.Start();

					} else {
						Graphics g = Graphics.FromHwnd(panel.Handle);
						g.SmoothingMode = SmoothingMode.HighQuality;
						g.InterpolationMode = InterpolationMode.HighQualityBicubic;
						g.PixelOffsetMode = PixelOffsetMode.HighQuality;
						g.DrawImage(GetBitmap(Size), new Rectangle(0, 0, Size.Width, Size.Height));
					}
				}
			}
			if (cascade && NextDisplay != null) return NextDisplay.UpdateDisplay(cascade);
			return true;
		}


		private void AlphaTimer_Tick(object sender, System.EventArgs e) {
			if (AlphaOpacity < 255) {
				if (AlphaOpacity + config.BlendSpeed > 255) AlphaOpacity = (byte)255;
				else AlphaOpacity = (byte)(AlphaOpacity + config.BlendSpeed);

				AlphaForm.SetBitmap(bmp, AlphaOpacity);
			} else {
				AlphaTimer.Stop();
				Graphics g = Graphics.FromHwnd(panel.Handle);
				g.DrawImage(bmp, new Rectangle(0, 0, Size.Width, Size.Height));
				AlphaForm.Hide();
			}
		}

		public void ChangeDisplayCoord(Config config) {
			if (config == null) return;
			this.Size = new Size(config.BeamBoxSizeX, config.BeamBoxSizeY);
			this.Location = new Point(config.BeamBoxPosX, config.BeamBoxPosY);
		}

	}

}
