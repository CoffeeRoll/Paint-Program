using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint_Program
{
    class SelectionTool : ITool
    {
        bool isInit = false;
        SharedSettings ss;

        Point p1, p2;

        public void init(Graphics g, int w, int h, SharedSettings s)
        {
            ss = s;
            isInit = true;
        }


        public string getToolIconPath()
        {
            return @"..\..\Images\selector.png";
        }

        public Bitmap getToolLayer()
        {
            throw new NotImplementedException();
        }

        public string getToolTip()
        {
            return "Selector Tool";
        }

        public bool isInitalized()
        {
            return isInit;
        }

        public void onMouseDown(object sender, MouseEventArgs e)
        {
            p1 = new Point(e.X, e.Y);
        }

        public void onMouseMove(object sender, MouseEventArgs e)
        {
            
        }

        public void onMouseUp(object sender, MouseEventArgs e)
        {
            p2 = new Point(e.X, e.Y);

            if (p1 == p2)
            {
                ss.setSelectionPoint(new Point(-1, -1));
                ss.setSelectionSize(new Size(-1, -1));
                ss.setRenderBitmapInterface(false);
            }
            else
            {
                int tlX = p1.X, tlY = p1.Y;
                int width = Math.Abs(p1.X - p2.X);
                int height = Math.Abs(p1.Y - p2.Y);

                if (p2.X < p1.X)
                {
                    tlX = p2.X;
                }
                if (p2.Y < p1.Y)
                {
                    tlY = p2.Y;
                }

                ss.setSelectionPoint(new Point(tlX, tlY));
                ss.setSelectionSize(new Size(width, height));

                Bitmap temp = new Bitmap(ss.getCanvasWidth(), ss.getCanvasHeight());
                Pen p = new Pen(Color.Black);
                p.DashPattern = new float[] { 3.0F, 3.0F };
                p.DashCap = System.Drawing.Drawing2D.DashCap.Flat;
                Graphics.FromImage(temp).DrawRectangle(p, tlX, tlY, width, height);
                ss.setInterfaceBitmap(temp);
                ss.setRenderBitmapInterface(true);

            }
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
