using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Runtime.InteropServices;
using DirectShowLib;
using System.Resources;
using System.Globalization;
using System.Threading;


namespace DreamBeam
{
	/// <summary>
	/// Zusammenfassende Beschreibung für Class
	/// </summary>



	public class TextGraphics
	{

		protected ShowBeam _ShowBeam = null;
		protected Song _Song = null;

		public Image curImage;
		public Image curImage4Thread;

		public TextGraphics(ShowBeam impShowBeam, Song impSong)
		{
			_ShowBeam = impShowBeam;
			_Song = impSong;
		}

		public static int GetLongestLine(string Text){
			int length = 0;
			Text = Text + "\n";
			do{
				string tmp = Text.Substring(0,Text.IndexOf ("\n"));
				Text = Text.Substring(Text.IndexOf("\n")+1,Text.Length -Text.IndexOf("\n")-1);
				if(tmp.Length > length)
					length = tmp.Length;
			}while(Text.IndexOf("\n") != -1);
			return length;
		}

		#region GetTextSize
		public Size GetTextSize(int j, string strTempText){



			GraphicsPath pth = TextPath(j, strTempText,0,0,0);


			RectangleF r = new RectangleF();
			r = pth.GetBounds();

			Size p = new Size();
			p.Width  = (int) Math.Round(r.Width);
			p.Height = (int) Math.Round(r.Height);
	   //		MessageBox.Show (p.Height.ToString() + strTempText);


			if(_Song.MultiLang && j == 1){
				   GraphicsPath pth2 = TextPath(j, strTempText,1,0,0);
				   r = pth2.GetBounds();
				   if(r.Width > p.Width){
					p.Width = (int)Math.Round (r.Width);
				   }
				   GraphicsPath pth3 = TextPath(j, "just one line",1,0,0);
				   r = pth3.GetBounds();
				   p.Height = p.Height + (int) Math.Round (r.Height);

			}


			return p;
		}
		#endregion

