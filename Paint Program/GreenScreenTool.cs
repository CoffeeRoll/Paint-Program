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
            return "Colored Alpha";
        }

        public void onMouseDown(object sender, MouseEventArgs e)
        {
            if (settings.getBitmapCurrentLayer(true) != null)
            {
                Color c = settings.getBitmapCurrentLayer(true).GetPixel(e.X, e.Y);

                if (e.Button == MouseButtons.Left)
                {
                    Bitmap Temp = settings.getBitmapCurrentLayer(true);
                    Temp.MakeTransparent(c);
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