namespace Paint_Program
{
    partial class ZoomControl
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
            this.lZoom = new System.Windows.Forms.Label();
            this.tbZoom = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lZoom
            // 
            this.lZoom.AutoSize = true;
            this.lZoom.Location = new System.Drawing.Point(23, 23);
            this.lZoom.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lZoom.Name = "lZoom";
            this.lZoom.Size = new System.Drawing.Size(43, 13);
            this.lZoom.TabIndex = 1;
            this.lZoom.Text = "Zoom : ";
            this.lZoom.Click += new System.EventHandler(this.label1_Click);
            // 
            // tbZoom
            // 
            this.tbZoom.Location = new System.Drawing.Point(90, 21);
            this.tbZoom.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbZoom.Name = "tbZoom";
            this.tbZoom.Size = new System.Drawing.Size(53, 20);
            this.tbZoom.TabIndex = 2;
            this.tbZoom.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbZoom.WordWrap = false;
            this.tbZoom.TextChanged += new System.EventHandler(this.tbZoom_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(146, 23);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "%";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // ZoomControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbZoom);
            this.Controls.Add(this.lZoom);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "ZoomControl";
            this.Size = new System.Drawing.Size(202, 58);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lZoom;
        private System.Windows.Forms.TextBox tbZoom;
        private System.Windows.Forms.Label label2;
    }
}
