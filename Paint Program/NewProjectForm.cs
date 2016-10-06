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

    
    public partial class NewProjectForm : Form
    {

        public int CanvasWidth { get; private set; }
        public int CanvasHeight{ get; private set; }

        public NewProjectForm()
        {
            InitializeComponent();
            CanvasWidth = -1;
            CanvasHeight = -1;
        }

        private void bSubmit_Click(object sender, EventArgs e)
        {
            int width, height;

            if (!Int32.TryParse(tbWidth.Text, out width) || (Int32.TryParse(tbWidth.Text, out width) && width <= 0))
            {
                //Failed, Invalid Width Value
                tbWidth.BackColor = Color.Pink;
                tbWidth.Update();
            }

            if (!Int32.TryParse(tbHeight.Text, out height) || (Int32.TryParse(tbHeight.Text, out height) && height <= 0))
            {
                //Failed, Invalid Height Value
                tbHeight.BackColor = Color.Pink;
                tbHeight.Update();
            }

            if (width > 0 && height > 0)
            {
                this.CanvasWidth = width;
                this.CanvasHeight = height;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
