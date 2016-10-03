using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint_Program
{
    class SharedSettings
    {
        public static Color cPrimaryBrushColor{ get; set; }

        public static Color cSecondaryBrushColor { get; set; }

        public static float fBrushSize { get; set; }


        public SharedSettings()
        {
            cPrimaryBrushColor = Color.Black;

            cSecondaryBrushColor = Color.White;

            fBrushSize = 1.0F;
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

    }
}
