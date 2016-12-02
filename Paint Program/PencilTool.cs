using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint_Program
{
    class PencilTool : ITool
    {
        private Graphics graphics;
        private int width, height;
        private SharedSettings settings;
        private bool bMouseDown, bInit;

        Color cPrime, cSec;

        private Point pOld, pNew;

        public PencilTool()
        {

        }

        public void init(Graphics g, int w, int h, SharedSettings s)
        {
            graphics = g;
            width = w;
            height = h;
            settings = s;
            bInit = true;
            bMouseDown = false;
            updateColors();

            if (graphics != null)
            {
                graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
            }
        }

        public string getToolIconPath()
        {
            return @"..\..\Images\pencil.png";
        }

        public string getToolTip()
        {
            return "Pencil Tool";
        }

        private void updateColors()
        {
            int R = settings.getPrimaryBrushColor().R;
            int G = settings.getPrimaryBrushColor().G;
            int B = settings.getPrimaryBrushColor().B;

            cPrime = Color.FromArgb(settings.getBrushHardness(), R, G, B);


            R = settings.getSecondaryBrushColor().R;
            G = settings.getSecondaryBrushColor().G;
            B = settings.getSecondaryBrushColor().B;

            cSec = Color.FromArgb(settings.getBrushHardness(), R, G, B);

            //pSec.LineJoin = LineJoin.Round;
        }

        public void onMouseDown(object sender, MouseEventArgs e)
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
                    int R = settings.getSecondaryBrushColor().R;
                    int G = settings.getSecondaryBrushColor().G;
                    int B = settings.getSecondaryBrushColor().B;

                    temp = new SolidBrush(cSec);
                    graphics.FillRectangle(temp, e.X, e.Y, 1, 1);
                }
            }
        }

        public void onMouseMove(object sender, MouseEventArgs e)
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
            return false;
        }

        public void setLayerData(Bitmap bit)
        {
        }
    }
}
