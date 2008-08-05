using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace DreamBeam
{
    public partial class ColoredTextBoxPanel : UserControl
    {
        public ColoredTextBoxPanel()
        {
            InitializeComponent();
        }

        
    }

    public class RichEdit50 : RichTextBox
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr LoadLibrary(string lpFileName);

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams prams = base.CreateParams;
                if (LoadLibrary("msftedit.dll") != IntPtr.Zero)
                {
                    prams.ExStyle |= 0x020; // transparent
                    prams.ClassName = "RICHEDIT50W";
                }
                return prams;
            }
        }
    }

    

}
