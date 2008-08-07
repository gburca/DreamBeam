namespace DreamBeam{

    partial class ColoredTextBoxPanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ColoredTextBoxPanel));
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.asdfToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chorusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.otherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Scrollpanel = new CustomAutoScrollPanel.ScrollablePanel();
            this.ButtonPanel = new System.Windows.Forms.Panel();
            this.rtePanel = new System.Windows.Forms.Panel();
            this.rte = new DreamBeam.RicherTextBox2();
            this.contextMenuStrip.SuspendLayout();
            this.Scrollpanel.SuspendLayout();
            this.rtePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.asdfToolStripMenuItem,
            this.chorusToolStripMenuItem,
            this.otherToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(113, 70);
            // 
            // asdfToolStripMenuItem
            // 
            this.asdfToolStripMenuItem.Name = "asdfToolStripMenuItem";
            this.asdfToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.asdfToolStripMenuItem.Text = "Verse";
            this.asdfToolStripMenuItem.Click += new System.EventHandler(this.asdfToolStripMenuItem_Click);
            // 
            // chorusToolStripMenuItem
            // 
            this.chorusToolStripMenuItem.Name = "chorusToolStripMenuItem";
            this.chorusToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.chorusToolStripMenuItem.Text = "Chorus";
            this.chorusToolStripMenuItem.Click += new System.EventHandler(this.chorusToolStripMenuItem_Click);
            // 
            // otherToolStripMenuItem
            // 
            this.otherToolStripMenuItem.Name = "otherToolStripMenuItem";
            this.otherToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.otherToolStripMenuItem.Text = "Other";
            this.otherToolStripMenuItem.Click += new System.EventHandler(this.otherToolStripMenuItem_Click);
            // 
            // Scrollpanel
            // 
            this.Scrollpanel.AutoScroll = true;
            this.Scrollpanel.AutoScrollHorizontalMaximum = 100;
            this.Scrollpanel.AutoScrollHorizontalMinimum = 100;
            this.Scrollpanel.AutoScrollHPos = 0;
            this.Scrollpanel.AutoScrollVerticalMaximum = 100;
            this.Scrollpanel.AutoScrollVerticalMinimum = 0;
            this.Scrollpanel.AutoScrollVPos = 100;
            this.Scrollpanel.BackColor = System.Drawing.SystemColors.Control;
            this.Scrollpanel.Controls.Add(this.ButtonPanel);
            this.Scrollpanel.Controls.Add(this.rtePanel);
            this.Scrollpanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Scrollpanel.EnableAutoScrollHorizontal = false;
            this.Scrollpanel.EnableAutoScrollVertical = true;
            this.Scrollpanel.Location = new System.Drawing.Point(0, 0);
            this.Scrollpanel.Name = "Scrollpanel";
            this.Scrollpanel.Size = new System.Drawing.Size(621, 464);
            this.Scrollpanel.TabIndex = 3;
            this.Scrollpanel.VisibleAutoScrollHorizontal = false;
            this.Scrollpanel.VisibleAutoScrollVertical = false;
            // 
            // ButtonPanel
            // 
            this.ButtonPanel.BackColor = System.Drawing.SystemColors.Control;
            this.ButtonPanel.Location = new System.Drawing.Point(571, 0);
            this.ButtonPanel.Name = "ButtonPanel";
            this.ButtonPanel.Size = new System.Drawing.Size(50, 464);
            this.ButtonPanel.TabIndex = 1;
            // 
            // rtePanel
            // 
            this.rtePanel.BackColor = System.Drawing.Color.White;
            this.rtePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtePanel.Controls.Add(this.rte);
            this.rtePanel.Location = new System.Drawing.Point(0, 0);
            this.rtePanel.Name = "rtePanel";
            this.rtePanel.Padding = new System.Windows.Forms.Padding(1);
            this.rtePanel.Size = new System.Drawing.Size(568, 403);
            this.rtePanel.TabIndex = 2;
            // 
            // rte
            // 
            this.rte.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rte.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rte.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rte.Location = new System.Drawing.Point(1, 1);
            this.rte.Name = "rte";
            this.rte.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Horizontal;
            this.rte.Size = new System.Drawing.Size(564, 399);
            this.rte.TabIndex = 3;
            this.rte.Text = resources.GetString("rte.Text");
            this.rte.KeyUp += new System.Windows.Forms.KeyEventHandler(this.rte_KeyUp);
            this.rte.TextChanged += new System.EventHandler(this.rte_TextChanged);
            // 
            // ColoredTextBoxPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.Scrollpanel);
            this.Name = "ColoredTextBoxPanel";
            this.Size = new System.Drawing.Size(621, 464);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ColoredTextBoxPanel_Paint);
            this.Resize += new System.EventHandler(this.ColoredTextBoxPanel_Resize);
            this.contextMenuStrip.ResumeLayout(false);
            this.Scrollpanel.ResumeLayout(false);
            this.rtePanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel ButtonPanel;
        private System.Windows.Forms.Panel rtePanel;
        private RicherTextBox2 rte;
        private CustomAutoScrollPanel.ScrollablePanel Scrollpanel;
        

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem asdfToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chorusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem otherToolStripMenuItem;
        
    }
}
