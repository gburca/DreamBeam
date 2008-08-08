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
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.ListEx_Available = new Lister.ListEx();
            this.ListEx_Sequence = new Lister.ListEx();
            this.SuspendLayout();
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(24, 24);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(100, 16);
            this.label10.TabIndex = 7;
            this.label10.Text = "Lyrics sequence:";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(170, 24);
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
            this.ListEx_Available.Imgs = null;
            this.ListEx_Available.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(199)))), ((int)(((byte)(199)))));
            this.ListEx_Available.Location = new System.Drawing.Point(168, 42);
            this.ListEx_Available.Name = "ListEx_Available";
            this.ListEx_Available.ReadOnly = true;
            this.ListEx_Available.ShowBullets = true;
            this.ListEx_Available.Size = new System.Drawing.Size(120, 324);
            this.ListEx_Available.TabIndex = 5;
            this.ListEx_Available.PressIcon += new Lister.ListEx.EventHandler(this.ListEx_Available_PressIcon);
            // 
            // ListEx_Sequence
            // 
            this.ListEx_Sequence.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.ListEx_Sequence.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.ListEx_Sequence.Imgs = null;
            this.ListEx_Sequence.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(199)))), ((int)(((byte)(199)))));
            this.ListEx_Sequence.Location = new System.Drawing.Point(22, 42);
            this.ListEx_Sequence.Name = "ListEx_Sequence";
            this.ListEx_Sequence.ReadOnly = true;
            this.ListEx_Sequence.ShowBullets = true;
            this.ListEx_Sequence.Size = new System.Drawing.Size(120, 324);
            this.ListEx_Sequence.TabIndex = 4;
            this.ListEx_Sequence.PressIcon += new Lister.ListEx.EventHandler(this.ListEx_Sequence_PressIcon);
            // 
            // SequenceDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.OldLace;
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.ListEx_Available);
            this.Controls.Add(this.ListEx_Sequence);
            this.Name = "SequenceDialog";
            this.Size = new System.Drawing.Size(316, 393);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        public Lister.ListEx ListEx_Available;
        public Lister.ListEx ListEx_Sequence;
    }
}
