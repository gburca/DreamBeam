using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;
using System.Data;
using System.Diagnostics;
using Microsoft.Win32;

namespace DreamBeam {
	/// <summary>
	/// Zusammenfassende Beschreibung f�r WinForm
	/// </summary>
	public class Splash : System.Windows.Forms.Form {
		/// <summary>
		/// Erforderliche Designervariable
		/// </summary>
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label1;
		public string version = "";
		public string InfoText = "Copyright 2004 Stefan Kaufmann\nPortions Copyright 2006-2008 Gabriel Burca\nThis Software is distributed under the terms of the GNU General Public License.\n";


		// Threading
		static Splash ms_frmSplash = null;
		static Thread ms_oThread = null;

		// Fade in and out.
		private double m_dblOpacityIncrement = .05;
		private double m_dblOpacityDecrement = .08;
		private const int TIMER_INTERVAL = 50;

		// Status and progress bar
		static string ms_sStatus;
		private double m_dblCompletionFraction = 0;
		//private Rectangle m_rProgress;


		// Progress smoothing
		private double m_dblLastCompletionFraction = 0.0;
		private double m_dblPBIncrementPerTimerInterval = .015;

		// Self-calibration support
		private bool m_bFirstLaunch = false;
		private DateTime m_dtStart;
		private bool m_bDTSet = false;
		private int m_iIndex = 1;
		private int m_iActualTicks = 0;
		private ArrayList m_alPreviousCompletionFraction;
		private ArrayList m_alActualTimes = new ArrayList();
		private const string REG_KEY_INITIALIZATION = "Initialization";
		private const string REGVALUE_PB_MILISECOND_INCREMENT = "Increment";
		private const string REGVALUE_PB_PERCENTS = "Percents";
		private System.Windows.Forms.Timer timer1;




		public Splash() {
			//
			// Erforderlich f�r die Unterst�tzung des Windows Forms-Designer
			//
			InitializeComponent();


			//this.label1.Text  = "Dreambeam "+ this.version +"\n";
			//this.label1.Text += " hallo";

			this.version = Application.ProductVersion.Substring(0, Application.ProductVersion.Length - DreamTools.Reverse(Application.ProductVersion).IndexOf(".") - 1);
			//
			this.Opacity = .00;
			timer1.Interval = TIMER_INTERVAL;
			timer1.Start();

			//
			// TODO: F�gen Sie nach dem Aufruf von InitializeComponent() Konstruktorcode hinzu.
			//
		}

