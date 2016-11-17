using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint_Program
{
    class DebugTool
    {
        private Graphics graphics;
        private int width, height, numPoints;
        private SharedSettings settings;
        private bool bMouseDown, bInit;

        private Point pOld, pNew;

        private Pen pPrime, pSec;

        List<Point> points;

        public DebugTool()
        {
            
        }

        private void updateBrush()
        {
            int R = settings.getPrimaryBrushColor().R;
            int G = settings.getPrimaryBrushColor().G;
            int B = settings.getPrimaryBrushColor().B;

            pPrime = new Pen(Color.FromArgb(settings.getBrushHardness(), R, G, B), settings.getBrushSize());
            pPrime.LineJoin = LineJoin.Round;
            pPrime.MiterLimit = pPrime.Width;

            R = settings.getSecondaryBrushColor().R;
            G = settings.getSecondaryBrushColor().G;
            B = settings.getSecondaryBrushColor().B;

            pSec = new Pen(Color.FromArgb(settings.getBrushHardness(), R, G, B), settings.getBrushSize());
            pSec.LineJoin = LineJoin.Round;
        }

        public void init(Graphics g, int w, int h, SharedSettings s)
        {
            graphics = g;
            width = w;
            height = h;
            settings = s;
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

        public string getToolTip()
        {
            return "Brush Tool";
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
