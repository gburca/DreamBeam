using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Microsoft.Win32;

namespace SysCheck
{
	/// <summary>
	/// Zusammenfassende Beschreibung für WinForm
	/// </summary>
	public class WinForm : System.Windows.Forms.Form
	{
		/// <summary>
		/// Erforderliche Designervariable
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.GroupBox DirectXBox;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.Button Done_Button;
		private System.Windows.Forms.Button Cancel_Button;
		public static int ReturnCode = 1;
		private System.Windows.Forms.GroupBox MDXBox;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textBox4;
		private System.Windows.Forms.Label label4;
		public WinForm()
		{
			//
			// Erforderlich für die Unterstützung des Windows Forms-Designer
			//
			InitializeComponent();






			//
			// TODO: Fügen Sie nach dem Aufruf von InitializeComponent() Konstruktorcode hinzu.
			//
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
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(WinForm));
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.DirectXBox = new System.Windows.Forms.GroupBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.button2 = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.Done_Button = new System.Windows.Forms.Button();
			this.Cancel_Button = new System.Windows.Forms.Button();
			this.MDXBox = new System.Windows.Forms.GroupBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.button4 = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.DirectXBox.SuspendLayout();
			this.MDXBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(496, 65);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// DirectXBox
			// 
			this.DirectXBox.Controls.Add(this.textBox2);
			this.DirectXBox.Controls.Add(this.label3);
			this.DirectXBox.Controls.Add(this.button2);
			this.DirectXBox.Controls.Add(this.label2);
			this.DirectXBox.Controls.Add(this.button1);
			this.DirectXBox.Controls.Add(this.label1);
			this.DirectXBox.Controls.Add(this.textBox1);
			this.DirectXBox.Location = new System.Drawing.Point(8, 72);
			this.DirectXBox.Name = "DirectXBox";
			this.DirectXBox.Size = new System.Drawing.Size(480, 176);
			this.DirectXBox.TabIndex = 1;
			this.DirectXBox.TabStop = false;
			this.DirectXBox.Text = "DirectX 9b or better not found";
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(88, 144);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(384, 20);
			this.textBox2.TabIndex = 6;
			this.textBox2.Text = "http://www.microsoft.com/downloads/details.aspx?FamilyID=a6dee0db-" +  
				"dcce-43ea-87bb-7c7e1fd1eaa2&displaylang=en";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(24, 152);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(40, 16);
			this.label3.TabIndex = 5;
			this.label3.Text = "URL:";
			// 
			// button2
			// 
			this.button2.BackColor = System.Drawing.Color.Coral;
			this.button2.Location = new System.Drawing.Point(16, 112);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(224, 23);
			this.button2.TabIndex = 4;
			this.button2.Text = "Download Full Setup for other Computers";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(24, 80);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(40, 16);
			this.label2.TabIndex = 3;
			this.label2.Text = "URL:";
			// 
			// button1
			// 
			this.button1.BackColor = System.Drawing.Color.Coral;
			this.button1.Location = new System.Drawing.Point(16, 48);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(136, 23);
			this.button1.TabIndex = 2;
			this.button1.Text = "Download WebInstaller";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.Firebrick;
			this.label1.Location = new System.Drawing.Point(16, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(336, 23);
			this.label1.TabIndex = 1;
			this.label1.Text = "You need at least DirectX 9b to run DreamBeam.";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(88, 80);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(384, 20);
			this.textBox1.TabIndex = 0;
			this.textBox1.Text = "http://www.microsoft.com/downloads/details.aspx?FamilyID=141d5f9e-" +  
				"07c1-462a-baef-5eab5c851cf5&displaylang=en";
			// 
			// Done_Button
			// 
			this.Done_Button.BackColor = System.Drawing.Color.GreenYellow;
			this.Done_Button.ForeColor = System.Drawing.Color.Black;
			this.Done_Button.Location = new System.Drawing.Point(384, 416);
			this.Done_Button.Name = "Done_Button";
			this.Done_Button.Size = new System.Drawing.Size(104, 23);
			this.Done_Button.TabIndex = 2;
			this.Done_Button.Text = "Done";
			this.Done_Button.Click += new System.EventHandler(this.Done_Button_Click);
			// 
			// Cancel_Button
			// 
			this.Cancel_Button.BackColor = System.Drawing.Color.OrangeRed;
			this.Cancel_Button.Location = new System.Drawing.Point(264, 416);
			this.Cancel_Button.Name = "Cancel_Button";
			this.Cancel_Button.Size = new System.Drawing.Size(112, 23);
			this.Cancel_Button.TabIndex = 3;
			this.Cancel_Button.Text = "Cancel Installation";
			this.Cancel_Button.Click += new System.EventHandler(this.Cancel_Button_Click);
			// 
			// MDXBox
			// 
			this.MDXBox.Controls.Add(this.label4);
			this.MDXBox.Controls.Add(this.label5);
			this.MDXBox.Controls.Add(this.button4);
			this.MDXBox.Controls.Add(this.label6);
			this.MDXBox.Controls.Add(this.textBox4);
			this.MDXBox.Location = new System.Drawing.Point(8, 248);
			this.MDXBox.Name = "MDXBox";
			this.MDXBox.Size = new System.Drawing.Size(480, 160);
			this.MDXBox.TabIndex = 4;
			this.MDXBox.TabStop = false;
			this.MDXBox.Text = "Managed DirectX not found";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(16, 120);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(448, 32);
			this.label4.TabIndex = 4;
			this.label4.Text = "If you downloaded the DirectX Full Setup, install the file mdxredi" +  
				"st.msi from your installation folder.";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(24, 80);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(40, 16);
			this.label5.TabIndex = 3;
			this.label5.Text = "URL:";
			// 
			// button4
			// 
			this.button4.BackColor = System.Drawing.Color.Coral;
			this.button4.Location = new System.Drawing.Point(16, 48);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(136, 23);
			this.button4.TabIndex = 2;
			this.button4.Text = "Download";
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// label6
			// 
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.ForeColor = System.Drawing.Color.Firebrick;
			this.label6.Location = new System.Drawing.Point(16, 24);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(336, 23);
			this.label6.TabIndex = 1;
			this.label6.Text = "Dreambeam requires Managed DirectX ";
			// 
			// textBox4
			// 
			this.textBox4.Location = new System.Drawing.Point(88, 80);
			this.textBox4.Name = "textBox4";
			this.textBox4.Size = new System.Drawing.Size(384, 20);
			this.textBox4.TabIndex = 0;
			this.textBox4.Text = "http://dreambeam.sf.net/mdxredist.msi";
			// 
			// WinForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(496, 456);
			this.Controls.Add(this.MDXBox);
			this.Controls.Add(this.Cancel_Button);
			this.Controls.Add(this.Done_Button);
			this.Controls.Add(this.DirectXBox);
			this.Controls.Add(this.pictureBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "WinForm";
			this.Text = "DreamBeam Requirements";
			this.Load += new System.EventHandler(this.WinForm_Load);
			this.DirectXBox.ResumeLayout(false);
			this.MDXBox.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		#endregion

		/// <summary>
		/// Der Haupteinsprungspunkt für die Anwendung
		/// </summary>
		[STAThread]
		static int Main()
		{
			if(!CheckAll()){
				Application.Run(new WinForm());
			}

		  return ReturnCode;
		}
	#region registry
		static bool DirectXInstalled(){

			RegistryKey regKey = Registry.LocalMachine.OpenSubKey(
			@"SOFTWARE\Microsoft\DirectX");

			int Version = 0;

			if (regKey != null){
				object regValue = regKey.GetValue("Version");
				if (regValue != null){
					Version = Convert.ToInt32(regValue.ToString().Replace(".",""));
					if (Version >= 409000902){
						return true;
					}
				}

			}else{
			}
			return false;
		}

		static bool MDXInstalled(){

			RegistryKey regKey = Registry.LocalMachine.OpenSubKey(
			@"SOFTWARE\Microsoft\DirectX");

			int Version = 0;

			if (regKey != null){
				object regValue = regKey.GetValue("ManagedDirectXVersion");
				if (regValue != null){
					Version = Convert.ToInt32(regValue.ToString().Replace(".",""));
					if (Version >= 409000901){
						return true;
					}
				}

			}else{
			}
			return false;
		}
	#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{
					System.Diagnostics.Process.Start("http://www.microsoft.com/downloads/details.aspx?FamilyID=141d5f9e-07c1-462a-baef-5eab5c851cf5&displaylang=en");
		}
		
		private void button2_Click(object sender, System.EventArgs e)
		{
					System.Diagnostics.Process.Start("http://www.microsoft.com/downloads/details.aspx?FamilyID=a6dee0db-dcce-43ea-87bb-7c7e1fd1eaa2&displaylang=en");

		}
		
		private void Done_Button_Click(object sender, System.EventArgs e)
		{
			CheckAgain();

		}

		 static bool CheckAll(){
			bool perfect = true;

			if (!DirectXInstalled()){
				perfect = false;
			}else{

			}
			if (!MDXInstalled()){
				perfect = false;
			}else{

			}


			if (perfect){
            	ReturnCode = 0;
			}
			return perfect;
		}

		 public bool CheckAgain(){
			bool perfect = true;

			if (!DirectXInstalled()){
				perfect = false;
				DirectXBox.Visible = true;
			}else{
				DirectXBox.Visible = false;
			}
			if (!MDXInstalled()){
				perfect = false;
				this.MDXBox.Visible = true;
			}else{
				this.MDXBox.Visible = false;
			}


			if (perfect){
				ReturnCode = 0;
				MessageBox.Show("You need to reboot your System after this installation!");
				this.Close();
			}
			return perfect;
		}

		private void Cancel_Button_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void button4_Click(object sender, System.EventArgs e)
		{
			System.Diagnostics.Process.Start("http://dreambeam.sf.net/mdxredist.msi");
		}
		
		private void WinForm_Load(object sender, System.EventArgs e)
		{
			this.CheckAgain();
		}
	}
}
