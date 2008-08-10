/*

DreamBeam - a Church Song Presentation Program
Copyright (C) 2004 Stefan Kaufmann

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
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace DreamBeam
{


	/// <summary>
	/// Zusammenfassende Beschreibung für WinForm
	/// </summary>
	public class About : System.Windows.Forms.Form
	{
		/// <summary>
		/// Erforderliche Designervariable
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label DreamBeam;
		private System.Windows.Forms.Label LabelVersion;
		private System.Windows.Forms.Button OkButton;
		public string version = "";
		private System.Windows.Forms.Label Copyright;
		private System.Windows.Forms.LinkLabel email2;
		private System.Windows.Forms.LinkLabel webSite1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.RichTextBox LicenseTextBox;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.LinkLabel linkLabel3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.LinkLabel linkLabel4;
		private System.Windows.Forms.Label label5;
		private LinkLabel webSite2;
		private Panel webSites;
		private LinkLabel email1;
		private System.Windows.Forms.LinkLabel linkLabel5;


		public About()
		{
			//
			// Erforderlich für die Unterstützung des Windows Forms-Designer
			//

			InitializeComponent();

			Language l = new Language();
			this.LicenseTextBox.Text = l.say("GnuLicense");

			// Keep it broken up so that spam bots do not pick it up.
			email1.Text = "staeff" + "@" + "staeff.de";
			email2.Text = "gburca-dreambeam" + "@" + "ebixio.com";
		}

		/// <summary>
		/// Ressourcen nach der Verwendung bereinigen
		/// </summary>
		protected override void Dispose (bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Vom Windows Form-Designer erzeugter Code
		/// <summary>
		/// Erforderliche Methode zur Unterstützung des Designers -
		/// ändern Sie die Methode nicht mit dem Quelltext-Editor.
		/// </summary>
		private void InitializeComponent() {
			this.panel1 = new System.Windows.Forms.Panel();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.LicenseTextBox = new System.Windows.Forms.RichTextBox();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.linkLabel5 = new System.Windows.Forms.LinkLabel();
			this.label5 = new System.Windows.Forms.Label();
			this.linkLabel4 = new System.Windows.Forms.LinkLabel();
			this.label4 = new System.Windows.Forms.Label();
			this.linkLabel3 = new System.Windows.Forms.LinkLabel();
			this.label3 = new System.Windows.Forms.Label();
			this.webSite1 = new System.Windows.Forms.LinkLabel();
			this.email2 = new System.Windows.Forms.LinkLabel();
			this.Copyright = new System.Windows.Forms.Label();
			this.LabelVersion = new System.Windows.Forms.Label();
			this.OkButton = new System.Windows.Forms.Button();
			this.DreamBeam = new System.Windows.Forms.Label();
			this.webSite2 = new System.Windows.Forms.LinkLabel();
			this.webSites = new System.Windows.Forms.Panel();
			this.email1 = new System.Windows.Forms.LinkLabel();
			this.panel1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.webSites.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.White;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.webSites);
			this.panel1.Controls.Add(this.tabControl1);
			this.panel1.Controls.Add(this.Copyright);
			this.panel1.Controls.Add(this.LabelVersion);
			this.panel1.Controls.Add(this.OkButton);
			this.panel1.Controls.Add(this.DreamBeam);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(495, 370);
			this.panel1.TabIndex = 0;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Location = new System.Drawing.Point(3, 135);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(487, 200);
			this.tabControl1.TabIndex = 10;
			// 
			// tabPage1
			// 
			this.tabPage1.BackColor = System.Drawing.Color.White;
			this.tabPage1.Controls.Add(this.LicenseTextBox);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(479, 174);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "License";
			// 
			// LicenseTextBox
			// 
			this.LicenseTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LicenseTextBox.Location = new System.Drawing.Point(0, 0);
			this.LicenseTextBox.Name = "LicenseTextBox";
			this.LicenseTextBox.Size = new System.Drawing.Size(479, 174);
			this.LicenseTextBox.TabIndex = 12;
			this.LicenseTextBox.Text = "GNU License text";
			this.LicenseTextBox.WordWrap = false;
			// 
			// tabPage2
			// 
			this.tabPage2.BackColor = System.Drawing.Color.White;
			this.tabPage2.Controls.Add(this.linkLabel5);
			this.tabPage2.Controls.Add(this.label5);
			this.tabPage2.Controls.Add(this.linkLabel4);
			this.tabPage2.Controls.Add(this.label4);
			this.tabPage2.Controls.Add(this.linkLabel3);
			this.tabPage2.Controls.Add(this.label3);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(479, 174);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Thanks for 3rd Party Controls and Scripts";
			// 
			// linkLabel5
			// 
			this.linkLabel5.AutoSize = true;
			this.linkLabel5.LinkColor = System.Drawing.Color.Black;
			this.linkLabel5.Location = new System.Drawing.Point(256, 152);
			this.linkLabel5.Name = "linkLabel5";
			this.linkLabel5.Size = new System.Drawing.Size(144, 13);
			this.linkLabel5.TabIndex = 14;
			this.linkLabel5.TabStop = true;
			this.linkLabel5.Text = "http://www.codeproject.com";
			this.linkLabel5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.linkLabel5.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.www_LinkClicked);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(16, 128);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(368, 16);
			this.label5.TabIndex = 13;
			this.label5.Text = "And thanks to the Code Project for all their Tutorials and Code-Snippets.";
			// 
			// linkLabel4
			// 
			this.linkLabel4.AutoSize = true;
			this.linkLabel4.LinkColor = System.Drawing.Color.Black;
			this.linkLabel4.Location = new System.Drawing.Point(256, 96);
			this.linkLabel4.Name = "linkLabel4";
			this.linkLabel4.Size = new System.Drawing.Size(160, 13);
			this.linkLabel4.TabIndex = 12;
			this.linkLabel4.TabStop = true;
			this.linkLabel4.Text = "http://www.crosswire.org/sword";
			this.linkLabel4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.linkLabel4.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.www_LinkClicked);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(16, 72);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(392, 16);
			this.label4.TabIndex = 11;
			this.label4.Text = "Thanks to the Sword Project, for their Bible Tools.";
			// 
			// linkLabel3
			// 
			this.linkLabel3.AutoSize = true;
			this.linkLabel3.LinkColor = System.Drawing.Color.Black;
			this.linkLabel3.Location = new System.Drawing.Point(256, 48);
			this.linkLabel3.Name = "linkLabel3";
			this.linkLabel3.Size = new System.Drawing.Size(113, 13);
			this.linkLabel3.TabIndex = 10;
			this.linkLabel3.TabStop = true;
			this.linkLabel3.Text = "http://www.divil.co.uk";
			this.linkLabel3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.www_LinkClicked);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(16, 16);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(392, 32);
			this.label3.TabIndex = 0;
			this.label3.Text = "Most of those eye candy things (menus, Toolbars, Docking Panels, Document Manager" +
				") are from Tim Dawson. Great!";
			// 
			// webSite1
			// 
			this.webSite1.AutoSize = true;
			this.webSite1.LinkColor = System.Drawing.Color.Blue;
			this.webSite1.Location = new System.Drawing.Point(3, 3);
			this.webSite1.Name = "webSite1";
			this.webSite1.Size = new System.Drawing.Size(135, 13);
			this.webSite1.TabIndex = 9;
			this.webSite1.TabStop = true;
			this.webSite1.Text = "http://www.dreambeam.de";
			this.webSite1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.www_LinkClicked);
			// 
			// email2
			// 
			this.email2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.email2.LinkColor = System.Drawing.Color.Blue;
			this.email2.Location = new System.Drawing.Point(233, 19);
			this.email2.Name = "email2";
			this.email2.Size = new System.Drawing.Size(246, 16);
			this.email2.TabIndex = 6;
			this.email2.TabStop = true;
			this.email2.Text = "email2@text";
			this.email2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.email2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.email_LinkClicked);
			// 
			// Copyright
			// 
			this.Copyright.Location = new System.Drawing.Point(140, 43);
			this.Copyright.Name = "Copyright";
			this.Copyright.Size = new System.Drawing.Size(213, 41);
			this.Copyright.TabIndex = 4;
			this.Copyright.Text = "Free Software\r\n(c)2004 by Stefan Kaufmann\r\nPortions (c)2006-2008 Gabriel Burca";
			this.Copyright.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// LabelVersion
			// 
			this.LabelVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.LabelVersion.Location = new System.Drawing.Point(129, 30);
			this.LabelVersion.Name = "LabelVersion";
			this.LabelVersion.Size = new System.Drawing.Size(234, 16);
			this.LabelVersion.TabIndex = 2;
			this.LabelVersion.Text = "Version";
			this.LabelVersion.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// OkButton
			// 
			this.OkButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.OkButton.Location = new System.Drawing.Point(206, 341);
			this.OkButton.Name = "OkButton";
			this.OkButton.Size = new System.Drawing.Size(80, 23);
			this.OkButton.TabIndex = 3;
			this.OkButton.Text = "OK";
			this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
			// 
			// DreamBeam
			// 
			this.DreamBeam.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.DreamBeam.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.DreamBeam.Location = new System.Drawing.Point(103, 0);
			this.DreamBeam.Name = "DreamBeam";
			this.DreamBeam.Size = new System.Drawing.Size(287, 30);
			this.DreamBeam.TabIndex = 1;
			this.DreamBeam.Text = "DreamBeam";
			this.DreamBeam.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// webSite2
			// 
			this.webSite2.AutoSize = true;
			this.webSite2.LinkColor = System.Drawing.Color.Blue;
			this.webSite2.Location = new System.Drawing.Point(3, 21);
			this.webSite2.Name = "webSite2";
			this.webSite2.Size = new System.Drawing.Size(214, 13);
			this.webSite2.TabIndex = 11;
			this.webSite2.TabStop = true;
			this.webSite2.Text = "http://sourceforge.net/projects/dreambeam";
			this.webSite2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.www_LinkClicked);
			// 
			// webSites
			// 
			this.webSites.Controls.Add(this.email1);
			this.webSites.Controls.Add(this.webSite1);
			this.webSites.Controls.Add(this.webSite2);
			this.webSites.Controls.Add(this.email2);
			this.webSites.Location = new System.Drawing.Point(3, 87);
			this.webSites.Name = "webSites";
			this.webSites.Size = new System.Drawing.Size(487, 42);
			this.webSites.TabIndex = 12;
			// 
			// email1
			// 
			this.email1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.email1.Location = new System.Drawing.Point(233, 3);
			this.email1.Name = "email1";
			this.email1.Size = new System.Drawing.Size(246, 16);
			this.email1.TabIndex = 0;
			this.email1.TabStop = true;
			this.email1.Text = "email1@text";
			this.email1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.email1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.email_LinkClicked);
			// 
			// About
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(495, 370);
			this.ControlBox = false;
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "About";
			this.Opacity = 0.89999997615814209;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "About DreamBeam";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.WinForm_Load);
			this.panel1.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			this.webSites.ResumeLayout(false);
			this.webSites.PerformLayout();
			this.ResumeLayout(false);

		}
		#endregion

		private void WinForm_Load(object sender, System.EventArgs e)
		{
			//this.LabelVersion.Text = "Version: " + Application.ProductVersion.Substring(0, Application.ProductVersion.Length - Tools.Reverse(Application.ProductVersion).IndexOf(".")-1);
			this.LabelVersion.Text = "Version: " + Application.ProductVersion;
		}
		
		private void OkButton_Click(object sender, System.EventArgs e)
		{
			this.Hide();
		}
		
		private void email_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			LinkLabel label = sender as LinkLabel;
			if (label != null)
				System.Diagnostics.Process.Start("mailto:" + label.Text);
		}

		private void www_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e) {
			LinkLabel label = sender as LinkLabel;
			if (label != null)
				System.Diagnostics.Process.Start(label.Text);
		}

	}
}
