using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Collections;
using System.Drawing.Imaging;

namespace Paint_Program
{
    public partial class Form1 : Form
    {

        Canvas c;

        private SharedSettings ss;

        public Form1()
        {
            InitializeComponent();

            KeyPreview = true;
            this.KeyDown += Form1_KeyDown;

            this.MinimumSize = new System.Drawing.Size(900, 677);
            ss = new SharedSettings();

            CultureInfo ci = CultureInfo.CurrentUICulture;

            //Potential Issue is Computer Language isn't supported
            SharedSettings.languageFolderPath = @"..\..\Languages\";
            SharedSettings.language = ci.Name.ToString();

            populateLanguages();

            updateText();

            //Default Project
            makeNewProject(500, 500);
        }

        private void populateLanguages()
        {
            string[] langs = Directory.GetFiles(SharedSettings.languageFolderPath);

            foreach (string s in langs)
            {
                //Check to make sur file is a resource file
                if (s.EndsWith(".resx"))
                {
                    try
                    {
                        using (ResXResourceReader resxReader = new ResXResourceReader(s))
                        {
                            foreach (DictionaryEntry entry in resxReader)
                            {
                                if ((string)entry.Key == "resource_languagename")
                                {
                                    Console.WriteLine("Adding: " + s + " to the list of available languages.");
                                    ToolStripMenuItem temp = new ToolStripMenuItem();
                                    temp.Text = (string) entry.Value;
                                    temp.Tag = Path.GetFileNameWithoutExtension(s);
                                    temp.Click += delegate (object sender, EventArgs e) {
                                        //Sets language to the file name with no extention
                                        string filename = (string)((ToolStripMenuItem) sender).Tag;
                                        SharedSettings.language = filename;
                                        updateText();
                                    };
                                    tsmiInternational.DropDownItems.Add(temp);
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error using the resource file " + s + ".\n\n" + e);
                    }
                }
            }
        }

        private void updateText()
        {
            //Form Title
            this.Text = SharedSettings.getGlobalString("title");

            //File Menu Items
            tsmiFile.Text = SharedSettings.getGlobalString("file_menu");
            tsmiFile_New.Text = SharedSettings.getGlobalString("file_menu_new");
            tsmiFile_Save.Text = SharedSettings.getGlobalString("file_menu_save");
            tsmiFile_Load.Text = SharedSettings.getGlobalString("file_menu_open");
            tsmiFile_Import.Text = SharedSettings.getGlobalString("file_menu_import");
            tsmiFile_Export.Text = SharedSettings.getGlobalString("file_menu_export");
            tsmiFile_SaveGoogleDrive.Text = SharedSettings.getGlobalString("file_menu_googledrive");

            //Edit Menu Items
            tsmiEdit.Text = SharedSettings.getGlobalString("edit_menu");
            tsmiEdit_Redo.Text = SharedSettings.getGlobalString("edit_menu_redo");
            tsmiEdit_Undo.Text = SharedSettings.getGlobalString("edit_menu_undo");
            tsmiEdit_Resize.Text = SharedSettings.getGlobalString("edit_menu_resize");
            
            //View Menu Items
            tsmiView.Text = SharedSettings.getGlobalString("view_menu");
            tsmiView_GridLines.Text = SharedSettings.getGlobalString("view_menu_grid");
            tsmiView_GridLines_Auto.Text = SharedSettings.getGlobalString("view_menu_grid_auto");
            tsmiView_ShowTools.Text = SharedSettings.getGlobalString("view_menu_tools");

            //Preferences Menu Items
            tsmiPreferences.Text = SharedSettings.getGlobalString("preferences_menu");
            tsmiPreferences_Watermark.Text = SharedSettings.getGlobalString("preferences_menu_watermark");
            tsmiPreferences_Watermark_SetImage.Text = SharedSettings.getGlobalString("preferences_menu_watermark_set");
            tsmiPreferences_Watermark_ShowWatermark.Text = SharedSettings.getGlobalString("preferences_menu_watermark_show");
            tsmiPreferences_Watermark_Style.Text = SharedSettings.getGlobalString("preferences_menu_watermark_style");
            tsmiPreferences_Watermark_Style_SingleBottom.Text = SharedSettings.getGlobalString("preferences_menu_watermark_style_singlebottom");
            tsmiPreferences_Watermark_Style_SingleCentered.Text = SharedSettings.getGlobalString("preferences_menu_watermark_style_singlecentered");
            tsmiPreferences_Watermark_Style_Tiled.Text = SharedSettings.getGlobalString("preferences_menu_watermark_style_tiled");
            tsmiInternational.Text = SharedSettings.getGlobalString("preferences_menu_international");

            //ToolStrip Item ToolTips
            tsNew.ToolTipText = SharedSettings.getGlobalString("toolstripmenu_new");
            tsImport.ToolTipText = SharedSettings.getGlobalString("toolstripmenu_import");
            tsSave.ToolTipText = SharedSettings.getGlobalString("toostripmenu_save");

            foreach (Control c in Controls)
            {
                if (c is ITextUpdate)
                {
                    ((ITextUpdate)c).updateText();
                }
            }

            this.Refresh();
        }
        
        private void tsmiFile_New_Click(object sender, EventArgs e)
        {

            using (NewProjectForm NewProjForm = new NewProjectForm())
            {
                if (NewProjForm.ShowDialog(this) == DialogResult.OK)
                {

                    clearControls();

                    int w = NewProjForm.CanvasWidth;
                    int h = NewProjForm.CanvasHeight;

                    makeNewProject(w, h);
                }
            }
        }

        private void clearControls()
        {
            for (int i = this.Controls.Count - 1; i >= 0; i--)
            {
                if (!(this.Controls[i] == msMenu) && !(this.Controls[i] == toolStrip1)) //prevents menu distruction
                {
                    this.Controls[i].Dispose(); //Needs to be Dispose or will leak memory
                }
            }
        }

        private void makeNewProject(int w, int h)
        {
            if(c != null)
            {
                c.Trash(); //To help with memory leak issues
            }
            c = new Canvas(w, h, this.Width, this.Height);
            c.Location = new Point(200, 5);
            this.Controls.Add(c);
            c.initCanvas();
            ss = c.getSharedSettings();
        }

        private void makeNewProject(SharedSettings s)
        {
            c = new Canvas(this.Width, this.Height, s);
            c.Location = new Point(200, 5);
            this.Controls.Add(c);
            c.initCanvas();
            this.Update();
        }

        private void tsmiFile_Save_Click(object sender, EventArgs e)
        {
            //Save Project Function

            try
            {
                ProjectSave ps = new ProjectSave(c.getSharedSettings());
            }
            catch (Exception err)
            {
                String s = SharedSettings.getGlobalString("error_save_project") + err.ToString();
                MessageBox.Show(s);
            }
        }

        private void tsmiFile_Import_Click(object sender, EventArgs e)
        {
            //Import Image
            try
            {
                ImageImport ii = new Paint_Program.ImageImport(c.getSharedSettings());
            }
            catch (Exception err)
            {
                Console.WriteLine(err.ToString());
            }
        }

        private void tsmiFile_Export_Click(object sender, EventArgs e)
        {
            //Export Image
            FileSave fs = new FileSave(c.getSharedSettings());

        }

        private void tsmiEdit_Undo_Click(object sender, EventArgs e)
        {
            //Undo Function
        }

        private void tsmiEdit_Redo_Click(object sender, EventArgs e)
        {
            //Redo Function
        }

        private void tsmiEdit_ImageSize_Click(object sender, EventArgs e)
        {
            //Resize Image Function
        }

        private void tsmiView_Tablet_Click(object sender, EventArgs e)
        {
            //Tablet Mode
        }

        private void tsmiView_ShowTools_Click(object sender, EventArgs e)
        {
            if (tsmiView_ShowTools.Checked)
            {
                c.ShowTools();
            }
            else
            {
                c.HideTools();
            }
        }

        private void tsmiFile_Load_Click(object sender, EventArgs e)
        {
            SharedSettings s = new SharedSettings();
            ProjectLoad pl = new ProjectLoad(s);

            if (s.getLoadFromSettings() == false)
            {
                return;
            }

            clearControls();
            makeNewProject(s);
        }

        #region GridLines
        private void tsmiView_GridLines_5_Click(object sender, EventArgs e)
        {
            if (ss != null)
            {
                if (tsmiView_GridLines_5.CheckState == CheckState.Unchecked)
                {
                    ss.setGridToggle(false);
                }
                else
                {
                    GridUncheck();
                    tsmiView_GridLines_5.CheckState = CheckState.Checked;
                    ss.setGridToggle(true);
                    ss.setGridWidth(5);
                }
            }
        }

        private void tsmiView_GridLines_10_Click(object sender, EventArgs e)
        {
            if (ss != null)
            {
                if (tsmiView_GridLines_10.CheckState == CheckState.Unchecked)
                {
                    ss.setGridToggle(false);
                }
                else
                {
                    GridUncheck();
                    tsmiView_GridLines_10.CheckState = CheckState.Checked;
                    ss.setGridToggle(true);
                    ss.setGridWidth(10);
                }
            }
        }

        private void tsmiView_GridLines_25_Click(object sender, EventArgs e)
        {
            if (ss != null)
            {
                if (tsmiView_GridLines_25.CheckState == CheckState.Unchecked)
                {
                    ss.setGridToggle(false);
                }
                else
                {
                    GridUncheck();
                    tsmiView_GridLines_25.CheckState = CheckState.Checked;
                    ss.setGridToggle(true);
                    ss.setGridWidth(25);
                }
            }
        }

        private void tsmiView_GridLines_50_Click(object sender, EventArgs e)
        {
            if (ss != null)
            {
                if (tsmiView_GridLines_50.CheckState == CheckState.Unchecked)
                {
                    ss.setGridToggle(false);
                }
                else
                {
                    GridUncheck();
                    tsmiView_GridLines_50.CheckState = CheckState.Checked;
                    ss.setGridToggle(true);
                    ss.setGridWidth(50);
                }
            }
        }

        private void tsmiView_GridLines_100_Click(object sender, EventArgs e)
        {
            if (ss != null)
            {
                if (tsmiView_GridLines_100.CheckState == CheckState.Unchecked)
                {
                    ss.setGridToggle(false);
                }
                else
                {
                    GridUncheck();
                    tsmiView_GridLines_100.CheckState = CheckState.Checked;
                    ss.setGridToggle(true);
                    ss.setGridWidth(100);
                }
            }
        }

        private void tsmiView_GridLines_Auto_Click(object sender, EventArgs e)
        {
            if (ss != null)
            {
                if (tsmiView_GridLines_Auto.CheckState == CheckState.Unchecked)
                {
                    GridUncheck();
                    tsmiView_GridLines_Auto.CheckState = CheckState.Checked;
                    ss.setGridToggle(true);
                    ss.setGridWidth(-1);
                }
                else
                {
                    tsmiView_GridLines_Auto.CheckState = CheckState.Unchecked;
                    ss.setGridToggle(false);

                }
            }
        }

        private void GridUncheck()
        {
            tsmiView_GridLines_5.CheckState = CheckState.Unchecked;
            tsmiView_GridLines_10.CheckState = CheckState.Unchecked;
            tsmiView_GridLines_25.CheckState = CheckState.Unchecked;
            tsmiView_GridLines_50.CheckState = CheckState.Unchecked;
            tsmiView_GridLines_100.CheckState = CheckState.Unchecked;
            tsmiView_GridLines_Auto.CheckState = CheckState.Unchecked;
        }



        #endregion
        
        private void tsNew_Click(object sender, EventArgs e)
        {
            tsmiFile_New_Click(sender, e);
        }

        private void tsOpen_Click(object sender, EventArgs e)
        {
            tsmiFile_Import_Click(sender, e);
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            tsmiFile_Save_Click(sender, e);
        }

        private void setImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog sfd = new OpenFileDialog();
                sfd.Filter = SharedSettings.getGlobalString("watermarkdialog_filter");
                sfd.Title = SharedSettings.getGlobalString("watermarkdialog_title");
                sfd.ShowDialog();

                SharedSettings.watermarkPath = sfd.FileName;
                SharedSettings.bitmapWatermark = new Bitmap(sfd.FileName);
            }
            catch
            {

            }

            if (SharedSettings.watermarkPath != null)
            {
                tsmiPreferences_Watermark_ShowWatermark.Enabled = true;
                tsmiPreferences_Watermark_Style.Enabled = true;
            }
        }

        private void showWatermarkToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (tsmiPreferences_Watermark_ShowWatermark.Checked == false)
            {
                
                SharedSettings.bitmapWatermark = (Bitmap)Image.FromFile(SharedSettings.watermarkPath);
                SharedSettings.bitmapWatermark.SetResolution(SharedSettings.iCanvasWidth, SharedSettings.iCanvasHeight);
                SharedSettings.bRenderWatermark = true;
                tsmiPreferences_Watermark_ShowWatermark.Checked = true;

            }
            else
            {
                SharedSettings.bRenderWatermark = false;
                tsmiPreferences_Watermark_ShowWatermark.Checked = false;
            }
        }

