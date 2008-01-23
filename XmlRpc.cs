using System;
using System.Runtime.Remoting;
using System.Runtime.InteropServices;
using CookComputing.XmlRpc;

namespace DreamBeam
{
	#region Enums
	public enum ContentType {
		Song,
		BibleVerseIdx,
		BibleVerseRef,
		PlainText,  // == SermonText
        BibleVerse  // Not used for XML-RPC
	}
	#endregion

	[XmlRpcMissingMapping(MappingAction.Ignore)]
	public struct ContentIdentity {
		//public ContentType Type;	// XML-RPC doesn't support enums
		public int Type;
		/// <summary>For type == ContentType.Song</summary>
		public string SongName;
		public int SongStrophe;
		/// <summary>For type == ContentType.BibleVerseIdx</summary>
		public int VerseIdx;
		public string BibleTransl;
		public string VerseRef;
		public string Text;
	}

	/// <summary>
	/// Summary description for XmlRpcClient.
	/// </summary>
	public class XmlRpcClient : XmlRpcClientProtocol
	{
		public XmlRpcClient()
		{
		}

		[XmlRpcMethod]
		public bool SetContent(ContentIdentity identity) {
			// TODO: Handle timeouts

			if (identity.Text == null) identity.Text = "";
			if (identity.SongName == null) identity.SongName = "";
			if (identity.BibleTransl == null) identity.BibleTransl = "";
			if (identity.VerseRef == null) identity.VerseRef = "";
			try {
				return (bool)Invoke("SetContent", new Object[]{identity});
			} catch (Exception e) {
				Console.WriteLine("Could not SetContent over XML-RPC: ", e.Message);
			}
			return false;
		}

//		[XmlRpcMethod] public bool UpdateDisplay() {
//			// TODO: Handle timeouts
//			return (bool)Invoke("UpdateDisplay", new Object[]{});
//		}

		[XmlRpcMethod] public bool ContentPrev() {
			try {
				return (bool)Invoke("ContentPrev", new Object[]{});
			} catch {}
			return false;
		}

		[XmlRpcMethod] public bool ContentNext() {
			try {
				return (bool)Invoke("ContentNext", new Object[]{});
			} catch {}
			return false;
		}

		[XmlRpcMethod] public bool HideText(bool Hidden) {
			try {
				return (bool)Invoke("HideText", new Object[]{Hidden});
			} catch {}
			return false;
		}
	}

	/// <summary>
	/// These are the methods we expose to clients
	/// </summary>
	[XmlRpcService(AutoDocVersion=false)]
	public class XmlRpcServer : MarshalByRefObject {
		public static MainForm mainForm;

		public XmlRpcServer() {
		}

		[XmlRpcMethod("SetContent")]
		public bool SetContent(ContentIdentity identity) {
			return mainForm.DisplayLiveServer.SetContent(identity);
		}

//		[XmlRpcMethod("UpdateDisplay")]
//		public bool UpdateDisplay() {
//			return mainForm.DisplayLiveServer.UpdateDisplay();
//		}

		[XmlRpcMethod("ContentPrev")] public bool ContentPrev() {
			return mainForm.DisplayLiveServer.ContentPrev();
		}

		[XmlRpcMethod("ContentNext")] public bool ContentNext() {
			return mainForm.DisplayLiveServer.ContentNext();
		}

		[XmlRpcMethod("HideText")] public bool HideText(bool Hidden) {
			return mainForm.DisplayLiveServer.HideText(Hidden);
		}
	}

}
