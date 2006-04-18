
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Collections;
using System.Windows.Forms;
using System.Threading;

namespace DreamBeam
{
	/// <summary>
	/// Zusammenfassende Beschreibung für Class
	/// </summary>
	public class Prerenderer
	{

		public ArrayList RenderedSongs = new ArrayList();
		protected ShowBeam _ShowBeam = null;
		protected Song _Song = null;
		static Thread Thread_Render = null;
		bool RepeatReandering = false;


		private System.Windows.Forms.Timer ReRenderTimer;

		public Prerenderer(ShowBeam impShowBeam, Song impSong)
		{
			_ShowBeam = impShowBeam;
			_Song = impSong;

		   Thread_Render = new Thread(new ThreadStart(RenderAll));
			Thread_Render.Priority = ThreadPriority.Lowest;
			Thread_Render.IsBackground = true;
			this.ReRenderTimer = new System.Windows.Forms.Timer();
			//
			// ReRenderTimer
			//
			this.ReRenderTimer.Interval = 1000;
			this.ReRenderTimer.Tick += new System.EventHandler(this.ReRenderTimer_Tick);
			this.ReRenderTimer.Enabled = true;

		}


		public void RenderAllThreaded(){
			if(_ShowBeam.Config.PreRender){
				if(Thread_Render.IsAlive){
					RepeatReandering = true;
					Thread.Sleep(200);
					Thread_Render.Abort(); //Beta -trying to kill old prerenderer
				  /*	while(Thread_Render.IsAlive){
						Thread.Sleep(1);

					}  */
				}else{
						Thread_Render = new Thread(new ThreadStart(RenderAll));
						Thread_Render.Priority = ThreadPriority.Lowest;
						Thread_Render.Name = "Render";
						Thread_Render.IsBackground = true;
						Thread_Render.Start();
				}
			}
		}

		public void RenderAll(){
			this.RepeatReandering = false;
			ClearOld();
			int strophes = _Song.CountStrophes();
			_ShowBeam._MainForm.RenderStatus.Maximum = strophes;
				_ShowBeam._MainForm.RenderStatus.Visible = true;

			for (int i = -1; i < strophes;i++){
				if(RepeatReandering){
					_ShowBeam._MainForm.RenderStatus.Visible = false;
					return;
				}
				_ShowBeam._MainForm.RenderStatus.Value = i+1;
				RenderOne(i);
			}

			_ShowBeam._MainForm.RenderStatus.Visible = false;

		}

		void RenderOne(int i){
				bool found = false;
				foreach(RenderedSong s in RenderedSongs){
					if(s.strophe == i){
						found = true;
					}
				}
				if(found == false){AddStrophe(i);}
		}

		public Bitmap GetStrophe(int i){


			if(_ShowBeam.Config.PreRender && Thread_Render.IsAlive == false){

				if(_ShowBeam.HideText)
					i = -1;
				ClearOld();
				RenderOne(i);
				foreach(RenderedSong s in RenderedSongs){
					if(s.strophe == i){

						return s.bmp;
					}
				}
				return(new Bitmap(_ShowBeam.Width,_ShowBeam.Height,PixelFormat.Format32bppArgb));
			}else{
				Bitmap b = new Bitmap(_ShowBeam.Width,_ShowBeam.Height,PixelFormat.Format32bppArgb);
				_ShowBeam.TextGraphics.DrawSongBitmap(b,i,_ShowBeam.Width,_ShowBeam.Height,_ShowBeam.HideText);
				return b;

			}
		}



		public void ClearOld(){
			ArrayList tmp = new ArrayList();
			int i = 0;
			foreach(RenderedSong s in RenderedSongs){

						if(VerifyHash(s.strophe,s.hash)==false){
							tmp.Add (i);
						}
						i++;
			}

			i = 0;
			foreach(int x in tmp){
			   RenderedSongs.RemoveAt(x - i);
			   i++;
			}


		}

		public void AddStrophe(int i){


			bool HideText = false;
			if(i == -1){
				HideText = true;
			}

			Bitmap b = new Bitmap(_ShowBeam.Width,_ShowBeam.Height,PixelFormat.Format32bppArgb);
			_ShowBeam.TextGraphics.DrawSongBitmap(b,i,_ShowBeam.Width,_ShowBeam.Height,HideText);
			RenderedSongs.Add(new RenderedSong(i,GetHash(i),b));
		}


		private void ReRenderTimer_Tick(object sender, System.EventArgs e) {
			if(this.RepeatReandering){
				if(Thread_Render.IsAlive == false){
				 this.RepeatReandering = false;
				 RenderAllThreaded();
				}
			}
		}


		#region HashTools
		public string GetHash(int i){
		   return SimpleHash.ComputeHash(_Song.SerializeStropheSettings(i)+SerializeSettings(),"SHA1",null);
		}


		public bool VerifyHash(int i,string hash){
			return SimpleHash.VerifyHash(_Song.SerializeStropheSettings(i)+SerializeSettings(),"SHA1",hash);
		}

				public string SerializeSettings(){
			string Settings = "Settings:";

				Settings += Convert.ToString(_ShowBeam.HideBG);
				Settings += Convert.ToString(_ShowBeam.HideTitle);
				Settings += Convert.ToString(_ShowBeam.HideVerse);
				Settings += Convert.ToString(_ShowBeam.HideAuthor);
				if(_ShowBeam.OverWriteBG){
					Settings += _ShowBeam.ImageOverWritePath;
				}

			return Settings;
		}

		#endregion
	}



	#region RenderedSongClass
	public class RenderedSong
	{
	 public int strophe = 0;
	 public string hash = "";
	 public Bitmap bmp = null;

	 public RenderedSong(int s,string h, Bitmap b){
		strophe = s;
		hash = h;
		bmp = b;
	 }

	}
	#endregion

}
