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

        public void Init()
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

        public string GetToolIconPath()
        {
            return @"..\..\Images\sampler.png";
        }

        public void OnMouseDown(object sender, MouseEventArgs e)
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

        public void OnMouseMove(object sender, MouseEventArgs e)
        {

        }

        public void OnMouseUp(object sender, MouseEventArgs e)
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

        public Bitmap GetToolLayer()
        {
            return null;
        }

        public bool RequiresLayerData()
        {
            return true;
        }

        public void SetLayerData(Bitmap bit)
        {
        }

        public string GetToolTip()
        {
            return SharedSettings.getGlobalString("tooltip_colorselect");
        }

        public void UpdateInterfaceLayer()
        {
        }
    }
}
