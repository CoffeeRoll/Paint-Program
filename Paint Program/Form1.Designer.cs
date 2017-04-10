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
            this.tsmiFile_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFile_Load = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFile_Import = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFile_Export = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFile_SaveGoogleDrive = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEdit_Undo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEdit_Redo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEdit_Resize = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiView = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiView_GridLines = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiView_GridLines_5 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiView_GridLines_10 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiView_GridLines_25 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiView_GridLines_50 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiView_GridLines_100 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiView_GridLines_Auto = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiView_Tablet = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiView_ShowTools = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPreferences = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPreferences_Watermark = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPreferences_Watermark_SetImage = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPreferences_Watermark_ShowWatermark = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPreferences_Watermark_Style = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPreferences_Watermark_Style_SingleCentered = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPreferences_Watermark_Style_SingleBottom = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPreferences_Watermark_Style_Tiled = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiInternational = new System.Windows.Forms.ToolStripMenuItem();
            this.updateTimer = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsNew = new System.Windows.Forms.ToolStripButton();
            this.tsImport = new System.Windows.Forms.ToolStripButton();
            this.tsSave = new System.Windows.Forms.ToolStripButton();
            this.msMenu.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // msMenu
            // 
            this.msMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.msMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFile,
            this.tsmiEdit,
            this.tsmiView,
            this.tsmiPreferences});
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
            this.tsmiFile_Save,
            this.tsmiFile_Load,
            this.tsmiFile_Import,
            this.tsmiFile_Export,
            this.tsmiFile_SaveGoogleDrive});
            this.tsmiFile.Name = "tsmiFile";
            this.tsmiFile.Size = new System.Drawing.Size(37, 22);
            this.tsmiFile.Text = "File";
            // 
            // tsmiFile_New
            // 
            this.tsmiFile_New.Name = "tsmiFile_New";
            this.tsmiFile_New.Size = new System.Drawing.Size(186, 22);
            this.tsmiFile_New.Text = "New";
            this.tsmiFile_New.Click += new System.EventHandler(this.tsmiFile_New_Click);
            // 
            // tsmiFile_Save
            // 
            this.tsmiFile_Save.Name = "tsmiFile_Save";
            this.tsmiFile_Save.Size = new System.Drawing.Size(186, 22);
            this.tsmiFile_Save.Text = "Save";
            this.tsmiFile_Save.Click += new System.EventHandler(this.tsmiFile_Save_Click);
            // 
            // tsmiFile_Load
            // 
            this.tsmiFile_Load.Name = "tsmiFile_Load";
            this.tsmiFile_Load.Size = new System.Drawing.Size(186, 22);
            this.tsmiFile_Load.Text = "Load";
            this.tsmiFile_Load.Click += new System.EventHandler(this.tsmiFile_Load_Click);
            // 
            // tsmiFile_Import
            // 
            this.tsmiFile_Import.Name = "tsmiFile_Import";
            this.tsmiFile_Import.Size = new System.Drawing.Size(186, 22);
            this.tsmiFile_Import.Text = "Import Image";
            this.tsmiFile_Import.Click += new System.EventHandler(this.tsmiFile_Import_Click);
            // 
            // tsmiFile_Export
            // 
            this.tsmiFile_Export.Name = "tsmiFile_Export";
            this.tsmiFile_Export.Size = new System.Drawing.Size(186, 22);
            this.tsmiFile_Export.Text = "Export Image";
            this.tsmiFile_Export.Click += new System.EventHandler(this.tsmiFile_Export_Click);
            // 
            // tsmiFile_SaveGoogleDrive
            // 
            this.tsmiFile_SaveGoogleDrive.Name = "tsmiFile_SaveGoogleDrive";
            this.tsmiFile_SaveGoogleDrive.Size = new System.Drawing.Size(186, 22);
            this.tsmiFile_SaveGoogleDrive.Text = "Save To Google Drive";
            this.tsmiFile_SaveGoogleDrive.Click += new System.EventHandler(this.tsmi_Save_Google_Drive_Click);
            // 
            // tsmiEdit
            // 
            this.tsmiEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiEdit_Undo,
            this.tsmiEdit_Redo,
            this.tsmiEdit_Resize});
            this.tsmiEdit.Name = "tsmiEdit";
            this.tsmiEdit.Size = new System.Drawing.Size(39, 22);
            this.tsmiEdit.Text = "Edit";
            // 
            // tsmiEdit_Undo
            // 
            this.tsmiEdit_Undo.Name = "tsmiEdit_Undo";
            this.tsmiEdit_Undo.Size = new System.Drawing.Size(130, 22);
            this.tsmiEdit_Undo.Text = "Undo";
            this.tsmiEdit_Undo.Click += new System.EventHandler(this.tsmiEdit_Undo_Click);
            // 
            // tsmiEdit_Redo
            // 
            this.tsmiEdit_Redo.Name = "tsmiEdit_Redo";
            this.tsmiEdit_Redo.Size = new System.Drawing.Size(130, 22);
            this.tsmiEdit_Redo.Text = "Redo";
            this.tsmiEdit_Redo.Click += new System.EventHandler(this.tsmiEdit_Redo_Click);
            // 
            // tsmiEdit_Resize
            // 
            this.tsmiEdit_Resize.Name = "tsmiEdit_Resize";
            this.tsmiEdit_Resize.Size = new System.Drawing.Size(130, 22);
            this.tsmiEdit_Resize.Text = "Image Size";
            this.tsmiEdit_Resize.Click += new System.EventHandler(this.tsmiEdit_ImageSize_Click);
            // 
            // tsmiView
            // 
            this.tsmiView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiView_GridLines,
            this.tsmiView_Tablet,
            this.tsmiView_ShowTools});
            this.tsmiView.Name = "tsmiView";
            this.tsmiView.Size = new System.Drawing.Size(44, 22);
            this.tsmiView.Text = "View";
            // 
            // tsmiView_GridLines
            // 
            this.tsmiView_GridLines.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiView_GridLines_5,
            this.tsmiView_GridLines_10,
            this.tsmiView_GridLines_25,
            this.tsmiView_GridLines_50,
            this.tsmiView_GridLines_100,
            this.tsmiView_GridLines_Auto});
            this.tsmiView_GridLines.Name = "tsmiView_GridLines";
            this.tsmiView_GridLines.Size = new System.Drawing.Size(166, 22);
            this.tsmiView_GridLines.Text = "Toggle Grid Lines";
            // 
            // tsmiView_GridLines_5
            // 
            this.tsmiView_GridLines_5.CheckOnClick = true;
            this.tsmiView_GridLines_5.Name = "tsmiView_GridLines_5";
            this.tsmiView_GridLines_5.Size = new System.Drawing.Size(100, 22);
            this.tsmiView_GridLines_5.Text = "5";
            this.tsmiView_GridLines_5.Click += new System.EventHandler(this.tsmiView_GridLines_5_Click);
            // 
            // tsmiView_GridLines_10
            // 
            this.tsmiView_GridLines_10.CheckOnClick = true;
            this.tsmiView_GridLines_10.Name = "tsmiView_GridLines_10";
            this.tsmiView_GridLines_10.Size = new System.Drawing.Size(100, 22);
            this.tsmiView_GridLines_10.Text = "10";
            this.tsmiView_GridLines_10.Click += new System.EventHandler(this.tsmiView_GridLines_10_Click);
            // 
            // tsmiView_GridLines_25
            // 
            this.tsmiView_GridLines_25.CheckOnClick = true;
            this.tsmiView_GridLines_25.Name = "tsmiView_GridLines_25";
            this.tsmiView_GridLines_25.Size = new System.Drawing.Size(100, 22);
            this.tsmiView_GridLines_25.Text = "25";
            this.tsmiView_GridLines_25.Click += new System.EventHandler(this.tsmiView_GridLines_25_Click);
            // 
            // tsmiView_GridLines_50
            // 
            this.tsmiView_GridLines_50.CheckOnClick = true;
            this.tsmiView_GridLines_50.Name = "tsmiView_GridLines_50";
            this.tsmiView_GridLines_50.Size = new System.Drawing.Size(100, 22);
            this.tsmiView_GridLines_50.Text = "50";
            this.tsmiView_GridLines_50.Click += new System.EventHandler(this.tsmiView_GridLines_50_Click);
            // 
            // tsmiView_GridLines_100
            // 
            this.tsmiView_GridLines_100.CheckOnClick = true;
            this.tsmiView_GridLines_100.Name = "tsmiView_GridLines_100";
            this.tsmiView_GridLines_100.Size = new System.Drawing.Size(100, 22);
            this.tsmiView_GridLines_100.Text = "100";
            this.tsmiView_GridLines_100.Click += new System.EventHandler(this.tsmiView_GridLines_100_Click);
            // 
            // tsmiView_GridLines_Auto
            // 
            this.tsmiView_GridLines_Auto.Name = "tsmiView_GridLines_Auto";
            this.tsmiView_GridLines_Auto.Size = new System.Drawing.Size(100, 22);
            this.tsmiView_GridLines_Auto.Text = "Auto";
            this.tsmiView_GridLines_Auto.Click += new System.EventHandler(this.tsmiView_GridLines_Auto_Click);
            // 
            // tsmiView_Tablet
            // 
            this.tsmiView_Tablet.Name = "tsmiView_Tablet";
            this.tsmiView_Tablet.Size = new System.Drawing.Size(166, 22);
            this.tsmiView_Tablet.Text = "Tablet Mode";
            this.tsmiView_Tablet.Click += new System.EventHandler(this.tsmiView_Tablet_Click);
            // 
            // tsmiView_ShowTools
            // 
            this.tsmiView_ShowTools.Checked = true;
            this.tsmiView_ShowTools.CheckOnClick = true;
            this.tsmiView_ShowTools.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsmiView_ShowTools.Name = "tsmiView_ShowTools";
            this.tsmiView_ShowTools.Size = new System.Drawing.Size(166, 22);
            this.tsmiView_ShowTools.Text = "Show Tools";
            this.tsmiView_ShowTools.Click += new System.EventHandler(this.tsmiView_ShowTools_Click);
            // 
            // tsmiPreferences
            // 
            this.tsmiPreferences.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiPreferences_Watermark,
            this.tsmiInternational});
            this.tsmiPreferences.Name = "tsmiPreferences";
            this.tsmiPreferences.Size = new System.Drawing.Size(80, 22);
            this.tsmiPreferences.Text = "Preferences";
            // 
            // tsmiPreferences_Watermark
            // 
            this.tsmiPreferences_Watermark.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiPreferences_Watermark_SetImage,
            this.tsmiPreferences_Watermark_ShowWatermark,
            this.tsmiPreferences_Watermark_Style});
            this.tsmiPreferences_Watermark.Name = "tsmiPreferences_Watermark";
            this.tsmiPreferences_Watermark.Size = new System.Drawing.Size(141, 22);
            this.tsmiPreferences_Watermark.Text = "Watermark...";
            // 
            // tsmiPreferences_Watermark_SetImage
            // 
            this.tsmiPreferences_Watermark_SetImage.Name = "tsmiPreferences_Watermark_SetImage";
            this.tsmiPreferences_Watermark_SetImage.Size = new System.Drawing.Size(164, 22);
            this.tsmiPreferences_Watermark_SetImage.Text = "Set Image...";
            this.tsmiPreferences_Watermark_SetImage.Click += new System.EventHandler(this.setImageToolStripMenuItem_Click);
            // 
            // tsmiPreferences_Watermark_ShowWatermark
            // 
            this.tsmiPreferences_Watermark_ShowWatermark.Enabled = false;
            this.tsmiPreferences_Watermark_ShowWatermark.Name = "tsmiPreferences_Watermark_ShowWatermark";
            this.tsmiPreferences_Watermark_ShowWatermark.Size = new System.Drawing.Size(164, 22);
            this.tsmiPreferences_Watermark_ShowWatermark.Text = "Show Watermark";
            this.tsmiPreferences_Watermark_ShowWatermark.Click += new System.EventHandler(this.showWatermarkToolStripMenuItem_Click);
            // 
            // tsmiPreferences_Watermark_Style
            // 
            this.tsmiPreferences_Watermark_Style.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiPreferences_Watermark_Style_SingleCentered,
            this.tsmiPreferences_Watermark_Style_SingleBottom,
            this.tsmiPreferences_Watermark_Style_Tiled});
            this.tsmiPreferences_Watermark_Style.Enabled = false;
            this.tsmiPreferences_Watermark_Style.Name = "tsmiPreferences_Watermark_Style";
            this.tsmiPreferences_Watermark_Style.Size = new System.Drawing.Size(164, 22);
            this.tsmiPreferences_Watermark_Style.Text = "Style";
            // 
            // tsmiPreferences_Watermark_Style_SingleCentered
            // 
            this.tsmiPreferences_Watermark_Style_SingleCentered.Checked = true;
            this.tsmiPreferences_Watermark_Style_SingleCentered.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsmiPreferences_Watermark_Style_SingleCentered.Name = "tsmiPreferences_Watermark_Style_SingleCentered";
            this.tsmiPreferences_Watermark_Style_SingleCentered.Size = new System.Drawing.Size(157, 22);
            this.tsmiPreferences_Watermark_Style_SingleCentered.Text = "Single Centered";
            this.tsmiPreferences_Watermark_Style_SingleCentered.Click += new System.EventHandler(this.tsmiPreferences_Watermark_SingleCenter_Click);
            // 
            // tsmiPreferences_Watermark_Style_SingleBottom
            // 
            this.tsmiPreferences_Watermark_Style_SingleBottom.Name = "tsmiPreferences_Watermark_Style_SingleBottom";
            this.tsmiPreferences_Watermark_Style_SingleBottom.Size = new System.Drawing.Size(157, 22);
            this.tsmiPreferences_Watermark_Style_SingleBottom.Text = "Single Bottom";
            this.tsmiPreferences_Watermark_Style_SingleBottom.Click += new System.EventHandler(this.tsmiPreferences_Watermark_SingleBottom_Click);
            // 
            // tsmiPreferences_Watermark_Style_Tiled
            // 
            this.tsmiPreferences_Watermark_Style_Tiled.Name = "tsmiPreferences_Watermark_Style_Tiled";
            this.tsmiPreferences_Watermark_Style_Tiled.Size = new System.Drawing.Size(157, 22);
            this.tsmiPreferences_Watermark_Style_Tiled.Text = "Tiled";
            this.tsmiPreferences_Watermark_Style_Tiled.Click += new System.EventHandler(this.tsmiPreferences_Watermark_Tiled_Click);
            // 
            // tsmiInternational
            // 
            this.tsmiInternational.Name = "tsmiInternational";
            this.tsmiInternational.Size = new System.Drawing.Size(141, 22);
            this.tsmiInternational.Text = "International";
            // 
            // updateTimer
            // 
            this.updateTimer.Enabled = true;
            this.updateTimer.Interval = 500;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsNew,
            this.tsImport,
            this.tsSave});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(884, 31);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsNew
            // 
            this.tsNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsNew.Image = ((System.Drawing.Image)(resources.GetObject("tsNew.Image")));
            this.tsNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsNew.Name = "tsNew";
            this.tsNew.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.tsNew.Size = new System.Drawing.Size(35, 28);
            this.tsNew.Text = "New Project";
            this.tsNew.Click += new System.EventHandler(this.tsNew_Click);
            // 
            // tsImport
            // 
            this.tsImport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsImport.Image = ((System.Drawing.Image)(resources.GetObject("tsImport.Image")));
            this.tsImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsImport.Name = "tsImport";
            this.tsImport.Size = new System.Drawing.Size(28, 28);
            this.tsImport.Text = "Open File";
            this.tsImport.Click += new System.EventHandler(this.tsOpen_Click);
            // 
            // tsSave
            // 
            this.tsSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsSave.Image = ((System.Drawing.Image)(resources.GetObject("tsSave.Image")));
            this.tsSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSave.Name = "tsSave";
            this.tsSave.Size = new System.Drawing.Size(28, 28);
            this.tsSave.Text = "Save";
            this.tsSave.Click += new System.EventHandler(this.tsSave_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(884, 639);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.msMenu);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.msMenu;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(855, 646);
            this.Name = "Form1";
            this.Text = "Le Paint BETA 0.0.0.0.0.3";
            this.msMenu.ResumeLayout(false);
            this.msMenu.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip msMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiFile;
        private System.Windows.Forms.ToolStripMenuItem tsmiFile_New;
        private System.Windows.Forms.Timer updateTimer;
        private System.Windows.Forms.ToolStripMenuItem tsmiFile_Save;
        private System.Windows.Forms.ToolStripMenuItem tsmiFile_Import;
        private System.Windows.Forms.ToolStripMenuItem tsmiFile_Export;
        private System.Windows.Forms.ToolStripMenuItem tsmiEdit;
        private System.Windows.Forms.ToolStripMenuItem tsmiEdit_Undo;
        private System.Windows.Forms.ToolStripMenuItem tsmiEdit_Redo;
        private System.Windows.Forms.ToolStripMenuItem tsmiEdit_Resize;
        private System.Windows.Forms.ToolStripMenuItem tsmiView;
        private System.Windows.Forms.ToolStripMenuItem tsmiView_GridLines;
        private System.Windows.Forms.ToolStripMenuItem tsmiView_Tablet;
        private System.Windows.Forms.ToolStripMenuItem tsmiView_ShowTools;
        private System.Windows.Forms.ToolStripMenuItem tsmiFile_Load;
        private System.Windows.Forms.ToolStripMenuItem tsmiView_GridLines_5;
        private System.Windows.Forms.ToolStripMenuItem tsmiView_GridLines_10;
        private System.Windows.Forms.ToolStripMenuItem tsmiView_GridLines_25;
        private System.Windows.Forms.ToolStripMenuItem tsmiView_GridLines_50;
        private System.Windows.Forms.ToolStripMenuItem tsmiView_GridLines_100;
        private System.Windows.Forms.ToolStripMenuItem tsmiView_GridLines_Auto;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsNew;
        private System.Windows.Forms.ToolStripButton tsImport;
        private System.Windows.Forms.ToolStripButton tsSave;
        private System.Windows.Forms.ToolStripMenuItem tsmiPreferences;
        private System.Windows.Forms.ToolStripMenuItem tsmiPreferences_Watermark;
        private System.Windows.Forms.ToolStripMenuItem tsmiPreferences_Watermark_SetImage;
        private System.Windows.Forms.ToolStripMenuItem tsmiPreferences_Watermark_ShowWatermark;
        private System.Windows.Forms.ToolStripMenuItem tsmiPreferences_Watermark_Style;
        private System.Windows.Forms.ToolStripMenuItem tsmiPreferences_Watermark_Style_SingleCentered;
        private System.Windows.Forms.ToolStripMenuItem tsmiPreferences_Watermark_Style_SingleBottom;
        private System.Windows.Forms.ToolStripMenuItem tsmiPreferences_Watermark_Style_Tiled;
        private System.Windows.Forms.ToolStripMenuItem tsmiInternational;
        private System.Windows.Forms.ToolStripMenuItem tsmiFile_SaveGoogleDrive;
    }
}

