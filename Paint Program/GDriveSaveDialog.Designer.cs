namespace Paint_Program
{
    partial class GDriveSaveDialog
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
            this.lbl_FileName = new System.Windows.Forms.Label();
            this.lbl_FileType = new System.Windows.Forms.Label();
            this.tb_FileName = new System.Windows.Forms.TextBox();
            this.cb_FileType = new System.Windows.Forms.ComboBox();
            this.btn_save = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl_FileName
            // 
            this.lbl_FileName.AutoSize = true;
            this.lbl_FileName.Location = new System.Drawing.Point(75, 20);
            this.lbl_FileName.Name = "lbl_FileName";
            this.lbl_FileName.Size = new System.Drawing.Size(57, 13);
            this.lbl_FileName.TabIndex = 0;
            this.lbl_FileName.Text = "File Name:";
            // 
            // lbl_FileType
            // 
            this.lbl_FileType.AutoSize = true;
            this.lbl_FileType.Location = new System.Drawing.Point(82, 48);
            this.lbl_FileType.Name = "lbl_FileType";
            this.lbl_FileType.Size = new System.Drawing.Size(50, 13);
            this.lbl_FileType.TabIndex = 1;
            this.lbl_FileType.Text = "FileType:";
            // 
            // tb_FileName
            // 
            this.tb_FileName.Location = new System.Drawing.Point(138, 13);
            this.tb_FileName.Name = "tb_FileName";
            this.tb_FileName.Size = new System.Drawing.Size(241, 20);
            this.tb_FileName.TabIndex = 2;
            // 
            // cb_FileType
            // 
            this.cb_FileType.FormattingEnabled = true;
            this.cb_FileType.Items.AddRange(new object[] {
            "Jpeg | *.jpg",
            "PNG | *.png",
            "GIF | *.gif",
            "ICON | *.ico",
            "Animated GIF | *.gif",
            "TIFF | *.tiff",
            "Bitmap | *.bmp",
            "LePaint Project File | *.lep"});
            this.cb_FileType.Location = new System.Drawing.Point(138, 40);
            this.cb_FileType.Name = "cb_FileType";
            this.cb_FileType.Size = new System.Drawing.Size(241, 21);
            this.cb_FileType.TabIndex = 3;
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(138, 68);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(75, 23);
            this.btn_save.TabIndex = 4;
            this.btn_save.Text = "Save";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // GDriveSaveDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 101);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.cb_FileType);
            this.Controls.Add(this.tb_FileName);
            this.Controls.Add(this.lbl_FileType);
            this.Controls.Add(this.lbl_FileName);
            this.Name = "GDriveSaveDialog";
            this.Text = "Save to Google Drive";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GDriveSaveDialog_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_FileName;
        private System.Windows.Forms.Label lbl_FileType;
        private System.Windows.Forms.TextBox tb_FileName;
        private System.Windows.Forms.ComboBox cb_FileType;
        private System.Windows.Forms.Button btn_save;
    }
}