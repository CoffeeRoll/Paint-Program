using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace Paint_Program
{
    public partial class LayerItem : UserControl
    {
        private bool isSelected;
        private bool isVisible;

        private Color cSelected = Color.FromArgb(105, 185, 255);
        private Color cNotSelected = Color.FromArgb(192, 192, 192);

        private Bitmap LayerBitmap;

        public LayerItem(int w, int h, PixelFormat pf)
        {
            InitializeComponent();
            isSelected = false;
            isVisible = true;
            LayerBitmap = new Bitmap(w, h, pf);

            //Stretch inner Bitmap to fit
            pbLayerPreview.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        public void updatePreview()
        {
            pbLayerPreview.Image = LayerBitmap;
        }

        public Bitmap getBitmap()
        {
            return LayerBitmap;
        }

        public void setBitmap(Bitmap b)
        {
            LayerBitmap = b;
        }

        private void cbVisible_CheckedChanged(object sender, EventArgs e)
        {
            //Updates the Visibility state based on the checkbox
            isVisible = cbVisible.Checked;
        }

        private void LayerForm_Click(object sender, EventArgs e)
        {
            isSelected = !isSelected;
            this.BackColor = isSelected ? cSelected : cNotSelected;
        }

        public bool isLayerSelected()
        {
            return isSelected;
        }

        public bool isLayerVisible()
        {
            return isVisible;
        }
    }
}
