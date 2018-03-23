using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Paint_Program
{
    class EraserTool : ITool
    {
        private Graphics graphics;
        private int width, height;
        private bool bMouseDown, bInit;
        private Pen eraser;

        private Point pOld, pNew;

        public Bitmap getCanvas()
        {
            return new Bitmap(width, height, graphics);
        }

        public string GetToolIconPath()
        {
            return @"..\..\Images\erasoir.png";
        }

        public void Init()
        {
            graphics = SharedSettings.getActiveGraphics();
            width = SharedSettings.getCanvasWidth();
            height = SharedSettings.getCanvasHeight();
            bInit = true;
            bMouseDown = false;

            eraser = new Pen(Color.Transparent);
            eraser.Width = SharedSettings.getBrushSize() / 2;
            eraser.SetLineCap(LineCap.Round, LineCap.Round, DashCap.Round);

            if (graphics != null)
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
            }
        }

        public bool isInitalized()
        {
            return bInit;
        }

        public void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (graphics != null)
            {
                bMouseDown = true;
                pOld = e.Location;
            }
        }

        public void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (graphics != null && bMouseDown)
            {
                if (e.Button == MouseButtons.Left)
                {
                    pNew = e.Location;

                    if (SharedSettings.getTabletPressure() == -1)
                    {
                        graphics.DrawLine(eraser, pOld, pNew);
                    }else if(SharedSettings.getTabletPressure() >= 0)
                    {
                        eraser.Width = (float) Math.Pow(2.0, SharedSettings.MapDouble(0, SharedSettings.iMaxTabletPressure, 0.0, 6.0, SharedSettings.iTabletPressure));
                        graphics.DrawLine(eraser, pOld, pNew);
                    }
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

        public Bitmap GetToolLayer()
        {
            throw new NotImplementedException();
        }

        public bool RequiresLayerData()
        {
            return false;
        }

        public void SetLayerData(Bitmap bit)
        {
            throw new NotImplementedException();
        }

        public string GetToolTip()
        {
            return SharedSettings.getGlobalString("tooltip_eraser");
        }

        public void UpdateInterfaceLayer()
        {
        }
    }
}
