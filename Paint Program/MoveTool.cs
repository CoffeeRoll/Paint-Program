using System.Drawing;
using System.Windows.Forms;

namespace Paint_Program
{
    class MoveTool:ITool
    {
        private Graphics graphics;
        private int width, height;
        private bool bInit;

        private Point pOld, pNew;
        

        public MoveTool()
        {

        }

        public void Init()
        {
            graphics = SharedSettings.getActiveGraphics();
            width = SharedSettings.getCanvasWidth();
            height = SharedSettings.getCanvasHeight();
            bInit = true;
        }

        public string GetToolIconPath()
        {
            return @"..\..\Images\move.png";
        }

        public void OnMouseDown(object sender, MouseEventArgs e)
        {
            pOld = e.Location;
        }

        public void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                pNew = e.Location;
                Point p = SharedSettings.pSelectionPoint;
                SharedSettings.pSelectionPoint = new Point(p.X + (pNew.X - pOld.X), p.Y + (pNew.Y - pOld.Y));
            }
        }

        public void OnMouseUp(object sender, MouseEventArgs e)
        {
            pOld.X = 0;
            pOld.Y = 0;
            pNew.X = 0;
            pNew.Y = 0;
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
            return SharedSettings.getGlobalString("tooltip_move");
        }

        public void UpdateInterfaceLayer()
        {
        }
    }
}
