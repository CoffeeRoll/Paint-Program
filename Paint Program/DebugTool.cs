using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Paint_Program
{
	/// <summary>
	/// Debug class, please ignore
	/// </summary>
    class DebugTool
    {
        private Graphics graphics;
        private int width, height, numPoints;
        private bool bMouseDown, bInit;

        private Point pOld, pNew;

        private Pen pPrime, pSec;

        List<Point> points;

        public DebugTool()
        {
            
        }

        //Update Function
        private void updateBrush()
        {
            int R = SharedSettings.getPrimaryBrushColor().R;
            int G = SharedSettings.getPrimaryBrushColor().G;
            int B = SharedSettings.getPrimaryBrushColor().B;

            pPrime = new Pen(Color.FromArgb(SharedSettings.getBrushHardness(), R, G, B), SharedSettings.getBrushSize());
            pPrime.LineJoin = LineJoin.Round;
            pPrime.MiterLimit = pPrime.Width;

            R = SharedSettings.getSecondaryBrushColor().R;
            G = SharedSettings.getSecondaryBrushColor().G;
            B = SharedSettings.getSecondaryBrushColor().B;

            pSec = new Pen(Color.FromArgb(SharedSettings.getBrushHardness(), R, G, B), SharedSettings.getBrushSize());
            pSec.LineJoin = LineJoin.Round;
        }

        public void init(Graphics g, int w, int h)
        {
            graphics = g;
            width = w;
            height = h;
            bInit = true;
            bMouseDown = false;

            points = new List<Point>();

            pOld = pNew = new Point(-1, -1);
            numPoints = 0;

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
                numPoints = 1;
            }
        }

        public void onMouseMove(object sender, MouseEventArgs e)
        {
            if (graphics != null && bMouseDown)
            {
                pNew = e.Location;
                
                if(pOld.X != -1 && pOld.Y != -1)
                {
                    double slope = (double)(pNew.Y - pOld.Y) / (double)(pNew.X - pOld.X);
                    double invSlope = -1.0 / slope; //y = mx + b
                    double halfW = pPrime.Width / 2.0;



                }

                pOld = pNew;
            }
        }

        public void onMouseUp(object sender, MouseEventArgs e)
        {
            if (graphics != null)
            {
                bMouseDown = false;
            }
            numPoints = 0;
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
    }
}