        private void tsmiPreferences_Watermark_SingleCenter_Click(object sender, EventArgs e)
        {
            SharedSettings.watermarkStyle = "Single Center";
            tsmiPreferences_Watermark_Style_SingleCentered.Checked = true;
            tsmiPreferences_Watermark_Style_SingleBottom.Checked = false;
            tsmiPreferences_Watermark_Style_Tiled.Checked = false;
        }

        private void tsmiPreferences_Watermark_SingleBottom_Click(object sender, EventArgs e)
        {
            SharedSettings.watermarkStyle = "Single Bottom";
            tsmiPreferences_Watermark_Style_SingleCentered.Checked = false;
            tsmiPreferences_Watermark_Style_SingleBottom.Checked = true;
            tsmiPreferences_Watermark_Style_Tiled.Checked = false;
        }

        private void tsmiPreferences_Watermark_Tiled_Click(object sender, EventArgs e)
        {
            SharedSettings.watermarkStyle = "Tiled";
            tsmiPreferences_Watermark_Style_SingleCentered.Checked = false;
            tsmiPreferences_Watermark_Style_SingleBottom.Checked = false;
            tsmiPreferences_Watermark_Style_Tiled.Checked = true;
        }


        private void tsmiFile_SaveGoogleDrive_Click(object sender, EventArgs e)
        {
            using (GDriveSaveDialog gDrive = new GDriveSaveDialog())
            {
                if (gDrive.ShowDialog(this) == DialogResult.OK)
                {
                    string fileName = gDrive.fileName;
                    string fileType = gDrive.fileType;

                    SaveToDrive sd = new SaveToDrive(c.getSharedSettings(), fileName, fileType);
                }
            }
        }

