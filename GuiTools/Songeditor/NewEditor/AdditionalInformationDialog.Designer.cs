namespace DreamBeam
{
    partial class AdditionalInformationDialog
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.MinorKey = new System.Windows.Forms.CheckBox();
            this.KeyRangeHigh = new System.Windows.Forms.ComboBox();
            this.KeyRangeLow = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Notes = new System.Windows.Forms.TextBox();
            this.Number = new System.Windows.Forms.TextBox();
            this.Author = new System.Windows.Forms.TextBox();
            this.Collection = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.MinorKey);
            this.panel1.Controls.Add(this.KeyRangeHigh);
            this.panel1.Controls.Add(this.KeyRangeLow);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.Notes);
            this.panel1.Controls.Add(this.Number);
            this.panel1.Controls.Add(this.Author);
            this.panel1.Controls.Add(this.Collection);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(348, 348);
            this.panel1.TabIndex = 0;
            // 
            // MinorKey
            // 
            this.MinorKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.MinorKey.Location = new System.Drawing.Point(255, 288);
            this.MinorKey.Name = "MinorKey";
            this.MinorKey.Size = new System.Drawing.Size(64, 24);
            this.MinorKey.TabIndex = 48;
            this.MinorKey.Text = "Minor";
            // 
            // KeyRangeHigh
            // 
            this.KeyRangeHigh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.KeyRangeHigh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.KeyRangeHigh.ItemHeight = 13;
            this.KeyRangeHigh.Items.AddRange(new object[] {
            "",
            "A",
            "A#",
            "Bb",
            "B",
            "C",
            "C#",
            "Db",
            "D",
            "D#",
            "Eb",
            "E",
            "F",
            "F#",
            "Gb",
            "G",
            "G#",
            "Ab"});
            this.KeyRangeHigh.Location = new System.Drawing.Point(173, 288);
            this.KeyRangeHigh.MaxDropDownItems = 13;
            this.KeyRangeHigh.Name = "KeyRangeHigh";
            this.KeyRangeHigh.Size = new System.Drawing.Size(74, 21);
            this.KeyRangeHigh.TabIndex = 47;
            // 
            // KeyRangeLow
            // 
            this.KeyRangeLow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.KeyRangeLow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.KeyRangeLow.ItemHeight = 13;
            this.KeyRangeLow.Items.AddRange(new object[] {
            "",
            "A",
            "A#",
            "Bb",
            "B",
            "C",
            "C#",
            "Db",
            "D",
            "D#",
            "Eb",
            "E",
            "F",
            "F#",
            "Gb",
            "G",
            "G#",
            "Ab"});
            this.KeyRangeLow.Location = new System.Drawing.Point(87, 288);
            this.KeyRangeLow.MaxDropDownItems = 13;
            this.KeyRangeLow.Name = "KeyRangeLow";
            this.KeyRangeLow.Size = new System.Drawing.Size(76, 21);
            this.KeyRangeLow.TabIndex = 46;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.Location = new System.Drawing.Point(7, 292);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 16);
            this.label8.TabIndex = 53;
            this.label8.Text = "Key range:";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(7, 89);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 16);
            this.label6.TabIndex = 52;
            this.label6.Text = "Notes:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(7, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 16);
            this.label5.TabIndex = 51;
            this.label5.Text = "Number";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(7, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 16);
            this.label4.TabIndex = 50;
            this.label4.Text = "Collection:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(7, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 16);
            this.label3.TabIndex = 49;
            this.label3.Text = "Author:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Notes
            // 
            this.Notes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Notes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Notes.Location = new System.Drawing.Point(87, 87);
            this.Notes.Multiline = true;
            this.Notes.Name = "Notes";
            this.Notes.Size = new System.Drawing.Size(247, 195);
            this.Notes.TabIndex = 45;
            // 
            // Number
            // 
            this.Number.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Number.Location = new System.Drawing.Point(87, 61);
            this.Number.Name = "Number";
            this.Number.Size = new System.Drawing.Size(68, 20);
            this.Number.TabIndex = 44;
            // 
            // Author
            // 
            this.Author.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Author.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Author.Location = new System.Drawing.Point(87, 7);
            this.Author.Name = "Author";
            this.Author.Size = new System.Drawing.Size(243, 20);
            this.Author.TabIndex = 42;
            // 
            // Collection
            // 
            this.Collection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Collection.Location = new System.Drawing.Point(87, 33);
            this.Collection.Name = "Collection";
            this.Collection.Size = new System.Drawing.Size(243, 21);
            this.Collection.TabIndex = 43;
            // 
            // AdditionalInformationDialog
            // 
            this.BackColor = System.Drawing.Color.SeaShell;
            this.Controls.Add(this.panel1);
            this.Name = "AdditionalInformationDialog";
            this.Size = new System.Drawing.Size(348, 348);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox MinorKey;
        private System.Windows.Forms.ComboBox KeyRangeHigh;
        private System.Windows.Forms.ComboBox KeyRangeLow;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Notes;
        private System.Windows.Forms.TextBox Number;
        private System.Windows.Forms.TextBox Author;
        private System.Windows.Forms.ComboBox Collection;
    }
}
