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

		public Config Conf;
		private Language Lang = new Language();
		public MainForm _MainForm = null;

        public Options(MainForm mainForm) {
			_MainForm = mainForm;
			this.Conf = _MainForm.Config;

			InitializeComponent();
            this.DataDirectory.Text = Tools.GetAppDocPath();

			PopulateBibleCacheTab();

			string DataSetFile = Path.Combine(Tools.GetAppDocPath(), _MainForm.ConfigSet + ".dataset.config.xml");
			if (Tools.FileExists(DataSetFile)) {
				this.Options_DataSet.ReadXml( DataSetFile, XmlReadMode.ReadSchema );
			}

			SizeColumns(this.BibleConversions_dataGrid);
        }

        /// <summary>
        /// Ressourcen nach der Verwendung bereinigen
        /// </summary>
        protected override void Dispose (bool disposing) {
            if (disposing) {
                if (components != null) {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        private void HandleThemeChange() {
            IContentOperations content = _MainForm.DisplayPreview.content;
            if (content != null) {
                // First, let's clear the pre-render cache
                try {
                    (content as Content).RenderedFramesClear();
                } catch { }

                switch ((ContentType)content.GetIdentity().Type) {
                    case ContentType.Song:
                        content.ChangeTheme(this.songThemeWidget.Theme);
                        break;
                    case ContentType.PlainText:
                        content.ChangeTheme(this.sermonThemeWidget.Theme);
                        break;
                    case ContentType.BibleVerseIdx:
                    case ContentType.BibleVerseRef:
                    case ContentType.BibleVerse:
                        content.ChangeTheme(this.bibleFormatWidget.Theme);
                        break;
                }
                _MainForm.DisplayPreview.UpdateDisplay(true);
            }
        }

		private void Options_OkBtn_Click(object sender, System.EventArgs e) {
			this.Conf.Alphablending = this.Alpha_CheckBox.Checked;
			this.Conf.PreRender = this.PreRendercheckBox.Checked;
			this.Conf.BlendSpeed = (int)Speed_Updown.Value;
			this.Conf.useDirect3D = this.Direct3D_CheckBox.Checked;
			this.Conf.OutlineSize = (float)this.OutlineSize_UpDown1.Value;


			if (Conf.BeamBoxAutoPosSize && ScreenList.SelectedIndex >= 0) {
				this.Conf.BeamBoxScreenNum = ScreenList.SelectedIndex;
				Rectangle r = System.Windows.Forms.Screen.AllScreens[ScreenList.SelectedIndex].Bounds;

				this.Conf.BeamBoxPosX = r.X;
				this.Conf.BeamBoxPosY = r.Y;
				this.Conf.BeamBoxSizeX = r.Width;
				this.Conf.BeamBoxSizeY = r.Height;				
			} else {
				this.Conf.BeamBoxPosX = (int)this.BeamBox_posX.Value;
				this.Conf.BeamBoxPosY = (int)this.BeamBox_posY.Value;
				this.Conf.BeamBoxSizeX = (int)this.BeamBox_Width.Value;
				this.Conf.BeamBoxSizeY = (int)this.BeamBox_Height.Value;
			}

			if (this._MainForm.DisplayLiveLocal != null) {
				this._MainForm.DisplayLiveLocal.ChangeDisplayCoord(this.Conf);
			}

			this.Conf.SwordPath = this.Sword_PathBox.Text;
			this._MainForm.Check_SwordProject();

			if (! Tools.StringIsNullOrEmpty( this.Sword_LanguageBox.Text ) ) {
				this.Conf.BibleLang = this.Sword_LanguageBox.Text;
				SetBibleLocale(this._MainForm.bibles, this.Conf.SwordPath, this.Conf.BibleLang);
			}
			
			this.Conf.HideMouse = this.BeamBox_HideMouse.Checked;
			this.Conf.AlwaysOnTop = this.BeamBox_AlwaysOnTop.Checked;

			switch( LanguageList.SelectedIndex) {
			case 0:
				Conf.Language = "en";
				break;
			case 1:
				Conf.Language = "de";
				break;
			}

			Conf.RememberPanelLocations = this.Options_PanelLocations_checkBox.Checked;

            Conf.theme.Song.set(this.songThemeWidget.Theme);
            Conf.theme.Bible.set(this.bibleFormatWidget.Theme);
            Conf.theme.Sermon.set(this.sermonThemeWidget.Theme);
            HandleThemeChange();

			Conf.ServerAddress = this.ServerAddress.Text;
			Conf.ListeningPort = (int)this.ListeningPort.Value;
			OperatingMode oldMode = Conf.AppOperatingMode;
			if (this.OperatingMode_StandAlone.Checked) {
				Conf.AppOperatingMode = OperatingMode.StandAlone;
			} else if (this.OperatingMode_Client.Checked) {
				Conf.AppOperatingMode = OperatingMode.Client;
			} else if (this.OperatingMode_Server.Checked) {
				Conf.AppOperatingMode = OperatingMode.Server;
			}

			if (oldMode != Conf.AppOperatingMode) {
				_MainForm.InitDisplays();
			}

			Conf.Options_DataSet = this.Options_DataSet;
			Directory.CreateDirectory( Tools.GetAppDocPath() );
			this.Options_DataSet.WriteXml( Path.Combine(Tools.GetAppDocPath(), _MainForm.ConfigSet + ".dataset.config.xml"), XmlWriteMode.WriteSchema );

			Config.SerializeTo(this.Conf, Path.Combine(Tools.GetAppDocPath(), _MainForm.ConfigSet + ".config.xml"));
			this.Close();
		}

        private void Options_Cancelbtn_Click(object sender, System.EventArgs e) {
            // Restore original settings in case the user made changes
            //_MainForm.Config = (Config)Config.DeserializeFrom(new Config(),
            //    Path.Combine(Tools.GetAppDocPath(), _MainForm.ConfigSet + ".config.xml"));
            //this.Conf = _MainForm.Config;

            //HandleThemeChange();
            this.PopulateControls();
            HandleThemeChange();
            this.Close();
        }

		private void Select_Sword_Click(object sender, System.EventArgs e) {
			this.openFileDialog1.FileName = Path.Combine(this.Sword_PathBox.Text, "sword.exe");
			this.openFileDialog1.Filter = "sword.exe|sword.exe";
			if (this.openFileDialog1.ShowDialog() != DialogResult.Cancel) {
				this.Sword_PathBox.Text = this.openFileDialog1.FileName.Substring(0,this.openFileDialog1.FileName.Length-9);
				MessageBox.Show("To use the Bible, you have to restart DreamBeam.");
			}
		}

		private void Options_Load(object sender, System.EventArgs e) {
			this.PopulateControls();
		}

		private void PopulateControls() {
			this.Conf = _MainForm.Config;
			this.SetLanguage();
			this.Sword_PathBox.Text = this.Conf.SwordPath;
			this.Sword_LanguageBox.SelectedItem = this.Conf.BibleLang;
			this.OutlineSize_UpDown1.Value = (int)this.Conf.OutlineSize;
			this.BeamBox_posX.Value = (int)this.Conf.BeamBoxPosX;
			this.BeamBox_posY.Value = (int)this.Conf.BeamBoxPosY;
			this.BeamBox_Width.Value = (int)this.Conf.BeamBoxSizeX;
			this.BeamBox_Height.Value = (int)this.Conf.BeamBoxSizeY;
			this.BeamBox_HideMouse.Checked = this.Conf.HideMouse;
			this.Alpha_CheckBox.Checked = this.Conf.Alphablending;
			this.PreRendercheckBox.Checked = this.Conf.PreRender;
			this.Direct3D_CheckBox.Checked = this.Conf.useDirect3D;
			this.BeamBox_AlwaysOnTop.Checked = this.Conf.AlwaysOnTop;
			Speed_Updown.Value = this.Conf.BlendSpeed;

			switch(Conf.Language.Substring(0,2)) {
			case "en":
				LanguageList.SelectedIndex = 0;
				break;
			case "de":
				LanguageList.SelectedIndex = 1;
				break;
			}

			if(this.Conf.BeamBoxAutoPosSize) {
				this.SizePosControl.SelectedIndex = 0;
			} else {
				this.SizePosControl.SelectedIndex = 1;
			}
			GetScreens();


            if(ScreenList.Items.Count > this.Conf.BeamBoxScreenNum) {
                ScreenList.SelectedIndex = this.Conf.BeamBoxScreenNum;
            }

			this.Options_PanelLocations_checkBox.Checked = this.Conf.RememberPanelLocations;

			this.ServerAddress.Text = this.Conf.ServerAddress;
			this.ListeningPort.Value = this.Conf.ListeningPort;
			switch( this.Conf.AppOperatingMode ) {
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

			if ( !Tools.StringIsNullOrEmpty(this.Sword_LanguageBox.Text) ) {
				SetBibleLocale(this._MainForm.bibles, this.Conf.SwordPath, this.Sword_LanguageBox.Text);
			}

            this.songThemeWidget.Theme = Conf.theme.Song as Theme;
            this.bibleFormatWidget.Theme = Conf.theme.Bible as Theme;
            this.sermonThemeWidget.Theme = Conf.theme.Sermon as Theme;
        }

        private void BeamBox_ColorButton_Click(object sender, System.EventArgs e) {
            this.colorDialog1.Color = this.Conf.BackgroundColor;
			if (this.colorDialog1.ShowDialog() == DialogResult.OK) {
				this.Conf.BackgroundColor = this.colorDialog1.Color;
			}
			if (this.Conf.BackgroundColor.IsEmpty) this.Conf.BackgroundColor = Color.Black;
        }

		#region SizePosition

        public void GetScreens() {
            this.ScreenList.Items.Clear();

            // Find second monitor
            int i = 0;
            int FirstSecundary = -1;
            foreach(System.Windows.Forms.Screen s in System.Windows.Forms.Screen.AllScreens) {
                if (s == System.Windows.Forms.Screen.PrimaryScreen) {
                    this.ScreenList.Items.Add ("Primary Screen");
                    // secondary = s;
                } else {
					this.ScreenList.Items.Add ("Secondary Screen " + (i + 1).ToString());
					i++;
                    if (FirstSecundary == -1) {
                        FirstSecundary = i;
                    }
                }
                //if no Secundary Found take the Primary (in this case, the only one found)
				if (FirstSecundary == -1) {
                    FirstSecundary = 0;
                }

				if (Conf.BeamBoxScreenNum < 0) { Conf.BeamBoxScreenNum = 0; }

                if (Conf.BeamBoxAutoPosSize) {
                    if (Conf.BeamBoxScreenNum < ScreenList.Items.Count) {
                        ScreenList.SelectedIndex = Conf.BeamBoxScreenNum;
                    }
                } else {
                    ScreenList.SelectedIndex = FirstSecundary - 1;
                }

            }
        }


        private void ScreenList_SelectedIndexChanged(object sender, System.EventArgs e) {
            if(ScreenList.SelectedIndex >=  0) {
				Rectangle r = System.Windows.Forms.Screen.AllScreens[ScreenList.SelectedIndex].Bounds;
                AutoPosLabelX.Text = "X: " + r.X.ToString();
                AutoPosLabelY.Text = " Y: " + r.Y.ToString();
                AutoSizeLabelW.Text = "Width: " + r.Width.ToString();
                AutoSizeLabelH.Text = "Height: " + r.Height.ToString();
            }
        }

        private void SizePosControl_Click(object sender, System.EventArgs e) {
            if (SizePosControl.SelectedIndex == 0) {
                Conf.BeamBoxAutoPosSize = true;
            } else {
                Conf.BeamBoxAutoPosSize = false;
            }
        }


		#endregion

		#region Language
		void SetLanguage(){

			Lang.setCulture(Conf.Language);
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
			 Grafics_Outline.Text = Lang.say("Options.Graphics.Outline");
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

			BibleCache_progressBar.Value = BibleCache_progressBar.Maximum / 10;
			BibleCache_Message.Visible = true;

			if (_MainForm.bibles.Add(_MainForm.Diatheke, tr, Options_RegEx_Table, new EventHandler(this.onProgress)) ) {
				BiblesAvailable_listEx.Remove(Index);
				_MainForm.BibleText_Translations_Populate();
				BiblesCached_listEx.Add(tr, 1);
			}

			BibleCache_progressBar.Value = BibleCache_progressBar.Maximum;
			BibleCache_Message.Visible = false;
		}
		private void BiblesAvailable_listEx_DoubleClick(object sender, System.EventArgs e) {
			BiblesAvailable_listEx_PressIcon( BiblesAvailable_listEx.SelectedIndex );
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
			BiblesCached_listEx_PressIcon( BiblesCached_listEx.SelectedIndex );
		}

		#endregion


		protected void SizeColumns(DataGrid grid) {
			Graphics g = CreateGraphics();  

			DataTable dataTable = (DataTable)grid.DataSource;

			DataGridTableStyle dataGridTableStyle = new DataGridTableStyle();

			dataGridTableStyle.MappingName = dataTable.TableName;

			foreach(DataColumn dataColumn in dataTable.Columns) {
				int maxSize = 0;

				SizeF size = g.MeasureString(
					dataColumn.ColumnName,
					grid.Font
					);

				if(size.Width > maxSize)
					maxSize = (int)size.Width;

				foreach(DataRow row in dataTable.Rows) {
					size = g.MeasureString(
						row[dataColumn.ColumnName].ToString(),
						grid.Font
						);

					if(size.Width > maxSize)
						maxSize = (int)size.Width;
				}

				DataGridColumnStyle dataGridColumnStyle =  new DataGridTextBoxColumn();
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

        private void bibleFormatWidget_Load(object sender, EventArgs e) {

        }

	}
}
