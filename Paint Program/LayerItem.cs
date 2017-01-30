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
        private bool isVisible;
        private bool isActive;

        private SharedSettings ss;

        private String sLayerName;

        private Color cActive = Color.FromArgb(100, 155, 155);
        private Color cNotActive = Color.FromArgb(192, 192, 192);

        private Bitmap LayerBitmap;

        private Graphics g;

        public LayerItem(int w, int h, PixelFormat pf, String name, SharedSettings s)
        {
            ss = s;
            InitializeComponent();
            isVisible = true;
            LayerBitmap = new Bitmap(w, h, pf);
            g = Graphics.FromImage(LayerBitmap);
            cbVisible.Checked = true;

            this.BackColor = cNotActive;

            //Stretch The Preview Image to fit
            pbLayerPreview.SizeMode = PictureBoxSizeMode.Zoom;
            
            pbLayerPreview.BackColor = Color.White;

            sLayerName = name;
            tbLayerName.Text = sLayerName;

            this.Update();
        }

        public void updatePreview()
        {
            pbLayerPreview.Image = LayerBitmap;
        }

        public Graphics getGraphics()
        {
            return g;
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
            ss.setBitmapCurrentLayer(getBitmap());
            ss.setActiveGraphics(g);
        }

        public void setVisibility(bool f)
        {
            isVisible = f;
            cbVisible.Checked = isVisible;
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
            if (isActive)
            {
                this.BackColor = cActive;
            }
            else
            {
                this.BackColor = cNotActive;
            }
        }

        public String getLayerName()
        {
            return sLayerName;
        }

        public void setOnClick(System.EventHandler func)
        {
            this.Click += func;
            pbLayerPreview.Click += func;
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

        private void tbLayerName_Click(object sender, EventArgs e)
        {
            tbLayerName.BackColor = Color.White;
        }

        private void tbLayerName_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                tbLayerName.BackColor = Color.LightGray;
                sLayerName = tbLayerName.Text;
            }
        }

        private void tbLayerName_TextChanged(object sender, EventArgs e)
        {
            tbLayerName.BackColor = Color.White;
            sLayerName = tbLayerName.Text;
        }
    }
}
