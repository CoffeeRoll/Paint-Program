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
        private int yLayerLocation;

        public LayerView(int w, int h)
        {
            InitializeComponent();
            width = w;
            height = h;
            yLayerLocation = 0;
            LayerItem setup = new LayerItem(w, h, pf);
            this.width = setup.Width;
            this.height = 600;
            Layers = new List<LayerItem>();
            addLayer();
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

        public Bitmap getActiveLayerBitmap()
        {
            //Searches for the active layer
            foreach(LayerItem layer in Layers)
            {
                if (layer.isLayerActive())
                {
                    return layer.getBitmap();
                }
            }

            //No Active Layer
            return null;
        }

        private void bAddLayer_Click(object sender, EventArgs e)
        {
            addLayer();
        }

        private void bRemoveLayer_Click(object sender, EventArgs e)
        {
            removeLayer();
        }

        private void addLayer()
        {

            foreach(LayerItem layer in Layers)
            {
                layer.setActive(false);
            }

            LayerItem temp = new LayerItem(width, height, pf);
            temp.Location = new Point(0, yLayerLocation);
            yLayerLocation += temp.Height + 5;
            temp.setActive(true);
            temp.setOnClick(handleLayerItemClick);
            temp.setOnDoubleClick(handleLayerItemDoubleClick);
            temp.setActive(true);
            Layers.Add(temp);
            pLayerDisplay.Controls.Add(Layers[Layers.Count - 1]);

            if (Layers.Count > 1)
            {
                bRemoveLayer.Enabled = true;
            }

            redrawLayerItems();
        }

        private void removeLayer()
        {
            //Foreach loop to iterate through all layers
            try
            {
                for (int t = Layers.Count - 1; t >= 0; t--)
                {
                    if (Layers.Count <= 1)
                    {
                        break;
                    }
                    if (Layers[t].isLayerSelected())
                    {
                        //removes the LayerItem from the Display
                        pLayerDisplay.Controls.Remove(Layers[t]);

                        //Removes selected layer from the List
                        Layers.Remove(Layers[t]);
                    }
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err.InnerException);
            }
            redrawLayerItems();

            //Disable the Remove Layer Button if only one ayer Exists
            bRemoveLayer.Enabled = (Layers.Count > 1);
        }

        private void redrawLayerItems()
        {
            //reset index
            yLayerLocation = 0;

            //Redraws in reverse so most recent layer is always on top of the list
            for(int t = Layers.Count - 1; t >= 0; t--)
            {
                //adds AutoScrollPosition.Y to accomodate for vertical scrolling
                Layers[t].Location = new Point(0, yLayerLocation + pLayerDisplay.AutoScrollPosition.Y);
                yLayerLocation += Layers[t].Height + 5;
            }
        }

        private void handleLayerItemClick(object obj, System.EventArgs args)
        {
            LayerItem layer = ((LayerItem)obj);
            layer.setActive(!layer.isLayerActive());
        }

        private void handleLayerItemDoubleClick(object obj, System.EventArgs args)
        {
            LayerItem layer = ((LayerItem)obj);
            layer.setSelected(!layer.isLayerSelected());
        }
    }
}
