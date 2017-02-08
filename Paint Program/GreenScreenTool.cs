﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint_Program
{
    class GreenScreenTool : ITool
    {
        private Graphics graphics;
        private int width, height;
        private SharedSettings settings;
        private bool bActive, bMouseDown, bInit;

        private Bitmap bLayer;

        public void init(SharedSettings s)
        {
            graphics = s.getActiveGraphics();
            width = s.getCanvasWidth();
            height = s.getCanvasHeight();
            settings = s;
            bActive = false;
            bInit = true;
            bMouseDown = false;
        }

        public Bitmap getCanvas()
        {
            //Not used
            return null;
        }

        public string getToolIconPath()
        {
            return @"..\..\Images\sampler.png";
        }

        public string getToolTip()
        {
            return "Green Screen";
        }

        public void onMouseDown(object sender, MouseEventArgs e)
        {
            if (settings.getBitmapCurrentLayer(true) != null)
            {
                Color c = settings.getBitmapCurrentLayer(true).GetPixel(e.X, e.Y);

                int tol = 3;//settings.getGreenScreenTolerance();

                if (e.Button == MouseButtons.Left)
                {
                    Bitmap Temp = settings.getBitmapCurrentLayer(false);

                    for (int a = -tol; a < tol; a++)
                    {
                        for (int r = -tol; r < tol; r++)
                        {
                            for (int g = -tol; g < tol; g++)
                            {
                                for (int b = -tol; b < tol; b++)
                                {
                                    if (!((c.R + r) < 0 || (c.G + g) < 0 || (c.B + b) < 0 || (c.R + r) > 255 || (c.G + g) > 255 || (c.B + b) > 255 || (c.A + a) < 0 || (c.A + a) > 255))
                                    {
                                        Color tmpColor = Color.FromArgb(c.R + r, c.G + g, c.B + b);
                                        Temp.MakeTransparent(tmpColor);
                                    }
                                }
                            }
                        }
                    }
                    
                    settings.setBitmapLayerUpdate(Temp);
                    
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
            return true;
        }

        public void setLayerData(Bitmap bit)
        {
            bLayer = bit;
        }
    }
}
