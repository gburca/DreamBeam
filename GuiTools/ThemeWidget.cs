using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DreamBeam {
    public partial class ThemeWidget : UserControl {
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

        /// <summary>
        /// Returns the TextFormatOptions control from the i-th tab
        /// </summary>
        /// <param name="i">The tab page to return the control of</param>
        /// <returns></returns>
        public TextFormatOptions getFormatControl(int i) {
            return (tabControl.TabPages[i].Controls[0] as TextFormatOptions);
        }

        public Theme Theme {
            set {
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
    }
}
