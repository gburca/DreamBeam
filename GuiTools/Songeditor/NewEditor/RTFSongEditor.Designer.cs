namespace DreamBeam
{
    partial class RTFSongEditor
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
            this.rtfEditorPanel1 = new DreamBeam.RTFEditorPanel();
            this.SuspendLayout();
            // 
            // rtfEditorPanel1
            // 
            this.rtfEditorPanel1.AutoScroll = true;
            this.rtfEditorPanel1.Location = new System.Drawing.Point(3, 3);
            this.rtfEditorPanel1.Name = "rtfEditorPanel1";
            this.rtfEditorPanel1.Size = new System.Drawing.Size(494, 471);
            this.rtfEditorPanel1.TabIndex = 0;
            this.rtfEditorPanel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.rtfEditorPanel1_MouseDown);
            // 
            // RTFSongEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rtfEditorPanel1);
            this.Name = "RTFSongEditor";
            this.Size = new System.Drawing.Size(658, 477);
            this.ResumeLayout(false);

        }

        #endregion

        private RTFEditorPanel rtfEditorPanel1;
    }
}