		#region TextPath
//	 GraphicsPath
		public GraphicsPath TextPath(int j, string strTempText, int lineLang, int x, int y){
			GraphicsPath pth = new GraphicsPath();
			string tmpFontFace = _Song.FontFace[j];
			float tmpFontfSize = _Song.FontSize[j];
			bool DrawLine = true;
			bool origLang = false;



			
			StringFormat sf = new StringFormat();
			sf.Alignment = StringAlignment.Near;
				switch(_Song.TextAlign){
								case "left":
									sf.Alignment = StringAlignment.Near;
									if(_Song.AutoPos[2]){
											if(j == 2){
												sf.Alignment = StringAlignment.Far;
											}
									}
								break;
								case "center":
									//sf = new StringFormat.GenericTypographic;
									sf.Alignment = StringAlignment.Center;
								break;
								case "right":

									sf.Alignment = StringAlignment.Far;

								break;
							}


						// splits the Lines
						if(strTempText.Length > 0){
							string Input = strTempText +"\n";
							string Needle = "\n";
							strTempText = "";

							SizeF LineStringSize = new SizeF();
							SizeF LineStringSize2 = new SizeF();
							int i = 0;
							string strLine = "";



							while (Input.IndexOf(Needle)>= 0) {
								if(Input.Trim().Length > 0){
									if(Input.Trim().Substring(0,1) != "#"){


											// Check if this line is the language to draw

												if(origLang ){
													origLang = false;
													if(lineLang == 1)
													   DrawLine = true;
													else
													   DrawLine = false;
												}else{
													origLang = true;
													if(lineLang == 0)
													   DrawLine = true;
													else
													   DrawLine = false;
												}


										strLine = Input.Substring(0,Input.IndexOf(Needle));



									   // Define Sizes and Positions
									   if(_Song.MultiLang && j==1){

											//Definititions for Multilang Songs:

//											LineStringSize = graphics.MeasureString(strLine,new Font(_Song.FontFace[1],_Song.FontSize[1]));
//											LineStringSize.Width = (int)System.Math.Round((double)LineStringSize.Width*72/100);
//											LineStringSize.Height = (int)System.Math.Round((double)LineStringSize.Height*72/100);



											if(DrawLine){
												if(lineLang==0){
													// Definitions for first Lang:
													Graphics g = Graphics.FromImage(new Bitmap(100,100));
													LineStringSize2 =  g.MeasureString(strLine,new Font(_Song.FontFace[3],_Song.FontSize[3]));
													LineStringSize2.Width = (int)System.Math.Round((double)LineStringSize2.Width*72/100);
													LineStringSize2.Height = (int)System.Math.Round((double)LineStringSize2.Height*100/100);

													LineStringSize =  g.MeasureString(strLine,new Font(_Song.FontFace[1],_Song.FontSize[1]));
													LineStringSize.Width = (int)System.Math.Round((double)LineStringSize.Width*72/100);
													LineStringSize.Height = (int)System.Math.Round((double)LineStringSize.Height*72/100);

													int tmpi = i;
															tmpi = ((int)tmpi/2);


													g.Dispose();

													int tmpY= y+(int)(tmpi*(LineStringSize.Height*1))+(int)(tmpi*(LineStringSize2.Height*1))-(int)(LineStringSize2.Height/2);


													pth.AddString(strLine,
													  new FontFamily(_Song.FontFace[j]),Convert.ToInt32(_Song.FontStyle[j]),_Song.FontSize[j],
													  new Point(x,tmpY),sf);
												}
												else{

													// Definitions for Single Lang Songs:
													Graphics g = Graphics.FromImage(new Bitmap(100,100));

													LineStringSize2 =  g.MeasureString(strLine,new Font(_Song.FontFace[3],_Song.FontSize[3]));
													LineStringSize2.Width = (int)System.Math.Round((double)LineStringSize2.Width*72/100);
													LineStringSize2.Height = (int)System.Math.Round((double)LineStringSize2.Height*100/100);

													LineStringSize =  g.MeasureString(strLine,new Font(_Song.FontFace[1],_Song.FontSize[1]));
													LineStringSize.Width = (int)System.Math.Round((double)LineStringSize.Width*72/100);
													LineStringSize.Height = (int)System.Math.Round((double)LineStringSize.Height*72/100);

													g.Dispose();

													int tmpi = i-1;
													tmpi = ((int)tmpi/2);


												   //	int tmpY = y + (int)(tmpi*(LineStringSize2.Height*1)) + (int)((tmpi+1)*(LineStringSize.Height*1));;

												   // set position to the line before
													int tmpY= y+(int)(tmpi*(LineStringSize.Height*1))+(int)(tmpi*(LineStringSize2.Height*1))-(int)(LineStringSize2.Height/2);
													tmpY = tmpY + (int)((LineStringSize2.Height+LineStringSize.Height)/2);

													pth.AddString(strLine,
													  new FontFamily(_Song.FontFace[3]),Convert.ToInt32(_Song.FontStyle[3]),_Song.FontSize[3],
													  new Point(x,tmpY),sf);
												}
											}

									   }else{
/*
											GraphicsPath p = new GraphicsPath();
											p.AddString(strLine,
											  new FontFamily(_Song.FontFace[j]),Convert.ToInt32(_Song.FontStyle[j]),_Song.FontSize[j],
												new Point(0,0),sf);
*/

											// Definitions for Single Lang Songs:
											Graphics g = Graphics.FromImage(new Bitmap(100,100));
											LineStringSize =  g.MeasureString(strLine,new Font(_Song.FontFace[1],_Song.FontSize[1]));
											LineStringSize.Width = (int)System.Math.Round((double)LineStringSize.Width*72/100);
											LineStringSize.Height = (int)System.Math.Round((double)LineStringSize.Height*72/100);
											g.Dispose();

								   //		Tools.Beep(100, 30);
//											RectangleF r = new RectangleF();
//											r = p.GetBounds();
//											LineStringSize.Width = r.Width;
//											LineStringSize.Height  = r.Height;


//											System.Drawing.Rectangle rect = new System.Drawing.Rectangle(this.Size.Width/25,this.Size.Height/25,this.Size.Width-this.Size.Width/25,this.Size.Height-this.Size.Height/25);

											pth.AddString(strLine,
											  new FontFamily(_Song.FontFace[j]),Convert.ToInt32(_Song.FontStyle[j]),_Song.FontSize[j],
//											  new FontFamily(Song.FontFace[j]),0,Song.FontSize[j],
												new Point(x,y+(int)(i*(LineStringSize.Height*1))),sf);

									   }


										i++;
									}
								}
								Input=Input.Substring(Input.IndexOf(Needle)+Needle.Length);
							}
						}

			return pth;
		}
	#endregion

		#region Transform Path
		public void TransformPath(GraphicsPath pth){
			RectangleF r = new RectangleF();
			r = pth.GetBounds();

			PointF[] target = new PointF[4];

			// Top Left
			target[0].X = r.Left;
			target[0].Y = r.Top-100;

			// Top Right
			target[1].X = r.Right;
			target[1].Y = r.Top;

			// Bottom Left
			target[2].X = r.Left;
			target[2].Y = r.Bottom;

			// Bottom Right
			target[3].X = r.Right;
			target[3].Y = r.Bottom;///2;

		  try{
			pth.Warp(target,pth.GetBounds());
		  }catch(Exception e){MessageBox.Show(e.Message);}
          /*

		  */
		}
		#endregion

