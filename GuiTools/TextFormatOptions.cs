using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Xml.Serialization;
using System.Xml;


namespace DreamBeam
{

	/// <summary>
	/// This is the control in the title, verse, etc... tabs in the
	/// Edit->Options dialog box. There's one of each for each type of text we
    /// handle (song title, song verse, etc...) so we can control the format of
    /// each type.
	/// </summary>
	public class TextFormatOptions : System.Windows.Forms.UserControl
	{
		#region Designer generated variables
		private OPaC.Themed.Forms.GroupBox groupBox3;
		private OPaC.Themed.Forms.Label label7;
		private OPaC.Themed.Forms.Label label6;
		private OPaC.Themed.Forms.Label label5;
		private OPaC.Themed.Forms.Label label4;
		private OPaC.Themed.Forms.GroupBox groupBox5;
		private OPaC.Themed.Forms.Label label8;
		private System.Windows.Forms.Button FontButton;
		private System.Windows.Forms.Button TextColor;
		private System.Windows.Forms.Button OutlineColor;
		private OPaC.Themed.Forms.GroupBox VerticalAlignment;
		private System.Windows.Forms.RadioButton VAlignBottom;
		private System.Windows.Forms.RadioButton VAlignCenter;
		private System.Windows.Forms.RadioButton VAlignTop;
		private OPaC.Themed.Forms.GroupBox HorizontalAlignment;
		private System.Windows.Forms.RadioButton HAlignRight;
		private System.Windows.Forms.RadioButton HAlignCenter;
		private System.Windows.Forms.RadioButton HAlignLeft;
		private System.Windows.Forms.FontDialog fontDialog;
		private System.Windows.Forms.ColorDialog colorDialog;
		private System.Windows.Forms.NumericUpDown Bounds4;
		private System.Windows.Forms.NumericUpDown Bounds3;
		private System.Windows.Forms.NumericUpDown Bounds2;
		private System.Windows.Forms.NumericUpDown Bounds1;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.NumericUpDown OutlineSize;
		private System.Windows.Forms.ComboBox Effects;
		private OPaC.Themed.Forms.Label label1;
		#endregion

		private BeamTextFormat format1 = new BeamTextFormat();

		public TextFormatOptions()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			// TODO: Add any initialization after the InitializeComponent call
			SetControls();
		}

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public BeamTextFormat Format {
			get {
				this.ReadControls();
				return this.format1;
			}
			set {
				if (value == null) { return; }
				this.format1 = value.Clone();
				this.SetControls();
			}
		}

		private void ReadControls() {
			this.format1.Bounds.X = (float)this.Bounds1.Value;
			this.format1.Bounds.Y = (float)this.Bounds2.Value;
			this.format1.Bounds.Width = (float)(100 - this.Bounds3.Value - this.Bounds1.Value);
			this.format1.Bounds.Height = (float)(100 - this.Bounds4.Value - this.Bounds2.Value);

			this.format1.OutlineSize = (int)this.OutlineSize.Value;

			this.format1.TextFont = this.fontDialog.Font;

			if (this.HAlignLeft.Checked) {
				this.format1.HAlignment = StringAlignment.Near;
			} else if (this.HAlignRight.Checked) {
				this.format1.HAlignment = StringAlignment.Far;
			} else {
				this.format1.HAlignment = StringAlignment.Center;
			}

			if (this.VAlignTop.Checked) {
				this.format1.VAlignment = StringAlignment.Near;
			} else if (this.VAlignBottom.Checked) {
				this.format1.VAlignment = StringAlignment.Far;
			} else {
				this.format1.VAlignment = StringAlignment.Center;
			}

			this.format1.Effects = this.Effects.Text;
		}

