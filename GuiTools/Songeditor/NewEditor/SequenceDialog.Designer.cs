namespace DreamBeam
{
    partial class SequenceDialog
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SequenceDialog));
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.ListEx_Available = new Lister.ListEx();
            this.ListEx_Sequence = new Lister.ListEx();
            this.panel1 = new System.Windows.Forms.Panel();
            this.AutoSequence = new RibbonStyle.RibbonMenuButton();
            this.ListEx_ImageList = new System.Windows.Forms.ImageList(this.components);
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(12, 47);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(100, 16);
            this.label10.TabIndex = 7;
            this.label10.Text = "Lyrics sequence:";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(158, 47);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(100, 16);
            this.label9.TabIndex = 6;
            this.label9.Text = "Available lyrics:";
            // 
            // ListEx_Available
            // 
            this.ListEx_Available.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.ListEx_Available.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.ListEx_Available.Imgs = this.ListEx_ImageList;
            this.ListEx_Available.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(199)))), ((int)(((byte)(199)))));
            this.ListEx_Available.Location = new System.Drawing.Point(152, 65);
            this.ListEx_Available.Name = "ListEx_Available";
            this.ListEx_Available.ReadOnly = true;
            this.ListEx_Available.ShowBullets = true;
            this.ListEx_Available.Size = new System.Drawing.Size(120, 314);
            this.ListEx_Available.TabIndex = 5;
            this.ListEx_Available.PressIcon += new Lister.ListEx.EventHandler(this.ListEx_Available_PressIcon);
            // 
            // ListEx_Sequence
            // 
            this.ListEx_Sequence.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.ListEx_Sequence.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.ListEx_Sequence.Imgs = this.ListEx_ImageList;
            this.ListEx_Sequence.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(199)))), ((int)(((byte)(199)))));
            this.ListEx_Sequence.Location = new System.Drawing.Point(10, 65);
            this.ListEx_Sequence.Name = "ListEx_Sequence";
            this.ListEx_Sequence.ReadOnly = true;
            this.ListEx_Sequence.ShowBullets = true;
            this.ListEx_Sequence.Size = new System.Drawing.Size(120, 314);
            this.ListEx_Sequence.TabIndex = 4;
            this.ListEx_Sequence.PressIcon += new Lister.ListEx.EventHandler(this.ListEx_Sequence_PressIcon);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Controls.Add(this.AutoSequence);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.ListEx_Sequence);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.ListEx_Available);
            this.panel1.Location = new System.Drawing.Point(19, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(283, 388);
            this.panel1.TabIndex = 8;
            // 
            // AutoSequence
            // 
            this.AutoSequence.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
            this.AutoSequence.BackColor = System.Drawing.Color.Transparent;
            this.AutoSequence.Checked = false;
            this.AutoSequence.ColorBase = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(209)))), ((int)(((byte)(240)))));
            this.AutoSequence.ColorBaseStroke = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(187)))), ((int)(((byte)(213)))));
            this.AutoSequence.ColorOn = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.AutoSequence.ColorOnStroke = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.AutoSequence.ColorPress = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.AutoSequence.ColorPressStroke = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(58)))), ((int)(((byte)(67)))), ((int)(((byte)(76)))));
            this.AutoSequence.FadingSpeed = 35;
            this.AutoSequence.FlatAppearance.BorderSize = 0;
            this.AutoSequence.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AutoSequence.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
            this.AutoSequence.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.AutoSequence.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
            this.AutoSequence.ImageOffset = 2;
            this.AutoSequence.IsPressed = false;
            this.AutoSequence.KeepPress = true;
            this.AutoSequence.Location = new System.Drawing.Point(67, 10);
            this.AutoSequence.MaxImageSize = new System.Drawing.Point(24, 24);
            this.AutoSequence.MenuPos = new System.Drawing.Point(0, 0);
            this.AutoSequence.Name = "AutoSequence";
            this.AutoSequence.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.AutoSequence.Radius = 6;
            this.AutoSequence.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
            this.AutoSequence.SinglePressButton = true;
            this.AutoSequence.Size = new System.Drawing.Size(147, 29);
            this.AutoSequence.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
            this.AutoSequence.SplitDistance = 0;
            this.AutoSequence.TabIndex = 26;
            this.AutoSequence.Text = "         Auto Sequence";
            this.AutoSequence.Title = "";
            this.AutoSequence.UseVisualStyleBackColor = true;
            this.AutoSequence.Click += new System.EventHandler(this.AutoSequence_Click);
            // 
            // ListEx_ImageList
            // 
            this.ListEx_ImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ListEx_ImageList.ImageStream")));
            this.ListEx_ImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.ListEx_ImageList.Images.SetKeyName(0, "");
            this.ListEx_ImageList.Images.SetKeyName(1, "");
            // 
            // SequenceDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.OldLace;
            this.Controls.Add(this.panel1);
            this.Name = "SequenceDialog";
            this.Size = new System.Drawing.Size(323, 394);
            this.SizeChanged += new System.EventHandler(this.SequenceDialog_SizeChanged);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        public Lister.ListEx ListEx_Available;
        public Lister.ListEx ListEx_Sequence;
        private System.Windows.Forms.Panel panel1;
        public RibbonStyle.RibbonMenuButton AutoSequence;
        private System.Windows.Forms.ImageList ListEx_ImageList;
    }
}
