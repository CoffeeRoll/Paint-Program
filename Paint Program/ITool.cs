﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint_Program
{
    interface ITool
    {
        /* Initalizes the Graphics object that the tool manipulates, as well as the width and height */
        void init(Graphics g, int w, int h, SharedSettings s);

        /* Returns a bitmap of the canvas */
        Bitmap getCanvas();

        /* Called when the mouse goes down */
        void onMouseDown(object sender, MouseEventArgs e);

        /* Called when the mouse goes up */
        void onMouseUp(object sender, MouseEventArgs e);

        /* Called when the mouse moves */
        void onMouseMove(object sender, MouseEventArgs e);

        bool isInitalized();

        /* returns a string of the tool's tooltip */
        string getToolTip();

        /* returns a bitmap that will be displayed in the ToolStrip */
        string getToolIconPath();

    }
}