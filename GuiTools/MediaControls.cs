using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DreamBeam {

	public partial class MediaControls : UserControl {

		[
		Browsable(true),
		Category("Action"),
		Description("Occurs when one of the media buttons is clicked")
		]
		public event MediaControlsChanged MediaButtonPressed;

		[
		Bindable(true),
		Category("Appearance"),
		Description("Controls the color of the label text")
		]
		public Color LabelColor {
			get { return this.label.ForeColor; }
			set { this.label.ForeColor = value; }
		}

		[
		Bindable(true),
		Category("Appearance"),
		Description("Label text")
		]
		public String LabelText {
			get { return this.label.Text; }
			set { this.label.Text = value; }
		}

		public MediaControls() {
			InitializeComponent();
		}

		private void notify(MediaButton btn) {
			if (MediaButtonPressed != null)
				MediaButtonPressed(this, new MediaControlsEvent(btn));
		}

		private void skipBkButton_Click(object sender, EventArgs e) {
			notify(MediaButton.SkipBk);
		}
		private void seekBkButton_Click(object sender, EventArgs e) {
			notify(MediaButton.SeekBk);
		}
		private void playButton_Click(object sender, EventArgs e) {
			notify(MediaButton.Play);
		}
		private void pauseButton_Click(object sender, EventArgs e) {
			notify(MediaButton.Pause);
		}
		private void stopButton_Click(object sender, EventArgs e) {
			notify(MediaButton.Stop);
		}
		private void seekFwButton_Click(object sender, EventArgs e) {
			notify(MediaButton.SeekFw);
		}
		private void skipFwButton_Click(object sender, EventArgs e) {
			notify(MediaButton.SkipFw);
		}
	}

	public delegate void MediaControlsChanged(object sender, MediaControlsEvent e);

	public enum MediaButton {
		SkipBk,
		SeekBk,
		Play,
		Pause,
		Stop,
		SeekFw,
		SkipFw
	}

	public class MediaControlsEvent : EventArgs {

		public MediaButton button;

		public MediaControlsEvent(MediaButton btn)
			: base() {
			button = btn;
		}
	}
}
