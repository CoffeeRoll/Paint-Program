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
        private bool isActive;

        private Color cSelected = Color.FromArgb(105, 185, 255);
        private Color cActive = Color.FromArgb(100, 150, 100);
        private Color cNotSelected = Color.FromArgb(192, 192, 192);

        private Bitmap LayerBitmap;

        public LayerItem(int w, int h, PixelFormat pf)
        {
            InitializeComponent();
            isSelected = false;
            isVisible = true;
            LayerBitmap = new Bitmap(w, h, pf);

            cbVisible.Checked = true;

            this.BackColor = cNotSelected;

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

        public void setActive(bool f)
        {
            isActive = f;
            updateColor();

        }

        public void setSelected(bool f)
        {
            isSelected = f;
            updateColor();
        }

        public void setVisibility(bool f)
        {
            isVisible = f;
            cbVisible.Checked = isVisible;
        }

        public bool isLayerSelected()
        {
            return isSelected;
        }

        public bool isLayerVisible()
        {
            return isVisible;
        }

        public bool isLayerActive()
        {
            return isActive;
        }

        private void updateColor()
        {
            if (isSelected)
            {
                BackColor = cSelected;
            }
            else if (isActive)
            {
                BackColor = cActive;
            }
            else
            {
                BackColor = cNotSelected;
            }
        }

        public void setOnClick(System.EventHandler func)
        {
            this.Click += func;
            pbLayerPreview.Click += func;
        }

        public void setOnDoubleClick(System.EventHandler func)
        {
            this.DoubleClick += func;
            pbLayerPreview.DoubleClick += func;
        }

        private void cbVisible_CheckedChanged(object sender, EventArgs e)
        {
            //Updates the Visibility state based on the checkbox
            isVisible = cbVisible.Checked;
        }

        private void pbLayerPreview_Click(object sender, EventArgs e)
        {

        }

        private void LayerForm_Click(object sender, EventArgs e)
        {

        }
    }
}
