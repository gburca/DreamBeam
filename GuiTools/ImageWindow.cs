using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DreamBeam
{
    public partial class ImageWindow : Form
    {
        public ImageWindow(MainForm m)
        {
            _mainform = m;
            InitializeComponent();
        }
        
        

        private Display _display;
        private MainForm _mainform;
        private RectangleF[] UndoBounds;
        private bool changingRect = false;

        public Display Display
        {
            get {
                return _display;
            }
            set {
                _display = value;
                _display.SetDestination(this.imagePanel);
                SetContent();
                
            }
        }

        public IContentOperations Content{
            get
            {
                return _display.content;
            }

        }

        private ThemeWidget getWidget()
        {
            IContentOperations content = _display.content;
            if (content != null)
            {
                switch ((ContentType)content.GetIdentity().Type)
                {
                    case ContentType.Song:
                        return _mainform.songThemeWidget;
                        break;
                    case ContentType.PlainText:
                        return _mainform.sermonThemeWidget;
                        break;
                    case ContentType.BibleVerseIdx:
                    case ContentType.BibleVerseRef:
                    case ContentType.BibleVerse:
                        return _mainform.bibleThemeWidget;

                        break;
                }

            }
            return new ThemeWidget();
        }

        public void Activate(Display d)
        {
            this.Display = d;
            SetTextButtons();
            UpdateMarginBoxes();
            SaveUndo();
            this.ShowDialog();
        }

        private void SetContent()
        {
            //IContentOperations content = _display.content;
            if (Content != null)
            {
                // First, let's clear the pre-render cache
                try
                {
                    (Content as Content).RenderedFramesClear();
                }catch { }                
                this.imagePanel.RectPosition = Content.Theme.TextFormat[getWidget().getSelectedTab()].Bounds;                                                
                this.imagePanel.ShowRect = true;
                Content.ShowRectangles = false;
                _display.UpdateDisplay(true);   
            }
        }

        private void SelectActiveTextButton()
        {

            ((RibbonStyle.RibbonMenuButton)this.grouper1.Controls[getWidget().getSelectedTab()]).Check();
        }

        private void SetTextButtons()
        {
            grouper1.Controls.Clear();
            grouper1.Height = 35 + (getWidget().getTabNames().Length * 23);
            grouper2.Top = grouper1.Top + grouper1.Height + 10;
            int i = 0;
            foreach (string s in getWidget().getTabNames())
            {               
                RibbonStyle.RibbonMenuButton b = new RibbonStyle.RibbonMenuButton();
                b.SetBounds(9,25+i*23,100,23);
                b.Text = s;
                b.Click += new System.EventHandler(this.TextButton_Click);
                b.ColorBase = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(209)))), ((int)(((byte)(240)))));
                b.ColorBaseStroke = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(187)))), ((int)(((byte)(213)))));
                b.ColorOn = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
                b.ColorOnStroke = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
                b.ColorPress = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
                b.ColorPressStroke = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
                b.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
                b.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
                b.ImageOffset = 2;
                b.KeepPress = true;
                b.SinglePressButton = false;
                b.FadingSpeed = 35;                               
                b.Name = i.ToString();
                
                b.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.Center;
                if (i == 0) b.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.Top;
                if (i == getWidget().getTabNames().Length -1) b.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.Bottom;
                
                grouper1.Controls.Add(b);
               // if (i == getWidget().getSelectedTab()) b.Check();
                i++;
            }
            

        }

        private void TextButton_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(((RibbonStyle.RibbonMenuButton)sender).Text.ToString());
            //getWidget().                       
            this.imagePanel.RectPosition = this.Content.Theme.TextFormat[Convert.ToInt32(((Control)sender).Name)].Bounds;
            getWidget().selectTab(Convert.ToInt32(((Control)sender).Name));
            _display.UpdateDisplay(true);   
        }


        private void Update()
        {
            if (Content != null)
            {
                Content.Theme.TextFormat[getWidget().getSelectedTab()].Bounds = imagePanel.RectPosition;
                getWidget().Theme = Content.Theme;
                _display.UpdateDisplay(true);
            }
        }

        private void imagePanel_RectangleChangedEvent()
        {                        
            changingRect = true;
            UpdateMarginBoxes();            
            changingRect = false;
            //if (!ChangeRect()) Update();                       
            Update();                      
        }

        private void UpdateMarginBoxes()
        {
            changingRect = true;
            // If we assign values that are out-of-range to the controls, we'll get an exception.
            //this.Bounds1.Value = Tools.ForceToRange(Bounds1.Minimum, Bounds1.Maximum, (decimal)imagePanel.RectPosition.Left);
            //this.Bounds2.Value = Tools.ForceToRange(Bounds2.Minimum, Bounds2.Maximum, (decimal)imagePanel.RectPosition.Top);
            //this.Bounds3.Value = Tools.ForceToRange(Bounds3.Minimum, Bounds3.Maximum, (decimal)(100F - imagePanel.RectPosition.Right));
            //this.Bounds4.Value = Tools.ForceToRange(Bounds4.Minimum, Bounds4.Maximum, (decimal)(100F - imagePanel.RectPosition.Bottom));
            changingRect = true;
        }
       

        private void ImageWindow_Shown(object sender, EventArgs e)
        {
            this.SelectActiveTextButton();
        }

      
       public void SaveUndo(){
           this.UndoBounds = new RectangleF[Content.Theme.TextFormat.Length];
           for (int i = 0; i < Content.Theme.TextFormat.Length; i++)
           {
             this.UndoBounds[i] = Content.Theme.TextFormat[i].Bounds;
           }                      
       }
       
        public void Undo()
       {
           
           for (int i = 0; i < Content.Theme.TextFormat.Length; i++)
           {
               Content.Theme.TextFormat[i].Bounds = this.UndoBounds[i];               
           }           
       }

  

        private void Cancel_Button_Click(object sender, EventArgs e)
        {
            this.Undo();
            this.Close();
        }

        private void OK_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void imagePanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.Close();
            }
        }

        private void Bounds_ValueChanged(object sender, EventArgs e)
        {
            if (!changingRect) ChangeRect();
        }

        private bool ChangeRect()
        {
            RectangleF r = new RectangleF();
            r.X = (float)this.Bounds1.Value;
            r.Y = (float)this.Bounds2.Value;
            r.Width = (float)(100 - this.Bounds3.Value - this.Bounds1.Value);
            r.Height = (float)(100 - this.Bounds4.Value - this.Bounds2.Value);
            
            //Comparing in String Format seems to work better
            if (r.X.ToString() != imagePanel.RectPosition.X.ToString() || r.Y.ToString() != imagePanel.RectPosition.Y.ToString() || r.Width.ToString() != imagePanel.RectPosition.Width.ToString() || r.Height.ToString() != imagePanel.RectPosition.Height.ToString())            
            {
                imagePanel.RectPosition = r;                
                Update();
                return true;
            }
            return false;
        }

        private void ImageWindow_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (e.KeyChar == (Char)Keys.Q)
            {
               
            }
        }

       

      

        private void imagePanel_Click(object sender, EventArgs e)
        {
            this.ActiveControl = this.imagePanel;
        }

        

    }
}
