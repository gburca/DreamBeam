using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace DreamBeam
{
    public delegate void RectangleChangeHandler();

    public class CDrawDragRect : Panel
    {
        public Rectangle rcLT, rcRT, rcLB, rcRB, rcLT2, rcRT2, rcLB2, rcRB2, rcTM, rcBM, rcLM,rcRM;
        Cursor OldCur ;

        public int nIsBounce;
        int nThatsIt;
        public int nWd;
        public int nHt;        
        int nResizeRT,nResizeBL, nResizeLT,nResizeRB;
        int nResizeTM, nResizeBM, nResizeLM, nResizeRM;

        public Rectangle rcBone;
        int nBone = 0;
        Point ptOld;
        Point ptNew;
        Point ptTmpStart;
        Point ptStart;
        int nBroderSize;
        public Rectangle rcOld, rcNew;
        Region rgnDiffCombined;
        Region rgnDiffOld, rgnDiffNew;
        Region rgnTmpDeflated, rgnTmpPreDefalted;
        Rectangle rcTmp;
        protected bool _showrect = false;
        private bool mousemoved = false;

        public bool ShowRect
        {
            get { return _showrect; }
            set { _showrect = value; 
                if (this._imagepack != null)this.Refresh(); 
            }

        }

        protected ResizedImagePack _imagepack;
      

        public const uint PATINVERT = (uint)0x005A0049; /* dest = pattern XOR dest         */
        public const uint BLACKNESS = (uint)0x00000042; /* dest = BLACK                    */
        public const uint PATCOPY = (uint)0x00F00021;/* dest = pattern                  */
        [DllImport("gdi32.dll")]
        static extern bool PatBlt(IntPtr hdc, int nXLeft, int nYLeft, int nWidth,
                                    int nHeight, uint dwRop);
        [DllImport("gdi32.dll")]
        static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        [DllImport("gdi32.dll")]
        static extern int SelectClipRgn(IntPtr hdc, IntPtr hrgn);

        [DllImport("gdi32.dll", EntryPoint = "CreateSolidBrush", SetLastError = true)]
        public static extern IntPtr CreateSolidBrush(int crColor);

        [DllImport("gdi32.dll")]
        static extern bool DeleteObject(IntPtr hObject);
        public Rectangle rcOriginal;
        public Rectangle rcBegin;
        public CDrawDragRect()
        {
            nWd = 0;
            nHt = 0;
            RefreshAll(100,100);            
        }

        public void RefreshAll(int x, int y)
        {
            nThatsIt = 0;
            nResizeRT = 0;
            nResizeBL = 0;
            nResizeLT = 0;
            nResizeRB = 0;
            nBroderSize = 2;
            if(rgnDiffCombined != null)
                rgnDiffCombined.Dispose();
            rgnDiffCombined = new Region();
            if (rgnDiffOld != null  )
                rgnDiffOld.Dispose();
            rgnDiffOld = new Region();

            rcTmp = new Rectangle(0, 0, 0, 0);
            rcOriginal = new Rectangle(0, 0, 0, 0);
            ptOld = new Point(0, 0);
            int nSize = 6;
            if(nWd ==0)
                nWd = 150;
            if (nHt == 0)
                nHt = 150;
            rcNew = rcOld = rcBone = new Rectangle(x,y, nWd, nHt);
            
            // Border Rectangles
            rcLT = new Rectangle(0, 0, nSize, nSize);
            rcRT = new Rectangle(0, 0, nSize, nSize);
            rcLB = new Rectangle(0, 0, nSize, nSize);
            rcRB = new Rectangle(0, 0, nSize, nSize);
            
            // Middle Rectangles
            rcLM = new Rectangle(0, 0, nSize, nSize);
            rcRM = new Rectangle(0, 0, nSize, nSize);
            rcTM = new Rectangle(0, 0, nSize, nSize);
            rcBM = new Rectangle(0, 0, nSize, nSize);

            rcLT2 = new Rectangle(0, 0, nSize-2, nSize-2);
            rcRT2 = new Rectangle(0, 0, nSize-2, nSize-2);
            rcLB2 = new Rectangle(0, 0, nSize-2, nSize-2);
            rcRB2 = new Rectangle(0, 0, nSize-2, nSize-2);
            AdjustResizeRects();
            OldCur = Cursor;
            nIsBounce = 0;
            rcBegin = new Rectangle();            
            Invalidator();
        }
        

        protected virtual void Invalidator(){
            //Invalidate();
        }

        public void AdjustResizeRects()
        {
            rcLT.X = rcBone.Left+1;
            rcLT.Y = rcBone.Top+1;
         
            rcRT.X = rcBone.Right - rcRT.Width;
            rcRT.Y = rcBone.Top+1;
            
            rcLB.X = rcBone.Left+1;
            rcLB.Y = rcBone.Bottom - rcLB.Height;
            
            rcRB.X = rcBone.Right - rcRB.Width;
            rcRB.Y = rcBone.Bottom - rcRB.Height;



            rcTM.X = rcBone.Left + (rcBone.Width / 2)-2;
            rcTM.Y = rcBone.Top + 1;

            rcBM.X = rcBone.Left + (rcBone.Width / 2)-2;
            rcBM.Y = rcBone.Bottom - rcRB.Height;

            rcLM.X = rcBone.Left + 1;
            rcLM.Y = rcBone.Top + (rcBone.Height /2)-2;

            rcRM.X = rcBone.Right - rcRB.Width;
            rcRM.Y = rcBone.Top + (rcBone.Height / 2) -2;
        }

        public int RGB(int r, int g, int b)
        {
            return ((int)(((byte)(r) | ((ushort)((byte)(g)) << 8)) | (((ushort)(byte)(b)) << 16)));
        }

        void DrawDragRect(MouseEventArgs e)
        {
            Graphics grfxClient = CreateGraphics();
            if (e.Button == MouseButtons.Left)
            {
                RectangleF rcF; 
                Point point = new Point(e.X, e.Y);
                ptNew = point;
                
                rcTmp = rcOld;
                rgnTmpPreDefalted = new Region(new Rectangle(rcTmp.Left, rcTmp.Top, rcTmp.Width, rcTmp.Height));
                rcTmp.Inflate(-nBroderSize, -nBroderSize);
                rgnTmpDeflated = new Region(new Rectangle(rcTmp.Left, rcTmp.Top, rcTmp.Width, rcTmp.Height));

                rgnDiffOld = rgnTmpDeflated;
                rgnDiffOld.Xor(rgnTmpPreDefalted);

                rcF = rgnDiffCombined.GetBounds(grfxClient);
                rcTmp.X = (int)rcF.X;
                rcTmp.Y = (int)rcF.Y;
                rcTmp.Width = (int)rcF.Width;
                rcTmp.Height = (int)rcF.Height;

                rcTmp = rcNew;
                rgnTmpPreDefalted = new Region(new Rectangle(rcTmp.Left, rcTmp.Top, rcTmp.Width, rcTmp.Height));
                rcTmp.Inflate(-nBroderSize, -nBroderSize);
                rgnTmpDeflated = new Region(new Rectangle(rcTmp.Left, rcTmp.Top, rcTmp.Width, rcTmp.Height));

                rgnDiffNew = rgnTmpDeflated;
                rgnDiffNew.Xor(rgnTmpPreDefalted);

                rgnDiffCombined = rgnDiffNew;
                //After this step rgnDiffCombined is a region that is the Union of 
                //the boundaries of rcOld and rcNew
                rgnDiffCombined.Xor(rgnDiffOld);

                rcF = rgnDiffCombined.GetBounds(grfxClient);
                rcTmp.X = (int)rcF.X;
                rcTmp.Y = (int)rcF.Y;
                rcTmp.Width = (int)rcF.Width;
                rcTmp.Height = (int)rcF.Height;

                IntPtr hBr = CreateSolidBrush(RGB(255, 255, 0));
                IntPtr hrgn = rgnDiffCombined.GetHrgn(grfxClient);
                IntPtr hDc = grfxClient.GetHdc();

                SelectClipRgn(hDc, hrgn);
                IntPtr hOldBrush = SelectObject(hDc, hBr);

                PatBlt(hDc, rcTmp.Left, rcTmp.Top, rcTmp.Width, rcTmp.Height, PATINVERT);

                SelectObject(hDc, hOldBrush);
                DeleteObject(hBr);
                grfxClient.ReleaseHdc(); //Do this else expect GDI memory Leak
                rgnDiffCombined.ReleaseHrgn(hrgn);//Do this else expect GDI memory Leak
                rcOld = rcNew;
            }
            grfxClient.Dispose();
        }
     
        public virtual void ChildDisplay()
        {
        }
     
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (nThatsIt == 0 || !mousemoved) return;
            
            mousemoved = false;         
            nBone = 0;
            //Invalidate();
            nResizeRB = 0;
            nResizeBL = 0;
            nResizeRT = 0;
            nResizeLT = 0;

            nResizeBM = 0;
            nResizeTM = 0;
            nResizeRM = 0;
            nResizeLM = 0;
            

            if (rcBone.Width <= 0 || rcBone.Height <= 0)
                rcBone = rcOriginal;

            if (rcBone.Right > ClientRectangle.Width)
                rcBone.Width = ClientRectangle.Width - rcBone.X;

            if (rcBone.Bottom > ClientRectangle.Height)
                rcBone.Height = ClientRectangle.Height - rcBone.Y;

            if (rcBone.X < 0)
                rcBone.X = 0;

            if (rcBone.Y < 0)
                rcBone.Y = 0;


            //AdjustResizeRects();
            
            base.OnMouseUp(e);
                     
            nWd = rcNew.Width;
            nHt = rcNew.Height;
            rcBegin = rcNew;
            nWd = rcNew.Width;
            nHt = rcNew.Height;
            NotifyRectangleChangeListeners();
        }    
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (_showrect)
            {
                Point pt = new Point(e.X, e.Y);
                rcOriginal = rcBone;
                rcBegin = rcBone;
                if (rcRB.Contains(pt))
                {
                    rcOld = new Rectangle(rcBone.X, rcBone.Y, rcBone.Width, rcBone.Height);
                    rcNew = rcOld;
                    nResizeRB = 1;
                }
                else if (rcLB.Contains(pt))
                {
                    rcOld = new Rectangle(rcBone.X, rcBone.Y, rcBone.Width, rcBone.Height);
                    rcNew = rcOld;
                    nResizeBL = 1;
                }
                else if (rcRT.Contains(pt))
                {
                    rcOld = new Rectangle(rcBone.X, rcBone.Y, rcBone.Width, rcBone.Height);
                    rcNew = rcOld;
                    nResizeRT = 1;
                }
                else if (rcLT.Contains(pt))
                {
                    rcOld = new Rectangle(rcBone.X, rcBone.Y, rcBone.Width, rcBone.Height);
                    rcNew = rcOld;
                    nResizeLT = 1;
                }
                else if (rcTM.Contains(pt))
                {
                    rcOld = new Rectangle(rcBone.X, rcBone.Y, rcBone.Width, rcBone.Height);
                    rcNew = rcOld;
                    nResizeTM = 1;
                }
                else if (rcBM.Contains(pt))
                {
                    rcOld = new Rectangle(rcBone.X, rcBone.Y, rcBone.Width, rcBone.Height);
                    rcNew = rcOld;
                    nResizeBM = 1;
                }
                else if (rcLM.Contains(pt))
                {
                    rcOld = new Rectangle(rcBone.X, rcBone.Y, rcBone.Width, rcBone.Height);
                    rcNew = rcOld;
                    nResizeLM = 1;
                }
                else if (rcRM.Contains(pt))
                {
                    rcOld = new Rectangle(rcBone.X, rcBone.Y, rcBone.Width, rcBone.Height);
                    rcNew = rcOld;
                    nResizeRM = 1;
                }
                else if (rcBone.Contains(pt))
                {
                    nBone = 1;
                    ptNew = ptOld = pt;
                }
                nThatsIt = 1;
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (_showrect)
            {

                Point pt = new Point(e.X, e.Y);

                if (rcLT.Contains(pt)) Cursor = Cursors.SizeNWSE;
                else if (rcRT.Contains(pt)) Cursor = Cursors.SizeNESW;
                else if (rcLB.Contains(pt)) Cursor = Cursors.SizeNESW;
                else if (rcRB.Contains(pt)) Cursor = Cursors.SizeNWSE;
                else if (rcTM.Contains(pt) || rcBM.Contains(pt)) Cursor = Cursors.SizeNS;
                else if (rcLM.Contains(pt) || rcRM.Contains(pt)) Cursor = Cursors.SizeWE;
                else if (rcBone.Contains(pt)) Cursor = Cursors.Hand;
                else
                {
                    Cursor = Cursors.Default;
                    nBone = 0;
                }


                if (e.Button == MouseButtons.Left)
                {
                    //Graphics dcClient = CreateGraphics();
                    //dcClient.DrawLine(Pens.Red, new Point(0,rcBegin.Y), new Point(500, rcBegin.Y));
                    //dcClient.DrawLine(Pens.Red, new Point(rcBegin.X, 0), new Point(rcBegin.X, 500));
                    //dcClient.DrawLine(Pens.Red, new Point(rcBone.X, 0), new Point(rcBone.X, 500));
                    //dcClient.Dispose();

                    if (nResizeRB == 1) //Bottom:Right
                    {
                        mousemoved = true;
                        rcNew.X = rcBone.X;
                        rcNew.Y = rcBone.Y;
                        rcNew.Width = pt.X - rcNew.Left;
                        rcNew.Height = pt.Y - rcBone.Top;
                        if (nIsBounce == 0)
                        {
                            //method 2
                            //if (rcNew.X > rcNew.Right)
                            if (rcNew.X > pt.X)
                            {
                                rcNew.X = pt.X;
                                rcNew.Width = rcBegin.X - pt.X;

                                nResizeRB = 0;
                                nResizeBL = 1;
                                rcBegin = rcNew;
                            }
                            if (rcNew.Y > pt.Y)
                            //if (rcNew.Y > rcNew.Bottom)
                            {
                                //rcNew.X = rcBegin.X;
                                rcNew.Y = pt.Y;
                                rcNew.Height = rcBegin.Y - pt.Y;

                                nResizeRB = 0;
                                nResizeRT = 1;
                                rcBegin = rcNew;
                            }
                        }
                        else
                        {
                            //Method 1
                            if (rcNew.X > rcNew.Right)
                            {
                                rcNew.Offset(-nWd, 0);
                                if (rcNew.X < 0)
                                    rcNew.X = 0;
                            }
                            if (rcNew.Y > rcNew.Bottom)
                            {
                                rcNew.Offset(0, -nHt);
                                if (rcNew.Y < 0)
                                    rcNew.Y = 0;
                            }
                        }
                        DrawDragRect(e);
                        rcOld = rcBone = rcNew;
                        //rcBone = rcNew;
                        //rcOld = rcNew;
                        Cursor = Cursors.SizeNWSE;
                    }
                    else
                        if (nResizeBL == 1)
                        {
                            mousemoved = true;
                            rcNew.X = pt.X;
                            rcNew.Y = rcBone.Y;
                            rcNew.Width = rcBone.Right - pt.X;
                            rcNew.Height = pt.Y - rcNew.Top;

                            if (nIsBounce == 0)
                            {
                                //Method 2
                                if (pt.X > rcNew.Right)
                                //if (rcNew.X > rcNew.Right)
                                {
                                    rcNew.X = rcBegin.Right;
                                    //rcNew.X = pt.X;
                                    rcNew.Width = rcBegin.Right - pt.X;

                                    nResizeBL = 0;
                                    nResizeRB = 1;
                                    rcBegin = rcNew;
                                }
                                if (pt.Y < rcNew.Y)
                                //if (rcNew.Y > rcNew.Bottom)
                                {
                                    rcNew.Y = rcBegin.Y;
                                    rcNew.Height = rcNew.Y - rcBegin.Top;

                                    //rcNew.Y = pt.Y;
                                    //rcNew.Height = rcBegin.Top - pt.Y;

                                    nResizeBL = 0;
                                    nResizeLT = 1;
                                    rcBegin = rcNew;
                                }
                            }
                            else
                            {
                                //method One
                                if (rcNew.X > rcNew.Right)
                                {
                                    rcNew.Offset(nWd, 0);
                                    if (rcNew.Right > ClientRectangle.Width)
                                        rcNew.Width = ClientRectangle.Width - rcNew.X;
                                }
                                if (rcNew.Y > rcNew.Bottom)
                                {
                                    rcNew.Offset(0, -nHt);
                                    if (rcNew.Y < 0)
                                        rcNew.Y = 0;
                                }
                            }
                            DrawDragRect(e);
                            rcOld = rcBone = rcNew;
                            Cursor = Cursors.SizeNESW;
                        }
                        else if (nResizeRT == 1)
                        {
                            mousemoved = true;
                            rcNew.X = rcBone.X;
                            rcNew.Y = pt.Y;
                            rcNew.Width = pt.X - rcNew.Left;
                            rcNew.Height = rcBone.Bottom - pt.Y;

                            if (nIsBounce == 0)
                            {
                                //method Two
                                if (pt.X < rcNew.X)
                                //if (rcNew.X > rcNew.Right)
                                {
                                    rcNew.X = pt.X;
                                    rcNew.Width = rcBegin.X - pt.X;
                                    nResizeRT = 0;
                                    nResizeLT = 1;
                                    rcBegin = rcNew;
                                }
                                if (pt.Y > rcNew.Bottom)
                                //if (rcNew.Y > rcNew.Bottom)
                                {
                                    rcNew.Y = rcBegin.Bottom;
                                    rcNew.Height = pt.Y - rcBegin.Bottom;
                                    nResizeRT = 0;
                                    nResizeRB = 1;
                                    rcBegin = rcNew;
                                }
                            }


                            else
                            {
                                //method One
                                if (rcNew.X > rcNew.Right)
                                {
                                    rcNew.Offset(-nWd, 0);
                                    if (rcNew.X < 0)
                                        rcNew.X = 0;
                                }
                                if (rcNew.Y > rcNew.Bottom)
                                {
                                    rcNew.Offset(0, nHt);
                                    if (rcNew.Bottom > ClientRectangle.Height)
                                        rcNew.Y = ClientRectangle.Height - rcNew.Height;
                                }
                            }
                            DrawDragRect(e);
                            rcOld = rcBone = rcNew;
                            Cursor = Cursors.SizeNESW;
                        }
                        else if (nResizeLT == 1)
                        {
                            mousemoved = true;
                            rcNew.X = pt.X;
                            rcNew.Y = pt.Y;
                            rcNew.Width = rcBone.Right - pt.X;
                            rcNew.Height = rcBone.Bottom - pt.Y;

                            if (nIsBounce == 0)
                            {
                                //Method Two;
                                if (pt.X > rcNew.Right)
                                //if (rcNew.X > rcNew.Right)
                                {
                                    rcNew.X = rcBegin.Right;
                                    rcNew.Width = pt.X = rcBegin.Right;
                                    nResizeLT = 0;
                                    nResizeRT = 1;
                                    rcBegin = rcNew;
                                }
                                if (pt.Y > rcNew.Bottom)
                                //if (rcNew.Y > rcNew.Bottom)
                                {
                                    rcNew.Y = rcBegin.Bottom;
                                    rcNew.Height = pt.Y - rcBegin.Bottom;
                                    nResizeLT = 0;
                                    nResizeBL = 1;
                                    rcBegin = rcNew;
                                }
                            }
                            else
                            {
                                //method One
                                if (rcNew.X > rcNew.Right)
                                {
                                    rcNew.Offset(nWd, 0);
                                    if (rcNew.Right > ClientRectangle.Width)
                                        rcNew.Width = ClientRectangle.Width - rcNew.X;
                                }
                                if (rcNew.Y > rcNew.Bottom)
                                {
                                    rcNew.Offset(0, nHt);
                                    if (rcNew.Bottom > ClientRectangle.Height)
                                        rcNew.Height = ClientRectangle.Height - rcNew.Y;
                                }
                            }

                            DrawDragRect(e);
                            rcOld = rcBone = rcNew;
                            Cursor = Cursors.SizeNWSE;
                        }

                        // Modified 1
                        else if (nResizeTM == 1)
                        {
                            mousemoved = true;
                            rcNew.X = rcBone.Left;
                            rcNew.Y = pt.Y;
                            rcNew.Width = rcBone.Right - rcBone.Left;
                            rcNew.Height = rcBone.Bottom - pt.Y;
                            if (nIsBounce == 0)
                            {
                                if (pt.Y > rcNew.Bottom - 7)
                                {
                                    rcNew.Y = rcBone.Bottom - 8;
                                    rcNew.Height = 8;
                                }
                            }
                            DrawDragRect(e);
                            rcOld = rcBone = rcNew;
                            Cursor = Cursors.SizeNS;
                        }

                    // Modified 2
                        else if (nResizeBM == 1)
                        {
                            mousemoved = true;
                            rcNew.X = rcBone.Left;
                            rcNew.Y = rcBone.Top;
                            rcNew.Width = rcBone.Right - rcBone.Left;
                            rcNew.Height = pt.Y - rcNew.Top;
                            //rcBone.Bottom - pt.Y;
                            if (nIsBounce == 0)
                            {
                                if (pt.Y < rcNew.Top + 7)
                                {
                                    rcNew.Height = 8;
                                }
                            }
                            DrawDragRect(e);
                            rcOld = rcBone = rcNew;
                            Cursor = Cursors.SizeNS;
                        }

                       // Modified 3
                        else if (nResizeLM == 1)
                        {
                            mousemoved = true;
                            rcNew.X = pt.X;
                            rcNew.Y = rcBone.Top;
                            rcNew.Width = rcBone.Right - pt.X;
                            rcNew.Height = rcBone.Bottom - rcBone.Top;
                            //rcBone.Bottom - pt.Y;
                            if (nIsBounce == 0)
                            {
                                if (pt.X > rcBone.Right - 9)
                                {
                                    Console.WriteLine("Event");
                                    rcNew.X = rcBone.Right - 10;
                                    rcNew.Width = 10;
                                }
                            }
                            DrawDragRect(e);
                            rcOld = rcBone = rcNew;
                            Cursor = Cursors.SizeWE;
                        }
                        // Modified 4
                        else if (nResizeRM == 1)
                        {
                            mousemoved = true;
                            rcNew.X = rcBone.Left;
                            rcNew.Y = rcBone.Top;
                            rcNew.Width = pt.X - rcBone.Left;
                            rcNew.Height = rcBone.Bottom - rcBone.Top;
                            //rcBone.Bottom - pt.Y;
                            if (nIsBounce == 0)
                            {
                                if (pt.X < rcNew.Left + 9)
                                {
                                    rcNew.Width = 10;
                                }
                            }
                            DrawDragRect(e);
                            rcOld = rcBone = rcNew;
                            Cursor = Cursors.SizeWE;
                        }

                        else
                            if (nBone == 1) //Moving the rectangle
                            {

                                mousemoved = true;
                                ptNew = pt;
                                int dx = ptNew.X - ptOld.X;
                                int dy = ptNew.Y - ptOld.Y;
                                rcBone.Offset(dx, dy);
                                rcNew = rcBone;
                                DrawDragRect(e);
                                ptOld = ptNew;                                
                            }

                }
            }
            base.OnMouseMove(e);
        }


        // Declare the event, which is associated with our
        // delegate SubmitClickedHandler(). Add some attributes
        // for the Visual C# control property.
        [Category("Action")]
        [Description("Fires when Rectangle was changed.")]
        public event RectangleChangeHandler RectangleChangedEvent;

        private void RectangleChanged(Object sender, EventArgs e)
        {
            // If we're in the process of changing all the controls, wait until
            // they're all changed before firing the event.
            if (_imagepack != null)
            {
                NotifyRectangleChangeListeners();
            }
        }

        protected void NotifyRectangleChangeListeners()
        {
            if (RectangleChangedEvent != null)
            {
                RectangleChangedEvent();
            }
        }

    }
}
