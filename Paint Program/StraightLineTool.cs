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
    class StraightLineTool : ITool
    {
        private Graphics graphics;
        private int width, height;
        private SharedSettings settings;
        private bool bMouseDown, bInit;

        private Point pOld, pNew;

        private Pen pen;

        private Color primaryColor, secondaryColor;

        public StraightLineTool()
        {

        }

        private void updateBrush()
        {
            int R = settings.getPrimaryBrushColor().R;
            int G = settings.getPrimaryBrushColor().G;
            int B = settings.getPrimaryBrushColor().B;

            primaryColor = Color.FromArgb(settings.getBrushHardness(), R, G, B);

            R = settings.getSecondaryBrushColor().R;
            G = settings.getSecondaryBrushColor().G;
            B = settings.getSecondaryBrushColor().B;

            secondaryColor = Color.FromArgb(settings.getBrushHardness(), R, G, B);

        }

        public void init(SharedSettings s)
        {
            graphics = s.getActiveGraphics();
            width = s.getCanvasWidth();
            height = s.getCanvasHeight();
            settings = s;
            bInit = true;
            bMouseDown = false;

            pOld = pNew = new Point(-1, -1);

            updateBrush();
            pen = new Pen(primaryColor, settings.getBrushSize() / 2);

            pen.SetLineCap(LineCap.Round, LineCap.Round, DashCap.Round);

            if (graphics != null)
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
            }

            updateBrush();

        }

        public string getToolIconPath()
        {
            return @"..\..\Images\Line.png";
        }

        public void onMouseDown(object sender, MouseEventArgs e)
        {
            if (graphics != null)
            {
                bMouseDown = true;
                pOld = e.Location;
            }

            updateBrush();
        }

        public void onMouseMove(object sender, MouseEventArgs e)
        {
            //Shh! don't tell anyone I'm here... ;)
        }

        public void onMouseUp(object sender, MouseEventArgs e)
        {
            if (graphics != null)
            {
                bMouseDown = false;
                pNew = e.Location;

                switch (e.Button)
                {
                    //TODO: ADD TABLET PRESSURE
                    case MouseButtons.Left:
                        pen.Color = primaryColor;
                        pen.Width = settings.getBrushSize() / 2;
                        graphics.DrawLine(pen, pOld, pNew);
                        break;
                    case MouseButtons.Right:
                        pen.Color = secondaryColor;
                        pen.Width = settings.getBrushSize() / 2;
                        graphics.DrawLine(pen, pOld, pNew);
                        break;
                    default:
                        break;
                }

                pOld = pNew;
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
