namespace Paint_Program
{
    partial class NewProjectForm
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
            this.lWidth = new System.Windows.Forms.Label();
            this.lHeight = new System.Windows.Forms.Label();
            this.tbWidth = new System.Windows.Forms.TextBox();
            this.tbHeight = new System.Windows.Forms.TextBox();
            this.bSubmit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lWidth
            // 
            this.lWidth.AutoSize = true;
            this.lWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.lWidth.Location = new System.Drawing.Point(51, 38);
            this.lWidth.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lWidth.Name = "lWidth";
            this.lWidth.Size = new System.Drawing.Size(63, 24);
            this.lWidth.TabIndex = 0;
            this.lWidth.Text = "Width:";
            // 
            // lHeight
            // 
            this.lHeight.AutoSize = true;
            this.lHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.lHeight.Location = new System.Drawing.Point(45, 93);
            this.lHeight.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lHeight.Name = "lHeight";
            this.lHeight.Size = new System.Drawing.Size(70, 24);
            this.lHeight.TabIndex = 1;
            this.lHeight.Text = "Height:";
            // 
            // tbWidth
            // 
            this.tbWidth.Location = new System.Drawing.Point(134, 42);
            this.tbWidth.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbWidth.Name = "tbWidth";
            this.tbWidth.Size = new System.Drawing.Size(105, 20);
            this.tbWidth.TabIndex = 2;
            this.tbWidth.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Handle_KeyPress);
            // 
            // tbHeight
            // 
            this.tbHeight.Location = new System.Drawing.Point(134, 97);
            this.tbHeight.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbHeight.Name = "tbHeight";
            this.tbHeight.Size = new System.Drawing.Size(105, 20);
            this.tbHeight.TabIndex = 3;
            this.tbHeight.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Handle_KeyPress);
            // 
            // bSubmit
            // 
            this.bSubmit.Location = new System.Drawing.Point(116, 142);
            this.bSubmit.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.bSubmit.Name = "bSubmit";
            this.bSubmit.Size = new System.Drawing.Size(69, 29);
            this.bSubmit.TabIndex = 4;
            this.bSubmit.Text = "Submit";
            this.bSubmit.UseVisualStyleBackColor = true;
            this.bSubmit.Click += new System.EventHandler(this.bSubmit_Click);
            // 
            // NewProjectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 192);
            this.Controls.Add(this.bSubmit);
            this.Controls.Add(this.tbHeight);
            this.Controls.Add(this.tbWidth);
            this.Controls.Add(this.lHeight);
            this.Controls.Add(this.lWidth);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "NewProjectForm";
            this.Text = "NewProjectForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lWidth;
        private System.Windows.Forms.Label lHeight;
        private System.Windows.Forms.TextBox tbWidth;
        private System.Windows.Forms.TextBox tbHeight;
        private System.Windows.Forms.Button bSubmit;
    }
}