        public void updateViews()
        {
            if (c != null)
            {
                c.updatePositions(this);
            }
        }

        #region Shorcuts
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //CTRL + N for New Project
            if(e.Control && e.KeyCode == Keys.N)
            {
                tsmiFile_New_Click(this, null);
            }
            //CTRL + S for Save Project
            if (e.Control && e.KeyCode == Keys.S)
            {
                tsmiFile_Save_Click(this, null);
            }
            //CTRL + O for open Project
            if (e.Control && e.KeyCode == Keys.O)
            {
                tsmiFile_Load_Click(this, null);
            }
            //CTRL + I for Import Image
            if (e.Control && e.KeyCode == Keys.I)
            {
                tsmiFile_Import_Click(this, null);
            }
            //CTRL + E for Export Image
            if (e.Control && e.KeyCode == Keys.E)
            {
                tsmiFile_Export_Click(this, null);
            }
            //CTRL + G for Export to Google Drive
            if (e.Control && e.KeyCode == Keys.G)
            {
                tsmiFile_SaveGoogleDrive_Click(this, null);
            }
            //CTRL + C for Copy Selection
            if(e.Control && e.KeyCode == Keys.C)
            {
                if(SharedSettings.bitmapSelectionArea != null)
                {
                    copySelectionToClipboard();
                }
            }
            //CTRL + X for Cut Selection
            if (e.Control && e.KeyCode == Keys.X)
            {
                if (SharedSettings.bitmapSelectionArea != null)
                {
                    copySelectionToClipboard();
                    SharedSettings.bitmapSelectionArea = null;
                    SharedSettings.bRenderBitmapInterface = false;
                    SharedSettings.bActiveSelection = false;
                    SharedSettings.bFlattenSelection = true;
                    SharedSettings.gActiveGraphics = SharedSettings.gActiveLayerGraphics;
                }
            }
            //CTRL + V for Paste Clipboard
            if (e.Control && e.KeyCode == Keys.V)
            {
                if(Clipboard.GetImage() != null)
                {
                    SharedSettings.bitmapSelectionArea = (Bitmap) GetClipboardImage();
                    SharedSettings.sSelectionSize = new Size(SharedSettings.bitmapSelectionArea.Width, SharedSettings.bitmapSelectionArea.Height);
                    SharedSettings.pSelectionPoint = new Point(0, 0);
                    SharedSettings.bActiveSelection = true;
                    SharedSettings.bFlattenSelection = false;
                    SharedSettings.bRenderBitmapInterface = true;
                    SharedSettings.bitmapCurrentLayer = SharedSettings.bitmapSelectionArea;
                }
            }
            //CTRL + + for zoom in
            if (e.Control && e.KeyCode == Keys.Oemplus)
            {
                if(c != null)
                {
                    c.zoomIn();
                }
            }
            //CTRL + - for zoom out
            if (e.Control && e.KeyCode == Keys.OemMinus)
            {
                if (c != null)
                {
                    c.zoomOut();
                }
            }
        }

        private void copySelectionToClipboard()
        {
            IDataObject data = new DataObject();
            MemoryStream ms = new MemoryStream();
            SharedSettings.bitmapSelectionArea.Save(ms, ImageFormat.Png);
            data.SetData(DataFormats.Bitmap, SharedSettings.bitmapCurrentLayer);
            data.SetData("PNG", false, ms);
            
            Clipboard.Clear();
            Clipboard.SetImage(SharedSettings.bitmapCurrentLayer);
            Clipboard.SetDataObject(data, true);
            
        }

        private Image GetClipboardImage()
        {
            // Try to paste PNG data.
            if (Clipboard.ContainsData("PNG"))
            {
                Object png_object = Clipboard.GetData("PNG");
                if (png_object is MemoryStream)
                {
                    MemoryStream png_stream = png_object as MemoryStream;
                    return Image.FromStream(png_stream);
                }
            }

            // Try to paste bitmap data.
            if (Clipboard.ContainsImage())
            {
                return Clipboard.GetImage();
            }

            // We couldn't find anything useful. Return null.
            return null;
        }

        #endregion
    }

}
