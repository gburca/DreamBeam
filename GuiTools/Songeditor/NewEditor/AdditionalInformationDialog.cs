using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;

using System.Text;
using System.Windows.Forms;

namespace DreamBeam
{
    public partial class AdditionalInformationDialog : SlidePanel
    {
        public AdditionalInformationDialog(Control poOwner, float pfStep) : base(poOwner,pfStep)
        {
            InitializeComponent();
        }
    }
}
