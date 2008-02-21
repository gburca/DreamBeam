/*
DreamBeam - a Church Song Presentation Program
Copyright (C) 2008 Gabriel Burca

This program is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation; either version 2
of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.

*/

using System;
using Microsoft.DirectX.AudioVideoPlayback;
using AxShockwaveFlashObjects;

namespace DreamBeam {

	public interface IMediaOperations {
		void SkipBk();
		void SeekBk();
		void Play();
		void Pause();
		void Stop();
		void SeekFw();
		void SkipFw();

		void SkipTo(double location);

		// Maybe these should go in a IMediaProperties interface
		double Duration();
	}

	public class MediaFlash : IMediaOperations {
		AxShockwaveFlash flash = null;
		public int seekAmount = 10;

		public MediaFlash(AxShockwaveFlash axShockwaveFlash) {
			flash = axShockwaveFlash;
		}

		protected void Seek(int amount) {
			if (flash == null) return;
			Boolean wasPlaying = flash.Playing;
			Stop();

			int current = flash.CurrentFrame();
			int total = flash.TotalFrames;
			int target = current + amount;
			flash.FrameNum = Math.Max(0, Math.Min(target, total));

			if (wasPlaying) Play();
		}

		#region IMediaOperations
		public void SkipBk() { Stop(); }
		public void SeekBk() { Seek(-seekAmount); }
		public void Play() {
			if (flash == null) return;
			flash.Play();
		}
		public void Pause() {
			if (flash == null) return; 
			flash.Stop();
		}
		public void Stop() {
			if (flash == null) return; 
			flash.Stop();
			flash.Rewind();
		}
		public void SeekFw() { Seek(seekAmount); }
		public void SkipFw() { Stop(); }

		public void SkipTo(double location) {
			if (flash == null) return;
			Seek(location - flash.CurrentFrame());
		}

		public double Duration() {
			if (flash == null) return 0;
			return flash.TotalFrames;
		}
		#endregion
	}

	public class MediaMovie : IMediaOperations {
		Video video = null;
		public double seekAmount = 0;

		public MediaMovie(Video video) {
			this.video = video;
		}

		protected void Seek(double amount) {
			if (video == null) return;
			Boolean wasPlaying = video.Playing;
			video.Pause();

			double current = video.CurrentPosition;
			double total = video.Duration;
			double target = current + amount;
			video.CurrentPosition = Math.Max(0, Math.Min(target, total));

			if (wasPlaying) Play();
		}

		#region IMediaOperations
		public void SkipBk() { Stop(); }
		public void SeekBk() { Seek(-seekAmount); }
		public void Play() {
			if (video == null) return;
			video.Play();
		}
		public void Pause() {
			if (video == null) return;
			video.Pause();
		}
		public void Stop() {
			if (video == null) return;
			video.Stop();
		}
		public void SeekFw() { Seek(seekAmount); }
		public void SkipFw() { Stop(); }

		public void SkipTo(double location) {
			if (video == null) return;
			Seek(location - video.CurrentPosition);
		}

		public double Duration() {
			if (video == null) return 0;
			return video.Duration;
		}
		#endregion
	}
}