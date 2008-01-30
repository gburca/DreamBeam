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
        private const string ImageFileFilter = "Image Files (*.bmp;*.jpg;*.jpeg;*.png;*.gif)|*.bmp;*.jpg;*.jpeg;*.png;*.gif|All files (*.*)|*.*";
        private Theme theme = null;

        public ThemeWidget() {
            InitializeComponent();
            BgImagePath.TextChanged += new EventHandler(ControlChanged);
        }

        public ThemeWidget(string[] tabNames) : this() {
            setTabNames(tabNames);
        }

        /// <summary>
        /// Creates the proper number of tabs and labels them according to the
        /// strings in the tabNames array.
        /// </summary>
        /// <param name="tabNames">The tab names to use</param>
        public void setTabNames(string[] tabNames) {
            this.SuspendLayout();
            tabControl.Controls.Clear();

            foreach (string tn in tabNames) {
                TextFormatOptions textOpt = new TextFormatOptions();
                textOpt.Location = new Point(0, 0);
                //textOpt.Size = new Point(392, 206);
                textOpt.ControlChangedEvent += new ControlChangeHandler(TextFormatOptions_ControlChangedEvent);

                TabPage tabPage = new TabPage();
                tabPage.Controls.Add(textOpt);
                tabPage.Text = tn;
                tabControl.Controls.Add(tabPage);
            }

            this.ResumeLayout(false);
        }

        public string[] getTabNames() {
            int tabs = tabControl.TabPages.Count;
            string[] names = new string[tabs];
            for (int i = 0; i < tabs; i++) {
                names[i] = tabControl.TabPages[i].Text;
            }
            return names;
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

                this.BgImagePath.Text = value.BGImagePath;
                for (int i = 0; i < Math.Min(value.TextFormat.Length, tabControl.TabPages.Count); i++) {
                    getFormatControl(i).Format = value.TextFormat[i];
                }
                this.theme = value;
            }
            get {
                int tabs = tabControl.TabPages.Count;
                if (theme == null) theme = new SongTheme();
                theme.BGImagePath = BgImagePath.Text;
                for (int i = 0; i < Math.Min(tabs, theme.TextFormat.Length); i++) {
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
            ofd.InitialDirectory = Path.Combine(Tools.GetAppDocPath(), "Backgrounds");
            if (ofd.ShowDialog() != DialogResult.Cancel) {
                this.BgImagePath.Text = ofd.FileName;
            }
        }

        private void saveAsBtn_Click(object sender, EventArgs e) {
            this.Theme.SaveAs();
        }

        private void openBtn_Click(object sender, EventArgs e) {
            Theme newTheme = null;

            if (this.theme.GetType() == typeof(SongTheme)) {
                newTheme = SongTheme.OpenFile();
            } else if (this.theme.GetType() == typeof(BibleTheme)) {
                newTheme = BibleTheme.OpenFile();
            } else if (this.theme.GetType() == typeof(SongTheme)) {
                newTheme = SermonTheme.OpenFile();
            }

            this.Theme = newTheme;
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
            if (ControlChangedEvent != null) {
                ControlChangedEvent(this, new ThemeEventArgs(this.Theme));
            }
        }
    }

    public class ThemeEventArgs: EventArgs
    {
        public Theme theme;
        public ThemeEventArgs(Theme t) {
            theme = t;
        }
    }
}
