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

		// Maybe these should go in a IMediaProperties interface
		double Duration();
		int Volume {
			get;
			set;
		}
		Boolean Playing {
			get;
		}
		double CurrentLocation {
			get;
			set;
		}
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
			Pause();

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

		public double Duration() {
			if (flash == null) return 0;
			return flash.TotalFrames;
		}

		public int Volume {
			get { return 0; }
			set { }
		}

		public Boolean Playing {
			get {
				if (flash == null) return false;
				return flash.Playing;
			}
		}

		public double CurrentLocation {
			get {
				if (flash == null) return 0;
				return flash.CurrentFrame();
			}
			set {
				if (flash == null) return;
				Seek((int)(value - flash.CurrentFrame()));
			}
		}
		#endregion
	}

	public class MediaMovie : IMediaOperations {
		Video video = null;
		public double seekAmount = 1;

		public MediaMovie(Video video) {
			this.video = video;
		}

		protected void Seek(double amount) {
			if (video == null) return;
			Boolean wasPlaying = video.Playing;
			Pause();

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

		public int Volume {
			get {
				if (video == null) return 0;
				return video.Audio.Volume;
			}
			set {
				if (video == null) return;
				try {
					video.Audio.Volume = value;
				} catch { }
			}
		}

		public Boolean Playing {
			get {
				if (video == null) return false;
				return video.Playing;
			}
		}

		public double CurrentLocation {
			get {
				if (video == null) return 0;
				return video.CurrentPosition;
			}
			set {
				if (video == null) return;
				video.CurrentPosition = value;
			}
		}
		#endregion
	}
}