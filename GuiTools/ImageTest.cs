using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DreamBeam
{
    public partial class ImageTest : Form
    {
        public ImageTest()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.imagePanel.ShowAllPositions();
        }
    }
}
