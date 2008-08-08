using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.ComponentModel;
using System.Collections;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace DreamBeam
{
    public partial class ModernSongEditor : UserControl, ISongEditor
    {
        private AdditionalInformationDialog AdditionalInformation;
        private SequenceDialog Sequence;

        private ArrayList sequenceAvailable = new ArrayList();
        private ArrayList sequence = new ArrayList();
        private Song song;
        


        public ModernSongEditor()
        {
            InitializeComponent();

             AdditionalInformation = new AdditionalInformationDialog(EditorPanel, 0.1f);
             AdditionalInformation.Hide();
             AdditionalInformation.SlideDirection = AdditionalInformationDialog.SLIDE_DIRECTION.BOTTOM;
             AdditionalInformation.Size = EditorPanel.Size;

             Sequence = new SequenceDialog(EditorPanel, 0.1f);
             Sequence.Hide();
             Sequence.SlideDirection = AdditionalInformationDialog.SLIDE_DIRECTION.LEFT;
             Sequence.Size = EditorPanel.Size;

             this.Controls.Add(AdditionalInformation);
             this.Controls.Add(Sequence);
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Song Song
        {
            set { song = (Song)value; PopulateControls(song); }
            get { return ReadControls(); }
        }

        public string[] Collections
        {
            set
            {
                AdditionalInformation.Collection.Items.Clear();
                foreach (string c in value)
                {
                    AdditionalInformation.Collection.Items.Add(c);
                }
            }
            get
            {
                string[] collections = new String[AdditionalInformation.Collection.Items.Count];
                int i = 0;
                foreach (string c in AdditionalInformation.Collection.Items)
                {
                    collections[i++] = c;
                }
                return collections;
            }
        }

        private void PopulateControls(Song song)
        {
            this.Title.Text = song.Title;
            AdditionalInformation.Author.Text = song.Author;
            AdditionalInformation.Collection.Text = song.Collection;
            AdditionalInformation.Number.Text = song.Number;
            AdditionalInformation.Notes.Text = (song.Notes != null) ? Regex.Replace(song.Notes, "\n", "\r\n") : null;
            AdditionalInformation.KeyRangeLow.Text = song.KeyRangeLow;
            AdditionalInformation.KeyRangeHigh.Text = song.KeyRangeHigh;
            AdditionalInformation.MinorKey.Checked = song.MinorKey;
            //this.DualLanguage.Checked = song.DualLanguage;
            //            this.songThemeWidget.Design_checkBox.Checked = song.UseDesign;
            if (song.config != null)
            {
                coloredTextBoxPanel1.verseSeparator = song.config.SongVerseSeparator;
            }
            else
            {
                coloredTextBoxPanel1.verseSeparator = SongVerseSeparator.OneBlankLine;
            }

            // If we don't Clear, old RTF formatting codes from the previous Paste operation
            // remain in the control and cause the new text to take on that formatting
            
            //this.VerseLyrics.Clear();
            //this.ChorusLyrics.Clear();
            //this.OtherLyrics.Clear();
            coloredTextBoxPanel1.setText(song);
            //this.VerseLyrics.Text = song.GetLyrics(LyricsType.Verse, verseSeparator);
            //this.ChorusLyrics.Text = song.GetLyrics(LyricsType.Chorus, verseSeparator);
            //this.OtherLyrics.Text = song.GetLyrics(LyricsType.Other, verseSeparator);

            Sequence.ListEx_Sequence.Items.Clear();
            this.sequence.Clear();
            foreach (LyricsSequenceItem item in song.Sequence)
            {
                Sequence.ListEx_Sequence.Add(item.ToString(), 1);
                this.sequence.Add(item);
            }

            this.UpdateAvailableLyrics();
        }

        private Song ReadControls()
        {
            if (song == null) song = new Song();
            song.Title = this.Title.Text;
            song.Author = AdditionalInformation.Author.Text;
            song.Collection = AdditionalInformation.Collection.Text;
            song.Number = AdditionalInformation.Number.Text;
            song.Notes = (AdditionalInformation.Notes.Text != null) ? Regex.Replace(AdditionalInformation.Notes.Text, "\r\n", "\n") : null;
            song.KeyRangeLow = AdditionalInformation.KeyRangeLow.Text;
            song.KeyRangeHigh = AdditionalInformation.KeyRangeHigh.Text;
            song.MinorKey = AdditionalInformation.MinorKey.Checked;
            //song.DualLanguage = AdditionalInformation.DualLanguage.Checked;
            


            //song.SetLyrics(LyricsType.Verse, this.VerseLyrics.Text, verseSeparator);
            //song.SetLyrics(LyricsType.Chorus, this.ChorusLyrics.Text, verseSeparator);
            //song.SetLyrics(LyricsType.Other, this.OtherLyrics.Text, verseSeparator);

            song.Sequence = new ArrayList();
            foreach (LyricsSequenceItem item in this.sequence)
            {
                song.Sequence.Add(new LyricsSequenceItem(item));
            }

            return song;
        }

        private void UpdateAvailableLyrics()
        {
            Song s = this.ReadControls();

            Sequence.ListEx_Available.Items.Clear();

            if (s.SongLyrics == null) return;
            s.SongLyrics.Sort();
            this.sequenceAvailable.Clear();
            foreach (LyricsItem l in s.SongLyrics)
            {
                LyricsSequenceItem item = new LyricsSequenceItem(l.Type, l.Number);
                Sequence.ListEx_Available.Add(item.ToString(), 0);
                this.sequenceAvailable.Add(item);
            }

            // TODO: Update the sequence list as well, in case the user removed a verse that was part of the sequence.
        }


        private void ribbonMenuButton2_Click(object sender, EventArgs e)
        {
                        
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
