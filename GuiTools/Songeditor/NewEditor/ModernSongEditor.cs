using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;

using System.Text;
using System.Windows.Forms;

namespace DreamBeam
{
    public partial class ModernSongEditor : UserControl
    {
        AdditionalInformationDialog AdditionalInformation;
        SequenceDialog Sequence;

        public ModernSongEditor()
        {
            InitializeComponent();

             AdditionalInformation = new AdditionalInformationDialog(EditorPanel, 0.1f);
             AdditionalInformation.Hide();
             AdditionalInformation.SlideDirection = AdditionalInformationDialog.SLIDE_DIRECTION.BOTTOM;
             AdditionalInformation.Size = EditorPanel.Size;

             Sequence = new SequenceDialog(EditorPanel, 0.1f);
             Sequence.Hide();
             Sequence.SlideDirection = AdditionalInformationDialog.SLIDE_DIRECTION.RIGHT;
             Sequence.Size = EditorPanel.Size;

             this.Controls.Add(AdditionalInformation);
             this.Controls.Add(Sequence);
        }

        private void ribbonMenuButton2_Click(object sender, EventArgs e)
        {
            
            //AdditionalInformationDialog d = new AdditionalInformationDialog(button1, 0.1f);
            if (Sequence.IsExpanded)
            {
                Sequence.Slide();
                SequenceButton.Checked = false;
            }
            AdditionalInformation.BringToFront();
            
            AdditionalInformation.Slide();
        }

        private void ModernSongEditor_SizeChanged(object sender, EventArgs e)
        {
            Sequence.Size = EditorPanel.Size;
            AdditionalInformation.Size = EditorPanel.Size;
        }

        private void SequenceButton_Click(object sender, EventArgs e)
        {
            if (AdditionalInformation.IsExpanded)
            {
                AdditionalInformation.Slide();
                AdditionalInformationButton.Checked = false;
            }
            Sequence.BringToFront();
            Sequence.Slide();

        }
    }
}
