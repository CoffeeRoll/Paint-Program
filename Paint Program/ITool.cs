using System;
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
        void init(SharedSettings s);

        /* Returns a bitmap of the layer produced by the tool to show information not in the picture
           such as when the pen tool needs nodes to maipulate the curve
        */
        Bitmap getToolLayer();

        /* Called when the mouse goes down */
        void onMouseDown(object sender, MouseEventArgs e);

        /* Called when the mouse goes up */
        void onMouseUp(object sender, MouseEventArgs e);

        /* Called when the mouse moves */
        void onMouseMove(object sender, MouseEventArgs e);

        bool isInitalized();

        /* returns a bitmap that will be displayed in the ToolStrip */
        string getToolIconPath();

        bool requiresLayerData();

        void setLayerData(Bitmap bit);

    }
}