		/// <summary>
		/// Ressourcen nach der Verwendung bereinigen
		/// </summary>
		protected override void Dispose(bool disposing) {
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Vom Windows Form-Designer erzeugter Code
		/// <summary>
		/// Erforderliche Methode zur Unterst�tzung des Designers -
		/// �ndern Sie die Methode nicht mit dem Quelltext-Editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Splash));
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(600, 371);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.White;
			this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.label1.Location = new System.Drawing.Point(0, 368);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(600, 78);
			this.label1.TabIndex = 2;
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// timer1
			// 
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// Splash
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(600, 446);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.pictureBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "Splash";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "WinForm";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		// A static method to create the thread and
		// launch the SplashScreen.
		static public void ShowSplashScreen() {

			// Make sure it's only launched once.
			if (ms_frmSplash != null)
				return;
			ms_oThread = new Thread(new ThreadStart(Splash.ShowForm));
			ms_oThread.IsBackground = true;
			ms_oThread.Name = "ShowSplashScreen";
			ms_oThread.SetApartmentState(ApartmentState.STA);
			ms_oThread.Start();
		}

		// A property returning the splash screen instance
		static public Splash SplashForm {
			get {
				return ms_frmSplash;
			}
		}

		// A private entry point for the thread.
		static private void ShowForm() {
			ms_frmSplash = new Splash();
			Application.Run(ms_frmSplash);
		}

		// A static method to close the SplashScreen
		static public void CloseForm() {
			if (ms_frmSplash != null && ms_frmSplash.IsDisposed == false) {
				// Make it start going away.
				ms_frmSplash.m_dblOpacityIncrement = -ms_frmSplash.m_dblOpacityDecrement;
			}
			ms_oThread = null; // we don't need these any more.
			ms_frmSplash = null;
		}

		// A static method to set the status and update the reference.
		static public void SetStatus(string newStatus) {
			SetStatus(newStatus, true);
		}


		// A static method to set the status and optionally update the reference.
		// This is useful if you are in a section of code that has a variable
		// set of status string updates.  In that case, don't set the reference.
		static public void SetStatus(string newStatus, bool setReference) {
			ms_sStatus = newStatus;
			if (ms_frmSplash == null)
				return;
			if (setReference)
				ms_frmSplash.SetReferenceInternal();
		}

		// Static method called from the initializing application to
		// give the splash screen reference points.  Not needed if
		// you are using a lot of status strings.
		static public void SetReferencePoint() {
			if (ms_frmSplash == null)
				return;
			ms_frmSplash.SetReferenceInternal();

		}

		// ************ Private methods ************

		// Internal method for setting reference points.
		private void SetReferenceInternal() {
			if (m_bDTSet == false) {
				m_bDTSet = true;
				m_dtStart = DateTime.Now;
				ReadIncrements();
			}
			double dblMilliseconds = ElapsedMilliSeconds();
			m_alActualTimes.Add(dblMilliseconds);
			m_dblLastCompletionFraction = m_dblCompletionFraction;
			if (m_alPreviousCompletionFraction != null && m_iIndex < m_alPreviousCompletionFraction.Count)
				m_dblCompletionFraction = (double)m_alPreviousCompletionFraction[m_iIndex++];
			else
				m_dblCompletionFraction = (m_iIndex > 0) ? 1 : 0;
		}

		// Utility function to return elapsed Milliseconds since the
		// SplashScreen was launched.
		private double ElapsedMilliSeconds() {
			TimeSpan ts = DateTime.Now - m_dtStart;
			return ts.TotalMilliseconds;
		}

		// Function to read the checkpoint intervals from the previous invocation of the
		// splashscreen from the registry.
		private void ReadIncrements() {
			string sPBIncrementPerTimerInterval = RegistryAccess.GetStringRegistryValue(REGVALUE_PB_MILISECOND_INCREMENT, "0.0015");
			double dblResult;

			if (Double.TryParse(sPBIncrementPerTimerInterval, System.Globalization.NumberStyles.Float, System.Globalization.NumberFormatInfo.InvariantInfo, out dblResult) == true)
				m_dblPBIncrementPerTimerInterval = dblResult;
			else
				m_dblPBIncrementPerTimerInterval = .0015;

			string sPBPreviousPctComplete = RegistryAccess.GetStringRegistryValue(REGVALUE_PB_PERCENTS, "");

			if (sPBPreviousPctComplete != "") {
				string[] aTimes = sPBPreviousPctComplete.Split(null);
				m_alPreviousCompletionFraction = new ArrayList();

				for (int i = 0; i < aTimes.Length; i++) {
					double dblVal;
					if (Double.TryParse(aTimes[i], System.Globalization.NumberStyles.Float, System.Globalization.NumberFormatInfo.InvariantInfo, out dblVal))
						m_alPreviousCompletionFraction.Add(dblVal);
					else
						m_alPreviousCompletionFraction.Add(1.0);
				}
			} else {
				m_bFirstLaunch = true;
				//lblTimeRemaining.Text = "";
			}
		}

		// Method to store the intervals (in percent complete) from the current invocation of
		// the splash screen to the registry.
		private void StoreIncrements() {
			string sPercent = "";
			double dblElapsedMilliseconds = ElapsedMilliSeconds();
			for (int i = 0; i < m_alActualTimes.Count; i++)
				sPercent += ((double)m_alActualTimes[i] / dblElapsedMilliseconds).ToString("0.####", System.Globalization.NumberFormatInfo.InvariantInfo) + " ";

			RegistryAccess.SetStringRegistryValue(REGVALUE_PB_PERCENTS, sPercent);

			m_dblPBIncrementPerTimerInterval = 1.0 / (double)m_iActualTicks;
			RegistryAccess.SetStringRegistryValue(REGVALUE_PB_MILISECOND_INCREMENT, m_dblPBIncrementPerTimerInterval.ToString("#.000000", System.Globalization.NumberFormatInfo.InvariantInfo));
		}

		//********* Event Handlers ************

		// Tick Event handler for the Timer control.  Handle fade in and fade out.  Also
		// handle the smoothed progress bar.
		private void timer1_Tick(object sender, System.EventArgs e) {
			string tempText = "DreamBeam " + this.version + "\n" + InfoText;
			label1.Text = tempText + "\n" + ms_sStatus;

			if (m_dblOpacityIncrement > 0) {
				m_iActualTicks++;
				if (this.Opacity < 1)
					this.Opacity += m_dblOpacityIncrement;
			} else {
				if (this.Opacity > 0)
					this.Opacity += m_dblOpacityIncrement;
				else {
					StoreIncrements();
					this.Close();
					Debug.WriteLine("Called this.Close()");
				}
			}
			if (m_bFirstLaunch == false && m_dblLastCompletionFraction < m_dblCompletionFraction) {
				m_dblLastCompletionFraction += m_dblPBIncrementPerTimerInterval;
				//    int width = (int)Math.Floor(pnlStatus.ClientRectangle.Width * m_dblLastCompletionFraction);
				//    int height = pnlStatus.ClientRectangle.Height;
				//    int x = pnlStatus.ClientRectangle.X;
				/*    int y = pnlStatus.ClientRectangle.Y;
					if( width > 0 && height > 0 )
					{
					 m_rProgress = new Rectangle( x, y, width, height);
					 pnlStatus.Invalidate(m_rProgress);
					 int iSecondsLeft = 1 + (int)(TIMER_INTERVAL * ((1.0 - m_dblLastCompletionFraction)/m_dblPBIncrementPerTimerInterval)) / 1000;
					 if( iSecondsLeft == 1 )
					  lblTimeRemaining.Text = string.Format( "1 second remaining");
					 else
					  lblTimeRemaining.Text = string.Format( "{0} seconds remaining", iSecondsLeft);
					}*/
			}
		}

		// Paint the portion of the panel invalidated during the tick event.
		private void pnlStatus_Paint(object sender, System.Windows.Forms.PaintEventArgs e) {
			if (m_bFirstLaunch == false && e.ClipRectangle.Width > 0 && m_iActualTicks > 1) {
				//    LinearGradientBrush brBackground = new LinearGradientBrush(m_rProgress, Color.FromArgb(58, 96, 151), Color.FromArgb(181, 237, 254), LinearGradientMode.Horizontal);
				//    e.Graphics.FillRectangle(brBackground, m_rProgress);
			}
		}

		// Close the form if they double click on it.
		private void SplashScreen_DoubleClick(object sender, System.EventArgs e) {
			CloseForm();
		}
	}

