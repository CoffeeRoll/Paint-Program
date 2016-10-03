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

        private void tsmiFile_New_Click(object sender, EventArgs e)
        {

            using (NewProjectForm NewProjForm = new NewProjectForm())
            {
                if (NewProjForm.ShowDialog(this) == DialogResult.OK)
                {
                    int w = NewProjForm.CanvasWidth;
                    int h = NewProjForm.CanvasHeight;

                    Canvas c = new Canvas(w, h, this.Width, this.Height);
                    c.Location = new Point(200, 5);
                    this.Controls.Add(c);
                    c.initCanvas();
                    

                    this.Update();

                }
            }
        }

        private void tsmiFile_Save_Image(object sender, EventArgs e)
        {

        }

        private void tsmiFile_Import_Image(object sender, EventArgs e)
        {

        }

        private void tsmiFile_Click(object sender, EventArgs e)
        {

        }

        private void tsmiFile_Export_Image(object sender, EventArgs e)
        {

        }

        private void tsmiEdit_Undo(object sender, EventArgs e)
        {

        }

        private void tsmiEdit_Redo(object sender, EventArgs e)
        {

        }

        private void tsmiView_Grid_Lines(object sender, EventArgs e)
        {

        }
    }
}
