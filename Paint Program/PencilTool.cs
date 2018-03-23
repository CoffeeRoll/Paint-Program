using System.Drawing;
using System.Windows.Forms;

namespace Paint_Program
{
    class PencilTool : ITool
    {
        private Graphics graphics;
        private int width, height;
        private bool bMouseDown, bInit;

        Color cPrime, cSec;

        private Point pOld, pNew;

        public PencilTool()
        {

        }

        public void Init()
        {
            graphics = SharedSettings.getActiveGraphics();
            width = SharedSettings.getCanvasWidth();
            height = SharedSettings.getCanvasHeight();
            bInit = true;
            bMouseDown = false;
            updateColors();

            if (graphics != null)
            {
                graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
            }
        }

        public string GetToolIconPath()
        {
            return @"..\..\Images\pencil.png";
        }

        private void updateColors()
        {
            int R = SharedSettings.getPrimaryBrushColor().R;
            int G = SharedSettings.getPrimaryBrushColor().G;
            int B = SharedSettings.getPrimaryBrushColor().B;

            cPrime = Color.FromArgb(SharedSettings.getBrushHardness(), R, G, B);


            R = SharedSettings.getSecondaryBrushColor().R;
            G = SharedSettings.getSecondaryBrushColor().G;
            B = SharedSettings.getSecondaryBrushColor().B;

            cSec = Color.FromArgb(SharedSettings.getBrushHardness(), R, G, B);

            //pSec.LineJoin = LineJoin.Round;
        }

        public void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (graphics != null)
            {
                bMouseDown = true;
                pOld = e.Location;
                Brush temp;
                if (e.Button == MouseButtons.Left)
                {
                    temp = new SolidBrush(cPrime);
                    graphics.FillRectangle(temp, e.X, e.Y, 1, 1);
                }
                else if(e.Button == MouseButtons.Right)
                {
                    int R = SharedSettings.getSecondaryBrushColor().R;
                    int G = SharedSettings.getSecondaryBrushColor().G;
                    int B = SharedSettings.getSecondaryBrushColor().B;

                    temp = new SolidBrush(cSec);
                    graphics.FillRectangle(temp, e.X, e.Y, 1, 1);
                }
            }
        }

        public void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (graphics != null && bMouseDown)
            {
                if (e.Button == MouseButtons.Left)
                {
                    pNew = e.Location;
                    graphics.DrawLine(new Pen(cPrime), pOld, pNew);
                    pOld = pNew;
                }
                else
                {
                    pNew = e.Location;
                    graphics.DrawLine(new Pen(cSec), pOld, pNew);
                    pOld = pNew;
                }
            }
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
            return false;
        }

        public void SetLayerData(Bitmap bit)
        {
        }

        public string GetToolTip()
        {
            return SharedSettings.getGlobalString("tooltip_pencil");
        }

        public void UpdateInterfaceLayer()
        {
        }
    }
}
