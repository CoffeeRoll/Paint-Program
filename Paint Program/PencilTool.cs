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
        private bool bActive, bMouseDown, bInit;

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
            bActive = false;
            bInit = true;
            bMouseDown = false;
        }

        public string getToolIconPath()
        {
            return @"..\..\Images\pencil.png";
        }

        public string getToolTip()
        {
            return "Pencil Tool";
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
                    graphics.DrawLine(new Pen(settings.getPrimaryBrushColor()), pOld, pNew);
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
