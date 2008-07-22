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
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace DreamBeam {
	// Most action events (like the Click event) in Windows Forms
	// use the EventHandler delegate and the EventArgs arguments.
	// We will define our own delegate that does not specify parameters.
	// Mostly, we really don't care what the conditions of the
	// change were, we just care that something was changed.
	public delegate void ControlChangeHandler();

    
    
	/// <summary>
	/// This is the control in the title, verse, etc... tabs in the
	/// Edit->Options dialog box. There's one of each for each type of text we
	/// handle (song title, song verse, etc...) so we can control the format of
	/// each type.
	/// </summary>
	public class TextFormatOptions : System.Windows.Forms.UserControl {
		#region Designer generated variables
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
		private System.Windows.Forms.FontDialog fontDialog;
		private System.Windows.Forms.ColorDialog colorDialog;
		private System.Windows.Forms.NumericUpDown Bounds4;
		private System.Windows.Forms.NumericUpDown Bounds3;
		private System.Windows.Forms.NumericUpDown Bounds2;
        private System.Windows.Forms.NumericUpDown Bounds1;
        private IContainer components;
        private System.Windows.Forms.NumericUpDown OutlineSize;
		#endregion

		public bool changingControls = false;
		private Font font = null;
        private string Effects = "Normal";
		private Color textColor, outlineColor;
        private OfficePickers.ColorPicker.ComboBoxColorPicker ColorComboBox;
        //        private RibbonStyle.RibbonMenuButton ribbonMenuButton1;
        private FontCombo.FontComboBox fontComboBox1;
        private Panel panel2;
        private RibbonStyle.RibbonMenuButton VAlignTopButton;
        private RibbonStyle.RibbonMenuButton VAlignMiddleButton;
        private RibbonStyle.RibbonMenuButton VAlignBottomButton;
        private Panel panel3;
        private RibbonStyle.RibbonMenuButton HAlignRightButton;
        private RibbonStyle.RibbonMenuButton HAlignCenterButton;
        private RibbonStyle.RibbonMenuButton HAlignLeftButton;
        private Panel panel1;
        private RibbonStyle.RibbonMenuButton fontUnderlineButton;
        private RibbonStyle.RibbonMenuButton fontItalicButton;
        private RibbonStyle.RibbonMenuButton fontBoldButton;
        private ComboBox Fontsize;
        private RibbonStyle.RibbonMenuButton ribbonMenuButton1;
        private RibbonStyle.RibbonMenuButton TextSizeIncreaseButton;
        private RibbonStyle.RibbonMenuButton TextSizeDecreaseButton;
        private RibbonStyle.RibbonMenuButton ribbonMenuButton4;
        private OfficePickers.ColorPicker.ComboBoxColorPicker OutlineColorComboBox;
        private RibbonStyle.RibbonMenuButton ribbonMenuButton2;
        private RibbonStyle.RibbonMenuButton TextOutlineMenuButton;
        private ContextMenuStrip OutlineMenu;
        private ToolStripMenuItem NoOutlineItem;
        private ToolStripMenuItem FilledOutlineItem;
        private ToolStripMenuItem ClearOutlineItem;
        private RibbonStyle.RibbonMenuButton ribbonMenuButton3;
        private CodeVendor.Controls.Grouper grouper1;
        private CodeVendor.Controls.Grouper grouper2;
        private CodeVendor.Controls.Grouper grouper3;
        

		public TextFormatOptions() {
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			SetControls(new BeamTextFormat());


			Bounds1.ValueChanged += new EventHandler(ControlChanged);
			Bounds2.ValueChanged += new EventHandler(ControlChanged);
			Bounds3.ValueChanged += new EventHandler(ControlChanged);
			Bounds4.ValueChanged += new EventHandler(ControlChanged);

			OutlineSize.ValueChanged += new EventHandler(ControlChanged);
            Fontsize.TextChanged += new EventHandler(ControlChanged);
            fontComboBox1.TextChanged += new EventHandler(ControlChanged);
            
            ColorComboBox.SelectedColorChanged += new EventHandler(ControlChanged);
            OutlineColorComboBox.SelectedColorChanged += new EventHandler(ControlChanged);

            //HAlignCenter.CheckedChanged += new EventHandler(ControlChanged);
			//HAlignLeft.CheckedChanged += new EventHandler(ControlChanged);
			//HAlignRight.CheckedChanged += new EventHandler(ControlChanged);

            HAlignLeftButton.Click += new EventHandler(ControlChanged);
            HAlignRightButton.Click += new EventHandler(ControlChanged);
            HAlignCenterButton.Click += new EventHandler(ControlChanged);

            VAlignBottomButton.Click += new EventHandler(ControlChanged);
            VAlignMiddleButton.Click += new EventHandler(ControlChanged);
            VAlignTopButton.Click += new EventHandler(ControlChanged);

            fontBoldButton.Click += new EventHandler(ControlChanged);
            fontItalicButton.Click += new EventHandler(ControlChanged);
            fontUnderlineButton.Click += new EventHandler(ControlChanged);

            fontComboBox1.Populate(false);
            
            
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public BeamTextFormat Format {
			get {                
				return ReadControls();
			}
			set {
				if (value == null) { return; }
				this.SetControls(value);
			}
		}
        
        private Font CreateFont()
        {   
            FontStyle fstyle = FontStyle.Regular;

            if (this.fontBoldButton.Checked) fstyle = fstyle | FontStyle.Bold;
            if (this.fontItalicButton.Checked) fstyle = fstyle | FontStyle.Italic;
            if (this.fontUnderlineButton.Checked) fstyle = fstyle | FontStyle.Underline;          
            if (this.fontComboBox1.Text.Length == 0) this.fontComboBox1.Text = "Arial";
            if (this.Fontsize.Text.Length == 0) this.Fontsize.Text = "55";
            return new Font(this.fontComboBox1.SelectedText, (float)Convert.ToDecimal(this.Fontsize.Text), fstyle);
        }

        private void Setfont()
        {        
            this.fontBoldButton.Checked = this.font.Bold;
            this.fontItalicButton.Checked = this.font.Italic;
            this.fontUnderlineButton.Checked = this.font.Underline;
            this.fontComboBox1.Text = this.font.FontFamily.Name;
            this.Fontsize.Text = this.font.Size.ToString();

            
        }

		private BeamTextFormat ReadControls() {
			BeamTextFormat format = new BeamTextFormat();

			format.Bounds.X = (float)this.Bounds1.Value;
			format.Bounds.Y = (float)this.Bounds2.Value;
			format.Bounds.Width = (float)(100 - this.Bounds3.Value - this.Bounds1.Value);
			format.Bounds.Height = (float)(100 - this.Bounds4.Value - this.Bounds2.Value);

			format.OutlineSize = (int)this.OutlineSize.Value;                        
            this.font = CreateFont();

            format.Effects = this.Effects;

			if (this.font != null) format.TextFont = this.font;
            format.TextColor = ColorComboBox.Color;

            format.OutlineColor = OutlineColorComboBox.Color;

            if (this.HAlignLeftButton.Checked){
				format.HAlignment = StringAlignment.Near;}
            else if (this.HAlignRightButton.Checked
                ){
				format.HAlignment = StringAlignment.Far;
			} else {
				format.HAlignment = StringAlignment.Center;
			}

			if (this.VAlignTopButton.Checked) {
				
                format.VAlignment = StringAlignment.Far;
			} else if (this.VAlignBottomButton.Checked) {
                format.VAlignment = StringAlignment.Near;				
			} else {
				format.VAlignment = StringAlignment.Center;
			}
		
			return format;
		}

		private void SetControls(BeamTextFormat format) {            
            changingControls = true;

			this.font = format.TextFont;
            Setfont();
			ColorComboBox.Color = format.TextColor;			
            OutlineColorComboBox.Color = format.OutlineColor;

			// If we assign values that are out-of-range to the controls, we'll get an exception.
			this.Bounds1.Value = Tools.ForceToRange(Bounds1.Minimum, Bounds1.Maximum, (decimal)format.Bounds.Left);
			this.Bounds2.Value = Tools.ForceToRange(Bounds2.Minimum, Bounds2.Maximum, (decimal)format.Bounds.Top);
			this.Bounds3.Value = Tools.ForceToRange(Bounds3.Minimum, Bounds3.Maximum, (decimal)(100F - format.Bounds.Right));
			this.Bounds4.Value = Tools.ForceToRange(Bounds4.Minimum, Bounds4.Maximum, (decimal)(100F - format.Bounds.Bottom));

			this.OutlineSize.Value = Tools.ForceToRange(OutlineSize.Minimum, OutlineSize.Maximum, format.OutlineSize);
			if (format.TextFont != null) {
				this.fontDialog.Font = format.TextFont;
			} else {
				this.fontDialog.Font = new Font("Times New Roman", 48);
			}

                           
			switch (format.HAlignment) {
				case StringAlignment.Near:                  
                    this.HAlignLeftButton.Check();
					break;
				case StringAlignment.Center:
                    this.HAlignCenterButton.Check();
					break;
				case StringAlignment.Far:
                    this.HAlignRightButton.Check();
					break;
			}

			switch (format.VAlignment) {
				case StringAlignment.Near:
                    this.VAlignBottomButton.Check();
					break;
				case StringAlignment.Center:
                    this.VAlignMiddleButton.Check();
					break;
				case StringAlignment.Far:
                    this.VAlignTopButton.Check();                    
					break;
			}

			this.Effects = format.Effects;

            switch (this.Effects)
            {
                case "Normal":
                    TextOutlineMenuButton.Image = NoOutlineItem.Image;
                    break;
                case "Outline":
                    TextOutlineMenuButton.Image = ClearOutlineItem.Image;
                    break;
                case "Filled Outline":
                    TextOutlineMenuButton.Image = FilledOutlineItem.Image;
                    break;
            }

			changingControls = false;            
			NotifyControlChangeListeners();
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing) {
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Bounds4 = new System.Windows.Forms.NumericUpDown();
            this.Bounds3 = new System.Windows.Forms.NumericUpDown();
            this.Bounds2 = new System.Windows.Forms.NumericUpDown();
            this.Bounds1 = new System.Windows.Forms.NumericUpDown();
            this.OutlineMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.NoOutlineItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FilledOutlineItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ClearOutlineItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fontDialog = new System.Windows.Forms.FontDialog();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.grouper3 = new CodeVendor.Controls.Grouper();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.grouper2 = new CodeVendor.Controls.Grouper();
            this.OutlineColorComboBox = new OfficePickers.ColorPicker.ComboBoxColorPicker();
            this.OutlineSize = new System.Windows.Forms.NumericUpDown();
            this.grouper1 = new CodeVendor.Controls.Grouper();
            this.ColorComboBox = new OfficePickers.ColorPicker.ComboBoxColorPicker();
            this.fontComboBox1 = new FontCombo.FontComboBox();
            this.Fontsize = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.HAlignRightButton = new RibbonStyle.RibbonMenuButton();
            this.HAlignCenterButton = new RibbonStyle.RibbonMenuButton();
            this.HAlignLeftButton = new RibbonStyle.RibbonMenuButton();
            this.VAlignTopButton = new RibbonStyle.RibbonMenuButton();
            this.VAlignMiddleButton = new RibbonStyle.RibbonMenuButton();
            this.VAlignBottomButton = new RibbonStyle.RibbonMenuButton();
            this.TextOutlineMenuButton = new RibbonStyle.RibbonMenuButton();
            this.ribbonMenuButton2 = new RibbonStyle.RibbonMenuButton();
            this.ribbonMenuButton3 = new RibbonStyle.RibbonMenuButton();
            this.ribbonMenuButton4 = new RibbonStyle.RibbonMenuButton();
            this.fontUnderlineButton = new RibbonStyle.RibbonMenuButton();
            this.fontItalicButton = new RibbonStyle.RibbonMenuButton();
            this.fontBoldButton = new RibbonStyle.RibbonMenuButton();
            this.ribbonMenuButton1 = new RibbonStyle.RibbonMenuButton();
            this.TextSizeIncreaseButton = new RibbonStyle.RibbonMenuButton();
            this.TextSizeDecreaseButton = new RibbonStyle.RibbonMenuButton();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Bounds4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bounds3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bounds2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bounds1)).BeginInit();
            this.OutlineMenu.SuspendLayout();
            this.grouper3.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.grouper2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OutlineSize)).BeginInit();
            this.grouper1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
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
            this.groupBox3.Location = new System.Drawing.Point(30, 169);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(152, 118);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Margins (in %)";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(58, 77);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 16);
            this.label7.TabIndex = 7;
            this.label7.Text = "Bottom";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(100, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 16);
            this.label6.TabIndex = 6;
            this.label6.Text = "Right";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(58, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 16);
            this.label5.TabIndex = 5;
            this.label5.Text = "Top";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(8, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 16);
            this.label4.TabIndex = 4;
            this.label4.Text = "Left";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Bounds4
            // 
            this.Bounds4.DecimalPlaces = 1;
            this.Bounds4.Location = new System.Drawing.Point(56, 93);
            this.Bounds4.Name = "Bounds4";
            this.Bounds4.Size = new System.Drawing.Size(48, 20);
            this.Bounds4.TabIndex = 3;
            this.Bounds4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Bounds4.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // Bounds3
            // 
            this.Bounds3.DecimalPlaces = 1;
            this.Bounds3.Location = new System.Drawing.Point(98, 61);
            this.Bounds3.Name = "Bounds3";
            this.Bounds3.Size = new System.Drawing.Size(48, 20);
            this.Bounds3.TabIndex = 2;
            this.Bounds3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Bounds3.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // Bounds2
            // 
            this.Bounds2.DecimalPlaces = 1;
            this.Bounds2.Location = new System.Drawing.Point(56, 27);
            this.Bounds2.Name = "Bounds2";
            this.Bounds2.Size = new System.Drawing.Size(48, 20);
            this.Bounds2.TabIndex = 1;
            this.Bounds2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Bounds2.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // Bounds1
            // 
            this.Bounds1.DecimalPlaces = 1;
            this.Bounds1.Location = new System.Drawing.Point(6, 59);
            this.Bounds1.Name = "Bounds1";
            this.Bounds1.Size = new System.Drawing.Size(48, 20);
            this.Bounds1.TabIndex = 0;
            this.Bounds1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Bounds1.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // OutlineMenu
            // 
            this.OutlineMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NoOutlineItem,
            this.FilledOutlineItem,
            this.ClearOutlineItem});
            this.OutlineMenu.Name = "OutlineMenu";
            this.OutlineMenu.Size = new System.Drawing.Size(181, 70);
            // 
            // NoOutlineItem
            // 
            this.NoOutlineItem.Image = global::DreamBeam.Properties.Resources.stock_text_nonoutline;
            this.NoOutlineItem.Name = "NoOutlineItem";
            this.NoOutlineItem.Size = new System.Drawing.Size(180, 22);
            this.NoOutlineItem.Text = "toolStripMenuItem1";
            this.NoOutlineItem.Click += new System.EventHandler(this.NoOutlineItem_Click);
            // 
            // FilledOutlineItem
            // 
            this.FilledOutlineItem.Image = global::DreamBeam.Properties.Resources.stock_text_filledoutline;
            this.FilledOutlineItem.Name = "FilledOutlineItem";
            this.FilledOutlineItem.Size = new System.Drawing.Size(180, 22);
            this.FilledOutlineItem.Text = "toolStripMenuItem2";
            this.FilledOutlineItem.Click += new System.EventHandler(this.FilledOutlineItem_Click);
            // 
            // ClearOutlineItem
            // 
            this.ClearOutlineItem.Image = global::DreamBeam.Properties.Resources.stock_text_outline;
            this.ClearOutlineItem.Name = "ClearOutlineItem";
            this.ClearOutlineItem.Size = new System.Drawing.Size(180, 22);
            this.ClearOutlineItem.Text = "toolStripMenuItem3";
            this.ClearOutlineItem.Click += new System.EventHandler(this.ClearOutlineItem_Click);
            // 
            // grouper3
            // 
            this.grouper3.BackgroundColor = System.Drawing.Color.BlanchedAlmond;
            this.grouper3.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouper3.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.BackwardDiagonal;
            this.grouper3.BorderColor = System.Drawing.Color.Black;
            this.grouper3.BorderThickness = 1F;
            this.grouper3.Controls.Add(this.panel3);
            this.grouper3.Controls.Add(this.panel2);
            this.grouper3.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grouper3.GroupImage = null;
            this.grouper3.GroupTitle = "";
            this.grouper3.Location = new System.Drawing.Point(152, 92);
            this.grouper3.Name = "grouper3";
            this.grouper3.Padding = new System.Windows.Forms.Padding(20);
            this.grouper3.PaintGroupBox = false;
            this.grouper3.RoundCorners = 2;
            this.grouper3.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouper3.ShadowControl = false;
            this.grouper3.ShadowThickness = 3;
            this.grouper3.Size = new System.Drawing.Size(83, 71);
            this.grouper3.TabIndex = 27;
            this.grouper3.TinyMode = true;
            this.grouper3.TitleBorder = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.HAlignRightButton);
            this.panel3.Controls.Add(this.HAlignCenterButton);
            this.panel3.Controls.Add(this.HAlignLeftButton);
            this.panel3.Location = new System.Drawing.Point(5, 12);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(76, 26);
            this.panel3.TabIndex = 24;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.VAlignTopButton);
            this.panel2.Controls.Add(this.VAlignMiddleButton);
            this.panel2.Controls.Add(this.VAlignBottomButton);
            this.panel2.Location = new System.Drawing.Point(4, 36);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(79, 29);
            this.panel2.TabIndex = 23;
            // 
            // grouper2
            // 
            this.grouper2.BackgroundColor = System.Drawing.Color.BlanchedAlmond;
            this.grouper2.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouper2.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.BackwardDiagonal;
            this.grouper2.BorderColor = System.Drawing.Color.Black;
            this.grouper2.BorderThickness = 1F;
            this.grouper2.Controls.Add(this.OutlineColorComboBox);
            this.grouper2.Controls.Add(this.TextOutlineMenuButton);
            this.grouper2.Controls.Add(this.ribbonMenuButton2);
            this.grouper2.Controls.Add(this.OutlineSize);
            this.grouper2.Controls.Add(this.ribbonMenuButton3);
            this.grouper2.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grouper2.GroupImage = null;
            this.grouper2.GroupTitle = "Text Outline";
            this.grouper2.Location = new System.Drawing.Point(0, 92);
            this.grouper2.Name = "grouper2";
            this.grouper2.Padding = new System.Windows.Forms.Padding(20);
            this.grouper2.PaintGroupBox = false;
            this.grouper2.RoundCorners = 2;
            this.grouper2.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouper2.ShadowControl = false;
            this.grouper2.ShadowThickness = 3;
            this.grouper2.Size = new System.Drawing.Size(149, 55);
            this.grouper2.TabIndex = 26;
            this.grouper2.TinyMode = true;
            this.grouper2.TitleBorder = true;
            // 
            // OutlineColorComboBox
            // 
            this.OutlineColorComboBox.Color = System.Drawing.Color.Black;
            this.OutlineColorComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.OutlineColorComboBox.DropDownHeight = 1;
            this.OutlineColorComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OutlineColorComboBox.DropDownWidth = 1;
            this.OutlineColorComboBox.FormattingEnabled = true;
            this.OutlineColorComboBox.IntegralHeight = false;
            this.OutlineColorComboBox.ItemHeight = 16;
            this.OutlineColorComboBox.Items.AddRange(new object[] {
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color"});
            this.OutlineColorComboBox.Location = new System.Drawing.Point(91, 21);
            this.OutlineColorComboBox.Name = "OutlineColorComboBox";
            this.OutlineColorComboBox.Size = new System.Drawing.Size(46, 22);
            this.OutlineColorComboBox.TabIndex = 32;
            // 
            // OutlineSize
            // 
            this.OutlineSize.Location = new System.Drawing.Point(46, 23);
            this.OutlineSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.OutlineSize.Name = "OutlineSize";
            this.OutlineSize.Size = new System.Drawing.Size(37, 20);
            this.OutlineSize.TabIndex = 8;
            this.OutlineSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // grouper1
            // 
            this.grouper1.BackgroundColor = System.Drawing.Color.BlanchedAlmond;
            this.grouper1.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouper1.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.BackwardDiagonal;
            this.grouper1.BorderColor = System.Drawing.Color.Black;
            this.grouper1.BorderThickness = 1F;
            this.grouper1.Controls.Add(this.ColorComboBox);
            this.grouper1.Controls.Add(this.ribbonMenuButton4);
            this.grouper1.Controls.Add(this.fontComboBox1);
            this.grouper1.Controls.Add(this.Fontsize);
            this.grouper1.Controls.Add(this.panel1);
            this.grouper1.Controls.Add(this.ribbonMenuButton1);
            this.grouper1.Controls.Add(this.TextSizeIncreaseButton);
            this.grouper1.Controls.Add(this.TextSizeDecreaseButton);
            this.grouper1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grouper1.GroupImage = null;
            this.grouper1.GroupTitle = "Font";
            this.grouper1.Location = new System.Drawing.Point(0, 0);
            this.grouper1.Name = "grouper1";
            this.grouper1.Padding = new System.Windows.Forms.Padding(20);
            this.grouper1.PaintGroupBox = false;
            this.grouper1.RoundCorners = 2;
            this.grouper1.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouper1.ShadowControl = false;
            this.grouper1.ShadowThickness = 3;
            this.grouper1.Size = new System.Drawing.Size(235, 88);
            this.grouper1.TabIndex = 25;
            this.grouper1.TinyMode = true;
            this.grouper1.TitleBorder = true;
            // 
            // ColorComboBox
            // 
            this.ColorComboBox.Color = System.Drawing.Color.Black;
            this.ColorComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ColorComboBox.DropDownHeight = 1;
            this.ColorComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ColorComboBox.DropDownWidth = 1;
            this.ColorComboBox.FormattingEnabled = true;
            this.ColorComboBox.IntegralHeight = false;
            this.ColorComboBox.ItemHeight = 16;
            this.ColorComboBox.Items.AddRange(new object[] {
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color",
            "Color"});
            this.ColorComboBox.Location = new System.Drawing.Point(123, 51);
            this.ColorComboBox.Name = "ColorComboBox";
            this.ColorComboBox.Size = new System.Drawing.Size(46, 22);
            this.ColorComboBox.TabIndex = 16;
            // 
            // fontComboBox1
            // 
            this.fontComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.fontComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fontComboBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.fontComboBox1.FormattingEnabled = true;
            this.fontComboBox1.IntegralHeight = false;
            this.fontComboBox1.Location = new System.Drawing.Point(9, 21);
            this.fontComboBox1.MaxDropDownItems = 20;
            this.fontComboBox1.Name = "fontComboBox1";
            this.fontComboBox1.Size = new System.Drawing.Size(162, 21);
            this.fontComboBox1.TabIndex = 15;
            this.fontComboBox1.SelectedIndexChanged += new System.EventHandler(this.fontComboBox1_SelectedIndexChanged);
            // 
            // Fontsize
            // 
            this.Fontsize.FormattingEnabled = true;
            this.Fontsize.Items.AddRange(new object[] {
            "10",
            "14",
            "18",
            "20",
            "24",
            "28",
            "36",
            "48",
            "54",
            "62",
            "72",
            "84",
            "100",
            "110"});
            this.Fontsize.Location = new System.Drawing.Point(175, 21);
            this.Fontsize.Name = "Fontsize";
            this.Fontsize.Size = new System.Drawing.Size(46, 21);
            this.Fontsize.TabIndex = 27;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.fontUnderlineButton);
            this.panel1.Controls.Add(this.fontItalicButton);
            this.panel1.Controls.Add(this.fontBoldButton);
            this.panel1.Location = new System.Drawing.Point(4, 46);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(123, 33);
            this.panel1.TabIndex = 25;
            // 
            // HAlignRightButton
            // 
            this.HAlignRightButton.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
            this.HAlignRightButton.BackColor = System.Drawing.Color.Transparent;
            this.HAlignRightButton.Checked = false;
            this.HAlignRightButton.ColorBase = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(209)))), ((int)(((byte)(240)))));
            this.HAlignRightButton.ColorBaseStroke = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(187)))), ((int)(((byte)(213)))));
            this.HAlignRightButton.ColorOn = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.HAlignRightButton.ColorOnStroke = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.HAlignRightButton.ColorPress = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.HAlignRightButton.ColorPressStroke = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.HAlignRightButton.FadingSpeed = 35;
            this.HAlignRightButton.FlatAppearance.BorderSize = 0;
            this.HAlignRightButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.HAlignRightButton.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.Right;
            this.HAlignRightButton.Image = global::DreamBeam.Properties.Resources.stock_text_right_16;
            this.HAlignRightButton.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Top;
            this.HAlignRightButton.ImageOffset = 4;
            this.HAlignRightButton.IsPressed = true;
            this.HAlignRightButton.KeepPress = true;
            this.HAlignRightButton.Location = new System.Drawing.Point(47, 0);
            this.HAlignRightButton.MaxImageSize = new System.Drawing.Point(16, 16);
            this.HAlignRightButton.MenuPos = new System.Drawing.Point(0, 0);
            this.HAlignRightButton.Name = "HAlignRightButton";
            this.HAlignRightButton.Padding = new System.Windows.Forms.Padding(1);
            this.HAlignRightButton.Radius = 8;
            this.HAlignRightButton.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
            this.HAlignRightButton.SinglePressButton = false;
            this.HAlignRightButton.Size = new System.Drawing.Size(24, 24);
            this.HAlignRightButton.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
            this.HAlignRightButton.SplitDistance = 0;
            this.HAlignRightButton.TabIndex = 21;
            this.HAlignRightButton.Text = "\r\n";
            this.HAlignRightButton.Title = "";
            this.HAlignRightButton.UseVisualStyleBackColor = true;
            // 
            // HAlignCenterButton
            // 
            this.HAlignCenterButton.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
            this.HAlignCenterButton.BackColor = System.Drawing.Color.Transparent;
            this.HAlignCenterButton.Checked = false;
            this.HAlignCenterButton.ColorBase = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(209)))), ((int)(((byte)(240)))));
            this.HAlignCenterButton.ColorBaseStroke = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(187)))), ((int)(((byte)(213)))));
            this.HAlignCenterButton.ColorOn = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.HAlignCenterButton.ColorOnStroke = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.HAlignCenterButton.ColorPress = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.HAlignCenterButton.ColorPressStroke = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.HAlignCenterButton.FadingSpeed = 35;
            this.HAlignCenterButton.FlatAppearance.BorderSize = 0;
            this.HAlignCenterButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.HAlignCenterButton.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.Center;
            this.HAlignCenterButton.Image = global::DreamBeam.Properties.Resources.stock_text_center_16;
            this.HAlignCenterButton.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Top;
            this.HAlignCenterButton.ImageOffset = 4;
            this.HAlignCenterButton.IsPressed = false;
            this.HAlignCenterButton.KeepPress = true;
            this.HAlignCenterButton.Location = new System.Drawing.Point(24, 0);
            this.HAlignCenterButton.MaxImageSize = new System.Drawing.Point(16, 16);
            this.HAlignCenterButton.MenuPos = new System.Drawing.Point(0, 0);
            this.HAlignCenterButton.Name = "HAlignCenterButton";
            this.HAlignCenterButton.Padding = new System.Windows.Forms.Padding(1);
            this.HAlignCenterButton.Radius = 8;
            this.HAlignCenterButton.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
            this.HAlignCenterButton.SinglePressButton = false;
            this.HAlignCenterButton.Size = new System.Drawing.Size(24, 24);
            this.HAlignCenterButton.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
            this.HAlignCenterButton.SplitDistance = 0;
            this.HAlignCenterButton.TabIndex = 20;
            this.HAlignCenterButton.Text = "\r\n";
            this.HAlignCenterButton.Title = "";
            this.HAlignCenterButton.UseVisualStyleBackColor = true;
            // 
            // HAlignLeftButton
            // 
            this.HAlignLeftButton.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
            this.HAlignLeftButton.BackColor = System.Drawing.Color.Transparent;
            this.HAlignLeftButton.Checked = false;
            this.HAlignLeftButton.ColorBase = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(209)))), ((int)(((byte)(240)))));
            this.HAlignLeftButton.ColorBaseStroke = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(187)))), ((int)(((byte)(213)))));
            this.HAlignLeftButton.ColorOn = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.HAlignLeftButton.ColorOnStroke = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.HAlignLeftButton.ColorPress = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.HAlignLeftButton.ColorPressStroke = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.HAlignLeftButton.FadingSpeed = 35;
            this.HAlignLeftButton.FlatAppearance.BorderSize = 0;
            this.HAlignLeftButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.HAlignLeftButton.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.Left;
            this.HAlignLeftButton.Image = global::DreamBeam.Properties.Resources.stock_text_left_16;
            this.HAlignLeftButton.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Top;
            this.HAlignLeftButton.ImageOffset = 4;
            this.HAlignLeftButton.IsPressed = false;
            this.HAlignLeftButton.KeepPress = true;
            this.HAlignLeftButton.Location = new System.Drawing.Point(1, 0);
            this.HAlignLeftButton.MaxImageSize = new System.Drawing.Point(16, 16);
            this.HAlignLeftButton.MenuPos = new System.Drawing.Point(0, 0);
            this.HAlignLeftButton.Name = "HAlignLeftButton";
            this.HAlignLeftButton.Radius = 6;
            this.HAlignLeftButton.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
            this.HAlignLeftButton.SinglePressButton = false;
            this.HAlignLeftButton.Size = new System.Drawing.Size(24, 24);
            this.HAlignLeftButton.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
            this.HAlignLeftButton.SplitDistance = 0;
            this.HAlignLeftButton.TabIndex = 19;
            this.HAlignLeftButton.Text = "\r\n";
            this.HAlignLeftButton.Title = "";
            this.HAlignLeftButton.UseVisualStyleBackColor = true;
            // 
            // VAlignTopButton
            // 
            this.VAlignTopButton.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
            this.VAlignTopButton.BackColor = System.Drawing.Color.Transparent;
            this.VAlignTopButton.Checked = false;
            this.VAlignTopButton.ColorBase = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(209)))), ((int)(((byte)(240)))));
            this.VAlignTopButton.ColorBaseStroke = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(187)))), ((int)(((byte)(213)))));
            this.VAlignTopButton.ColorOn = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.VAlignTopButton.ColorOnStroke = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.VAlignTopButton.ColorPress = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.VAlignTopButton.ColorPressStroke = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.VAlignTopButton.FadingSpeed = 35;
            this.VAlignTopButton.FlatAppearance.BorderSize = 0;
            this.VAlignTopButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.VAlignTopButton.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.Right;
            this.VAlignTopButton.Image = global::DreamBeam.Properties.Resources.stock_text_vdown16;
            this.VAlignTopButton.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Top;
            this.VAlignTopButton.ImageOffset = 4;
            this.VAlignTopButton.IsPressed = true;
            this.VAlignTopButton.KeepPress = true;
            this.VAlignTopButton.Location = new System.Drawing.Point(48, 2);
            this.VAlignTopButton.MaxImageSize = new System.Drawing.Point(16, 16);
            this.VAlignTopButton.MenuPos = new System.Drawing.Point(0, 0);
            this.VAlignTopButton.Name = "VAlignTopButton";
            this.VAlignTopButton.Padding = new System.Windows.Forms.Padding(1);
            this.VAlignTopButton.Radius = 8;
            this.VAlignTopButton.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
            this.VAlignTopButton.SinglePressButton = false;
            this.VAlignTopButton.Size = new System.Drawing.Size(24, 24);
            this.VAlignTopButton.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
            this.VAlignTopButton.SplitDistance = 0;
            this.VAlignTopButton.TabIndex = 21;
            this.VAlignTopButton.Text = "\r\n";
            this.VAlignTopButton.Title = "";
            this.VAlignTopButton.UseVisualStyleBackColor = true;
            // 
            // VAlignMiddleButton
            // 
            this.VAlignMiddleButton.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
            this.VAlignMiddleButton.BackColor = System.Drawing.Color.Transparent;
            this.VAlignMiddleButton.Checked = false;
            this.VAlignMiddleButton.ColorBase = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(209)))), ((int)(((byte)(240)))));
            this.VAlignMiddleButton.ColorBaseStroke = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(187)))), ((int)(((byte)(213)))));
            this.VAlignMiddleButton.ColorOn = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.VAlignMiddleButton.ColorOnStroke = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.VAlignMiddleButton.ColorPress = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.VAlignMiddleButton.ColorPressStroke = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.VAlignMiddleButton.FadingSpeed = 35;
            this.VAlignMiddleButton.FlatAppearance.BorderSize = 0;
            this.VAlignMiddleButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.VAlignMiddleButton.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.Center;
            this.VAlignMiddleButton.Image = global::DreamBeam.Properties.Resources.stock_text_vcenter16;
            this.VAlignMiddleButton.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Top;
            this.VAlignMiddleButton.ImageOffset = 4;
            this.VAlignMiddleButton.IsPressed = false;
            this.VAlignMiddleButton.KeepPress = true;
            this.VAlignMiddleButton.Location = new System.Drawing.Point(25, 2);
            this.VAlignMiddleButton.MaxImageSize = new System.Drawing.Point(16, 16);
            this.VAlignMiddleButton.MenuPos = new System.Drawing.Point(0, 0);
            this.VAlignMiddleButton.Name = "VAlignMiddleButton";
            this.VAlignMiddleButton.Padding = new System.Windows.Forms.Padding(1);
            this.VAlignMiddleButton.Radius = 8;
            this.VAlignMiddleButton.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
            this.VAlignMiddleButton.SinglePressButton = false;
            this.VAlignMiddleButton.Size = new System.Drawing.Size(24, 24);
            this.VAlignMiddleButton.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
            this.VAlignMiddleButton.SplitDistance = 0;
            this.VAlignMiddleButton.TabIndex = 20;
            this.VAlignMiddleButton.Text = "\r\n";
            this.VAlignMiddleButton.Title = "";
            this.VAlignMiddleButton.UseVisualStyleBackColor = true;
            // 
            // VAlignBottomButton
            // 
            this.VAlignBottomButton.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
            this.VAlignBottomButton.BackColor = System.Drawing.Color.Transparent;
            this.VAlignBottomButton.Checked = false;
            this.VAlignBottomButton.ColorBase = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(209)))), ((int)(((byte)(240)))));
            this.VAlignBottomButton.ColorBaseStroke = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(187)))), ((int)(((byte)(213)))));
            this.VAlignBottomButton.ColorOn = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.VAlignBottomButton.ColorOnStroke = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.VAlignBottomButton.ColorPress = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.VAlignBottomButton.ColorPressStroke = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.VAlignBottomButton.FadingSpeed = 35;
            this.VAlignBottomButton.FlatAppearance.BorderSize = 0;
            this.VAlignBottomButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.VAlignBottomButton.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.Left;
            this.VAlignBottomButton.Image = global::DreamBeam.Properties.Resources.stock_text_vup16;
            this.VAlignBottomButton.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Top;
            this.VAlignBottomButton.ImageOffset = 4;
            this.VAlignBottomButton.IsPressed = false;
            this.VAlignBottomButton.KeepPress = true;
            this.VAlignBottomButton.Location = new System.Drawing.Point(2, 2);
            this.VAlignBottomButton.MaxImageSize = new System.Drawing.Point(16, 16);
            this.VAlignBottomButton.MenuPos = new System.Drawing.Point(0, 0);
            this.VAlignBottomButton.Name = "VAlignBottomButton";
            this.VAlignBottomButton.Radius = 6;
            this.VAlignBottomButton.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
            this.VAlignBottomButton.SinglePressButton = false;
            this.VAlignBottomButton.Size = new System.Drawing.Size(24, 24);
            this.VAlignBottomButton.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
            this.VAlignBottomButton.SplitDistance = 0;
            this.VAlignBottomButton.TabIndex = 19;
            this.VAlignBottomButton.Text = "\r\n";
            this.VAlignBottomButton.Title = "";
            this.VAlignBottomButton.UseVisualStyleBackColor = true;
            // 
            // TextOutlineMenuButton
            // 
            this.TextOutlineMenuButton.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.ToDown;
            this.TextOutlineMenuButton.BackColor = System.Drawing.Color.Transparent;
            this.TextOutlineMenuButton.Checked = false;
            this.TextOutlineMenuButton.ColorBase = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(209)))), ((int)(((byte)(240)))));
            this.TextOutlineMenuButton.ColorBaseStroke = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(187)))), ((int)(((byte)(213)))));
            this.TextOutlineMenuButton.ColorOn = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.TextOutlineMenuButton.ColorOnStroke = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.TextOutlineMenuButton.ColorPress = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.TextOutlineMenuButton.ColorPressStroke = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.TextOutlineMenuButton.ContextMenuStrip = this.OutlineMenu;
            this.TextOutlineMenuButton.FadingSpeed = 35;
            this.TextOutlineMenuButton.FlatAppearance.BorderSize = 0;
            this.TextOutlineMenuButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TextOutlineMenuButton.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.Left;
            this.TextOutlineMenuButton.Image = global::DreamBeam.Properties.Resources.stock_text_outline;
            this.TextOutlineMenuButton.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
            this.TextOutlineMenuButton.ImageOffset = 2;
            this.TextOutlineMenuButton.IsPressed = false;
            this.TextOutlineMenuButton.KeepPress = false;
            this.TextOutlineMenuButton.Location = new System.Drawing.Point(5, 18);
            this.TextOutlineMenuButton.MaxImageSize = new System.Drawing.Point(24, 24);
            this.TextOutlineMenuButton.MenuPos = new System.Drawing.Point(0, 0);
            this.TextOutlineMenuButton.Name = "TextOutlineMenuButton";
            this.TextOutlineMenuButton.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.TextOutlineMenuButton.Radius = 6;
            this.TextOutlineMenuButton.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
            this.TextOutlineMenuButton.SinglePressButton = false;
            this.TextOutlineMenuButton.Size = new System.Drawing.Size(36, 29);
            this.TextOutlineMenuButton.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.Yes;
            this.TextOutlineMenuButton.SplitDistance = 43;
            this.TextOutlineMenuButton.TabIndex = 23;
            this.TextOutlineMenuButton.Text = "\r\n";
            this.TextOutlineMenuButton.Title = "";
            this.TextOutlineMenuButton.UseVisualStyleBackColor = true;
            // 
            // ribbonMenuButton2
            // 
            this.ribbonMenuButton2.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
            this.ribbonMenuButton2.BackColor = System.Drawing.Color.Transparent;
            this.ribbonMenuButton2.Checked = false;
            this.ribbonMenuButton2.ColorBase = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(209)))), ((int)(((byte)(240)))));
            this.ribbonMenuButton2.ColorBaseStroke = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(187)))), ((int)(((byte)(213)))));
            this.ribbonMenuButton2.ColorOn = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.ribbonMenuButton2.ColorOnStroke = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.ribbonMenuButton2.ColorPress = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.ribbonMenuButton2.ColorPressStroke = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.ribbonMenuButton2.Enabled = false;
            this.ribbonMenuButton2.FadingSpeed = 35;
            this.ribbonMenuButton2.FlatAppearance.BorderSize = 0;
            this.ribbonMenuButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ribbonMenuButton2.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.Right;
            this.ribbonMenuButton2.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Top;
            this.ribbonMenuButton2.ImageOffset = 4;
            this.ribbonMenuButton2.IsPressed = false;
            this.ribbonMenuButton2.KeepPress = true;
            this.ribbonMenuButton2.Location = new System.Drawing.Point(86, 18);
            this.ribbonMenuButton2.MaxImageSize = new System.Drawing.Point(24, 24);
            this.ribbonMenuButton2.MenuPos = new System.Drawing.Point(0, 0);
            this.ribbonMenuButton2.Name = "ribbonMenuButton2";
            this.ribbonMenuButton2.Padding = new System.Windows.Forms.Padding(1);
            this.ribbonMenuButton2.Radius = 8;
            this.ribbonMenuButton2.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
            this.ribbonMenuButton2.SinglePressButton = false;
            this.ribbonMenuButton2.Size = new System.Drawing.Size(58, 29);
            this.ribbonMenuButton2.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
            this.ribbonMenuButton2.SplitDistance = 0;
            this.ribbonMenuButton2.TabIndex = 33;
            this.ribbonMenuButton2.Text = "\r\n";
            this.ribbonMenuButton2.Title = "";
            this.ribbonMenuButton2.UseVisualStyleBackColor = true;
            // 
            // ribbonMenuButton3
            // 
            this.ribbonMenuButton3.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
            this.ribbonMenuButton3.BackColor = System.Drawing.Color.Transparent;
            this.ribbonMenuButton3.Checked = false;
            this.ribbonMenuButton3.ColorBase = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(209)))), ((int)(((byte)(240)))));
            this.ribbonMenuButton3.ColorBaseStroke = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(187)))), ((int)(((byte)(213)))));
            this.ribbonMenuButton3.ColorOn = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.ribbonMenuButton3.ColorOnStroke = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.ribbonMenuButton3.ColorPress = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.ribbonMenuButton3.ColorPressStroke = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.ribbonMenuButton3.Enabled = false;
            this.ribbonMenuButton3.FadingSpeed = 35;
            this.ribbonMenuButton3.FlatAppearance.BorderSize = 0;
            this.ribbonMenuButton3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ribbonMenuButton3.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.Center;
            this.ribbonMenuButton3.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Top;
            this.ribbonMenuButton3.ImageOffset = 4;
            this.ribbonMenuButton3.IsPressed = false;
            this.ribbonMenuButton3.KeepPress = true;
            this.ribbonMenuButton3.Location = new System.Drawing.Point(40, 18);
            this.ribbonMenuButton3.MaxImageSize = new System.Drawing.Point(24, 24);
            this.ribbonMenuButton3.MenuPos = new System.Drawing.Point(0, 0);
            this.ribbonMenuButton3.Name = "ribbonMenuButton3";
            this.ribbonMenuButton3.Padding = new System.Windows.Forms.Padding(1);
            this.ribbonMenuButton3.Radius = 8;
            this.ribbonMenuButton3.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
            this.ribbonMenuButton3.SinglePressButton = false;
            this.ribbonMenuButton3.Size = new System.Drawing.Size(47, 29);
            this.ribbonMenuButton3.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
            this.ribbonMenuButton3.SplitDistance = 0;
            this.ribbonMenuButton3.TabIndex = 34;
            this.ribbonMenuButton3.Text = "\r\n";
            this.ribbonMenuButton3.Title = "";
            this.ribbonMenuButton3.UseVisualStyleBackColor = true;
            // 
            // ribbonMenuButton4
            // 
            this.ribbonMenuButton4.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
            this.ribbonMenuButton4.BackColor = System.Drawing.Color.Transparent;
            this.ribbonMenuButton4.Checked = false;
            this.ribbonMenuButton4.ColorBase = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(209)))), ((int)(((byte)(240)))));
            this.ribbonMenuButton4.ColorBaseStroke = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(187)))), ((int)(((byte)(213)))));
            this.ribbonMenuButton4.ColorOn = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.ribbonMenuButton4.ColorOnStroke = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.ribbonMenuButton4.ColorPress = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.ribbonMenuButton4.ColorPressStroke = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.ribbonMenuButton4.Enabled = false;
            this.ribbonMenuButton4.FadingSpeed = 35;
            this.ribbonMenuButton4.FlatAppearance.BorderSize = 0;
            this.ribbonMenuButton4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ribbonMenuButton4.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.Right;
            this.ribbonMenuButton4.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Top;
            this.ribbonMenuButton4.ImageOffset = 4;
            this.ribbonMenuButton4.IsPressed = false;
            this.ribbonMenuButton4.KeepPress = true;
            this.ribbonMenuButton4.Location = new System.Drawing.Point(118, 48);
            this.ribbonMenuButton4.MaxImageSize = new System.Drawing.Point(24, 24);
            this.ribbonMenuButton4.MenuPos = new System.Drawing.Point(0, 0);
            this.ribbonMenuButton4.Name = "ribbonMenuButton4";
            this.ribbonMenuButton4.Padding = new System.Windows.Forms.Padding(1);
            this.ribbonMenuButton4.Radius = 8;
            this.ribbonMenuButton4.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
            this.ribbonMenuButton4.SinglePressButton = false;
            this.ribbonMenuButton4.Size = new System.Drawing.Size(58, 29);
            this.ribbonMenuButton4.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
            this.ribbonMenuButton4.SplitDistance = 0;
            this.ribbonMenuButton4.TabIndex = 31;
            this.ribbonMenuButton4.Text = "\r\n";
            this.ribbonMenuButton4.Title = "";
            this.ribbonMenuButton4.UseVisualStyleBackColor = true;
            // 
            // fontUnderlineButton
            // 
            this.fontUnderlineButton.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
            this.fontUnderlineButton.BackColor = System.Drawing.Color.Transparent;
            this.fontUnderlineButton.Checked = false;
            this.fontUnderlineButton.ColorBase = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(209)))), ((int)(((byte)(240)))));
            this.fontUnderlineButton.ColorBaseStroke = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(187)))), ((int)(((byte)(213)))));
            this.fontUnderlineButton.ColorOn = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.fontUnderlineButton.ColorOnStroke = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.fontUnderlineButton.ColorPress = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.fontUnderlineButton.ColorPressStroke = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.fontUnderlineButton.FadingSpeed = 35;
            this.fontUnderlineButton.FlatAppearance.BorderSize = 0;
            this.fontUnderlineButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.fontUnderlineButton.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.Center;
            this.fontUnderlineButton.Image = global::DreamBeam.Properties.Resources.stock_text_underlined;
            this.fontUnderlineButton.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Top;
            this.fontUnderlineButton.ImageOffset = 4;
            this.fontUnderlineButton.IsPressed = true;
            this.fontUnderlineButton.KeepPress = true;
            this.fontUnderlineButton.Location = new System.Drawing.Point(77, 2);
            this.fontUnderlineButton.MaxImageSize = new System.Drawing.Point(24, 24);
            this.fontUnderlineButton.MenuPos = new System.Drawing.Point(0, 0);
            this.fontUnderlineButton.Name = "fontUnderlineButton";
            this.fontUnderlineButton.Padding = new System.Windows.Forms.Padding(1);
            this.fontUnderlineButton.Radius = 8;
            this.fontUnderlineButton.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
            this.fontUnderlineButton.SinglePressButton = true;
            this.fontUnderlineButton.Size = new System.Drawing.Size(38, 29);
            this.fontUnderlineButton.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
            this.fontUnderlineButton.SplitDistance = 0;
            this.fontUnderlineButton.TabIndex = 21;
            this.fontUnderlineButton.Text = "\r\n";
            this.fontUnderlineButton.Title = "";
            this.fontUnderlineButton.UseVisualStyleBackColor = true;
            // 
            // fontItalicButton
            // 
            this.fontItalicButton.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
            this.fontItalicButton.BackColor = System.Drawing.Color.Transparent;
            this.fontItalicButton.Checked = false;
            this.fontItalicButton.ColorBase = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(209)))), ((int)(((byte)(240)))));
            this.fontItalicButton.ColorBaseStroke = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(187)))), ((int)(((byte)(213)))));
            this.fontItalicButton.ColorOn = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.fontItalicButton.ColorOnStroke = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.fontItalicButton.ColorPress = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.fontItalicButton.ColorPressStroke = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.fontItalicButton.FadingSpeed = 35;
            this.fontItalicButton.FlatAppearance.BorderSize = 0;
            this.fontItalicButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.fontItalicButton.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.Center;
            this.fontItalicButton.Image = global::DreamBeam.Properties.Resources.stock_text_italic;
            this.fontItalicButton.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Top;
            this.fontItalicButton.ImageOffset = 4;
            this.fontItalicButton.IsPressed = false;
            this.fontItalicButton.KeepPress = true;
            this.fontItalicButton.Location = new System.Drawing.Point(40, 2);
            this.fontItalicButton.MaxImageSize = new System.Drawing.Point(24, 24);
            this.fontItalicButton.MenuPos = new System.Drawing.Point(0, 0);
            this.fontItalicButton.Name = "fontItalicButton";
            this.fontItalicButton.Padding = new System.Windows.Forms.Padding(1);
            this.fontItalicButton.Radius = 8;
            this.fontItalicButton.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
            this.fontItalicButton.SinglePressButton = true;
            this.fontItalicButton.Size = new System.Drawing.Size(38, 29);
            this.fontItalicButton.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
            this.fontItalicButton.SplitDistance = 0;
            this.fontItalicButton.TabIndex = 20;
            this.fontItalicButton.Text = "\r\n";
            this.fontItalicButton.Title = "";
            this.fontItalicButton.UseVisualStyleBackColor = true;
            // 
            // fontBoldButton
            // 
            this.fontBoldButton.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
            this.fontBoldButton.BackColor = System.Drawing.Color.Transparent;
            this.fontBoldButton.Checked = false;
            this.fontBoldButton.ColorBase = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(209)))), ((int)(((byte)(240)))));
            this.fontBoldButton.ColorBaseStroke = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(187)))), ((int)(((byte)(213)))));
            this.fontBoldButton.ColorOn = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.fontBoldButton.ColorOnStroke = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.fontBoldButton.ColorPress = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.fontBoldButton.ColorPressStroke = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.fontBoldButton.FadingSpeed = 35;
            this.fontBoldButton.FlatAppearance.BorderSize = 0;
            this.fontBoldButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.fontBoldButton.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.Left;
            this.fontBoldButton.Image = global::DreamBeam.Properties.Resources.stock_text_bold;
            this.fontBoldButton.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Top;
            this.fontBoldButton.ImageOffset = 4;
            this.fontBoldButton.IsPressed = false;
            this.fontBoldButton.KeepPress = true;
            this.fontBoldButton.Location = new System.Drawing.Point(2, 2);
            this.fontBoldButton.MaxImageSize = new System.Drawing.Point(24, 24);
            this.fontBoldButton.MenuPos = new System.Drawing.Point(0, 0);
            this.fontBoldButton.Name = "fontBoldButton";
            this.fontBoldButton.Radius = 6;
            this.fontBoldButton.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
            this.fontBoldButton.SinglePressButton = true;
            this.fontBoldButton.Size = new System.Drawing.Size(39, 29);
            this.fontBoldButton.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
            this.fontBoldButton.SplitDistance = 0;
            this.fontBoldButton.TabIndex = 19;
            this.fontBoldButton.Text = "\r\n";
            this.fontBoldButton.Title = "";
            this.fontBoldButton.UseVisualStyleBackColor = true;
            // 
            // ribbonMenuButton1
            // 
            this.ribbonMenuButton1.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
            this.ribbonMenuButton1.BackColor = System.Drawing.Color.Transparent;
            this.ribbonMenuButton1.Checked = false;
            this.ribbonMenuButton1.ColorBase = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(209)))), ((int)(((byte)(240)))));
            this.ribbonMenuButton1.ColorBaseStroke = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(187)))), ((int)(((byte)(213)))));
            this.ribbonMenuButton1.ColorOn = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.ribbonMenuButton1.ColorOnStroke = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.ribbonMenuButton1.ColorPress = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.ribbonMenuButton1.ColorPressStroke = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.ribbonMenuButton1.Enabled = false;
            this.ribbonMenuButton1.FadingSpeed = 35;
            this.ribbonMenuButton1.FlatAppearance.BorderSize = 0;
            this.ribbonMenuButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ribbonMenuButton1.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
            this.ribbonMenuButton1.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Top;
            this.ribbonMenuButton1.ImageOffset = 4;
            this.ribbonMenuButton1.IsPressed = false;
            this.ribbonMenuButton1.KeepPress = true;
            this.ribbonMenuButton1.Location = new System.Drawing.Point(5, 17);
            this.ribbonMenuButton1.MaxImageSize = new System.Drawing.Point(24, 24);
            this.ribbonMenuButton1.MenuPos = new System.Drawing.Point(0, 0);
            this.ribbonMenuButton1.Name = "ribbonMenuButton1";
            this.ribbonMenuButton1.Padding = new System.Windows.Forms.Padding(1);
            this.ribbonMenuButton1.Radius = 8;
            this.ribbonMenuButton1.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
            this.ribbonMenuButton1.SinglePressButton = false;
            this.ribbonMenuButton1.Size = new System.Drawing.Size(224, 29);
            this.ribbonMenuButton1.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.Yes;
            this.ribbonMenuButton1.SplitDistance = 0;
            this.ribbonMenuButton1.TabIndex = 28;
            this.ribbonMenuButton1.Text = "\r\n";
            this.ribbonMenuButton1.Title = "";
            this.ribbonMenuButton1.UseVisualStyleBackColor = true;
            // 
            // TextSizeIncreaseButton
            // 
            this.TextSizeIncreaseButton.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
            this.TextSizeIncreaseButton.BackColor = System.Drawing.Color.Transparent;
            this.TextSizeIncreaseButton.Checked = false;
            this.TextSizeIncreaseButton.ColorBase = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(209)))), ((int)(((byte)(240)))));
            this.TextSizeIncreaseButton.ColorBaseStroke = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(187)))), ((int)(((byte)(213)))));
            this.TextSizeIncreaseButton.ColorOn = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.TextSizeIncreaseButton.ColorOnStroke = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.TextSizeIncreaseButton.ColorPress = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.TextSizeIncreaseButton.ColorPressStroke = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.TextSizeIncreaseButton.FadingSpeed = 35;
            this.TextSizeIncreaseButton.FlatAppearance.BorderSize = 0;
            this.TextSizeIncreaseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TextSizeIncreaseButton.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.Left;
            this.TextSizeIncreaseButton.Image = global::DreamBeam.Properties.Resources.stock_increase_font_16;
            this.TextSizeIncreaseButton.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Top;
            this.TextSizeIncreaseButton.ImageOffset = 4;
            this.TextSizeIncreaseButton.IsPressed = false;
            this.TextSizeIncreaseButton.KeepPress = false;
            this.TextSizeIncreaseButton.Location = new System.Drawing.Point(178, 51);
            this.TextSizeIncreaseButton.MaxImageSize = new System.Drawing.Point(16, 16);
            this.TextSizeIncreaseButton.MenuPos = new System.Drawing.Point(0, 0);
            this.TextSizeIncreaseButton.Name = "TextSizeIncreaseButton";
            this.TextSizeIncreaseButton.Radius = 6;
            this.TextSizeIncreaseButton.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
            this.TextSizeIncreaseButton.SinglePressButton = false;
            this.TextSizeIncreaseButton.Size = new System.Drawing.Size(24, 24);
            this.TextSizeIncreaseButton.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
            this.TextSizeIncreaseButton.SplitDistance = 0;
            this.TextSizeIncreaseButton.TabIndex = 29;
            this.TextSizeIncreaseButton.Text = "\r\n";
            this.TextSizeIncreaseButton.Title = "";
            this.TextSizeIncreaseButton.UseVisualStyleBackColor = true;
            this.TextSizeIncreaseButton.Click += new System.EventHandler(this.TextSizeIncreaseButton_Click);
            // 
            // TextSizeDecreaseButton
            // 
            this.TextSizeDecreaseButton.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
            this.TextSizeDecreaseButton.BackColor = System.Drawing.Color.Transparent;
            this.TextSizeDecreaseButton.Checked = false;
            this.TextSizeDecreaseButton.ColorBase = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(209)))), ((int)(((byte)(240)))));
            this.TextSizeDecreaseButton.ColorBaseStroke = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(187)))), ((int)(((byte)(213)))));
            this.TextSizeDecreaseButton.ColorOn = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.TextSizeDecreaseButton.ColorOnStroke = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.TextSizeDecreaseButton.ColorPress = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.TextSizeDecreaseButton.ColorPressStroke = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.TextSizeDecreaseButton.FadingSpeed = 35;
            this.TextSizeDecreaseButton.FlatAppearance.BorderSize = 0;
            this.TextSizeDecreaseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TextSizeDecreaseButton.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.Right;
            this.TextSizeDecreaseButton.Image = global::DreamBeam.Properties.Resources.stock_decrease_font_16;
            this.TextSizeDecreaseButton.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Top;
            this.TextSizeDecreaseButton.ImageOffset = 4;
            this.TextSizeDecreaseButton.IsPressed = true;
            this.TextSizeDecreaseButton.KeepPress = false;
            this.TextSizeDecreaseButton.Location = new System.Drawing.Point(201, 51);
            this.TextSizeDecreaseButton.MaxImageSize = new System.Drawing.Point(16, 16);
            this.TextSizeDecreaseButton.MenuPos = new System.Drawing.Point(0, 0);
            this.TextSizeDecreaseButton.Name = "TextSizeDecreaseButton";
            this.TextSizeDecreaseButton.Padding = new System.Windows.Forms.Padding(1);
            this.TextSizeDecreaseButton.Radius = 8;
            this.TextSizeDecreaseButton.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
            this.TextSizeDecreaseButton.SinglePressButton = false;
            this.TextSizeDecreaseButton.Size = new System.Drawing.Size(24, 24);
            this.TextSizeDecreaseButton.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
            this.TextSizeDecreaseButton.SplitDistance = 0;
            this.TextSizeDecreaseButton.TabIndex = 30;
            this.TextSizeDecreaseButton.Text = "\r\n";
            this.TextSizeDecreaseButton.Title = "";
            this.TextSizeDecreaseButton.UseVisualStyleBackColor = true;
            this.TextSizeDecreaseButton.Click += new System.EventHandler(this.TextSizeDecreaseButton_Click);
            // 
            // TextFormatOptions
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.grouper3);
            this.Controls.Add(this.grouper2);
            this.Controls.Add(this.grouper1);
            this.Controls.Add(this.groupBox3);
            this.Name = "TextFormatOptions";
            this.Size = new System.Drawing.Size(245, 302);
            this.Load += new System.EventHandler(this.TextFormatOptions_Load);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Bounds4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bounds3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bounds2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bounds1)).EndInit();
            this.OutlineMenu.ResumeLayout(false);
            this.grouper3.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.grouper2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.OutlineSize)).EndInit();
            this.grouper1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private void FontButton_Click(object sender, System.EventArgs e) {
			try {
				if (this.fontDialog.ShowDialog() == DialogResult.OK) {
					this.font = this.fontDialog.Font;
                    this.Setfont();
                    NotifyControlChangeListeners();                    
				}
			} catch { }
		}

		private void TextColor_Click(object sender, System.EventArgs e) {
			try {
				this.colorDialog.Color = this.textColor;
				if (this.colorDialog.ShowDialog() == DialogResult.OK) {
					this.textColor = this.colorDialog.Color;
					NotifyControlChangeListeners();
				}
			} catch { }
		}

		private void OutlineColor_Click(object sender, System.EventArgs e) {
			try {
				this.colorDialog.Color = outlineColor;
				if (this.colorDialog.ShowDialog() == DialogResult.OK) {
					this.outlineColor = this.colorDialog.Color;
					NotifyControlChangeListeners();
				}
			} catch { }
		}

		// Declare the event, which is associated with our
		// delegate SubmitClickedHandler(). Add some attributes
		// for the Visual C# control property.
		[Category("Action")]
		[Description("Fires when a text format option setting is changed.")]
		public event ControlChangeHandler ControlChangedEvent;

		private void ControlChanged(Object sender, EventArgs e) {
			// If we're in the process of changing all the controls, wait until
			// they're all changed before firing the event.
			if (!changingControls) {                
				NotifyControlChangeListeners();
			}
		}

		protected void NotifyControlChangeListeners() {
			if (ControlChangedEvent != null) {
				ControlChangedEvent();
			}
		}

      

        private void fontComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {            
            NotifyControlChangeListeners();
        }

      



        private void TextSizeIncreaseButton_Click(object sender, EventArgs e)
        {
            if (this.Fontsize.SelectedIndex >= 0 & this.Fontsize.SelectedIndex < this.Fontsize.Items.Count - 1)
            {
                this.Fontsize.SelectedIndex++;
            }
        }

        private void TextSizeDecreaseButton_Click(object sender, EventArgs e)
        {
            if (this.Fontsize.SelectedIndex > 0 & this.Fontsize.SelectedIndex <= this.Fontsize.Items.Count - 1)
            {
                this.Fontsize.SelectedIndex--;
            }
        }

        private void NoOutlineItem_Click(object sender, EventArgs e)
        {
            this.TextOutlineMenuButton.Image = NoOutlineItem.Image;
            if (!changingControls)
            this.Effects = "Normal";
            NotifyControlChangeListeners();
        }

        private void FilledOutlineItem_Click(object sender, EventArgs e)
        {
            this.TextOutlineMenuButton.Image = FilledOutlineItem.Image;             
            this.Effects = "Filled Outline";
            NotifyControlChangeListeners();
            
        }

        private void ClearOutlineItem_Click(object sender, EventArgs e)
        {
            this.TextOutlineMenuButton.Image = ClearOutlineItem.Image;                
             this.Effects = "Outline";
             NotifyControlChangeListeners();
            
        }

        private void TextFormatOptions_Load(object sender, EventArgs e)
        {
            
        }

     

        

	}


	/// <summary>
	/// These are the settings we need to keep track of for most text pieces in DreamBeam.
	/// They correspond to the controls of the TextFormatOptions UserControl.
	/// </summary>
	[Serializable]
	public class BeamTextFormat : ICloneable {
		// XML can not serialize Color. We use a property to get around the problem.
		[XmlIgnore]
		public System.Drawing.Color TextColor = new System.Drawing.Color();
		[XmlIgnore]
		public System.Drawing.Color OutlineColor = new System.Drawing.Color();
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

		/* Just as the background image is scaled to cover the user's display resolution,
		 * the font needs to be scaled along with it. The font sizes specified by the user
		 * are assumed to be for a display of ReferenceResolutionH pixels high.
		 */
		[XmlIgnore()]
		public static int ReferenceResolutionH = 600;

		public BeamTextFormat() {
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
			//BeamTextFormat f = new BeamTextFormat();

			//// Start with a flat, memberwise copy
			//f = this.MemberwiseClone() as BeamTextFormat;

			//// Copy things that need special attention
			////f.FontFamily = (string)this.FontFamily.Clone();
			////f.Effects = (string)this.Effects.Clone();
			//return f;

			MemoryStream ms = new MemoryStream();
			BinaryFormatter bf = new BinaryFormatter();
			bf.Serialize(ms, this);
			ms.Position = 0;
			object clone = bf.Deserialize(ms);
			ms.Close();

			return (BeamTextFormat)clone;
		}

		/// <summary>
		/// This is not a true hash code. It is only used to determine if any
		/// graphically visible characteristics of the object have changed.
		/// </summary>
		/// <returns></returns>
		public virtual int VisibleHashCode() {
			int h = 0;
			h += TextColor.GetHashCode();
			h += OutlineColor.GetHashCode();
			h += OutlineSize;
			h += HAlignment.GetHashCode();
			h += VAlignment.GetHashCode();
			h += Bounds.GetHashCode();
			h += Effects.GetHashCode();
			h += FontFamily.GetHashCode();
			h += FontEmSize.GetHashCode();
			h += FontStyle.GetHashCode();
			return h;
		}


		#endregion
	}


}
