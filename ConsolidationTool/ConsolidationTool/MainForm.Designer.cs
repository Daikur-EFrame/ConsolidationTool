namespace ConsolidationTool
{
    partial class MainForm
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statuslbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.WatchedFolderlbl = new System.Windows.Forms.Label();
            this.watchedFoldertxtBox = new System.Windows.Forms.TextBox();
            this.BrowseFolderbtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statuslbl});
            this.statusStrip1.Location = new System.Drawing.Point(0, 107);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(490, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statuslbl
            // 
            this.statuslbl.Name = "statuslbl";
            this.statuslbl.Size = new System.Drawing.Size(157, 17);
            this.statuslbl.Text = "Waiting for watched folder...";
            // 
            // WatchedFolderlbl
            // 
            this.WatchedFolderlbl.AutoSize = true;
            this.WatchedFolderlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WatchedFolderlbl.Location = new System.Drawing.Point(12, 48);
            this.WatchedFolderlbl.Name = "WatchedFolderlbl";
            this.WatchedFolderlbl.Size = new System.Drawing.Size(133, 15);
            this.WatchedFolderlbl.TabIndex = 1;
            this.WatchedFolderlbl.Text = "Current watched folder:";
            // 
            // watchedFoldertxtBox
            // 
            this.watchedFoldertxtBox.Location = new System.Drawing.Point(12, 66);
            this.watchedFoldertxtBox.Name = "watchedFoldertxtBox";
            this.watchedFoldertxtBox.ReadOnly = true;
            this.watchedFoldertxtBox.Size = new System.Drawing.Size(321, 20);
            this.watchedFoldertxtBox.TabIndex = 2;
            // 
            // BrowseFolderbtn
            // 
            this.BrowseFolderbtn.Location = new System.Drawing.Point(339, 64);
            this.BrowseFolderbtn.Name = "BrowseFolderbtn";
            this.BrowseFolderbtn.Size = new System.Drawing.Size(92, 23);
            this.BrowseFolderbtn.TabIndex = 3;
            this.BrowseFolderbtn.Text = "Browse folder...";
            this.BrowseFolderbtn.UseVisualStyleBackColor = true;
            this.BrowseFolderbtn.Click += new System.EventHandler(this.BrowseFolderbtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Cooper Black", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(286, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(192, 21);
            this.label1.TabIndex = 4;
            this.label1.Text = "Consolidation Tool";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(490, 129);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BrowseFolderbtn);
            this.Controls.Add(this.watchedFoldertxtBox);
            this.Controls.Add(this.WatchedFolderlbl);
            this.Controls.Add(this.statusStrip1);
            this.Name = "MainForm";
            this.Text = "Consolidation Tool";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statuslbl;
        private System.Windows.Forms.Label WatchedFolderlbl;
        private System.Windows.Forms.TextBox watchedFoldertxtBox;
        private System.Windows.Forms.Button BrowseFolderbtn;
        private System.Windows.Forms.Label label1;
    }
}

