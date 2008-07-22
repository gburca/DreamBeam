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
    public partial class Textfield : UserControl
    {
        public Textfield()
        {
            InitializeComponent();
        }

        

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            this.Height = 10 + textBox.Lines.Count() * 13;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(this.textBox.Lines.Count().ToString());
           // MessageBox.Show(this.textBox.get);
        }
    }
}