	/// <summary>
	/// A class for managing registry access.
	/// </summary>
	public class RegistryAccess {
		private const string SOFTWARE_KEY = "Software";
		private const string COMPANY_NAME = "GNU";
		private const string APPLICATION_NAME = "DreamBeam";

		// Method for retrieving a Registry Value.
		static public string GetStringRegistryValue(string key, string defaultValue) {
			RegistryKey rkCompany;
			RegistryKey rkApplication;

			rkCompany = Registry.CurrentUser.OpenSubKey(SOFTWARE_KEY, false).OpenSubKey(COMPANY_NAME, false);
			if (rkCompany != null) {
				rkApplication = rkCompany.OpenSubKey(APPLICATION_NAME, true);
				if (rkApplication != null) {
					foreach (string sKey in rkApplication.GetValueNames()) {
						if (sKey == key) {
							return (string)rkApplication.GetValue(sKey);
						}
					}
				}
			}
			return defaultValue;
		}

		// Method for storing a Registry Value.
		static public void SetStringRegistryValue(string key, string stringValue) {
			RegistryKey rkSoftware;
			RegistryKey rkCompany;
			RegistryKey rkApplication;

			rkSoftware = Registry.CurrentUser.OpenSubKey(SOFTWARE_KEY, true);
			rkCompany = rkSoftware.CreateSubKey(COMPANY_NAME);
			if (rkCompany != null) {
				rkApplication = rkCompany.CreateSubKey(APPLICATION_NAME);
				if (rkApplication != null) {
					rkApplication.SetValue(key, stringValue);
				}
			}
		}
	}
}

