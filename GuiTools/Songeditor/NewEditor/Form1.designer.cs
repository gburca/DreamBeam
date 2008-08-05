namespace DreamBeam
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.rt1 = new System.Windows.Forms.RichTextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnLoadSource = new System.Windows.Forms.Button();
            this.btnShade = new System.Windows.Forms.Button();
            this.btnSaveSource = new System.Windows.Forms.Button();
            this.t1 = new System.Windows.Forms.TextBox();
            this.rt2 = new System.Windows.Forms.RichTextBox();
            this.btnUndo = new System.Windows.Forms.Button();
            this.btnShowRTF = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnTest2 = new System.Windows.Forms.Button();
            this.btnShadeSyntax = new System.Windows.Forms.Button();
            this.btnSyntax = new System.Windows.Forms.Button();
            this.btnTest1 = new System.Windows.Forms.Button();
            this.btnArrow = new System.Windows.Forms.Button();
            this.btnSaveClassList = new System.Windows.Forms.Button();
            this.btnSort = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.rt3 = new System.Windows.Forms.RichTextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rt1
            // 
            this.rt1.BackColor = System.Drawing.Color.White;
            this.rt1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rt1.EnableAutoDragDrop = true;
            this.rt1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rt1.ForeColor = System.Drawing.Color.Black;
            this.rt1.HideSelection = false;
            this.rt1.Location = new System.Drawing.Point(0, 0);
            this.rt1.Name = "rt1";
            this.rt1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rt1.ShowSelectionMargin = true;
            this.rt1.Size = new System.Drawing.Size(592, 450);
            this.rt1.TabIndex = 0;
            this.rt1.Text = "The quick brown fox jumps ...";
            this.rt1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.rt1_MouseDoubleClick_1);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnClose.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(454, 72);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(116, 24);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnLoadSource
            // 
            this.btnLoadSource.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnLoadSource.Enabled = false;
            this.btnLoadSource.Font = new System.Drawing.Font("Lucida Sans Unicode", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadSource.Location = new System.Drawing.Point(36, 16);
            this.btnLoadSource.Name = "btnLoadSource";
            this.btnLoadSource.Size = new System.Drawing.Size(106, 23);
            this.btnLoadSource.TabIndex = 4;
            this.btnLoadSource.Text = "Load Source";
            this.btnLoadSource.UseVisualStyleBackColor = false;
            this.btnLoadSource.Click += new System.EventHandler(this.btnLoadSource_Click);
            // 
            // btnShade
            // 
            this.btnShade.BackColor = System.Drawing.Color.Gainsboro;
            this.btnShade.Font = new System.Drawing.Font("Lucida Sans Unicode", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShade.Location = new System.Drawing.Point(160, 15);
            this.btnShade.Name = "btnShade";
            this.btnShade.Size = new System.Drawing.Size(116, 24);
            this.btnShade.TabIndex = 5;
            this.btnShade.Text = "Shade only";
            this.btnShade.UseVisualStyleBackColor = false;
            this.btnShade.Click += new System.EventHandler(this.btnShade_Click);
            // 
            // btnSaveSource
            // 
            this.btnSaveSource.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnSaveSource.Font = new System.Drawing.Font("Lucida Sans Unicode", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveSource.Location = new System.Drawing.Point(36, 43);
            this.btnSaveSource.Name = "btnSaveSource";
            this.btnSaveSource.Size = new System.Drawing.Size(106, 23);
            this.btnSaveSource.TabIndex = 7;
            this.btnSaveSource.Text = "Save Source";
            this.btnSaveSource.UseVisualStyleBackColor = false;
            this.btnSaveSource.Click += new System.EventHandler(this.btnSaveSource_Click);
            // 
            // t1
            // 
            this.t1.BackColor = System.Drawing.Color.SteelBlue;
            this.t1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.t1.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.t1.ForeColor = System.Drawing.Color.White;
            this.t1.Location = new System.Drawing.Point(60, 15);
            this.t1.Multiline = true;
            this.t1.Name = "t1";
            this.t1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.t1.Size = new System.Drawing.Size(520, 419);
            this.t1.TabIndex = 10;
            this.t1.Text = "t1";
            this.t1.Visible = false;
            // 
            // rt2
            // 
            this.rt2.BackColor = System.Drawing.Color.White;
            this.rt2.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rt2.Location = new System.Drawing.Point(22, 188);
            this.rt2.Name = "rt2";
            this.rt2.Size = new System.Drawing.Size(32, 76);
            this.rt2.TabIndex = 11;
            this.rt2.Text = "rt2";
            this.rt2.Visible = false;
            // 
            // btnUndo
            // 
            this.btnUndo.BackColor = System.Drawing.Color.Gainsboro;
            this.btnUndo.Font = new System.Drawing.Font("Lucida Sans Unicode", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUndo.Location = new System.Drawing.Point(294, 15);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(116, 24);
            this.btnUndo.TabIndex = 12;
            this.btnUndo.Text = "Un-format";
            this.btnUndo.UseVisualStyleBackColor = false;
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // btnShowRTF
            // 
            this.btnShowRTF.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnShowRTF.Font = new System.Drawing.Font("Lucida Sans Unicode", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowRTF.Location = new System.Drawing.Point(36, 73);
            this.btnShowRTF.Name = "btnShowRTF";
            this.btnShowRTF.Size = new System.Drawing.Size(106, 23);
            this.btnShowRTF.TabIndex = 14;
            this.btnShowRTF.Text = "Show/Hide RTF";
            this.btnShowRTF.UseVisualStyleBackColor = false;
            this.btnShowRTF.Click += new System.EventHandler(this.btnShowRTF_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gainsboro;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnTest2);
            this.panel1.Controls.Add(this.btnShadeSyntax);
            this.panel1.Controls.Add(this.btnSyntax);
            this.panel1.Controls.Add(this.btnTest1);
            this.panel1.Controls.Add(this.btnArrow);
            this.panel1.Controls.Add(this.btnShade);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.btnShowRTF);
            this.panel1.Controls.Add(this.btnLoadSource);
            this.panel1.Controls.Add(this.btnSaveSource);
            this.panel1.Controls.Add(this.btnUndo);
            this.panel1.Location = new System.Drawing.Point(0, 452);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(600, 113);
            this.panel1.TabIndex = 16;
            // 
            // btnTest2
            // 
            this.btnTest2.Location = new System.Drawing.Point(454, 44);
            this.btnTest2.Name = "btnTest2";
            this.btnTest2.Size = new System.Drawing.Size(42, 24);
            this.btnTest2.TabIndex = 24;
            this.btnTest2.Text = "T2";
            this.btnTest2.UseVisualStyleBackColor = true;
            this.btnTest2.Click += new System.EventHandler(this.btnTest2_Click);
            // 
            // btnShadeSyntax
            // 
            this.btnShadeSyntax.BackColor = System.Drawing.Color.Gainsboro;
            this.btnShadeSyntax.Font = new System.Drawing.Font("Lucida Sans Unicode", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShadeSyntax.Location = new System.Drawing.Point(160, 72);
            this.btnShadeSyntax.Name = "btnShadeSyntax";
            this.btnShadeSyntax.Size = new System.Drawing.Size(116, 24);
            this.btnShadeSyntax.TabIndex = 22;
            this.btnShadeSyntax.Text = "Shade and Syntax";
            this.btnShadeSyntax.UseVisualStyleBackColor = false;
            this.btnShadeSyntax.Click += new System.EventHandler(this.btnShadeSyntax_Click);
            // 
            // btnSyntax
            // 
            this.btnSyntax.BackColor = System.Drawing.Color.Gainsboro;
            this.btnSyntax.Font = new System.Drawing.Font("Lucida Sans Unicode", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSyntax.Location = new System.Drawing.Point(160, 43);
            this.btnSyntax.Name = "btnSyntax";
            this.btnSyntax.Size = new System.Drawing.Size(116, 24);
            this.btnSyntax.TabIndex = 21;
            this.btnSyntax.Text = "Syntax only";
            this.btnSyntax.UseVisualStyleBackColor = false;
            this.btnSyntax.Click += new System.EventHandler(this.btnSyntax_Click);
            // 
            // btnTest1
            // 
            this.btnTest1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnTest1.Location = new System.Drawing.Point(454, 15);
            this.btnTest1.Name = "btnTest1";
            this.btnTest1.Size = new System.Drawing.Size(42, 24);
            this.btnTest1.TabIndex = 19;
            this.btnTest1.Text = "T1";
            this.btnTest1.UseVisualStyleBackColor = false;
            this.btnTest1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnArrow
            // 
            this.btnArrow.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnArrow.Font = new System.Drawing.Font("Wingdings 3", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.btnArrow.Location = new System.Drawing.Point(5, 73);
            this.btnArrow.Name = "btnArrow";
            this.btnArrow.Size = new System.Drawing.Size(26, 23);
            this.btnArrow.TabIndex = 18;
            this.btnArrow.Text = "6";
            this.btnArrow.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnArrow.UseVisualStyleBackColor = false;
            this.btnArrow.Click += new System.EventHandler(this.btnArrow_Click);
            // 
            // btnSaveClassList
            // 
            this.btnSaveClassList.BackColor = System.Drawing.Color.Gainsboro;
            this.btnSaveClassList.Font = new System.Drawing.Font("Lucida Sans Unicode", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveClassList.Location = new System.Drawing.Point(5, 679);
            this.btnSaveClassList.Name = "btnSaveClassList";
            this.btnSaveClassList.Size = new System.Drawing.Size(168, 28);
            this.btnSaveClassList.TabIndex = 23;
            this.btnSaveClassList.Text = "   Save Class List";
            this.btnSaveClassList.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSaveClassList.UseVisualStyleBackColor = false;
            this.btnSaveClassList.Click += new System.EventHandler(this.btnSaveClassList_Click);
            // 
            // btnSort
            // 
            this.btnSort.BackColor = System.Drawing.Color.Gainsboro;
            this.btnSort.Font = new System.Drawing.Font("Lucida Sans Unicode", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSort.Location = new System.Drawing.Point(5, 644);
            this.btnSort.Name = "btnSort";
            this.btnSort.Size = new System.Drawing.Size(168, 28);
            this.btnSort.TabIndex = 25;
            this.btnSort.Text = "   Sort and Save  Class List";
            this.btnSort.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSort.UseVisualStyleBackColor = false;
            this.btnSort.Click += new System.EventHandler(this.btnSort_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Lucida Sans Unicode", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 593);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 15);
            this.label1.TabIndex = 26;
            this.label1.Text = "Class List can be hand edited.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Lucida Sans Unicode", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 608);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 15);
            this.label2.TabIndex = 27;
            this.label2.Text = "Insert at least one space";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Lucida Sans Unicode", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(8, 623);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 15);
            this.label3.TabIndex = 28;
            this.label3.Text = "between entries.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(77, 574);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 15);
            this.label4.TabIndex = 29;
            this.label4.Text = "Class List";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Wingdings", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.label5.Location = new System.Drawing.Point(148, 574);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(27, 21);
            this.label5.TabIndex = 30;
            this.label5.Text = "ð";
            // 
            // rt3
            // 
            this.rt3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rt3.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rt3.Location = new System.Drawing.Point(182, 574);
            this.rt3.Name = "rt3";
            this.rt3.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rt3.Size = new System.Drawing.Size(404, 133);
            this.rt3.TabIndex = 31;
            this.rt3.Text = "";
            this.rt3.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.rt3_MouseDoubleClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(592, 723);
            this.Controls.Add(this.rt3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSort);
            this.Controls.Add(this.btnSaveClassList);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.rt2);
            this.Controls.Add(this.t1);
            this.Controls.Add(this.rt1);
            this.Location = new System.Drawing.Point(500, 200);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "  RTF Parsing";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rt1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnLoadSource;
        private System.Windows.Forms.Button btnShade;
        private System.Windows.Forms.Button btnSaveSource;
        private System.Windows.Forms.TextBox t1;
        private System.Windows.Forms.RichTextBox rt2;
        private System.Windows.Forms.Button btnUndo;
        private System.Windows.Forms.Button btnShowRTF;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSaveClassList;
        private System.Windows.Forms.Button btnArrow;
        private System.Windows.Forms.Button btnTest1;
        private System.Windows.Forms.Button btnSort;
        private System.Windows.Forms.Button btnShadeSyntax;
        private System.Windows.Forms.Button btnSyntax;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnTest2;
        private System.Windows.Forms.RichTextBox rt3;
    }
}

