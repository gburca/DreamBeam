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

        public ThemeWidget() {
            InitializeComponent();
        }

        public ThemeWidget(string[] tabNames) : this() {
            setTabs(tabNames);
        }

        /// <summary>
        /// Creates the proper number of tabs and labels them according to the
        /// strings in the tabNames array.
        /// </summary>
        /// <param name="tabNames">The tab names to use</param>
        public void setTabs(string[] tabNames) {
            this.SuspendLayout();
            tabControl.Controls.Clear();

            foreach (string tn in tabNames) {
                TextFormatOptions textOpt = new TextFormatOptions();
                textOpt.Location = new Point(6, 5);
                //textOpt.Size = new Point(392, 206);

                TabPage tabPage = new TabPage();
                tabPage.Controls.Add(textOpt);
                tabPage.Text = tn;
                tabControl.Controls.Add(tabPage);
            }

            this.ResumeLayout(false);
        }

        public string[] getTabs() {
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
            }
            get {
                int tabs = tabControl.TabPages.Count;
                Theme t = new Theme(tabs);
                t.BGImagePath = BgImagePath.Text;
                for (int i = 0; i < tabs; i++) {
                    t.TextFormat[i] = getFormatControl(i).Format;
                }
                return t;
            }
        }
 
        public string[] panelNames {
            set { setTabs(value); }
            get { return getTabs(); }
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
            SongTheme theme = new SongTheme();
            theme.set(this.Theme);
            theme.SaveAs();
        }

        private void openBtn_Click(object sender, EventArgs e) {
            SongTheme theme = SongTheme.OpenFile();
            this.Theme = theme as Theme;
        }
 
    }
}
