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
        public Form1()
        {
            InitializeComponent();
        }
        Canvas c;

        private void tsmiFile_New_Click(object sender, EventArgs e)
        {

            using (NewProjectForm NewProjForm = new NewProjectForm())
            {
                if (NewProjForm.ShowDialog(this) == DialogResult.OK)
                {

                    for (int i = this.Controls.Count - 1; i >= 0; i--)
                    {
                        if (!(this.Controls[i] is MenuStrip))
                        {
                            this.Controls.Remove(this.Controls[i]);
                        }
                    }

                    int w = NewProjForm.CanvasWidth;
                    int h = NewProjForm.CanvasHeight;
                    c = new Canvas(w, h, this.Width, this.Height);
                    c.Location = new Point(200, 5);
                    this.Controls.Add(c);
                    c.initCanvas();
                    

                    this.Update();

                }
            }
        }
        
        private void tsmiFile_Save_Click(object sender, EventArgs e)
        {
            //Save Image Function

        }

        private void tsmiFile_Import_Click(object sender, EventArgs e)
        {
            //Import Image
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

        private void tsmiView_GridLines_Click(object sender, EventArgs e)
        {
            //Show/Hide Grid Lines
        }

        private void tsmiView_Tablet_Click(object sender, EventArgs e)
        {
            //Tablet Mode
        }

        private void showToolsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (showToolsToolStripMenuItem.Checked)
            {
                foreach (Canvas.ToolButtons)
                {

                }
            }
        }
    }
}
