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

namespace DreamBeam
{



	/// <summary>
	/// Zusammenfassende Beschreibung für Class
	/// </summary>
	public class BitmapCreator
	{

    ///<summary>The final Bitmap for the BeamBox </summary>
	private Bitmap bmp = null;



		public BitmapCreator()
		{
			//
			// TODO: Hier die Konstruktorlogik einfügen
			//
		}


		/// <summary>
		/// Dispose components
		/// </summary>
		protected void Dispose (bool disposing)
		{
			if (disposing)
			{
/*				if (components != null)
				{
					components.Dispose();
				}*/
			}
		}


		///<summary>Selects which sort of Bitmap has to be Paint</summary>
/*		public Bitmap DrawBitmap(int Width,int Height){
//->		public Bitmap DrawBitmap(int Width,int Height){
  //			Bitmap bmp = null;

			switch(this.DrawWhat){
				case 0:
//					bmp = DrawSongBitmap(Width,Height);
					DrawSongBitmap2(Width,Height);
					return bmp;
				break;
				case 1:
//					bmp = DrawSermonBitmap(Width,Height);
//					DrawSermonBitmap(Width,Height);
					return bmp;
				break;
			}

			return bmp;
//->				return DrawSongBitmap(Width,Height);
		}*/


		/*
		///<summary>Paints a Bitmap from the Song Contents</summary>
		public DrawSongBitmap(int Width,int Heigt){
//->		public Bitmap DrawSongBitmap(int Width,int Heigt){
		MessageBox.Show ("1");
		Pen p;
			SolidBrush sdBrush1;
			sdBrush1 = new SolidBrush(Color.Black);

    		bmp.Dispose();
			bmp =  new Bitmap(Width,Height);
			graphics = Graphics.FromImage(bmp);
			graphics.InterpolationMode =InterpolationMode.HighQualityBicubic;
			graphics.PixelOffsetMode =PixelOffsetMode.HighQuality;

				if (OverWriteBG){
					if (this.ImageOverWritePath != null){
						if (this.ImageOverWritePath.Length > 0){
							if (this.strImageLoaded != this.ImageOverWritePath){
								curImage = Image.FromFile(this.ImageOverWritePath);
							}
							this.strImageLoaded = this.ImageOverWritePath;
						}
						graphics.DrawImage(curImage, 0, 0, Width, Height);
					}else{graphics.FillRectangle(sdBrush1, new Rectangle(0,0,this.Width, this.Height));}; //draw a blank rectangle if no image is defined.
				}else{
					if (Song.bg_image != null && Song.bg_image.Length > 0){
						if (this.strImageLoaded != Song.bg_image){
							curImage = Image.FromFile(this.Song.bg_image);
						}
						this.strImageLoaded = Song.bg_image;
						graphics.DrawImage(curImage, 0, 0, Width, Height);
					}else{graphics.FillRectangle(sdBrush1, new Rectangle(0,0,this.Width, this.Height));}; //draw a blank rectangle if no image is defined.
				}



			   graphics.SmoothingMode = SmoothingMode.AntiAlias;
				//declare Variables, needed in  loop
				GraphicsPath pth;


				string strTempText,temp;
				strTempText = "";
				for (int j = 0; j <=2; j++){
				if(Song.Text[j].Length > 0){

					strTempText = Song.Text[j];
					if (j == 1){
						//get strophe
						temp = strTempText;
						int x = 0;
						while (temp.IndexOf(strSeperator)>= 0) {
							if(x == Song.strophe){
								strTempText = temp.Substring(0,temp.IndexOf(strSeperator));
							}
							temp=temp.Substring(temp.IndexOf(strSeperator)+strSeperator.Length);
							x++;
						}
						if (x == Song.strophe){
						strTempText = temp;
						}
					}


					pth = new GraphicsPath();

					graphics.SmoothingMode = SmoothingMode.HighQuality;
					if (Song.TextEffect[j] == "Normal" || Song.TextEffect[j] == ""){

						   graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

						p=new Pen(Song.TextColor[j],1.0f);
						pth.AddString(strTempText,
						new FontFamily(Song.FontFace[j]),0,Song.FontSize[j],
						new Point(Song.posX[j],Song.posY[j]),StringFormat.GenericTypographic);
						sdBrush1 = new SolidBrush(Song.TextColor[j]);
						graphics.FillPath(sdBrush1,pth);
						graphics.DrawPath(p,pth);

					}else if (Song.TextEffect[j] == "Filled Outline"){
						p=new Pen(Song.TextColor[j],0.9f);
						pth.AddString(strTempText,
						new FontFamily(Song.FontFace[j]),0,Song.FontSize[j],
						new Point(Song.posX[j],Song.posY[j]),StringFormat.GenericTypographic);
						sdBrush1 = new SolidBrush(Song.TextColor[j]);
						graphics.FillPath(sdBrush1,pth);

						graphics.DrawPath(p,pth);

						GraphicsPath myPath = new GraphicsPath();
						graphics.DrawPath(Pens.Black, myPath);

						// Widen the path.
						Pen widenPen = new Pen(Song.OutlineColor[j], OutlineSize);
						System.Drawing.Drawing2D.Matrix widenMatrix = new System.Drawing.Drawing2D.Matrix();
						widenMatrix.Translate(0, 0);
						pth.Widen(widenPen, widenMatrix);
						graphics.FillPath(new SolidBrush(Song.OutlineColor[j]), pth);



						// Draw the original Text over the outline again.
						pth = new GraphicsPath();
						pth.AddString(strTempText,
						new FontFamily(Song.FontFace[j]),0,Song.FontSize[j],
						new Point(Song.posX[j],Song.posY[j]),StringFormat.GenericTypographic);
						graphics.DrawPath(p,pth);
						graphics.FillPath(sdBrush1,pth);

						//graphics.DrawPath(Pens.Black, myPath);

					   //	widenPen = new Pen(Color.Red, 1.0f);
					  //	pth.Widen(widenPen, widenMatrix);
						// Draw the widened path to the screen in red.
					   //	graphics.FillPath(new SolidBrush(Color.Black), pth);


					}else  if (Song.TextEffect[j] == "Outline"){
						p=new Pen(Song.OutlineColor[j],1.5f);
						pth.AddString(strTempText,
						new FontFamily(Song.FontFace[j]),0,Song.FontSize[j],
						new Point(Song.posX[j],Song.posY[j]),StringFormat.GenericTypographic);

						graphics.DrawPath(p,pth);
					}
				}
				}

		MessageBox.Show ("1");
//->			return bmp;

		}*/


	}
}
