using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DreamBeam {
	public partial class ThemeWidget : UserControl {
        public bool changingControls = false;
        private const string ImageFileFilter = "Image Files (*.bmp;*.jpg;*.jpeg;*.png;*.gif)|*.bmp;*.jpg;*.jpeg;*.png;*.gif|All files (*.*)|*.*";
		private System.Type themeType;
        private bool _usedesign;
        private bool _settextposition = false;
        private string _ThemePath = "";
        public string BGImagePath
        {
            get { return this.BGImagePathLabel.Text; }
            set
            {
                if (this._usedesign) this.BGImagePathLabel.Text = value;
            }
        }

        public string ThemePath
        {
            get { return _ThemePath; }
            set{
                _ThemePath = value;
                if (value == "") value = "None";
                if (!_usedesign) { this.ThemeLabel.Text = Path.GetFileNameWithoutExtension(value); }
                else
                {
                    this.ThemeLabel.Text = "None";
                    _ThemePath = "";
                }
            }
        }

        public bool UseDesign
        {
            get { return _usedesign; }
            set { 
                if (value) {
                    this.Design_checkBox.Checked = true;
                    this.ThemePath = "";
                }
                else{
                    this.Design_checkBox.Checked = false;                    
                }
                this.DesignCheckText();
                _usedesign = value;
            }
        }
        public bool SetTextPosition
        {
            get { return _settextposition; }
            set
            {
                if (value)
                {
                    this.SetTextPositionButton.Checked = true;
                    
                }
                else
                {
                    this.SetTextPositionButton.Checked = false;
                }

                _settextposition = value;
            }
        }

		public ThemeWidget() {
			InitializeComponent();		
            //this.grouper1.MouseMove += new EventHandler(MouseInside);
            MouseInsideEventMaker(this);

		}

		public ThemeWidget(string[] tabNames)
			: this() {
			setTabNames(tabNames);
		}

		/// <summary>
		/// Creates the proper number of tabs and labels them according to the
		/// strings in the tabNames array.
		/// </summary>
		/// <param name="tabNames">The tab names to use</param>
		public void setTabNames(string[] tabNames) {
			this.SuspendLayout();
            changingControls = true;
			tabControl.Controls.Clear();

			foreach (string tn in tabNames) {
				TextFormatOptions textOpt = new TextFormatOptions();
				textOpt.Location = new Point(0, 0);
				//textOpt.Size = new Point(392, 206);
				textOpt.ControlChangedEvent += new ControlChangeHandler(TextFormatOptions_ControlChangedEvent);
                
                textOpt.MouseInsideEvent += new MouseInsideHandler(MouseInside);

				TabPage tabPage = new TabPage();
				tabPage.Controls.Add(textOpt);
				tabPage.Text = tn;
				tabControl.Controls.Add(tabPage);
			}

			this.ResumeLayout(false);
            changingControls = false;
		}

        private void MouseInsideEventMaker(Control obj)
        {
            obj.MouseMove += new MouseEventHandler(MouseInside);
            obj.MouseHover += new EventHandler(MouseInside);
            foreach (Control x in obj.Controls)
            {
                MouseInsideEventMaker(x);
            }

        }

		public string[] getTabNames() {
			int tabs = tabControl.TabPages.Count;
			string[] names = new string[tabs];
			for (int i = 0; i < tabs; i++) {
				names[i] = tabControl.TabPages[i].Text;
			}
			return names;
		}

        public int getSelectedTab()
        {
            return tabControl.SelectedIndex;
        }

		/// <summary>
		/// Returns the TextFormatOptions control from the i-th tab
		/// </summary>
		/// <param name="i">The tab page to return the control of</param>
		/// <returns></returns>
		public TextFormatOptions getFormatControl(int i) {
			return (tabControl.TabPages[i].Controls[0] as TextFormatOptions);
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public Theme Theme {
			set {
				if (value == null | value.TextFormat == null) return;

                this.changingControls = true;
				this.themeType = value.GetType();
				if (value.BGImagePath != null) {
                    this.BGImagePathLabel.Text = Tools.GetRelativePath(DirType.DataRoot, value.BGImagePath);                    
				} else {
                    this.BGImagePathLabel.Text = "";
				}
                if(!UseDesign) this.ThemePath = value.ThemeFile;
				for (int i = 0; i < Math.Min(value.TextFormat.Length, tabControl.TabPages.Count); i++) {
					getFormatControl(i).Format = value.TextFormat[i];
				}
                this.changingControls = false;
			}
            get
            {                
                int tabs = tabControl.TabPages.Count;
                Theme theme;
                if (this.themeType != null)
                {
                    theme = (Theme)this.themeType.GetConstructor(new System.Type[0]).Invoke(new object[0]);
                }
                else
                {
                    theme = new SongTheme();
                }
                theme.BGImagePath = Tools.GetRelativePath(DirType.DataRoot, BGImagePathLabel.Text);
                theme.ThemeFile = this.ThemePath;
                for (int i = 0; i < Math.Min(tabs, theme.TextFormat.Length); i++)
                {
                    theme.TextFormat[i] = getFormatControl(i).Format;
                }
                return theme;
            }
		}

		public string[] TabNames {
			set { setTabNames(value); }
			get { return getTabNames(); }
		}

		private void bgImageBrowse_Click(object sender, EventArgs e) {
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Filter = ImageFileFilter;
			ofd.InitialDirectory = Tools.GetDirectory(DirType.Backgrounds);
			if (ofd.ShowDialog() != DialogResult.Cancel) {
                this.BGImagePathLabel.Text = Tools.GetRelativePath(DirType.DataRoot, ofd.FileName);
			}
		}

		private void saveAsBtn_Click(object sender, EventArgs e) {
			this.Theme.SaveAs();
		}

        public void LoadFile(Type _themetype, string path)
        {
            this.themeType = _themetype;
            if (File.Exists(path))
            {
                Theme t = (Theme)Theme.DeserializeFrom(themeType, path);
                if (t != null)
                {
                    this.Theme = t;
                    this.UseDesign = false;
                    this.ThemePath = t.ThemeFile;
                    NotifyControlChangeListeners();
                }
            }
        }

		private void openBtn_Click(object sender, EventArgs e) {			            
            Theme t = (Theme)this.themeType.GetMethod("OpenFile").Invoke(null, null);            
            try
            {                
                if (t != null)
                {
                    this.Theme = t;
                    this.UseDesign = false;
                    this.ThemePath = t.ThemeFile;
                    NotifyControlChangeListeners();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Theme not loaded: " + ex.Message);
            }            
		}


		
        [Category("Action")]
        [Description("Fires when mouse is in area.")]
        public event EventHandler MouseInsideEvent;

        protected void MouseInside(Object sender, EventArgs e)
        {

            NotifyMouseInsideListeners();

        }
        public void MouseInside()
        {
            NotifyMouseInsideListeners();
        }

        protected void NotifyMouseInsideListeners()
        {
            if (MouseInsideEvent != null)
            {
                MouseInsideEvent(this, new EventArgs());
            }
        }

        [Category("Action")]
        [Description("Fires when a theme setting is changed.")]
        public event EventHandler ControlChangedEvent;

		protected void ControlChanged(Object sender, EventArgs e) {
            
                NotifyControlChangeListeners();
            
		}

		public void TextFormatOptions_ControlChangedEvent() {
			// Pass on the TextFormatOptions change event.            
            NotifyControlChangeListeners();
		}

		protected void NotifyControlChangeListeners() {
            
            if (!this.textFormatOptions1.changingControls & ! this.changingControls)
            {

                if (this.themeType == null) MessageBox.Show("Error: Themetype not defined");
                if (ControlChangedEvent != null && this.themeType != null)
                {
                    
                    ControlChangedEvent(this, new ThemeEventArgs(this.Theme));
                }
            }
		}

        private void DesignCheckText()
        {
            if (this.Design_checkBox.Checked)
            {
                this.Design_checkBox.Text = "    Individual Design (on)";                
                this.TabContainerPanel.Show();
            }
            if (!this.Design_checkBox.Checked)
            {
                this.Design_checkBox.Text = "    Individual Design (off)";                
                this.TabContainerPanel.Hide();
            }
        }

        private void Design_checkBox_Click(object sender, EventArgs e)
        {
            //this.DesignCheckText();
            this.UseDesign = Design_checkBox.Checked;
            NotifyControlChangeListeners();
        }

        private void ribbonMenuButton1_Click(object sender, EventArgs e)
        {
            this.SetTextPosition = this.SetTextPositionButton.Checked;
            NotifyControlChangeListeners();
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            NotifyControlChangeListeners();
        }

      




	}

	public class ThemeEventArgs : EventArgs {
		public Theme theme;
		public ThemeEventArgs(Theme t) {
			theme = t;
		}
	}
}
