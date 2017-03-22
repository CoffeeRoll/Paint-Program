using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint_Program
{
    public partial class Form1 : Form
    {

        Canvas c;

        private SharedSettings ss;

        public Form1()
        {
            InitializeComponent();
            this.MinimumSize = new System.Drawing.Size(900, 677);
            ss = null;
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
                String s = "Error Saving Project! " + err.ToString();
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

        private void showToolsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (showToolsToolStripMenuItem.Checked)
            {
                c.ShowTools();
            }
            else
            {
                c.HideTools();
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
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
        private void tsmiGrid5_Click(object sender, EventArgs e)
        {
            if (ss != null)
            {
                if (tsmiGrid5.CheckState == CheckState.Unchecked)
                {
                    ss.setGridToggle(false);
                }
                else
                {
                    GridUncheck();
                    tsmiGrid5.CheckState = CheckState.Checked;
                    ss.setGridToggle(true);
                    ss.setGridWidth(5);
                }
            }
        }

        private void tsmiGrid10_Click(object sender, EventArgs e)
        {
            if (ss != null)
            {
                if (tsmiGrid10.CheckState == CheckState.Unchecked)
                {
                    ss.setGridToggle(false);
                }
                else
                {
                    GridUncheck();
                    tsmiGrid10.CheckState = CheckState.Checked;
                    ss.setGridToggle(true);
                    ss.setGridWidth(10);
                }
            }
        }

        private void tsmiGrid25_Click(object sender, EventArgs e)
        {
            if (ss != null)
            {
                if (tsmiGrid25.CheckState == CheckState.Unchecked)
                {
                    ss.setGridToggle(false);
                }
                else
                {
                    GridUncheck();
                    tsmiGrid25.CheckState = CheckState.Checked;
                    ss.setGridToggle(true);
                    ss.setGridWidth(25);
                }
            }
        }

        private void tsmiGrid50_Click(object sender, EventArgs e)
        {
            if (ss != null)
            {
                if (tsmiGrid50.CheckState == CheckState.Unchecked)
                {
                    ss.setGridToggle(false);
                }
                else
                {
                    GridUncheck();
                    tsmiGrid50.CheckState = CheckState.Checked;
                    ss.setGridToggle(true);
                    ss.setGridWidth(50);
                }
            }
        }

        private void tsmiGrid100_Click(object sender, EventArgs e)
        {
            if (ss != null)
            {
                if (tsmiGrid100.CheckState == CheckState.Unchecked)
                {
                    ss.setGridToggle(false);
                }
                else
                {
                    GridUncheck();
                    tsmiGrid100.CheckState = CheckState.Checked;
                    ss.setGridToggle(true);
                    ss.setGridWidth(100);
                }
            }
        }

        private void tsmiGridAuto_Click(object sender, EventArgs e)
        {
            if (ss != null)
            {
                if (tsmiGridAuto.CheckState == CheckState.Unchecked)
                {
                    GridUncheck();
                    tsmiGridAuto.CheckState = CheckState.Checked;
                    ss.setGridToggle(true);
                    ss.setGridWidth(-1);
                }
                else
                {
                    tsmiGridAuto.CheckState = CheckState.Unchecked;
                    ss.setGridToggle(false);

                }
            }
        }

        private void GridUncheck()
        {
            tsmiGrid5.CheckState = CheckState.Unchecked;
            tsmiGrid10.CheckState = CheckState.Unchecked;
            tsmiGrid25.CheckState = CheckState.Unchecked;
            tsmiGrid50.CheckState = CheckState.Unchecked;
            tsmiGrid100.CheckState = CheckState.Unchecked;
            tsmiGridAuto.CheckState = CheckState.Unchecked;
        }



        #endregion
        //Whoops,    v newfile v
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            tsmiFile_New_Click(sender, e);
        }

        private void OpenFile_Click(object sender, EventArgs e)
        {
            tsmiFile_Import_Click(sender, e);
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            tsmiFile_Save_Click(sender, e);
        }

        private void setImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog sfd = new OpenFileDialog();
                sfd.Filter = "All Files|*.*";
                sfd.Title = "Select Watermark File";
                sfd.ShowDialog();

                SharedSettings.watermarkPath = sfd.FileName;
                SharedSettings.bitmapWatermark = new Bitmap(sfd.FileName);
            }
            catch
            {

            }

            if (SharedSettings.watermarkPath != null)
            {
                showWatermarkToolStripMenuItem.Enabled = true;
                styleToolStripMenuItem.Enabled = true;
            }
        }

        private void showWatermarkToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (showWatermarkToolStripMenuItem.Checked == false)
            {
                
                SharedSettings.bitmapWatermark = (Bitmap)Image.FromFile(SharedSettings.watermarkPath);
                SharedSettings.bitmapWatermark.SetResolution(SharedSettings.iCanvasWidth, SharedSettings.iCanvasHeight);
                SharedSettings.bRenderWatermark = true;
                showWatermarkToolStripMenuItem.Checked = true;

            }
            else
            {
                SharedSettings.bRenderWatermark = false;
                showWatermarkToolStripMenuItem.Checked = false;
            }
        }

        private void singleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SharedSettings.watermarkStyle = "Single Center";
            singleToolStripMenuItem.Checked = true;
            singleBottomToolStripMenuItem.Checked = false;
            tiledToolStripMenuItem.Checked = false;
        }

        private void singleBottomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SharedSettings.watermarkStyle = "Single Bottom";
            singleToolStripMenuItem.Checked = false;
            singleBottomToolStripMenuItem.Checked = true;
            tiledToolStripMenuItem.Checked = false;
        }

        private void tiledToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SharedSettings.watermarkStyle = "Tiled";
            singleToolStripMenuItem.Checked = false;
            singleBottomToolStripMenuItem.Checked = false;
            tiledToolStripMenuItem.Checked = true;
        }
    }

}
