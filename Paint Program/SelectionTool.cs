using System;
using System.Drawing;
using System.Windows.Forms;

namespace Paint_Program
{
    class SelectionTool : ITool
    {
        bool isInit = false;

        Point p1, p2;

        Point pOld, pNew;

        Bitmap bEdit;

        public void init()
        {
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

        public bool isInitalized()
        {
            return isInit;
        }

        public void onMouseDown(object sender, MouseEventArgs e)
        {
            p1 = new Point(e.X, e.Y);
            pOld = e.Location;
        }

        public void onMouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                pNew = e.Location;
            }
        }

        public void onMouseUp(object sender, MouseEventArgs e)
        {
            pOld.X = 0;
            pOld.Y = 0;
            pNew.X = 0;
            pNew.Y = 0;

            p2 = new Point(e.X, e.Y);

            if (p1.X == p2.X || p1.Y == p2.Y)
            {
                SharedSettings.flattenSelection();
            }
            else
            {
                if (!SharedSettings.getActiveSelection())
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

                    tlX = tlX < 0 ? 0 : tlX;
                    tlY = tlY < 0 ? 0 : tlY;
                    width = width + tlX <= SharedSettings.iCanvasWidth ? width : SharedSettings.iCanvasWidth - tlX;
                    height = height + tlY <= SharedSettings.iCanvasHeight ? height : SharedSettings.iCanvasHeight - tlY;

                    Point loc = new Point(tlX, tlY);
                    Size sze = new Size(width, height);
					SharedSettings.setSelectionPoint(loc);
					SharedSettings.setSelectionSize(sze);
                    
                    updateInterfaceLayer();

                    //Get selected area data
                    bEdit = SharedSettings.getBitmapCurrentLayer(true).Clone(new Rectangle(loc, sze), SharedSettings.getBitmapCurrentLayer(true).PixelFormat);

					//clear data below selection
					SharedSettings.getActiveGraphics().CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
					SharedSettings.getActiveGraphics().FillRectangle(new SolidBrush(Color.Transparent), new Rectangle(loc, sze));
					SharedSettings.getActiveGraphics().CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;

                    SharedSettings.setSelection(bEdit, loc);
                }
            }
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
            return SharedSettings.getGlobalString("tooltip_selection");
        }

        public void Clean()
        {
            if (bEdit != null)
            {
                bEdit.Dispose();
            }
        }

        public void updateInterfaceLayer()
        {
            Bitmap temp = new Bitmap(SharedSettings.getCanvasWidth(), SharedSettings.getCanvasHeight());

			SharedSettings.setSelectionPoint(new Point(SharedSettings.getSelectionPoint().X, SharedSettings.getSelectionPoint().Y));

            Pen p = new Pen(Color.Black);
            p.DashPattern = new float[] { 3.0F, 3.0F };
            p.DashCap = System.Drawing.Drawing2D.DashCap.Flat;

            Graphics tmpGr = Graphics.FromImage(temp);
            tmpGr.DrawRectangle(p, new Rectangle(SharedSettings.pSelectionPoint, SharedSettings.sSelectionSize));
			SharedSettings.setInterfaceBitmap(temp);
        }

    }
}
