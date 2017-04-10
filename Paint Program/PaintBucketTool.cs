using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Paint_Program
{
    class PaintBucketTool : ITool
    {
        private Graphics graphics;
        private int width, height;
        private SharedSettings settings;
        private bool bActive, bMouseDown, bInit;
        private Color replacementColor, targetColor;
        private bool[,] state;
        private Queue<Point> nextPixels;
        private Bitmap bmp;


        private Point pOld, pNew;

        public bool ColorMatch(Color a, Color b)
        {
            return (a.ToArgb() == b.ToArgb());
        }

        public void AddNextPixel(Point p)
        {
            if(!state[p.X,p.Y])
            {
                nextPixels.Enqueue(p);
                state[p.X,p.Y] = true;
            }
        }

        public PaintBucketTool()
        {
        }

        public void init(SharedSettings s)
        {
            graphics = s.getActiveGraphics();
            width = s.getCanvasWidth();
            height = s.getCanvasHeight();
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

        public void onMouseDown(object sender, MouseEventArgs e)
        {
            if(graphics != null)
            {
                bMouseDown = true;
                pOld = e.Location;
                int x = pOld.X;
                int y = pOld.Y;

                bmp = settings.getBitmapCurrentLayer(true);
                state = new bool[bmp.Width, bmp.Height];
                if (bmp == null)
                    return;

                nextPixels = new Queue<Point>();

                switch(e.Button)
                {
                    case MouseButtons.Left:
                        replacementColor = settings.getPrimaryBrushColor();
                        break;
                    case MouseButtons.Right:
                        replacementColor = settings.getSecondaryBrushColor();
                        break;
                    default:
                        break;
                }

                targetColor = bmp.GetPixel(x, y);

                if (targetColor.ToArgb() == replacementColor.ToArgb()) return;

                AddNextPixel(pOld);
                while(nextPixels.Count > 0) // while the queue is not empty
                {
                    Point p = nextPixels.Dequeue();
                    if(ColorMatch(bmp.GetPixel(p.X, p.Y), targetColor))
                    {
                        // add 8 surrounding pixels to queue
                        int xNew = p.X, yNew = p.Y;

                        // add 3 pixels above
                        if(yNew - 1 >= 0)
                        {
                            if (xNew + 1 < bmp.Width)
                                AddNextPixel(new Point(xNew + 1, yNew - 1));
                            if (xNew - 1 >= 0)
                                AddNextPixel(new Point(xNew - 1, yNew - 1));
                            AddNextPixel(new Point(xNew, yNew - 1));
                        }

                        // add left and right
                        if (xNew > 0)
                            AddNextPixel(new Point(xNew - 1, yNew));
                        if (xNew + 1 < bmp.Width)
                            AddNextPixel(new Point(xNew + 1, yNew));

                        // add 3 upixels below
                        if(yNew + 1 < bmp.Height)
                        {
                            if (xNew + 1 < bmp.Width)
                                AddNextPixel(new Point(xNew + 1, yNew + 1));
                            if (xNew - 1 >= 0)
                                AddNextPixel(new Point(xNew - 1, yNew + 1));
                            AddNextPixel(new Point(xNew, yNew + 1));
                        }

                        bmp.SetPixel(xNew, yNew, replacementColor); // replace the pixel color
                    }
                    else
                    {
                        continue;
                    }
                }

                nextPixels.Clear();
                return;
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

        public string getToolTip()
        {
            return SharedSettings.getGlobalString("tooltip_bucket");
        }

        public void updateInterfaceLayer()
        {
        }
    }
}
