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
    class DebugTool : ITool
    {
        private Graphics graphics;
        private int width, height;
        private SharedSettings settings;
        private bool bMouseDown, bInit;

        private Point pOld, pNew;

        private Pen pen;

        private TextureBrush brTexture;

        private Color primaryColor, secondaryColor;

        Bitmap TextureOG, texture1, texture2;

        int pr, pg, pb, sr, sg, sb;

        public DebugTool()
        {

        }

        private void updateBrush()
        {
            pr = settings.getPrimaryBrushColor().R;
            pg = settings.getPrimaryBrushColor().G;
            pb = settings.getPrimaryBrushColor().B;

            primaryColor = Color.FromArgb(settings.getBrushHardness(), pr, pg, pb);

            sr = settings.getSecondaryBrushColor().R;
            sg = settings.getSecondaryBrushColor().G;
            sb = settings.getSecondaryBrushColor().B;

            secondaryColor = Color.FromArgb(settings.getBrushHardness(), sr, sg, sb);

            updateBrushColor();

        }

        private void updateBrushColor()
        {
            for(int w = 0; w < texture1.Width; w++)
            {
                for(int h = 0; h < texture1.Height; h++)
                {
                    int v = texture1.GetPixel(w, h).A;
                    texture1.SetPixel(w, h, Color.FromArgb((int)(v * ((float)SharedSettings.iBrushHardness / 255.0)), pr, pg, pb));
                }
            }
            brTexture = new TextureBrush(texture1);
            pen = new Pen(brTexture);
            pen.SetLineCap(LineCap.Round, LineCap.Round, DashCap.Round);
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

            TextureOG = (Bitmap) Bitmap.FromFile(@"..\..\Brushes\stripes.png");
            texture1 = (Bitmap) TextureOG.Clone();
            texture2 = (Bitmap)TextureOG.Clone();
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
            return @"..\..\Images\brush.png";
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
            if (graphics != null && bMouseDown)
            {
                pNew = e.Location;
                int pressure = -1;
                if (settings.getTabletPressure() >= 0)
                {
                    pressure = SharedSettings.MapValue(0, settings.getMaxTabletPressure(), settings.getMinTabletWidth(), settings.getMaxTabletWidth(), settings.getTabletPressure());

                    if (pressure >= 0)
                    {
                        pen.Width = pressure / 2;
                    }
                }
                switch (e.Button)
                {
                    // TODO: Add tablet pressure back in...
                    case MouseButtons.Left:
                        //pen.Color = primaryColor;
                        pen.Width = settings.getBrushSize() / 2;
                        graphics.DrawLine(pen, pOld, pNew);
                        break;
                    case MouseButtons.Right:
                        //pen.Color = secondaryColor;
                        pen.Width = settings.getBrushSize() / 2;
                        graphics.DrawLine(pen, pOld, pNew);
                        break;
                    default:
                        break;
                }
                pOld = pNew;

            }
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
                        //pen.Color = primaryColor;
                        pen.Width = settings.getBrushSize() / 2;
                        graphics.DrawLine(pen, pOld, pNew);
                        break;
                    case MouseButtons.Right:
                        //pen.Color = secondaryColor;
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

        public string getToolTip()
        {
            return SharedSettings.getGlobalString("tooltip_brush");
        }

        public void updateInterfaceLayer()
        {
        }
    }
}
