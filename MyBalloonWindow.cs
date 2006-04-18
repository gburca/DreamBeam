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
using System.IO;
using System.Resources;
using System.Collections;
using System.Reflection;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Rilling.Common.UI.Forms;

namespace DreamBeam {
    public class MyBalloonWindow : BalloonWindow {
        private System.Windows.Forms.ComboBox cboTrigShape;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer tmrMoveTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnlTrigShape;
        private System.Windows.Forms.Label HelpText;
        private System.ComponentModel.IContainer components;
        public System.Windows.Forms.Button button1;
        public System.Windows.Forms.Button button2;
        public System.Windows.Forms.Button button3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label Header;
		private int ButtonClicked = 0;
		public Config Config = new Config();
		private Language Lang = new Language();

        public MyBalloonWindow() {
            // This call is required by the Windows Form Designer.
            InitializeComponent();

        }

        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
        }

        protected override void Dispose( bool disposing ) {
            /*   if( disposing )
               {
                if (components != null)
                {
                 components.Dispose();
                }
               }
               base.Dispose( disposing );*/
        }

#region Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MyBalloonWindow));
			this.cboTrigShape = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.tmrMoveTime = new System.Windows.Forms.Timer(this.components);
			this.label2 = new System.Windows.Forms.Label();
			this.pnlTrigShape = new System.Windows.Forms.Panel();
			this.Header = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.HelpText = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.pnlTrigShape.SuspendLayout();
			this.SuspendLayout();
			// 
			// cboTrigShape
			// 
			this.cboTrigShape.Location = new System.Drawing.Point(0, 0);
			this.cboTrigShape.Name = "cboTrigShape";
			this.cboTrigShape.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.TabIndex = 0;
			// 
			// label2
			// 
			this.label2.BackColor = System.Drawing.Color.Transparent;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(32, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(248, 16);
			this.label2.TabIndex = 10;
			this.label2.Text = "Sample Balloon Containing Controls";
			// 
			// pnlTrigShape
			// 
			this.pnlTrigShape.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlTrigShape.Controls.Add(this.Header);
			this.pnlTrigShape.Controls.Add(this.pictureBox1);
			this.pnlTrigShape.Controls.Add(this.HelpText);
			this.pnlTrigShape.Location = new System.Drawing.Point(8, 24);
			this.pnlTrigShape.Name = "pnlTrigShape";
			this.pnlTrigShape.Size = new System.Drawing.Size(288, 136);
			this.pnlTrigShape.TabIndex = 20;
			// 
			// Header
			// 
			this.Header.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Header.Location = new System.Drawing.Point(48, 12);
			this.Header.Name = "Header";
			this.Header.Size = new System.Drawing.Size(224, 23);
			this.Header.TabIndex = 23;
			this.Header.Text = "label3";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(8, 8);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(32, 32);
			this.pictureBox1.TabIndex = 22;
			this.pictureBox1.TabStop = false;
			// 
			// HelpText
			// 
			this.HelpText.BackColor = System.Drawing.Color.Transparent;
			this.HelpText.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.HelpText.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.HelpText.Location = new System.Drawing.Point(0, 46);
			this.HelpText.Name = "HelpText";
			this.HelpText.Size = new System.Drawing.Size(286, 88);
			this.HelpText.TabIndex = 21;
			this.HelpText.Text = "label5";
			this.HelpText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// button1
			// 
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.button1.Location = new System.Drawing.Point(240, 168);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(56, 16);
			this.button1.TabIndex = 21;
			this.button1.Text = "Next";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.button2.Location = new System.Drawing.Point(112, 168);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(56, 16);
			this.button2.TabIndex = 22;
			this.button2.Text = "Cancel";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button3
			// 
			this.button3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.button3.Location = new System.Drawing.Point(176, 168);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(56, 16);
			this.button3.TabIndex = 23;
			this.button3.Text = "Back";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// MyBalloonWindow
			// 
			this.AnchorPoint = new System.Drawing.Point(312, 30);
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(300, 200);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.pnlTrigShape);
			this.Location = new System.Drawing.Point(0, 0);
			this.Name = "MyBalloonWindow";
			this.Shadow = false;
			this.Load += new System.EventHandler(this.MyBalloonWindow_Load);
			this.pnlTrigShape.ResumeLayout(false);
			this.ResumeLayout(false);
		}
#endregion

        public int ShowHelp(Point pt,string strHeader, string strHelpText) {
            this.Header.Text = strHeader;
            this.HelpText.Text = strHelpText;
            // Move the balloon to anchor position.
            MoveAnchorTo(pt);
            this.ButtonClicked = 0;
            this.ShowDialog();
            return this.ButtonClicked;


        }

        public int ShowHelp(Control control,Point pt,string strHeader, string strHelpText) {
            if(control == null)
                throw(new ArgumentNullException("control"));

            this.Header.Text = strHeader;
            this.HelpText.Text = strHelpText;
            this.Shadow = true;
            // Move the balloon to anchor position.
            MoveAnchorTo(control);
            this.Location = new Point (this.Location.X+pt.X,this.Location.Y+pt.Y);

            this.ButtonClicked = 0;
            this.ShowDialog();
            return this.ButtonClicked;
        }




        protected override void OnVisibleChanged(EventArgs e) {
            base.OnVisibleChanged(e);
        }

        private void button2_Click(object sender, System.EventArgs e) {
            this.ButtonClicked = 0;
            this.Close();
        }

        private void button3_Click(object sender, System.EventArgs e) {
            this.ButtonClicked = 1;
            this.Close();
        }

        private void button1_Click(object sender, System.EventArgs e) {
            this.ButtonClicked = 2;
            this.Close();
        }
		
		private void MyBalloonWindow_Load(object sender, System.EventArgs e)
		{
			Lang.setCulture(Config.Language);
			#region Language
				button2.Text = Lang.say("Global.Buttons.Cancel");
				button3.Text = Lang.say("Global.Buttons.Back");
				button1.Text = Lang.say("Global.Buttons.Next");
			#endregion
		}


    }
}



