using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint_Program
{
    class LayerManager
    {

        private List<Layer> Layers;
        private PixelFormat pf = PixelFormat.Format32bppArgb;
        private int width, height;


        public LayerManager(int w, int h)
        {
            width = w;
            height = h;
            Layers = new List<Layer>();
            Layers.Add(new Layer(w, h, pf));
        }

        public Bitmap getRender()
        {
            Bitmap bit = new Bitmap(width, height, pf);
            Graphics g = Graphics.FromImage(bit);

            for(int t = 0; t < Layers.Count; t++)
            {
                g.DrawImage(Layers[t].getBitmap(), new Point(0, 0));
            }

            return bit;
        }

        public void addLayer(Layer l)
        {
            Layers.Add(l);
        }

        public void removeLayer(Layer l)
        {
            Layers.Remove(l);
        }

    }
}
