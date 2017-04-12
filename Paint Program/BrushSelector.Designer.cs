namespace Paint_Program
{
    partial class BrushSelector
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tsBrushes = new System.Windows.Forms.ToolStrip();
            this.SuspendLayout();
            // 
            // tsBrushes
            // 
            this.tsBrushes.Location = new System.Drawing.Point(0, 0);
            this.tsBrushes.Name = "tsBrushes";
            this.tsBrushes.Size = new System.Drawing.Size(250, 25);
            this.tsBrushes.TabIndex = 0;
            this.tsBrushes.Text = "tsBrushes";
            // 
            // BrushSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tsBrushes);
            this.Name = "BrushSelector";
            this.Size = new System.Drawing.Size(250, 100);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsBrushes;
    }
}
