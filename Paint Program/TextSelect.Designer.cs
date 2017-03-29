namespace Paint_Program
{
    partial class TextSelect
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
            this.tSelect = new System.Windows.Forms.Button();
            this.userText = new System.Windows.Forms.TextBox();
            this.fontSize = new System.Windows.Forms.TextBox();
            this.Fontbox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // tSelect
            // 
            this.tSelect.BackColor = System.Drawing.Color.Green;
            this.tSelect.Location = new System.Drawing.Point(299, 12);
            this.tSelect.Name = "tSelect";
            this.tSelect.Size = new System.Drawing.Size(32, 23);
            this.tSelect.TabIndex = 0;
            this.tSelect.Text = "OK";
            this.tSelect.UseVisualStyleBackColor = false;
            this.tSelect.Click += new System.EventHandler(this.tSelect_Click);
            // 
            // userText
            // 
            this.userText.Location = new System.Drawing.Point(13, 13);
            this.userText.Name = "userText";
            this.userText.Size = new System.Drawing.Size(100, 20);
            this.userText.TabIndex = 1;
            // 
            // fontSize
            // 
            this.fontSize.Location = new System.Drawing.Point(120, 13);
            this.fontSize.Name = "fontSize";
            this.fontSize.Size = new System.Drawing.Size(46, 20);
            this.fontSize.TabIndex = 2;
            this.fontSize.Text = "14";
            // 
            // Fontbox
            // 
            this.Fontbox.FormattingEnabled = true;
            this.Fontbox.Location = new System.Drawing.Point(172, 12);
            this.Fontbox.Name = "Fontbox";
            this.Fontbox.Size = new System.Drawing.Size(121, 21);
            this.Fontbox.TabIndex = 3;
            // 
            // TextSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(337, 45);
            this.Controls.Add(this.Fontbox);
            this.Controls.Add(this.fontSize);
            this.Controls.Add(this.userText);
            this.Controls.Add(this.tSelect);
            this.Name = "TextSelect";
            this.Text = "TextSelect";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button tSelect;
        private System.Windows.Forms.TextBox userText;
        private System.Windows.Forms.TextBox fontSize;
        private System.Windows.Forms.ComboBox Fontbox;
    }
}
