using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class SongTextEditor : UserControl
    {
        public SongTextEditor()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            Textfield textfield = new Textfield();
            textfield.Dock = DockStyle.Top;
            //textbox.Multiline = true;
            //textbox.Height = 150;
            this.panel1.Controls.Add(textfield);
        }
    }
}
