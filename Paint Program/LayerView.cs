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
    public partial class LayerView : UserControl
    {

        private List<LayerItem> Layers;
        private PixelFormat pf = PixelFormat.Format32bppArgb;
        private int width, height;

        public LayerView(int w, int h)
        {
            InitializeComponent();
            width = w;
            height = h;
            Layers = new List<LayerItem>();
            Layers.Add(new LayerItem(w, h, pf));
        }

        public void updateSelectedLayer()
        {
            //Foreach loop to iterate through all layers
            foreach (LayerItem layer in Layers)
            {
                if (layer.isLayerSelected())
                {
                    layer.updatePreview();
                    layer.Update();
                }
            }
        }

        public Bitmap getRender()
        {
            Bitmap bit = new Bitmap(width, height, pf);
            Graphics g = Graphics.FromImage(bit);

            for (int t = 0; t < Layers.Count; t++)
            {
                g.DrawImage(Layers[t].getBitmap(), new Point(0, 0));
            }

            return bit;
        }

        private void bAddLayer_Click(object sender, EventArgs e)
        {
            Layers.Add(new LayerItem(width, height, pf));
            pLayerDisplay.Controls.Add(Layers[Layers.Count - 1]);
        }

        private void bRemoveLayer_Click(object sender, EventArgs e)
        {
            //Foreach loop to iterate through all layers
            foreach(LayerItem layer in Layers) {
                if(Layers.Count <= 1)
                {
                    break;
                }
                if (layer.isLayerSelected())
                {
                    //Removes selected layer from the List
                    Layers.Remove(layer);

                    //removes the LayerItem from the Display
                    pLayerDisplay.Controls.Remove(layer);
                }
            }

            pLayerDisplay.Update();

            //Disable the Remove Layer Button if only one ayer Exists
            bRemoveLayer.Enabled = (Layers.Count > 1);
        }
    }
}
