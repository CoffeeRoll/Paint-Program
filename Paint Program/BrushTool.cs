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
    class BrushTool : ITool
    {
        private Graphics graphics;
        private int width, height, numPoints;
        private SharedSettings settings;
        private bool bActive, bMouseDown, bInit;

        private Point pOld, pMid, pNew;

        private Brush pPrime, pSec;

        private RectangleF rect;

        public BrushTool()
        {

        }

        private void updateBrush()
        {
            int R = settings.getPrimaryBrushColor().R;
            int G = settings.getPrimaryBrushColor().G;
            int B = settings.getPrimaryBrushColor().B;

            pPrime = new SolidBrush(Color.FromArgb(settings.getBrushHardness(), R, G, B));
            //pPrime.LineJoin = LineJoin.Round;
            //pPrime.MiterLimit = pPrime.Width;

            R = settings.getSecondaryBrushColor().R;
            G = settings.getSecondaryBrushColor().G;
            B = settings.getSecondaryBrushColor().B;

            pSec = new SolidBrush(Color.FromArgb(settings.getBrushHardness(), R, G, B));
            
            //pSec.LineJoin = LineJoin.Round;
        }

        public void init(Graphics g, int w, int h, SharedSettings s)
        {
            graphics = g;
            width = w;
            height = h;
            settings = s;
            bActive = false;
            bInit = true;
            bMouseDown = false;

            pOld = pMid = pNew = new Point(-1,-1);
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
                rect.Width = settings.getBrushSize() / 2;
                rect.Height = settings.getBrushSize() / 2;
                rect.X = pOld.X / 2;
                rect.Y = pOld.Y / 2;
            }
        }

        public void onMouseMove(object sender, MouseEventArgs e)
        {
            if (graphics != null && bMouseDown)
            {
                pNew = e.Location;
                rect.X = pNew.X;
                rect.Y = pNew.Y;
                numPoints = 2;
                int pressure = -1;
                if(settings.getTabletPressure() >= 0)
                {
                    pressure = SharedSettings.MapValue(0, settings.getMaxTabletPressure(), settings.getMinTabletWidth(), settings.getMaxTabletWidth(), settings.getTabletPressure());
                }
                if(numPoints == 2)
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        if (pressure >= 0)
                        {
                            rect.Width = pressure / 2;
                            rect.Height = pressure / 2;
                        }
                        graphics.FillEllipse(pPrime, rect);
                    }
                    else if (e.Button == MouseButtons.Right)
                    {
                        if (pressure >= 0)
                        {
                            rect.Width = pressure / 2;
                            rect.Height = pressure / 2;
                        }
                        graphics.FillEllipse(pSec, rect);
                    }
                    pMid = pNew;
                    numPoints = 3;
                }
                else if (numPoints == 3)
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        if (pressure >= 0)
                        {
                            rect.Width = pressure / 2;
                            rect.Height = pressure / 2;
                        }
                        // graphics.DrawLine(pPrime, pOld, pNew);
                        graphics.FillEllipse(pPrime, rect);
                    }
                    else if (e.Button == MouseButtons.Right)
                    {
                        if (pressure >= 0)
                        {
                            rect.Width = pressure / 2;
                            rect.Height = pressure / 2;
                        }
                        graphics.FillEllipse(pSec, rect);
                    }
                }

                pOld = pMid;
                pMid = pNew;
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
