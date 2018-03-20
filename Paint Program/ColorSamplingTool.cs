using System.Drawing;
using System.Windows.Forms;

namespace Paint_Program
{
    class ColorSamplingTool : ITool
    {

        private Graphics graphics;
        private int width, height;
        private bool bActive, bMouseDown, bInit;

        public ColorSamplingTool()
        {

        }

        public void init()
        {
            graphics = SharedSettings.getActiveGraphics();
            width = SharedSettings.getCanvasWidth();
            height = SharedSettings.getCanvasHeight();
            bActive = false;
            bInit = true;
            bMouseDown = false;
        }

        public Bitmap getCanvas()
        {
            //Not used
            return null;
        }

        public string getToolIconPath()
        {
            return @"..\..\Images\sampler.png";
        }

        public void onMouseDown(object sender, MouseEventArgs e)
        {
            if (graphics != null)
            {
                bMouseDown = true;
                Color c = SharedSettings.bitmapCurrentLayer.GetPixel(e.Location.X, e.Location.Y);

                if (e.Button == MouseButtons.Left) {
					SharedSettings.setPrimaryBrushColor(c);
                }
                else if(e.Button == MouseButtons.Right)
                {
					SharedSettings.setSecondaryBrushColor(c);
                }
            }
        }

        public void onMouseMove(object sender, MouseEventArgs e)
        {

        }

        public void onMouseUp(object sender, MouseEventArgs e)
        {
            if (graphics != null)
            {
                bMouseDown = false;

            }
        }

        public bool isInitalized()
        {
            return bInit;
        }

        public Bitmap getToolLayer()
        {
            return null;
        }

        public bool requiresLayerData()
        {
            return true;
        }

        public void setLayerData(Bitmap bit)
        {
        }

        public string getToolTip()
        {
            return SharedSettings.getGlobalString("tooltip_colorselect");
        }

        public void updateInterfaceLayer()
        {
        }
    }
}
