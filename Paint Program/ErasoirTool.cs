using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint_Program
{
    class ErasoirTool : ITool
    {
        private Graphics graphics;
        private int width, height;
        private SharedSettings settings;
        private bool bActive, bMouseDown, bInit;

        private Point pOld, pNew;

        public Bitmap getCanvas()
        {
            return new Bitmap(width, height, graphics);
        }

        public string getToolIconPath()
        {
            return @"..\..\Images\erasoir";
        }

        public string getToolTip()
        {
            return "Erasoir Tool";
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
        }

        public bool isInitalized()
        {
            return bInit;
        }

        public void onMouseDown(object sender, MouseEventArgs e)
        {
            if (graphics != null)
            {
                bMouseDown = true;
                pOld = e.Location;
            }
        }

        public void onMouseMove(object sender, MouseEventArgs e)
        {
            if (graphics != null && bMouseDown)
            {
                if (e.Button == MouseButtons.Left)
                {
                    pNew = e.Location;
                    graphics.FillRectangle(new SolidBrush(Color.Transparent), new Rectangle(pOld, new System.Drawing.Size(width, height)));
                    pOld = pNew;
                }
                else
                {
                    pNew = e.Location;
                    graphics.DrawLine(new Pen(settings.getSecondaryBrushColor()), pOld, pNew);
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

        public Bitmap getToolLayer()
        {
            throw new NotImplementedException();
        }

        public bool requiresLayerData()
        {
            return false;
        }

        public void setLayerData(Bitmap bit)
        {
            throw new NotImplementedException();
        }
    }
}
