using System.Drawing;
using System.Windows.Forms;

namespace Paint_Program
{
    interface ITool
    {
        /* Initializes the Graphics object that the tool manipulates, as well as the width and height */
        void init();

        /* Returns a bitmap of the layer produced by the tool to show information not in the picture
           such as when the pen tool needs nodes to manipulate the curve
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

        string getToolTip();

        bool requiresLayerData();

        void setLayerData(Bitmap bit);

        void updateInterfaceLayer();

    }
}
