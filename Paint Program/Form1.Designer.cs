namespace Paint_Program
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.msMenu = new System.Windows.Forms.MenuStrip();
            this.tsmiFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFile_New = new System.Windows.Forms.ToolStripMenuItem();
            this.saveImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aspectRatioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gridLinesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiGrid5 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiGrid10 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiGrid25 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiGrid50 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiGrid100 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiGridCustom = new System.Windows.Forms.ToolStripTextBox();
            this.tabletModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateTimer = new System.Windows.Forms.Timer(this.components);
            this.msMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // msMenu
            // 
            this.msMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.msMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFile,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.msMenu.Location = new System.Drawing.Point(0, 0);
            this.msMenu.Name = "msMenu";
            this.msMenu.Padding = new System.Windows.Forms.Padding(4, 1, 0, 1);
            this.msMenu.Size = new System.Drawing.Size(884, 24);
            this.msMenu.TabIndex = 1;
            this.msMenu.Text = "menuStrip1";
            // 
            // tsmiFile
            // 
            this.tsmiFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFile_New,
            this.saveImageToolStripMenuItem,
            this.loadToolStripMenuItem,
            this.importImageToolStripMenuItem,
            this.exportImageToolStripMenuItem});
            this.tsmiFile.Name = "tsmiFile";
            this.tsmiFile.Size = new System.Drawing.Size(37, 22);
            this.tsmiFile.Text = "File";
            // 
            // tsmiFile_New
            // 
            this.tsmiFile_New.Name = "tsmiFile_New";
            this.tsmiFile_New.Size = new System.Drawing.Size(146, 22);
            this.tsmiFile_New.Text = "New";
            this.tsmiFile_New.Click += new System.EventHandler(this.tsmiFile_New_Click);
            // 
            // saveImageToolStripMenuItem
            // 
            this.saveImageToolStripMenuItem.Name = "saveImageToolStripMenuItem";
            this.saveImageToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.saveImageToolStripMenuItem.Text = "Save";
            this.saveImageToolStripMenuItem.Click += new System.EventHandler(this.tsmiFile_Save_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // importImageToolStripMenuItem
            // 
            this.importImageToolStripMenuItem.Name = "importImageToolStripMenuItem";
            this.importImageToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.importImageToolStripMenuItem.Text = "Import Image";
            this.importImageToolStripMenuItem.Click += new System.EventHandler(this.tsmiFile_Import_Click);
            // 
            // exportImageToolStripMenuItem
            // 
            this.exportImageToolStripMenuItem.Name = "exportImageToolStripMenuItem";
            this.exportImageToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.exportImageToolStripMenuItem.Text = "Export Image";
            this.exportImageToolStripMenuItem.Click += new System.EventHandler(this.tsmiFile_Export_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.aspectRatioToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 22);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.undoToolStripMenuItem.Text = "Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.tsmiEdit_Undo_Click);
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.redoToolStripMenuItem.Text = "Redo";
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.tsmiEdit_Redo_Click);
            // 
            // aspectRatioToolStripMenuItem
            // 
            this.aspectRatioToolStripMenuItem.Name = "aspectRatioToolStripMenuItem";
            this.aspectRatioToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.aspectRatioToolStripMenuItem.Text = "Image Size";
            this.aspectRatioToolStripMenuItem.Click += new System.EventHandler(this.tsmiEdit_ImageSize_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gridLinesToolStripMenuItem,
            this.tabletModeToolStripMenuItem,
            this.showToolsToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 22);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // gridLinesToolStripMenuItem
            // 
            this.gridLinesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiGrid5,
            this.tsmiGrid10,
            this.tsmiGrid25,
            this.tsmiGrid50,
            this.tsmiGrid100,
            this.tsmiGridCustom});
            this.gridLinesToolStripMenuItem.Name = "gridLinesToolStripMenuItem";
            this.gridLinesToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.gridLinesToolStripMenuItem.Text = "Toggle Grid Lines";
            // 
            // tsmiGrid5
            // 
            this.tsmiGrid5.CheckOnClick = true;
            this.tsmiGrid5.Name = "tsmiGrid5";
            this.tsmiGrid5.Size = new System.Drawing.Size(240, 22);
            this.tsmiGrid5.Text = "5";
            this.tsmiGrid5.Click += new System.EventHandler(this.tsmiGrid5_Click);
            // 
            // tsmiGrid10
            // 
            this.tsmiGrid10.CheckOnClick = true;
            this.tsmiGrid10.Name = "tsmiGrid10";
            this.tsmiGrid10.Size = new System.Drawing.Size(240, 22);
            this.tsmiGrid10.Text = "10";
            this.tsmiGrid10.Click += new System.EventHandler(this.tsmiGrid10_Click);
            // 
            // tsmiGrid25
            // 
            this.tsmiGrid25.CheckOnClick = true;
            this.tsmiGrid25.Name = "tsmiGrid25";
            this.tsmiGrid25.Size = new System.Drawing.Size(240, 22);
            this.tsmiGrid25.Text = "25";
            this.tsmiGrid25.Click += new System.EventHandler(this.tsmiGrid25_Click);
            // 
            // tsmiGrid50
            // 
            this.tsmiGrid50.CheckOnClick = true;
            this.tsmiGrid50.Name = "tsmiGrid50";
            this.tsmiGrid50.Size = new System.Drawing.Size(240, 22);
            this.tsmiGrid50.Text = "50";
            this.tsmiGrid50.Click += new System.EventHandler(this.tsmiGrid50_Click);
            // 
            // tsmiGrid100
            // 
            this.tsmiGrid100.CheckOnClick = true;
            this.tsmiGrid100.Name = "tsmiGrid100";
            this.tsmiGrid100.Size = new System.Drawing.Size(240, 22);
            this.tsmiGrid100.Text = "100";
            this.tsmiGrid100.Click += new System.EventHandler(this.tsmiGrid100_Click);
            // 
            // tsmiGridCustom
            // 
            this.tsmiGridCustom.Name = "tsmiGridCustom";
            this.tsmiGridCustom.Size = new System.Drawing.Size(180, 23);
            this.tsmiGridCustom.Text = "Custom";
            // 
            // tabletModeToolStripMenuItem
            // 
            this.tabletModeToolStripMenuItem.Name = "tabletModeToolStripMenuItem";
            this.tabletModeToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.tabletModeToolStripMenuItem.Text = "Tablet Mode";
            this.tabletModeToolStripMenuItem.Click += new System.EventHandler(this.tsmiView_Tablet_Click);
            // 
            // showToolsToolStripMenuItem
            // 
            this.showToolsToolStripMenuItem.Checked = true;
            this.showToolsToolStripMenuItem.CheckOnClick = true;
            this.showToolsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showToolsToolStripMenuItem.Name = "showToolsToolStripMenuItem";
            this.showToolsToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.showToolsToolStripMenuItem.Text = "Show Tools";
            this.showToolsToolStripMenuItem.Click += new System.EventHandler(this.showToolsToolStripMenuItem_Click);
            // 
            // updateTimer
            // 
            this.updateTimer.Enabled = true;
            this.updateTimer.Interval = 500;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(884, 639);
            this.Controls.Add(this.msMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.msMenu;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(900, 677);
            this.Name = "Form1";
            this.Text = "Le Paint BETA 0.0.0.0.3";
            this.msMenu.ResumeLayout(false);
            this.msMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip msMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiFile;
        private System.Windows.Forms.ToolStripMenuItem tsmiFile_New;
        private System.Windows.Forms.Timer updateTimer;
        private System.Windows.Forms.ToolStripMenuItem saveImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aspectRatioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gridLinesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tabletModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showToolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiGrid5;
        private System.Windows.Forms.ToolStripMenuItem tsmiGrid10;
        private System.Windows.Forms.ToolStripMenuItem tsmiGrid25;
        private System.Windows.Forms.ToolStripMenuItem tsmiGrid50;
        private System.Windows.Forms.ToolStripMenuItem tsmiGrid100;
        private System.Windows.Forms.ToolStripTextBox tsmiGridCustom;
    }
}

