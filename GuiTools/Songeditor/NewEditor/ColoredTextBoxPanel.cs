using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections;


namespace DreamBeam
{
    public partial class ColoredTextBoxPanel : UserControl
    {
        private string[] oldLines = {"1"};
        private int oldLineCount = 0;
        public string separator = "\n\n\n";
        public SongVerseSeparator verseSeparator = SongVerseSeparator.OneBlankLine;
        private int buttonClicked = 0;
        private bool renew = false;
        private int textHeight = 15;
        private bool Updating = false;
        public int minLineNumber = 20;
        ArrayList Typelist = new ArrayList();

        private const int WM_SCROLL = 276; // Horizontal scroll
        private const int WM_VSCROLL = 277; // Vertical scroll
        private const int SB_LINEUP = 0; // Scrolls one line up
        private const int SB_LINELEFT = 0;// Scrolls one cell left
        private const int SB_LINEDOWN = 1; // Scrolls one line down
        private const int SB_LINERIGHT = 1;// Scrolls one cell right
        private const int SB_PAGEUP = 2; // Scrolls one page up
        private const int SB_PAGELEFT = 2;// Scrolls one page left
        private const int SB_PAGEDOWN = 3; // Scrolls one page down
        private const int SB_PAGERIGTH = 3; // Scrolls one page right
        private const int SB_PAGETOP = 6; // Scrolls to the upper left
        private const int SB_LEFT = 6; // Scrolls to the left
        private const int SB_PAGEBOTTOM = 7; // Scrolls to the upper right
        private const int SB_RIGHT = 7; // Scrolls to the right
        private const int SB_ENDSCROLL = 8; // Ends scroll

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);

    

        public ColoredTextBoxPanel()
        {
            InitializeComponent();
            //verseSeparator = f.Config.SongVerseSeparator;

            this.rte.MouseWheel += new MouseEventHandler(this.MouseWheelHandler);

            renew = true;
            oldLines = DreamTools.SplitString(rte.Text, separator);
            for (int i = 0; i < oldLines.Length; i++)
            {
                Typelist.Add(LyricsType.Verse);
            }
            this.Update();
        }


        public void setText(Song song)
        {
            if (verseSeparator == SongVerseSeparator.OneBlankLine) separator = "\n\n";
            if (verseSeparator == SongVerseSeparator.TwoBlankLines) separator = "\n\n\n";          
                        
            Typelist = new ArrayList();            
            song.SongLyrics.Sort();
            StringBuilder s = new StringBuilder();
            int i = 0;

            foreach (LyricsItem item in song.SongLyrics)
            {
                if (item.Lyrics.Length > 0)
                {
                    if(s.Length >0) s.Append(separator);

                    s.Append(item.Lyrics);
                    Typelist.Add(item.Type);
                    i++;
                }               
            }
            if (Typelist.Count == 0)
            {
               Typelist.Add(LyricsType.Verse);
            }
                Updating = true;

                rte.Text = song.SanitizeLyrics(s.ToString());
                oldLines = DreamTools.SplitString(rte.Text, separator);
                rte.Font = new Font("Microsoft Sans Serif", 9);
                Updating = false;
                renew = true;
                Update();                                   
                Resizer();
        }


        public void GetText(Song song)
        {
            string[] newLines = DreamTools.SplitString(rte.Text, separator);
            int verseCount = 1;
            int chorusCount = 1;
            int otherCount = 1;
            int j = 0;
            song.ClearLyrics();
            foreach (string line in newLines)
            {
                switch ((LyricsType)Typelist[j])
                {
                    case LyricsType.Verse:
                        verseCount = song.AddLyrics(LyricsType.Verse, line, verseCount, verseSeparator);
                        break;
                    case LyricsType.Chorus:
                        chorusCount = song.AddLyrics(LyricsType.Chorus, line, chorusCount, verseSeparator);
                        break;
                    case LyricsType.Other:
                        otherCount = song.AddLyrics(LyricsType.Other, line, otherCount, verseSeparator);
                        break;
                }
                
            }

        }


