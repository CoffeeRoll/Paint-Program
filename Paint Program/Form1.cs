﻿using System;
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

                    Console.WriteLine(w + " + " + h);

                    makeNewProject(w, h);
                }
            }
        }

        private void clearControls()
        {
            for (int i = this.Controls.Count - 1; i >= 0; i--)
            {
                if (!(this.Controls[i] is MenuStrip) && !(this.Controls[i] is ToolStrip))
                {
                    this.Controls.Remove(this.Controls[i]);
                }
            }
        }

        private void makeNewProject(int w, int h)
        {
            c = new Canvas(w, h, this.Width, this.Height);
            c.Location = new Point(200, 5);
            this.Controls.Add(c);
            c.initCanvas();
            ss = c.getSharedSettings();
            this.Update();
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
            clearControls();

            SharedSettings s = new SharedSettings();
            ProjectLoad pl = new ProjectLoad(s);

            if (s.getLoadFromSettings() == false)
            {
                return;
            }

            makeNewProject(s);
        }

        #region GridLines
        private void tsmiGrid5_Click(object sender, EventArgs e)
        {
            if (ss != null)
            {
                GridUncheck();
                tsmiGrid5.CheckState = CheckState.Checked;

                if (tsmiGrid5.Checked)
                {
                    ss.setGridToggle(true);
                    ss.setGridWidth(5);
                }
            }
        }

        private void tsmiGrid10_Click(object sender, EventArgs e)
        {
            if (ss != null)
            {
                GridUncheck();
                tsmiGrid10.CheckState = CheckState.Checked;

                if (tsmiGrid10.Checked)
                {
                    ss.setGridToggle(true);
                    ss.setGridWidth(10);
                }
            }
        }

        private void tsmiGrid25_Click(object sender, EventArgs e)
        {
            if (ss != null)
            {
                GridUncheck();
                tsmiGrid25.CheckState = CheckState.Checked;

                if (tsmiGrid25.Checked)
                {
                    ss.setGridToggle(true);
                    ss.setGridWidth(25);
                }
            }
        }

        private void tsmiGrid50_Click(object sender, EventArgs e)
        {
            if (ss != null)
            {
                GridUncheck();
                tsmiGrid50.CheckState = CheckState.Checked;

                if (tsmiGrid50.Checked)
                {
                    ss.setGridToggle(true);
                    ss.setGridWidth(50);
                }
            }
        }

        private void tsmiGrid100_Click(object sender, EventArgs e)
        {
            if (ss != null)
            {
                GridUncheck();
                tsmiGrid100.CheckState = CheckState.Checked;

                if (tsmiGrid100.Checked)
                {
                    ss.setGridToggle(true);
                    ss.setGridWidth(100);
                }
            }
        }

        private void tsmiGridAuto_Click(object sender, EventArgs e)
        {
            if (ss != null)
            {
                GridUncheck();
                tsmiGridAuto.CheckState = CheckState.Checked;

                if (tsmiGrid5.Checked)
                {
                    ss.setGridToggle(true);
                    ss.setGridWidth(-1);
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
    }

}
