namespace DreamBeam
{
    partial class TestForm
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

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.coloredTextBoxPanel1 = new DreamBeam.ColoredTextBoxPanel();
            this.SuspendLayout();
            // 
            // coloredTextBoxPanel1
            // 
            this.coloredTextBoxPanel1.Location = new System.Drawing.Point(40, 12);
            this.coloredTextBoxPanel1.Name = "coloredTextBoxPanel1";
            this.coloredTextBoxPanel1.Size = new System.Drawing.Size(621, 464);
            this.coloredTextBoxPanel1.TabIndex = 0;
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(786, 519);
            this.Controls.Add(this.coloredTextBoxPanel1);
            this.Name = "TestForm";
            this.Text = "TestForm";
            this.ResumeLayout(false);

        }

        #endregion

        private ColoredTextBoxPanel coloredTextBoxPanel1;


    }
}