using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace RibbonDemo
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            InitList();
        }

        private void InitList()
        {
            Image[] images = new Image[50];
            RibbonProfessionalRenderer rend = new RibbonProfessionalRenderer();
            Random r = new Random();

            using (GraphicsPath path = rend.RoundRectangle(new Rectangle(3, 3, 26, 26), 4))
            {
                using (GraphicsPath outer = rend.RoundRectangle(new Rectangle(0,0,32,32), 4))
                {
                    for (int i = 0; i < images.Length; i++)
                    {
                        Bitmap b = new Bitmap(32, 32);

                        using (Graphics g = Graphics.FromImage(b))
                        {
                            g.SmoothingMode = SmoothingMode.AntiAlias;

                            using (SolidBrush br = new SolidBrush(Color.FromKnownColor((KnownColor)r.Next(1, 150))))
                            {
                                g.FillPath(br, path);
                            }

                            using (Pen p = new Pen(Color.White, 3))
                            {
                                g.DrawPath(p, path);
                            }

                            g.DrawPath(Pens.Wheat, path);
                        }

                        images[i] = b;

                        RibbonButton btn = new RibbonButton();
                        btn.Image = b;
                        lst.Buttons.Add(btn);
                    }  
                }
            }

            //lst.DropDownItems.Add(new RibbonSeparator("Available styles"));
            RibbonButtonList lst2 = new RibbonButtonList();
            
            for (int i = 0; i < images.Length; i++)
            {
                RibbonButton btn = new RibbonButton();
                btn.Image = images[i];
                lst2.Buttons.Add(btn);
            }
            lst.DropDownItems.Add(lst2);
            //lst.DropDownItems.Add(new RibbonSeparator("Style Options"));
            lst.DropDownItems.Add(new RibbonButton("Save style..."));
            lst.DropDownItems.Add(new RibbonButton("Create style"));
            lst.DropDownItems.Add(new RibbonButton("Apply style..."));

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void ribbonPanel3_ButtonMoreClick(object sender, EventArgs e)
        {
            MessageBox.Show("Paragraph");
        }

        private void ribbonButton44_Click(object sender, EventArgs e)
        {
            MessageBox.Show("SmartArt");
        }

        
    }
}