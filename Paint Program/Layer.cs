using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint_Program
{
    class Layer
    {
        private Bitmap LayerCanvas;
        private bool isVisible;

        public Layer(int w, int h, PixelFormat pf)
        {
            LayerCanvas = new Bitmap(w, h, pf);
            isVisible = true;
        }

        public Bitmap getBitmap()
        {
            return LayerCanvas;
        }

        public void setBitmap(Bitmap b)
        {
            LayerCanvas = b;
        }

        public void setVisibility(bool b)
        {
            isVisible = b;
        }

        public bool isLayerVisible()
        {
            return isVisible;
        }


    }
}