        private void WhatChanged()
        {
            string[] newLines = DreamTools.SplitString(rte.Text, separator);

            if (newLines.Length != oldLines.Length)
            {                
                ArrayList NewTypelist = new ArrayList();

                for (int i = 0; i < newLines.Length; i++)
                {
                    NewTypelist.Add(LyricsType.Undefined);
                }                
                int j = 0;
                foreach (string line in newLines)
                {
                    int k = 0;
                    foreach (string oldline in oldLines)
                    {
                        if (line == oldline)
                        {
                            NewTypelist[j] = Typelist[k];
                        }
                        k++;
                    }
                    j++;
                }

                for (int i = 0; i < NewTypelist.Count; i++)
                {

                    if ((LyricsType)NewTypelist[i] == LyricsType.Undefined)
                    {

                        // Maybe the Line was split, so the next line is undefined too
                        if (i + 1 < NewTypelist.Count)
                        {
                            if ((LyricsType)NewTypelist[i + 1] == LyricsType.Undefined)
                            {
                                if (i < Typelist.Count)
                                {
                                    NewTypelist[i] = Typelist[i];
                                    NewTypelist[i + 1] = Typelist[i];
                                }
                            }
                        }


                        // if its still not undefined...
                        if ((LyricsType)NewTypelist[i] == LyricsType.Undefined)
                        {
                            //is it a new verse at the first line?
                            if (i == 0)
                            {
                                if (i < Typelist.Count) NewTypelist[i] = Typelist[i];
                            }
                            else
                            {
                                NewTypelist[i] = Typelist[i - 1];
                            }
                        }

                        // Last Option, if nothing worked
                        if ((LyricsType)NewTypelist[i] == LyricsType.Undefined)
                        {
                            NewTypelist[i] = LyricsType.Verse;
                        }
                    }
                }                
                Typelist = NewTypelist;
                renew = true;
            }
            oldLines = DreamTools.SplitString(rte.Text, separator);            
        }


