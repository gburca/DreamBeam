using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;

using System.Text;
using System.Windows.Forms;

namespace DreamBeam
{
    public partial class SequenceDialog : SlidePanel
        //SlidePanel
    {
        public SequenceDialog(Control poOwner, float pfStep): base(poOwner, pfStep)
        {
            InitializeComponent();
        }


        private void ListEx_Available_PressIcon(int Index)
        {
            this.ListEx_Sequence.Add((string)this.ListEx_Available.Items[Index], 1);
            //this.sequence.Add(this.sequenceAvailable[Index]);
        }

        private void ListEx_Sequence_PressIcon(int Index)
        {
            this.ListEx_Sequence.Remove(Index);
           // this.sequence.RemoveAt(Index);
        }

        private void SequenceDialog_SizeChanged(object sender, EventArgs e)
        {
            this.panel1.Location = new Point((this.Width - panel1.Width) / 2, panel1.Location.Y);
        }

        public void EnableDisableListBoxes()
        {
            this.ListEx_Available.Enabled = !this.AutoSequence.Checked;
            this.ListEx_Sequence.Enabled = !this.AutoSequence.Checked;
        }

        private void AutoSequence_Click(object sender, EventArgs e)
        {
            this.EnableDisableListBoxes();
        }

		
    }
}
