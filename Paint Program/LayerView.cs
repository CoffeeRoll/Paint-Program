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
            Layers = new List<LayerItem>();
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
            LayerItem temp = new LayerItem(width, height, pf);
            temp.Location = new Point(0, yLayerLocation);
            yLayerLocation += temp.Height + 5;
            Console.WriteLine("Y:" + yLayerLocation);
            Layers.Add(temp);
            pLayerDisplay.Controls.Add(Layers[Layers.Count - 1]);

            if (Layers.Count > 1)
            {
                bRemoveLayer.Enabled = true;
            }
        }

        private void bRemoveLayer_Click(object sender, EventArgs e)
        {
            //Foreach loop to iterate through all layers
            try {
                for(int t = Layers.Count-1; t >=0; t--) {
                    if (Layers.Count <= 1)
                    {
                        break;
                    }
                    if (Layers[t].isLayerSelected())
                    {
                        yLayerLocation = Layers[t].Location.Y;
                        Console.WriteLine("Y: " + yLayerLocation);

                        //removes the LayerItem from the Display
                        pLayerDisplay.Controls.Remove(Layers[t]);

                        //Removes selected layer from the List
                        Layers.Remove(Layers[t]);
                        
                    }
                }
            }catch(Exception err)
            {
                Console.WriteLine(err.InnerException);
            }
            foreach (LayerItem layer in Layers)
            {
                if (layer.Location.Y > yLayerLocation)
                {
                    Console.WriteLine("Moved " + layer.Location.Y + " " + yLayerLocation);

                    layer.Location = new Point(0, yLayerLocation);

                    yLayerLocation += layer.Height + 5;

                    Console.WriteLine(layer.Location.Y);

                }
            }
            pLayerDisplay.Update();

            //Disable the Remove Layer Button if only one ayer Exists
            bRemoveLayer.Enabled = (Layers.Count > 1);
        }
    }
}
