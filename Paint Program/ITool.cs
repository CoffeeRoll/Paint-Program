using System.Drawing;
using System.Windows.Forms;

namespace Paint_Program
{
    interface ITool
    {
        /* Initializes the Graphics object that the tool manipulates, as well as the width and height */
        void Init();

        /* Returns a bitmap of the layer produced by the tool to show information not in the picture
           such as when the pen tool needs nodes to manipulate the curve
        */
        Bitmap GetToolLayer();

        /* Called when the mouse goes down */
        void OnMouseDown(object sender, MouseEventArgs e);

        /* Called when the mouse goes up */
        void OnMouseUp(object sender, MouseEventArgs e);

        /* Called when the mouse moves */
        void OnMouseMove(object sender, MouseEventArgs e);

        bool isInitalized();

        /* returns a bitmap that will be displayed in the ToolStrip */
        string GetToolIconPath();

        string GetToolTip();

        bool RequiresLayerData();

        void SetLayerData(Bitmap bit);

        void UpdateInterfaceLayer();

    }
}
