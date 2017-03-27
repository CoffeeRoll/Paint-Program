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
    class ErasoirTool : ITool
    {
        private Graphics graphics;
        private int width, height;
        private SharedSettings settings;
        private bool bMouseDown, bInit;
        private Pen eraser;

        private Point pOld, pNew;

        public Bitmap getCanvas()
        {
            return new Bitmap(width, height, graphics);
        }

        public string getToolIconPath()
        {
            return @"..\..\Images\erasoir.png";
        }

        public void init(SharedSettings s)
        {
            graphics = s.getActiveGraphics();
            width = s.getCanvasWidth();
            height = s.getCanvasHeight();
            settings = s;
            bInit = true;
            bMouseDown = false;


            eraser = new Pen(Color.Transparent);
            eraser.Width = settings.getBrushSize() / 2;
            eraser.SetLineCap(LineCap.Round, LineCap.Round, DashCap.Round);

            if (graphics != null)
            {
                graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
            }
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

                    if (settings.getTabletPressure() == -1)
                    {
                        graphics.DrawLine(eraser, pOld, pNew);
                    }else if(settings.getTabletPressure() >= 0)
                    {
                        eraser.Width = SharedSettings.MapValue(0, settings.getMaxTabletPressure(), settings.getMinTabletWidth(), settings.getMaxTabletWidth(), settings.getTabletPressure());
                        graphics.DrawLine(eraser, pOld, pNew);
                    }
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