		#region RenderPath
		public void RenderPath(Graphics graphics,GraphicsPath pth,Color TextColor, Color OutlineColor, float OutlineSize, String Effect){

	  // 	TransformPath(pth);

			Pen pen;
			SolidBrush sdBrush1;

			graphics.SmoothingMode = SmoothingMode.HighQuality;
			graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

			try{
				graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
				pen=new Pen(TextColor,0.9f);
				pen.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;



/* ========= Shadow


				// Draw Shadow
					Bitmap ShadowBmp =  new Bitmap(_ShowBeam.Width,_ShowBeam.Height,PixelFormat.Format32bppArgb);
					Bitmap ShadowBmpTarget =  new Bitmap(_ShowBeam.Width,_ShowBeam.Height,PixelFormat.Format32bppArgb);
					Graphics ShadowGraphics = Graphics.FromImage(ShadowBmp);

					ShadowBmpTarget = ShadowBmpTarget.Clone (new Rectangle(0, 0, _ShowBeam.Width, _ShowBeam.Height),PixelFormat.Format32bppArgb);


					int SSize = 5;
					int SDist = 2;
					int SOpacity = 75;
					SOpacity =  255 - (int)(SOpacity *2.55);
					Color ShadowColor = Color.FromArgb(80,20,150);
					Pen ShadowPen = new Pen(ShadowColor,(int)SSize/2);
					SolidBrush ShadowBrush = new SolidBrush(ShadowColor);

					ShadowPen.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
//					System.Drawing.Drawing2D.Matrix ShadowMatrix = new System.Drawing.Drawing2D.Matrix();
//					ShadowMatrix.Translate(10, 10);

					GraphicsPath pth3 = new GraphicsPath();
					graphics.SmoothingMode = SmoothingMode.HighQuality;

					pth3 = (GraphicsPath)pth.Clone();
//					pth3.Transform(ShadowMatrix);


					ShadowGraphics.FillPath(ShadowBrush, pth3);
				  //	pth3.Widen(ShadowPen);
				  //	ShadowGraphics.DrawPath(ShadowPen,pth3);


					// Define all different Alpha Steps
					byte[] alphas = new byte[(SSize*4)+2];
						alphas[0] = 0;
						alphas[(SSize*4)+1] = (byte)SOpacity;
					for(byte i = 1;i<= SSize*4;i++){
						alphas[i] = (byte)System.Math.Round((double)(i*SOpacity/((SSize*4)+1)));

					}
					for(int i =0;i<= (SSize*4)+1;i++){
					   //	MessageBox.Show (i.ToString() +"-"+ alphas[i].ToString ());
					}
					// GDI+ still lies to us - the return format is BGR, NOT RGB.

					BitmapData bmDataSrc = ShadowBmp.LockBits(new Rectangle(0, 0, ShadowBmp.Width, ShadowBmp.Height),ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
					BitmapData bmDataTgt = ShadowBmpTarget.LockBits(new Rectangle(0, 0, ShadowBmp.Width, ShadowBmp.Height),ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

					int stride = bmDataSrc.Stride;
					System.IntPtr Scan0Src = bmDataSrc.Scan0;
					System.IntPtr Scan0Tgt = bmDataTgt.Scan0;
					unsafe
					{
						byte * pSrc = (byte *)(void *)Scan0Src;
						byte * pTgt = (byte *)(void *)Scan0Tgt;

						int nOffset = stride - ShadowBmp.Width*4;
						int line = (ShadowBmp.Width*4)+nOffset;

						for(int y=0;y < ShadowBmp.Height;++y)
						{
							for(int x=0; x < ShadowBmp.Width; ++x )
							{
								int Neighbors = 0;
								if(pSrc[3]>0)
									Neighbors++;
								byte * tmp = (byte *)(void *)Scan0Src;
									for(int i = 1;i<=SSize;i++){

										if(y>=i){
											tmp = pSrc-(i*line);
											if(tmp[3]>0)
												Neighbors ++;
										}

										if(ShadowBmp.Height-y>i){
											tmp = pSrc+(i*line);
											if(tmp[3]>0)
												Neighbors ++;
										}

										if(x>=i){
											tmp = pSrc-(i*4);
											if(tmp[3]>0)
												Neighbors ++;
										}

										if(ShadowBmp.Width-x>i){
											tmp = pSrc+(i*4);
											if(tmp[3]>0)
												Neighbors ++;
										}
									}
								  //	if(Neighbors>0)
									//	MessageBox.Show(y.ToString()+":"+Neighbors.ToString());
									  if(Neighbors >0){
										pTgt[3]=alphas[Neighbors];
										pTgt[0]=OutlineColor.B;
										pTgt[1]=OutlineColor.G;
										pTgt[2]=OutlineColor.R;
									  }
								//p[3]=(byte)100;
								pSrc += 4;
								pTgt += 4;
							}
							pSrc += nOffset;
							pTgt += nOffset;
						}
					}
					ShadowBmp.UnlockBits(bmDataSrc);
					ShadowBmpTarget.UnlockBits(bmDataTgt);


//					ShadowPen = new Pen(Color.FromArgb(SOpacity,ShadowColor.R,ShadowColor.G,ShadowColor.B),1);

					graphics.DrawImage(ShadowBmpTarget,0,0);

// ====== SHADOW */



				   //
//					pth3.
					//ShadowBrush = new SolidBrush(Color.Black);
				 //







				if (Effect == "Filled Outline"){
					GraphicsPath pth2 = new GraphicsPath();
					pth2 = (GraphicsPath)pth.Clone();
					Pen widenPen = new Pen(OutlineColor, OutlineSize);
					widenPen.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
					System.Drawing.Drawing2D.Matrix widenMatrix = new System.Drawing.Drawing2D.Matrix();
					widenMatrix.Translate(0, 0);
					pth2.Widen(widenPen, widenMatrix,(float)1/10000);
					graphics.FillPath(new SolidBrush(OutlineColor), pth2);
					pth2.Dispose();
				}

				// Draw Normal Text
				sdBrush1 = new SolidBrush(TextColor);
				graphics.FillPath(sdBrush1,pth);
				graphics.DrawPath(pen,pth);





			 } catch {}
		}
	#endregion


