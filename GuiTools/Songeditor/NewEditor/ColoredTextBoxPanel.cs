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
        private string seperator = "\n\n\n";
        private int buttonClicked = 0;
        private bool renew = false;
        ArrayList Typelist = new ArrayList();


      

        public enum SongListItem
        {
            Verse,
            Chorus,
            Other,
            Undefined
        }

        public ColoredTextBoxPanel()
        {
            InitializeComponent();


            renew = true;
            oldLines = DreamTools.SplitString(rte.Text, seperator);
            for (int i = 0; i < oldLines.Length; i++)
            {
                Typelist.Add(SongListItem.Verse);
            }
            this.Update();
        }

        private void WhatChanged()
        {
            string[] newLines = DreamTools.SplitString(rte.Text, seperator);

            if (newLines.Length != oldLines.Length)
            {
                ArrayList Changes = new ArrayList();
                ArrayList NewTypelist = new ArrayList();

                for (int i = 0; i < newLines.Length; i++)
                {
                    NewTypelist.Add(SongListItem.Undefined);
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

                    if ((SongListItem)NewTypelist[i] == SongListItem.Undefined)
                    {

                        // Maybe the Line was split, so the next line is undefined too
                        if (i + 1 < NewTypelist.Count)
                        {
                            if ((SongListItem)NewTypelist[i + 1] == SongListItem.Undefined)
                            {
                                if (i < Typelist.Count)
                                {
                                    NewTypelist[i] = Typelist[i];
                                    NewTypelist[i + 1] = Typelist[i];
                                }
                            }
                        }


                        // if its still not undefined...
                        if ((SongListItem)NewTypelist[i] == SongListItem.Undefined)
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
                        if ((SongListItem)NewTypelist[i] == SongListItem.Undefined)
                        {
                            NewTypelist[i] = SongListItem.Verse;
                        }
                    }
                }                
                Typelist = NewTypelist;
                renew = true;
            }
            oldLines = DreamTools.SplitString(rte.Text, seperator);
        }


        private void Update()
        {
           Resizer();
            WhatChanged();            
            int i = 0;
            int totalrows = 0;
            int height = 15;

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
                    p.Height = (rows * height) + 14;
                    p.Width = rtePanel.Width + 1;

                    p.Location = new Point(-1, (totalrows * height) - 11);
                    p.BackgroundColor = GetColor((SongListItem)Typelist[i]);
                    p.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.ForwardDiagonal;
                    p.BackgroundGradientColor = Color.LightBlue;
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
                    b.SetBounds(0, (totalrows * height), 30, (rows * height) + 4);
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
                    ButtonPanel.Controls.Add(b);

                    totalrows = rows + 2 + totalrows;
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

                            ((RibbonStyle.RibbonMenuButton)c).SetBounds(0, (totalrows * height), 30, (rows * height) + 4);
                            totalrows = rows + 2 + totalrows;
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

                            c.Location = new Point(-1, (totalrows * height) - 11);
                            c.Height = (rows * height) + 14;
                            totalrows = rows + 2 + totalrows;
                            i++;
                        }
                    }
                }
                #endregion 


            }
            }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        void b_MouseDown(object sender, MouseEventArgs e)
        {
            buttonClicked = Convert.ToInt32(((Control)sender).Name);
        }

        private void ColoredTextBoxPanel_Paint(object sender, PaintEventArgs e)
        {
            rte.Font = new Font("Microsoft Sans Serif", 9);
        }

        private Color GetColor(SongListItem item){
            switch (item)
            {
                case SongListItem.Verse:
                    return Color.AliceBlue;
                    break;
                case SongListItem.Chorus:
                    return Color.Wheat;
                    break;
                case SongListItem.Other:
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

        
        private void button2_Click(object sender, EventArgs e)
        {
         

            
        }

        private void ColoredTextBoxPanel_Resize(object sender, EventArgs e)
        {
            Resizer();
        }

        private void Resizer()
        {
            this.rtePanel.Width = this.Width-30;            
        }

        private void asdfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Typelist[buttonClicked] = SongListItem.Verse;
            Update();
        }

        private void chorusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Typelist[buttonClicked] = SongListItem.Chorus;
            Update();
        }

        private void otherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Typelist[buttonClicked] = SongListItem.Other;
            Update();
        }

        private void rte_TextChanged(object sender, EventArgs e)
        {
            Update();
        }

       
    }

    
    

}