		private void SetControls() {
			// If we assign values that are out-of-range to the controls, we'll get an exception.
			this.Bounds1.Value = Tools.ForceToRange(Bounds1.Minimum, Bounds1.Maximum, (decimal)this.format1.Bounds.Left);
			this.Bounds2.Value = Tools.ForceToRange(Bounds2.Minimum, Bounds2.Maximum, (decimal)this.format1.Bounds.Top);
			this.Bounds3.Value = Tools.ForceToRange(Bounds3.Minimum, Bounds3.Maximum, (decimal)(100F - this.format1.Bounds.Right));
			this.Bounds4.Value = Tools.ForceToRange(Bounds4.Minimum, Bounds4.Maximum, (decimal)(100F - this.format1.Bounds.Bottom));

			this.OutlineSize.Value = Tools.ForceToRange(OutlineSize.Minimum, OutlineSize.Maximum, this.format1.OutlineSize);
			if (format1.TextFont != null) {
				this.fontDialog.Font = this.format1.TextFont;
			} else {
				this.fontDialog.Font = new Font("Times New Roman", 48);
			}

			switch (format1.HAlignment) {
				case StringAlignment.Near:
					this.HAlignLeft.Checked = true;
					break;
				case StringAlignment.Center:
					this.HAlignCenter.Checked = true;
					break;
				case StringAlignment.Far:
					this.HAlignRight.Checked = true;
					break;
			}

			switch (format1.VAlignment) {
				case StringAlignment.Near:
					this.VAlignTop.Checked = true;
					break;
				case StringAlignment.Center:
					this.VAlignCenter.Checked = true;
					break;
				case StringAlignment.Far:
					this.VAlignBottom.Checked = true;
					break;
			}

			this.Effects.Text = format1.Effects;
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.VerticalAlignment = new OPaC.Themed.Forms.GroupBox();
			this.VAlignBottom = new System.Windows.Forms.RadioButton();
			this.VAlignCenter = new System.Windows.Forms.RadioButton();
			this.VAlignTop = new System.Windows.Forms.RadioButton();
			this.groupBox3 = new OPaC.Themed.Forms.GroupBox();
			this.label7 = new OPaC.Themed.Forms.Label();
			this.label6 = new OPaC.Themed.Forms.Label();
			this.label5 = new OPaC.Themed.Forms.Label();
			this.label4 = new OPaC.Themed.Forms.Label();
			this.Bounds4 = new System.Windows.Forms.NumericUpDown();
			this.Bounds3 = new System.Windows.Forms.NumericUpDown();
			this.Bounds2 = new System.Windows.Forms.NumericUpDown();
			this.Bounds1 = new System.Windows.Forms.NumericUpDown();
			this.HorizontalAlignment = new OPaC.Themed.Forms.GroupBox();
			this.HAlignRight = new System.Windows.Forms.RadioButton();
			this.HAlignCenter = new System.Windows.Forms.RadioButton();
			this.HAlignLeft = new System.Windows.Forms.RadioButton();
			this.FontButton = new System.Windows.Forms.Button();
			this.TextColor = new System.Windows.Forms.Button();
			this.groupBox5 = new OPaC.Themed.Forms.GroupBox();
			this.label1 = new OPaC.Themed.Forms.Label();
			this.label8 = new OPaC.Themed.Forms.Label();
			this.OutlineSize = new System.Windows.Forms.NumericUpDown();
			this.OutlineColor = new System.Windows.Forms.Button();
			this.Effects = new System.Windows.Forms.ComboBox();
			this.fontDialog = new System.Windows.Forms.FontDialog();
			this.colorDialog = new System.Windows.Forms.ColorDialog();
			this.VerticalAlignment.SuspendLayout();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Bounds4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Bounds3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Bounds2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Bounds1)).BeginInit();
			this.HorizontalAlignment.SuspendLayout();
			this.groupBox5.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.OutlineSize)).BeginInit();
			this.SuspendLayout();
			// 
			// VerticalAlignment
			// 
			this.VerticalAlignment.BackColor = System.Drawing.Color.Transparent;
			this.VerticalAlignment.Controls.Add(this.VAlignBottom);
			this.VerticalAlignment.Controls.Add(this.VAlignCenter);
			this.VerticalAlignment.Controls.Add(this.VAlignTop);
			this.VerticalAlignment.Location = new System.Drawing.Point(176, 64);
			this.VerticalAlignment.Name = "VerticalAlignment";
			this.VerticalAlignment.Size = new System.Drawing.Size(208, 52);
			this.VerticalAlignment.TabIndex = 13;
			this.VerticalAlignment.TabStop = false;
			this.VerticalAlignment.Text = "Vertical Alignment";
			// 
			// VAlignBottom
			// 
			this.VAlignBottom.Location = new System.Drawing.Point(144, 20);
			this.VAlignBottom.Name = "VAlignBottom";
			this.VAlignBottom.Size = new System.Drawing.Size(58, 24);
			this.VAlignBottom.TabIndex = 2;
			this.VAlignBottom.Text = "Bottom";
			// 
			// VAlignCenter
			// 
			this.VAlignCenter.Location = new System.Drawing.Point(74, 20);
			this.VAlignCenter.Name = "VAlignCenter";
			this.VAlignCenter.Size = new System.Drawing.Size(58, 24);
			this.VAlignCenter.TabIndex = 1;
			this.VAlignCenter.Text = "Center";
			// 
			// VAlignTop
			// 
			this.VAlignTop.Location = new System.Drawing.Point(6, 20);
			this.VAlignTop.Name = "VAlignTop";
			this.VAlignTop.Size = new System.Drawing.Size(58, 24);
			this.VAlignTop.TabIndex = 0;
			this.VAlignTop.Text = "Top";
			// 
			// groupBox3
			// 
			this.groupBox3.BackColor = System.Drawing.Color.Transparent;
			this.groupBox3.Controls.Add(this.label7);
			this.groupBox3.Controls.Add(this.label6);
			this.groupBox3.Controls.Add(this.label5);
			this.groupBox3.Controls.Add(this.label4);
			this.groupBox3.Controls.Add(this.Bounds4);
			this.groupBox3.Controls.Add(this.Bounds3);
			this.groupBox3.Controls.Add(this.Bounds2);
			this.groupBox3.Controls.Add(this.Bounds1);
			this.groupBox3.Location = new System.Drawing.Point(8, 46);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(152, 154);
			this.groupBox3.TabIndex = 12;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Margins (in %)";
			// 
			// label7
			// 
			this.label7.BackColor = System.Drawing.Color.Transparent;
			this.label7.Location = new System.Drawing.Point(58, 106);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(44, 16);
			this.label7.TabIndex = 7;
			this.label7.Text = "Bottom";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label6
			// 
			this.label6.BackColor = System.Drawing.Color.Transparent;
			this.label6.Location = new System.Drawing.Point(100, 64);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(44, 16);
			this.label6.TabIndex = 6;
			this.label6.Text = "Right";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label5
			// 
			this.label5.BackColor = System.Drawing.Color.Transparent;
			this.label5.Location = new System.Drawing.Point(58, 22);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(44, 16);
			this.label5.TabIndex = 5;
			this.label5.Text = "Top";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label4
			// 
			this.label4.BackColor = System.Drawing.Color.Transparent;
			this.label4.Location = new System.Drawing.Point(8, 62);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(44, 16);
			this.label4.TabIndex = 4;
			this.label4.Text = "Left";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// Bounds4
			// 
			this.Bounds4.Location = new System.Drawing.Point(56, 122);
			this.Bounds4.Name = "Bounds4";
			this.Bounds4.Size = new System.Drawing.Size(48, 20);
			this.Bounds4.TabIndex = 3;
			this.Bounds4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.Bounds4.Value = new System.Decimal(new int[] {
																  5,
																  0,
																  0,
																  0});
			// 
			// Bounds3
			// 
			this.Bounds3.Location = new System.Drawing.Point(98, 80);
			this.Bounds3.Name = "Bounds3";
			this.Bounds3.Size = new System.Drawing.Size(48, 20);
			this.Bounds3.TabIndex = 2;
			this.Bounds3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.Bounds3.Value = new System.Decimal(new int[] {
																  5,
																  0,
																  0,
																  0});
			// 
			// Bounds2
			// 
			this.Bounds2.Location = new System.Drawing.Point(56, 38);
			this.Bounds2.Name = "Bounds2";
			this.Bounds2.Size = new System.Drawing.Size(48, 20);
			this.Bounds2.TabIndex = 1;
			this.Bounds2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.Bounds2.Value = new System.Decimal(new int[] {
																  5,
																  0,
																  0,
																  0});
			// 
			// Bounds1
			// 
			this.Bounds1.Location = new System.Drawing.Point(6, 78);
			this.Bounds1.Name = "Bounds1";
			this.Bounds1.Size = new System.Drawing.Size(48, 20);
			this.Bounds1.TabIndex = 0;
			this.Bounds1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.Bounds1.Value = new System.Decimal(new int[] {
																  5,
																  0,
																  0,
																  0});
			// 
			// HorizontalAlignment
			// 
			this.HorizontalAlignment.BackColor = System.Drawing.Color.Transparent;
			this.HorizontalAlignment.Controls.Add(this.HAlignRight);
			this.HorizontalAlignment.Controls.Add(this.HAlignCenter);
			this.HorizontalAlignment.Controls.Add(this.HAlignLeft);
			this.HorizontalAlignment.Location = new System.Drawing.Point(176, 8);
			this.HorizontalAlignment.Name = "HorizontalAlignment";
			this.HorizontalAlignment.Size = new System.Drawing.Size(208, 52);
			this.HorizontalAlignment.TabIndex = 11;
			this.HorizontalAlignment.TabStop = false;
			this.HorizontalAlignment.Text = "Horizontal Alignment";
			// 
			// HAlignRight
			// 
			this.HAlignRight.Location = new System.Drawing.Point(142, 20);
			this.HAlignRight.Name = "HAlignRight";
			this.HAlignRight.Size = new System.Drawing.Size(58, 24);
			this.HAlignRight.TabIndex = 2;
			this.HAlignRight.Text = "Right";
			// 
			// HAlignCenter
			// 
			this.HAlignCenter.Location = new System.Drawing.Point(76, 20);
			this.HAlignCenter.Name = "HAlignCenter";
			this.HAlignCenter.Size = new System.Drawing.Size(58, 24);
			this.HAlignCenter.TabIndex = 1;
			this.HAlignCenter.Text = "Center";
			// 
			// HAlignLeft
			// 
			this.HAlignLeft.Location = new System.Drawing.Point(10, 20);
			this.HAlignLeft.Name = "HAlignLeft";
			this.HAlignLeft.Size = new System.Drawing.Size(58, 24);
			this.HAlignLeft.TabIndex = 0;
			this.HAlignLeft.Text = "Left";
			// 
			// FontButton
			// 
			this.FontButton.Location = new System.Drawing.Point(8, 12);
			this.FontButton.Name = "FontButton";
			this.FontButton.Size = new System.Drawing.Size(38, 23);
			this.FontButton.TabIndex = 9;
			this.FontButton.Text = "Font";
			this.FontButton.Click += new System.EventHandler(this.FontButton_Click);
			// 
			// TextColor
			// 
			this.TextColor.Location = new System.Drawing.Point(48, 12);
			this.TextColor.Name = "TextColor";
			this.TextColor.Size = new System.Drawing.Size(40, 23);
			this.TextColor.TabIndex = 10;
			this.TextColor.Text = "Color";
			this.TextColor.Click += new System.EventHandler(this.TextColor_Click);
			// 
			// groupBox5
			// 
			this.groupBox5.BackColor = System.Drawing.Color.Transparent;
			this.groupBox5.Controls.Add(this.label1);
			this.groupBox5.Controls.Add(this.label8);
			this.groupBox5.Controls.Add(this.OutlineSize);
			this.groupBox5.Controls.Add(this.OutlineColor);
			this.groupBox5.Controls.Add(this.Effects);
			this.groupBox5.Location = new System.Drawing.Point(176, 120);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(206, 78);
			this.groupBox5.TabIndex = 14;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Text Outline";
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Location = new System.Drawing.Point(8, 22);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(42, 20);
			this.label1.TabIndex = 16;
			this.label1.Text = "Effects";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label8
			// 
			this.label8.BackColor = System.Drawing.Color.Transparent;
			this.label8.Location = new System.Drawing.Point(8, 52);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(70, 15);
			this.label8.TabIndex = 9;
			this.label8.Text = "Outline size:";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// OutlineSize
			// 
			this.OutlineSize.Location = new System.Drawing.Point(84, 50);
			this.OutlineSize.Minimum = new System.Decimal(new int[] {
																		1,
																		0,
																		0,
																		0});
			this.OutlineSize.Name = "OutlineSize";
			this.OutlineSize.Size = new System.Drawing.Size(46, 20);
			this.OutlineSize.TabIndex = 8;
			this.OutlineSize.Value = new System.Decimal(new int[] {
																	  1,
																	  0,
																	  0,
																	  0});
			// 
			// OutlineColor
			// 
			this.OutlineColor.Location = new System.Drawing.Point(138, 52);
			this.OutlineColor.Name = "OutlineColor";
			this.OutlineColor.Size = new System.Drawing.Size(56, 18);
			this.OutlineColor.TabIndex = 2;
			this.OutlineColor.Text = "Color";
			this.OutlineColor.Click += new System.EventHandler(this.OutlineColor_Click);
			// 
			// Effects
			// 
			this.Effects.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Effects.Items.AddRange(new object[] {
														 "Normal",
														 "Outline",
														 "Filled Outline"});
			this.Effects.Location = new System.Drawing.Point(56, 20);
			this.Effects.Name = "Effects";
			this.Effects.Size = new System.Drawing.Size(138, 21);
			this.Effects.TabIndex = 15;
			// 
			// TextFormatOptions
			// 
			this.Controls.Add(this.VerticalAlignment);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.HorizontalAlignment);
			this.Controls.Add(this.FontButton);
			this.Controls.Add(this.TextColor);
			this.Controls.Add(this.groupBox5);
			this.Name = "TextFormatOptions";
			this.Size = new System.Drawing.Size(392, 206);
			this.VerticalAlignment.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Bounds4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Bounds3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Bounds2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Bounds1)).EndInit();
			this.HorizontalAlignment.ResumeLayout(false);
			this.groupBox5.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.OutlineSize)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void FontButton_Click(object sender, System.EventArgs e) {
			try {
				this.fontDialog.ShowDialog();
			} catch {}
			this.format1.TextFont = this.fontDialog.Font;
		}

		private void TextColor_Click(object sender, System.EventArgs e) {
			try {
				this.colorDialog.Color = this.format1.TextColor;
				this.colorDialog.ShowDialog();
			} catch {}
			this.format1.TextColor = this.colorDialog.Color;
		}

		private void OutlineColor_Click(object sender, System.EventArgs e) {
			try {
				this.colorDialog.Color = this.format1.OutlineColor;
				this.colorDialog.ShowDialog();
			} catch {}
			this.format1.OutlineColor = this.colorDialog.Color;
		}

	}


	/// <summary>
	/// These are the settings we need to keep track of for most text pieces in DreamBeam.
	/// They correspond to the controls of the TextFormatOptions UserControl.
	/// </summary>
	[Serializable]
	public class BeamTextFormat : ICloneable {
		// XML can not serialize Color. We use a property to get around the problem.
		[XmlIgnore] public System.Drawing.Color TextColor = new System.Drawing.Color();
		[XmlIgnore] public System.Drawing.Color OutlineColor = new System.Drawing.Color();
		public int OutlineSize = 1;
		public System.Drawing.StringAlignment HAlignment = StringAlignment.Center;
		public System.Drawing.StringAlignment VAlignment = StringAlignment.Center;

		/// <summary>
		/// This represents the boundary of the text on the screen, and is in
        /// percentage points with respect to the screen dimmensions.
		/// </summary>
		public RectangleF Bounds = new RectangleF(5F, 10F, 90F, 85F);
		public string Effects = "Normal";
		public string FontFamily = "Arial";
		public Single FontEmSize = 36.0F;
		public FontStyle FontStyle = FontStyle.Regular;

		public BeamTextFormat () {
			TextColor = Color.White;
			OutlineColor = Color.Red;
		}

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public string XmlTextColor {
			get { return Tools.SerializeColor(TextColor); }
			set { TextColor = Tools.DeserializeColor(value); }
		}

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public string XmlOutlineColor {
			get { return Tools.SerializeColor(OutlineColor); }
			set { OutlineColor = Tools.DeserializeColor(value); }
		}

		/// <summary>
		/// Font objects can not be serialized, so we use the TextFont property to
		/// allow font information to be serialized.
		/// </summary>
		[XmlIgnore()]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Font TextFont {
			get {
				return new Font(FontFamily, FontEmSize, FontStyle);
			}
			set {
				FontFamily = value.FontFamily.Name;
				FontEmSize = value.Size;
				FontStyle = value.Style;
			}
		}
		#region ICloneable Members

		/// <summary>
		/// Implements the ICloneable interface
		/// </summary>
		/// <returns></returns>
		object ICloneable.Clone() {
			// simply delegate to our type-safe cousin
			return this.Clone();
		}

		public virtual BeamTextFormat Clone() {
			BeamTextFormat f = new BeamTextFormat();

			// Start with a flat, memberwise copy
			f = this.MemberwiseClone() as BeamTextFormat;

			// Copy things that need special attention
			//f.FontFamily = (string)this.FontFamily.Clone();
			//f.Effects = (string)this.Effects.Clone();
			return f;
		}

		#endregion
	}


}
