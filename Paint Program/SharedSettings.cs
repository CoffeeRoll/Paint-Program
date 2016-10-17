using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint_Program
{
    public class SharedSettings
    {
        public static Color cPrimaryBrushColor{ get; set; }

        public static Color cSecondaryBrushColor { get; set; }

        public static float fBrushSize { get; set; }

        public static int iBrushHardness { get; set; }

        public static int iTabletPressure { get; set; }

        public static int iCanvasWidth { get; set; }

        public static int iCanvasHeight { get; set; }

        public static Bitmap bitmapCanvas { get; set; }

        public SharedSettings()
        {
            cPrimaryBrushColor = Color.Black;

            cSecondaryBrushColor = Color.White;

            fBrushSize = 8.0F;

            iBrushHardness = 100;

            //No Tablet Input
            iTabletPressure = -1;
        }


        public void setPrimaryBrushColor(Color c)
        {
            cPrimaryBrushColor = c;
        }

        public void setSecondaryBrushColor(Color c)
        {
            cSecondaryBrushColor = c;
        }

        public void setBrushSize(float f)
        {
            fBrushSize = f;
        }

        public void setBrushHardness(int f)
        {
            iBrushHardness = f;
        }

        public void setTabletPressure(int p)
        {
            iTabletPressure = p;
        }

        public void setCanvasWidth(int w)
        {
            iCanvasWidth = w;
        }

        public void setCanvasHeight(int h)
        {
            iCanvasHeight = h;
        }

        public void setBitmapCanvas(Bitmap b)
        {
            bitmapCanvas = b;
        }

        public Color getPrimaryBrushColor()
        {
            return cPrimaryBrushColor;
        }

        public Color getSecondaryBrushColor()
        {
            return cSecondaryBrushColor;
        }

        public float getBrushSize()
        {
            return fBrushSize;
        }

        public int getBrushHardness()
        {
            return iBrushHardness;
        }

        public int getTabletPressure()
        {
            return iTabletPressure;
        }

        public int getCanvasWidth()
        {
            return iCanvasWidth;
        }

        public int getCanvasHeight()
        {
            return iCanvasHeight;
        }

        public Bitmap getBitmapCanvas()
        {
            return bitmapCanvas;
        }
    }
}