        private void Update()
        {
            
            WhatChanged();
            
            
            int i = 0;
            int totalrows = 0;
           // renew = true;

            int blankrows = 1;            
            if (verseSeparator == SongVerseSeparator.TwoBlankLines) blankrows = 2;
            try
            {
                if (renew)
                {
                    #region renew
                    RemoveBackground();
                    foreach (string line in oldLines)
                    {


                        string[] mystringarr = line.Split("\n".ToCharArray());
                        int rows = mystringarr.Length;

                        // Background Panel
                        CodeVendor.Controls.Grouper p = new CodeVendor.Controls.Grouper();
                        p.Height = (rows * textHeight) + 14;
                        p.Width = rtePanel.Width + 1;

                        p.Location = new Point(-1, (totalrows * textHeight) - 11);
                        p.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.ForwardDiagonal;
                        //p.BackgroundColor = GetColor((SongListItem)Typelist[i]);
                        //p.BackgroundGradientColor = Color.LightBlue;
                        setColor(p, (LyricsType)Typelist[i]);

                        p.GroupTitle = "";
                        p.BorderColor = Color.White;
                        p.RoundCorners = 0;
                        p.Hide();
                        rtePanel.Controls.Add(p);
                        p.Name = i.ToString();
                        p.SendToBack();
                        p.Show();

                        //Button

                        RibbonStyle.RibbonMenuButton b = new RibbonStyle.RibbonMenuButton();
                        b.SetBounds(0, (totalrows * textHeight), 30, (rows * textHeight) + 4);
                        b.Text = "";
                        //b.Click += new System.EventHandler(this.TextButton_Click);
                        b.MouseDown += new MouseEventHandler(b_MouseDown);
                        b.ColorBase = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(209)))), ((int)(((byte)(240)))));
                        b.ColorBaseStroke = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(187)))), ((int)(((byte)(213)))));
                        b.ColorOn = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
                        b.ColorOnStroke = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
                        b.ColorPress = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
                        b.ColorPressStroke = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
                        b.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
                        b.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
                        b.ImageOffset = 2;
                        b.KeepPress = false;
                        b.SinglePressButton = false;
                        b.FadingSpeed = 35;
                        b.Name = i.ToString();
                        b.ContextMenuStrip = this.contextMenuStrip;
                        b.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.ToDown;
                        b.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.Yes;
                        b.SplitDistance = b.Width;
                        if ((LyricsType)Typelist[i] == LyricsType.Verse) { b.Image = global::DreamBeam.Properties.Resources.verse; }
                        else if ((LyricsType)Typelist[i] == LyricsType.Chorus) { b.Image = global::DreamBeam.Properties.Resources.chorus; }
                        else { b.Image = global::DreamBeam.Properties.Resources.Other; }
                        b.MaxImageSize = new Point(16, 16);
                        ButtonPanel.Controls.Add(b);

                        if (verseSeparator == SongVerseSeparator.OneBlankLine) separator = "\n\n";

                        totalrows = rows + blankrows + totalrows;
                        i++;
                    }
                    renew = false;
                    #endregion
                }
                else
                {
                    #region refresh
                    while (i < Typelist.Count)
                    {
                        foreach (Control c in ButtonPanel.Controls)
                        {
                            if (c.Name == i.ToString())
                            {

                                string[] mystringarr = oldLines[i].Split("\n".ToCharArray());
                                int rows = mystringarr.Length;

                                ((RibbonStyle.RibbonMenuButton)c).SetBounds(0, (totalrows * textHeight), 30, (rows * textHeight) + 4);
                                if ((LyricsType)Typelist[i] == LyricsType.Verse) { ((RibbonStyle.RibbonMenuButton)c).Image = global::DreamBeam.Properties.Resources.verse; }
                                else if ((LyricsType)Typelist[i] == LyricsType.Chorus) { ((RibbonStyle.RibbonMenuButton)c).Image = global::DreamBeam.Properties.Resources.chorus; }
                                else { ((RibbonStyle.RibbonMenuButton)c).Image = global::DreamBeam.Properties.Resources.Other; }

                                totalrows = rows + blankrows + totalrows;
                                i++;
                            }
                        }
                    }
                    totalrows = 0;
                    i = 0;
                    while (i < Typelist.Count)
                    {
                        foreach (Control c in rtePanel.Controls)
                        {
                            if (c.Name == i.ToString() && c.Visible)
                            {

                                string[] mystringarr = oldLines[i].Split("\n".ToCharArray());
                                int rows = mystringarr.Length;
                                //c.Hide();
                                setColor((CodeVendor.Controls.Grouper)c, (LyricsType)Typelist[i]);
                                c.Location = new Point(-1, (totalrows * textHeight) - 11);
                                c.Height = (rows * textHeight) + 14;
                                c.Width = rtePanel.Width + 1;
                                totalrows = rows + blankrows + totalrows;
                                c.SendToBack();
                                //c.Show();
                                i++;
                            }
                        }
                    }
                    #endregion


                }
            }catch(Exception e){}
            }

        

        void b_MouseDown(object sender, MouseEventArgs e)
        {
            buttonClicked = Convert.ToInt32(((Control)sender).Name);
        }

        private void ColoredTextBoxPanel_Paint(object sender, PaintEventArgs e)
        {
            rte.Font = new Font("Microsoft Sans Serif", 9);
        }


        private void setColor(CodeVendor.Controls.Grouper p, LyricsType item)
        {
            switch (item)
            {
                case LyricsType.Verse:
                    p.BackgroundColor = Color.AliceBlue;
                    p.BackgroundGradientColor = Color.LightBlue;                    
                    break;
                case LyricsType.Chorus:
                    p.BackgroundColor = Color.LightGoldenrodYellow; //Color.LightYellow;
                    p.BackgroundGradientColor = Color.Moccasin;
                    break;
                case LyricsType.Other:
                    p.BackgroundColor = Color.MintCream;
                    p.BackgroundGradientColor = Color.LightGreen;
                    break;
            }

        }

        private Color GetColor(LyricsType item)
        {
            switch (item)
            {
                case LyricsType.Verse:
                    return Color.AliceBlue;
                    break;
                case LyricsType.Chorus:
                    return Color.Wheat;
                    break;
                case LyricsType.Other:
                    return Color.NavajoWhite;
                    break;
            }
            return Color.White;
            
        }

        private void RemoveBackground()
        {
            foreach (Control c in rtePanel.Controls)
            {
                if (c != rte) { c.Hide(); }
            }

            foreach (Control c in rtePanel.Controls)
            {
                if (c != rte) { rtePanel.Controls.Remove(c); c.Dispose(); }
            }

            foreach (Control c in ButtonPanel.Controls)
            {
                c.Hide();
            }

            foreach (Control c in ButtonPanel.Controls)
            {
                ButtonPanel.Controls.Remove(c); c.Dispose();
            }
        
        }

        
        

        private void ColoredTextBoxPanel_Resize(object sender, EventArgs e)
        {
            Resizer();
        }

        private void Resizer()
        {
            
           
            //this.rtePanel.Location = new Point(0, 0);            

            this.rtePanel.Width = Scrollpanel.Width-50;

            minLineNumber = Scrollpanel.Height / textHeight;
            rte.Font = new Font("Microsoft Sans Serif", 9);
            if (rte.Lines.Length > minLineNumber)
            {
                
                this.rtePanel.Height = rte.Lines.Length * textHeight +6;
                SendMessage(this.rte.Handle, WM_VSCROLL, (IntPtr)SB_PAGEUP, IntPtr.Zero);
            }
            else
            {
                this.rtePanel.Height = (minLineNumber * textHeight) + 9;
            }
            this.ButtonPanel.Height = rtePanel.Height;
            this.ButtonPanel.Location = new Point(this.rtePanel.Width, ButtonPanel.Location.Y);

            
                foreach (Control c in rtePanel.Controls)
                {                                       
                    if(c.Name!= "rte") c.Width = rtePanel.Width+ 1;                        
                        //c.SendToBack();                                           
                }
            
            
        }

        private void verseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Typelist[buttonClicked] = LyricsType.Verse;            
            Update();
        }

        private void chorusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Typelist[buttonClicked] = LyricsType.Chorus;            
            Update();
        }

        private void otherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Typelist[buttonClicked] = LyricsType.Other;
            Update();

           
        }

        private void rte_TextChanged(object sender, EventArgs e)
        {
            if (!Updating)
            {
                if (rte.Lines.Length != oldLineCount) Resizer();
                oldLineCount = rte.Lines.Length;
                Update();
            }

        }
      

        private void rte_KeyUp(object sender, KeyEventArgs e)
        {
            ScrollToCursor();
        }


        private void ScrollToCursor()
        {


            #region ScrollDownToCursor
            int loops = 0; // just to prevent an endless loop, (didn't happen yet)
            while ((Scrollpanel.AutoScrollVPos + Scrollpanel.Height) < ((rte.CurrentLine * textHeight) + 23) && loops < 1000)
            {
                loops++;
                if (loops == 999) Console.WriteLine("Endless Loop in ColoredTextBoxPanel  rte_KeyUp");

                // Scroll PageDown if possible
                if ((Scrollpanel.AutoScrollVPos + (Scrollpanel.Height) + 50) < ((rte.CurrentLine * textHeight) + 23))
                {
                    SendMessage(this.Scrollpanel.Handle, WM_VSCROLL, (IntPtr)SB_PAGEDOWN, IntPtr.Zero);
                }
                else
                {
                    SendMessage(this.Scrollpanel.Handle, WM_VSCROLL, (IntPtr)SB_LINEDOWN, IntPtr.Zero);
                }
            }
            #endregion

            #region ScrollUptoCursor
            if (rte.CurrentLine == 1)
            {
                SendMessage(this.Scrollpanel.Handle, WM_VSCROLL, (IntPtr)SB_PAGETOP, IntPtr.Zero);
                SendMessage(this.Scrollpanel.Handle, WM_VSCROLL, (IntPtr)SB_LINEUP, IntPtr.Zero);
                
            }else if (rte.Height > Scrollpanel.Height && (rte.CurrentLine * textHeight) - 15 >= 0)
            {
                loops = 0;
                while (Scrollpanel.AutoScrollVPos > (rte.CurrentLine * textHeight) - 15 && loops < 1000)
                {
                    loops++;
                    if (loops == 999) Console.WriteLine("Endless Loop 2 in ColoredTextBoxPanel  rte_KeyUp");

                    if (Scrollpanel.AutoScrollVPos - 50 > (rte.CurrentLine * textHeight) - 15)
                    {
                        SendMessage(this.Scrollpanel.Handle, WM_VSCROLL, (IntPtr)SB_PAGEUP, IntPtr.Zero);
                    }
                    else
                    {
                        SendMessage(this.Scrollpanel.Handle, WM_VSCROLL, (IntPtr)SB_LINEUP, IntPtr.Zero);
                    }

                }
            }
            #endregion

        }

        private void MouseWheelHandler(object sender, System.Windows.Forms.MouseEventArgs e)
        {                        
            if (e.Delta < 0)
            {                
                for (int i = 1; i <= SystemInformation.MouseWheelScrollLines; i++)
                {
                    SendMessage(this.Scrollpanel.Handle, WM_VSCROLL, (IntPtr)SB_LINEDOWN, IntPtr.Zero);
                    SendMessage(this.Scrollpanel.Handle, WM_VSCROLL, (IntPtr)SB_LINEDOWN, IntPtr.Zero);
                    SendMessage(this.Scrollpanel.Handle, WM_VSCROLL, (IntPtr)SB_LINEDOWN, IntPtr.Zero);
                }
            }
            if (e.Delta > 0)
            {
                for (int i = 1; i <= SystemInformation.MouseWheelScrollLines; i++)
                {
                    SendMessage(this.Scrollpanel.Handle, WM_VSCROLL, (IntPtr)SB_LINEUP, IntPtr.Zero);
                    SendMessage(this.Scrollpanel.Handle, WM_VSCROLL, (IntPtr)SB_LINEUP, IntPtr.Zero);
                    SendMessage(this.Scrollpanel.Handle, WM_VSCROLL, (IntPtr)SB_LINEUP, IntPtr.Zero);
                }
                
            }

        }

        private void rte_MouseEnter(object sender, EventArgs e)
        {
            rte.Focus();
        }

        

        

        

       
    }

    
    

}
