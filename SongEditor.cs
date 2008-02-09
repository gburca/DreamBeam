using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace DreamBeam {
	/// <summary>
	/// Summary description for SongEditor.
	/// </summary>
	public class SongEditor : System.Windows.Forms.UserControl {
		private ArrayList sequenceAvailable = new ArrayList();
		private ArrayList sequence = new ArrayList();
		private NewSong song;

		#region Designer variables
		private System.Windows.Forms.TextBox Title;
		private System.Windows.Forms.TextBox Number;
		private System.Windows.Forms.ComboBox Collection;
		private System.Windows.Forms.TextBox Author;
		private System.Windows.Forms.TabPage General_Tab;
		private System.Windows.Forms.TabPage Lyrics_Tab;
		private System.Windows.Forms.TabPage Sequence_Tab;
		private System.Windows.Forms.TabPage LyricsVerse_Tab;
		private System.Windows.Forms.TabPage LyricsChorus_Tab;
		private System.Windows.Forms.TabPage LyricsOther_Tab;
		private System.Windows.Forms.ImageList ListEx_ImageList;
		private Lister.ListEx ListEx_Sequence;
		private Lister.ListEx ListEx_Available;
		private System.Windows.Forms.Label label1;
		private TD.SandDock.DockContainer leftSandDock;
		private TD.SandDock.DockContainer rightSandDock;
		private TD.SandDock.DockContainer bottomSandDock;
		private TD.SandDock.DockContainer topSandDock;
		private TD.SandDock.SandDockManager sandDockManager0;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.ToolTip ToolTip;
		private System.Windows.Forms.RichTextBox ChorusLyrics;
		public System.Windows.Forms.TextBox Background;
		private System.Windows.Forms.ComboBox KeyRangeLow;
		private System.Windows.Forms.ComboBox KeyRangeHigh;
		private System.Windows.Forms.CheckBox MinorKey;
		private System.Windows.Forms.RichTextBox VerseLyrics;
		private System.Windows.Forms.RichTextBox OtherLyrics;
		private ctlLEDRadioButton.LEDradioButton DualLanguage;
		private System.Windows.Forms.TextBox Notes;
		private System.Windows.Forms.TabControl MainTabs;
		private System.Windows.Forms.TabControl LyricsTabs;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox CustomFormat;
		private System.ComponentModel.IContainer components;
		#endregion

		public SongEditor() {
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

		}

		public NewSong Song {
			set { song = (NewSong)value; PopulateControls(song); }
			get { return ReadControls(); }
		}

		public string[] Collections {
			set {
				this.Collection.Items.Clear();
				foreach (string c in value) {
					this.Collection.Items.Add(c);
				}
			}
			get {
				string[] collections = new String[this.Collection.Items.Count];
				int i = 0;
				foreach (string c in this.Collection.Items) {
					collections[i++] = c;
				}
				return collections;
			}
		}

		public void PopulateControls(NewSong song) {
			this.Title.Text = song.Title;
			this.Author.Text = song.Author;
			this.Collection.Text = song.Collection;
			this.Number.Text = song.Number;
			this.Notes.Text = (song.Notes != null) ? Regex.Replace(song.Notes, "\n", "\r\n") : null;
			this.KeyRangeLow.Text = song.KeyRangeLow;
			this.KeyRangeHigh.Text = song.KeyRangeHigh;
			this.MinorKey.Checked = song.MinorKey;
			this.CustomFormat.Checked = song.CustomFormat;
			this.DualLanguage.Checked = song.DualLanguage;
			this.Background.Text = song.BGImagePath;

			// If we don't Clear, old RTF formatting codes from the previous Paste operation
			// remain in the control and cause the new text to take on that formatting
			this.VerseLyrics.Clear();
			this.ChorusLyrics.Clear();
			this.OtherLyrics.Clear();
			this.VerseLyrics.Text = song.GetLyrics(LyricsType.Verse);
			this.ChorusLyrics.Text = song.GetLyrics(LyricsType.Chorus);
			this.OtherLyrics.Text = song.GetLyrics(LyricsType.Other);

			this.ListEx_Sequence.Items.Clear();
			this.sequence.Clear();
			foreach (LyricsSequenceItem item in song.Sequence) {
				this.ListEx_Sequence.Add(item.ToString(), 1);
				this.sequence.Add(item);
			}

			this.UpdateAvailableLyrics();
		}

		public NewSong ReadControls() {
			if (song == null) song = new NewSong();

			song.Title = this.Title.Text;
			song.Author = this.Author.Text;
			song.Collection = this.Collection.Text;
			song.Number = this.Number.Text;
			song.Notes = (this.Notes.Text != null) ? Regex.Replace(this.Notes.Text, "\r\n", "\n") : null;
			song.KeyRangeLow = this.KeyRangeLow.Text;
			song.KeyRangeHigh = this.KeyRangeHigh.Text;
			song.MinorKey = this.MinorKey.Checked;
			song.DualLanguage = this.DualLanguage.Checked;
			song.CustomFormat = this.CustomFormat.Checked;

			song.SetLyrics(LyricsType.Verse, this.VerseLyrics.Text);
			song.SetLyrics(LyricsType.Chorus, this.ChorusLyrics.Text);
			song.SetLyrics(LyricsType.Other, this.OtherLyrics.Text);

			song.Sequence = new ArrayList();
			foreach (LyricsSequenceItem item in this.sequence) {
				song.Sequence.Add(new LyricsSequenceItem(item));
			}

			return song;
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing) {
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SongEditor));
			this.Title = new System.Windows.Forms.TextBox();
			this.Number = new System.Windows.Forms.TextBox();
			this.Collection = new System.Windows.Forms.ComboBox();
			this.Author = new System.Windows.Forms.TextBox();
			this.MainTabs = new System.Windows.Forms.TabControl();
			this.General_Tab = new System.Windows.Forms.TabPage();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.CustomFormat = new System.Windows.Forms.CheckBox();
			this.button1 = new System.Windows.Forms.Button();
			this.Background = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.MinorKey = new System.Windows.Forms.CheckBox();
			this.KeyRangeHigh = new System.Windows.Forms.ComboBox();
			this.KeyRangeLow = new System.Windows.Forms.ComboBox();
			this.label8 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.Notes = new System.Windows.Forms.TextBox();
			this.Lyrics_Tab = new System.Windows.Forms.TabPage();
			this.LyricsTabs = new System.Windows.Forms.TabControl();
			this.LyricsVerse_Tab = new System.Windows.Forms.TabPage();
			this.VerseLyrics = new System.Windows.Forms.RichTextBox();
			this.LyricsChorus_Tab = new System.Windows.Forms.TabPage();
			this.ChorusLyrics = new System.Windows.Forms.RichTextBox();
			this.LyricsOther_Tab = new System.Windows.Forms.TabPage();
			this.OtherLyrics = new System.Windows.Forms.RichTextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.DualLanguage = new ctlLEDRadioButton.LEDradioButton();
			this.Sequence_Tab = new System.Windows.Forms.TabPage();
			this.label10 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.ListEx_Available = new Lister.ListEx();
			this.ListEx_ImageList = new System.Windows.Forms.ImageList(this.components);
			this.ListEx_Sequence = new Lister.ListEx();
			this.sandDockManager0 = new TD.SandDock.SandDockManager();
			this.leftSandDock = new TD.SandDock.DockContainer();
			this.rightSandDock = new TD.SandDock.DockContainer();
			this.bottomSandDock = new TD.SandDock.DockContainer();
			this.topSandDock = new TD.SandDock.DockContainer();
			this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.MainTabs.SuspendLayout();
			this.General_Tab.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.Lyrics_Tab.SuspendLayout();
			this.LyricsTabs.SuspendLayout();
			this.LyricsVerse_Tab.SuspendLayout();
			this.LyricsChorus_Tab.SuspendLayout();
			this.LyricsOther_Tab.SuspendLayout();
			this.panel1.SuspendLayout();
			this.Sequence_Tab.SuspendLayout();
			this.SuspendLayout();
			// 
			// Title
			// 
			this.Title.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.Title.Location = new System.Drawing.Point(90, 10);
			this.Title.Name = "Title";
			this.Title.Size = new System.Drawing.Size(228, 20);
			this.Title.TabIndex = 0;
			// 
			// Number
			// 
			this.Number.Location = new System.Drawing.Point(90, 90);
			this.Number.Name = "Number";
			this.Number.Size = new System.Drawing.Size(68, 20);
			this.Number.TabIndex = 4;
			// 
			// Collection
			// 
			this.Collection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.Collection.Location = new System.Drawing.Point(90, 62);
			this.Collection.Name = "Collection";
			this.Collection.Size = new System.Drawing.Size(228, 21);
			this.Collection.TabIndex = 2;
			// 
			// Author
			// 
			this.Author.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.Author.Location = new System.Drawing.Point(90, 36);
			this.Author.Name = "Author";
			this.Author.Size = new System.Drawing.Size(228, 20);
			this.Author.TabIndex = 1;
			// 
			// MainTabs
			// 
			this.MainTabs.Controls.Add(this.General_Tab);
			this.MainTabs.Controls.Add(this.Lyrics_Tab);
			this.MainTabs.Controls.Add(this.Sequence_Tab);
			this.MainTabs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MainTabs.Location = new System.Drawing.Point(0, 0);
			this.MainTabs.Name = "MainTabs";
			this.MainTabs.SelectedIndex = 0;
			this.MainTabs.Size = new System.Drawing.Size(348, 398);
			this.MainTabs.TabIndex = 0;
			this.MainTabs.SelectedIndexChanged += new System.EventHandler(this.MainTabs_SelectedIndexChanged);
			// 
			// General_Tab
			// 
			this.General_Tab.Controls.Add(this.groupBox1);
			this.General_Tab.Controls.Add(this.MinorKey);
			this.General_Tab.Controls.Add(this.KeyRangeHigh);
			this.General_Tab.Controls.Add(this.KeyRangeLow);
			this.General_Tab.Controls.Add(this.label8);
			this.General_Tab.Controls.Add(this.label6);
			this.General_Tab.Controls.Add(this.label5);
			this.General_Tab.Controls.Add(this.label4);
			this.General_Tab.Controls.Add(this.label3);
			this.General_Tab.Controls.Add(this.label2);
			this.General_Tab.Controls.Add(this.Notes);
			this.General_Tab.Controls.Add(this.Title);
			this.General_Tab.Controls.Add(this.Number);
			this.General_Tab.Controls.Add(this.Author);
			this.General_Tab.Controls.Add(this.Collection);
			this.General_Tab.Location = new System.Drawing.Point(4, 22);
			this.General_Tab.Name = "General_Tab";
			this.General_Tab.Size = new System.Drawing.Size(340, 372);
			this.General_Tab.TabIndex = 0;
			this.General_Tab.Text = "General";
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.CustomFormat);
			this.groupBox1.Controls.Add(this.button1);
			this.groupBox1.Controls.Add(this.Background);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Location = new System.Drawing.Point(6, 270);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(326, 94);
			this.groupBox1.TabIndex = 19;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Song display configuration";
			// 
			// CustomFormat
			// 
			this.CustomFormat.Location = new System.Drawing.Point(8, 26);
			this.CustomFormat.Name = "CustomFormat";
			this.CustomFormat.Size = new System.Drawing.Size(90, 24);
			this.CustomFormat.TabIndex = 18;
			this.CustomFormat.Text = "Use custom configuration";
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Location = new System.Drawing.Point(256, 64);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(62, 23);
			this.button1.TabIndex = 7;
			this.button1.Text = "Browse...";
			// 
			// Background
			// 
			this.Background.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.Background.Location = new System.Drawing.Point(88, 66);
			this.Background.Name = "Background";
			this.Background.Size = new System.Drawing.Size(162, 20);
			this.Background.TabIndex = 6;
			this.ToolTip.SetToolTip(this.Background, "Leave blank to use the default background");
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(6, 66);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(70, 16);
			this.label7.TabIndex = 16;
			this.label7.Text = "Background:";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// MinorKey
			// 
			this.MinorKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.MinorKey.Location = new System.Drawing.Point(258, 238);
			this.MinorKey.Name = "MinorKey";
			this.MinorKey.Size = new System.Drawing.Size(64, 24);
			this.MinorKey.TabIndex = 10;
			this.MinorKey.Text = "Minor";
			// 
			// KeyRangeHigh
			// 
			this.KeyRangeHigh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.KeyRangeHigh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.KeyRangeHigh.ItemHeight = 13;
			this.KeyRangeHigh.Items.AddRange(new object[] {
            "",
            "A",
            "A#",
            "Bb",
            "B",
            "C",
            "C#",
            "Db",
            "D",
            "D#",
            "Eb",
            "E",
            "F",
            "F#",
            "Gb",
            "G",
            "G#",
            "Ab"});
			this.KeyRangeHigh.Location = new System.Drawing.Point(176, 238);
			this.KeyRangeHigh.MaxDropDownItems = 13;
			this.KeyRangeHigh.Name = "KeyRangeHigh";
			this.KeyRangeHigh.Size = new System.Drawing.Size(74, 21);
			this.KeyRangeHigh.TabIndex = 9;
			// 
			// KeyRangeLow
			// 
			this.KeyRangeLow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.KeyRangeLow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.KeyRangeLow.ItemHeight = 13;
			this.KeyRangeLow.Items.AddRange(new object[] {
            "",
            "A",
            "A#",
            "Bb",
            "B",
            "C",
            "C#",
            "Db",
            "D",
            "D#",
            "Eb",
            "E",
            "F",
            "F#",
            "Gb",
            "G",
            "G#",
            "Ab"});
			this.KeyRangeLow.Location = new System.Drawing.Point(90, 238);
			this.KeyRangeLow.MaxDropDownItems = 13;
			this.KeyRangeLow.Name = "KeyRangeLow";
			this.KeyRangeLow.Size = new System.Drawing.Size(76, 21);
			this.KeyRangeLow.TabIndex = 8;
			// 
			// label8
			// 
			this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label8.Location = new System.Drawing.Point(10, 242);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(70, 16);
			this.label8.TabIndex = 17;
			this.label8.Text = "Key range:";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(10, 118);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(70, 16);
			this.label6.TabIndex = 15;
			this.label6.Text = "Notes:";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(10, 92);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(70, 16);
			this.label5.TabIndex = 14;
			this.label5.Text = "Number";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(10, 66);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(70, 16);
			this.label4.TabIndex = 13;
			this.label4.Text = "Collection:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(10, 40);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(70, 16);
			this.label3.TabIndex = 12;
			this.label3.Text = "Author:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(10, 14);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(70, 16);
			this.label2.TabIndex = 11;
			this.label2.Text = "Title:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// Notes
			// 
			this.Notes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.Notes.Location = new System.Drawing.Point(90, 116);
			this.Notes.Multiline = true;
			this.Notes.Name = "Notes";
			this.Notes.Size = new System.Drawing.Size(232, 116);
			this.Notes.TabIndex = 5;
			// 
			// Lyrics_Tab
			// 
			this.Lyrics_Tab.Controls.Add(this.LyricsTabs);
			this.Lyrics_Tab.Controls.Add(this.label1);
			this.Lyrics_Tab.Controls.Add(this.panel1);
			this.Lyrics_Tab.Location = new System.Drawing.Point(4, 22);
			this.Lyrics_Tab.Name = "Lyrics_Tab";
			this.Lyrics_Tab.Size = new System.Drawing.Size(340, 372);
			this.Lyrics_Tab.TabIndex = 1;
			this.Lyrics_Tab.Text = "Lyrics";
			// 
			// LyricsTabs
			// 
			this.LyricsTabs.Controls.Add(this.LyricsVerse_Tab);
			this.LyricsTabs.Controls.Add(this.LyricsChorus_Tab);
			this.LyricsTabs.Controls.Add(this.LyricsOther_Tab);
			this.LyricsTabs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LyricsTabs.Location = new System.Drawing.Point(0, 23);
			this.LyricsTabs.Multiline = true;
			this.LyricsTabs.Name = "LyricsTabs";
			this.LyricsTabs.SelectedIndex = 0;
			this.LyricsTabs.Size = new System.Drawing.Size(340, 311);
			this.LyricsTabs.TabIndex = 0;
			// 
			// LyricsVerse_Tab
			// 
			this.LyricsVerse_Tab.Controls.Add(this.VerseLyrics);
			this.LyricsVerse_Tab.Location = new System.Drawing.Point(4, 22);
			this.LyricsVerse_Tab.Name = "LyricsVerse_Tab";
			this.LyricsVerse_Tab.Size = new System.Drawing.Size(332, 285);
			this.LyricsVerse_Tab.TabIndex = 0;
			this.LyricsVerse_Tab.Text = "Verse";
			// 
			// VerseLyrics
			// 
			this.VerseLyrics.AcceptsTab = true;
			this.VerseLyrics.Dock = System.Windows.Forms.DockStyle.Fill;
			this.VerseLyrics.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.VerseLyrics.Location = new System.Drawing.Point(0, 0);
			this.VerseLyrics.Name = "VerseLyrics";
			this.VerseLyrics.Size = new System.Drawing.Size(332, 285);
			this.VerseLyrics.TabIndex = 0;
			this.VerseLyrics.Text = "";
			// 
			// LyricsChorus_Tab
			// 
			this.LyricsChorus_Tab.Controls.Add(this.ChorusLyrics);
			this.LyricsChorus_Tab.Location = new System.Drawing.Point(4, 22);
			this.LyricsChorus_Tab.Name = "LyricsChorus_Tab";
			this.LyricsChorus_Tab.Size = new System.Drawing.Size(332, 285);
			this.LyricsChorus_Tab.TabIndex = 1;
			this.LyricsChorus_Tab.Text = "Chorus";
			// 
			// ChorusLyrics
			// 
			this.ChorusLyrics.AcceptsTab = true;
			this.ChorusLyrics.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ChorusLyrics.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ChorusLyrics.Location = new System.Drawing.Point(0, 0);
			this.ChorusLyrics.Name = "ChorusLyrics";
			this.ChorusLyrics.Size = new System.Drawing.Size(332, 285);
			this.ChorusLyrics.TabIndex = 0;
			this.ChorusLyrics.Text = "";
			// 
			// LyricsOther_Tab
			// 
			this.LyricsOther_Tab.Controls.Add(this.OtherLyrics);
			this.LyricsOther_Tab.Location = new System.Drawing.Point(4, 22);
			this.LyricsOther_Tab.Name = "LyricsOther_Tab";
			this.LyricsOther_Tab.Size = new System.Drawing.Size(332, 285);
			this.LyricsOther_Tab.TabIndex = 2;
			this.LyricsOther_Tab.Text = "Other";
			// 
			// OtherLyrics
			// 
			this.OtherLyrics.AcceptsTab = true;
			this.OtherLyrics.Dock = System.Windows.Forms.DockStyle.Fill;
			this.OtherLyrics.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.OtherLyrics.Location = new System.Drawing.Point(0, 0);
			this.OtherLyrics.Name = "OtherLyrics";
			this.OtherLyrics.Size = new System.Drawing.Size(332, 285);
			this.OtherLyrics.TabIndex = 0;
			this.OtherLyrics.Text = "";
			// 
			// label1
			// 
			this.label1.Dock = System.Windows.Forms.DockStyle.Top;
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(340, 23);
			this.label1.TabIndex = 1;
			this.label1.Text = "Separate verses or choruses with a blank line";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.DualLanguage);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 334);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(340, 38);
			this.panel1.TabIndex = 3;
			// 
			// DualLanguage
			// 
			this.DualLanguage.Anchor = System.Windows.Forms.AnchorStyles.Top;
			//this.DualLanguage.Appearance = System.Windows.Forms.Appearance.Button;
			this.DualLanguage.bottomColor = System.Drawing.Color.DarkBlue;
			this.DualLanguage.BottomTransparent = 64;
			this.DualLanguage.LEDColor = System.Drawing.Color.Green;
			this.DualLanguage.Location = new System.Drawing.Point(118, 7);
			this.DualLanguage.Name = "DualLanguage";
			this.DualLanguage.Size = new System.Drawing.Size(104, 24);
			this.DualLanguage.TabIndex = 2;
			this.DualLanguage.Text = "Dual Language";
			this.DualLanguage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.DualLanguage.topColor = System.Drawing.Color.LightGreen;
			this.DualLanguage.TopTransparent = 64;
			// 
			// Sequence_Tab
			// 
			this.Sequence_Tab.Controls.Add(this.label10);
			this.Sequence_Tab.Controls.Add(this.label9);
			this.Sequence_Tab.Controls.Add(this.ListEx_Available);
			this.Sequence_Tab.Controls.Add(this.ListEx_Sequence);
			this.Sequence_Tab.Location = new System.Drawing.Point(4, 22);
			this.Sequence_Tab.Name = "Sequence_Tab";
			this.Sequence_Tab.Size = new System.Drawing.Size(340, 372);
			this.Sequence_Tab.TabIndex = 2;
			this.Sequence_Tab.Text = "Sequence";
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(12, 10);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(100, 16);
			this.label10.TabIndex = 3;
			this.label10.Text = "Lyrics sequence:";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(158, 10);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(100, 16);
			this.label9.TabIndex = 2;
			this.label9.Text = "Available lyrics:";
			// 
			// ListEx_Available
			// 
			this.ListEx_Available.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)));
			this.ListEx_Available.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
			this.ListEx_Available.Imgs = this.ListEx_ImageList;
			this.ListEx_Available.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(199)))), ((int)(((byte)(199)))));
			this.ListEx_Available.Location = new System.Drawing.Point(156, 28);
			this.ListEx_Available.Name = "ListEx_Available";
			this.ListEx_Available.ReadOnly = true;
			this.ListEx_Available.ShowBullets = true;
			this.ListEx_Available.Size = new System.Drawing.Size(120, 324);
			this.ListEx_Available.TabIndex = 1;
			this.ToolTip.SetToolTip(this.ListEx_Available, "Click on the \"+\" icon in the order the lyrics will be sung");
			this.ListEx_Available.PressIcon += new Lister.ListEx.EventHandler(this.ListEx_Available_PressIcon);
			// 
			// ListEx_ImageList
			// 
			this.ListEx_ImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ListEx_ImageList.ImageStream")));
			this.ListEx_ImageList.TransparentColor = System.Drawing.Color.Transparent;
			this.ListEx_ImageList.Images.SetKeyName(0, "");
			this.ListEx_ImageList.Images.SetKeyName(1, "");
			// 
			// ListEx_Sequence
			// 
			this.ListEx_Sequence.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)));
			this.ListEx_Sequence.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
			this.ListEx_Sequence.Imgs = this.ListEx_ImageList;
			this.ListEx_Sequence.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(199)))), ((int)(((byte)(199)))));
			this.ListEx_Sequence.Location = new System.Drawing.Point(10, 28);
			this.ListEx_Sequence.Name = "ListEx_Sequence";
			this.ListEx_Sequence.ReadOnly = true;
			this.ListEx_Sequence.ShowBullets = true;
			this.ListEx_Sequence.Size = new System.Drawing.Size(120, 324);
			this.ListEx_Sequence.TabIndex = 0;
			this.ToolTip.SetToolTip(this.ListEx_Sequence, "Click on the \"-\" icon to remove the item from the list");
			this.ListEx_Sequence.PressIcon += new Lister.ListEx.EventHandler(this.ListEx_Sequence_PressIcon);
			// 
			// sandDockManager0
			// 
			this.sandDockManager0.DockingManager = TD.SandDock.DockingManager.Whidbey;
			this.sandDockManager0.OwnerForm = null;
			this.sandDockManager0.Renderer = new TD.SandDock.Rendering.EverettRenderer();
			// 
			// leftSandDock
			// 
			this.leftSandDock.Dock = System.Windows.Forms.DockStyle.Left;
			this.leftSandDock.Guid = new System.Guid("34c126c4-bb10-42ba-8322-ba00448f73c1");
			this.leftSandDock.LayoutSystem = new TD.SandDock.SplitLayoutSystem(250, 400);
			this.leftSandDock.Location = new System.Drawing.Point(0, 0);
			this.leftSandDock.Manager = this.sandDockManager0;
			this.leftSandDock.Name = "leftSandDock";
			this.leftSandDock.Size = new System.Drawing.Size(0, 398);
			this.leftSandDock.TabIndex = 5;
			// 
			// rightSandDock
			// 
			this.rightSandDock.Dock = System.Windows.Forms.DockStyle.Right;
			this.rightSandDock.Guid = new System.Guid("ab4f7f00-e57a-4ead-b8ff-f49baa6306f2");
			this.rightSandDock.LayoutSystem = new TD.SandDock.SplitLayoutSystem(250, 400);
			this.rightSandDock.Location = new System.Drawing.Point(348, 0);
			this.rightSandDock.Manager = this.sandDockManager0;
			this.rightSandDock.Name = "rightSandDock";
			this.rightSandDock.Size = new System.Drawing.Size(0, 398);
			this.rightSandDock.TabIndex = 6;
			// 
			// bottomSandDock
			// 
			this.bottomSandDock.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.bottomSandDock.Guid = new System.Guid("29d0f55e-83b5-4569-890e-1a0712353397");
			this.bottomSandDock.LayoutSystem = new TD.SandDock.SplitLayoutSystem(250, 400);
			this.bottomSandDock.Location = new System.Drawing.Point(0, 398);
			this.bottomSandDock.Manager = this.sandDockManager0;
			this.bottomSandDock.Name = "bottomSandDock";
			this.bottomSandDock.Size = new System.Drawing.Size(348, 0);
			this.bottomSandDock.TabIndex = 7;
			// 
			// topSandDock
			// 
			this.topSandDock.Dock = System.Windows.Forms.DockStyle.Top;
			this.topSandDock.Guid = new System.Guid("5bf990f7-5cdc-4110-a187-3e6c30b29a2c");
			this.topSandDock.LayoutSystem = new TD.SandDock.SplitLayoutSystem(250, 400);
			this.topSandDock.Location = new System.Drawing.Point(0, 0);
			this.topSandDock.Manager = this.sandDockManager0;
			this.topSandDock.Name = "topSandDock";
			this.topSandDock.Size = new System.Drawing.Size(348, 0);
			this.topSandDock.TabIndex = 8;
			// 
			// SongEditor
			// 
			this.Controls.Add(this.MainTabs);
			this.Controls.Add(this.leftSandDock);
			this.Controls.Add(this.rightSandDock);
			this.Controls.Add(this.bottomSandDock);
			this.Controls.Add(this.topSandDock);
			this.Name = "SongEditor";
			this.Size = new System.Drawing.Size(348, 398);
			this.MainTabs.ResumeLayout(false);
			this.General_Tab.ResumeLayout(false);
			this.General_Tab.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.Lyrics_Tab.ResumeLayout(false);
			this.LyricsTabs.ResumeLayout(false);
			this.LyricsVerse_Tab.ResumeLayout(false);
			this.LyricsChorus_Tab.ResumeLayout(false);
			this.LyricsOther_Tab.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.Sequence_Tab.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void MainTabs_SelectedIndexChanged(object sender, System.EventArgs e) {
			if (this.MainTabs.SelectedTab == this.Sequence_Tab) {
				UpdateAvailableLyrics();
			}
		}

		private void UpdateAvailableLyrics() {
			NewSong s = this.ReadControls();

			this.ListEx_Available.Items.Clear();

			if (s.SongLyrics == null) return;
			s.SongLyrics.Sort();
			this.sequenceAvailable.Clear();
			foreach (LyricsItem l in s.SongLyrics) {
				LyricsSequenceItem item = new LyricsSequenceItem(l.Type, l.Number);
				this.ListEx_Available.Add(item.ToString(), 0);
				this.sequenceAvailable.Add(item);
			}

			// TODO: Update the sequence list as well, in case the user removed a verse that was part of the sequence.
		}


		private void ListEx_Available_PressIcon(int Index) {
			this.ListEx_Sequence.Add((string)this.ListEx_Available.Items[Index], 1);
			this.sequence.Add(this.sequenceAvailable[Index]);
		}

		private void ListEx_Sequence_PressIcon(int Index) {
			this.ListEx_Sequence.Remove(Index);
			this.sequence.RemoveAt(Index);
		}

	}
}
