using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
//using Rilling.Common.UI.Forms;
using Lister;
using DreamBeam.FileTypes;
using cs_IniHandlerDevelop;

namespace DreamBeam {

	public partial class Options : System.Windows.Forms.Form {

		private Language Lang;
		public MainForm _MainForm;
		private Color showBeamBackground;
		private Boolean loadComplete = false;
        private BackgroundWorker swordWorker;

		public Options(MainForm mainForm) {
			_MainForm = mainForm;
			Lang = mainForm.Lang;

			InitializeComponent();
            InitializeBackgroundWorker();

			this.DataDirectory.Text = DreamTools.GetAppDocPath();

			PopulateBibleCacheTab();

            string DataSetFile = DreamTools.GetDirectory(DirType.Config, _MainForm.ConfigSet + ".dataset.config.xml");
			if (DreamTools.FileExists(DataSetFile)) {
				this.Options_DataSet.ReadXml(DataSetFile, XmlReadMode.ReadSchema);
			}
            if (Options_RegEx_Table.Rows.Count > 0) {
                BibleConversions_Custom.Checked = true;
                BibleConversions_dataGrid_enable(true);
            } else {
                BibleConversions_Default.Checked = true;
                BibleConversions_dataGrid_enable(false);
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


			// We need to update all displays with the new size.
			// They use this size to generate the bitmaps.
			this._MainForm.UpdateDisplaySizes();

			config.SwordPath = this.Sword_PathBox.Text;

			if (!String.IsNullOrEmpty(this.Sword_LanguageBox.Text)) {
				config.BibleLang = this.Sword_LanguageBox.Text;
				SetBibleLocale(this._MainForm.bibles, config.SwordPath, config.BibleLang);
			}

			if (this.verseSep2L.Checked) {
				config.SongVerseSeparator = SongVerseSeparator.TwoBlankLines;
			} else {
				config.SongVerseSeparator = SongVerseSeparator.OneBlankLine;
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
			//config.theme.Song = this.songThemeWidget.Theme as SongTheme;
			//config.theme.Bible = this.bibleFormatWidget.Theme as BibleTheme;
			//config.theme.Sermon = this.sermonThemeWidget.Theme as SermonTheme;

			IContentOperations content = _MainForm.DisplayPreview.content;
			if (content != null) content.ShowRectangles = false;


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

            string s = (String)DefaultSongFormatListBox.SelectedItem;
            config.DefaultThemes.SongThemePath = Path.Combine("Themes\\",s+".SongTheme.xml");
            s = (String)DefaultSermonFormatListBox.SelectedItem;
            config.DefaultThemes.SermonThemePath = Path.Combine("Themes\\", s + ".SermonTheme.xml");
            s = (String)DefaultBibleFormatListBox.SelectedItem;
            config.DefaultThemes.BibleThemePath = Path.Combine("Themes\\", s + ".BibleTheme.xml");


			config.Options_DataSet = this.Options_DataSet;
			this.Options_DataSet.WriteXml(DreamTools.GetDirectory(DirType.Config, _MainForm.ConfigSet + ".dataset.config.xml"), XmlWriteMode.WriteSchema);

			Config.SerializeTo(config, DreamTools.GetDirectory(DirType.Config, _MainForm.ConfigSet + ".config.xml"));
			this.Close();
		}

		private void Options_Cancelbtn_Click(object sender, System.EventArgs e) {
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
			IContentOperations content = _MainForm.DisplayPreview.content;
			if (content != null) content.ShowRectangles = true;
			_MainForm.DisplayPreview.UpdateDisplay(true);

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
            this.Speed_Updown.Value = config.BlendSpeed;			
			this.PreRendercheckBox.Checked = config.PreRender;
			this.Direct3D_CheckBox.Checked = config.useDirect3D;
			this.BeamBox_AlwaysOnTop.Checked = config.AlwaysOnTop;
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

			switch (config.SongVerseSeparator) {
				case SongVerseSeparator.OneBlankLine:
					this.verseSep1L.Checked = true;
					break;
				case SongVerseSeparator.TwoBlankLines:
					this.verseSep2L.Checked = true;
					break;
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

			if (!String.IsNullOrEmpty(this.Sword_LanguageBox.Text)) {
				SetBibleLocale(this._MainForm.bibles, config.SwordPath, this.Sword_LanguageBox.Text);
			}
            this.ListThemes(config);
			//this.songThemeWidget.Theme = config.theme.Song;
			//this.bibleFormatWidget.Theme = config.theme.Bible;
			//this.sermonThemeWidget.Theme = config.theme.Sermon;

			this.loadComplete = true;
		}

		private void BeamBox_ColorButton_Click(object sender, System.EventArgs e) {
			this.colorDialog1.Color = this.showBeamBackground;
			if (this.colorDialog1.ShowDialog() == DialogResult.OK) {
				this.showBeamBackground = this.colorDialog1.Color;
			}
		}

        private void ListThemes(Config config){

            this.DefaultSongFormatListBox.Items.Clear();
            this.DefaultBibleFormatListBox.Items.Clear();
            this.DefaultSermonFormatListBox.Items.Clear();
            
            DirectoryInfo di = new DirectoryInfo(DreamTools.GetDirectory(DirType.Themes));
            
            FileInfo[] rgFiles = di.GetFiles("*.SongTheme.xml");
            foreach (FileInfo fi in rgFiles)
            {
                this.DefaultSongFormatListBox.Items.Add(Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(fi.Name)));
                if (Path.GetFileName(config.DefaultThemes.SongThemePath).ToLower() == Path.GetFileName(fi.Name).ToLower()) this.DefaultSongFormatListBox.SelectedIndex = this.DefaultSongFormatListBox.Items.Count - 1;                
            }

            rgFiles = di.GetFiles("*.BibleTheme.xml");
            foreach (FileInfo fi in rgFiles)
            {
                this.DefaultBibleFormatListBox.Items.Add(Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(fi.Name)));
                if (Path.GetFileName(config.DefaultThemes.BibleThemePath).ToLower() == Path.GetFileName(fi.Name).ToLower()) this.DefaultBibleFormatListBox.SelectedIndex = this.DefaultBibleFormatListBox.Items.Count - 1;                                
            }

            rgFiles = di.GetFiles("*.SermonTheme.xml");
            foreach (FileInfo fi in rgFiles)
            {
                this.DefaultSermonFormatListBox.Items.Add(Path.GetFileNameWithoutExtension((Path.GetFileNameWithoutExtension(fi.Name))));
                if (Path.GetFileName(config.DefaultThemes.SermonThemePath).ToLower() == Path.GetFileName(fi.Name).ToLower()) this.DefaultSermonFormatListBox.SelectedIndex = this.DefaultSermonFormatListBox.Items.Count - 1;                                
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
			Misc_Tab.Text = Lang.say("Options.Tabs.Misc");
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

			foreach (string book in SwordW.Instance().getBibles()) {
				available.Add(book);
			}

			available = available - cached;

			foreach (string book in available.Keys) {
				BiblesAvailable_listEx.Add(book, 0);
			}

        }

        #region Sword Worker Thread

        // This event handler is where the actual,
        // potentially time-consuming work is done.
        private void swordWorker_DoWork(object sender,
            DoWorkEventArgs e) {
            // Get the BackgroundWorker that raised this event.
            BackgroundWorker worker = sender as BackgroundWorker;

            // Assign the result of the computation
            // to the Result property of the DoWorkEventArgs
            // object. This is will be available to the 
            // RunWorkerCompleted eventhandler.
            WorkerArgs wArgs = (WorkerArgs)e.Argument;
            e.Result = _MainForm.bibles.Add(worker, e, wArgs.tr, wArgs.replacements);
        }

        private void swordWorker_RunWorkerCompleted(
            object sender,
            RunWorkerCompletedEventArgs e) {

            bool added;

            // First, handle the case where an exception was thrown.
            if (e.Error != null) {
                MessageBox.Show(e.Error.Message);
                added = false;
            } else if (e.Cancelled) {
                // Next, handle the case where the user canceled 
                // the operation.
                // Note that due to a race condition in 
                // the DoWork event handler, the Cancelled
                // flag may not have been set, even though
                // CancelAsync was called.
                added = false;
            } else {
                // Finally, handle the case where the operation 
                // succeeded.

                // e.Result.ToString();
                added = (bool)e.Result;
            }

            if (added) {
                int Index = BiblesAvailable_listEx.SelectedIndex;
                string tr = BiblesAvailable_listEx.Items[Index].ToString();

                BiblesAvailable_listEx.Remove(Index);
                _MainForm.BibleText_Translations_Populate();
                BiblesCached_listEx.Add(tr, 1);
            }

            BibleCache_progressBar.Value = BibleCache_progressBar.Maximum;
            BibleCache_Message.Visible = false;
            BibleCache_progressBar.Visible = false;
        }

        // This event handler updates the progress bar.
        private void swordWorker_ProgressChanged(object sender,
            ProgressChangedEventArgs e) {
            BibleCache_progressBar.Value = e.ProgressPercentage;
        }

        // Set up the BackgroundWorker object by 
        // attaching event handlers. 
        private void InitializeBackgroundWorker() {
            swordWorker = new BackgroundWorker();

            swordWorker.WorkerReportsProgress = true;
            swordWorker.WorkerSupportsCancellation = true;

            swordWorker.DoWork +=
                new DoWorkEventHandler(swordWorker_DoWork);

            swordWorker.RunWorkerCompleted +=
                new RunWorkerCompletedEventHandler(
            swordWorker_RunWorkerCompleted);

            swordWorker.ProgressChanged +=
                new ProgressChangedEventHandler(
            swordWorker_ProgressChanged);
        }

        struct WorkerArgs {
            public string tr;
            public System.Data.DataTable replacements;
        }
        #endregion

        /// <summary>
		/// Adds a bible translation to the cache
		/// </summary>
		/// <param name="Index"></param>
		private void BiblesAvailable_listEx_PressIcon(int Index) {
			BibleCache_Message.Visible = true;
            BibleCache_progressBar.Visible = true;
			BibleCache_progressBar.Value = 0;

            WorkerArgs wArgs;
            wArgs.tr = BiblesAvailable_listEx.Items[Index].ToString();

            if (BibleConversions_Custom.Checked) {
                wArgs.replacements = Options_RegEx_Table;
            } else {
                wArgs.replacements = null;
            }

            swordWorker.RunWorkerAsync(wArgs);
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
            if (dataTable == null) return; // No entries in the grid

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

        /// <summary>
        /// When displaying bible verses (on the live screen), the reference shown comes
        /// from the bible.BibleBooks[?].Long (or .Short) fields. This code sets those
        /// fields for all bibles in the library to the names found in the user selected
        /// Sword locales.d file.
        /// </summary>
        /// <param name="Bibles"></param>
        /// <param name="SwordPath"></param>
        /// <param name="locale"></param>
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

		

        private void BibleConversions_dataGrid_enable(bool enabled) {
            if (enabled) {
                BibleConversions_dataGrid.DataSource = Options_RegEx_Table;
                BibleConversions_dataGrid.Enabled = true;
                BibleConversions_dataGrid.ResetAlternatingBackColor();
                BibleConversions_dataGrid.ResetBackColor();
            } else {
                // Can't get these colors to work, so we'll null out the data source
                BibleConversions_dataGrid.AlternatingBackColor = Color.DarkGray;
                BibleConversions_dataGrid.BackColor = Color.DarkGray;
                BibleConversions_dataGrid.DataSource = null;
                BibleConversions_dataGrid.Enabled = false;
            }
        }

        private void BibleConversions_Type_Click(object sender, EventArgs e) {
            if (sender.Equals(BibleConversions_Default)) {
                BibleConversions_dataGrid_enable(false);
            } else {
                BibleConversions_dataGrid_enable(true);
            }
        }

        private void openDataDirectoryButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer", DreamTools.GetDirectory(DirType.DataRoot));
        }

	}
}