		#region DrawSongBitmap
			///<summary>Paints a Bitmap from the Song Contents</summary>
		public void DrawSongBitmap(Bitmap DrawerBmp,int Strophe,int iWidth,int iHeight,bool HideText) {

			_ShowBeam.DrawingSong = true;
			SolidBrush sdBrush1;
			sdBrush1 = new SolidBrush(_ShowBeam.BackgroundColor);

            Graphics graphics;
			graphics = Graphics.FromImage(DrawerBmp);
			graphics.InterpolationMode =InterpolationMode.HighQualityBicubic;
			graphics.PixelOffsetMode =PixelOffsetMode.HighQuality;

		#region Care about BG Image
		   try{
				string bmImagePath;
				if(_ShowBeam.OverWriteBG)
					bmImagePath = _ShowBeam.ImageOverWritePath;
				else
					bmImagePath = _ShowBeam.Song.bg_image;



				// Load BG Image, if enabled
					if (bmImagePath != null && bmImagePath.Length > 0 && _ShowBeam.HideBG == false) {
						if (_ShowBeam.strImageLoaded != bmImagePath) {
						   //Load BG Image
								if(_ShowBeam.Config.PreRender == false){
										try{curImage = Image.FromFile(bmImagePath);}finally{}
								}else if(Thread.CurrentThread.Name == "Render"){
									//bool loaded = false;
//									while(loaded == false){
										try {
											curImage4Thread = Image.FromFile(bmImagePath);
											//loaded = true;
										} finally {}
//									}
								}else{
									//bool loaded = false;
//									while(loaded == false){
										try {
											curImage = Image.FromFile(bmImagePath);
											//loaded = true;
										} finally {}
//									}
								}
						 }

						//Draw BG Image
						if(_ShowBeam.Config.PreRender == false || Thread.CurrentThread.Name != "Render" ){
							try{graphics.DrawImage(curImage, 0, 0, iWidth, iHeight); }catch(Exception e){MessageBox.Show(e.Message + "789");}
						}else if(Thread.CurrentThread.Name == "Render"){
							try{graphics.DrawImage(curImage4Thread, 0, 0, iWidth, iHeight); }catch(Exception e){MessageBox.Show(e.Message + "790");}
						}else{
							try{graphics.DrawImage(Image.FromFile(bmImagePath), 0, 0, iWidth, iHeight); }catch(Exception e){MessageBox.Show(e.Message + "791");}
						}
					} else {
						//draw a blank rectangle if no image is defined.
						try{graphics.FillRectangle(sdBrush1, new Rectangle(0,0,iWidth, iHeight)); }catch(Exception e){MessageBox.Show(e.Message);}
					}


			} catch {}
		#endregion

			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			//declare Variables, needed in  loop
			GraphicsPath pth;
			String[] Strophes = new String[100];
			string strTempText,strCurrentStrophe;
			strCurrentStrophe = _ShowBeam.Song.GetStrophe(Strophe);
			string strLongestStrophe = _ShowBeam.Song.GetLongestStrophe();
			string strWidestStrophe = _ShowBeam.Song.GetWidestStrophe();


			if (!HideText) {

				strTempText = "";
				for (int j = 0; j <=2; j++) {
					if(_ShowBeam.Song.GetText(j).Length > 0) {

						strTempText = _ShowBeam.Song.GetText(j);
						if(j == 1)
							strTempText = strCurrentStrophe;
						int[] tmpPosX =new int[3];
						int[] tmpPosY =new int[3];
						tmpPosX[j] = _ShowBeam.Song.posX[j];
						tmpPosY[j] = _ShowBeam.Song.posY[j];

				   //		#region GetSizes

							Size CaptionSize = this.GetTextSize(0,_ShowBeam.Song.GetText(0));

							Size StropheSize = GetTextSize(1,strCurrentStrophe);
							Size LongestStropheSize = GetTextSize(1,strLongestStrophe);

							Size WidestStropheSize = GetTextSize(1,strWidestStrophe);


							Size AuthorSize =  GetTextSize(2,_ShowBeam.Song.GetText(2));

				   //		#endregion



						#region LeftAlignPos
							if (j == 0 && _ShowBeam.Song.AutoPos[0]) {


								try{
									// if strophe is AutoPos
									if (_ShowBeam.Song.AutoPos[1]) {

										tmpPosX[j] = (int)System.Math.Round((double)((iWidth - WidestStropheSize.Width) /2));

											tmpPosY[j] = (int)System.Math.Round((double)(((iHeight - LongestStropheSize.Height)/4)-(int)System.Math.Round((double)(CaptionSize.Height/2))));

									} else {
										tmpPosY[0] = ((int)System.Math.Round((double)(tmpPosY[1]/2)) -(int)System.Math.Round((double)(CaptionSize.Height/2)));
									}

								} catch {}
							}
							if (j == 1 && _ShowBeam.Song.AutoPos[1]) {
								try{
								  if(CaptionSize.Height == 0)
									tmpPosY[0]=0;

									tmpPosX[j] = (int)System.Math.Round((double)(iWidth-WidestStropheSize.Width) /2);
									tmpPosY[j] = (int)System.Math.Round((double)(((iHeight - _ShowBeam.Song.posY[0]) - StropheSize.Height)/2) + tmpPosY[0]);

								} catch {}
							}
							if (j == 2 && _ShowBeam.Song.AutoPos[2]) {
								try{
									tmpPosX[j] = (int)System.Math.Round((double)(iWidth - iWidth/300));
									tmpPosY[j] = (int)System.Math.Round((double)(iHeight - AuthorSize.Height)-(iHeight/100));
								} catch {}
							}

						 #endregion

						#region CenterPosition
						 if(_ShowBeam.Song.TextAlign == "center" && _ShowBeam.Song.AutoPos[j]){

							tmpPosX[j] = (int)System.Math.Round((double)iWidth/2);
						 }
						#endregion

						#region RightAlignPos
						 if(_ShowBeam.Song.TextAlign == "right" && _ShowBeam.Song.AutoPos[j]){

							if(j == 0)
								tmpPosX[j] = iWidth - tmpPosX[0];
							if(j == 1)
								tmpPosX[j] = tmpPosX[0];

						 }
						#endregion

						StringFormat sf = new StringFormat();


						if(_ShowBeam.DrawSongItem(j)){

									pth = new GraphicsPath();
									pth = TextPath(j,strTempText,0,tmpPosX[j],tmpPosY[j]);

									RenderPath(graphics,pth,_ShowBeam.Song.TextColor[j], _ShowBeam.Song.OutlineColor[j], _ShowBeam.OutlineSize, _ShowBeam.Song.TextEffect[j]);

									if(_ShowBeam.Song.MultiLang){
										pth = TextPath(j,strTempText,1,tmpPosX[j],tmpPosY[j]);
										RenderPath(graphics,pth,_ShowBeam.Song.TextColor[3], _ShowBeam.Song.OutlineColor[3], _ShowBeam.OutlineSize, _ShowBeam.Song.TextEffect[j]);
									}

						}

					}
				}

			}
			sdBrush1.Dispose();
			graphics.Dispose();
			_ShowBeam.DrawingSong = false;

		}
		#endregion


	}
}






