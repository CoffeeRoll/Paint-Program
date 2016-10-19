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


            eraser = new Pen(Color.FromArgb(s.getBrushHardness(), 0, 0, 0));
            eraser.Width = settings.getBrushSize();

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
                        eraser.Color = Color.FromArgb(MapValue(0, 1023, 0, 255, settings.getTabletPressure()), 0, 0, 0);
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

        public static int MapValue(
    int originalStart, int originalEnd, // original range
    int newStart, int newEnd, // desired range
    int value) // value to convert
        {
            double scale = (double)(newEnd - newStart) / (originalEnd - originalStart);
            return (int)(newStart + ((value - originalStart) * scale));
        }
    }
}
