using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint_Program
{
    class PaintBucketTool : ITool
    {
        private Graphics graphics;
        private int width, height;
        private SharedSettings settings;
        private bool bActive, bMouseDown, bInit;

        private Point pOld, pNew;

        public PaintBucketTool()
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

            if (graphics != null)
            {
                graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
            }
        }

        public string getToolIconPath()
        {
            return @"..\..\Images\bucket.png";
        }

        public string getToolTip()
        {
            return "Paint Bucket Tool";
        }

        public void onMouseDown(object sender, MouseEventArgs e)
        {
            if (graphics != null)
            {
                bMouseDown = true;
                pOld = e.Location;
                if (e.Button == MouseButtons.Left)
                {
                    Bitmap bmp = settings.getBitmapCurrentLayer(true);
                    Stack<Point> pixels = new Stack<Point>();
                    if (bmp == null)
                    {
                        return;
                    }

                    Color targetColor = bmp.GetPixel(pOld.X, pOld.Y);
                    Color replacementColor = settings.getPrimaryBrushColor();
                    pixels.Push(pOld);

                    while (pixels.Count > 0)
                    {
                        if (targetColor.ToArgb() == replacementColor.ToArgb()) break;

                        Point a = pixels.Pop();
                        if (a.X < bmp.Width && a.X >= 0 &&
                                a.Y < bmp.Height && a.Y >= 0)//make sure we stay within bounds
                        {

                            if (bmp.GetPixel(a.X, a.Y) == targetColor)
                            {
                                bmp.SetPixel(a.X, a.Y, replacementColor);
                                pixels.Push(new Point(a.X - 1, a.Y));
                                pixels.Push(new Point(a.X + 1, a.Y));
                                pixels.Push(new Point(a.X, a.Y - 1));
                                pixels.Push(new Point(a.X, a.Y + 1));
                            }
                        }
                    }
                    pixels.Clear();
                    return;

                }

            }
        }

        public void onMouseMove(object sender, MouseEventArgs e)
        {

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
