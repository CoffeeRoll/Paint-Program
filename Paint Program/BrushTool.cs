using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Paint_Program
{
    class BrushTool : ITool
    {
        private Graphics graphics;
        private int width, height;
        private bool bMouseDown, bInit;

        private Point pOld, pNew;
        
        private Pen pen;

        private Color primaryColor, secondaryColor;

        public BrushTool()
        {

        }

        private void updateBrush()
        {
            int R = SharedSettings.getPrimaryBrushColor().R;
            int G = SharedSettings.getPrimaryBrushColor().G;
            int B = SharedSettings.getPrimaryBrushColor().B;

            primaryColor = Color.FromArgb(SharedSettings.getBrushHardness(), R, G, B);

            R = SharedSettings.getSecondaryBrushColor().R;
            G = SharedSettings.getSecondaryBrushColor().G;
            B = SharedSettings.getSecondaryBrushColor().B;

            secondaryColor = Color.FromArgb(SharedSettings.getBrushHardness(), R, G, B);
            
        }

        public void init()
        {
            graphics = SharedSettings.getActiveGraphics();
            width = SharedSettings.getCanvasWidth();
            height = SharedSettings.getCanvasHeight();
            bInit = true;
            bMouseDown = false;

            pOld = pNew = new Point(-1, -1);

            updateBrush();
            pen = new Pen(primaryColor, SharedSettings.getBrushSize() / 2);

            pen.SetLineCap(LineCap.Round, LineCap.Round, DashCap.Round);

            if (graphics != null)
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
            }

            updateBrush();

        }

        public string getToolIconPath()
        {
            return @"..\..\Images\brush.png";
        }

        public void onMouseDown(object sender, MouseEventArgs e)
        {
            if (graphics != null)
            {
                bMouseDown = true;
                pOld = e.Location;
            }
            updateBrush();
        }

        public void onMouseMove(object sender, MouseEventArgs e)
        {
            if (graphics != null && bMouseDown)
            {
                pNew = e.Location;
                if (SharedSettings.getTabletPressure() > 0)
                {
                    double pressure = Math.Pow(2.0, SharedSettings.MapDouble(0, SharedSettings.iMaxTabletPressure, 0.0, 6.0, SharedSettings.iTabletPressure));
                    Console.WriteLine(pressure);

                    if (pressure >= 0)
                    {
                        pen.Width = (float) pressure;
                    }
                }
                else
                {
                    pen.Width = SharedSettings.getBrushSize() / 2;
                }

                switch (e.Button)
                {
                    case MouseButtons.Left:
                        pen.Color = primaryColor;
                        graphics.DrawLine(pen, pOld, pNew);
                        break;
                    case MouseButtons.Right:
                        pen.Color = secondaryColor;
                        graphics.DrawLine(pen, pOld, pNew);
                        break;
                    default:
                        break;
                
                }
                pOld = pNew;

            }
        }

        public void onMouseUp(object sender, MouseEventArgs e)
        {
            if (graphics != null)
            {
                bMouseDown = false;
                pNew = e.Location;

                switch (e.Button)
                {
                    case MouseButtons.Left:
                        pen.Color = primaryColor;
                        pen.Width = SharedSettings.getBrushSize() / 2;
                        graphics.DrawLine(pen, pOld, pNew);
                        break;
                    case MouseButtons.Right:
                        pen.Color = secondaryColor;
                        pen.Width = SharedSettings.getBrushSize() / 2;
                        graphics.DrawLine(pen, pOld, pNew);
                        break;
                    default:
                        break;
                }

                pOld = pNew;
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
            return false;
        }

        public void setLayerData(Bitmap bit)
        {
        }

        public string getToolTip()
        {
            return SharedSettings.getGlobalString("tooltip_brush");
        }

        public void updateInterfaceLayer()
        {
        }
    }
}
