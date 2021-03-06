using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DreamBeam
{
    

    public struct PaintStruct
    {
        public Graphics dc;
        public bool isPaint;
    };
    public class ImagePanel : CDrawDragRect
    {
        public string strImagePath;
        string strTmp;        
        public Image imgResize;
        public int nDblClick;
        public RectangleF _rectPosition;        
        public List<Rectangle> rectList;
        public List<string> stringList;
        Bitmap MemoryBitmap;

        public RectangleF RectPosition{
            get
            {
                _rectPosition = new RectangleF();
                if (_imagepack != null)
                {
                    _rectPosition.X = ((float)rcBone.X - ((float)(this._imagepack.Bitmap.Size.Width - this._imagepack.realsize.Width) / 2f)) * 100f / (float)this._imagepack.realsize.Width;
                    _rectPosition.Y = ((float)rcBone.Y - ((float)(this._imagepack.Bitmap.Size.Height - this._imagepack.realsize.Height) / 2f)) * 100f / (float)this._imagepack.realsize.Height;
                    _rectPosition.Height = rcBone.Height * 100f / (float)this._imagepack.realsize.Height;
                    _rectPosition.Width = rcBone.Width * 100f / (float)this._imagepack.realsize.Width;
                }

                return _rectPosition;
            }
            set
            {
                if (value != null)
                {
                    _rectPosition = value;
                    SetPosition(value);
                }
            }
    
        }

        public override void Refresh()
        {
            this.PaintImage();
            //base.Refresh();


            Graphics dcPaint = Graphics.FromHwnd(this.Handle);

         
            if (MemoryBitmap != null)
           {
                
                dcPaint.DrawImage(MemoryBitmap, 0, 0);
            }
            //dcPaint.Dispose();

        }

        

        private void SetPosition(RectangleF value)
        {
            if (_imagepack != null)
            {
                rcBone.X = (value.X * (float)this._imagepack.realsize.Width / (float)100) + ((this._imagepack.Bitmap.Size.Width - this._imagepack.realsize.Width) / 2f);
                rcBone.Y = (value.Y * (float)this._imagepack.realsize.Height / (float)100) + ((this._imagepack.Bitmap.Size.Height - this._imagepack.realsize.Height) / 2f);
                nWd = rcBone.Width = (value.Width * (float)this._imagepack.realsize.Width / (float)100);
                nHt = rcBone.Height = (value.Height * (float)this._imagepack.realsize.Height / (float)100);
                RefreshAll(rcBone.X, rcBone.Y);
            }
        }

        protected override void Invalidator()
        {
            //this.PaintImage();
            //base.Invalidator();
            this.Refresh();
        }

        public ResizedImagePack ImagePack
        {
            get { return this._imagepack; }
            set
            {
                if (value != null)
                {
                    this._imagepack = value;
                    this.Image = value.Bitmap;
                    if (this._showrect && this._rectPosition != null)
                    {
                        SetPosition(this._rectPosition);
                    }
                    else
                    {
                        this.Refresh();
                    }
                }
            }
        }

        public Image Image;
  
        public ImagePanel()
        {
           

        }
       
        public void SizeToFit(int nRectNumber)
        {
            MessageBox.Show(Size.Width.ToString());
            //MessageBox.Show(nRectNumber.ToString());
            rcBone = rectList[nRectNumber];
            nWd = rectList[nRectNumber].Width;
            nHt = rectList[nRectNumber].Height;
            RefreshAll(rcBone.X, rcBone.Y);
            rectList.RemoveAt(nRectNumber);
            stringList.RemoveAt(nRectNumber);
        }      
   

        public void DrawImage(PaintStruct ps)
        {
            if (Image != null)
            {
                if (imgResize != null)
                    imgResize.Dispose();
                if (ClientRectangle.Width > 0 && ClientRectangle.Height > 0)
                {
                    imgResize = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
                    Graphics dcTmp = Graphics.FromImage(imgResize);
                    dcTmp.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    dcTmp.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                    //dcTmp.DrawImage(img, ClientRectangle);
                    dcTmp.DrawImage(Image, new Point(0, 0));
                    dcTmp.Dispose();

                    if (ps.isPaint)
                    {
                        Graphics dc = ps.dc;
                        dc.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        dc.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                        //dc.DrawImage(imgResize, ClientRectangle);
                        dc.DrawImage(imgResize, new Point(0, 0));
                    }
                    else
                    {
                        Graphics dc = CreateGraphics();
                        dc.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        dc.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                        //dc.DrawImage(imgResize, ClientRectangle);
                        dc.DrawImage(imgResize, new Point(0, 0));
                        dc.Dispose();
                    }
                }
            }
        }

        public void DrawRectangle(Graphics dc)
        {
            if (_showrect)
            {
                dc.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
                dc.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

                Pen p = new Pen(Color.FromArgb(200, Color.Blue), 3);
                dc.DrawRectangle(p, rcBone.X, rcBone.Y, rcBone.Width, rcBone.Height);                
                Pen x = new Pen(Color.FromArgb(200, Color.LightBlue), 1);
                dc.DrawRectangle(x, rcBone.X, rcBone.Y, rcBone.Width, rcBone.Height);

                dc.FillRectangle(Brushes.Blue, rcLT);
                dc.FillRectangle(Brushes.Blue, rcRT);
                dc.FillRectangle(Brushes.Blue, rcLB);
                dc.FillRectangle(Brushes.Blue, rcRB);

                dc.FillRectangle(Brushes.Blue, rcTM);
                dc.FillRectangle(Brushes.Blue, rcBM);
                dc.FillRectangle(Brushes.Blue, rcLM);
                dc.FillRectangle(Brushes.Blue, rcRM);

        
            }
        }


        protected override bool ProcessDialogKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Down:

                    this.rcBone.Y += 1;
                    NotifyRectangleChangeListeners();
                    break;
                case Keys.Up:
                    this.rcBone.Y -= 1;
                    NotifyRectangleChangeListeners();
                    break;
                case Keys.Left:
                    this.rcBone.X -= 1;
                    
                    NotifyRectangleChangeListeners();
                    break;
                case Keys.Right:
                    this.rcBone.X += 1;
                    
                    NotifyRectangleChangeListeners();
                    break;
            }
            return base.ProcessDialogKey(keyData);

        }


        protected override void OnPaint(PaintEventArgs e)
        {            
            Graphics dcPaint = e.Graphics;

         
            if (MemoryBitmap != null)
           {
                
                dcPaint.DrawImage(MemoryBitmap, 0, 0);
            }
            dcPaint.Dispose();
   
           /*if (ClientRectangle.Width > 0 && ClientRectangle.Height > 0 && Image != null)
            {                
                PaintStruct ps;
                ps.dc = e.Graphics;
                ps.isPaint = true;
                DrawImage(ps);
                DrawRectangle(dcPaint);
            }*/
            base.OnPaint(e);
        }


        public void PaintImage(){
            if (this.Height != null && this.Width != null && this.Height > 0 && this.Width >0)
            {
                MemoryBitmap = new Bitmap(this.Width, this.Height);
                Graphics dcPaint = Graphics.FromImage(MemoryBitmap);
                {
                    PaintStruct ps;
                    ps.dc = Graphics.FromImage(MemoryBitmap); ;
                    ps.isPaint = true;
                    DrawImage(ps);
                    DrawRectangle(dcPaint);
                }
                dcPaint.Dispose();
            }
        }

       
        protected override void OnDoubleClick(EventArgs e)
        {
            Point pt = MousePosition;
            pt = PointToClient(pt);
            if (rcBegin.Contains(pt))
            {
                ChildDisplay();
                base.OnDoubleClick(e);
                return;
            }
            pt.X = (int)(pt.X - nWd / 2);
            pt.Y = (int)(pt.Y - nHt / 2);
            RefreshAll(pt.X, pt.Y);

            base.OnDoubleClick(e);
            NotifyRectangleChangeListeners();
        }

    }
}
