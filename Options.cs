using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Rilling.Common.UI.Forms;
using Lister;
using DreamBeam.FileTypes;
using cs_IniHandlerDevelop;

namespace DreamBeam {

	public partial class Options : System.Windows.Forms.Form {

		private Language Lang = new Language();
		public MainForm _MainForm;
		private Color showBeamBackground;
		private Boolean loadComplete = false;

		public Options(MainForm mainForm) {
			_MainForm = mainForm;

			InitializeComponent();
			this.DataDirectory.Text = Tools.GetAppDocPath();

			PopulateBibleCacheTab();

			string DataSetFile = Tools.GetDirectory(DirType.Config, _MainForm.ConfigSet + ".dataset.config.xml");
			if (Tools.FileExists(DataSetFile)) {
				this.Options_DataSet.ReadXml(DataSetFile, XmlReadMode.ReadSchema);
			}

			SizeColumns(this.BibleConversions_dataGrid);
		}

		/// <summary>
		/// Ressourcen nach der Verwendung bereinigen
		/// </summary>
		protected override void Dispose(bool disposing) {
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		/// <summary>
		/// By default we handle theme changes by using the contents of the widgets
		/// </summary>
		private void HandleThemeChange() {
			/* This will get called 3-times on load, as each theme is configured.
			 * After the first widget is configured, the yet-unconfigured ones will
			 * return a "default" SongTheme which can't be typecast to a Bible or SermonTheme.
			 */
			if (!loadComplete) return;
			HandleThemeChange(
				this.songThemeWidget.Theme as SongTheme,
				this.bibleFormatWidget.Theme as BibleTheme,
				this.sermonThemeWidget.Theme as SermonTheme);
		}

		/// <summary>
		/// Propagates the provided themes to the preview window content
		/// </summary>
		/// <param name="song"></param>
		/// <param name="bible"></param>
		/// <param name="sermon"></param>
		private void HandleThemeChange(SongTheme song, BibleTheme bible, SermonTheme sermon) {
			IContentOperations content = _MainForm.DisplayPreview.content;
			if (content != null) {
				// First, let's clear the pre-render cache
				try {
					(content as Content).RenderedFramesClear();
				} catch { }

				switch ((ContentType)content.GetIdentity().Type) {
					case ContentType.Song:
						content.Theme = song;
						break;
					case ContentType.PlainText:
						content.Theme = sermon;
						break;
					case ContentType.BibleVerseIdx:
					case ContentType.BibleVerseRef:
					case ContentType.BibleVerse:
						content.Theme = bible;
						break;
				}
				_MainForm.DisplayPreview.UpdateDisplay(true);
			}
		}

		private void Options_OkBtn_Click(object sender, System.EventArgs e) {
			Config config = new Config();
			this._MainForm.Config = config;

			config.Alphablending = this.Alpha_CheckBox.Checked;
			config.PreRender = this.PreRendercheckBox.Checked;
			config.BlendSpeed = (int)Speed_Updown.Value;
			config.useDirect3D = this.Direct3D_CheckBox.Checked;

			config.BackgroundColor = this.showBeamBackground;
			if (config.BackgroundColor.IsEmpty) config.BackgroundColor = Color.Black;

			if (SizePosControl.SelectedIndex == 0) {
				config.BeamBoxAutoPosSize = true;
			} else {
				config.BeamBoxAutoPosSize = false;
			}

			if (config.BeamBoxAutoPosSize && ScreenList.SelectedIndex >= 0) {
				config.BeamBoxScreenNum = ScreenList.SelectedIndex;
				Rectangle r = System.Windows.Forms.Screen.AllScreens[ScreenList.SelectedIndex].Bounds;

				config.BeamBoxPosX = r.X;
				config.BeamBoxPosY = r.Y;
				config.BeamBoxSizeX = r.Width;
				config.BeamBoxSizeY = r.Height;
			} else {
				config.BeamBoxPosX = (int)this.BeamBox_posX.Value;
				config.BeamBoxPosY = (int)this.BeamBox_posY.Value;
				config.BeamBoxSizeX = (int)this.BeamBox_Width.Value;
				config.BeamBoxSizeY = (int)this.BeamBox_Height.Value;
			}

			if (this._MainForm.DisplayLiveLocal != null) {
				this._MainForm.DisplayLiveLocal.ChangeDisplayCoord(config);
			}

			config.SwordPath = this.Sword_PathBox.Text;
			this._MainForm.Check_SwordProject(config);

			if (!String.IsNullOrEmpty(this.Sword_LanguageBox.Text)) {
				config.BibleLang = this.Sword_LanguageBox.Text;
				SetBibleLocale(this._MainForm.bibles, config.SwordPath, config.BibleLang);
			}

			config.HideMouse = this.BeamBox_HideMouse.Checked;
			config.AlwaysOnTop = this.BeamBox_AlwaysOnTop.Checked;

			switch (LanguageList.SelectedIndex) {
				case 0:
					config.Language = "en";
					break;
				case 1:
					config.Language = "de";
					break;
			}

			config.RememberPanelLocations = this.Options_PanelLocations_checkBox.Checked;

			config.theme.Song = this.songThemeWidget.Theme as SongTheme;
			config.theme.Bible = this.bibleFormatWidget.Theme as BibleTheme;
			config.theme.Sermon = this.sermonThemeWidget.Theme as SermonTheme;
			HandleThemeChange();

			config.ServerAddress = this.ServerAddress.Text;
			config.ListeningPort = (int)this.ListeningPort.Value;
			OperatingMode oldMode = config.AppOperatingMode;
			if (this.OperatingMode_StandAlone.Checked) {
				config.AppOperatingMode = OperatingMode.StandAlone;
			} else if (this.OperatingMode_Client.Checked) {
				config.AppOperatingMode = OperatingMode.Client;
			} else if (this.OperatingMode_Server.Checked) {
				config.AppOperatingMode = OperatingMode.Server;
			}

			if (oldMode != config.AppOperatingMode) {
				_MainForm.InitDisplays();
			}

			config.Options_DataSet = this.Options_DataSet;
			this.Options_DataSet.WriteXml(Tools.GetDirectory(DirType.Config, _MainForm.ConfigSet + ".dataset.config.xml"), XmlWriteMode.WriteSchema);

			Config.SerializeTo(config, Tools.GetDirectory(DirType.Config, _MainForm.ConfigSet + ".config.xml"));
			this.Close();
		}

		private void Options_Cancelbtn_Click(object sender, System.EventArgs e) {
			ComboTheme t = _MainForm.Config.theme;
			HandleThemeChange(t.Song, t.Bible, t.Sermon);
			this.Close();
		}

		private void Select_Sword_Click(object sender, System.EventArgs e) {
			this.openFileDialog1.FileName = Path.Combine(this.Sword_PathBox.Text, "sword.exe");
			this.openFileDialog1.Filter = "sword.exe|sword.exe";
			if (this.openFileDialog1.ShowDialog() != DialogResult.Cancel) {
				this.Sword_PathBox.Text = this.openFileDialog1.FileName.Substring(0, this.openFileDialog1.FileName.Length - 9);
				MessageBox.Show("To use the Bible, you have to restart DreamBeam.");
			}
		}

		private void Options_Load(object sender, System.EventArgs e) {
			this.PopulateControls(_MainForm.Config);
		}

		private void PopulateControls(Config config) {
			this.SetLanguage(config);
			this.Sword_PathBox.Text = config.SwordPath;
			this.Sword_LanguageBox.SelectedItem = config.BibleLang;
			this.BeamBox_posX.Value = (int)config.BeamBoxPosX;
			this.BeamBox_posY.Value = (int)config.BeamBoxPosY;
			this.BeamBox_Width.Value = (int)config.BeamBoxSizeX;
			this.BeamBox_Height.Value = (int)config.BeamBoxSizeY;
			this.BeamBox_HideMouse.Checked = config.HideMouse;
			this.Alpha_CheckBox.Checked = config.Alphablending;
			this.PreRendercheckBox.Checked = config.PreRender;
			this.Direct3D_CheckBox.Checked = config.useDirect3D;
			this.BeamBox_AlwaysOnTop.Checked = config.AlwaysOnTop;
			this.Speed_Updown.Value = config.BlendSpeed;
			this.showBeamBackground = config.BackgroundColor;


			switch (config.Language.Substring(0, 2)) {
				case "en":
					LanguageList.SelectedIndex = 0;
					break;
				case "de":
					LanguageList.SelectedIndex = 1;
					break;
			}

			if (config.BeamBoxAutoPosSize) {
				this.SizePosControl.SelectedIndex = 0;
			} else {
				this.SizePosControl.SelectedIndex = 1;
			}
			GetScreens(config);


			if (ScreenList.Items.Count > config.BeamBoxScreenNum) {
				ScreenList.SelectedIndex = config.BeamBoxScreenNum;
			}

			this.Options_PanelLocations_checkBox.Checked = config.RememberPanelLocations;

			this.ServerAddress.Text = config.ServerAddress;
			this.ListeningPort.Value = config.ListeningPort;
			switch (config.AppOperatingMode) {
				case OperatingMode.StandAlone:
					this.OperatingMode_StandAlone.Checked = true;
					break;
				case OperatingMode.Client:
					this.OperatingMode_Client.Checked = true;
					break;
				case OperatingMode.Server:
					this.OperatingMode_Server.Checked = true;
					break;
			}

			if (!Tools.StringIsNullOrEmpty(this.Sword_LanguageBox.Text)) {
				SetBibleLocale(this._MainForm.bibles, config.SwordPath, this.Sword_LanguageBox.Text);
			}

			this.songThemeWidget.Theme = config.theme.Song;
			this.bibleFormatWidget.Theme = config.theme.Bible;
			this.sermonThemeWidget.Theme = config.theme.Sermon;

			this.loadComplete = true;
		}

		private void BeamBox_ColorButton_Click(object sender, System.EventArgs e) {
			this.colorDialog1.Color = this.showBeamBackground;
			if (this.colorDialog1.ShowDialog() == DialogResult.OK) {
				this.showBeamBackground = this.colorDialog1.Color;
			}
		}

		#region SizePosition

		public void GetScreens(Config config) {
			this.ScreenList.Items.Clear();

			// Find second monitor
			int i = 0;
			int FirstSecondary = -1;
			foreach (System.Windows.Forms.Screen s in System.Windows.Forms.Screen.AllScreens) {
				if (s == System.Windows.Forms.Screen.PrimaryScreen) {
					this.ScreenList.Items.Add("Primary Screen");
					// secondary = s;
				} else {
					this.ScreenList.Items.Add("Secondary Screen " + (i + 1).ToString());
					i++;
					if (FirstSecondary == -1) {
						FirstSecondary = i;
					}
				}
				//if no Secundary Found take the Primary (in this case, the only one found)
				if (FirstSecondary == -1) {
					FirstSecondary = 0;
				}

				if (config.BeamBoxScreenNum < 0) { config.BeamBoxScreenNum = 0; }

				if (config.BeamBoxAutoPosSize) {
					if (config.BeamBoxScreenNum < ScreenList.Items.Count) {
						ScreenList.SelectedIndex = config.BeamBoxScreenNum;
					}
				} else {
					ScreenList.SelectedIndex = FirstSecondary - 1;
				}

			}
		}


		private void ScreenList_SelectedIndexChanged(object sender, System.EventArgs e) {
			if (ScreenList.SelectedIndex >= 0) {
				Rectangle r = System.Windows.Forms.Screen.AllScreens[ScreenList.SelectedIndex].Bounds;
				AutoPosLabelX.Text = "X: " + r.X.ToString();
				AutoPosLabelY.Text = " Y: " + r.Y.ToString();
				AutoSizeLabelW.Text = "Width: " + r.Width.ToString();
				AutoSizeLabelH.Text = "Height: " + r.Height.ToString();
			}
		}


		#endregion

		#region Language
		void SetLanguage(Config config) {

			Lang.setCulture(config.Language);
			#region Tabs
			Common_Tab.Text = Lang.say("Options.Tabs.Common");
			Graphics_tab.Text = Lang.say("Options.Tabs.Graphics");
			BeamBox_tab.Text = Lang.say("Options.Tabs.BeamBox");
			Bible_Tab.Text = Lang.say("Options.Tabs.Bible");
			#endregion

			#region Common
			LanguageBox.Text = Lang.say("Options.Common.Language");
			TranslateLabel.Text = Lang.say("Options.Common.Translate");
			#endregion

			#region Graphics
			Alpha_groupBox1.Text = Lang.say("Options.Graphics.AlphaBlending");
			Alpha_CheckBox.Text = Lang.say("Options.Graphics.AlphaCheckBox");
			SpeedLabel.Text = Lang.say("Options.Graphics.AlphaSpeed");

			PreRenderBox.Text = Lang.say("Options.Graphics.PreRenderTitle");
			PreRendercheckBox.Text = Lang.say("Options.Graphics.PreRender");
			PreRenderLabel.Text = Lang.say("Options.Graphics.PreRenderDescription");
			#endregion

			#region ProjectorWindow
			Position_Title_GroupBox.Text = Lang.say("Options.BeamBox.PositionTitle");

			BeamBox_PosBox.Text = Lang.say("Options.BeamBox.PositionGroup");
			Position_groupBox4.Text = Lang.say("Options.BeamBox.PositionGroup");

			BeamBox_SizeBox.Text = Lang.say("Options.BeamBox.SizeGroup");
			Size_groupBox5.Text = Lang.say("Options.BeamBox.SizeGroup");

			Mouse_groupBox2.Text = Lang.say("Options.BeamBox.MouseGroup");
			BeamBox_HideMouse.Text = Lang.say("Options.BeamBox.HideMouseCheckBox");
			BeamBox_AlwaysOnTop.Text = Lang.say("Options.BeamBox.OnTopCheckBox");
			Background_groupBox3.Text = Lang.say("Options.BeamBox.BackgroundGroup");
			BeamBox_ColorButton.Text = Lang.say("Options.BeamBox.BGColorButton");
			tabPage1.Text = Lang.say("Options.BeamBox.tabpage1");
			tabPage2.Text = Lang.say("Options.BeamBox.tabpage2");
			WidthLabel.Text = Lang.say("Options.BeamBox.Width");
			HeightLabel.Text = Lang.say("Options.BeamBox.Height");
			#endregion

			#region Bible
			SwordFolderGroupBox.Text = Lang.say("Options.Bible.SwordFolderGroup");
			Select_Sword.Text = Lang.say("Options.Bible.SwordFolderButton");
			SwordLangGroupBox.Text = Lang.say("Options.Bible.SwordLanguage");
			#endregion

			#region Buttons
			Options_Okaybtn.Text = Lang.say("Options.Okay");
			Options_Cancelbtn.Text = Lang.say("Options.Cancel");
			#endregion
		}
		#endregion

		#region BibleCache tab

		public void PopulateBibleCacheTab() {
			Extensions.Set available = new Extensions.Set();
			Extensions.Set cached = new Extensions.Set();

			BiblesAvailable_listEx.Items.Clear();
			BiblesCached_listEx.Items.Clear();

			foreach (string book in _MainForm.bibles.Translations()) {
				cached.Add(book);
				BiblesCached_listEx.Add(book, 1);
			}

			foreach (string book in _MainForm.DiathekeBooks(true)) {
				available.Add(book);
			}

			available = available - cached;

			foreach (string book in available.Keys) {
				BiblesAvailable_listEx.Add(book, 0);
			}

		}

		private void onProgress(object sender, EventArgs sent) {
			Console.WriteLine("onProgress");
			if (BibleCache_progressBar.Value == BibleCache_progressBar.Maximum) {
				BibleCache_progressBar.Value = BibleCache_progressBar.Minimum;
			} else {
				BibleCache_progressBar.Value++;
			}
		}

		/// <summary>
		/// Adds a bible translation to the cache
		/// 
		/// TODO: Make use of the progress bar because this is a very slow process.
		/// </summary>
		/// <param name="Index"></param>
		private void BiblesAvailable_listEx_PressIcon(int Index) {
			string tr = BiblesAvailable_listEx.Items[Index].ToString();

			BibleCache_Message.Visible = true;
			BibleCache_progressBar.Value = BibleCache_progressBar.Maximum / 10;

			if (_MainForm.bibles.Add(_MainForm.Diatheke, tr, Options_RegEx_Table, new EventHandler(this.onProgress))) {
				BiblesAvailable_listEx.Remove(Index);
				_MainForm.BibleText_Translations_Populate();
				BiblesCached_listEx.Add(tr, 1);
			}

			BibleCache_progressBar.Value = BibleCache_progressBar.Maximum;
			BibleCache_Message.Visible = false;
		}
		private void BiblesAvailable_listEx_DoubleClick(object sender, System.EventArgs e) {
			BiblesAvailable_listEx_PressIcon(BiblesAvailable_listEx.SelectedIndex);
		}

		/// <summary>
		/// Removes a bible translation from the cache
		/// </summary>
		/// <param name="Index"></param>
		private void BiblesCached_listEx_PressIcon(int Index) {
			string tr = BiblesCached_listEx.Items[Index].ToString();
			_MainForm.bibles.Remove(tr);
			BiblesCached_listEx.Remove(Index);
			BiblesAvailable_listEx.Add(tr);
			_MainForm.BibleText_Translations_Populate();
		}
		private void BiblesCached_listEx_DoubleClick(object sender, System.EventArgs e) {
			BiblesCached_listEx_PressIcon(BiblesCached_listEx.SelectedIndex);
		}

		#endregion


		protected void SizeColumns(DataGrid grid) {
			Graphics g = CreateGraphics();

			DataTable dataTable = (DataTable)grid.DataSource;

			DataGridTableStyle dataGridTableStyle = new DataGridTableStyle();

			dataGridTableStyle.MappingName = dataTable.TableName;

			foreach (DataColumn dataColumn in dataTable.Columns) {
				int maxSize = 0;

				SizeF size = g.MeasureString(
					dataColumn.ColumnName,
					grid.Font
					);

				if (size.Width > maxSize)
					maxSize = (int)size.Width;

				foreach (DataRow row in dataTable.Rows) {
					size = g.MeasureString(
						row[dataColumn.ColumnName].ToString(),
						grid.Font
						);

					if (size.Width > maxSize)
						maxSize = (int)size.Width;
				}

				DataGridColumnStyle dataGridColumnStyle = new DataGridTextBoxColumn();
				dataGridColumnStyle.MappingName = dataColumn.ColumnName;
				dataGridColumnStyle.HeaderText = dataColumn.ColumnName;
				dataGridColumnStyle.Width = maxSize + 5;
				dataGridTableStyle.GridColumnStyles.Add(dataGridColumnStyle);
			}
			grid.TableStyles.Add(dataGridTableStyle);

			g.Dispose();
		}

		public static void SetBibleLocale(BibleLib Bibles, string SwordPath, string locale) {
			string localeDir = Path.Combine(SwordPath, "locales.d");
			string longFile = Path.Combine(localeDir, locale + ".conf");
			string abbrFile = Path.Combine(localeDir, locale + "_abbr.conf");
			IniStructure longNames = IniStructure.ReadIni(longFile);
			IniStructure abbrNames = IniStructure.ReadIni(abbrFile);

			if (longNames == null) return;
			foreach (string translation in Bibles.Translations()) {
				BibleVersion bible = Bibles[translation];
				int i = 0;
				foreach (string book in BibleVersion.SwordBookNames) {
					string localizedName = longNames.GetValue("Text", book);
					bible.BibleBooks[i].Long = longNames.GetValue("Text", book);
					if (abbrNames != null) {
						bible.BibleBooks[i].Short = abbrNames.GetValue("Text", book);
					} else {
						bible.BibleBooks[i].Short = longNames.GetValue("Text", book);
					}
					i++;
				}
			}
		}

		private void songThemeWidget_ControlChangedEvent(object sender, EventArgs e) {
			HandleThemeChange();
		}

		private void bibleFormatWidget_ControlChangedEvent(object sender, EventArgs e) {
			HandleThemeChange();
		}

		private void sermonThemeWidget_ControlChangedEvent(object sender, EventArgs e) {
			HandleThemeChange();
		}

	}
}
